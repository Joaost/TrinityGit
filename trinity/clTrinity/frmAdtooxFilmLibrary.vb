Public Class frmAdtooxFilmLibrary

    Private _selectedFilms As Collection

    Public ReadOnly Property SelectedFilms()
        Get
            Return _selectedFilms
        End Get
    End Property

    Private Sub frmAdtooxFilmLibrary_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmAdtooxFilmLibrary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmbShow.SelectedIndex = 2
    End Sub

    Sub UpdateList()

        Cursor = Windows.Forms.Cursors.WaitCursor

        Dim AdtooxBrand As Long
        Dim AdtooxAdvertiser As Long
        _selectedFilms = New Collection
        If Campaign.AdToox Is Nothing Then Campaign.AdToox = New Trinity.cAdtoox
        Dim table As DataTable = DBReader.getAllFromProducts(Campaign.ProductID)
        If table.Rows.Count = 1 Then
            If Not IsDBNull(table.Rows(0)!AdtooxBrandID) Then
                AdtooxBrand = table.Rows(0)!AdtooxBrandID
            End If
            If Not IsDBNull(table.Rows(0)!AdtooxAdvertiserID) Then
                AdtooxAdvertiser = table.Rows(0)!AdtooxAdvertiserID
            End If
        End If

        grdAdtooxFilms.Rows.Clear()

        Dim frmProg As New frmProgress With {.Text = "Fetching films from E.C. Express..."}
        frmProg.Show()
        Dim i As Integer = 0
        Dim Customers As Trinity.cAdtooxCustomers = Campaign.AdToox.GetCustomersForUser
        grdAdtooxFilms.Rows.Clear()
        If cmbShow.SelectedIndex = 0 Then
            If Customers.Count = 0 Then
                Windows.Forms.MessageBox.Show("Adtoox returned no films. Have you entered your username and password" & vbNewLine _
                                              & "in the Adtoox section of Setup?")
                Exit Sub
            End If
            For Each Customer As Trinity.cAdtooxCustomer In Customers
                For Each film As Trinity.cAdTooxFilmcode In Campaign.AdToox.GetFilmCodesForCustomer(Customer.ID)
                    grdAdtooxFilms.Rows.Add()
                    grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Cells(0).Value = film.AdvertiserName
                    grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Cells(1).Value = film.BrandName
                    grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Cells(2).Value = film.Title
                    grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Cells(3).Value = film.Length
                    grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Cells(4).Value = film.CopyCode
                    If film.FirstAirdate < DateSerial(1999, 12, 31) Then
                        grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Cells(5).Value = "Not yet aired"
                    Else
                        grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Cells(5).Value = Format(film.FirstAirdate, "yyyy-MM-dd")
                    End If
                    grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Tag = film
                Next
                i += 1
                frmProg.Progress = (i * 100) / Customers.Count
            Next
        Else
            Dim films As Trinity.cAdTooxFilmCodes = Campaign.AdToox.GetFilmCodesForCustomer(AdtooxAdvertiser)
            For Each film As Trinity.cAdTooxFilmcode In films
                If cmbShow.SelectedIndex = 1 OrElse film.BrandID = AdtooxBrand Then
                    grdAdtooxFilms.Rows.Add()
                    grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Cells(0).Value = film.AdvertiserName
                    grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Cells(1).Value = film.BrandName
                    grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Cells(2).Value = film.Title
                    grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Cells(3).Value = film.Length
                    grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Cells(4).Value = film.CopyCode
                    If film.FirstAirdate < DateSerial(1999, 12, 31) Then
                        grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Cells(5).Value = "Not yet aired"
                    Else
                        grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Cells(5).Value = Format(film.FirstAirdate, "yyyy-MM-dd")
                    End If
                    grdAdtooxFilms.Rows(grdAdtooxFilms.Rows.Count - 1).Tag = film
                    i += 1
                    frmProg.Progress = (i * 100) / films.Count
                End If
            Next
        End If
        frmProg.Close()
        Cursor = Windows.Forms.Cursors.Default

    End Sub

    Private Sub grdAdtooxFilms_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAdtooxFilms.CellClick

        If e.RowIndex < 0 Then Exit Sub

        If grdAdtooxFilms.Rows(e.RowIndex).Selected Then
            grdAdtooxFilms.Rows(e.RowIndex).Selected = False
        Else
            grdAdtooxFilms.Rows(e.RowIndex).Selected = True
        End If
        If e.ColumnIndex = 4 Then
            'Dim bws As New Windows.Forms.WebBrowser
            'Me.Controls.Add(bws)
            'bws.Dock = Windows.Forms.DockStyle.Fill
            'bws.Visible = True
            'AddHandler bws.DocumentCompleted, AddressOf ShowFilmWindow
            'bws.Navigate(DirectCast(grdAdtooxFilms.Rows(e.RowIndex).Tag, Trinity.cAdTooxFilmcode).GetLinkToPlayVideo())

            Dim url As String = DirectCast(grdAdtooxFilms.Rows(e.RowIndex).Tag, Trinity.cAdTooxFilmcode).GetLinkToPlayVideo()

            Dim WC As System.Net.WebRequest
            Dim WR As System.Net.WebResponse

            WC = System.Net.WebRequest.Create(url)
            WR = WC.GetResponse

            Dim streamResponse As IO.Stream = WR.GetResponseStream()

            Dim SReader As New IO.StreamReader(streamResponse)
            Dim TmpString As String

            TmpString = SReader.ReadToEnd

            SReader.Close()
            streamResponse.Close()
            WR.Close()

            Dim func As String = TmpString.Substring(TmpString.IndexOf("writePlayer('"), TmpString.IndexOf(");", TmpString.IndexOf("writePlayer('")) - TmpString.IndexOf("writePlayer('"))
            Dim flvMovie As String = func.Substring(func.IndexOf("'"), func.IndexOf("'", func.IndexOf("'") + 1) - func.IndexOf("'")).TrimStart("'")
            func = func.Substring(func.IndexOf(flvMovie) + flvMovie.Length + 2).Trim.TrimStart("'")
            Dim previewPicture As String = func.Substring(0, func.IndexOf("'"))
            func = func.Substring(previewPicture.Length).Trim.TrimStart("'").Trim.Trim(",")
            Dim type As Integer = 1
            Dim copycode As String = func.Substring(func.IndexOf(",")).Trim.TrimStart(",").Trim.Trim("""")
            Dim flashvars As String = "type=flv&file=" & flvMovie & "&image=" & previewPicture
            Dim frmFlash As New frmFlashPlayer("https://ecexpress.adtoox.com/ec/flvplayer.swf", flashvars, 519, 312)
            frmFlash.Text = grdAdtooxFilms.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            frmFlash.Show()
            '<embed id="single" height="312" width="519" flashvars="type=flv&file=fileextractor/61285529_clean_proxy.flv%3Fforceattach%3Dtrue%26id%3D61285540%26ccpd%3D8JosF4SW029%26ccid%3D61285528%26name%3D61285529_clean_proxy.flv&image=fileextractor/61285529_picture.jpg%3Fforceattach%3Dtrue%26id%3D61285543%26ccpd%3D8JosF4SW029%26ccid%3D61285528%26name%3D61285529_picture.jpg&width=519&height=312" allowfullscreen="true" quality="high" name="single" style="" src="https://ecexpress.adtoox.com/ec/flvplayer.swf" type="application/x-shockwave-flash"/>

            'Dim frmAdtoox As New frmAdtooxWindow(CStr(grdAdtooxFilms.Rows(e.RowIndex).Cells(e.ColumnIndex).Value))
            'AddHandler frmAdtoox.PageLoaded, AddressOf ShowFilmWindow
            'frmAdtoox.Show()
        End If
        'grdAdtooxFilms.Refresh()
    End Sub

    Sub ShowFilmWindow(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs)
        Dim html As mshtml.HTMLDocument = DirectCast(sender, Windows.Forms.WebBrowser).Document.DomDocument

        If e.Url.AbsoluteUri = sender.Url.AbsoluteUri Then
            Stop
        End If
    End Sub

    Private Sub cmdPick_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPick.Click
        For Each row As Windows.Forms.DataGridViewRow In grdAdtooxFilms.SelectedRows
            If row.Visible = True Then
                _selectedFilms.Add(row.Tag)
            End If
        Next
        Me.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub grdAdtooxFilms_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAdtooxFilms.CellContentClick

    End Sub

    Private Sub txtFindFilm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFindFilm.TextChanged

        For Each row As Windows.Forms.DataGridViewRow In grdAdtooxFilms.Rows
            Dim cellContents As String = row.Cells(1).Value & row.Cells(2).Value & row.Cells(3).Value & row.Cells(4).Value
            If cellContents.ToUpper.Contains(txtFindFilm.Text.ToUpper) Then
                row.Visible = True
            Else
                row.Visible = False
            End If
        Next
    End Sub

    Private Sub dtPickFirstAirdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPickFirstAirdate.ValueChanged
        For Each row As Windows.Forms.DataGridViewRow In grdAdtooxFilms.Rows
            If DirectCast(row.Tag, Trinity.cAdTooxFilmcode).FirstAirdate < dtPickFirstAirdate.Value Then
                row.Visible = False
            Else
                row.Visible = True
            End If
        Next
    End Sub

    Private Sub chkShowAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        UpdateList()
    End Sub

    Private Sub cmbShow_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbShow.SelectedIndexChanged
        UpdateList()
    End Sub
End Class