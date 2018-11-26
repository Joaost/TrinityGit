Public Class frmPreCampaignAnalysis

    Private Sub frmPreCampaignAnalysis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim File As String
        cmbTemplate.Items.Clear()
        For Each File In My.Computer.FileSystem.GetFiles(DataPath & "Templates\", FileIO.SearchOption.SearchTopLevelOnly, "*.xl*")
            cmbTemplate.Items.Add(My.Computer.FileSystem.GetName(File))
        Next
        If DataPath <> TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork) Then
            For Each File In My.Computer.FileSystem.GetFiles(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork) & "Templates\", FileIO.SearchOption.SearchTopLevelOnly, "*.xl*")
                cmbTemplate.Items.Add(My.Computer.FileSystem.GetName(File))
            Next
        End If
        cmbTemplate.Items.Add("Other...")
        cmbTemplate.SelectedIndex = 0
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If chkPlannedNet.Checked Then
            Campaign.PrintPlannedNet = True
        Else
            Campaign.PrintConfirmedNet = False
        End If
        If chkPlannedGrossConfirmedNet.Checked
            Campaign.PrintPlannedGrossConfNet = True
        Else
            Campaign.PrintPlannedGrossConfNet = False
        End If
        If Campaign.PlannedTotCTC > Campaign.BudgetTotalCTC AndAlso Campaign.BudgetTotalCTC > 0 Then
            If Windows.Forms.MessageBox.Show("The campaign has exceeded the total CTC budget." & vbCrLf & vbCrLf & "Do you still want to print a pre-campaign analysis?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
        End If

        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        Dim Excel As New CultureSafeExcel.Application(False)
        'Excel = CreateObject("CultureSafeExcel")

        Excel.displayalerts = False

        If cmbTemplate.SelectedIndex < cmbTemplate.Items.Count - 1 Then
            If cmbTemplate.SelectedIndex < My.Computer.FileSystem.GetFiles(DataPath & "Templates\", FileIO.SearchOption.SearchTopLevelOnly, "*.xl*").Count Then
                Excel.OpenWorkbook(DataPath & "Templates\" & cmbTemplate.Text)
            Else
                Excel.OpenWorkbook(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork) & "Templates\" & cmbTemplate.Text)
            End If
        Else
            Excel.OpenWorkbook(Filename:=dlgOpen.FileName, UpdateLinks:=False)
        End If
        Dim Exists As Boolean = False
        For Each TmpSheet As Object In Excel.Workbooks(1).Sheets
            If TmpSheet.Name = "Setup" Then
                Exists = True
                Exit For
            End If
        Next
        If Exists Then
            'If Windows.Forms.MessageBox.Show("Använd nya?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            If True Then
                Dim _analysis As New Trinity.cPreAnalysisExcel(Campaign, Excel)
                _analysis.IncludeCompensations = chkCompensations.Checked
                _analysis.ShowCombinationsAsSingleRow = chkPrintCombinations.Checked
                _analysis.ShowProgressWindow = True
                _analysis.Run()
            Else
                ExportNewPreAnalysis(Excel, chkCompensations.Checked, chkPrintCombinations.Checked)
            End If
        Else
            ExportOldPreAnalysis(Excel)
            If chkHide.Checked Then
                Excel.Sheets("Prognos").Visible = False
                Excel.Sheets("Riks").Visible = False
                Excel.Sheets("Sat").Visible = False
            End If
        End If

        Excel.Workbooks(1).saveas(Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.Temp) & Excel.Workbooks(1).Name)
        Excel.displayalerts = True
        '  Excel.screenupdating = True
        Excel.Visible = True
        Me.Cursor = Windows.Forms.Cursors.Default

        Exit Sub

    End Sub

    Private Sub cmbTemplate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTemplate.SelectedIndexChanged
        If cmbTemplate.SelectedIndex = cmbTemplate.Items.Count - 1 Then
            dlgOpen.CheckFileExists = True
            If dlgOpen.ShowDialog <> Windows.Forms.DialogResult.OK Then
                cmbTemplate.SelectedIndex = 0
                Exit Sub
            End If
        End If
    End Sub

End Class