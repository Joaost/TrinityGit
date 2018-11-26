Public Class cSchedule

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

        Dim Excel As New Microsoft.Office.Interop.Excel.Application
        Dim WB As Microsoft.Office.Interop.Excel.Workbook
        Dim oldCI As System.Globalization.CultureInfo

        oldCI = System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")

        Excel.ScreenUpdating = False
        If Not (UCase(Filename).EndsWith("XLS") Or UCase(Filename).EndsWith("XLSX")) Then
            Dim Word As Microsoft.Office.Interop.Word.Application
            Word = New Microsoft.Office.Interop.Word.Application 'CreateObject("word.application")
            With Word.Documents.Open(Filename, , True, False)
                WB = Excel.Workbooks.Add
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
            WB = Excel.Workbooks.Open(Filename)
        End If

        For Each _sheet As Microsoft.Office.Interop.Excel.Worksheet In WB.Sheets
            If _sheet.UsedRange.Columns.Count > 1 AndAlso _sheet.UsedRange.Rows.Count > 1 Then
                _sheets.Add(_sheet.Name, ReadSheet(_sheet))
            End If
        Next
        WB.Close()
        Excel.Quit()
        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
    End Sub

    Function ReadSheet(Sheet As Microsoft.Office.Interop.Excel.Worksheet) As cScheduleSheet
        Dim _sheet As New cScheduleSheet(Sheet.UsedRange.Rows.Count + Sheet.UsedRange.Row - 1, Sheet.UsedRange.Columns.Count + Sheet.UsedRange.Column - 1)
        For _row As Integer = 1 To Sheet.UsedRange.Rows.Count + Sheet.UsedRange.Row - 1
            For _col As Integer = 1 To Sheet.UsedRange.Columns.Count + Sheet.UsedRange.Column - 1
                If Sheet.Cells(_row, _col).value IsNot Nothing Then
                    _sheet.Cells(_row, _col) = Sheet.Cells(_row, _col).value.ToString
                Else
                    _sheet.Cells(_row, _col) = ""
                End If
            Next
        Next
        Return _sheet
    End Function

End Class
