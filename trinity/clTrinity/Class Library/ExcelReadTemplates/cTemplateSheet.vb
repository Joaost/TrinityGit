Imports Microsoft.Office.Interop

Namespace ExcelReadTemplates
    Public Class cTemplateSheet

        Private _sheet As CultureSafeExcel.Worksheet
        Private _sheetHasBeenRead As Boolean = False
        Private _topRow As Integer = 1
        Private _leftColumn As Integer = 1

        Private _rows As Integer
        Public ReadOnly Property Rows As Integer
            Get
                Return _rows
            End Get
        End Property

        Private _columns As Integer
        Public ReadOnly Property Columns As Integer
            Get
                Return _columns
            End Get
        End Property

        Public ReadOnly Property Sheet As CultureSafeExcel.Worksheet
            Get
                Return _sheet
            End Get
        End Property

        Public Sub New(Sheet As CultureSafeExcel.Worksheet)
            _sheet = Sheet
            _topRow = Sheet.UsedRange.Row - 1
            _leftColumn = Sheet.UsedRange.Column - 1
            _rows = Sheet.UsedRange.Rows.Count + _topRow
            _columns = Sheet.UsedRange.Columns.Count + _leftColumn
            ReDim _cells(Rows, Columns)
        End Sub

        Public Sub New(Rows As Integer, Columns As Integer)
            _rows = Rows
            _columns = Columns
            ReDim _cells(Rows, Columns)
        End Sub

        Private _cells(,) As Object
        Public Property Cells(Row As Integer, Column As Integer) As Object
            Get
                'cSchedule.SetCulture()
                If Not _sheetHasBeenRead AndAlso _cells(Row, Column) Is Nothing Then
                    _cells(Row, Column) = _sheet.Cells(Row + _topRow, Column + _leftColumn).Value
                End If
                If String.IsNullOrEmpty(_cells(Row, Column)) Then _cells(Row, Column) = ""
                Dim _val = _cells(Row, Column)
                'cSchedule.ResetCulture()
                Return _val
            End Get
            Set(value As Object)
                _cells(Row, Column) = value
            End Set
        End Property

        Public Sub ReadSheet()
            For _row As Integer = 1 To _sheet.UsedRange.Rows.Count + _sheet.UsedRange.Row - 1
                For _col As Integer = 1 To _sheet.UsedRange.Columns.Count + _sheet.UsedRange.Column - 1
                    If _sheet.Cells(_row, _col).Value IsNot Nothing Then
                        _cells(_row, _col) = _sheet.Cells(_row, _col).Value
                    Else
                        _cells(_row, _col) = ""
                    End If
                Next
            Next
            _sheetHasBeenRead = True
        End Sub

        Public Sub SetTopRow(row As Integer)
            _topRow = row
            _rows -= row - 1
            ReDim _cells(Rows, Columns)
        End Sub
    End Class
End Namespace
