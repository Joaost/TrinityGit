Public Class frmActualSpotFilmNotFound

    Dim _filmcode As String
    Dim _channel As String

    Public Sub New(ByVal Filmcode As String, ByVal FilmLength As Integer, ByVal Channel As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _channel = Channel
        _filmcode = Filmcode
        lblFilm.Text = _filmcode & " in " & _channel & " (" & FilmLength & " sec)"
        lblFilm.Tag = _filmcode
        txtFilmLength.Text = FilmLength
        optCombination.Checked = True
    End Sub

    Private Sub frmActualSpotFilmNotFound_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim TmpFilm As Trinity.cFilm
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType

        cmbFilm.Items.Clear()
        For Each TmpFilm In Campaign.Channels(_channel).BookingTypes(1).Weeks(1).Films
            cmbFilm.Items.Add(TmpFilm.Name)
        Next
        cmbFilm.SelectedIndex = 0

        txtFilmName.Text = ""
        txtFilmDescription.Text = ""

        grdFilmDetails.Rows.Clear()
        For Each TmpChan In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    grdFilmDetails.Rows.Add()
                    grdFilmDetails.Rows(grdFilmDetails.Rows.Count - 1).HeaderCell.Value = TmpChan.Shortname & " " & TmpBT.Shortname
                    grdFilmDetails.Rows(grdFilmDetails.Rows.Count - 1).Cells(0).Value = lblFilm.Tag.ToString.ToUpper
                    grdFilmDetails.Rows(grdFilmDetails.Rows.Count - 1).Cells(1).Value = TmpBT.FilmIndex(txtFilmLength.Text)
                    grdFilmDetails.AutoResizeRow(grdFilmDetails.Rows.Count - 1)
                End If
            Next
        Next
        grdFilmDetails.AutoResizeColumns()
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtFilmLength_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilmLength.TextChanged
        Dim Chan As String
        Dim BT As String
        Dim i As Integer

        If chkFilmAutoIndex.Checked AndAlso Val(txtFilmLength.Text) > 0 Then
            For i = 0 To grdFilmDetails.Rows.Count - 1
                Chan = LongName(Microsoft.VisualBasic.Left(grdFilmDetails.Rows(i).HeaderCell.Value, InStr(grdFilmDetails.Rows(i).HeaderCell.Value, " ") - 1))
                BT = LongBT(Mid(grdFilmDetails.Rows(i).HeaderCell.Value, InStr(grdFilmDetails.Rows(i).HeaderCell.Value, " ") + 1))
                grdFilmDetails.Rows(i).Cells(1).Value = Campaign.Channels(Chan).BookingTypes(BT).FilmIndex(txtFilmLength.Text)
            Next
        End If

    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek
        Dim i As Integer

        Cursor = Windows.Forms.Cursors.WaitCursor
        If optSetAsNew.Checked Then
            If txtFilmName.Text = "" Then
                MsgBox("You must enter a name for the film", MsgBoxStyle.Critical, "Attention")
                Exit Sub
            End If
            For Each TmpChan In Campaign.Channels
                For Each TmpBT In TmpChan.BookingTypes
                    For Each TmpWeek In TmpBT.Weeks
                        TmpWeek.Films.Add(txtFilmName.Text)
                        TmpWeek.Films(txtFilmName.Text).Description = txtFilmDescription.Text
                        TmpWeek.Films(txtFilmName.Text).FilmLength = txtFilmLength.Text
                        For i = 2 To grdFilmDetails.Rows.Count - 1
                            If TmpChan.Shortname & " " & TmpBT.Name = grdFilmDetails.Rows(i).HeaderCell.Value Then
                                TmpWeek.Films(txtFilmName.Text).Filmcode = grdFilmDetails.Rows(i).Cells(0).Value
                                TmpWeek.Films(txtFilmName.Text).Index = grdFilmDetails.Rows(i).Cells(1).Value
                                Exit For
                            End If
                        Next
                        If TmpWeek.Films(txtFilmName.Text).Filmcode = "" Then
                            TmpWeek.Films(txtFilmName.Text).Filmcode = lblFilm.Tag.ToUpper
                            TmpWeek.Films(txtFilmName.Text).Index = TmpBT.FilmIndex(txtFilmLength.Text)
                        End If
                    Next
                Next
            Next
            Me.Hide()
        Else '
            If optThisChannel.Checked Then
                For Each TmpBT In Campaign.Channels(_channel).BookingTypes
                    For Each TmpWeek In TmpBT.Weeks
                        TmpWeek.Films(cmbFilm.Text).Filmcode = TmpWeek.Films(cmbFilm.Text).Filmcode & "," & lblFilm.Tag.ToUpper
                    Next
                Next
                Me.Hide()
            ElseIf optCombination.Checked Then
                For Each TmpBT In Campaign.Channels(_channel).BookingTypes
                    For Each TmpWeek In TmpBT.Weeks
                        TmpWeek.Films(cmbFilm.Text).Filmcode = TmpWeek.Films(cmbFilm.Text).Filmcode & "," & lblFilm.Tag.ToUpper
                    Next
                    For Each TmpComb As Trinity.cCombination In Campaign.Combinations
                        If TmpComb.IncludesBookingtype(TmpBT) Then
                            For Each TmpCC As Trinity.cCombinationChannel In TmpComb.Relations
                                If TmpCC.Bookingtype IsNot TmpBT Then
                                    For Each TmpWeek In TmpCC.Bookingtype.Weeks
                                        If TmpWeek.Films(lblFilm.Tag.ToString.ToUpper) Is Nothing Then
                                            TmpWeek.Films(cmbFilm.Text).Filmcode = TmpWeek.Films(cmbFilm.Text).Filmcode & "," & lblFilm.Tag.ToUpper
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                    Me.Hide()
                Next
            ElseIf optAllChannels.Checked Then
                For Each TmpChan In Campaign.Channels
                    For Each TmpBT In TmpChan.BookingTypes
                        For Each TmpWeek In TmpBT.Weeks
                            TmpWeek.Films(cmbFilm.Text).Filmcode = TmpWeek.Films(cmbFilm.Text).Filmcode & "," & lblFilm.Tag.ToUpper

                        Next
                    Next
                Next
                Me.Hide()
            End If
        End If
        Cursor = Windows.Forms.Cursors.Default
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub optSetAsNew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSetAsNew.CheckedChanged
        grpFilm.Enabled = True
        grpSetAs.Enabled = False
    End Sub

    Private Sub optSetAsOld_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSetAsOld.CheckedChanged
        grpSetAs.Enabled = True
        grpFilm.Enabled = False
    End Sub

End Class