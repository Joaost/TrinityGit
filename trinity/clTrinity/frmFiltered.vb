Public Class frmFiltered
    Dim fCol As Collection
    Dim bolPlanned As Boolean = False

    Private Sub frmFiltered_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        grd.RowHeadersVisible = False
        grd.SelectionMode = Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        grd.AllowUserToAddRows = False
    End Sub

    Public Sub addCol(ByVal col As Collection, ByVal Planned As Boolean)
        fCol = col
        Dim idx As Integer
        grd.Columns.Clear()
        If Planned Then
            bolPlanned = True
            grd.Columns.Add("ID", "ID")
            grd.Columns("ID").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("ID").Width = 60
            grd.Columns.Add("Date", "Date")
            grd.Columns("Date").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Date").Width = 40
            grd.Columns.Add("Week", "Week")
            grd.Columns("Week").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Week").Width = 40
            grd.Columns.Add("Time", "Time")
            grd.Columns("Time").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Time").Width = 40
            grd.Columns.Add("Channel", "Channel")
            grd.Columns("Channel").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Channel").Width = 50
            grd.Columns.Add("Bookingtype", "Bookingtype")
            grd.Columns("Bookingtype").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Bookingtype").Width = 70
            grd.Columns.Add("Program", "Program")
            grd.Columns("Program").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill

            grd.Columns.Add("Gross Price", "Gross Price")
            grd.Columns("Gross Price").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Gross Price").Width = 60

            grd.Columns.Add("Net Price", "Net Price")
            grd.Columns("Net Price").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Net Price").Width = 60

            grd.Columns.Add("Chan Est", "Chan Est")
            grd.Columns("Chan Est").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Chan Est").Width = 40
            grd.Columns.Add("My Est", "My Est")
            grd.Columns("My Est").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("My Est").Width = 40

            grd.Columns.Add("Filmcode", "Filmcode")
            grd.Columns("Filmcode").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Filmcode").Width = 60

            grd.Columns.Add("Duration", "Duration")
            grd.Columns("Duration").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Duration").Width = 60

            For Each spot As Trinity.cPlannedSpot In fCol
                idx = grd.Rows.Add()
                grd.Rows(idx).Tag = spot.ID
                Try
                    grd.Rows(idx).Cells(0).Value = spot.ID
                Catch ex As Exception
                    grd.Rows(idx).Cells(0).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(1).Value = Format(Date.FromOADate(spot.AirDate).Date, "yyMMdd")
                Catch ex As Exception
                    grd.Rows(idx).Cells(1).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(2).Value = spot.Week.Name
                Catch ex As Exception
                    grd.Rows(idx).Cells(2).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(3).Value = Trinity.Helper.Mam2Tid(spot.MaM)
                Catch ex As Exception
                    grd.Rows(idx).Cells(3).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(4).Value = spot.Channel.ChannelName
                Catch ex As Exception
                    grd.Rows(idx).Cells(4).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(5).Value = spot.Bookingtype.Name
                Catch ex As Exception
                    grd.Rows(idx).Cells(5).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(6).Value = spot.Programme
                Catch ex As Exception
                    grd.Rows(idx).Cells(6).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(7).Value = spot.PriceGross
                Catch ex As Exception
                    grd.Rows(idx).Cells(7).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(8).Value = spot.PriceNet
                Catch ex As Exception
                    grd.Rows(idx).Cells(8).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(9).Value = spot.ChannelRating
                Catch ex As Exception
                    grd.Rows(idx).Cells(9).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(10).Value = spot.MyRating
                Catch ex As Exception
                    grd.Rows(idx).Cells(10).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(11).Value = spot.Filmcode
                Catch ex As Exception
                    grd.Rows(idx).Cells(11).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(12).Value = spot.SpotLength
                Catch ex As Exception
                    grd.Rows(idx).Cells(12).Value = "N/A"
                End Try
            Next
        Else
            grd.Columns.Add("ID", "ID")
            grd.Columns("ID").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("ID").Width = 60
            grd.Columns.Add("Date", "Date")
            grd.Columns("Date").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Date").Width = 40
            grd.Columns.Add("Week", "Week")
            grd.Columns("Week").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Week").Width = 40
            grd.Columns.Add("Time", "Time")
            grd.Columns("Time").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Time").Width = 40
            grd.Columns.Add("Channel", "Channel")
            grd.Columns("Channel").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Channel").Width = 50
            grd.Columns.Add("Bookingtype", "Bookingtype")
            grd.Columns("Bookingtype").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Bookingtype").Width = 70
            grd.Columns.Add("Program", "Program")
            grd.Columns("Program").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill


            grd.Columns.Add("Rating", "Rating")
            grd.Columns("Rating").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Rating").Width = 40
            grd.Columns.Add("Rating 30", "Rating 30")
            grd.Columns("Rating 30").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Rating 30").Width = 60

            grd.Columns.Add("Filmcode", "Filmcode")
            grd.Columns("Filmcode").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Filmcode").Width = 60

            grd.Columns.Add("Duration", "Duration")
            grd.Columns("Duration").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.None
            grd.Columns("Duration").Width = 60

            For Each spot As Trinity.cActualSpot In fCol
                idx = grd.Rows.Add()
                grd.Rows(idx).Tag = spot.ID
                Try
                    grd.Rows(idx).Cells(0).Value = spot.ID
                Catch ex As Exception
                    grd.Rows(idx).Cells(0).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(1).Value = Format(Date.FromOADate(spot.AirDate).Date, "yyMMdd")
                Catch ex As Exception
                    grd.Rows(idx).Cells(1).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(2).Value = spot.Week.Name
                Catch ex As Exception
                    grd.Rows(idx).Cells(2).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(3).Value = Trinity.Helper.Mam2Tid(spot.MaM)
                Catch ex As Exception
                    grd.Rows(idx).Cells(3).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(4).Value = spot.Channel.ChannelName
                Catch ex As Exception
                    grd.Rows(idx).Cells(4).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(5).Value = spot.Bookingtype.Name
                Catch ex As Exception
                    grd.Rows(idx).Cells(5).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(6).Value = spot.Programme
                Catch ex As Exception
                    grd.Rows(idx).Cells(6).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(7).Value = spot.Rating
                Catch ex As Exception
                    grd.Rows(idx).Cells(7).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(8).Value = spot.Rating30
                Catch ex As Exception
                    grd.Rows(idx).Cells(8).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(9).Value = spot.Filmcode
                Catch ex As Exception
                    grd.Rows(idx).Cells(9).Value = "N/A"
                End Try
                Try
                    grd.Rows(idx).Cells(10).Value = spot.SpotLength
                Catch ex As Exception
                    grd.Rows(idx).Cells(10).Value = "N/A"
                End Try
            Next

        End If
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim TmpRow As Windows.Forms.DataGridViewRow

        If grd.SelectedRows.Count = 0 Then
            Windows.Forms.MessageBox.Show("No spot selected.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If bolPlanned Then
            For Each TmpRow In grd.SelectedRows
                Campaign.PlannedSpots.Remove(TmpRow.Tag)
                grd.Rows.Remove(TmpRow)
            Next
        Else
            For Each TmpRow In grd.SelectedRows
                Campaign.ActualSpots.Remove(TmpRow.Tag)
                grd.Rows.Remove(TmpRow)
            Next
        End If
    End Sub
End Class


                   
                         