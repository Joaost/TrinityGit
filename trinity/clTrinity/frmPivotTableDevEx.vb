Option Infer On
'Imports System.Windows.Forms
Imports System.Linq
Imports owc11 = Microsoft.Office.Interop.Owc11
Imports System.IO


Public Class frmPivotTableDevEx

    Dim campaignData As DataTable

    Dim availableFields As New List(Of String)
    Dim WeekDays As New List(Of String)
    Dim Months As New List(Of String)
    Dim availableUnits As New List(Of String)
    Dim FieldTranslation As New Dictionary(Of String, String)

    Private Records As PivotRecords
    Private ChannelSortList As New Dictionary(Of String, Integer)

#Region "Pivot Events"

    Private Sub CustomCellDisplayText(ByVal sender As Object, ByVal e As DevExpress.XtraPivotGrid.PivotCellDisplayTextEventArgs) Handles pvtTable.CustomCellDisplayText
        If e.SummaryValue IsNot Nothing AndAlso e.SummaryValue.Max.GetType Is GetType(Single) Then
            e.DisplayText = Format(e.Value, "P1")
        ElseIf e.SummaryValue IsNot Nothing AndAlso e.SummaryValue.Max.GetType Is GetType(Decimal) Then
            e.DisplayText = Format(e.Value, "C0")
        Else
            e.DisplayText = Format(e.Value, "N1")
        End If
    End Sub

    Private Sub CustomSummary(ByVal sender As Object, ByVal e As DevExpress.XtraPivotGrid.PivotGridCustomSummaryEventArgs) Handles pvtTable.CustomSummary
        e.CustomValue = e.SummaryValue.Summary
    End Sub

    Private Sub CustomFieldSort(ByVal sender As Object, ByVal e As DevExpress.XtraPivotGrid.PivotGridCustomFieldSortEventArgs) Handles pvtTable.CustomFieldSort
        Select Case e.Field.Caption
            Case "Channel"
                If Not ChannelSortList.ContainsKey(e.Value1) Then
                    e.Result = 0
                ElseIf ChannelSortList(e.Value1) > ChannelSortList(e.Value2) Then
                    e.Result = 1
                ElseIf ChannelSortList(e.Value1) < ChannelSortList(e.Value2) Then
                    e.Result = -1
                Else
                    e.Result = 0
                End If
                e.Handled = True
            Case "Weekday"
                If Not WeekDays.Contains(e.Value1) Then
                    e.Result = 0
                ElseIf WeekDays.IndexOf(e.Value1) > WeekDays.IndexOf(e.Value2) Then
                    e.Result = 1
                ElseIf WeekDays.IndexOf(e.Value1) < WeekDays.IndexOf(e.Value2) Then
                    e.Result = -1
                Else
                    e.Result = 0
                End If
                e.Handled = True
            Case Else
                e.Handled = False
        End Select
    End Sub

    Private Sub pvtTable_CustomUnboundFieldData(ByVal sender As Object, ByVal e As DevExpress.XtraPivotGrid.CustomFieldDataEventArgs) Handles pvtTable.CustomUnboundFieldData

        'Dim i As Integer = 2
        'e.Value = e.GetListSourceColumnValue("Value")
        Select Case e.Field.FieldName
            Case Is = "CPP Main"
                Dim TRP As Single = Single.Parse(e.GetListSourceColumnValue("TRP Main"))
                If TRP = 0 Then
                    e.Value = 0
                Else
                    e.Value = Single.Parse(e.GetListSourceColumnValue("Budget Net")) / TRP
                End If
            Case Is = "CPP Buying"
                Dim TRP As Single = Single.Parse(e.GetListSourceColumnValue("TRP Buying"))
                If TRP = 0 Then
                    e.Value = 0
                Else
                    e.Value = Single.Parse(e.GetListSourceColumnValue("Budget Net")) / TRP
                End If
            Case Is = "CPT Main"
                Dim _000 As Single = e.GetListSourceColumnValue("'000 Main")
                If _000 = 0 Then
                    e.Value = 0
                Else
                    e.Value = Single.Parse(e.GetListSourceColumnValue("Budget Net")) / _000
                End If
            Case Is = "CPT Buying"
                Dim _000 As Single = e.GetListSourceColumnValue("'000 Buying")
                If _000 = 0 Then
                    e.Value = 0
                Else
                    e.Value = Single.Parse(e.GetListSourceColumnValue("Budget Net")) / _000
                End If
            Case Else
                e.Value = Single.Parse(e.GetListSourceColumnValue(FieldTranslation(e.Field.FieldName)))
        End Select

    End Sub
#End Region


    Private Sub cmdDefine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDefine.Click
        Dim frmDefinePivot As New frmPivotDefinition()

        'Clear it
        frmDefinePivot.tvwAvailable.Nodes.Clear()
        frmDefinePivot.tvwColumns.Nodes.Clear()
        frmDefinePivot.tvwRows.Nodes.Clear()
        frmDefinePivot.lstUnits.Items.Clear()

        'Get the location of the fields and add them to the TreeViews
        For Each s As String In availableFields
            If pvtTable.Fields.GetFieldByName(FieldTranslation(s)) Is Nothing Then
                frmDefinePivot.tvwAvailable.Nodes.Add(s)
            Else
                If pvtTable.Fields.GetFieldByName(FieldTranslation(s)).Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea Then
                    frmDefinePivot.tvwColumns.Nodes.Add(s)
                Else
                    frmDefinePivot.tvwRows.Nodes.Add(s)
                End If
            End If
        Next

        For Each s As String In availableUnits
            Dim NewIndex As Integer = frmDefinePivot.lstUnits.Items.Add(s)
            If Not pvtTable.Fields.GetFieldByName("fldUnit") Is Nothing AndAlso pvtTable.Fields.GetFieldByName("fldUnit").FilterValues.Contains(s) Then
                frmDefinePivot.lstUnits.SetItemChecked(NewIndex, True)
            Else
                frmDefinePivot.lstUnits.SetItemChecked(NewIndex, False)
            End If
        Next

        If frmDefinePivot.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim Dimensions As New List(Of String)

            pvtTable.DataSource = Nothing
            pvtTable.Fields.Clear()
            With pvtTable.Fields.Add("Value", DevExpress.XtraPivotGrid.PivotArea.DataArea)
                .Options.ShowGrandTotal = False
                .Caption = "Value"
                .Name = "fldValue"
                .AllowedAreas = DevExpress.XtraPivotGrid.PivotGridAllowedAreas.DataArea
                .TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
                .SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom
            End With
            For Each TmpNode As System.Windows.Forms.TreeNode In frmDefinePivot.tvwColumns.Nodes
                With pvtTable.Fields.Add(TmpNode.Text, DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
                    .Options.ShowGrandTotal = False
                    .Caption = TmpNode.Text
                    .Name = FieldTranslation(TmpNode.Text)
                    If TmpNode.Text = "Channel" Then
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom
                    ElseIf TmpNode.Text = "Week" Then
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Value
                    ElseIf TmpNode.Text = "Weekday" Then
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom
                    Else
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Default
                    End If
                    .AllowedAreas = DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea + DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea
                    .TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
                End With
                Dimensions.Add(TmpNode.Text)
            Next
            For Each TmpNode As System.Windows.Forms.TreeNode In frmDefinePivot.tvwRows.Nodes
                With pvtTable.Fields.Add(TmpNode.Text, DevExpress.XtraPivotGrid.PivotArea.RowArea)
                    .Options.ShowGrandTotal = False
                    .Caption = TmpNode.Text
                    .Name = FieldTranslation(TmpNode.Text)
                    If TmpNode.Text = "Channel" Then
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom
                    ElseIf TmpNode.Text = "Week" Then
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Value
                    ElseIf TmpNode.Text = "Weekday" Then
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom
                    Else
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Default
                    End If
                    .AllowedAreas = DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea + DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea
                    .TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
                End With
                Dimensions.Add(TmpNode.Text)
            Next
            pvtTable.Fields.GetFieldByName("fldUnit").FilterValues.Clear()
            Dim count As Integer = 0
            Dim TmpObj(frmDefinePivot.lstUnits.Items.Count - frmDefinePivot.lstUnits.CheckedItems.Count - 1) As Object
            For i As Integer = 0 To frmDefinePivot.lstUnits.Items.Count - 1
                If Not frmDefinePivot.lstUnits.GetItemChecked(i) Then
                    TmpObj(count) = frmDefinePivot.lstUnits.Items(i)
                    count += 1
                End If
            Next
            pvtTable.Fields.GetFieldByName("fldUnit").FilterValues.SetValues(TmpObj, DevExpress.Data.PivotGrid.PivotFilterType.Excluded, True)

            pvtTable.DataSource = CreateDataSource(Dimensions)
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End If
    End Sub

    Function CreateDataSource(ByVal Dimensions As List(Of String))
        If Records Is Nothing Then Return Nothing

        'Get what Fields to show.
        If Not Dimensions Is Nothing Then
            Records.ShowChannels = Dimensions.Contains("Channel")
            Records.ShowBookingtypeNames = Dimensions.Contains("Bookingtype")
            Records.ShowDayParts = Dimensions.Contains("Daypart")
            Records.ShowFilms = Dimensions.Contains("Film")
            Records.ShowFilmCodes = Dimensions.Contains("Filmcode")
            Records.ShowMonths = Dimensions.Contains("Month")
            Records.ShowWeeks = Dimensions.Contains("Week")
            Records.ShowWeekDays = Dimensions.Contains("Weekday")
            Records.ShowStatuses = Dimensions.Contains("Status")
            Records.ShowCombinations = Dimensions.Contains("Combination")
        End If

        Dim list = From rec In Records Group By rec.Channel, rec.BookingType, rec.Month, rec.Week, rec.Weekday, rec.Film, rec.Filmcode, rec.Daypart, rec.Status, rec.Combination Into Group

        Dim UseSpots As New Dictionary(Of Trinity.cBookingType, Boolean)

        'Create the Table
        campaignData = New DataTable()
        campaignData.Columns.Add("Channel", GetType(String))
        campaignData.Columns.Add("Combination", GetType(String))
        campaignData.Columns.Add("Bookingtype", GetType(String))
        campaignData.Columns.Add("Month", GetType(String))
        campaignData.Columns.Add("Week", GetType(String))
        campaignData.Columns.Add("Weekday", GetType(String))
        campaignData.Columns.Add("Film", GetType(String))
        campaignData.Columns.Add("Filmcode", GetType(String))
        campaignData.Columns.Add("Daypart", GetType(String))
        campaignData.Columns.Add("Status", GetType(String))
        campaignData.Columns.Add("Unit", GetType(String))
        campaignData.Columns.Add("Value", GetType(Object))

        For Each Record In list
            'TRP Main
            campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "TRP Main", CType(Record.Group.Sum(Function(rec) rec.TRPMain), Double))
            'TRP Second
            campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "TRP Second", CType(Record.Group.Sum(Function(rec) rec.TRPSecond), Double))
            'TRP Buying
            campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "TRP Buying", CType(Record.Group.Sum(Function(rec) rec.TRPBuying), Double))
            ''000 Main
            campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "'000 Main", CType(Record.Group.Sum(Function(rec) rec._000Main), Double))
            ''000 Buying
            campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "'000 Buying", CType(Record.Group.Sum(Function(rec) rec._000Buying), Double))
            'Budget Gross
            campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "Budget Gross", CType(Record.Group.Sum(Function(rec) rec.BudgetGross), Decimal))
            'Budget Net
            campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "Budget Net", CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Decimal))

            'Discount
            If Record.Group.Sum(Function(rec) rec.BudgetGross) > 0 Then 'cant divide by zero
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "Discount", CType(1 - (CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Single) / CType(Record.Group.Sum(Function(rec) rec.BudgetGross), Single)), Single))
            Else
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "Discount", CType(0, Single))
            End If

            'CPP Main
            If Record.Group.Sum(Function(rec) rec.TRPMain) > 0 Then 'cant divide by zero
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Main", CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Double) / CType(Record.Group.Sum(Function(rec) rec.TRPMain), Double))
            Else
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Main", CType(0, Double))
            End If

            'CPP Main Gross
            If Record.Group.Sum(Function(rec) rec.TRPMain) > 0 Then 'cant divide by zero
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Main Gross", CType(Record.Group.Sum(Function(rec) rec.BudgetGross), Double) / CType(Record.Group.Sum(Function(rec) rec.TRPMain), Double))
            Else
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Main Gross", CType(0, Double))
            End If

            'CPP Main 30
            If Record.Group.Sum(Function(rec) rec.TRPMain30) > 0 Then 'cant divide by zero
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP30 Main", CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Double) / CType(Record.Group.Sum(Function(rec) rec.TRPMain30), Double))
            Else
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP30 Main", CType(0, Double))
            End If

            'CPP Buying
            If Record.Group.Sum(Function(rec) rec.TRPMain) > 0 Then 'cant divide by zero
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Buying", CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Double) / CType(Record.Group.Sum(Function(rec) rec.TRPBuying), Double))
            Else
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Buying", CType(0, Double))
            End If

            'CPP Buying Gross
            If Record.Group.Sum(Function(rec) rec.TRPMain) > 0 Then 'cant divide by zero
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Buying Gross", CType(Record.Group.Sum(Function(rec) rec.BudgetGross), Double) / CType(Record.Group.Sum(Function(rec) rec.TRPBuying), Double))
            Else
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Buying Gross", CType(0, Double))
            End If

            'CPP Buying 30
            If Record.Group.Sum(Function(rec) rec.TRPMain30) > 0 Then 'cant divide by zero
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP30 Buying", CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Double) / CType(Record.Group.Sum(Function(rec) rec.TRPBuying30), Double))
            Else
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP30 Buying", CType(0, Double))
            End If

            'CPP Second
            If Record.Group.Sum(Function(rec) rec.TRPSecond) > 0 Then 'cant divide by zero
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Second", CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Double) / CType(Record.Group.Sum(Function(rec) rec.TRPSecond), Double))
            Else
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Second", CType(0, Double))
            End If

            'CPP Second Gross
            If Record.Group.Sum(Function(rec) rec.TRPMain) > 0 Then 'cant divide by zero
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Second Gross", CType(Record.Group.Sum(Function(rec) rec.BudgetGross), Double) / CType(Record.Group.Sum(Function(rec) rec.TRPSecond), Double))
            Else
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Second Gross", CType(0, Double))
            End If

            'CPP Second 30
            If Record.Group.Sum(Function(rec) rec.TRPSecond30) > 0 Then 'cant divide by zero
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP30 Second", CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Double) / CType(Record.Group.Sum(Function(rec) rec.TRPSecond30), Double))
            Else
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP30 Second", CType(0, Double))
            End If


            'CPT Main
            If Record.Group.Sum(Function(rec) rec.TRPMain) > 0 Then 'cant divide by zero
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPT Main", CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Single) / CType(Record.Group.Sum(Function(rec) rec._000Main), Double))
            Else
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPT Main", CType(0, Double))
            End If

            'CPT Main Gross
            If Record.Group.Sum(Function(rec) rec.TRPMain) > 0 Then 'cant divide by zero
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPT Main Gross", CType(Record.Group.Sum(Function(rec) rec.BudgetGross), Single) / CType(Record.Group.Sum(Function(rec) rec._000Main), Double))
            Else
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPT Main Gross", CType(0, Double))
            End If

            'CPT Buying
            If Record.Group.Sum(Function(rec) rec.TRPMain) > 0 Then 'cant divide by zero
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPT Buying", CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Double) / CType(Record.Group.Sum(Function(rec) rec._000Buying), Double))
            Else
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPT Buying", CType(0, Double))
            End If

            'CPT Buying Gross
            If Record.Group.Sum(Function(rec) rec.TRPMain) > 0 Then 'cant divide by zero
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPT Buying Gross", CType(Record.Group.Sum(Function(rec) rec.BudgetGross), Double) / CType(Record.Group.Sum(Function(rec) rec._000Buying), Double))
            Else
                campaignData.Rows.Add(Record.Channel, Record.Combination, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPT Buying Gross", CType(0, Double))
            End If

        Next

        Return campaignData
    End Function

    Function CreateDataSourceADODB(ByVal Dimensions As List(Of String)) As ADODB.Recordset
        If Records Is Nothing Then Return Nothing

        'Get what Fields to show.
        If Not Dimensions Is Nothing Then
            Records.ShowChannels = Dimensions.Contains("Channel")
            Records.ShowBookingtypeNames = Dimensions.Contains("Bookingtype")
            Records.ShowDayParts = Dimensions.Contains("Daypart")
            Records.ShowFilms = Dimensions.Contains("Film")
            Records.ShowFilmCodes = Dimensions.Contains("Filmcode")
            Records.ShowMonths = Dimensions.Contains("Month")
            Records.ShowWeeks = Dimensions.Contains("Week")
            Records.ShowWeekDays = Dimensions.Contains("Weekday")
            Records.ShowStatuses = Dimensions.Contains("Status")
        End If

        Dim list = From rec In Records Group By rec.Channel, rec.BookingType, rec.Month, rec.Week, rec.Weekday, rec.Film, rec.Filmcode, rec.Daypart, rec.Status Into Group

        Dim UseSpots As New Dictionary(Of Trinity.cBookingType, Boolean)

        'Create the Table
        Dim rs As ADODB.Recordset = New ADODB.Recordset()
        rs.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        rs.CursorType = ADODB.CursorTypeEnum.adOpenDynamic
        rs.LockType = ADODB.LockTypeEnum.adLockOptimistic
        rs.Fields.Append("Channel", ADODB.DataTypeEnum.adChar, 255)
        rs.Fields.Append("Bookingtype", ADODB.DataTypeEnum.adChar, 100)
        rs.Fields.Append("Month", ADODB.DataTypeEnum.adChar, 20)
        rs.Fields.Append("Week", ADODB.DataTypeEnum.adChar, 20)
        rs.Fields.Append("Weekday", ADODB.DataTypeEnum.adChar, 20)
        rs.Fields.Append("Film", ADODB.DataTypeEnum.adChar, 255)
        rs.Fields.Append("Filmcode", ADODB.DataTypeEnum.adChar, 20)
        rs.Fields.Append("Daypart", ADODB.DataTypeEnum.adChar, 20)
        rs.Fields.Append("Status", ADODB.DataTypeEnum.adChar, 220)
        rs.Fields.Append("Unit", ADODB.DataTypeEnum.adChar, 255)
        rs.Fields.Append("Value", ADODB.DataTypeEnum.adSingle)
        rs.Open()

        For Each Record In list
            'TRP Main
            AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "TRP Main", CType(Record.Group.Sum(Function(rec) rec.TRPMain), Double))
            'TRP Second
            AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "TRP Second", CType(Record.Group.Sum(Function(rec) rec.TRPSecond), Double))
            'TRP Buying
            AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "TRP Buying", CType(Record.Group.Sum(Function(rec) rec.TRPBuying), Double))
            ''000 Main
            AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "'000 Main", CType(Record.Group.Sum(Function(rec) rec._000Main), Double))
            ''000 Buying
            AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "'000 Buying", CType(Record.Group.Sum(Function(rec) rec._000Buying), Double))
            'Budget Gross
            AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "Budget Gross", CType(Record.Group.Sum(Function(rec) rec.BudgetGross), Double))
            'Budget Net
            AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "Budget Net", CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Double))

            'Discount
            If Record.Group.Sum(Function(rec) rec.BudgetGross) > 0 Then 'cant divide by zero
                AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "Discount", 1 - (CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Double) / CType(Record.Group.Sum(Function(rec) rec.BudgetGross), Double)))
            Else
                AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "Discount", CType(0, Double))
            End If

            'CPP Main
            If Record.Group.Sum(Function(rec) rec.TRPMain) > 0 Then 'cant divide by zero
                AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Main", CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Double) / CType(Record.Group.Sum(Function(rec) rec.TRPMain), Double))
            Else
                AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Main", CType(0, Double))
            End If

            'CPP Buying
            If Record.Group.Sum(Function(rec) rec.TRPMain) > 0 Then 'cant divide by zero
                AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Buying", CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Double) / CType(Record.Group.Sum(Function(rec) rec.TRPBuying), Double))
            Else
                AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPP Buying", CType(0, Double))
            End If

            'CPT Main
            If Record.Group.Sum(Function(rec) rec.TRPMain) > 0 Then 'cant divide by zero
                AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPT Main", CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Single) / CType(Record.Group.Sum(Function(rec) rec._000Main), Double))
            Else
                AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPT Main", CType(0, Double))
            End If

            'CPT Buying
            If Record.Group.Sum(Function(rec) rec.TRPMain) > 0 Then 'cant divide by zero
                AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPT Buying", CType(Record.Group.Sum(Function(rec) rec.BudgetNet), Double) / CType(Record.Group.Sum(Function(rec) rec._000Buying), Double))
            Else
                AddToPivot(rs, Record.Channel, Record.BookingType, Record.Month, Record.Week, Record.Weekday, Record.Film, Record.Filmcode, Record.Daypart, Record.Status, _
                                  "CPT Buying", CType(0, Double))
            End If
        Next

        Return rs
    End Function

    Sub AddToPivot(ByVal rs As ADODB.Recordset, ByVal Channel As String, ByVal Bookingtype As String, ByVal month As String, ByVal week As String, ByVal weekDay As String, ByVal Film As String, ByVal Filmcode As String, ByVal Daypart As String, ByVal Status As String, ByVal Unit As String, ByVal Value As Single)
        rs.AddNew()
        rs.Fields(0).Value = Channel
        rs.Fields(1).Value = Bookingtype
        rs.Fields(2).Value = month
        rs.Fields(3).Value = week
        rs.Fields(4).Value = weekDay
        rs.Fields(5).Value = Film
        rs.Fields(6).Value = Filmcode
        rs.Fields(7).Value = Daypart
        rs.Fields(8).Value = Status
        rs.Fields(9).Value = Unit
        rs.Fields(10).Value = Value
    End Sub

    Private Sub createBaseRecords()
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek
        Dim TmpFilm As Trinity.cFilm
        Dim TmpDayPart As Trinity.cDaypart

        Dim DP As Short
        Dim Percent As Single
        Dim WD As Short
        Dim Disc As Single

        Dim UseSpots As New Dictionary(Of Trinity.cBookingType, Boolean)

        Dim Record As PivotRecord

        Records = New PivotRecords()

        'Fetch all the values
        'Just values for RBS types
        Dim TotDPs = (From _chan As Trinity.cChannel In Campaign.Channels From _bt As Trinity.cBookingType In _chan.BookingTypes Where _bt.BookIt From _week As Trinity.cWeek In _bt.Weeks Select _week.Films.Count * _bt.Dayparts.Count).Sum
        Dim Tot = TotDPs * 7

        Dim _progress As New frmProgress
        _progress.MaxValue = Tot
        _progress.Show()
        For Each TmpChan In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                'For Each TmpDayPart In TmpBT.Dayparts 'Hannes added

                Dim IsPartOfCombination As Boolean = False
                Dim CombinationName As String = ""

                If TmpBT.Combination IsNot Nothing Then
                    IsPartOfCombination = True
                    CombinationName = TmpBT.Combination.Name
                End If

                If TmpBT.BookIt Then
                    If Campaign.PlannedSpots.TotalNetBudget(TmpChan.ChannelName, TmpBT.Name) = TmpBT.ConfirmedNetBudget Then
                        UseSpots.Add(TmpBT, True)
                    Else
                        UseSpots.Add(TmpBT, False)
                    End If


                    If TmpBT.BuyingTarget.CalcCPP Then
                        For Each TmpWeek In TmpBT.Weeks
                            For Each TmpFilm In TmpWeek.Films
                                For DP = 0 To TmpBT.Dayparts.Count - 1 'This is DP
                                    For WD = 1 To 7
                                        Percent = ((TmpFilm.Share / 100) * (TmpBT.Dayparts(DP).Share / 100)) / 7
                                        Record = Records.Add
                                        _progress.Progress += 1
                                        If IsPartOfCombination Then
                                            Record.Combination = CombinationName
                                        Else
                                            Record.Combination = TmpChan.ChannelName
                                        End If

                                        Record.Channel = TmpChan.ChannelName
                                        Record.BookingType = TmpBT.Name
                                        Record.Week = TmpWeek.Name
                                        Record.Weekday = WeekDays(WD - 1)
                                        Record.Month = Months(Month(Date.FromOADate(TmpWeek.StartDate + WD - 1)) - 1)
                                        Record.Daypart = TmpBT.Dayparts(DP).Name
                                        Record.Film = TmpFilm.Name
                                        Record.Filmcode = TmpFilm.Filmcode
                                        Record.Status = "Planned"

                                        If TmpWeek.NetBudget = 0 Then
                                            Disc = 0
                                        Else
                                            Disc = TmpWeek.GrossBudget / TmpWeek.NetBudget
                                        End If

                                        Record.BudgetGross = ((TmpWeek.TRPBuyingTarget * Percent) * TmpWeek.NetCPP30 * (TmpFilm.Index / 100)) * Disc
                                        Record.BudgetNet = (TmpWeek.TRPBuyingTarget * Percent) * TmpWeek.NetCPP30 * (TmpFilm.Index / 100)

                                        Record.TRPMain = TmpWeek.TRP * Percent
                                        Record.TRPSecond = TmpWeek.TRP * Percent * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                        Record.TRPBuying = TmpWeek.TRPBuyingTarget * Percent
                                        Record.TRPMain30 = (TmpWeek.TRP * Percent) / (TmpFilm.Index / 100) * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                        Record.TRPBuying30 = (TmpWeek.TRPBuyingTarget * Percent) / (TmpFilm.Index / 100)
                                        Record._000Main = (TmpWeek.TRP * Percent / 100) * Campaign.MainTarget.UniSize
                                        Record._000Buying = (TmpWeek.TRPBuyingTarget * Percent / 100) * TmpBT.BuyingTarget.Target.UniSize

                                        If Not UseSpots(TmpBT) Then
                                            Percent = ((TmpWeek.TRPBuyingTarget * Percent) * TmpWeek.NetCPP30 * (TmpFilm.Index / 100)) / TmpBT.PlannedNetBudget

                                            'Add the "Confirmed" budget
                                            Record = Records.Add

                                            If IsPartOfCombination Then
                                                Record.Combination = CombinationName
                                            End If

                                            Record.Channel = TmpChan.ChannelName
                                            Record.BookingType = TmpBT.Name
                                            Record.Week = TmpWeek.Name
                                            Record.Weekday = WeekDays(WD - 1)
                                            Record.Month = Months(Month(Date.FromOADate(TmpWeek.StartDate + WD - 1)) - 1)
                                            Record.Daypart = TmpBT.Dayparts(DP).Name
                                            Record.Film = TmpFilm.Name
                                            Record.Filmcode = TmpFilm.Filmcode
                                            Record.Status = "Confirmed"

                                            Record.BudgetGross = TmpBT.ConfirmedNetBudget * Percent * Disc
                                            Record.BudgetNet = TmpBT.ConfirmedNetBudget * Percent

                                            'Add the "Actual" Budget
                                            Record = Records.Add

                                            If IsPartOfCombination Then
                                                Record.Combination = CombinationName
                                            End If

                                            Record.Channel = TmpChan.ChannelName
                                            Record.BookingType = TmpBT.Name
                                            Record.Week = TmpWeek.Name
                                            Record.Weekday = WeekDays(WD - 1)
                                            Record.Month = Months(Month(Date.FromOADate(TmpWeek.StartDate + WD - 1)) - 1)
                                            Record.Daypart = TmpBT.Dayparts(DP).Name
                                            Record.Film = TmpFilm.Name
                                            Record.Filmcode = TmpFilm.Filmcode
                                            Record.Status = "Actual"

                                            Record.BudgetGross = TmpBT.ActualNetValue * Percent * Disc
                                            Record.BudgetNet = TmpBT.ActualNetValue * Percent

                                        End If
                                    Next
                                Next 'This is DP
                            Next TmpFilm
                        Next TmpWeek
                    Else 'Not calculating price from dayparts
                        For Each TmpWeek In TmpBT.Weeks
                            For Each TmpFilm In TmpWeek.Films
                                For DP = 0 To TmpBT.Dayparts.Count - 1
                                    For WD = 1 To 7
                                        Percent = ((TmpFilm.Share / 100) * (TmpBT.Dayparts(DP).Share / 100)) / 7
                                        Record = Records.Add
                                        _progress.Progress += 1

                                        If IsPartOfCombination Then
                                            Record.Combination = CombinationName
                                        Else
                                            Record.Combination = TmpChan.ChannelName
                                        End If

                                        Record.Channel = TmpChan.ChannelName
                                        Record.BookingType = TmpBT.Name
                                        Record.Week = TmpWeek.Name
                                        Record.Weekday = WeekDays(WD - 1)
                                        Record.Month = Months(Month(Date.FromOADate(TmpWeek.StartDate + WD - 1)) - 1)
                                        'Record.Daypart = TmpDayPart.Name
                                        Record.Daypart = TmpBT.Dayparts(DP).Name
                                        Record.Film = TmpFilm.Name
                                        Record.Filmcode = TmpFilm.Filmcode
                                        Record.Status = "Planned"

                                        If TmpWeek.NetBudget = 0 Then
                                            Disc = 0
                                        Else
                                            Disc = TmpWeek.GrossBudget / TmpWeek.NetBudget
                                        End If

                                        Record.BudgetGross = ((TmpWeek.TRPBuyingTarget * Percent) * TmpWeek.NetCPP30 * (TmpFilm.Index / 100)) * Disc

                                        Record.BudgetNet = (TmpWeek.TRPBuyingTarget * Percent) * TmpWeek.NetCPP30 * (TmpFilm.Index / 100)

                                        Record.TRPMain = TmpWeek.TRP * Percent
                                        Record.TRPSecond = TmpWeek.TRP * Percent * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                        Record.TRPBuying = TmpWeek.TRPBuyingTarget * Percent
                                        Record.TRPMain30 = (TmpWeek.TRP * Percent) / (TmpFilm.Index / 100) * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                        Record.TRPBuying30 = (TmpWeek.TRPBuyingTarget * Percent) / (TmpFilm.Index / 100)
                                        Record._000Main = (TmpWeek.TRP * Percent / 100) * Campaign.MainTarget.UniSize
                                        Record._000Buying = (TmpWeek.TRPBuyingTarget * Percent / 100) * TmpBT.BuyingTarget.Target.UniSize

                                        If Not UseSpots(TmpBT) Then
                                            Percent = ((TmpWeek.TRPBuyingTarget * Percent) * TmpWeek.NetCPP30 * (TmpFilm.Index / 100)) / TmpBT.PlannedNetBudget

                                            'Add the "Confirmed" budget
                                            Record = Records.Add

                                            If IsPartOfCombination Then
                                                Record.Combination = CombinationName
                                            End If

                                            Record.Channel = TmpChan.ChannelName
                                            Record.BookingType = TmpBT.Name
                                            Record.Week = TmpWeek.Name
                                            Record.Weekday = WeekDays(WD - 1)
                                            Record.Month = Months(Month(Date.FromOADate(TmpWeek.StartDate + WD - 1)) - 1)
                                            Record.Daypart = TmpBT.Dayparts(DP).Name
                                            'Record.Daypart = TmpDayPart.Name
                                            Record.Film = TmpFilm.Name
                                            Record.Filmcode = TmpFilm.Filmcode
                                            Record.Status = "Confirmed"

                                            Record.BudgetGross = TmpBT.ConfirmedNetBudget * Percent * Disc

                                            Record.BudgetNet = TmpBT.ConfirmedNetBudget * Percent



                                            'Add the "Actual" Budget
                                            Record = Records.Add

                                            If IsPartOfCombination Then
                                                Record.Combination = CombinationName
                                            End If

                                            Record.Channel = TmpChan.ChannelName
                                            Record.BookingType = TmpBT.Name
                                            Record.Week = TmpWeek.Name
                                            Record.Weekday = WeekDays(WD - 1)
                                            Record.Month = Months(Month(Date.FromOADate(TmpWeek.StartDate + WD - 1)) - 1)
                                            'Record.Daypart = TmpDayPart.Name
                                            Record.Daypart = TmpBT.Dayparts(DP).Name
                                            Record.Film = TmpFilm.Name
                                            Record.Filmcode = TmpFilm.Filmcode
                                            Record.Status = "Actual"

                                            Record.BudgetGross = TmpBT.ConfirmedNetBudget * Percent * Disc
                                            Record.BudgetNet = TmpBT.ConfirmedNetBudget * Percent


                                        End If
                                    Next
                                Next
                            Next TmpFilm
                        Next TmpWeek
                    End If
                End If
                ' Next TmpDayPart 'Hannes added
            Next TmpBT
        Next TmpChan


        'Now add records for the booked spots from specifics types
        For Each TmpBookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
            If TmpBookedSpot.Bookingtype.BookIt Then
                Record = Records.Add

                Record.Channel = TmpBookedSpot.Channel.ChannelName
                Record.BookingType = TmpBookedSpot.Bookingtype.Name
                Record.Week = TmpBookedSpot.week.Name
                Record.Weekday = WeekDays(Weekday(TmpBookedSpot.AirDate, FirstDayOfWeek.Monday) - 1)
                Record.Month = Months(Month(TmpBookedSpot.AirDate) - 1)
                Record.Daypart = TmpBookedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpBookedSpot.MaM).Name
                If TmpBookedSpot.Film IsNot Nothing Then
                    Record.Film = TmpBookedSpot.Film.Name
                Else
                    Record.Film = "<Unknown>"
                End If

                Record.Filmcode = TmpBookedSpot.Filmcode
                Record.Status = "Booked"

                Record.BudgetGross = TmpBookedSpot.GrossPrice
                Record.BudgetNet = TmpBookedSpot.NetPrice
                Record.TRPMain = TmpBookedSpot.MyEstimate
                Record.TRPSecond = TmpBookedSpot.MyEstimate * (TmpBookedSpot.Bookingtype.IndexSecondTarget / TmpBookedSpot.Bookingtype.IndexMainTarget)
                Record.TRPBuying = TmpBookedSpot.MyEstimateBuyTarget
                Record.TRPMain30 = TmpBookedSpot.MyEstimate / (TmpBookedSpot.Film.Index / 100)
                Record.TRPBuying30 = TmpBookedSpot.MyEstimateBuyTarget / (TmpBookedSpot.Film.Index / 100)
                Record._000Main = (TmpBookedSpot.MyEstimate / 100) * Campaign.MainTarget.UniSize
                Record._000Buying = (TmpBookedSpot.MyEstimateBuyTarget / 100) * TmpBookedSpot.Bookingtype.BuyingTarget.Target.UniSize
            End If
        Next TmpBookedSpot

        'Now data for the planned spots
        For Each TmpPlannedSpot As Trinity.cPlannedSpot In Campaign.PlannedSpots
            If TmpPlannedSpot.Bookingtype.BookIt Then
                If Not TmpPlannedSpot.Week Is Nothing Then
                    If UseSpots(TmpPlannedSpot.Bookingtype) Then
                        Record = Records.Add

                        Record.Channel = TmpPlannedSpot.Channel.ChannelName
                        Record.BookingType = TmpPlannedSpot.Bookingtype.Name
                        Record.Week = TmpPlannedSpot.Week.Name
                        Record.Weekday = WeekDays(Weekday(Date.FromOADate(TmpPlannedSpot.MaM), FirstDayOfWeek.Monday) - 1)
                        Record.Month = Months(Month(Date.FromOADate(TmpPlannedSpot.AirDate)) - 1)
                        Record.Daypart = TmpPlannedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpPlannedSpot.MaM).Name
                        Record.Film = TmpPlannedSpot.Film.Name
                        Record.Filmcode = TmpPlannedSpot.Filmcode
                        Record.Status = "Confirmed"

                        Record.BudgetGross = TmpPlannedSpot.PriceGross
                        Record.BudgetNet = TmpPlannedSpot.PriceNet
                        Record.TRPMain = TmpPlannedSpot.MyRating
                        Record.TRPSecond = TmpPlannedSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget)
                        Record.TRPBuying = TmpPlannedSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
                        Record.TRPMain30 = TmpPlannedSpot.MyRating / (TmpPlannedSpot.Film.Index / 100)
                        Record.TRPBuying30 = TmpPlannedSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) / (TmpPlannedSpot.Film.Index / 100)
                        Record._000Main = (TmpPlannedSpot.MyRating / 100) * Campaign.MainTarget.UniSize
                        Record._000Buying = (TmpPlannedSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) / 100) * TmpPlannedSpot.Bookingtype.BuyingTarget.Target.UniSize

                        'Add Actual budget
                        Record = Records.Add

                        Record.Channel = TmpPlannedSpot.Channel.ChannelName
                        Record.BookingType = TmpPlannedSpot.Bookingtype.Name
                        Record.Week = TmpPlannedSpot.Week.Name
                        Record.Weekday = WeekDays(Weekday(Date.FromOADate(TmpPlannedSpot.MaM), FirstDayOfWeek.Monday) - 1)
                        Record.Month = Months(Month(Date.FromOADate(TmpPlannedSpot.AirDate)) - 1)
                        Record.Daypart = TmpPlannedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpPlannedSpot.MaM).Name
                        Record.Film = TmpPlannedSpot.Film.Name
                        Record.Filmcode = TmpPlannedSpot.Filmcode
                        Record.Status = "Actual"

                        Record.BudgetGross = TmpPlannedSpot.PriceGross
                        Record.BudgetNet = TmpPlannedSpot.PriceNet
                    Else
                        Record = Records.Add

                        Record.Channel = TmpPlannedSpot.Channel.ChannelName
                        Record.BookingType = TmpPlannedSpot.Bookingtype.Name
                        Record.Week = TmpPlannedSpot.Week.Name
                        Record.Weekday = WeekDays(Weekday(Date.FromOADate(TmpPlannedSpot.MaM), FirstDayOfWeek.Monday) - 1)
                        Record.Month = Months(Month(Date.FromOADate(TmpPlannedSpot.AirDate)) - 1)
                        Record.Daypart = TmpPlannedSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpPlannedSpot.MaM).Name
                        If TmpPlannedSpot.Film IsNot Nothing Then
                            Record.Film = TmpPlannedSpot.Film.Name
                        Else
                            Record.Film = TmpPlannedSpot.Filmcode
                        End If
                        Record.Filmcode = TmpPlannedSpot.Filmcode
                        Record.Status = "Confirmed"

                        Record.TRPMain = TmpPlannedSpot.MyRating
                        Record.TRPSecond = TmpPlannedSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget)
                        Record.TRPBuying = TmpPlannedSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
                        If TmpPlannedSpot.Film IsNot Nothing Then
                            Record.TRPMain30 = TmpPlannedSpot.MyRating / (TmpPlannedSpot.Film.Index / 100)
                            Record.TRPBuying30 = TmpPlannedSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) / (TmpPlannedSpot.Film.Index / 100)
                        Else
                            Dim _idx As Single = TmpPlannedSpot.Bookingtype.FilmIndex(TmpPlannedSpot.SpotLength)
                            Record.TRPMain30 = IIf(_idx > 0, TmpPlannedSpot.MyRating / (_idx / 100), 0)
                            Record.TRPBuying30 = IIf(_idx > 0, TmpPlannedSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) / (_idx / 100), 0)
                        End If
                        Record._000Main = (TmpPlannedSpot.MyRating / 100) * Campaign.MainTarget.UniSize
                        Record._000Buying = (TmpPlannedSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) / 100) * TmpPlannedSpot.Bookingtype.BuyingTarget.Target.UniSize
                    End If
                End If
            End If
        Next

        'Now the actual spots
        For Each TmpActualSpot As Trinity.cActualSpot In Campaign.ActualSpots
            If TmpActualSpot.Bookingtype.BookIt Then
                If Not TmpActualSpot.Week Is Nothing Then
                    With Records.Add

                        .Channel = TmpActualSpot.Channel.ChannelName
                        .BookingType = TmpActualSpot.Bookingtype.Name
                        .Week = TmpActualSpot.Week.Name
                        .Weekday = WeekDays(Weekday(Date.FromOADate(TmpActualSpot.MaM), FirstDayOfWeek.Monday) - 1) 'TmpActualSpot.AirDate
                        .Month = Months(Month(Date.FromOADate(TmpActualSpot.AirDate)) - 1)
                        .Daypart = TmpActualSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpActualSpot.MaM).Name
                        .Film = TmpActualSpot.Week.Films(TmpActualSpot.Filmcode).Name
                        .Filmcode = TmpActualSpot.Filmcode
                        .Status = "Actual"

                        .TRPMain = TmpActualSpot.Rating
                        .TRPSecond = TmpActualSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget)
                        .TRPBuying = TmpActualSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget)
                        .TRPMain30 = TmpActualSpot.Rating / (TmpActualSpot.Week.Films(TmpActualSpot.Filmcode).Index / 100)
                        .TRPBuying30 = TmpActualSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget) / (TmpActualSpot.Week.Films(TmpActualSpot.Filmcode).Index / 100)

                        ._000Main = TmpActualSpot.Rating000 * 1000
                        ._000Buying = TmpActualSpot.Rating000(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget) * 1000

                        .BudgetNet = TmpActualSpot.ActualNetValue
                        .BudgetGross = TmpActualSpot.ActualGrossValue
                    End With
                    'Record = Records.Add

                    'Record.Channel = TmpActualSpot.Channel.ChannelName
                    'Record.BookingType = TmpActualSpot.Bookingtype.Name
                    'Record.Week = TmpActualSpot.week.Name
                    'Record.Weekday = WeekDays(Weekday(Date.FromOADate(TmpActualSpot.MaM), FirstDayOfWeek.Monday) - 1) 'TmpActualSpot.AirDate
                    'Record.Month = Months(Month(Date.FromOADate(TmpActualSpot.AirDate)) - 1)
                    'Record.Daypart = TmpActualSpot.Bookingtype.Dayparts.GetDaypartForMam(TmpActualSpot.MaM).Name
                    'Record.Film = TmpActualSpot.week.Films(TmpActualSpot.Filmcode).Name
                    'Record.Filmcode = TmpActualSpot.Filmcode
                    'Record.Status = "Actual"

                    'Record.TRPMain = TmpActualSpot.Rating
                    'Record.TRPSecond = TmpActualSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget)
                    'Record.TRPBuying = TmpActualSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget)
                    'Record.TRPMain30 = TmpActualSpot.Rating / (TmpActualSpot.week.Films(TmpActualSpot.Filmcode).Index / 100)
                    'Record.TRPBuying30 = TmpActualSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget) / (TmpActualSpot.week.Films(TmpActualSpot.Filmcode).Index / 100)

                    'Record._000Main = TmpActualSpot.Rating000 * 1000
                    'Record._000Buying = TmpActualSpot.Rating000(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget) * 1000

                    'Record.BudgetNet = TmpActualSpot.ActualNetValue
                    'Record.BudgetGross = TmpActualSpot.ActualGrossValue
                End If
            End If
        Next
        _progress.Hide()
    End Sub

    Private Sub frmPivotTableDevEx_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'List all available fields
        availableFields.Add("Bookingtype")
        availableFields.Add("Channel")
        availableFields.Add("Daypart")
        availableFields.Add("Film")
        availableFields.Add("Filmcode")
        availableFields.Add("Month")
        availableFields.Add("Status")
        availableFields.Add("Unit")
        availableFields.Add("Week")
        availableFields.Add("Weekday")

        'Added to include combinations
        availableFields.Add("Combination")

        'List all available units
        availableUnits.Add("CPT Main")
        availableUnits.Add("CPT Main Gross")
        availableUnits.Add("CPT Buying")
        availableUnits.Add("CPT Buying Gross")
        availableUnits.Add("CPP Main")
        availableUnits.Add("CPP Main Gross")
        availableUnits.Add("CPP30 Main")
        availableUnits.Add("CPP Buying")
        availableUnits.Add("CPP Buying Gross")
        availableUnits.Add("CPP30 Buying")
        availableUnits.Add("CPP Second")
        availableUnits.Add("CPP Second Gross")
        availableUnits.Add("CPP30 Second")
        availableUnits.Add("Budget Net")
        availableUnits.Add("Budget Gross")
        availableUnits.Add("Discount")
        availableUnits.Add("TRP Main")
        availableUnits.Add("TRP Buying")
        availableUnits.Add("TRP Second")
        availableUnits.Add("'000 Main")
        availableUnits.Add("'000 Buying")

        'Add the fldNames
        FieldTranslation.Add("Bookingtype", "fldBookingtype")
        FieldTranslation.Add("Channel", "fldChannel")
        FieldTranslation.Add("Daypart", "fldDaypart")
        FieldTranslation.Add("Film", "fldFilm")
        FieldTranslation.Add("Filmcode", "fldFilmcode")
        FieldTranslation.Add("Status", "fldStatus")
        FieldTranslation.Add("Unit", "fldUnit")
        FieldTranslation.Add("Value", "fldValue")
        FieldTranslation.Add("Week", "fldWeek")
        FieldTranslation.Add("Weekday", "fldWeekday")
        FieldTranslation.Add("Month", "fldMonth")

        'Added to include combinations
        FieldTranslation.Add("Combination", "fldCombination")

        FieldTranslation.Add("CPT Main", "fldCPTMain")
        FieldTranslation.Add("CPT Main Gross", "fldCPTMainGross")
        FieldTranslation.Add("CPT Buying", "fldCPTBuy")
        FieldTranslation.Add("CPT Buying Gross", "fldCPTBuyingGross")
        FieldTranslation.Add("CPP Main", "fldCPPMain")
        FieldTranslation.Add("CPP Main Gross", "fldCPPMainGross")
        FieldTranslation.Add("CPP30 Main", "fldCPPMain30")
        FieldTranslation.Add("CPP Buying", "fldCPPBuy")
        FieldTranslation.Add("CPP Buying Gross", "fldCPPBuyingGross")
        FieldTranslation.Add("CPP30 Buying", "fldCPPBuy30")
        FieldTranslation.Add("CPP Second", "fldCPPSecond")
        FieldTranslation.Add("CPP Second Gross", "fldCPPSecondGross")
        FieldTranslation.Add("CPP30 Second", "fldCPPSecond30")
        FieldTranslation.Add("Budget Net", "fldBudNet")
        FieldTranslation.Add("Budget Gross", "fldBudGrs")
        FieldTranslation.Add("Discount", "fldDiscount")
        FieldTranslation.Add("TRP Main", "fldTRPMian")
        FieldTranslation.Add("TRP Buying", "fldTRPBuy")
        FieldTranslation.Add("TRP Second", "fldTRPSec")
        FieldTranslation.Add("'000 Main", "fld000M")
        FieldTranslation.Add("'000 Buying", "fld000B")

        'Weekday names
        WeekDays.Add("Monday")
        WeekDays.Add("Tuesday")
        WeekDays.Add("Wednesday")
        WeekDays.Add("Thursday")
        WeekDays.Add("Friday")
        WeekDays.Add("Saturday")
        WeekDays.Add("Sunday")

        'Month Names
        Months.Add("Jan")
        Months.Add("Feb")
        Months.Add("Mar")
        Months.Add("Apr")
        Months.Add("May")
        Months.Add("Jun")
        Months.Add("Jul")
        Months.Add("Aug")
        Months.Add("Sep")
        Months.Add("Oct")
        Months.Add("Nov")
        Months.Add("Dec")

        'Custom sorting on Channels
        ChannelSortList = New Dictionary(Of String, Integer)
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            ChannelSortList.Add(TmpChan.ChannelName, TmpChan.ListNumber)
        Next

        'create the base data used in hte pivot
        createBaseRecords()

        'Add the default Pivot
        pvtTable.DataSource = Nothing
        pvtTable.Fields.Clear()
        With pvtTable.Fields.Add("Value", DevExpress.XtraPivotGrid.PivotArea.DataArea)
            .Options.ShowGrandTotal = False
            .Caption = "Value"
            .Name = "fldValue"
            .AllowedAreas = DevExpress.XtraPivotGrid.PivotGridAllowedAreas.DataArea
            .TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
            .SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom
        End With

        Dim Dimensions As New List(Of String)

        'Not all have a saved default
        If TrinitySettings.getPivotDefaultColumns.Count > 0 OrElse TrinitySettings.getPivotDefaultRows.Count > 0 Then
            For Each s As String In TrinitySettings.getPivotDefaultColumns
                With pvtTable.Fields.Add(s, DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
                    .Options.ShowGrandTotal = False
                    .Caption = s
                    .Name = FieldTranslation(s)
                    If s = "Channel" Then
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom
                    ElseIf s = "Week" Then
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Value
                    ElseIf s = "Weekday" Then
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom
                    Else
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Default
                    End If
                    .AllowedAreas = DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea + DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea
                    .TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
                End With
                Dimensions.Add(s)
            Next

            For Each s As String In TrinitySettings.getPivotDefaultRows
                With pvtTable.Fields.Add(s, DevExpress.XtraPivotGrid.PivotArea.RowArea)
                    .Options.ShowGrandTotal = False
                    .Caption = s
                    .Name = FieldTranslation(s)
                    If s = "Channel" Then
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom
                    ElseIf s = "Week" Then
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Value
                    ElseIf s = "Weekday" Then
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom
                    Else
                        .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Default
                    End If
                    .AllowedAreas = DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea + DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea
                    .TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
                End With
                Dimensions.Add(s)
            Next

            Dim tmpObj(availableUnits.Count - TrinitySettings.getPivotDefaultUnits.Count - 1) As Object
            Dim count As Integer = 0
            Dim l As List(Of String) = TrinitySettings.getPivotDefaultUnits
            For Each s As String In availableUnits
                If Not l.Contains(s) Then
                    tmpObj(count) = s
                    count += 1
                End If
            Next
            pvtTable.Fields.GetFieldByName("fldUnit").FilterValues.SetValues(tmpObj, DevExpress.Data.PivotGrid.PivotFilterType.Excluded, True)

        Else
            With pvtTable.Fields.Add("Unit", DevExpress.XtraPivotGrid.PivotArea.RowArea)
                .Options.ShowGrandTotal = False
                .Caption = "Unit"
                .Name = "fldUnit"
                .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom
                .AllowedAreas = DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea + DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea
                .TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
            End With
            With pvtTable.Fields.Add("Status", DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
                .Options.ShowGrandTotal = False
                .Caption = "Status"
                .Name = FieldTranslation("Status")
                .SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Default
                .SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending
                .AllowedAreas = DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea + DevExpress.XtraPivotGrid.PivotGridAllowedAreas.ColumnArea
                .TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
            End With

            Dimensions.Add("Status")

            Dim tmpObj() As Object = {"TRP Main", "Budget Net"}
            pvtTable.Fields.GetFieldByName("fldUnit").FilterValues.SetValues(tmpObj, DevExpress.Data.PivotGrid.PivotFilterType.Included, True)
        End If
        
        pvtTable.DataSource = CreateDataSource(Dimensions)

        chtPivot.ChartAreas("ChartArea1").AxisY.IntervalAutoMode = Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount
        chtPivot.ChartAreas("ChartArea1").AxisX.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep30
        chtPivot.ChartAreas("ChartArea1").AxisX.Interval = 1
    End Sub

    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        Dim Excel As CultureSafeExcel.Application = New CultureSafeExcel.Application(False)
        Try
            Excel.ScreenUpdating = False
            Dim Filename As String = My.Computer.FileSystem.GetTempFileName & ".xls"
            pvtTable.ExportToXls(Filename)
            Excel.OpenWorkbook(Filename)
            Excel.screenupdating = True
            Excel.visible = True
        Catch
            If Excel IsNot Nothing Then
                Excel.quit()
            End If
        End Try
    End Sub

    Private Sub cmdPictureToCB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPictureToCB.Click
        Dim bmp As Bitmap
        Try

            Dim Filename As String = My.Computer.FileSystem.GetTempFileName & ".bmp"
            chtPivot.SaveImage(Filename, Windows.Forms.DataVisualization.Charting.ChartImageFormat.Jpeg)

            bmp = New Bitmap(Filename)

            My.Computer.Clipboard.SetImage(bmp)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub createChart()

        'Get the fields for the series from the pivot table
        Dim series As New List(Of List(Of String))
        Dim seriesLabelPositions As New List(Of Integer)
        Dim seriesLabelMaxPositions As New List(Of Integer)

        Dim axis As New List(Of List(Of String))
        Dim axisLabelPositions As New List(Of Integer)
        Dim axisLabelMaxPositions As New List(Of Integer)

     
        Dim tempList As List(Of String)
        For Each f As DevExpress.XtraPivotGrid.PivotGridField In pvtTable.Fields

            tempList = processPivotFieldForCharting(f)

            If f.IsColumn Then
                series.Add(tempList)
                seriesLabelMaxPositions.Add(tempList.Count)
                seriesLabelPositions.Add(0)
            ElseIf f.IsColumnOrRow Then
                axis.Add(tempList)
                axisLabelMaxPositions.Add(tempList.Count)
                axisLabelPositions.Add(0)
            End If
        Next

        'clear the chart from existing data
        chtPivot.Series.Clear()

        'chtPivot.ChartAreas("ChartArea1").Area3DStyle.PointDepth = 10
        'chtPivot.ChartAreas("ChartArea1").Area3DStyle.Rotation = 50
        'chtPivot.ChartAreas("ChartArea1").Area3DStyle.Perspective = 0
        'chtPivot.ChartAreas("ChartArea1").Area3DStyle.Inclination = 0
        'chtPivot.ChartAreas("ChartArea1").BackColor = Drawing.Color.White
        'chtPivot.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True

        'variable for getting series name from list
        Dim c As Integer = 0
        Dim r As Integer = 0
        Dim tempSeriesName = ""
        Dim tempAxisName = ""

        Dim axisNamesList As New List(Of String)

        For c = 0 To pvtTable.Cells.RowCount - 1
            tempAxisName = ""
            For i As Integer = 0 To axis.Count - 1
                tempAxisName += "/" & axis(i)(axisLabelPositions(i))
            Next
            axisNamesList.Add(tempAxisName.Substring(1))

            'update position in axisLabelPositions
            axisLabelPositions = updateListofInt(axisLabelPositions, axisLabelPositions.Count - 1, axisLabelMaxPositions)
        Next


        For r = 0 To pvtTable.Cells.ColumnCount - 1
            tempSeriesName = ""



            If series.Count = 0 Then

            Else
                For i As Integer = 0 To series.Count - 1
                    tempSeriesName += "/" & series(i)(seriesLabelPositions(i))
                Next
                tempSeriesName = tempSeriesName.Substring(1)
            End If

            'Add a new series collection
            Dim serie As Windows.Forms.DataVisualization.Charting.Series = chtPivot.Series.Add(tempSeriesName)
            serie.BackSecondaryColor = Color.Coral
            serie.BackGradientStyle = Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
            serie.BackSecondaryColor = Color.Gray



            serie.Color = Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(TrinitySettings.DefaultColorScheme, "Diagram" & ((r + 1) Mod 10))) 'set custom color



            'Add all values in the serie
            For c = 0 To pvtTable.Cells.RowCount - 1
                'serie.Points.AddXY(c, pvtTable.AllCells.GetCellInfo(r, c).Value)
                serie.Points.AddXY(axisNamesList(c), pvtTable.Cells.GetCellInfo(r, c).Value)
            Next

            chtPivot.ChartAreas("ChartArea1").AxisY.IntervalAutoMode = Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount

            'update position in seriesLabelPositions
            seriesLabelPositions = updateListofInt(seriesLabelPositions, seriesLabelPositions.Count - 1, seriesLabelMaxPositions)
        Next



    End Sub

    Private Function processPivotFieldForCharting(ByVal f As DevExpress.XtraPivotGrid.PivotGridField) As List(Of String)
        Dim tempList As New List(Of String)
        Try
            If WeekDays.Contains(f.FilterValues.ValuesIncluded.ElementAt(0)) Then
                tempList = WeekDays
                If f.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending Then
                    tempList.Reverse()
                End If
            ElseIf ChannelSortList.ContainsKey(f.FilterValues.ValuesIncluded.ElementAt(0)) Then
                Dim IsReverse As Boolean = False
                Dim channelList As New List(Of Integer)

                For i As Integer = 0 To f.FilterValues.ValuesIncluded.Count - 1
                    channelList.Add(ChannelSortList(f.FilterValues.ValuesIncluded.ElementAt(i)))
                Next

                channelList.Sort()
                If f.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending Then
                    channelList.Reverse()
                End If

                'Riktig fullösning med linq
                Dim l As New List(Of String)
                For Each i As Integer In channelList
                    l.Add((From k In ChannelSortList Where k.Value = i Select k.Key).FirstOrDefault())
                Next

                tempList = l
            Else
                For i As Integer = 0 To f.FilterValues.ValuesIncluded.Count - 1
                    tempList.Add(f.FilterValues.ValuesIncluded.ElementAt(i))
                Next

                tempList.Sort()
                If f.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending Then
                    tempList.Reverse()
                End If
            End If
        Catch ex As Exception
            If Not ex.GetType Is GetType(ArgumentException) Then
                Throw ex
            End If
        End Try
 

        Return tempList
    End Function

    Private Function updateListofInt(ByVal l As List(Of Integer), ByVal position As Integer, ByVal maxValues As List(Of Integer))
        If position = -1 Then Return l

        'update the list
        l(position) = l(position) + 1

        'check if we reached max value
        If l(position) >= maxValues(position) Then
            'update list
            l = updateListofInt(l, position - 1, maxValues)
            'reset current postition
            l(position) = 0
        End If

        Return l
    End Function

    Private Sub TabPage2_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage2.Enter
        cmdExcel.Enabled = False
        cmdDefine.Enabled = False
        cmdPictureToCB.Enabled = True
        'Try
        createChart()
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub TabPage1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage1.Enter
        cmdExcel.Enabled = True
        cmdDefine.Enabled = True
        cmdPictureToCB.Enabled = False
    End Sub

    Private Class PivotRecords
        Implements IEnumerable(Of PivotRecord)

        Private Records As New List(Of PivotRecord)

        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of PivotRecord) Implements System.Collections.Generic.IEnumerable(Of PivotRecord).GetEnumerator
            Return Records.GetEnumerator
        End Function

        Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Records.GetEnumerator
        End Function

        Private _showChannels As Boolean
        Public Property ShowChannels() As Boolean
            Get
                Return _showChannels
            End Get
            Set(ByVal value As Boolean)
                _showChannels = value
            End Set
        End Property

        Private _showMonths As Boolean
        Public Property ShowMonths() As Boolean
            Get
                Return _showMonths
            End Get
            Set(ByVal value As Boolean)
                _showMonths = value
            End Set
        End Property

        Private _showCombinations As Boolean
        Public Property ShowCombinations() As Boolean
            Get
                Return _showCombinations
            End Get
            Set(ByVal value As Boolean)
                _showCombinations = value
            End Set
        End Property

        Private _showWeeks As Boolean
        Public Property ShowWeeks() As Boolean
            Get
                Return _showWeeks
            End Get
            Set(ByVal value As Boolean)
                _showWeeks = value
            End Set
        End Property

        Private _showWeekDays As Boolean
        Public Property ShowWeekDays() As Boolean
            Get
                Return _showWeekDays
            End Get
            Set(ByVal value As Boolean)
                _showWeekDays = value
            End Set
        End Property

        Private _showDayParts As Boolean
        Public Property ShowDayParts() As Boolean
            Get
                Return _showDayParts
            End Get
            Set(ByVal value As Boolean)
                _showDayParts = value
            End Set
        End Property

        Private _showBookingtypeNames As Boolean
        Public Property ShowBookingtypeNames() As Boolean
            Get
                Return _showBookingtypeNames
            End Get
            Set(ByVal value As Boolean)
                _showBookingtypeNames = value
            End Set
        End Property

        Private _showFilms As Boolean
        Public Property ShowFilms() As Boolean
            Get
                Return _showFilms
            End Get
            Set(ByVal value As Boolean)
                _showFilms = value
            End Set
        End Property

        Private _showFilmCodes As Boolean
        Public Property ShowFilmCodes() As Boolean
            Get
                Return _showFilmCodes
            End Get
            Set(ByVal value As Boolean)
                _showFilmCodes = value
            End Set
        End Property

        Private _showStatuses As Boolean
        Public Property ShowStatuses() As Boolean
            Get
                Return _showStatuses
            End Get
            Set(ByVal value As Boolean)
                _showStatuses = value
            End Set
        End Property

        Function Add() As PivotRecord
            Dim Record As New PivotRecord(Me)
            Records.Add(Record)
            Return Record
        End Function

    End Class

    Private Class PivotRecord
        Private _parent As PivotRecords

        Public Sub New(ByVal Parent As PivotRecords)
            _parent = Parent
        End Sub

#Region "Fields"
        Private _channel As String
        Public Property Channel() As String
            Get
                If Not _parent.ShowChannels Then Return ""
                Return _channel
            End Get
            Set(ByVal value As String)
                _channel = value
            End Set
        End Property

        Private _bookingType As String
        Public Property BookingType() As String
            Get
                If Not _parent.ShowBookingtypeNames Then Return ""
                Return _bookingType
            End Get
            Set(ByVal value As String)
                _bookingType = value
            End Set
        End Property

        Private _combination As String
        Public Property Combination() As String
            Get
                If Not _parent.ShowCombinations Then Return ""
                Return _combination
            End Get
            Set(ByVal value As String)
                _combination = value
            End Set
        End Property

        Private _month As String
        Public Property Month() As String
            Get
                If Not _parent.ShowMonths Then Return ""
                Return _month
            End Get
            Set(ByVal value As String)
                _month = value
            End Set
        End Property

        Private _week As String
        Public Property Week() As String
            Get
                If Not _parent.ShowWeeks Then Return ""
                Return _week
            End Get
            Set(ByVal value As String)
                _week = value
            End Set
        End Property

        Private _weekDay As String
        Public Property Weekday() As String
            Get
                If Not _parent.ShowWeekDays Then Return ""
                Return _weekDay
            End Get
            Set(ByVal value As String)
                _weekDay = value
            End Set
        End Property

        Private _film As String
        Public Property Film() As String
            Get
                If Not _parent.ShowFilms Then Return ""
                Return _film
            End Get
            Set(ByVal value As String)
                _film = value
            End Set
        End Property

        Private _filmcode As String
        Public Property Filmcode() As String
            Get
                If Not _parent.ShowFilmCodes Then Return ""
                Return _filmcode
            End Get
            Set(ByVal value As String)
                _filmcode = value
            End Set
        End Property

        Private _daypart As String
        Public Property Daypart() As String
            Get
                If Not _parent.ShowDayParts Then Return ""
                Return _daypart
            End Get
            Set(ByVal value As String)
                _daypart = value
            End Set
        End Property

        Private _status As String
        Public Property Status() As String
            Get
                If Not _parent.ShowStatuses Then Return ""
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property

#End Region

#Region "Values"

        Private _budgetNet As Double
        Public Property BudgetNet() As Double
            Get
                Return _budgetNet
            End Get
            Set(ByVal value As Double)
                _budgetNet = value
            End Set
        End Property

        Private _budgetGross As Double
        Public Property BudgetGross() As Double
            Get
                Return _budgetGross
            End Get
            Set(ByVal value As Double)
                _budgetGross = value
            End Set
        End Property

        Private _TRPMain As Double
        Public Property TRPMain() As Double
            Get
                Return _TRPMain
            End Get
            Set(ByVal value As Double)
                _TRPMain = value
            End Set
        End Property

        Private _TRPMain30 As Double
        Public Property TRPMain30() As Double
            Get
                Return _TRPMain30
            End Get
            Set(ByVal value As Double)
                _TRPMain30 = value
            End Set
        End Property

        Private _TRPBuying As Double
        Public Property TRPBuying() As Double
            Get
                Return _TRPBuying
            End Get
            Set(ByVal value As Double)
                _TRPBuying = value
            End Set
        End Property

        Private _TRPBuying30 As Double
        Public Property TRPBuying30() As Double
            Get
                Return _TRPBuying30
            End Get
            Set(ByVal value As Double)
                _TRPBuying30 = value
            End Set
        End Property

        Private _TRPSecond As Double
        Public Property TRPSecond() As Double
            Get
                Return _TRPSecond
            End Get
            Set(ByVal value As Double)
                _TRPSecond = value
            End Set
        End Property

        Private _TRPSecond30 As Double
        Public Property TRPSecond30() As Double
            Get
                Return _TRPSecond30
            End Get
            Set(ByVal value As Double)
                _TRPSecond30 = value
            End Set
        End Property

        Private __000Main As Double
        Public Property _000Main() As Double
            Get
                Return __000Main
            End Get
            Set(ByVal value As Double)
                __000Main = value
            End Set
        End Property

        Private __000Buying As Double
        Public Property _000Buying() As Double
            Get
                Return __000Buying
            End Get
            Set(ByVal value As Double)
                __000Buying = value
            End Set
        End Property

#End Region
    End Class

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class