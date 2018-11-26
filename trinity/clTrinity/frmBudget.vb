Imports System.Windows.Forms
Imports System.Xml.Linq

Public Class frmBudget

    'This form draws up a table regarding the Campaign budget
    'The budget is only available if there is a campaign set up

    'The Pushed and Needed subs are used to update the grid when changes are made to a cell

    Private Sub grdBudget_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdBudget.CellValueNeeded
        If Not grdBudget.Rows(e.RowIndex).Tag Is Nothing AndAlso grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            If e.RowIndex < grdBudget.RowCount - 1 Then
                If e.ColumnIndex = 0 Then
                    e.Value = DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cCombination).Name
                ElseIf e.ColumnIndex = 4 Then 'PlannedGrossBudget
                    Dim Sum As Double = 0
                    For Each channel As Trinity.cCombinationChannel In DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations
                        Sum += channel.Bookingtype.PlannedGrossBudget
                    Next
                    e.Value = Format(Sum, "N0")
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf e.ColumnIndex = 5 Then 'PlannedNetBudget

                    Dim Sum As Double = 0
                    For Each channel As Trinity.cCombinationChannel In DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations
                        If channel.Bookingtype.IsSpecific Then
                            Dim Budget As Decimal = 0
                            For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                If TmpSpot.Bookingtype Is channel.Bookingtype Then
                                    Budget += TmpSpot.GrossPrice * TmpSpot.AddedValueIndex
                                End If
                                Sum += Budget
                            Next
                            e.Value = Format(Sum, "N0")
                            grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                            grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Black
                        Else
                            Sum += channel.Bookingtype.PlannedNetBudget
                        End If
                    Next
                    e.Value = Format(Sum, "N0")
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Gray
                ElseIf e.ColumnIndex = 6 Then 'ConfirmedGrossBudget
                    Dim Sum As Double = 0
                    For Each channel As Trinity.cCombinationChannel In DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations
                        Sum += channel.Bookingtype.ConfirmedGrossBudget
                    Next
                    e.Value = Format(Sum, "N0")
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf e.ColumnIndex = 7 Then 'PlannedNetBudget
                    Dim Sum As Double = 0
                    For Each channel As Trinity.cCombinationChannel In DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations
                        Sum += channel.Bookingtype.PlannedNetBudget
                    Next
                    e.Value = Format(Sum, "N0")
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf e.ColumnIndex = 8 Then 'PlannedNetBudget

                    Dim Sum As Double = 0
                    For Each channel As Trinity.cCombinationChannel In DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations
                        If channel.Bookingtype.IsSpecific Then
                            Dim Budget As Decimal = 0
                            For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                If TmpSpot.Bookingtype Is channel.Bookingtype Then
                                    Budget += TmpSpot.NetPrice * TmpSpot.AddedValueIndex
                                End If
                                Sum += Budget
                            Next
                            e.Value = Format(Sum, "N0")
                            grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                            grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Black
                        Else
                            Sum += channel.Bookingtype.PlannedNetBudget
                        End If
                    Next
                    e.Value = Format(Sum, "N0")
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Gray

                ElseIf e.ColumnIndex = 9 Then 'PlannedNetBudget
                    Dim Sum As Double = 0
                    For Each channel As Trinity.cCombinationChannel In DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations
                        Sum += channel.Bookingtype.ConfirmedNetBudget
                    Next
                    e.Value = Format(Sum, "N0")
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                End If
            End If
        Else

            If e.RowIndex < grdBudget.RowCount - 1 Then
                If e.ColumnIndex = 0 Then
                    e.Value = DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).ParentChannel.ChannelName
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf e.ColumnIndex = 1 Then
                    e.Value = DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).Name
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf e.ColumnIndex = 2 Then
                    e.Value = DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).ContractNumber
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = False
                ElseIf e.ColumnIndex = 3 Then
                    e.Value = DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).OrderNumber
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = False
                ElseIf e.ColumnIndex = 4 Then
                    e.Value = Format(DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).PlannedGrossBudget, "N0")
                    'grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf e.ColumnIndex = 5 Then
                    If DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).IsCompensation Then
                        e.Value = Format(0, "N0")
                    ElseIf DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).IsSpecific Then
                        Dim Budget As Decimal = 0
                        For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                            If TmpSpot.Bookingtype Is DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType) Then
                                Budget += TmpSpot.GrossPrice * TmpSpot.AddedValueIndex(False)
                            End If
                        Next
                        e.Value = Format(Budget, "N0")
                        grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                    Else
                        e.Value = Format(DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).PlannedGrossBudget, "N0")
                        grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Gray
                    End If
                ElseIf e.ColumnIndex = 6 Then
                    e.Value = Format(DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).ConfirmedGrossBudget, "N0")
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = False
                ElseIf e.ColumnIndex = 7 Then
                    e.Value = Format(DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).PlannedNetBudget, "N0")
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf e.ColumnIndex = 8 Then
                    If DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).IsCompensation Then
                        e.Value = Format(0, "N0")
                    ElseIf DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).IsSpecific Then
                        Dim Budget As Decimal = 0
                        For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                            If TmpSpot.Bookingtype Is DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType) Then
                                Budget += TmpSpot.NetPrice * TmpSpot.AddedValueIndex
                            End If
                        Next
                        e.Value = Format(Budget, "N0")
                        grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                        grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Black
                    Else
                        e.Value = Format(DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).PlannedNetBudget, "N0")
                        grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Gray
                    End If
                ElseIf e.ColumnIndex = 9 Then
                    e.Value = Format(DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).ConfirmedNetBudget, "N0")
                    grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = Not TrinitySettings.UseConfirmedBudget
                End If
                grdBudget.EditMode = Windows.Forms.DataGridViewEditMode.EditOnEnter
                grdBudget.Rows(e.RowIndex).DefaultCellStyle.BackColor = Drawing.Color.White
            Else
                If e.ColumnIndex > 3 Then
                    Dim Sum As Single = 0
                    For i As Integer = 0 To grdBudget.RowCount - 2
                        Sum += grdBudget.Rows(i).Cells(e.ColumnIndex).Tag
                    Next
                    e.Value = Format(Sum, "N0")
                End If
                grdBudget.Rows(e.RowIndex).DefaultCellStyle.BackColor = Drawing.Color.DarkGray
            End If
            grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = e.Value
        End If
    End Sub

    Private Sub grdLink_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdLink.CellValueNeeded
        If e.RowIndex < grdLink.RowCount - 1 Then
            Dim _linkedCamp As cLinkedCampaignChannel = grdLink.Rows(e.RowIndex).Tag
            Select Case e.ColumnIndex
                Case 0, 1
                    e.Value = grdBudget.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    grdLink.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                Case 2
                    e.Value = Format(_linkedCamp.BookedTRP, "N1")
                Case 3
                    e.Value = Format(_linkedCamp.ActualTRP, "N1")
                Case 4
                    e.Value = Format(_linkedCamp.PlannedGross, "N0")
                Case 5
                    e.Value = Format(_linkedCamp.BookedGross, "N0")
                Case 6
                    e.Value = Format(_linkedCamp.ConfirmedGross, "N0")
                Case 7
                    e.Value = Format(_linkedCamp.PlannedNet, "N0")
                Case 8
                    e.Value = Format(_linkedCamp.BookedNet, "N0")
                Case 9
                    e.Value = Format(_linkedCamp.ConfirmedNet, "N0")
            End Select
            'ElseIf e.ColumnIndex = 2 Then
            '    e.Value = DirectCast(grdLink.Rows(e.RowIndex).Tag, Trinity.cBookingType).ContractNumber
            '    grdLink.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = False
            'ElseIf e.ColumnIndex = 3 Then
            '    e.Value = DirectCast(grdLink.Rows(e.RowIndex).Tag, Trinity.cBookingType).OrderNumber
            '    grdLink.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = False
            'ElseIf e.ColumnIndex = 4 Then
            '    e.Value = Format(DirectCast(grdLink.Rows(e.RowIndex).Tag, Trinity.cBookingType).PlannedGrossBudget, "N0")
            '    grdLink.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
            'ElseIf e.ColumnIndex = 5 Then
            '    If DirectCast(grdLink.Rows(e.RowIndex).Tag, Trinity.cBookingType).IsSpecific Then
            '        Dim Budget As Decimal = 0
            '        For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
            '            If TmpSpot.Bookingtype Is DirectCast(grdLink.Rows(e.RowIndex).Tag, Trinity.cBookingType) Then
            '                Budget += TmpSpot.GrossPrice * TmpSpot.AddedValueIndex(False)
            '            End If
            '        Next
            '        e.Value = Format(Budget, "N0")
            '        grdLink.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
            '    Else
            '        e.Value = Format(DirectCast(grdLink.Rows(e.RowIndex).Tag, Trinity.cBookingType).PlannedGrossBudget, "N0")
            '        grdLink.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Gray
            '    End If
            'ElseIf e.ColumnIndex = 6 Then
            '    e.Value = Format(DirectCast(grdLink.Rows(e.RowIndex).Tag, Trinity.cBookingType).ConfirmedGrossBudget, "N0")
            '    grdLink.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
            'ElseIf e.ColumnIndex = 7 Then
            '    e.Value = Format(DirectCast(grdLink.Rows(e.RowIndex).Tag, Trinity.cBookingType).PlannedNetBudget, "N0")
            '    grdLink.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
            'ElseIf e.ColumnIndex = 8 Then
            '    If DirectCast(grdLink.Rows(e.RowIndex).Tag, Trinity.cBookingType).IsSpecific Then
            '        Dim Budget As Decimal = 0
            '        For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
            '            If TmpSpot.Bookingtype Is DirectCast(grdLink.Rows(e.RowIndex).Tag, Trinity.cBookingType) Then
            '                Budget += TmpSpot.NetPrice * TmpSpot.AddedValueIndex
            '            End If
            '        Next
            '        e.Value = Format(Budget, "N0")
            '        grdLink.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
            '        grdLink.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Black
            '    Else
            '        e.Value = Format(DirectCast(grdLink.Rows(e.RowIndex).Tag, Trinity.cBookingType).PlannedNetBudget, "N0")
            '        grdLink.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Gray
            '    End If
            'ElseIf e.ColumnIndex = 9 Then
            '    e.Value = Format(DirectCast(grdLink.Rows(e.RowIndex).Tag, Trinity.cBookingType).ConfirmedNetBudget, "N0")
            '    grdLink.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = Not TrinitySettings.UseConfirmedBudget
            grdLink.EditMode = Windows.Forms.DataGridViewEditMode.EditOnEnter
        Else
            If e.ColumnIndex > 1 Then
                Dim Sum As Single = 0
                For i As Integer = 0 To grdLink.RowCount - 2
                    If grdLink.Rows(i).Tag.Sum Then
                        Sum += grdLink.Rows(i).Cells(e.ColumnIndex).Tag
                    End If
                Next
                If e.ColumnIndex > 3 Then
                    e.Value = Format(Sum, "N0")
                Else
                    e.Value = Format(Sum, "N1")
                End If
            End If
            grdLink.Rows(e.RowIndex).DefaultCellStyle.BackColor = Drawing.Color.DarkGray
        End If
        grdLink.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = e.Value
    End Sub

    Private Sub frmBudget_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        grdBudget.Rows.Clear()
        Dim alreadyPrinted As New List(Of String)

        For Each combination As Trinity.cCombination In Campaign.Combinations
            For Each combochannel As Trinity.cCombinationChannel In combination.Relations
                If combochannel.Bookingtype.BookIt Then
                    grdBudget.Rows.Add()
                    alreadyPrinted.Add(combochannel.Bookingtype.ParentChannel.ChannelName & combochannel.Bookingtype.Name)
                    grdBudget.Rows(grdBudget.Rows.Count - 1).Tag = combochannel.Bookingtype
                    grdBudget.AutoResizeRow(grdBudget.Rows.Count - 1, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells)

                    'grdMetrics.Rows.Add()
                    'grdMetrics.Rows(grdBudget.Rows.Count - 1).Tag = combochannel.Bookingtype
                    'grdMetrics.AutoResizeRow(grdBudget.Rows.Count - 1, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells)
                End If
            Next

            grdBudget.Rows.Add()
            grdBudget.Rows(grdBudget.Rows.Count - 1).Tag = combination
            grdBudget.Rows(grdBudget.Rows.Count - 1).DefaultCellStyle.BackColor = Drawing.Color.LightGray
            grdBudget.AutoResizeRow(grdBudget.Rows.Count - 1, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells)

            'grdMetrics.Rows.Add()
            'grdMetrics.Rows(grdBudget.Rows.Count - 1).Tag = combination
            'grdMetrics.Rows(grdBudget.Rows.Count - 1).DefaultCellStyle.BackColor = Drawing.Color.LightGray
            'grdMetrics.AutoResizeRow(grdBudget.Rows.Count - 1, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells)
        Next
        'Goes through all bookings in all the channels in the campaign and then add the booking into the budget grid
        For Each TmpChan In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.BookIt And Not alreadyPrinted.Contains(TmpChan.ChannelName & TmpBT.Name) Then
                    'adds a empty row
                    grdBudget.Rows.Add()
                    'put TmpBT into the newly created row
                    grdBudget.Rows(grdBudget.Rows.Count - 1).Tag = TmpBT
                    grdBudget.AutoResizeRow(grdBudget.Rows.Count - 1, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells)

                    grdMetrics.Rows.Add()
                    'put TmpBT into the newly created row
                    grdMetrics.Rows(grdMetrics.Rows.Count - 1).Tag = TmpBT
                    grdMetrics.AutoResizeRow(grdMetrics.Rows.Count - 1, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells)
                End If
            Next
        Next
        'puts a empty line in the bottom of the grid after populating it with all the bookings
        grdBudget.Rows.Add()
        grdBudget.AutoResizeRow(grdBudget.Rows.Count - 1, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells)

        grdMetrics.Rows.Add()
        grdMetrics.AutoResizeRow(grdMetrics.Rows.Count - 1, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells)
        grdMetrics.Height = grdBudget.Height
    End Sub

    Private Sub grdBudget_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdBudget.CellValuePushed

        Try
            Dim numberCandidate As Double = CDbl(e.Value)
            If numberCandidate < 0 Then Exit Sub
        Catch ex As Exception
            Exit Sub
        End Try

        If e.ColumnIndex = 2 Then
            DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).ContractNumber = e.Value
        ElseIf e.ColumnIndex = 3 Then
            DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).OrderNumber = e.Value
        ElseIf e.ColumnIndex = 6 Then 'Confirmed gross
            DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).ConfirmedGrossBudget = e.Value
            grdBudget.Invalidate()
        ElseIf e.ColumnIndex = 9 Then
            If Not grdBudget.Rows(e.RowIndex).Tag Is Nothing AndAlso Not grdBudget.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                DirectCast(grdBudget.Rows(e.RowIndex).Tag, Trinity.cBookingType).ConfirmedNetBudget = e.Value
                grdBudget.InvalidateCell(grdBudget.Rows(e.RowIndex).Cells(6))
            End If
        End If
        grdBudget.InvalidateRow(grdBudget.RowCount - 1)
    End Sub

    Private Sub frmBudget_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        grdBudget.Anchor = Windows.Forms.AnchorStyles.None
        grdBudget.Anchor = Windows.Forms.AnchorStyles.Left
        grdBudget.Anchor = Windows.Forms.AnchorStyles.Top
        grdBudget.Size = grdBudget.PreferredSize
        grdBudget.Height = grdBudget.GetRowDisplayRectangle(grdBudget.Rows.Count - 1, False).Bottom + 1
        grdBudget.Width = grdBudget.GetColumnDisplayRectangle(grdBudget.Columns.Count - 1, False).Right + 1
        Me.Height = grdBudget.Height + (tabBudget.Height - tpCampaign.Height) + StatusStrip1.Height + 27
        Me.Width = grdBudget.Width + (tabBudget.Width - tpCampaign.Width) + 10

        grdLink.Anchor = Windows.Forms.AnchorStyles.None
        grdLink.Anchor = Windows.Forms.AnchorStyles.Left
        grdLink.Anchor = Windows.Forms.AnchorStyles.Top
        grdLink.Size = grdBudget.Size

        grdProduct.Anchor = Windows.Forms.AnchorStyles.None
        grdProduct.Anchor = Windows.Forms.AnchorStyles.Left
        grdProduct.Anchor = Windows.Forms.AnchorStyles.Top
        grdProduct.Size = grdBudget.Size

        grdClient.Anchor = Windows.Forms.AnchorStyles.None
        grdClient.Anchor = Windows.Forms.AnchorStyles.Left
        grdClient.Anchor = Windows.Forms.AnchorStyles.Top
        grdClient.Size = grdBudget.Size

        tpLinked.Enabled = (Campaign.LinkedCampaigns.Count > 0)

        tpLinked.Visible = (TrinitySettings.SaveCampaignsAsFiles)
        tpClientTotal.Visible = Not (TrinitySettings.SaveCampaignsAsFiles)
        tpProductTotal.Visible = Not (TrinitySettings.SaveCampaignsAsFiles)

    End Sub

    Private Sub tabBudget_SelectedIndexChanging(ByVal sender As Object, ByVal e As TabSelectionChangingArgs) Handles tabBudget.SelectedIndexChanging
        If Not tabBudget.TabPages(e.NewTabIndex).Enabled Then
            e.Cancel = True
        End If
    End Sub

    Private Sub grdBudget_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdBudget.SelectionChanged
        If grdBudget.SelectedCells.Count = 1 Then
            ToolStripStatusLabel2.Text = ""
            Exit Sub
        End If

        Dim sumOfSelected As Double = 0
        For Each cell As DataGridViewCell In grdBudget.SelectedCells
            If cell.ColumnIndex > 3 Then
                sumOfSelected += CType(cell.Value, Double)
            End If
        Next
        ToolStripStatusLabel2.Text = sumOfSelected
    End Sub

    Private Sub tpLinked_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpLinked.Enter
        If grdLink.Rows.Count > 0 Then

        Else
            Dim frmProg As New frmProgress
            frmProg.Status = "Reading linked campaigns..."
            frmProg.Show()
            Dim r As Integer = 0
            For Each _row As DataGridViewRow In grdBudget.Rows
                frmProg.Progress = (r / grdBudget.Rows.Count) * 100
                If _row.Tag IsNot Nothing Then
                    Dim _camp As New cLinkedCampaignChannel(_row.Tag)
                    For Each _linkCampaign As Trinity.cLinkedCampaign In Campaign.LinkedCampaigns
                        If _linkCampaign.Link Then
                            _camp.AddCampaign(_linkCampaign.Path)
                        End If
                    Next
                    With grdLink.Rows(grdLink.Rows.Add)
                        .Tag = _camp
                        .Height = _row.Height
                        .DefaultCellStyle = _row.DefaultCellStyle
                    End With
                    _camp.AddCampaign(Campaign.Filename)
                Else
                    With grdLink.Rows(grdLink.Rows.Add)
                        .Height = _row.Height
                        .DefaultCellStyle = _row.DefaultCellStyle
                    End With
                End If                    
                r += 1
            Next
            frmProg.Close()
            frmProg.Dispose()
        End If
    End Sub

    Private Class cLinkedCampaignChannel
        Private _channel As Trinity.cChannel
        Private _bookingtypes As New List(Of Trinity.cBookingType)
        Private _bookedTRP As Single = 0
        Private _actualTRP As Single = 0
        Private _plannedGross As Decimal = 0
        Private _bookedGross As Decimal = 0
        Private _confirmedGross As Decimal = 0
        Private _plannedNet As Decimal = 0
        Private _bookedNet As Decimal = 0
        Private _confirmedNet As Decimal = 0
        Private _sum As Boolean = True

        Public Sub New(ByVal obj As Object)
            If obj.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                For Each _rel As Trinity.cCombinationChannel In obj.Relations
                    _bookingtypes.Add(_rel.Bookingtype)
                Next
                _sum = False
            Else
                _bookingtypes.Add(obj)
            End If
        End Sub

        Public Sub AddCampaign(ByVal Path As String)
            Dim XMLDoc As New Xml.XmlDocument
            XMLDoc.Load(Path)

            Try
                Dim XMLCamp As Xml.XmlElement = XMLDoc.GetElementsByTagName("Campaign")(0)

                For Each _bookingType As Trinity.cBookingType In _bookingtypes
                    Dim XMLBookingType As Xml.XmlElement = XMLCamp.SelectSingleNode("Channels/Channel[@Name='" & _bookingType.ParentChannel.ChannelName & "']/BookingTypes/BookingType[@Name='" & _bookingType.Name & "']")

                    If XMLBookingType IsNot Nothing Then
                        For Each XMLWeek As Xml.XmlElement In XMLBookingType.SelectSingleNode("Weeks").ChildNodes
                            _bookedTRP += XMLWeek.GetAttribute("TRPBuyingTarget")
                            _plannedGross += XMLWeek.GetAttribute("GrossBudget")
                            _plannedNet += XMLWeek.GetAttribute("NetBudget")
                            If XMLBookingType.GetAttribute("ConfirmedNetBudget") = 0 Then
                                _confirmedNet += XMLWeek.GetAttribute("NetBudget")
                            End If
                            If Not _bookingType.IsSpecific Then
                                _bookedGross += XMLWeek.GetAttribute("GrossBudget")
                                _bookedNet += XMLWeek.GetAttribute("NetBudget")
                            End If
                        Next
                        If XMLBookingType.GetAttribute("ConfirmedNetBudget") > 0 Then
                            _confirmedNet += XMLBookingType.GetAttribute("ConfirmedNetBudget")
                        End If
                        If _plannedGross > 0 Then
                            _confirmedGross = _confirmedNet / (_plannedNet / _plannedGross)
                        End If
                        For Each XMLSpot As Xml.XmlElement In XMLCamp.SelectNodes("ActualSpots/Spot[@Channel='" & _bookingType.ParentChannel.ChannelName & "' and @BookingType='" & _bookingType.Name & "']")
                            _actualTRP += XMLSpot.GetAttribute("RatingBuying")
                        Next
                        If _bookingType.IsSpecific Then
                            For Each XMLSpot As Xml.XmlElement In XMLCamp.SelectNodes("BookedSpots/Spot[@Channel='" & _bookingType.ParentChannel.ChannelName & "' and @Bookingtype='" & _bookingType.Name & "']")
                                _bookedNet += XMLSpot.GetAttribute("NetPrice")
                                _bookedGross += XMLSpot.GetAttribute("GrossPrice")
                            Next
                        End If
                    End If
                Next

            Catch ex As Exception

            End Try

        End Sub

        Public ReadOnly Property Sum() As Boolean
            Get
                Return _sum
            End Get
        End Property

        Public ReadOnly Property Channel() As Trinity.cChannel
            Get
                Return _channel
            End Get
        End Property

        Public ReadOnly Property BookedTRP() As Single
            Get
                Return _BookedTRP
            End Get
        End Property

        Public ReadOnly Property ActualTRP() As Single
            Get
                Return _actualTRP
            End Get
        End Property

        Public ReadOnly Property PlannedGross() As Decimal
            Get
                Return _plannedGross
            End Get
        End Property

        Public ReadOnly Property BookedGross() As Decimal
            Get
                Return _bookedGross
            End Get
        End Property

        Public ReadOnly Property ConfirmedGross() As Decimal
            Get
                Return _confirmedGross
            End Get
        End Property

        Public ReadOnly Property PlannedNet() As Decimal
            Get
                Return _plannedNet
            End Get
        End Property

        Public ReadOnly Property BookedNet() As Decimal
            Get
                Return _bookedNet
            End Get
        End Property

        Public ReadOnly Property ConfirmedNet() As Decimal
            Get
                Return _confirmedNet
            End Get
        End Property
    End Class

   
    Private Sub grdMetrics_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdMetrics.CellValueNeeded

        If Not grdMetrics.Rows(e.RowIndex).Tag Is Nothing AndAlso grdMetrics.Rows(e.RowIndex).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
            If e.RowIndex < grdMetrics.RowCount - 1 Then
                If e.ColumnIndex = 0 Then
                    e.Value = DirectCast(grdMetrics.Rows(e.RowIndex).Tag, Trinity.cCombination).Name
                ElseIf e.ColumnIndex = 4 Then 'PlannedGrossBudget
                    Dim Sum As Double = 0
                    For Each channel As Trinity.cCombinationChannel In DirectCast(grdMetrics.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations
                        Sum += channel.Bookingtype.PlannedGrossBudget
                    Next
                    e.Value = Format(Sum, "N0")
                    grdMetrics.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf e.ColumnIndex = 5 Then 'PlannedNetBudget

                    Dim Sum As Double = 0
                    For Each channel As Trinity.cCombinationChannel In DirectCast(grdMetrics.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations
                        If channel.Bookingtype.IsSpecific Then
                            Dim Budget As Decimal = 0
                            For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                If TmpSpot.Bookingtype Is channel.Bookingtype Then
                                    Budget += TmpSpot.GrossPrice * TmpSpot.AddedValueIndex
                                End If
                                Sum += Budget
                            Next
                            e.Value = Format(Sum, "N0")
                            grdMetrics.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                            grdMetrics.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Black
                        Else
                            Sum += channel.Bookingtype.PlannedNetBudget
                        End If
                    Next
                    e.Value = Format(Sum, "N0")
                    grdMetrics.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Gray
                ElseIf e.ColumnIndex = 6 Then 'ConfirmedGrossBudget
                    Dim Sum As Double = 0
                    For Each channel As Trinity.cCombinationChannel In DirectCast(grdMetrics.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations
                        Sum += channel.Bookingtype.ConfirmedGrossBudget
                    Next
                    e.Value = Format(Sum, "N0")
                    grdMetrics.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf e.ColumnIndex = 7 Then 'PlannedNetBudget
                    Dim Sum As Double = 0
                    For Each channel As Trinity.cCombinationChannel In DirectCast(grdMetrics.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations
                        Sum += channel.Bookingtype.PlannedNetBudget
                    Next
                    e.Value = Format(Sum, "N0")
                    grdMetrics.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf e.ColumnIndex = 8 Then 'PlannedNetBudget

                    Dim Sum As Double = 0
                    For Each channel As Trinity.cCombinationChannel In DirectCast(grdMetrics.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations
                        If channel.Bookingtype.IsSpecific Then
                            Dim Budget As Decimal = 0
                            For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                If TmpSpot.Bookingtype Is channel.Bookingtype Then
                                    Budget += TmpSpot.NetPrice * TmpSpot.AddedValueIndex
                                End If
                                Sum += Budget
                            Next
                            e.Value = Format(Sum, "N0")
                            grdMetrics.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                            grdMetrics.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Black
                        Else
                            Sum += channel.Bookingtype.PlannedNetBudget
                        End If
                    Next
                    e.Value = Format(Sum, "N0")
                    grdMetrics.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Gray

                ElseIf e.ColumnIndex = 9 Then 'PlannedNetBudget
                    Dim Sum As Double = 0
                    For Each channel As Trinity.cCombinationChannel In DirectCast(grdMetrics.Rows(e.RowIndex).Tag, Trinity.cCombination).Relations
                        Sum += channel.Bookingtype.ConfirmedNetBudget
                    Next
                    e.Value = Format(Sum, "N0")
                    grdMetrics.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                End If
            End If
        Else
            'Not a combination
            If e.RowIndex < grdMetrics.RowCount - 1 Then
                If e.ColumnIndex = 0 Then
                    e.Value = DirectCast(grdMetrics.Rows(e.RowIndex).Tag, Trinity.cBookingType).ParentChannel.ChannelName
                    grdMetrics.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf e.ColumnIndex = 1 Then
                    e.Value = DirectCast(grdMetrics.Rows(e.RowIndex).Tag, Trinity.cBookingType).Name
                    grdMetrics.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf e.ColumnIndex = 2 Then
                    If DirectCast(grdMetrics.Rows(e.RowIndex).Tag, Trinity.cBookingType).PlannedTRP30 > 0 Then
                        e.Value = Format((DirectCast(grdMetrics.Rows(e.RowIndex).Tag, Trinity.cBookingType).PlannedNetBudget / (DirectCast(grdMetrics.Rows(e.RowIndex).Tag, Trinity.cBookingType).PlannedTRP30 / DirectCast(grdMetrics.Rows(e.RowIndex).Tag, Trinity.cBookingType).IndexSecondTarget)), "N0")
                    Else
                        e.Value = 0
                    End If
                    grdMetrics.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = False

                    grdMetrics.EditMode = Windows.Forms.DataGridViewEditMode.EditOnEnter
                    grdMetrics.Rows(e.RowIndex).DefaultCellStyle.BackColor = Drawing.Color.White
                Else
                    If e.ColumnIndex > 3 Then
                        Dim Sum As Single = 0
                        For i As Integer = 0 To grdMetrics.RowCount - 2
                            Sum += grdMetrics.Rows(i).Cells(e.ColumnIndex).Tag
                        Next
                        e.Value = Format(Sum, "N0")
                    End If
                    grdMetrics.Rows(e.RowIndex).DefaultCellStyle.BackColor = Drawing.Color.DarkGray
                End If
                grdMetrics.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = e.Value
            Else
                Dim trp30 As Double
                Dim budget As Double
                If e.ColumnIndex = 2 Then
                    For Each chan As Trinity.cChannel In Campaign.Channels
                        For Each bt As Trinity.cBookingType In chan.BookingTypes
                            trp30 += bt.PlannedTRP30
                            budget += bt.PlannedNetBudget
                        Next
                    Next
                    e.Value = Format(budget / trp30, "N0")
                End If
            End If
        End If
    End Sub

    Private Sub grdLink_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLink.CellContentClick

    End Sub

    Private Sub tpClientTotal_Enter(sender As Object, e As System.EventArgs) Handles tpClientTotal.Enter
        If grdClient.Rows.Count > 0 Then

        Else
            Dim frmProg As New frmProgress
            frmProg.Status = "Reading campaigns..."
            frmProg.Show()
            Dim _campaigns As List(Of CampaignEssentials) = DBReader.GetCampaigns("SELECT * FROM campaigns WHERE client=" & Campaign.ClientID & " AND (StartDate<='" & Year(Date.FromOADate(Campaign.StartDate + (Campaign.EndDate - Campaign.StartDate) / 2)) & "-12-31' OR EndDate>='" & Year(Date.FromOADate(Campaign.StartDate + (Campaign.EndDate - Campaign.StartDate) / 2)) & "-01-01')")
            Dim c As Integer = 0
            Dim _bts As New Dictionary(Of String, CampaignEntry)
            For Each _ce As CampaignEssentials In _campaigns
                frmProg.Progress = (c / _campaigns.Count) * 100
                Dim _campXML As String = DBReader.GetCampaign(_ce.id, True)
                Dim _camp As XDocument = XDocument.Parse(_campXML)

                For Each _bt As XElement In _camp.<Campaign>.<Channels>.<Channel>.<BookingTypes>...<BookingType>.Where(Function(b) b.@BookIt)
                    Dim _chanName As String = _bt.Parent.Parent.@Name
                    Dim _btName As String = _bt.@Name
                    If Not _bts.ContainsKey(_chanName & " " & _btName) Then
                        _bts.Add(_chanName & " " & _btName, New CampaignEntry)
                        _bts(_chanName & " " & _btName).Key = _chanName & " " & _btName
                        _bts(_chanName & " " & _btName).Channel = _chanName
                        _bts(_chanName & " " & _btName).BookingType = _btName
                    End If
                    Dim _weeks As List(Of XElement) = _bt.<Weeks>...<Week>.ToList
                    _bts(_chanName & " " & _btName).BoughtTRP += _weeks.Sum(Function(w) w.@TRP)
                    _bts(_chanName & " " & _btName).ActualTRP += _camp.<Campaign>.<ActualSpots>...<Spot>.Where(Function(s) s.@Channel = _chanName AndAlso s.@BookingType = _btName).Sum(Function(s) CSng(s.@RatingMain))
                    _bts(_chanName & " " & _btName).PlannedGross += _weeks.Sum(Function(w) w.@GrossBudget)
                    _bts(_chanName & " " & _btName).PlannedNet += _weeks.Sum(Function(w) w.@NetBudget)
                    _bts(_chanName & " " & _btName).BookedGross += _camp.<Campaign>.<BookedSpots>...<Spot>.Where(Function(s) s.@Channel = _chanName AndAlso s.@BookingType = _btName).Sum(Function(s) CSng(s.@GrossPrice))
                    _bts(_chanName & " " & _btName).BookedNet += _camp.<Campaign>.<BookedSpots>...<Spot>.Where(Function(s) s.@Channel = _chanName AndAlso s.@BookingType = _btName).Sum(Function(s) CSng(s.@NetPrice))
                    Dim _confirmedGross As Single = _camp.<Campaign>.<BookedSpots>...<Spot>.Where(Function(s) s.@Channel = _chanName AndAlso s.@BookingType = _btName).Sum(Function(s) CSng(s.@GrossCost))
                    Dim _confirmedNet As Single = _camp.<Campaign>.<BookedSpots>...<Spot>.Where(Function(s) s.@Channel = _chanName AndAlso s.@BookingType = _btName).Sum(Function(s) CSng(s.@GrossCost))
                    If _confirmedNet = 0 Then
                        _confirmedNet = _weeks.Sum(Function(w) w.@NetBudget)
                        _confirmedGross = _weeks.Sum(Function(w) w.@GrossBudget)
                    End If
                    _bts(_chanName & " " & _btName).ConfirmedGross += _confirmedGross
                    _bts(_chanName & " " & _btName).ConfirmedNet += _confirmedNet
                Next
                c += 1
            Next
            For Each _bt As KeyValuePair(Of String, CampaignEntry) In _bts
                With grdClient.Rows(grdClient.Rows.Add)
                    .Tag = _bt.Value
                End With
            Next
            frmProg.Close()
            frmProg.Dispose()
        End If
    End Sub

    Private Sub tpProductTotal_Enter(sender As Object, e As System.EventArgs) Handles tpProductTotal.Enter
        If grdProduct.Rows.Count > 0 Then

        Else
            Dim frmProg As New frmProgress
            frmProg.Status = "Reading campaigns..."
            frmProg.Show()
            Dim _campaigns As List(Of CampaignEssentials) = DBReader.GetCampaigns("SELECT * FROM campaigns WHERE product=" & Campaign.ProductID & " AND (StartDate<='" & Year(Date.FromOADate(Campaign.StartDate + (Campaign.EndDate - Campaign.StartDate) / 2)) & "-12-31' OR EndDate>='" & Year(Date.FromOADate(Campaign.StartDate + (Campaign.EndDate - Campaign.StartDate) / 2)) & "-01-01')")
            Dim c As Integer = 0
            Dim _bts As New Dictionary(Of String, CampaignEntry)
            Dim _total As New CampaignEntry
            For Each _ce As CampaignEssentials In _campaigns
                frmProg.Progress = (c / _campaigns.Count) * 100
                Dim _campXML As String = DBReader.GetCampaign(_ce.id, True)
                Dim _camp As XDocument = XDocument.Parse(_campXML)

                For Each _bt As XElement In _camp.<Campaign>.<Channels>.<Channel>.<BookingTypes>...<BookingType>.Where(Function(b) b.@BookIt)
                    Dim _chanName As String = _bt.Parent.Parent.@Name
                    Dim _btName As String = _bt.@Name
                    If Not _bts.ContainsKey(_chanName & " " & _btName) Then
                        _bts.Add(_chanName & " " & _btName, New CampaignEntry)
                        _bts(_chanName & " " & _btName).Key = _chanName & " " & _btName
                        _bts(_chanName & " " & _btName).Channel = _chanName
                        _bts(_chanName & " " & _btName).BookingType = _btName
                    End If
                    Dim _weeks As List(Of XElement) = _bt.<Weeks>...<Week>.ToList
                    _bts(_chanName & " " & _btName).BoughtTRP += _weeks.Sum(Function(w) w.@TRP)
                    _bts(_chanName & " " & _btName).ActualTRP += _camp.<Campaign>.<ActualSpots>...<Spot>.Where(Function(s) s.@Channel = _chanName AndAlso s.@BookingType = _btName).Sum(Function(s) CSng(s.@RatingMain))
                    _bts(_chanName & " " & _btName).PlannedGross += _weeks.Sum(Function(w) w.@GrossBudget)
                    _bts(_chanName & " " & _btName).PlannedNet += _weeks.Sum(Function(w) w.@NetBudget)
                    _bts(_chanName & " " & _btName).BookedGross += _camp.<Campaign>.<BookedSpots>...<Spot>.Where(Function(s) s.@Channel = _chanName AndAlso s.@BookingType = _btName).Sum(Function(s) CSng(s.@GrossPrice))
                    _bts(_chanName & " " & _btName).BookedNet += _camp.<Campaign>.<BookedSpots>...<Spot>.Where(Function(s) s.@Channel = _chanName AndAlso s.@BookingType = _btName).Sum(Function(s) CSng(s.@NetPrice))
                    Dim _confirmedGross As Single = _camp.<Campaign>.<BookedSpots>...<Spot>.Where(Function(s) s.@Channel = _chanName AndAlso s.@BookingType = _btName).Sum(Function(s) CSng(s.@GrossCost))
                    Dim _confirmedNet As Single = _camp.<Campaign>.<BookedSpots>...<Spot>.Where(Function(s) s.@Channel = _chanName AndAlso s.@BookingType = _btName).Sum(Function(s) CSng(s.@GrossCost))
                    If _confirmedNet = 0 Then
                        _confirmedNet = _weeks.Sum(Function(w) w.@NetBudget)
                        _confirmedGross = _weeks.Sum(Function(w) w.@GrossBudget)
                    End If
                    _bts(_chanName & " " & _btName).ConfirmedGross += _confirmedGross
                    _bts(_chanName & " " & _btName).ConfirmedNet += _confirmedNet

                    _total.BoughtTRP += _weeks.Sum(Function(w) w.@TRP)
                    _total.ActualTRP += _camp.<Campaign>.<ActualSpots>...<Spot>.Where(Function(s) s.@Channel = _chanName AndAlso s.@BookingType = _btName).Sum(Function(s) CSng(s.@RatingMain))
                    _total.PlannedGross += _weeks.Sum(Function(w) w.@GrossBudget)
                    _total.PlannedNet += _weeks.Sum(Function(w) w.@NetBudget)
                    _total.BookedGross += _camp.<Campaign>.<BookedSpots>...<Spot>.Where(Function(s) s.@Channel = _chanName AndAlso s.@BookingType = _btName).Sum(Function(s) CSng(s.@GrossPrice))
                    _total.BookedNet += _camp.<Campaign>.<BookedSpots>...<Spot>.Where(Function(s) s.@Channel = _chanName AndAlso s.@BookingType = _btName).Sum(Function(s) CSng(s.@NetPrice))
                    _total.ConfirmedGross += _confirmedGross
                    _total.ConfirmedNet += _confirmedNet

                Next
                c += 1
            Next
            For Each _bt As KeyValuePair(Of String, CampaignEntry) In _bts
                With grdProduct.Rows(grdProduct.Rows.Add)
                    .Tag = _bt.Value
                End With
            Next
            With grdProduct.Rows(grdProduct.Rows.Add)
                .Tag = _total
            End With
            frmProg.Close()
            frmProg.Dispose()
        End If
    End Sub

    Private Class CampaignEntry

        Public Key As String

        Public Channel As String
        Public BookingType As String
 
        Public BoughtTRP As Single
        Public ActualTRP As Single
        Public PlannedGross As Decimal
        Public BookedGross As Decimal
        Public ConfirmedGross As Decimal
        Public PlannedNet As Decimal
        Public BookedNet As Decimal
        Public ConfirmedNet As Decimal

    End Class

    Private Sub grdClient_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdClient.CellFormatting, grdProduct.CellFormatting
        If e.RowIndex = sender.rowCount - 1 Then
            e.CellStyle.BackColor = Color.DarkGray
        End If
    End Sub

    Private Sub grdClient_CellValueNeeded(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdClient.CellValueNeeded, grdProduct.CellValueNeeded
        Dim _ce As CampaignEntry = sender.Rows(e.RowIndex).Tag

        Select Case e.ColumnIndex
            Case 0
                e.Value = _ce.Channel
            Case 1
                e.Value = _ce.BookingType
            Case 2
                e.Value = Format(_ce.BoughtTRP, "N1")
            Case 3
                e.Value = Format(_ce.ActualTRP, "N1")
            Case 4
                e.Value = Format(_ce.PlannedGross, "N0")
            Case 5
                e.Value = Format(_ce.BookedGross, "N0")
            Case 6
                e.Value = Format(_ce.ConfirmedGross, "N0")
            Case 7
                e.Value = Format(_ce.PlannedNet, "N0")
            Case 8
                e.Value = Format(_ce.BookedNet, "N0")
            Case 9
                e.Value = Format(_ce.ConfirmedNet, "N0")
        End Select
    End Sub

    Private Sub btnExportGridToExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnExportGridToExcel.Click
        Dim _excel As New CultureSafeExcel.Application(False)
        Dim _wb As CultureSafeExcel.Workbook = _excel.AddWorkbook

        With _wb.Sheets(1)

            ' Write Brand, Month a Campaign name in the budget
            .Cells(1, 1).Value = Campaign.Client
            .Cells(1, 2).Value = MonthName(Date.FromOADate(Campaign.StartDate).Month, True)
            .Cells(1, 3).Value = Campaign.Name

            For _c As Integer = 0 To grdBudget.ColumnCount - 1
                .Cells(1, _c + 4).Value = grdBudget.Columns(_c).HeaderText
                Dim _r As Integer = 1
                For Each _row As DataGridViewRow In grdBudget.Rows
                    If _c < 4 Then
                        .Cells(_r + 1, _c + 4).Value = _row.Cells(_c).Value
                    Else
                        .Cells(_r + 1, _c + 4).Value = CSng(_row.Cells(_c).Value)
                    End If
                    _r += 1
                Next
            Next
        End With
        _excel.Visible = True
    End Sub

    Private Sub grdBudget_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdBudget.CellContentClick

    End Sub
End Class