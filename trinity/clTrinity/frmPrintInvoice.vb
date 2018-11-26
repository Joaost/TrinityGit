Public Class frmPrintInvoice

    Dim dblNetBudget As Double = 0

    Private Sub frmPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmPrint.Click

        'if checked we save them as default for the user
        If chkSaveSettings.Checked Then
            'TrinitySettings.DefaultPrintBundled = chkBundled.Checked
            'TrinitySettings.DefaultPrintBundledSingle = chkSingle.Checked
            TrinitySettings.DefaultPrintWeekBudget = chkWeeklyBudget.Checked
            TrinitySettings.DefaultPrintFilmBudget = chkBudgetFilm.Checked

            If rdbNot.Checked Then
                TrinitySettings.DefaultPrintCombinations = 0
            ElseIf rdbOne.Checked Then
                TrinitySettings.DefaultPrintCombinations = 1
            Else
                TrinitySettings.DefaultPrintCombinations = 2
            End If
            TrinitySettings.DefaultPrintCombinedSingle = chkCombinedSingle.Checked
        End If

        Dim Excel As CultureSafeExcel.Application
        Dim r As Integer
        Dim TopRow As Integer

        Dim lstBundled As New List(Of String)
        Dim listPrinted As New List(Of String)

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        On Error GoTo ErrHandle

        Excel = New CultureSafeExcel.Application(False)

        'Excel = CreateObject("CultureSafeExcel")
        Dim product As DataTable = DBReader.getAllFromProducts(Campaign.ProductID)
        Dim Client As String = ""
        If product.Rows.Count = 1 Then
            For Each rd As DataRow In product.Rows
                Client = rd("MarathonClient")
            Next
        End If

        'write the head of the page
        With Excel.AddWorkbook().Sheets(1)
            .Name = "Channel"
            .AllCells.Interior.Color = RGB(255, 255, 255)
            .AllCells.Font.Bold = True
            .AllCells.Rows.RowHeight = 20.25
            With .Range("A1:J1")
                .Merge()
                .Interior.ColorIndex = 37
                .Font.Italic = True
                .Font.Size = 14
                .Value = "Invoicing details - " & Campaign.Name
                .HorizontalAlignment = -4108
            End With
            .Cells(3, 1).Value = "CLIENT:"
            .Cells(4, 1).Value = "PRODUCT:"
            .Cells(5, 1).Value = "PLANNER: "
            If Not Client = "" Then
                .Cells(3, 2).Value = Campaign.Client & "(" & Client & ")"
            Else
                .Cells(3, 2).Value = Campaign.Client
            End If
            .Cells(4, 2).Value = Campaign.Product
            .Cells(5, 2).Value = Campaign.Buyer

            'start printing channels at row 7
            r = 7

            'go through all combinations
            For Each comb As Trinity.cCombination In Campaign.Combinations

                If rdbAll.Checked OrElse (rdbOne.Checked AndAlso comb.ShowAsOne) Then
                    'print the header for the channel
                    With .Range("A" & r & ":J" & r)
                        .Merge()
                        .Font.Size = 12
                        .Font.Color = RGB(255, 255, 255)
                        .Interior.Color = 0
                        .HorizontalAlignment = -4108
                        .Value = comb.Name
                    End With
                    r += 1
                    .Cells(r, 1).Value = "Ordernr"
                    .Cells(r, 2).Value = "Start date"
                    .Cells(r, 3).Value = "End date"
                    .Cells(r, 4).Value = "Weeks"
                    .Cells(r, 5).Value = "Bookingtype"
                    .Cells(r, 6).Value = "Spots"
                    .Cells(r, 7).Value = "Spot lengh"
                    .Cells(r, 8).Value = "Gross"
                    .Cells(r, 9).Value = "Net"
                    .Cells(r, 10).Value = "Discount"

                    TopRow = r + 1
                    'print the bookingtype one by one
                    For Each cc As Trinity.cCombinationChannel In comb.Relations
                        r += 1
                        'we add them to the list if printed bookingtypes only if we are not supposed to print them as singles aswell
                        If Not chkCombinedSingle.Checked Then
                            listPrinted.Add(cc.Bookingtype.ToString)
                        End If

                        Dim strFilms As String = ""
                        For z As Integer = 1 To cc.Bookingtype.Weeks(1).Films.Count
                            If Not strFilms.Contains(cc.Bookingtype.Weeks(1).Films(z).FilmLength) Then
                                strFilms = strFilms & "," & cc.Bookingtype.Weeks(1).Films(z).FilmLength & "s"
                            End If
                        Next
                        strFilms = strFilms.Substring(1) 'removes the first letter

                        .Cells(r, 1).Value = cc.Bookingtype.OrderNumber
                        .Cells(r, 2).Value = Format(Date.FromOADate(Campaign.StartDate), "Short date")
                        .Cells(r, 3).Value = Format(Date.FromOADate(Campaign.EndDate), "Short date")
                        .Cells(r, 4).Value = "'" & cc.Bookingtype.Weeks(1).Name & " - " & cc.Bookingtype.Weeks(CInt(cc.Bookingtype.Weeks.Count)).Name
                        .Cells(r, 5).Value = cc.Bookingtype.ToString
                        .Cells(r, 6).Value = cc.Bookingtype.EstimatedSpotCount
                        .Cells(r, 7).Value = strFilms
                        .Cells(r, 8).Style = "Currency"
                        .Cells(r, 8).Numberformat = "##,## $"
                        .Cells(r, 9).Style = "Currency"
                        .Cells(r, 9).Numberformat = "##,## $"
                        If cc.Bookingtype.ConfirmedNetBudget > 0 Then
                            .Cells(r, 8).Value = cc.Bookingtype.ConfirmedGrossBudget
                            .Cells(r, 9).Value = cc.Bookingtype.ConfirmedNetBudget
                        Else
                            .Cells(r, 8).Value = cc.Bookingtype.PlannedGrossBudget
                            .Cells(r, 9).Value = cc.Bookingtype.PlannedNetBudget
                        End If
                        .Cells(r, 10).Numberformat = "0.00%"
                        .Cells(r, 10).Formula = "=1-(" & .Cells(r, 9).Address & "/" & .Cells(r, 8).Address & ")"
                    Next

                    'write the total row
                    r += 1
                    .Range("A" & r & ":H" & r).Interior.ColorIndex = 15
                    .Cells(r, 1).Value = "Total:"
                    .Cells(r, 2).Value = Format(Date.FromOADate(Campaign.StartDate), "Short date")
                    .Cells(r, 3).Value = Format(Date.FromOADate(Campaign.EndDate), "Short date")
                    .Cells(r, 4).Formula = "=D" & (r - 1).ToString
                    .Cells(r, 6).Formula = "=SUM(F" & TopRow & ":F" & r - 1 & ")"
                    .Cells(r, 8).Style = "Currency"
                    .Cells(r, 8).Numberformat = "##,## $"
                    .Cells(r, 9).Style = "Currency"
                    .Cells(r, 9).Numberformat = "##,## $"
                    .Cells(r, 8).Formula = "=SUM(H" & TopRow & ":H" & r - 1 & ")"
                    .Cells(r, 9).Formula = "=SUM(I" & TopRow & ":I" & r - 1 & ")"
                    .Cells(r, 10).Formula = "=1-(" & .Cells(r, 9).Address & "/" & .Cells(r, 8).Address & ")"
                    .Cells(r, 10).Numberformat = "0.00%"
                    With .Range("A" & TopRow - 1 & ":J" & r)
                        With .Borders(7)
                            .LineStyle = 1
                            .Weight = -4138
                            .ColorIndex = -4105
                        End With
                        With .Borders(8)
                            .LineStyle = 1
                            .Weight = 2
                            .ColorIndex = -4105
                        End With
                        With .Borders(9)
                            .LineStyle = 1
                            .Weight = 2
                            .ColorIndex = -4105
                        End With
                        With .Borders(10)
                            .LineStyle = 1
                            .Weight = -4138
                            .ColorIndex = -4105
                        End With
                        With .Borders(11)
                            .LineStyle = 1
                            .Weight = -4138
                            .ColorIndex = -4105
                        End With
                        With .Borders(12)
                            .LineStyle = 1
                            .Weight = 2
                            .ColorIndex = -4105
                        End With
                        .HorizontalAlignment = -4108
                    End With

                    'prepares for the next header
                    r += 2
                End If
            Next

            'get the bundled channels if they are supposed to be printed
            'Bundled channels where only one of them is actually booked will NOT be in the list.
            'If chkBundled.Checked Then
            '    For Each TmpChan As Trinity.cChannel In Campaign.Channels
            '        For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
            '            If TmpBT.BookIt Then
            '                If Not TmpChan.ConnectedChannel = "" AndAlso Not Campaign.Channels(TmpChan.ConnectedChannel) Is Nothing Then
            '                    For Each tmpbtc As Trinity.cBookingType In Campaign.Channels(TmpChan.ConnectedChannel).BookingTypes
            '                        If tmpbtc.BookIt Then
            '                            If Not lstBundled.Contains(TmpChan.ConnectedChannel) Then
            '                                lstBundled.Add(TmpChan.ConnectedChannel)
            '                            End If
            '                        End If
            '                    Next
            '                End If
            '            End If
            '        Next
            '    Next
            'End If

            'the list lstBundled only contains channel names IF we are to print them
            While lstBundled.Count > 0
                Dim chan1 As Trinity.cChannel
                Dim chan2 As Trinity.cChannel

                chan1 = Campaign.Channels(lstBundled(0))
                chan2 = Campaign.Channels(chan1.ConnectedChannel)
                'If chan2 Is Nothing Then
                'lstBundled.Remove(chan1.ChannelName)
                'lstBundled.Remove(chan2.ChannelName)
                'Continue While
                'End If


                Dim lstBT As New List(Of String)
                For Each TmpBT As Trinity.cBookingType In chan2.BookingTypes
                    If TmpBT.BookIt Then
                        lstBT.Add(TmpBT.Name)

                        'we add them to the list if printed bookingtypes only if we are not supposed to print them as singles aswell
                        'If Not chkSingle.Checked Then
                        '    listPrinted.Add(TmpBT.ToString)
                        'End If
                    End If
                Next


                With .Range("A" & r & ":J" & r)
                    .Merge()
                    .Font.Size = 12

                    .Font.Color = RGB(255, 255, 255)
                    .Interior.Color = 0
                    .HorizontalAlignment = -4108
                    .Value = chan2.ChannelName & "/" & chan1.ChannelName
                End With
                r += 1
                .Cells(r, 1).Value = "Ordernr"
                .Cells(r, 2).Value = "Start date"
                .Cells(r, 3).Value = "End date"
                .Cells(r, 4).Value = "Weeks"
                .Cells(r, 5).Value = "Bookingtype"
                .Cells(r, 6).Value = "Spots"
                .Cells(r, 7).Value = "Spot lengh"
                .Cells(r, 8).Value = "Gross"
                .Cells(r, 9).Value = "Net"
                .Cells(r, 10).Value = "Discount"

                TopRow = r + 1
                For Each TmpBT As Trinity.cBookingType In chan1.BookingTypes
                    If TmpBT.BookIt Then

                        'we add them to the list if printed bookingtypes only if we are not supposed to print them as singles aswell
                        'If Not chkSingle.Checked Then
                        '    listPrinted.Add(TmpBT.ToString)
                        'End If

                        'if the bundled channel has the same bookingtypes we delete it from the list
                        If lstBT.Contains(TmpBT.Name) Then
                            lstBT.Remove(TmpBT.Name)
                        End If

                        r += 1
                        Dim strFilms As String = ""
                        For z As Integer = 1 To TmpBT.Weeks(1).Films.Count
                            If Not strFilms.Contains(TmpBT.Weeks(1).Films(z).FilmLength) Then
                                strFilms = strFilms & "," & TmpBT.Weeks(1).Films(z).FilmLength & "s"
                            End If
                        Next
                        strFilms = strFilms.Substring(1) 'removes the first letter

                        .Cells(r, 1).Value = TmpBT.OrderNumber
                        .Cells(r, 2).Value = Format(Date.FromOADate(Campaign.StartDate), "Short date")
                        .Cells(r, 3).Value = Format(Date.FromOADate(Campaign.EndDate), "Short date")
                        .Cells(r, 4).Value = "'" & TmpBT.Weeks(1).Name & " - " & TmpBT.Weeks(CInt(TmpBT.Weeks.Count)).Name
                        .Cells(r, 5).Value = TmpBT.Name
                        .Cells(r, 6).Value = (TmpBT.EstimatedSpotCount + chan2.BookingTypes(TmpBT.Name).EstimatedSpotCount)
                        .Cells(r, 7).Value = strFilms
                        .Cells(r, 8).Style = "Currency"
                        .Cells(r, 8).Numberformat = "##,## $"
                        .Cells(r, 9).Style = "Currency"
                        .Cells(r, 9).Numberformat = "##,## $"
                        If TmpBT.ConfirmedNetBudget > 0 Then
                            .Cells(r, 8).Value = TmpBT.ConfirmedGrossBudget + chan2.BookingTypes(TmpBT.Name).ConfirmedGrossBudget
                            .Cells(r, 9).Value = TmpBT.ConfirmedNetBudget + chan2.BookingTypes(TmpBT.Name).ConfirmedNetBudget
                        Else
                            .Cells(r, 8).Value = TmpBT.PlannedGrossBudget + chan2.BookingTypes(TmpBT.Name).PlannedGrossBudget
                            .Cells(r, 9).Value = TmpBT.PlannedNetBudget + chan2.BookingTypes(TmpBT.Name).PlannedNetBudget
                        End If
                        .Cells(r, 10).Numberformat = "0.00%"
                        .Cells(r, 10).Formula = "=1-(" & .Cells(r, 9).Address & "/" & .Cells(r, 8).Address & ")"
                    End If
                Next
                If lstBT.Count > 0 Then
                    r += 1
                    Dim TmpBT As Trinity.cBookingType = chan2.BookingTypes(lstBT(0))
                    Dim strFilms As String = ""
                    For z As Integer = 1 To TmpBT.Weeks(1).Films.Count
                        If Not strFilms.Contains(TmpBT.Weeks(1).Films(z).FilmLength) Then
                            strFilms = strFilms & "," & TmpBT.Weeks(1).Films(z).FilmLength & "s"
                        End If
                    Next
                    strFilms = strFilms.Substring(1) 'removes the first letter

                    .Cells(r, 1).Value = TmpBT.OrderNumber
                    .Cells(r, 2).Value = Format(Date.FromOADate(Campaign.StartDate), "Short date")
                    .Cells(r, 3).Value = Format(Date.FromOADate(Campaign.EndDate), "Short date")
                    .Cells(r, 4).Value = "'" & TmpBT.Weeks(1).Name & " - " & TmpBT.Weeks(CInt(TmpBT.Weeks.Count)).Name
                    .Cells(r, 5).Value = TmpBT.Name
                    .Cells(r, 6).Value = TmpBT.EstimatedSpotCount
                    .Cells(r, 7).Value = strFilms
                    .Cells(r, 8).Style = "Currency"
                    .Cells(r, 8).Numberformat = "##,## $"
                    .Cells(r, 9).Style = "Currency"
                    .Cells(r, 9).Numberformat = "##,## $"
                    If TmpBT.ConfirmedNetBudget > 0 Then
                        .Cells(r, 8).Value = TmpBT.ConfirmedGrossBudget
                        .Cells(r, 9).Value = TmpBT.ConfirmedNetBudget
                    Else
                        .Cells(r, 8).Value = TmpBT.PlannedGrossBudget
                        .Cells(r, 9).Value = TmpBT.PlannedNetBudget
                    End If
                    .Cells(r, 10).Numberformat = "0.00%"
                    .Cells(r, 10).Formula = "=1-(" & .Cells(r, 9).Address & "/" & .Cells(r, 8).Address & ")"

                    'remove the occurance from the list
                    lstBT.Remove(TmpBT.Name)
                End If

                'write the total row
                r += 1
                .Range("A" & r & ":H" & r).Value.interior.colorindex = 15
                .Cells(r, 1).Value = "Total:"
                .Cells(r, 2).Value = Format(Date.FromOADate(Campaign.StartDate), "Short date")
                .Cells(r, 3).Value = Format(Date.FromOADate(Campaign.EndDate), "Short date")
                .Cells(r, 4).Formula = "=D" & (r - 1).ToString
                .Cells(r, 6).Formula = "=SUM(F" & TopRow & ":F" & r - 1 & ")"
                .Cells(r, 8).Style = "Currency"
                .Cells(r, 8).Numberformat = "##,## $"
                .Cells(r, 9).Style = "Currency"
                .Cells(r, 9).Numberformat = "##,## $"
                .Cells(r, 8).Formula = "=SUM(H" & TopRow & ":H" & r - 1 & ")"
                .Cells(r, 9).Formula = "=SUM(I" & TopRow & ":I" & r - 1 & ")"
                .Cells(r, 10).Formula = "=1-(" & .Cells(r, 9).Address & "/" & .Cells(r, 8).Address & ")"
                .Cells(r, 10).Numberformat = "0.00%"
                With .Range("A" & TopRow - 1 & ":J" & r)
                    With .Borders(7)
                        .LineStyle = 1
                        .Weight = -4138
                        .ColorIndex = -4105
                    End With
                    With .Borders(8)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                    With .Borders(9)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                    With .Borders(10)
                        .LineStyle = 1
                        .Weight = -4138
                        .ColorIndex = -4105
                    End With
                    With .Borders(11)
                        .LineStyle = 1
                        .Weight = -4138
                        .ColorIndex = -4105
                    End With
                    With .Borders(12)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                    .HorizontalAlignment = -4108
                End With
                r += 2

                'remove the channel occurances
                lstBundled.Remove(chan1.ChannelName)
                lstBundled.Remove(chan2.ChannelName)
            End While



            'we go trough all channels
            For Each TmpChan As Trinity.cChannel In Campaign.Channels
                Dim UseIt As Boolean = False

                'if we have used the channel we have UsedIT and then proceed with the printing
                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    If TmpBT.BookIt AndAlso Not listPrinted.Contains(TmpBT.ToString) Then
                        UseIt = True
                    End If
                Next

                'we only proceed if the channel is used AND if either the user set single channels to be printed (even if bundled) OR the channel is not bundled
                If UseIt Then
                    With .Range("A" & r & ":J" & r)
                        .Merge()
                        .Font.Size = 12
                        .Font.Color = RGB(255, 255, 255)
                        .Interior.Color = 0
                        .HorizontalAlignment = -4108
                        .Value = TmpChan.ChannelName
                    End With
                    r += 1
                    .Cells(r, 1).Value = "Ordernr"
                    .Cells(r, 2).Value = "Start date"
                    .Cells(r, 3).Value = "End date"
                    .Cells(r, 4).Value = "Weeks"
                    .Cells(r, 5).Value = "Bookingtype"
                    .Cells(r, 6).Value = "Spots"
                    .Cells(r, 7).Value = "Spot lengh"
                    .Cells(r, 8).Value = "Gross"
                    .Cells(r, 9).Value = "Net"
                    .Cells(r, 10).Value = "Discount"

                    TopRow = r + 1
                    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                        If TmpBT.BookIt AndAlso Not listPrinted.Contains(TmpBT.ToString) Then
                            r += 1
                            Dim strFilms As String = ""
                            For z As Integer = 1 To TmpBT.Weeks(1).Films.Count
                                If Not strFilms.Contains(TmpBT.Weeks(1).Films(z).FilmLength) Then
                                    strFilms = strFilms & "," & TmpBT.Weeks(1).Films(z).FilmLength & "s"
                                End If
                            Next
                            strFilms = strFilms.Substring(1) 'removes the first letter

                            .Cells(r, 1).Value = TmpBT.OrderNumber
                            .Cells(r, 2).Value = Format(Date.FromOADate(Campaign.StartDate), "Short date")
                            .Cells(r, 3).Value = Format(Date.FromOADate(Campaign.EndDate), "Short date")
                            .Cells(r, 4).Value = "'" & TmpBT.Weeks(1).Name & " - " & TmpBT.Weeks(CInt(TmpBT.Weeks.Count)).Name
                            .Cells(r, 5).Value = TmpBT.Name
                            .Cells(r, 6).Value = TmpBT.EstimatedSpotCount
                            .Cells(r, 7).Value = strFilms
                            .Cells(r, 8).Style = "Currency"
                            .Cells(r, 8).Numberformat = "##,## $"
                            .Cells(r, 9).Style = "Currency"
                            .Cells(r, 9).Numberformat = "##,## $"
                            If TmpBT.ConfirmedNetBudget > 0 Then
                                .Cells(r, 8).Value = TmpBT.ConfirmedGrossBudget
                                .Cells(r, 9).Value = TmpBT.ConfirmedNetBudget
                            Else
                                .Cells(r, 8).Value = TmpBT.PlannedGrossBudget
                                .Cells(r, 9).Value = TmpBT.PlannedNetBudget
                            End If
                            .Cells(r, 10).Numberformat = "0.00%"
                            .Cells(r, 10).Formula = "=1-(" & .Cells(r, 9).Address & "/" & .Cells(r, 8).Address & ")"
                        End If
                    Next
                    r += 1
                    .Range("A" & r & ":H" & r).Interior.ColorIndex = 15
                    .Cells(r, 1).Value = "Total:"
                    .Cells(r, 2).Value = Format(Date.FromOADate(Campaign.StartDate), "Short date")
                    .Cells(r, 3).Value = Format(Date.FromOADate(Campaign.EndDate), "Short date")
                    .Cells(r, 4).Formula = "=D" & (r - 1).ToString
                    .Cells(r, 6).Formula = "=SUM(F" & TopRow & ":F" & r - 1 & ")"
                    .Cells(r, 8).Style = "Currency"
                    .Cells(r, 8).Numberformat = "##,## $"
                    .Cells(r, 9).Style = "Currency"
                    .Cells(r, 9).Numberformat = "##,## $"
                    .Cells(r, 8).Formula = "=SUM(H" & TopRow & ":H" & r - 1 & ")"
                    .Cells(r, 9).Formula = "=SUM(I" & TopRow & ":I" & r - 1 & ")"
                    .Cells(r, 10).Formula = "=1-(" & .Cells(r, 9).Address & "/" & .Cells(r, 8).Address & ")"
                    .Cells(r, 10).Numberformat = "0.00%"
                    With .Range("A" & TopRow - 1 & ":J" & r)
                        With .Borders(7)
                            .LineStyle = 1
                            .Weight = -4138
                            .ColorIndex = -4105
                        End With
                        With .Borders(8)
                            .LineStyle = 1
                            .Weight = 2
                            .ColorIndex = -4105
                        End With
                        With .Borders(9)
                            .LineStyle = 1
                            .Weight = 2
                            .ColorIndex = -4105
                        End With
                        With .Borders(10)
                            .LineStyle = 1
                            .Weight = -4138
                            .ColorIndex = -4105
                        End With
                        With .Borders(11)
                            .LineStyle = 1
                            .Weight = -4138
                            .ColorIndex = -4105
                        End With
                        With .Borders(12)
                            .LineStyle = 1
                            .Weight = 2
                            .ColorIndex = -4105
                        End With
                        .HorizontalAlignment = -4108
                    End With
                    r += 2
                End If
            Next

            'Print the EXTRA COSTS grid
            With .Range("A" & r & ":D" & r)
                .Merge()
                .Interior.Color = 0
                .Font.Color = RGB(255, 255, 255)
                .Font.Size = 12
                .Value = "Extra costs"
            End With
            r += 1
            .Cells(r, 1).Value = "Name"
            .Cells(r, 2).Value = "Amount"
            .Cells(r, 3).Value = "Type"
            .Cells(r, 4).Value = "Total"
            TopRow = r
            For Each TmpCost As Trinity.cCost In Campaign.Costs
                r += 1
                .Cells(r, 1).Value = TmpCost.CostName
                Select Case TmpCost.CostType
                    Case Trinity.cCost.CostTypeEnum.CostTypeOnDiscount

                        Dim SumUnit As Double = 0
                        If TmpCost.CountCostOn Is Nothing Then
                            .Cells(r, 3).Value = "On all channels"
                            For Each TmpChan As Trinity.cChannel In Campaign.Channels
                                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                                    For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                                        Dim Discount As Single = TmpWeek.GrossBudget - TmpWeek.NetBudget
                                        SumUnit += (Discount * TmpCost.Amount)
                                    Next
                                Next
                            Next
                            'TmpCost.Amount * (Campaign.
                        Else
                            .Cells(r, 3).Value = "On " & DirectCast(TmpCost.CountCostOn, Trinity.cChannel).ChannelName
                            For Each TmpBT As Trinity.cBookingType In DirectCast(TmpCost.CountCostOn, Trinity.cChannel).BookingTypes
                                For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                                    Dim Discount As Single = TmpWeek.GrossBudget - TmpWeek.NetBudget
                                    SumUnit += (Discount * TmpCost.Amount)
                                Next
                            Next
                        End If

                        .Cells(r, 2).Numberformat = "0.0%"
                        .Cells(r, 4).Value = Format(SumUnit, "##,##0 kr")

                    Case Trinity.cCost.CostTypeEnum.CostTypeFixed
                        .Cells(r, 3).Value = "Fixed"
                        .Cells(r, 4).Value = TmpCost.Amount
                    Case Trinity.cCost.CostTypeEnum.CostTypePercent
                        If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnMediaNet Then
                            .Cells(r, 3).Value = "On Media net"
                            .Cells(r, 4).Value = Campaign.PlannedMediaNet * TmpCost.Amount
                        ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNet Then
                            .Cells(r, 3).Value = "On Net"
                            .Cells(r, 4).Value = Campaign.PlannedNet * TmpCost.Amount
                        ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNetNet Then
                            .Cells(r, 3).Value = "On NetNet"
                            .Cells(r, 4).Value = Campaign.PlannedNetNet * TmpCost.Amount
                        End If
                        .Cells(r, 2).Numberformat = "0.0%"
                        .Cells(r, 4).Numberformat = "#,0 $"
                    Case Trinity.cCost.CostTypeEnum.CostTypePerUnit
                        If TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP Then
                            .Cells(r, 3).Value = "Per Buy TRP"

                            Dim sum As Double = 0
                            For Each c As Trinity.cChannel In Campaign.Channels
                                For Each bt As Trinity.cBookingType In c.BookingTypes
                                    If bt.BookIt Then
                                        sum += bt.TotalTRPBuyingTarget
                                    End If
                                Next
                            Next
                            .Cells(r, 4).Value = sum * TmpCost.Amount
                        ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnMainTRP Then
                            .Cells(r, 3).Value = "Per Main TRP"

                            Dim sum As Double = 0
                            For Each c As Trinity.cChannel In Campaign.Channels
                                For Each bt As Trinity.cBookingType In c.BookingTypes
                                    If bt.BookIt Then
                                        sum += bt.TotalTRP
                                    End If
                                Next
                            Next
                            .Cells(r, 4).Value = sum * TmpCost.Amount
                        ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots Then
                            .Cells(r, 3).Value = "Per spot"

                            Dim sum As Double = 0
                            For Each c As Trinity.cChannel In Campaign.Channels
                                For Each bt As Trinity.cBookingType In c.BookingTypes
                                    If bt.BookIt Then
                                        sum += bt.EstimatedSpotCount
                                    End If
                                Next
                            Next
                            .Cells(r, 4).Value = TmpCost.Amount * sum
                        End If
                        .Cells(r, 4).Numberformat = "#,0 $"
                        .Cells(r, 2).Numberformat = "#,##0 $"
                End Select
                .Cells(r, 2).Value = TmpCost.Amount
            Next
            With .Range("A" & TopRow - 1 & ":D" & r)
                With .Borders(7)
                    .LineStyle = 1
                    .Weight = -4138
                    .ColorIndex = -4105
                End With
                With .Borders(8)
                    .LineStyle = 1
                    .Weight = 2
                    .ColorIndex = -4105
                End With
                With .Borders(9)
                    .LineStyle = 1
                    .Weight = 2
                    .ColorIndex = -4105
                End With
                With .Borders(10)
                    .LineStyle = 1
                    .Weight = -4138
                    .ColorIndex = -4105
                End With
                With .Borders(11)
                    .LineStyle = 1
                    .Weight = -4138
                    .ColorIndex = -4105
                End With
                With .Borders(12)
                    .LineStyle = 1
                    .Weight = 2
                    .ColorIndex = -4105
                End With
                .HorizontalAlignment = -4108
            End With

            .Columns(1).ColumnWidth = 21.57
            .Columns(2).ColumnWidth = 21.57
            .Columns(3).ColumnWidth = 21.57
            .Columns(4).ColumnWidth = 21.57
            .Columns(5).ColumnWidth = 31.43
            .Columns(6).ColumnWidth = 16.86
            .Columns(7).ColumnWidth = 21.57
            .Columns(8).ColumnWidth = 21.57
            .Columns(9).ColumnWidth = 21.57
            .Columns(10).ColumnWidth = 16.86
            Excel.ActiveWindow.Zoom = 75
            With .PageSetup
                .PrintTitleRows = ""
                .PrintTitleColumns = ""
            End With
            .PageSetup.PrintArea = ""

            With .PageSetup
                .LeftMargin = Excel.InchesToPoints(0.787401575)
                .RightMargin = Excel.InchesToPoints(0.787401575)
                .TopMargin = Excel.InchesToPoints(0.984251969)
                .BottomMargin = Excel.InchesToPoints(0.984251969)
                .HeaderMargin = Excel.InchesToPoints(0.5)
                .FooterMargin = Excel.InchesToPoints(0.5)
                .PrintHeadings = False
                .PrintGridlines = False
                .Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape
                                '.PrintComments = -4142
                ''.PrintQuality = 600
                '.CenterHorizontally = False
                '.CenterVertically = False
                '.Orientation = 2
                '.Draft = False
                '.PaperSize = 9
                '.FirstPageNumber = -4105
                '.Order = 1
                '.BlackAndWhite = False
                '.Zoom = False
                '.FitToPagesWide = 1
                '.FitToPagesTall = 1
                '.PrintErrors = 0
            End With
            r += 2

        End With

        If Excel.SheetCount = 1 Then
            Excel.Workbooks(1).AddSheet()
            'move the sheet back
            Excel.Workbooks(1).Sheets(1).Move(After:=Excel.Sheets(2))
        End If

        With Excel.Sheets(2)
            .Name = "Week and Film"
            'print the budget per week grid
            r = 4

            .AllCells.Rows.RowHeight = 10
            .AllCells.Columns.ColumnWidth = 14

            .AllCells.Interior.Color = RGB(255, 255, 255)
            .AllCells.Font.Bold = True
            .AllCells.Rows.RowHeight = 12
            With .Range("A1:J1")
                .Merge()
                .Interior.ColorIndex = 37
                .Font.Italic = True
                .Font.Size = 10
                .HorizontalAlignment = -4108
                .Value = "Invoicing details - " & Campaign.Name
            End With

            If chkWeeklyBudget.Checked Then
                With .Range("A" & r & ":" & .Columns(CInt(Campaign.Channels(1).BookingTypes(1).Weeks.Count * 2 + 3)).Address.Substring(0, 3).Trim("$").Trim(":") & r)
                    .Merge()
                    .Interior.Color = 0
                    .Font.Color = RGB(255, 255, 255)
                    .Font.Size = 10
                    .Value = "Weekly budget (Gross/Net)"
                End With
                r += 1
                .Cells(r, 1).Value = "Channel\Week"

                Dim c As Integer = 2
                For Each week As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                    .Range(Char.ConvertFromUtf32(64 + c) & r & ":" & .Columns(c + 1).Address.Substring(0, 3).Trim("$").Trim(":") & r).Merge()
                    .Cells(r, c).Value = week.Name
                    c += 2
                Next
                .Cells(r, c).Value = "Tot Gross"  ' sum up to the left
                .Cells(r, c + 1).Value = "Tot Net"  ' sum up to the left

                TopRow = r
                r += 1

                For Each chan As Trinity.cChannel In Campaign.Channels
                    'One entry in each of these lists for each week
                    Dim listN As New List(Of Double)
                    Dim listG As New List(Of Double)
                    For Each week As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                        listN.Add(0)
                        listG.Add(0)
                    Next

                    Dim UseIt As Boolean = False

                    'if we have used the channel we have UsedIT and then proceed with the printing
                    For Each TmpBT As Trinity.cBookingType In chan.BookingTypes
                        If TmpBT.BookIt Then
                            UseIt = True
                        End If
                    Next
                    If UseIt Then
                        .Cells(r, 1).Value = chan.ChannelName

                        'get the budgets
                        For Each bt As Trinity.cBookingType In chan.BookingTypes
                            If bt.BookIt Then
                                c = 0
                                For Each week As Trinity.cWeek In bt.Weeks
                                    listN(c) = listN(c) + week.NetBudget
                                    listG(c) = listG(c) + week.GrossBudget
                                    c += 1
                                Next
                            End If
                        Next
                        c = 2
                        'print the budgets
                        Dim i As Integer = 0
                        For Each week As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                            .Cells(r, c).Numberformat = "##,## $"
                            .Cells(r, c + 1).Numberformat = "##,## $"
                            .Cells(r, c).Value = listG(i)
                            .Cells(r, c + 1).Value = listN(i)
                            c += 2
                            i += 1
                        Next
                        'print the summary to the left
                        .Cells(r, c).Numberformat = "##,## $"
                        Dim sum As Double = 0
                        For Each d As Double In listG
                            sum = sum + d
                        Next
                        .Cells(r, c).Value = sum
                        sum = 0
                        .Cells(r, c + 1).Numberformat = "##,## $"
                        For Each d As Double In listN
                            sum = sum + d
                        Next
                        .Cells(r, c + 1).Value = sum
                        r += 1
                    End If
                Next

                'print the summary at the bottom
                .Cells(r, 1).Value = "Tot"
                c = 2
                While Not .Cells(r - 1, c).Value Is Nothing
                    .Cells(r, c).Numberformat = "##,## $"
                    .Cells(r, c).Value = "=SUM(" & .Columns(c).Address.Substring(0, 3).Trim("$").Trim(":") & TopRow + 1 & ":" & .Columns(c).Address.Substring(0, 3).Trim("$").Trim(":") & r - 1 & ")"
                    c += 1
                End While

                'color the totals
                .Range("A" & r & ":" & .Columns(c - 1).Address.Substring(0, 3).Trim("$").Trim(":") & r).Interior.ColorIndex = 15
                .Range(.Columns(c + 1).Address.Substring(0, 3).Trim("$").Trim(":") & TopRow & ":" & .Columns(c - 1).Address.Substring(0, 3).Trim("$").Trim(":") & r - 1).Interior.ColorIndex = 15

                With .Range("A" & TopRow - 1 & ":" & .Columns(c - 1).Address.Substring(0, 3).Trim("$").Trim(":") & r)
                    With .Borders(7)
                        .LineStyle = 1
                        .Weight = -4138
                        .ColorIndex = -4105
                    End With
                    With .Borders(8)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                    With .Borders(9)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                    With .Borders(10)
                        .LineStyle = 1
                        .Weight = -4138
                        .ColorIndex = -4105
                    End With
                    With .Borders(11)
                        .LineStyle = 1
                        .Weight = -4138
                        .ColorIndex = -4105
                    End With
                    With .Borders(12)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                    .HorizontalAlignment = -4108
                End With

                Excel.ActiveWindow.Zoom = 75
                With .PageSetup
                    .PrintTitleRows = ""
                    .PrintTitleColumns = ""
                End With
                .PageSetup.PrintArea = ""
                With .PageSetup
                    .LeftMargin = Excel.InchesToPoints(0.787401575)
                    .RightMargin = Excel.InchesToPoints(0.787401575)
                    .TopMargin = Excel.InchesToPoints(0.984251969)
                    .BottomMargin = Excel.InchesToPoints(0.984251969)
                    .HeaderMargin = Excel.InchesToPoints(0.5)
                    .FooterMargin = Excel.InchesToPoints(0.5)
                    .PrintHeadings = False
                    .PrintGridlines = False                    
                End With
                r += 2
            End If
            If chkBudgetFilm.Checked Then
                With .Range("A" & r & ":" & Char.ConvertFromUtf32(64 + Campaign.Channels(1).BookingTypes(1).Weeks(1).Films.Count * 2 + 3) & r)
                    .Merge()
                    .Interior.Color = 0
                    .Font.Color = RGB(255, 255, 255)
                    .Font.Size = 10
                    .Value = "Budget per film (Gross/Net)"
                End With
                r += 1
                .Cells(r, 1).Value = "Channel/Film"

                Dim c As Integer = 2
                For Each TmpFilm As Trinity.cFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
                    .Range(Char.ConvertFromUtf32(64 + c) & r & ":" & Char.ConvertFromUtf32(64 + c + 1) & r).Merge()
                    .Cells(r, c).Value = TmpFilm.Name
                    c += 2
                Next
                .Cells(r, c).Value = "Tot Gross"  ' sum up to the left
                .Cells(r, c + 1).Value = "Tot Net"  ' sum up to the left

                TopRow = r
                r += 1

                For Each chan As Trinity.cChannel In Campaign.Channels
                    Dim listN As New Dictionary(Of String, Double)
                    Dim listG As New Dictionary(Of String, Double)
                    'For Each TmpWeek As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                    For Each TmpFilm As Trinity.cFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
                        listN.Add(TmpFilm.Name, 0)
                        listG.Add(TmpFilm.Name, 0)
                    Next
                    'Next
                    Dim UseIt As Boolean = False

                    'if we have used the channel we have UsedIT and then proceed with the printing
                    For Each TmpBT As Trinity.cBookingType In chan.BookingTypes
                        If TmpBT.BookIt Then
                            UseIt = True
                        End If
                    Next
                    If UseIt Then
                        .Cells(r, 1).Value = chan.ChannelName

                        'get the budgets
                        For Each bt As Trinity.cBookingType In chan.BookingTypes
                            c = 0
                            For Each week As Trinity.cWeek In bt.Weeks
                                For Each TmpFilm As Trinity.cFilm In week.Films
                                    listN(TmpFilm.Name) = listN(TmpFilm.Name) + week.NetBudget * TmpFilm.Share(Trinity.cFilm.FilmShareEnum.fseBudget) / 100
                                    listG(TmpFilm.Name) = listG(TmpFilm.Name) + week.GrossBudget * TmpFilm.Share(Trinity.cFilm.FilmShareEnum.fseBudget) / 100
                                Next
                            Next
                        Next
                        c = 2
                        'print the budgets
                        Dim i As Integer = 0
                        'For Each week As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                        For Each TmpFilm As Trinity.cFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
                            .Cells(r, c).Numberformat = "##,## $"
                            .Cells(r, c + 1).Numberformat = "##,## $"
                            .Cells(r, c).Value = listG(TmpFilm.Name)
                            .Cells(r, c + 1).Value = listN(TmpFilm.Name)
                            c += 2
                            i += 1
                        Next
                        'Next
                        'print the summary to the left
                        .Cells(r, c).Numberformat = "##,## $"
                        Dim sum As Double = 0
                        For Each d As Double In listG.Values
                            sum = sum + d
                        Next
                        .Cells(r, c).Value = sum
                        sum = 0
                        .Cells(r, c + 1).Numberformat = "##,## $"
                        For Each d As Double In listN.Values
                            sum = sum + d
                        Next
                        .Cells(r, c + 1).Value = sum
                        r += 1
                    End If
                Next
                'print the summary at the bottom
                .Cells(r, 1).Value = "Tot"
                c = 2
                While Not .Cells(r - 1, c).Value Is Nothing
                    .Cells(r, c).Numberformat = "##,## $"
                    .Cells(r, c).Formula = "=SUM(" & Char.ConvertFromUtf32(64 + c) & TopRow + 1 & ":" & Char.ConvertFromUtf32(64 + c) & r - 1 & ")"
                    c += 1
                End While

                'color the totals
                .Range("A" & r & ":" & Char.ConvertFromUtf32(64 + c - 1) & r).Interior.ColorIndex = 15
                .Range(Char.ConvertFromUtf32(64 + c - 1) & TopRow & ":" & Char.ConvertFromUtf32(64 + c - 1) & r - 1).Interior.ColorIndex = 15

                With .Range("A" & TopRow - 1 & ":" & Char.ConvertFromUtf32(64 + c - 1) & r)
                    With .Borders(7)
                        .LineStyle = 1
                        .Weight = -4138
                        .ColorIndex = -4105
                    End With
                    With .Borders(8)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                    With .Borders(9)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                    With .Borders(10)
                        .LineStyle = 1
                        .Weight = -4138
                        .ColorIndex = -4105
                    End With
                    With .Borders(11)
                        .LineStyle = 1
                        .Weight = -4138
                        .ColorIndex = -4105
                    End With
                    With .Borders(12)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                    .HorizontalAlignment = -4108
                End With

                Excel.ActiveWindow.Zoom = 75
                With .PageSetup
                    .PrintTitleRows = ""
                    .PrintTitleColumns = ""
                End With
                .PageSetup.PrintArea = ""
                With .PageSetup
                    .LeftMargin = Excel.InchesToPoints(0.787401575)
                    .RightMargin = Excel.InchesToPoints(0.787401575)
                    .TopMargin = Excel.InchesToPoints(0.984251969)
                    .BottomMargin = Excel.InchesToPoints(0.984251969)
                    .HeaderMargin = Excel.InchesToPoints(0.5)
                    .FooterMargin = Excel.InchesToPoints(0.5)
                    .PrintHeadings = False
                    .PrintGridlines = False
                    .Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape
                    '.PrintComments = -4142
                    '.CenterHorizontally = False
                    '.CenterVertically = False
                    '.Orientation = 2
                    '.Draft = False
                    '.PaperSize = 9
                    '.FirstPageNumber = -4105
                    '.Order = 1
                    '.BlackAndWhite = False
                    '.Zoom = False
                    '.FitToPagesWide = 1
                    '.FitToPagesTall = 1
                    '.PrintErrors = 0
                End With
                r += 2

            End If

            With .PageSetup
                .PrintTitleRows = ""
                .PrintTitleColumns = ""
            End With
            .PageSetup.PrintArea = ""
            With .PageSetup
                .LeftMargin = Excel.InchesToPoints(0.787401575)
                .RightMargin = Excel.InchesToPoints(0.787401575)
                .TopMargin = Excel.InchesToPoints(0.984251969)
                .BottomMargin = Excel.InchesToPoints(0.984251969)
                .HeaderMargin = Excel.InchesToPoints(0.5)
                .FooterMargin = Excel.InchesToPoints(0.5)
                .PrintHeadings = False
                .PrintGridlines = False
                .Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape
                '.PrintComments = -4142
                ''.PrintQuality = 600
                '.CenterHorizontally = False
                '.CenterVertically = False
                '.Orientation = 2
                '.Draft = False
                '.PaperSize = 9
                '.FirstPageNumber = -4105
                '.Order = 1
                '.BlackAndWhite = False
                '.Zoom = False
                '.FitToPagesWide = 1
                '.FitToPagesTall = 1
                '.PrintErrors = 0
            End With

        End With

        Me.Cursor = Windows.Forms.Cursors.Default


        Excel.ScreenUpdating = True
        Excel.Visible = True
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Exit Sub

ErrHandle:
        Me.Cursor = Windows.Forms.Cursors.Default
        Windows.Forms.MessageBox.Show("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & " in PrintInvoiceDetails.", "Error", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        Excel.Quit()
        Exit Sub
        'Resume

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub


    '    Sub PrintInvoiceDetails(ByVal sender As Object, ByVal e As EventArgs)
    '        Dim Excel As Object
    '        Dim r As Integer
    '        Dim TopRow As Integer

    '        Dim lstBundled As New List(Of String)

    '        Me.Cursor = Windows.Forms.Cursors.WaitCursor
    '        On Error GoTo ErrHandle

    '        System.Threading.Thread.CurrentThread.CurrentCulture = _
    '            New System.Globalization.CultureInfo("en-US")

    '        Excel = CreateObject("CultureSafeExcel")

    '        With Excel.Workbooks.Add.Sheets(1)
    '            .AllCells.interior.color = RGB(255, 255, 255)
    '            .AllCells.font.bold = True
    '            .AllCells.rows.rowheight = 20.25
    '            With .range("A1:J1")
    '                .merge()
    '                .interior.colorindex = 37
    '                .font.italic = True
    '                .font.size = 14
    '                .value = "Invoicing details - " & Campaign.Name
    '                .horizontalalignment = -4108
    '            End With
    '            .cells(3, 1) = "CLIENT:"
    '            .cells(4, 1) = "PRODUCT:"
    '            .cells(5, 1) = "PLANNER: "
    '            .cells(3, 2) = Campaign.Client
    '            .cells(4, 2) = Campaign.Product
    '            .cells(5, 2) = Campaign.Buyer

    '            r = 7
    '            For Each TmpChan As Trinity.cChannel In Campaign.Channels
    '                Dim UseIt As Boolean = False
    '                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
    '                    If TmpBT.BookIt Then
    '                        UseIt = True
    '                    End If
    '                Next
    '                If UseIt Then
    '                    If Not TmpChan.ConnectedChannel = "" Then
    '                        lstBundled.Add(TmpChan.ConnectedChannel)
    '                    End If
    '                    With .range("A" & r & ":J" & r)
    '                        .merge()
    '                        .font.size = 12
    '                        .value = TmpChan.ChannelName
    '                        .font.color = RGB(255, 255, 255)
    '                        .interior.color = 0
    '                        .horizontalalignment = -4108
    '                    End With
    '                    r += 1
    '                    .cells(r, 1) = "Ordernr"
    '                    .cells(r, 2) = "Start date"
    '                    .cells(r, 3) = "End date"
    '                    .cells(r, 4) = "Weeks"
    '                    .cells(r, 5) = "Bookingtype"
    '                    .cells(r, 6) = "Spots"
    '                    .cells(r, 7) = "Spot lengh"
    '                    .cells(r, 8) = "Gross"
    '                    .cells(r, 9) = "Net"
    '                    .cells(r, 10) = "Discount"

    '                    TopRow = r + 1
    '                    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
    '                        If TmpBT.BookIt Then
    '                            r += 1
    '                            Dim strFilms As String = ""
    '                            For z As Integer = 1 To TmpBT.Weeks(1).Films.Count
    '                                If Not strFilms.Contains(TmpBT.Weeks(1).Films(z).FilmLength) Then
    '                                    strFilms = strFilms & "," & TmpBT.Weeks(1).Films(z).FilmLength & "s"
    '                                End If
    '                            Next
    '                            strFilms = strFilms.Substring(1) 'removes the first letter

    '                            .cells(r, 1) = TmpBT.OrderNumber
    '                            .cells(r, 2) = Format(Date.FromOADate(Campaign.StartDate), "Short date")
    '                            .cells(r, 3) = Format(Date.FromOADate(Campaign.EndDate), "Short date")
    '                            .cells(r, 4) = "'" & TmpBT.Weeks(1).Name & " - " & TmpBT.Weeks(TmpBT.Weeks.Count).Name
    '                            .cells(r, 5) = TmpBT.Name
    '                            .cells(r, 6) = TmpBT.EstimatedSpotCount
    '                            .cells(r, 7) = strFilms
    '                            .cells(r, 8).Style = "Currency"
    '                            .cells(r, 8).NumberFormat = "##,## $"
    '                            .cells(r, 9).Style = "Currency"
    '                            .cells(r, 9).NumberFormat = "##,## $"
    '                            If TmpBT.PlannedNetBudget > 0 Then
    '                                .cells(r, 8) = TmpBT.PlannedGrossBudget
    '                                .cells(r, 9) = TmpBT.PlannedNetBudget
    '                            Else
    '                                .cells(r, 8) = TmpBT.ConfirmedGrossBudget
    '                                .cells(r, 9) = TmpBT.ConfirmedNetBudget
    '                            End If
    '                            .cells(r, 10).NumberFormat = "0.00%"
    '                            .cells(r, 10).formula = "=1-(" & .cells(r, 9).address & "/" & .cells(r, 8).address & ")"
    '                        End If
    '                    Next
    '                    r += 1
    '                    .range("A" & r & ":H" & r).interior.colorindex = 15
    '                    .cells(r, 1) = "Total:"
    '                    .cells(r, 2) = Format(Date.FromOADate(Campaign.StartDate), "Short date")
    '                    .cells(r, 3) = Format(Date.FromOADate(Campaign.EndDate), "Short date")
    '                    .cells(r, 4).formula = "=D" & (r - 1).ToString
    '                    .cells(r, 6).formula = "=SUM(F" & TopRow & ":F" & r - 1 & ")"
    '                    .cells(r, 8).Style = "Currency"
    '                    .cells(r, 8).NumberFormat = "##,## $"
    '                    .cells(r, 9).Style = "Currency"
    '                    .cells(r, 9).NumberFormat = "##,## $"
    '                    .cells(r, 8).formula = "=SUM(H" & TopRow & ":H" & r - 1 & ")"
    '                    .cells(r, 9).formula = "=SUM(I" & TopRow & ":I" & r - 1 & ")"
    '                    .cells(r, 10).formula = "=1-(" & .cells(r, 9).address & "/" & .cells(r, 8).address & ")"
    '                    .cells(r, 10).NumberFormat = "0.00%"
    '                    With .range("A" & TopRow - 1 & ":J" & r)
    '                        With .Borders(7)
    '                            .LineStyle = 1
    '                            .Weight = -4138
    '                            .ColorIndex = -4105
    '                        End With
    '                        With .Borders(8)
    '                            .LineStyle = 1
    '                            .Weight = 2
    '                            .ColorIndex = -4105
    '                        End With
    '                        With .Borders(9)
    '                            .LineStyle = 1
    '                            .Weight = 2
    '                            .ColorIndex = -4105
    '                        End With
    '                        With .Borders(10)
    '                            .LineStyle = 1
    '                            .Weight = -4138
    '                            .ColorIndex = -4105
    '                        End With
    '                        With .Borders(11)
    '                            .LineStyle = 1
    '                            .Weight = -4138
    '                            .ColorIndex = -4105
    '                        End With
    '                        With .Borders(12)
    '                            .LineStyle = 1
    '                            .Weight = 2
    '                            .ColorIndex = -4105
    '                        End With
    '                        .horizontalalignment = -4108
    '                    End With
    '                    r += 2
    '                End If
    '            Next

    '            'print bundled channels
    '            While lstBundled.Count > 0
    '                Dim chan1 As Trinity.cChannel
    '                Dim chan2 As Trinity.cChannel

    '                chan1 = Campaign.Channels(lstBundled(0))
    '                chan2 = Campaign.Channels(chan1.ConnectedChannel)

    '                Dim lstBT As New List(Of String)
    '                For Each TmpBT As Trinity.cBookingType In chan2.BookingTypes
    '                    If TmpBT.BookIt Then
    '                        lstBT.Add(TmpBT.Name)
    '                    End If
    '                Next


    '                With .range("A" & r & ":J" & r)
    '                    .merge()
    '                    .font.size = 12
    '                    .value = chan2.ChannelName & "/" & chan1.ChannelName
    '                    .font.color = RGB(255, 255, 255)
    '                    .interior.color = 0
    '                    .horizontalalignment = -4108
    '                End With
    '                r += 1
    '                .cells(r, 1) = "Ordernr"
    '                .cells(r, 2) = "Start date"
    '                .cells(r, 3) = "End date"
    '                .cells(r, 4) = "Weeks"
    '                .cells(r, 5) = "Bookingtype"
    '                .cells(r, 6) = "Spots"
    '                .cells(r, 7) = "Spot lengh"
    '                .cells(r, 8) = "Gross"
    '                .cells(r, 9) = "Net"
    '                .cells(r, 10) = "Discount"

    '                TopRow = r + 1
    '                For Each TmpBT As Trinity.cBookingType In chan1.BookingTypes
    '                    If TmpBT.BookIt Then

    '                        'if the bundled channel has the same bookingtypes we delete it from the list
    '                        If lstBT.Contains(TmpBT.Name) Then
    '                            lstBT.Remove(TmpBT.Name)
    '                        End If

    '                        r += 1
    '                        Dim strFilms As String = ""
    '                        For z As Integer = 1 To TmpBT.Weeks(1).Films.Count
    '                            If Not strFilms.Contains(TmpBT.Weeks(1).Films(z).FilmLength) Then
    '                                strFilms = strFilms & "," & TmpBT.Weeks(1).Films(z).FilmLength & "s"
    '                            End If
    '                        Next
    '                        strFilms = strFilms.Substring(1) 'removes the first letter

    '                        .cells(r, 1) = TmpBT.OrderNumber
    '                        .cells(r, 2) = Format(Date.FromOADate(Campaign.StartDate), "Short date")
    '                        .cells(r, 3) = Format(Date.FromOADate(Campaign.EndDate), "Short date")
    '                        .cells(r, 4) = "'" & TmpBT.Weeks(1).Name & " - " & TmpBT.Weeks(TmpBT.Weeks.Count).Name
    '                        .cells(r, 5) = TmpBT.Name
    '                        .cells(r, 6) = (TmpBT.EstimatedSpotCount + chan2.BookingTypes(TmpBT.Name).EstimatedSpotCount)
    '                        .cells(r, 7) = strFilms
    '                        .cells(r, 8).Style = "Currency"
    '                        .cells(r, 8).NumberFormat = "##,## $"
    '                        .cells(r, 9).Style = "Currency"
    '                        .cells(r, 9).NumberFormat = "##,## $"
    '                        If TmpBT.PlannedNetBudget > 0 Then
    '                            .cells(r, 8) = TmpBT.PlannedGrossBudget + chan2.BookingTypes(TmpBT.Name).PlannedGrossBudget
    '                            .cells(r, 9) = TmpBT.PlannedNetBudget + chan2.BookingTypes(TmpBT.Name).PlannedNetBudget
    '                        Else
    '                            .cells(r, 8) = TmpBT.ConfirmedGrossBudget + chan2.BookingTypes(TmpBT.Name).ConfirmedGrossBudget
    '                            .cells(r, 9) = TmpBT.ConfirmedNetBudget + chan2.BookingTypes(TmpBT.Name).ConfirmedNetBudget
    '                        End If
    '                        .cells(r, 10).NumberFormat = "0.00%"
    '                        .cells(r, 10).formula = "=1-(" & .cells(r, 9).address & "/" & .cells(r, 8).address & ")"
    '                    End If
    '                Next
    '                If lstBT.Count > 0 Then
    '                    r += 1
    '                    Dim TmpBT As Trinity.cBookingType = chan2.BookingTypes(lstBT(0))
    '                    Dim strFilms As String = ""
    '                    For z As Integer = 1 To TmpBT.Weeks(1).Films.Count
    '                        If Not strFilms.Contains(TmpBT.Weeks(1).Films(z).FilmLength) Then
    '                            strFilms = strFilms & "," & TmpBT.Weeks(1).Films(z).FilmLength & "s"
    '                        End If
    '                    Next
    '                    strFilms = strFilms.Substring(1) 'removes the first letter

    '                    .cells(r, 1) = TmpBT.OrderNumber
    '                    .cells(r, 2) = Format(Date.FromOADate(Campaign.StartDate), "Short date")
    '                    .cells(r, 3) = Format(Date.FromOADate(Campaign.EndDate), "Short date")
    '                    .cells(r, 4) = "'" & TmpBT.Weeks(1).Name & " - " & TmpBT.Weeks(TmpBT.Weeks.Count).Name
    '                    .cells(r, 5) = TmpBT.Name
    '                    .cells(r, 6) = TmpBT.EstimatedSpotCount
    '                    .cells(r, 7) = strFilms
    '                    .cells(r, 8).Style = "Currency"
    '                    .cells(r, 8).NumberFormat = "##,## $"
    '                    .cells(r, 9).Style = "Currency"
    '                    .cells(r, 9).NumberFormat = "##,## $"
    '                    If TmpBT.PlannedNetBudget > 0 Then
    '                        .cells(r, 8) = tmpbt.PlannedGrossBudget
    '                        .cells(r, 9) = tmpbt.PlannedNetBudget
    '                    Else
    '                        .cells(r, 8) = tmpbt.ConfirmedGrossBudget
    '                        .cells(r, 9) = tmpbt.ConfirmedNetBudget
    '                    End If
    '                    .cells(r, 10).NumberFormat = "0.00%"
    '                    .cells(r, 10).formula = "=1-(" & .cells(r, 9).address & "/" & .cells(r, 8).address & ")"

    '                    'remove the occurance from the list
    '                    lstBT.Remove(tmpbt.Name)
    '                End If

    '                'write the total row
    '                r += 1
    '                .range("A" & r & ":H" & r).interior.colorindex = 15
    '                .cells(r, 1) = "Total:"
    '                .cells(r, 2) = Format(Date.FromOADate(Campaign.StartDate), "Short date")
    '                .cells(r, 3) = Format(Date.FromOADate(Campaign.EndDate), "Short date")
    '                .cells(r, 4).formula = "=D" & (r - 1).ToString
    '                .cells(r, 6).formula = "=SUM(F" & TopRow & ":F" & r - 1 & ")"
    '                .cells(r, 8).Style = "Currency"
    '                .cells(r, 8).NumberFormat = "##,## $"
    '                .cells(r, 9).Style = "Currency"
    '                .cells(r, 9).NumberFormat = "##,## $"
    '                .cells(r, 8).formula = "=SUM(H" & TopRow & ":H" & r - 1 & ")"
    '                .cells(r, 9).formula = "=SUM(I" & TopRow & ":I" & r - 1 & ")"
    '                .cells(r, 10).formula = "=1-(" & .cells(r, 9).address & "/" & .cells(r, 8).address & ")"
    '                .cells(r, 10).NumberFormat = "0.00%"
    '                With .range("A" & TopRow - 1 & ":J" & r)
    '                    With .Borders(7)
    '                        .LineStyle = 1
    '                        .Weight = -4138
    '                        .ColorIndex = -4105
    '                    End With
    '                    With .Borders(8)
    '                        .LineStyle = 1
    '                        .Weight = 2
    '                        .ColorIndex = -4105
    '                    End With
    '                    With .Borders(9)
    '                        .LineStyle = 1
    '                        .Weight = 2
    '                        .ColorIndex = -4105
    '                    End With
    '                    With .Borders(10)
    '                        .LineStyle = 1
    '                        .Weight = -4138
    '                        .ColorIndex = -4105
    '                    End With
    '                    With .Borders(11)
    '                        .LineStyle = 1
    '                        .Weight = -4138
    '                        .ColorIndex = -4105
    '                    End With
    '                    With .Borders(12)
    '                        .LineStyle = 1
    '                        .Weight = 2
    '                        .ColorIndex = -4105
    '                    End With
    '                    .horizontalalignment = -4108
    '                End With
    '                r += 2

    '                'remove the channel occurances
    '                lstBundled.Remove(chan1.ChannelName)
    '                lstBundled.Remove(chan2.ChannelName)
    '            End While


    '            With .range("A" & r & ":C" & r)
    '                .merge()
    '                .interior.color = 0
    '                .font.color = RGB(255, 255, 255)
    '                .font.size = 12
    '                .value = "Extra costs"
    '            End With
    '            r += 1
    '            .cells(r, 1) = "Name"
    '            .cells(r, 2) = "Amount"
    '            .cells(r, 3) = "Type"
    '            TopRow = r
    '            For Each TmpCost As Trinity.cCost In Campaign.Costs
    '                r += 1
    '                .cells(r, 1) = TmpCost.CostName
    '                Select Case TmpCost.CostType
    '                    Case Trinity.cCost.CostTypeEnum.CostTypeFixed
    '                        .cells(r, 3) = "Fixed"
    '                    Case Trinity.cCost.CostTypeEnum.CostTypePercent
    '                        If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnMediaNet Then
    '                            .cells(r, 3) = "On Media net"
    '                        ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNet Then
    '                            .cells(r, 3) = "On Net"
    '                        ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNetNet Then
    '                            .cells(r, 3) = "On NetNet"
    '                        End If
    '                        .cells(r, 2).numberformat = "0.0%"
    '                    Case Trinity.cCost.CostTypeEnum.CostTypePerUnit
    '                        If TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP Then
    '                            .cells(r, 3) = "Per Buy TRP"
    '                        ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnMainTRP Then
    '                            .cells(r, 3) = "Per Main TRP"
    '                        ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots Then
    '                            .cells(r, 3) = "Per spot"
    '                        End If
    '                        .cells(r, 2).numberformat = "##,##0 $"
    '                End Select
    '                .cells(r, 2) = TmpCost.Amount
    '            Next
    '            With .range("A" & TopRow - 1 & ":C" & r)
    '                With .Borders(7)
    '                    .LineStyle = 1
    '                    .Weight = -4138
    '                    .ColorIndex = -4105
    '                End With
    '                With .Borders(8)
    '                    .LineStyle = 1
    '                    .Weight = 2
    '                    .ColorIndex = -4105
    '                End With
    '                With .Borders(9)
    '                    .LineStyle = 1
    '                    .Weight = 2
    '                    .ColorIndex = -4105
    '                End With
    '                With .Borders(10)
    '                    .LineStyle = 1
    '                    .Weight = -4138
    '                    .ColorIndex = -4105
    '                End With
    '                With .Borders(11)
    '                    .LineStyle = 1
    '                    .Weight = -4138
    '                    .ColorIndex = -4105
    '                End With
    '                With .Borders(12)
    '                    .LineStyle = 1
    '                    .Weight = 2
    '                    .ColorIndex = -4105
    '                End With
    '                .horizontalalignment = -4108
    '            End With

    '            .columns(1).columnwidth = 21.57
    '            .columns(2).columnwidth = 21.57
    '            .columns(3).columnwidth = 21.57
    '            .columns(4).columnwidth = 21.57
    '            .columns(5).columnwidth = 31.43
    '            .columns(6).columnwidth = 16.86
    '            .columns(7).columnwidth = 21.57
    '            .columns(8).columnwidth = 21.57
    '            .columns(9).columnwidth = 21.57
    '            .columns(10).columnwidth = 16.86
    '            Excel.ActiveWindow.Zoom = 75
    '            With .PageSetup
    '                .PrintTitleRows = ""
    '                .PrintTitleColumns = ""
    '            End With
    '            .PageSetup.PrintArea = ""
    '            With .PageSetup
    '                .LeftHeader = ""
    '                .CenterHeader = ""
    '                .RightHeader = ""
    '                .LeftFooter = ""
    '                .CenterFooter = ""
    '                .RightFooter = ""
    '                .LeftMargin = Excel.InchesToPoints(0.787401575)
    '                .RightMargin = Excel.InchesToPoints(0.787401575)
    '                .TopMargin = Excel.InchesToPoints(0.984251969)
    '                .BottomMargin = Excel.InchesToPoints(0.984251969)
    '                .HeaderMargin = Excel.InchesToPoints(0.5)
    '                .FooterMargin = Excel.InchesToPoints(0.5)
    '                .PrintHeadings = False
    '                .PrintGridlines = False
    '                .PrintComments = -4142
    '                '.PrintQuality = 600
    '                .CenterHorizontally = False
    '                .CenterVertically = False
    '                .Orientation = 2
    '                .Draft = False
    '                .PaperSize = 9
    '                .FirstPageNumber = -4105
    '                .Order = 1
    '                .BlackAndWhite = False
    '                .Zoom = False
    '                .FitToPagesWide = 1
    '                .FitToPagesTall = 1
    '                .PrintErrors = 0
    '            End With
    '        End With

    '        Me.Cursor = Windows.Forms.Cursors.Default

    '        System.Threading.Thread.CurrentThread.CurrentCulture = _
    '            New System.Globalization.CultureInfo("sv-SE")
    '        Excel.visible = True

    '        Exit Sub

    'ErrHandle:
    '        Me.Cursor = Windows.Forms.Cursors.Default
    '        Windows.Forms.MessageBox.Show("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & " in PrintInvoiceDetails.", "Error", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
    '        System.Threading.Thread.CurrentThread.CurrentCulture = _
    '            New System.Globalization.CultureInfo("sv-SE")
    '        Excel.quit()

    '    End Sub

    Private Sub frmPrintInvoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'get the default options
        'chkBundled.Checked = TrinitySettings.DefaultPrintBundled
        'chkSingle.Checked = TrinitySettings.DefaultPrintBundledSingle
        chkWeeklyBudget.Checked = TrinitySettings.DefaultPrintWeekBudget
        chkBudgetFilm.Checked = TrinitySettings.DefaultPrintFilmBudget

        rdbNot.Checked = (TrinitySettings.DefaultPrintCombinations = 0)
        rdbOne.Checked = (TrinitySettings.DefaultPrintCombinations = 1)
        rdbAll.Checked = (TrinitySettings.DefaultPrintCombinations = 2)
        chkCombinedSingle.Checked = TrinitySettings.DefaultPrintCombinedSingle

      

    End Sub
End Class