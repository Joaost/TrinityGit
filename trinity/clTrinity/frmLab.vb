Public Class frmLab
    Private ActiveCampaign As Trinity.cKampanj
    Private WithEvents Karma As Trinity.cKarma

    'Dim BLT As New Trinity.cBalloonToolTip

    Private SkipIt As Boolean = False

    'regular black text on white backgroud, D has one decimal, P is percent
    Dim styleNormalD As Windows.Forms.DataGridViewCellStyle
    Dim styleNormal As Windows.Forms.DataGridViewCellStyle
    Dim styleNormalDLocked As Windows.Forms.DataGridViewCellStyle
    Dim styleNormalLocked As Windows.Forms.DataGridViewCellStyle


    Dim styleNoSet As Windows.Forms.DataGridViewCellStyle
    Dim styleNoSetD As Windows.Forms.DataGridViewCellStyle
    Dim styleNoSetLocked As Windows.Forms.DataGridViewCellStyle
    Dim styleNoSetDLocked As Windows.Forms.DataGridViewCellStyle

    'greyed out since it cant be set
    Dim styleCantSetN As Windows.Forms.DataGridViewCellStyle
    Dim styleCantSetN0 As Windows.Forms.DataGridViewCellStyle
    Dim styleCantSetP As Windows.Forms.DataGridViewCellStyle


    'blue text on white
    Dim styleBlue As Windows.Forms.DataGridViewCellStyle

    'black text on red background
    Dim styleBackRed As Windows.Forms.DataGridViewCellStyle

    Dim BudgetLoading(,) As Single = Nothing
    Dim TRPLoading(,) As Single = Nothing

    Sub GetLoading()
        If BudgetLoading Is Nothing Then
            ReDim BudgetLoading(grdLoading.Rows.Count - 1, grdLoading.Columns.Count - 1)
            For r As Integer = 0 To grdBudget.Rows.Count - 1
                For c As Integer = 0 To grdBudget.Columns.Count - 3
                    If grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(grdBudget.Columns.Count - 1).Value = 0 Then
                        BudgetLoading(r, c) = 0
                    Else
                        If c = grdBudget.ColumnCount - 3 Then
                            BudgetLoading(r, c) = Math.Round(grdBudget.Rows(r).Cells(c + 2).Value / grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(grdBudget.Columns.Count - 1).Value, 4)
                        Else
                            BudgetLoading(r, c) = Math.Round(grdBudget.Rows(r).Cells(c + 2).Value / grdBudget.Rows(r).Cells(grdBudget.Columns.Count - 1).Value, 4)
                        End If
                    End If
                Next
            Next
            ReDim TRPLoading(grdLoading.Rows.Count - 1, grdLoading.Columns.Count - 1)
            For r As Integer = 1 To grdTRP.Rows.Count - 1 Step 2
                For c As Integer = 0 To grdTRP.Columns.Count - 1
                    If grdGrandSum.Rows(0).Cells(0).Value = 0 Then
                        TRPLoading(r \ 2, c) = 0
                    Else
                        TRPLoading(r \ 2, c) = Math.Round(grdTRP.Rows(r).Cells(c).Value / grdSumChannels.Rows(r).Cells(0).Value, 4)
                    End If
                Next
            Next
            For c As Integer = 0 To grdSumWeeks.Columns.Count - 1
                If grdGrandSum.Rows(0).Cells(0).Value = 0 Then
                    TRPLoading(grdTRP.Rows.Count \ 2, c) = 0
                Else
                    TRPLoading(grdTRP.Rows.Count \ 2, c) = Math.Round(grdSumWeeks.Rows(0).Cells(c).Value / grdGrandSum.Rows(0).Cells(0).Value, 4)
                End If
            Next
            For r As Integer = 1 To grdSumChannels.Rows.Count - 1 Step 2
                If grdGrandSum.Rows(0).Cells(0).Value = 0 Then
                    TRPLoading(r \ 2, grdTRP.Columns.Count) = 0
                Else
                    TRPLoading(r \ 2, grdTRP.Columns.Count) = Math.Round(grdSumChannels.Rows(r).Cells(0).Value / grdGrandSum.Rows(0).Cells(0).Value, 4)
                End If
            Next
            TRPLoading(grdTRP.Rows.Count \ 2, grdTRP.Columns.Count) = 1
        End If
    End Sub

    Private Sub frmLab_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If Karma Is Nothing Then
            tpProfile.Enabled = False
        End If
        If cmbTRPBudget.SelectedIndex = -1 Then
            cmbTRPBudget.SelectedIndex = 0
            cmbFF.SelectedIndex = 0
        End If
    End Sub

    Private Sub frmLab_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        'BLT.Destroy()
    End Sub

    Private Sub frmLab_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim TmpCampaign As Trinity.cKampanj

        If Campaign.Campaigns Is Nothing OrElse Campaign.Campaigns.Count = 0 Then
            Campaign.Campaigns = New Dictionary(Of String, Trinity.cKampanj)
            TmpCampaign = New Trinity.cKampanj(False)
            TmpCampaign.LoadCampaign("", True, Campaign.SaveCampaign(, True))
            Campaign.Campaigns.Add("Campaign 1", TmpCampaign)
        End If

        cmbCampaigns.Items.Clear()
        For Each kv As KeyValuePair(Of String, Trinity.cKampanj) In Campaign.Campaigns
            cmbCampaigns.Items.Add(kv.Key)
            cmbProfileCampaign.Items.Add(kv.Key)
        Next
        cmbCampaigns.SelectedIndex = 0
        cmbProfileCampaign.SelectedIndex = 0
        onlyOneSelectedGrid()
        cmbLoading.SelectedIndex = 0
        cmbChannelLoading.SelectedIndex = 0

    End Sub

    Private Sub picCollapseDiscounts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCollapseDiscounts.Click
        grpDiscounts.Height = 17
        picExpandDiscounts.Visible = True
        picCollapseDiscounts.Visible = False
        grpLoading.Top = grpDiscounts.Bottom + 6
        grpBudget.Top = grpLoading.Bottom + 6
        ' BLT.Destroy()
    End Sub


    Private Sub picExpandDiscounts_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picExpandDiscounts.Click
        grpDiscounts.Height = 10000
        grpDiscounts.Height = grdDiscounts.Bottom + 5
        picExpandDiscounts.Visible = False
        picCollapseDiscounts.Visible = True
        grpLoading.Top = grpDiscounts.Bottom + 6
        grpBudget.Top = grpLoading.Bottom + 6

        'For Each tmpChan As Trinity.cChannel In ActiveCampaign.Channels
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

    Private Sub picCollapseLoading_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCollapseLoading.Click
        grpLoading.Height = 17
        picExpandLoading.Visible = True
        picCollapseLoading.Visible = False
        grpLoading.Top = grpDiscounts.Bottom + 6
        grpBudget.Top = grpLoading.Bottom + 6
    End Sub

    Private Sub picExpandLoading_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picExpandLoading.Click
        grpLoading.Height = 10000
        grpLoading.Height = cmdApplyLoading.Bottom + 5
        picExpandLoading.Visible = False
        picCollapseLoading.Visible = True
        grpLoading.Top = grpDiscounts.Bottom + 6
        grpBudget.Top = grpLoading.Bottom + 6
    End Sub

    Private Sub picExpandAV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picExpandAV.Click
        grpAV.Height = 10000
        grpAV.Height = grdAV.Bottom + 5
        picExpandAV.Visible = False
        picCollapseAV.Visible = True
        grpDiscounts.Top = grpAV.Bottom + 6
        grpLoading.Top = grpDiscounts.Bottom + 6
        grpBudget.Top = grpLoading.Bottom + 6
    End Sub

    Private Sub picCollapseAV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picCollapseAV.Click
        grpAV.Height = 17
        picExpandAV.Visible = True
        picCollapseAV.Visible = False
        grpDiscounts.Top = grpAV.Bottom + 6
        grpLoading.Top = grpDiscounts.Bottom + 6
        grpBudget.Top = grpLoading.Bottom + 6
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
            grdFilms.ReadOnly = False
            grdFilms.ForeColor = Color.Black
        End If

        'clear the film grid
        grdFilms.Rows.Clear()
        SkipIt = True

        If cmbFilmChannel.SelectedItem.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            tmpc = cmbFilmChannel.SelectedItem
            TmpBT = tmpc.Relations(1).Bookingtype

            If TmpBT Is Nothing Then
                For Each TmpFilm In ActiveCampaign.Channels(1).BookingTypes(1).Weeks(1).Films
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
                For Each TmpFilm In ActiveCampaign.Channels(1).BookingTypes(1).Weeks(1).Films
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
        grdFilms.Width = grdFilms.GetColumnDisplayRectangle(grdFilms.Columns.Count - 1, False).Right + 12 + pnlChoose.Width
        pnlChoose.Left = grdFilms.Right + 6
        grdFilms.Height = 10000
        grdFilms.Height = grdFilms.GetRowDisplayRectangle(grdFilms.Rows.Count - 1, False).Bottom + 1
        grpFilms.Height = grdFilms.Bottom + 6
        grpFilms.Width = pnlChoose.Right + 6
        grpIndex.Top = grpFilms.Bottom + 6
        grpFindReach.Top = grpIndex.Bottom + 6
        grpCompensation.Top = grpFindReach.Bottom + 6
        grpCompensation.Left = grpFindReach.Left
        ColorFilmGrid()
    End Sub

    Sub ColorFilmGrid()
        Dim i As Integer
        Dim j As Integer
        Dim Tot As Single
        For i = 0 To grdFilms.Columns.Count - 1
            Tot = 0
            For j = 0 To grdFilms.Rows.Count - 1
                Tot = Tot + grdFilms.Rows(j).Cells(i).Value
            Next
            If Tot <> 1 Then
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

    'Private Sub grdFilms_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdFilms.CellValueChanged
    '    Dim Chan As String
    '    Dim BT As String
    '    Dim r As Integer

    '    If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
    '    Chan = Microsoft.VisualBasic.Left(cmbFilmChannel.Text, InStr(cmbFilmChannel.Text, " ") - 1)
    '    BT = Mid(cmbFilmChannel.Text, InStr(cmbFilmChannel.Text, " ") + 1)
    '    If Not SkipIt Then
    '        ActiveCampaign.Channels(Chan).BookingTypes(BT).Weeks(e.ColumnIndex + 1).Films(e.RowIndex + 1).Share = grdFilms.Rows(e.RowIndex).Cells(e.ColumnIndex).Value * 100
    '    End If
    '    If Format(grdFilms.Rows(e.RowIndex).Cells(e.ColumnIndex).Value, "P0") <> grdFilms.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue Then
    '        grdFilms.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = CSng(grdFilms.Rows(e.RowIndex).Cells(e.ColumnIndex).Value / 100)
    '        grdFilms.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "P0"
    '    End If
    '    ColorFilmGrid()
    '    UpdateTRPGrid()
    '    SumBudget()
    '    For r = cmbFilmChannel.SelectedIndex * 2 + 1 To cmbFilmChannel.SelectedIndex * 2 + 2
    '        'UpdateTRPCell(grdFilms.col + 1, r)
    '    Next
    '    SumChannelTRP(cmbFilmChannel.SelectedIndex * 2 + 1)
    '    SumWeekTRP(e.ColumnIndex + 1)
    'End Sub

    'Sub UpdateTRPGrid()
    '    Dim i As Integer
    '    Dim j As Integer
    '    Dim TmpChan As Trinity.cChannel
    '    Dim TmpBT As Trinity.cBookingType
    '    Dim TmpWeek As Trinity.cWeek
    '    Dim SumTarget As Single

    '    If ActiveCampaign.RootCampaign Is Nothing And cmbDisplay.SelectedIndex = 0 Then
    '        grdTRP.ForeColor = Drawing.Color.Black
    '        grdSumWeeks.ForeColor = Drawing.Color.Black
    '        grdSumChannels.ForeColor = Drawing.Color.Black
    '        'grdGrandSum.ForeColor = Drawing.Color.Black
    '        grdBudget.ForeColor = Drawing.Color.Black
    '        grdFilms.ForeColor = Drawing.Color.Black
    '        grdDiscounts.ForeColor = Drawing.Color.Black
    '        grdAV.ForeColor = Drawing.Color.Black
    '        grdDiscounts.ForeColor = Drawing.Color.Black
    '    Else
    '        grdTRP.ForeColor = Drawing.Color.LightGray
    '        If ActiveCampaign.RootCampaign Is Nothing Then
    '            grdSumWeeks.ForeColor = Drawing.Color.Black
    '            grdSumChannels.ForeColor = Drawing.Color.Black
    '            'grdGrandSum.ForeColor = Drawing.Color.Black
    '            grdBudget.ForeColor = Drawing.Color.Black
    '            grdFilms.ForeColor = Drawing.Color.Black
    '            grdDiscounts.ForeColor = Drawing.Color.Black
    '            grdAV.ForeColor = Drawing.Color.Black
    '            grdDiscounts.ForeColor = Drawing.Color.Black
    '        Else
    '            grdSumWeeks.ForeColor = Drawing.Color.LightGray
    '            grdSumChannels.ForeColor = Drawing.Color.LightGray
    '            'grdGrandSum.ForeColor = Drawing.Color.LightGray
    '            grdBudget.ForeColor = Drawing.Color.LightGray
    '            grdFilms.ForeColor = Drawing.Color.LightGray
    '            grdDiscounts.ForeColor = Drawing.Color.LightGray
    '            grdAV.ForeColor = Drawing.Color.LightGray
    '            grdDiscounts.ForeColor = Drawing.Color.LightGray
    '        End If
    '    End If
    '    For i = 0 To grdTRP.Rows.Count - 1 Step 2
    '        TmpBT = grdTRP.Rows(i).Tag
    '        For j = 0 To grdTRP.Columns.Count - 1
    '            TmpWeek = grdTRP.Rows(i).Cells(j).Tag
    '            If cmbDisplay.SelectedIndex = 0 Then
    '                grdTRP.Rows(i).Cells(j).Style.Format = "N1"
    '                grdTRP.Rows(i + 1).Cells(j).Style.Format = "N1"
    '                If cmbUniverse.SelectedIndex = 0 Then
    '                    If (TmpBT.IndexMainTarget / 100) > 0 Then
    '                        SkipIt = True
    '                        grdTRP.Rows(i).Cells(j).Value = TmpWeek.TRP / (TmpBT.IndexMainTarget / 100)
    '                    Else
    '                        SkipIt = True
    '                        grdTRP.Rows(i).Cells(j).Value = 0
    '                    End If
    '                    SkipIt = True
    '                    grdTRP.Rows(i + 1).Cells(j).Value = TmpWeek.TRP
    '                Else
    '                    SkipIt = True
    '                    grdTRP.Rows(i).Cells(j).Value = TmpWeek.TRPBuyingTarget
    '                    SkipIt = True
    '                    grdTRP.Rows(i + 1).Cells(j).Value = TmpWeek.TRPBuyingTarget * (TmpBT.IndexMainTarget / 100)
    '                End If
    '                grdTRP.Rows(i).Cells(j).ReadOnly = False
    '                grdTRP.Rows(i + 1).Cells(j).ReadOnly = False
    '            ElseIf cmbDisplay.SelectedIndex = 2 Then
    '                grdTRP.Rows(i).Cells(j).Style.Format = "P1"
    '                grdTRP.Rows(i + 1).Cells(j).Style.Format = "P1"
    '                If TmpBT.TotalTRP <> 0 Then
    '                    SkipIt = True
    '                    grdTRP.Rows(i).Cells(j).Value = TmpWeek.TRPBuyingTarget / TmpBT.TotalTRPBuyingTarget
    '                    SkipIt = True
    '                    grdTRP.Rows(i + 1).Cells(j).Value = TmpWeek.TRP / TmpBT.TotalTRP
    '                Else
    '                    SkipIt = True
    '                    grdTRP.Rows(i).Cells(j).Value = 0
    '                    SkipIt = True
    '                    grdTRP.Rows(i + 1).Cells(j).Value = 0
    '                End If
    '                grdTRP.Rows(i).Cells(j).ReadOnly = True
    '                grdTRP.Rows(i + 1).Cells(j).ReadOnly = True
    '            Else
    '                grdTRP.Rows(i).Cells(j).Style.Format = "P1"
    '                grdTRP.Rows(i + 1).Cells(j).Style.Format = "P1"
    '                SumTarget = 0
    '                For Each TmpChan In ActiveCampaign.Channels
    '                    For Each TmpBT In TmpChan.BookingTypes
    '                        If TmpBT.BookIt Then
    '                            SumTarget = SumTarget + TmpBT.Weeks(TmpWeek.Name).TRP
    '                        End If
    '                    Next
    '                Next
    '                TmpBT = grdTRP.Rows(i).Tag
    '                If SumTarget <> 0 Then
    '                    SkipIt = True
    '                    grdTRP.Rows(i + 1).Cells(j).Value = TmpWeek.TRP / SumTarget
    '                Else
    '                    SkipIt = True
    '                    grdTRP.Rows(i + 1).Cells(j).Value = 0
    '                End If
    '                grdTRP.Rows(i).Cells(j).Value = "-"
    '                grdTRP.Rows(i).Cells(j).ReadOnly = True
    '                grdTRP.Rows(i + 1).Cells(j).ReadOnly = True
    '            End If
    '        Next
    '    Next
    'End Sub

    Private Sub cmbUniverse_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUniverse.SelectedIndexChanged
        SkipIt = True
        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
        grdBudget.Invalidate()
        grdSumChannels.Invalidate()
        SkipIt = False
    End Sub

    Private Sub cmbDisplay_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDisplay.SelectedIndexChanged
        SkipIt = True
        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
        grdBudget.Invalidate()
        grdSumChannels.Invalidate()
        SkipIt = False
    End Sub

    'Private Sub grdTRP_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTRP.CellValueChanged
    '    Dim TmpBT As Trinity.cBookingType
    '    Dim TmpWeek As Trinity.cWeek
    '    If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
    '    If grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString = "-" Then Exit Sub
    '    If grdTRP.ForeColor = Drawing.Color.LightGray Then Exit Sub
    '    If Not SkipIt Then
    '        TmpBT = grdTRP.Rows(e.RowIndex).Tag
    '        If cmbUniverse.SelectedIndex = 0 Then
    '            If e.RowIndex / 2 <> e.RowIndex \ 2 Then
    '                TmpWeek = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
    '                TmpWeek.TRP = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
    '                SkipIt = True
    '                grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex).Value = TmpWeek.TRP / (TmpBT.IndexMainTarget / 100)
    '                SkipIt = False
    '            Else
    '                TmpWeek = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
    '                TmpWeek.TRP = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Value * (TmpBT.IndexMainTarget / 100)
    '                SkipIt = True
    '                grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex).Value = TmpWeek.TRP
    '                SkipIt = False
    '            End If
    '        Else
    '            If e.RowIndex / 2 <> e.RowIndex \ 2 Then
    '                TmpWeek = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
    '                TmpWeek.TRPBuyingTarget = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Value / (TmpBT.IndexMainTarget / 100)
    '                SkipIt = True
    '                grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex).Value = TmpWeek.TRPBuyingTarget
    '                SkipIt = False
    '            Else
    '                TmpWeek = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
    '                TmpWeek.TRPBuyingTarget = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
    '                SkipIt = True
    '                grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex).Value = TmpWeek.TRPBuyingTarget * (TmpBT.IndexMainTarget / 100)
    '                SkipIt = False
    '            End If
    '        End If
    '        TmpWeek.TRPControl = True
    '        If Format(CSng(grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Value), grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format) <> grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue Then
    '            grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = CSng(grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
    '            grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format
    '        End If
    '    End If
    '    SumWeekTRP(e.ColumnIndex + 1)
    '    SumChannelTRP(((e.RowIndex) \ 2) * 2)
    '    SumBudget()
    'End Sub


    '    Sub SumChannelTRP(ByVal Row As Integer, Optional ByVal col As Integer = -1, Optional ByVal IncludeGrand As Boolean = True)
    '        Dim Chan As String
    '        Dim BT As String
    '        Dim TmpBT As Trinity.cBookingType
    '        Dim TmpWeek As Trinity.cWeek
    '        Dim SumNat As Single
    '        Dim SumChn As Single
    '        Dim SumSumNat As Single
    '        Dim x As Integer

    '        Dim a As Integer

    '        On Error GoTo SumChannelTRP_Error

    '        TmpBT = grdTRP.Rows(Row).Tag

    '        For Each TmpWeek In TmpBT.Weeks
    '            SumNat = SumNat + TmpWeek.TRP
    '            SumChn = SumChn + TmpWeek.TRPBuyingTarget
    '        Next
    '        For x = 0 To grdTRP.Rows.Count - 1 Step 2
    '            TmpBT = grdTRP.Rows(x).Tag
    '            For Each TmpWeek In TmpBT.Weeks
    '                SumSumNat = SumSumNat + TmpWeek.TRP
    '            Next
    '        Next

    '        SkipIt = True
    '        If Row / 2 <> Row \ 2 Then
    '            Row = Row - 1
    '        End If
    '        If col = 0 Or col = -1 Then
    '            grdSumChannels.Rows(Row + 1).Cells(0).Value = SumNat
    '        End If
    '        If col = 1 Or col = -1 Then
    '            grdSumChannels.Rows(Row + 1).Cells(1).Value = SumChn
    '        End If
    '        If IncludeGrand Then
    '            grdGrandSum.Rows(0).Cells(0).Value = SumSumNat
    '        End If
    '        SkipIt = False

    '        On Error GoTo 0
    '        Exit Sub

    'SumChannelTRP_Error:

    '        If IsIDE() Then
    '            a = MsgBox("Error:" & Chr(10) & Chr(10) & "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & Chr(10) & Chr(10) & "Vill du utföra en felsökning?", vbYesNo, "TRINITY")
    '            If a = vbNo Then Exit Sub
    '            Stop
    '            Resume Next
    '        End If
    '        MsgBox("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & " in SumChannelTRP.", vbCritical, "Error")


    '    End Sub


    'Sub SumWeekTRP(ByVal week, Optional ByVal Row = -1)
    '    Dim TmpChan As Trinity.cChannel
    '    Dim TmpBT As Trinity.cBookingType
    '    Dim SumTarget As Single
    '    Dim SumAllAdults As Single

    '    For Each TmpChan In ActiveCampaign.Channels
    '        For Each TmpBT In TmpChan.BookingTypes
    '            If TmpBT.BookIt Then
    '                SumTarget = SumTarget + TmpBT.Weeks(week).TRP
    '                SumAllAdults = SumAllAdults + TmpBT.Weeks(week).TRPAllAdults
    '            End If
    '        Next
    '    Next
    '    If Row = 0 Or Row = -1 Then
    '        grdSumWeeks.Rows(0).Cells(week - 1).value = SumTarget
    '    End If
    '    If Row = 1 Or Row = -1 Then
    '        grdSumWeeks.Rows(1).Cells(week - 1).value = SumAllAdults
    '    End If
    'End Sub




    '    Sub SumBudget(Optional ByVal ExcludeRow As Integer = -1, Optional ByVal ExcludeWeek As Integer = -1)
    '        Dim ColSum() As Single
    '        Dim c As Integer
    '        Dim r As Integer
    '        Dim Chan As String
    '        Dim BT As String
    '        Dim TmpBT As Trinity.cBookingType
    '        Dim SumRow As Single
    '        Dim SumTotal As Single
    '        Dim TmpWeek As Trinity.cWeek

    '        Dim a As Integer

    '        On Error GoTo SumBudget_Error

    '        ReDim ColSum(ActiveCampaign.Channels(1).BookingTypes(1).Weeks.Count)
    '        For r = 0 To grdBudget.Rows.Count - 2
    '            SumRow = 0
    '            For c = 2 To grdBudget.Columns.Count - 2
    '                TmpBT = grdBudget.Rows(r).Tag
    '                TmpWeek = grdBudget.Rows(r).Cells(c).Tag
    '                If Not (ExcludeRow = r And ExcludeWeek = c - 2) Then
    '                    SkipIt = True
    '                    grdBudget.Rows(r).Cells(c).Value = TmpWeek.NetBudget
    '                End If
    '                SumRow = SumRow + TmpWeek.NetBudget
    '                ColSum(c - 2) = ColSum(c - 2) + TmpWeek.NetBudget
    '                SumTotal = SumTotal + TmpWeek.NetBudget
    '            Next
    '            SkipIt = True
    '            grdBudget.Rows(r).Cells(grdBudget.Columns.Count - 1).Value = SumRow
    '        Next

    '        For c = 2 To grdBudget.Columns.Count - 2
    '            SkipIt = True
    '            grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(c).Value = ColSum(c - 2)
    '        Next
    '        SkipIt = True
    '        grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(grdBudget.Columns.Count - 1).Value = SumTotal

    '        For r = 0 To grdBudget.Rows.Count - 2
    '            If grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(grdBudget.Columns.Count - 1).Value <> 0 Then
    '                SkipIt = True
    '                grdBudget.Rows(r).Cells(1).Value = grdBudget.Rows(r).Cells(grdBudget.Columns.Count - 1).Value / grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(grdBudget.Columns.Count - 1).Value
    '            Else
    '                SkipIt = True
    '                grdBudget.Rows(r).Cells(1).Value = 0
    '            End If
    '        Next
    '        SumTotal = 0
    '        For r = 0 To grdSumChannels.Rows.Count - 1 Step 2
    '            SumTotal = SumTotal + grdSumChannels.Rows(r + 1).Cells(0).Value
    '        Next
    '        For r = 0 To grdSumChannels.Rows.Count - 1 Step 2
    '            If SumTotal = 0 Then
    '                SkipIt = True
    '                grdBudget.Rows(r / 2).Cells(0).Value = 0
    '            Else
    '                SkipIt = True
    '                grdBudget.Rows(r / 2).Cells(0).Value = grdSumChannels.Rows(r + 1).Cells(0).Value / SumTotal
    '            End If
    '        Next
    '        grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(0).Value = 1
    '        grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(1).Value = 1
    '        lblCTC.Text = Format(ActiveCampaign.PlannedTotCTC, "C0")
    '        lblCTC.Left = grdBudget.Right - lblCTC.Width
    '        lblCTCLabel.Left = lblCTC.Left - lblCTCLabel.Width
    '        If ActiveCampaign.PlannedTotCTC > Campaign.BudgetTotalCTC Then
    '            lblCTC.ForeColor = Drawing.Color.Red
    '        Else
    '            lblCTC.ForeColor = Drawing.Color.Green
    '        End If

    '        'UpdateForms(Me)
    '        SkipIt = False
    '        On Error GoTo 0
    '        Exit Sub

    'SumBudget_Error:

    '        If IsIDE() Then
    '            a = MsgBox("Error:" & Chr(10) & Chr(10) & "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & Chr(10) & Chr(10) & "Vill du utföra en felsökning?", vbYesNo, "TRINITY")
    '            If a = vbNo Then Exit Sub
    '            Stop
    '            Resume Next
    '        End If
    '        MsgBox("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & " in SumBudget.", vbCritical, "Error")

    '    End Sub

    'Sub UpdateDiscountsGrid()
    '    Dim TmpWeek As Trinity.cWeek
    '    Dim i As Integer
    '    Dim j As Integer

    '    For i = 0 To grdDiscounts.Rows.Count - 1 Step 3
    '        For j = 0 To grdDiscounts.Columns.Count - 1
    '            TmpWeek = DirectCast(grdDiscounts.Rows(i).Cells(j).Tag, Trinity.cWeek)
    '            grdDiscounts.Rows(i).Cells(j).Value = TmpWeek.Discount(True)
    '            grdDiscounts.Rows(i).Cells(j).Style.Format = "P1"
    '            grdDiscounts.Rows(i).Cells(j).Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    '            grdDiscounts.Rows(i).Cells(j).Style.ForeColor = Drawing.Color.Blue
    '            grdDiscounts.Rows(i + 1).Cells(j).Value = TmpWeek.NetCPP30()
    '            grdDiscounts.Rows(i + 1).Cells(j).Style.Format = "N1"
    '            grdDiscounts.Rows(i + 1).Cells(j).Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    '            grdDiscounts.Rows(i + 1).Cells(j).Style.ForeColor = Drawing.Color.Blue
    '            grdDiscounts.Rows(i + 2).Cells(j).Value = TmpWeek.Index() * 100
    '            grdDiscounts.Rows(i + 2).Cells(j).Style.Format = "N0"
    '            grdDiscounts.Rows(i + 2).Cells(j).Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    '            grdDiscounts.Rows(i + 2).Cells(j).Style.ForeColor = Drawing.Color.Blue
    '        Next
    '    Next
    'End Sub

    'Private Sub grdBudget_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdBudget.CellValueChanged
    '    Dim TmpBT As Trinity.cBookingType
    '    Dim TmpWeek As Trinity.cWeek
    '    Dim BudgetSum As Single
    '    Dim r As Integer
    '    Dim c As Integer

    '    If e.RowIndex < 0 Or e.ColumnIndex < 2 Then Exit Sub
    '    If grdBudget.ForeColor = Drawing.Color.LightGray Then Exit Sub
    '    If e.RowIndex < grdBudget.Rows.Count - 1 AndAlso e.ColumnIndex < grdBudget.Columns.Count - 1 Then
    '        If Not SkipIt Then
    '            TmpBT = grdBudget.Rows(e.RowIndex).Tag
    '            TmpWeek = grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
    '            TmpWeek.NetBudget = grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
    '            TmpWeek.TRPControl = False
    '            If Format(CSng(grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Value), grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format) <> grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue Then
    '                grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = CSng(grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
    '                grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format
    '            End If
    '            SumWeekTRP(e.ColumnIndex - 1)
    '            SkipIt = True
    '            SumBudget(e.RowIndex, e.ColumnIndex - 1)
    '            SkipIt = True
    '            UpdateTRPGrid()
    '        End If
    '    ElseIf e.ColumnIndex = grdBudget.Columns.Count - 1 Then
    '        If Not SkipIt Then
    '            If e.RowIndex < grdBudget.Rows.Count - 1 Then
    '                BudgetSum = 0
    '                For c = 2 To grdBudget.Columns.Count - 2
    '                    BudgetSum = BudgetSum + grdBudget.Rows(e.RowIndex).Cells(c).Value
    '                Next
    '                For c = 2 To grdBudget.Columns.Count - 2
    '                    TmpWeek = grdBudget.Rows(e.RowIndex).Cells(c).Tag
    '                    TmpWeek.NetBudget = grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Value * (grdBudget.Rows(e.RowIndex).Cells(c).Value / BudgetSum)
    '                    TmpWeek.TRPControl = False
    '                Next
    '                SkipIt = True
    '                SumBudget(e.RowIndex, e.ColumnIndex - 1)
    '                SkipIt = True
    '                UpdateTRPGrid()
    '            Else
    '                BudgetSum = 0
    '                For c = 2 To grdBudget.Columns.Count - 2
    '                    For r = 0 To grdBudget.Rows.Count - 2
    '                        BudgetSum = BudgetSum + grdBudget.Rows(r).Cells(c).Value
    '                    Next
    '                Next
    '                For c = 2 To grdBudget.Columns.Count - 2
    '                    For r = 0 To grdBudget.Rows.Count - 2
    '                        TmpWeek = grdBudget.Rows(r).Cells(c).Tag
    '                        TmpWeek.NetBudget = grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Value * (grdBudget.Rows(r).Cells(c).Value / BudgetSum)
    '                        TmpWeek.TRPControl = False
    '                    Next
    '                Next
    '                SkipIt = True
    '                SumBudget(e.RowIndex, e.ColumnIndex - 1)
    '                SkipIt = True
    '                UpdateTRPGrid()
    '            End If
    '        End If
    '    ElseIf e.RowIndex = grdBudget.Rows.Count - 1 Then
    '        If Not SkipIt Then
    '            BudgetSum = 0
    '            For r = 0 To grdBudget.Rows.Count - 2
    '                BudgetSum = BudgetSum + grdBudget.Rows(r).Cells(e.ColumnIndex).Value
    '            Next
    '            For r = 0 To grdBudget.Rows.Count - 2
    '                TmpWeek = grdBudget.Rows(r).Cells(e.ColumnIndex).Tag
    '                TmpWeek.NetBudget = grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Value * (grdBudget.Rows(r).Cells(e.ColumnIndex).Value / BudgetSum)
    '                TmpWeek.TRPControl = False
    '            Next
    '            SkipIt = True
    '            SumBudget(e.RowIndex, e.ColumnIndex - 1)
    '            SkipIt = True
    '            UpdateTRPGrid()
    '        End If
    '    End If
    '    SkipIt = False
    'End Sub

    'Sub UpdateIndexGrid()
    '    Dim i As Integer
    '    Dim TmpBT As Trinity.cBookingType

    '    For i = 0 To grdIndex.Rows.Count - 1
    '        TmpBT = DirectCast(grdIndex.Rows(i).Tag, Trinity.cBookingType)
    '        SkipIt = True
    '        grdIndex.Rows(i).Cells(0).Value = TmpBT.IndexMainTarget
    '        SkipIt = True
    '        grdIndex.Rows(i).Cells(1).Value = TmpBT.IndexSecondTarget
    '        SkipIt = True
    '        grdIndex.Rows(i).Cells(2).Value = TmpBT.IndexAllAdults
    '        SkipIt = False
    '    Next
    'End Sub

    'Private Sub grdIndex_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdIndex.CellValueChanged
    '    Dim TmpBT As Trinity.cBookingType
    '    If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
    '    If grdIndex.ForeColor = Drawing.Color.LightGray Then Exit Sub
    '    TmpBT = grdIndex.Rows(e.RowIndex).Tag
    '    If e.ColumnIndex = 0 Then
    '        TmpBT.IndexMainTarget = grdIndex.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
    '    ElseIf e.ColumnIndex = 1 Then
    '        TmpBT.IndexSecondTarget = grdIndex.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
    '    ElseIf e.ColumnIndex = 2 Then
    '        TmpBT.IndexAllAdults = grdIndex.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
    '    End If
    '    SumBudget()
    '    UpdateTRPGrid()
    'End Sub

    Private Sub cmdNaturalDelivery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNaturalDelivery.Click
        Dim EstimationPeriod As Trinity.AllocateFunctions.EstimationPeriodEnum
        If mnuLastWeeks.Checked Then
            EstimationPeriod = Trinity.AllocateFunctions.EstimationPeriodEnum.LastWeeks
        ElseIf mnuLastYear.Checked Then
            EstimationPeriod = Trinity.AllocateFunctions.EstimationPeriodEnum.LastYear
        Else
            EstimationPeriod = Trinity.AllocateFunctions.EstimationPeriodEnum.CustomPeriod

        End If
        Trinity.AllocateFunctions.CalculateNaturalDelivery(grdIndex, EstimationPeriod, ActiveCampaign, cmdNaturalDelivery.Tag)

    End Sub

    Private Sub cmdIndexSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIndexSettings.Click
        mnuSetup.Show(cmdIndexSettings, New System.Drawing.Point(0, cmdIndexSettings.Height))
    End Sub

    Private Sub mnuLastWeeks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLastWeeks.Click
        mnuLastWeeks.Checked = True
        mnuLastYear.Checked = False
    End Sub

    Private Sub mnuLastYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLastYear.Click
        mnuLastWeeks.Checked = False
        mnuLastYear.Checked = True
    End Sub

    Private Sub cmdCopyToAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCopyToAll.Click
        Dim Chan As String
        Dim BT As String
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek
        Dim TmpFilm As Trinity.cFilm
        If Not ActiveCampaign.RootCampaign Is Nothing Then Exit Sub

        If cmbFilmChannel.SelectedItem.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            Dim tmpC As Trinity.cCombination = cmbFilmChannel.SelectedItem
            Dim tmpCC As Trinity.cCombinationChannel = tmpC.Relations(1)

            For Each TmpWeek In tmpCC.Bookingtype.Weeks
                For Each TmpFilm In TmpWeek.Films
                    For Each TmpChan In ActiveCampaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            TmpBT.Weeks(TmpWeek.Name).Films(TmpFilm.Name).Share = TmpFilm.Share
                        Next
                    Next
                    For Each tmpC2 As Trinity.cCombination In ActiveCampaign.Combinations
                        If tmpC2.ShowAsOne Then
                            For Each tmpCC2 As Trinity.cCombinationChannel In tmpC.Relations
                                tmpCC2.Bookingtype.Weeks(TmpWeek.Name).Films(TmpFilm.Name).Share = TmpFilm.Share
                            Next
                        End If
                    Next
                Next
            Next

        Else
            Chan = DirectCast(cmbFilmChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
            BT = DirectCast(cmbFilmChannel.SelectedItem, Trinity.cBookingType).Name
            For Each TmpWeek In ActiveCampaign.Channels(Chan).BookingTypes(BT).Weeks
                For Each TmpFilm In TmpWeek.Films
                    For Each TmpChan In ActiveCampaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            If TmpBT.BookIt Then
                                TmpBT.Weeks(TmpWeek.Name).Films(TmpFilm.Name).Share = TmpFilm.Share
                            End If
                        Next
                    Next
                    For Each tmpC As Trinity.cCombination In ActiveCampaign.Combinations
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            tmpCC.Bookingtype.Weeks(TmpWeek.Name).Films(TmpFilm.Name).Share = TmpFilm.Share
                        Next
                    Next
                Next
            Next
        End If
        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
        grdBudget.Invalidate()
        grdSumChannels.Invalidate()
    End Sub

    Private Sub cmdAddCampaign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddCampaign.Click

        Dim TmpMenu As New Windows.Forms.ContextMenu
        TmpMenu.MenuItems.Add("Copy of main campaign", AddressOf AddCampaign)
        If Campaign.Campaigns.Count > 0 Then
            With TmpMenu.MenuItems.Add("Copy of lab campaign")
                For Each _campName As String In Campaign.Campaigns.Keys
                    .MenuItems.Add(_campName, AddressOf AddCampaign)
                Next
            End With
        End If
        TmpMenu.Show(cmdAddCampaign, New Point(0, cmdAddCampaign.Height))
    End Sub

    Sub AddCampaign(ByVal sender As Object, ByVal e As EventArgs)
        Dim TmpName As String
        Dim TmpCampaign As Trinity.cKampanj

        TmpName = InputBox("Descriptive title:", "New solution")
        If TmpName = "" Then Exit Sub

        TmpCampaign = New Trinity.cKampanj(False)
        If DirectCast(sender, Windows.Forms.MenuItem).Text = "Copy of main campaign" Then
            TmpCampaign.LoadCampaign("", True, Campaign.SaveCampaign(, True, True, True))
        Else
            TmpCampaign.LoadCampaign("", True, Campaign.Campaigns(DirectCast(sender, Windows.Forms.MenuItem).Text).SaveCampaign(, True, True, True))
        End If
        'TmpCampaign.Name = TmpName
        Campaign.Campaigns.Add(TmpName, TmpCampaign)
        cmbCampaigns.Items.Add(TmpName)
        Karma = Nothing
        chtKarma.Karma = Nothing
    End Sub

    Private Sub cmbCampaigns_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCampaigns.SelectedIndexChanged

        'Dim TmpChan As Trinity.cChannel
        'Dim TmpBT As Trinity.cBookingType
        'Dim TmpWeek As Trinity.cWeek
        'Dim TmpAV As Trinity.cAddedValue
        'Dim j As Integer
        'Dim i As Integer
        'Dim lblBuyingTarget As System.Windows.Forms.Label = Nothing

        'frmMain.Cursor = Windows.Forms.Cursors.WaitCursor
        'ActiveCampaign = Campaign.Campaigns(cmbCampaigns.Text)

        'grdTRP.Columns.Clear()
        'grdTRP.Rows.Clear()
        'grdSumWeeks.Columns.Clear()
        'grdSumWeeks.Rows.Clear()
        'grdSumChannels.Rows.Clear()
        'grdAV.Columns.Clear()
        'grdAV.Rows.Clear()
        'grdDiscounts.Columns.Clear()
        'grdDiscounts.Columns.Clear()
        'grdFilms.Columns.Clear()
        'grdFilms.Rows.Clear()
        'grdIndex.Rows.Clear()
        'grdGrandSum.Rows.Clear()
        'cmbFilmChannel.Items.Clear()
        'While grdBudget.Columns.Count > 2
        '    grdBudget.Columns.Remove(grdBudget.Columns(2))
        'End While
        'grdBudget.Rows.Clear()
        'grdTRP.ColumnHeadersDefaultCellStyle.Alignment = Windows.Forms.DataGridViewContentAlignment.TopRight
        'grdSumChannels.DefaultCellStyle.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        'grdBudget.ColumnHeadersDefaultCellStyle.Alignment = Windows.Forms.DataGridViewContentAlignment.TopRight
        'colBudget.HeaderCell.Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter


        'For Each tmpC As Trinity.cCombination In ActiveCampaign.Combinations
        '    'if showAsOne is true we need to display the contents as one in the window
        '    If tmpC.ShowAsOne Then
        '        If grdTRP.Columns.Count = 0 Then
        '            'Sets the properties for the TRP,SumWeek,AV,Discount,Budget and film grid
        '            For Each TmpWeek In tmpC.Relations(1).Bookingtype.Weeks
        '                grdTRP.Columns.Add("colWeek" & TmpWeek.Name, TmpWeek.Name)
        '                grdTRP.Columns(grdTRP.Columns.Count - 1).Width = 60
        '                grdTRP.Columns(grdTRP.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '                grdSumWeeks.Columns.Add("colSumWeek" & TmpWeek.Name, "")
        '                grdSumWeeks.Columns(grdSumWeeks.Columns.Count - 1).Width = 60
        '                grdSumWeeks.Columns(grdSumWeeks.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '                grdAV.Columns.Add("colAV" & TmpWeek.Name, TmpWeek.Name)
        '                grdAV.Columns(grdAV.Columns.Count - 1).Width = 60
        '                grdAV.Columns(grdAV.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '                grdDiscounts.Columns.Add("colDiscounts" & TmpWeek.Name, TmpWeek.Name)
        '                grdDiscounts.Columns(grdDiscounts.Columns.Count - 1).Width = 60
        '                grdDiscounts.Columns(grdDiscounts.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '                grdBudget.Columns.Add("colBudgetWeek" & TmpWeek.Name, TmpWeek.Name)
        '                grdBudget.Columns(grdBudget.Columns.Count - 1).Width = 60
        '                grdBudget.Columns(grdBudget.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '                grdFilms.Columns.Add("colFilmWeek" & TmpWeek.Name, TmpWeek.Name)
        '                grdFilms.Columns(grdFilms.Columns.Count - 1).Width = 40
        '                grdFilms.Columns(grdFilms.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '            Next
        '            'makes the "total" column in the end of the budget grid
        '            grdBudget.Columns.Add("colBudgetSum", "Total")
        '            grdBudget.Columns(grdBudget.Columns.Count - 1).Width = 60
        '            grdBudget.Columns(grdBudget.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '        End If

        '        grdTRP.Rows.Add(2)
        '        grdTRP.Rows(grdTRP.Rows.Count - 1).Tag = tmpC
        '        grdTRP.Rows(grdTRP.Rows.Count - 2).Tag = tmpC
        '        grdDiscounts.Rows.Add(4)
        '        grdDiscounts.Rows(grdDiscounts.Rows.Count - 1).Tag = tmpC
        '        grdDiscounts.Rows(grdDiscounts.Rows.Count - 2).Tag = tmpC
        '        grdDiscounts.Rows(grdDiscounts.Rows.Count - 3).Tag = tmpC
        '        grdDiscounts.Rows(grdDiscounts.Rows.Count - 4).Tag = tmpC
        '        TmpBT = tmpC.Relations(1).Bookingtype
        '        grdBudget.Rows.Add()
        '        grdBudget.Rows(grdBudget.Rows.Count - 1).Tag = tmpC
        '        grdIndex.Rows.Add()
        '        grdIndex.Rows(grdIndex.Rows.Count - 1).Tag = TmpBT
        '        grdIndex.Rows(grdIndex.Rows.Count - 1).Cells(0).Tag = tmpC

        '        'Add the Added values to its grid 'they should contain the same so we only check one
        '        For Each TmpAV In TmpBT.AddedValues
        '            If Not TmpAV.ShowIn = Trinity.cAddedValue.ShowInEnum.siBooking Then
        '                grdAV.Rows.Add()
        '                grdAV.Rows(grdAV.Rows.Count - 1).Tag = TmpAV
        '                grdAV.Rows(grdAV.Rows.Count - 1).Cells(0).Tag = tmpC
        '            End If
        '        Next

        '        For i = 0 To TmpBT.Weeks.Count - 1
        '            grdTRP.Rows(grdTRP.Rows.Count - 1).Cells(i).Tag = tmpC
        '            grdTRP.Rows(grdTRP.Rows.Count - 2).Cells(i).Tag = tmpC
        '            grdDiscounts.Rows(grdDiscounts.Rows.Count - 1).Cells(i).Tag = tmpC
        '            grdDiscounts.Rows(grdDiscounts.Rows.Count - 2).Cells(i).Tag = tmpC
        '            grdDiscounts.Rows(grdDiscounts.Rows.Count - 3).Cells(i).Tag = tmpC
        '            grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(2 + i).Tag = TmpBT.Weeks(i + 1)
        '        Next
        '        grdSumChannels.Rows.Add(2)
        '        cmbFilmChannel.Items.Add(tmpC)
        '    End If
        'Next

        ''steps trough all the channels selected for the campaign and their bookings
        'For Each TmpChan In ActiveCampaign.Channels
        '    For Each TmpBT In TmpChan.BookingTypes
        '        If TmpBT.BookIt AndAlso TmpBT.ShowMe Then
        '            If grdTRP.Columns.Count = 0 Then
        '                'Sets the properties for the TRP,SumWeek,AV,Discount,Budget and film grid
        '                For Each TmpWeek In TmpBT.Weeks
        '                    grdTRP.Columns.Add("colWeek" & TmpWeek.Name, TmpWeek.Name)
        '                    grdTRP.Columns(grdTRP.Columns.Count - 1).Width = 60
        '                    grdTRP.Columns(grdTRP.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '                    grdSumWeeks.Columns.Add("colSumWeek" & TmpWeek.Name, "")
        '                    grdSumWeeks.Columns(grdSumWeeks.Columns.Count - 1).Width = 60
        '                    grdSumWeeks.Columns(grdSumWeeks.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '                    grdAV.Columns.Add("colAV" & TmpWeek.Name, TmpWeek.Name)
        '                    grdAV.Columns(grdAV.Columns.Count - 1).Width = 60
        '                    grdAV.Columns(grdAV.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '                    grdDiscounts.Columns.Add("colDiscounts" & TmpWeek.Name, TmpWeek.Name)
        '                    grdDiscounts.Columns(grdDiscounts.Columns.Count - 1).Width = 60
        '                    grdDiscounts.Columns(grdDiscounts.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '                    grdBudget.Columns.Add("colBudgetWeek" & TmpWeek.Name, TmpWeek.Name)
        '                    grdBudget.Columns(grdBudget.Columns.Count - 1).Width = 60
        '                    grdBudget.Columns(grdBudget.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '                    grdFilms.Columns.Add("colFilmWeek" & TmpWeek.Name, TmpWeek.Name)
        '                    grdFilms.Columns(grdFilms.Columns.Count - 1).Width = 40
        '                    grdFilms.Columns(grdFilms.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '                Next
        '                'makes the "total" column in the end of the budget grid
        '                grdBudget.Columns.Add("colBudgetSum", "Total")
        '                grdBudget.Columns(grdBudget.Columns.Count - 1).Width = 60
        '                grdBudget.Columns(grdBudget.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '            End If
        '            'Add the Added values to its grid
        '            For Each TmpAV In TmpBT.AddedValues
        '                If Not TmpAV.ShowIn = Trinity.cAddedValue.ShowInEnum.siBooking Then
        '                    grdAV.Rows.Add()
        '                    grdAV.Rows(grdAV.Rows.Count - 1).Tag = TmpAV
        '                End If
        '            Next
        '            grdTRP.Rows.Add(2)
        '            grdTRP.Rows(grdTRP.Rows.Count - 1).Tag = TmpBT
        '            grdTRP.Rows(grdTRP.Rows.Count - 2).Tag = TmpBT
        '            grdDiscounts.Rows.Add(4)
        '            grdDiscounts.Rows(grdDiscounts.Rows.Count - 1).Tag = TmpBT
        '            grdDiscounts.Rows(grdDiscounts.Rows.Count - 2).Tag = TmpBT
        '            grdDiscounts.Rows(grdDiscounts.Rows.Count - 3).Tag = TmpBT
        '            grdDiscounts.Rows(grdDiscounts.Rows.Count - 4).Tag = TmpBT
        '            grdBudget.Rows.Add()
        '            grdBudget.Rows(grdBudget.Rows.Count - 1).Tag = TmpBT
        '            grdIndex.Rows.Add()
        '            grdIndex.Rows(grdIndex.Rows.Count - 1).Tag = TmpBT
        '            For i = 0 To TmpBT.Weeks.Count - 1
        '                grdTRP.Rows(grdTRP.Rows.Count - 1).Cells(i).Tag = TmpBT.Weeks(i + 1)
        '                grdTRP.Rows(grdTRP.Rows.Count - 2).Cells(i).Tag = TmpBT.Weeks(i + 1)
        '                grdDiscounts.Rows(grdDiscounts.Rows.Count - 1).Cells(i).Tag = TmpBT.Weeks(i + 1)
        '                grdDiscounts.Rows(grdDiscounts.Rows.Count - 2).Cells(i).Tag = TmpBT.Weeks(i + 1)
        '                grdDiscounts.Rows(grdDiscounts.Rows.Count - 3).Cells(i).Tag = TmpBT.Weeks(i + 1)
        '                grdDiscounts.Rows(grdDiscounts.Rows.Count - 4).Cells(i).Tag = TmpBT.Weeks(i + 1)
        '                grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(2 + i).Tag = TmpBT.Weeks(i + 1)
        '            Next
        '            grdSumChannels.Rows.Add(2)
        '            cmbFilmChannel.Items.Add(TmpBT)
        '        End If
        '    Next
        'Next

        ''For Each TmpChan In ActiveCampaign.Channels
        ''    For Each TmpBT In TmpChan.BookingTypes
        ''        If TmpBT.BookIt Then
        ''            If grdTRP.Columns.Count = 0 Then
        ''                For Each TmpWeek In TmpBT.Weeks
        ''                    grdTRP.Columns.Add("colWeek" & TmpWeek.Name, TmpWeek.Name)
        ''                    grdTRP.Columns(grdTRP.Columns.Count - 1).Width = 60
        ''                    grdTRP.Columns(grdTRP.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        ''                    grdSumWeeks.Columns.Add("colSumWeek" & TmpWeek.Name, "")
        ''                    grdSumWeeks.Columns(grdSumWeeks.Columns.Count - 1).Width = 60
        ''                    grdSumWeeks.Columns(grdSumWeeks.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        ''                    grdAV.Columns.Add("colAV" & TmpWeek.Name, TmpWeek.Name)
        ''                    grdAV.Columns(grdAV.Columns.Count - 1).Width = 60
        ''                    grdAV.Columns(grdAV.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        ''                    grdDiscounts.Columns.Add("colDiscounts" & TmpWeek.Name, TmpWeek.Name)
        ''                    grdDiscounts.Columns(grdDiscounts.Columns.Count - 1).Width = 60
        ''                    grdDiscounts.Columns(grdDiscounts.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        ''                    grdBudget.Columns.Add("colBudgetWeek" & TmpWeek.Name, TmpWeek.Name)
        ''                    grdBudget.Columns(grdBudget.Columns.Count - 1).Width = 60
        ''                    grdBudget.Columns(grdBudget.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        ''                    grdFilms.Columns.Add("colFilmWeek" & TmpWeek.Name, TmpWeek.Name)
        ''                    grdFilms.Columns(grdFilms.Columns.Count - 1).Width = 40
        ''                    grdFilms.Columns(grdFilms.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        ''                Next
        ''                grdBudget.Columns.Add("colBudgetSum", "Total")
        ''                grdBudget.Columns(grdBudget.Columns.Count - 1).Width = 60
        ''                grdBudget.Columns(grdBudget.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
        ''            End If
        ''            For Each TmpAV In TmpBT.AddedValues
        ''                grdAV.Rows.Add()
        ''                grdAV.Rows(grdAV.Rows.Count - 1).Tag = TmpAV
        ''            Next
        ''            grdTRP.Rows.Add(2)
        ''            grdTRP.Rows(grdTRP.Rows.Count - 1).Tag = TmpBT
        ''            grdTRP.Rows(grdTRP.Rows.Count - 2).Tag = TmpBT
        ''            grdDiscounts.Rows.Add(3)
        ''            grdDiscounts.Rows(grdDiscounts.Rows.Count - 1).Tag = TmpBT
        ''            grdDiscounts.Rows(grdDiscounts.Rows.Count - 2).Tag = TmpBT
        ''            grdDiscounts.Rows(grdDiscounts.Rows.Count - 3).Tag = TmpBT
        ''            grdBudget.Rows.Add()
        ''            grdBudget.Rows(grdBudget.Rows.Count - 1).Tag = TmpBT
        ''            grdIndex.Rows.Add()
        ''            grdIndex.Rows(grdIndex.Rows.Count - 1).Tag = TmpBT
        ''            For i = 0 To TmpBT.Weeks.Count - 1
        ''                grdTRP.Rows(grdTRP.Rows.Count - 1).Cells(i).Tag = TmpBT.Weeks(i + 1)
        ''                grdTRP.Rows(grdTRP.Rows.Count - 2).Cells(i).Tag = TmpBT.Weeks(i + 1)
        ''                grdDiscounts.Rows(grdDiscounts.Rows.Count - 1).Cells(i).Tag = TmpBT.Weeks(i + 1)
        ''                grdDiscounts.Rows(grdDiscounts.Rows.Count - 2).Cells(i).Tag = TmpBT.Weeks(i + 1)
        ''                grdDiscounts.Rows(grdDiscounts.Rows.Count - 3).Cells(i).Tag = TmpBT.Weeks(i + 1)
        ''                grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(2 + i).Tag = TmpBT.Weeks(i + 1)
        ''            Next
        ''            grdSumChannels.Rows.Add(2)
        ''            cmbFilmChannel.Items.Add(TmpChan.ChannelName & " " & TmpBT.Name)
        ''        End If
        ''    Next
        ''Next
        'grdFilms.RowHeadersWidthSizeMode = Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        'grdBudget.Rows.Add()
        'grdGrandSum.Rows.Add()
        'grdSumWeeks.Rows.Add(2)
        'grdTRP.Height = 10000
        'grdTRP.Height = grdTRP.GetRowDisplayRectangle(grdTRP.Rows.Count - 1, False).Bottom + 1
        'grdTRP.Width = 10000
        'grdTRP.Width = grdTRP.GetColumnDisplayRectangle(grdTRP.Columns.Count - 1, False).Right + 1
        'grdSumChannels.Left = grdTRP.Right - 1
        'grdSumChannels.Height = grdTRP.Height
        'grdSumWeeks.Top = grdTRP.Bottom - 1
        'grdSumWeeks.Width = grdTRP.Width
        'grdSumWeeks.Height = grdSumWeeks.GetRowDisplayRectangle(grdSumWeeks.Rows.Count - 1, False).Bottom + 1
        'grdGrandSum.Left = grdSumChannels.Left
        'grdGrandSum.Top = grdSumWeeks.Top
        'grdAV.Width = grdTRP.Width
        'If grdAV.Rows.Count > 0 Then
        '    grdAV.Height = 10000
        '    grdAV.Height = grdAV.GetRowDisplayRectangle(grdAV.Rows.Count - 1, True).Bottom + 1
        'Else
        '    grdAV.Height = 0
        'End If
        'grdDiscounts.Width = grdTRP.Width
        'grdDiscounts.Height = 10000
        'grdDiscounts.Height = grdDiscounts.GetRowDisplayRectangle(grdDiscounts.Rows.Count - 1, False).Bottom + 1
        'grdBudget.Width = 10000
        'grdBudget.Width = grdBudget.GetColumnDisplayRectangle(grdBudget.Columns.Count - 1, False).Right + 1
        'grdBudget.Height = 10000
        'grdBudget.Height = grdBudget.GetRowDisplayRectangle(grdBudget.Rows.Count - 1, False).Bottom + 1
        'grdIndex.Height = 10000
        'grdIndex.Height = grdIndex.GetRowDisplayRectangle(grdIndex.Rows.Count - 1, False).Bottom + 1
        'colMainTarget.HeaderCell.Value = ActiveCampaign.MainTarget.TargetNameNice
        'colSecTarget.HeaderCell.Value = ActiveCampaign.SecondaryTarget.TargetNameNice
        'colAllAdults.HeaderCell.Value = ActiveCampaign.AllAdults

        'lblCTCLabel.Top = grdBudget.Bottom + 3
        'lblCTC.Top = grdBudget.Bottom + 3
        'lblCTC.Left = grdBudget.Right - lblCTC.Width
        'lblCTCLabel.Left = lblCTC.Left - lblCTCLabel.Width

        'grpTRP.Width = grdSumChannels.Right + 6
        'If grpTRP.Width < 385 Then
        '    grpTRP.Width = 385
        'End If
        'grpTRP.Height = 10000
        'grpTRP.Height = grdSumWeeks.Bottom + 25
        ''grpTRP.Height = grdSumWeeks.Bottom + 6
        'grpAV.Top = grpTRP.Bottom + 6
        'grpAV.Height = 17
        'grpAV.Width = grdAV.Right + 6
        'picExpandAV.Visible = True
        'picCollapseAV.Visible = False
        'grpDiscounts.Height = 17
        'grpDiscounts.Width = grdDiscounts.Right + 6
        'grpDiscounts.Top = grpAV.Bottom + 6
        'picExpandDiscounts.Visible = True
        'picCollapseDiscounts.Visible = False
        'grpBudget.Width = grdBudget.Right + 6
        'grpBudget.Height = 10000
        'grpBudget.Height = lblCTC.Bottom + 6
        'grpBudget.Top = grpDiscounts.Bottom + 6
        'grpFilms.Left = grpTRP.Right + 6
        'grpIndex.Height = grdIndex.Bottom + 6
        'grpIndex.Left = grpFilms.Left
        'grpFindReach.Left = grpIndex.Left
        'grpFindReach.Top = grpIndex.Bottom + 6

        'For i = 0 To grdTRP.Rows.Count - 1 Step 2
        '    Dim lblChan As New System.Windows.Forms.Label
        '    grpTRP.Controls.Add(lblChan)
        '    lblChan.AutoSize = False
        '    lblChan.TextAlign = Drawing.ContentAlignment.MiddleLeft
        '    If grdTRP.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
        '        lblChan.Text = DirectCast(grdTRP.Rows(i).Tag, Trinity.cCombination).Name
        '    Else
        '        lblChan.Text = DirectCast(grdTRP.Rows(i).Tag, Trinity.cBookingType).ParentChannel.Shortname & " " & DirectCast(grdTRP.Rows(i).Tag, Trinity.cBookingType).Shortname
        '    End If
        '    'lblChan.Text = DirectCast(grdTRP.Rows(i).Tag, Trinity.cBookingType).ParentChannel.Shortname & " " & DirectCast(grdTRP.Rows(i).Tag, Trinity.cBookingType).Shortname
        '    lblChan.Top = grdTRP.GetRowDisplayRectangle(i, True).Top + grdTRP.Top - 1
        '    lblChan.Left = cmbUniverse.Left
        '    lblChan.Height = grdTRP.GetRowDisplayRectangle(i, True).Height + grdTRP.GetRowDisplayRectangle(i + 1, True).Height
        '    lblChan.Width = 65
        '    lblChan.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        '    lblChan.BringToFront()

        '    lblBuyingTarget = New System.Windows.Forms.Label
        '    grpTRP.Controls.Add(lblBuyingTarget)
        '    lblBuyingTarget.AutoSize = False
        '    lblBuyingTarget.TextAlign = Drawing.ContentAlignment.MiddleLeft
        '    If grdTRP.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
        '        Dim c As Trinity.cCombination = grdTRP.Rows(i).Tag
        '        If c.BuyingTarget = "" Then
        '            lblBuyingTarget.Text = "-"
        '        Else
        '            lblBuyingTarget.Text = c.BuyingTarget
        '        End If
        '        'lblBuyingTarget.Text = grdTRP.Rows(i).Tag.Name
        '    Else
        '        lblBuyingTarget.Text = DirectCast(grdTRP.Rows(i).Tag, Trinity.cBookingType).BuyingTarget.TargetName
        '    End If
        '    'lblBuyingTarget.Text = DirectCast(grdTRP.Rows(i).Tag, Trinity.cBookingType).BuyingTarget.TargetName
        '    lblBuyingTarget.Top = grdTRP.GetRowDisplayRectangle(i, True).Top + grdTRP.Top - 1
        '    lblBuyingTarget.Left = lblChan.Right
        '    lblBuyingTarget.Height = grdTRP.GetRowDisplayRectangle(i, True).Height
        '    lblBuyingTarget.Width = grdTRP.Left - lblChan.Right
        '    lblBuyingTarget.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        '    lblBuyingTarget.BringToFront()

        '    Dim lblMainTarget As New System.Windows.Forms.Label
        '    grpTRP.Controls.Add(lblMainTarget)
        '    lblMainTarget.AutoSize = False
        '    lblMainTarget.TextAlign = Drawing.ContentAlignment.MiddleLeft
        '    lblMainTarget.Text = ActiveCampaign.MainTarget.TargetName
        '    lblMainTarget.Top = grdTRP.GetRowDisplayRectangle(i + 1, True).Top + grdTRP.Top - 1
        '    lblMainTarget.Left = lblChan.Right
        '    lblMainTarget.Height = grdTRP.GetRowDisplayRectangle(i + 1, True).Height
        '    lblMainTarget.Width = grdTRP.Left - lblChan.Right
        '    lblMainTarget.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        '    lblMainTarget.BringToFront()
        'Next
        'For i = 0 To grdAV.Rows.Count - 1
        '    Dim lblChan As New System.Windows.Forms.Label
        '    grpAV.Controls.Add(lblChan)
        '    lblChan.AutoSize = False
        '    lblChan.TextAlign = Drawing.ContentAlignment.MiddleLeft
        '    lblChan.Text = DirectCast(grdAV.Rows(i).Tag, Trinity.cAddedValue).Bookingtype.ParentChannel.Shortname & " " & DirectCast(grdAV.Rows(i).Tag, Trinity.cAddedValue).Bookingtype.Shortname
        '    lblChan.Top = grdAV.GetRowDisplayRectangle(i, True).Top + grdDiscounts.Top - 1
        '    lblChan.Left = cmbUniverse.Left
        '    lblChan.Height = grdAV.GetRowDisplayRectangle(i, True).Height * DirectCast(grdAV.Rows(i).Tag, Trinity.cAddedValue).Bookingtype.AddedValues.Count
        '    lblChan.Width = 65
        '    lblChan.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        '    For j = 1 To DirectCast(grdAV.Rows(i).Tag, Trinity.cAddedValue).Bookingtype.AddedValues.Count
        '        Dim lblAV As New System.Windows.Forms.Label
        '        grpAV.Controls.Add(lblAV)
        '        lblAV.AutoSize = False
        '        lblAV.TextAlign = Drawing.ContentAlignment.MiddleLeft
        '        lblAV.Text = DirectCast(grdAV.Rows(i).Tag, Trinity.cAddedValue).Name
        '        lblAV.Top = grdAV.GetRowDisplayRectangle(i + j - 1, True).Top + grdDiscounts.Top - 1
        '        lblAV.Left = lblChan.Right
        '        lblAV.Height = grdAV.GetRowDisplayRectangle(i + j - 1, True).Height * DirectCast(grdAV.Rows(i).Tag, Trinity.cAddedValue).Bookingtype.AddedValues.Count
        '        lblAV.Width = grdDiscounts.Left - lblChan.Right
        '        lblAV.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        '    Next
        '    i = i + DirectCast(grdAV.Rows(i).Tag, Trinity.cAddedValue).Bookingtype.AddedValues.Count - 1
        'Next
        'For i = 0 To grdDiscounts.Rows.Count - 1 Step 3
        '    Dim lblChan As New System.Windows.Forms.Label
        '    grpDiscounts.Controls.Add(lblChan)
        '    lblChan.AutoSize = False
        '    lblChan.TextAlign = Drawing.ContentAlignment.MiddleLeft
        '    If grdDiscounts.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
        '        lblChan.Text = DirectCast(grdDiscounts.Rows(i).Tag, Trinity.cCombination).Name
        '    Else
        '        lblChan.Text = DirectCast(grdDiscounts.Rows(i).Tag, Trinity.cBookingType).ParentChannel.Shortname & " " & DirectCast(grdDiscounts.Rows(i).Tag, Trinity.cBookingType).Shortname
        '    End If
        '    'lblChan.Text = DirectCast(grdDiscounts.Rows(i).Tag, Trinity.cBookingType).ParentChannel.Shortname & " " & DirectCast(grdDiscounts.Rows(i).Tag, Trinity.cBookingType).Shortname
        '    lblChan.Top = grdDiscounts.GetRowDisplayRectangle(i, True).Top + grdDiscounts.Top - 1
        '    lblChan.Left = cmbUniverse.Left
        '    lblChan.Height = grdDiscounts.GetRowDisplayRectangle(i, True).Height * 3
        '    lblChan.Width = 65
        '    lblChan.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

        '    Dim lblEffDisc As New System.Windows.Forms.Label
        '    grpDiscounts.Controls.Add(lblEffDisc)
        '    lblEffDisc.AutoSize = False
        '    lblEffDisc.TextAlign = Drawing.ContentAlignment.MiddleLeft
        '    lblEffDisc.Text = "Eff. Discount"
        '    lblEffDisc.Top = grdDiscounts.GetRowDisplayRectangle(i, True).Top + grdDiscounts.Top - 1
        '    lblEffDisc.Left = lblChan.Right
        '    lblEffDisc.Height = grdDiscounts.GetRowDisplayRectangle(i, True).Height
        '    lblEffDisc.Width = grdDiscounts.Left - lblChan.Right
        '    lblEffDisc.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

        '    Dim lblNetCPP As New System.Windows.Forms.Label
        '    grpDiscounts.Controls.Add(lblNetCPP)
        '    lblNetCPP.AutoSize = False
        '    lblNetCPP.TextAlign = Drawing.ContentAlignment.MiddleLeft
        '    lblNetCPP.Text = "Net CPP 30"""
        '    lblNetCPP.Top = grdDiscounts.GetRowDisplayRectangle(i + 1, True).Top + grdDiscounts.Top - 1
        '    lblNetCPP.Left = lblChan.Right
        '    lblNetCPP.Height = grdDiscounts.GetRowDisplayRectangle(i + 1, True).Height
        '    lblNetCPP.Width = grdDiscounts.Left - lblChan.Right
        '    lblNetCPP.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

        '    Dim lblIndex As New System.Windows.Forms.Label
        '    grpDiscounts.Controls.Add(lblIndex)
        '    lblIndex.AutoSize = False
        '    lblIndex.TextAlign = Drawing.ContentAlignment.MiddleLeft
        '    lblIndex.Text = "Index"
        '    lblIndex.Top = grdDiscounts.GetRowDisplayRectangle(i + 2, True).Top + grdDiscounts.Top - 1
        '    lblIndex.Left = lblChan.Right
        '    lblIndex.Height = grdDiscounts.GetRowDisplayRectangle(i + 2, True).Height
        '    lblIndex.Width = grdDiscounts.Left - lblChan.Right
        '    lblIndex.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        'Next

        'For i = 0 To grdBudget.Rows.Count - 1
        '    Dim lblChan As New System.Windows.Forms.Label
        '    grpBudget.Controls.Add(lblChan)
        '    lblChan.AutoSize = False
        '    lblChan.TextAlign = Drawing.ContentAlignment.MiddleLeft
        '    If i < grdBudget.Rows.Count - 1 Then
        '        If grdBudget.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
        '            lblChan.Text = DirectCast(grdBudget.Rows(i).Tag, Trinity.cCombination).Name
        '        Else
        '            lblChan.Text = DirectCast(grdBudget.Rows(i).Tag, Trinity.cBookingType).ParentChannel.Shortname & " " & DirectCast(grdBudget.Rows(i).Tag, Trinity.cBookingType).Shortname
        '        End If
        '        'lblChan.Text = DirectCast(grdBudget.Rows(i).Tag, Trinity.cBookingType).ParentChannel.Shortname & " " & DirectCast(grdBudget.Rows(i).Tag, Trinity.cBookingType).Shortname
        '    Else
        '        lblChan.Text = "Total:"
        '    End If
        '    lblChan.Top = grdBudget.GetRowDisplayRectangle(i, True).Top + grdBudget.Top - 1
        '    lblChan.Left = cmbUniverse.Left
        '    lblChan.Height = grdBudget.GetRowDisplayRectangle(i, True).Height
        '    lblChan.Width = 65
        '    lblChan.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

        'Next

        'For i = 0 To grdIndex.Rows.Count - 1
        '    Dim lblChan As New System.Windows.Forms.Label
        '    grpIndex.Controls.Add(lblChan)
        '    lblChan.AutoSize = False
        '    lblChan.TextAlign = Drawing.ContentAlignment.MiddleLeft
        '    lblChan.Text = DirectCast(grdIndex.Rows(i).Tag, Trinity.cBookingType).ParentChannel.Shortname & " " & DirectCast(grdIndex.Rows(i).Tag, Trinity.cBookingType).Shortname
        '    lblChan.Top = grdIndex.GetRowDisplayRectangle(i, True).Top + grdIndex.Top - 1
        '    lblChan.Left = 6
        '    lblChan.Height = grdIndex.GetRowDisplayRectangle(i, True).Height
        '    lblChan.Width = 65
        '    lblChan.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

        '    lblBuyingTarget = New System.Windows.Forms.Label
        '    grpIndex.Controls.Add(lblBuyingTarget)
        '    lblBuyingTarget.AutoSize = False
        '    lblBuyingTarget.TextAlign = Drawing.ContentAlignment.MiddleLeft
        '    lblBuyingTarget.Text = DirectCast(grdIndex.Rows(i).Tag, Trinity.cBookingType).BuyingTarget.TargetName
        '    lblBuyingTarget.Top = grdIndex.GetRowDisplayRectangle(i, True).Top + grdIndex.Top - 1
        '    lblBuyingTarget.Left = lblChan.Right
        '    lblBuyingTarget.Height = grdIndex.GetRowDisplayRectangle(i, True).Height
        '    lblBuyingTarget.Width = grdIndex.Left - lblChan.Right
        '    lblBuyingTarget.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        'Next

        'Dim lblSum As New System.Windows.Forms.Label
        'grpTRP.Controls.Add(lblSum)
        'lblSum.AutoSize = False
        'lblSum.TextAlign = Drawing.ContentAlignment.MiddleLeft
        'lblSum.Text = ActiveCampaign.MainTarget.TargetName & " Nat"
        'lblSum.Top = grdSumWeeks.GetRowDisplayRectangle(0, True).Top + grdSumWeeks.Top - 1
        'lblSum.Left = lblBuyingTarget.Left
        'lblSum.Height = grdSumWeeks.GetRowDisplayRectangle(0, True).Height + 1
        'lblSum.Width = lblBuyingTarget.Width
        'lblSum.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

        'lblSum = New System.Windows.Forms.Label
        'grpTRP.Controls.Add(lblSum)
        'lblSum.AutoSize = False
        'lblSum.TextAlign = Drawing.ContentAlignment.MiddleLeft
        'lblSum.Text = ActiveCampaign.AllAdults & " Nat"
        'lblSum.Top = grdSumWeeks.GetRowDisplayRectangle(1, True).Top + grdSumWeeks.Top - 1
        'lblSum.Left = lblBuyingTarget.Left
        'lblSum.Height = grdSumWeeks.GetRowDisplayRectangle(1, True).Height + 1
        'lblSum.Width = lblBuyingTarget.Width
        'lblSum.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

        'Remove old labels
        Dim i As Integer
        For i = 0 To 1 'We need to to this twice. Why? No idea.
            For Each TmpControl As Windows.Forms.Control In tpCampaigns.Controls
                If TmpControl.Name = "" Then
                    Me.Controls.Remove(TmpControl)
                    TmpControl.Dispose()
                End If
                For Each TmpCtrl As Windows.Forms.Control In TmpControl.Controls
                    If TmpCtrl.Name = "" Then
                        TmpControl.Controls.Remove(TmpCtrl)
                        TmpCtrl.Dispose()
                    End If
                Next
            Next
        Next

        'Reset loading
        TRPLoading = Nothing
        BudgetLoading = Nothing

        frmMain.Cursor = Windows.Forms.Cursors.WaitCursor
        ActiveCampaign = Campaign.Campaigns(cmbCampaigns.Text)

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
        grdDiscounts.Rows.Clear()
        'clears the discount grid
        grdLoading.Columns.Clear()
        grdLoading.Rows.Clear()
        'clears the commercial films grid
        grdFilms.Columns.Clear()
        grdFilms.Rows.Clear()
        'clears the grid containing the Target ranges
        grdIndex.Rows.Clear()
        'a small grid summirinzing Channels/weeks
        grdGrandSum.Rows.Clear()
        'clear the combo box containing all channels with films
        cmbFilmChannel.Items.Clear()

        grdTRP.ColumnHeadersDefaultCellStyle.Alignment = Windows.Forms.DataGridViewContentAlignment.TopRight
        grdSumChannels.DefaultCellStyle.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        grdBudget.ColumnHeadersDefaultCellStyle.Alignment = Windows.Forms.DataGridViewContentAlignment.TopRight
        colBudget.HeaderCell.Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter

        For Each tmpC As Trinity.cCombination In ActiveCampaign.Combinations
            'if showAsOne is true we need to display the contents as one in the window
            If tmpC.ShowAsOne Then
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
                        grdLoading.Columns.Add("colLoading" & TmpWeek.Name, TmpWeek.Name)
                        grdLoading.Columns(grdLoading.Columns.Count - 1).Width = 60
                        grdLoading.Columns(grdLoading.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
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

                    grdDiscounts.Columns.Add("colTotal", "Total")
                    grdDiscounts.Columns(grdDiscounts.Columns.Count - 1).Width = 60
                    grdDiscounts.Columns(grdDiscounts.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable

                    grdLoading.Columns.Add("colLoadingSum", "Total")
                    grdLoading.Columns(grdLoading.Columns.Count - 1).Width = 60
                    grdLoading.Columns(grdLoading.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
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
                grdLoading.Rows.Add()
                grdLoading.Rows(grdLoading.Rows.Count - 1).Tag = tmpC
                grdBudget.Rows.Add()
                grdBudget.Rows(grdBudget.Rows.Count - 1).Tag = tmpC
                grdIndex.Rows.Add()
                grdIndex.Rows(grdIndex.Rows.Count - 1).Tag = tmpC
                grdIndex.Rows(grdIndex.Rows.Count - 1).Cells(0).Tag = tmpC

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
                    grdLoading.Rows(grdLoading.Rows.Count - 1).Cells(i).Tag = TmpBT.Weeks(i + 1)
                    grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(2 + i).Tag = TmpBT.Weeks(i + 1)
                Next
                grdSumChannels.Rows.Add(2)
                cmbFilmChannel.Items.Add(tmpC)
            End If
        Next

        'steps trough all the channels selected for the campaign and their bookings
        For Each TmpChan In ActiveCampaign.Channels
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
                            grdLoading.Columns.Add("colLoading" & TmpWeek.Name, TmpWeek.Name)
                            grdLoading.Columns(grdLoading.Columns.Count - 1).Width = 60
                            grdLoading.Columns(grdLoading.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
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

                        grdDiscounts.Columns.Add("colTotal", "Total")
                        grdDiscounts.Columns(grdDiscounts.Columns.Count - 1).Width = 60
                        grdDiscounts.Columns(grdDiscounts.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable

                        grdLoading.Columns.Add("colLoadingSum", "Total")
                        grdLoading.Columns(grdLoading.Columns.Count - 1).Width = 60
                        grdLoading.Columns(grdLoading.Columns.Count - 1).SortMode = Windows.Forms.DataGridViewColumnSortMode.NotSortable
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
                    grdLoading.Rows.Add()
                    grdLoading.Rows(grdLoading.Rows.Count - 1).Tag = TmpBT
                    grdBudget.Rows.Add()
                    grdBudget.Rows(grdBudget.Rows.Count - 1).Tag = TmpBT
                    grdIndex.Rows.Add()
                    grdIndex.Rows(grdIndex.Rows.Count - 1).Tag = TmpBT
                    For i = 0 To TmpBT.Weeks.Count - 1
                        grdTRP.Rows(grdTRP.Rows.Count - 1).Cells(i).Tag = TmpBT.Weeks(i + 1)
                        grdTRP.Rows(grdTRP.Rows.Count - 2).Cells(i).Tag = TmpBT.Weeks(i + 1)
                        grdDiscounts.Rows(grdDiscounts.Rows.Count - 1).Cells(i).Tag = TmpBT.Weeks(i + 1)
                        grdDiscounts.Rows(grdDiscounts.Rows.Count - 2).Cells(i).Tag = TmpBT.Weeks(i + 1)
                        grdDiscounts.Rows(grdDiscounts.Rows.Count - 3).Cells(i).Tag = TmpBT.Weeks(i + 1)
                        grdDiscounts.Rows(grdDiscounts.Rows.Count - 4).Cells(i).Tag = TmpBT.Weeks(i + 1)
                        grdLoading.Rows(grdLoading.Rows.Count - 1).Cells(i).Tag = TmpBT.Weeks(i + 1)
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
        cmbFilmChannel.Items.Add("TOTAL")
        grdFilms.RowHeadersWidthSizeMode = Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        grdLoading.Rows.Add()
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
        'grdDiscounts.AutoSizeColumnsMode = Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        'grdDiscounts.AutoSize = True
        grdDiscounts.Height = 10000
        grdDiscounts.Height = grdDiscounts.GetRowDisplayRectangle(grdDiscounts.Rows.Count - 1, False).Bottom + 1

        grdLoading.Width = 10000
        grdLoading.Width = grdLoading.GetColumnDisplayRectangle(grdLoading.Columns.Count - 1, False).Right + 1
        grdLoading.Height = 10000
        grdLoading.Height = grdLoading.GetRowDisplayRectangle(grdLoading.Rows.Count - 1, False).Bottom + 1
        cmdApplyLoading.Top = grdLoading.Bottom + 6
        cmdResetLoading.Top = cmdApplyLoading.Top

        'Sets the budget grid and shows the overflow (the "False" value)
        grdBudget.Width = 10000
        grdBudget.Width = grdBudget.GetColumnDisplayRectangle(grdBudget.Columns.Count - 1, False).Right + 1
        grdBudget.Height = 10000
        grdBudget.Height = grdBudget.GetRowDisplayRectangle(grdBudget.Rows.Count - 1, False).Bottom + 1

        grdIndex.Height = 10000
        grdIndex.Height = grdIndex.GetRowDisplayRectangle(grdIndex.Rows.Count - 1, False).Bottom + 1

        colMainTarget.HeaderCell.Style.WrapMode = Windows.Forms.DataGridViewTriState.False
        colMainTarget.HeaderCell.Value = ActiveCampaign.MainTarget.TargetNameNice
        colSecTarget.HeaderCell.Style.WrapMode = Windows.Forms.DataGridViewTriState.False
        colSecTarget.HeaderCell.Value = ActiveCampaign.SecondaryTarget.TargetNameNice
        colAllAdults.HeaderCell.Value = ActiveCampaign.AllAdults

        'sets the position od the CTC label at the bottom of the budget grid
        lblCTCLabel.Top = grdBudget.Bottom + 3
        lblCTC.Top = grdBudget.Bottom + 3
        lblCTC.Left = grdBudget.Right - lblCTC.Width
        lblCTCLabel.Left = lblCTC.Left - lblCTCLabel.Width
        cmdLockOnBudget.Left = lblCTC.Right + 6
        cmdEditCTC.Left = lblCTC.Right + 6
        cmdEditCTC.Top = lblCTC.Top - 4

        grpTRP.Width = grdSumChannels.Right + 6
        If grpTRP.Width < 415 Then
            grpTRP.Width = 415
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

        grpLoading.Height = 17
        grpLoading.Width = grdLoading.Right + 6
        grpLoading.Top = grpDiscounts.Bottom + 6
        cmdApplyLoading.Left = grdBudget.Right - cmdApplyLoading.Width - 9
        cmdResetLoading.Left = cmdApplyLoading.Left - cmdResetLoading.Width - 6
        picExpandLoading.Visible = True
        picCollapseLoading.Visible = False

        'sets the size of the Budget group (the frame around the budget grids)
        grpBudget.Width = cmdEditCTC.Right + 6
        grpBudget.Height = 10000
        grpBudget.Height = lblCTC.Bottom + 6
        grpBudget.Top = grpLoading.Bottom + 6

        grpFilms.Left = grpTRP.Right + 6
        grpIndex.Height = grdIndex.Bottom + 6
        grpIndex.Left = grpFilms.Left
        grpFindReach.Left = grpIndex.Left
        grpFindReach.Top = grpIndex.Bottom + 6

        For i = 0 To grdTRP.Rows.Count - 1 Step 2
            Dim lblChan As New System.Windows.Forms.Label
            grpTRP.Controls.Add(lblChan)
            lblChan.AutoSize = False
            lblChan.TextAlign = Drawing.ContentAlignment.MiddleLeft
            lblChan.BringToFront()

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
            lblBuyingTarget.BringToFront()
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
            lblMainTarget.Text = ActiveCampaign.MainTarget.TargetName
            lblMainTarget.Top = grdTRP.GetRowDisplayRectangle(i + 1, True).Top + grdTRP.Top - 1
            lblMainTarget.Left = lblChan.Right
            lblMainTarget.Height = grdTRP.GetRowDisplayRectangle(i + 1, True).Height
            lblMainTarget.Width = grdTRP.Left - lblChan.Right
            lblMainTarget.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
            lblMainTarget.BringToFront()
        Next
        For i = 0 To grdAV.Rows.Count - 1
            Dim lblChan As New System.Windows.Forms.Label
            grpAV.Controls.Add(lblChan)
            lblChan.AutoSize = False
            lblChan.TextAlign = Drawing.ContentAlignment.MiddleLeft
            lblChan.BringToFront()

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
                    lblAV.BringToFront()
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
            lblChan.BringToFront()
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
            lblEffDisc.BringToFront()

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
            lblNetCPP.BringToFront()

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
            lblActCPP.BringToFront()

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
            lblIndex.BringToFront()
        Next

        For i = 0 To grdLoading.Rows.Count - 1
            Dim lblChan As New System.Windows.Forms.Label
            grpLoading.Controls.Add(lblChan)
            lblChan.AutoSize = False
            lblChan.TextAlign = Drawing.ContentAlignment.MiddleLeft
            If i < grdLoading.Rows.Count - 1 Then
                If grdLoading.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    lblChan.Text = DirectCast(grdLoading.Rows(i).Tag, Trinity.cCombination).Name
                Else
                    lblChan.Text = DirectCast(grdLoading.Rows(i).Tag, Trinity.cBookingType).ParentChannel.Shortname & " " & DirectCast(grdLoading.Rows(i).Tag, Trinity.cBookingType).Shortname
                End If
            Else
                lblChan.Text = "Total:"
            End If
            lblChan.Top = grdLoading.GetRowDisplayRectangle(i, True).Top + grdLoading.Top - 1
            lblChan.Left = cmbUniverse.Left
            lblChan.Height = grdLoading.GetRowDisplayRectangle(i, True).Height
            lblChan.Width = grdLoading.Left - lblChan.Left
            lblChan.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
            lblChan.BringToFront()
        Next

        For i = 0 To grdBudget.Rows.Count - 1
            Dim lblChan As New System.Windows.Forms.Label
            grpBudget.Controls.Add(lblChan)
            lblChan.AutoSize = False
            lblChan.TextAlign = Drawing.ContentAlignment.MiddleLeft
            lblChan.BringToFront()
            If i < grdBudget.Rows.Count - 1 Then
                If grdBudget.Rows(i).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    lblChan.Text = DirectCast(grdBudget.Rows(i).Tag, Trinity.cCombination).Name
                Else
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
            lblChan.BringToFront()

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
            lblBuyingTarget.BringToFront()

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
        lblSum.Text = ActiveCampaign.MainTarget.TargetName & " Nat"
        lblSum.Top = grdSumWeeks.GetRowDisplayRectangle(0, True).Top + grdSumWeeks.Top - 1
        lblSum.Left = lblBuyingTarget.Left
        lblSum.Height = grdSumWeeks.GetRowDisplayRectangle(0, True).Height
        lblSum.Width = grdTRP.Left - lblSum.Left
        lblSum.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        lblSum.BringToFront()

        lblSum = New System.Windows.Forms.Label
        grpTRP.Controls.Add(lblSum)
        lblSum.AutoSize = False
        lblSum.TextAlign = Drawing.ContentAlignment.MiddleLeft
        lblSum.Text = ActiveCampaign.AllAdults & " Nat"
        lblSum.Top = grdSumWeeks.GetRowDisplayRectangle(1, True).Top + grdSumWeeks.Top - 1
        lblSum.Left = lblBuyingTarget.Left
        lblSum.Height = grdSumWeeks.GetRowDisplayRectangle(1, True).Height
        lblSum.Width = grdTRP.Left - lblSum.Left
        lblSum.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        lblSum.BringToFront()

        lblSum = New System.Windows.Forms.Label
        grpTRP.Controls.Add(lblSum)
        lblSum.AutoSize = False
        lblSum.TextAlign = Drawing.ContentAlignment.MiddleLeft
        lblSum.Text = "Est. reach"
        lblSum.Top = grdSumWeeks.GetRowDisplayRectangle(2, True).Top + grdSumWeeks.Top - 1
        lblSum.Left = lblBuyingTarget.Left
        lblSum.Height = grdSumWeeks.GetRowDisplayRectangle(2, True).Height
        lblSum.Width = grdTRP.Left - lblSum.Left
        lblSum.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        lblSum.BringToFront()

        cmbUniverse.SelectedIndex = 0
        cmbDisplay.SelectedIndex = 0
        cmbFilmChannel.SelectedIndex = 0
        cmbTargets.SelectedIndex = 0

        onlyOneSelectedGrid()

        cmbTargets.SelectedIndex = 0
        cmbUniverse.SelectedIndex = 0
        cmbDisplay.SelectedIndex = 0
        cmbFilmChannel.SelectedIndex = 0
        frmMain.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub tpReach_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpReach.Enter
        Dim kv As KeyValuePair(Of String, Trinity.cKampanj)
        Dim i As Integer
        Dim c As Integer

        grdReach.Columns.Clear()
        grdReach.Rows.Clear()
        grdReach.Font = New Drawing.Font("Segoe UI", 7)
        For Each kv In Campaign.Campaigns
            grdReach.Columns.Add(kv.Key, kv.Key)
            grdReach.Columns(kv.Key).Width = grdReach.Columns(kv.Key).GetPreferredWidth(Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells, True)
            grdReach.Columns(kv.Key).DefaultCellStyle.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Next
        For i = 1 To 5
            grdReach.Rows.Add()
            grdReach.Rows(i - 1).HeaderCell.Value = i & "+"
            grdReach.Rows(i - 1).Height = grdReach.Rows(i - 1).GetPreferredHeight(i - 1, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells, True)
        Next
        grdReach.Rows.Add()
        grdReach.Rows(5).HeaderCell.Value = "OTS:"
        grdReach.Rows(5).Height = grdReach.Rows(5).GetPreferredHeight(5, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells, True)
        grdReach.RowHeadersWidth = 60

        grdReach.Rows.Add()
        grdReach.Rows(6).HeaderCell.Value = "CPP:"
        grdReach.Rows(6).Height = grdReach.Rows(5).GetPreferredHeight(5, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells, True)
        grdReach.RowHeadersWidth = 60

        grdReach.Rows.Add()
        grdReach.Rows(7).HeaderCell.Value = "CPS:"
        grdReach.Rows(7).Height = grdReach.Rows(5).GetPreferredHeight(5, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells, True)
        grdReach.RowHeadersWidth = 60

        grdReach.Rows.Add()
        grdReach.Rows(8).HeaderCell.Value = "Budget:"
        grdReach.Rows(8).Height = grdReach.Rows(5).GetPreferredHeight(5, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells, True)
        grdReach.RowHeadersWidth = 60


        c = 0
        For Each kv In Campaign.Campaigns
            For i = 1 To 5
                If Not Karma Is Nothing Then
                    grdReach.Rows(i - 1).Cells(c).Value = Format(Karma.Campaigns.Item(kv.Key).Reach(0, i), "0.0")
                Else
                    grdReach.Rows(i - 1).Cells(c).Value = "0.0"
                End If
                'kv.Value.ReachTargets(i, "Nat") = Karma.Campaigns.Item(Campaign.Campaigns.Keys(c)).Reach(0, i)
            Next
            If Not Karma Is Nothing Then
                grdReach.Rows(5).Cells(c).Value = Format(kv.Value.TotalTRP / Karma.Campaigns.Item(kv.Key).Reach(0, 1), "0.0")
                grdReach.Rows(6).Cells(c).Value = Format(kv.Value.PlannedMediaNet / kv.Value.TotalTRP, "0.0")
                grdReach.Rows(7).Cells(c).Value = Format(kv.Value.PlannedMediaNet / Karma.Campaigns.Item(kv.Key).Reach(0, 1), "0.0")
                grdReach.Rows(8).Cells(c).Value = Format(kv.Value.PlannedMediaNet, "0.0")
            Else
                grdReach.Rows(5).Cells(c).Value = "0.0"
                grdReach.Rows(6).Cells(c).Value = "0.0"
                grdReach.Rows(7).Cells(c).Value = "0.0"
                grdReach.Rows(8).Cells(c).Value = "0.0"

            End If
            c = c + 1
        Next

        chtKarma.Campaigns = Campaign.Campaigns
        If cmbFreq.SelectedIndex = -1 Then
            cmbFreq.SelectedIndex = 0
        End If
    End Sub

    Private Sub cmbFreq_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFreq.SelectedIndexChanged

        Dim kv As KeyValuePair(Of String, Trinity.cKampanj)
        Dim TotalTRP As Single
        chtKarma.DrawFrequency = cmbFreq.SelectedIndex + 1
        TotalTRP = 0
        For Each kv In Campaign.Campaigns
            If kv.Value.TotalTRP > TotalTRP Then TotalTRP = kv.Value.TotalTRP
        Next
        TotalTRP += (50 - (TotalTRP Mod 50))
        chtKarma.TotalTRP = TotalTRP
    End Sub

    Private Sub cmdCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCalculate.Click
        Dim PeriodStr As String = ""
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim IsUsed As Boolean
        Dim i As Integer
        Dim kv As KeyValuePair(Of String, Trinity.cKampanj)
        Dim c As Integer
        Dim UseSponsorship As Boolean = False
        Dim UseCommercial As Boolean = False

        frmMain.Cursor = Windows.Forms.Cursors.WaitCursor

        If Karma Is Nothing Then
            Karma = New Trinity.cKarma(Campaign)

            If optLastYear.Checked Then
                Dim TmpDate As Long = Date.FromOADate(ActiveCampaign.EndDate).AddYears(-1).ToOADate
                Dim DateDiff As Long

                While Weekday(Date.FromOADate(TmpDate), FirstDayOfWeek.Monday) <> Weekday(Date.FromOADate(ActiveCampaign.EndDate), FirstDayOfWeek.Monday)
                    TmpDate = TmpDate + 1
                End While
                DateDiff = ActiveCampaign.EndDate - TmpDate

                For Each TmpWeek As Trinity.cWeek In ActiveCampaign.Channels(1).BookingTypes(1).Weeks
                    PeriodStr &= Format(Date.FromOADate(TmpWeek.StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate - DateDiff), "ddMMyy") & ","
                Next
            Else
                Dim TmpDate As Long = Karma.KarmaAdedge.getDataRangeTo(Connect.eDataType.mSpot)
                Dim DateDiff As Long

                While Weekday(Date.FromOADate(TmpDate)) <> Weekday(Date.FromOADate(Campaign.EndDate))
                    TmpDate = TmpDate - 1
                End While
                DateDiff = ActiveCampaign.EndDate - TmpDate

                For Each TmpWeek As Trinity.cWeek In ActiveCampaign.Channels(1).BookingTypes(1).Weeks
                    PeriodStr &= Format(Date.FromOADate(TmpWeek.StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate - DateDiff), "ddMMyy") & ","
                Next
            End If

            Karma.ReferencePeriod = PeriodStr

            For Each TmpChan In ActiveCampaign.Channels
                IsUsed = False
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        IsUsed = True
                    End If
                    If TmpBT.IsSponsorship Then
                        UseSponsorship = True
                    Else
                        UseCommercial = True
                    End If
                Next
                If IsUsed Then
                    Karma.Channels.Add(TmpChan.ChannelName)
                    Karma.Weeks = TmpChan.BookingTypes(1).Weeks.Count
                End If
            Next
            Karma.Populate(UseSponsorship, UseCommercial)
        Else
            If optLastYear.Checked Then
                Dim TmpDate As Long = Date.FromOADate(ActiveCampaign.EndDate).AddYears(-1).ToOADate
                Dim DateDiff As Long

                While Weekday(Date.FromOADate(TmpDate), FirstDayOfWeek.Monday) <> Weekday(Date.FromOADate(ActiveCampaign.EndDate), FirstDayOfWeek.Monday)
                    TmpDate = TmpDate + 1
                End While
                DateDiff = ActiveCampaign.EndDate - TmpDate

                For Each TmpWeek As Trinity.cWeek In ActiveCampaign.Channels(1).BookingTypes(1).Weeks
                    PeriodStr &= Format(Date.FromOADate(TmpWeek.StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate - DateDiff), "ddMMyy") & ","
                Next
            Else
                Dim TmpDate As Long = ActiveCampaign.EndDate
                Dim DateDiff As Long

                While TmpDate >= Karma.KarmaAdedge.getDataRangeTo(Connect.eDataType.mSpot)
                    TmpDate = TmpDate - 1
                End While
                DateDiff = ActiveCampaign.EndDate - TmpDate

                For Each TmpWeek As Trinity.cWeek In ActiveCampaign.Channels(1).BookingTypes(1).Weeks
                    PeriodStr &= Format(Date.FromOADate(TmpWeek.StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate - DateDiff), "ddMMyy") & ","
                Next
            End If
        End If

        c = 0

        Dim highestOTS As Single = Single.MinValue
        Dim lowestCPP As Single = Single.MaxValue
        Dim lowestCPS As Single = Single.MaxValue
        Dim lowestBudget As Single = Single.MaxValue

        For Each kv In Campaign.Campaigns


            Karma.Campaigns.Add(kv.Key, kv.Value)
            Karma.Campaigns.Item(kv.Key).Run()
            For i = 1 To 5
                grdReach.Rows(i - 1).Cells(c).Value = Format(Karma.Campaigns.Item(kv.Key).Reach(0, i), "0.0")
                Campaign.Campaigns.Item(kv.Key).ReachGoal(i) = Karma.Campaigns.Item(kv.Key).Reach(0, i)
            Next

            For i = 6 To 10
                Campaign.Campaigns.Item(kv.Key).ReachGoal(i) = Karma.Campaigns.Item(kv.Key).Reach(0, i)
            Next

            If kv.Value.TotalTRP / Karma.Campaigns.Item(kv.Key).Reach(0, 1) > highestOTS Then
                For column As Integer = 0 To c
                    grdReach.Rows(5).Cells(column).Style.BackColor = Color.White
                Next
                highestOTS = kv.Value.TotalTRP / Karma.Campaigns.Item(kv.Key).Reach(0, 1)
                grdReach.Rows(5).Cells(c).Style.BackColor = Color.LightGreen
            End If

            If kv.Value.PlannedMediaNet / kv.Value.TotalTRP < lowestCPP Then
                For column As Integer = 0 To c
                    grdReach.Rows(6).Cells(column).Style.BackColor = Color.White
                Next
                lowestCPP = kv.Value.PlannedMediaNet / kv.Value.TotalTRP
                grdReach.Rows(6).Cells(c).Style.BackColor = Color.LightGreen
            End If

            If kv.Value.PlannedMediaNet / Karma.Campaigns.Item(kv.Key).Reach(0, 1) < lowestCPS Then
                For column As Integer = 0 To c
                    grdReach.Rows(7).Cells(column).Style.BackColor = Color.White
                Next
                lowestCPS = kv.Value.PlannedMediaNet / Karma.Campaigns.Item(kv.Key).Reach(0, 1)
                grdReach.Rows(7).Cells(c).Style.BackColor = Color.LightGreen
            End If

            If kv.Value.PlannedMediaNet < lowestBudget Then
                For column As Integer = 0 To c
                    grdReach.Rows(8).Cells(column).Style.BackColor = Color.White
                Next
                lowestBudget = kv.Value.PlannedMediaNet
                grdReach.Rows(8).Cells(c).Style.BackColor = Color.LightGreen
            End If

            'grdReach.Rows(grdReach.Rows.Count - 1).Cells(c).Value = Format(kv.Value.TotalTRP / Karma.Campaigns.Item(kv.Key).Reach(0, 1), "0.0")
            grdReach.Rows(5).Cells(c).Value = Format(kv.Value.TotalTRP / Karma.Campaigns.Item(kv.Key).Reach(0, 1), "0.0")
            grdReach.Rows(6).Cells(c).Value = Format(kv.Value.PlannedMediaNet / kv.Value.TotalTRP, "### ### kr")
            grdReach.Rows(7).Cells(c).Value = Format(kv.Value.PlannedMediaNet / Karma.Campaigns.Item(kv.Key).Reach(0, 1), "### ### ### kr")
            grdReach.Rows(8).Cells(c).Value = Format(kv.Value.PlannedMediaNet, "### ### ### kr")

            'grdReach.Columns(c).AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells

            c = c + 1
        Next

        tpProfile.Enabled = True
        chtKarma.Karma = Karma
        frmMain.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub optLastYear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optLastYear.CheckedChanged
        dtCustomDate.Visible = False
        Karma = Nothing
        chtKarma.Karma = Nothing
    End Sub

    Private Sub optCustomDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCustomDate.CheckedChanged
        dtCustomDate.Visible = True
        Karma = Nothing
        chtKarma.Karma = Nothing
    End Sub

    Private Sub optLastWeeks_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optLastWeeks.CheckedChanged
        dtCustomDate.Visible = False
        Karma = Nothing
        chtKarma.Karma = Nothing
    End Sub

    'Private Sub grdSumChannels_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSumChannels.CellValueChanged
    '    Dim TRPSum As Single
    '    Dim c As Integer
    '    Dim TmpWeek As Trinity.cWeek

    '    If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then Exit Sub
    '    If e.ColumnIndex = 0 Then
    '        If Not SkipIt Then
    '            TRPSum = 0
    '            For c = 0 To grdTRP.Columns.Count - 1
    '                TmpWeek = grdTRP.Rows(e.RowIndex).Cells(c).Tag
    '                TRPSum = TRPSum + TmpWeek.TRP
    '            Next
    '            For c = 0 To grdTRP.Columns.Count - 1
    '                TmpWeek = grdTRP.Rows(e.RowIndex).Cells(c).Tag
    '                TmpWeek.TRP = grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value * (TmpWeek.TRP / TRPSum)
    '                TmpWeek.TRPControl = True
    '            Next
    '            SkipIt = True
    '            SumBudget()
    '            SkipIt = True
    '            UpdateTRPGrid()
    '            SkipIt = False
    '        End If
    '    ElseIf e.ColumnIndex = 1 Then
    '        If Not SkipIt Then
    '            TRPSum = 0
    '            For c = 0 To grdTRP.Columns.Count - 1
    '                TRPSum = TRPSum + grdTRP.Rows(e.RowIndex).Cells(c).Value
    '            Next
    '            For c = 0 To grdTRP.Columns.Count - 1
    '                TmpWeek = grdTRP.Rows(e.RowIndex - 1).Cells(c).Tag
    '                TmpWeek.TRPBuyingTarget = grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value * (grdTRP.Rows(e.RowIndex).Cells(c).Value / TRPSum)
    '                TmpWeek.TRPControl = True
    '            Next
    '            SkipIt = True
    '            SumBudget()
    '            SkipIt = True
    '            UpdateTRPGrid()
    '            SkipIt = False
    '        End If
    '    End If
    'End Sub

    'Private Sub grdGrandSum_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdGrandSum.CellValueChanged
    '    Dim TRPSum As Single
    '    Dim c As Integer
    '    Dim r As Integer
    '    Dim TmpWeek As Trinity.cWeek

    '    If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then Exit Sub
    '    If Not SkipIt Then
    '        TRPSum = 0
    '        For c = 0 To grdTRP.Columns.Count - 1
    '            For r = 1 To grdTRP.Rows.Count - 1 Step 2
    '                TmpWeek = grdTRP.Rows(r).Cells(c).Tag
    '                TRPSum = TRPSum + TmpWeek.TRP
    '            Next
    '        Next
    '        For c = 0 To grdTRP.Columns.Count - 1
    '            For r = 1 To grdTRP.Rows.Count - 1 Step 2
    '                TmpWeek = grdTRP.Rows(r).Cells(c).Tag
    '                TmpWeek.TRP = grdGrandSum.Rows(0).Cells(0).Value * (TmpWeek.TRP / TRPSum)
    '                TmpWeek.TRPControl = True
    '            Next
    '        Next
    '        SkipIt = True
    '        SumBudget()
    '        SkipIt = True
    '        UpdateTRPGrid()
    '        SkipIt = False
    '    End If
    'End Sub

    'Private Sub grdSumWeeks_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSumWeeks.CellValueChanged
    '    Dim TRPSum As Single
    '    Dim r As Integer
    '    Dim TmpWeek As Trinity.cWeek

    '    If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then Exit Sub
    '    If Not SkipIt Then
    '        TRPSum = 0
    '        For r = 1 To grdTRP.Rows.Count - 1 Step 2
    '            TmpWeek = grdTRP.Rows(r).Cells(e.ColumnIndex).Tag
    '            TRPSum = TRPSum + TmpWeek.TRP
    '        Next
    '        For r = 1 To grdTRP.Rows.Count - 1 Step 2
    '            TmpWeek = grdTRP.Rows(r).Cells(e.ColumnIndex).Tag
    '            If e.RowIndex = 0 Then
    '                TmpWeek.TRP = grdSumWeeks.Rows(0).Cells(e.ColumnIndex).Value * (TmpWeek.TRP / TRPSum)
    '            Else
    '                TmpWeek.TRPAllAdults = grdSumWeeks.Rows(1).Cells(e.ColumnIndex).Value * (TmpWeek.TRP / TRPSum)
    '            End If
    '            TmpWeek.TRPControl = True
    '        Next
    '        SkipIt = True
    '        SumBudget()
    '        SkipIt = True
    '        UpdateTRPGrid()
    '        SkipIt = False
    '    End If
    'End Sub

    Private Sub cmdGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGo.Click
        mnuFindReach.Show(cmdGo, New System.Drawing.Point(0, cmdGo.Height))
    End Sub


    Private Sub mnuFindReachLastWeeks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFindReachLastWeeks.Click
        Dim DateDiff As Integer = Campaign.EndDate - Campaign.InternalAdedge.getDataRangeTo(Connect.eDataType.mSpot)

        While Weekday(Date.FromOADate(Campaign.EndDate - DateDiff)) <> Weekday(Date.FromOADate(Campaign.EndDate))
            DateDiff += 1
        End While
        Dim ReferencePeriod As String = ""
        For Each TmpWeek As Trinity.cWeek In ActiveCampaign.Channels(1).BookingTypes(1).Weeks
            ReferencePeriod &= Format(Date.FromOADate(TmpWeek.StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate - DateDiff), "ddMMyy") & ","
        Next
        FindReach(ReferencePeriod)
    End Sub


    Private Sub mnuFindReachLastYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFindReachLastYear.Click
        Dim DateDiff As Integer = Campaign.EndDate - Date.FromOADate(Campaign.EndDate).AddYears(-1).ToOADate

        While Weekday(Date.FromOADate(Campaign.EndDate - DateDiff)) <> Weekday(Date.FromOADate(Campaign.EndDate))
            DateDiff += 1
        End While
        Dim ReferencePeriod As String = ""
        For Each TmpWeek As Trinity.cWeek In ActiveCampaign.Channels(1).BookingTypes(1).Weeks
            ReferencePeriod &= Format(Date.FromOADate(TmpWeek.StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate - DateDiff), "ddMMyy") & ","
        Next
        FindReach(ReferencePeriod)
    End Sub

    Sub FindReach(ByVal Period As String)
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim IsUsed As Boolean
        Dim UseSponsorship As Boolean = False
        Dim UseCommercial As Boolean = False
        'Dim i As Integer

        frmProgress.pbProgress.Maximum = 100
        frmProgress.Progress = 0
        frmProgress.Status = "Finding spots..."
        frmProgress.Show()

        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        If Karma Is Nothing Then
            Karma = New Trinity.cKarma(Campaign)
            Karma.ReferencePeriod = Period

            For Each TmpChan In ActiveCampaign.Channels
                IsUsed = False
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        IsUsed = True
                    End If
                    If TmpBT.IsSponsorship Then
                        UseSponsorship = True
                    ElseIf TmpBT.BookIt Then
                        UseCommercial = True
                    End If
                Next
                If IsUsed Then
                    Karma.Channels.Add(TmpChan.ChannelName)
                    Karma.Weeks = TmpChan.BookingTypes(1).Weeks.Count
                End If
            Next
            Karma.Populate(UseSponsorship, UseCommercial)
        End If

        frmProgress.Status = "Finding reach..."

        cmdGo.Visible = False

        Karma.Campaigns.Add(cmbCampaigns.Text, Campaign.Campaigns(cmbCampaigns.Text))
        Karma.Campaigns.Item(cmbCampaigns.Text).Reset()
        AddHandler Karma.Campaigns.Item(cmbCampaigns.Text).Progress, AddressOf Karma_PopulateProgress

        If txtReach.Text = "" OrElse txtSteps.Text = "" OrElse txtTolerance.Text = "" Then
            Me.Cursor = Windows.Forms.Cursors.Default
            Windows.Forms.MessageBox.Show("You need to specify a reach goal, TRP- or Budget steps and" & vbCrLf & "a tolerance to proceed.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Exit Sub
        End If
        While (Karma.Campaigns.Item(cmbCampaigns.Text).Reach(0, cmbFF.SelectedIndex + 1) < CSng(txtReach.Text) - CSng(txtTolerance.Text)) Or (Karma.Campaigns.Item(cmbCampaigns.Text).Reach(0, cmbFF.SelectedIndex + 1) > CSng(txtReach.Text) + CSng(txtTolerance.Text))
            Karma.Campaigns.Item(cmbCampaigns.Text).Run()
            If Karma.Campaigns(cmbCampaigns.Text).Reach(100, 1) = 0 Then
                Exit While
            End If
            lblCurrentReach.Text = "Current reach: " & Format(Karma.Campaigns.Item(cmbCampaigns.Text).Reach(0, cmbFF.SelectedIndex + 1), "0.0") & "%"
            System.Windows.Forms.Application.DoEvents()
            If Karma.Campaigns.Item(cmbCampaigns.Text).Reach(0, cmbFF.SelectedIndex + 1) > txtReach.Text + 1 Then
                If cmbTRPBudget.SelectedIndex = 1 Then
                    grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(grdBudget.Columns.Count - 1).Value -= txtSteps.Text
                Else
                    grdGrandSum.Rows(0).Cells(0).Value -= txtSteps.Text
                End If
            ElseIf Karma.Campaigns.Item(cmbCampaigns.Text).Reach(0, cmbFF.SelectedIndex + 1) < txtReach.Text - 1 Then
                If cmbTRPBudget.SelectedIndex = 1 Then
                    grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(grdBudget.Columns.Count - 1).Value += txtSteps.Text
                Else
                    grdGrandSum.Rows(0).Cells(0).Value += txtSteps.Text
                End If
            End If
        End While
        cmdGo.Visible = True
        Me.Cursor = Windows.Forms.Cursors.Default
        frmProgress.Hide()
    End Sub

    Private Sub grdTRP_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdTRP.CellFormatting
        If cmbDisplay.SelectedIndex = 0 Then
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
            Else
                Dim TmpWeek As Trinity.cWeek = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
                If TmpWeek Is Nothing Then Exit Sub
                If TmpWeek.TRPControl Then
                    If grdTRP.Rows(e.RowIndex).Tag.Islocked OrElse TmpWeek.IsLocked Then
                        e.CellStyle = styleNormalDLocked
                    Else
                        e.CellStyle = styleNormalD
                    End If
                Else
                    If grdTRP.Rows(e.RowIndex).Tag.Islocked OrElse TmpWeek.IsLocked Then
                        e.CellStyle = styleNoSetDLocked
                    Else
                        e.CellStyle = styleNoSetD
                    End If
                End If
            End If
        Else
            e.CellStyle = styleCantSetP
        End If
    End Sub

    Private Sub grdTRP_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdTRP.CellValueNeeded
        'updates the grid on a number of events (window activation, mouse movement etc)
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpC As Trinity.cCombination
        Dim TmpWeek As Trinity.cWeek
        Dim SumTarget As Single

        If grdTRP.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            'if we have a single row combination
            TmpC = grdTRP.Rows(e.RowIndex).Tag

            If cmbDisplay.SelectedIndex = 0 Then
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
                            If cmbTargets.SelectedIndex = 0 Then
                                sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP
                            Else
                                sum += (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / (tmpCC.Bookingtype.IndexMainTarget / 100)) * (tmpCC.Bookingtype.IndexSecondTarget / 100)
                            End If
                        Next
                        e.Value = sum
                    End If
                Else
                    If e.RowIndex / 2 = e.RowIndex \ 2 Then
                        Dim sum As Double = 0
                        For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                            sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget
                        Next
                        e.Value = sum
                    Else
                        Dim sum As Double = 0
                        For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                            If cmbTargets.SelectedIndex = 0 Then
                                sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget * (tmpCC.Bookingtype.IndexMainTarget / 100)
                            Else
                                sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget * (tmpCC.Bookingtype.IndexSecondTarget / 100)
                            End If
                        Next
                        e.Value = sum
                    End If
                End If
            ElseIf cmbDisplay.SelectedIndex = 2 Then
                Dim sum As Double = 0
                For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                    If e.RowIndex / 2 = e.RowIndex \ 2 Then
                        sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget / tmpCC.Bookingtype.TotalTRPBuyingTarget
                    Else
                        sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / tmpCC.Bookingtype.TotalTRP
                    End If
                Next

                sum = sum / TmpC.Relations.count

                If Double.IsNaN(sum) Then
                    e.Value = 0
                Else
                    e.Value = sum
                End If
                grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
            Else
                If e.RowIndex / 2 = e.RowIndex \ 2 Then
                    e.Value = "-"
                Else
                    SumTarget = 0
                    For Each TmpChan In ActiveCampaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            If TmpBT.BookIt Then
                                SumTarget = SumTarget + TmpBT.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP
                            End If
                        Next
                    Next

                    If SumTarget <> 0 Then
                        Dim sum As Double = 0
                        For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                            sum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / SumTarget
                        Next
                        e.Value = sum
                    Else
                        e.Value = 0
                    End If
                    grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                End If
            End If
        Else
            'normal version
            TmpBT = grdTRP.Rows(e.RowIndex).Tag
            TmpWeek = grdTRP.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag

            If cmbDisplay.SelectedIndex = 0 Then
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
            ElseIf cmbDisplay.SelectedIndex = 2 Then
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
            Else
                If e.RowIndex / 2 = e.RowIndex \ 2 Then
                    e.Value = "-"
                Else
                    SumTarget = 0
                    For Each TmpChan In ActiveCampaign.Channels
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
        If cmbDisplay.SelectedIndex > 0 Then Exit Sub

        Saved = False
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek
        Dim ScrollLeft As Integer = Me.DisplayRectangle.X

        If grdTRP.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            'a one line combo row
            Dim tmpC As Trinity.cCombination
            tmpC = grdTRP.Rows(e.RowIndex).Tag

            If tmpC.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                If cmbUniverse.SelectedIndex = 0 Then
                    If e.RowIndex / 2 <> e.RowIndex \ 2 Then
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            If cmbTargets.SelectedIndex = 0 Then
                                tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value * tmpCC.Percent)
                            Else
                                tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = ((e.Value / (tmpCC.Bookingtype.IndexSecondTarget / 100)) * (tmpCC.Bookingtype.IndexMainTarget / 100) * tmpCC.Percent)
                            End If
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                        Next
                        grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex))
                    Else
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value * (tmpCC.Bookingtype.IndexMainTarget / 100)) * tmpCC.Percent
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                        Next
                        grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex))
                    End If
                Else
                    If e.RowIndex / 2 <> e.RowIndex \ 2 Then
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value / tmpCC.Bookingtype.IndexMainTarget / 100) * tmpCC.Percent
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                        Next
                        grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex))
                    Else
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget = (e.Value * tmpCC.Percent)
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                        Next
                        grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex))
                    End If
                End If
            Else

                For Each TmpCC As Trinity.cCombinationChannel In tmpC.Relations
                    TmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).NetBudget = (TmpCC.Percent) * 1000000
                    TmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = False
                Next

                Dim sumTRP As Double
                For Each TmpCC As Trinity.cCombinationChannel In tmpC.Relations
                    sumTRP += TmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP
                Next

                If cmbUniverse.SelectedIndex = 0 Then
                    If e.RowIndex / 2 <> e.RowIndex \ 2 Then
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
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
                        Next
                        grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex))
                    Else
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            If sumTRP > 0 Then
                                tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / sumTRP)) * (tmpCC.Bookingtype.IndexMainTarget / 100)
                            Else
                                tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = 0
                            End If
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                        Next
                        grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex))
                    End If
                Else
                    If e.RowIndex / 2 <> e.RowIndex \ 2 Then
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP = (e.Value / tmpCC.Bookingtype.IndexMainTarget / 100) * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / sumTRP)
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                        Next
                        grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex - 1).Cells(e.ColumnIndex))
                    Else
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget = (e.Value * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRP / sumTRP))
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(e.ColumnIndex).HeaderText).TRPControl = True
                        Next
                        grdTRP.InvalidateCell(grdTRP.Rows(e.RowIndex + 1).Cells(e.ColumnIndex))
                    End If
                End If
            End If

        Else
            'normal row
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
            For Each TmpCombo As Trinity.cCombination In ActiveCampaign.Combinations
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
            Next
        End If

        'updates the grids
        grdSumChannels.Invalidate()
        grdSumWeeks.Invalidate()
        grdTRP.InvalidateColumn(e.ColumnIndex)
        grdBudget.Invalidate()
    End Sub

    Private Sub grdTRP_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTRP.GotFocus
        SkipIt = False
    End Sub

    Private Sub tpReach_Layout(ByVal sender As Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles tpReach.Layout
        If grdReach.Rows.Count = 0 Then Exit Sub
        grdReach.Height = (grdReach.Rows.Count) * grdReach.Rows(0).Height + grdReach.ColumnHeadersHeight + 2
        grdReach.Width = 10000
        grdReach.Width = grdReach.GetColumnDisplayRectangle(grdReach.Columns.Count - 1, False).Right + 1
    End Sub

    'Private Sub grdDiscounts_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDiscounts.CellDoubleClick
    '    Dim TmpBT As Trinity.cBookingType
    '    Dim TmpIndex As Trinity.cIndex
    '    Dim TmpWeek As Trinity.cWeek
    '    Dim Idx As Single
    '    Dim r As Integer

    '    If grdDiscounts.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
    '        'start of printing one row combo detials

    '        Dim tmpC As Trinity.cCombination = grdDiscounts.Rows(e.RowIndex).Tag

    '        Dim frmDetails As New frmDetails
    '        'clears the grid
    '        frmDetails.grdDetails.Rows.Clear()
    '        frmDetails.grdDetails.Rows.Add(100)
    '        frmDetails.grdDetails.ForeColor = Color.Black

    '        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
    '            TmpBT = tmpCC.Bookingtype
    '            TmpWeek = tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText)

    '            frmDetails.grdDetails.Rows(r).Cells(0).Value = tmpCC.Bookingtype.ParentChannel.ChannelName

    '            r += 1

    '            'Calculate total index from Pricelist
    '            frmDetails.grdDetails.Rows(r).Cells(0).Value = "Base CPP 30"""

    '            Idx = 1
    '            For Each TmpIndex In TmpBT.Indexes
    '                If TmpIndex.UseThis Then
    '                    If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
    '                        If (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate) Or (TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate And TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
    '                            Idx = Idx * (TmpIndex.Index / 100)
    '                        End If
    '                    End If
    '                End If
    '            Next

    '            ''Print out ratecard CPP
    '            'If TmpWeek.SpotIndex(True) > 0 Then
    '            '    Idx *= TmpWeek.SpotIndex(True) / 100
    '            'End If
    '            Dim TmpCPP As Single = 0
    '            For d As Long = TmpWeek.StartDate To TmpWeek.EndDate
    '                TmpCPP += TmpBT.BuyingTarget.GetCPPForDate(d)
    '            Next
    '            TmpCPP /= (TmpWeek.EndDate - TmpWeek.StartDate + 1)
    '            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpCPP, "C0")

    '            'Print out ratecard indexes
    '            If TmpBT.RatecardCPPIsGross Then
    '                r += 1
    '                For Each TmpIndex In TmpBT.BuyingTarget.Indexes
    '                    If TmpIndex.UseThis AndAlso TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
    '                        If (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate) Or (TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate And TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then

    '                            frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
    '                            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
    '                            r = r + 1
    '                        End If
    '                    End If
    '                Next
    '            End If

    '            'Print out user created indexes on Gross CPP
    '            r += 1
    '            For Each TmpIndex In TmpBT.Indexes
    '                If TmpIndex.UseThis Then
    '                    If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
    '                        If (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate) Or (TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate And TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then

    '                            frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
    '                            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
    '                            r = r + 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            frmDetails.grdDetails.Rows(r).Cells(1).Value = "Spot index"
    '            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.SpotIndex(True), "N1")
    '            r += 1
    '            frmDetails.grdDetails.Rows(r).Cells(2).Value = "------------"
    '            r = r + 1
    '            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.GrossCPP, "C0")
    '            r = r + 2

    '            'Print out the Discount, Net CPT or Net CPP depending on wich was enterd by the user
    '            If TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eDiscount Then
    '                frmDetails.grdDetails.Rows(r).Cells(0).Value = "Discount"
    '                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.Discount, "P1")
    '            ElseIf TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPT Then
    '                frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPT"
    '                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.NetCPT, "N1")
    '                r = r + 1
    '                frmDetails.grdDetails.Rows(r).Cells(0).Value = "Universe"
    '                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.getUniSizeUni(TmpWeek.StartDate), "N0")
    '            ElseIf TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPP Then
    '                frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPP"
    '                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.NetCPP, "N1")
    '            End If
    '            r = r + 1

    '            'Print out indexes that is on Net CPP or TRP
    '            For Each TmpIndex In TmpBT.Indexes
    '                If TmpIndex.UseThis Then
    '                    If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP Then
    '                        If (TmpIndex.FromDate.ToOADate >= TmpWeek.StartDate And TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate) Or (TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate <= TmpWeek.EndDate) Then
    '                            frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
    '                            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
    '                            r = r + 1
    '                        ElseIf TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate Then
    '                            frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
    '                            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
    '                            r = r + 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            For Each TmpIndex In TmpBT.Indexes
    '                If TmpIndex.UseThis Then
    '                    If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eTRP Then
    '                        If (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate) Or (TmpIndex.FromDate.ToOADate >= TmpWeek.StartDate And TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate) Then

    '                            frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
    '                            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1") & " (TRP)"
    '                            r = r + 1
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            frmDetails.grdDetails.Rows(r).Cells(2).Value = "------------"
    '            r = r + 1

    '            'Print out final 30 sec Net CPP
    '            frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPP 30"""
    '            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.NetCPP30(), "C1")

    '            r = r + 2
    '            frmDetails.grdDetails.Rows(r).Cells(1).Value = "Spot index"
    '            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.SpotIndex, "N1")

    '            r = r + 2
    '            frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPP"
    '            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.NetCPP, "C1")

    '            r += 1
    '            frmDetails.grdDetails.Rows(r).Cells(0).Value = "------------"
    '            frmDetails.grdDetails.Rows(r).Cells(2).Value = "------------"

    '            r += 2

    '        Next

    '        While frmDetails.grdDetails.Rows.Count > r + 1
    '            frmDetails.grdDetails.Rows.Remove(frmDetails.grdDetails.Rows(frmDetails.grdDetails.Rows.Count - 1))
    '        End While

    '        frmDetails.ShowDialog()

    '        'end of one row combo details
    '    Else
    '        'start of printing normal details
    '        TmpBT = grdDiscounts.Rows(e.RowIndex).Tag
    '        TmpWeek = grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag

    '        Dim frmDetails As New frmDetails
    '        'clears the grid
    '        frmDetails.grdDetails.Rows.Clear()
    '        frmDetails.MaximumSize = New Size(frmDetails.Width, System.Windows.Forms.Screen.GetBounds(Me).Height * 0.8)
    '        frmDetails.grdDetails.Rows.Add(100)
    '        frmDetails.grdDetails.ForeColor = Color.Black
    '        frmDetails.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen

    '        'Calculate total index from Pricelist
    '        frmDetails.grdDetails.Rows(0).Cells(0).Value = "Base CPP 30"""
    '        r = 0
    '        Idx = 1
    '        For Each TmpIndex In TmpBT.Indexes
    '            If TmpIndex.UseThis Then
    '                If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
    '                    If (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate) Or (TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate And TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
    '                        Idx = Idx * (TmpIndex.Index / 100)
    '                    End If
    '                End If
    '            End If
    '        Next
    '        ''Print out ratecard CPP
    '        'If TmpWeek.SpotIndex(True) > 0 Then
    '        '    Idx *= TmpWeek.SpotIndex(True) / 100
    '        'End If
    '        Dim TmpCPP As Single = 0
    '        For d As Long = TmpWeek.StartDate To TmpWeek.EndDate
    '            TmpCPP += TmpBT.BuyingTarget.GetCPPForDate(d)
    '        Next
    '        TmpCPP /= (TmpWeek.EndDate - TmpWeek.StartDate + 1)
    '        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpCPP, "C0")

    '        'Print out ratecard indexes
    '        r += 1
    '        For Each TmpIndex In TmpBT.BuyingTarget.Indexes
    '            If TmpIndex.UseThis AndAlso TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
    '                If (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate) Or (TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate And TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then

    '                    frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
    '                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
    '                    r = r + 1
    '                End If
    '            End If
    '        Next

    '        'Print out user created indexes on Gross CPP
    '        r += 1
    '        For Each TmpIndex In TmpBT.Indexes
    '            If TmpIndex.UseThis Then
    '                If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
    '                    If (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate) Or (TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate And TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then

    '                        frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
    '                        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
    '                        r = r + 1
    '                    End If
    '                End If
    '            End If
    '        Next
    '        frmDetails.grdDetails.Rows(r).Cells(1).Value = "Spot index"
    '        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.SpotIndex(True), "N1")
    '        r += 1
    '        frmDetails.grdDetails.Rows(r).Cells(2).Value = "------------"
    '        r = r + 1
    '        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.GrossCPP, "C0")
    '        r = r + 2

    '        'Print out the Dicount, Net CPT or Net CPP depending on wich was enterd by the user
    '        If TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eDiscount Then
    '            frmDetails.grdDetails.Rows(r).Cells(0).Value = "Discount"
    '            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.Discount, "P1")
    '        ElseIf TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPT Then
    '            frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPT"
    '            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.NetCPT, "N1")
    '            r = r + 1
    '            frmDetails.grdDetails.Rows(r).Cells(0).Value = "Universe"
    '            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.getUniSizeUni(TmpWeek.StartDate), "N0")
    '        ElseIf TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPP Then
    '            frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPP"
    '            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.NetCPP, "N1")
    '        End If
    '        r = r + 1

    '        'Print out indexes that is on Net CPP or TRP
    '        For Each TmpIndex In TmpBT.Indexes
    '            If TmpIndex.UseThis Then
    '                If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP Then
    '                    If (TmpIndex.FromDate.ToOADate >= TmpWeek.StartDate And TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate) Or (TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate <= TmpWeek.EndDate) Then
    '                        frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
    '                        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
    '                        r = r + 1
    '                    ElseIf TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate Then
    '                        frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
    '                        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
    '                        r = r + 1
    '                    End If
    '                End If
    '            End If
    '        Next
    '        For Each TmpIndex In TmpBT.Indexes
    '            If TmpIndex.UseThis Then
    '                If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eTRP Then
    '                    If (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate) Or (TmpIndex.FromDate.ToOADate >= TmpWeek.StartDate And TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate) Then

    '                        frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
    '                        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1") & " (TRP)"
    '                        r = r + 1
    '                    End If
    '                End If
    '            End If
    '        Next
    '        frmDetails.grdDetails.Rows(r).Cells(2).Value = "------------"
    '        r = r + 1
    '        'Print out final 30 sec Net CPP
    '        frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPP 30"""
    '        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.NetCPP30(), "C1")

    '        r = r + 2
    '        frmDetails.grdDetails.Rows(r).Cells(1).Value = "Spot index"
    '        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.SpotIndex, "N1")

    '        r = r + 2
    '        frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPP"
    '        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.NetCPP, "C1")

    '        While frmDetails.grdDetails.Rows.Count > r + 1
    '            frmDetails.grdDetails.Rows.Remove(frmDetails.grdDetails.Rows(frmDetails.grdDetails.Rows.Count - 1))
    '        End While

    '        frmDetails.ShowDialog()

    '        'end of printing normal details
    '    End If

    'End Sub

    Private Sub lblCTC_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCTC.DoubleClick
        Dim TmpCost As Trinity.cCost
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim r As Integer
        Dim SumUnit As Decimal
        Dim UnitStr As String = ""

        frmDetails.grdDetails.Rows.Clear()
        frmDetails.grdDetails.Rows.Add(50)
        frmDetails.grdDetails.Rows(0).Cells(0).Value = "Gross"
        frmDetails.grdDetails.Rows(0).Cells(2).Value = Format(ActiveCampaign.PlannedGross, "##,##0 kr")

        r = 1
        For Each TmpCost In ActiveCampaign.Costs
            If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnRatecard Then
                    frmDetails.grdDetails.Rows(r).Cells(0).Value = TmpCost.CostName
                    frmDetails.grdDetails.Rows(r).Cells(1).Value = Format(TmpCost.Amount, "P2")
                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpCost.Amount * ActiveCampaign.PlannedGross, "##,##0 kr")
                    frmDetails.grdDetails.Rows(r).Cells(0).Style = styleBlue
                    frmDetails.grdDetails.Rows(r).Cells(1).Style = styleBlue
                    frmDetails.grdDetails.Rows(r).Cells(2).Style = styleBlue
                    r = r + 1
                End If
            End If
        Next

        frmDetails.grdDetails.Rows(r).Cells(0).Value = "Media Net"
        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(ActiveCampaign.PlannedMediaNet, "##,##0 kr")
        r += 1
        For Each TmpCost In ActiveCampaign.Costs
            If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnMediaNet Then
                    frmDetails.grdDetails.Rows(r).Cells(0).Value = TmpCost.CostName
                    frmDetails.grdDetails.Rows(r).Cells(1).Value = Format(TmpCost.Amount, "P2")
                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpCost.Amount * ActiveCampaign.PlannedMediaNet, "##,##0 kr")
                    frmDetails.grdDetails.Rows(r).Cells(0).Style.ForeColor = Drawing.Color.Blue
                    frmDetails.grdDetails.Rows(r).Cells(1).Style.ForeColor = Drawing.Color.Blue
                    frmDetails.grdDetails.Rows(r).Cells(2).Style.ForeColor = Drawing.Color.Blue
                    r = r + 1
                End If
            End If
        Next
        frmDetails.grdDetails.Rows(r).Cells(0).Style.ForeColor = Drawing.Color.Red
        frmDetails.grdDetails.Rows(r).Cells(1).Style.ForeColor = Drawing.Color.Red
        frmDetails.grdDetails.Rows(r).Cells(2).Style.ForeColor = Drawing.Color.Red
        frmDetails.grdDetails.Rows(r).Cells(0).Value = "Commission"
        If ActiveCampaign.PlannedMediaNet = 0 Then
            frmDetails.grdDetails.Rows(r).Cells(1).Value = "0,0%"
        Else
            frmDetails.grdDetails.Rows(r).Cells(1).Value = Format(-(ActiveCampaign.EstimatedCommission / ActiveCampaign.PlannedMediaNet), "0.0%")
        End If
        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(-ActiveCampaign.EstimatedCommission, "##,##0 kr")
        r = r + 1
        frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net"
        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(ActiveCampaign.PlannedNet, "##,##0 kr")
        r = r + 1
        For Each TmpCost In ActiveCampaign.Costs
            If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNet Then
                    frmDetails.grdDetails.Rows(r).Cells(0).Value = TmpCost.CostName
                    frmDetails.grdDetails.Rows(r).Cells(1).Value = Format(TmpCost.Amount, "0.0%")
                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpCost.Amount * ActiveCampaign.PlannedNet, "##,##0 kr")
                    frmDetails.grdDetails.Rows(r).Cells(0).Style.ForeColor = Drawing.Color.Blue
                    frmDetails.grdDetails.Rows(r).Cells(1).Style.ForeColor = Drawing.Color.Blue
                    frmDetails.grdDetails.Rows(r).Cells(2).Style.ForeColor = Drawing.Color.Blue
                    r = r + 1
                End If
            ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeFixed Then
                frmDetails.grdDetails.Rows(r).Cells(0).Value = TmpCost.CostName
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpCost.Amount, "##,##0 kr")
                frmDetails.grdDetails.Rows(r).Cells(0).Style.ForeColor = Drawing.Color.Blue
                frmDetails.grdDetails.Rows(r).Cells(2).Style.ForeColor = Drawing.Color.Blue
                r = r + 1
            ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePerUnit Then
                SumUnit = 0
                If TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots Then
                    For Each TmpChan In ActiveCampaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            SumUnit = SumUnit + TmpBT.EstimatedSpotCount * TmpCost.Amount
                        Next
                    Next
                    UnitStr = " / Spot"
                ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP Then
                    For Each TmpChan In ActiveCampaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            SumUnit = SumUnit + TmpBT.TotalTRPBuyingTarget * TmpCost.Amount
                        Next
                    Next
                    UnitStr = " / TRP"
                ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP Then
                    For Each TmpChan In ActiveCampaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            SumUnit = SumUnit + TmpBT.TotalTRP * TmpCost.Amount
                        Next
                    Next
                    UnitStr = " / TRP"
                ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnWeeks Then
                    UnitStr = " / Week"
                    SumUnit = ActiveCampaign.Channels(1).BookingTypes(1).Weeks.Count * TmpCost.Amount
                End If
                frmDetails.grdDetails.Rows(r).Cells(0).Value = TmpCost.CostName
                frmDetails.grdDetails.Rows(r).Cells(1).Value = Format(TmpCost.Amount, "0 kr") & UnitStr
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(SumUnit, "##,##0 kr")
                frmDetails.grdDetails.Rows(r).Cells(0).Style.ForeColor = Drawing.Color.Blue
                frmDetails.grdDetails.Rows(r).Cells(1).Style.ForeColor = Drawing.Color.Blue
                frmDetails.grdDetails.Rows(r).Cells(2).Style.ForeColor = Drawing.Color.Blue
                r = r + 1
            End If
        Next
        frmDetails.grdDetails.Rows(r).Cells(0).Value = "NetNet"
        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(ActiveCampaign.PlannedNetNet, "##,##0 kr")
        r = r + 1
        For Each TmpCost In ActiveCampaign.Costs
            If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNetNet Then
                    frmDetails.grdDetails.Rows(r).Cells(0).Value = TmpCost.CostName
                    frmDetails.grdDetails.Rows(r).Cells(1).Value = Format(TmpCost.Amount, "0.0%")
                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpCost.Amount * ActiveCampaign.PlannedNetNet, "##,##0 kr")
                    frmDetails.grdDetails.Rows(r).Cells(0).Style.ForeColor = Drawing.Color.Blue
                    frmDetails.grdDetails.Rows(r).Cells(1).Style.ForeColor = Drawing.Color.Blue
                    frmDetails.grdDetails.Rows(r).Cells(2).Style.ForeColor = Drawing.Color.Blue
                    r = r + 1
                End If
            End If
        Next
        frmDetails.grdDetails.Rows(r).Cells(0).Value = "CTC"
        frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(ActiveCampaign.PlannedTotCTC, "##,##0 kr")

        While frmDetails.grdDetails.Rows.Count > r + 1
            frmDetails.grdDetails.Rows.Remove(frmDetails.grdDetails.Rows(frmDetails.grdDetails.Rows.Count - 1))
        End While
        frmDetails.ShowDialog()

    End Sub

    Private Sub tpProfile_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpProfile.Enter
        Dim i As Integer
        Dim ProfileTRP(0 To 13) As Single

        'only happens when changing tab pages
        If Not sender Is cmbProfileCampaign Then
            cmbProfileCampaign.Items.Clear()
            For Each kv As KeyValuePair(Of String, Trinity.cKampanj) In Campaign.Campaigns
                cmbProfileCampaign.Items.Add(kv.Key)
            Next
            cmbProfileCampaign.SelectedIndex = cmbCampaigns.SelectedIndex
        End If

        chtProfile.Target = ActiveCampaign.MainTarget

        'only once per campaign
        If Karma.Campaigns.Item(cmbProfileCampaign.Text).TRPProfile(1) = 0 Then
            Karma.Campaigns.Item(cmbProfileCampaign.Text).CalculateProfile()
        End If
        'For i = 1 To 14
        '    ProfileTRP(i - 1) = ProfileTRP(i - 1) + Karma.Campaigns.Item(cmbProfileCampaign.Text).TRPProfile(i)
        'Next
        For i = 0 To 13
            chtProfile.AgeTRP(i) = Karma.Campaigns.Item(cmbProfileCampaign.Text).TRPProfile(i + 1)
        Next
    End Sub

    Private Sub cmbProfileCampaign_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProfileCampaign.SelectedIndexChanged
        If tabLab.SelectedTab Is tpProfile Then
            tpProfile_Enter(cmbProfileCampaign, New EventArgs)
        End If
    End Sub

    Private Sub tabLab_SelectedIndexChanging(ByVal sender As Object, ByVal e As TabSelectionChangingArgs) Handles tabLab.SelectedIndexChanging
        If Not tabLab.TabPages(e.NewTabIndex).Enabled Then
            e.Cancel = True
        End If
    End Sub

    Private Sub cmdDeleteCampaign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteCampaign.Click
        If cmbCampaigns.SelectedIndex < 0 Then
            System.Windows.Forms.MessageBox.Show("No campaign selected.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Exit Sub
        End If
        If Windows.Forms.MessageBox.Show("Are you sure you want to delete this campaign?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OKCancel, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If
        Campaign.Campaigns.Remove(cmbCampaigns.Text)
        cmbCampaigns.Items.RemoveAt(cmbCampaigns.SelectedIndex)
    End Sub

    Private Sub grdAV_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdAV.CellValuePushed
        If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then Exit Sub

        'the combination object is set to the tag of the first cell
        If grdAV.Rows(e.RowIndex).Cells(0).Tag Is Nothing Then
            Dim TmpAV As Trinity.cAddedValue = grdAV.Rows(e.RowIndex).Tag
            TmpAV.Amount(e.ColumnIndex + 1) = e.Value
        Else
            Dim comb As Trinity.cCombination = grdAV.Rows(e.RowIndex).Cells(0).Tag
            For Each tmpCC As Trinity.cCombinationChannel In comb.Relations
                tmpCC.Bookingtype.AddedValues(grdAV.Rows(e.RowIndex).Tag.ID).Amount(e.ColumnIndex + 1) = e.Value
            Next
        End If

        grdDiscounts.Invalidate()
        grdBudget.Invalidate()
        grdTRP.Invalidate()
    End Sub

    Private Sub grdAV_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdAV.CellValueNeeded
        If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then Exit Sub

        'the AV is loaded in the tag on combinations aswell
        'since the combinated channels have the same AV we can simply show it
        Dim TmpAV As Trinity.cAddedValue = grdAV.Rows(e.RowIndex).Tag
        e.Value = Format(TmpAV.Amount(e.ColumnIndex + 1) / 100, "P1")
    End Sub

    Private Sub grdFilms_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdFilms.CellValueNeeded
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
            If e.ColumnIndex < grdFilms.ColumnCount - 1 Then
                e.Value = TmpBT.Weeks(e.ColumnIndex + 1).Films(TmpFilm.Name).Share(Show) / 100
            Else
                Dim TRP As Single = 0
                For i As Integer = 1 To TmpBT.Weeks.Count
                    TRP += (TmpBT.Weeks(i).Films(TmpFilm.Name).Share(Show) / 100) * TmpBT.Weeks(i).TRP
                Next
                e.Value = TRP / TotTRP
            End If
        ElseIf Not tmpC Is Nothing Then
            'one line combo
            For i As Integer = 1 To tmpC.Relations(1).Bookingtype.Weeks.Count
                For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                    TotTRP += tmpCC.Bookingtype.Weeks(i).TRP
                Next
            Next
            If e.ColumnIndex < grdFilms.ColumnCount - 1 Then
                Dim LastValue As Single = tmpC.Relations(1).Bookingtype.Weeks(e.ColumnIndex + 1).Films(TmpFilm.Name).Share
                Dim NotSame As Boolean = False
                For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                    If LastValue <> tmpCC.Bookingtype.Weeks(e.ColumnIndex + 1).Films(TmpFilm.Name).Share Then
                        NotSame = True
                    End If
                Next
                grdFilms.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = NotSame
                e.Value = tmpC.Relations(1).Bookingtype.Weeks(e.ColumnIndex + 1).Films(TmpFilm.Name).Share(Show) / 100
            Else
                Dim TRP As Single = 0
                For i As Integer = 1 To tmpC.Relations(1).Bookingtype.Weeks.Count
                    For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                        TRP += (tmpCC.Bookingtype.Weeks(i).Films(TmpFilm.Name).Share(Show) / 100) * tmpCC.Bookingtype.Weeks(i).TRP
                    Next
                Next
                e.Value = TRP / TotTRP
            End If
            'end one line combo
        Else
            If e.ColumnIndex < grdFilms.ColumnCount - 1 Then
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
                If TotTRP > 0 Then
                    e.Value = TRP / TotTRP
                Else
                    e.Value = 0
                End If
            End If
        End If
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
            Exit Sub
        Else
            If Not cmbFilmChannel.SelectedItem.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                TmpBT = cmbFilmChannel.SelectedItem
            End If
        End If


        'deletes the % ibn the string if there is one present
        strTemp = e.Value
        If e.Value Is Nothing Then strTemp = "0"
        strTemp = strTemp.Trim("%")

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


        'update all the grids since the costs and TRP is changed it you change the films
        grdFilms.Invalidate()
        grdDiscounts.Invalidate()
        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
        grdBudget.Invalidate()
        grdSumChannels.Invalidate()
        ColorFilmGrid() 'if the films show % dont sum up to 100 % they are shown in red
    End Sub

    Private Sub btnTRP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTRP.CheckedChanged, btnBudget.CheckedChanged
        If sender Is btnTRP Then
            grpFilms.Text = "Films (% of TRPs)"
        Else
            grpFilms.Text = "Films (% of Budget)"
        End If
        grdFilms.Invalidate()
    End Sub

    Private Sub grdIndex_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdIndex.CellValueNeeded
        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        If grdIndex.ForeColor = Drawing.Color.LightGray Then Exit Sub

        If grdIndex.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cBookingType" Then
            Dim TmpBT As Trinity.cBookingType = grdIndex.Rows(e.RowIndex).Tag
            If e.ColumnIndex = 0 Then
                e.Value = TmpBT.IndexMainTarget
            ElseIf e.ColumnIndex = 1 Then
                e.Value = TmpBT.IndexSecondTarget
            ElseIf e.ColumnIndex = 2 Then
                e.Value = TmpBT.IndexAllAdults
            End If
        Else
            Dim TmpC As Trinity.cCombination = grdIndex.Rows(e.RowIndex).Tag
            If e.ColumnIndex = 0 Then
                e.Value = TmpC.IndexMainTarget
            ElseIf e.ColumnIndex = 1 Then
                e.Value = TmpC.IndexSecondTarget
            ElseIf e.ColumnIndex = 2 Then
                e.Value = TmpC.IndexAllAdults
            End If
        End If
    End Sub

    Private Sub grdIndex_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdIndex.CellValuePushed
        Saved = False
        Dim TmpBT As Trinity.cBookingType
        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        If grdIndex.ForeColor = Drawing.Color.LightGray Then Exit Sub

        If grdIndex.Rows(e.RowIndex).Cells(0).Tag Is Nothing Then
            TmpBT = grdIndex.Rows(e.RowIndex).Tag
            If e.ColumnIndex = 0 Then
                TmpBT.IndexMainTarget = e.Value
            ElseIf e.ColumnIndex = 1 Then
                TmpBT.IndexSecondTarget = e.Value
            ElseIf e.ColumnIndex = 2 Then
                TmpBT.IndexAllAdults = e.Value
            End If
        Else
            If e.ColumnIndex = 0 Then
                For Each tmpCC As Trinity.cCombinationChannel In grdIndex.Rows(e.RowIndex).Cells(0).Tag.relations
                    tmpCC.Bookingtype.IndexMainTarget = e.Value
                Next
            ElseIf e.ColumnIndex = 1 Then
                For Each tmpCC As Trinity.cCombinationChannel In grdIndex.Rows(e.RowIndex).Cells(0).Tag.relations
                    tmpCC.Bookingtype.IndexSecondTarget = e.Value
                Next
            ElseIf e.ColumnIndex = 2 Then
                For Each tmpCC As Trinity.cCombinationChannel In grdIndex.Rows(e.RowIndex).Cells(0).Tag.relations
                    tmpCC.Bookingtype.IndexAllAdults = e.Value
                Next
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



    'Private Sub grdDiscounts_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdDiscounts.CellValueNeeded

    '    'Begin block that calculates averages, etc in the "Total" column
    '    '===============================================================
    '    If e.ColumnIndex = grdDiscounts.Columns.Count - 1 Then
    '        Select Case e.RowIndex Mod 4
    '            'all first rows of the channels, the discount
    '            Case Is = 0
    '                Dim grossbudget As Double = 0
    '                Dim netbudget As Double = 0
    '                If grdDiscounts.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
    '                    Dim discount As Double = 0
    '                    Dim count As Integer = 0

    '                    Dim tmpc As Trinity.cCombination = grdDiscounts.Rows(e.RowIndex).Tag
    '                    For Each tmpCC As Trinity.cCombinationChannel In tmpc.Relations
    '                        For Each tmpWeek As Trinity.cWeek In tmpCC.Bookingtype.Weeks
    '                            grossbudget += tmpWeek.GrossBudget
    '                            netbudget += tmpWeek.NetBudget
    '                        Next
    '                    Next

    '                    e.Value = 1 - (netbudget / grossbudget)
    '                Else

    '                    Dim TmpBT As Trinity.cBookingType = DirectCast(grdDiscounts.Rows(e.RowIndex).Tag, Trinity.cBookingType)
    '                    For Each tmpWeek As Trinity.cWeek In TmpBT.Weeks
    '                        grossbudget += tmpWeek.GrossBudget
    '                        netbudget += tmpWeek.NetBudget
    '                    Next
    '                    e.Value = 1 - (netbudget / grossbudget)
    '                End If
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "P1"
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue

    '            Case Is = 1 'all second rows of the channels, the NetCPP30

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

    '            Case Is = 2 'all third rows of the channels, the actual CPP buying target
    '                'If grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
    '                Dim trps As Double = 0
    '                Dim netbudget As Double = 0

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

    '            Case Is = 3 'all fourth rows of the channels, the index
    '                Exit Sub
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

    '        'End block that calculates averages, etc in the "Total" column
    '        '===============================================================


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
    '                    Dim _trp30 As Single = 0
    '                    Dim _netBudget As Single = 0
    '                    For Each tmpCC As Trinity.cCombinationChannel In tmpc.Relations
    '                        count += 1
    '                        netCPP30 += (tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).NetCPP30(True) * tmpCC.Percent)
    '                        _trp30 += tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget / (tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).SpotIndex / 100)
    '                        _netBudget += tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).NetBudget
    '                    Next
    '                    netCPP30 = _netBudget / _trp30
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
    '                If grdTRP.Rows.Count = 0 Then Exit Sub

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
    '                            e.Value = "-"
    '                        End If
    '                    Else
    '                        e.Value = DirectCast(grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag, Trinity.cWeek).NetCPP / (DirectCast(grdDiscounts.Rows(e.RowIndex).Tag, Trinity.cBookingType).IndexMainTarget / 100)
    '                    End If
    '                End If

    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "N1"
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    '                grdDiscounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue

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

    Private Sub grdDiscounts_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdDiscounts.CellFormatting
        e.CellStyle = Trinity.AllocateFunctions.ApplyDiscountCellFormat(e.CellStyle, e.RowIndex, e.ColumnIndex)
    End Sub

    Private Sub grdDiscounts_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdDiscounts.CellValueNeeded
        e.Value = Trinity.AllocateFunctions.GetDiscountCellValue(grdDiscounts.Rows(e.RowIndex).Tag, e.RowIndex, grdDiscounts.Columns(e.ColumnIndex).HeaderText)
    End Sub

    Private Sub grdDiscounts_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDiscounts.CellDoubleClick

        If e.ColumnIndex = grdDiscounts.ColumnCount - 1 Then Exit Sub
        Trinity.AllocateFunctions.ShowDiscountWindow(grdDiscounts.Rows(e.RowIndex).Tag, grdDiscounts.Columns(e.ColumnIndex).HeaderText)

    End Sub

    Private Sub grdBudget_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdBudget.CellMouseClick
        If e.RowIndex = -1 Then Exit Sub
        If e.ColumnIndex < 2 Then Exit Sub

        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim mnu As New System.Windows.Forms.ContextMenuStrip
            Dim sendItem As Object

            If e.ColumnIndex < grdBudget.ColumnCount - 1 Then
                Dim strWeek As String = grdBudget.Columns(e.ColumnIndex).HeaderText

                'we can lock the weeks so they are not included when you fex set a total budget
                With DirectCast(mnu.Items.Add("Lock/Unlock week " & grdBudget.Columns(e.ColumnIndex).HeaderText, Nothing, AddressOf LockWeek), System.Windows.Forms.ToolStripMenuItem)
                    .Tag = grdBudget.Columns(e.ColumnIndex).HeaderText
                End With
            End If

            If e.RowIndex < grdBudget.RowCount - 1 Then
                sendItem = grdBudget.Rows(e.RowIndex).Tag
                With DirectCast(mnu.Items.Add("Lock/Unlock channel: " & sendItem.ToString, Nothing, AddressOf LockChannel), System.Windows.Forms.ToolStripMenuItem)
                    .Tag = sendItem
                End With
            End If

            Dim item As System.Windows.Forms.ToolStripItem = mnu.Items.Add("Copy to clipboard", Nothing, AddressOf BudgettoClipboard)
            item.Tag = "CopyBudget"
            item.Name = "CopyBudget"

            mnu.Show(Me.ParentForm, New System.Drawing.Point(MousePosition.X, MousePosition.Y - 15))
        End If
    End Sub

    Private Sub grdBudget_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdBudget.CellValueNeeded
        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        If grdSumChannels.RowCount = 0 Then Exit Sub

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

                'we sum up the total
                For r = 0 To grdBudget.Rows.Count - 2

                    'goes trough the rows and sumarize them
                    If grdBudget.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                        For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(r).Tag.relations
                            For Each TmpWeek In tmpCC.Bookingtype.Weeks
                                SumGrandBudget += TmpWeek.NetBudget
                            Next
                        Next
                    Else
                        Dim TmpBT As Trinity.cBookingType
                        TmpBT = grdBudget.Rows(r).Tag
                        For Each TmpWeek In TmpBT.Weeks
                            SumGrandBudget += TmpWeek.NetBudget
                        Next
                    End If
                Next

                'sum up the channel/media
                'check if it is a combination
                If grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(e.RowIndex).Tag.relations
                        For Each TmpWeek In tmpCC.Bookingtype.Weeks
                            SumBudget += TmpWeek.NetBudget
                        Next
                    Next
                Else
                    Dim TmpBT As Trinity.cBookingType = grdBudget.Rows(e.RowIndex).Tag
                    For Each week As Trinity.cWeek In TmpBT.Weeks
                        SumBudget += week.NetBudget
                    Next
                End If
                If SumGrandBudget > 0 Then
                    e.Value = SumBudget / SumGrandBudget
                Else
                    e.Value = 0
                End If
            End If
        ElseIf e.ColumnIndex = grdBudget.Columns.Count - 1 Then
            'The summary to the right
            Dim sum As Double = 0
            Dim r As Integer = e.RowIndex
            Dim bolCTC As Boolean = False

            If cmbDisplay.SelectedIndex = 0 Then
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
                'sets the sum for the CTC label (Cost to Client)
                lblCTC.Left = grdBudget.Right - lblCTC.Width
                lblCTCLabel.Left = lblCTC.Left - lblCTCLabel.Width
                lblCTC.Text = Format(ActiveCampaign.PlannedTotCTC, "C0")
            End If

            e.Value = sum
        Else

            'the weekly budgets
            If e.RowIndex = grdBudget.Rows.Count - 1 Then

                If cmbDisplay.SelectedIndex = 0 Then
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNormal
                Else
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleCantSetN0
                End If

                Dim sum As Double = 0
                For r As Integer = 0 To grdBudget.Rows.Count - 2
                    sum += grdBudget.Rows(r).Cells(e.ColumnIndex).Value
                Next
                e.Value = sum
            Else
                If grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    Dim sum As Double
                    For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(e.RowIndex).Tag.relations
                        sum += tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).NetBudget
                        If cmbDisplay.SelectedIndex = 0 Then
                            If tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).TRPControl Then
                                If tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).IsLocked OrElse tmpCC.Bookingtype.IsLocked Then
                                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNoSetLocked
                                Else
                                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNoSet
                                End If
                            Else
                                If tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).IsLocked OrElse tmpCC.Bookingtype.IsLocked Then
                                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNormalLocked
                                Else
                                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNormal
                                End If
                            End If
                        Else
                            grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleCantSetN0
                        End If

                    Next

                    e.Value = sum
                Else
                    Dim TmpWeek As Trinity.cWeek = grdBudget.Rows(e.RowIndex).Tag.weeks(grdBudget.Columns(e.ColumnIndex).HeaderText)
                    Dim TmpBT As Trinity.cBookingType = grdBudget.Rows(e.RowIndex).Tag

                    If cmbDisplay.SelectedIndex = 0 Then
                        If TmpWeek.TRPControl Then
                            If TmpWeek.IsLocked OrElse TmpBT.IsLocked Then
                                grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNoSetLocked
                            Else
                                grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNoSet
                            End If

                        Else
                            If TmpWeek.IsLocked OrElse TmpBT.IsLocked Then
                                grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNormalLocked
                            Else
                                grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleNormal
                            End If
                        End If
                    Else
                        grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = styleCantSetN0
                    End If
                    e.Value = TmpWeek.NetBudget
                End If
            End If
        End If

    End Sub

    Private Sub grdBudget_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdBudget.CellValuePushed
        If e.RowIndex < 0 Or e.ColumnIndex < 2 Then Exit Sub
        If cmbDisplay.SelectedIndex > 0 Then Exit Sub
        Saved = False

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
                If Windows.Forms.MessageBox.Show("You have not entered a budget." + vbCrLf + vbCrLf + "Do you want to distribute budget according to Loading?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    For r As Integer = 0 To grdBudget.Rows.Count - 2
                        For c As Integer = 0 To grdBudget.Columns.Count - 2
                            grdBudget.Rows(r).Cells(c).Value = BudgetLoading(r, c) * e.Value
                        Next
                    Next
                    Exit Sub
                Else
                    ratio = 0
                End If
            End If
            'For r As Integer = 0 To grdBudget.Rows.Count - 2 'not the last row since its the cell we are currently in
            '    If grdBudget.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            '        If Not grdBudget.Rows(r).Tag.relations(1).bookingtype.islocked Then
            '            grdBudget.Rows(r).Cells(grdBudget.Columns.Count - 1).Value *= ratio
            '        End If
            '    Else
            '        If Not grdBudget.Rows(r).Tag.islocked Then
            '            grdBudget.Rows(r).Cells(grdBudget.Columns.Count - 1).Value *= ratio
            '        End If
            '    End If
            'Next

            'Commented out block above and copied block below from Allocate

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
                        Dim CombName As String = ""
                        For Each Comb As Trinity.cCombination In ActiveCampaign.Combinations
                            For Each CC As Trinity.cCombinationChannel In Comb.Relations
                                If CC.Bookingtype.ToString = grdBudget.Rows(r).Tag.ToString Then
                                    CombName = Comb.Name
                                End If
                            Next
                        Next
                        If Not setList.Contains(CombName) Then
                            grdBudget.Rows(r).Cells(grdBudget.Columns.Count - 1).Value *= ratio
                            If Not CombName = "" Then
                                setList.Add(CombName)
                            End If
                        End If
                    End If
                End If
            Next

        ElseIf e.ColumnIndex = grdBudget.Columns.Count - 1 AndAlso Not e.RowIndex = grdBudget.Rows.Count - 1 Then
            'if a row sum is entered

            'check and make sure the bookingtype is not locked
            If grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                If grdBudget.Rows(e.RowIndex).Tag.Relations(1).Bookingtype.islocked Then
                    MsgBox("This bookingtype is locked, if you wish to change TRPs you need to unlock it first", MsgBoxStyle.Information, "Locked Week")
                    Exit Sub
                End If
            Else
                If grdBudget.Rows(e.RowIndex).Tag.islocked Then
                    MsgBox("This bookingtype is locked, if you wish to change TRPs you need to unlock it first", MsgBoxStyle.Information, "Locked Week")
                    Exit Sub
                End If
            End If

            'get the change ratio, all weeks needs to be claculated with this ratio
            Dim newValue As Double = e.Value
            Dim oldValue = grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

            Dim sngSum As Single = 0

            If grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                For c As Integer = 2 To grdBudget.ColumnCount - 2
                    If Not grdBudget.Rows(e.RowIndex).Tag.relations(1).bookingtype.weeks(grdBudget.Columns(c).HeaderText).islocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(e.RowIndex).Tag.relations
                            sngSum += tmpCC.Bookingtype.Weeks(grdBudget.Columns(c).HeaderText).NetBudget
                        Next
                    End If
                Next
            Else
                For c As Integer = 2 To grdBudget.ColumnCount - 2
                    Dim TmpWeek As Trinity.cWeek = grdBudget.Rows(e.RowIndex).Tag.weeks(grdBudget.Columns(c).HeaderText)
                    If Not TmpWeek.IsLocked Then
                        sngSum += TmpWeek.NetBudget
                    End If
                Next
            End If

            Dim ratio As Double

            If sngSum <> 0 Then
                ratio = (newValue - (oldValue - sngSum)) / sngSum
            Else
                ratio = 0
            End If

            'For c As Integer = 2 To grdBudget.Columns.Count - 2 'dont use the % columns or the sum columns
            '    If grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            '        If Not grdBudget.Rows(e.RowIndex).Tag.relations(1).bookingtype.weeks(grdBudget.Columns(c).HeaderText).islocked Then
            '            For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(e.RowIndex).Tag.relations
            '                tmpCC.Bookingtype.Weeks(grdBudget.Columns(c).HeaderText).NetBudget *= ratio
            '                tmpCC.Bookingtype.Weeks(grdBudget.Columns(c).HeaderText).TRPControl = False
            '            Next
            '        End If
            '    Else
            '        Dim TmpWeek As Trinity.cWeek = grdBudget.Rows(e.RowIndex).Tag.weeks(grdBudget.Columns(c).HeaderText)
            '        If Not TmpWeek.IsLocked Then
            '            TmpWeek.TRPControl = False
            '            TmpWeek.NetBudget *= ratio
            '        End If
            '    End If
            'Next

            'Commented out block above and copied block below from Allocate

            For c As Integer = 2 To grdBudget.Columns.Count - 2 'dont use the % columns or the sum columns
                If grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    If Not grdBudget.Rows(e.RowIndex).Tag.relations(1).bookingtype.weeks(grdBudget.Columns(c).HeaderText).islocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(e.RowIndex).Tag.relations
                            tmpCC.Bookingtype.Weeks(grdBudget.Columns(c).HeaderText).NetBudget *= ratio
                            tmpCC.Bookingtype.Weeks(grdBudget.Columns(c).HeaderText).TRPControl = False
                        Next
                    End If
                Else
                    Dim TmpWeek As Trinity.cWeek = grdBudget.Rows(e.RowIndex).Tag.weeks(grdBudget.Columns(c).HeaderText)
                    If Not TmpWeek.IsLocked Then
                        TmpWeek.TRPControl = False
                        TmpWeek.NetBudget *= ratio

                        For Each TmpCombo As Trinity.cCombination In ActiveCampaign.Combinations
                            If TmpCombo.IncludesBookingtype(grdBudget.Rows(e.RowIndex).Tag) Then
                                Dim Factor As Single
                                For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                                    If TmpCC.Bookingtype Is grdBudget.Rows(e.RowIndex).Tag Then
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
                                            TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPControl = True
                                            TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPBuyingTarget = Factor * TmpCC.Relation
                                        Else
                                            TmpCC.Bookingtype.Weeks(TmpWeek.Name).NetBudget = Factor * TmpCC.Relation
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


            'get the change ratio, all sums needs to be calculated with this ratio
            Dim newValue As Double = e.Value
            Dim oldValue = grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

            Dim sngSum As Single = 0
            For r As Integer = 0 To grdBudget.Rows.Count - 2 '-2 because the last ow is the sum row
                If grdBudget.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    If Not grdBudget.Rows(r).Tag.relations(1).bookingtype.islocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(r).Tag.relations
                            sngSum += tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).NetBudget
                        Next
                    End If
                Else
                    Dim TmpWeek As Trinity.cWeek = grdBudget.Rows(r).Tag.weeks(grdBudget.Columns(e.ColumnIndex).HeaderText)
                    If Not grdTRP.Rows(r * 2).Tag.islocked Then
                        sngSum += TmpWeek.NetBudget
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
                            tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).NetBudget *= ratio
                            tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).TRPControl = False
                        Next
                    End If
                Else
                    Dim TmpWeek As Trinity.cWeek = grdBudget.Rows(r).Tag.weeks(grdBudget.Columns(e.ColumnIndex).HeaderText)
                    If Not grdTRP.Rows(r * 2).Tag.islocked Then
                        TmpWeek.TRPControl = False
                        TmpWeek.NetBudget *= ratio
                    End If
                End If
            Next
        Else
            'if a regular week is entered
            If grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(e.RowIndex).Tag.relations
                    tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).NetBudget = e.Value * tmpCC.Percent
                    tmpCC.Bookingtype.Weeks(grdBudget.Columns(e.ColumnIndex).HeaderText).TRPControl = False
                Next
            Else
                Dim TmpWeek As Trinity.cWeek = grdBudget.Rows(e.RowIndex).Tag.weeks(grdBudget.Columns(e.ColumnIndex).HeaderText)
                TmpWeek.TRPControl = False
                TmpWeek.NetBudget = e.Value

                For Each TmpCombo As Trinity.cCombination In ActiveCampaign.Combinations
                    If TmpCombo.IncludesBookingtype(grdBudget.Rows(e.RowIndex).Tag) Then
                        Dim Factor As Single
                        For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                            If TmpCC.Bookingtype Is grdBudget.Rows(e.RowIndex).Tag Then
                                If TmpCombo.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                                    Factor = TmpWeek.TRPBuyingTarget / TmpCC.Relation
                                Else
                                    Factor = TmpWeek.NetBudget / TmpCC.Relation
                                End If
                            End If
                        Next
                        For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                            If TmpCC.Bookingtype IsNot grdBudget.Rows(e.RowIndex).Tag Then
                                If TmpCombo.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                                    TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPBuyingTarget = Factor * TmpCC.Relation
                                    'we need to set this to true for the CalcCPP function for calculate correct
                                    TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPControl = True
                                    TmpCC.Bookingtype.Weeks(TmpWeek.Name).RecalculateCPP()
                                    TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPControl = False
                                Else
                                    TmpCC.Bookingtype.Weeks(TmpWeek.Name).NetBudget = Factor * TmpCC.Relation
                                End If
                                TmpCC.Bookingtype.Weeks(TmpWeek.Name).TRPControl = False
                            End If
                        Next
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

    Private Sub grdTRP_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdTRP.CellMouseClick
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

            With DirectCast(mnu.Items.Add("Estimate reach"), System.Windows.Forms.ToolStripMenuItem)
                With .DropDownItems.Add("Use last weeks of data", Nothing, AddressOf CalculateReachForWeek)
                    .Tag = e.ColumnIndex
                End With
                With .DropDownItems.Add("Use same period last year", Nothing, AddressOf CalculateReachForWeek)
                    .Tag = e.ColumnIndex
                End With
            End With

            For Each _week As Trinity.cWeek In ActiveCampaign.Channels(1).BookingTypes(1).Weeks
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
        Dim TmpDate As Long = ActiveCampaign.EndDate
        Dim DateDiff As Long

        If sender.text = "Use last weeks of data" Then
            While TmpDate >= Karma.KarmaAdedge.getDataRangeTo(Connect.eDataType.mSpot)
                TmpDate = TmpDate - 1
            End While
            DateDiff = ActiveCampaign.EndDate - TmpDate
            Periodstr = Format(Date.FromOADate(ActiveCampaign.Channels(1).BookingTypes(1).Weeks(sender.tag + 1).StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(ActiveCampaign.Channels(1).BookingTypes(1).Weeks(sender.tag + 1).EndDate - DateDiff), "ddMMyy")
        Else
            While Weekday(Date.FromOADate(TmpDate), FirstDayOfWeek.Monday) <> Weekday(Date.FromOADate(ActiveCampaign.EndDate), FirstDayOfWeek.Monday)
                TmpDate = TmpDate + 1
            End While
            DateDiff = ActiveCampaign.EndDate - TmpDate

            Periodstr &= Format(Date.FromOADate(ActiveCampaign.Channels(1).BookingTypes(1).Weeks(sender.tag + 1).StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(ActiveCampaign.Channels(1).BookingTypes(1).Weeks(sender.tag + 1).EndDate - DateDiff), "ddMMyy")
        End If

        Karma.ReferencePeriod = Periodstr

        For Each TmpChan In ActiveCampaign.Channels
            IsUsed = False
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    IsUsed = True
                End If
                If TmpBT.IsSponsorship Then
                    UseSponsorship = True
                ElseIf TmpBT.BookIt Then
                    UseCommercial = True
                End If
            Next
            If IsUsed Then
                Karma.Channels.Add(TmpChan.ChannelName)
                Karma.Weeks = TmpChan.BookingTypes(1).Weeks.Count
            End If
        Next
        frmProgress.Status = "Fetching spots..."
        frmProgress.Progress = 0
        frmProgress.Show()
        AddHandler Karma.PopulateProgress, AddressOf Progress
        Karma.Populate(UseSponsorship, UseCommercial)

        TmpStr = ActiveCampaign.Name
        If TmpStr Is Nothing Then TmpStr = ""
        Karma.Campaigns.Add(TmpStr, ActiveCampaign)
        frmProgress.Status = "Calculating reach for " & TmpStr & " week " & grdTRP.Columns(CInt(sender.tag)).HeaderText
        frmProgress.Progress = 0
        frmProgress.Show()
        AddHandler Karma.Campaigns(TmpStr).Progress, AddressOf Progress
        Karma.Campaigns(TmpStr).Run(sender.tag + 1)
        frmProgress.Hide()

        ActiveCampaign.EstimatedWeeklyReach(grdTRP.Columns(CInt(sender.tag)).HeaderText) = Karma.Campaigns.Item(TmpStr).Reach(0, 1)

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
        Dim column As Integer = sender.name

        SkipIt = True
        For Each c As Trinity.cChannel In ActiveCampaign.Channels
            For Each bt As Trinity.cBookingType In c.BookingTypes
                If bt.BookIt Then
                    SkipIt = True
                    bt.Weeks(newWeek).NetBudget = bt.Weeks(oldWeek).NetBudget
                    bt.Weeks(newWeek).TRPControl = False
                End If
            Next
        Next
        SkipIt = False

        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
        grdSumChannels.Invalidate()
        grdBudget.Invalidate()
    End Sub

    Private Sub CopyTRPWeek(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim newWeek As String = sender.text
        Dim oldWeek As String = sender.tag
        Dim column As Integer = sender.name

        SkipIt = True
        For Each c As Trinity.cChannel In ActiveCampaign.Channels
            For Each bt As Trinity.cBookingType In c.BookingTypes
                If bt.BookIt Then
                    SkipIt = True
                    bt.Weeks(newWeek).TRP = bt.Weeks(oldWeek).TRP
                    bt.Weeks(newWeek).TRPControl = True
                End If
            Next
        Next
        SkipIt = False

        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
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

    Private Sub grdSumChannels_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdSumChannels.CellFormatting
        If e.RowIndex = 0 Then Exit Sub
        If e.RowIndex Mod 2 > 0 Then
            If cmbDisplay.SelectedIndex = 0 Then
                e.CellStyle = styleNormalD
            Else
                e.CellStyle = styleCantSetP
            End If
        End If
    End Sub

    Private Sub grdSumChannels_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSumChannels.CellValueNeeded
        '1 is because we only sum up every second row
        If grdTRP.Rows.Count = 0 Then Exit Sub
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
        If cmbDisplay.SelectedIndex = 0 Then
            If grdTRP.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                Dim tmpC As Trinity.cCombination = grdTRP.Rows(e.RowIndex).Tag
                For Each cc As Trinity.cCombinationChannel In tmpC.Relations
                    TmpBT = cc.Bookingtype

                    For Each TmpWeek In TmpBT.Weeks
                        SumNat = SumNat + TmpWeek.TRP
                        SumChn = SumChn + TmpWeek.TRPBuyingTarget
                    Next
                    For x = 0 To grdTRP.Rows.Count - 1 Step 2
                        If grdTRP.Rows(x).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                            Dim tmpC2 As Trinity.cCombination = grdTRP.Rows(x).Tag
                            For Each cc2 As Trinity.cCombinationChannel In tmpC2.Relations
                                TmpBT = cc2.Bookingtype
                                For Each TmpWeek In TmpBT.Weeks
                                    SumSumNat = SumSumNat + TmpWeek.TRP
                                Next
                            Next
                        Else
                            TmpBT = grdTRP.Rows(x).Tag
                            For Each TmpWeek In TmpBT.Weeks
                                SumSumNat = SumSumNat + TmpWeek.TRP
                            Next
                        End If
                    Next
                Next
                SumSumNat = SumSumNat / tmpC.Relations.count
            Else
                TmpBT = grdTRP.Rows(e.RowIndex).Tag

                For Each TmpWeek In TmpBT.Weeks
                    SumNat = SumNat + TmpWeek.TRP
                    SumChn = SumChn + TmpWeek.TRPBuyingTarget
                Next
                For x = 0 To grdTRP.Rows.Count - 1 Step 2
                    If grdTRP.Rows(x).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                        Dim tmpC2 As Trinity.cCombination = grdTRP.Rows(x).Tag
                        For Each cc2 As Trinity.cCombinationChannel In tmpC2.Relations
                            TmpBT = cc2.Bookingtype
                            For Each TmpWeek In TmpBT.Weeks
                                SumSumNat = SumSumNat + TmpWeek.TRP
                            Next
                        Next
                    Else
                        TmpBT = grdTRP.Rows(x).Tag
                        For Each TmpWeek In TmpBT.Weeks
                            SumSumNat = SumSumNat + TmpWeek.TRP
                        Next
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
        ElseIf cmbDisplay.SelectedIndex = 1 Then 'if we have a sum %
            Dim sum As Double = 0
            Dim w As Integer = 0
            For w = 0 To grdTRP.ColumnCount - 1
                sum += grdTRP.Rows(e.RowIndex).Cells(w).Value
            Next

            e.Value = sum / w
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
            If grdTRP.Rows(e.RowIndex).Tag.islocked Then
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
                            TRPSum += tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRP
                        Next
                    End If
                Next

                Dim ratio As Single = (newValue - (grdSumChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value - TRPSum)) / TRPSum

                For c = 0 To grdTRP.Columns.Count - 1
                    Dim tmpC As Trinity.cCombination = grdTRP.Rows(e.RowIndex).Tag
                    If Not tmpC.Relations(1).Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).IsLocked Then
                        For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRP *= ratio
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRPControl = True
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
                    End If
                Next
            End If
        ElseIf e.ColumnIndex = 1 Then
            TRPSum = 0
            If grdTRP.Rows(e.RowIndex).Cells(c).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                For c = 0 To grdTRP.Columns.Count - 1
                    Dim tmpC As Trinity.cCombination = grdTRP.Rows(e.RowIndex).Cells(c).Tag
                    For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                        TRPSum = TRPSum + tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRPBuyingTarget
                    Next
                Next
                For c = 0 To grdTRP.Columns.Count - 1
                    Dim tmpC As Trinity.cCombination = grdTRP.Rows(e.RowIndex).Cells(c).Tag
                    For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                        tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRPBuyingTarget = e.Value * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRPBuyingTarget / TRPSum)
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
                Next
            End If
        End If

        grdTRP.Invalidate()
        grdBudget.Invalidate()
        grdSumChannels.Invalidate()
    End Sub

    Private Sub grdSumWeeks_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdSumWeeks.CellFormatting
        If e.RowIndex = 1 Then Exit Sub

        If cmbDisplay.SelectedIndex = 0 Then
            e.CellStyle = styleNormalD
        Else
            e.CellStyle = styleCantSetP
        End If
    End Sub

    Private Sub grdSumWeeks_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSumWeeks.CellValueNeeded
        If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then Exit Sub

        Dim SumTarget As Single = 0
        Dim i As Integer

        If cmbDisplay.SelectedIndex > 0 Then
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

            For i = 1 To grdTRP.Rows.Count - 1 Step 2
                SumTarget += grdTRP.Rows(i).Cells(e.ColumnIndex).Value
            Next
            e.Value = SumTarget


        ElseIf e.RowIndex = 1 Then 'All adults

            'we only take every second row in the for loops since the table have both buying target and all adults ratings

            For i = 0 To grdTRP.Rows.Count - 1 Step 2
                SumTarget += grdTRP.Rows(i).Cells(e.ColumnIndex).Value
            Next

            e.Value = SumTarget
        Else
            e.Value = ActiveCampaign.EstimatedWeeklyReach(grdTRP.Columns(e.ColumnIndex).HeaderText)
        End If
    End Sub

    Private Sub grdSumWeeks_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSumWeeks.CellValuePushed
        If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then Exit Sub
        Saved = False

        If e.RowIndex = 2 Then
            ActiveCampaign.EstimatedWeeklyReach(grdTRP.Columns(e.ColumnIndex).HeaderText) = e.Value
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

    Private Sub grdGrandSum_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdGrandSum.CellValueNeeded
        'updates the grid on a number of events (window activation, mouse movement etc)
        If cmbDisplay.SelectedIndex = 0 Then
            e.Value = grdGrandSum.Tag
            grdBudget.Invalidate()
        Else
            e.Value = "-"
        End If
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
        Dim value As Double = e.Value

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

        Dim ratio As Single = (value - (grdGrandSum.Rows(0).Cells(0).Value - TRPSum)) / TRPSum

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
        grdSumChannels.Invalidate()
    End Sub

    Private Sub onlyOneSelectedGrid(Optional ByVal sender As Object = Nothing, Optional ByVal e As System.EventArgs = Nothing) Handles grdAV.GotFocus, grdBudget.GotFocus, grdDiscounts.GotFocus, grdFilms.GotFocus, grdGrandSum.GotFocus, grdIndex.GotFocus, grdSumChannels.GotFocus, grdSumWeeks.GotFocus, grdTRP.GotFocus
        If sender Is Nothing Then
            grdAV.ClearSelection()
            grdBudget.ClearSelection()
            grdDiscounts.ClearSelection()
            grdFilms.ClearSelection()
            grdGrandSum.ClearSelection()
            grdIndex.ClearSelection()
            grdSumChannels.ClearSelection()
            grdSumWeeks.ClearSelection()
            grdTRP.ClearSelection()
        Else
            Dim grid As Windows.Forms.DataGridView
            grid = sender

            Select Case grid.Name
                Case Is = "grdAV"
                    grdBudget.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdBudget"
                    grdAV.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdDiscounts"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdFilms"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdGrandSum"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdIndex"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdSumChannels"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSumWeeks.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdSumWeeks"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdTRP.ClearSelection()
                Case Is = "grdTRP"
                    grdAV.ClearSelection()
                    grdBudget.ClearSelection()
                    grdDiscounts.ClearSelection()
                    grdFilms.ClearSelection()
                    grdGrandSum.ClearSelection()
                    grdIndex.ClearSelection()
                    grdSumChannels.ClearSelection()
                    grdSumWeeks.ClearSelection()
            End Select
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        styleNormal = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
        styleNormalD = New Windows.Forms.DataGridViewCellStyle(grdTRP.DefaultCellStyle)
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

        styleNormal.Format = "N0"
        styleNormalLocked.Format = "N0"
        styleNormalLocked.BackColor = Color.Khaki

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

        lblExplain.ForeColor = Color.Maroon
    End Sub

    Private Sub cmbTargets_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTargets.SelectedIndexChanged
        If ActiveCampaign.RootCampaign Is Nothing And cmbDisplay.SelectedIndex = 0 Then
            grdSumWeeks.DefaultCellStyle = styleNormalD
            grdSumChannels.ForeColor = Color.Black
            grdBudget.DefaultCellStyle = styleNormal
            grdDiscounts.DefaultCellStyle = styleNormal
            grdAV.DefaultCellStyle = styleNormal
            grdDiscounts.DefaultCellStyle = styleNormal
            grdIndex.DefaultCellStyle = styleNormal

            grdTRP.ReadOnly = False
            grdSumWeeks.ReadOnly = False
            grdSumChannels.ReadOnly = False
            grdBudget.ReadOnly = False
            grdFilms.ReadOnly = False
            grdAV.ReadOnly = False
            grdIndex.ReadOnly = False

        Else
            grdTRP.ReadOnly = True
            If ActiveCampaign.RootCampaign Is Nothing Then
                grdSumWeeks.DefaultCellStyle = styleNormalD
                grdSumChannels.ForeColor = styleNormal.ForeColor
                grdBudget.DefaultCellStyle = styleNormal
                grdDiscounts.DefaultCellStyle = styleNormal
                grdAV.DefaultCellStyle = styleNormal
                grdIndex.DefaultCellStyle = styleNormal

                grdSumWeeks.ReadOnly = False
                grdSumChannels.ReadOnly = False
                grdBudget.ReadOnly = False
                grdFilms.ReadOnly = False
                grdAV.ReadOnly = False
                grdIndex.ReadOnly = False
            Else
                grdSumWeeks.DefaultCellStyle = styleCantSetN
                grdSumChannels.ForeColor = styleCantSetN.ForeColor
                grdBudget.DefaultCellStyle = styleCantSetN
                grdDiscounts.DefaultCellStyle = styleCantSetN
                grdAV.DefaultCellStyle = styleCantSetN
                grdDiscounts.DefaultCellStyle = styleCantSetN
                grdIndex.DefaultCellStyle = styleCantSetN

                grdSumWeeks.ReadOnly = True
                grdSumChannels.ReadOnly = True
                grdBudget.ReadOnly = True
                grdFilms.ReadOnly = True
                grdAV.ReadOnly = True
                grdIndex.ReadOnly = True
            End If
        End If

        SkipIt = True
        grdTRP.Invalidate()
        grdBudget.Invalidate()
        grdSumChannels.Invalidate()
        grdSumWeeks.Invalidate()
        SkipIt = False
    End Sub

    Private Sub cmdEditCTC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditCTC.Click
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        Windows.Forms.Application.DoEvents()
        Try
            ActiveCampaign.PlannedTotCTC = InputBox("CTC:", "T R I N I T Y", ActiveCampaign.PlannedTotCTC)
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        End Try
        grdSumChannels.Invalidate()
        grdBudget.Invalidate()
        grdTRP.Invalidate()
        grdSumWeeks.Invalidate()
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub grdLoading_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLoading.CellEndEdit
        Dim TmpValue As Single = grdLoading.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
        If grdLoading.SelectedCells.Count > 1 Then
            For Each TmpCell As Windows.Forms.DataGridViewCell In grdLoading.SelectedCells
                TmpCell.Value = TmpValue * 100
            Next
        End If
    End Sub

    Private Sub grdLoading_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdLoading.CellFormatting
        e.CellStyle.Format = "P1"
        If e.ColumnIndex = grdLoading.Columns.Count - 1 Then
            Dim Sum As Single = 0
            For r As Integer = 0 To grdLoading.Rows.Count - 2
                If cmbChannelLoading.SelectedIndex = 0 Then
                    Sum += TRPLoading(r, e.ColumnIndex)
                Else
                    Sum += BudgetLoading(r, e.ColumnIndex)
                End If
            Next
            If Math.Round(Sum * 1000) <> 1000 Then
                e.CellStyle.BackColor = Color.Red
            Else
                e.CellStyle.BackColor = Color.White
            End If
        Else
            Dim Sum As Single = 0
            For c As Integer = 0 To grdLoading.Columns.Count - 2
                If cmbLoading.SelectedIndex = 0 Then
                    Sum += TRPLoading(e.RowIndex, c)
                Else
                    Sum += BudgetLoading(e.RowIndex, c)
                End If
            Next
            If Math.Round(Sum * 1000) <> 1000 Then
                e.CellStyle.BackColor = Color.Red
            Else
                e.CellStyle.BackColor = Color.White
            End If
        End If
        e.CellStyle.ForeColor = Color.Black
    End Sub

    Private Sub grdLoading_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdLoading.CellValueNeeded
        GetLoading()
        If grdLoading.Columns.Count = 0 Or grdLoading.Rows.Count = 0 Then Exit Sub
        Try
            Dim Dummy As Single = TRPLoading(e.RowIndex, e.ColumnIndex)
            Dummy = BudgetLoading(e.RowIndex, e.ColumnIndex)
        Catch ex As Exception
            TRPLoading = Nothing
            BudgetLoading = Nothing
            GetLoading()
        End Try
        If (cmbLoading.SelectedIndex = 0 AndAlso e.ColumnIndex < grdLoading.ColumnCount - 1) OrElse (cmbChannelLoading.SelectedIndex = 0 AndAlso e.ColumnIndex = grdLoading.ColumnCount - 1) Then
            e.Value = TRPLoading(e.RowIndex, e.ColumnIndex)
        Else
            e.Value = BudgetLoading(e.RowIndex, e.ColumnIndex)
        End If
    End Sub

    Private Sub grdLoading_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdLoading.CellValuePushed
        Dim LoadingArray As Array
        If (cmbLoading.SelectedIndex = 0 AndAlso e.ColumnIndex < grdLoading.ColumnCount - 1) OrElse (cmbChannelLoading.SelectedIndex = 0 AndAlso e.ColumnIndex = grdLoading.ColumnCount - 1) Then
            LoadingArray = TRPLoading
        Else
            LoadingArray = BudgetLoading
        End If

        If e.RowIndex = grdLoading.Rows.Count - 1 Then
            For r As Integer = 0 To grdLoading.Rows.Count - 2
                LoadingArray(r, e.ColumnIndex) = e.Value.ToString.Trim("%") / 100
            Next
        End If
        LoadingArray(e.RowIndex, e.ColumnIndex) = e.Value.ToString.Trim("%") / 100

        grdLoading.Invalidate()
    End Sub

    Private Sub cmbLoading_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLoading.SelectedIndexChanged
        grdLoading.Invalidate()
    End Sub

    Private Sub old_cmdApplyLoading_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim LoadingArray As Array
        If cmbLoading.SelectedIndex = 0 Then
            LoadingArray = TRPLoading
        Else
            LoadingArray = BudgetLoading
        End If
        Dim Not100 As Boolean = False
        For r As Integer = 0 To grdLoading.Rows.Count - 2
            Dim Sum As Single = 0
            For c As Integer = 0 To grdLoading.Columns.Count - 2
                Sum += grdLoading.Rows(r).Cells(c).Value
            Next
            If Math.Round(Sum, 2) <> 1 Then
                Not100 = True
                Exit For
            End If
        Next
        If Not100 Then
            Windows.Forms.MessageBox.Show("Can not apply loading that does not sum to 100%.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Exit Sub
        End If
        If cmbLoading.SelectedIndex = 0 Then
            Dim TRPSum As Decimal = grdGrandSum.Rows(0).Cells(0).Value
            For r As Integer = 0 To grdLoading.Rows.Count - 2
                For c As Integer = 0 To grdLoading.Columns.Count - 2
                    Dim TRP As Decimal = (LoadingArray(r, grdLoading.Columns.Count - 1) * TRPSum) * LoadingArray(r, c)
                    If grdTRP.Rows(r * 2).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                        For Each tmpCC As Trinity.cCombinationChannel In grdTRP.Rows(r * 2).Tag.relations
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRP = TRP * tmpCC.Percent
                            tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRPControl = True
                        Next
                    Else
                        Dim TmpWeek As Trinity.cWeek = grdTRP.Rows(r * 2).Tag.weeks(grdTRP.Columns(c).HeaderText)
                        TmpWeek.TRPControl = False
                        TmpWeek.TRP = TRP
                    End If
                Next
                grdSumChannels.Rows(r * 2 + 1).Cells(0).Value = (LoadingArray(r, grdLoading.Columns.Count - 1) * TRPSum)
            Next
            grdGrandSum.Rows(0).Cells(0).Value = TRPSum
        Else
            Dim BudgetSum As Decimal = grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(grdBudget.Columns.Count - 1).Value
            For r As Integer = 0 To grdLoading.Rows.Count - 2
                For c As Integer = 0 To grdLoading.Columns.Count - 2
                    Dim Budget As Decimal = (LoadingArray(r, grdLoading.Columns.Count - 1) * BudgetSum) * LoadingArray(r, c)
                    If grdBudget.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                        For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(r).Tag.relations
                            tmpCC.Bookingtype.Weeks(grdBudget.Columns(c + 2).HeaderText).NetBudget = Budget * tmpCC.Percent
                            tmpCC.Bookingtype.Weeks(grdBudget.Columns(c + 2).HeaderText).TRPControl = False
                        Next
                    Else
                        Dim TmpWeek As Trinity.cWeek = grdBudget.Rows(r).Tag.weeks(grdBudget.Columns(c + 2).HeaderText)
                        TmpWeek.TRPControl = False
                        TmpWeek.NetBudget = Budget
                    End If
                Next
                grdBudget.Rows(r).Cells(grdBudget.Columns.Count - 1).Value = (LoadingArray(r, grdLoading.Columns.Count - 1) * BudgetSum)
            Next
            grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(grdBudget.Columns.Count - 1).Value = BudgetSum
        End If
        grdBudget.Invalidate()
        grdTRP.Invalidate()
        TRPLoading = Nothing
        BudgetLoading = Nothing
        GetLoading()
    End Sub

    Private Sub cmdResetLoading_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdResetLoading.Click
        BudgetLoading = Nothing
        TRPLoading = Nothing
        grdLoading.Invalidate()
    End Sub

    Private Sub cmbChannelLoading_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbChannelLoading.SelectedIndexChanged
        grdLoading.Invalidate()
    End Sub

    Private Sub cmdApplyLoading_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApplyLoading.Click
        Dim TotalValue As Single

        If cmbChannelLoading.SelectedIndex = 0 Then
            TotalValue = grdGrandSum.Rows(0).Cells(0).Value
        Else
            TotalValue = grdBudget.Rows(grdBudget.RowCount - 1).Cells(grdBudget.ColumnCount - 1).Value
        End If

        If cmbLoading.SelectedIndex = 0 Then
            For r As Integer = 0 To grdLoading.Rows.Count - 2
                For c As Integer = 0 To grdLoading.Columns.Count - 2
                    Dim TRP As Single = TRPLoading(r, c) * 100
                    If grdTRP.Rows(r * 2).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                        Dim tmpC As Trinity.cCombination = grdTRP.Rows(r * 2).Tag
                        If tmpC.CombinationOn = Trinity.cCombination.CombinationOnEnum.coTRP Then
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRP = TRP
                                tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRPControl = True
                            Next
                        Else
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).NetBudget = (tmpCC.Percent) * 1000000
                                tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRPControl = False
                            Next
                            Dim sumTRP As Double = 0
                            For Each TmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                sumTRP += TmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRP
                            Next
                            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                                If sumTRP > 0 Then
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRP = TRP * (tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRP / sumTRP)
                                Else
                                    tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRP = 0
                                End If
                                tmpCC.Bookingtype.Weeks(grdTRP.Columns(c).HeaderText).TRPControl = True
                            Next
                        End If
                    Else
                        Dim TmpWeek As Trinity.cWeek = grdTRP.Rows(r * 2).Tag.weeks(grdTRP.Columns(c).HeaderText)
                        TmpWeek.TRPControl = True
                        TmpWeek.TRP = TRP
                    End If
                Next
            Next
        Else
            Dim BudgetSum As Decimal = grdBudget.Rows(grdBudget.Rows.Count - 1).Cells(grdBudget.Columns.Count - 1).Value
            For r As Integer = 0 To grdLoading.Rows.Count - 2
                For c As Integer = 0 To grdLoading.Columns.Count - 2
                    Dim Budget As Decimal = BudgetLoading(r, c) * 100
                    If grdBudget.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                        For Each tmpCC As Trinity.cCombinationChannel In grdBudget.Rows(r).Tag.relations
                            tmpCC.Bookingtype.Weeks(grdBudget.Columns(c + 2).HeaderText).NetBudget = Budget * tmpCC.Percent
                            tmpCC.Bookingtype.Weeks(grdBudget.Columns(c + 2).HeaderText).TRPControl = False
                        Next
                    Else
                        Dim TmpWeek As Trinity.cWeek = grdBudget.Rows(r).Tag.weeks(grdBudget.Columns(c + 2).HeaderText)
                        TmpWeek.TRPControl = False
                        TmpWeek.NetBudget = Budget
                    End If
                Next
            Next
        End If
        grdBudget.Invalidate()
        If cmbChannelLoading.SelectedIndex = 0 Then
            For r As Integer = 0 To grdLoading.Rows.Count - 2
                grdSumChannels.Rows(r * 2 + 1).Cells(0).Value = (TRPLoading(r, grdLoading.Columns.Count - 1) * TotalValue)
            Next
        Else
            For r As Integer = 0 To grdLoading.Rows.Count - 2
                grdBudget.Rows(r).Cells(grdBudget.Columns.Count - 1).Value = (BudgetLoading(r, grdLoading.Columns.Count - 1) * TotalValue)
            Next
        End If
    End Sub

    Private Sub Karma_PopulateProgress(ByVal p As Integer) Handles Karma.PopulateProgress
        frmProgress.Progress = p
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Karma.Campaigns(ActiveCampaign.Name).CancelRun()
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click

        ' If no campaign is selected, do nothing
        If cmbCampaigns.SelectedItem Is Nothing Then
            Windows.Forms.MessageBox.Show("No campaign were selected!", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim TmpName As String
        Dim TmpCampaign As Trinity.cKampanj

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        Dim TmpCamp As Trinity.cKampanj = Campaign.Campaigns(cmbCampaigns.SelectedItem)

        TmpName = cmbCampaigns.SelectedItem

        TmpCampaign = New Trinity.cKampanj(False)
        TmpCampaign.LoadCampaign("", True, Campaign.SaveCampaign(, True, True, True, True))
        TmpCampaign.Name = TmpName

        For Each TmpChan As Trinity.cChannel In TmpCampaign.Channels
            If TmpCamp.Channels(TmpChan.ChannelName) IsNot Nothing Then
                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    If TmpCamp.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name) IsNot Nothing Then
                        With TmpCamp.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name)
                            Dim wn As Integer = 0
                            For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                                wn += 1
                                If .Weeks(TmpWeek.Name) IsNot Nothing Then
                                    With .Weeks(TmpWeek.Name)
                                        For Each TmpFilm As Trinity.cFilm In TmpWeek.Films
                                            If .Films(TmpFilm.Name) IsNot Nothing Then
                                                TmpFilm.Share = .Films(TmpFilm.Name).Share
                                            End If
                                        Next
                                        TmpWeek.TRPControl = .TRPControl
                                        If .TRPControl Then
                                            TmpWeek.TRPBuyingTarget = .TRPBuyingTarget
                                        Else
                                            TmpWeek.NetBudget = .NetBudget
                                        End If
                                    End With
                                    For Each TmpAV As Trinity.cAddedValue In TmpBT.AddedValues
                                        If TmpCamp.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).AddedValues(TmpAV.ID) Is Nothing Then
                                            With TmpCamp.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).AddedValues.Add(TmpAV.Name, TmpAV.ID)
                                                .IndexGross = TmpAV.IndexGross
                                                .IndexNet = TmpAV.IndexNet
                                                .ShowIn = TmpAV.ShowIn
                                            End With
                                        End If
                                        TmpAV.Amount(wn) = TmpCamp.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).AddedValues(TmpAV.ID).Amount(wn)
                                    Next
                                End If
                            Next
                            TmpBT.IndexMainTarget = .IndexMainTarget
                            TmpBT.IndexSecondTarget = .IndexSecondTarget
                            TmpBT.IndexAllAdults = .IndexAllAdults
                        End With
                    End If
                Next
            End If
        Next

        Campaign.Campaigns.Remove(TmpName)
        Campaign.Campaigns.Add(TmpName, TmpCampaign)
        Karma = Nothing
        chtKarma.Karma = Nothing

        cmbCampaigns_SelectedIndexChanged(sender, e)

        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub


    Private Sub cmdSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetup.Click
        If cmbCampaigns.SelectedItem Is Nothing Then
            Windows.Forms.MessageBox.Show("No campaign were selected!", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim _frmSetup As New frmSetup(Campaign.Campaigns(cmbCampaigns.SelectedItem))
        _frmSetup.txtName.Enabled = False
        _frmSetup.cmbBuyer.Enabled = False
        _frmSetup.cmbPlanner.Enabled = False
        _frmSetup.cmbClient.Enabled = False
        _frmSetup.cmbProduct.Enabled = False
        _frmSetup.cmdAddClient.Enabled = False
        _frmSetup.cmdAddProduct.Enabled = False
        _frmSetup.cmdEditClient.Enabled = False
        _frmSetup.cmdEditProduct.Enabled = False

        _frmSetup.ShowDialog()
        cmbCampaigns_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub cmdEditName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditName.Click

        Dim TmpName As String

        TmpName = InputBox("Descriptive title:", cmbCampaigns.SelectedItem, cmbCampaigns.SelectedItem)
        If TmpName = "" Then Exit Sub

        Dim tmpCampaign As Trinity.cKampanj = Campaign.Campaigns(cmbCampaigns.SelectedItem)
        tmpCampaign.Name = TmpName

        Campaign.Campaigns.Remove(cmbCampaigns.SelectedItem.ToString)
        Campaign.Campaigns.Add(TmpName, tmpCampaign)
        cmbCampaigns.Items.Clear()
        For Each kv As KeyValuePair(Of String, Trinity.cKampanj) In Campaign.Campaigns
            cmbCampaigns.Items.Add(kv.Key)
            cmbProfileCampaign.Items.Add(kv.Key)
        Next

    End Sub

    Private Sub cmdAddChannel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddChannel.Click
        Saved = False
        Dim Ready As Boolean = False
        For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
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
        For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If TmpBT.ShowMe Then
                    For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                        grdCompensation.Rows.Add()
                        grdCompensation.Rows(grdCompensation.Rows.Count - 1).Tag = TmpComp
                    Next
                End If
            Next
        Next
        For Each TmpC As Trinity.cCombination In ActiveCampaign.Combinations
            If TmpC.ShowAsOne Then
                For Each TmpComp As Trinity.cCompensation In TmpC.Relations(1).Bookingtype.Compensations
                    grdCompensation.Rows.Add()
                    grdCompensation.Rows(grdCompensation.Rows.Count - 1).Tag = TmpComp.ID
                    grdCompensation.Rows(grdCompensation.Rows.Count - 1).Cells(0).Tag = TmpC
                Next
            End If
        Next
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
                        For Each c As Trinity.cCombination In ActiveCampaign.Combinations
                            If c.ShowAsOne Then
                                .Items.Add(c)
                            End If
                        Next
                        For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
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
                        For Each c As Trinity.cCombination In ActiveCampaign.Combinations
                            If c.ShowAsOne Then
                                .Items.Add(c)
                            End If
                        Next
                        For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
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

        'grdSpotCount.Invalidate()
        grdSumChannels.Invalidate()
        grdSumWeeks.Invalidate()
        SkipIt = False
    End Sub

    Private Sub cmdLockOnBudget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLockOnBudget.Click
        changeTRPLockOnAll(False)
    End Sub

    Private Sub cmdLockOnTRP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLockOnTRP.Click
        changeTRPLockOnAll(True)
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

        Windows.Forms.Clipboard.SetDataObject(sb.ToString(), True)
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

        Windows.Forms.Clipboard.SetDataObject(sb.ToString(), True)

    End Sub

    Private Sub grdDiscounts_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdDiscounts.CellMouseClick
        Trinity.AllocateFunctions.CellMouseClick(sender, e, ActiveCampaign)
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

        SB.Append("Copied from Films for booking type " & cmbFilmChannel.SelectedItem.name & " in the campaign " & ActiveCampaign.Name & vbNewLine)

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

        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim mnu As New System.Windows.Forms.ContextMenuStrip

            Dim item As System.Windows.Forms.ToolStripItem = mnu.Items.Add("Copy to clipboard", Nothing, AddressOf IndexToClipBoard)
            item.Tag = "CopyDiscounts"
            item.Name = "CopyDiscounts"

            mnu.Show(Me.ParentForm, New System.Drawing.Point(MousePosition.X, MousePosition.Y - 15))
        End If
    End Sub

    Private Sub IndexToClipBoard()

        Dim SB As New System.Text.StringBuilder
        Dim CellFormat As String = "N0"

        SB.Append("Copied from Indexes in the campaign " & ActiveCampaign.Name & vbNewLine)

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

    Private Sub frmLab_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        ' BLT.Destroy()
    End Sub
End Class