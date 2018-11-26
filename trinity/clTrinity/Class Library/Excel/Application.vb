Imports System.Globalization
Imports Microsoft.Office.Interop

Namespace CultureSafeExcel
    Public Class Application

        Private _excel As Excel.Application
        Private _ci As CultureInfo = New System.Globalization.CultureInfo("en-US")
        Private _workbook As Workbook

        Private _autoQuit As Boolean = True
        Public Property AutoQuit() As Boolean
            Get
                Return _autoQuit
            End Get
            Set(ByVal value As Boolean)
                _autoQuit = value
            End Set
        End Property

        Public Function AddWorkbook() As Workbook
            Try
                Dim _wbs As Object = _excel.Workbooks
                Dim _wb = _wbs.GetType().InvokeMember("Add", Reflection.BindingFlags.InvokeMethod, Nothing, _wbs, Nothing, _ci)
                _workbook = New Workbook(_wb)
                If _workbook.SheetCount < 3 Then
                    _workbook.AddSheet()
                End If
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
            Return _workbook
        End Function

        Public Function OpenWorkbook(Filename As String, Optional UpdateLinks As Object = Nothing, Optional CorruptLoad As Object = Nothing) As Workbook
            Try
                Dim _wbs As Object = _excel.Workbooks
                If UpdateLinks Is Nothing Then UpdateLinks = System.Reflection.Missing.Value
                If CorruptLoad Is Nothing Then CorruptLoad = System.Reflection.Missing.Value
                Dim _args(14) As Object
                For i As Integer = 0 To 14
                    _args(i) = System.Reflection.Missing.Value
                Next
                _args(0) = Filename
                _args(1) = UpdateLinks
                _args(14) = CorruptLoad
                Dim _wb = _wbs.GetType().InvokeMember("Open", Reflection.BindingFlags.InvokeMethod, Nothing, _wbs, _args, _ci)
                _workbook = New Workbook(_wb)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
            Return _workbook
        End Function

        Dim _workbooks As Workbook = Nothing
        Public Function Workbooks(Index As Integer) As Workbook
            Try
                If _workbooks Is Nothing Then
                    _workbooks = New Workbook(_excel.GetType.InvokeMember("Workbooks", Reflection.BindingFlags.GetProperty, Nothing, _excel, New Object() {Index}, _ci))
                End If
                Return _workbooks
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub New(AutoQuit As Boolean)
            _excel = New Excel.Application
            _autoQuit = AutoQuit
            _excel.GetType().InvokeMember("ScreenUpdating", Reflection.BindingFlags.SetProperty, Nothing, _excel, New Object() {False}, _ci)
        End Sub

        Public ReadOnly Property Sheets(Index As Integer) As Worksheet
            Get
                If _workbook IsNot Nothing Then
                    Return _workbook.Sheets(Index)
                End If
                Return Nothing
            End Get
        End Property

        Public ReadOnly Property Sheets(SheetName As String) As Worksheet
            Get
                If _workbook IsNot Nothing Then
                    Return _workbook.Sheets(SheetName)
                End If
                Return Nothing
            End Get
        End Property

        Public ReadOnly Property SheetCount As Integer
            Get
                If _workbook IsNot Nothing Then
                    Return _workbook.SheetCount
                End If
                Return Nothing
            End Get
        End Property

        Protected Overrides Sub Finalize()
            Try
                _excel.ScreenUpdating = True
                If _autoQuit Then _excel.Quit()
            Catch

            End Try
            MyBase.Finalize()
        End Sub

        Public WriteOnly Property ScreenUpdating As Boolean
            Set(value As Boolean)
                Try
                    _excel.GetType().InvokeMember("ScreenUpdating", Reflection.BindingFlags.SetProperty, Nothing, _excel, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property Visible As Boolean
            Set(value As Boolean)
                Try
                    ScreenUpdating = value
                    _excel.GetType().InvokeMember("Visible", Reflection.BindingFlags.SetProperty, Nothing, _excel, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property DisplayAlerts As Boolean
            Set(value As Boolean)
                Try
                    _excel.GetType().InvokeMember("DisplayAlerts", Reflection.BindingFlags.SetProperty, Nothing, _excel, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Sub Quit()
            _excel.Quit()
        End Sub

        Function Windows(Name As String) As Excel.Window
            Return _excel.Windows(Name)
        End Function

        Public ReadOnly Property ActiveSheet As Worksheet
            Get
                Try
                    Return New Worksheet(_excel.GetType.InvokeMember("ActiveSheet", Reflection.BindingFlags.GetProperty, Nothing, _excel, Nothing, _ci), ActiveWorkbook)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property ActiveWorkbook As Workbook
            Get
                Try
                    Return New Workbook(_excel.GetType.InvokeMember("ActiveWorkbook", Reflection.BindingFlags.GetProperty, Nothing, _excel, Nothing, _ci))
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property InchesToPoints(Inches As Single) As Single
            Get
                Try
                    Return _excel.GetType.InvokeMember("InchesToPoints", Reflection.BindingFlags.GetProperty, Nothing, _excel, New Object() {Inches}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property ActiveWindow As Window
            Get
                Try
                    Return New Window(_excel.GetType.InvokeMember("ActiveWindow", Reflection.BindingFlags.GetProperty, Nothing, _excel, Nothing, _ci))
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property
    End Class
End Namespace
