﻿Imports System.Windows.Forms
Imports clTrinity
Imports System
Imports System.Collections


Public Class frmRemoveSpotts

    Dim listOfRemovableSpotts As new List(Of Trinity.cBookedSpot)
    Dim listOfRemovableAcualSpotts As New List(Of Trinity.cActualSpot)
    Dim listOfRemovableFilmcodes As New List(Of String)
    Dim listOfDistinctRemovableFilmcodes As New List(Of String)

    Public sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        
        If Campaign.BookedSpots.Count > 1 Then
            For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                If Trinity.Helper.FormatDateForBooking(tmpSpot.AirDate) < Trinity.Helper.FormatDateForBooking(Campaign.StartDate) Or Trinity.Helper.FormatDateForBooking(tmpSpot.AirDate) > Trinity.Helper.FormatDateForBooking(Campaign.EndDate) Then
                    Dim tmpDate = Trinity.Helper.FormatDateForBooking(Campaign.EndDate)

                    listOfRemovableSpotts.Add(tmpSpot)
                    'Fast solution to get all filmcodes that needs to be removed.'
                    'Alternativly only use:
                    'For Each tmpFilm As Trinity.cBookedSpot In Campaign.BookedSpots
                    'Debug.Print(tmpFilm.Filmcode)
                    'Next
                    For Each tmpFilm As Trinity.cBookedSpot In Campaign.BookedSpots
                        listOfRemovableFilmcodes.Add(tmpFilm.Filmcode)
                    Next
                    Dim filmResult As List(Of String) = listOfRemovableFilmcodes.Distinct().ToList
                    For Each element As String In filmResult
                        listOfDistinctRemovableFilmcodes.Add(element)
                    Next

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
        Dim removedAcSpotts As Integer = 0
        Dim removedBSpotts As Integer = 0
        Campaign.ActualSpots.Collection.Clear()
        For i As Integer = 1 To Campaign.BookedSpots.Count - 1

            Campaign.BookedSpots.Remove(1)
        Next
        For Each tmpremovedSpotts As Trinity.cBookedSpot In Campaign.BookedSpots

        Next
        Windows.Forms.MessageBox.Show("Removed " & removedBSpotts & " booked sptts and " & removedAcSpotts, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
    End Sub
End Class