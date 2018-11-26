Namespace ScheduleTemplates
    Public Class cSchedule

        Private Excel As New CultureSafeExcel.Application(True)
        Private WB As CultureSafeExcel.Workbook
 
        Private _sheets As New Dictionary(Of String, cScheduleSheet)
        Public ReadOnly Property Sheets As Dictionary(Of String, cScheduleSheet)
            Get
                Return _sheets
            End Get
        End Property

        Public Sub Load(Filename As String)

            If Not My.Computer.FileSystem.FileExists(Filename) Then
                Throw New IO.FileNotFoundException()
            End If

            Excel.ScreenUpdating = False
            Excel.DisplayAlerts = False
            If Not (UCase(Filename).EndsWith("XLS") Or UCase(Filename).EndsWith("XLSX")) Then
                Dim Word As Microsoft.Office.Interop.Word.Application
                Word = New Microsoft.Office.Interop.Word.Application 'CreateObject("word.application")
                With Word.Documents.Open(Filename, , True, False)
                    WB = Excel.AddWorkbook
                    Word.Selection.WholeStory()
                    Word.Selection.Copy()
                    'Line below needed because of curious bug with Excel not being ready for the Paste operation
                    On Error Resume Next
                    WB.Sheets(1).Paste()
                    While Err.Number > 0
                        On Error Resume Next
                        WB.Sheets(1).Paste()
                    End While
                    .Close()
                    Word.Quit()
                End With
            Else
                WB = Excel.OpenWorkbook(Filename)
            End If
            For _s As Integer = 1 To WB.SheetCount
                Dim _sheet As CultureSafeExcel.Worksheet = WB.Sheets(_s)
                If _sheet.UsedRange.Columns.Count > 1 AndAlso _sheet.UsedRange.Rows.Count > 1 Then
                    _sheets.Add(_sheet.Name, New cScheduleSheet(_sheet))
                End If
            Next
        End Sub

        Function ReadSheet(Sheet As Microsoft.Office.Interop.Excel.Worksheet) As cScheduleSheet
            Dim _sheet As New cScheduleSheet(Sheet.UsedRange.Rows.Count + Sheet.UsedRange.Row - 1, Sheet.UsedRange.Columns.Count + Sheet.UsedRange.Column - 1)
            For _row As Integer = 1 To Sheet.UsedRange.Rows.Count + Sheet.UsedRange.Row - 1
                For _col As Integer = 1 To Sheet.UsedRange.Columns.Count + Sheet.UsedRange.Column - 1
                    If Sheet.Cells(_row, _col).value IsNot Nothing Then
                        _sheet.Cells(_row, _col) = Sheet.Cells(_row, _col).Text
                    Else
                        _sheet.Cells(_row, _col) = ""
                    End If
                Next
            Next
            Return _sheet
        End Function

        Protected Overrides Sub Finalize()
            Try
                WB.Close()
                Excel.ScreenUpdating = True
                Excel.DisplayAlerts = True
                Excel.Quit()
            Catch

            End Try
            MyBase.Finalize()
            'ResetCulture()
        End Sub
    End Class
End Namespace