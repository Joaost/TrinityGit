Public Class frmFindFilmInLibrary


    Private Sub frmFindFilmInLibrary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbSettings.Items.Clear()
        cmbSettings.Items.Add("Show films for " & Campaign.Product)
        cmbSettings.Items.Add("Show films for " & Campaign.Client)
        cmbSettings.Items.Add("Show all films")
        cmbSettings.SelectedIndex = 0
        dtFrom.Value = TrinitySettings.DefaultFilmLibraryDate
    End Sub

    Sub UpdateList()
        Dim products As DataTable
        'Dim rd As Odbc.OdbcDataReader
        'Dim sql As String = "SELECT DISTINCT Films.Name,Films.Length,Films.Description,Films.Created FROM Films,Products WHERE (Films.Name like '%" & txtFilm.Text & "%' or Films.Description like '%" & txtFilm.Text & "%') AND Films.Created>=cdate('" & Format(dtFrom.Value, "Short date") & "')"
        If cmbSettings.SelectedIndex = 0 Then
            products = DBReader.findFilmOnProduct(txtFilm.Text, dtFrom.Value, Campaign.ProductID)
            lvwFilms.Items.Clear()
            For Each dr As DataRow In products.Rows
                Dim TmpFilm() As String = {dr("Name"), dr("Length"), dr("Description"), Format(CDate(dr("Created")), "yyyy-MM-dd")}
                Dim TmpItem As New Windows.Forms.ListViewItem(TmpFilm)
                If InStr(TmpFilm(0), txtFilm.Text) > 0 OrElse InStr(TmpFilm(2), txtFilm.Text) > 0 Then
                    lvwFilms.Items.Add(TmpItem)
                End If
            Next
            'sql += " AND Films.Product=" & Campaign.ProductID
        ElseIf cmbSettings.SelectedIndex = 1 Then
            products = DBReader.findFilmOnClient(txtFilm.Text, dtFrom.Value, Campaign.ClientID)
            lvwFilms.Items.Clear()
            For Each dr As DataRow In products.Rows
                Dim TmpFilm() As String = {dr("Name"), dr("Length"), dr("Description"), Format(CDate(dr("Created")), "yyyy-MM-dd")}
                Dim TmpItem As New Windows.Forms.ListViewItem(TmpFilm)
                If InStr(TmpFilm(0), txtFilm.Text) > 0 OrElse InStr(TmpFilm(2), txtFilm.Text) > 0 Then
                    lvwFilms.Items.Add(TmpItem)
                End If
            Next
            'Sql += " AND (Films.Product=Products.ID and Products.ClientID=" & Campaign.ClientID & ")"
        ElseIf cmbSettings.SelectedIndex = 2 Then
            lvwFilms.Items.Clear()
            products = DBReader.getAllFilms()
            lvwFilms.Items.Clear()
            For Each dr As DataRow In products.Rows
                If Not IsDBNull(dr("created")) Then
                    Dim TmpFilm() As String = {dr("Name"), dr("Length"), dr("Description"), Format(CDate(dr("Created")), "yyyy-MM-dd")}
                    Dim TmpItem As New Windows.Forms.ListViewItem(TmpFilm)
                    If InStr(TmpFilm(0), txtFilm.Text) > 0 OrElse InStr(TmpFilm(2), txtFilm.Text) > 0 Then
                        lvwFilms.Items.Add(TmpItem)
                    End If
                End If
            Next
        End If

        'Dim com As New Odbc.OdbcCommand(Sql, DBConn)
        'rd = com.ExecuteReader

        'While rd.Read
        '    Dim TmpFilm() As String = {rd!Name, rd!Length, rd!Description, rd!Created}
        '    Dim TmpItem As New Windows.Forms.ListViewItem(TmpFilm)
        '    lvwFilms.Items.Add(TmpItem)
        'End While

    End Sub

    Private Sub txtFilm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilm.TextChanged
        UpdateList()
    End Sub

    Private Sub dtFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtFrom.ValueChanged
        UpdateList()
    End Sub

    Private Sub lvwFilms_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvwFilms.KeyUp
        If lvwFilms.SelectedItems.Count = 0 Then Exit Sub
        If e.KeyCode = Windows.Forms.Keys.Delete Then
            If cmbSettings.SelectedIndex > 0 Then
                Windows.Forms.MessageBox.Show("Can only delete films when the list is set to show films from this product.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                Exit Sub
            End If
            If Windows.Forms.MessageBox.Show("Are you sure you want to delete this spot from the library?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel) = Windows.Forms.DialogResult.Yes Then
                For i As Integer = 0 To lvwFilms.SelectedItems.Count - 1
                    DBReader.DeleteFilm(lvwFilms.SelectedItems(i).Text, Campaign.ProductID)
                    'Dim com As New Odbc.OdbcCommand("DELETE FROM Films WHERE Name ='" & lvwFilms.SelectedItems(i).Text & "' AND Product=" & Campaign.ProductID, DBConn)
                    'com.ExecuteReader()
                Next
            End If
        End If
        UpdateList()
    End Sub

    Private Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        For i As Integer = 0 To lvwFilms.SelectedItems.Count - 1
            Dim products As New DataTable
            If cmbSettings.SelectedIndex = 0 Then
                products = DBReader.findFilmAndProduct(lvwFilms.SelectedItems(i).Text, Campaign.ProductID)
                
            ElseIf cmbSettings.SelectedIndex = 1 Then
                products = DBReader.findFilmAndProductClient(lvwFilms.SelectedItems(i).Text, Campaign.ClientID)

            ElseIf cmbSettings.SelectedIndex = 2 Then
                products = DBReader.findFilmAndProduct(lvwFilms.SelectedItems(i).Text)
            End If

            For Each TmpChan As Trinity.cChannel In Campaign.Channels
                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                        Dim TmpFilm As Trinity.cFilm = TmpWeek.Films.Add(products.Rows(0)("Name"))
                        TmpFilm.Description = products.Rows(0)("description")
                        TmpFilm.FilmLength = products.Rows(0)("Length")
                    Next
                Next
            Next

            For Each dr As DataRow In products.Rows
                Dim TmpChan As Trinity.cChannel = Campaign.Channels(dr("Channel"))
                If Not TmpChan Is Nothing Then
                    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                        For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                            Dim TmpFilm As Trinity.cFilm = TmpWeek.Films(dr("Name"))
                            TmpFilm.Filmcode = dr("Filmcode")
                            TmpFilm.GrossIndex = TmpBT.FilmIndex(TmpFilm.FilmLength)
                            If DBReader.getDBType = Trinity.cDBReader.DBTYPE.SQLce Then
                                TmpFilm.Index = dr("Idx")
                            Else
                                TmpFilm.Index = dr("Index")
                            End If
                        Next
                    Next
                End If
            Next
            'End While

            'Dim rd As Odbc.OdbcDataReader
            'Dim sql As String = "SELECT Films.* FROM Films,Products WHERE Films.Name ='" & lvwFilms.SelectedItems(i).Text & "'"
            'If cmbSettings.SelectedIndex = 0 Then
            '    sql += " AND Films.Product=" & Campaign.ProductID
            'ElseIf cmbSettings.SelectedIndex = 1 Then
            '    sql += " AND (Films.Product=Products.ID and Products.ClientID=" & Campaign.ClientID & ")"
            'End If
            'Dim com As New Odbc.OdbcCommand(sql, DBConn)

            'rd = com.ExecuteReader
            'rd.Read()
            'For Each TmpChan As Trinity.cChannel In Campaign.Channels
            '    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
            '        For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
            '            Dim TmpFilm As Trinity.cFilm = TmpWeek.Films.Add(rd!Name)
            '            TmpFilm.Description = rd!description
            '            TmpFilm.FilmLength = rd!Length
            '            TmpFilm.GrossIndex = TmpBT.FilmIndex(TmpFilm.FilmLength)
            '            TmpFilm.Index = rd!Index
            '        Next
            '    Next
            'Next
            'rd.Close()
            'rd = com.ExecuteReader
            'While rd.Read
            '    Dim TmpChan As Trinity.cChannel = Campaign.Channels(rd!Channel)
            '    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
            '        For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
            '            Dim TmpFilm As Trinity.cFilm = TmpWeek.Films(rd!Name)
            '            TmpFilm.Filmcode = rd!FilmCode
            '        Next
            '    Next
            'End While
        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cmbSettings_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSettings.SelectedIndexChanged
        UpdateList()
    End Sub

    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport.Click

        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        Dim Excel As New CultureSafeExcel.Application(False)
        Dim WB As CultureSafeExcel.Workbook = Excel.AddWorkbook

        With WB.sheets(1)
            Dim r As Integer = 2
            .Cells(1, 1).Value = "Name"
            .Cells(1, 2).Value = "Length"
            .Cells(1, 3).Value = "Description"
            .Cells(1, 4).Value = "Created"
            For Each TmpFilm As Windows.Forms.ListViewItem In lvwFilms.Items
                .Cells(r, 1).Value = TmpFilm.Text
                .Cells(r, 2).Value = TmpFilm.SubItems(1).Text
                .Cells(r, 3).Value = TmpFilm.SubItems(2).Text
                .Cells(r, 4).Value = TmpFilm.SubItems(3).Text
                r += 1
            Next
        End With

        Me.Cursor = Windows.Forms.Cursors.Default
        Excel.displayalerts = True
        Excel.ScreenUpdating = True
        Excel.Visible = True
    End Sub

End Class