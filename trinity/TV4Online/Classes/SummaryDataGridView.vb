Imports System.Windows.Forms
Imports System.Drawing

Public Class SummaryDataGridView
    Inherits DataGridView

    Private _columnsSums As New Dictionary(Of DataGridViewColumn, SumLabel)

    Public Event ColumnSumCalculate(sender As Object, ByRef e As SummarizeColumnEventArgs)
    Public Event ColumnSumFormatting(sender As Object, ByRef e As SummarizeColumnFormatEventArgs)
    Public Event ColumnSumsUpdated(sender As Object, e As EventArgs)

    Private _valueCache As New Dictionary(Of Integer, Object)

    Private _hasSummaryRow As Boolean = True
    Public Property HasSummaryRow() As Boolean
        Get
            Return _hasSummaryRow
        End Get
        Set(ByVal value As Boolean)
            _hasSummaryRow = value
            For Each _label As Label In _columnsSums.Values
                _label.Visible = value
            Next
        End Set
    End Property

    Protected Overrides Sub SetClientSizeCore(x As Integer, y As Integer)
        MyBase.SetClientSizeCore(x, y)
    End Sub

    Protected Overrides Sub OnClientSizeChanged(e As System.EventArgs)
        MyBase.OnClientSizeChanged(e)
    End Sub

    Public Property SelectColumn As DataGridViewCheckBoxColumn = Nothing

    Protected Overrides Sub OnColumnAdded(e As System.Windows.Forms.DataGridViewColumnEventArgs)
        MyBase.OnColumnAdded(e)

        Dim _label As Label = CreateSumLabel()
        _columnsSums.Add(e.Column, _label)
        _label.Width = e.Column.Width
        'Shrunk the label height so now it it not visible
        'JK 2017-01-13
        _label.Height = Me.RowTemplate.Height - 22
        Me.Controls.Add(_label)
        RepositionSummary()
    End Sub

    Protected Overrides Sub OnColumnWidthChanged(e As System.Windows.Forms.DataGridViewColumnEventArgs)
        MyBase.OnColumnWidthChanged(e)
        RepositionSummary()
    End Sub

    Protected Overrides Sub OnScroll(e As System.Windows.Forms.ScrollEventArgs)
        MyBase.OnScroll(e)
        RepositionSummary()
    End Sub

    Protected Overrides Sub OnResize(e As System.EventArgs)
        MyBase.OnResize(e)
        RepositionSummary()
    End Sub

    Private Sub RepositionSummary()
        For Each _kv As KeyValuePair(Of DataGridViewColumn, SumLabel) In _columnsSums
            If _kv.Key.Visible AndAlso ColumnCount > _kv.Key.Index Then
                _kv.Value.Location = New Point(Me.GetColumnDisplayRectangle(_kv.Key.Index, False).Left, Me.Height - _kv.Value.Height - 1 - IIf(Me.HorizontalScrollBar.Visible, Me.HorizontalScrollBar.Height, 0))
                _kv.Value.Width = _kv.Key.Width - 1
            Else
                _kv.Value.Visible = False
            End If
        Next
    End Sub

    Protected Overrides Sub OnInvalidated(e As System.Windows.Forms.InvalidateEventArgs)
        MyBase.OnInvalidated(e)
        InvalidateSummary()
    End Sub

    Protected Overrides Sub OnCellValueChanged(e As System.Windows.Forms.DataGridViewCellEventArgs)
        MyBase.OnCellValueChanged(e)

        If SelectColumn IsNot Nothing AndAlso e.ColumnIndex = SelectColumn.Index Then
            ResetSums()
        End If
    End Sub

    Sub ResetSums()
        For Each _label In _columnsSums.Values
            _label.Tag = Nothing
        Next
    End Sub

    Protected Overrides Sub OnCellValueNeeded(e As System.Windows.Forms.DataGridViewCellValueEventArgs)
        If Not _valueCache.ContainsKey(Me.Rows(e.RowIndex).Cells(e.ColumnIndex).GetHashCode) Then
            _valueCache.Add(Me.Rows(e.RowIndex).Cells(e.ColumnIndex).GetHashCode, 0)
        End If
        MyBase.OnCellValueNeeded(e)

        If _columnsSums(Me.Columns(e.ColumnIndex)).Tag IsNot Nothing Then
            If e.Value IsNot Nothing AndAlso _valueCache(Me.Rows(e.RowIndex).Cells(e.ColumnIndex).GetHashCode) IsNot Nothing AndAlso e.Value.GetType Is _valueCache(Me.Rows(e.RowIndex).Cells(e.ColumnIndex).GetHashCode).GetType Then
                If (IsReference(_valueCache(Me.Rows(e.RowIndex).Cells(e.ColumnIndex).GetHashCode)) AndAlso (Not _valueCache(Me.Rows(e.RowIndex).Cells(e.ColumnIndex).GetHashCode).Equals(e.Value))) OrElse (Not IsReference(_valueCache(Me.Rows(e.RowIndex).Cells(e.ColumnIndex).GetHashCode)) AndAlso _valueCache(Me.Rows(e.RowIndex).Cells(e.ColumnIndex).GetHashCode) <> e.Value) Then
                    _columnsSums(Me.Columns(e.ColumnIndex)).Tag = Nothing
                    _valueCache(Me.Rows(e.RowIndex).Cells(e.ColumnIndex).GetHashCode) = e.Value
                    InvalidateSummary()
                End If
            End If
        End If
        _valueCache(Me.Rows(e.RowIndex).Cells(e.ColumnIndex).GetHashCode) = e.Value
    End Sub

    Protected Overrides Sub OnColumnStateChanged(e As System.Windows.Forms.DataGridViewColumnStateChangedEventArgs)
        MyBase.OnColumnStateChanged(e)
        _columnsSums(e.Column).Visible = e.Column.Visible
    End Sub

    Private Function CachedValue(Row As Integer, Column As Integer) As Object
        If _valueCache.ContainsKey(Me.Rows(Row).Cells(Column).GetHashCode) Then
            Return _valueCache(Me.Rows(Row).Cells(Column).GetHashCode)
        Else
            Return Me.Rows(Row).Cells(Column).Value
        End If
    End Function

    Shadows Sub Invalidate()
        ResetSums()
        MyBase.Invalidate()
    End Sub

    Private Sub InvalidateSummary()
        'For Each _kv As KeyValuePair(Of DataGridViewColumn, SumLabel) In _columnsSums
        '    Dim _e As New SummarizeColumnEventArgs With {.Column = _kv.Key, .Calculated = False, .Sum = 0}
        '    RaiseEvent ColumnSumCalculate(Me, _e)
        '    If Not _e.DoNotSum Then
        '        Dim _sum As Object
        '        If Not _e.Calculated Then
        '            _sum = GetColumnSum(_e.Column.Index, _e.SumFunction)
        '        Else
        '            _sum = _e.Sum
        '        End If
        '        Dim _fe As New SummarizeColumnFormatEventArgs With {.Column = _e.Column, .Sum = _sum}
        '        _fe.Font = Me.Font
        '        If Not String.IsNullOrEmpty(_kv.Key.DefaultCellStyle.Format) Then
        '            Try
        '                _fe.FormattedSum = Format(_sum, _kv.Key.DefaultCellStyle.Format)
        '            Catch

        '            End Try
        '        End If
        '        RaiseEvent ColumnSumFormatting(Me, _fe)
        '        _kv.Value.Font = _fe.Font
        '        _kv.Value.ForeColor = _fe.ForeColor
        '        _kv.Value.BackColor = _fe.BackgroundColor
        '        _kv.Value.TextAlign = _fe.TextAlignment
        '        _kv.Value.Text = _fe.FormattedSum
        '    Else
        '        Dim _fe As New SummarizeColumnFormatEventArgs With {.Column = _e.Column, .Sum = ""}
        '        _fe.Font = Me.Font
        '        RaiseEvent ColumnSumFormatting(Me, _fe)
        '        _kv.Value.Font = _fe.Font
        '        _kv.Value.ForeColor = _fe.ForeColor
        '        _kv.Value.BackColor = _fe.BackgroundColor
        '        _kv.Value.TextAlign = _fe.TextAlignment
        '        _kv.Value.Text = ""
        '    End If
        'Next
        'RepositionSummary()
        'RaiseEvent ColumnSumsUpdated(Me, EventArgs.Empty)
    End Sub

    Public Function GetColumnSum(column As Integer, Optional SumFunction As SummarizeColumnEventArgs.SumFunctionEnum = SummarizeColumnEventArgs.SumFunctionEnum.Sum) As Object
        Dim _sum As Object
        If column >= Me.ColumnCount OrElse Not _columnsSums.ContainsKey(Me.Columns(column)) Then
            Return 0
        End If
        Dim _label As SumLabel = _columnsSums(Me.Columns(column))
        If _label.Tag Is Nothing Then
            _sum = 0.0F
            For Each _row As DataGridViewRow In Me.Rows
                If SelectColumn Is Nothing OrElse _row.Cells(SelectColumn.Index).Value Then
                    Dim _value As Single
                    Try
                        If Single.TryParse(Me.CachedValue(_row.Index, column), _value) Then
                            _sum += _value
                        Else
                            _sum = ""
                            Exit For
                        End If
                    Catch
                        _sum = ""
                        Exit For
                    End Try
                End If
            Next
            If SumFunction = SummarizeColumnEventArgs.SumFunctionEnum.Average AndAlso _sum.ToString <> "" AndAlso Me.RowCount > 0 Then
                _sum /= Me.RowCount
            End If
            _label.Tag = _sum
        Else
            _sum = _label.Tag
        End If
        Return _sum
    End Function

    Private Function CreateSumLabel() As SumLabel
        Dim _label As New SumLabel
        Return _label
    End Function

    Public Class SummarizeColumnEventArgs
        Inherits EventArgs

        Public Enum SumFunctionEnum
            Sum
            Average
        End Enum

        Public Property Column As DataGridViewColumn
        Public Property Sum As Single
        Public Property Calculated As Boolean = False
        Public Property SumFunction As SumFunctionEnum = SumFunctionEnum.Sum
        Public Property DoNotSum As Boolean = True

    End Class

    Public Class SummarizeColumnFormatEventArgs
        Inherits EventArgs

        Public Property Column As DataGridViewColumn
        Public Property FormattingApplied As Boolean = False
        Public Property Font As System.Drawing.Font
        Public Property BackgroundColor As Color = Color.Black
        Public Property ForeColor As Color = Color.White
        Public Property TextAlignment As DataGridViewContentAlignment = DataGridViewContentAlignment.MiddleCenter

        Private _sum As Object
        Public Property Sum As Object
            Get
                Return _sum
            End Get
            Set(value As Object)
                _sum = value
                FormattedSum = _sum
            End Set
        End Property

        Public Property FormattedSum As String

    End Class

    Private Class SumLabel
        Inherits Label

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            'MyBase.OnPaint(e)
            'Using _gfx As Graphics = e.Graphics
            'e.Graphics.DrawLines(Pens.LightGray, {New PointF(0, 0), New PointF(e.ClipRectangle.Width, 0), New PointF(e.ClipRectangle.Width, e.ClipRectangle.Height)})
            'End Using
        End Sub
    End Class
End Class
