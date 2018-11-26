
Public Class frmExportSpotlist
    'exports the choosen spots to a excel sheet.
    Dim mnuLanguage As New Windows.Forms.ContextMenuStrip

    Structure LanguageTag
        Public Abbrevation As String
        Public Path As String
    End Structure

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub frmExportSpotlist_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        
    End Sub

    Sub ChangeLanguage(ByVal sender As Object, ByVal e As EventArgs)
        Dim TmpMnu As Windows.Forms.ToolStripMenuItem
        Dim LangINI As New Trinity.clsIni
        Dim i As Integer

        cmdLanguage.Tag = DirectCast(sender.Tag, LanguageTag).Path
        cmdLanguage.Image = frmMain.ilsBig.Images(DirectCast(sender.Tag, LanguageTag).Abbrevation)

        For Each TmpMnu In mnuLanguage.Items
            TmpMnu.Checked = False
        Next
        sender.Checked = True

        cmbHeadline.Items.Clear()
        LangINI.Create(cmdLanguage.Tag)
        For i = 1 To LangINI.Data("Headlines", "Count")
            cmbHeadline.Items.Add(LangINI.Text("Headlines", "Headline" & i))
        Next
        cmbHeadline.SelectedIndex = 0
    End Sub

    Private Sub tvwAvailable_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvwAvailable.ItemDrag
        tvwChosen.DoDragDrop(e.Item, Windows.Forms.DragDropEffects.All)
    End Sub

    Private Sub tvwChosen_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwChosen.DragDrop
        Dim targetPoint As System.Drawing.Point = tvwChosen.PointToClient(New System.Drawing.Point(e.X, e.Y))

        ' Retrieve the node at the drop location.
        Dim targetNode As System.Windows.Forms.TreeNode = tvwChosen.GetNodeAt(targetPoint)

        ' Retrieve the node that was dragged.
        Dim draggedNode As System.Windows.Forms.TreeNode = CType(e.Data.GetData(GetType(System.Windows.Forms.TreeNode)), System.Windows.Forms.TreeNode)

        draggedNode.TreeView.Nodes.Remove(draggedNode)

        If targetNode Is Nothing Then
            tvwChosen.Nodes.Add(draggedNode)
        Else
            tvwChosen.Nodes.Insert(targetNode.Index, draggedNode)
        End If
    End Sub

    Private Sub tvwChosen_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwChosen.DragEnter
        e.Effect = e.AllowedEffect
    End Sub

    Private Sub tvwChosen_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwChosen.DragOver
        Dim targetPoint As System.Drawing.Point = tvwChosen.PointToClient(New System.Drawing.Point(e.X, e.Y))

        e.Effect = e.AllowedEffect
        ' Select the node at the mouse position.
        tvwChosen.SelectedNode = tvwChosen.GetNodeAt(targetPoint)

    End Sub

    Private Class cSheetHandler
        'Class to handle printing channels on different sheets in Sub below class definition

        Private _sheetPerChannel As Boolean
        Private _channels As New Dictionary(Of String, Trinity.cChannel)
        Private _currentRow As New Hashtable
        Private _lastSumRow As New Hashtable
        Private _lastWeek As New Hashtable
        Private _rowCount As New Hashtable
        Private _workbook As Object
        Private _daypartSum As New Hashtable

        Private _header As cHTMLTable
        Public Property Header() As cHTMLTable
            Get
                If _header Is Nothing Then _header = New cHTMLTable
                Return _header
            End Get
            Set(ByVal value As cHTMLTable)
                _header = value
            End Set
        End Property

        Private _spotlist As New Hashtable
        Public ReadOnly Property Spotlist(ByVal Sheet As String) As cHTMLTable
            Get
                If Not _sheetPerChannel Then
                    Sheet = ""
                End If
                If Not _spotlist.ContainsKey(Sheet) Then
                    _spotlist.Add(Sheet, New cHTMLTable)
                End If
                Return _spotlist(Sheet)
            End Get
        End Property

        Public Property SheetPerChannel() As Boolean
            Get
                Return _sheetPerChannel
            End Get
            Set(ByVal value As Boolean)
                _sheetPerChannel = value
            End Set
        End Property

        Public Function GetSheetForChannel(ByVal ChannelName As String) As String
            If Not _sheetPerChannel Then
                Return _workbook.sheets(1).name
            Else
                Return ChannelName
            End If
        End Function

        Public Function GetSheetList() As List(Of String)
            Dim TmpList As New List(Of String)
            If Not _sheetPerChannel Then
                TmpList.Add(_workbook.sheets(1).name)
            Else
                For Each kv As KeyValuePair(Of String, Trinity.cChannel) In _channels
                    TmpList.Add(kv.Key)
                Next
            End If
            Return TmpList
        End Function

        Public Sub New(ByVal Workbook As Object)
            _workbook = Workbook
            For Each TmpChan As Trinity.cChannel In Campaign.Channels
                Dim UseChan As Boolean = False
                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        UseChan = True
                        Exit For
                    End If
                Next
                If UseChan Then
                    _channels.Add(TmpChan.ChannelName, TmpChan)
                End If
            Next
        End Sub

        Public Property CurrentRow(ByVal Sheet As String) As Integer
            Get
                If Not _sheetPerChannel Then
                    Sheet = ""
                End If
                If Not _currentRow.ContainsKey(Sheet) Then
                    _currentRow.Add(Sheet, 2) 'Add 2 as first value
                End If
                Return _currentRow(Sheet)
            End Get
            Set(ByVal value As Integer)
                If Not _sheetPerChannel Then
                    Sheet = ""
                End If
                If Not _currentRow.ContainsKey(Sheet) Then
                    _currentRow.Add(Sheet, 2) 'Add 2 as first value
                End If
                _currentRow(Sheet) = value
            End Set
        End Property

        Public Property LastSumRow(ByVal Sheet As String) As Integer
            Get
                If Not _sheetPerChannel Then
                    Sheet = ""
                End If
                If Not _lastSumRow.ContainsKey(Sheet) Then
                    _lastSumRow.Add(Sheet, -1) 'Add -1 as first value
                End If
                Return _lastSumRow(Sheet)
            End Get
            Set(ByVal value As Integer)
                If Not _sheetPerChannel Then
                    Sheet = ""
                End If
                If Not _lastSumRow.ContainsKey(Sheet) Then
                    _lastSumRow.Add(Sheet, -1) 'Add -1 as first value
                End If
                _lastSumRow(Sheet) = value
            End Set
        End Property

        Public Property LastWeek(ByVal Sheet As String) As Integer
            Get
                If Not _sheetPerChannel Then
                    Sheet = ""
                End If
                If Not _lastWeek.ContainsKey(Sheet) Then
                    _lastWeek.Add(Sheet, -1) 'Add -1 as first value
                End If
                Return _lastWeek(Sheet)
            End Get
            Set(ByVal value As Integer)
                If Not _sheetPerChannel Then
                    Sheet = ""
                End If
                If Not _lastWeek.ContainsKey(Sheet) Then
                    _lastWeek.Add(Sheet, -1) 'Add -1 as first value
                End If
                _lastWeek(Sheet) = value
            End Set
        End Property

        Public Property RowCount(ByVal Sheet As String) As Integer
            Get
                If Not _sheetPerChannel Then
                    Sheet = ""
                End If
                If Not _rowCount.ContainsKey(Sheet) Then
                    _rowCount.Add(Sheet, 0) 'Add -1 as first value
                End If
                Return _rowCount(Sheet)
            End Get
            Set(ByVal value As Integer)
                If Not _sheetPerChannel Then
                    Sheet = ""
                End If
                If Not _rowCount.ContainsKey(Sheet) Then
                    _rowCount.Add(Sheet, 0) 'Add -1 as first value
                End If
                _rowCount(Sheet) = value
            End Set
        End Property

        Public Property DaypartSum(ByVal Sheet As String, ByVal Daypart As Integer) As Single
            Get
                If Not _sheetPerChannel Then
                    Sheet = ""
                End If
                If Not _daypartSum.ContainsKey(Sheet) Then
                    Dim TmpArray(Campaign.Dayparts.Count) As Single
                    _daypartSum.Add(Sheet, TmpArray)
                End If
                Return _daypartSum(Sheet)(Daypart)
            End Get
            Set(ByVal value As Single)
                If Not _sheetPerChannel Then
                    Sheet = ""
                End If
                If Not _daypartSum.ContainsKey(Sheet) Then
                    Dim TmpArray(Campaign.Dayparts.Count) As Single
                    _daypartSum.Add(Sheet, TmpArray)
                End If
                _daypartSum(Sheet)(Daypart) = value
            End Set
        End Property
    End Class

    Private Function GetHTMLColor(ByVal Color As Long) As String
        Dim TmpStr As String
        TmpStr = Hex(Color)
        While TmpStr.Length < 6
            TmpStr = TmpStr & "0"
        End While
        TmpStr = TmpStr.Substring(4, 2) & TmpStr.Substring(2, 2) & TmpStr.Substring(0, 2)
        Return "#" & TmpStr
    End Function

    Private Sub ExportSpotlist(ByVal sender As Object, ByVal e As EventArgs) Handles cmdOk.Click
        Dim Excel As CultureSafeExcel.Application
        Dim colorArray(20) As Long
        Dim coloring As New List(Of String)
        Dim WB As CultureSafeExcel.Workbook
        Dim LangIni As New Trinity.clsIni
        Dim PeriodStr As String

        If Me.Tag = "ACTUAL" Then
            cmdOk_Click(sender, e)
            Exit Sub
        End If



        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'Create array of colors
        If chkColorCode.Checked Or chkColorCodeFilm.Checked Then
            For i As Integer = 1 To 20
                colorArray(i - 1) = RGB(ConvertIntToARGB(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Diagram" & i)).B, ConvertIntToARGB(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Diagram" & i)).G, ConvertIntToARGB(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Diagram" & i)).R)
            Next
        End If

        LangIni.Create(cmdLanguage.Tag)

        Dim Days() As String = {LangIni.Text("Weekdays", "Day1"), LangIni.Text("Weekdays", "Day2"), LangIni.Text("Weekdays", "Day3"), LangIni.Text("Weekdays", "Day4"), LangIni.Text("Weekdays", "Day5"), LangIni.Text("Weekdays", "Day6"), LangIni.Text("Weekdays", "Day7")}

        If chkDefault.Checked Then
            SaveDefaults()
        End If

        'Create period string
        If DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) Then
            PeriodStr = LangIni.Text("Spotlist", "Week") & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & ", " & Year(Date.FromOADate(Campaign.StartDate))
        Else
            PeriodStr = LangIni.Text("Spotlist", "Week") & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & "-" & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & ", " & Year(Date.FromOADate(Campaign.StartDate))
        End If

        'creates all variables needed to setup the excel sheet
        Excel = New CultureSafeExcel.Application(False)

        Excel.ScreenUpdating = False
        Excel.DisplayAlerts = False
        Excel.Visible = False
        WB = Excel.AddWorkbook

        'Add as many sheets as there are channels, in case the one sheet per channel option is checked
        'Also, name the sheets
        'The sheethandler contains all the channels of the campaign with BookIt = True
        Dim SheetHandler As New cSheetHandler(WB)
        SheetHandler.SheetPerChannel = chkSheetPerChannel.Checked
        If chkSheetPerChannel.Checked Then
            Dim SheetCount As Integer = WB.SheetCount
            For Each TmpString As String In SheetHandler.GetSheetList
                With WB.AddSheet(After:=WB.Sheets(WB.SheetCount))
                    .Name = TmpString
                End With
            Next
            For i As Integer = 1 To SheetCount
                WB.Sheets(i).Delete()
            Next
        Else
            WB.Sheets(1).Name = cmbHeadline.Text
        End If

        Dim Header As cHTMLTable = SheetHandler.Header
        Header.ShowHeader = False
        Header.Columns.Add()
        Header.Columns.Add()
        Dim TitleRow As New cHTMLTableRow
        Dim TitleCell As New cHTMLTableCell
        With TitleCell
            If chkPreliminary.Checked Then
                .Text = LangIni.Text("Spotlist", "Preliminary") & " " & cmbHeadline.Text & " " & Campaign.Name
            Else
                .Text = cmbHeadline.Text & " " & Campaign.Name
            End If
            .ColumnSpan = tvwChosen.Nodes.Count
            .CSSStyle = "font-weight: bold; font-size: 14pt; background-color:" & GetHTMLColor(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")) & ";color:" & GetHTMLColor(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText"))
        End With
        TitleRow.Cells.Add(TitleCell)
        Header.Rows.Add(TitleRow)
        Header.Rows.Add()

        Dim WeekSumRows As New Hashtable

        'Create column headers
        For Each TmpString As String In SheetHandler.GetSheetList
            Dim TmpNode As Windows.Forms.TreeNode = tvwChosen.Nodes(1)
            While Not TmpNode.PrevNode Is Nothing
                TmpNode = TmpNode.PrevNode
            End While
            While Not TmpNode Is Nothing
                With SheetHandler.Spotlist(TmpString).Columns.Add()
                    If Not LangIni.Text("Spotlist", TmpNode.Text) = "" Then
                        .HeaderCell.Text = LangIni.Text("Spotlist", TmpNode.Text)
                    Else
                        .HeaderCell.Text = TmpNode.Text
                    End If
                End With
                TmpNode = TmpNode.NextNode
            End While
            SheetHandler.Spotlist(TmpString).HeaderCssStyle = "font-weight: bold; font-size: 10pt; background-color:" & GetHTMLColor(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")) & ";color:" & GetHTMLColor(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")) & ";border-top:1px solid black;border-bottom:1px solid black;"
            SheetHandler.Spotlist(TmpString).Columns(0).HeaderCell.CSSStyle = "border-left:1px solid black;"
            SheetHandler.Spotlist(TmpString).Columns(SheetHandler.Spotlist(TmpString).Columns.Count - 1).HeaderCell.CSSStyle = "border-right:1px solid black;"
            WeekSumRows.Add(TmpString, New ArrayList)
        Next

        'get the program column number
        Dim programCol As Integer
        For programCol = 0 To frmSpots.grdConfirmed.Columns.Count - 1
            If frmSpots.grdConfirmed.Columns(programCol).HeaderText = "Program" Then
                Exit For
            End If
        Next

        frmProgress.pbProgress.Maximum = frmSpots.grdConfirmed.Rows.Count
        frmProgress.pbProgress.Value = 0
        frmProgress.lblStatus.Text = "Creating spotlist..."
        frmProgress.Show()

        Dim rowstoprint As Windows.Forms.DataGridViewRowCollection

        If Me.Tag = "ACTUAL" Then
            rowstoprint = frmSpots.grdActual.Rows
        Else
            rowstoprint = frmSpots.grdConfirmed.Rows
        End If

        'populate the newly created excel sheet with the information
        For Each TmpRow As Windows.Forms.DataGridViewRow In rowstoprint
            frmProgress.pbProgress.Value += 1
            Windows.Forms.Application.DoEvents()

            'only print the visible rows, meaning we dont print the filtered ones
            If TmpRow.Visible Then
                'added to not count the RBS created spots
                If TmpRow.Cells(programCol).Value.ToString.Length < 3 OrElse Not TmpRow.Cells(programCol).Value.ToString.Substring(0, 3) = "RBS" Then
                    Dim TmpNode As Windows.Forms.TreeNode = tvwChosen.Nodes(1)
                    While Not TmpNode.PrevNode Is Nothing
                        TmpNode = TmpNode.PrevNode
                    End While
                    If TmpRow.Tag IsNot Nothing Then
                        Dim ID As String = TmpRow.Tag.Item("ID")

                        Dim TmpSheet As String = SheetHandler.GetSheetForChannel(Campaign.PlannedSpots(ID).Channel.ChannelName)
                        If SheetHandler.Spotlist(TmpSheet).Columns.Count > 0 Then
                            SheetHandler.RowCount(TmpSheet) += 1

                            Dim SaveNode As Windows.Forms.TreeNode = TmpNode

                            Dim [Date] As Date
                            If chkConvertTime.Checked Then
                                [Date] = Date.FromOADate(Trinity.Helper.DateTimeSerial(Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), Campaign.PlannedSpots(ID).MaM))
                            Else
                                [Date] = Date.FromOADate(Campaign.PlannedSpots(ID).AirDate)
                            End If
                            If Not SheetHandler.LastWeek(TmpSheet) = -1 AndAlso DatePart(DateInterval.WeekOfYear, [Date], FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) > SheetHandler.LastWeek(TmpSheet) Then
                                SheetHandler.Spotlist(TmpSheet).Rows.Add()
                                If chkWeekSum.Checked Then
                                    SheetHandler.Spotlist(TmpSheet).Rows.Add()
                                    WeekSumRows(TmpSheet).Add(SheetHandler.Spotlist(TmpSheet).Rows.Count)
                                End If
                            End If
                            SheetHandler.LastWeek(TmpSheet) = DatePart(DateInterval.WeekOfYear, [Date], FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                            With SheetHandler.Spotlist(TmpSheet).Rows.Add
                                If chkColorCode.Checked Then
                                    If coloring.IndexOf(Campaign.PlannedSpots(ID).Channel.ChannelName.ToUpper) > -1 Then
                                        .CSSStyle &= "color:" & GetHTMLColor(colorArray(coloring.IndexOf(Campaign.PlannedSpots(ID).Channel.ChannelName.ToUpper))) & ";"
                                    Else
                                        coloring.Add(Campaign.PlannedSpots(ID).Channel.ChannelName.ToUpper)
                                        .CSSStyle &= "color:" & GetHTMLColor(colorArray(coloring.IndexOf(Campaign.PlannedSpots(ID).Channel.ChannelName.ToUpper))) & ";"
                                    End If
                                End If
                                If chkColorCodeFilm.Checked Then
                                    If Not Campaign.PlannedSpots(ID).Film Is Nothing Then
                                        Dim _idx As Integer = coloring.IndexOf(Campaign.PlannedSpots(ID).Film.Name)
                                        While _idx > colorArray.Length
                                            _idx -= colorArray.Length + 1
                                        End While
                                        If coloring.IndexOf(Campaign.PlannedSpots(ID).Film.Name) <> -1 Then
                                            .CSSStyle &= "color:" & GetHTMLColor(colorArray(_idx))
                                        Else
                                            coloring.Add(Campaign.PlannedSpots(ID).Film.Name)
                                            _idx = coloring.IndexOf(Campaign.PlannedSpots(ID).Film.Name)
                                            .CSSStyle &= "color:" & GetHTMLColor(colorArray(_idx))
                                        End If
                                    End If
                                End If
                                Dim c As Integer = 0
                                While Not TmpNode Is Nothing
                                    Dim Est As Single = Campaign.PlannedSpots(ID).MyRating
                                    Dim EstBT As Single = Campaign.PlannedSpots(ID).MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
                                    If TmpNode.Text <> "Date" AndAlso TmpNode.Text <> "Program" Then
                                        .Cells(c).CSSStyle = "text-align:center;"
                                        SheetHandler.Spotlist(TmpSheet).Columns(c).HeaderCell.CSSStyle &= "text-align:center;"
                                    Else
                                        .Cells(c).CSSStyle = "text-align:left;"
                                        SheetHandler.Spotlist(TmpSheet).Columns(c).HeaderCell.CSSStyle &= "text-align:left;"
                                    End If

                                    Select Case TmpNode.Text
                                        Case "ID" : .Cells(c).Text = Campaign.PlannedSpots(ID).ID
                                        Case "TimeStamp" : .Cells(c).Text = Trinity.Helper.DateTimeSerial(Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), Campaign.PlannedSpots(ID).MaM)
                                        Case "Date"
                                            .Cells(c).Text = Format([Date], "yyyy-MM-dd")
                                        Case "Week" : .Cells(c).Text = DatePart(DateInterval.WeekOfYear, [Date], FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                                        Case "Bookingtype"
                                            .Cells(c).Text = Campaign.PlannedSpots(ID).Bookingtype.Name
                                        Case "Duration"
                                            .Cells(c).Text = Campaign.PlannedSpots(ID).SpotLength
                                        Case "Weekday"
                                            .Cells(c).Text = Days(Weekday(Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), vbMonday) - 1)
                                            If chkConvertTime.Checked Then
                                                .Cells(c).Text = Days(Weekday(Date.FromOADate(Trinity.Helper.DateTimeSerial(Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), Campaign.PlannedSpots(ID).MaM)), FirstDayOfWeek.Monday) - 1)
                                            Else
                                                .Cells(c).Text = Days(Weekday(Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), FirstDayOfWeek.Monday) - 1)
                                            End If
                                        Case "Time"
                                            '.range(Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow).NumberFormat = "@"
                                            If chkConvertTime.Checked Then
                                                .Cells(c).Text = Format(Date.FromOADate(Trinity.Helper.DateTimeSerial(Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), Campaign.PlannedSpots(ID).MaM)), "HH:mm")
                                            Else
                                                .Cells(c).Text = Trinity.Helper.Mam2Tid(Campaign.PlannedSpots(ID).MaM)
                                            End If
                                        Case "Channel"
                                            .Cells(c).Text = Campaign.PlannedSpots(ID).Channel.ChannelName
                                        Case "Added value"
                                            If Campaign.PlannedSpots(ID).AddedValue Is Nothing Then
                                                .Cells(c).Text = ""
                                            Else
                                                .Cells(c).Text = Campaign.PlannedSpots(ID).AddedValue.Name
                                            End If

                                        Case "Program"
                                            If chkCapital.Checked Then
                                                .Cells(c).Text = UCase(Campaign.PlannedSpots(ID).Programme)
                                            ElseIf chkCapOnFirst.Checked Then
                                                If Not Campaign.PlannedSpots(ID).Programme = "" Then
                                                    .Cells(c).Text = UCase(Campaign.PlannedSpots(ID).Programme.Substring(0, 1)) & LCase(Campaign.PlannedSpots(ID).Programme.Substring(1))
                                                End If
                                            Else
                                                .Cells(c).Text = Campaign.PlannedSpots(ID).Programme
                                            End If
                                        Case "Gross Price"
                                            '.range(Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                            .Cells(c).Text = Campaign.PlannedSpots(ID).PriceGross
                                        Case "Chan Est"
                                            '.range(Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow).NumberFormat = "0.0"
                                            .Cells(c).Text = Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget).ToString.Replace(".", ",")
                                        Case "Net Price"
                                            '.range(Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                            .Cells(c).Text = Campaign.PlannedSpots(ID).PriceNet
                                        Case "Remarks"
                                            If Campaign.PlannedSpots(ID).Remark = "L" Then
                                                .Cells(c).Text = "Local"
                                            End If
                                        Case "Gross CPP"
                                            If Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                                .Cells(c).Text = Campaign.PlannedSpots(ID).PriceNet / Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
                                            Else
                                                .Cells(c).Text = 0
                                            End If
                                        Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                                            If Est > 0 Then
                                                .Cells(c).Text = Campaign.PlannedSpots(ID).PriceNet / Est
                                            Else
                                                .Cells(c).Text = 0
                                            End If
                                        Case "CPP (Chn Est)"
                                            If Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                                .Cells(c).Text = Campaign.PlannedSpots(ID).PriceNet / Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
                                            Else
                                                .Cells(c).Text = 0
                                            End If
                                        Case "Est"
                                            .Cells(c).Text = Campaign.PlannedSpots(ID).MyRating.ToString.Replace(".", ",")
                                        Case "Filmcode"
                                            If Campaign.PlannedSpots(ID).Film Is Nothing Then
                                                .Cells(c).Text = "NO FILMCODE"
                                            Else
                                                .Cells(c).Text = Campaign.PlannedSpots(ID).Film.Filmcode
                                            End If

                                        Case "Film" : .Cells(c).Text = Campaign.PlannedSpots(ID).Film.Name
                                        Case "Film dscr" : .Cells(c).Text = Campaign.PlannedSpots(ID).Film.Description
                                            'Hannes added the next two cases
                                        Case "Prog Before"
                                            If chkCapital.Checked Then
                                                .Cells(c).Text = UCase(Campaign.PlannedSpots(ID).ProgBefore)
                                            ElseIf chkCapOnFirst.Checked Then
                                                If Not Campaign.PlannedSpots(ID).ProgBefore = "" Then
                                                    .Cells(c).Text = UCase(Campaign.PlannedSpots(ID).ProgBefore.Substring(0, 1)) & LCase(Campaign.PlannedSpots(ID).ProgBefore.Substring(1))
                                                End If
                                            Else
                                                .Cells(c).Text = Campaign.PlannedSpots(ID).ProgBefore
                                            End If
                                        Case "Prog After"
                                            If chkCapital.Checked Then
                                                .Cells(c).Text = UCase(Campaign.PlannedSpots(ID).ProgAfter)
                                            ElseIf chkCapOnFirst.Checked Then
                                                If Not Campaign.PlannedSpots(ID).ProgAfter = "" Then
                                                    .Cells(c).Text = UCase(Campaign.PlannedSpots(ID).ProgAfter.Substring(0, 1)) & LCase(Campaign.PlannedSpots(ID).ProgAfter.Substring(1))
                                                End If
                                            Else
                                                .Cells(c).Text = Campaign.PlannedSpots(ID).ProgAfter
                                            End If
                                    End Select
                                    TmpNode = TmpNode.NextNode
                                    c = c + 1
                                End While
                            End With
                            SheetHandler.DaypartSum(TmpSheet, Campaign.Dayparts.GetDaypartIndexForMam(Campaign.PlannedSpots(ID).MaM)) += Campaign.PlannedSpots(ID).MyRating
                            SheetHandler.RowCount(TmpSheet) += 1
                        End If
                    End If
                End If
            End If
        Next

        Dim SpotCountRow As Integer = 0

        For i As Integer = 0 To lstInfo.Items.Count - 1
            If lstInfo.GetItemChecked(i) Then
                Dim TmpRow As cHTMLTableRow = Header.Rows.Add
                With TmpRow
                    .Cells(0).Text = LangIni.Text("Spotlist", lstInfo.Items(i))
                    .Cells(0).CSSStyle = "font-weight:bold;"
                    Select Case lstInfo.Items(i)
                        Case "Client" : .Cells(1).Text = Campaign.Client
                        Case "Product" : .Cells(1).Text = Campaign.Product
                        Case "Buyer" : .Cells(1).Text = Campaign.Buyer
                        Case "Period" : .Cells(1).Text = PeriodStr
                        Case "E-mail" : .Cells(1).Text = Campaign.BuyerEmail
                        Case "Phone Nr" : .Cells(1).Text = Campaign.BuyerPhone
                        Case "Spot count" : .Cells(1).Text = 0 : SpotCountRow = Header.Rows.Count
                    End Select
                    .Cells(1).CSSStyle = "text-align:left;"
                End With
            End If
        Next

        If SheetHandler.GetSheetList.Count > 1 Then
            frmProgress.Progress = 0
            frmProgress.Status = "Updating layout..."
            frmProgress.pbProgress.Maximum = SheetHandler.GetSheetList.Count
        End If
        For Each TmpString As String In SheetHandler.GetSheetList
            If SheetHandler.GetSheetList.Count > 1 Then
                frmProgress.pbProgress.Value += 1
                My.Application.DoEvents()
            End If

            'add a weeksumrow in the bottom of the list
            If chkWeekSum.Checked Then
                SheetHandler.Spotlist(TmpString).Rows.Add()
                WeekSumRows(TmpString).Add(SheetHandler.Spotlist(TmpString).Rows.Count + 1)
            End If
            Dim LastWeekSum As Integer
            With WB.Sheets(TmpString)
                Dim SumRow As Integer = Header.Rows.Count + 3 + SheetHandler.Spotlist(TmpString).Rows.Count
                'Paste spotlist
                .Activate()
                .AllCells.Font.Name = TrinitySettings.SchemeFont(cmbColorScheme.SelectedIndex + 1)
                .Cells(Header.Rows.Count + 2, 1).select()
                SheetHandler.Spotlist(TmpString).ShowHeader = True
                Windows.Forms.Clipboard.SetText(SheetHandler.Spotlist(TmpString).ToHTML, Windows.Forms.TextDataFormat.Html)
                .PasteSpecial(Format:="HTML", Link:=False, DisplayAsIcon:=False)
                .AllCells.WrapText = False
                .AllCells.EntireColumn.AutoFit()

                'Paste header
                .Cells(1, 1).Select()
                Windows.Forms.Clipboard.SetText(Header.ToHTML, Windows.Forms.TextDataFormat.Html)
                .PasteSpecial(Format:="HTML", Link:=False, DisplayAsIcon:=False)
                .AllCells.WrapText = False

                'Create footer
                .Range("A" & Header.Rows.Count + 2 & ":" & Chr(64 + tvwChosen.Nodes.Count) & Header.Rows.Count + 2).Copy()
                .Cells(SumRow, 1).pastespecial(Paste:=-4122, Operation:=-4142, Skipblanks:=False, Transpose:=False)

                'Create sums and format columns
                Dim TmpNode As Windows.Forms.TreeNode = tvwChosen.Nodes(1)
                While Not TmpNode.PrevNode Is Nothing
                    TmpNode = TmpNode.PrevNode
                End While
                Dim c As Integer = 1
                While TmpNode IsNot Nothing
                    Dim DivStr As String = ""
                    If chkWeekSum.Checked Then
                        'If week sums are present divide the sum by two, since the weekly sums will be included in the total sum
                        DivStr = "/2"
                    End If
                    .Range(Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow).Replace(What:=".", Replacement:=",", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                    If TmpNode.Text <> "Date" Then
                        '.range(Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow).HorizontalAlignment = -4142
                    End If
                    LastWeekSum = Header.Rows.Count + 2
                    Select Case TmpNode.Text
                        Case "Gross Price"
                            .Range(Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow).Numberformat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                            .Cells(SumRow, c).Value = "=SUM(" & Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow - 1 & ")" & DivStr
                        Case "Chan Est"
                            .Range(Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow).Numberformat = "0.0"
                            .Cells(SumRow, c).Value = "=SUM(" & Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow - 1 & ")" & DivStr
                        Case "Net Price"
                            .Range(Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow).Numberformat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                            .Cells(SumRow, c).Value = "=SUM(" & Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow - 1 & ")" & DivStr
                        Case "Gross CPP"
                            .Range(Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow).Numberformat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                        Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                            .Range(Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow).Numberformat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                        Case "CPP (Chn Est)"
                            .Range(Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow).Numberformat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                        Case "Est"
                            .Range(Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow).Numberformat = "0.0"
                            .Cells(SumRow, c).Value = "=SUM(" & Chr(64 + c) & Header.Rows.Count + 3 & ":" & Chr(64 + c) & SumRow - 1 & ")" & DivStr
                            For Each TmpRow As Integer In WeekSumRows(TmpString)
                                .Cells(TmpRow + Header.Rows.Count + 1, c).Formula = "=SUM(" & Chr(64 + c) & LastWeekSum + 1 & ":" & Chr(64 + c) & TmpRow + Header.Rows.Count & ")"
                                LastWeekSum = TmpRow + Header.Rows.Count + 1
                            Next
                    End Select
                    TmpNode = TmpNode.NextNode
                    c += 1
                End While

                'Print Daypart sum
                If chkSumDaypart.Checked Then
                    Dim x As Integer
                    'write the headline
                    SheetHandler.CurrentRow(TmpString) = SumRow + 2
                    With .Range("A" & SheetHandler.CurrentRow(TmpString) & ":" & Char.ConvertFromUtf32(65 + Campaign.Dayparts.Count - 1) & SheetHandler.CurrentRow(TmpString) + 1)
                        .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                        .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                        For x = 7 To Campaign.Dayparts.Count + 7
                            .Borders(x).LineStyle = 1
                            .Borders(x).Weight = -4138
                            .Borders(x).Color = 0
                        Next
                        .Cells(1, 1).Font.Bold = True
                        'Next
                        .Cells(1, 1).Value = "Daypart summary"
                    End With
                    'write the border
                    With .Range("A" & SheetHandler.CurrentRow(TmpString) + 2 & ":" & Char.ConvertFromUtf32(65 + Campaign.Dayparts.Count - 1) & SheetHandler.CurrentRow(TmpString) + 1 + Campaign.Dayparts.Count - 1)
                        For x = 7 To Campaign.Dayparts.Count + 7
                            .Borders(x).LineStyle = 1
                            .Borders(x).Weight = 2
                            .Borders(x).Color = 0
                        Next
                    End With
                    Dim sum As Double = 0
                    For x = 0 To Campaign.Dayparts.Count - 1
                        .Cells(SheetHandler.CurrentRow(TmpString) + 1, 1 + x).Font.Bold = True
                        .Cells(SheetHandler.CurrentRow(TmpString) + 1, 1 + x).Value = Campaign.Dayparts(x).Name.ToString
                    Next
                    For x = 0 To Campaign.Dayparts.Count - 1
                        .Cells(SheetHandler.CurrentRow(TmpString) + 2, 1 + x).Numberformat = "0.0"
                        .Cells(SheetHandler.CurrentRow(TmpString) + 2, 1 + x).Value = SheetHandler.DaypartSum(TmpString, x)
                        sum += SheetHandler.DaypartSum(TmpString, x)
                    Next
                    For x = 0 To Campaign.Dayparts.Count - 1
                        .Cells(SheetHandler.CurrentRow(TmpString) + 3, 1 + x).Numberformat = "0.0%"
                        If SheetHandler.DaypartSum(TmpString, x) > 0 Then
                            .Cells(SheetHandler.CurrentRow(TmpString) + 3, 1 + x).Value = Math.Round((SheetHandler.DaypartSum(TmpString, x) / sum), 3)
                        Else
                            .Cells(SheetHandler.CurrentRow(TmpString) + 3, 1 + x).Value = 0
                        End If
                    Next
                End If

                'Set style on week rows
                If chkWeekSum.Checked Then
                    For Each TmpRow As Integer In WeekSumRows(TmpString)
                        With .Range("A" & TmpRow + Header.Rows.Count + 1 & ":" & Char.ConvertFromUtf32(64 + tvwChosen.Nodes.Count) & TmpRow + Header.Rows.Count + 1)
                            For x As Integer = 7 To 10
                                .Borders(x).LineStyle = 1
                                .Borders(x).Weight = 3
                                .Borders(x).Color = 0
                            Next
                            .Font.Bold = True
                            .HorizontalAlignment = -4108
                        End With
                    Next
                End If

                If SpotCountRow > 0 Then
                    .Cells(SpotCountRow, 2).select()
                    .Cells(SpotCountRow, 2).Value = SheetHandler.RowCount(TmpString) / 2
                End If

                .PageSetup.PrintTitleRows = "$" & Header.Rows.Count + 2 & ":$" & Header.Rows.Count + 2
                .Select()
                Excel.ActiveWindow.View = 2
                While .VPageBreaks.Count > 0
                    .VPageBreaks(1).DragOff(Direction:=-4161, RegionIndex:=1)
                End While
                Excel.ActiveWindow.View = 1
                Dim TmpLogo As Object = .InsertPicture(DataPath & "Logos\" & cmbLogo.Text)
                Dim Scal As Single = 180 / TmpLogo.width
                Scal = 1
                TmpLogo.ScaleWidth(Scal, 0, 0)
                TmpLogo.ScaleHeight(Scal, 0, 0)
                TmpLogo.Top = 20
                TmpLogo.Left = .Columns(tvwChosen.Nodes.Count + 1).Left - TmpLogo.Width - 10
                If SheetHandler.Spotlist(TmpString).Rows.Count = 0 Then
                    .Delete()
                End If

                'Unselect all cells

                '.cells(1, 1).select()
            End With
        Next

        Excel.ScreenUpdating = True
        Excel.DisplayAlerts = True
        Excel.Visible = True
        frmProgress.Visible = False
        frmProgress.Progress = 0

        Me.Cursor = Windows.Forms.Cursors.Default
        Me.DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Sub SaveDefaults()

        Dim i As Integer

        If chkCapital.Checked Then
            TrinitySettings.DefaultCapitals = 0
        ElseIf chkCapOnFirst.Checked Then
            TrinitySettings.DefaultCapitals = 1
        Else
            TrinitySettings.DefaultCapitals = 2
        End If
        If chkColorCodeFilm.Checked Then
            TrinitySettings.DefaultColorCoding = 0
        ElseIf chkColorCodeFilm.Checked Then
            TrinitySettings.DefaultColorCoding = 1
        Else
            TrinitySettings.DefaultColorCoding = 2
        End If
        TrinitySettings.DefaultConvertToRealTime = chkConvertTime.Checked
        'TrinitySettings.DefaultColorCodeChannels = chkColorCode.Checked
        'TrinitySettings.DefaultColorCodeFilms = chkColorCodeFilm.Checked
        TrinitySettings.DefaultSumWeeks = chkWeekSum.Checked
        TrinitySettings.DefaultSumDayparts = chkSumDaypart.Checked
        TrinitySettings.DefaultOneSheetPerChannel = chkSheetPerChannel.Checked
        TrinitySettings.DefaultLogo = cmbLogo.SelectedIndex
        TrinitySettings.DefaultColorScheme = cmbColorScheme.SelectedIndex
        For i = 0 To lstInfo.Items.Count - 1
            TrinitySettings.DefaultInfo(lstInfo.Items(i)) = lstInfo.GetItemChecked(i)
        Next
        TrinitySettings.PrintColumnCount = tvwChosen.Nodes.Count
        Dim TmpNode As System.Windows.Forms.TreeNode = tvwChosen.Nodes(1)
        While Not TmpNode.PrevNode Is Nothing
            TmpNode = TmpNode.PrevNode
        End While
        i = 1
        While Not TmpNode Is Nothing
            If TmpNode.Text = "CPP (" & Campaign.MainTarget.TargetNameNice & ")" Then
                TrinitySettings.PrintColumn(i) = "CPP Main"
            Else
                TrinitySettings.PrintColumn(i) = TmpNode.Text
            End If
            i = i + 1
            TmpNode = TmpNode.NextNode
        End While

    End Sub

    Private Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim TmpRow As Windows.Forms.DataGridViewRow
        Dim c As Integer
        Dim ID As String = ""

        Dim Est As Single
        Dim EstBT As Single

        Dim HeadlineRow As Integer
        Dim TmpNode As Windows.Forms.TreeNode
        Dim Excel As CultureSafeExcel.Application
        Dim WB As CultureSafeExcel.Workbook
        Dim LangIni As New Trinity.clsIni
        Dim PeriodStr As String
        Dim TmpLogo As CultureSafeExcel.Shape
        Dim Scal As Single
        Dim colorArray(9) As Long
        Dim coloring As New List(Of String)


        On Error Resume Next

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        If chkColorCode.Checked Or chkColorCodeFilm.Checked Then
            For i = 1 To 10
                colorArray(i - 1) = RGB(ConvertIntToARGB(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Diagram" & i)).R, ConvertIntToARGB(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Diagram" & i)).G, ConvertIntToARGB(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Diagram" & i)).B)
                ' colorArray(i - 1) = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Diagram" & i)
            Next
        End If

        Dim Days(0 To 6) As String

        'if the "set as default" is checked then we save the settings in TrinitySettings
        If chkDefault.Checked Then
            If chkCapital.Checked Then
                TrinitySettings.DefaultCapitals = 0
            ElseIf chkCapOnFirst.Checked Then
                TrinitySettings.DefaultCapitals = 1
            Else
                TrinitySettings.DefaultCapitals = 2
            End If
            If chkColorCodeFilm.Checked Then
                TrinitySettings.DefaultColorCoding = 0
            ElseIf chkColorCodeFilm.Checked Then
                TrinitySettings.DefaultColorCoding = 1
            Else
                TrinitySettings.DefaultColorCoding = 2
            End If
            TrinitySettings.DefaultConvertToRealTime = chkConvertTime.Checked
            'TrinitySettings.DefaultColorCodeChannels = chkColorCode.Checked
            'TrinitySettings.DefaultColorCodeFilms = chkColorCodeFilm.Checked
            TrinitySettings.DefaultSumWeeks = chkWeekSum.Checked
            TrinitySettings.DefaultSumDayparts = chkSumDaypart.Checked
            TrinitySettings.DefaultOneSheetPerChannel = chkSheetPerChannel.Checked
            TrinitySettings.DefaultLogo = cmbLogo.SelectedIndex
            TrinitySettings.DefaultColorScheme = cmbColorScheme.SelectedIndex
            For i = 0 To lstInfo.Items.Count - 1
                TrinitySettings.DefaultInfo(lstInfo.Items(i)) = lstInfo.GetItemChecked(i)
            Next
            TrinitySettings.PrintColumnCount = tvwChosen.Nodes.Count
            TmpNode = tvwChosen.Nodes(1)
            While Not TmpNode.PrevNode Is Nothing
                TmpNode = TmpNode.PrevNode
            End While
            i = 1
            While Not TmpNode Is Nothing
                If TmpNode.Text = "CPP (" & Campaign.MainTarget.TargetNameNice & ")" Then
                    TrinitySettings.PrintColumn(i) = "CPP Main"
                Else
                    TrinitySettings.PrintColumn(i) = TmpNode.Text
                End If
                i = i + 1
                TmpNode = TmpNode.NextNode
            End While
        End If

        LangIni.Create(cmdLanguage.Tag)

        For i = 0 To 6
            Days(i) = LangIni.Text("Weekdays", "Day" & i + 1)
        Next

        'creates all variables needed to setup the excel sheet
        Excel = New CultureSafeExcel.Application(False)

        Excel.ScreenUpdating = False
        Excel.DisplayAlerts = False
        Excel.visible = True
        WB = Excel.AddWorkbook

        'Add as many sheets as there are channels, in case the one sheet per channel option is checked
        'Also, name the sheets
        'The sheethandler contains all the channels of the campaign with BookIt = True
        Dim SheetHandler As New cSheetHandler(WB)
        SheetHandler.SheetPerChannel = chkSheetPerChannel.Checked
        If chkSheetPerChannel.Checked Then
            Dim SheetCount As Integer = WB.sheets.count
            For Each TmpString As String In SheetHandler.GetSheetList
                With WB.AddSheet(After:=WB.Sheets(WB.Sheets.Count))
                    .name = TmpString
                End With
            Next
            For i = 1 To SheetCount
                WB.Sheets(i).Delete()
            Next
        Else
            WB.Sheets(1).Name = cmbHeadline.Text
        End If

        'Start printing the spotlist!
        'This section is for the planned/confirmed spots
        If Me.Tag = "PLANNED" Then
            'setup the size and colors of the excel sheets
            For Each TmpString As String In SheetHandler.GetSheetList
                With WB.Sheets(TmpString)
                    .AllCells.Font.Name = TrinitySettings.SchemeFont(cmbColorScheme.SelectedIndex + 1)
                    If chkPreliminary.Checked Then
                        .Cells(1, 1).Value = LangIni.Text("Spotlist", "Preliminary") & " " & cmbHeadline.Text & " " & Campaign.Name
                    Else
                        .Cells(1, 1).Value = cmbHeadline.Text & " " & Campaign.Name
                    End If
                    With .Range("A1:" & Chr(64 + tvwChosen.Nodes.Count) & "1")
                        .Merge()
                        .HorizontalAlignment = -4131
                        .Font.Bold = True
                        .Font.Size = 14
                        .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                        .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                    End With
                    HeadlineRow = lstInfo.CheckedItems.Count + 4
                    SheetHandler.LastSumRow(TmpString) = HeadlineRow
                    With .Range("A" & HeadlineRow & ":" & Chr(64 + tvwChosen.Nodes.Count) & HeadlineRow)
                        .Font.Bold = True
                        .Font.Size = 10
                        .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                        .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                        For i = 7 To 10
                            With .Borders(i)
                                .LineStyle = 1
                                .Weight = -4138
                                .ColorIndex = -4105
                            End With
                        Next
                    End With
                End With
            Next

            'get the program row number
            Dim programRow As Integer
            For programRow = 0 To frmSpots.grdConfirmed.Columns.Count - 1
                If frmSpots.grdConfirmed.Columns(programRow).HeaderText = "Program" Then
                    Exit For
                End If
            Next

            frmProgress.pbProgress.Maximum = frmSpots.grdConfirmed.Rows.Count
            frmProgress.pbProgress.Value = 0
            frmProgress.lblStatus.Text = "Creating spotlist..."
            frmProgress.Show()

            'populate the newly created excel sheet with the information
            For Each TmpRow In frmSpots.grdConfirmed.Rows
                frmProgress.pbProgress.Value += 1
                Windows.Forms.Application.DoEvents()

                'only print the visible rows, meaning we dont print the filtered ones
                If TmpRow.Visible Then
                    'added to not count the RBS created spots
                    If TmpRow.Cells(programRow).Value.ToString.Length < 3 OrElse Not TmpRow.Cells(programRow).Value.ToString.Substring(0, 3) = "RBS" Then
                        TmpNode = tvwChosen.Nodes(1)
                        While Not TmpNode.PrevNode Is Nothing
                            TmpNode = TmpNode.PrevNode
                        End While
                        ID = TmpRow.Tag.Item("ID")

                        Dim TmpSheet As String = SheetHandler.GetSheetForChannel(Campaign.PlannedSpots(ID).Channel.ChannelName)
                        SheetHandler.RowCount(TmpSheet) += 1
                        Dim SaveNode As Windows.Forms.TreeNode = TmpNode
                        With WB.Sheets(TmpSheet)
                            'check if we change week
                            If SheetHandler.LastWeek(TmpSheet) > -1 AndAlso SheetHandler.LastWeek(TmpSheet) <> DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) Then
                                If chkWeekSum.Checked Then
                                    With .Range("A" & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1 & ":" & Chr(64 + tvwChosen.Nodes.Count) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1)
                                        .Font.Bold = True
                                        .Font.Size = 10
                                        For i = 7 To 10
                                            With .Borders(i)
                                                .LineStyle = 1
                                                .Weight = -4138
                                                .ColorIndex = -4105
                                            End With
                                        Next
                                    End With

                                    'sum up the entire week
                                    TmpNode = tvwChosen.Nodes(1)
                                    While Not TmpNode.PrevNode Is Nothing
                                        TmpNode = TmpNode.PrevNode
                                    End While
                                    c = 1
                                    While Not TmpNode Is Nothing
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4108
                                        Select Case TmpNode.Text
                                            Case "Gross Price"
                                                .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                                .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                            Case "Chan Est"
                                                .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                                .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                            Case "Net Price"
                                                .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                                .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                                'Case "Gross CPP"
                                                '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                                '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c) = "=SUM(" & Chr(64 + c) & Lastsum + 1 & ":" & Chr(64 + c) & HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 2 & ")"
                                                'Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                                                '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                                '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c) = "=SUM(" & Chr(64 + c) & Lastsum + 1 & ":" & Chr(64 + c) & HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 2 & ")"
                                                'Case "CPP (Chn Est)"
                                                '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                                '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c) = "=SUM(" & Chr(64 + c) & Lastsum + 1 & ":" & Chr(64 + c) & HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 2 & ")"
                                            Case "Est"
                                                .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                                .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                        End Select
                                        TmpNode = TmpNode.NextNode
                                        c = c + 1
                                    End While

                                    'done suming up the week
                                    SheetHandler.LastSumRow(TmpSheet) = SheetHandler.CurrentRow(TmpSheet) + HeadlineRow
                                    SheetHandler.CurrentRow(TmpSheet) += 1

                                End If
                                TmpNode = SaveNode
                                SheetHandler.CurrentRow(TmpSheet) += 1
                            End If
                            SheetHandler.LastWeek(TmpSheet) = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                            If chkColorCode.Checked Then
                                If coloring.IndexOf(Campaign.PlannedSpots(ID).Channel.ChannelName.ToUpper) > -1 Then
                                    .Rows(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1).Font.Color = colorArray(coloring.IndexOf(Campaign.PlannedSpots(ID).Channel.ChannelName.ToUpper))
                                Else
                                    coloring.Add(Campaign.PlannedSpots(ID).Channel.ChannelName.ToUpper)
                                    .Rows(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1).Font.Color = colorArray(coloring.IndexOf(Campaign.PlannedSpots(ID).Channel.ChannelName.ToUpper))
                                End If
                                '.Rows(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Color" & Campaign.PlannedSpots(ID).Channel.ListNumber - 2)
                            End If
                            If chkColorCodeFilm.Checked Then
                                If coloring.IndexOf(Campaign.PlannedSpots(ID).Film.Name) <> -1 Then
                                    .Rows(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1).Font.Color = colorArray(coloring.IndexOf(Campaign.PlannedSpots(ID).Film.Name))
                                Else
                                    coloring.Add(Campaign.PlannedSpots(ID).Film.Name)
                                    .Rows(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1).Font.Color = colorArray(coloring.IndexOf(Campaign.PlannedSpots(ID).Film.Name))
                                End If
                                '.Rows(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1).Font.Color = colorArray(Campaign.PlannedSpots(ID).Channel.ListNumber - 2).ToArgb
                            End If
                            c = 1
                            While Not TmpNode Is Nothing
                                If SheetHandler.CurrentRow(TmpSheet) = 2 Then
                                    If LangIni.Text("Spotlist", TmpNode.Text) <> "" Then
                                        .Cells(HeadlineRow, c).Value = LangIni.Text("Spotlist", TmpNode.Text)
                                    Else
                                        .Cells(HeadlineRow, c).Value = TmpNode.Text
                                    End If
                                    .Cells(HeadlineRow, c).HorizontalAlignment = -4108
                                End If
                                .Columns(c).AutoFit()
                                Est = Campaign.PlannedSpots(ID).MyRating
                                EstBT = Campaign.PlannedSpots(ID).MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
                                .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4108
                                Select Case TmpNode.Text
                                    Case "ID" : .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).ID
                                    Case "TimeStamp" : .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Trinity.Helper.DateTimeSerial(Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), Campaign.PlannedSpots(ID).MaM)
                                    Case "Date"
                                        If chkConvertTime.Checked Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Date.FromOADate(Trinity.Helper.DateTimeSerial(Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), Campaign.PlannedSpots(ID).MaM)), "yyyy-MM-dd")
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), "yyyy-MM-dd")
                                        End If
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4131
                                        .Cells(HeadlineRow, c).HorizontalAlignment = -4131
                                    Case "Added value"
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).AddedValue.ToString()
                                    Case "Week" : .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                                    Case "Bookingtype"
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).Bookingtype.Name
                                    Case "Duration"
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).SpotLength
                                    Case "Weekday"
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Days(Weekday(Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), vbMonday) - 1)
                                        If chkConvertTime.Checked Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Days(Weekday(Date.FromOADate(Trinity.Helper.DateTimeSerial(Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), Campaign.PlannedSpots(ID).MaM)), FirstDayOfWeek.Monday) - 1)
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Days(Weekday(Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), FirstDayOfWeek.Monday) - 1)
                                        End If
                                    Case "Time"
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "@"
                                        If chkConvertTime.Checked Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Date.FromOADate(Trinity.Helper.DateTimeSerial(Date.FromOADate(Campaign.PlannedSpots(ID).AirDate), Campaign.PlannedSpots(ID).MaM)), "HH:mm")
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Trinity.Helper.Mam2Tid(Campaign.PlannedSpots(ID).MaM)
                                        End If
                                    Case "Channel"
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).Channel.ChannelName
                                    Case "Program"
                                        If chkCapital.Checked Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = UCase(Campaign.PlannedSpots(ID).Programme)
                                        ElseIf chkCapOnFirst.Checked Then
                                            If Not Campaign.PlannedSpots(ID).Programme = "" Then
                                                .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = UCase(Campaign.PlannedSpots(ID).Programme.Substring(0, 1)) & LCase(Campaign.PlannedSpots(ID).Programme.Substring(1))
                                            End If
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).Programme

                                        End If
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4131
                                        .Cells(HeadlineRow, c).HorizontalAlignment = -4131
                                    Case "Gross Price"
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).PriceGross
                                    Case "Chan Est"
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
                                    Case "Net Price"
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).PriceNet
                                    Case "Remarks"
                                        If Campaign.PlannedSpots(ID).Remark = "L" Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "Local"
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4131
                                            .Cells(HeadlineRow, c).HorizontalAlignment = -4131
                                        End If
                                    Case "Gross CPP"
                                        If Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.PlannedSpots(ID).PriceNet / Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0 kr")
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                        End If
                                    Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                                        If Est > 0 Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.PlannedSpots(ID).PriceNet / Est, "##,##0 kr")
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                        End If
                                    Case "CPP (Chn Est)"
                                        If Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.PlannedSpots(ID).PriceNet / Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0 kr")
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                        End If
                                    Case "Est"
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).MyRating
                                    Case "Filmcode" : .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).Film.Filmcode
                                    Case "Film" : .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).Film.Name
                                    Case "Film dscr" : .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).Film.Description
                                        'Hannes added the next two cases
                                    Case "Prog Before"
                                        If chkCapital.Checked Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = UCase(Campaign.PlannedSpots(ID).ProgBefore)
                                        ElseIf chkCapOnFirst.Checked Then
                                            If Not Campaign.PlannedSpots(ID).ProgBefore = "" Then
                                                .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = UCase(Campaign.PlannedSpots(ID).ProgBefore.Substring(0, 1)) & LCase(Campaign.PlannedSpots(ID).ProgBefore.Substring(1))
                                            End If
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).ProgBefore
                                        End If
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4131
                                        .Cells(HeadlineRow, c).HorizontalAlignment = -4131
                                    Case "Prog After"
                                        If chkCapital.Checked Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = UCase(Campaign.PlannedSpots(ID).ProgAfter)
                                        ElseIf chkCapOnFirst.Checked Then
                                            If Not Campaign.PlannedSpots(ID).ProgAfter = "" Then
                                                .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = UCase(Campaign.PlannedSpots(ID).ProgAfter.Substring(0, 1)) & LCase(Campaign.PlannedSpots(ID).ProgAfter.Substring(1))
                                            End If
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).ProgAfter
                                        End If
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4131
                                        .Cells(HeadlineRow, c).HorizontalAlignment = -4131
                                End Select
                                TmpNode = TmpNode.NextNode
                                c = c + 1
                            End While
                            SheetHandler.CurrentRow(TmpSheet) = SheetHandler.CurrentRow(TmpSheet) + 1
                        End With
                        SheetHandler.DaypartSum(TmpSheet, Campaign.Dayparts.GetDaypartIndexForMam(Campaign.PlannedSpots(ID).MaM)) += Campaign.PlannedSpots(ID).MyRating
                    End If 'end if
                End If
            Next
            For Each TmpSheet As String In SheetHandler.GetSheetList
                With WB.Sheets(TmpSheet)
                    If chkWeekSum.Checked Then
                        'sum the last week
                        With .Range("A" & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1 & ":" & Chr(64 + tvwChosen.Nodes.Count) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1)
                            .Font.Bold = True
                            .Font.Size = 10
                            For i = 7 To 10
                                With .Borders(i)
                                    .LineStyle = 1
                                    .Weight = -4138
                                    .ColorIndex = -4105
                                End With
                            Next
                        End With

                        'sum up the entire week
                        TmpNode = tvwChosen.Nodes(1)
                        While Not TmpNode.PrevNode Is Nothing
                            TmpNode = TmpNode.PrevNode
                        End While
                        c = 1
                        While Not TmpNode Is Nothing
                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4108
                            Select Case TmpNode.Text
                                Case "Gross Price"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                Case "Chan Est"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                Case "Net Price"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                    'Case "Gross CPP"
                                    '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c) = "=SUM(" & Chr(64 + c) & Lastsum + 1 & ":" & Chr(64 + c) & HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 2 & ")"
                                    'Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                                    '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c) = "=SUM(" & Chr(64 + c) & Lastsum + 1 & ":" & Chr(64 + c) & HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 2 & ")"
                                Case "Est"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                            End Select
                            TmpNode = TmpNode.NextNode
                            c = c + 1
                        End While
                        SheetHandler.CurrentRow(TmpSheet) += 1
                        'done sum week
                    End If

                    With .Range("A" & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1 & ":" & Chr(64 + tvwChosen.Nodes.Count) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1)
                        .Font.Bold = True
                        .Font.Size = 10
                        .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                        .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                        For i = 7 To 10
                            With .Borders(i)
                                .LineStyle = 1
                                .Weight = -4138
                                .ColorIndex = -4105
                            End With
                        Next
                    End With


                    'sum up the entire page
                    TmpNode = tvwChosen.Nodes(1)
                    While Not TmpNode.PrevNode Is Nothing
                        TmpNode = TmpNode.PrevNode
                    End While
                    c = 1
                    While Not TmpNode Is Nothing
                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4108
                        If chkWeekSum.Checked Then
                            Select Case TmpNode.Text
                                Case "Gross Price"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")/2"
                                Case "Chan Est"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")/2"
                                Case "Net Price"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")/2"
                                Case "Gross CPP"
                                    If Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.PlannedSpots(ID).PriceNet / Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0 kr")
                                    Else
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                    End If
                                Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                                    If Est > 0 Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.PlannedSpots(ID).PriceNet / Est, "##,##0 kr")
                                    Else
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                    End If
                                Case "CPP (Chn Est)"
                                    If Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.PlannedSpots(ID).PriceNet / Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0 kr")
                                    Else
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                    End If
                                Case "Est"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")/2"
                            End Select
                        Else
                            Select Case TmpNode.Text
                                Case "Gross Price"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                Case "Chan Est"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                Case "Net Price"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                Case "Gross CPP"
                                    If Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.PlannedSpots(ID).PriceNet / Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0 kr")
                                    Else
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                    End If
                                Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                                    If Est > 0 Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.PlannedSpots(ID).PriceNet / Est, "##,##0 kr")
                                    Else
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                    End If
                                Case "CPP (Chn Est)"
                                    If Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.PlannedSpots(ID).PriceNet / Campaign.PlannedSpots(ID).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0 kr")
                                    Else
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                    End If
                                Case "Est"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                            End Select
                        End If
                        TmpNode = TmpNode.NextNode
                        c = c + 1
                    End While

                    If DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) Then
                        PeriodStr = LangIni.Text("Spotlist", "Week") & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & ", " & Year(Date.FromOADate(Campaign.StartDate))
                    Else
                        PeriodStr = LangIni.Text("Spotlist", "Week") & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & "-" & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & ", " & Year(Date.FromOADate(Campaign.StartDate))
                    End If
                    If chkSumDaypart.Checked Then
                        Dim x As Integer
                        'write the headline
                        SheetHandler.CurrentRow(TmpSheet) = SheetHandler.CurrentRow(TmpSheet) + HeadlineRow + 2
                        With .Range("A" & SheetHandler.CurrentRow(TmpSheet) & ":" & Char.ConvertFromUtf32(65 + Campaign.Dayparts.Count - 1) & SheetHandler.CurrentRow(TmpSheet) + 1)
                            .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                            .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                            For x = 7 To Campaign.Dayparts.Count + 7
                                .Borders(x).LineStyle = 1
                                .Borders(x).Weight = -4138
                                .Borders(x).Color = 0
                            Next
                            .Cells(1, 1).Value = "Daypart summary"
                            .Cells(1, 1).Font.Bold = True
                        End With
                        'write the border
                        With .Range("A" & SheetHandler.CurrentRow(TmpSheet) + 2 & ":" & Char.ConvertFromUtf32(65 + Campaign.Dayparts.Count - 1) & SheetHandler.CurrentRow(TmpSheet) + 1 + Campaign.Dayparts.Count - 1)
                            For x = 7 To Campaign.Dayparts.Count + 7
                                .Borders(x).LineStyle = 1
                                .Borders(x).Weight = 2
                                .Borders(x).Color = 0
                            Next
                        End With
                        Dim sum As Double
                        For x = 0 To Campaign.Dayparts.Count - 1
                            .Cells(SheetHandler.CurrentRow(TmpSheet) + 1, 1 + x).Font.Bold = True
                            .Cells(SheetHandler.CurrentRow(TmpSheet) + 1, 1 + x).Value = Campaign.Dayparts(x).Name.ToString
                        Next
                        For x = 0 To Campaign.Dayparts.Count - 1
                            .Cells(SheetHandler.CurrentRow(TmpSheet) + 2, 1 + x).Numberformat = "0.0"
                            .Cells(SheetHandler.CurrentRow(TmpSheet) + 2, 1 + x).Value = SheetHandler.DaypartSum(TmpSheet, x)
                            sum += SheetHandler.DaypartSum(TmpSheet, x)
                        Next
                        For x = 0 To Campaign.Dayparts.Count - 1
                            .Cells(SheetHandler.CurrentRow(TmpSheet) + 3, 1 + x).Numberformat = "0.0%"
                            If SheetHandler.DaypartSum(TmpSheet, x) > 0 Then
                                .Cells(SheetHandler.CurrentRow(TmpSheet) + 3, 1 + x).Value = Math.Round((SheetHandler.DaypartSum(TmpSheet, x) / sum), 3)
                            Else
                                .Cells(SheetHandler.CurrentRow(TmpSheet) + 3, 1 + x).Value = 0
                            End If
                        Next
                    End If
                    SheetHandler.CurrentRow(TmpSheet) = 2
                    For i = 0 To lstInfo.Items.Count - 1
                        If lstInfo.GetItemChecked(i) Then
                            SheetHandler.CurrentRow(TmpSheet) = SheetHandler.CurrentRow(TmpSheet) + 1
                            .Cells(SheetHandler.CurrentRow(TmpSheet), 1).Value = LangIni.Text("Spotlist", lstInfo.Items(i))
                            .Cells(SheetHandler.CurrentRow(TmpSheet), 1).Font.Bold = True
                            Select Case lstInfo.Items(i)
                                Case "Client" : .Cells(SheetHandler.CurrentRow(TmpSheet), 2).Value = Campaign.Client
                                Case "Product" : .Cells(SheetHandler.CurrentRow(TmpSheet), 2).Value = Campaign.Product
                                Case "Buyer" : .Cells(SheetHandler.CurrentRow(TmpSheet), 2).Value = TrinitySettings.UserName
                                Case "Period" : .Cells(SheetHandler.CurrentRow(TmpSheet), 2).Value = PeriodStr
                                Case "E-mail" : .Cells(SheetHandler.CurrentRow(TmpSheet), 2).Value = TrinitySettings.UserEmail
                                Case "Phone Nr" : .Cells(SheetHandler.CurrentRow(TmpSheet), 2).Value = TrinitySettings.UserPhoneNr
                                Case "Spot count" : .Cells(SheetHandler.CurrentRow(TmpSheet), 2).Value = SheetHandler.RowCount(TmpSheet)
                            End Select
                            .Cells(SheetHandler.CurrentRow(TmpSheet), 2).HorizontalAlignment = -4131
                        End If
                    Next
                    .PageSetup.PrintTitleRows = "$" & HeadlineRow & ":$" & HeadlineRow
                    .Select()
                    Excel.ActiveWindow.View = 2
                    While .VPageBreaks.Count > 0
                        .VPageBreaks(1).DragOff(Direction:=-4161, RegionIndex:=1)
                    End While
                    Excel.ActiveWindow.View = 1
                    TmpLogo = .InsertPicture(DataPath & "Logos\" & cmbLogo.Text)
                    Scal = 180 / TmpLogo.width
                    Scal = 1
                    TmpLogo.ScaleWidth(Scal, 0, 0)
                    TmpLogo.ScaleHeight(Scal, 0, 0)
                    TmpLogo.Top = 20
                    TmpLogo.Left = .Columns(tvwChosen.Nodes.Count + 1).Left - TmpLogo.Width - 10
                    If SheetHandler.RowCount(TmpSheet) = 0 Then
                        .delete()
                    End If
                End With
            Next
            'And way down here is what we do if the spotlist we are printing isnt the confirmed/planned one
        Else
            For Each TmpString As String In SheetHandler.GetSheetList
                'This only happens once if one sheet per channel isn't checked
                With WB.Sheets(TmpString)
                    If chkPreliminary.Checked Then
                        .Cells(1, 1).Value = LangIni.Text("Spotlist", "Preliminary") & " " & cmbHeadline.Text & " " & Campaign.Name
                    Else
                        .Cells(1, 1).Value = cmbHeadline.Text & " " & Campaign.Name
                    End If
                    With .Range("A1:" & Chr(64 + tvwChosen.Nodes.Count) & "1")
                        .Merge()
                        .HorizontalAlignment = -4131
                        .Font.Bold = True
                        .Font.Size = 14
                        .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                        .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                    End With
                    HeadlineRow = lstInfo.CheckedItems.Count + 4
                    SheetHandler.LastSumRow(TmpString) = HeadlineRow
                    With .Range("A" & HeadlineRow & ":" & Chr(64 + tvwChosen.Nodes.Count) & HeadlineRow)
                        .Font.Bold = True
                        .Font.Size = 10
                        .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                        .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                        For i = 7 To 10
                            With .Borders(i)
                                .LineStyle = 1
                                .Weight = -4138
                                .ColorIndex = -4105
                            End With
                        Next
                    End With
                End With
            Next

            frmProgress.pbProgress.Value = 0
            frmProgress.pbProgress.Maximum = frmSpots.grdActual.Rows.Count
            frmProgress.lblStatus.Text = "Creating spotlist..."
            frmProgress.Show()
            For Each TmpRow In frmSpots.grdActual.Rows
                frmProgress.pbProgress.Value += 1
                ' Windows.Forms.Application.DoEvents()
                'only print the visible rows, meaning we dont print the filtered ones
                If TmpRow.Visible AndAlso TmpRow.Tag IsNot Nothing Then

                    TmpNode = tvwChosen.Nodes(1)
                    While Not TmpNode.PrevNode Is Nothing
                        TmpNode = TmpNode.PrevNode
                    End While
                    ID = TmpRow.Tag.Item("ID")

                    Dim TmpSheet As String = SheetHandler.GetSheetForChannel(Campaign.ActualSpots(ID).Channel.ChannelName)
                    SheetHandler.RowCount(TmpSheet) += 1
                    With WB.Sheets(TmpSheet)
                        'check if we change week
                        If SheetHandler.LastWeek(TmpSheet) > -1 AndAlso SheetHandler.LastWeek(TmpSheet) <> DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.ActualSpots(ID).AirDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) Then
                            If chkWeekSum.Checked Then
                                With .Range("A" & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1 & ":" & Chr(64 + tvwChosen.Nodes.Count) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1)
                                    .Font.Bold = True
                                    .Font.Size = 10
                                    For i = 7 To 10
                                        With .Borders(i)
                                            .LineStyle = 1
                                            .Weight = -4138
                                            .ColorIndex = -4105
                                        End With
                                    Next
                                End With
                                'sum up the entire week
                                TmpNode = tvwChosen.Nodes(1)
                                While Not TmpNode.PrevNode Is Nothing
                                    TmpNode = TmpNode.PrevNode
                                End While
                                c = 1
                                While Not TmpNode Is Nothing
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4108
                                    Select Case TmpNode.Text
                                        Case "Gross Price"
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Numberformat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                        Case "Chan Est"
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Numberformat = "0.0"
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                        Case "Net Price"
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Numberformat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                            'Case "Gross CPP"
                                            '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                            '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c) = "=SUM(" & Chr(64 + c) & Lastsum + 1 & ":" & Chr(64 + c) & HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 2 & ")"
                                            'Case "CPP (Chn Est)"
                                            '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                            '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c) = "=SUM(" & Chr(64 + c) & Lastsum + 1 & ":" & Chr(64 + c) & HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 2 & ")"
                                        Case "Actual"
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Numberformat = "0.0"
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                    End Select
                                    TmpNode = TmpNode.NextNode
                                    c = c + 1
                                End While

                                'done suming up the week
                                SheetHandler.LastSumRow(TmpSheet) = SheetHandler.CurrentRow(TmpSheet) + HeadlineRow
                            End If
                            SheetHandler.CurrentRow(TmpSheet) += 1
                        End If
                        'ID = frmSpots.grdActual.Rows(SheetHandler.CurrentRow(TmpSheet)  - 2).Tag
                        SheetHandler.LastWeek(TmpSheet) = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.ActualSpots(ID).AirDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                        If chkColorCode.Checked Then
                            If coloring.IndexOf(Campaign.ActualSpots(ID).Channel.ChannelName.ToUpper) > -1 Then
                                .Rows(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1).Font.Color = colorArray(coloring.IndexOf(Campaign.ActualSpots(ID).Channel.ChannelName.ToUpper))
                            Else
                                coloring.Add(Campaign.ActualSpots(ID).Channel.ChannelName.ToUpper)
                                .Rows(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1).Font.Color = colorArray(coloring.IndexOf(Campaign.ActualSpots(ID).Channel.ChannelName.ToUpper))
                            End If
                            '.Rows(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Color" & Campaign.actualspots(ID).Channel.ListNumber - 2)
                        End If
                        If chkColorCodeFilm.Checked Then
                            If coloring.IndexOf(Campaign.ActualSpots(ID).Week.Films(Campaign.ActualSpots(ID).Filmcode).Name) > -1 Then
                                .Rows(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1).Font.Color = colorArray(coloring.IndexOf(Campaign.ActualSpots(ID).Week.Films(Campaign.ActualSpots(ID).Filmcode).Name))
                            Else
                                coloring.Add(Campaign.ActualSpots(ID).Week.Films(Campaign.ActualSpots(ID).Filmcode).Name)
                                .Rows(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1).Font.Color = colorArray(coloring.IndexOf(Campaign.ActualSpots(ID).Week.Films(Campaign.ActualSpots(ID).Filmcode).Name))
                            End If
                            '.Rows(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1).Font.Color = colorArray(Campaign.actualspots(ID).Channel.ListNumber - 2).ToArgb
                        End If
                        If SheetHandler.CurrentRow(TmpSheet) = 307 Then
                            c = 1
                        End If

                        If chkColorCode.Checked Then
                            .Rows(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Color" & Campaign.ActualSpots(ID).Channel.ListNumber - 2)
                        End If
                        c = 1
                        While Not TmpNode Is Nothing
                            If SheetHandler.CurrentRow(TmpSheet) = 2 Then
                                If LangIni.Text("Spotlist", TmpNode.Text) <> "" Then
                                    .Cells(HeadlineRow, c).Value = LangIni.Text("Spotlist", TmpNode.Text)
                                Else
                                    .Cells(HeadlineRow, c).Value = TmpNode.Text
                                End If
                                .Cells(HeadlineRow, c).HorizontalAlignment = -4108
                            End If
                            .Columns(c).AutoFit()
                            If Campaign.ActualSpots(ID).MatchedSpot Is Nothing Then
                                Est = 0
                                EstBT = 0
                            Else
                                Est = Campaign.ActualSpots(ID).MatchedSpot.MyRating
                                EstBT = Campaign.ActualSpots(ID).MatchedSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
                            End If
                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4108
                            Select Case TmpNode.Text
                                Case "ID" : .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.ActualSpots(ID).ID
                                Case "TimeStamp" : .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Trinity.Helper.DateTimeSerial(Date.FromOADate(Campaign.ActualSpots(ID).AirDate), Campaign.ActualSpots(ID).MaM)
                                Case "Date"
                                    If chkConvertTime.Checked Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Date.FromOADate(Trinity.Helper.DateTimeSerial(Date.FromOADate(Campaign.ActualSpots(ID).AirDate), Campaign.ActualSpots(ID).MaM)), "yyyy-MM-dd")
                                    Else
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Date.FromOADate(Campaign.ActualSpots(ID).AirDate), "yyyy-MM-dd")
                                    End If
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4131
                                    .Cells(HeadlineRow, c).HorizontalAlignment = -4131
                                Case "Week" : .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.ActualSpots(ID).AirDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                                Case "Weekday" : .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Days(Weekday(Date.FromOADate(Campaign.ActualSpots(ID).AirDate), vbMonday) - 1)
                                Case "Time"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Numberformat = "@"
                                    If chkConvertTime.Checked Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Date.FromOADate(Trinity.Helper.DateTimeSerial(Date.FromOADate(Campaign.ActualSpots(ID).AirDate), Campaign.ActualSpots(ID).MaM)), "HH:mm")
                                    Else
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Trinity.Helper.Mam2Tid(Campaign.ActualSpots(ID).MaM)
                                    End If
                                Case "Channel"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.ActualSpots(ID).Channel.ChannelName
                                Case "Program"

                                    If chkCapital.Checked Then

                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = UCase(Campaign.ActualSpots(ID).Programme)
                                    ElseIf chkCapOnFirst.Checked Then
                                        If Campaign.ActualSpots(ID).Programme <> "" Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = UCase(Campaign.ActualSpots(ID).Programme.Substring(0, 1)) & LCase(Campaign.ActualSpots(ID).Programme.Substring(1))
                                        End If
                                    Else
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.ActualSpots(ID).Programme
                                    End If
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4131
                                    .Cells(HeadlineRow, c).HorizontalAlignment = -4131

                                Case "Gross Price"

                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Numberformat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    If Campaign.ActualSpots(ID).MatchedSpot Is Nothing Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = ""
                                    Else
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.ActualSpots(ID).MatchedSpot.PriceGross
                                    End If
                                Case "Chan Est"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Numberformat = "0.0"
                                    If Campaign.ActualSpots(ID).MatchedSpot Is Nothing Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = ""
                                    Else
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.ActualSpots(ID).MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
                                    End If
                                Case "Actual"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Numberformat = "0.0"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.ActualSpots(ID).Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget)
                                Case "Net Price"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Numberformat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    If Campaign.ActualSpots(ID).MatchedSpot Is Nothing Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = ""
                                    Else
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.ActualSpots(ID).MatchedSpot.PriceNet
                                    End If
                                Case "Remarks"
                                    If Campaign.ActualSpots(ID).MatchedSpot Is Nothing Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = ""
                                    Else
                                        If Campaign.ActualSpots(ID).MatchedSpot.Remark = "L" Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "Local"
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4131
                                            .Cells(HeadlineRow, c).HorizontalAlignment = -4131
                                        End If
                                    End If
                                Case "Gross CPP"
                                    If Campaign.ActualSpots(ID).MatchedSpot Is Nothing Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = ""
                                    Else
                                        If Campaign.ActualSpots(ID).MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.ActualSpots(ID).MatchedSpot.PriceNet / Campaign.ActualSpots(ID).MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0 kr")
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                        End If
                                    End If
                                Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                                    If Campaign.ActualSpots(ID).MatchedSpot Is Nothing Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = ""
                                    Else
                                        If Campaign.ActualSpots(ID).MatchedSpot.PriceNet / Campaign.ActualSpots(ID).Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget) > 0 Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.ActualSpots(ID).MatchedSpot.PriceNet / Campaign.ActualSpots(ID).Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), "##,##0 kr")
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                        End If
                                    End If
                                Case "CPP (Chn Est)"
                                    If Campaign.ActualSpots(ID).MatchedSpot Is Nothing Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = ""
                                    Else
                                        If Campaign.ActualSpots(ID).MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.ActualSpots(ID).MatchedSpot.PriceNet / Campaign.ActualSpots(ID).MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0 kr")
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                        End If
                                    End If
                                Case "Actual"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Numberformat = "0.0"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.ActualSpots(ID).Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget)
                                Case "Filmcode" : .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.ActualSpots(ID).Filmcode
                                Case "Film" : .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.ActualSpots(ID).Week.Films(Campaign.ActualSpots(ID).Filmcode).Name
                                Case "Film dscr" : .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.ActualSpots(ID).Week.Films(Campaign.ActualSpots(ID).Filmcode).Description
                                    'Hannes added from here ....
                                Case "Prog Before"
                                    If chkCapital.Checked Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = UCase(Campaign.PlannedSpots(ID).ProgBefore)
                                    ElseIf chkCapOnFirst.Checked Then
                                        If Not Campaign.PlannedSpots(ID).ProgBefore = "" Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = UCase(Campaign.PlannedSpots(ID).ProgBefore.Substring(0, 1)) & LCase(Campaign.PlannedSpots(ID).ProgBefore.Substring(1))
                                        End If
                                    Else
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).ProgBefore
                                    End If
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4131
                                    .Cells(HeadlineRow, c).HorizontalAlignment = -4131
                                Case "Prog After"
                                    If chkCapital.Checked Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = UCase(Campaign.PlannedSpots(ID).ProgAfter)
                                    ElseIf chkCapOnFirst.Checked Then
                                        If Not Campaign.PlannedSpots(ID).ProgAfter = "" Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = UCase(Campaign.PlannedSpots(ID).ProgAfter.Substring(0, 1)) & LCase(Campaign.PlannedSpots(ID).ProgAfter.Substring(1))
                                        End If
                                    Else
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.PlannedSpots(ID).ProgAfter
                                    End If
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4131
                                    .Cells(HeadlineRow, c).HorizontalAlignment = -4131
                                    ' ... to here
                                Case "PIB"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Campaign.ActualSpots(ID).PosInBreak & " / " & Campaign.ActualSpots(ID).SpotsInBreak
                            End Select
                            TmpNode = TmpNode.NextNode
                            c = c + 1
                        End While
                        SheetHandler.CurrentRow(TmpSheet) = SheetHandler.CurrentRow(TmpSheet) + 1
                        SheetHandler.DaypartSum(TmpSheet, Campaign.Dayparts.GetDaypartIndexForMam(Campaign.ActualSpots(ID).MaM)) += Campaign.ActualSpots(ID).Rating
                    End With
                End If
            Next

            For Each TmpSheet As String In SheetHandler.GetSheetList
                With WB.Sheets(TmpSheet)
                    If chkWeekSum.Checked Then
                        'sum the last week
                        With .Range("A" & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1 & ":" & Chr(64 + tvwChosen.Nodes.Count) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1)
                            .Font.Bold = True
                            .Font.Size = 10
                            For i = 7 To 10
                                With .Borders(i)
                                    .LineStyle = 1
                                    .Weight = -4138
                                    .ColorIndex = -4105
                                End With
                            Next
                        End With

                        'sum up the entire week
                        TmpNode = tvwChosen.Nodes(1)
                        While Not TmpNode.PrevNode Is Nothing
                            TmpNode = TmpNode.PrevNode
                        End While
                        c = 1
                        While Not TmpNode Is Nothing
                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4108
                            Select Case TmpNode.Text
                                Case "Gross Price"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                Case "Chan Est"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                Case "Net Price"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                    'Case "Gross CPP"
                                    '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c) = "=SUM(" & Chr(64 + c) & Lastsum + 1 & ":" & Chr(64 + c) & HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 2 & ")"
                                    'Case "CPP (Chn Est)"
                                    '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    '    .Cells(HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 1, c) = "=SUM(" & Chr(64 + c) & Lastsum + 1 & ":" & Chr(64 + c) & HeadlineRow +  SheetHandler.CurrentRow(TmpSheet)  - 2 & ")"
                                Case "Actual"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & SheetHandler.LastSumRow(TmpSheet) + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                            End Select
                            TmpNode = TmpNode.NextNode
                            c = c + 1
                        End While
                        SheetHandler.CurrentRow(TmpSheet) += 1
                        'done sum week
                    End If

                    With .Range("A" & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1 & ":" & Chr(64 + tvwChosen.Nodes.Count) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1)
                        .Font.Bold = True
                        .Font.Size = 10
                        .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                        .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                        For i = 7 To 10
                            With .Borders(i)
                                .LineStyle = 1
                                .Weight = -4138
                                .ColorIndex = -4105
                            End With
                        Next
                    End With
                    TmpNode = tvwChosen.Nodes(1)
                    While Not TmpNode.PrevNode Is Nothing
                        TmpNode = TmpNode.PrevNode
                    End While
                    c = 1
                    While Not TmpNode Is Nothing
                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).HorizontalAlignment = -4108
                        If chkWeekSum.Checked Then
                            Select Case TmpNode.Text
                                Case "Gross Price"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")/2"
                                Case "Chan Est"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")/2"
                                Case "Net Price"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")/2"
                                Case "Gross CPP"
                                    If Campaign.ActualSpots(ID).MatchedSpot Is Nothing Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = ""
                                    Else
                                        If Campaign.ActualSpots(ID).MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.ActualSpots(ID).MatchedSpot.PriceNet / Campaign.ActualSpots(ID).MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0 kr")
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                        End If
                                    End If
                                Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                                    If Campaign.ActualSpots(ID).MatchedSpot Is Nothing Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = ""
                                    Else
                                        If Campaign.ActualSpots(ID).Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget) > 0 Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.ActualSpots(ID).MatchedSpot.PriceNet / Campaign.ActualSpots(ID).Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), "##,##0 kr")
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                        End If
                                    End If
                                Case "CPP (Chn Est)"
                                    If Campaign.ActualSpots(ID).MatchedSpot Is Nothing Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = ""
                                    Else
                                        If Campaign.ActualSpots(ID).MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.ActualSpots(ID).MatchedSpot.PriceNet / Campaign.ActualSpots(ID).MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0 kr")
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                        End If
                                    End If
                                Case "Actual"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")/2"
                            End Select
                        Else
                            Select Case TmpNode.Text
                                Case "Gross Price"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                Case "Chan Est"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                Case "Net Price"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "_-* # ##0 kr_-;-* # ##0 kr_-;_-* ""-""?? kr_-;_-@_-"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                                Case "Gross CPP"
                                    If Campaign.ActualSpots(ID).MatchedSpot Is Nothing Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = ""
                                    Else
                                        If Campaign.ActualSpots(ID).MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.ActualSpots(ID).MatchedSpot.PriceNet / Campaign.ActualSpots(ID).MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0 kr")
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                        End If
                                    End If
                                Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                                    If Campaign.ActualSpots(ID).MatchedSpot Is Nothing Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = ""
                                    Else
                                        If Campaign.ActualSpots(ID).Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget) > 0 Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.ActualSpots(ID).MatchedSpot.PriceNet / Campaign.ActualSpots(ID).Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), "##,##0 kr")
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                        End If
                                    End If
                                Case "CPP (Chn Est)"
                                    If Campaign.ActualSpots(ID).MatchedSpot Is Nothing Then
                                        .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = ""
                                    Else
                                        If Campaign.ActualSpots(ID).MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = Format(Campaign.ActualSpots(ID).MatchedSpot.PriceNet / Campaign.ActualSpots(ID).MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0 kr")
                                        Else
                                            .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Value = "0 kr"
                                        End If
                                    End If
                                Case "Actual"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).NumberFormat = "0.0"
                                    .Cells(HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 1, c).Formula = "=SUM(" & Chr(64 + c) & HeadlineRow + 1 & ":" & Chr(64 + c) & HeadlineRow + SheetHandler.CurrentRow(TmpSheet) - 2 & ")"
                            End Select
                        End If

                        TmpNode = TmpNode.NextNode
                        c = c + 1
                    End While

                    If DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) Then
                        PeriodStr = LangIni.Text("Spotlist", "Week") & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & ", " & Year(Date.FromOADate(Campaign.StartDate))
                    Else
                        PeriodStr = LangIni.Text("Spotlist", "Week") & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & "-" & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & ", " & Year(Date.FromOADate(Campaign.StartDate))
                    End If
                    SheetHandler.CurrentRow(TmpSheet) = 2
                    For i = 0 To lstInfo.Items.Count - 1
                        If lstInfo.GetItemChecked(i) Then
                            SheetHandler.CurrentRow(TmpSheet) = SheetHandler.CurrentRow(TmpSheet) + 1
                            .Cells(SheetHandler.CurrentRow(TmpSheet), 1).Value = LangIni.Text("Spotlist", lstInfo.Items(i))
                            .Cells(SheetHandler.CurrentRow(TmpSheet), 1).Font.Bold = True
                            Select Case lstInfo.Items(i)
                                Case "Client" : .Cells(SheetHandler.CurrentRow(TmpSheet), 2).Value = Campaign.Client
                                Case "Product" : .Cells(SheetHandler.CurrentRow(TmpSheet), 2).Value = Campaign.Product
                                Case "Buyer" : .Cells(SheetHandler.CurrentRow(TmpSheet), 2).Value = TrinitySettings.UserName
                                Case "Period" : .Cells(SheetHandler.CurrentRow(TmpSheet), 2).Value = PeriodStr
                                Case "E-Mail" : .Cells(SheetHandler.CurrentRow(TmpSheet), 2).Value = TrinitySettings.UserEmail
                                Case "Phone nr" : .Cells(SheetHandler.CurrentRow(TmpSheet), 2).Value = TrinitySettings.UserPhoneNr
                                Case "Spot count" : .Cells(SheetHandler.CurrentRow(TmpSheet), 2).Value = SheetHandler.RowCount(TmpSheet)
                            End Select
                            .Cells(SheetHandler.CurrentRow(TmpSheet), 2).HorizontalAlignment = -4131
                        End If
                    Next
                    If chkSumDaypart.Checked Then
                        Dim x As Integer
                        'write the headline
                        With .Range("F5:" & Char.ConvertFromUtf32(70 + Campaign.Dayparts.Count - 1) & "6")
                            .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                            .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                            For x = 7 To Campaign.Dayparts.Count + 7
                                .Borders(x).LineStyle = 1
                                .Borders(x).Weight = -4138
                                .Borders(x).Color = 0
                            Next
                            'Next
                            .Cells(1, 1).Value = "Daypart summary"
                            .Cells(1, 1).Font.Bold = True
                        End With
                        'write the border
                        With .Range("F7:" & Char.ConvertFromUtf32(70 + Campaign.Dayparts.Count - 1) & 6 + Campaign.Dayparts.Count - 1)
                            For x = 7 To Campaign.Dayparts.Count + 7
                                .Borders(x).LineStyle = 1
                                .Borders(x).Weight = 2
                                .Borders(x).Color = 0
                            Next
                        End With
                        Dim sum As Double
                        For x = 0 To Campaign.Dayparts.Count - 1
                            .Cells(6, 6 + x).Font.Bold = True
                            .Cells(6, 6 + x).Value = Campaign.Dayparts(x).Name.ToString
                        Next
                        For x = 0 To Campaign.Dayparts.Count - 1
                            .Cells(7, 6 + x).Numberformat = "0.0"
                            .Cells(7, 6 + x).Value = SheetHandler.DaypartSum(TmpSheet, x)
                            sum += SheetHandler.DaypartSum(TmpSheet, x)
                        Next
                        For x = 0 To Campaign.Dayparts.Count - 1
                            .Cells(8, 6 + x).Numberformat = "0.0%"
                            If SheetHandler.DaypartSum(TmpSheet, x) > 0 Then
                                .Cells(8, 6 + x).Value = Math.Round((SheetHandler.DaypartSum(TmpSheet, x) / sum), 3)
                            Else
                                .Cells(8, 6 + x).Value = 0
                            End If
                        Next
                    End If
                    .PageSetup.PrintTitleRows = "$" & HeadlineRow & ":$" & HeadlineRow
                    .Select()
                    Excel.ActiveWindow.View = 2
                    While .VPageBreaks.Count > 0
                        .VPageBreaks(1).DragOff(Direction:=-4161, RegionIndex:=1)
                    End While
                    Excel.ActiveWindow.View = 1
                    TmpLogo = .InsertPicture(DataPath & "Logos\" & cmbLogo.Text)
                    Scal = 180 / TmpLogo.Width
                    Scal = 1
                    TmpLogo.ScaleWidth(Scal, 0, 0)
                    TmpLogo.ScaleHeight(Scal, 0, 0)
                    TmpLogo.Top = 20
                    TmpLogo.Left = .Columns(tvwChosen.Nodes.Count + 1).Left - TmpLogo.Width - 10
                    If SheetHandler.RowCount(TmpSheet) = 0 Then
                        .delete()
                    End If
                End With
            Next
        End If
        frmProgress.Hide()
        Excel.ScreenUpdating = True
        Excel.DisplayAlerts = True
        Excel.Visible = True

        Me.Cursor = Windows.Forms.Cursors.Default
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub tvwAvailable_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwAvailable.NodeMouseDoubleClick
        tvwAvailable.Nodes.Remove(e.Node)
        tvwChosen.Nodes.Add(e.Node)
    End Sub

    Private Sub tvwChosen_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwChosen.AfterSelect

    End Sub

    Private Sub tvwChosen_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwChosen.NodeMouseDoubleClick
        tvwChosen.Nodes.Remove(e.Node)
        tvwAvailable.Nodes.Add(e.Node)
    End Sub

    Private Sub tvwChosen_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvwChosen.ItemDrag
        tvwChosen.DoDragDrop(e.Item, Windows.Forms.DragDropEffects.All)
    End Sub

    Private Sub cmdLanguage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLanguage.Click
        mnuLanguage.Show(cmdLanguage, 0, cmdLanguage.Height)
    End Sub

    Function ConvertIntToARGB(ByVal Int As Integer) As Drawing.Color
        'makes a RGB value out of the windows color type
        Dim HexVal As String = Hex(Int)

        While HexVal.Length < 6
            HexVal = "0" & HexVal
        End While
        Return Drawing.Color.FromArgb(Val("&H" & HexVal.Substring(4, 2)), Val("&H" & HexVal.Substring(2, 2)), Val("&H" & HexVal.Substring(0, 2)))
    End Function

    Private Sub frmExportSpotlist_Click(sender As Object, e As EventArgs) Handles Me.Click

    End Sub

    Private Sub frmExportSpotlist_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        Dim LangIni As New Trinity.clsIni
        Dim File As String

        cmbLogo.Items.Clear()
        mnuLanguage.Items.Clear()
        For Each File In My.Computer.FileSystem.GetFiles(DataPath & "Logos\", FileIO.SearchOption.SearchAllSubDirectories, "*.bmp", "*.gif", "*.jpg")
            cmbLogo.Items.Add(My.Computer.FileSystem.GetName(File))
        Next
        LangIni.Create(TrinitySettings.ActiveDataPath & "language.ini")
        Dim TmpLang As LanguageTag
        TmpLang.Path = TrinitySettings.ActiveDataPath & "language.ini"
        TmpLang.Abbrevation = LangIni.Text("General", "Code")
        With mnuLanguage.Items.Add(LangIni.Text("General", "Name"))
            .Tag = TmpLang
            AddHandler .Click, AddressOf ChangeLanguage
        End With
        For i = 1 To TrinitySettings.AreaCount
            LangIni.Create(TrinitySettings.ActiveDataPath & TrinitySettings.Area(i) & "\language.ini")
            With DirectCast(mnuLanguage.Items.Add(LangIni.Text("General", "Name")), Windows.Forms.ToolStripMenuItem)
                Dim TmpLangTag As LanguageTag
                TmpLangTag.Path = TrinitySettings.ActiveDataPath & TrinitySettings.Area(i) & "\language.ini"
                TmpLangTag.Abbrevation = LangIni.Text("General", "Code")
                .Tag = TmpLangTag
                If TrinitySettings.Area(i) = Campaign.Area Then
                    .Checked = True
                    cmdLanguage.Tag = TmpLangTag.Path
                End If
                AddHandler .Click, AddressOf ChangeLanguage
            End With
        Next
        cmbColorScheme.Items.Clear()

        If Campaign.xmlColorSchemes.Count = 0 Then
            For i = 1 To TrinitySettings.ColorSchemeCount
                cmbColorScheme.Items.Add(TrinitySettings.ColorScheme(i))
            Next

            If TrinitySettings.DefaultColorScheme < cmbColorScheme.Items.Count Then
                cmbColorScheme.SelectedIndex = TrinitySettings.DefaultColorScheme
            Else
                cmbColorScheme.SelectedIndex = 0
            End If
            If TrinitySettings.DefaultLogo < cmbLogo.Items.Count Then
                cmbLogo.SelectedIndex = TrinitySettings.DefaultLogo
            Else
                cmbLogo.SelectedIndex = 0
            End If

        Else
            cmbColorScheme.DisplayMember = "name"
            For Each scheme As Trinity.cColorScheme In Campaign.xmlColorSchemes
                cmbColorScheme.Items.Add(scheme)
            Next

        End If
        If TrinitySettings.DefaultLogo < cmbLogo.Items.Count Then
            cmbLogo.SelectedIndex = TrinitySettings.DefaultLogo
        Else
            cmbLogo.SelectedIndex = 0
        End If


        cmbHeadline.Items.Clear()
        LangIni.Create(cmdLanguage.Tag)
        For i = 1 To LangIni.Data("Headlines", "Count")
            cmbHeadline.Items.Add(LangIni.Text("Headlines", "Headline" & i))
        Next
        cmbHeadline.SelectedIndex = 0

        For i = 0 To lstInfo.Items.Count - 1
            If TrinitySettings.DefaultInfo(lstInfo.Items(i)) Then
                lstInfo.SetItemChecked(i, True)
            End If
        Next

        chkWeekSum.Checked = TrinitySettings.DefaultSumWeeks
        chkNone.Checked = (TrinitySettings.DefaultColorCoding = 0)
        chkColorCodeFilm.Checked = (TrinitySettings.DefaultCapitals = 1)
        chkColorCode.Checked = (TrinitySettings.DefaultCapitals = 2)
        chkPreliminary.Checked = Me.Tag = "PLANNED"
        chkCapital.Checked = (TrinitySettings.DefaultCapitals = 0)
        chkCapOnFirst.Checked = (TrinitySettings.DefaultCapitals = 1)
        chkDontChangeCap.Checked = (TrinitySettings.DefaultCapitals = 2)
        chkConvertTime.Checked = TrinitySettings.DefaultConvertToRealTime
        chkSumDaypart.Checked = TrinitySettings.DefaultSumDayparts

        'sets the picture on the area buttion depending on what country is selected
        cmdLanguage.Image = frmMain.ilsBig.Images(Campaign.Area)
    End Sub
End Class