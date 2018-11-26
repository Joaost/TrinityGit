Imports System.Globalization
Imports Microsoft.Office.Interop

Namespace CultureSafeExcel
    Public Class Worksheet

        Private _worksheet As Excel.Worksheet
        Private _workbook As Workbook
        Private _ci As CultureInfo = New System.Globalization.CultureInfo("en-US")

        Public Sub New(Worksheet As Excel.Worksheet, Workbook As Workbook)
            _worksheet = Worksheet
            _workbook = Workbook
        End Sub

        Public Property Name As String
            Get
                Try
                    Return _worksheet.GetType.InvokeMember("Name", Reflection.BindingFlags.GetProperty, Nothing, _worksheet, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
            Set(value As String)
                Try
                    Dim _oldName = Name
                    _workbook.Sheets.Remove(Me)
                    _worksheet.GetType.InvokeMember("Name", Reflection.BindingFlags.SetProperty, Nothing, _worksheet, New Object() {value}, _ci)
                    _workbook.Sheets.Add(Me)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public ReadOnly Property Cells(Row As Integer, Column As Integer) As Range
            Get
                Try
                    Dim _range As Excel.Range = _worksheet.GetType.InvokeMember("Cells", Reflection.BindingFlags.GetProperty, Nothing, _worksheet, New Object() {Row, Column}, _ci)
                    Return New Range(_range)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                     Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property AllCells As Range
            Get
                Try
                    Dim _range As Excel.Range = _worksheet.GetType.InvokeMember("Cells", Reflection.BindingFlags.GetProperty, Nothing, _worksheet, Nothing, _ci)
                    Return New Range(_range)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property Columns(Column As Integer) As Range
            Get
                Try
                    Dim _range As Excel.Range = _worksheet.GetType.InvokeMember("Columns", Reflection.BindingFlags.GetProperty, Nothing, _worksheet, New Object() {Column}, _ci)
                    Return New Range(_range)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property Columns(Range As String) As Range
            Get
                Try
                    Dim _range As Excel.Range = _worksheet.GetType.InvokeMember("Columns", Reflection.BindingFlags.GetProperty, Nothing, _worksheet, New Object() {Range}, _ci)
                    Return New Range(_range)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property Rows(Row As Integer)
            Get
                Try
                    Dim _range As Excel.Range = _worksheet.GetType.InvokeMember("Rows", Reflection.BindingFlags.GetProperty, Nothing, _worksheet, New Object() {Row}, _ci)
                    Return New Range(_range)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property Range(Address As String) As Range
            Get
                Try
                    Dim _range As Excel.Range = _worksheet.GetType.InvokeMember("Range", Reflection.BindingFlags.GetProperty, Nothing, _worksheet, New Object() {Address}, _ci)
                    Return New Range(_range)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Private _usedRange As Range
        Public ReadOnly Property UsedRange As Range
            Get
                Try
                    If _usedRange Is Nothing Then
                        Dim _range As Excel.Range = _worksheet.GetType.InvokeMember("UsedRange", Reflection.BindingFlags.GetProperty, Nothing, _worksheet, Nothing, _ci)
                        _usedRange = New Range(_range)
                    End If
                    Return _usedRange
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public Sub Paste()
            Try
                _worksheet.GetType.InvokeMember("Paste", Reflection.BindingFlags.InvokeMethod, Nothing, _worksheet, Nothing, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub PasteSpecial(Optional Format As Object = Nothing, Optional Link As Object = Nothing, Optional DisplayAsIcon As Object = Nothing, Optional IconFileName As Object = Nothing, Optional IconIndex As Object = Nothing, Optional IconLabel As Object = Nothing, Optional NoHTMLFormatting As Object = Nothing)
            Try
                _worksheet.GetType.InvokeMember("PasteSpecial", Reflection.BindingFlags.InvokeMethod, Nothing, _worksheet, New Object() {IIf(Format Is Nothing, System.Reflection.Missing.Value, Format), IIf(Link Is Nothing, System.Reflection.Missing.Value, Link), IIf(DisplayAsIcon Is Nothing, System.Reflection.Missing.Value, DisplayAsIcon), IIf(IconFileName Is Nothing, System.Reflection.Missing.Value, IconFileName), IIf(IconIndex Is Nothing, System.Reflection.Missing.Value, IconIndex), IIf(IconLabel Is Nothing, System.Reflection.Missing.Value, IconLabel), IIf(NoHTMLFormatting Is Nothing, System.Reflection.Missing.Value, NoHTMLFormatting)}, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private _pageSetup As PageSetup
        Public ReadOnly Property PageSetup As PageSetup
            Get
                Try
                    If _pageSetup Is Nothing Then _pageSetup = New PageSetup(_worksheet.PageSetup)
                    Return _pageSetup
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Sub Delete()
            Try
                _worksheet.GetType.InvokeMember("Delete", Reflection.BindingFlags.InvokeMethod, Nothing, _worksheet, Nothing, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Function Move(Optional After As Worksheet = Nothing)
            Try
                If After Is Nothing Then
                    _worksheet.GetType.InvokeMember("Move", Reflection.BindingFlags.InvokeMethod, Nothing, _worksheet, New Object() {System.Reflection.Missing.Value, IIf(After Is Nothing, System.Reflection.Missing.Value, Nothing)})
                Else
                    _worksheet.GetType.InvokeMember("Move", Reflection.BindingFlags.InvokeMethod, Nothing, _worksheet, New Object() {System.Reflection.Missing.Value, IIf(After Is Nothing, System.Reflection.Missing.Value, After.RawSheet)})
                End If
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Property Visible As Boolean
            Get
                Try
                    Return _worksheet.GetType.InvokeMember("Visible", Reflection.BindingFlags.GetProperty, Nothing, _worksheet, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
            Set(value As Boolean)
                Try
                    _worksheet.GetType.InvokeMember("Visible", Reflection.BindingFlags.SetProperty, Nothing, _worksheet, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public ReadOnly Property VPageBreaks As Excel.VPageBreaks
            Get
                Return _worksheet.VPageBreaks
            End Get
        End Property

        Public Sub [Select]()
            Try
                _worksheet.GetType.InvokeMember("Select", Reflection.BindingFlags.InvokeMethod, Nothing, _worksheet, Nothing, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Activate()
            Try
                _worksheet.GetType.InvokeMember("Activate", Reflection.BindingFlags.InvokeMethod, Nothing, _worksheet, Nothing, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function InsertPicture(Path As String) As Shape
            Try
                Dim _pics As Object = _worksheet.GetType.InvokeMember("Pictures", Reflection.BindingFlags.GetProperty, Nothing, _worksheet, Nothing, _ci)
                Dim _pic As Object = _pics.GetType.InvokeMember("Insert", Reflection.BindingFlags.InvokeMethod, Nothing, _pics, New Object() {Path}, _ci)
                Return New Shape(_pic)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public ReadOnly Property Pictures(Index As Integer) As Shape
            Get
                Try
                    Return New Shape(_worksheet.GetType.InvokeMember("Pictures", Reflection.BindingFlags.GetProperty, Nothing, _worksheet, New Object() {Index}, _ci))
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Sub ClearSortFields()
            Try
                Dim _sortFields As Object = GetSortFields()
                _sortFields.GetType.InvokeMember("Clear", Reflection.BindingFlags.InvokeMethod, Nothing, _sortFields, Nothing, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Sub AddSortField(Key As Range, SortOn As Excel.XlSortOn, Order As Excel.XlSortOrder, DataOption As Excel.XlSortDataOption)
            Try
                Dim _sortFields = GetSortFields()
                _sortFields.GetType.InvokeMember("Add", Reflection.BindingFlags.InvokeMethod, Nothing, _sortFields, New Object() {Key.RawRange, SortOn, Order, Reflection.Missing.Value, DataOption}, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Sub Sort(Range As Range, HasHeader As Boolean, MatchCase As Boolean, Orientation As Excel.XlSortOrientation, Method As Excel.XlSortMethod)
            'With ActiveWorkbook.Worksheets("Sheet1").Sort
            '    .SetRange(Range("B19:M40"))
            '    .Header = xlYes
            '    .MatchCase = False
            '    .Orientation = xlTopToBottom
            '    .SortMethod = xlPinYin
            '    .Apply()
            'End With
            Try
                Dim _sort As Object = GetSort()
                _sort.GetType.InvokeMember("SetRange", Reflection.BindingFlags.InvokeMethod, Nothing, _sort, New Object() {Range.RawRange}, _ci)
                _sort.GetType.InvokeMember("Header", Reflection.BindingFlags.SetProperty, Nothing, _sort, New Object() {IIf(HasHeader, Excel.XlYesNoGuess.xlYes, Excel.XlYesNoGuess.xlNo)}, _ci)
                _sort.GetType.InvokeMember("MatchCase", Reflection.BindingFlags.SetProperty, Nothing, _sort, New Object() {MatchCase}, _ci)
                _sort.GetType.InvokeMember("Orientation", Reflection.BindingFlags.SetProperty, Nothing, _sort, New Object() {Orientation}, _ci)
                _sort.GetType.InvokeMember("SortMethod", Reflection.BindingFlags.SetProperty, Nothing, _sort, New Object() {Method}, _ci)
                _sort.GetType.InvokeMember("Apply", Reflection.BindingFlags.InvokeMethod, Nothing, _sort, Nothing, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Function GetSort() As Object
            Try
                Return _worksheet.GetType.InvokeMember("Sort", Reflection.BindingFlags.GetProperty, Nothing, _worksheet, Nothing, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Private Function GetSortFields() As Object
            Try
                Dim _sort As Object = GetSort()
                Return _sort.GetType.InvokeMember("SortFields", Reflection.BindingFlags.GetProperty, Nothing, _sort, Nothing, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Friend Function RawSheet() As Excel.Worksheet
            Return _worksheet
        End Function
    End Class
End Namespace
