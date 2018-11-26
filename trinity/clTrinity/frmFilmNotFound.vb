Public Class frmFilmNotFound

    Dim _filmcode As String
    Dim _channel As String
    Dim _bookingtype As String

    Public Sub New(ByVal Filmcode As String, ByVal FilmLength As Integer, ByVal Channel As String, ByVal Bookingtype As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _channel = Channel
        _bookingtype = Bookingtype
        _filmcode = Filmcode
        lblFilm.Text = _filmcode
        txtFilmLength.Text = FilmLength
    End Sub

    Private Sub frmFilmNotFound_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim TmpFilm As Trinity.cFilm
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType

        cmbFilm.Items.Clear()
        For Each TmpFilm In Campaign.Channels(_channel).BookingTypes(_bookingtype).Weeks(1).Films
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
                    grdFilmDetails.Rows(grdFilmDetails.Rows.Count - 1).Cells(0).Value = lblFilm.Text
                    grdFilmDetails.Rows(grdFilmDetails.Rows.Count - 1).Cells(1).Value = TmpBT.FilmIndex(txtFilmLength.Text)
                    grdFilmDetails.AutoResizeRow(grdFilmDetails.Rows.Count - 1)
                End If
            Next
        Next
        grdFilmDetails.AutoResizeColumns()
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmFilmNotFound_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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
                            TmpWeek.Films(txtFilmName.Text).Filmcode = lblFilm.Text
                            TmpWeek.Films(txtFilmName.Text).Index = TmpBT.FilmIndex(txtFilmLength.Text)
                        End If
                    Next
                Next
            Next
            Me.Hide()
        Else
            For Each TmpWeek In Campaign.Channels(_channel).BookingTypes(_bookingtype).Weeks
                If optChangeOnFilm.Checked Then
                    TmpWeek.Films(cmbFilm.Text).Filmcode = TmpWeek.Films(cmbFilm.Text).Filmcode & "," & lblFilm.Text
                End If
            Next
            Me.Hide()
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