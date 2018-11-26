Imports clTrinity.CultureSafeExcel

Namespace Trinity
    Public Class cPreAnalysisExcelCustomSheet
        Inherits cPreAnalysisExcel

        ''' <summary>
        ''' Holds the name of the sheet where data should be written
        ''' </summary>
        Private _sheetName As String

        Sub New(ByVal Campaign As cKampanj, ByVal Excel As Application, SheetName As String)
            MyBase.New(Campaign, Excel)
            _sheetName = SheetName
        End Sub

        ''' <summary>
        ''' Gets the Excel sheet where data should be written.
        ''' </summary>
        ''' <returns></returns>
        Friend Overrides Function GetSheet() As String
            Return _sheetName
        End Function

    End Class

End Namespace
