

Public Class frmMarathon
    'Marathon is the economy program used. All costs are billed the customer though Marathon.
    Private Structure CostTag
        Public BookingType As Trinity.cBookingType
        Public Cost As Trinity.cCost
    End Structure

    Private Sub frmMarathon_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        grdSummary.AutoResizeRowHeadersWidth(Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders)
        grdSummary.Columns(0).Width = grdSummary.Width - grdSummary.RowHeadersWidth - 2
    End Sub

    Private Sub frmMarathon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        grdMarathon.Rows.Clear()
        'set the size big so all rows can fit (it will be resized later)
        grpMarathon.Height = 10000
        grdMarathon.Height = 10000

        'for each channel and booking type we list the costs
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If TmpBT.BookIt And TmpBT.Combination Is Nothing Then
                    Dim TmpCT As CostTag
                    TmpCT.BookingType = TmpBT
                    TmpCT.Cost = Nothing
                    grdMarathon.Rows.Add()
                    grdMarathon.Rows(grdMarathon.Rows.Count - 1).Tag = TmpCT
                    Dim TmpLabel As New Windows.Forms.Label
                    TmpLabel.Parent = grpMarathon
                    TmpLabel.Top = grdMarathon.GetRowDisplayRectangle(grdMarathon.Rows.Count - 1, False).Top + grdMarathon.Top - 1
                    TmpLabel.Left = 6
                    TmpLabel.Width = grdMarathon.Left - 6
                    TmpLabel.TextAlign = Drawing.ContentAlignment.MiddleLeft
                    TmpLabel.Text = TmpBT.ToString
                    TmpLabel.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
                    'Edited by Joakim Koch 2016-05-13
                    'The bureaus probably dont use this cost any more since they want the Spotcheck
                    'For Each TmpCost As Trinity.cCost In Campaign.Costs
                    'If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePerUnit Then
                    '    TmpCT = New CostTag
                    '    TmpCT.BookingType = TmpBT
                    '    TmpCT.Cost = TmpCost
                    '    grdMarathon.Rows.Add()
                    '    grdMarathon.Rows(grdMarathon.Rows.Count - 1).Tag = TmpCT
                    '    grdMarathon.Rows(grdMarathon.Rows.Count - 1).Cells(3).Tag = (TmpCT.BookingType.EstimatedSpotCount * TmpCT.Cost.Amount)
                    '    'grdMarathon.Rows(grdMarathon.Rows.Count - 1).Cells(3).Tag = (TmpCT.BookingType.ConfirmedSpotCount * TmpCT.Cost.Amount)
                    'End If
                    'Next
                    TmpLabel.Height = grdMarathon.Top + grdMarathon.GetRowDisplayRectangle(grdMarathon.Rows.Count - 1, False).Bottom - TmpLabel.Top - 1
                End If
            Next
        Next
        'iterates through each combination channel
        For Each tmpC As Trinity.cCombination In Campaign.Combinations
            Dim TmpCT As CostTag = Nothing
            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                If tmpCC.Bookingtype IsNot Nothing Then
                    TmpCT.BookingType = tmpCC.Bookingtype
                    TmpCT.Cost = Nothing

                End If
            Next

            grdMarathon.Rows.Add()
            grdMarathon.Rows(grdMarathon.Rows.Count - 1).Tag = TmpCT
            Dim TmpLabel As New Windows.Forms.Label
            TmpLabel.Parent = grpMarathon
            TmpLabel.Top = grdMarathon.GetRowDisplayRectangle(grdMarathon.Rows.Count - 1, False).Top + grdMarathon.Top - 1
            TmpLabel.Left = 6
            TmpLabel.Width = grdMarathon.Left - 6
            TmpLabel.TextAlign = Drawing.ContentAlignment.MiddleLeft
            TmpLabel.Text = tmpC.Name
            TmpLabel.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
            TmpLabel.Height = grdMarathon.Top + grdMarathon.GetRowDisplayRectangle(grdMarathon.Rows.Count - 1, False).Bottom - TmpLabel.Top - 1
        Next


        grdMarathon.Height = grdMarathon.GetRowDisplayRectangle(grdMarathon.Rows.Count - 1, False).Bottom + 1
        grpMarathon.Height = grdMarathon.Bottom + 6
        grpSummary.Top = grpMarathon.Bottom + 6
        grpSummary.Height = 10000
        grdSummary.Height = 10000
        Me.Height = 10000
        grdSummary.Rows.Clear()
        grdSummary.Rows.Add()
        grdSummary.Rows(0).HeaderCell.Value = "Media cost"
        grdSummary.Rows(0).Tag = Nothing
        'summarize the costs in the summary grid        
        For Each TmpCost As Trinity.cCost In Campaign.Costs
            If TmpCost.MarathonID <> "" Then
                If TmpCost.MarathonID = "A341"
                    Dim totalCost As Double = 0
                    For each ch as Trinity.cChannel in Campaign.Channels
                        For each bt As Trinity.cBookingType In ch.BookingTypes
                            totalCost += (bt.EstimatedSpotCount * TmpCost.Amount)
                        Next
                    Next
                End If
                'If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePerUnit                                        
                '    grdSummary.Rows.Add()
                '    grdSummary.Rows(grdSummary.Rows.Count - 1).HeaderCell.Value = TmpCost.CostName
                '    Dim totalCost As Double = 0
                '    For each c As Trinity.cChannel In Campaign.Channels
                '        For each bt As Trinity.cBookingType In c.BookingTypes
                '            totalCost += bt.EstimatedSpotCount * TmpCost.Amount
                '        Next
                '    Next
                '    grdSummary.Rows.Add()
                '    grdSummary.Rows(grdSummary.Rows.Count - 1).HeaderCell.Value = TmpCost.CostName
                '    grdSummary.Rows(grdSummary.Rows.Count - 1).Tag = totalCost
                'Else
                '    grdSummary.Rows.Add()
                '    grdSummary.Rows(grdSummary.Rows.Count - 1).HeaderCell.Value = TmpCost.CostName
                '    grdSummary.Rows(grdSummary.Rows.Count - 1).Tag = TmpCost

                'End If
                grdSummary.Rows.Add()
                grdSummary.Rows(grdSummary.Rows.Count - 1).HeaderCell.Value = TmpCost.CostName
                grdSummary.Rows(grdSummary.Rows.Count - 1).Tag = TmpCost
            End If
        Next
        grdSummary.Height = grdSummary.GetRowDisplayRectangle(grdSummary.Rows.Count - 1, False).Bottom + 1
        grpSummary.Height = grdSummary.Bottom + 6
        pnlSignature.Top = grpSummary.Bottom + 6
        Me.Height = pnlSignature.Bottom + 36

    End Sub

    Private Sub grdMarathon_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdMarathon.CellFormatting
        'Formats the 3rd column
        If e.ColumnIndex = 3 Then
            e.Value = Format(e.Value, "N0")
            e.FormattingApplied = True
        End If
    End Sub

    Private Sub grdMarathon_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdMarathon.CellValueNeeded
        'updates the grid automatically on a number of events (mouse over, activation etc)
        Dim TmpCT As CostTag = grdMarathon.Rows(e.RowIndex).Tag
        If e.ColumnIndex = 0 Then
            If TmpCT.Cost Is Nothing Then
                e.Value = "Media cost"
            Else
                e.Value = TmpCT.Cost.CostName
            End If
        ElseIf e.ColumnIndex = 1 Then
            If TmpCT.Cost Is Nothing Then
                If TmpCT.BookingType.Combination Is Nothing Then
                    e.Value = Format(TmpCT.BookingType.PlannedNetBudget, "N0")
                Else
                    Dim plannedNetBudget As Integer = 0
                    For Each TmpCC As Trinity.cCombinationChannel In TmpCT.BookingType.Combination.Relations
                        plannedNetBudget += TmpCC.Bookingtype.PlannedNetBudget
                    Next
                    e.Value = Format(plannedNetBudget, "N0")
                End If
            ElseIf TmpCT.Cost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots Then
                e.Value = Format(TmpCT.BookingType.EstimatedSpotCount * TmpCT.Cost.Amount, "N0")
            End If
        ElseIf e.ColumnIndex = 2 Then
            If TmpCT.Cost Is Nothing Then
                e.Value = Format(TmpCT.BookingType.ConfirmedNetBudget, "N0")
            ElseIf TmpCT.Cost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots Then
                e.Value = Format(TmpCT.BookingType.ConfirmedSpotCount * TmpCT.Cost.Amount, "N0")
            End If
        ElseIf e.ColumnIndex = 3 Then
            If TmpCT.Cost Is Nothing Then
                If TmpCT.BookingType.Combination Is Nothing Then
                    If TmpCT.BookingType.MarathonNetBudget > 0 Then
                        e.Value = TmpCT.BookingType.MarathonNetBudget
                    ElseIf TmpCT.BookingType.ConfirmedNetBudget > 0 Then
                        e.Value = TmpCT.BookingType.ConfirmedNetBudget
                    Else
                        e.Value = TmpCT.BookingType.PlannedNetBudget
                    End If
                Else
                    Dim plannedNetBudget As Integer = 0
                    For Each TmpCC As Trinity.cCombinationChannel In TmpCT.BookingType.Combination.Relations
                        plannedNetBudget += TmpCC.Bookingtype.PlannedNetBudget
                    Next
                    e.Value = plannedNetBudget
                End If
            ElseIf TmpCT.Cost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots Then
                e.Value = Val(grdMarathon.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag)
            End If
        End If
    End Sub

    Private Sub grdMarathon_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdMarathon.CellValuePushed
        'runs when a cell value is altered by the user
        If e.ColumnIndex < 3 Then Exit Sub
        Dim TmpCT As CostTag = grdMarathon.Rows(e.RowIndex).Tag
        If TmpCT.Cost Is Nothing Then
            TmpCT.BookingType.MarathonNetBudget = e.Value
        Else
            If TmpCT.Cost.CostType = Trinity.cCost.CostTypeEnum.CostTypePerUnit Then
                If TmpCT.Cost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots Then
                    grdMarathon.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = e.Value
                End If
            End If
        End If
        grdSummary.Invalidate()
    End Sub

    Private Sub grdSummary_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSummary.CellValueNeeded
        'updates the grid automatically on a number of events (mouse over, activation etc)
        Dim TmpCost As Trinity.cCost = grdSummary.Rows(e.RowIndex).Tag

        Dim Sum As Single = 0

        If Not TmpCost Is Nothing AndAlso TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then

        ElseIf Not TmpCost Is Nothing AndAlso TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeFixed Then
            e.Value = Format(TmpCost.Amount, "N0")
        Else
            For i As Integer = 0 To grdMarathon.Rows.Count - 1
                Dim TmpCT As CostTag = grdMarathon.Rows(i).Tag
                If TmpCT.Cost Is TmpCost Then
                    Sum = Sum + grdMarathon.Rows(i).Cells(3).Value
                End If
            Next
            e.Value = Format(Sum, "N0")
        End If
    End Sub

    Function AcceptAll(ByVal sender As Object, ByVal certificate As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click

        Dim _marathon As New Marathon(TrinitySettings.MarathonCommand)

        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType

        Dim MarathonXML As New System.Text.StringBuilder 'System.Xml.XmlDocument

        Dim XMLSettings As New System.Xml.XmlWriterSettings
        XMLSettings.OmitXmlDeclaration = True
        Dim ExportedToMarathon As XmlWriter = XmlWriter.Create(MarathonXML, XMLSettings)

        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        'selects the product

        Dim product As DataTable = DBReader.getAllFromProducts(Campaign.ProductID)
        'Dim com As New Odbc.OdbcCommand("SELECT * FROM Products Where ID=" & Campaign.ProductID, DBConn)

        pbMarathon.Top = pnlSignature.Top
        pbMarathon.Visible = True
        pbMarathon.Maximum = grdMarathon.Rows.Count

        Dim _otherInsertion As New Marathon.Insertion
        _otherInsertion.CompanyID = product.Rows(0)!MarathonCompany
        _otherInsertion.OrderNumber = Campaign.MarathonOtherOrder
        _otherInsertion.InsertionDate = Date.FromOADate(Campaign.StartDate)
        _otherInsertion.EndDate = Date.FromOADate(Campaign.EndDate)

        If Campaign.MarathonOtherOrder = 0 Then
            'check if there are any "other" orders present
            For i As Integer = 0 To grdMarathon.Rows.Count - 1
                If grdMarathon.Rows(i).Cells(3).Value > 0 Then
                    If Windows.Forms.MessageBox.Show("This campaign contains no Maraton definition for 'GeneralMedia' and insertions might not be correct." & vbCrLf & "Do you want to proceed?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Exit For
                    Else
                        Exit Sub
                    End If
                End If
            Next

            ExportedToMarathon.WriteStartElement("MarathonInsertions")

            'Each row in this grid represents a booking type, and an insertion must be made for each
            For i As Integer = 0 To grdMarathon.Rows.Count - 1
                pbMarathon.Value = i
                Windows.Forms.Application.DoEvents()
                Dim TmpCT As CostTag = grdMarathon.Rows(i).Tag
                TmpChan = TmpCT.BookingType.ParentChannel
                TmpBT = TmpCT.BookingType

                Dim _insertion As New Marathon.Insertion
                _insertion.CompanyID = product.Rows(0)!MarathonCompany
                _insertion.OrderNumber = TmpBT.OrderNumber
                _insertion.InsertionDate = Date.FromOADate(Campaign.StartDate)
                _insertion.EndDate = Date.FromOADate(Campaign.EndDate)

                'We're still on the same insertion, now figuring out what price to give it
                Dim _price As New Marathon.Pricerow
                _price.DiscountCode = TrinitySettings.MarathonDiscountCode
                If TmpBT.ConfirmedGrossBudget > 0 And TmpBT.ConfirmedNetBudget > 0 Then
                    'If there is an estimated number of spots and Trinity is set to use spot count in Marathon (e.g. Spot control) then do this

                    If TmpBT.EstimatedSpotCount > 0 And TrinitySettings.MarathonUseSpotcount Then
                        _price.Quantity = TmpBT.EstimatedSpotCount
                        _price.UnitPrice = (grdMarathon.Rows(i).Cells(3).Value / (TmpBT.ConfirmedNetBudget / TmpBT.ConfirmedGrossBudget)) / TmpBT.EstimatedSpotCount
                        _price.NetCost = Int(grdMarathon.Rows(i).Cells(3).Value)
                    Else
                        _price.UnitPrice = (grdMarathon.Rows(i).Cells(3).Value / (TmpBT.ConfirmedNetBudget / TmpBT.ConfirmedGrossBudget))
                        _price.NetCost = Int(grdMarathon.Rows(i).Cells(3).Value)
                    End If

                    'We want to use the confirmed gross budget, but if it isn't available, we use the planned gross budget
                ElseIf TmpBT.PlannedGrossBudget > 0 And TmpBT.PlannedNetBudget > 0 Then
                    If TmpBT.EstimatedSpotCount > 0 And TrinitySettings.MarathonUseSpotcount Then
                        _price.Quantity = TmpBT.EstimatedSpotCount
                        _price.UnitPrice = (grdMarathon.Rows(i).Cells(3).Value / (TmpBT.PlannedNetBudget / TmpBT.PlannedGrossBudget)) / TmpBT.EstimatedSpotCount
                        _price.NetCost = Int(grdMarathon.Rows(i).Cells(3).Value)
                    Else
                        _price.UnitPrice = (grdMarathon.Rows(i).Cells(3).Value / (TmpBT.PlannedNetBudget / TmpBT.PlannedGrossBudget))
                        _price.NetCost = Int(grdMarathon.Rows(i).Cells(3).Value)
                    End If
                    'If neither of those two are available, we use whatever is in the grid
                Else
                    _price.UnitPrice = Int(grdMarathon.Rows(i).Cells(3).Value)
                    _price.NetCost = Int(grdMarathon.Rows(i).Cells(3).Value)
                End If
                _insertion.PriceRows.Add(_price)


                'OK this insertion is done, upload it
                If TrinitySettings.MarathonCreateInsertionsPerWeek Then
                    CreateWeeklyInsertions(_insertion, TmpBT)
                Else
                    Try
                        _marathon.CreateInsertion(_insertion)
                    Catch ex As Exception
                        Windows.Forms.MessageBox.Show("There was an error while creating insertions for " & TmpChan.ChannelName & "." & vbCrLf & vbCrLf & "Marathon response: " & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                    End Try
                End If
            Next
        Else

            ExportedToMarathon.WriteStartElement("MarathonInsertions")

            'Each row in this grid represents a booking type, and an insertion must be made for each
            For i As Integer = 0 To grdMarathon.Rows.Count - 1
                pbMarathon.Value = i
                Windows.Forms.Application.DoEvents()
                Dim TmpCT As CostTag = grdMarathon.Rows(i).Tag
                TmpChan = TmpCT.BookingType.ParentChannel
                TmpBT = TmpCT.BookingType

                Dim _insertion As New Marathon.Insertion
                _insertion.CompanyID = product.Rows(0)!MarathonCompany
                _insertion.OrderNumber = TmpBT.OrderNumber

                _insertion.InsertionDate = Date.FromOADate(Campaign.StartDate)
                _insertion.EndDate = Date.FromOADate(Campaign.EndDate)

                If TmpCT.Cost Is Nothing Then

                    ExportedToMarathon.WriteStartElement("Order")

                    ExportedToMarathon.WriteStartAttribute("OrderNumber")
                    ExportedToMarathon.WriteValue(TmpBT.OrderNumber)
                    ExportedToMarathon.WriteEndAttribute()

                    ExportedToMarathon.WriteStartAttribute("Channel")
                    ExportedToMarathon.WriteValue(TmpChan.ChannelName)
                    ExportedToMarathon.WriteEndAttribute()

                    ExportedToMarathon.WriteStartAttribute("BookingType")
                    ExportedToMarathon.WriteValue(TmpBT.Name)
                    ExportedToMarathon.WriteEndAttribute()

                    ExportedToMarathon.WriteStartAttribute("Net")
                    ExportedToMarathon.WriteValue(Int(grdMarathon.Rows(i).Cells(3).Value))
                    ExportedToMarathon.WriteEndAttribute()

                    ExportedToMarathon.WriteEndElement()

                    Dim _price As New Marathon.Pricerow
                    _price.DiscountCode = TrinitySettings.MarathonDiscountCode

                    'We're still on the same insertion, now figuring out what price to give it
                    If TmpBT.ConfirmedGrossBudget > 0 And TmpBT.ConfirmedNetBudget > 0 Then
                        'If there is an estimated number of spots and Trinity is set to use spot count in Marathon (e.g. Spot control) then do this
                        If TmpBT.EstimatedSpotCount > 0 And TrinitySettings.MarathonUseSpotcount Then
                            _price.Code = TrinitySettings.MarathonInsertionCode
                            _price.Quantity = TmpBT.EstimatedSpotCount
                            _price.UnitPrice = (grdMarathon.Rows(i).Cells(3).Value / (TmpBT.ConfirmedNetBudget / TmpBT.ConfirmedGrossBudget)) / TmpBT.EstimatedSpotCount
                            _price.NetCost = Int(grdMarathon.Rows(i).Cells(3).Value)
                        Else
                            _price.Code = TrinitySettings.MarathonInsertionCode
                            _price.UnitPrice = (grdMarathon.Rows(i).Cells(3).Value / (TmpBT.ConfirmedNetBudget / TmpBT.ConfirmedGrossBudget))
                            _price.NetCost = Int(grdMarathon.Rows(i).Cells(3).Value)
                        End If
                        'We want to use the confirmed gross budget, but if it isn't available, we use the planned gross budget
                    ElseIf TmpBT.PlannedGrossBudget > 0 And TmpBT.PlannedNetBudget > 0 Then
                        If TmpBT.EstimatedSpotCount > 0 And TrinitySettings.MarathonUseSpotcount Then
                            _price.Code = TrinitySettings.MarathonInsertionCode
                            _price.Quantity = TmpBT.EstimatedSpotCount
                            _price.UnitPrice = (grdMarathon.Rows(i).Cells(3).Value / (TmpBT.PlannedNetBudget / TmpBT.PlannedGrossBudget)) / TmpBT.EstimatedSpotCount
                            _price.NetCost = Int(grdMarathon.Rows(i).Cells(3).Value)
                        Else
                            _price.Code = TrinitySettings.MarathonInsertionCode
                            _price.UnitPrice = (grdMarathon.Rows(i).Cells(3).Value / (TmpBT.PlannedNetBudget / TmpBT.PlannedGrossBudget))
                            _price.NetCost = Int(grdMarathon.Rows(i).Cells(3).Value)
                        End If
                        'If neither of those two are available, we use whatever is in the grid
                    Else
                        _price.Code = TrinitySettings.MarathonInsertionCode
                        _price.UnitPrice = Int(grdMarathon.Rows(i).Cells(3).Value)
                        _price.NetCost = Int(grdMarathon.Rows(i).Cells(3).Value)
                    End If
                    _insertion.PriceRows.Add(_price)

                    'OK this insertion is done, upload it

                    If TrinitySettings.MarathonCreateInsertionsPerWeek Then
                        CreateWeeklyInsertions(_insertion, TmpBT)
                    Else
                        Try
                            _marathon.CreateInsertion(_insertion)
                        Catch ex As Exception
                            Windows.Forms.MessageBox.Show("There was an error while creating insertions for " & TmpChan.ChannelName & "." & vbCrLf & vbCrLf & "Marathon response: " & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                        End Try
                    End If
                Else
                    'This is if there is a Cost associated
                    If grdMarathon.Rows(i).Cells(3).Value > 0 Then
                        Dim _price As New Marathon.Pricerow
                        _price.Code = Format(TmpCT.Cost.MarathonID, "000")
                        _price.UnitPrice = Int(grdMarathon.Rows(i).Cells(3).Value)
                        _price.DiscountCode = TrinitySettings.MarathonDiscountCode
                        _price.NetCost = grdMarathon.Rows(i).Cells(3).Value

                        _otherInsertion.PriceRows.Add(_price)

                        ExportedToMarathon.WriteStartElement("Order")

                        ExportedToMarathon.WriteStartAttribute("OrderNumber")
                        ExportedToMarathon.WriteValue(Campaign.MarathonOtherOrder)
                        ExportedToMarathon.WriteEndAttribute()

                        ExportedToMarathon.WriteStartAttribute("Channel")
                        ExportedToMarathon.WriteValue(TmpCT.Cost.CostName)
                        ExportedToMarathon.WriteEndAttribute()

                        ExportedToMarathon.WriteStartAttribute("BookingType")
                        ExportedToMarathon.WriteValue("Cost")
                        ExportedToMarathon.WriteEndAttribute()

                        ExportedToMarathon.WriteStartAttribute("Net")
                        ExportedToMarathon.WriteValue(Int(grdMarathon.Rows(i).Cells(3).Value))
                        ExportedToMarathon.WriteEndAttribute()

                        ExportedToMarathon.WriteEndElement()
                    End If
                End If

            Next
        End If

        ExportedToMarathon.WriteEndElement()
        ExportedToMarathon.Flush()
        Try
            _marathon.CreateInsertion(_otherInsertion)
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("There was an error while creating insertions for the general media." & vbCrLf & vbCrLf & "Marathon response: " & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try

        Dim _ctc As Single
        Try
            _ctc = _marathon.GetCTCForPlan(product.Rows(0)("MarathonCompany"), Campaign.MarathonPlanNr)
            Campaign.MarathonCTC = _ctc
        Catch ex As Exception

        End Try

        Dim newDoc As New System.Xml.XmlDocument
        newDoc.LoadXml(MarathonXML.ToString)
        Campaign.MarathonInsertions = newDoc.FirstChild

        Me.Cursor = Windows.Forms.Cursors.Default
        pbMarathon.Visible = False
        MsgBox("Campaign was succefully exported to Marathon." & vbCrLf & "Marathon CTC for campaign was " & Format(Campaign.MarathonCTC, "N0"), vbInformation, "T R I N I T Y")
        Me.Close()

    End Sub

    Sub CreateWeeklyInsertions(Insertion As Marathon.Insertion, Bookingtype As Trinity.cBookingType)
        Dim _marathon As New Marathon(TrinitySettings.MarathonCommand)
        For Each _week As Trinity.cWeek In Bookingtype.Weeks
            If Not Bookingtype.PlannedNetBudget = 0 Then
                Dim _percent As Single = _week.NetBudget / Bookingtype.PlannedNetBudget
                Dim _weekInsertion As New Marathon.Insertion
                _weekInsertion.CompanyID = Insertion.CompanyID
                _weekInsertion.OrderNumber = Insertion.OrderNumber
                _weekInsertion.InsertionDate = Date.FromOADate(_week.StartDate)
                _weekInsertion.EndDate = Date.FromOADate(_week.EndDate)
                For Each _origPrice As Marathon.Pricerow In Insertion.PriceRows
                    Dim _price As New Marathon.Pricerow
                    If _origPrice.Quantity > 1 Then
                        _price.Quantity = _origPrice.Quantity * _percent
                        _price.UnitPrice = _origPrice.UnitPrice
                    Else
                        _price.Quantity = 1
                        _price.UnitPrice = _origPrice.UnitPrice * _percent
                    End If
                    _price.NetCost = _origPrice.NetCost * _percent
                    _price.Code = _origPrice.Code
                    _price.DiscountCode = _origPrice.DiscountCode
                    _weekInsertion.PriceRows.Add(_price)
                Next
                _marathon.CreateInsertion(_weekInsertion)
            End If
        Next
    End Sub
End Class