Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel

Public Class frmPivot
    Inherits Control

    Private Class ColumnEdges
        Public LeftColumn As Integer
        Public RightColumn As Integer
    End Class

    Private Class CellValues
        Public Sum As Single
        Public Count As Single
        Public PivotTotalFunction As PivotTotalFunctionEnum
        Public NumberFormat As String

        Public Function Average() As Single
            Return Sum / Count
        End Function

    End Class

    Public Enum PivotTotalFunctionEnum
        plFunctionSum = 1
        plFunctionAvg = 2
        plcFunctionCount = 3
    End Enum

    Private _fieldSets As New PivotFieldSets(Me)
    Private _data As New DataTable
    Private _rowAxis As New PivotAxis(Me)
    Private _columnAxis As New PivotAxis(Me)
    Private _total As New PivotTotal
    Dim hscroll As New HScrollBar
    Dim vscroll As New VScrollBar

    Sub Clear()
        _data.Clear()
        _fieldSets.Clear()
    End Sub

    Private _scrollRows As Integer
    Public Property ScrollRows() As Integer
        Get
            Return _scrollRows
        End Get
        Set(ByVal value As Integer)
            _scrollRows = value
        End Set
    End Property

    Private _scrollColumns As Integer
    Public Property ScrollColumns() As Integer
        Get
            Return _scrollColumns
        End Get
        Set(ByVal value As Integer)
            _scrollColumns = value
        End Set
    End Property

    Public Property Total() As PivotTotal
        Get
            Return _total
        End Get
        Set(ByVal value As PivotTotal)
            _total = value
        End Set
    End Property

    Sub AddNew(ByVal Fields() As String, ByVal Values() As Object)
        Dim TmpRow As DataRow = _data.NewRow
        For i As Integer = 0 To Fields.GetUpperBound(0)
            If Not _fieldSets.ContainsKey(Fields(i)) Then
                AddField(Fields(i), Values(i).GetType)
            End If
            TmpRow.Item(Fields(i)) = Values(i)
            If Not _fieldSets(Fields(i)).Fields.Contains(Values(i)) Then
                _fieldSets(Fields(i)).Fields.Add(Values(i))
            End If
        Next
        _data.Rows.Add(TmpRow)
    End Sub

    Private Sub AddField(ByVal FieldName As String, ByVal Type As Type)
        Dim TmpFieldSet As New PivotFieldSet
        _data.Columns.Add(FieldName, Type)
        TmpFieldSet.Caption = FieldName
        TmpFieldSet.Name = FieldName
        _fieldSets.Add(TmpFieldSet)
    End Sub

    Public ReadOnly Property RowAxis() As PivotAxis
        Get
            Return _rowAxis
        End Get
    End Property

    Public ReadOnly Property ColumnAxis() As PivotAxis
        Get
            Return _columnAxis
        End Get
    End Property

    Public ReadOnly Property FieldSets() As PivotFieldSets
        Get
            Return _fieldSets
        End Get
    End Property

    Private Sub HeadlineItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs)

    End Sub

    Private Sub HeadlineDragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)

    End Sub

    Private Sub HeadlineDragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)

    End Sub

    Private Sub HeadlineDragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)

    End Sub

    Overrides Sub Refresh()

        If _fieldSets.Count = 0 OrElse _total.FieldSet Is Nothing OrElse _total.TotalFields.Count = 0 Then Exit Sub

        While Me.Controls.Count > 2
            For Each TmpCtrl As Control In Me.Controls
                If Not TmpCtrl Is hscroll AndAlso Not TmpCtrl Is vscroll Then
                    If TmpCtrl.Controls.Count > 0 Then
                        For Each TmpCtrl2 As Control In TmpCtrl.Controls
                            TmpCtrl.Controls.Remove(TmpCtrl2)
                            TmpCtrl2.Dispose()
                        Next
                    End If
                    Me.Controls.Remove(TmpCtrl)
                    TmpCtrl.Dispose()
                End If
            Next
        End While

        Dim _grid As New DataGridView
        Me.Controls.Add(_grid)

        Dim TmpColumnList As New List(Of PivotFieldSet)
        For Each kv As KeyValuePair(Of String, PivotFieldSet) In _columnAxis.FieldSets
            TmpColumnList.Add(kv.Value)
        Next
        Dim TmpRowList As New List(Of PivotFieldSet)
        For Each kv As KeyValuePair(Of String, PivotFieldSet) In _rowAxis.FieldSets
            TmpRowList.Add(kv.Value)
        Next
        _grid.RowHeadersVisible = False
        _grid.ColumnHeadersVisible = False
        _grid.AllowUserToAddRows = False
        _grid.AllowUserToDeleteRows = False
        _grid.AllowUserToOrderColumns = False
        _grid.AllowUserToResizeColumns = False
        _grid.AllowUserToResizeRows = False
        _grid.ScrollBars = ScrollBars.None
        _grid.ReadOnly = True
        _grid.Width = 10000
        _grid.Height = 10000
        _grid.BorderStyle = BorderStyle.None
        For i As Integer = 0 To TmpRowList.Count - 1
            _grid.Columns.Add("col" & TmpRowList(i).Name, "")
        Next
        _grid.Columns.Add(CreateGUID, "")
        For i As Integer = 0 To TmpColumnList.Count - 1
            _grid.Rows.Add()
        Next
        AddColumnsToGrid(_grid, TmpColumnList)
        _grid.Columns.Remove(_grid.Columns(_grid.Columns.Count - 1))

        _grid.Rows.Add()
        AddRowsToGrid(_grid, TmpRowList)
        _grid.Rows.Remove(_grid.Rows(_grid.Rows.Count - 1))

        For i As Integer = 0 To TmpRowList.Count - 1
            _grid.Columns(i).Visible = False
        Next
        For i As Integer = 0 To TmpColumnList.Count - 1
            _grid.Rows(i).Visible = False
        Next

        hscroll.Top = Me.Height - hscroll.Height
        hscroll.Left = 0
        hscroll.Width = Me.Width - vscroll.Width
        hscroll.Minimum = 0
        hscroll.Maximum = _grid.Columns.Count - TmpRowList.Count - 1
        hscroll.Value = _scrollColumns
        hscroll.SmallChange = 1
        hscroll.BringToFront()
        hscroll.LargeChange = 1
        AddHandler hscroll.Scroll, AddressOf GridScroll
        hscroll.Visible = True
        _grid.Height = _grid.Height - hscroll.Height

        vscroll.Top = 0
        vscroll.Left = Me.Width - vscroll.Width
        vscroll.Height = Me.Height - hscroll.Height
        vscroll.Minimum = 0
        vscroll.Maximum = _grid.Rows.Count - TmpColumnList.Count - 1
        vscroll.Value = _scrollRows
        vscroll.SmallChange = 1
        vscroll.BringToFront()
        vscroll.LargeChange = 1
        AddHandler vscroll.Scroll, AddressOf GridScroll
        vscroll.Visible = True
        _grid.Width = _grid.Width - vscroll.Width

        For i As Integer = TmpRowList.Count To TmpRowList.Count + _scrollColumns - 1
            _grid.Columns.RemoveAt(TmpRowList.Count)
        Next

        For i As Integer = TmpColumnList.Count To TmpColumnList.Count + _scrollRows - 1
            _grid.Rows.RemoveAt(TmpColumnList.Count)
        Next

        SortData()
        For Each TmpRow As DataRow In _data.Rows
            Dim TmpData As New CellValues
            If _grid.Rows(FindRow(_grid, TmpRow, TmpRowList)).Cells(FindColumn(_grid, TmpRow, TmpColumnList)).Tag Is Nothing Then
                _grid.Rows(FindRow(_grid, TmpRow, TmpRowList)).Cells(FindColumn(_grid, TmpRow, TmpColumnList)).Tag = TmpData
            End If
            TmpData = _grid.Rows(FindRow(_grid, TmpRow, TmpRowList)).Cells(FindColumn(_grid, TmpRow, TmpColumnList)).Tag
            TmpData.Count += 1
            TmpData.Sum += TmpRow!value
            TmpData.PivotTotalFunction = _total.TotalFields(TmpRow.Item(_total.FieldSet.Name)).TotalFunction
            TmpData.NumberFormat = _total.TotalFields(TmpRow.Item(_total.FieldSet.Name)).NumberFormat
        Next
        For r As Integer = TmpColumnList.Count To _grid.Rows.Count - 1
            For c As Integer = TmpRowList.Count To _grid.Columns.Count - 1
                Dim TmpData As CellValues = _grid.Rows(r).Cells(c).Tag
                If TmpData Is Nothing Then
                    _grid.Rows(r).Cells(c).Value = ""
                Else
                    Select Case TmpData.PivotTotalFunction
                        Case PivotTotalFunctionEnum.plcFunctionCount
                            _grid.Rows(r).Cells(c).Value = Format(TmpData.Count, TmpData.NumberFormat)
                        Case PivotTotalFunctionEnum.plFunctionAvg
                            _grid.Rows(r).Cells(c).Value = Format(TmpData.Average, TmpData.NumberFormat)
                        Case PivotTotalFunctionEnum.plFunctionSum
                            _grid.Rows(r).Cells(c).Value = Format(TmpData.Sum, TmpData.NumberFormat)
                    End Select
                End If
                _grid.AutoResizeColumn(c, DataGridViewAutoSizeColumnMode.AllCells)
            Next
        Next

        Dim TmpLabel As Label = Nothing

        Dim RowPanel As New Panel
        RowPanel.Height = 20 * (_columnAxis.FieldSets.Count + 1)
        Dim LastRight As Integer = 1
        For r As Integer = 0 To _columnAxis.FieldSets.Count - 1
            Dim c As Integer = _rowAxis.FieldSets.Count
            Dim TmpHeadline As New Label
            Dim TmpGfx As Graphics = RowPanel.CreateGraphics
            Dim LastString As String = ""
            TmpHeadline.BorderStyle = BorderStyle.Fixed3D
            TmpHeadline.BackColor = Color.DarkBlue
            TmpHeadline.ForeColor = Color.White
            TmpHeadline.Text = TmpColumnList(r).Caption
            TmpHeadline.Width = TmpGfx.MeasureString(TmpHeadline.Text, TmpHeadline.Font).Width + 5
            TmpHeadline.Height = 20
            TmpHeadline.TextAlign = ContentAlignment.MiddleCenter
            TmpHeadline.Left = LastRight + 1
            TmpHeadline.AllowDrop = True
            LastRight = TmpHeadline.Right
            If RowPanel.Width < TmpHeadline.Right + 1 Then
                RowPanel.Width = TmpHeadline.Right + 1
            End If
            RowPanel.Controls.Add(TmpHeadline)
            While c < _grid.Columns.Count
                If _grid.Rows(r).Cells(c).Value <> LastString Then
                    LastString = _grid.Rows(r).Cells(c).Value
                    TmpLabel = New Label
                    TmpLabel.Tag = New ColumnEdges
                    TmpLabel.Width = _grid.Columns(c).Width
                    TmpLabel.Text = _grid.Rows(r).Cells(c).Value
                    TmpLabel.BackColor = Color.LightBlue
                    RowPanel.Controls.Add(TmpLabel)
                    If TmpGfx.MeasureString(TmpLabel.Text, TmpLabel.Font).Width + 5 > _grid.GetColumnDisplayRectangle(c, False).Width Then
                        _grid.Columns(c).Width = TmpGfx.MeasureString(TmpLabel.Text, TmpHeadline.Font).Width + 5
                    End If
                    TmpLabel.Height = 20
                    TmpLabel.Top = (r + 1) * 20
                    TmpLabel.BorderStyle = BorderStyle.Fixed3D
                    TmpLabel.TextAlign = ContentAlignment.MiddleCenter
                    TmpLabel.Left = _grid.GetColumnDisplayRectangle(c, True).Left
                    DirectCast(TmpLabel.Tag, ColumnEdges).LeftColumn = c
                End If
                DirectCast(TmpLabel.Tag, ColumnEdges).RightColumn = c
                c += 1
            End While
        Next
        Me.Controls.Add(RowPanel)

        Dim ColumnsPanel As New Panel
        For c As Integer = 0 To _rowAxis.FieldSets.Count - 1
            Dim r As Integer = _columnAxis.FieldSets.Count
            Dim LastRow = _columnAxis.FieldSets.Count
            Dim TmpPanel As New Panel
            Dim TmpHeadline As New Label
            Dim LastString As String = ""
            Dim TmpGfx As Graphics = TmpPanel.CreateGraphics
            TmpPanel.Name = "Panel" & c
            TmpPanel.Width = 1
            TmpPanel.Height = 10000
            TmpPanel.BorderStyle = BorderStyle.None
            TmpHeadline.BorderStyle = BorderStyle.Fixed3D
            TmpHeadline.BackColor = Color.DarkBlue
            TmpHeadline.ForeColor = Color.White
            TmpHeadline.Text = TmpRowList(c).Caption
            If TmpGfx.MeasureString(TmpHeadline.Text, TmpHeadline.Font).Width + 5 > TmpPanel.Width Then
                TmpPanel.Width = TmpGfx.MeasureString(TmpHeadline.Text, TmpHeadline.Font).Width + 5
            End If
            TmpHeadline.Width = TmpPanel.Width
            TmpHeadline.Anchor = AnchorStyles.Left + AnchorStyles.Right
            TmpHeadline.Height = 20
            TmpHeadline.TextAlign = ContentAlignment.MiddleLeft
            TmpPanel.Controls.Add(TmpHeadline)
            While r < _grid.Rows.Count
                If _grid.Rows(r).Cells(c).Value <> LastString Then
                    LastString = _grid.Rows(r).Cells(c).Value
                    TmpLabel = New Label
                    TmpLabel.Width = TmpPanel.Width
                    TmpLabel.Anchor = AnchorStyles.Left + AnchorStyles.Right
                    TmpLabel.Text = _grid.Rows(r).Cells(c).Value
                    TmpLabel.BackColor = Color.LightBlue
                    TmpPanel.Controls.Add(TmpLabel)
                    If TmpGfx.MeasureString(TmpLabel.Text, TmpLabel.Font).Width + 5 > TmpPanel.Width Then
                        TmpPanel.Width = TmpGfx.MeasureString(TmpLabel.Text, TmpLabel.Font).Width + 5
                    End If
                    TmpLabel.Height = 0
                    TmpLabel.Top = _grid.GetRowDisplayRectangle(r, False).Top + TmpHeadline.Height
                    TmpLabel.BorderStyle = BorderStyle.Fixed3D
                End If
                TmpLabel.Height = TmpLabel.Height + _grid.GetRowDisplayRectangle(r, True).Height
                TmpLabel.TextAlign = ContentAlignment.MiddleLeft
                r += 1
            End While
            If c = 0 Then
                TmpPanel.Left = 0
            Else
                TmpPanel.Left = ColumnsPanel.Controls("Panel" & c - 1).Right + 1
            End If
            TmpPanel.Top = 0
            ColumnsPanel.Controls.Add(TmpPanel)
            TmpGfx.Dispose()
            If ColumnsPanel.Height < TmpLabel.Bottom + 1 Then
                ColumnsPanel.Height = TmpLabel.Bottom + 1
            End If
        Next
        ColumnsPanel.Width = ColumnsPanel.Controls("Panel" & _rowAxis.FieldSets.Count - 1).Right + 1
        Me.Controls.Add(ColumnsPanel)
        ColumnsPanel.Top = RowPanel.Bottom - 20
        _grid.Top = ColumnsPanel.Top + 20
        _grid.Left = ColumnsPanel.Right + 1
        RowPanel.Left = ColumnsPanel.Right + 1

        _grid.Width = Me.Width - _grid.Left
        _grid.Height = Me.Height - _grid.Top

        For Each lbl As Label In RowPanel.Controls
            If Not lbl.Tag Is Nothing Then
                With DirectCast(lbl.Tag, ColumnEdges)
                    If _grid.Columns(.LeftColumn).Displayed Then
                        lbl.Left = _grid.GetColumnDisplayRectangle(.LeftColumn, True).Left
                    Else
                        lbl.Left = _grid.GetColumnDisplayRectangle(_grid.FirstDisplayedScrollingColumnIndex, True).Left
                    End If
                    If _grid.Columns(.RightColumn).Displayed Then
                        lbl.Width = _grid.GetColumnDisplayRectangle(.RightColumn, True).Right - _grid.GetColumnDisplayRectangle(.LeftColumn, True).Left
                    Else
                        lbl.Width = _grid.GetColumnDisplayRectangle(_grid.FirstDisplayedScrollingColumnIndex + _grid.DisplayedColumnCount(True) - 1, True).Right - _grid.GetColumnDisplayRectangle(.LeftColumn, True).Left
                    End If
                    If lbl.Width = 0 Then Stop
                    If lbl.Right + 1 > RowPanel.Width Then
                        RowPanel.Width = lbl.Right + 1
                    End If
                End With
            End If
        Next

        MyBase.Refresh()

    End Sub

    Private Sub GridScroll(ByVal sender As Object, ByVal e As ScrollEventArgs)
        'Stop
        If sender Is hscroll Then
            If _scrollColumns <> sender.value Then
                _scrollColumns = sender.value
                Refresh()
            End If
        Else
            If _scrollRows <> sender.value Then
                _scrollRows = sender.value
                Refresh()
            End If
        End If
    End Sub

    Private Function FindColumn(ByVal grid As DataGridView, ByVal row As DataRow, ByVal cols As List(Of PivotFieldSet), Optional ByVal StartColumn As Integer = 0, Optional ByVal Level As Integer = 0) As Integer
        If StartColumn = 0 And Level = 0 Then
            While grid.Rows(Level).Cells(StartColumn).Value = ""
                StartColumn += 1
            End While
        End If
        For c As Integer = StartColumn To grid.Columns.Count - 1
            If grid.Rows(Level).Cells(c).Value = row.Item(cols(Level).Name) Then
                If Level = cols.Count - 1 Then
                    Return c
                Else
                    Return FindColumn(grid, row, cols, c, Level + 1)
                End If
            End If
        Next
    End Function

    Private Function FindRow(ByVal grid As DataGridView, ByVal row As DataRow, ByVal rows As List(Of PivotFieldSet), Optional ByVal StartRow As Integer = 0, Optional ByVal Level As Integer = 0) As Integer
        If StartRow = 0 And Level = 0 Then
            While grid.Rows(StartRow).Cells(Level).Value = ""
                StartRow += 1
            End While
        End If
        For r As Integer = StartRow To grid.Rows.Count - 1
            If grid.Rows(r).Cells(Level).Value = row.Item(rows(Level).Name) Then
                If Level = rows.Count - 1 Then
                    Return r
                Else
                    Dim TmpR As Integer = FindRow(grid, row, rows, r, Level + 1)
                    If grid.Rows(TmpR).Cells(Level).Value = row.Item(rows(Level).Name) Then
                        Return TmpR
                    End If
                End If
            End If
        Next
        Return Nothing
    End Function

    Friend Sub AddColumnsToGrid(ByVal grid As DataGridView, ByVal cols As List(Of PivotFieldSet), Optional ByVal Start As Integer = 0)
        For i As Integer = 0 To cols(Start).Fields.Count - 1
            If i > 0 Then
                For j As Integer = 0 To Start - 1
                    If Not grid.Rows(j).Cells(grid.Columns.Count - 2).Value Is Nothing Then
                        grid.Rows(j).Cells(grid.Columns.Count - 1).Value = grid.Rows(j).Cells(grid.Columns.Count - 2).Value
                    End If
                Next
            End If
            If (cols(Start).IncludeFields.Count = 0 And Not cols(Start).ExcludeFields.Contains(cols(Start).Fields(i))) OrElse cols(Start).IncludeFields.Contains(cols(Start).Fields(i)) Then
                grid.Rows(Start).Cells(grid.Columns.Count - 1).Value = cols(Start).Fields(i)
                If Start < cols.Count - 1 Then
                    AddColumnsToGrid(grid, cols, Start + 1)
                Else
                    grid.AutoResizeColumn(grid.Columns.Count - 1)
                    grid.Columns.Add(CreateGUID, "")
                End If
            End If
        Next
    End Sub

    Friend Sub AddRowsToGrid(ByVal grid As DataGridView, ByVal rows As List(Of PivotFieldSet), Optional ByVal Start As Integer = 0)
        For i As Integer = 0 To rows(Start).Fields.Count - 1
            If (rows(Start).IncludeFields.Count = 0 And Not rows(Start).ExcludeFields.Contains(rows(Start).Fields(i))) OrElse rows(Start).IncludeFields.Contains(rows(Start).Fields(i)) Then
                If i > 0 Then
                    For j As Integer = 0 To Start - 1
                        grid.Rows(grid.Rows.Count - 1).Cells(j).Value = grid.Rows(grid.Rows.Count - 2).Cells(j).Value
                    Next
                End If
                grid.Rows(grid.Rows.Count - 1).Cells(Start).Value = rows(Start).Fields(i)
                If Start < rows.Count - 1 Then
                    AddRowsToGrid(grid, rows, Start + 1)
                Else
                    grid.AutoResizeRow(grid.Rows.Count - 1)
                    grid.Rows.Add()
                End If
            End If
        Next
    End Sub

    Friend Sub SortData()

    End Sub

    Public Sub New()
        Me.Font = New Font("Segoe UI", 8)
        hscroll.Visible = False
        vscroll.Visible = False
        Me.Controls.Add(hscroll)
        Me.Controls.Add(vscroll)
    End Sub

    Private Sub Pivot_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Refresh()
    End Sub
End Class

Public Class PivotFieldSet
    Private _fields As New List(Of String)
    Private _caption As String
    Private _name As String
    Private _excludeFields As New List(Of String)
    Private _includeFields As New List(Of String)

    Public Property Caption() As String
        Get
            Return _caption
        End Get
        Set(ByVal value As String)
            _caption = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public ReadOnly Property Fields() As List(Of String)
        Get
            Return _fields
        End Get
    End Property

    Public Property ExcludeFields() As List(Of String)
        Get
            Return _excludeFields
        End Get
        Set(ByVal value As List(Of String))
            _excludeFields = value
        End Set
    End Property

    Public Property IncludeFields() As List(Of String)
        Get
            Return _includeFields
        End Get
        Set(ByVal value As List(Of String))
            _includeFields = value
        End Set
    End Property

End Class

Public Class PivotFieldSets
    Implements ICollection(Of PivotFieldSet)

    Dim _fields As New Dictionary(Of String, PivotFieldSet)
    Dim _parentPivot As frmPivot

    Public Sub New(ByVal pivot As frmPivot)
        _parentPivot = pivot
    End Sub

    Public ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of PivotFieldSet).Count
        Get
            Return _fields.Count
        End Get
    End Property

    Default ReadOnly Property Item(ByVal name As String) As PivotFieldSet
        Get
            Return _fields(name)
        End Get
    End Property

    Public Function Add(ByVal fieldName As String) As PivotFieldSet
        If _fields.ContainsKey(fieldName) Then
            _fields.Remove(fieldName)
        End If
        _fields.Add(fieldName, _parentPivot.FieldSets(fieldName))

        Return _fields(fieldName)
    End Function

    Public Sub Add(ByVal field As PivotFieldSet) Implements System.Collections.Generic.ICollection(Of PivotFieldSet).Add
        _fields.Add(field.Name, field)
    End Sub

    Public Sub Clear() Implements System.Collections.Generic.ICollection(Of PivotFieldSet).Clear
        _fields.Clear()
    End Sub

    Public Function Contains(ByVal item As PivotFieldSet) As Boolean Implements System.Collections.Generic.ICollection(Of PivotFieldSet).Contains
        Return _fields.ContainsKey(item.Name)
    End Function

    Public Function ContainsKey(ByVal name As String) As Boolean
        Return _fields.ContainsKey(name)
    End Function

    Public Sub CopyTo(ByVal array() As PivotFieldSet, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of PivotFieldSet).CopyTo

    End Sub

    Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of PivotFieldSet).IsReadOnly
        Get
            Return True
        End Get
    End Property

    Public Function Remove(ByVal item As PivotFieldSet) As Boolean Implements System.Collections.Generic.ICollection(Of PivotFieldSet).Remove
        _fields.Remove(item.Name)
    End Function

    Public Function GetEnumerator1() As System.Collections.Generic.IEnumerator(Of PivotFieldSet) Implements System.Collections.Generic.IEnumerable(Of PivotFieldSet).GetEnumerator
        Return Nothing
    End Function

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return _fields.GetEnumerator
    End Function

End Class

Public Class PivotAxis

    Private _fields As PivotFieldSets
    Dim _parentPivot As frmPivot

    Public Sub New(ByVal pivot As frmPivot)
        _parentPivot = pivot
        _fields = New PivotFieldSets(_parentPivot)
    End Sub

    Sub InsertField(ByVal fieldName As String)
        _fields.Add(fieldName)
    End Sub

    Public ReadOnly Property FieldSets() As PivotFieldSets
        Get
            Return _fields
        End Get
    End Property

End Class

Public Class PivotDataAxis
    Inherits PivotAxis

    Public Sub New(ByVal pivot As frmPivot)
        MyBase.New(pivot)
    End Sub

    Sub InsertTotal(ByVal Total As PivotTotal)

    End Sub
End Class

Public Class PivotTotalField

    Private _totalFunction As frmPivot.PivotTotalFunctionEnum
    Public Property TotalFunction() As frmPivot.PivotTotalFunctionEnum
        Get
            Return _totalFunction
        End Get
        Set(ByVal value As frmPivot.PivotTotalFunctionEnum)
            _totalFunction = value
        End Set
    End Property

    Private _numberFormat As String
    Public Property NumberFormat() As String
        Get
            Return _numberFormat
        End Get
        Set(ByVal value As String)
            _numberFormat = value
        End Set
    End Property

    Private _field As String
    Public Property Field() As String
        Get
            Return _field
        End Get
        Set(ByVal value As String)
            _field = value
        End Set
    End Property

End Class

Public Class PivotTotal

    Dim _totalFields As Dictionary(Of String, PivotTotalField)

    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _fieldset As PivotFieldSet
    Public Property FieldSet() As PivotFieldSet
        Get
            Return _fieldset
        End Get
        Set(ByVal value As PivotFieldSet)
            _fieldset = value
            _totalFields = New Dictionary(Of String, PivotTotalField)
            If Not _fieldset Is Nothing Then
                For Each TmpStr As String In _fieldset.Fields
                    Dim TmpField As New PivotTotalField
                    TmpField.Field = TmpStr
                    TmpField.TotalFunction = frmPivot.PivotTotalFunctionEnum.plFunctionSum
                    TmpField.NumberFormat = "N0"
                    _totalFields.Add(TmpStr, TmpField)
                Next
            End If
        End Set
    End Property

    Public ReadOnly Property TotalFields() As Dictionary(Of String, PivotTotalField)
        Get
            Return _totalFields
        End Get
    End Property
End Class