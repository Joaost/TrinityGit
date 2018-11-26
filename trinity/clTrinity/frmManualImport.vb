Imports System.Data.SqlClient

Public Class frmManualImport

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click

        Dim fileDlg As Windows.Forms.FileDialog = New Windows.Forms.OpenFileDialog

        fileDlg.ShowDialog()

        Dim Excel As New CultureSafeExcel.Application(True)

        Dim WB As Microsoft.Office.Interop.Excel.Workbook

        Dim WS As Microsoft.Office.Interop.Excel.Worksheet

       

        WB = Excel.OpenWorkbook(fileDlg.FileName)

        WS = WB.Worksheets(1)

        Dim Cols As Integer = WS.UsedRange.Columns.Count
        Dim Rows As Integer = WS.UsedRange.Rows.Count
        Dim CurrentRow As Integer = 0

        For i As Integer = 1 To Cols
            grdSpotlist.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn())
        Next

        For i As Integer = 1 To Rows
            grdSpotlist.Rows.Add()
            For a As Integer = 1 To Cols
                Dim CellValue As String = WS.Cells(i, a).value
                grdSpotlist.Rows(i - 1).Cells(a - 1).Value = CellValue
            Next
        Next




    End Sub

    Private Sub grdSpotlist_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles grdSpotlist.ColumnDisplayIndexChanged

    End Sub

    Private Sub grdSpotlist_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles grdSpotlist.DragDrop

    End Sub

    Private Sub cmbImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbImport.Click

        Dim DateColumn As Integer
        Dim TimeColumn As Integer
        Dim ProgramColumn As Integer
        Dim FilmcodeColumn As Integer

        Dim tmpConfirmedSpot As Trinity.cPlannedSpot

        For Each col As Windows.Forms.DataGridViewColumn In grdSpotlist.Columns

            Select Case col.DisplayIndex
                Case Is = 0
                    DateColumn = col.Index
                Case Is = 1
                    TimeColumn = col.Index
                Case Is = 2
                    ProgramColumn = col.Index
                Case Is = 3
                    FilmcodeColumn = col.Index
            End Select

        Next

        For Each row As Windows.Forms.DataGridViewRow In grdSpotlist.Rows
            tmpConfirmedSpot = Campaign.PlannedSpots.Add
            tmpConfirmedSpot.Bookingtype = cmbBookingType.SelectedItem
            tmpConfirmedSpot.Channel = tmpConfirmedSpot.Bookingtype.ParentChannel
            tmpConfirmedSpot.AirDate = GuessDate(row.Cells(DateColumn).Value).ToOADate
            tmpConfirmedSpot.Filmcode = row.Cells(FilmcodeColumn).Value
            tmpConfirmedSpot.ProgAfter = row.Cells(ProgramColumn).Value

            tmpConfirmedSpot.Week = tmpConfirmedSpot.Bookingtype.GetWeek(Date.FromOADate(tmpConfirmedSpot.AirDate))
            Trinity.Helper.SetFilmForSpot(tmpConfirmedSpot)



        Next

    End Sub

    Private Function GuessDate(ByVal DateObject As Object) As DateTime

        Try
            Return CDate(DateObject)
        Catch ex As Exception
            Return Date.FromOADate(Campaign.StartDate)
        End Try

    End Function

    Private Sub frmManualImport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        For Each chan As Trinity.cChannel In Campaign.Channels
            For Each bt As Trinity.cBookingType In chan.BookingTypes
                If bt.BookIt Then cmbBookingType.Items.Add(bt)
            Next
        Next
    End Sub
End Class