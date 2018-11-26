Public Class frmImportCampaigns

    Private _activeWindow As String = "pnlWelcome"
    Private _fileList As New Dictionary(Of String, String)
    Private _finishedStatus As New List(Of String)

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub frmImportCampaigns_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cmdNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        cmdNext.Enabled = False
        cmdCancel.Enabled = False
        cmdPrevious.Enabled = False
        Select Case _activeWindow
            Case "pnlWelcome"
                If optSingleFile.Checked Then
                    SetActiveWindow("pnlSingleFile")
                Else
                    SetActiveWindow("pnlMultipleFiles")
                End If
            Case "pnlSingleFile"
                If My.Computer.FileSystem.FileExists(txtSingleFileName.Text) Then
                    Dim _xmlFile As System.Xml.Linq.XDocument = Xml.Linq.XDocument.Load(txtSingleFileName.Text)
                    _fileList.Add(txtSingleFileName.Text, _xmlFile.<Campaign>.@Name)
                    DoImport()
                Else
                    Windows.Forms.MessageBox.Show("The file does not exist.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
                    SetActiveWindow("pnlSingleFile")
                    Exit Sub
                End If
            Case "pnlMultipleFiles"
                lblDuplicate.Visible = False
                If My.Computer.FileSystem.DirectoryExists(txtStartFolder.Text) Then
                    ImportFiles(txtStartFolder.Text)
                    Dim _duplicates As List(Of String) = _fileList.GroupBy(Function(f) f.Value).Where(Function(g) g.Count > 1).Select(Function(g) g.Key).ToList
                    Dim _databaseCamps As List(Of String) = (From _ce As CampaignEssentials In DBReader.GetCampaigns("SELECT name FROM campaigns") Select _ce.name).ToList()
                    grdChooseFiles.Rows.Clear()
                    For Each _file As KeyValuePair(Of String, String) In _fileList
                        If _databaseCamps.Contains(_file.Value) Then
                            With grdChooseFiles.Rows(grdChooseFiles.Rows.Add(False, _file.Value, _file.Key, "Overwrite"))
                                DirectCast(.Cells(3), Windows.Forms.DataGridViewComboBoxCell).Items.Clear()
                                DirectCast(.Cells(3), Windows.Forms.DataGridViewComboBoxCell).Items.AddRange("Overwrite", "Rename")
                                .DefaultCellStyle.ForeColor = Color.Red
                                .Cells("colName").ReadOnly = True
                                .Tag = _file.Value
                                lblDuplicate.Visible = True
                            End With
                        ElseIf Not _duplicates.Contains(_file.Value) Then
                            With grdChooseFiles.Rows(grdChooseFiles.Rows.Add(True, _file.Value, _file.Key, ""))
                                .Cells("colAction").ReadOnly = True
                                .Cells("colName").ReadOnly = True
                            End With
                        Else
                            With grdChooseFiles.Rows(grdChooseFiles.Rows.Add(False, _file.Value, _file.Key, "Do nothing"))
                                .DefaultCellStyle.ForeColor = Color.Red
                                .Cells("colName").ReadOnly = True
                                .Tag = _file.Value
                                lblDuplicate.Visible = True
                            End With
                        End If
                    Next
                    grdChooseFiles.Sort(grdChooseFiles.Columns("colName"), ListSortDirection.Ascending)
                Else
                    Windows.Forms.MessageBox.Show("The folder does not exist.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
                    SetActiveWindow("pnlMultipleFiles")
                    Exit Sub
                End If
                SetActiveWindow("pnlChooseFiles")
            Case "pnlChooseFiles"
                Dim _tmpFileList As New Dictionary(Of String, String)
                _finishedStatus.Clear()
                For Each _row As Windows.Forms.DataGridViewRow In grdChooseFiles.Rows
                    If _row.Cells("colImport").Value Then
                        _tmpFileList.Add(_row.Cells("colPath").Value, _row.Cells("colName").Value)
                        If _row.Cells("colAction").Value = "Set 'Cancelled'" Then
                            _finishedStatus.Add(_row.Cells("colPath").Value)
                        End If
                    End If
                Next
                _fileList = _tmpFileList
                DoImport()
            Case "pnlDuplicates"
                DoImport()
        End Select
    End Sub

    Sub DoImport()
        pbImportFiles.Visible = True
        pbImportFiles.Maximum = _fileList.Count
        Dim _databaseCamps As Dictionary(Of String, Long) = (From _ce As CampaignEssentials In DBReader.GetCampaigns("SELECT id,name FROM campaigns") Select _ce).GroupBy(Function(ce) ce.name, Function(ce) ce).ToDictionary(Of String, Long)(Function(ce) ce(0).name, Function(ce) ce(0).id)
        For Each _file As KeyValuePair(Of String, String) In _fileList
            Dim _campaign As New Trinity.cKampanj(TrinitySettings.ErrorChecking)
            Trinity.Helper.MainObject = _campaign
            TrinitySettings.MainObject = _campaign
            Try
                _campaign.LoadCampaign(_file.Key)
                _campaign.ReadOnly = False
                If _finishedStatus.Contains(_file.Key) Then
                    _campaign.Status = "Cancelled"
                End If
                If _databaseCamps.ContainsKey(_file.Value) Then
                    If _campaign.Name = _file.Value OrElse Windows.Forms.MessageBox.Show("You renamed the campaign '" & _campaign.Name & "' to '" & _file.Value & "'," & vbCrLf & "but that campaign name also exists in the database." & vbCrLf & vbCrLf & "Do you want to replace that campaign?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        _campaign.DatabaseID = _databaseCamps(_file.Value)
                        _campaign.SaveCampaign("", True, , , , True)
                    End If
                Else
                    _campaign.SaveCampaign("", True, , , , True)
                End If
            Catch ex As Exception
                Windows.Forms.MessageBox.Show("Could not load campaign '" & _file.Key & "'" & vbCrLf & "Please note the filename and send it to the system admins." & vbCrLf & vbCrLf & "Error: " & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            End Try
            pbImportFiles.Value += 1
            My.Application.DoEvents()
        Next
        Trinity.Helper.MainObject = Campaign
        TrinitySettings.MainObject = Campaign
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Sub ImportFiles(ByVal Directory As String)
        _fileList.Clear()
        pbFiles.Value = 0
        pbFiles.Visible = True
        Dim _option As Microsoft.VisualBasic.FileIO.SearchOption
        Select Case chkDoNotSearchSubFolders.Checked
            Case False
                _option = Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories
            Case Else
                _option = Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly
        End Select

        'Get the list of folders. The lambda expression is to filter out folders from the txtExceludeFolders textbox
        Dim _list As List(Of String) = My.Computer.FileSystem.GetFiles(Directory, _option, "*.cmp").Where(Function(f) Not chkExcludeFolders.Checked OrElse txtExcludeFolders.Text.Split(",").Where(Function(exclude) f.ToString.ToUpper.Contains("\" & exclude.ToUpper & "\")).Count = 0).ToList()

        pbFiles.Maximum = _list.Count

        'Fixa nedan så att lambdan fixar kommaseparerad sträng
        For Each _file As String In _list
            pbFiles.Value += 1
            My.Application.DoEvents()

            Dim _xmlFile As System.Xml.Linq.XDocument = Nothing
            Try
                _xmlFile = Xml.Linq.XDocument.Load(_file)
                If Campaign.Adedge.validate > 0 Then
                    Campaign.Adedge.setPeriod("-1d")
                    Campaign.Adedge.setTargetMnemonic("3+", False)
                    Campaign.Adedge.setArea(Campaign.Area)
                    Campaign.Adedge.setChannels("TV3 se")
                End If
                Dim _dataTo As Long = Campaign.Adedge.getDataRangeTo(Connect.eDataType.mSpot)

                If Not chkOnlyWhereIAmPlannerOrBuyer.Checked OrElse (_xmlFile.<Campaign>.@Buyer = TrinitySettings.UserName OrElse _xmlFile.<Campaign>.@Planner = TrinitySettings.UserName) Then
                    If Not chkOnlyWithBudget.Checked OrElse (From _week As Xml.Linq.XElement In _xmlFile.<Campaign>.<Channels>...<Week> Where _week.@NetBudget > 0).Count > 0 Then
                        If Not chkDate.Checked OrElse _xmlFile.<Campaign>.<Channels>...<Week>.First.@StartDate >= dtDate.Value.ToOADate Then
                            If Not chkOnlyWithRatings.Checked OrElse _xmlFile.<Campaign>.<Channels>...<Week>.Last.@EndDate > _dataTo.ToString OrElse _xmlFile.<Campaign>.<ActualSpots>.<Spot>.Count > 0 Then
                                Try
                                    _fileList.Add(_file, _xmlFile.<Campaign>.@Name)
                                Catch
                                    'Ignore errors while adding
                                End Try
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try

        Next
    End Sub


    Sub SetActiveWindow(ByVal WindowName As String)
        pnlViewport.Controls(_activeWindow).Visible = False
        pnlViewport.Controls(WindowName).Visible = True
        _activeWindow = WindowName
        If _activeWindow = "pnlWelcome" Then
            cmdPrevious.Enabled = False
        Else
            cmdPrevious.Enabled = True
        End If
        If _activeWindow = "pnlSelectSingleFile" OrElse _activeWindow = "pnlChooseFiles" Then
            cmdNext.Text = "Finish"
        Else
            cmdNext.Text = "Next >"
        End If
        cmdNext.Enabled = True
        cmdCancel.Enabled = True
        pbFiles.Visible = False
        pbImportFiles.Visible = False
    End Sub

    Private Sub cmdPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrevious.Click
        Select Case _activeWindow
            Case "pnlSingleFile"
                SetActiveWindow("pnlWelcome")
            Case "pnlMultipleFiles"
                SetActiveWindow("pnlWelcome")
            Case "pnlChooseFiles"
                SetActiveWindow("pnlMultipleFiles")
        End Select
    End Sub

    Private Sub cmdBrowseFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseFolder.Click
        Dim _dlgFolders As New Windows.Forms.FolderBrowserDialog()
        _dlgFolders.SelectedPath = TrinitySettings.CampaignFiles
        _dlgFolders.ShowNewFolderButton = False
        If _dlgFolders.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtStartFolder.Text = _dlgFolders.SelectedPath
        End If
    End Sub

    Private Sub cmdBrowseSingleFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseSingleFile.Click
        Dim _dlgOpen As New Windows.Forms.OpenFileDialog
        _dlgOpen.AddExtension = True
        _dlgOpen.CheckFileExists = True
        _dlgOpen.CheckPathExists = True
        _dlgOpen.DefaultExt = "*.cmp"
        _dlgOpen.Filter = "Trinity campaigns|*.cmp"
        _dlgOpen.InitialDirectory = TrinitySettings.CampaignFiles
        If _dlgOpen.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtSingleFileName.Text = _dlgOpen.FileName
        End If
    End Sub

    Private Sub grdDuplicates_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub grdDuplicates_CellValueNeeded(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs)

    End Sub

    Private Sub grdChooseFiles_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdChooseFiles.CellEndEdit

    End Sub

    Private Sub grdChooseFiles_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdChooseFiles.CellValueChanged
        If grdChooseFiles.Columns(e.ColumnIndex).Name = "colAction" Then
            grdChooseFiles.Rows(e.RowIndex).Cells("colName").ReadOnly = Not grdChooseFiles.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Rename"
            If grdChooseFiles.Rows(e.RowIndex).Cells("colName").ReadOnly Then
                grdChooseFiles.Rows(e.RowIndex).Cells("colName").Value = grdChooseFiles.Rows(e.RowIndex).Tag
            End If
        End If
    End Sub

    Private Sub chkDoNotSearchSubFolders_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDoNotSearchSubFolders.CheckedChanged
        chkExcludeFolders.Enabled = Not chkDoNotSearchSubFolders.Checked
    End Sub

    Private Sub chkExcludeFolders_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkExcludeFolders.CheckedChanged
        txtExcludeFolders.Visible = chkExcludeFolders.Checked
    End Sub

    Private Sub chkExcludeFolders_EnabledChanged(sender As Object, e As System.EventArgs) Handles chkExcludeFolders.EnabledChanged
        txtExcludeFolders.Enabled = chkExcludeFolders.Enabled
    End Sub

    Private Sub chkDate_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDate.CheckedChanged
        dtDate.Enabled = chkDate.Checked
    End Sub
End Class