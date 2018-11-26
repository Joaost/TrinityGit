Imports Microsoft.Office.Interop
Imports System.Globalization

Namespace CultureSafeExcel
    Public Class Workbook

        Private _workbook As Excel.Workbook
        Private _sheets As WorksheetCollection
        Private _ci As CultureInfo = New System.Globalization.CultureInfo("en-US")

        Public Sub New(Workbook As Excel.Workbook)
            _workbook = Workbook
        End Sub

        Public ReadOnly Property Name As String
            Get
                Try
                    Return _workbook.GetType.InvokeMember("Name", Reflection.BindingFlags.GetProperty, Nothing, _workbook, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property Sheets As WorksheetCollection
            Get
                If _sheets Is Nothing Then
                    _sheets = New WorksheetCollection
                    For Each _sheet As Excel.Worksheet In _workbook.Sheets
                        _sheets.Add(New Worksheet(_sheet, Me), _sheet.Name)
                    Next
                End If
                Return _sheets
            End Get
        End Property

        Sub SaveAs(Filename As String)
            Try
                _workbook.GetType.InvokeMember("SaveAs", Reflection.BindingFlags.InvokeMethod, Nothing, _workbook, New Object() {Filename}, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        'Public ReadOnly Property Sheets(Index As Integer) As CultureSafeExcel.Worksheet
        '    Get
        '        Return Sheets(_workbook.Sheets(Index).Name)
        '    End Get
        'End Property

        'Public ReadOnly Property Sheets(SheetName As String) As Worksheet
        '    Get
        '        If Not _sheets.ContainsKey(SheetName) Then
        '            _sheets.Add(SheetName, New Worksheet(_workbook.GetType.InvokeMember("Sheets", Reflection.BindingFlags.GetProperty, Nothing, _workbook, New Object() {SheetName}, _ci)))
        '        End If
        '        Return _sheets(SheetName)
        '    End Get
        'End Property

        Public ReadOnly Property SheetCount As Integer
            Get
                Return _workbook.Sheets.Count
            End Get
        End Property

        Sub Close()
            Try
                _workbook.GetType.InvokeMember("Close", Reflection.BindingFlags.InvokeMethod, Nothing, _workbook, Nothing, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public WriteOnly Property Colors(Color As Integer) As Integer
            Set(value As Integer)
                Try
                    _workbook.GetType.InvokeMember("Colors", Reflection.BindingFlags.SetProperty, Nothing, _workbook, New Object() {Color, value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public Function AddSheet(Optional After As Worksheet = Nothing)
            Try
                If After Is Nothing Then
                    Dim _sheet As New Worksheet(_workbook.Sheets.GetType.InvokeMember("Add", Reflection.BindingFlags.InvokeMethod, Nothing, _workbook.Sheets, New Object() {}, _ci), Me)
                    'Sheets.Add(_sheet, _sheet.Name)
                    Return _sheet
                Else
                    Dim _sheet As New Worksheet(_workbook.Sheets.GetType.InvokeMember("Add", Reflection.BindingFlags.InvokeMethod, Nothing, _workbook.Sheets, New Object() {}, _ci), Me)
                    'Sheets.Add(_sheet, _sheet.Name)
                    Return _sheet
                End If

            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex.InnerException
            End Try
        End Function

        Public ReadOnly Property Path As String
            Get
                Try
                    Return _workbook.GetType.InvokeMember("Path", Reflection.BindingFlags.GetProperty, Nothing, _workbook, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property
    End Class
End Namespace
