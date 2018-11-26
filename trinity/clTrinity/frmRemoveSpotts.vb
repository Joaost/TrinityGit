Imports System.Windows.Forms
Imports clTrinity


Public Class frmRemoveSpotts

    Dim listOfRemovableSpotts As new List(Of Trinity.cBookedSpot)
    Dim listOfRemovableAcualSpotts As new List(Of Trinity.cActualSpot)

    Public sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        
        If Campaign.BookedSpots.Count > 1 Then
            For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                If Trinity.Helper.FormatDateForBooking(tmpSpot.AirDate) < Trinity.Helper.FormatDateForBooking(Campaign.StartDate) Or Trinity.Helper.FormatDateForBooking(tmpSpot.AirDate) > Trinity.Helper.FormatDateForBooking(Campaign.EndDate) Then
                    Dim tmpDate = Trinity.Helper.FormatDateForBooking(Campaign.EndDate)

                    listOfRemovableSpotts.Add(tmpSpot)
                End If
            Next
        End If
        If Campaign.ActualSpots.Count > 1
            For each tmpSpot As Trinity.cActualSpot in Campaign.ActualSpots
                If Trinity.Helper.FormatDateForBooking(tmpSpot.AirDate) <= Trinity.Helper.FormatDateForBooking(Campaign.StartDate) Or Trinity.Helper.FormatDateForBooking(tmpSpot.AirDate) >= Trinity.Helper.FormatDateForBooking(Campaign.EndDate) Then
                    Dim tmpDate = Trinity.Helper.FormatDateForBooking(Campaign.EndDate)
                    listOfRemovableAcualSpotts.Add(tmpSpot)
                End If
            Next
        End If
       
        fillGrid()

        Dim listOfFilm As new List(Of Trinity.cFilm)
        For each tmpFilm As Trinity.cFilm In Campaign.Channels(1).BookingTypes(1).Weeks(2).Films
            listOfFilm.Add(tmpFilm)
        Next
    End Sub
    Sub fillGrid()
        If grdSpotts.RowCount < 1
            Dim camp As Trinity.cKampanj = Campaign
            Dim succes As Boolean = False



            For each tmpC as Trinity.cChannel In camp.Channels
                For each tmpB As Trinity.cBookingType In tmpC.BookingTypes
                    If tmpB.bookit
                        For each tmpW As Trinity.cWeek in tmpB.Weeks
                            succes = True
                        Next

                        Dim list As Trinity.cIndexes = tmpB.Indexes
                    End If
                Next
            Next

            If grdSpotts.RowCount < 1
                grdSpotts.Rows.Clear()
                If Campaign.BookedSpots.Count > 1
                    For each spott As Trinity.cBookedSpot in listOfRemovableSpotts
                        Dim newRow As Integer = grdSpotts.Rows.Add
                        grdSpotts.Rows(newRow).Tag = spott
                    Next
                End if
                If Campaign.ActualSpots.Count > 1
                    For each spott As Trinity.cActualSpot in listOfRemovableAcualSpotts
                        Dim newRow As Integer = grdSpotts.Rows.Add
                        grdSpotts.Rows(newRow).Tag = spott
                    Next
                End if
            End If
        End If
    End Sub

    Private Sub grdSpotts_CellValueNeeded(sender As Object, e As DataGridViewCellValueEventArgs) Handles grdSpotts.CellValueNeeded
       
        If listOfRemovableAcualSpotts.Count > 1
            
            Dim x As Trinity.cActualSpot = listOfRemovableAcualSpotts(e.RowIndex)
            If x Is Nothing Then Exit Sub
            Select Case e.ColumnIndex
                Case colSelected.Index
                    e.Value = True
                Case colDate.Index
                    e.Value = x.AirDate
                Case colTRP.Index
                    e.Value = ""
            End Select

        End If
        If listOfRemovableSpotts.Count > 1
            
            Dim x As Trinity.cBookedSpot = listOfRemovableSpotts(e.RowIndex)
            If x Is Nothing Then Exit Sub

            Select Case e.ColumnIndex
                Case colSelected.Index
                    e.Value = True
                Case colDate.Index
                    e.Value = x.AirDate
                Case colTRP.Index
                    e.Value = x.MyEstimate
            End Select

        End If
    End Sub

    Private Sub cmdRemoveSpotts_Click(sender As Object, e As EventArgs) Handles cmdRemoveSpotts.Click
        Campaign.ActualSpots.Collection.Clear()
        
    End Sub
End Class