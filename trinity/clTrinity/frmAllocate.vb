Imports System.Threading
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class frmAllocate
    Private SkipIt As Boolean = False
    Dim strPricelistUpdate As String = ""

    'regular black text on white backgroud, D has one decimal, P is percent
    Dim styleNormalD As Windows.Forms.DataGridViewCellStyle
    Dim styleNormal As Windows.Forms.DataGridViewCellStyle
    Dim styleNormalP As Windows.Forms.DataGridViewCellStyle

    Dim styleNormalDLocked As Windows.Forms.DataGridViewCellStyle
    Dim styleNormalLocked As Windows.Forms.DataGridViewCellStyle


    Dim styleNoSet As Windows.Forms.DataGridViewCellStyle
    Dim styleNoSetD As Windows.Forms.DataGridViewCellStyle
    Dim styleNoSetLocked As Windows.Forms.DataGridViewCellStyle
    Dim styleNoSetDLocked As Windows.Forms.DataGridViewCellStyle
    Dim styleExceeded As Windows.Forms.DataGridViewCellStyle

    Dim styleCompensation As Windows.Forms.DataGridViewCellStyle
    Dim styleCompensationD As Windows.Forms.DataGridViewCellStyle

    Dim styleSpons As Windows.Forms.DataGridViewCellStyle
    Dim styleSponsD As Windows.Forms.DataGridViewCellStyle

    'greyed out since it cant be set
    Dim styleCantSetN As Windows.Forms.DataGridViewCellStyle
    Dim styleCantSetN0 As Windows.Forms.DataGridViewCellStyle
    Dim styleCantSetP As Windows.Forms.DataGridViewCellStyle


    'blue text on white
    Dim styleBlue As Windows.Forms.DataGridViewCellStyle

    'black text on red background
    Dim styleBackRed As Windows.Forms.DataGridViewCellStyle

    Dim Karma As Trinity.cKarma

    'Dim BLT As New Trinity.cBalloonToolTip

    Dim Thread1 As System.Threading.Thread

    Private lblFreqToCalculate As System.Windows.Forms.Label

    Private Enum DisplayModeEnum
        TRP
        TRP30
        PercentOfWeek
        PercentOfChannel
        Imp000
    End Enum


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        styleNormal = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleNormalD = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleNormalP = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleNoSet = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleNoSetD = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleNormalLocked = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleNormalDLocked = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleNoSetLocked = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleNoSetDLocked = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleCantSetN = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleCantSetN0 = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleCantSetP = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleBlue = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleBackRed = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleCompensation = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleCompensationD = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)

        'New'
        styleSpons = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleSponsD = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)

        styleExceeded = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleNormal.Format = "N0"
        styleNormalLocked.Format = "N0"
        styleNormalLocked.BackColor = Color.Khaki

        styleExceeded.Format = "N1"
        styleExceeded.Format = "N1"
        styleExceeded.BackColor = Color.Red

        styleNormalD.Format = "N1"
        styleNormalDLocked.Format = "N1"
        styleNormalDLocked.BackColor = Color.Khaki

        styleBackRed.BackColor = Color.Red
        styleBackRed.Font = New System.Drawing.Font("Segoe UI", 8.25)

        styleBlue.ForeColor = Color.Blue
        styleBlue.Font = New System.Drawing.Font("Segoe UI", 8.25)

        styleNoSet.ForeColor = Color.Maroon
        styleNoSet.Format = "N0"

        styleNoSetLocked.ForeColor = Color.Maroon
        styleNoSetLocked.Format = "N0"
        styleNoSetLocked.BackColor = Color.Khaki

        styleNoSetD.ForeColor = Color.Maroon
        styleNoSetD.Format = "N1"

        styleNoSetDLocked.ForeColor = Color.Maroon
        styleNoSetDLocked.Format = "N1"
        styleNoSetDLocked.BackColor = Color.Khaki

        styleCantSetN.ForeColor = Color.DimGray
        styleCantSetN.Format = "N1"

        styleCantSetN0.Format = "N0"
        styleCantSetN0.ForeColor = Color.DimGray

        styleCantSetP.ForeColor = Color.DimGray
        styleCantSetP.Format = "P1"

        styleNormalP.Format = "P0"

        styleCompensation.Format = "N0"
        styleCompensation.BackColor = Color.LightGray

        styleCompensationD.Format = "N1"
        styleCompensationD.BackColor = Color.LightGray

        lblExplain.ForeColor = Color.Maroon

        ' Fill the work on types in the budget window
        cmdBudgetType.Items.Add(BudgetType.GrossBudget)
        cmdBudgetType.Items.Add(BudgetType.NetBudget)

        cmdBudgetType.SelectedItem = BudgetType.NetBudget
    End Sub

    Private Sub frmAllocate_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        For Each cmd As Windows.Forms.Control In grpTRP.Controls
            If cmd.Name = "Notes" Then
                If cmd.Tag.GetType Is GetType(Trinity.cCombination) Then
                    If DirectCast(cmd.Tag, Trinity.cCombination).Relations(1).Bookingtype.Comments <> "" Then
                        cmd.BackColor = Color.Red
                    Else
                        cmd.BackColor = Color.White
                    End If
                Else
                    If DirectCast(cmd.Tag, Trinity.cBookingType).Comments <> "" Then
                        cmd.BackColor = Color.Red
                    Else
                        cmd.BackColor = Color.White
                    End If
                End If
            ElseIf cmd.Name = "Spots" Then
                If DirectCast(cmd.Tag, Trinity.cBookingType).SpecificSponsringPrograms.Count = 0 Then
                    cmd.BackColor = Color.Red
                Else
                    cmd.BackColor = Color.White
                End If
            End If

        Next
        'if no .ign is availabe then the lab is closed and the prodecure is ended
        If Campaign.Campaigns Is Nothing Then
            cmdLab.Enabled = False
        Else
            cmdLab.Enabled = (Campaign.Campaigns.Count > 0)
        End If
        'Populates lists with all the channel and bookingtype names to be accessed by their shortnames
        LongName.Clear()
        LongBT.Clear()
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            If Not LongName.ContainsKey(TmpChan.Shortname) Then
                LongName.Add(TmpChan.Shortname, TmpChan.ChannelName)
            Else
                MessageBox.Show(TmpChan.ChannelName & " has the short name " & TmpChan.Shortname & ". " & LongName(TmpChan.Shortname) & " is already using this. Please change it!")
                LongName.Add(TmpChan.Shortname & "x", TmpChan.ChannelName)
            End If
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                TmpBT.InvalidateCPPs()
                TmpBT.InvalidateTotalTRP()
                TmpBT.InvalidateActualNetValue()
            Next
        Next
        For i As Integer = 1 To Campaign.Channels(1).BookingTypes.Count
            If Not LongBT.ContainsKey(Campaign.Channels(1).BookingTypes(i).Shortname) Then
                LongBT.Add(Campaign.Channels(1).BookingTypes(i).Shortname, Campaign.Channels(1).BookingTypes(i).Name)
            End If
        Next
        'enables the lab button
        'Updates the grid containing 
        grdTRP.Invalidate()
        'summarize the budget
        grdBudget.Invalidate()
        'sumarize the TRP for channels
        grdSpotCount.Invalidate()
        grdSumChannels.Invalidate()
        grdDiscounts.Invalidate()
        chkIncludeInSums.Checked = TrinitySettings.IncludeCompensations

        grpCompensation.Visible = False
        For Each _chan As Trinity.cChannel In Campaign.Channels
            For Each _bt As Trinity.cBookingType In _chan.BookingTypes
                If _bt.Compensations.Count > 0 Then
                    grpCompensation.Visible = True
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub frmAllocate_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'This code cancels the form close if the pricelist check is not done
        'Note thet it will be impossible to close the form!

        If Thread1 IsNot Nothing AndAlso Thread1.ThreadState = ThreadState.Running Then
            e.Cancel = True
        End If
    End Sub

    Private Sub frmAllocate_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        'BLT.Destroy()
    End Sub

    Private Sub frmAllocate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Remove old labels
        Dim i As Integer
        For i = 0 To 1 'We need to to this twice. Why? No idea.
            For Each TmpControl As Windows.Forms.Control In Me.Controls
                If TmpControl.Name = "" Then
                    Me.Controls.Remove(TmpControl)
                    TmpControl.Dispose()
                End If
                Dim Controls As List(Of Control) = (From ctrl As Control In TmpControl.Controls Where ctrl.Name = "").ToList
                While Controls.Count > 0
                    For Each TmpCtrl As Control In Controls
                        TmpControl.Controls.Remove(TmpCtrl)
                        TmpCtrl.Dispose()
                    Next
                    Controls = (From ctrl As Control In TmpControl.Controls Where ctrl.Name = "").ToList
                End While
            Next
        Next

        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek
        Dim TmpAV As Trinity.cAddedValue
        Dim j As Integer
        Dim lblBuyingTarget As System.Windows.Forms.Label = Nothing

        'clears the TRP grid
        grdTRP.Columns.Clear()
        grdTRP.Rows.Clear()
        grdBudget.Rows.Clear()
        'clears the budget grid
        While grdBudget.Columns.Count > 2
            grdBudget.Columns.RemoveAt(2)
        End While
        'SumWeeks is the summarizing grid just beneath the TRP grid, it is cleared
        grdSumWeeks.Columns.Clear()
        grdSumWeeks.Rows.Clear()
        'SumChannels is a summarizing grid to the right of TRP grid. 
        grdSumChannels.Rows.Clear()
        'clears the grid for Added Values
        grdAV.Columns.Clear()
        grdAV.Rows.Clear()
        'clears the discount grid
        grdDiscounts.Columns.Clear()
        grdDiscounts.Columns.Clear()
        'clears the commercial films grid
        grdFilms.Columns.Clear()
        grdFilms.Rows.Clear()
        'clears the grid containing the Target ranges
        grdIndex.Rows.Clear()
        'a small grid summirinzing Channels/weeks
        grdGrandSum.Rows.Clear()
        'clear the combo box containing all channels with films
        cmbFilmChannel.Items.Clear()

        grdSpotCount.Rows.Clear()
        grdTRP.ColumnHeadersDefaultCellStyle.Alignment = Windows.Forms.DataGridViewContentAlignment.TopRight
        grdSumChannels.DefaultCellStyle.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        grdBudget.ColumnHeadersDefaultCellStyle.Alignment = Windows.Forms.DataGridViewContentAlignment.TopRight
        colBudget.HeaderCell.Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter

        For Each tmpC As Trinity.cCombination In Campaign.Combinations
            'if showAsOne is true we need to display the contents as one in the window
            If tmpC.ShowAsOne AndAlso tmpC.Relations.count > 0 Then
                If grdTRP.Columns.Count = 0 Then
                    'Sets the properties for the TRP,SumWeek,AV,Discount,Budget and film grid
                    For Each TmpWeek In tmpC.Relations(1).Bookingtype.Weeks
                        grdTRP.Columns.Add("colWeek" & TmpWeek.Name, TmpWeek.Name)
                        grdTRP.Columns(grdTRP.Columns.Count - 1).Width = 60
                        grdTRP.Columns(grdTRP.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
                        grdSumWeeks.Columns.Add("colSumWeek" & TmpWeek.Name, "")
                        grdSumWeeks.Columns(grdSumWeeks.Columns.Count - 1).Width = 60
                        grdSumWeeks.Columns(grdSumWeeks.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
                        grdAV.Columns.Add("colAV" & TmpWeek.Name, TmpWeek.Name)
                        grdAV.Columns(grdAV.Columns.Count - 1).Width = 60
                        grdAV.Columns(grdAV.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
                        grdDiscounts.Columns.Add("colDiscounts" & TmpWeek.Name, TmpWeek.Name)
                        grdDiscounts.Columns(grdDiscounts.Columns.Count - 1).Width = 60
                        grdDiscounts.Columns(grdDiscounts.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
                        grdBudget.Columns.Add("colBudgetWeek" & TmpWeek.Name, TmpWeek.Name)
                        grdBudget.Columns(grdBudget.Columns.Count - 1).Width = 60
                        grdBudget.Columns(grdBudget.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
                        grdFilms.Columns.Add("colFilmWeek" & TmpWeek.Name, TmpWeek.Name)
                        grdFilms.Columns(grdFilms.Columns.Count - 1).Width = 40
                        grdFilms.Columns(grdFilms.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
                    Next
                    'makes the "total" column in the end of the budget grid
                    grdBudget.Columns.Add("colBudgetSum", "Total")
                    grdBudget.Columns(grdBudget.Columns.Count - 1).Width = 60
                    grdBudget.Columns(grdBudget.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
                End If

                grdTRP.Rows.Add(2)
                grdTRP.Rows(grdTRP.Rows.Count - 1).Tag = tmpC
                grdTRP.Rows(grdTRP.Rows.Count - 2).Tag = tmpC
                grdDiscounts.Rows.Add(4)
                grdDiscounts.Rows(grdDiscounts.Rows.Count - 1).Tag = tmpC
                grdDiscounts.Rows(grdDiscounts.Rows.Count - 2).Tag = tmpC
                grdDiscounts.Rows(grdDiscounts.Rows.Count - 3).Tag = tmpC
                grdDiscounts.Rows(grdDiscounts.Rows.Count - 4).Tag = tmpC
                TmpBT = tmpC.Relations(1).Bookingtype
                grdBudget.Rows.Add()
                grdBudget.Rows(grdBudget.Rows.Count - 1).Tag = tmpC
                grdIndex.Rows.Add()
                '<Fredagsedits>
                'grdIndex.Rows(grdIndex.Rows.Count - 1).Tag = TmpBT
                grdIndex.Rows(grdIndex.Rows.Count - 1).Tag = tmpC
                grdIndex.Rows(grdIndex.Rows.Count - 1).Cells(0).Tag = tmpC
                grdSpotCount.Rows.Add()
                grdSpotCount.Rows(grdSpotCount.Rows.Count - 1).Tag = tmpC
                grdSpotCount.Rows(grdSpotCount.Rows.Count - 1).HeaderCell.Value = tmpC.Name

                'Add the Added values to its grid 'they should contain the same so we only check one
                For Each TmpAV In TmpBT.AddedValues
                    If Not TmpAV.ShowIn = Trinity.cAddedValue.ShowInEnum.siBooking Then
                        grdAV.Rows.Add()
                        grdAV.Rows(grdAV.Rows.Count - 1).Tag = TmpAV
                        grdAV.Rows(grdAV.Rows.Count - 1).Cells(0).Tag = tmpC
                    End If
                Next

                For i = 0 To TmpBT.Weeks.Count - 1
                    grdTRP.Rows(grdTRP.Rows.Count - 1).Cells(i).Tag = tmpC
                    grdTRP.Rows(grdTRP.Rows.Count - 2).Cells(i).Tag = tmpC
                    grdDiscounts.Rows(grdDiscounts.Rows.Count - 1).Cells(i).Tag = tmpC
                    grdDiscounts.Rows(grdDiscounts.Rows.Count - 2).Cells(i).Tag = tmpC
                    grdDiscounts.Rows(grdDiscounts.Rows.Count - 3).Cells(i).Tag = tmpC
                    grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(2 + i).Tag = TmpBT.Weeks(i + 1)
                Next
                grdSumChannels.Rows.Add(2)
                cmbFilmChannel.Items.Add(tmpC)
            ElseIf tmpC.ShowAsOne AndAlso tmpC.Relations.count = 0 Then
                Windows.Forms.MessageBox.Show(String.Format("The combination '{0}' has no channels in it and has been ignored.", tmpC.Name), "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Next

        'steps trough all the channels selected for the campaign and their bookings
        For Each TmpChan In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.BookIt AndAlso TmpBT.ShowMe Then
                    If grdTRP.Columns.Count = 0 Then
                        'Sets the properties for the TRP,SumWeek,AV,Discount,Budget and film grid
                        For Each TmpWeek In TmpBT.Weeks
                            grdTRP.Columns.Add("colWeek" & TmpWeek.Name, TmpWeek.Name)
                            grdTRP.Columns(grdTRP.Columns.Count - 1).Width = 60
                            grdTRP.Columns(grdTRP.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
                            grdSumWeeks.Columns.Add("colSumWeek" & TmpWeek.Name, "")
                            grdSumWeeks.Columns(grdSumWeeks.Columns.Count - 1).Width = 60
                            grdSumWeeks.Columns(grdSumWeeks.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
                            grdAV.Columns.Add("colAV" & TmpWeek.Name, TmpWeek.Name)
                            grdAV.Columns(grdAV.Columns.Count - 1).Width = 60
                            grdAV.Columns(grdAV.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
                            grdDiscounts.Columns.Add("colDiscounts" & TmpWeek.Name, TmpWeek.Name)
                            grdDiscounts.Columns(grdDiscounts.Columns.Count - 1).Width = 60
                            grdDiscounts.Columns(grdDiscounts.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
                            grdBudget.Columns.Add("colBudgetWeek" & TmpWeek.Name, TmpWeek.Name)
                            grdBudget.Columns(grdBudget.Columns.Count - 1).Width = 60
                            grdBudget.Columns(grdBudget.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
                            grdFilms.Columns.Add("colFilmWeek" & TmpWeek.Name, TmpWeek.Name)
                            grdFilms.Columns(grdFilms.Columns.Count - 1).Width = 40
                            grdFilms.Columns(grdFilms.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
                        Next
                        'makes the "total" column in the end of the budget grid
                        grdBudget.Columns.Add("colBudgetSum", "Total")
                        grdBudget.Columns(grdBudget.Columns.Count - 1).Width = 60
                        grdBudget.Columns(grdBudget.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
                    End If
                    'Add the Added values to its grid
                    For Each TmpAV In TmpBT.AddedValues
                        If Not TmpAV.ShowIn = Trinity.cAddedValue.ShowInEnum.siBooking Then
                            grdAV.Rows.Add()
                            grdAV.Rows(grdAV.Rows.Count - 1).Tag = TmpAV
                        End If
                    Next
                    grdTRP.Rows.Add(2)
                    grdTRP.Rows(grdTRP.Rows.Count - 1).Tag = TmpBT
                    grdTRP.Rows(grdTRP.Rows.Count - 2).Tag = TmpBT
                    grdDiscounts.Rows.Add(4)
                    grdDiscounts.Rows(grdDiscounts.Rows.Count - 1).Tag = TmpBT
                    grdDiscounts.Rows(grdDiscounts.Rows.Count - 2).Tag = TmpBT
                    grdDiscounts.Rows(grdDiscounts.Rows.Count - 3).Tag = TmpBT
                    grdDiscounts.Rows(grdDiscounts.Rows.Count - 4).Tag = TmpBT
                    grdBudget.Rows.Add()
                    With grdBudget.Rows(grdBudget.Rows.Count - 1)
                        .Tag = TmpBT
                        If TmpBT.IsPremium AndAlso TmpBT.IsSpecific Then
                            .ReadOnly = True
                            .DefaultCellStyle = styleCantSetP
                            .Tag = TmpBT
                        End If
                    End With
                    grdIndex.Rows.Add()
                    grdIndex.Rows(grdIndex.Rows.Count - 1).Tag = TmpBT
                    grdSpotCount.Rows.Add()
                    grdSpotCount.Rows(grdSpotCount.Rows.Count - 1).Tag = TmpBT
                    grdSpotCount.Rows(grdSpotCount.Rows.Count - 1).HeaderCell.Value = TmpBT.ParentChannel.Shortname & " " & TmpBT.Shortname
                    For i = 0 To grdTRP.Columns.Count - 1
                        'For i = 0 To TmpBT.Weeks.Count - 1
                        grdTRP.Rows(grdTRP.Rows.Count - 1).Cells(i).Tag = TmpBT.Weeks(i + 1)
                        grdTRP.Rows(grdTRP.Rows.Count - 2).Cells(i).Tag = TmpBT.Weeks(i + 1)
                        grdDiscounts.Rows(grdDiscounts.Rows.Count - 1).Cells(i).Tag = TmpBT.Weeks(i + 1)
                        grdDiscounts.Rows(grdDiscounts.Rows.Count - 2).Cells(i).Tag = TmpBT.Weeks(i + 1)
                        grdDiscounts.Rows(grdDiscounts.Rows.Count - 3).Cells(i).Tag = TmpBT.Weeks(i + 1)
                        grdDiscounts.Rows(grdDiscounts.Rows.Count - 4).Cells(i).Tag = TmpBT.Weeks(i + 1)
                        grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(2 + i).Tag = TmpBT.Weeks(i + 1)
                    Next
                    grdSumChannels.Rows.Add(2)
                    cmbFilmChannel.Items.Add(TmpBT)
                End If
            Next
        Next
        grdFilms.Columns.Add("colTotal", "Total")
        grdFilms.Columns("colTotal").ReadOnly = True
        grdFilms.Columns("colTotal").CellTemplate.Style.ForeColor = Color.DarkGray
        grdFilms.Columns("colTotal").Width = 40
        grdFilms.Columns.Add(New AdTooxColumn With {.HeaderText = "AdToox", .Width = 100, .Visible = TrinitySettings.AdtooxEnabled})
        With grdSpotCount.Rows(grdSpotCount.Rows.Add())
            .ReadOnly = True
            .DefaultCellStyle.ForeColor = Color.DarkGray
            .HeaderCell.Value = "TOTAL:"
        End With
        cmbFilmChannel.Items.Add("TOTAL")
        'cmbFilmChannel.SelectedIndex = 0
        grdFilms.RowHeadersWidthSizeMode = Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        grdBudget.Rows.Add()
        grdGrandSum.Rows.Add()
        grdSumWeeks.Rows.Add(3)
        grdTRP.Height = 10000
        grdTRP.Height = grdTRP.GetRowDisplayRectangle(grdTRP.Rows.Count - 1, False).Bottom + 1
        grdTRP.Width = 10000
        grdTRP.Width = grdTRP.GetColumnDisplayRectangle(grdTRP.Columns.Count - 1, False).Right + 1
        'sets sumChannels height equal to the grid beside and makes sure there is no space in the middle
        grdSumChannels.Left = grdTRP.Right - 1
        grdSumChannels.Height = grdTRP.Height
        'sets sumWeek widht equal to the grid above and makes sure there is no space in the middle
        grdSumWeeks.Top = grdTRP.Bottom - 1
        grdSumWeeks.Width = grdTRP.Width
        grdSumWeeks.Height = grdSumWeeks.GetRowDisplayRectangle(grdSumWeeks.Rows.Count - 1, False).Bottom + 1
        'puts the grandSum grid up in the corner between the SumChannel and SumWeeks grids
        grdGrandSum.Left = grdSumChannels.Left
        grdGrandSum.Top = grdSumWeeks.Top
        grdAV.Width = grdTRP.Width

        'sets the AV grid and hides the overflow (the "true" value)

        'all the 10000 widths and heights are set because, if the grid is larger than the 
        'container holding it, the grid.getSize will not count whats outside the container
        'boundaries. 
        If grdAV.Rows.Count > 0 Then
            grdAV.Height = 10000
            grdAV.Height = grdAV.GetRowDisplayRectangle(grdAV.Rows.Count - 1, True).Bottom + 1
        Else
            grdAV.Height = 0
        End If
        'Sets the discount grid and shows the overflow (the "False" value)
        grdDiscounts.Width = grdTRP.Width + 80
        grdDiscounts.Height = 10000
        grdDiscounts.Height = grdDiscounts.GetRowDisplayRectangle(grdDiscounts.Rows.Count - 1, False).Bottom + 1
        'Sets the budget grid and shows the overflow (the "False" value)
        grdBudget.Width = 10000
        grdBudget.Width = grdBudget.GetColumnDisplayRectangle(grdBudget.Columns.Count - 1, False).Right + 1
        grdBudget.Height = 10000
        grdBudget.Height = grdBudget.GetRowDisplayRectangle(grdBudget.Rows.Count - 1, False).Bottom + 1

        grdIndex.Height = 10000
        grdIndex.Height = grdIndex.GetRowDisplayRectangle(grdIndex.Rows.Count - 1, False).Bottom + 1

        grdSpotCount.Width = 10000
        grdSpotCount.Width = grdSpotCount.GetColumnDisplayRectangle(grdSpotCount.Columns.Count - 1, False).Right + 1
        cmdSpotcountND.Left = grdSpotCount.Right + 6
        cmdSpotcountSettings.Left = grdSpotCount.Right + 6
        grdSpotCount.Height = 10000
        grdSpotCount.Height = grdSpotCount.GetRowDisplayRectangle(grdSpotCount.Rows.Count - 1, False).Bottom + 1

        colMainTarget.HeaderCell.Style.WrapMode = Windows.Forms.DataGridViewTriState.False
        colMainTarget.HeaderCell.Value = Campaign.MainTarget.TargetNameNice
        colSecTarget.HeaderCell.Style.WrapMode = Windows.Forms.DataGridViewTriState.False
        colSecTarget.HeaderCell.Value = Campaign.SecondaryTarget.TargetNameNice
        colAllAdults.HeaderCell.Value = Campaign.AllAdults

        'sets the position od the CTC label at the bottom of the budget grid
        lblCTCLabel.Top = grdBudget.Bottom + 3
        lblCTC.Top = grdBudget.Bottom + 3
        lblCTC.Left = grdBudget.Right - lblCTC.Width
        lblCTCLabel.Left = lblCTC.Left - lblCTCLabel.Width
        cmdEditCTC.Left = lblCTC.Right + 6
        cmdEditCTC.Top = lblCTC.Top - 4
        cmdLockOnBudget.Left = lblCTC.Right + 6
        cmdBudgetType.Left = cmdLockOnBudget.Left - cmdBudgetType.Width - 5

        lblSumBudgetLabel.Top = grdBudget.Bottom + 3
        lblSumBudget.Top = grdBudget.Bottom + 3
        lblSumBudget.Left = lblSumBudgetLabel.Width + 1
        lblSumBudgetLabel.Left = 2

        lblPercentageText.Top = grdBudget.Bottom + 3
        lblPercentage.Top = grdBudget.Bottom + 3
        lblPercentage.Left = lblPercentageText.Right + 1
        'lblPercentageText.Left = lblSumBudget.Right + 10

        lblMarathonCTC.Left = lblCTC.Left
        lblMarathonCTC.Top = lblCTC.Bottom + 3

        lblMarathonCTCLabel.Left = lblCTCLabel.Left
        lblMarathonCTCLabel.Top = lblCTCLabel.Bottom + 3

        grpTRP.Width = grdSumChannels.Right + 6
        If grpTRP.Width < 425 Then
            grpTRP.Width = 425
        End If
        grpTRP.Height = 10000
        grpTRP.Height = grdSumWeeks.Bottom + 25

        grpAV.Top = grpTRP.Bottom + 6
        grpAV.Height = 17
        grpAV.Width = grdAV.Right + 6
        picExpandAV.Visible = True
        picCollapseAV.Visible = False
        grpDiscounts.Height = 17
        grpDiscounts.Width = grdDiscounts.Right + 6
        grpDiscounts.Top = grpAV.Bottom + 6
        picExpandDiscounts.Visible = True
        picCollapseDiscounts.Visible = False

        'sets the size of the Budget group (the frame around the budget grids)
        grpBudget.Width = cmdEditCTC.Right + 6
        grpBudget.Height = 10000
        grpBudget.Height = lblMarathonCTC.Bottom + 6
        grpBudget.Top = grpDiscounts.Bottom + 6

        grpFilms.Left = grpTRP.Right + 6
        grpIndex.Height = grdIndex.Bottom + 6
        grpIndex.Left = grpFilms.Left
        grpSpotCount.Top = grpIndex.Bottom + 6
        grpSpotCount.Height = grdSpotCount.Bottom + 6
        grpSpotCount.Left = grpFilms.Left
        grpSpotCount.Width = cmdSpotcountND.Right + 6
        grpCompensation.Top = grpSpotCount.Bottom + 6
        grpCompensation.Left = grpIndex.Left

        For i = 0 To grdTRP.Rows.Count - 1 Step 2
            Dim lblChan As New System.Windows.Forms.Label
            grpTRP.Controls.Add(lblChan)
            lblChan.AutoSize = False
            lblChan.TextAlign = Drawing.ContentAlignment.TopLeft

            If grdTRP.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                lblChan.Text = DirectCast(grdTRP.Rows(i).Tag, Trinity.cCombination).Name
            Else
                lblChan.Text = DirectCast(grdTRP.Rows(i).Tag, Trinity.cBookingType).ParentChannel.Shortname & " " & DirectCast(grdTRP.Rows(i).Tag, Trinity.cBookingType).Shortname
            End If
            lblChan.Top = grdTRP.GetRowDisplayRectangle(i, True).Top + grdTRP.Top - 1
            lblChan.Left = cmbUniverse.Left
            lblChan.Height = grdTRP.GetRowDisplayRectangle(i, True).Height + grdTRP.GetRowDisplayRectangle(i + 1, True).Height
            lblChan.Width = 65
            lblChan.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            lblBuyingTarget = New System.Windows.Forms.Label
            grpTRP.Controls.Add(lblBuyingTarget)
            lblBuyingTarget.AutoSize = False
            lblBuyingTarget.TextAlign = Drawing.ContentAlignment.MiddleLeft
            If grdTRP.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                Dim c As Trinity.cCombination = grdTRP.Rows(i).Tag
                If c.BuyingTarget = "" Then
                    lblBuyingTarget.Text = "-"
                Else
                    lblBuyingTarget.Text = c.BuyingTarget
                End If
                'lblBuyingTarget.Text = grdTRP.Rows(i).Tag.Name
            Else
                lblBuyingTarget.Text = DirectCast(grdTRP.Rows(i).Tag, Trinity.cBookingType).BuyingTarget.TargetName
            End If
            lblBuyingTarget.Top = grdTRP.GetRowDisplayRectangle(i, True).Top + grdTRP.Top - 1
            lblBuyingTarget.Left = lblChan.Right
            lblBuyingTarget.Height = grdTRP.GetRowDisplayRectangle(i, True).Height
            lblBuyingTarget.Width = grdTRP.Left - lblChan.Right
            lblBuyingTarget.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            Dim lblMainTarget As New System.Windows.Forms.Label
            grpTRP.Controls.Add(lblMainTarget)
            lblMainTarget.AutoSize = False
            lblMainTarget.TextAlign = Drawing.ContentAlignment.MiddleLeft
            lblMainTarget.Text = Campaign.MainTarget.TargetName
            lblMainTarget.Top = grdTRP.GetRowDisplayRectangle(i + 1, True).Top + grdTRP.Top - 1
            lblMainTarget.Left = lblChan.Right
            lblMainTarget.Height = grdTRP.GetRowDisplayRectangle(i + 1, True).Height
            lblMainTarget.Width = grdTRP.Left - lblChan.Right
            lblMainTarget.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            Dim cmdChannelNotes As New System.Windows.Forms.Button
            cmdChannelNotes.Width = 24
            cmdChannelNotes.Height = 24
            cmdChannelNotes.FlatStyle = Windows.Forms.FlatStyle.Standard
            cmdChannelNotes.Image = My.Resources.notes_small_16x16
            cmdChannelNotes.Left = lblChan.Left + 3
            cmdChannelNotes.Top = lblChan.Bottom - cmdChannelNotes.Height - 3
            If grdTRP.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                If DirectCast(grdTRP.Rows(i).Tag, Trinity.cCombination).Relations(1).Bookingtype.Comments <> "" Then
                    cmdChannelNotes.BackColor = Color.Red
                End If
            Else
                If DirectCast(grdTRP.Rows(i).Tag, Trinity.cBookingType).Comments <> "" Then
                    cmdChannelNotes.BackColor = Color.Red
                End If
            End If
            'Used to update backcolor in Form_Activate
            cmdChannelNotes.Name = "Notes"
            cmdChannelNotes.Tag = grdTRP.Rows(i).Tag
            AddHandler cmdChannelNotes.Click, AddressOf BookingTypeNotes
            grpTRP.Controls.Add(cmdChannelNotes)
            cmdChannelNotes.BringToFront()

            If Not grdTRP.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" AndAlso (DirectCast(grdTRP.Rows(i).Tag, Trinity.cBookingType).IsSponsorship AndAlso DirectCast(grdTRP.Rows(i).Tag, Trinity.cBookingType).IsSpecific) Then
                Dim cmdSpots As New System.Windows.Forms.Button
                cmdSpots.Width = 24
                cmdSpots.Height = 24
                cmdSpots.FlatStyle = Windows.Forms.FlatStyle.Standard
                cmdSpots.Image = My.Resources.add2
                cmdSpots.Left = cmdChannelNotes.Right + 3
                cmdSpots.Top = cmdChannelNotes.Top
                If DirectCast(grdTRP.Rows(i).Tag, Trinity.cBookingType).SpecificSponsringPrograms.Count = 0 Then
                    cmdSpots.BackColor = Color.Red
                End If
                'Used to update backcolor in Form_Activate
                cmdSpots.Name = "Spots"
                cmdSpots.Tag = grdTRP.Rows(i).Tag
                AddHandler cmdSpots.Click, AddressOf SpecificSponsringSpots
                grpTRP.Controls.Add(cmdSpots)
                cmdSpots.BringToFront()
            End If
        Next
        For i = 0 To grdAV.Rows.Count - 1
            Dim lblChan As New System.Windows.Forms.Label
            grpAV.Controls.Add(lblChan)
            lblChan.AutoSize = False
            lblChan.TextAlign = Drawing.ContentAlignment.MiddleLeft

            'check for combination
            If grdAV.Rows(i).Cells(0).Tag Is Nothing Then
                lblChan.Text = DirectCast(grdAV.Rows(i).Tag, Trinity.cAddedValue).Bookingtype.ParentChannel.Shortname & " " & DirectCast(grdAV.Rows(i).Tag, Trinity.cAddedValue).Bookingtype.Shortname
                lblChan.Height = grdAV.GetRowDisplayRectangle(i, True).Height * DirectCast(grdAV.Rows(i).Tag, Trinity.cAddedValue).Bookingtype.AddedValues.Count
                lblChan.Top = grdAV.GetRowDisplayRectangle(i, True).Top + grdDiscounts.Top - 1
                lblChan.Left = cmbUniverse.Left
                lblChan.Width = 65
                lblChan.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
                For j = 1 To DirectCast(grdAV.Rows(i).Tag, Trinity.cAddedValue).Bookingtype.AddedValues.Count
                    Dim lblAV As New System.Windows.Forms.Label
                    grpAV.Controls.Add(lblAV)
                    lblAV.AutoSize = False
                    lblAV.TextAlign = Drawing.ContentAlignment.MiddleLeft
                    lblAV.Text = DirectCast(grdAV.Rows(i + j - 1).Tag, Trinity.cAddedValue).Name
                    lblAV.Top = grdAV.GetRowDisplayRectangle(i + j - 1, True).Top + grdDiscounts.Top - 1
                    lblAV.Left = lblChan.Right
                    lblAV.Height = grdAV.GetRowDisplayRectangle(i + j - 1, True).Height '* DirectCast(grdAV.Rows(i).Tag, Trinity.cAddedValue).Bookingtype.AddedValues.Count
                    lblAV.Width = grdDiscounts.Left - lblChan.Right
                    lblAV.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
                Next
                i = i + DirectCast(grdAV.Rows(i).Tag, Trinity.cAddedValue).Bookingtype.AddedValues.Count - 1
            Else
                lblChan.Text = DirectCast(grdAV.Rows(i).Cells(0).Tag, Trinity.cCombination).Name
                lblChan.Height = grdAV.GetRowDisplayRectangle(i, True).Height * DirectCast(grdAV.Rows(i).Cells(0).Tag, Trinity.cCombination).Relations(1).Bookingtype.AddedValues.Count
                lblChan.Top = grdAV.GetRowDisplayRectangle(i, True).Top + grdDiscounts.Top - 1
                lblChan.Left = cmbUniverse.Left
                lblChan.Width = 65
                lblChan.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
                For j = 1 To DirectCast(grdAV.Rows(i).Cells(0).Tag, Trinity.cCombination).Relations(1).Bookingtype.AddedValues.Count
                    Dim lblAV As New System.Windows.Forms.Label
                    grpAV.Controls.Add(lblAV)
                    lblAV.AutoSize = False
                    lblAV.TextAlign = Drawing.ContentAlignment.MiddleLeft
                    lblAV.Text = DirectCast(grdAV.Rows(i + j - 1).Tag, Trinity.cAddedValue).Name
                    lblAV.Top = grdAV.GetRowDisplayRectangle(i + j - 1, True).Top + grdDiscounts.Top - 1
                    lblAV.Left = lblChan.Right
                    lblAV.Height = grdAV.GetRowDisplayRectangle(i + j - 1, True).Height '* DirectCast(grdAV.Rows(i).Tag, Trinity.cAddedValue).Bookingtype.AddedValues.Count
                    lblAV.Width = grdDiscounts.Left - lblChan.Right
                    lblAV.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
                Next
                i = i + DirectCast(grdAV.Rows(i).Cells(0).Tag, Trinity.cCombination).Relations(1).Bookingtype.AddedValues.Count - 1
            End If

        Next
        For i = 0 To grdDiscounts.Rows.Count - 1 Step 4
            Dim lblChan As New System.Windows.Forms.Label
            grpDiscounts.Controls.Add(lblChan)
            lblChan.AutoSize = False
            lblChan.TextAlign = Drawing.ContentAlignment.MiddleLeft
            If grdDiscounts.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                lblChan.Text = DirectCast(grdDiscounts.Rows(i).Tag, Trinity.cCombination).Name
            Else
                lblChan.Text = DirectCast(grdDiscounts.Rows(i).Tag, Trinity.cBookingType).ParentChannel.Shortname & " " & DirectCast(grdDiscounts.Rows(i).Tag, Trinity.cBookingType).Shortname
            End If
            lblChan.Top = grdDiscounts.GetRowDisplayRectangle(i, True).Top + grdDiscounts.Top - 1
            lblChan.Left = cmbUniverse.Left
            lblChan.Height = grdDiscounts.GetRowDisplayRectangle(i, True).Height * 4
            lblChan.Width = 65
            lblChan.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            Dim lblEffDisc As New System.Windows.Forms.Label
            grpDiscounts.Controls.Add(lblEffDisc)
            lblEffDisc.AutoSize = False
            lblEffDisc.TextAlign = Drawing.ContentAlignment.MiddleLeft
            lblEffDisc.Text = "Eff. Discount"
            lblEffDisc.Top = grdDiscounts.GetRowDisplayRectangle(i, True).Top + grdDiscounts.Top - 1
            lblEffDisc.Left = lblChan.Right
            lblEffDisc.Height = grdDiscounts.GetRowDisplayRectangle(i, True).Height
            lblEffDisc.Width = grdDiscounts.Left - lblChan.Right
            lblEffDisc.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            Dim lblNetCPP As New System.Windows.Forms.Label
            grpDiscounts.Controls.Add(lblNetCPP)
            lblNetCPP.AutoSize = False
            lblNetCPP.TextAlign = Drawing.ContentAlignment.MiddleLeft
            lblNetCPP.Text = "Net CPP 30"""
            lblNetCPP.Top = grdDiscounts.GetRowDisplayRectangle(i + 1, True).Top + grdDiscounts.Top - 1
            lblNetCPP.Left = lblChan.Right
            lblNetCPP.Height = grdDiscounts.GetRowDisplayRectangle(i + 1, True).Height
            lblNetCPP.Width = grdDiscounts.Left - lblChan.Right
            lblNetCPP.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            Dim lblActCPP As New System.Windows.Forms.Label
            grpDiscounts.Controls.Add(lblActCPP)
            lblActCPP.AutoSize = False
            lblActCPP.TextAlign = Drawing.ContentAlignment.MiddleLeft
            lblActCPP.Text = "Actual CPP"
            lblActCPP.Top = grdDiscounts.GetRowDisplayRectangle(i + 2, True).Top + grdDiscounts.Top - 1
            lblActCPP.Left = lblChan.Right
            lblActCPP.Height = grdDiscounts.GetRowDisplayRectangle(i + 2, True).Height
            lblActCPP.Width = grdDiscounts.Left - lblChan.Right
            lblActCPP.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            Dim lblIndex As New System.Windows.Forms.Label
            grpDiscounts.Controls.Add(lblIndex)
            lblIndex.AutoSize = False
            lblIndex.TextAlign = Drawing.ContentAlignment.MiddleLeft
            lblIndex.Text = "Index"
            lblIndex.Top = grdDiscounts.GetRowDisplayRectangle(i + 3, True).Top + grdDiscounts.Top - 1
            lblIndex.Left = lblChan.Right
            lblIndex.Height = grdDiscounts.GetRowDisplayRectangle(i + 3, True).Height
            lblIndex.Width = grdDiscounts.Left - lblChan.Right
            lblIndex.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        Next

        For i = 0 To grdBudget.Rows.Count - 1
            Dim lblChan As New System.Windows.Forms.Label
            grpBudget.Controls.Add(lblChan)
            lblChan.AutoSize = False
            lblChan.TextAlign = Drawing.ContentAlignment.MiddleLeft
            If i < grdBudget.Rows.Count - 1 Then
                If grdBudget.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    lblChan.Text = DirectCast(grdBudget.Rows(i).Tag, Trinity.cCombination).Name
                Else
                    'Normal row
                    lblChan.Text = DirectCast(grdBudget.Rows(i).Tag, Trinity.cBookingType).ParentChannel.Shortname & " " & DirectCast(grdBudget.Rows(i).Tag, Trinity.cBookingType).Shortname
                End If
            Else
                lblChan.Text = "Total:"
            End If
            lblChan.Top = grdBudget.GetRowDisplayRectangle(i, True).Top + grdBudget.Top - 1
            lblChan.Left = cmbUniverse.Left
            lblChan.Height = grdBudget.GetRowDisplayRectangle(i, True).Height
            lblChan.Width = 65
            lblChan.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

        Next

        For i = 0 To grdIndex.Rows.Count - 1
            Dim lblChan As New System.Windows.Forms.Label
            grpIndex.Controls.Add(lblChan)
            lblChan.AutoSize = False
            lblChan.TextAlign = Drawing.ContentAlignment.MiddleLeft

            'ifit is nothing we have a normal, else we have a combination here
            If grdIndex.Rows(i).Cells(0).Tag Is Nothing Then
                lblChan.Text = DirectCast(grdIndex.Rows(i).Tag, Trinity.cBookingType).ParentChannel.Shortname & " " & DirectCast(grdIndex.Rows(i).Tag, Trinity.cBookingType).Shortname
            Else
                lblChan.Text = DirectCast(grdIndex.Rows(i).Cells(0).Tag, Trinity.cCombination).Name
            End If
            lblChan.Top = grdIndex.GetRowDisplayRectangle(i, True).Top + grdIndex.Top - 1
            lblChan.Left = 6
            lblChan.Height = grdIndex.GetRowDisplayRectangle(i, True).Height
            lblChan.Width = 65
            lblChan.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            lblBuyingTarget = New System.Windows.Forms.Label
            grpIndex.Controls.Add(lblBuyingTarget)
            lblBuyingTarget.AutoSize = False
            lblBuyingTarget.TextAlign = Drawing.ContentAlignment.MiddleLeft

            'if its not nothing we have a combination
            If grdIndex.Rows(i).Cells(0).Tag Is Nothing Then
                lblBuyingTarget.Text = DirectCast(grdIndex.Rows(i).Tag, Trinity.cBookingType).BuyingTarget.TargetName
            Else
                Dim c As Trinity.cCombination = grdIndex.Rows(i).Cells(0).Tag
                If c.BuyingTarget = "" Then
                    lblBuyingTarget.Text = "-"
                Else
                    lblBuyingTarget.Text = c.BuyingTarget
                End If
            End If
            lblBuyingTarget.Top = grdIndex.GetRowDisplayRectangle(i, True).Top + grdIndex.Top - 1
            lblBuyingTarget.Left = lblChan.Right
            lblBuyingTarget.Height = grdIndex.GetRowDisplayRectangle(i, True).Height
            lblBuyingTarget.Width = grdIndex.Left - lblChan.Right
            lblBuyingTarget.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        Next

        Dim lblSum As New System.Windows.Forms.Label
        grpTRP.Controls.Add(lblSum)
        lblSum.AutoSize = False
        lblSum.TextAlign = Drawing.ContentAlignment.MiddleLeft
        lblSum.Text = Campaign.MainTarget.TargetName
        lblSum.Top = grdSumWeeks.GetRowDisplayRectangle(0, True).Top + grdSumWeeks.Top - 1
        lblSum.Left = lblBuyingTarget.Left
        lblSum.Height = grdSumWeeks.GetRowDisplayRectangle(0, True).Height
        lblSum.Width = grdTRP.Left - lblSum.Left
        lblSum.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

        lblSum = New System.Windows.Forms.Label
        grpTRP.Controls.Add(lblSum)
        lblSum.AutoSize = False
        lblSum.TextAlign = Drawing.ContentAlignment.MiddleLeft
        lblSum.Text = "TRP " & Campaign.AllAdults ' & " Nat"
        lblSum.Top = grdSumWeeks.GetRowDisplayRectangle(1, True).Top + grdSumWeeks.Top - 1
        lblSum.Left = lblBuyingTarget.Left
        lblSum.Height = grdSumWeeks.GetRowDisplayRectangle(1, True).Height
        lblSum.Width = grdTRP.Left - lblSum.Left
        lblSum.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

        'lblFreqToCalculate = New System.Windows.Forms.Label
        lblFreqToCalculate = New System.Windows.Forms.Label
        grpTRP.Controls.Add(lblFreqToCalculate)
        lblFreqToCalculate.AutoSize = False
        lblFreqToCalculate.TextAlign = Drawing.ContentAlignment.MiddleLeft
        lblFreqToCalculate.Text = "Est freq. " & Campaign.WeeklyFrequency & "+"
        lblFreqToCalculate.Top = grdSumWeeks.GetRowDisplayRectangle(2, True).Top + grdSumWeeks.Top - 1
        lblFreqToCalculate.Left = lblBuyingTarget.Left
        lblFreqToCalculate.Height = grdSumWeeks.GetRowDisplayRectangle(2, True).Height
        lblFreqToCalculate.Width = grdTRP.Left - lblFreqToCalculate.Left
        lblFreqToCalculate.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

        AddHandler lblFreqToCalculate.Click, AddressOf lblSumChangeFreq

        cmbUniverse.SelectedIndex = 0
        cmbTargets.SelectedIndex = 0
        cmbDisplay.SelectedIndex = DisplayModeEnum.TRP
        cmbFilmChannel.SelectedIndex = 0

        grdCompensation.Rows.Clear()

        For Each TmpC As Trinity.cCombination In Campaign.Combinations
            If TmpC.ShowAsOne Then
                For Each TmpComp As Trinity.cCompensation In TmpC.Relations(1).Bookingtype.Compensations
                    grdCompensation.Rows.Add()
                    grdCompensation.Rows(grdCompensation.Rows.Count - 1).Tag = TmpComp
                    grdCompensation.Rows(grdCompensation.Rows.Count - 1).Cells(0).Tag = TmpC
                Next
            End If
        Next
        For Each TmpChan In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.ShowMe Then
                    For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                        grdCompensation.Rows.Add()
                        grdCompensation.Rows(grdCompensation.Rows.Count - 1).Tag = TmpComp
                    Next
                End If
            Next
        Next

        grdDiscounts.Columns.Add("colTotal", "Total")
        grdDiscounts.Columns("colTotal").Width = 60
        grdDiscounts.Width = grdDiscounts.GetColumnDisplayRectangle(grdDiscounts.Columns.Count - 1, False).Right + 1
        onlyOneSelectedGrid()

        ' Dim tmpMarathon As New Marathon(TrinitySettings.MarathonCommand)
        ' lblMarathonCTC.Text = tmpMarathon.GetCTCForPlan("MECS", Campaign.MarathonPlanNr)

        Dim check As New Trinity.cPricelistCheck(Campaign, Me)
        Thread1 = New System.Threading.Thread(AddressOf check.checkPricelists)
        Thread1.IsBackground = True
        Thread1.Start()
    End Sub

    Sub SpecificSponsringSpots(ByVal sender As Object, ByVal e As EventArgs)
        frmSpecSpons.BookingType = sender.tag
        frmSpecSpons.ShowDialog()
        frmAllocate_Activated(sender, e)
    End Sub

    Sub BookingTypeNotes(ByVal sender As Object, ByVal e As EventArgs)
        Dim frmBTNotes As frmNotes
        If sender.tag.GetType Is GetType(Trinity.cBookingType) Then
            frmBTNotes = New frmNotes(DirectCast(sender.tag, Trinity.cBookingType))
        Else
            frmBTNotes = New frmNotes(DirectCast(sender.tag, Trinity.cCombination))
        End If
        frmBTNotes.MdiParent = frmMain
        frmBTNotes.Show()
    End Sub

    Public Sub setPricelistLabel(ByVal Errors As String)
        strPricelistUpdate = Errors

        If Errors = "" Then
            lblOldPricelist.Visible = False
        Else
            lblOldPricelist.Visible = True
        End If
        lblOldPricelist.Text = "The pricelists of this campaign are not all up to date"
    End Sub

    Private Sub picCollapseDiscounts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCollapseDiscounts.Click
        'Mionimized the Discount group
        grpDiscounts.Height = 17
        picExpandDiscounts.Visible = True
        picCollapseDiscounts.Visible = False
        grpBudget.Top = grpDiscounts.Bottom + 6

        'BLT.Destroy()

    End Sub

    Private Sub picExpandDiscounts_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picExpandDiscounts.Click
        'Expand the Discounts group
        grpDiscounts.Height = 10000
        grpDiscounts.Height = grdDiscounts.Bottom + 5
        picExpandDiscounts.Visible = False
        picCollapseDiscounts.Visible = True
        grpBudget.Top = grpDiscounts.Bottom + 6

        'For Each tmpChan As Trinity.cChannel In Campaign.Channels
        '    For Each tmpBT As Trinity.cBookingType In tmpChan.BookingTypes
        '        If tmpBT.BookIt Then
        '            For Each tmpIndex As Trinity.cIndex In tmpBT.Indexes
        '                If tmpIndex.UseThis AndAlso tmpIndex.Index < 10 Then
        '                    BLT = New Trinity.cBalloonToolTip
        '                    BLT.Style = Trinity.cBalloonToolTip.ttStyleEnum.TTBalloon
        '                    BLT.TipText = "One or more indexes have a value below 10. This may be an error. If so, remove this index in Setup"
        '                    BLT.VisibleTime = 2000
        '                    BLT.CreateToolTip(picExpandDiscounts.Handle.ToInt32)
        '                    BLT.Show(MousePosition.X, MousePosition.Y)
        '                End If
        '            Next
        '        End If
        '    Next
        'Next

    End Sub

    Private Sub picExpandAV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picExpandAV.Click
        'expands the Added value group
        grpAV.Height = 10000
        grpAV.Height = grdAV.Bottom + 5
        picExpandAV.Visible = False
        picCollapseAV.Visible = True
        grpDiscounts.Top = grpAV.Bottom + 6
        grpBudget.Top = grpDiscounts.Bottom + 6
    End Sub

    Private Sub picCollapseAV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picCollapseAV.Click
        'minimize the Added value group
        grpAV.Height = 17
        picExpandAV.Visible = True
        picCollapseAV.Visible = False
        grpDiscounts.Top = grpAV.Bottom + 6
        grpBudget.Top = grpDiscounts.Bottom + 6
    End Sub

    Private Sub ChangeFreq(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Campaign.WeeklyFrequency = sender.tag
        lblFreqToCalculate.Text = "Est freq. " & Campaign.WeeklyFrequency & "+"
    End Sub

    Private Sub lblSumChangeFreq(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim freqMenu As New Windows.Forms.ContextMenuStrip

        Dim cameFrom As Object = sender

        For i As Integer = 1 To 10
            Dim subMenu As New Windows.Forms.ToolStripMenuItem
            subMenu.Text = i & "+"
            subMenu.Tag = i
            AddHandler subMenu.Click, AddressOf ChangeFreq
            freqMenu.Items.Add(subMenu)
        Next






        freqMenu.Show(MousePosition)

    End Sub

    Private Sub cmbFilmChannel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFilmChannel.SelectedIndexChanged

        Dim tmpc As Trinity.cCombination
        Dim TmpBT As Trinity.cBookingType
        Dim TmpFilm As Trinity.cFilm

        If cmbFilmChannel.SelectedIndex = cmbFilmChannel.Items.Count - 1 Then
            TmpBT = Nothing
            grdFilms.ReadOnly = True
            grdFilms.DefaultCellStyle = styleCantSetP
        Else
            If Campaign.RootCampaign Is Nothing Then
                grdFilms.ReadOnly = False
                grdFilms.ForeColor = Color.Black
                grdFilms.DefaultCellStyle = styleNormalP
            Else
                grdFilms.ReadOnly = True
                grdFilms.DefaultCellStyle = styleCantSetP
            End If
        End If

        'clear the film grid
        grdFilms.Rows.Clear()
        SkipIt = True

        If cmbFilmChannel.SelectedItem.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            tmpc = cmbFilmChannel.SelectedItem
            TmpBT = tmpc.Relations(1).Bookingtype

            If TmpBT Is Nothing Then
                For Each TmpFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
                    grdFilms.Rows.Add()
                    grdFilms.Rows(grdFilms.Rows.Count - 1).HeaderCell.Value = TmpFilm.Name
                    grdFilms.Rows(grdFilms.Rows.Count - 1).Tag = TmpFilm
                Next
            Else
                For Each TmpFilm In TmpBT.Weeks(1).Films
                    grdFilms.Rows.Add()
                    grdFilms.Rows(grdFilms.Rows.Count - 1).HeaderCell.Value = TmpFilm.Name
                    grdFilms.Rows(grdFilms.Rows.Count - 1).Tag = TmpFilm
                Next
            End If
        Else
            If cmbFilmChannel.SelectedIndex = cmbFilmChannel.Items.Count - 1 Then
                TmpBT = Nothing
            Else
                TmpBT = cmbFilmChannel.SelectedItem
            End If


            If TmpBT Is Nothing Then
                For Each TmpFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
                    grdFilms.Rows.Add()
                    grdFilms.Rows(grdFilms.Rows.Count - 1).HeaderCell.Value = TmpFilm.Name
                    grdFilms.Rows(grdFilms.Rows.Count - 1).Tag = TmpFilm
                Next
            Else
                For Each TmpFilm In TmpBT.Weeks(1).Films
                    grdFilms.Rows.Add()
                    grdFilms.Rows(grdFilms.Rows.Count - 1).HeaderCell.Value = TmpFilm.Name
                    grdFilms.Rows(grdFilms.Rows.Count - 1).Tag = TmpFilm
                Next
            End If
        End If

        SkipIt = False
        grdFilms.Width = 10000
        grdFilms.Width = grdFilms.GetColumnDisplayRectangle(grdFilms.Columns.Count - 2, False).Right + grdFilms.GetColumnDisplayRectangle(grdFilms.Columns.Count - 1, False).Width + 12 + pnlChoose.Width
        pnlChoose.Left = grdFilms.Right + 6
        grdFilms.Height = 10000
        grdFilms.Height = grdFilms.GetRowDisplayRectangle(grdFilms.Rows.Count - 1, False).Bottom + 1
        grpFilms.Height = grdFilms.Bottom + 6
        grpFilms.Width = pnlChoose.Right + 6
        grpIndex.Top = grpFilms.Bottom + 6
        grpCompensation.Top = grpIndex.Bottom + 6
        grpSpotCount.Top = grpCompensation.Bottom + 6
        SkipIt = True
        SkipIt = False
        ColorFilmGrid()
    End Sub

    Sub ColorFilmGrid()
        'sets colors on the film grid depending on whether the sum is correct or not (adds to 100%)
        Dim i As Integer
        Dim j As Integer
        Dim Tot As Decimal
        For i = 0 To grdFilms.Columns.Count - 2
            Tot = 0
            For j = 0 To grdFilms.Rows.Count - 1
                If grdFilms.Rows(j).Cells(i).Value = grdFilms.Rows(j).Cells(i).Value Then
                    Try
                        'Tot = Tot + Format(grdFilms.Rows(j).Cells(i).Value * 100, "N2")

                        Tot = Tot + (grdFilms.Rows(j).Cells(i).Value * 100)
                    Catch
                        Tot += 0
                    End Try
                Else
                    Tot += 0
                End If
            Next
            If Format(Tot, "N1") <> 100 Then 'if the films dont sum up make them red
                For j = 0 To grdFilms.Rows.Count - 1
                    grdFilms.Rows(j).Cells(i).Style.BackColor = Drawing.Color.Red
                Next
            Else
                For j = 0 To grdFilms.Rows.Count - 1
                    grdFilms.Rows(j).Cells(i).Style.BackColor = Drawing.Color.White
                Next
            End If
        Next
    End Sub

    Private Sub grdFilms_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdFilms.CellFormatting
        If cmbFilmChannel.SelectedItem.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            If Not grdFilms.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag Then
                e.CellStyle.ForeColor = Color.Black
            Else
                e.CellStyle.ForeColor = Color.Blue
            End If
        End If
    End Sub

    Private Sub cmbUniverse_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUniverse.SelectedIndexChanged, cmbDisplay.SelectedIndexChanged, cmbTargets.SelectedIndexChanged
        If Campaign.RootCampaign Is Nothing And cmbDisplay.SelectedIndex < DisplayModeEnum.PercentOfWeek Then
            grdSumWeeks.DefaultCellStyle = styleNormalD
            grdSumChannels.ForeColor = Color.Black
            grdBudget.DefaultCellStyle = styleNormal
            grdDiscounts.DefaultCellStyle = styleNormal
            grdAV.DefaultCellStyle = styleNormal
            grdDiscounts.DefaultCellStyle = styleNormal
            grdIndex.DefaultCellStyle = styleNormal
            grdSpotCount.DefaultCellStyle = styleNormal
            grdTRP.DefaultCellStyle = styleNormal

            grdTRP.ReadOnly = False

            grdSumWeeks.ReadOnly = False
            grdSumChannels.ReadOnly = False
            grdBudget.ReadOnly = False
            grdFilms.ReadOnly = False
            grdDiscounts.ReadOnly = False
            grdAV.ReadOnly = False
            grdDiscounts.ReadOnly = False
            grdIndex.ReadOnly = False
            grdSpotCount.ReadOnly = False

        Else
            grdTRP.ReadOnly = True
            If Campaign.RootCampaign Is Nothing Then
                grdSumWeeks.DefaultCellStyle = styleNormalD
                grdSumChannels.ForeColor = styleNormal.ForeColor
                grdBudget.DefaultCellStyle = styleNormal
                grdDiscounts.DefaultCellStyle = styleNormal
                grdAV.DefaultCellStyle = styleNormal
                grdIndex.DefaultCellStyle = styleNormal
                grdSpotCount.DefaultCellStyle = styleNormal
                grdTRP.DefaultCellStyle = styleNormal

                grdSumWeeks.ReadOnly = False
                grdSumChannels.ReadOnly = False
                grdBudget.ReadOnly = False
                grdFilms.ReadOnly = False
                grdDiscounts.ReadOnly = False
                grdAV.ReadOnly = False
                grdDiscounts.ReadOnly = False
                grdIndex.ReadOnly = False
                grdSpotCount.ReadOnly = False
            Else
                grdSumWeeks.DefaultCellStyle = styleCantSetN
                grdSumChannels.ForeColor = styleCantSetN.ForeColor
                grdBudget.DefaultCellStyle = styleCantSetN
                grdDiscounts.DefaultCellStyle = styleCantSetN
                grdAV.DefaultCellStyle = styleCantSetN
                grdDiscounts.DefaultCellStyle = styleCantSetN
                grdIndex.DefaultCellStyle = styleCantSetN
                grdSpotCount.DefaultCellStyle = styleCantSetN
                grdTRP.DefaultCellStyle = styleCantSetN

                grdSumWeeks.ReadOnly = True
                grdSumChannels.ReadOnly = True
                grdBudget.ReadOnly = True
                grdFilms.ReadOnly = True
                grdDiscounts.ReadOnly = True
                grdAV.ReadOnly = True
                grdDiscounts.ReadOnly = True
                grdIndex.ReadOnly = True
                grdSpotCount.ReadOnly = True
            End If
        End If

        SkipIt = True

        UpdateLabels()

        grdTRP.Invalidate()
        grdBudget.Invalidate()
        grdSumChannels.Invalidate()
        grdSumWeeks.Invalidate()
        SkipIt = False
    End Sub

    Private Sub UpdateLabels()

        Try
            Dim displayString As String

            If cmbTargets.SelectedIndex = 0 Then
                displayString = Campaign.MainTarget.TargetName
            Else
                displayString = Campaign.SecondaryTarget.TargetName
            End If

            Dim Labels As List(Of Windows.Forms.Label) = (From lbl As Windows.Forms.Label In Me.Controls("grpTRP").Controls.OfType(Of Label)() Select lbl Where lbl.Text = Campaign.SecondaryTarget.TargetName Or lbl.Text = Campaign.MainTarget.TargetName).ToList
            For Each tmpLabel As Label In Labels
                tmpLabel.Text = displayString
                tmpLabel.Invalidate()
            Next
        Catch ex As Exception
            Debug.Print("Labels could not be updated - " & ex.Message)
        End Try


    End Sub
    Private Sub grdDiscounts_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdDiscounts.CellFormatting
        e.CellStyle = Trinity.AllocateFunctions.ApplyDiscountCellFormat(e.CellStyle, e.RowIndex, e.ColumnIndex)
    End Sub

    Private Sub grdDiscounts_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdDiscounts.CellValueNeeded
        e.Value = Trinity.AllocateFunctions.GetDiscountCellValue(grdDiscounts.Rows(e.RowIndex).Tag, e.RowIndex, grdDiscounts.Columns(e.ColumnIndex).HeaderText)
    End Sub

    'Private Sub grdDiscounts_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdDiscounts.CellValueNeeded

    '    If grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).OwningColumn.Name = "colTotal" Then
    '        Dim trps As Double = 0
    '        Dim netbudget As Double = 0
    '        Select Case e.RowIndex Mod 4
    '            Case Is = 0
    '                If grdDiscounts.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
    '                    Dim discount As Double = 0
    '                    Dim count As Integer = 0
    '                    Dim tmpc As Trinity.cCombination = grdDiscounts.Rows(e.RowIndex).Tag

    '                    For Each tmpCC As Trinity.cCombinationChannel In tmpc.Relations
    '                        For Each week As Trinity.cWeek In tmpCC.Bookingtype.Weeks
    '                            trps += week.TRPBuyingTarget
    '                        Next
    '                    Next

    '                    For Each tmpCC As Trinity.cCombinationChannel In tmpc.Relations
    '                        For Each week As Trinity.cWeek In tmpCC.Bookingtype.Weeks
    '                            discount += week.Discount(True) * (week.TRPBuyingTarget / trps)
    '                        Next
    '                    Next
    '                    e.Value = discount
    '                Else
    '                    'Dim TmpWeek As Trinity.cWeek = DirectCast(grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag, Trinity.cWeek)
    '                    Dim discount As Double = 0
    '                    For Each week As Trinity.cWeek In grdDiscounts.Rows(e.RowIndex).Tag.weeks
    '                        trps += week.TRPBuyingTarget
    '                    Next

    '                    For Each week As Trinity.cWeek In grdDiscounts.Rows(e.RowIndex).Tag.weeks
    '                        discount += week.Discount(True) * (week.TRPBuyingTarget / trps)
    '                    Next
    '                    e.Value = discount
    '                End If
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "P1"
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue

    '            Case Is = 1
    '                Dim _trp30 As Single = 0
    '                Dim _netBudget As Single = 0

    '                If grdDiscounts.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
    '                    Dim netCPP30 As Double = 0
    '                    Dim count As Integer = 0
    '                    Dim tmpc As Trinity.cCombination = grdDiscounts.Rows(e.RowIndex).Tag

    '                    For Each tmpCC As Trinity.cCombinationChannel In tmpc.Relations
    '                        For Each tmpweek As Trinity.cWeek In tmpCC.Bookingtype.Weeks
    '                            _trp30 += tmpweek.TRPBuyingTarget * (tmpweek.SpotIndex / 100)
    '                            _netBudget += tmpweek.NetBudget
    '                        Next
    '                        'count += 1
    '                        'netCPP30 += (tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).NetCPP30(True) * tmpCC.Percent)
    '                        '_trp30 += tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget / (tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).SpotIndex / 100)
    '                        '_netBudget += tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).NetBudget
    '                    Next
    '                    'netCPP30 = _netBudget / _trp30
    '                    e.Value = _netBudget / _trp30
    '                Else
    '                    Dim TmpBT As Trinity.cBookingType = DirectCast(grdDiscounts.Rows(e.RowIndex).Tag, Trinity.cBookingType)
    '                    For Each tmpWeek As Trinity.cWeek In TmpBT.Weeks
    '                        _trp30 += tmpWeek.TRPBuyingTarget * (tmpWeek.SpotIndex / 100)
    '                        _netBudget += tmpWeek.NetBudget
    '                    Next
    '                    e.Value = _netBudget / _trp30
    '                End If
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "N1"
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue


    '            Case Is = 2
    '                If grdDiscounts.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
    '                    Dim tmpc As Trinity.cCombination = grdDiscounts.Rows(e.RowIndex).Tag
    '                    For Each tmpCC As Trinity.cCombinationChannel In tmpc.Relations
    '                        For Each week As Trinity.cWeek In tmpCC.Bookingtype.Weeks
    '                            netbudget += week.NetBudget
    '                            trps += week.TRP
    '                        Next
    '                    Next
    '                    If trps > 0 Then
    '                        e.Value = netbudget / trps
    '                    Else
    '                        e.Value = 0
    '                    End If
    '                Else
    '                    For Each week As Trinity.cWeek In grdDiscounts.Rows(e.RowIndex).Tag.weeks
    '                        netbudget += week.NetBudget
    '                        trps += week.TRP
    '                    Next
    '                    e.Value = netbudget / trps
    '                End If


    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "N1"
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue
    '                '   End If
    '            Case Is = 3


    '        End Select
    '    Else

    '        Select Case e.RowIndex Mod 4
    '            'all first rows of the channels, the discount
    '            Case Is = 0
    '                If grdDiscounts.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
    '                    Dim discount As Double = 0
    '                    Dim count As Integer = 0
    '                    Dim tmpc As Trinity.cCombination = grdDiscounts.Rows(e.RowIndex).Tag
    '                    For Each tmpCC As Trinity.cCombinationChannel In tmpc.Relations
    '                        count += 1
    '                        discount += (tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).Discount(True) * tmpCC.Percent)
    '                    Next
    '                    e.Value = discount
    '                Else
    '                    Dim TmpWeek As Trinity.cWeek = DirectCast(grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag, Trinity.cWeek)

    '                    e.Value = TmpWeek.Discount(True)
    '                End If
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "P1"
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue

    '            Case Is = 1 'all second rows of the channels, the NetCPP30
    '                If grdDiscounts.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
    '                    Dim netCPP30 As Double = 0
    '                    Dim count As Integer = 0
    '                    Dim tmpc As Trinity.cCombination = grdDiscounts.Rows(e.RowIndex).Tag
    '                    For Each tmpCC As Trinity.cCombinationChannel In tmpc.Relations
    '                        count += 1
    '                        netCPP30 += (tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).NetCPP30(True) * tmpCC.Percent)
    '                    Next

    '                    e.Value = netCPP30
    '                Else
    '                    Dim TmpWeek As Trinity.cWeek = DirectCast(grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag, Trinity.cWeek)

    '                    e.Value = TmpWeek.NetCPP30()
    '                End If
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "N1"
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue

    '            Case Is = 2 'all third rows of the channels, the actual CPP buying target
    '                'If grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then

    '                Dim i As Integer = e.RowIndex \ 4

    '                ' If grdTRP.Rows((i * 2) + 1).AllCells.Count < e.ColumnIndex Then

    '                If grdTRP.Rows((i * 2) + 1).Cells(e.ColumnIndex).Value = 0 Then
    '                    e.Value = "-"
    '                Else
    '                    If grdDiscounts.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
    '                        Dim tmpc As Trinity.cCombination = grdDiscounts.Rows(e.RowIndex).Tag
    '                        Dim TmpBudget As Single = 0
    '                        Dim TmpTRP As Single = 0
    '                        For Each tmpCC As Trinity.cCombinationChannel In tmpc.Relations
    '                            TmpBudget += tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).NetBudget
    '                            TmpTRP += tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).TRP
    '                        Next
    '                        If TmpTRP > 0 Then
    '                            e.Value = TmpBudget / TmpTRP
    '                        Else
    '                            e.Value = 0
    '                        End If
    '                    Else
    '                        e.Value = DirectCast(grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag, Trinity.cWeek).NetCPP / (DirectCast(grdDiscounts.Rows(e.RowIndex).Tag, Trinity.cBookingType).IndexMainTarget / 100)
    '                    End If
    '                End If

    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "N1"
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue
    '                '   End If

    '            Case Is = 3 'all fourth rows of the channels, the index
    '                If grdDiscounts.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
    '                    Dim index As Double = 0
    '                    Dim count As Integer = 0
    '                    Dim tmpc As Trinity.cCombination = grdDiscounts.Rows(e.RowIndex).Tag
    '                    For Each tmpCC As Trinity.cCombinationChannel In tmpc.Relations
    '                        count += 1
    '                        index += ((tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).Index * 100) * tmpCC.Percent)
    '                    Next

    '                    e.Value = index
    '                Else
    '                    Dim TmpWeek As Trinity.cWeek = DirectCast(grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag, Trinity.cWeek)

    '                    e.Value = TmpWeek.Index() * 100
    '                End If
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "N0"
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue

    '        End Select
    '    End If

    'End Sub

    Private Sub grdBudget_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdBudget.CellMouseClick
        If e.RowIndex = -1 Then Exit Sub
        If e.ColumnIndex < 2 Then Exit Sub
        If e.ColumnIndex = grdBudget.ColumnCount - 1 Then Exit Sub
        If e.RowIndex = grdBudget.Rows.Count - 1 Then Exit Sub

        If e.Button = Windows.Forms.MouseButtons.Right AndAlso e.ColumnIndex < grdBudget.ColumnCount - 1 AndAlso e.ColumnIndex > 1 Then
            Dim mnu As New System.Windows.Forms.ContextMenuStrip
            Dim strWeek As String = grdTRP.Columns(e.ColumnIndex - 2).HeaderText

            Dim sendItem As Object

            'we can lock the weeks so they are not included when you fex set a total budget
            With DirectCast(mnu.Items.Add("Lock/Unlock week " & grdBudget.Columns(e.ColumnIndex).HeaderText, Nothing, AddressOf LockWeek), System.Windows.Forms.ToolStripMenuItem)
                .Tag = grdBudget.Columns(e.ColumnIndex).HeaderText
            End With

            sendItem = grdBudget.Rows(e.RowIndex).Tag

            With DirectCast(mnu.Items.Add("Lock/Unlock channel: " & sendItem.ToString, Nothing, AddressOf LockChannel), System.Windows.Forms.ToolStripMenuItem)
                .Tag = sendItem
            End With

            Dim item As System.Windows.Forms.ToolStripItem = mnu.Items.Add("Copy to clipboard", Nothing, AddressOf BudgettoClipboard)
            item.Tag = "CopyBudget"
            item.Name = "CopyBudget"

            mnu.Show(Me.ParentForm, New System.Drawing.Point(MousePosition.X, MousePosition.Y - 15))
        End If
    End Sub

    Private Sub grdBudget_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdBudget.CellValueNeeded
        Try
            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            'The two fist columns shows % of budget and TRPS
            If e.ColumnIndex = 0 Then
                'set the % of TRPs

                'we gray these out since they are not entreable
                grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleCantSetP

                If e.RowIndex = grdBudget.Rows.Count - 1 Then
                    e.Value = "100 %"
                ElseIf grdGrandSum.Tag = 0 Then
                    e.Value = "0%"
                Else
                    e.Value = grdSumChannels.Rows(e.RowIndex * 2 + 1).Cells(e.ColumnIndex).Tag / grdGrandSum.Tag
                End If
            ElseIf e.ColumnIndex = 1 Then

                'we gray these out since they are not entreable
                grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleCantSetP

                If e.RowIndex = grdBudget.Rows.Count - 1 Then
                    e.Value = "100 %"
                Else

                    Dim SumBudget As Double = 0
                    Dim SumGrandBudget As Double = 0
                    Dim r As Integer
                    Dim TmpWeek As Trinity.cWeek

                    ' Split to calculate differently with net and grossbudget

                    If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                        'we sum up the total
                        For r = 0 To grdBudget.Rows.Count - 2

                            'goes trough the rows and sumarize them
                            If grdBudget.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                                For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(r).Tag.relations
                                    If Not tmpCC.Bookingtype.IsCompensation Then
                                        For Each TmpWeek In tmpCC.Bookingtype.Weeks
                                            SumGrandBudget += TmpWeek.NetBudget
                                        Next
                                    End If
                                Next
                            Else
                                Dim TmpBT As Trinity.cBookingType
                                TmpBT = grdBudget.Rows(r).Tag
                                If Not TmpBT.IsCompensation Then
                                    For Each TmpWeek In TmpBT.Weeks
                                        SumGrandBudget += TmpWeek.NetBudget
                                    Next
                                End If
                            End If
                        Next

                        'sum up the channel/media
                        'check if it is a combination
                        If grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                            For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(e.RowIndex).Tag.relations
                                If Not tmpCC.Bookingtype.IsCompensation Then
                                    For Each TmpWeek In tmpCC.Bookingtype.Weeks
                                        SumBudget += TmpWeek.NetBudget
                                    Next
                                End If
                            Next
                        Else
                            Dim TmpBT As Trinity.cBookingType = grdBudget.Rows(e.RowIndex).Tag
                            If Not TmpBT.IsCompensation Then
                                For Each week As Trinity.cWeek In TmpBT.Weeks
                                    SumBudget += week.NetBudget
                                Next
                            End If
                        End If
                        If SumGrandBudget > 0 Then
                            e.Value = SumBudget / SumGrandBudget
                        Else
                            e.Value = 0
                        End If
                    ElseIf cmdBudgetType.SelectedItem Is BudgetType.GrossBudget Then
                        'we sum up the total
                        For r = 0 To grdBudget.Rows.Count - 2

                            'goes trough the rows and sumarize them
                            If grdBudget.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                                For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(r).Tag.relations
                                    If Not tmpCC.Bookingtype.IsCompensation Then
                                        For Each TmpWeek In tmpCC.Bookingtype.Weeks
                                            SumGrandBudget += TmpWeek.GrossBudget
                                        Next
                                    End If
                                Next
                            Else
                                Dim TmpBT As Trinity.cBookingType
                                TmpBT = grdBudget.Rows(r).Tag
                                If Not TmpBT.IsCompensation Then
                                    For Each TmpWeek In TmpBT.Weeks
                                        SumGrandBudget += TmpWeek.GrossBudget
                                    Next
                                End If
                            End If
                        Next

                        'sum up the channel/media
                        'check if it is a combination
                        If grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                            For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(e.RowIndex).Tag.relations
                                If Not tmpCC.Bookingtype.IsCompensation Then
                                    For Each TmpWeek In tmpCC.Bookingtype.Weeks
                                        SumBudget += TmpWeek.GrossBudget
                                    Next
                                End If
                            Next
                        Else
                            Dim TmpBT As Trinity.cBookingType = grdBudget.Rows(e.RowIndex).Tag
                            If Not TmpBT.IsCompensation Then
                                For Each week As Trinity.cWeek In TmpBT.Weeks
                                    SumBudget += week.GrossBudget
                                Next
                            End If
                        End If
                        If SumGrandBudget > 0 Then
                            e.Value = SumBudget / SumGrandBudget
                        Else
                            e.Value = 0
                        End If
                    End If
                End If
            ElseIf e.ColumnIndex = grdBudget.Columns.Count - 1 Then
                'The summary to the right
                Dim sum As Double = 0
                Dim r As Integer = e.RowIndex
                Dim bolCTC As Boolean = False
                Dim TmpBT As Trinity.cBookingType
                If grdBudget.Rows(e.RowIndex).Tag Is Nothing OrElse grdBudget.Rows(e.RowIndex).Tag.GetType Is GetType(Trinity.cBookingType) Then
                    TmpBT = grdBudget.Rows(e.RowIndex).Tag
                Else
                    TmpBT = DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations(1).Bookingtype
                End If

                If Not TmpBT Is Nothing AndAlso TmpBT.IsCompensation Then
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleCompensation
                ElseIf cmbDisplay.SelectedIndex < DisplayModeEnum.PercentOfWeek AndAlso (TmpBT Is Nothing OrElse Not (TmpBT.IsPremium AndAlso TmpBT.IsSpecific)) Then
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNormal
                Else
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleCantSetN0
                End If

                'by some reason you need to do it like this or else if won't work
                If e.RowIndex = grdBudget.Rows.Count - 1 Then bolCTC = True

                For c As Integer = 2 To grdBudget.Columns.Count - 2
                    sum += grdBudget.Rows(r).Cells(c).Value
                Next

                If bolCTC Then
                    'update the CTC
                    SetCTC()
                End If

                e.Value = sum
            Else

                'the weekly budgets
                If e.RowIndex = grdBudget.Rows.Count - 1 Then

                    If cmbDisplay.SelectedIndex = DisplayModeEnum.TRP Then
                        grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNormal
                    Else
                        grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleCantSetN0
                    End If

                    Dim sum As Double = 0
                    For r As Integer = 0 To grdBudget.Rows.Count - 2
                        If grdBudget.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" OrElse Not DirectCast(grdBudget.Rows(r).Tag, Trinity.cBookingType).IsCompensation Then
                            sum += grdBudget.Rows(r).Cells(e.ColumnIndex).Value
                        End If
                    Next
                    e.Value = sum
                Else
                    If grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                        Dim sum As Double
                        For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(e.RowIndex).Tag.relations
                            If Not tmpCC.Bookingtype.IsCompensation Then

                                ' Change what to calculate based on the selected type
                                If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                                    sum += tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).NetBudget
                                Else
                                    sum += tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).GrossBudget
                                End If

                                If cmbDisplay.SelectedIndex = DisplayModeEnum.TRP Then
                                    If tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).TRPControl Then
                                        If tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).IsLocked OrElse (tmpCC.Bookingtype.IsLocked AndAlso Not (tmpCC.Bookingtype.IsPremium AndAlso tmpCC.Bookingtype.IsSpecific)) Then
                                            grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNoSetLocked
                                        Else
                                            grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNoSet
                                        End If
                                    Else
                                        If tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).IsLocked OrElse (tmpCC.Bookingtype.IsLocked AndAlso Not (tmpCC.Bookingtype.IsPremium AndAlso tmpCC.Bookingtype.IsSpecific)) Then
                                            grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNormalLocked
                                        Else
                                            grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNormal
                                        End If
                                    End If
                                Else
                                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleCantSetN0
                                End If
                            End If
                        Next

                        e.Value = sum
                    Else
                        Dim TmpWeek As Trinity.cWeek = grdBudget.Rows(e.RowIndex).Tag.weeks(grdBudget.Columns(e.ColumnIndex).HeaderText)
                        Dim TmpBT As Trinity.cBookingType = grdBudget.Rows(e.RowIndex).Tag

                        If cmbDisplay.SelectedIndex = DisplayModeEnum.TRP AndAlso Not (TmpBT.IsPremium AndAlso TmpBT.IsSpecific) Then
                            If TmpWeek.TRPControl Then
                                If TmpBT.IsCompensation Then
                                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleCompensation
                                ElseIf TmpWeek.IsLocked OrElse TmpBT.IsLocked Then
                                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNoSetLocked
                                Else
                                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNoSet
                                End If
                            Else
                                If TmpBT.IsCompensation Then
                                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleCompensation
                                ElseIf TmpWeek.IsLocked OrElse TmpBT.IsLocked Then
                                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNormalLocked
                                Else
                                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNormal
                                End If
                            End If
                        Else
                            grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleCantSetN0
                        End If

                        ' Set the value depending on selected type
                        If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                            e.Value = TmpWeek.NetBudget
                        Else
                            e.Value = TmpWeek.GrossBudget
                        End If

                    End If
                End If
            End If
        Catch ex As NullReferenceException
            Windows.Forms.MessageBox.Show("There was an error while rendering the allocate window." & vbNewLine & "This is usually caused by a week/channel/film etc being removed in Setup while the Allocate window is open." & vbNewLine & "Allocate will now close, please re-open it. If the error persists - contact trinity@mecglobal.com.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End Try
    End Sub

    Private Sub SetCTC()
        'sets the sum for the CTC label (Cost to Client)
        lblCTC.Left = grdBudget.Right - lblCTC.Width
        lblCTCLabel.Left = lblCTC.Left - lblCTCLabel.Width
        lblCTC.Text = Format(Campaign.PlannedTotCTC, "C0")

        'if the budget is exceeded the number will be in red, otherwize in green
        If Campaign.PlannedTotCTC > Campaign.BudgetTotalCTC Then
            lblCTC.ForeColor = Drawing.Color.Red
        Else
            lblCTC.ForeColor = Drawing.Color.Green
        End If


    End Sub

    Private Sub grdBudget_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdBudget.CellValuePushed
        If e.RowIndex < 0 Or e.ColumnIndex < 2 Then Exit Sub
        If cmbDisplay.SelectedIndex > 0 Then Exit Sub
        Saved = False

        '
        'Old decrapted way of doing it
        'Try
        '    Dim numberCandidate As Double = CDbl(e.Value)
        'Catch ex As Exception
        '    Exit Sub
        'End Try

        ' New way, withour try catch
        Dim numberCandidate As Double
        If (Not Double.TryParse(e.Value, numberCandidate)) Then
            Exit Sub
        End If




        If e.ColumnIndex = grdBudget.Columns.Count - 1 AndAlso e.RowIndex = grdBudget.Rows.Count - 1 Then
            'if the grand sum is entered
            'get the change ratio, all sums needs to be claculated with this ratio
            Dim newValue As Double = e.Value
            Dim oldValue = grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            Dim sngSum As Single = 0
            For r As Integer = 0 To grdBudget.Rows.Count - 2 'not the last row since its the cell we are currently in
                If grdBudget.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    If Not grdBudget.Rows(r).Tag.relations(1).bookingtype.islocked Then
                        sngSum += grdBudget.Rows(r).Cells(grdBudget.Columns.Count - 1).Value
                    End If
                Else
                    If Not grdBudget.Rows(r).Tag.islocked Then
                        sngSum += grdBudget.Rows(r).Cells(grdBudget.Columns.Count - 1).Value
                    End If
                End If
            Next

            Dim ratio As Double
            If sngSum <> 0 Then
                ratio = (newValue - (oldValue - sngSum)) / sngSum
            Else
                ratio = 0
            End If

            Dim setList As New List(Of String)
            For r As Integer = 0 To grdBudget.Rows.Count - 2 'not the last row since its the cell we are currently in
                If grdBudget.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    If Not grdBudget.Rows(r).Tag.relations(1).bookingtype.islocked Then
                        If Not setList.Contains(DirectCast(grdBudget.Rows(r).Tag, Trinity.cCombination).Name) Then
                            setList.Add(DirectCast(grdBudget.Rows(r).Tag, Trinity.cCombination).Name)
                            grdBudget.Rows(r).Cells(grdBudget.Columns.Count - 1).Value *= ratio
                        End If
                    End If
                Else
                    If Not grdBudget.Rows(r).Tag.islocked Then
                        Dim CombName As String = Nothing
                        For Each Comb As Trinity.cCombination In Campaign.Combinations
                            For Each CC As Trinity.cCombinationChannel In Comb.Relations
                                If CC.Bookingtype.ToString = grdBudget.Rows(r).Tag.ToString Then
                                    CombName = Comb.Name
                                End If
                            Next
                        Next
                        If Not setList.Contains(CombName) Then
                            grdBudget.Rows(r).Cells(grdBudget.Columns.Count - 1).Value *= ratio

                            For Each TmpC As Trinity.cCombination In Campaign.Combinations
                                If TmpC.HasRelations And CombName IsNot Nothing Then
                                    setList.Add(CombName)
                                End If
                            Next
                        End If
                    End If
                End If
            Next

        ElseIf e.ColumnIndex = grdBudget.Columns.Count - 1 AndAlso Not e.RowIndex = grdBudget.Rows.Count - 1 Then
            'if a row sum is entered


            If grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                If grdBudget.Rows(e.RowIndex).Tag.Relations(1).Bookingtype.islocked Then
                    MsgBox("This bookingtype is locked, if you wish to change TRPs you need to unlock it first", MsgBoxStyle.Information, "Locked Week")
                    Exit Sub
                End If
            Else
                If grdBudget.Rows(e.RowIndex).Tag.islocked AndAlso Not grdBudget.Rows(e.RowIndex).Tag.isCompensation Then
                    MsgBox("This bookingtype is locked, if you wish to change TRPs you need to unlock it first", MsgBoxStyle.Information, "Locked Week")
                    Exit Sub
                End If
            End If
            'check and make sure the bookingtype is not locked
            Dim newValue As Double = e.Value
            Dim oldValue = grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

            Dim sngSum As Single = 0

            If grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                For c As Integer = 2 To grdBudget.ColumnCount - 2
                    If Not grdBudget.Rows(e.RowIndex).Tag.relations(1).bookingtype.weeks(grdBudget.Columns(c).HeaderText).islocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(e.RowIndex).Tag.relations
                            If Not tmpCC.Bookingtype.IsCompensation Then

                                ' Check for selected mode
                                If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                                    sngSum += tmpCC.Bookingtype.Weeks(grdBudget.Columns(c).HeaderText).NetBudget
                                Else
                                    sngSum += tmpCC.Bookingtype.Weeks(grdBudget.Columns(c).HeaderText).GrossBudget
                                End If
                            End If
                        Next
                    End If
                Next
            Else
                For c As Integer = 2 To grdBudget.ColumnCount - 2
                    Dim TmpWeek As Trinity.cWeek = grdBudget.Rows(e.RowIndex).Tag.weeks(grdBudget.Columns(c).HeaderText)
                    If Not TmpWeek.IsLocked Then

                        ' Check for selected mode
                        If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                            sngSum += TmpWeek.NetBudget
                        Else
                            sngSum += TmpWeek.GrossBudget
                        End If
                    End If
                Next
            End If

            Dim ratio As Double

            If sngSum <> 0 Then
                ratio = (newValue - (oldValue - sngSum)) / sngSum
            Else
                ratio = 0
            End If

            For c As Integer = 2 To grdBudget.Columns.Count - 2 'dont use the % columns or the sum columns
                If grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    If Not grdBudget.Rows(e.RowIndex).Tag.relations(1).bookingtype.weeks(grdBudget.Columns(c).HeaderText).islocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(e.RowIndex).Tag.relations
                            If Not tmpCC.Bookingtype.IsCompensation Then
                                ' Check for selected mode
                                If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                                    tmpCC.Bookingtype.Weeks(grdBudget.Columns(c).HeaderText).NetBudget *= ratio
                                Else
                                    tmpCC.Bookingtype.Weeks(grdBudget.Columns(c).HeaderText).GrossBudget *= ratio
                                End If
                                tmpCC.Bookingtype.Weeks(grdBudget.Columns(c).HeaderText).TRPControl = False
                            End If
                        Next
                    End If
                Else
                    Dim TmpWeek As Trinity.cWeek = grdBudget.Rows(e.RowIndex).Tag.weeks(grdBudget.Columns(c).HeaderText)
                    If Not TmpWeek.IsLocked Then
                        TmpWeek.TRPControl = False
                        ' Check for selected mode
                        If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                            TmpWeek.NetBudget *= ratio
                        Else
                            TmpWeek.GrossBudget *= ratio
                        End If

                        For Each TmpCombo As Trinity.cCombination In Campaign.Combinations
                            If TmpCombo.IncludesBookingtype(grdBudget.Rows(e.RowIndex).Tag) Then
                                Dim Factor As Single
                                For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                                    If TmpCC.Bookingtype Is grdBudget.Rows(e.RowIndex).Tag Then
                                        If TmpCombo.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                                            If TmpCC.Relation > 0 Then
                                                Factor = TmpWeek.TRPBuyingTarget / TmpCC.Relation
                                            Else
                                                Factor = 1
                                            End If
                                        Else
                                            If TmpCC.Relation > 0 Then
                                                ' Check for selected mode
                                                If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                                                    Factor = TmpWeek.NetBudget / TmpCC.Relation
                                                Else
                                                    Factor = TmpWeek.GrossBudget / TmpCC.Relation
                                                End If

                                            Else
                                                Factor = 1
                                            End If
                                        End If
                                    End If
                                Next
                                For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                                    If TmpCC.Bookingtype IsNot grdBudget.Rows(e.RowIndex).Tag Then
                                        If TmpCombo.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                                            TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPBuyingTarget = Factor * TmpCC.Relation
                                            TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPControl = True
                                            TmpCC.Bookingtype.Weeks(TmpWeek.Name).RecalculateCPP()
                                        Else
                                            If TmpCC.Relation > 0 Then
                                                ' Check for selected mode
                                                If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                                                    TmpCC.Bookingtype.Weeks(TmpWeek.Name).NetBudget = Factor * TmpCC.Relation
                                                Else
                                                    TmpCC.Bookingtype.Weeks(TmpWeek.Name).GrossBudget = Factor * TmpCC.Relation
                                                End If
                                            End If
                                        End If
                                        TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPControl = False
                                    End If
                                Next
                            End If
                        Next
                    End If
                End If
            Next
        ElseIf e.RowIndex = grdBudget.Rows.Count - 1 Then
            'if a week sum is entered

            'check and make sure the bookingtype is not locked
            If grdBudget.Rows(0).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                If grdBudget.Rows(0).Tag.relations(1).Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).islocked Then
                    MsgBox("This week is locked, if you wish to change TRPs you need to unlock it first", MsgBoxStyle.Information, "Locked Week")
                    Exit Sub
                End If
            Else
                If grdBudget.Rows(0).Tag.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).islocked Then
                    MsgBox("This week is locked, if you wish to change TRPs you need to unlock it first", MsgBoxStyle.Information, "Locked Week")
                    Exit Sub
                End If
            End If

            'get the change ratio, all sums needs to be claculated with this ratio
            Dim newValue As Double = e.Value
            Dim oldValue = grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

            Dim sngSum As Single = 0
            For r As Integer = 0 To grdBudget.Rows.Count - 2 '-2 because the last ow is the sum row
                If grdBudget.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    If Not grdBudget.Rows(r).Tag.relations(1).bookingtype.islocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(r).Tag.relations
                            If Not tmpCC.Bookingtype.IsCompensation Then

                                ' Check for selected mode
                                If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                                    sngSum += tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).NetBudget
                                Else
                                    sngSum += tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).GrossBudget
                                End If
                            End If
                        Next
                    End If
                Else
                    Dim TmpWeek As Trinity.cWeek = grdBudget.Rows(r).Tag.weeks(grdBudget.Columns(e.ColumnIndex).HeaderText)
                    If Not grdTRP.Rows(r * 2).Tag.islocked Then

                        ' Check for selected mode
                        If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                            sngSum += TmpWeek.NetBudget
                        Else
                            sngSum += TmpWeek.GrossBudget
                        End If

                    End If
                End If
            Next

            Dim ratio As Double
            If sngSum <> 0 Then
                ratio = (newValue - (oldValue - sngSum)) / sngSum
            Else
                ratio = 0
            End If

            For r As Integer = 0 To grdBudget.Rows.Count - 2 'not the last row since its the cell we are currently in
                If grdBudget.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    If Not grdBudget.Rows(r).Tag.relations(1).bookingtype.islocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(r).Tag.relations
                            If Not tmpCC.Bookingtype.IsCompensation Then

                                ' Check for selected mode
                                If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                                    tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).NetBudget *= ratio
                                Else
                                    tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).GrossBudget *= ratio
                                End If

                                tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).TRPControl = False
                            End If
                        Next
                    End If
                Else
                    Dim TmpWeek As Trinity.cWeek = grdBudget.Rows(r).Tag.weeks(grdBudget.Columns(e.ColumnIndex).HeaderText)
                    If Not grdTRP.Rows(r * 2).Tag.islocked Then
                        TmpWeek.TRPControl = False

                        ' Check for selected mode
                        If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                            TmpWeek.NetBudget *= ratio
                        Else
                            TmpWeek.GrossBudget *= ratio
                        End If

                    End If
                End If
            Next
        Else
            'if a regular week is entered
            If grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(e.RowIndex).Tag.relations
                    If Not tmpCC.Bookingtype.IsCompensation Then

                        ' Check for selected mode
                        If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                            tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).NetBudget = e.Value * tmpCC.Percent
                        Else
                            tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).GrossBudget = e.Value * tmpCC.Percent
                        End If

                        tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).TRPControl = False
                    End If
                Next
            Else
                Dim TmpWeek As Trinity.cWeek = grdBudget.Rows(e.RowIndex).Tag.weeks(grdBudget.Columns(e.ColumnIndex).HeaderText)
                TmpWeek.TRPControl = False

                ' Check for selected mode
                If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                    TmpWeek.NetBudget = e.Value
                Else
                    TmpWeek.GrossBudget = e.Value
                End If


                For Each TmpCombo As Trinity.cCombination In Campaign.Combinations
                    If TmpCombo.HasRelations Then
                        If TmpCombo.IncludesBookingtype(grdBudget.Rows(e.RowIndex).Tag) Then
                            Dim Factor As Single
                            For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                                If TmpCC.Bookingtype Is grdBudget.Rows(e.RowIndex).Tag Then
                                    If TmpCombo.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                                        Factor = TmpWeek.TRPBuyingTarget / TmpCC.Percent
                                    Else
                                        ' Check for selected mode
                                        If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                                            Factor = TmpWeek.NetBudget / TmpCC.Percent
                                        Else
                                            Factor = TmpWeek.GrossBudget / TmpCC.Percent
                                        End If
                                    End If
                                End If
                            Next
                            For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                                If TmpCC.Bookingtype IsNot grdBudget.Rows(e.RowIndex).Tag Then
                                    If TmpCombo.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                                        TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPBuyingTarget = Factor * TmpCC.Percent
                                        'we need to set this to true for the CalcCPP function for calculate correct
                                        TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPControl = True
                                        TmpCC.Bookingtype.Weeks(TmpWeek.Name).RecalculateCPP()
                                        TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPControl = False
                                    Else
                                        ' Check for selected mode
                                        If cmdBudgetType.SelectedItem Is BudgetType.NetBudget Then
                                            TmpCC.Bookingtype.Weeks(TmpWeek.Name).NetBudget = Factor * TmpCC.Percent
                                        Else
                                            TmpCC.Bookingtype.Weeks(TmpWeek.Name).GrossBudget = Factor * TmpCC.Percent
                                        End If
                                    End If
                                    TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPControl = False
                                End If
                            Next
                        End If
                    End If
                Next

            End If
        End If

        'update all cells
        grdBudget.Invalidate()
        grdTRP.Invalidate()
        grdSumChannels.Invalidate()
        grdSumWeeks.Invalidate()



    End Sub



    Private Sub cmdNaturalDelivery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNaturalDelivery.Click
        Dim EstimationPeriod As Trinity.AllocateFunctions.EstimationPeriodEnum
        If mnuLastWeeks.Checked Then
            EstimationPeriod = Trinity.AllocateFunctions.EstimationPeriodEnum.LastWeeks
        ElseIf mnuLastYear.Checked Then
            EstimationPeriod = Trinity.AllocateFunctions.EstimationPeriodEnum.LastYear
        Else
            EstimationPeriod = Trinity.AllocateFunctions.EstimationPeriodEnum.CustomPeriod

        End If
        Trinity.AllocateFunctions.CalculateNaturalDelivery(grdIndex, EstimationPeriod, Campaign, cmdNaturalDelivery.Tag)
    End Sub

    Private Sub cmdIndexSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIndexSettings.Click
        mnuSetup.Show(cmdIndexSettings, New System.Drawing.Point(0, cmdIndexSettings.Height))
    End Sub

    Private Sub mnuLastWeeks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLastWeeks.Click
        mnuLastWeeks.Checked = True
        mnuLastYear.Checked = False
        mnuUseCustom.Checked = False
    End Sub

    Private Sub mnuLastYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLastYear.Click
        mnuLastWeeks.Checked = False
        mnuLastYear.Checked = True
        mnuUseCustom.Checked = False
    End Sub

    Private Sub cmdCopyToAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCopyToAll.Click
        Saved = False
        Dim mnuCopyToChannels As New Windows.Forms.ContextMenuStrip
        mnuCopyToChannels.Items.Clear()

        'Show combinations or combinationsChannels
        For Each tmpC As Trinity.cCombination In Campaign.Combinations
            If tmpC.ShowAsOne Then
                mnuCopyToChannels.Items.Add(tmpC.Name.ToString(), Nothing, AddressOf CopyFromChannels)
            Else
                For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                    mnuCopyToChannels.Items.Add(tmpCC.ChannelName + " " + tmpCC.BookingTypeName, Nothing, AddressOf CopyFromChannels)
                Next
            End If
        Next
        'Show channels
        For Each tmpChan As Trinity.cChannel In Campaign.Channels
            For Each tmpBt As Trinity.cBookingType In tmpChan.BookingTypes
                If tmpBt.BookIt Then
                    If tmpBt.Combination Is Nothing Then
                        mnuCopyToChannels.Items.Add(tmpChan.ChannelName + " " + tmpBt.Name, Nothing, AddressOf CopyFromChannels)
                    End If
                End If
            Next
        Next
        mnuCopyToChannels.Items.Add("Copy to split to all", Nothing, AddressOf CopytToAll)
        mnuCopyToChannels.Show(cmdCopyToAll, 0, cmdCopyToAll.Height)
    End Sub

    Public Sub CopytToAll()

        Dim Chan As String
        Dim BT As String
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek
        Dim TmpFilm As Trinity.cFilm
        If Not Campaign.RootCampaign Is Nothing Then Exit Sub

        If cmbFilmChannel.SelectedItem.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            Dim tmpC As Trinity.cCombination = cmbFilmChannel.SelectedItem
            Dim tmpCC As Trinity.cCombinationChannel = tmpC.Relations(1)

            For Each TmpWeek In tmpCC.Bookingtype.Weeks
                For Each TmpFilm In TmpWeek.Films
                    For Each TmpChan In Campaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            TmpBT.Weeks(TmpWeek.Name).Films(TmpFilm.Name).Share = TmpFilm.Share
                        Next
                    Next
                    For Each tmpC2 As Trinity.cCombination In Campaign.Combinations
                        If tmpC2.ShowAsOne Then
                            For Each tmpCC2 As Trinity.cCombinationChannel In tmpC.Relations
                                tmpCC2.Bookingtype.Weeks(TmpWeek.Name).Films(TmpFilm.Name).Share = TmpFilm.Share
                            Next
                        End If
                    Next
                Next
            Next

        ElseIf cmbFilmChannel.SelectedItem.GetType.FullName = "clTrinity.Trinity.cBookingType" Then
            Chan = DirectCast(cmbFilmChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
            BT = DirectCast(cmbFilmChannel.SelectedItem, Trinity.cBookingType).Name
            For Each TmpWeek In Campaign.Channels(Chan).BookingTypes(BT).Weeks
                For Each TmpFilm In TmpWeek.Films
                    For Each TmpChan In Campaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            If TmpBT.BookIt Then
                                TmpBT.Weeks(TmpWeek.Name).Films(TmpFilm.Name).Share = TmpFilm.Share
                            End If
                        Next
                    Next
                    For Each tmpC As Trinity.cCombination In Campaign.Combinations
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            tmpCC.Bookingtype.Weeks(TmpWeek.Name).Films(TmpFilm.Name).Share = TmpFilm.Share
                        Next
                    Next
                Next
            Next
        Else
            For Each row As DataGridViewRow In grdFilms.Rows
                If row.Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    Dim tmpC As Trinity.cCombination = row.Tag
                    Dim tmpCC As Trinity.cCombinationChannel = tmpC.Relations(1)

                    For Each TmpWeek In tmpCC.Bookingtype.Weeks
                        For Each TmpFilm In TmpWeek.Films
                            For Each TmpChan In Campaign.Channels
                                For Each TmpBT In TmpChan.BookingTypes
                                    TmpBT.Weeks(TmpWeek.Name).Films(TmpFilm.Name).Share = TmpFilm.Share
                                Next
                            Next
                            For Each tmpC2 As Trinity.cCombination In Campaign.Combinations
                                If tmpC2.ShowAsOne Then
                                    For Each tmpCC2 As Trinity.cCombinationChannel In tmpC.Relations
                                        tmpCC2.Bookingtype.Weeks(TmpWeek.Name).Films(TmpFilm.Name).Share = TmpFilm.Share
                                    Next
                                End If
                            Next
                        Next
                    Next

                ElseIf row.Tag.GetType.FullName = "clTrinity.Trinity.cBookingtype" Then
                    Chan = DirectCast(row.Tag, Trinity.cBookingType).ParentChannel.ChannelName
                    BT = DirectCast(row.Tag, Trinity.cBookingType).Name
                    For Each TmpWeek In Campaign.Channels(Chan).BookingTypes(BT).Weeks
                        For Each TmpFilm In TmpWeek.Films
                            For Each TmpChan In Campaign.Channels
                                For Each TmpBT In TmpChan.BookingTypes
                                    If TmpBT.BookIt Then
                                        TmpBT.Weeks(TmpWeek.Name).Films(TmpFilm.Name).Share = TmpFilm.Share
                                    End If
                                Next
                            Next
                            For Each tmpC As Trinity.cCombination In Campaign.Combinations
                                For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                    tmpCC.Bookingtype.Weeks(TmpWeek.Name).Films(TmpFilm.Name).Share = TmpFilm.Share
                                Next
                            Next
                        Next
                    Next
                End If
            Next
        End If
        grdFilms.Invalidate()
        grdDiscounts.Invalidate()
        'update all the grids since the costs and TRP is changed it you change the films
        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
        grdBudget.Invalidate()
        grdSpotCount.Invalidate()
        grdSumChannels.Invalidate()
        ColorFilmGrid()

    End Sub

    Sub mnuLab_ItemClick_old(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim TmpBookedSpots As Trinity.cBookedSpots
        'Dim TmpPlannedSpots As Trinity.cPlannedSpots
        'Dim TmpActualSpots As Trinity.cActualSpots
        'Dim TmpHistory As System.Collections.Generic.Dictionary(Of String, Trinity.cKampanj)
        'Dim TmpCampaigns As System.Collections.Generic.Dictionary(Of String, Trinity.cKampanj)
        'Dim TmpCosts As Trinity.cCosts
        Dim TmpWeek As Trinity.cWeek
        Dim TmpBT As Trinity.cBookingType
        Dim TmpAV As Trinity.cAddedValue
        'Dim c As Integer
        'Dim r As Integer

        If System.Windows.Forms.MessageBox.Show("Are you sure you want to use the allocation from:" & vbCrLf & sender.text & vbCrLf & vbCrLf & "You will loose you current allocation. Continue?", "TRINITY", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question) <> vbYes Then Exit Sub

        Dim TmpCampaign As New Trinity.cKampanj(False)
        TmpCampaign.Area = Campaign.Area
        TmpCampaign.LoadCampaign("", True, Campaign.Campaigns(sender.text).SaveCampaign("", True, True, True))
        'Campaign = TmpCampaign

        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                Dim wn As Integer = 0
                For Each TmpWeek In TmpBT.Weeks
                    wn += 1
                    TmpWeek.TRPControl = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name).TRPControl
                    If TmpWeek.TRPControl Then
                        TmpWeek.TRPBuyingTarget = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name).TRPBuyingTarget
                    Else
                        TmpWeek.NetBudget = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name).NetBudget
                    End If
                    For Each TmpFilm As Trinity.cFilm In TmpWeek.Films
                        TmpFilm.Share = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name).Films(TmpFilm.Name).Share
                    Next
                    For Each TmpAV In TmpBT.AddedValues
                        If Not TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).AddedValues(TmpAV.ID) Is Nothing Then
                            TmpAV.Amount(wn) = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).AddedValues(TmpAV.ID).Amount(wn)
                        End If
                    Next
                Next
                TmpBT.IndexAllAdults = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).IndexAllAdults
                TmpBT.IndexMainTarget = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).IndexMainTarget
                TmpBT.IndexSecondTarget = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).IndexSecondTarget
                TmpBT.BookIt = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).BookIt
            Next
        Next
        'Campaign.BookedSpots = TmpBookedSpots
        'Campaign.PlannedSpots = TmpPlannedSpots
        'Campaign.ActualSpots = TmpActualSpots
        'Campaign.History = TmpHistory
        'Campaign.Campaigns = TmpCampaigns
        'Campaign.Costs = TmpCosts
        'For c = 0 To grdTRP.Columns.Count - 1
        '    For r = 0 To grdBudget.Rows.Count - 2
        '        TmpBT = grdTRP.Rows(r * 2).Tag
        '        TmpWeek = grdTRP.Rows(r * 2).Cells(c).Tag
        '        grdTRP.Rows(r * 2).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name)
        '        grdTRP.Rows(r * 2 + 1).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name)
        '        grdTRP.Rows(r * 2).Cells(c).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name)
        '        grdTRP.Rows(r).Cells(c).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name)
        '        grdBudget.Rows(r).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name)
        '        grdBudget.Rows(r).Cells(c + 2).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name)
        '        grdDiscounts.Rows(r * 3).Cells(c).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name)
        '        grdDiscounts.Rows(r * 3 + 1).Cells(c).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name)
        '        grdDiscounts.Rows(r * 3 + 2).Cells(c).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name)
        '        grdDiscounts.Rows(r * 3).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name)
        '        grdDiscounts.Rows(r * 3 + 1).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name)
        '        grdDiscounts.Rows(r * 3 + 2).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name)
        '        grdIndex.Rows(r).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name)
        '    Next
        '    For r = 0 To grdAV.Rows.Count - 1
        '        TmpAV = grdAV.Rows(r).Tag
        '        grdAV.Rows(r).Tag = Campaign.Channels(TmpAV.Bookingtype.ParentChannel.ChannelName).BookingTypes(TmpAV.Bookingtype.Name).AddedValues(TmpAV.ID)
        '    Next
        'Next
        ''cmbUniverse_SelectedIndexChanged(New Object, New EventArgs)
        ''cmbFilmChannel_SelectedIndexChanged(New Object, New EventArgs)
        frmAllocate_Load(New Object, New EventArgs)
        'Me.Close()
        'Me.Show()
    End Sub


    Sub mnuLab_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim TmpBookedSpots As Trinity.cBookedSpots = Campaign.BookedSpots
        Dim TmpPlannedSpots As Trinity.cPlannedSpots = Campaign.PlannedSpots
        Dim TmpActualSpots As Trinity.cActualSpots = Campaign.ActualSpots
        Dim TmpHistory As System.Collections.Generic.Dictionary(Of String, Trinity.cKampanj) = Campaign.History
        Dim TmpCampaigns As System.Collections.Generic.Dictionary(Of String, Trinity.cKampanj) = Campaign.Campaigns
        Dim TmpCosts As Trinity.cCosts = Campaign.Costs
        Dim TmpAdtoox As Trinity.cAdtoox = Campaign.AdToox
        Dim TmpFilename As String = Campaign.Filename
        Dim TmpFS As IO.FileStream = Campaign.fs

        Dim TmpName As String = Campaign.Name
        Dim TmpClientID As Short = Campaign.ClientID
        Dim TmpProductID As Short = Campaign.ProductID
        Dim TmpPlanner As String = Campaign.Planner
        Dim TmpBuyer As String = Campaign.Buyer

        'Dim TmpWeek As Trinity.cWeek
        'Dim TmpBT As Trinity.cBookingType
        'Dim TmpAV As Trinity.cAddedValue
        'Dim c As Integer
        'Dim r As Integer

        If System.Windows.Forms.MessageBox.Show("Are you sure you want to use the allocation from:" & vbCrLf & sender.text & vbCrLf & vbCrLf & "You will loose you current allocation. Continue?", "TRINITY", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question) <> vbYes Then Exit Sub

        Campaign.ErrorCheckingEnabled = False
        Dim TmpCampaign As New Trinity.cKampanj(TrinitySettings.ErrorChecking)
        TmpCampaign.Area = Campaign.Area
        TmpCampaign.LoadCampaign(TmpFilename, True, Campaign.Campaigns(sender.text).SaveCampaign(TmpFilename, True, True, True))
        Campaign = TmpCampaign

        'For Each TmpChan As Trinity.cChannel In Campaign.Channels
        '    For Each TmpBT In TmpChan.BookingTypes
        '        Dim wn As Integer = 0
        '        For Each TmpWeek In TmpBT.Weeks
        '            wn += 1
        '            TmpWeek.TRPControl = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name).TRPControl
        '            If TmpWeek.TRPControl Then
        '                TmpWeek.TRPBuyingTarget = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name).TRPBuyingTarget
        '            Else
        '                TmpWeek.NetBudget = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name).NetBudget
        '            End If
        '            For Each TmpFilm As Trinity.cFilm In TmpWeek.Films
        '                TmpFilm.Share = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name).Films(TmpFilm.Name).Share
        '            Next
        '            For Each TmpAV In TmpBT.AddedValues
        '                If Not TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).AddedValues(TmpAV.ID) Is Nothing Then
        '                    TmpAV.Amount(wn) = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).AddedValues(TmpAV.ID).Amount(wn)
        '                End If
        '            Next
        '        Next
        '        TmpBT.IndexAllAdults = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).IndexAllAdults
        '        TmpBT.IndexMainTarget = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).IndexMainTarget
        '        TmpBT.IndexSecondTarget = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).IndexSecondTarget
        '        TmpBT.BookIt = TmpCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).BookIt
        '    Next
        'Next

        Campaign.BookedSpots = TmpBookedSpots
        Campaign.PlannedSpots = TmpPlannedSpots
        Campaign.ActualSpots = TmpActualSpots
        Campaign.History = TmpHistory
        Campaign.Campaigns = TmpCampaigns
        Campaign.Costs = TmpCosts
        Campaign.AdToox = TmpAdtoox
        Campaign.fs = TmpFS
        Campaign.Name = TmpName

        'For c = 0 To grdTRP.Columns.Count - 1
        '    For r = 0 To grdBudget.Rows.Count - 2
        '        TmpBT = grdTRP.Rows(r * 2).Tag
        '        TmpWeek = grdTRP.Rows(r * 2).Cells(c).Tag
        '        grdTRP.Rows(r * 2).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name)
        '        grdTRP.Rows(r * 2 + 1).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name)
        '        grdTRP.Rows(r * 2).Cells(c).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name)
        '        grdTRP.Rows(r).Cells(c).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name)
        '        grdBudget.Rows(r).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name)
        '        grdBudget.Rows(r).Cells(c + 2).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name)
        '        grdDiscounts.Rows(r * 3).Cells(c).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name)
        '        grdDiscounts.Rows(r * 3 + 1).Cells(c).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name)
        '        grdDiscounts.Rows(r * 3 + 2).Cells(c).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Weeks(TmpWeek.Name)
        '        grdDiscounts.Rows(r * 3).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name)
        '        grdDiscounts.Rows(r * 3 + 1).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name)
        '        grdDiscounts.Rows(r * 3 + 2).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name)
        '        grdIndex.Rows(r).Tag = Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name)
        '    Next
        '    For r = 0 To grdAV.Rows.Count - 1
        '        TmpAV = grdAV.Rows(r).Tag
        '        grdAV.Rows(r).Tag = Campaign.Channels(TmpAV.Bookingtype.ParentChannel.ChannelName).BookingTypes(TmpAV.Bookingtype.Name).AddedValues(TmpAV.ID)
        '    Next
        'Next
        'cmbUniverse_SelectedIndexChanged(New Object, New EventArgs)
        'cmbFilmChannel_SelectedIndexChanged(New Object, New EventArgs)
        frmAllocate_Load(New Object, New EventArgs)
        Me.Close()
        Me.Show()
    End Sub

    Private Sub cmdLab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLab.Click
        'put the values into the lab for comparison
        Dim mnuLab As New System.Windows.Forms.ContextMenuStrip
        Dim kv As KeyValuePair(Of String, Trinity.cKampanj)

        For Each kv In Campaign.Campaigns
            mnuLab.Items.Add(kv.Key, Nothing, AddressOf mnuLab_ItemClick)
        Next
        mnuLab.Show(cmdLab, New System.Drawing.Point(0, cmdLab.Height))
    End Sub

    Private Sub grdDiscounts_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDiscounts.CellDoubleClick

        If e.ColumnIndex = grdDiscounts.ColumnCount - 1 Then Exit Sub
        Trinity.AllocateFunctions.ShowDiscountWindow(grdDiscounts.Rows(e.RowIndex).Tag, grdDiscounts.Columns(e.ColumnIndex).HeaderText)

    End Sub


    Private Sub lblCTC_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCTC.DoubleClick, lblCTCLabel.DoubleClick
        'opens a details window about the CTC, how the budget was calculated
        Dim TmpCost As Trinity.cCost
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim r As Integer
        Dim SumUnit As Decimal
        Dim UnitStr As String = ""


        'Gross budget
        frmDetails.grdDetails.Rows.Clear()
        frmDetails.grdDetails.Rows.Add(50)
        frmDetails.grdDetails.Rows(0).Cells(0).Value = "Gross"
        frmDetails.grdDetails.Rows(0).Cells(2).Value = Format(Campaign.PlannedGross, "##,##0 kr")


        'Costs that are percent costs on the ratecard prices
        r = 1
        For Each TmpCost In Campaign.Costs
            If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnRatecard Then
                    frmDetails.grdDetails.Rows(r).Cells(0).Value = TmpCost.CostName
                    frmDetails.grdDetails.Rows(r).Cells(1).Value = Format(TmpCost.Amount, "P2")
                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpCost.Amount * Campaign.PlannedGross, "##,##0 kr")
                    frmDetails.grdDetails.Rows(r).Cells(0).Style = styleBlue
                    frmDetails.grdDetails.Rows(r).Cells(1).Style = styleBlue
                    frmDetails.grdDetails.Rows(r).Cells(2).Style = styleBlue
                    r = r + 1
                End If
            End If
        Next

        'Costs that are based on the media net, like maybe a service fee based on net after discounts
        frmDetails.grdDetails.Rows(r).Cells(0).Value = "Media Net"
        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(Campaign.PlannedMediaNet, "##,##0 kr")
        r += 1
        For Each TmpCost In Campaign.Costs
            If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnMediaNet Then
                    frmDetails.grdDetails.Rows(r).Cells(0).Value = TmpCost.CostName
                    frmDetails.grdDetails.Rows(r).Cells(1).Value = Format(TmpCost.Amount, "P2")
                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpCost.Amount * Campaign.PlannedMediaNet, "##,##0 kr")
                    frmDetails.grdDetails.Rows(r).Cells(0).Style = styleBlue
                    frmDetails.grdDetails.Rows(r).Cells(1).Style = styleBlue
                    frmDetails.grdDetails.Rows(r).Cells(2).Style = styleBlue
                    r = r + 1
                End If
            End If
        Next
        frmDetails.grdDetails.Rows(r).Cells(0).Style.ForeColor = Drawing.Color.Red
        frmDetails.grdDetails.Rows(r).Cells(1).Style.ForeColor = Drawing.Color.Red
        frmDetails.grdDetails.Rows(r).Cells(2).Style.ForeColor = Drawing.Color.Red
        frmDetails.grdDetails.Rows(r).Cells(0).Value = "Commission"
        If Campaign.PlannedMediaNet = 0 Then
            frmDetails.grdDetails.Rows(r).Cells(1).Value = "0,0%"
        Else
            frmDetails.grdDetails.Rows(r).Cells(1).Value = Format(-(Campaign.EstimatedCommission / Campaign.PlannedMediaNet), "0.0%")
        End If
        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(-Campaign.EstimatedCommission, "##,##0 kr")
        r = r + 1
        frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net"
        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(Campaign.PlannedNet, "##,##0 kr")
        r = r + 1
        For Each TmpCost In Campaign.Costs
            If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNet Then
                    frmDetails.grdDetails.Rows(r).Cells(0).Value = TmpCost.CostName
                    frmDetails.grdDetails.Rows(r).Cells(1).Value = Format(TmpCost.Amount, "0.0%")
                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpCost.Amount * Campaign.PlannedNet, "##,##0 kr")
                    frmDetails.grdDetails.Rows(r).Cells(0).Style = styleBlue
                    frmDetails.grdDetails.Rows(r).Cells(1).Style = styleBlue
                    frmDetails.grdDetails.Rows(r).Cells(2).Style = styleBlue
                    r = r + 1
                End If
            ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeFixed Then
                frmDetails.grdDetails.Rows(r).Cells(0).Value = TmpCost.CostName
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpCost.Amount, "##,##0 kr")
                frmDetails.grdDetails.Rows(r).Cells(0).Style = styleBlue
                frmDetails.grdDetails.Rows(r).Cells(2).Style = styleBlue
                r = r + 1
            ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeOnDiscount Then
                SumUnit = 0
                If TmpCost.CountCostOn Is Nothing Then
                    For Each TmpChan In Campaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                                Dim Discount As Single = TmpWeek.GrossBudget - TmpWeek.NetBudget
                                SumUnit += (Discount * TmpCost.Amount)
                            Next
                        Next
                    Next
                Else
                    For Each TmpBT In DirectCast(TmpCost.CountCostOn, Trinity.cChannel).BookingTypes
                        For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                            Dim Discount As Single = TmpWeek.GrossBudget - TmpWeek.NetBudget
                            SumUnit += (Discount * TmpCost.Amount)
                        Next
                    Next
                End If
                frmDetails.grdDetails.Rows(r).Cells(0).Value = TmpCost.CostName
                frmDetails.grdDetails.Rows(r).Cells(1).Value = Format(TmpCost.Amount, "P1")
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(SumUnit, "##,##0 kr")
                frmDetails.grdDetails.Rows(r).Cells(0).Style = styleBlue
                frmDetails.grdDetails.Rows(r).Cells(1).Style = styleBlue
                frmDetails.grdDetails.Rows(r).Cells(2).Style = styleBlue
                r = r + 1
            ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePerUnit Then
                SumUnit = 0
                If TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots Then
                    For Each TmpChan In Campaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            If TmpBT.BookIt Then
                                SumUnit = SumUnit + TmpBT.EstimatedSpotCount * TmpCost.Amount
                            End If
                        Next
                    Next
                    UnitStr = " / Spot"
                ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP Then
                    For Each TmpChan In Campaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            If TmpBT.BookIt Then
                                SumUnit = SumUnit + TmpBT.TotalTRPBuyingTarget * TmpCost.Amount
                            End If
                        Next
                    Next
                    UnitStr = " / TRP"
                ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP Then
                    For Each TmpChan In Campaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            If TmpBT.BookIt Then
                                SumUnit = SumUnit + TmpBT.TotalTRP * TmpCost.Amount
                            End If
                        Next
                    Next
                    UnitStr = " / TRP"
                ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnWeeks Then
                    UnitStr = " / Week"
                    SumUnit = Campaign.Channels(1).BookingTypes(1).Weeks.Count * TmpCost.Amount
                End If
                frmDetails.grdDetails.Rows(r).Cells(0).Value = TmpCost.CostName
                frmDetails.grdDetails.Rows(r).Cells(1).Value = Format(TmpCost.Amount, "0 kr") & UnitStr
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(SumUnit, "##,##0 kr")
                frmDetails.grdDetails.Rows(r).Cells(0).Style = styleBlue
                frmDetails.grdDetails.Rows(r).Cells(1).Style = styleBlue
                frmDetails.grdDetails.Rows(r).Cells(2).Style = styleBlue
                r = r + 1
            End If
        Next
        frmDetails.grdDetails.Rows(r).Cells(0).Value = "NetNet"
        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(Campaign.PlannedNetNet, "##,##0 kr")
        r = r + 1
        For Each TmpCost In Campaign.Costs
            If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNetNet Then
                    frmDetails.grdDetails.Rows(r).Cells(0).Value = TmpCost.CostName
                    frmDetails.grdDetails.Rows(r).Cells(1).Value = Format(TmpCost.Amount, "0.0%")
                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpCost.Amount * Campaign.PlannedNetNet, "##,##0 kr")
                    frmDetails.grdDetails.Rows(r).Cells(0).Style = styleBlue
                    frmDetails.grdDetails.Rows(r).Cells(1).Style = styleBlue
                    frmDetails.grdDetails.Rows(r).Cells(2).Style = styleBlue
                    r = r + 1
                End If
            End If
        Next
        frmDetails.grdDetails.Rows(r).Cells(0).Value = "CTC"
        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(Campaign.PlannedTotCTC, "##,##0 kr")

        While frmDetails.grdDetails.Rows.Count > r + 1
            frmDetails.grdDetails.Rows.Remove(frmDetails.grdDetails.Rows(frmDetails.grdDetails.Rows.Count - 1))
        End While
        frmDetails.ShowDialog()

    End Sub

    Private Sub grdSpotCount_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSpotCount.CellEndEdit
        Saved = False
    End Sub


    Private Sub grdSpotCount_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSpotCount.CellValueNeeded
        'updates the grid on a number of events (window activation, mouse movement etc)
        If e.RowIndex < 0 Then Exit Sub

        If Not grdSpotCount.Rows(e.RowIndex).Tag Is Nothing AndAlso grdSpotCount.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            'a combined row
            Dim tmpC As Trinity.cCombination = grdSpotCount.Rows(e.RowIndex).Tag
            If e.ColumnIndex < 0 Then
                e.Value = tmpC.Name
            ElseIf e.ColumnIndex = 0 Then
                Dim sum As Double
                For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                    sum += tmpCC.Bookingtype.AverageRating * tmpCC.Percent
                Next
                e.Value = Format(sum, "N1")
            ElseIf e.ColumnIndex = 1 Then
                Dim sum As Double
                Dim sum2 As Double
                For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                    sum += tmpCC.Bookingtype.AverageRating * tmpCC.Percent
                    sum2 += tmpCC.Bookingtype.EstimatedSpotCount
                Next

                If sum > 0 Then
                    e.Value = Format(sum2, "N0")
                Else
                    e.Value = 0
                End If
            End If

            'end of combination row
        Else
            'a normal non combined row
            Dim TmpBT As Trinity.cBookingType = grdSpotCount.Rows(e.RowIndex).Tag
            If e.ColumnIndex < 0 Then
                If e.RowIndex = grdSpotCount.RowCount - 1 Then
                    e.Value = "TOTAL:"
                Else
                    e.Value = TmpBT.ParentChannel.Shortname & " " & TmpBT.Shortname
                End If
            ElseIf e.ColumnIndex = 0 Then
                If e.RowIndex = grdSpotCount.RowCount - 1 Then
                    e.Value = "-"
                Else
                    e.Value = Format(TmpBT.AverageRating, "N1")
                End If
            ElseIf e.ColumnIndex = 1 Then
                If e.RowIndex = grdSpotCount.RowCount - 1 Then
                    Dim SpotCount As Integer = 0
                    For i As Integer = 0 To grdSpotCount.RowCount - 2
                        SpotCount += grdSpotCount.Rows(i).Cells(1).Value
                    Next
                    e.Value = Format(SpotCount, "N0")
                Else
                    If TmpBT.AverageRating > 0 Then
                        e.Value = Format(TmpBT.EstimatedSpotCount, "N0")
                    Else
                        e.Value = 0
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub grdSpotCount_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSpotCount.CellValuePushed
        'when a change is made to the grid this code does the updates needed
        If grdSpotCount.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            Dim tmpC As Trinity.cCombination = grdSpotCount.Rows(e.RowIndex).Tag

            If e.ColumnIndex = 0 Then
                For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                    tmpCC.Bookingtype.AverageRating = e.Value
                Next
                grdSpotCount.InvalidateCell(grdSpotCount.Rows(e.RowIndex).Cells(1))
                grdSpotCount.InvalidateRow(grdSpotCount.RowCount - 1)
            ElseIf e.ColumnIndex = 1 Then
                If e.Value > 0 Then
                    For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                        tmpCC.Bookingtype.AverageRating = tmpCC.Bookingtype.PlannedTRP(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) / (e.Value * tmpCC.Percent)
                    Next
                Else
                    For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                        tmpCC.Bookingtype.AverageRating = 0
                    Next
                End If
                grdSpotCount.InvalidateCell(grdSpotCount.Rows(e.RowIndex).Cells(0))
                grdSpotCount.InvalidateRow(grdSpotCount.RowCount - 1)
            End If
        Else
            Dim TmpBT As Trinity.cBookingType = grdSpotCount.Rows(e.RowIndex).Tag
            If e.ColumnIndex = 0 Then
                TmpBT.AverageRating = e.Value
                grdSpotCount.InvalidateCell(grdSpotCount.Rows(e.RowIndex).Cells(1))
                grdSpotCount.InvalidateRow(grdSpotCount.RowCount - 1)
            ElseIf e.ColumnIndex = 1 Then
                If e.Value > 0 Then
                    TmpBT.AverageRating = TmpBT.PlannedTRP(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) / e.Value
                Else
                    TmpBT.AverageRating = 0
                End If
                grdSpotCount.InvalidateCell(grdSpotCount.Rows(e.RowIndex).Cells(0))
                grdSpotCount.InvalidateRow(grdSpotCount.RowCount - 1)
            End If
        End If

        'SumBudget()
        lblCTC.Text = Format(Campaign.PlannedTotCTC, "C0")

        'if the budget is exceeded the number will be in red, otherwize in green
        If Campaign.PlannedTotCTC > Campaign.BudgetTotalCTC Then
            lblCTC.ForeColor = Drawing.Color.Red
        Else
            lblCTC.ForeColor = Drawing.Color.Green
        End If
    End Sub

    Private Sub grdTRP_CellContentClick1(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTRP.CellContentClick

    End Sub


    Private Sub grdTRP_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdTRP.CellValueNeeded
        'updates the grid on a number of events (window activation, mouse movement etc)
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpC As Trinity.cCombination
        Dim TmpWeek As Trinity.cWeek
        Dim SumTarget As Single

        'If e.RowIndex = 0 And e.ColumnIndex = 0 Then Stop

        If grdTRP.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            'if we have a single row combination
            TmpC = grdTRP.Rows(e.RowIndex).Tag
            If TmpC.IndexMainTarget <> 0 Then 'This means we can use the Indexes stored in the combination itself, not the indexes on individual booking types in the combination
                If cmbDisplay.SelectedIndex = DisplayModeEnum.TRP Then
                    'We are in the Campaign universe - we are almost never in the channel universe
                    If cmbUniverse.SelectedIndex = 0 Then
                        If e.RowIndex / 2 = e.RowIndex \ 2 Then
                            'TRPs in buying target - even rows, starting with 0
                            Dim sum As Double = 0
                            For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse TmpC.IsOnlyCompensation Then
                                    If TmpC.IndexMainTarget > 0 Then
                                        sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / (TmpC.IndexMainTarget / 100)
                                    Else
                                        sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / (tmpCC.Bookingtype.IndexMainTarget / 100)
                                    End If
                                End If
                            Next
                            e.Value = sum
                        Else
                            'TRPs in main target, odd rows, starting with 1
                            Dim sum As Double = 0
                            For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse TmpC.IsOnlyCompensation Then
                                    If cmbTargets.SelectedIndex = 0 Then
                                        sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP '* (TmpC.IndexMainTarget / 100)
                                    Else
                                        sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP '* (TmpC.IndexSecondTarget / 100)
                                    End If
                                End If
                            Next
                            e.Value = sum
                        End If
                    Else
                        If e.RowIndex / 2 = e.RowIndex \ 2 Then
                            Dim sum As Double = 0
                            For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse TmpC.IsOnlyCompensation Then
                                    sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget
                                End If
                            Next
                            e.Value = sum
                        Else
                            Dim sum As Double = 0
                            For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse TmpC.IsOnlyCompensation Then
                                    If cmbTargets.SelectedIndex = 0 Then
                                        sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget * (TmpC.IndexMainTarget / 100)
                                    Else
                                        sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget * (TmpC.IndexSecondTarget / 100)
                                    End If
                                End If
                            Next
                            e.Value = sum
                        End If
                    End If
                ElseIf cmbDisplay.SelectedIndex = DisplayModeEnum.PercentOfChannel Then
                    Dim sum As Double = 0
                    Dim _count As Integer = 0
                    For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                        If Not tmpCC.Bookingtype.IsCompensation OrElse TmpC.IsOnlyCompensation Then
                            If e.RowIndex / 2 = e.RowIndex \ 2 Then
                                sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget / tmpCC.Bookingtype.TotalTRPBuyingTarget
                            Else
                                sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / tmpCC.Bookingtype.TotalTRP
                            End If
                            _count += 1
                        End If
                    Next

                    sum = sum / _count

                    If Double.IsNaN(sum) Then
                        e.Value = 0
                    Else
                        e.Value = sum
                    End If
                    grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf cmbDisplay.SelectedIndex = DisplayModeEnum.PercentOfWeek Then
                    If e.RowIndex / 2 = e.RowIndex \ 2 Then
                        e.Value = "-"
                    Else
                        SumTarget = 0
                        For Each TmpChan In Campaign.Channels
                            For Each TmpBT In TmpChan.BookingTypes
                                If TmpBT.BookIt Then
                                    SumTarget = SumTarget + TmpBT.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP
                                End If
                            Next
                        Next

                        If SumTarget <> 0 Then
                            Dim sum As Double = 0
                            For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation Then
                                    sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / SumTarget
                                End If
                            Next
                            e.Value = sum
                        Else
                            e.Value = 0
                        End If
                        grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                    End If
                End If
            Else
                'No index stored in the combination, using the values in the booking types in the combination
                If cmbDisplay.SelectedIndex = DisplayModeEnum.TRP Then
                    If cmbUniverse.SelectedIndex = 0 Then
                        If e.RowIndex / 2 = e.RowIndex \ 2 Then
                            Dim sum As Double = 0
                            For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                                If tmpCC.Bookingtype.IndexMainTarget > 0 Then
                                    sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / (tmpCC.Bookingtype.IndexMainTarget / 100)
                                End If
                            Next
                            e.Value = sum
                        Else
                            Dim sum As Double = 0
                            For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation Then
                                    If cmbTargets.SelectedIndex = 0 Then
                                        sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP
                                    Else
                                        sum += (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / (tmpCC.Bookingtype.IndexMainTarget / 100)) * (tmpCC.Bookingtype.IndexSecondTarget / 100)
                                    End If
                                End If
                            Next
                            e.Value = sum
                        End If
                    Else
                        If e.RowIndex / 2 = e.RowIndex \ 2 Then
                            Dim sum As Double = 0
                            For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation Then
                                    sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget
                                End If
                            Next
                            e.Value = sum
                        Else
                            Dim sum As Double = 0
                            For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation Then
                                    If cmbTargets.SelectedIndex = 0 Then
                                        sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget * (tmpCC.Bookingtype.IndexMainTarget / 100)
                                    Else
                                        sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget * (tmpCC.Bookingtype.IndexSecondTarget / 100)
                                    End If
                                End If
                            Next
                            e.Value = sum
                        End If
                    End If
                ElseIf cmbDisplay.SelectedIndex = DisplayModeEnum.PercentOfChannel Then
                    Dim sum As Double = 0
                    Dim _count As Integer = 0
                    For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                        If Not tmpCC.Bookingtype.IsCompensation Then
                            If e.RowIndex / 2 = e.RowIndex \ 2 Then
                                sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget / tmpCC.Bookingtype.TotalTRPBuyingTarget
                            Else
                                sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / tmpCC.Bookingtype.TotalTRP
                            End If
                            _count += 1
                        End If
                    Next

                    sum = sum / _count

                    If Double.IsNaN(sum) Then
                        e.Value = 0
                    Else
                        e.Value = sum
                    End If
                    grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf cmbDisplay.SelectedIndex = DisplayModeEnum.PercentOfWeek Then
                    If e.RowIndex / 2 = e.RowIndex \ 2 Then
                        e.Value = "-"
                    Else
                        SumTarget = 0
                        For Each TmpChan In Campaign.Channels
                            For Each TmpBT In TmpChan.BookingTypes
                                If TmpBT.BookIt Then
                                    SumTarget = SumTarget + TmpBT.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP
                                End If
                            Next
                        Next

                        If SumTarget <> 0 Then
                            Dim sum As Double = 0
                            For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation Then
                                    sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / SumTarget
                                End If
                            Next
                            e.Value = sum
                        Else
                            e.Value = 0
                        End If
                        grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                    End If
                End If
            End If
        Else
            'normal version,it's not a combination
            TmpBT = grdTRP.Rows(e.RowIndex).Tag
            TmpWeek = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag

            If cmbDisplay.SelectedIndex = DisplayModeEnum.TRP Then
                If cmbUniverse.SelectedIndex = 0 Then
                    If e.RowIndex / 2 = e.RowIndex \ 2 Then
                        If (TmpBT.IndexMainTarget / 100) > 0 Then
                            e.Value = Format(TmpWeek.TRP / (TmpBT.IndexMainTarget / 100), "N1")
                        Else
                            SkipIt = True
                            e.Value = 0
                        End If
                    Else
                        If cmbTargets.SelectedIndex = 0 Then
                            e.Value = TmpWeek.TRP
                        Else
                            e.Value = (TmpWeek.TRP / (TmpBT.IndexMainTarget / 100)) * ((TmpBT.IndexSecondTarget / 100))
                        End If
                    End If
                Else
                    If e.RowIndex / 2 = e.RowIndex \ 2 Then
                        e.Value = TmpWeek.TRPBuyingTarget
                    Else
                        If cmbTargets.SelectedIndex = 0 Then
                            e.Value = TmpWeek.TRPBuyingTarget * (TmpBT.IndexMainTarget / 100)
                        Else
                            e.Value = TmpWeek.TRPBuyingTarget * (TmpBT.IndexSecondTarget / 100)
                        End If
                    End If
                End If
            ElseIf cmbDisplay.SelectedIndex = DisplayModeEnum.PercentOfChannel Then
                If TmpBT.TotalTRP <> 0 Then
                    If e.RowIndex / 2 = e.RowIndex \ 2 Then
                        e.Value = TmpWeek.TRPBuyingTarget / TmpBT.TotalTRPBuyingTarget
                    Else
                        e.Value = TmpWeek.TRP / TmpBT.TotalTRP
                    End If
                Else
                    e.Value = 0
                End If
                grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True

            ElseIf cmbDisplay.SelectedIndex = DisplayModeEnum.TRP30 Then

                If cmbUniverse.SelectedIndex = 0 Then
                    If e.RowIndex / 2 = e.RowIndex \ 2 Then
                        If (TmpBT.IndexMainTarget / 100) > 0 Then
                            e.Value = Format(TmpWeek.TRP30 / (TmpBT.IndexMainTarget / 100), "N1")
                        Else
                            SkipIt = True
                            e.Value = 0
                        End If
                    Else
                        If cmbTargets.SelectedIndex = 0 Then
                            e.Value = Format(TmpWeek.TRP30, "N1")
                        Else
                            e.Value = Format((TmpWeek.TRP30 / (TmpBT.IndexMainTarget / 100)) * ((TmpBT.IndexSecondTarget / 100)), "N1")
                        End If
                    End If
                Else
                    If e.RowIndex / 2 = e.RowIndex \ 2 Then
                        e.Value = Format(TmpWeek.TRPBuyingTarget30, "N1")
                    Else
                        If cmbTargets.SelectedIndex = 0 Then
                            e.Value = Format((TmpWeek.TRPBuyingTarget30) * (TmpBT.IndexMainTarget / 100), "N1")
                        Else
                            e.Value = Format((TmpWeek.TRPBuyingTarget30) * (TmpBT.IndexSecondTarget / 100), "N1")
                        End If
                    End If
                End If
            ElseIf cmbDisplay.SelectedIndex = DisplayModeEnum.Imp000 Then
                If cmbUniverse.SelectedIndex = 0 Then
                    If e.RowIndex / 2 = e.RowIndex \ 2 Then
                        e.Value = Format(TmpWeek.ImpressionsBuyingTarget000, "N0")
                    Else
                        e.Value = Format(TmpWeek.Impressions000, "N0")
                    End If
                End If
            ElseIf cmbDisplay.SelectedIndex = DisplayModeEnum.PercentOfWeek Then
                If e.RowIndex / 2 = e.RowIndex \ 2 Then
                    e.Value = "-"
                Else
                    SumTarget = 0
                    For Each TmpChan In Campaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            If TmpBT.BookIt Then
                                SumTarget = SumTarget + TmpBT.Weeks(TmpWeek.Name).TRP
                            End If
                        Next
                    Next

                    If SumTarget <> 0 Then
                        e.Value = TmpWeek.TRP / SumTarget
                    Else
                        e.Value = "0%"
                    End If
                    grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                End If
            End If
        End If


    End Sub

    Private Sub grdTRP_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdTRP.CellValuePushed
        'when a change is made to the grid this code does the updates needed
        If cmbDisplay.SelectedIndex > DisplayModeEnum.TRP Then Exit Sub

        Try
            Dim numberCandidate As Double = CDbl(e.Value)
        Catch ex As Exception
            Exit Sub
        End Try

        Saved = False
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek
        Dim ScrollLeft As Integer = Me.DisplayRectangle.X

        If grdTRP.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            'a one line combo row
            Dim tmpC As Trinity.cCombination
            tmpC = grdTRP.Rows(e.RowIndex).Tag
            If tmpC.IndexMainTarget <> 0 Then

                'If the combination is on TRPs start here
                If tmpC.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                    If cmbUniverse.SelectedIndex = 0 Then
                        If e.RowIndex / 2 <> e.RowIndex \ 2 Then
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    If cmbTargets.SelectedIndex = 0 Then
                                        tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value * tmpCC.Percent) / tmpC.IndexMainTarget
                                    Else
                                        tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value / tmpC.IndexSecondTarget) * tmpCC.Percent
                                    End If
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex))
                        Else
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = e.Value * tmpCC.Percent '(e.Value * (tmpC.IndexMainTarget)) * tmpCC.Percent
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex))
                        End If
                    Else
                        If e.RowIndex / 2 <> e.RowIndex \ 2 Then
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value / tmpC.IndexMainTarget) * tmpCC.Percent
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex))
                        Else
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget = (e.Value * tmpCC.Percent)
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex))
                        End If
                    End If
                    'If the combination is on budget start here
                Else
                    'Simply set the budget for each bookingtype in this week to be its percentage of the whole * 1 million, resulting in values between 0 and 1 000 000
                    'Then set the TRP control to false because budget is controlling the TRPs
                    For Each TmpCC As Trinity.cCombinationChannel In tmpC.Relations
                        If Not TmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                            TmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).NetBudget = (TmpCC.Percent) * 1000000
                            TmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = False
                        End If
                    Next

                    'Add all the current TRPs for this week in the combination
                    Dim sumTRP As Double
                    For Each TmpCC As Trinity.cCombinationChannel In tmpC.Relations
                        If Not TmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                            sumTRP += TmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP
                        End If
                    Next

                    If cmbUniverse.SelectedIndex = 0 Then
                        'If its an odd row, meaning we entered TRPs for the main target
                        If e.RowIndex / 2 <> e.RowIndex \ 2 Then
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    If sumTRP > 0 Then
                                        If cmbTargets.SelectedIndex = 0 Then
                                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = e.Value * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / sumTRP)
                                        Else
                                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = e.Value * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / sumTRP) / (tmpCC.Bookingtype.IndexSecondTarget / 100)
                                        End If
                                    Else
                                        tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = 0
                                    End If
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex))

                        Else
                            'If its an even row, meanining we entered TRPs for the buying targets
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    If sumTRP > 0 Then
                                        If tmpC.IndexMainTarget > 0 Then
                                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / sumTRP)) * (tmpC.IndexMainTarget / 100)
                                        Else
                                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / sumTRP)) * (tmpCC.Bookingtype.IndexMainTarget / 100)
                                        End If
                                    Else
                                        tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = 0
                                    End If
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex))
                        End If
                    Else
                        If e.RowIndex / 2 <> e.RowIndex \ 2 Then
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value / tmpCC.Bookingtype.IndexMainTarget / 100) * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / sumTRP)
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex))
                        Else
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget = (e.Value * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / sumTRP))
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex))
                        End If
                    End If
                End If
            Else
                If tmpC.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                    If cmbUniverse.SelectedIndex = 0 Then
                        If e.RowIndex / 2 <> e.RowIndex \ 2 Then
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    If cmbTargets.SelectedIndex = 0 Then
                                        tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value * tmpCC.Percent)
                                    Else
                                        tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = ((e.Value / (tmpCC.Bookingtype.IndexSecondTarget / 100)) * (tmpCC.Bookingtype.IndexMainTarget / 100) * tmpCC.Percent)
                                    End If
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex))
                        Else
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value * (tmpCC.Bookingtype.IndexMainTarget / 100)) * tmpCC.Percent
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex))
                        End If
                    Else
                        If e.RowIndex / 2 <> e.RowIndex \ 2 Then
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value / tmpCC.Bookingtype.IndexMainTarget / 100) * tmpCC.Percent
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex))
                        Else
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget = (e.Value * tmpCC.Percent)
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex))
                        End If
                    End If
                Else

                    For Each TmpCC As Trinity.cCombinationChannel In tmpC.Relations
                        If Not TmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                            TmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).NetBudget = (TmpCC.Percent) * 1000000
                            TmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = False
                        End If
                    Next

                    Dim sumTRP As Double
                    For Each TmpCC As Trinity.cCombinationChannel In tmpC.Relations
                        If Not TmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                            sumTRP += TmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP
                        End If
                    Next

                    If cmbUniverse.SelectedIndex = 0 Then
                        If e.RowIndex / 2 <> e.RowIndex \ 2 Then
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    If sumTRP > 0 Then
                                        If cmbTargets.SelectedIndex = 0 Then
                                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = e.Value * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / sumTRP)
                                        Else
                                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = e.Value * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / sumTRP) / (tmpCC.Bookingtype.IndexSecondTarget / 100)
                                        End If
                                    Else
                                        tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = 0
                                    End If
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex))
                        Else
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    If sumTRP > 0 Then
                                        tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / sumTRP)) * (tmpCC.Bookingtype.IndexMainTarget / 100)
                                    Else
                                        tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = 0
                                    End If
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex))
                        End If
                    Else
                        If e.RowIndex / 2 <> e.RowIndex \ 2 Then
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value / tmpCC.Bookingtype.IndexMainTarget / 100) * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / sumTRP)
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex))
                        Else
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If Not tmpCC.Bookingtype.IsCompensation OrElse tmpC.IsOnlyCompensation Then
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget = (e.Value * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / sumTRP))
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                                End If
                            Next
                            grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex))
                        End If
                    End If
                End If
            End If
        Else
            'normal row, not a combination  
            TmpBT = grdTRP.Rows(e.RowIndex).Tag
            If cmbUniverse.SelectedIndex = 0 Then
                If e.RowIndex / 2 <> e.RowIndex \ 2 Then
                    TmpWeek = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
                    If cmbTargets.SelectedIndex = 0 Then
                        TmpWeek.TRP = e.Value
                    Else
                        TmpWeek.TRP = (e.Value / (TmpBT.IndexSecondTarget / 100)) * (TmpBT.IndexMainTarget / 100)
                    End If
                    grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex))
                Else
                    TmpWeek = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
                    TmpWeek.TRP = e.Value * (TmpBT.IndexMainTarget / 100)
                    grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex))
                End If
            Else
                If e.RowIndex / 2 <> e.RowIndex \ 2 Then
                    TmpWeek = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
                    TmpWeek.TRPBuyingTarget = e.Value / (TmpBT.IndexSecondTarget / 100)
                    grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex))
                Else
                    TmpWeek = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
                    TmpWeek.TRPBuyingTarget = e.Value
                    grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex))
                End If
            End If
            TmpWeek.TRPControl = True
            If Not grdTRP.Rows(e.RowIndex).Tag.IsCompensation Then
                For Each TmpCombo As Trinity.cCombination In Campaign.Combinations
                    If TmpCombo.HasRelations Then
                        If TmpCombo.IncludesBookingtype(grdTRP.Rows(e.RowIndex).Tag) Then
                            Dim Factor As Single
                            For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                                If TmpCC.Bookingtype Is grdTRP.Rows(e.RowIndex).Tag Then
                                    If TmpCombo.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                                        Factor = TmpWeek.TRPBuyingTarget / TmpCC.Relation
                                    Else
                                        Factor = TmpWeek.NetBudget / TmpCC.Relation
                                    End If
                                End If
                            Next
                            For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                                If TmpCC.Bookingtype IsNot grdTRP.Rows(e.RowIndex).Tag Then
                                    If TmpCombo.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                                        TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPBuyingTarget = Factor * TmpCC.Relation
                                    Else
                                        TmpCC.Bookingtype.Weeks(TmpWeek.Name).NetBudget = Factor * TmpCC.Relation
                                    End If
                                    TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPControl = True
                                End If
                            Next
                        End If
                    End If
                Next
            End If
        End If



        'SkipIt = True
        'updates the grids
        'SkipIt = True
        grdSpotCount.Invalidate()
        grdSumChannels.Invalidate()
        'SkipIt = True
        grdSumWeeks.Invalidate()
        grdTRP.InvalidateColumn(e.ColumnIndex)
        grdBudget.Invalidate()
    End Sub

    Private Sub cmdSpotcountSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSpotcountSettings.Click
        mnuSetup.Show(cmdSpotcountSettings, New System.Drawing.Point(0, cmdSpotcountSettings.Height))
    End Sub


    Private Sub cmdSpotcountND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSpotcountND.Click
        Saved = False
        Dim TmpAdedge As New ConnectWrapper.Breaks
        Dim TmpBT As Trinity.cBookingType
        Dim r As Integer
        Dim BreakCount As Long
        Dim TmpDay As Date
        Dim TRPDaypart() As Single
        Dim BreakcountDaypart() As Single
        Dim b As Integer
        Dim AvgRating As Single
        Dim i As Integer

        If Not Campaign.RootCampaign Is Nothing Then Exit Sub

        frmMain.Cursor = Windows.Forms.Cursors.WaitCursor
        TmpAdedge.setArea(Campaign.Area)
        If mnuLastWeeks.Checked Then
            Dim DateDiff As Long
            Dim PeriodStr As String = ""
            TmpDay = Date.FromOADate(Campaign.EndDate)
            While TmpDay.ToOADate >= TmpAdedge.getDataRangeTo(Connect.eDataType.mSpot)
                TmpDay = TmpDay.AddDays(-1)
            End While
            DateDiff = Campaign.EndDate - TmpDay.ToOADate

            For Each TmpWeek As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                PeriodStr &= Format(Date.FromOADate(TmpWeek.StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate - DateDiff), "ddMMyy") & ","
            Next
            TmpAdedge.setPeriod(PeriodStr)

        ElseIf mnuLastYear.Checked Then
            Dim StartDate As Date
            Dim EndDate As Date
            TmpDay = Date.FromOADate(Campaign.StartDate).AddYears(-1)
            While Not Weekday(TmpDay, FirstDayOfWeek.Monday) = Weekday(Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday)
                TmpDay = TmpDay.AddDays(1)
            End While
            StartDate = TmpDay
            TmpDay = Date.FromOADate(Campaign.EndDate).AddYears(-1)
            While Not Weekday(TmpDay, FirstDayOfWeek.Monday) = Weekday(Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday)
                TmpDay = TmpDay.AddDays(1)
            End While
            EndDate = TmpDay
            TmpAdedge.setPeriod(Format(StartDate, "ddMMyy") & "-" & Format(EndDate, "ddMMyy"))
        Else
            TmpAdedge.setPeriod(cmdNaturalDelivery.Tag)
        End If
        frmProgress.Status = "Calculating natural delivery..."
        frmProgress.Show()
        frmProgress.Progress = 0
        For r = 0 To grdSpotCount.Rows.Count - 2
            ReDim TRPDaypart(Trinity.cKampanj.MAX_DAYPARTS)
            ReDim BreakcountDaypart(Trinity.cKampanj.MAX_DAYPARTS)
            frmProgress.Progress = (r / (grdSpotCount.Rows.Count - 1)) * 100
            If grdSpotCount.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                Dim TmpC As Trinity.cCombination = grdSpotCount.Rows(r).Tag
                For Each TmpCC As Trinity.cCombinationChannel In TmpC.Relations
                    TmpBT = TmpCC.Bookingtype

                    TmpAdedge.setChannelsArea(TmpBT.ParentChannel.AdEdgeNames, Campaign.Area)
                    TmpAdedge.clearTargetSelection()
                    Trinity.Helper.AddTarget(TmpAdedge, TmpBT.BuyingTarget.Target)
                    TmpAdedge.setUniverseUserDefined(Campaign.MainTarget.Universe, Nothing, False)
                    Trinity.Helper.AddTimeShift(TmpAdedge)
                    TmpAdedge.clearList()
                    BreakCount = TmpAdedge.Run
                    For b = 0 To BreakCount - 1
                        TRPDaypart(TmpBT.Dayparts.GetDaypartIndexForMam(TmpAdedge.getAttrib(Connect.eAttribs.aFromTime, b) \ 60)) += TmpAdedge.getUnit(Connect.eUnits.uTRP, b, , Campaign.TimeShift)
                        BreakcountDaypart(TmpBT.Dayparts.GetDaypartIndexForMam(TmpAdedge.getAttrib(Connect.eAttribs.aFromTime, b) \ 60)) += 1
                    Next
                    AvgRating = 0
                    If Not TmpC.IsOnlyCompensation Then
                        For i = 0 To TmpBT.Dayparts.Count - 1
                            AvgRating += (TRPDaypart(i) / BreakcountDaypart(i)) * (TmpBT.Dayparts(i).Share / 100)
                        Next
                    End If
                    TmpBT.AverageRating = AvgRating
                Next
            Else
                TmpBT = grdSpotCount.Rows(r).Tag
                TmpAdedge.setChannelsArea(TmpBT.ParentChannel.AdEdgeNames, Campaign.Area)
                TmpAdedge.clearTargetSelection()
                Trinity.Helper.AddTarget(TmpAdedge, TmpBT.BuyingTarget.Target)
                'TmpAdedge.setUniverseUserDefined(Campaign.MainTarget.Universe)
                Trinity.Helper.AddTimeShift(TmpAdedge)
                TmpAdedge.clearList()
                BreakCount = TmpAdedge.Run
                For b = 0 To BreakCount - 1
                    TRPDaypart(TmpBT.Dayparts.GetDaypartIndexForMam(TmpAdedge.getAttrib(Connect.eAttribs.aFromTime, b) \ 60)) += TmpAdedge.getUnit(Connect.eUnits.uTRP, b, , Campaign.TimeShift)
                    BreakcountDaypart(TmpBT.Dayparts.GetDaypartIndexForMam(TmpAdedge.getAttrib(Connect.eAttribs.aFromTime, b) \ 60)) += 1
                Next
                AvgRating = 0
                For i = 0 To TmpBT.Dayparts.Count - 1
                    If Not BreakcountDaypart(i) = 0 Then
                        AvgRating += (TRPDaypart(i) / BreakcountDaypart(i)) * (TmpBT.Dayparts(i).Share / 100)
                    End If
                Next
                TmpBT.AverageRating = AvgRating
            End If
        Next
        frmProgress.Hide()
        grdBudget.Invalidate()
        grdSpotCount.Invalidate()
        frmMain.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub grdSumChannels_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdSumChannels.CellFormatting
        If e.RowIndex = 0 Then Exit Sub
        Dim TmpBT As Trinity.cBookingType
        If grdTRP.Rows(e.RowIndex).Tag.GetType Is GetType(Trinity.cBookingType) Then
            TmpBT = grdTRP.Rows(e.RowIndex).Tag
        Else
            TmpBT = DirectCast(grdTRP.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations(1).Bookingtype
        End If
        If e.RowIndex Mod 2 > 0 Then
            grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = False
            If cmbDisplay.SelectedIndex < DisplayModeEnum.PercentOfWeek Then 'TRP or TRP30 is being shown
                If (TmpBT.IsPremium AndAlso TmpBT.IsSpecific) Then
                    e.CellStyle = styleCantSetN
                    grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf TmpBT.IsCompensation Then
                    e.CellStyle = styleCompensationD
                Else
                    e.CellStyle = styleNormalD
                End If
            Else
                e.CellStyle = styleCantSetP
            End If
        Else
            grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
        End If
    End Sub

    Private Sub grdSumChannels_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSumChannels.CellValueNeeded
        '1 is because we only sum up every second row
        If e.RowIndex < 1 OrElse e.ColumnIndex < 0 Then Exit Sub
        If e.RowIndex Mod 2 = 0 Then Exit Sub

        Dim Chan As String
        Dim BT As String
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek
        Dim SumNat As Single
        Dim SumChn As Single
        Dim SumSumNat As Single
        Dim x As Integer

        Dim a As Integer

        On Error GoTo SumChannelTRP_Error
        If cmbDisplay.SelectedIndex = DisplayModeEnum.TRP And cmbTargets.SelectedIndex = 0 Then
            If grdTRP.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                Dim tmpC As Trinity.cCombination = grdTRP.Rows(e.RowIndex).Tag
                For Each cc As Trinity.cCombinationChannel In tmpC.Relations
                    If Not cc.Bookingtype.IsCompensation Then
                        TmpBT = cc.Bookingtype

                        For Each TmpWeek In TmpBT.Weeks
                            SumNat = SumNat + TmpWeek.TRP
                            SumChn = SumChn + TmpWeek.TRPBuyingTarget
                        Next
                        If chkIncludeInSums.Checked Then
                            For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                SumNat = SumNat + TmpComp.TRPMainTarget
                                SumChn = SumChn + TmpComp.TRPs
                            Next
                        End If
                        For x = 0 To grdTRP.Rows.Count - 1 Step 2
                            If grdTRP.Rows(x).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                                Dim tmpC2 As Trinity.cCombination = grdTRP.Rows(x).Tag
                                For Each cc2 As Trinity.cCombinationChannel In tmpC2.Relations
                                    TmpBT = cc2.Bookingtype
                                    For Each TmpWeek In TmpBT.Weeks
                                        SumSumNat = SumSumNat + TmpWeek.TRP
                                    Next
                                    If chkIncludeInSums.Checked Then
                                        For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                            SumSumNat = SumSumNat + TmpComp.TRPMainTarget
                                        Next
                                    End If
                                Next
                            Else
                                TmpBT = grdTRP.Rows(x).Tag
                                For Each TmpWeek In TmpBT.Weeks
                                    SumSumNat = SumSumNat + TmpWeek.TRP
                                Next
                                If chkIncludeInSums.Checked Then
                                    For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                        SumSumNat = SumSumNat + TmpComp.TRPMainTarget
                                    Next
                                End If
                            End If
                        Next
                    End If
                Next
                SumSumNat = SumSumNat / tmpC.Relations.count
            Else
                TmpBT = grdTRP.Rows(e.RowIndex).Tag

                For Each TmpWeek In TmpBT.Weeks
                    SumNat = SumNat + TmpWeek.TRP
                    SumChn = SumChn + TmpWeek.TRPBuyingTarget
                Next
                If chkIncludeInSums.Checked Then
                    For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                        SumNat = SumNat + TmpComp.TRPMainTarget
                        SumChn = SumChn + TmpComp.TRPs
                    Next
                End If
                For x = 0 To grdTRP.Rows.Count - 1 Step 2
                    If grdTRP.Rows(x).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                        Dim tmpC2 As Trinity.cCombination = grdTRP.Rows(x).Tag
                        For Each cc2 As Trinity.cCombinationChannel In tmpC2.Relations
                            If Not cc2.Bookingtype.IsCompensation Then
                                TmpBT = cc2.Bookingtype
                                For Each TmpWeek In TmpBT.Weeks
                                    SumSumNat = SumSumNat + TmpWeek.TRP
                                Next
                                If chkIncludeInSums.Checked Then
                                    For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                        SumSumNat = SumSumNat + TmpComp.TRPMainTarget
                                    Next
                                End If
                            End If
                        Next
                    Else
                        TmpBT = grdTRP.Rows(x).Tag
                        For Each TmpWeek In TmpBT.Weeks
                            SumSumNat = SumSumNat + TmpWeek.TRP
                        Next
                        If chkIncludeInSums.Checked Then
                            For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                SumSumNat = SumSumNat + TmpComp.TRPMainTarget
                            Next
                        End If
                    End If
                Next
            End If


            If e.RowIndex / 2 = e.RowIndex \ 2 Then
                grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = 0
                e.Value = ""
                Exit Sub
            End If
            If e.ColumnIndex = 0 Then
                grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = SumNat
                e.Value = SumNat
            End If
            If e.ColumnIndex = 1 Then
                e.Value = SumChn
            End If

            grdGrandSum.Tag = SumSumNat
            grdGrandSum.Invalidate()

        ElseIf cmbDisplay.SelectedIndex = DisplayModeEnum.TRP And cmbTargets.SelectedIndex = 1 Then
            If grdTRP.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                Dim tmpC As Trinity.cCombination = grdTRP.Rows(e.RowIndex).Tag
                For Each cc As Trinity.cCombinationChannel In tmpC.Relations
                    TmpBT = cc.Bookingtype

                    For Each TmpWeek In TmpBT.Weeks
                        SumNat = SumNat + TmpWeek.TRPBuyingTarget * (tmpC.IndexSecondTarget / 100)
                        SumChn = SumChn + TmpWeek.TRPBuyingTarget
                    Next
                    If chkIncludeInSums.Checked Then
                        For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                            SumNat = SumNat + TmpComp.TRPMainTarget * (tmpC.IndexSecondTarget / 100)
                            SumChn = SumChn + TmpComp.TRPs
                        Next
                    End If
                    For x = 0 To grdTRP.Rows.Count - 1 Step 2
                        If grdTRP.Rows(x).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                            Dim tmpC2 As Trinity.cCombination = grdTRP.Rows(x).Tag
                            For Each cc2 As Trinity.cCombinationChannel In tmpC2.Relations
                                If Not cc2.Bookingtype.IsCompensation Then
                                    TmpBT = cc2.Bookingtype
                                    For Each TmpWeek In TmpBT.Weeks
                                        SumSumNat = SumSumNat + TmpWeek.TRPBuyingTarget * (tmpC.IndexSecondTarget / 100)
                                    Next
                                    If chkIncludeInSums.Checked Then
                                        For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                            SumSumNat = SumSumNat + TmpComp.TRPMainTarget * (tmpC.IndexSecondTarget / 100)
                                        Next
                                    End If
                                End If
                            Next
                        Else
                            TmpBT = grdTRP.Rows(x).Tag
                            For Each TmpWeek In TmpBT.Weeks
                                SumSumNat = SumSumNat + TmpWeek.TRPBuyingTarget * (tmpC.IndexSecondTarget / 100)
                            Next
                            If chkIncludeInSums.Checked Then
                                For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                    SumSumNat = SumSumNat + TmpComp.TRPMainTarget * (tmpC.IndexSecondTarget / 100)
                                Next
                            End If
                        End If
                    Next
                Next
                SumSumNat = SumSumNat / tmpC.Relations.count
            Else
                TmpBT = grdTRP.Rows(e.RowIndex).Tag

                For Each TmpWeek In TmpBT.Weeks
                    SumNat = SumNat + TmpWeek.TRPBuyingTarget * (TmpBT.IndexSecondTarget / 100)
                    SumChn = SumChn + TmpWeek.TRPBuyingTarget
                Next
                If chkIncludeInSums.Checked Then
                    For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                        SumNat = SumNat + TmpComp.TRPMainTarget * (TmpBT.IndexSecondTarget / 100)
                        SumChn = SumChn + TmpComp.TRPs
                    Next
                End If
                For x = 0 To grdTRP.Rows.Count - 1 Step 2
                    If grdTRP.Rows(x).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                        Dim tmpC2 As Trinity.cCombination = grdTRP.Rows(x).Tag
                        For Each cc2 As Trinity.cCombinationChannel In tmpC2.Relations
                            If Not cc2.Bookingtype.IsCompensation Then
                                TmpBT = cc2.Bookingtype
                                For Each TmpWeek In TmpBT.Weeks
                                    SumSumNat = SumSumNat + TmpWeek.TRPBuyingTarget * (TmpBT.IndexSecondTarget / 100)
                                Next
                                If chkIncludeInSums.Checked Then
                                    For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                        SumSumNat = SumSumNat + TmpComp.TRPMainTarget * (TmpBT.IndexSecondTarget / 100)
                                    Next
                                End If
                            End If
                        Next
                    Else
                        TmpBT = grdTRP.Rows(x).Tag
                        For Each TmpWeek In TmpBT.Weeks
                            SumSumNat = SumSumNat + TmpWeek.TRPBuyingTarget * (TmpBT.IndexSecondTarget / 100)
                        Next
                        If chkIncludeInSums.Checked Then
                            For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                SumSumNat = SumSumNat + TmpComp.TRPMainTarget * (TmpBT.IndexSecondTarget / 100)
                            Next
                        End If
                    End If
                Next
            End If


            If e.RowIndex / 2 = e.RowIndex \ 2 Then
                grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = 0
                e.Value = ""
                Exit Sub
            End If
            If e.ColumnIndex = 0 Then
                grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = SumNat
                e.Value = SumNat
            End If
            If e.ColumnIndex = 1 Then
                e.Value = SumChn
            End If

            grdGrandSum.Tag = SumSumNat
            grdGrandSum.Invalidate()

        ElseIf cmbDisplay.SelectedIndex = DisplayModeEnum.PercentOfWeek Then 'if we have a sum %
            Dim sum As Double = 0
            Dim w As Integer = 0
            For w = 0 To grdTRP.ColumnCount - 1
                sum += grdTRP.Rows(e.RowIndex).Cells(w).Value
            Next

            e.Value = sum / w
        ElseIf cmbDisplay.SelectedIndex = DisplayModeEnum.Imp000 Then
            If grdTRP.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                '    Dim tmpC As Trinity.cCombination = grdTRP.Rows(e.RowIndex).Tag
                '    For Each cc As Trinity.cCombinationChannel In tmpC.Relations
                '        TmpBT = cc.Bookingtype

                '        For Each TmpWeek In TmpBT.Weeks
                '            SumNat = SumNat + TmpWeek.Impressions000
                '            SumChn = SumChn + TmpWeek.ImpressionsBuyingTarget000
                '        Next
                '        For x = 0 To grdTRP.Rows.Count - 1 Step 2
                '            If grdTRP.Rows(x).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                '                Dim tmpC2 As Trinity.cCombination = grdTRP.Rows(x).Tag
                '                For Each cc2 As Trinity.cCombinationChannel In tmpC2.Relations
                '                    TmpBT = cc2.Bookingtype
                '                    For Each TmpWeek In TmpBT.Weeks
                '                        SumSumNat = SumSumNat + TmpWeek.Impressions000
                '                    Next
                '                Next
                '            Else
                '                TmpBT = grdTRP.Rows(x).Tag
                '                For Each TmpWeek In TmpBT.Weeks
                '                    SumSumNat = SumSumNat + TmpWeek.Impressions000
                '                Next
                '            End If
                '        Next
                '    Next
                '    SumSumNat = SumSumNat / tmpC.Relations.count
            Else
                TmpBT = grdTRP.Rows(e.RowIndex).Tag

                'For Each TmpWeek In TmpBT.Weeks
                'SumNat = SumNat + TmpWeek.Impressions000
                'SumChn = SumChn + TmpWeek.ImpressionsBuyingTarget000
                'Next
                'For x = 0 To grdTRP.Rows.Count - 1 Step 2
                'If grdTRP.Rows(x).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                '    Dim tmpC2 As Trinity.cCombination = grdTRP.Rows(x).Tag
                '    For Each cc2 As Trinity.cCombinationChannel In tmpC2.Relations
                '        TmpBT = cc2.Bookingtype
                '        For Each TmpWeek In TmpBT.Weeks
                '            SumSumNat = SumSumNat + TmpWeek.Impressions000
                '        Next
                '    Next
                'Else
                '    TmpBT = grdTRP.Rows(x).Tag
                '    For Each TmpWeek In TmpBT.Weeks
                '        SumSumNat = SumSumNat + TmpWeek.Impressions000
                '    Next
                'End If
                'Next
            End If


            'If e.RowIndex / 2 = e.RowIndex \ 2 Then
            '    grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = 0
            '    e.Value = ""
            '    Exit Sub
            'End If
            'If e.ColumnIndex = 0 Then
            '    grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = SumNat
            '    e.Value = SumNat
            'End If
            'If e.ColumnIndex = 1 Then
            '    e.Value = SumChn
            'End If

            'grdGrandSum.Tag = SumSumNat
            'grdGrandSum.Invalidate()
        ElseIf cmbDisplay.SelectedIndex = DisplayModeEnum.TRP30 Then

            If grdTRP.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                Dim tmpC As Trinity.cCombination = grdTRP.Rows(e.RowIndex).Tag
                For Each cc As Trinity.cCombinationChannel In tmpC.Relations
                    TmpBT = cc.Bookingtype

                    For Each TmpWeek In TmpBT.Weeks
                        SumNat = SumNat + TmpWeek.TRP * (TmpWeek.SpotIndex / 100)
                        SumChn = SumChn + TmpWeek.TRPBuyingTarget * (TmpWeek.SpotIndex / 100)
                    Next
                    If chkIncludeInSums.Checked Then
                        For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                            SumNat = SumNat + TmpComp.TRPMainTarget
                            SumChn = SumChn + TmpComp.TRPs
                        Next
                    End If
                    For x = 0 To grdTRP.Rows.Count - 1 Step 2
                        If grdTRP.Rows(x).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                            Dim tmpC2 As Trinity.cCombination = grdTRP.Rows(x).Tag
                            For Each cc2 As Trinity.cCombinationChannel In tmpC2.Relations
                                If Not cc2.Bookingtype.IsCompensation Then
                                    TmpBT = cc2.Bookingtype
                                    For Each TmpWeek In TmpBT.Weeks
                                        SumSumNat = SumSumNat + TmpWeek.TRP * (TmpWeek.SpotIndex / 100)
                                    Next
                                    If chkIncludeInSums.Checked Then
                                        For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                            SumSumNat = SumSumNat + TmpComp.TRPMainTarget
                                        Next
                                    End If
                                End If
                            Next
                        Else
                            TmpBT = grdTRP.Rows(x).Tag
                            For Each TmpWeek In TmpBT.Weeks
                                SumSumNat = SumSumNat + TmpWeek.TRP * (TmpWeek.SpotIndex / 100)
                            Next
                            If chkIncludeInSums.Checked Then
                                For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                    SumSumNat = SumSumNat + TmpComp.TRPMainTarget
                                Next
                            End If
                        End If
                    Next
                Next
                SumSumNat = SumSumNat / tmpC.Relations.count
            Else
                TmpBT = grdTRP.Rows(e.RowIndex).Tag

                For Each TmpWeek In TmpBT.Weeks
                    SumNat = SumNat + TmpWeek.TRP * (TmpWeek.SpotIndex / 100)
                    SumChn = SumChn + TmpWeek.TRPBuyingTarget * (TmpWeek.SpotIndex / 100)
                Next
                If chkIncludeInSums.Checked Then
                    For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                        SumNat = SumNat + TmpComp.TRPMainTarget
                        SumChn = SumChn + TmpComp.TRPs
                    Next
                End If
                For x = 0 To grdTRP.Rows.Count - 1 Step 2
                    If grdTRP.Rows(x).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                        Dim tmpC2 As Trinity.cCombination = grdTRP.Rows(x).Tag
                        For Each cc2 As Trinity.cCombinationChannel In tmpC2.Relations
                            If Not cc2.Bookingtype.IsCompensation Then
                                TmpBT = cc2.Bookingtype
                                For Each TmpWeek In TmpBT.Weeks
                                    SumSumNat = SumSumNat + TmpWeek.TRP * (TmpWeek.SpotIndex / 100)
                                Next
                                If chkIncludeInSums.Checked Then
                                    For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                        SumSumNat = SumSumNat + TmpComp.TRPMainTarget
                                    Next
                                End If
                            End If
                        Next
                    Else
                        TmpBT = grdTRP.Rows(x).Tag
                        For Each TmpWeek In TmpBT.Weeks
                            SumSumNat = SumSumNat + TmpWeek.TRP * (TmpWeek.SpotIndex / 100)
                        Next
                        If chkIncludeInSums.Checked Then
                            For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                SumSumNat = SumSumNat + TmpComp.TRPMainTarget
                            Next
                        End If
                    End If
                Next
            End If


            If e.RowIndex / 2 = e.RowIndex \ 2 Then
                grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = 0
                e.Value = ""
                Exit Sub
            End If
            If e.ColumnIndex = 0 Then
                grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = SumNat
                e.Value = SumNat
            End If
            If e.ColumnIndex = 1 Then
                e.Value = SumChn
            End If

            grdGrandSum.Tag = SumSumNat
            grdGrandSum.Invalidate()

        Else
            e.Value = "-"
        End If

        On Error GoTo 0
        Exit Sub

SumChannelTRP_Error:

        If IsIDE() Then
            a = MsgBox("Error:" & Chr(10) & Chr(10) & "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & Chr(10) & Chr(10) & "Vill du utföra en felsökning?", vbYesNo, "TRINITY")
            If a = vbNo Then Exit Sub
            'Stop
            Resume Next
        End If
        MsgBox("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & " in SumChannelTRP.", vbCritical, "Error")


    End Sub

    Private Sub grdSumChannels_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSumChannels.CellValuePushed

        If grdTRP.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            If grdTRP.Rows(e.RowIndex).Tag.Relations(1).Bookingtype.islocked Then
                MsgBox("This bookingtype is locked, if you wish to change TRPs you need to unlock it first", MsgBoxStyle.Information, "Locked Week")
                Exit Sub
            End If
        Else
            If grdTRP.Rows(e.RowIndex).Tag.islocked AndAlso Not grdTRP.Rows(e.RowIndex).Tag.IsCompensation Then
                MsgBox("This bookingtype is locked, if you wish to change TRPs you need to unlock it first", MsgBoxStyle.Information, "Locked Week")
                Exit Sub
            End If
        End If

        Saved = False
        'when a change is made to the grid this code does the updates needed
        Dim TRPSum As Single
        Dim c As Integer
        Dim TmpWeek As Trinity.cWeek
        Dim newValue As Single

        If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then Exit Sub
        If e.ColumnIndex = 0 Then
            TRPSum = 0
            newValue = e.Value

            If grdTRP.Rows(e.RowIndex).Cells(c).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                For c = 0 To grdTRP.Columns.Count - 1
                    Dim tmpC As Trinity.cCombination = grdTRP.Rows(e.RowIndex).Tag
                    If Not tmpC.Relations(1).Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).IsLocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            If Not tmpCC.Bookingtype.IsCompensation Then
                                TRPSum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRP
                            End If
                        Next
                    End If
                Next

                Dim ratio As Single = (newValue - (grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value - TRPSum)) / TRPSum

                For c = 0 To grdTRP.Columns.Count - 1
                    Dim tmpC As Trinity.cCombination = grdTRP.Rows(e.RowIndex).Tag
                    If Not tmpC.Relations(1).Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).IsLocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            If Not tmpCC.Bookingtype.IsCompensation Then
                                tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRP *= ratio
                                tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRPControl = True
                            End If
                        Next
                    End If
                Next
            Else
                For c = 0 To grdTRP.Columns.Count - 1
                    TmpWeek = grdTRP.Rows(e.RowIndex).Cells(c).Tag
                    If Not TmpWeek.IsLocked Then
                        TRPSum = TRPSum + TmpWeek.TRP
                    End If
                Next

                Dim ratio As Single = (newValue - (grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value - TRPSum)) / TRPSum

                For c = 0 To grdTRP.Columns.Count - 1
                    TmpWeek = grdTRP.Rows(e.RowIndex).Cells(c).Tag
                    If Not TmpWeek.IsLocked Then
                        TmpWeek.TRP *= ratio
                        TmpWeek.TRPControl = True
                        If Not grdTRP.Rows(e.RowIndex).Tag.IsCompensation Then
                            For Each TmpCombo As Trinity.cCombination In Campaign.Combinations
                                If TmpCombo.HasRelations Then
                                    If TmpCombo.IncludesBookingtype(grdTRP.Rows(e.RowIndex).Tag) Then
                                        Dim Factor As Single
                                        For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                                            If TmpCC.Bookingtype Is grdTRP.Rows(e.RowIndex).Tag Then
                                                If TmpCombo.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                                                    Factor = TmpWeek.TRPBuyingTarget / TmpCC.Relation
                                                Else
                                                    Factor = TmpWeek.NetBudget / TmpCC.Relation
                                                End If
                                            End If
                                        Next
                                        For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                                            If TmpCC.Bookingtype IsNot grdTRP.Rows(e.RowIndex).Tag Then
                                                If TmpCombo.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                                                    TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPBuyingTarget = Factor * TmpCC.Relation
                                                Else
                                                    TmpCC.Bookingtype.Weeks(TmpWeek.Name).NetBudget = Factor * TmpCC.Relation
                                                End If
                                                TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPControl = True
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If
                Next

            End If


        ElseIf e.ColumnIndex = 1 Then
            TRPSum = 0
            If grdTRP.Rows(e.RowIndex).Cells(c).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                For c = 0 To grdTRP.Columns.Count - 1
                    Dim tmpC As Trinity.cCombination = grdTRP.Rows(e.RowIndex).Cells(c).Tag
                    For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                        If Not tmpCC.Bookingtype.IsCompensation Then
                            TRPSum = TRPSum + tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRPBuyingTarget
                        End If
                    Next
                Next
                For c = 0 To grdTRP.Columns.Count - 1
                    Dim tmpC As Trinity.cCombination = grdTRP.Rows(e.RowIndex).Cells(c).Tag
                    For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                        If Not tmpCC.Bookingtype.IsCompensation Then
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRPBuyingTarget = e.Value * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRPBuyingTarget / TRPSum)
                        End If
                    Next
                Next
            Else
                For c = 0 To grdTRP.Columns.Count - 1
                    TmpWeek = grdTRP.Rows(e.RowIndex).Cells(c).Tag
                    TRPSum = TRPSum + TmpWeek.TRPBuyingTarget
                Next
                For c = 0 To grdTRP.Columns.Count - 1
                    TmpWeek = grdTRP.Rows(e.RowIndex).Cells(c).Tag
                    TmpWeek.TRPBuyingTarget = e.Value * (TmpWeek.TRPBuyingTarget / TRPSum)
                    TmpWeek.TRPControl = True

                    For Each TmpCombo As Trinity.cCombination In Campaign.Combinations
                        If TmpCombo.HasRelations Then
                            If TmpCombo.IncludesBookingtype(grdTRP.Rows(e.RowIndex).Tag) Then
                                Dim Factor As Single
                                For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                                    If TmpCC.Bookingtype Is grdTRP.Rows(e.RowIndex).Tag Then
                                        If TmpCombo.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                                            Factor = TmpWeek.TRPBuyingTarget / TmpCC.Relation
                                        Else
                                            Factor = TmpWeek.NetBudget / TmpCC.Relation
                                        End If
                                    End If
                                Next
                                For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                                    If TmpCC.Bookingtype IsNot grdTRP.Rows(e.RowIndex).Tag Then
                                        If TmpCombo.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                                            TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPBuyingTarget = Factor * TmpCC.Relation
                                        Else
                                            TmpCC.Bookingtype.Weeks(TmpWeek.Name).NetBudget = Factor * TmpCC.Relation
                                        End If
                                        TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPControl = True
                                    End If
                                Next
                            End If
                        End If
                    Next
                Next
            End If
        End If

        grdBudget.Invalidate()
        grdTRP.Invalidate()
        grdSumChannels.Invalidate()
    End Sub

    Private Sub grdGrandSum_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdGrandSum.CellValuePushed
        'when a change is made to the grid this code does the updates needed
        Saved = False
        Dim TRPSum As Single
        Dim c As Integer
        Dim r As Integer
        Dim TmpWeek As Trinity.cWeek

        If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then Exit Sub
        TRPSum = 0
        For r = 1 To grdTRP.Rows.Count - 1 Step 2
            'if the bookingtype is locked there is no reason to loop the columns
            If grdTRP.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                If grdTRP.Rows(r).Tag.relations(1).bookingtype.islocked Then
                    GoTo Loop_row
                End If
            Else
                If grdTRP.Rows(r).Tag.islocked Then
                    GoTo Loop_row
                End If
            End If
            For c = 0 To grdTRP.Columns.Count - 1
                If grdTRP.Rows(r).Cells(c).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    Dim tmpC As Trinity.cCombination = grdTRP.Rows(r).Cells(c).Tag
                    If Not tmpC.Relations(1).Bookingtype.Weeks(c + 1).IsLocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            TRPSum = TRPSum + tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRP
                        Next
                    End If
                Else
                    TmpWeek = grdTRP.Rows(r).Cells(c).Tag
                    If Not TmpWeek.IsLocked Then
                        TRPSum = TRPSum + TmpWeek.TRP
                    End If
                End If
            Next
Loop_row:
        Next

        Dim ratio As Single = (e.Value - (grdGrandSum.Rows(0).Cells(0).Value - TRPSum)) / TRPSum

        For r = 1 To grdTRP.Rows.Count - 1 Step 2
            'if the bookingtype is locked there is no reason to loop the columns
            If grdTRP.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                If grdTRP.Rows(r).Tag.relations(1).bookingtype.islocked Then
                    GoTo Loop_row2
                End If
            Else
                If grdTRP.Rows(r).Tag.islocked Then
                    GoTo Loop_row2
                End If
            End If
            For c = 0 To grdTRP.Columns.Count - 1
                If grdTRP.Rows(r).Cells(c).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    Dim tmpC As Trinity.cCombination = grdTRP.Rows(r).Cells(c).Tag
                    If Not tmpC.Relations(1).Bookingtype.Weeks(c + 1).IsLocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRP *= ratio
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRPControl = True
                        Next
                    End If
                Else
                    TmpWeek = grdTRP.Rows(r).Cells(c).Tag
                    If Not TmpWeek.IsLocked Then
                        TmpWeek.TRP *= ratio
                        TmpWeek.TRPControl = True
                    End If
                End If
            Next
Loop_Row2:
            SkipIt = True
        Next

        grdBudget.Invalidate()
        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
        grdSpotCount.Invalidate()
        grdSumChannels.Invalidate()
    End Sub

    Private Sub grdGrandSum_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdGrandSum.CellValueNeeded
        'updates the grid on a number of events (window activation, mouse movement etc)
        If cmbDisplay.SelectedIndex < DisplayModeEnum.PercentOfWeek Then 'Is TRP or TRP30 being shown
            e.Value = grdGrandSum.Tag
            'grdBudget.Invalidate()
        Else
            e.Value = "-"
        End If
    End Sub

    Private Sub grdCompensation_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCompensation.CellEndEdit
        Saved = False
    End Sub

    Private Sub grdCompensation_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCompensation.CellValueNeeded
        'updates the grid on a number of events (window activation, mouse movement etc)

        'check if its a combination or a normal bookingtype
        If grdCompensation.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCompensation" Then

            'get the compensation
            Dim TmpComp As Trinity.cCompensation = grdCompensation.Rows(e.RowIndex).Tag

            If e.ColumnIndex = 0 Then
                With DirectCast(grdCompensation.Rows(e.RowIndex).Cells(e.ColumnIndex), ExtendedComboBoxCell)
                    If .Items.Count = 0 Then
                        .Items.Clear()
                        For Each c As Trinity.cCombination In Campaign.Combinations
                            If c.ShowAsOne Then
                                .Items.Add(c)
                            End If
                        Next
                        For Each TmpChan As Trinity.cChannel In Campaign.Channels
                            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                                If TmpBT.BookIt AndAlso TmpBT.ShowMe Then
                                    .Items.Add(TmpBT)
                                End If
                            Next
                        Next
                    End If
                End With
                grdCompensation.Rows(e.RowIndex).Tag = TmpComp
                e.Value = TmpComp.Bookingtype
            ElseIf e.ColumnIndex = 1 Then
                e.Value = TmpComp.FromDate
            ElseIf e.ColumnIndex = 2 Then
                e.Value = TmpComp.ToDate
            ElseIf e.ColumnIndex = 3 Then
                e.Value = Format(TmpComp.TRPs, "N1")
            ElseIf e.ColumnIndex = 4 Then
                e.Value = Format(TmpComp.Expense, "N0")
            ElseIf e.ColumnIndex = 5 Then
                e.Value = TmpComp.Comment
            End If

        Else
            'we have a compensation on a combination
            Dim TmpC As Trinity.cCombination = grdCompensation.Rows(e.RowIndex).Cells(0).Tag
            Dim TmpID As String = grdCompensation.Rows(e.RowIndex).Tag

            If e.ColumnIndex = 0 Then
                With DirectCast(grdCompensation.Rows(e.RowIndex).Cells(e.ColumnIndex), ExtendedComboBoxCell)
                    If .Items.Count = 0 Then
                        .Items.Clear()
                        For Each c As Trinity.cCombination In Campaign.Combinations
                            If c.ShowAsOne Then
                                .Items.Add(c)
                            End If
                        Next
                        For Each TmpChan As Trinity.cChannel In Campaign.Channels
                            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                                If TmpBT.BookIt AndAlso TmpBT.ShowMe Then
                                    .Items.Add(TmpBT)
                                End If
                            Next
                        Next
                    End If
                End With
                grdCompensation.Rows(e.RowIndex).Cells(0).Tag = TmpC
                grdCompensation.Rows(e.RowIndex).Tag = TmpID
                e.Value = TmpC
            ElseIf e.ColumnIndex = 1 Then
                e.Value = TmpC.Relations(1).Bookingtype.Compensations(TmpID).FromDate
            ElseIf e.ColumnIndex = 2 Then
                e.Value = TmpC.Relations(1).Bookingtype.Compensations(TmpID).ToDate
            ElseIf e.ColumnIndex = 3 Then
                Dim sum As Single = 0
                For Each TmpCC As Trinity.cCombinationChannel In TmpC.Relations
                    sum += TmpCC.Bookingtype.Compensations(TmpID).TRPs
                Next
                e.Value = Format(sum, "N1")
            ElseIf e.ColumnIndex = 4 Then
                Dim sum As Single = 0
                For Each TmpCC As Trinity.cCombinationChannel In TmpC.Relations
                    sum += TmpCC.Bookingtype.Compensations(TmpID).Expense
                Next
                e.Value = Format(sum, "N0")
            ElseIf e.ColumnIndex = 5 Then
                e.Value = TmpC.Relations(1).Bookingtype.Compensations(TmpID).Comment
            End If
        End If
    End Sub

    Private Sub grdCompensation_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCompensation.CellValuePushed
        'when a change is made to the grid this code does the updates needed

        'check if its a combination or a normal bookingtype
        If grdCompensation.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCompensation" Then
            Dim TmpComp As Trinity.cCompensation = grdCompensation.Rows(e.RowIndex).Tag
            If e.ColumnIndex = 0 Then
                TmpComp.Bookingtype.Compensations.Remove(TmpComp.ID)

                If e.Value.GetType.FullName = "clTrinity.Trinity.cBookingType" Then
                    'if we add a compensation on a regular bookingtype
                    With e.Value.Compensations.Add()
                        .Comment = TmpComp.Comment
                        .FromDate = TmpComp.FromDate
                        .ToDate = TmpComp.ToDate
                        .Expense = TmpComp.Expense
                        .TRPs = TmpComp.TRPs
                        grdCompensation.Rows(e.RowIndex).Tag = DirectCast(e.Value, Trinity.cBookingType).Compensations(.ID)
                    End With
                Else
                    'if we add a compensation on a combination
                    Dim strID As String = ""
                    For Each cc As Trinity.cCombinationChannel In e.Value.relations
                        With cc.Bookingtype.Compensations.Add()
                            .Comment = TmpComp.Comment
                            .FromDate = TmpComp.FromDate
                            .ToDate = TmpComp.ToDate
                            .Expense = TmpComp.Expense * cc.Percent
                            .TRPs = TmpComp.TRPs * cc.Percent
                            If strID = "" Then
                                strID = .ID
                            Else
                                .ID = strID
                            End If
                        End With
                    Next
                    grdCompensation.Rows(e.RowIndex).Cells(0).Tag = e.Value
                    grdCompensation.Rows(e.RowIndex).Tag = strID
                End If
            ElseIf e.ColumnIndex = 1 Then
                TmpComp.FromDate = e.Value
            ElseIf e.ColumnIndex = 2 Then
                TmpComp.ToDate = e.Value
            ElseIf e.ColumnIndex = 3 Then
                TmpComp.TRPs = e.Value
            ElseIf e.ColumnIndex = 4 Then
                TmpComp.Expense = e.Value
            ElseIf e.ColumnIndex = 5 Then
                TmpComp.Comment = e.Value
            End If
        Else

            'we have a compensation on a combination
            Dim TmpC As Trinity.cCombination = grdCompensation.Rows(e.RowIndex).Cells(0).Tag
            Dim TmpID As String = grdCompensation.Rows(e.RowIndex).Tag

            If e.ColumnIndex = 0 Then
                'we save a compensation so we can add the values to the new channel/combination
                Dim TmpComp As Trinity.cCompensation = TmpC.Relations(1).Bookingtype.Compensations(TmpID)

                'we null these so we can sum them up correctly using values from all CCs
                TmpComp.Expense = 0
                TmpComp.TRPs = 0

                For Each TmpCC As Trinity.cCombinationChannel In TmpC.Relations
                    TmpComp.Expense += TmpCC.Bookingtype.Compensations(TmpID).Expense
                    TmpComp.TRPs += TmpCC.Bookingtype.Compensations(TmpID).TRPs
                    TmpCC.Bookingtype.Compensations.Remove(TmpID)
                Next

                If e.Value.GetType.FullName = "clTrinity.Trinity.cBookingType" Then
                    'if we add a compensation on a regular bookingtype
                    With e.Value.Compensations.Add()
                        .Comment = TmpComp.Comment
                        .FromDate = TmpComp.FromDate
                        .ToDate = TmpComp.ToDate
                        .Expense = TmpComp.Expense
                        .TRPs = TmpComp.TRPs
                        grdCompensation.Rows(e.RowIndex).Tag = DirectCast(e.Value, Trinity.cBookingType).Compensations(.ID)
                    End With
                Else
                    'if we add a compensation on a combination
                    Dim strID As String = ""
                    For Each cc As Trinity.cCombinationChannel In e.Value.relations
                        With cc.Bookingtype.Compensations.Add()
                            .Comment = TmpComp.Comment
                            .FromDate = TmpComp.FromDate
                            .ToDate = TmpComp.ToDate
                            .Expense = TmpComp.Expense * cc.Percent
                            .TRPs = TmpComp.TRPs * cc.Percent
                            If strID = "" Then
                                strID = .ID
                            Else
                                .ID = strID
                            End If
                        End With
                    Next
                    grdCompensation.Rows(e.RowIndex).Cells(0).Tag = e.Value
                    grdCompensation.Rows(e.RowIndex).Tag = strID
                End If
            ElseIf e.ColumnIndex = 1 Then
                For Each TmpCC As Trinity.cCombinationChannel In TmpC.Relations
                    TmpCC.Bookingtype.Compensations(TmpID).FromDate = e.Value
                Next
            ElseIf e.ColumnIndex = 2 Then
                For Each TmpCC As Trinity.cCombinationChannel In TmpC.Relations
                    TmpCC.Bookingtype.Compensations(TmpID).ToDate = e.Value
                Next
            ElseIf e.ColumnIndex = 3 Then
                For Each TmpCC As Trinity.cCombinationChannel In TmpC.Relations
                    TmpCC.Bookingtype.Compensations(TmpID).TRPs = e.Value * TmpCC.Percent
                Next
            ElseIf e.ColumnIndex = 4 Then
                For Each TmpCC As Trinity.cCombinationChannel In TmpC.Relations
                    TmpCC.Bookingtype.Compensations(TmpID).Expense = e.Value * TmpCC.Percent
                Next
            ElseIf e.ColumnIndex = 5 Then
                For Each TmpCC As Trinity.cCombinationChannel In TmpC.Relations
                    TmpCC.Bookingtype.Compensations(TmpID).Comment = e.Value
                Next
            End If
        End If

        grdSpotCount.Invalidate()
        grdSumChannels.Invalidate()
        grdSumWeeks.Invalidate()
        SkipIt = False
    End Sub

    Private Sub cmdAddChannel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddChannel.Click
        Saved = False
        Dim Ready As Boolean = False
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                'If TmpBT.BookIt AndAlso Not Ready Then
                If TmpBT.ShowMe AndAlso TmpBT.BookIt AndAlso Not Ready Then
                    With TmpBT.Compensations.Add()
                        .FromDate = Date.FromOADate(Campaign.StartDate)
                        .ToDate = Date.FromOADate(Campaign.EndDate)
                    End With
                    Ready = True
                    Exit For
                End If
            Next
        Next
        grdCompensation.Rows.Clear()
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If TmpBT.ShowMe Then
                    For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                        grdCompensation.Rows.Add()
                        grdCompensation.Rows(grdCompensation.Rows.Count - 1).Tag = TmpComp
                    Next
                End If
            Next
        Next

        For Each TmpC As Trinity.cCombination In Campaign.Combinations
            If TmpC.ShowAsOne Then
                If TmpC.Relations(1).Bookingtype.Compensations.Count = 0 Then
                    Dim thisID As String = Nothing
                    For Each combChan As Trinity.cCombinationChannel In TmpC.Relations
                        With combChan.Bookingtype.Compensations.Add
                            .FromDate = Date.FromOADate(Campaign.StartDate)
                            .ToDate = Date.FromOADate(Campaign.EndDate)
                            If thisID Is Nothing Then thisID = .ID
                            .ID = thisID
                        End With
                    Next
                End If
                For Each TmpComp As Trinity.cCompensation In TmpC.Relations(1).Bookingtype.Compensations
                    'Dim TmpComp As New Trinity.cCompensation(TmpC.Relations(1).Bookingtype, TmpC.Relations(1).Bookingtype.Compensations.GetCollection)
                    grdCompensation.Rows.Add()
                    grdCompensation.Rows(grdCompensation.Rows.Count - 1).Tag = TmpComp.ID
                    grdCompensation.Rows(grdCompensation.Rows.Count - 1).Cells(0).Tag = TmpC
                Next
            End If
        Next
    End Sub

    Private Sub cmdRemoveChannel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveChannel.Click
        Saved = False
        If grdCompensation.SelectedRows.Count = 0 Then Exit Sub

        'check if its a combination or a normal bookingtype
        If grdCompensation.Rows(grdCompensation.SelectedRows(0).Index).Tag.GetType.FullName = "clTrinity.Trinity.cCompensation" Then
            With DirectCast(grdCompensation.SelectedRows.Item(0).Tag, Trinity.cCompensation)
                .Bookingtype.Compensations.Remove(.ID)
            End With
        Else
            For Each cc As Trinity.cCombinationChannel In grdCompensation.Rows(grdCompensation.SelectedRows(0).Index).Cells(0).Value.relations
                cc.Bookingtype.Compensations.Remove(grdCompensation.Rows(grdCompensation.SelectedRows(0).Index).Tag)
            Next
        End If

        grdCompensation.Rows.Remove(grdCompensation.SelectedRows.Item(0))



        'If e.Value.GetType.FullName = "clTrinity.Trinity.cBookingType" Then
        '    'if we add a compensation on a regular bookingtype
        '    With e.Value.Compensations.Add()
        '        .Comment = TmpComp.Comment
        '        .FromDate = TmpComp.FromDate
        '        .ToDate = TmpComp.ToDate
        '        .Expense = TmpComp.Expense
        '        .TRPs = TmpComp.TRPs
        '        grdCompensation.Rows(e.RowIndex).Tag = DirectCast(e.Value, Trinity.cBookingType).Compensations(.ID)
        '    End With
        'Else
        '    'if we add a compensation on a combination
        '    Dim strID As String = ""
        '    For Each cc As Trinity.cCombinationChannel In e.Value.relations
        '        With cc.Bookingtype.Compensations.Add()
        '            .Comment = TmpComp.Comment
        '            .FromDate = TmpComp.FromDate
        '            .ToDate = TmpComp.ToDate
        '            .Expense = TmpComp.Expense * cc.Percent
        '            .TRPs = TmpComp.TRPs * cc.Percent
        '            If strID = "" Then
        '                strID = .ID
        '            Else
        '                .ID = strID
        '            End If
        '        End With
        '    Next
        '    grdCompensation.Rows(e.RowIndex).Cells(0).Tag = e.Value
        '    grdCompensation.Rows(e.RowIndex).Tag = strID
        'End If
    End Sub

    Private Sub chkIncludeInSums_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIncludeInSums.CheckedChanged
        Saved = False
        grdSpotCount.Invalidate()
        grdSumChannels.Invalidate()
        grdSumWeeks.Invalidate()
        TrinitySettings.IncludeCompensations = chkIncludeInSums.Checked
        SkipIt = False
    End Sub

    Private Sub grdFilms_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdFilms.CellValueNeeded
        Try
            'updates the grid on a number of events (window activation, mouse movement etc)
            Dim TmpBT As Trinity.cBookingType = Nothing
            Dim tmpC As Trinity.cCombination = Nothing
            Dim Show As Trinity.cFilm.FilmShareEnum
            Dim TmpFilm As Trinity.cFilm = Nothing

            If btnTRP.Checked Then
                Show = Trinity.cFilm.FilmShareEnum.fseTRP
            Else
                Show = Trinity.cFilm.FilmShareEnum.fseBudget
            End If

            If cmbFilmChannel.SelectedIndex = cmbFilmChannel.Items.Count - 1 Then
                TmpBT = Nothing
                TmpFilm = grdFilms.Rows(e.RowIndex).Tag
            Else
                If cmbFilmChannel.SelectedItem.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    tmpC = cmbFilmChannel.SelectedItem
                    TmpFilm = grdFilms.Rows(e.RowIndex).Tag
                Else
                    TmpBT = cmbFilmChannel.SelectedItem
                    TmpFilm = grdFilms.Rows(e.RowIndex).Tag
                End If
            End If


            Dim TotTRP As Single = 0

            If Not TmpBT Is Nothing Then
                For i As Integer = 1 To TmpBT.Weeks.Count
                    TotTRP += TmpBT.Weeks(i).TRP
                Next
                If e.ColumnIndex < grdFilms.ColumnCount - 2 Then
                    e.Value = TmpBT.Weeks(e.ColumnIndex + 1).Films(TmpFilm.Name).Share(Show) / 100
                ElseIf e.ColumnIndex = grdFilms.ColumnCount - 2 Then
                    Dim TRP As Single = 0
                    For i As Integer = 1 To TmpBT.Weeks.Count
                        TRP += (TmpBT.Weeks(i).Films(TmpFilm.Name).Share(Show) / 100) * TmpBT.Weeks(i).TRP
                    Next
                    e.Value = TRP / TotTRP
                Else
                    'Adtoox
                    e.Value = TmpFilm.AdTooxStatus
                End If
            ElseIf Not tmpC Is Nothing Then
                'one line combo
                For i As Integer = 1 To tmpC.Relations(1).Bookingtype.Weeks.Count
                    For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                        TotTRP += tmpCC.Bookingtype.Weeks(i).TRP
                    Next
                Next
                If e.ColumnIndex < grdFilms.ColumnCount - 2 Then
                    Dim LastValue As Single = tmpC.Relations(1).Bookingtype.Weeks(e.ColumnIndex + 1).Films(TmpFilm.Name).Share
                    Dim NotSame As Boolean = False
                    For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                        If LastValue <> tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).Films(TmpFilm.Name).Share Then
                            NotSame = True
                        End If
                    Next
                    grdFilms.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = NotSame
                    e.Value = tmpC.Relations(1).Bookingtype.Weeks(e.ColumnIndex + 1).Films(TmpFilm.Name).Share(Show) / 100
                ElseIf e.ColumnIndex = grdFilms.ColumnCount - 2 Then
                    Dim TRP As Single = 0
                    For i As Integer = 1 To tmpC.Relations(1).Bookingtype.Weeks.Count
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            TRP += (tmpCC.Bookingtype.Weeks(i).Films(TmpFilm.Name).Share(Show) / 100) * tmpCC.Bookingtype.Weeks(i).TRP
                        Next
                    Next
                    e.Value = TRP / TotTRP
                Else
                    e.Value = Nothing
                End If
                'end one line combo
            Else
                If e.ColumnIndex < grdFilms.ColumnCount - 2 Then
                    Dim TRP As Single = 0
                    For i As Integer = 0 To cmbFilmChannel.Items.Count - 2
                        If cmbFilmChannel.Items(i).GetType.FullName = "clTrinity.Trinity.cCombination" Then
                            For Each tmpCC As Trinity.cCombinationChannel In cmbFilmChannel.Items(i).relations
                                TRP += (tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).Films(TmpFilm.Name).Share(Show) / 100) * tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).TRP
                                TotTRP += tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).TRP
                            Next
                        Else
                            TmpBT = cmbFilmChannel.Items(i)
                            TRP += (TmpBT.Weeks(e.ColumnIndex + 1).Films(TmpFilm.Name).Share(Show) / 100) * TmpBT.Weeks(e.ColumnIndex + 1).TRP
                            TotTRP += TmpBT.Weeks(e.ColumnIndex + 1).TRP
                        End If

                    Next
                    If TotTRP > 0 Then
                        e.Value = TRP / TotTRP
                    Else
                        e.Value = 0
                    End If
                Else
                    Dim TRP As Single = 0
                    For i As Integer = 0 To cmbFilmChannel.Items.Count - 2
                        If cmbFilmChannel.Items(i).GetType.FullName = "clTrinity.Trinity.cCombination" Then
                            For Each tmpCC As Trinity.cCombinationChannel In cmbFilmChannel.Items(i).relations
                                For j As Integer = 1 To tmpCC.Bookingtype.Weeks.Count
                                    TRP += (tmpCC.Bookingtype.Weeks(j).Films(TmpFilm.Name).Share(Show) / 100) * tmpCC.Bookingtype.Weeks(j).TRP
                                    TotTRP += tmpCC.Bookingtype.Weeks(j).TRP
                                Next
                            Next
                        Else
                            TmpBT = cmbFilmChannel.Items(i)
                            For j As Integer = 1 To TmpBT.Weeks.Count
                                TRP += (TmpBT.Weeks(j).Films(TmpFilm.Name).Share(Show) / 100) * TmpBT.Weeks(j).TRP
                                TotTRP += TmpBT.Weeks(j).TRP
                            Next
                        End If
                    Next
                    If e.ColumnIndex < grdFilms.ColumnCount - 1 Then
                        If TotTRP > 0 Then
                            e.Value = TRP / TotTRP
                        Else
                            e.Value = 0
                        End If
                    Else
                        e.Value = Campaign.GetTotalAdTooxStatus(grdFilms.Rows(e.RowIndex).HeaderCell.Value)
                    End If
                End If
            End If
        Catch ex As NullReferenceException
            Windows.Forms.MessageBox.Show("There was an error while rendering the allocate window." & vbNewLine & "This is usually caused by a week/channel/film etc being removed in Setup while the Allocate window is open." & vbNewLine & "Allocate will now close, please re-open it. If the error persists - contact trinity@mecglobal.com.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End Try
    End Sub

    Private Sub grdFilms_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdFilms.CellValuePushed
        'when a change is made to the grid this code does the updates needed
        Saved = False

        Dim TmpBT As Trinity.cBookingType = Nothing
        Dim Sum As Single
        Dim strTemp As String
        Dim Show As Trinity.cFilm.FilmShareEnum

        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

        If btnTRP.Checked Then
            Show = Trinity.cFilm.FilmShareEnum.fseTRP
        Else
            Show = Trinity.cFilm.FilmShareEnum.fseBudget
        End If

        If cmbFilmChannel.SelectedIndex = cmbFilmChannel.Items.Count - 1 Then
            ' Exit Sub
        Else
            If Not cmbFilmChannel.SelectedItem.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                TmpBT = cmbFilmChannel.SelectedItem
            End If
        End If


        'deletes the % ibn the string if there is one present
        strTemp = e.Value
        If e.Value Is Nothing Then strTemp = "0"
        strTemp = strTemp.Trim("%")
        strTemp = strTemp.Replace(".", ",")

        Dim result As Decimal = 0

        If (Not Decimal.TryParse(strTemp, result)) Then
            Exit Sub
        End If


        If cmbFilmChannel.SelectedItem.GetType.FullName = "System.String" AndAlso cmbFilmChannel.SelectedItem = "TOTAL" Then
            Exit Sub

            If grdFilms.Columns(e.ColumnIndex).HeaderText = "Total" Then Exit Sub

            For p As Integer = 0 To cmbFilmChannel.Items.Count - 2
                If cmbFilmChannel.Items(p).GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    For Each tmpCC As Trinity.cCombinationChannel In cmbFilmChannel.Items(p).relations 'cmbFilmChannel.SelectedItem.relations
                        tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).Films(e.RowIndex + 1).Share(Show) = strTemp

                        Sum = 0
                        For i As Integer = 1 To tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).Films.Count
                            Sum = Sum + tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).Films(i).Share(Show)
                        Next

                        If Not e.RowIndex + 2 > tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).Films.Count Then
                            tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).Films(e.RowIndex + 2).Share(Show) = tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).Films(e.RowIndex + 2).Share(Show) + (100 - Sum)
                        End If
                    Next
                Else
                    TmpBT = cmbFilmChannel.Items(p)
                    TmpBT.Weeks(e.ColumnIndex + 1).Films(e.RowIndex + 1).Share(Show) = strTemp
                    For i As Integer = 1 To TmpBT.Weeks(e.ColumnIndex + 1).Films.Count
                        Sum = Sum + TmpBT.Weeks(e.ColumnIndex + 1).Films(i).Share(Show)
                    Next
                    If Not e.RowIndex + 2 > TmpBT.Weeks(e.ColumnIndex + 1).Films.Count Then
                        TmpBT.Weeks(e.ColumnIndex + 1).Films(e.RowIndex + 2).Share(Show) = TmpBT.Weeks(e.ColumnIndex + 1).Films(e.RowIndex + 2).Share(Show) + (100 - Sum)
                    End If
                End If
            Next
        Else
            If cmbFilmChannel.SelectedItem.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                For Each tmpCC As Trinity.cCombinationChannel In cmbFilmChannel.SelectedItem.relations
                    tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).Films(e.RowIndex + 1).Share(Show) = strTemp

                    Sum = 0
                    For i As Integer = 1 To tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).Films.Count
                        Sum = Sum + tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).Films(i).Share(Show)
                    Next

                    If Not e.RowIndex + 2 > tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).Films.Count Then
                        tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).Films(e.RowIndex + 2).Share(Show) = tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).Films(e.RowIndex + 2).Share(Show) + (100 - Sum)
                    End If
                Next
            Else
                TmpBT.Weeks(e.ColumnIndex + 1).Films(e.RowIndex + 1).Share(Show) = strTemp
                For i As Integer = 1 To TmpBT.Weeks(e.ColumnIndex + 1).Films.Count
                    Sum = Sum + TmpBT.Weeks(e.ColumnIndex + 1).Films(i).Share(Show)
                Next
                If Not e.RowIndex + 2 > TmpBT.Weeks(e.ColumnIndex + 1).Films.Count Then
                    TmpBT.Weeks(e.ColumnIndex + 1).Films(e.RowIndex + 2).Share(Show) = TmpBT.Weeks(e.ColumnIndex + 1).Films(e.RowIndex + 2).Share(Show) + (100 - Sum)
                End If
            End If
        End If

        grdFilms.Invalidate()
        grdDiscounts.Invalidate()
        'update all the grids since the costs and TRP is changed it you change the films
        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
        grdBudget.Invalidate()
        grdSpotCount.Invalidate()
        grdSumChannels.Invalidate()
        ColorFilmGrid() 'if the films show % dont sum up to 100 % they are shown in red
    End Sub

    Protected Overrides Function ScrollToControl(activeControl As System.Windows.Forms.Control) As System.Drawing.Point
        Return DisplayRectangle.Location
    End Function

    Private Sub grdAV_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdAV.CellValueNeeded
        If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then Exit Sub

        'the AV is loaded in the tag on combinations aswell
        'since the combinated channels have the same AV we can simply show it
        Dim TmpAV As Trinity.cAddedValue = grdAV.Rows(e.RowIndex).Tag
        e.Value = Format(TmpAV.Amount(e.ColumnIndex + 1) / 100, "P1")

    End Sub

    Private Sub grdAV_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdAV.CellValuePushed
        If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then Exit Sub

        'the combination object is set to the tag of the first cell
        If grdAV.Rows(e.RowIndex).Cells(0).Tag Is Nothing Then
            Dim TmpAV As Trinity.cAddedValue = grdAV.Rows(e.RowIndex).Tag

            ' First trim away the % as it is expected by the user to write it, but the converter cant handle it
            ' Also replaces any dots by commas for convience
            Dim tmpStr As String = e.Value.ToString()
            Dim result As Decimal = 0

            If (Decimal.TryParse(tmpStr.Replace("%", "").Replace(".", ","), result)) Then
                TmpAV.Amount(e.ColumnIndex + 1) = result
            End If
        Else
            Dim comb As Trinity.cCombination = grdAV.Rows(e.RowIndex).Cells(0).Tag
            For Each tmpCC As Trinity.cCombinationChannel In comb.Relations
                tmpCC.Bookingtype.AddedValues(e.RowIndex + 1).Amount(e.ColumnIndex + 1) = e.Value
            Next
        End If

        grdDiscounts.Invalidate()
        grdBudget.Invalidate()
        grdTRP.Invalidate()
    End Sub

    Dim IndexStyleUserSet As Windows.Forms.DataGridViewCellStyle
    Dim IndexStyleUnchanged As Windows.Forms.DataGridViewCellStyle
    Dim IndexStyleND As Windows.Forms.DataGridViewCellStyle

    Private Sub grdIndex_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdIndex.CellFormatting
        'If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        'If grdIndex.ForeColor = Drawing.Color.LightGray Then Exit Sub

        'If IndexStyleUserSet Is Nothing Then
        '    IndexStyleUserSet = New Windows.Forms.DataGridViewCellStyle(grdIndex.DefaultCellStyle) With {.ForeColor = Color.Blue}
        '    IndexStyleUnchanged = New Windows.Forms.DataGridViewCellStyle(grdIndex.DefaultCellStyle) With {.ForeColor = Color.Red}
        '    IndexStyleND = New Windows.Forms.DataGridViewCellStyle(grdIndex.DefaultCellStyle) With {.ForeColor = Color.Black}
        'End If
        'Dim TmpBT As Trinity.cBookingType = grdIndex.Rows(e.RowIndex).Tag


        'Select Case e.ColumnIndex
        '    Case 0
        '        Select Case TmpBT.IndexMainTargetStatus
        '            Case Trinity.cBookingType.IndexStatusEnum.EnteredByUser
        '                e.CellStyle = IndexStyleUserSet
        '            Case Trinity.cBookingType.IndexStatusEnum.Unchanged
        '                e.CellStyle = IndexStyleUnchanged
        '            Case Trinity.cBookingType.IndexStatusEnum.NaturalDelivery Or Trinity.cBookingType.IndexStatusEnum.Unknown
        '                e.CellStyle = IndexStyleND
        '        End Select
        '        Select Case TmpBT.ManualIndexes
        '            Case True
        '                e.CellStyle.BackColor = styleNormalDLocked.BackColor
        '            Case False
        '                e.CellStyle.BackColor = styleNormalD.BackColor
        '        End Select
        '    Case 1
        '        Select Case TmpBT.IndexSecondTargetStatus
        '            Case Trinity.cBookingType.IndexStatusEnum.EnteredByUser
        '                e.CellStyle = IndexStyleUserSet
        '            Case Trinity.cBookingType.IndexStatusEnum.Unchanged
        '                e.CellStyle = IndexStyleUnchanged
        '            Case Trinity.cBookingType.IndexStatusEnum.NaturalDelivery Or Trinity.cBookingType.IndexStatusEnum.Unknown
        '                e.CellStyle = IndexStyleND
        '        End Select
        '        Select Case TmpBT.ManualIndexes
        '            Case True
        '                e.CellStyle.BackColor = styleNormalDLocked.BackColor
        '            Case False
        '                e.CellStyle.BackColor = styleNormalD.BackColor
        '        End Select
        '    Case 2
        '        Select Case TmpBT.IndexAllAdultsStatus
        '            Case Trinity.cBookingType.IndexStatusEnum.EnteredByUser
        '                e.CellStyle = IndexStyleUserSet
        '            Case Trinity.cBookingType.IndexStatusEnum.Unchanged
        '                e.CellStyle = IndexStyleUnchanged
        '            Case Trinity.cBookingType.IndexStatusEnum.NaturalDelivery Or Trinity.cBookingType.IndexStatusEnum.Unknown
        '                e.CellStyle = IndexStyleND
        '        End Select
        '        Select Case TmpBT.ManualIndexes
        '            Case True
        '                e.CellStyle.BackColor = styleNormalDLocked.BackColor
        '            Case False
        '                e.CellStyle.BackColor = styleNormalD.BackColor
        '        End Select
        'End Select

        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        If grdIndex.ForeColor = Drawing.Color.LightGray Then Exit Sub

        Dim TmpBT As Trinity.cBookingType
        Dim TmpC As Trinity.cCombination
        Dim IndexMainTargetStatus As Trinity.cBookingType.IndexStatusEnum
        Dim IndexSecondTargetStatus As Trinity.cBookingType.IndexStatusEnum
        Dim IndexAllAdultsStatus As Trinity.cBookingType.IndexStatusEnum
        Dim ManualIndexes As Boolean

        If IndexStyleUserSet Is Nothing Then
            IndexStyleUserSet = New Windows.Forms.DataGridViewCellStyle(grdIndex.DefaultCellStyle) With {.ForeColor = Color.Blue}
            IndexStyleUnchanged = New Windows.Forms.DataGridViewCellStyle(grdIndex.DefaultCellStyle) With {.ForeColor = Color.Red}
            IndexStyleND = New Windows.Forms.DataGridViewCellStyle(grdIndex.DefaultCellStyle) With {.ForeColor = Color.Black}
        End If

        If grdIndex.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            TmpC = grdIndex.Rows(e.RowIndex).Tag
            IndexMainTargetStatus = TmpC.IndexMainTargetStatus
            IndexSecondTargetStatus = TmpC.IndexSecondTargetStatus
            IndexAllAdultsStatus = TmpC.IndexAllAdultsStatus
            ManualIndexes = TmpC.ManualIndexes
        Else
            TmpBT = grdIndex.Rows(e.RowIndex).Tag
            IndexMainTargetStatus = TmpBT.IndexMainTargetStatus
            IndexSecondTargetStatus = TmpBT.IndexSecondTargetStatus
            IndexAllAdultsStatus = TmpBT.IndexAllAdultsStatus
            ManualIndexes = TmpBT.ManualIndexes
        End If

        Select Case e.ColumnIndex
            Case 0
                Select Case IndexMainTargetStatus
                    Case Trinity.cBookingType.IndexStatusEnum.EnteredByUser
                        e.CellStyle = IndexStyleUserSet
                    Case Trinity.cBookingType.IndexStatusEnum.Unchanged
                        e.CellStyle = IndexStyleUnchanged
                    Case Trinity.cBookingType.IndexStatusEnum.NaturalDelivery Or Trinity.cBookingType.IndexStatusEnum.Unknown
                        e.CellStyle = IndexStyleND
                End Select
                Select Case ManualIndexes
                    Case True
                        e.CellStyle.BackColor = styleNormalDLocked.BackColor
                    Case False
                        e.CellStyle.BackColor = styleNormalD.BackColor
                End Select
            Case 1
                Select Case IndexSecondTargetStatus
                    Case Trinity.cBookingType.IndexStatusEnum.EnteredByUser
                        e.CellStyle = IndexStyleUserSet
                    Case Trinity.cBookingType.IndexStatusEnum.Unchanged
                        e.CellStyle = IndexStyleUnchanged
                    Case Trinity.cBookingType.IndexStatusEnum.NaturalDelivery Or Trinity.cBookingType.IndexStatusEnum.Unknown
                        e.CellStyle = IndexStyleND
                End Select
                Select Case ManualIndexes
                    Case True
                        e.CellStyle.BackColor = styleNormalDLocked.BackColor
                    Case False
                        e.CellStyle.BackColor = styleNormalD.BackColor
                End Select
            Case 2
                Select Case IndexAllAdultsStatus
                    Case Trinity.cBookingType.IndexStatusEnum.EnteredByUser
                        e.CellStyle = IndexStyleUserSet
                    Case Trinity.cBookingType.IndexStatusEnum.Unchanged
                        e.CellStyle = IndexStyleUnchanged
                    Case Trinity.cBookingType.IndexStatusEnum.NaturalDelivery Or Trinity.cBookingType.IndexStatusEnum.Unknown
                        e.CellStyle = IndexStyleND
                End Select
                Select Case ManualIndexes
                    Case True
                        e.CellStyle.BackColor = styleNormalDLocked.BackColor
                    Case False
                        e.CellStyle.BackColor = styleNormalD.BackColor
                End Select
        End Select

        Dim cell As DataGridViewCell = grdIndex.Rows(e.RowIndex).Cells(e.ColumnIndex)

        If e.Value = 0 Then
            e.CellStyle.BackColor = Color.Red
            cell.ToolTipText = "Please run Natural Delivery or enter an index. " & _
                               "If you have done this, please check in Setup if " & _
                               "you have entered daypart percentages for this bookingtype"
        Else
            If Not ManualIndexes Then e.CellStyle.BackColor = Color.White
            cell.ToolTipText = Nothing
        End If



    End Sub


    Sub grdIndex_Lockindex(ByVal sender As Object, ByVal e As System.EventArgs)
        If sender.tag = "Lock one" Then
            grdIndex.CurrentRow.Tag.ManualIndexes = Not grdIndex.CurrentRow.Tag.ManualIndexes
        ElseIf sender.tag = "Lock selected" Then
            For Each cell As DataGridViewCell In grdIndex.SelectedCells
                grdIndex.Rows(cell.RowIndex).Tag.ManualIndexes = Not grdIndex.Rows(cell.RowIndex).Tag.ManualIndexes
            Next
        End If
        grdIndex.Invalidate()
    End Sub


    Private Sub grdIndex_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdIndex.CellValueNeeded
        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        If grdIndex.ForeColor = Drawing.Color.LightGray Then Exit Sub

        Dim IndexMainTarget, IndexSecondTarget, IndexAllAdults As Double

        If grdIndex.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cBookingType" Then

            Dim TmpBT As Trinity.cBookingType = grdIndex.Rows(e.RowIndex).Tag
            IndexMainTarget = TmpBT.IndexMainTarget
            IndexSecondTarget = TmpBT.IndexSecondTarget
            IndexAllAdults = TmpBT.IndexAllAdults
        Else
            Dim TmpC As Trinity.cCombination = grdIndex.Rows(e.RowIndex).Tag
            If TmpC.IndexMainTarget = 0 Then
                Dim tmpIndex As Integer = 0
                For Each tmpChannel As Trinity.cCombinationChannel In TmpC.Relations
                    tmpIndex += tmpChannel.Bookingtype.IndexMainTarget * tmpChannel.Percent
                Next
                TmpC.IndexMainTarget = tmpIndex
            End If

            If TmpC.IndexSecondTarget = 0 Then
                Dim tmpIndex As Integer = 0
                For Each tmpChannel As Trinity.cCombinationChannel In TmpC.Relations
                    tmpIndex += tmpChannel.Bookingtype.IndexSecondTarget * tmpChannel.Percent
                Next
                TmpC.IndexSecondTarget = tmpIndex
            End If

            If TmpC.IndexAllAdults = 0 Then
                Dim tmpIndex As Integer = 0
                For Each tmpChannel As Trinity.cCombinationChannel In TmpC.Relations
                    tmpIndex += tmpChannel.Bookingtype.IndexAllAdults * tmpChannel.Percent
                Next
                TmpC.IndexAllAdults = tmpIndex
            End If

            IndexMainTarget = TmpC.IndexMainTarget
            IndexSecondTarget = TmpC.IndexSecondTarget
            IndexAllAdults = TmpC.IndexAllAdults
        End If
        Dim IsCombination As Boolean = True


        If e.ColumnIndex = 0 Then
            e.Value = IndexMainTarget
        ElseIf e.ColumnIndex = 1 Then
            e.Value = IndexSecondTarget
        ElseIf e.ColumnIndex = 2 Then
            e.Value = IndexAllAdults
        End If

    End Sub

    Private Sub grdIndex_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdIndex.CellValuePushed

        If Not IsNumeric(e.Value) Then
            Exit Sub
        End If

        Saved = False
        Dim TmpBT As Trinity.cBookingType
        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        If grdIndex.ForeColor = Drawing.Color.LightGray Then Exit Sub

        If grdIndex.Rows(e.RowIndex).Tag.GetType.FullName <> "clTrinity.Trinity.cCombination" Then
            TmpBT = grdIndex.Rows(e.RowIndex).Tag
            If e.ColumnIndex = 0 Then
                TmpBT.IndexMainTarget = e.Value
                TmpBT.IndexMainTargetStatus = Trinity.cBookingType.IndexStatusEnum.EnteredByUser
            ElseIf e.ColumnIndex = 1 Then
                TmpBT.IndexSecondTarget = e.Value
                TmpBT.IndexSecondTargetStatus = Trinity.cBookingType.IndexStatusEnum.EnteredByUser
            ElseIf e.ColumnIndex = 2 Then
                TmpBT.IndexAllAdults = e.Value
                TmpBT.IndexAllAdultsStatus = Trinity.cBookingType.IndexStatusEnum.EnteredByUser
            End If
        Else
            Dim tmpC As Trinity.cCombination = grdIndex.Rows(e.RowIndex).Tag
            'Else this is a combination row
            If e.ColumnIndex = 0 Then
                tmpC.IndexMainTarget = e.Value
                tmpC.IndexMainTargetStatus = Trinity.cCombination.IndexStatusEnum.EnteredByUser
                'For Each tmpCC As Trinity.cCombinationChannel In grdIndex.Rows(e.RowIndex).Cells(0).Tag.relations
                '    tmpCC.Bookingtype.IndexMainTarget = e.Value
                '    tmpCC.Bookingtype.IndexMainTargetStatus = Trinity.cBookingType.IndexStatusEnum.EnteredByUser
                'Next

            ElseIf e.ColumnIndex = 1 Then
                tmpC.IndexSecondTarget = e.Value
                tmpC.IndexSecondTargetStatus = Trinity.cCombination.IndexStatusEnum.EnteredByUser
                'For Each tmpCC As Trinity.cCombinationChannel In grdIndex.Rows(e.RowIndex).Cells(0).Tag.relations
                '    tmpCC.Bookingtype.IndexSecondTarget = e.Value
                '    tmpCC.Bookingtype.IndexSecondTargetStatus = Trinity.cBookingType.IndexStatusEnum.EnteredByUser
                'Next

            ElseIf e.ColumnIndex = 2 Then
                tmpC.IndexAllAdults = e.Value
                tmpC.IndexAllAdultsStatus = Trinity.cCombination.IndexStatusEnum.EnteredByUser
                'For Each tmpCC As Trinity.cCombinationChannel In grdIndex.Rows(e.RowIndex).Cells(0).Tag.relations
                '    tmpCC.Bookingtype.IndexAllAdults = e.Value
                '    tmpCC.Bookingtype.IndexAllAdultsStatus = Trinity.cBookingType.IndexStatusEnum.EnteredByUser
                'Next
            End If
        End If

        grdTRP.Invalidate()
        SkipIt = True
        grdSumWeeks.Invalidate()
        SkipIt = False
        grdSumChannels.Invalidate()
        grdDiscounts.Invalidate()
        grdBudget.Invalidate()
    End Sub

    Private Sub grdSumWeeks_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdSumWeeks.CellFormatting
        If e.RowIndex = 1 Then Exit Sub

        If cmbDisplay.SelectedIndex < DisplayModeEnum.PercentOfWeek Then 'TRP or TRP30 is being shown
            e.CellStyle = styleNormalD
        Else
            e.CellStyle = styleCantSetP
        End If
    End Sub

    'Sub SumWeekTRP(ByVal week, Optional ByVal Row = -1)
    '    Dim TmpChan As Trinity.cChannel
    '    Dim TmpBT As Trinity.cBookingType
    '    Dim SumTarget As Single
    '    Dim SumAllAdults As Single

    '    For Each TmpChan In Campaign.Channels
    '        For Each TmpBT In TmpChan.BookingTypes
    '            If TmpBT.BookIt Then
    '                SumTarget = SumTarget + TmpBT.Weeks(week).TRP
    '                SumAllAdults = SumAllAdults + TmpBT.Weeks(week).TRPAllAdults
    '                If chkIncludeInSums.Checked Then
    '                    For i As Integer = TmpBT.Weeks(week).StartDate To TmpBT.Weeks(week).EndDate
    '                        SumTarget = SumTarget + TmpBT.Compensations.GetCompensationForDateInMainTarget(Date.FromOADate(i))
    '                        SumAllAdults = SumAllAdults + TmpBT.Compensations.GetCompensationForDateInAllAdults(Date.FromOADate(i))
    '                    Next
    '                End If
    '            End If
    '        Next
    '    Next
    '    If Row = 0 Or Row = -1 Then
    '        SkipIt = True
    '        grdSumWeeks.Rows(0).Cells(week - 1).Value = SumTarget
    '    End If
    '    If Row = 1 Or Row = -1 Then
    '        SkipIt = True
    '        grdSumWeeks.Rows(1).Cells(week - 1).Value = SumAllAdults
    '    End If
    '    grdSumWeeks.Invalidate()
    '    SkipIt = False
    'End Sub


    Private Sub grdSumWeeks_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSumWeeks.CellValueNeeded
        Try
            If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then Exit Sub

            Dim SumTarget As Single = 0
            Dim i As Integer
            Dim TmpBT As Trinity.cBookingType
            If cmbDisplay.SelectedIndex > DisplayModeEnum.Imp000 Then 'If not TRP or TRP30 is being shown
                If e.RowIndex = 0 Then
                    e.Value = 1
                Else
                    e.Value = "-"
                End If
                Exit Sub
            End If

            'buying target
            If e.RowIndex = 0 Then
                'we only take every second row in the for loops since the table have both buying target and all adults ratings

                If chkIncludeInSums.Checked Then
                    Dim d As Integer
                    Dim week As String


                    For i = 1 To grdTRP.Rows.Count - 1 Step 2
                        SumTarget += grdTRP.Rows(i).Cells(e.ColumnIndex).Value 'gets the week TRP from the grid

                        week = grdTRP.Columns(e.ColumnIndex).HeaderText 'get the current week

                        If grdTRP.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then

                            Dim comb As Trinity.cCombination = grdTRP.Rows(i).Tag

                            For Each cc As Trinity.cCombinationChannel In comb.Relations

                                'get the TRPs for each date and add it to the sum
                                For d = cc.Bookingtype.Weeks(week).StartDate To cc.Bookingtype.Weeks(week).EndDate
                                    SumTarget += cc.Bookingtype.Compensations.GetCompensationForDateInMainTarget(Date.FromOADate(d))
                                Next

                            Next

                        Else

                            TmpBT = grdTRP.Rows(i).Tag 'gets the current bookingtype

                            'get the TRPs for each date and add it to the sum
                            For d = TmpBT.Weeks(week).StartDate To TmpBT.Weeks(week).EndDate
                                SumTarget += TmpBT.Compensations.GetCompensationForDateInMainTarget(Date.FromOADate(d))
                            Next

                        End If

                    Next
                Else
                    For i = 1 To grdTRP.Rows.Count - 1 Step 2
                        SumTarget += grdTRP.Rows(i).Cells(e.ColumnIndex).Value
                    Next
                End If
                e.Value = SumTarget


            ElseIf e.RowIndex = 1 Then 'All adults

                'we only take every second row in the for loops since the table have both buying target and all adults ratings
                Dim TmpChan As Trinity.cChannel

                If chkIncludeInSums.Checked Then
                    Dim d As Integer
                    Dim week As String = grdTRP.Columns(e.ColumnIndex).HeaderText 'get the current week

                    For Each TmpChan In Campaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            If TmpBT.BookIt Then
                                SumTarget += TmpBT.Weeks(week).TRPAllAdults
                                For d = TmpBT.Weeks(week).StartDate To TmpBT.Weeks(week).EndDate
                                    SumTarget = SumTarget + TmpBT.Compensations.GetCompensationForDateInAllAdults(Date.FromOADate(d))
                                Next
                            End If
                        Next
                    Next
                Else
                    For i = 0 To grdTRP.Rows.Count - 1 Step 2
                        TmpBT = grdTRP.Rows(i).Tag
                        SumTarget += TmpBT.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPAllAdults
                    Next
                End If
                e.Value = SumTarget
            Else
                e.Value = Campaign.EstimatedWeeklyReach(grdTRP.Columns(e.ColumnIndex).HeaderText)
            End If
        Catch ex As NullReferenceException
            Windows.Forms.MessageBox.Show("There was an error while rendering the allocate window." & vbNewLine & "This is usually caused by a week/channel/film etc being removed in Setup while the Allocate window is open." & vbNewLine & "Allocate will now close, please re-open it. If the error persists - contact trinity@mecglobal.com.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End Try
    End Sub

    Private Sub grdSumWeeks_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSumWeeks.CellValuePushed
        If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then Exit Sub
        Saved = False

        If e.RowIndex = 2 Then
            Campaign.EstimatedWeeklyReach(grdTRP.Columns(e.ColumnIndex).HeaderText) = e.Value
            Exit Sub
        End If

        Dim changeRatio As Single

        Dim TRPSum As Single = 0
        Dim r As Integer
        Dim TmpWeek As Trinity.cWeek
        Dim TmpBT As Trinity.cBookingType

        'need to check if we have any unlocked weeks
        If grdTRP.Rows(0).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            If grdTRP.Rows(0).Tag.relations(1).bookingtype.weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).IsLocked Then
                MsgBox("This week is locked, if you wish to change TRPs you need to unlock it first", MsgBoxStyle.Information, "Locked Week")
                Exit Sub
            End If
        Else
            TmpWeek = grdTRP.Rows(0).Cells(e.ColumnIndex).Tag
            If TmpWeek.IsLocked Then
                MsgBox("This week is locked, if you wish to change TRPs you need to unlock it first", MsgBoxStyle.Information, "Locked Week")
                Exit Sub
            End If
        End If

        'check what TRP type was entered
        If e.RowIndex = 0 Then
            'we need to get the total sum of the weeks not LOCKED to get the change ratio
            For r = 0 To grdTRP.Rows.Count - 1 Step 2
                If grdTRP.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    Dim tmpC As Trinity.cCombination = grdTRP.Rows(r).Tag
                    If Not tmpC.Relations(1).Bookingtype.IsLocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            TRPSum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP
                        Next
                    End If
                Else
                    TmpBT = grdTRP.Rows(r).Tag
                    If Not TmpBT.IsLocked Then
                        TmpWeek = grdTRP.Rows(r).Cells(e.ColumnIndex).Tag
                        TRPSum += TmpWeek.TRP
                    End If
                End If
            Next 'end stepping trough the TRP rows

            changeRatio = (e.Value - (grdSumWeeks.Rows(e.RowIndex).Cells(e.ColumnIndex).Value - TRPSum)) / TRPSum

            'go trough every other row and get the object and multiply the TRP value with the ratio
            'It is importat to set the TRPcontrol to True markin that TRPs and not budget is set
            For r = 0 To grdTRP.Rows.Count - 1 Step 2
                If grdTRP.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    Dim tmpC As Trinity.cCombination = grdTRP.Rows(r).Tag
                    If Not tmpC.Relations(1).Bookingtype.IsLocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP *= changeRatio
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                        Next
                    End If
                Else
                    If Not grdTRP.Rows(r).Tag.islocked Then
                        TmpWeek = grdTRP.Rows(r).Cells(e.ColumnIndex).Tag

                        TmpWeek.TRP *= changeRatio
                        TmpWeek.TRPControl = True
                    End If
                End If
            Next 'end stepping trough the TRP rows

        ElseIf e.RowIndex = 1 Then 'rowindex = 1, IE we are setting All adults TRP
            'we need to get the total sum of the weeks not LOCKED to get the change ratio
            For r = 0 To grdTRP.Rows.Count - 1 Step 2
                If grdTRP.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    Dim tmpC As Trinity.cCombination = grdTRP.Rows(r).Tag
                    If Not tmpC.Relations(1).Bookingtype.IsLocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            TRPSum = tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget
                        Next
                    End If
                Else
                    TmpBT = grdTRP.Rows(r).Tag
                    If Not TmpBT.IsLocked Then
                        TmpWeek = grdTRP.Rows(r).Cells(e.ColumnIndex).Tag
                        TRPSum += TmpWeek.TRPBuyingTarget
                    End If
                End If
            Next 'end stepping trough the TRP rows

            changeRatio = (e.Value - (grdSumWeeks.Rows(e.RowIndex).Cells(e.ColumnIndex).Value - TRPSum)) / TRPSum

            'go trough every other row and get the object and multiply the TRP value with the ratio
            'It is importat to set the TRPcontrol to True markin that TRPs and not budget is set
            For r = 0 To grdTRP.Rows.Count - 1 Step 2
                If grdTRP.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    Dim tmpC As Trinity.cCombination = grdTRP.Rows(r).Tag
                    If Not tmpC.Relations(1).Bookingtype.IsLocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPAllAdults *= changeRatio
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                        Next
                    End If
                Else
                    If Not grdTRP.Rows(r).Tag.islocked Then
                        TmpWeek = grdTRP.Rows(r).Cells(e.ColumnIndex).Tag
                        TmpWeek.TRPAllAdults *= changeRatio
                        TmpWeek.TRPControl = True
                    End If
                End If
            Next 'end stepping trough the TRP rows
        End If

        grdBudget.Invalidate()
        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
    End Sub

    Private Sub onlyOneSelectedGrid(Optional ByVal sender As Object = Nothing, Optional ByVal e As System.EventArgs = Nothing) Handles grdAV.GotFocus, grdBudget.GotFocus, grdCompensation.GotFocus, grdDiscounts.GotFocus, grdFilms.GotFocus, grdGrandSum.GotFocus, grdIndex.GotFocus, grdSpotCount.GotFocus, grdSumChannels.GotFocus, grdSumWeeks.GotFocus, grdTRP.GotFocus
        If sender Is Nothing Then
            grdAV.ClearSelection()
            grdBudget.ClearSelection()
            grdCompensation.ClearSelection()
            grdDiscounts.ClearSelection()
            grdFilms.ClearSelection()
            grdGrandSum.ClearSelection()
            grdIndex.ClearSelection()
            grdSpotCount.ClearSelection()
            grdSumChannels.ClearSelection()
            grdSumWeeks.ClearSelection()
            grdTRP.ClearSelection()
        Else
            Dim grid As Windows.Forms.DataGridView
            grid = sender

            Select Case grid.Name
                Case Is = "grdAV"
                    grdBudget.ClearSelection()
                    grdCompensation.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSpotCount.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdBudget"
                    grdAV.ClearSelection()
                    grdCompensation.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSpotCount.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdCompensation"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSpotCount.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdDiscounts"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdCompensation.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSpotCount.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdFilms"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdCompensation.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSpotCount.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdGrandSum"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdCompensation.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSpotCount.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdIndex"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdCompensation.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdSpotCount.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdSpotCount"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdCompensation.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdSumChannels"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdCompensation.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSpotCount.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdSumWeeks"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdCompensation.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSpotCount.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdTRP"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdCompensation.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSpotCount.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
            End Select
        End If
    End Sub
    Private Sub cmdCopyWeeks_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyWeeks.Click
        Dim mnuCopyWeeks As New Windows.Forms.ContextMenuStrip
        mnuCopyWeeks.Items.Clear()

        'Show weeks to copy from
        For i As Integer = 0 To grdFilms.Columns.Count - 2
            mnuCopyWeeks.Items.Add(grdFilms.Columns(i).HeaderText, Nothing, AddressOf CopyWeeks)

        Next
        mnuCopyWeeks.Show(cmdCopyWeeks, 0, cmdCopyWeeks.Height)
    End Sub
    Sub CopyFromChannels(ByVal sender As Object, ByVal e As EventArgs)
        'Method to copy filmsplit from channel to channels
        'Added by JK

        Dim str As String
        Dim CopyFromBT As Trinity.cBookingType = Nothing
        Dim CopyFromCombination As Trinity.cCombination = Nothing
        Dim CopyFromCombinationChannel As Trinity.cCombinationChannel = Nothing
        Dim CopyToBT As Trinity.cBookingType = Nothing
        Dim CopyToCombination As Trinity.cCombination = Nothing
        Dim CopyToCombinationChannel As Trinity.cCombinationChannel = Nothing


        str = DirectCast(sender, System.Windows.Forms.ToolStripItem).Text

        'Find from which channels/channels
        For Each tmpChan As Trinity.cChannel In Campaign.Channels
            For Each tmpBt As Trinity.cBookingType In tmpChan.BookingTypes
                If tmpBt.Combination Is Nothing Then
                    Dim strBT As String = ""
                    strBT = tmpChan.ChannelName + " " + tmpBt.Name
                    'Take the channel object
                    If strBT = str Then
                        CopyFromBT = tmpBt
                    End If
                Else
                    For Each tmpC As Trinity.cCombination In Campaign.Combinations
                        If tmpC.ShowAsOne Then
                            'Take the combinationChannel in the combination
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                Dim strTmpCombo As String = ""
                                strTmpCombo = tmpC.Name.ToString()
                                If str = strTmpCombo Then
                                    CopyFromCombinationChannel = tmpCC
                                End If
                            Next
                        Else
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                Dim strTmpC As String = ""
                                strTmpC = tmpCC.ChannelName + " " + tmpCC.BookingTypeName
                                If str = strTmpC Then
                                    CopyFromCombinationChannel = tmpCC
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        Next
        'Find which Channel/Channels to copy to
        For Each tmpChan As Trinity.cChannel In Campaign.Channels
            For Each tmpBt As Trinity.cBookingType In tmpChan.BookingTypes
                If tmpBt.Combination Is Nothing Then
                    Dim strTmpBt As String = ""
                    strTmpBt = tmpChan.ChannelName + " " + tmpBt.Name
                    'Take the channel object
                    If cmbFilmChannel.SelectedItem.ToString() = strTmpBt Then
                        CopyToBT = tmpBt
                    End If
                Else
                    For Each tmpCombo As Trinity.cCombination In Campaign.Combinations
                        If tmpCombo.ShowAsOne Then
                            Dim strTmpCombo As String = ""
                            strTmpCombo = tmpCombo.Name.ToString()
                            'Take the Combination object
                            If cmbFilmChannel.SelectedItem.ToString() = strTmpCombo Then
                                CopyToCombination = tmpCombo
                            End If
                        Else
                            For Each tmpCC As Trinity.cCombinationChannel In tmpCombo.Relations
                                Dim strTmpCC As String = ""
                                strTmpCC = tmpCC.ChannelName.ToString + " " + tmpCC.BookingTypeName
                                'Take the combination object obj
                                If cmbFilmChannel.SelectedItem.ToString() = strTmpCC Then
                                    CopyToCombinationChannel = tmpCC
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        Next
        'From BT to BT
        If Not CopyFromBT Is Nothing And Not CopyToBT Is Nothing Then
            For Each tmpWeek As Trinity.cWeek In CopyToBT.Weeks
                For Each tmpFilm As Trinity.cFilm In tmpWeek.Films
                    tmpWeek.Films(tmpFilm.Name).Share = CopyFromBT.Weeks(tmpWeek.Name).Films(tmpFilm.Name).Share
                Next
            Next
        End If
        'Copy to combination from combination
        If Not CopyToCombination Is Nothing And CopyFromCombinationChannel IsNot Nothing Then
            For Each tmpCombo As Trinity.cCombination In Campaign.Combinations
                If tmpCombo.ShowAsOne Then
                    If tmpCombo Is CopyToCombination Then
                        For Each tmpCC As Trinity.cCombinationChannel In tmpCombo.Relations
                            For Each tmpWeek As Trinity.cWeek In tmpCC.Bookingtype.Weeks
                                For Each tmpFilm As Trinity.cFilm In tmpWeek.Films
                                    tmpFilm.Share = CopyFromCombinationChannel.Bookingtype.Weeks(tmpWeek.Name).Films(tmpFilm.Name).Share
                                Next
                            Next
                        Next
                    End If
                End If
            Next
        ElseIf Not CopyToBT Is Nothing And CopyFromCombinationChannel IsNot Nothing Then
            'copy to bt to combinationChannel
            For Each tmpWeek As Trinity.cWeek In CopyToBT.Weeks
                For Each tmpFilm As Trinity.cFilm In tmpWeek.Films
                    tmpWeek.Films(tmpFilm.Name).Share = CopyFromCombinationChannel.Bookingtype.Weeks(tmpWeek.Name).Films(tmpFilm.Name).Share
                Next
            Next
            'Copy to combinationchannel from bt
        ElseIf Not CopyToCombinationChannel Is Nothing And CopyFromBT IsNot Nothing Then
            For Each tmpCombo As Trinity.cCombination In Campaign.Combinations
                For Each tmpCC As Trinity.cCombinationChannel In tmpCombo.Relations
                    For Each tmpWeek As Trinity.cWeek In tmpCC.Bookingtype.Weeks
                        For Each tmpFilm As Trinity.cFilm In tmpWeek.Films
                            tmpFilm.Share = CopyFromBT.Weeks(tmpWeek.Name).Films(tmpFilm.Name).Share
                        Next
                    Next
                Next
            Next
            'Copy to Ccombinationchannel from combinationChannel
        ElseIf Not CopyToCombinationChannel Is Nothing And CopyFromCombinationChannel IsNot Nothing Then
            For Each tmpCombo As Trinity.cCombination In Campaign.Combinations
                For Each tmpCC As Trinity.cCombinationChannel In tmpCombo.Relations
                    For Each tmpWeek As Trinity.cWeek In tmpCC.Bookingtype.Weeks
                        For Each tmpFilm As Trinity.cFilm In tmpWeek.Films
                            tmpFilm.Share = CopyFromCombinationChannel.Bookingtype.Weeks(tmpWeek.Name).Films(tmpFilm.Name).Share
                        Next
                    Next
                Next
            Next
            'Copy to combo from BT
        ElseIf Not CopyToCombination Is Nothing And CopyFromBT IsNot Nothing Then
            For Each tmpCombo As Trinity.cCombination In Campaign.Combinations
                If tmpCombo Is CopyToCombination Then
                    For Each tmpCC As Trinity.cCombinationChannel In tmpCombo.Relations
                        For Each tmpWeek As Trinity.cWeek In tmpCC.Bookingtype.Weeks
                            For Each tmpFilm As Trinity.cFilm In tmpWeek.Films
                                tmpFilm.Share = CopyFromBT.Weeks(tmpWeek.Name).Films(tmpFilm.Name).Share
                            Next
                        Next
                    Next
                End If
            Next
        End If
        grdFilms.Invalidate()
        grdDiscounts.Invalidate()
        'update all the grids since the costs and TRP is changed it you change the films
        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
        grdBudget.Invalidate()
        grdSpotCount.Invalidate()
        grdSumChannels.Invalidate()
        ColorFilmGrid()
    End Sub
    Sub CopyWeeks(ByVal sender As Object, ByVal e As EventArgs)
        Dim str As String
        Dim weeknum As Integer
        str = DirectCast(sender, System.Windows.Forms.ToolStripItem).Text
        grdFilms.TurnOffAutoRedraw()
        For i As Integer = 0 To grdFilms.Columns.Count - 3
            If grdFilms.Columns(i).HeaderText = str Then
                weeknum = i
            End If
        Next
        For i As Integer = 0 To grdFilms.Columns.Count - 3
            For j As Integer = 0 To grdFilms.Rows.Count - 1
                grdFilms.Rows(j).Cells(i).Value = grdFilms.Rows(j).Cells(weeknum).Value * 100
            Next
        Next
        grdFilms.TurnOnAutoRedraw()
        grdFilms.Invalidate()
    End Sub

    Private Sub btnTRP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTRP.CheckedChanged, btnBudget.CheckedChanged
        If sender Is btnTRP Then
            grpFilms.Text = "Films (% of TRPs)"
        Else
            grpFilms.Text = "Films (% of Budget)"
        End If
        grdFilms.Invalidate()
    End Sub

    Private Sub cmdEditCTC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditCTC.Click
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        Windows.Forms.Application.DoEvents()


        'Try
        '    Campaign.PlannedTotCTC = InputBox("CTC:", "T R I N I T Y", Campaign.PlannedTotCTC)
        'Catch ex As Exception
        '    Windows.Forms.MessageBox.Show(ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        'End Try

        ' Update the CTC
        Dim dialogResult As String = InputBox("CTC:", "T R I N I T Y", Campaign.PlannedTotCTC)
        Dim result As Decimal

        If (Decimal.TryParse(dialogResult, result)) Then
            Try
                Campaign.PlannedTotCTC = result
            Catch ex As Exception
                MessageBox.Show(ex.Message, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try

        End If

        grdSpotCount.Invalidate()
        grdSumChannels.Invalidate()
        'SkipIt = True
        grdBudget.Invalidate()
        'SkipIt = True
        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
        grdSpotCount.Invalidate()
        'SkipIt = False
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub lblOldPricelist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblOldPricelist.Click
        MsgBox(strPricelistUpdate, MsgBoxStyle.Information, "")
    End Sub

    Sub mouse_enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblOldPricelist.MouseEnter
        Me.Cursor = Windows.Forms.Cursors.Hand
    End Sub

    Sub mouse_leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblOldPricelist.MouseLeave
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub mnuUseCustom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUseCustom.Click
        frmDates.ShowDialog()
        If frmDates.DialogResult = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        mnuLastWeeks.Checked = False
        mnuLastYear.Checked = False
        mnuUseCustom.Checked = True
        cmdNaturalDelivery.Tag = Format(frmDates.dateFrom.Value, "ddMMyy") & "-" & Format(frmDates.dateTo.Value, "ddMMyy")

        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub grdTRP_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdTRP.CellFormatting
        Dim TmpBT As Trinity.cBookingType

        If grdTRP.Rows(e.RowIndex).Tag.GetType Is GetType(Trinity.cBookingType) Then
            TmpBT = grdTRP.Rows(e.RowIndex).Tag
        Else
            TmpBT = DirectCast(grdTRP.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations(1).Bookingtype
        End If

        If TmpBT.BuyingTarget Is Nothing Then Exit Sub

        Dim oldStyle As Windows.Forms.DataGridViewCellStyle = New Windows.Forms.DataGridViewCellStyle(e.CellStyle)

        If cmbDisplay.SelectedIndex = DisplayModeEnum.TRP AndAlso Not (TmpBT.IsPremium AndAlso TmpBT.IsSpecific) Then
            If grdTRP.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                If DirectCast(grdTRP.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations(1).Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl Then
                    If DirectCast(grdTRP.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations(1).Bookingtype.IsLocked OrElse DirectCast(grdTRP.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations(1).Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).IsLocked Then
                        e.CellStyle = styleNormalDLocked
                    Else
                        e.CellStyle = styleNormalD
                    End If
                Else
                    If DirectCast(grdTRP.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations(1).Bookingtype.IsLocked OrElse DirectCast(grdTRP.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations(1).Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).IsLocked Then
                        e.CellStyle = styleNoSetDLocked
                    Else
                        e.CellStyle = styleNoSetD
                    End If
                End If
                oldStyle = e.CellStyle
                'This bit colors the cell red and shows a tooltip if the cell has more TRP than allowed by the user specified max ratings in the price list
                If e.RowIndex / 2 = e.RowIndex \ 2 Then
                    Dim setRed As Boolean = False
                    For Each tmpC As Trinity.cCombinationChannel In DirectCast(grdTRP.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations
                        If tmpC.Bookingtype.BuyingTarget.MaxRatings > 0 Then
                            If tmpC.Bookingtype.Weeks(e.ColumnIndex + 1).TRPBuyingTarget > tmpC.Bookingtype.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).MaxRatings Then
                                setRed = True
                            End If
                        End If
                    Next
                    If setRed Then
                        e.CellStyle = styleExceeded
                        grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).ToolTipText = "Planned TRP exceeeds max TRP per week specified in the price list"
                    Else
                        e.CellStyle = oldStyle
                        grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).ToolTipText = ""
                    End If
                End If

            Else
                Dim TmpWeek As Trinity.cWeek = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
                If TmpWeek Is Nothing Then Exit Sub
                If TmpWeek.TRPControl Then
                    If TmpBT.IsCompensation Then
                        e.CellStyle = styleCompensationD
                    ElseIf grdTRP.Rows(e.RowIndex).Tag.Islocked OrElse TmpWeek.IsLocked Then
                        e.CellStyle = styleNormalDLocked
                    Else
                        e.CellStyle = styleNormalD
                    End If
                Else
                    If TmpBT.IsCompensation Then
                        e.CellStyle = styleCompensationD
                    ElseIf grdTRP.Rows(e.RowIndex).Tag.Islocked OrElse TmpWeek.IsLocked Then
                        e.CellStyle = styleNoSetDLocked
                    Else
                        e.CellStyle = styleNoSetD
                    End If
                End If



                'This bit colors the cell red and shows a tooltip if the cell has more TRP than allowed by the user specified max ratings in the price list
                If e.RowIndex / 2 = e.RowIndex \ 2 Then
                    If Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Pricelist.Targets(TmpBT.BuyingTarget.TargetName).MaxRatings > 0 Then
                        If Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Pricelist.Targets(TmpBT.BuyingTarget.TargetName).MaxRatings _
                        < _
                        Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Weeks(e.ColumnIndex + 1).TRP Then
                            e.CellStyle = styleExceeded
                            grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).ToolTipText = "Planned TRP exceeeds max TRP per week specified in the price list"
                        Else
                            e.CellStyle = oldStyle
                            grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).ToolTipText = ""
                        End If
                    End If
                End If

            End If
            grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = False
        Else
            If cmbDisplay.SelectedIndex > 0 Then
                e.CellStyle = styleCantSetP
            Else
                e.CellStyle = styleCantSetN
            End If
            grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
        End If
    End Sub

    Private Sub LockChannel(ByVal sender As Object, ByVal e As System.EventArgs)
        If sender.tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            Dim TmpC As Trinity.cCombination = sender.tag

            For Each TmpCC As Trinity.cCombinationChannel In TmpC.Relations
                TmpCC.Bookingtype.IsLocked = Not TmpCC.Bookingtype.IsLocked
            Next
        Else
            Dim BT As Trinity.cBookingType = sender.tag
            BT.IsLocked = Not BT.IsLocked
        End If

        grdTRP.Invalidate()
        grdBudget.Invalidate()
    End Sub

    Private Sub LockWeek(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strWeek As String = sender.tag

        For i As Integer = 1 To grdTRP.Rows.Count - 1 Step 2
            If grdTRP.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                Dim TmpC As Trinity.cCombination = grdTRP.Rows(i).Tag

                For Each TmpCC As Trinity.cCombinationChannel In TmpC.Relations
                    TmpCC.Bookingtype.Weeks(strWeek).IsLocked = Not TmpCC.Bookingtype.Weeks(strWeek).IsLocked
                Next
            Else
                Dim TmpBT As Trinity.cBookingType = grdTRP.Rows(i).Tag
                Dim TmpWeek As Trinity.cWeek = TmpBT.Weeks(strWeek)
                TmpWeek.IsLocked = Not TmpWeek.IsLocked
            End If
        Next

        grdTRP.Invalidate()
        grdBudget.Invalidate()
    End Sub

    Private Sub grdTRP_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdTRP.CellMouseClick, grdTRP.CellMouseClick
        If e.RowIndex = -1 Then Exit Sub

        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim mnu As New System.Windows.Forms.ContextMenuStrip
            Dim strWeek As String = grdTRP.Columns(e.ColumnIndex).HeaderText

            Dim item As System.Windows.Forms.ToolStripItem
            Dim TRP As System.Windows.Forms.ToolStripMenuItem
            Dim Budget As System.Windows.Forms.ToolStripMenuItem
            Dim sendItem As Object

            'we can lock the weeks so they are not included when you fex set a total budget
            With DirectCast(mnu.Items.Add("Lock/Unlock week " & grdTRP.Columns(e.ColumnIndex).HeaderText, Nothing, AddressOf LockWeek), System.Windows.Forms.ToolStripMenuItem)
                .Tag = grdTRP.Columns(e.ColumnIndex).HeaderText
            End With

            sendItem = grdTRP.Rows(e.RowIndex).Tag

            With DirectCast(mnu.Items.Add("Lock/Unlock channel: " & sendItem.ToString, Nothing, AddressOf LockChannel), System.Windows.Forms.ToolStripMenuItem)
                .Tag = sendItem
            End With

            item = mnu.Items.Add("Copy TRP to all weeks (" & sendItem.ToString & ")", Nothing, AddressOf copyTRPtoAllWeeks)
            item.Tag = e.ColumnIndex
            item.Name = e.RowIndex

            TRP = mnu.Items.Add("Copy TRPs from week " & grdTRP.Columns(e.ColumnIndex).HeaderText & " to:")
            Budget = mnu.Items.Add("Copy Budget from week " & grdTRP.Columns(e.ColumnIndex).HeaderText & " to:")

            Dim weeknum As Integer = 0

            With DirectCast(mnu.Items.Add("Estimate % " & Campaign.WeeklyFrequency & "+"), System.Windows.Forms.ToolStripMenuItem)
                With .DropDownItems.Add("Use last weeks of data", Nothing, AddressOf CalculateReachForWeek)
                    .Tag = e.ColumnIndex
                End With
                With .DropDownItems.Add("Use same period last year", Nothing, AddressOf CalculateReachForWeek)
                    .Tag = e.ColumnIndex
                End With
            End With

            For Each _week As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                If Not _week.Name = strWeek Then
                    item = TRP.DropDown.Items.Add(_week.Name, Nothing, AddressOf CopyTRPWeek)
                    item.Tag = strWeek
                    item.Name = weeknum
                    item = Budget.DropDown.Items.Add(_week.Name, Nothing, AddressOf CopyBudgetWeek)
                    item.Tag = strWeek
                    item.Name = weeknum
                    weeknum += 1
                Else
                    weeknum += 1
                End If
            Next
            item = TRP.DropDown.Items.Add("All weeks", Nothing, AddressOf CopyTRPWeek)
            item.Tag = strWeek
            item.Name = "All"
            item = Budget.DropDown.Items.Add("All weeks", Nothing, AddressOf CopyBudgetWeek)
            item.Tag = strWeek
            item.Name = "All"

            item = mnu.Items.Add("Copy to clipboard", Nothing, AddressOf TRPtoClipboard)
            item.Tag = "CopyTRP"
            item.Name = "CopyTRP"

            mnu.Show(Me.ParentForm, New System.Drawing.Point(MousePosition.X, MousePosition.Y - 15))

        End If
    End Sub

    Private Sub CalculateReachForWeek(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Periodstr As String = ""
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim IsUsed As Boolean
        'Dim i As Integer
        Dim TmpStr As String
        Dim UseSponsorship As Boolean = False
        Dim UseCommercial As Boolean = False

        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        'Periodstr = "-" & Trim(Campaign.Channels(1).BookingTypes(1).Weeks.Count) & "fw"

        Karma = New Trinity.cKarma(Campaign)
        Karma.Tag = sender.text
        Dim TmpDate As Long = Campaign.EndDate
        Dim DateDiff As Long
        'Dim DateDiff2 As Long
        Dim startDate As String
        Dim endDate As String

        If sender.text = "Use last weeks of data" Then
            'Figure out what the last date we have spot data for is
            'When we are done, TmpDate will be this date
            While TmpDate >= Karma.KarmaAdedge.getDataRangeTo(Connect.eDataType.mSpot)
                TmpDate = TmpDate - 1
            End While

            'DateDiff will be the difference in days between the last day of this campaign and the last day we have data for
            DateDiff = Campaign.EndDate - TmpDate
            'DateDiff2 = DateDiff
            ''This block added just to make sure the reference period begins on a Monday
            'While Date.FromOADate(Campaign.Channels(1).BookingTypes(1).Weeks(sender.tag + 1).StartDate - DateDiff2).DayOfWeek <> DayOfWeek.Monday
            '    DateDiff2 += 1
            'End While

            'startDate is the date of the first day of the week we are interested in estimating, minus the number of days of dateDiff
            startDate = Format(Date.FromOADate(Campaign.Channels(1).BookingTypes(1).Weeks(sender.tag + 1).StartDate - DateDiff), "ddMMyy")
            endDate = Format(Date.FromOADate(Campaign.Channels(1).BookingTypes(1).Weeks(sender.tag + 1).EndDate - DateDiff), "ddMMyy")
            Periodstr = startDate & "-" & endDate
        Else
            TmpDate = Date.FromOADate(Campaign.EndDate).AddYears(-1).ToOADate
            While Weekday(Date.FromOADate(TmpDate), FirstDayOfWeek.Monday) <> Weekday(Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday)
                TmpDate = TmpDate + 1
            End While
            DateDiff = Campaign.EndDate - TmpDate

            Periodstr = Format(Date.FromOADate(Campaign.Channels(1).BookingTypes(1).Weeks(sender.tag + 1).StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(Campaign.Channels(1).BookingTypes(1).Weeks(sender.tag + 1).EndDate - DateDiff), "ddMMyy")
        End If

        Karma.ReferencePeriod = Periodstr

        For Each TmpChan In Campaign.Channels
            IsUsed = False
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    IsUsed = True
                End If
                If TmpBT.IsSponsorship AndAlso TmpBT.BookIt Then
                    UseSponsorship = True
                ElseIf TmpBT.BookIt Then
                    UseCommercial = True
                End If
            Next
            If IsUsed Then
                Karma.Channels.Add(TmpChan.ChannelName)
                'We add only 1 week for this channel in the Karma object
            End If
        Next
        Karma.Weeks = 1
        frmProgress.Status = "Fetching spots..."
        frmProgress.Progress = 0
        frmProgress.Show()
        AddHandler Karma.PopulateProgress, AddressOf Progress
        Karma.Populate(UseSponsorship, UseCommercial)

        TmpStr = Campaign.Name
        If TmpStr Is Nothing Then TmpStr = ""
        Karma.Campaigns.Add(TmpStr, Campaign)
        frmProgress.Status = "Calculating reach for " & TmpStr & " week " & grdTRP.Columns(CInt(sender.tag)).HeaderText
        frmProgress.Progress = 0
        frmProgress.Show()
        AddHandler Karma.Campaigns(TmpStr).Progress, AddressOf Progress
        Karma.Campaigns(TmpStr).Run(sender.tag + 1)
        frmProgress.Hide()

        Campaign.EstimatedWeeklyReach(grdTRP.Columns(CInt(sender.tag)).HeaderText) = Karma.Campaigns.Item(TmpStr).Reach(0, Campaign.WeeklyFrequency)

        grdSumWeeks.Invalidate()

        grdSumWeeks.Cursor = Windows.Forms.Cursors.Default
        Me.Cursor = Windows.Forms.Cursors.Default

    End Sub

    Sub Progress(ByVal p As Single)
        frmProgress.Progress = p
    End Sub

    Private Sub CopyBudgetWeek(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim newWeek As String = sender.text
        Dim oldWeek As String = sender.tag

        If sender.name <> "All" Then
            Dim column As Integer = sender.name
            SkipIt = True
            For Each c As Trinity.cChannel In Campaign.Channels
                For Each bt As Trinity.cBookingType In c.BookingTypes
                    If bt.BookIt Then
                        SkipIt = True
                        bt.Weeks(newWeek).NetBudget = bt.Weeks(oldWeek).NetBudget
                        bt.Weeks(newWeek).TRPControl = False
                    End If
                Next
            Next
            SkipIt = False
        Else
            SkipIt = True
            For column As Integer = 0 To grdTRP.Columns.Count - 1
                newWeek = grdTRP.Columns(column).HeaderText
                For Each c As Trinity.cChannel In Campaign.Channels
                    For Each bt As Trinity.cBookingType In c.BookingTypes
                        If bt.BookIt Then
                            SkipIt = True
                            bt.Weeks(newWeek).NetBudget = bt.Weeks(oldWeek).NetBudget
                            bt.Weeks(newWeek).TRPControl = False
                        End If
                    Next
                Next
                SkipIt = False
            Next
        End If
        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
        grdSpotCount.Invalidate()
        grdSumChannels.Invalidate()
        grdBudget.Invalidate()
    End Sub

    Private Sub CopyTRPWeek(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim newWeek As String = sender.text
        Dim oldWeek As String = sender.tag

        If sender.name <> "All" Then
            Dim column As Integer = sender.name

            SkipIt = True
            For Each c As Trinity.cChannel In Campaign.Channels
                For Each bt As Trinity.cBookingType In c.BookingTypes
                    If bt.BookIt Then
                        SkipIt = True
                        bt.Weeks(newWeek).TRP = bt.Weeks(oldWeek).TRP
                        bt.Weeks(newWeek).TRPControl = True
                    End If
                Next
            Next
            SkipIt = False
        Else
            SkipIt = True
            For column As Integer = 0 To grdTRP.Columns.Count - 1
                newWeek = grdTRP.Columns(column).HeaderText
                For Each c As Trinity.cChannel In Campaign.Channels
                    For Each bt As Trinity.cBookingType In c.BookingTypes
                        If bt.BookIt Then
                            SkipIt = True
                            bt.Weeks(newWeek).TRP = bt.Weeks(oldWeek).TRP
                            bt.Weeks(newWeek).TRPControl = True
                        End If
                    Next
                Next
                SkipIt = False
            Next
        End If

        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
        grdSpotCount.Invalidate()
        grdSumChannels.Invalidate()
        grdBudget.Invalidate()

    End Sub

    Private Sub copyTRPtoAllWeeks(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim WeekToCopy As Trinity.cWeek = grdTRP.Rows(sender.name).Cells(sender.tag).Tag
        Dim TmpBT As Trinity.cBookingType

        TmpBT = grdTRP.Rows(sender.name).Tag
        For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
            If Not TmpWeek Is WeekToCopy Then
                If cmbUniverse.SelectedIndex = 0 Then
                    TmpWeek.TRP = WeekToCopy.TRP
                    TmpWeek.TRPControl = True
                End If
            End If
        Next

        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
        grdSumChannels.Invalidate()
        grdGrandSum.Invalidate()
        grdBudget.Invalidate()
    End Sub

    Private Sub lblCTC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblCTC.Click

    End Sub

    Private Sub grdBudget_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdBudget.CellContentClick

    End Sub

    Private Sub grdTRP_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdTRP.KeyUp
        If e.Control AndAlso e.KeyCode = Windows.Forms.Keys.V Then
            Dim TRPs() As String = My.Computer.Clipboard.GetText.Split(vbTab)
            For c As Integer = grdTRP.SelectedCells(0).ColumnIndex To Math.Min(grdTRP.SelectedCells(0).ColumnIndex + TRPs.Length - 1, grdTRP.ColumnCount)
                Dim TRP As Single
                If TRPs(c - grdTRP.SelectedCells(0).ColumnIndex) <> "" Then
                    TRP = TRPs(c - grdTRP.SelectedCells(0).ColumnIndex)
                Else
                    TRP = 0
                End If
                grdTRP.Rows(grdTRP.SelectedCells(0).RowIndex).Cells(c).Value = TRP
            Next
        End If
    End Sub

    Private Sub grdDiscounts_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDiscounts.CellContentClick

    End Sub
    'Added Backcolor - JOOS 2018-11-16
    Private Sub cmdLockOnBudget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLockOnBudget.Click
        changeTRPLockOnAll(False)
        cmdLockOnBudget.BackColor = Color.SteelBlue
    End Sub
    'Added Backcolor - JOOS 2018-11-16
    Private Sub cmdLockOnTRP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLockOnTRP.Click
        changeTRPLockOnAll(True)
        cmdLockOnTRP.BackColor = Color.SteelBlue
    End Sub

    Private Sub changeTRPLockOnAll(ByVal setValue As Boolean)
        For i As Integer = 0 To grdBudget.RowCount - 2
            If grdBudget.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                Dim combo As Trinity.cCombination = grdBudget.Rows(i).Tag
                For Each cc As Trinity.cCombinationChannel In combo.Relations
                    For Each week As Trinity.cWeek In cc.Bookingtype.Weeks
                        week.TRPControl = setValue
                    Next
                Next
            Else
                Dim bt As Trinity.cBookingType = grdBudget.Rows(i).Tag
                For Each week As Trinity.cWeek In bt.Weeks
                    week.TRPControl = setValue
                Next
            End If
        Next

        grdTRP.Invalidate()
        grdBudget.Invalidate()
    End Sub

    Private Sub TRPtoClipboard()
        Dim sb As New System.Text.StringBuilder

        sb.Append("These values where exported from Trinity using ")
        sb.Append(cmbUniverse.SelectedItem & ", ")
        sb.Append(cmbTargets.SelectedItem & ", ")
        sb.Append(cmbDisplay.SelectedItem & ", ")
        sb.Append(vbCrLf)


        sb.Append(ClipboardTRPHeaders())

        For i As Integer = 0 To grdTRP.RowCount - 1
            sb.Append(ClipboardTRPRows(i))
        Next

        sb.Append(ClipboardTRPSums())

        Clipboard.SetDataObject(sb.ToString(), True)
    End Sub

    Private Function ClipboardTRPHeaders() As String
        Dim tmpString = ""
        For i As Integer = 0 To grdTRP.ColumnCount - 1
            tmpString += vbTab & grdTRP.Columns(i).HeaderText
        Next
        Return "Channel" & vbTab & "Target" & tmpString & vbTab & "Summary" & vbCrLf & vbCrLf
    End Function

    Private Function ClipboardTRPRows(ByVal idx As Integer) As String
        Dim tmpString = ""
        For i As Integer = 0 To grdTRP.ColumnCount - 1
            tmpString += vbTab & grdTRP.Rows(idx).Cells(i).Value
        Next

        Dim channelTarget As String
        Dim sum As String

        If grdTRP.Rows(idx).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            'Combination
            Dim comb As Trinity.cCombination = grdTRP.Rows(idx).Tag
            channelTarget = comb.Name & vbTab + comb.Relations(1).Bookingtype.BuyingTarget.TargetName

            If idx Mod 2 = 0 Then
                channelTarget = comb.Name & vbTab + comb.Relations(1).Bookingtype.BuyingTarget.TargetName
                sum = vbTab & grdSumChannels.Rows(idx + 1).Cells(1).Value
            Else
                channelTarget = comb.Name & vbTab + Campaign.MainTarget.TargetName
                sum = vbTab & grdSumChannels.Rows(idx).Cells(0).Value
            End If
        Else
            Dim bt As Trinity.cBookingType = grdTRP.Rows(idx).Tag

            If idx Mod 2 = 0 Then
                channelTarget = bt.ParentChannel.Shortname & " " & bt.Shortname & vbTab + bt.BuyingTarget.TargetName
                sum = vbTab & grdSumChannels.Rows(idx + 1).Cells(1).Value
            Else
                channelTarget = bt.ParentChannel.Shortname & " " & bt.Shortname & vbTab + Campaign.MainTarget.TargetName
                sum = vbTab & grdSumChannels.Rows(idx).Cells(0).Value
            End If

        End If
        Return channelTarget & tmpString & sum & vbCrLf
    End Function

    Private Function ClipboardTRPSums() As String
        Dim tmpStringBuying = vbTab & Campaign.MainTarget.TargetName
        Dim tmpString3 = vbTab & "3+"

        For i As Integer = 0 To grdSumWeeks.ColumnCount - 1
            tmpStringBuying += vbTab & grdSumWeeks.Rows(0).Cells(i).Value
            tmpString3 += vbTab & grdSumWeeks.Rows(1).Cells(i).Value
        Next

        Return tmpStringBuying & vbTab & vbCrLf & tmpString3 & vbTab
    End Function

    Private Sub BudgettoClipboard()
        Dim sb As New System.Text.StringBuilder

        sb.Append("These values where exported from Trinity")
        sb.Append(vbCrLf)
        sb.Append(vbCrLf)

        'Headers
        sb.Append("Channel")

        For i As Integer = 0 To grdBudget.ColumnCount - 1
            sb.Append(vbTab)
            sb.Append(grdBudget.Columns(i).HeaderText)
        Next

        sb.Append(vbCrLf)

        'Rows
        For i As Integer = 0 To grdBudget.RowCount - 2
            If grdBudget.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                Dim comb As Trinity.cCombination = grdBudget.Rows(i).Tag

                sb.Append(comb.Name)
            Else
                Dim bt As Trinity.cBookingType = grdBudget.Rows(i).Tag

                sb.Append(bt.ParentChannel.Shortname & " " & bt.Shortname)
            End If

            For c As Integer = 0 To grdBudget.ColumnCount - 1
                sb.Append(vbTab)
                sb.Append(grdBudget.Rows(i).Cells(c).Value.ToString().Replace(" ", ""))
            Next
            sb.Append(vbCrLf)
        Next

        'total raden
        sb.Append("Total:")
        For c As Integer = 0 To grdBudget.ColumnCount - 1
            sb.Append(vbTab)
            sb.Append(grdBudget.Rows(grdBudget.RowCount - 1).Cells(c).Value.ToString().Replace(" ", ""))
        Next

        Clipboard.SetDataObject(sb.ToString(), True)

    End Sub

    Private Sub grdDiscounts_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdDiscounts.CellMouseClick
        Trinity.AllocateFunctions.CellMouseClick(sender, e, Campaign)
    End Sub

    Private Sub grdFilms_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdFilms.CellMouseClick
        If e.RowIndex < 0 Then Exit Sub
        If e.ColumnIndex < 0 Then Exit Sub

        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim mnu As New System.Windows.Forms.ContextMenuStrip

            Dim item As System.Windows.Forms.ToolStripItem = mnu.Items.Add("Copy to clipboard", Nothing, AddressOf FilmsToClipBoard)
            item.Tag = "CopyFilms"
            item.Name = "CopyFilms"

            mnu.Show(Me.ParentForm, New System.Drawing.Point(MousePosition.X, MousePosition.Y - 15))
        End If
    End Sub

    Private Sub FilmsToClipBoard()

        Dim SB As New System.Text.StringBuilder
        Dim CellFormat As String = "P1"

        SB.Append("Copied from Films for booking type " & cmbFilmChannel.SelectedItem.name & " in the campaign " & Campaign.Name & vbNewLine)

        SB.Append(vbTab)

        For Each Cell As Windows.Forms.DataGridViewCell In grdFilms.Rows(0).Cells
            SB.Append(Cell.OwningColumn.HeaderText & vbTab)
        Next

        SB.Append(vbNewLine)

        Dim Counter As Integer = 0

        For Each Row As Windows.Forms.DataGridViewRow In grdFilms.Rows

            SB.Append(Row.HeaderCell.Value & vbTab)

            For Each Cell As Windows.Forms.DataGridViewCell In Row.Cells
                If Not Cell.Value Is Nothing Then
                    SB.Append(Format(Cell.Value, CellFormat).Replace(" ", "") & vbTab)
                End If

            Next
            SB.Append(vbNewLine)

        Next

        Windows.Forms.Clipboard.SetDataObject(SB.ToString(), True)

    End Sub

    Private Sub grdIndex_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdIndex.CellMouseClick
        If e.RowIndex < 0 Then Exit Sub
        If e.ColumnIndex < 0 Then Exit Sub
        If e.Button <> Windows.Forms.MouseButtons.Right Then Exit Sub
        'If Not e.Button = Windows.Forms.MouseButtons.Left Then

        '    Dim mnu As New System.Windows.Forms.ContextMenuStrip

        '    ' Dim item As System.Windows.Forms.ToolStripItem = mnu.Items.Add("Copy to clipboard", Nothing, AddressOf IndexToClipBoard)
        '    'item.Tag = "CopyDiscounts"
        '    'item.Name = "CopyDiscounts"

        '    mnu.Show(Me.ParentForm, New System.Drawing.Point(MousePosition.X, MousePosition.Y - 15))

        'Else
        Dim TmpBT As Trinity.cBookingType = grdIndex.Rows(e.RowIndex).Tag

        Dim mnuLocked As New ContextMenuStrip
        Dim subMenuLocked As ToolStripMenuItem = mnuLocked.Items.Add("Lock indices of " & TmpBT.ParentChannel.ChannelName & " " & TmpBT.Name & " " & TmpBT.BuyingTarget.TargetName)
        subMenuLocked.Tag = "Lock one"
        Dim subMenuLockSelected As ToolStripMenuItem = mnuLocked.Items.Add("Lock all marked indices ")
        subMenuLockSelected.Tag = "Lock selected"
        subMenuLocked.Checked = TmpBT.ManualIndexes
        Dim item As System.Windows.Forms.ToolStripItem = mnuLocked.Items.Add("Copy to clipboard", Nothing, AddressOf IndexToClipBoard)
        item.Tag = "CopyDiscounts"
        item.Name = "CopyDiscounts"
        AddHandler subMenuLocked.Click, AddressOf grdIndex_Lockindex
        AddHandler subMenuLockSelected.Click, AddressOf grdIndex_Lockindex

        mnuLocked.Show(MousePosition.X, MousePosition.Y)

        'If e.Button = Windows.Forms.MouseButtons.Right Then
        '    Dim mnu As New System.Windows.Forms.ContextMenuStrip

        '    Dim item As System.Windows.Forms.ToolStripItem = mnu.Items.Add("Copy to clipboard", Nothing, AddressOf IndexToClipBoard)
        '    item.Tag = "CopyDiscounts"
        '    item.Name = "CopyDiscounts"

        '    mnu.Show(Me.ParentForm, New System.Drawing.Point(MousePosition.X, MousePosition.Y - 15))
        'End If
        'End If


    End Sub

    Private Sub IndexToClipBoard()

        Dim SB As New System.Text.StringBuilder
        Dim CellFormat As String = "N0"

        SB.Append("Copied from Indexes in the campaign " & Campaign.Name & vbNewLine)

        SB.Append(vbTab)

        For Each Cell As Windows.Forms.DataGridViewCell In grdIndex.Rows(0).Cells
            SB.Append(Cell.OwningColumn.HeaderText & vbTab)
        Next

        SB.Append(vbNewLine)

        Dim Counter As Integer = 0

        For Each Row As Windows.Forms.DataGridViewRow In grdIndex.Rows
            If Row.Tag.GetType.FullName = "clTrinity.Trinity.cBookingType" Then
                SB.Append(DirectCast(Row.Tag, Trinity.cBookingType).ParentChannel.Shortname & " " & DirectCast(Row.Tag, Trinity.cBookingType).Name & vbTab)
            Else
                SB.Append(DirectCast(Row.Tag, Trinity.cCombination).Name & " " & DirectCast(Row.Tag, Trinity.cCombination).Name & vbTab)
            End If

            For Each Cell As Windows.Forms.DataGridViewCell In Row.Cells
                If Not Cell.Value Is Nothing Then
                    SB.Append(Format(Cell.Value, CellFormat).Replace(" ", "") & vbTab)
                End If

            Next
            SB.Append(vbNewLine)

        Next

        Windows.Forms.Clipboard.SetDataObject(SB.ToString(), True)

    End Sub

    Private Sub frmAllocate_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'BLT.Destroy()
    End Sub


    Private Sub lblMarathonCTC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMarathonCTC.Click

    End Sub

    Private Sub grdFilms_Scroll(sender As Object, e As System.Windows.Forms.ScrollEventArgs) Handles grdFilms.Scroll

    End Sub

    Private Sub grdTRP_CellStyleContentChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellStyleContentChangedEventArgs) Handles grdTRP.CellStyleContentChanged

    End Sub

    ' If the index of the BugdetType Dropdown box changes, the budget window needs to be redrawn
    Private Sub cmdBudgetType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmdBudgetType.SelectedIndexChanged
        grdBudget.Invalidate()
    End Sub

    Private Sub grdSumWeeks_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSumWeeks.CellContentClick

    End Sub

    Private Sub grdBudget_SelectionChanged(sender As Object, e As EventArgs) Handles grdBudget.SelectionChanged
        If grdBudget.SelectedCells.Count = 1 Then
            lblSumBudget.Text = ""
            Exit Sub
        End If

        Dim sumOfSelected As Double = 0
        Dim sumOfPercentage As Double = 0
        For Each cell As DataGridViewCell In grdBudget.SelectedCells
            If cell.ColumnIndex > 1 Then
                sumOfSelected += CType(cell.Value, Double)
            Else
                sumOfPercentage +=  CType(cell.Value, Double)
            End If
        Next
        'lblSumBudget.Text = Math.Round(sumOfSelected)
        lblPercentage.Text = FormatPercent(sumOfPercentage)
        lblPercentage.Visible = True
        lblSumBudget.Text = Format(sumOfSelected, "C0")
    End Sub

    Private Sub grdTRP_SelectionChanged(sender As Object, e As EventArgs) Handles grdTRP.SelectionChanged
        If grdTRP.SelectedCells.Count = 1 Then
            lblSumTRP.Text = ""
            Exit Sub
        End If

        Dim sumOfSelected As Double = 0
        For Each cell As DataGridViewCell In grdTRP.SelectedCells
            sumOfSelected += CType(cell.Value, Double)
        Next
        'lblSumBudget.Text = Math.Round(sumOfSelected)
        lblSumTRP.Text = Math.Round(sumOfSelected, 1)
    End Sub

    Private Sub grdSumChannels_SelectionChanged(sender As Object, e As EventArgs) Handles grdSumChannels.SelectionChanged
        If grdTRP.SelectedCells.Count = 1 Then
            lblSumTRP.Text = ""
            Exit Sub
        End If

        Dim sumOfSelected As Double = 0

        For Each cell As DataGridViewCell In grdSumChannels.SelectedCells
            sumOfSelected += CType(cell.Value, Double)
        Next

        'lblSumBudget.Text = Math.Round(sumOfSelected)
        lblSumTRP.Text = Math.Round(sumOfSelected, 1)
    End Sub

    Private Sub grdGrandSum_SelectionChanged(sender As Object, e As EventArgs) Handles grdGrandSum.SelectionChanged
        If grdTRP.SelectedCells.Count = 1 Then
            lblSumTRP.Text = ""
            Exit Sub
        End If

        Dim sumOfSelected As Double = 0
        For Each cell As DataGridViewCell In grdTRP.SelectedCells
            sumOfSelected += CType(cell.Value, Double)
        Next
        'lblSumBudget.Text = Math.Round(sumOfSelected)
        lblSumTRP.Text = Math.Round(sumOfSelected, 1)
    End Sub
End Class


' Budget type to that you can chose to work with (Gross or net)
Public Class BudgetType

    Private _name As String

    Public Shared GrossBudget = New BudgetType("Gross Budget")
    Public Shared NetBudget = New BudgetType("Net Budget")

    Public ReadOnly Property Name
        Get
            Return Me._name
        End Get
    End Property

    Private Sub New(ByVal name As String)
        Me._name = name
    End Sub
    Public Overrides Function ToString() As String
        Return Me.Name
    End Function

End Class