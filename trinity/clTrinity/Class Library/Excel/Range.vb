Imports Microsoft.Office.Interop
Imports System.Globalization

Namespace CultureSafeExcel
    Public Class Range

        Private _range As Excel.Range
        Private _ci As CultureInfo = New System.Globalization.CultureInfo("en-US")

        Public Sub New(Range As Excel.Range)
            _range = Range
        End Sub

        Public Property Value As Object
            Get
                Try
                    Return _range.GetType.InvokeMember("Value", Reflection.BindingFlags.GetProperty, Nothing, _range, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
            Set(value As Object)
                Try
                    _range.GetType.InvokeMember("Value", Reflection.BindingFlags.SetProperty, Nothing, _range, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property Style As String
            Set(value As String)
                Try
                    _range.GetType.InvokeMember("Style", Reflection.BindingFlags.SetProperty, Nothing, _range, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public Property Text As String
            Get
                Try
                    Return _range.GetType.InvokeMember("Text", Reflection.BindingFlags.GetProperty, Nothing, _range, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
            Set(value As String)
                Try
                    _range.GetType.InvokeMember("Text", Reflection.BindingFlags.SetProperty, Nothing, _range, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public Property Formula As String
            Get
                Try
                    Return _range.GetType.InvokeMember("Formula", Reflection.BindingFlags.GetProperty, Nothing, _range, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
            Set(value As String)
                Try
                    _range.GetType.InvokeMember("Formula", Reflection.BindingFlags.SetProperty, Nothing, _range, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property FormulaR1C1 As String
            Set(value As String)
                Try
                    _range.GetType.InvokeMember("FormulaR1C1", Reflection.BindingFlags.SetProperty, Nothing, _range, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public ReadOnly Property Address As String
            Get
                Try
                    Return _range.GetType.InvokeMember("Address", Reflection.BindingFlags.GetProperty, Nothing, _range, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        'Public ReadOnly Property RawRange As Excel.Range
        '    Get
        '        Return _range
        '    End Get
        'End Property

        Public WriteOnly Property Numberformat As String
            Set(value As String)
                Try
                    _range.GetType.InvokeMember("Numberformat", Reflection.BindingFlags.SetProperty, Nothing, _range, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        ''' <summary>
        ''' This function should not be used. Use the sorting methods on Worksheet instead
        ''' </summary>
        Sub Sort(Optional Key1 As Range = Nothing, Optional Order1 As Excel.XlSortOrder = Excel.XlSortOrder.xlAscending, Optional Key2 As Range = Nothing, Optional Type As Object = Nothing, Optional Order2 As Excel.XlSortOrder = Excel.XlSortOrder.xlAscending, Optional Key3 As Range = Nothing, Optional Order3 As Excel.XlSortOrder = Excel.XlSortOrder.xlAscending, Optional Header As Excel.XlYesNoGuess = Excel.XlYesNoGuess.xlNo, Optional OrderCustom As Object = Nothing, Optional MatchCase As Object = Nothing, Optional Orientation As Excel.XlSortOrientation = Excel.XlSortOrientation.xlSortRows, Optional SortMethod As Excel.XlSortMethod = Excel.XlSortMethod.xlPinYin, Optional DataOption1 As Excel.XlSortDataOption = Excel.XlSortDataOption.xlSortNormal, Optional DataOption2 As Excel.XlSortDataOption = Excel.XlSortDataOption.xlSortNormal, Optional DataOption3 As Excel.XlSortDataOption = Excel.XlSortDataOption.xlSortNormal)
            Dim _key1 As Object
            Dim _key2 As Object
            Dim _key3 As Object
            If Key1 Is Nothing Then
                _key1 = System.Reflection.Missing.Value
            Else
                _key1 = Key1.RawRange
            End If
            If Key2 Is Nothing Then
                _key2 = System.Reflection.Missing.Value
            Else
                _key2 = Key1.RawRange
            End If
            If Key3 Is Nothing Then
                _key3 = System.Reflection.Missing.Value
            Else
                _key3 = Key1.RawRange
            End If
            Try
                _range.GetType.InvokeMember("Sort", Reflection.BindingFlags.InvokeMethod, Nothing, _range, New Object() {_key1, Order1, _key2, IIf(Type Is Nothing, System.Reflection.Missing.Value, Type), Order2, _key3, Order3, Header, IIf(OrderCustom Is Nothing, System.Reflection.Missing.Value, OrderCustom), IIf(MatchCase Is Nothing, System.Reflection.Missing.Value, MatchCase), Orientation, SortMethod, DataOption1, DataOption2, DataOption3}, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Sub Delete()
            _range.GetType.InvokeMember("Delete", Reflection.BindingFlags.InvokeMethod, Nothing, _range, Nothing, _ci)
        End Sub

        Public ReadOnly Property Cells(Row As Integer, Column As Integer) As Range
            Get
                Try
                    Dim _r As Excel.Range = _range.GetType.InvokeMember("Cells", Reflection.BindingFlags.GetProperty, Nothing, _range, New Object() {Row, Column}, _ci)
                    Return New Range(_r)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property Rows As Range
            Get
                Try
                    Dim _r As Excel.Range = _range.GetType.InvokeMember("Rows", Reflection.BindingFlags.GetProperty, Nothing, _range, Nothing, _ci)
                    Return New Range(_r)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property Columns As Range
            Get
                Try
                    Dim _r As Excel.Range = _range.GetType.InvokeMember("Columns", Reflection.BindingFlags.GetProperty, Nothing, _range, Nothing, _ci)
                    Return New Range(_r)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property Row As Integer
            Get
                Try
                    Return _range.GetType.InvokeMember("Row", Reflection.BindingFlags.GetProperty, Nothing, _range, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property Column As Integer
            Get
                Try
                    Return _range.GetType.InvokeMember("Column", Reflection.BindingFlags.GetProperty, Nothing, _range, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property Count As Integer
            Get
                Try
                    Return _range.GetType.InvokeMember("Count", Reflection.BindingFlags.GetProperty, Nothing, _range, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public WriteOnly Property ColumnWidth As Single
            Set(value As Single)
                Try
                    _range.GetType.InvokeMember("ColumnWidth", Reflection.BindingFlags.SetProperty, Nothing, _range, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property RowHeight As Single
            Set(value As Single)
                Try
                    _range.GetType.InvokeMember("RowHeight", Reflection.BindingFlags.SetProperty, Nothing, _range, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public ReadOnly Property Left As Single
            Get
                Try
                    Return _range.GetType.InvokeMember("Left", Reflection.BindingFlags.GetProperty, Nothing, _range, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public Sub Replace(What As Object, Replacement As Object, Optional LookAt As Object = Nothing, Optional SearchOrder As Object = Nothing, Optional MatchCase As Object = Nothing, Optional MatchByte As Object = Nothing, Optional SearchFormat As Object = Nothing, Optional ReplaceFormat As Object = Nothing)
            _range.GetType.InvokeMember("Replace", Reflection.BindingFlags.InvokeMethod, Nothing, _range, New Object() {What, Replacement, IIf(LookAt Is Nothing, System.Reflection.Missing.Value, LookAt), IIf(SearchOrder Is Nothing, System.Reflection.Missing.Value, SearchOrder), IIf(MatchCase Is Nothing, System.Reflection.Missing.Value, MatchCase), IIf(MatchByte Is Nothing, System.Reflection.Missing.Value, MatchByte), IIf(SearchFormat Is Nothing, System.Reflection.Missing.Value, SearchFormat), IIf(ReplaceFormat Is Nothing, System.Reflection.Missing.Value, ReplaceFormat)}, _ci)
        End Sub

        Public ReadOnly Property Interior As Interior
            Get
                Try
                    Return New Interior(_range.GetType.InvokeMember("Interior", Reflection.BindingFlags.GetProperty, Nothing, _range, Nothing, _ci))
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property Font As Font
            Get
                Try
                    Return New Font(_range.GetType.InvokeMember("Font", Reflection.BindingFlags.GetProperty, Nothing, _range, Nothing, _ci))
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Sub Merge()
            _range.Merge()
        End Sub

        Public WriteOnly Property WrapText As Boolean
            Set(value As Boolean)
                Try
                    _range.GetType.InvokeMember("WrapText", Reflection.BindingFlags.SetProperty, Nothing, _range, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property VerticalAlignment As Excel.XlVAlign
            Set(value As Excel.XlVAlign)
                Try
                    _range.GetType.InvokeMember("VerticalAlignment", Reflection.BindingFlags.SetProperty, Nothing, _range, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property HorizontalAlignment As Excel.XlVAlign
            Set(value As Excel.XlVAlign)
                Try
                    _range.GetType.InvokeMember("HorizontalAlignment", Reflection.BindingFlags.SetProperty, Nothing, _range, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public ReadOnly Property EntireColumn As Range
            Get
                Try
                    Return New Range(_range.GetType.InvokeMember("EntireColumn", Reflection.BindingFlags.GetProperty, Nothing, _range, Nothing, _ci))
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property EntireRow As Range
            Get
                Try
                    Return New Range(_range.GetType.InvokeMember("EntireRow", Reflection.BindingFlags.GetProperty, Nothing, _range, Nothing, _ci))
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property Top As Single
            Get
                Try
                    Return _range.GetType.InvokeMember("Top", Reflection.BindingFlags.GetProperty, Nothing, _range, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property Borders(Index As Integer) As Border
            Get
                Try
                    Return New Border(_range.GetType.InvokeMember("Borders", Reflection.BindingFlags.GetProperty, Nothing, _range, New Object() {Index}, _ci))
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public Sub AutoFit()
            Try
                _range.GetType().InvokeMember("AutoFit", Reflection.BindingFlags.InvokeMethod, Nothing, _range, Nothing, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub [Select]()
            Try
                _range.GetType.InvokeMember("Select", Reflection.BindingFlags.InvokeMethod, Nothing, _range, Nothing, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Copy()
            Try
                _range.GetType.InvokeMember("Copy", Reflection.BindingFlags.InvokeMethod, Nothing, _range, Nothing, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub PasteSpecial(Optional Paste As Excel.XlPasteType = Excel.XlPasteType.xlPasteAll, Optional Operation As Excel.XlPasteSpecialOperation = Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, Optional SkipBlanks As Object = Nothing, Optional Transpose As Object = Nothing)
            Try
                _range.GetType.InvokeMember("PasteSpecial", Reflection.BindingFlags.InvokeMethod, Nothing, _range, New Object() {Paste, Operation, IIf(SkipBlanks Is Nothing, System.Reflection.Missing.Value, SkipBlanks), IIf(Transpose Is Nothing, System.Reflection.Missing.Value, Transpose)}, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Insert(Optional Shift As Object = Nothing, Optional CopyOrigin As Object = Nothing)
            Try
                _range.GetType.InvokeMember("Insert", Reflection.BindingFlags.InvokeMethod, Nothing, _range, New Object() {IIf(Shift Is Nothing, System.Reflection.Missing.Value, Shift), IIf(CopyOrigin Is Nothing, System.Reflection.Missing.Value, CopyOrigin)}, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Friend Function RawRange() As Excel.Range
            Return _range
        End Function
    End Class
End Namespace
