Imports System.Windows.Forms
Imports System.Diagnostics.FileVersionInfo

Public Class frmPostCampaignAnalysis

    Private Adedge As ConnectWrapper.Brands
    Private SplitCount As Long
    Public MaxusBool As Boolean = False
    Sub checkCompany()
        'Dim data As String = TrinitySettings.DataPath

        'data = data.Replace("\\sto-app60\databas\Trinity Data ", "")
        'If data.Contains("Maxus\") Then
        '    chkMaxus.Visible = True
        'End If
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'If chkMaxus.Checked Then
        '    MaxusBool = True
        'Else
        '    MaxusBool = False
        'End If
        Dim strFileName As String
        If chkPlannedGrossConfirmedNet.Checked Then
            Campaign.PrintPlannedGrossConfNet = True
        Else
            Campaign.PrintPlannedGrossConfNet = False
        End If
        If chkPlannedNet.Checked Then
            Campaign.PrintPlannedNet = True
        Else
            Campaign.PrintPlannedNet = False
        End If
        If chkConfirmedNet.Checked Then
            Campaign.PrintConfirmedNet = True
        Else
            Campaign.PrintConfirmedNet = False
        End If

        If cmbTemplate.SelectedIndex < cmbTemplate.Items.Count - 1 Then
            If cmbTemplate.SelectedIndex < My.Computer.FileSystem.GetFiles(DataPath & "Templates\", FileIO.SearchOption.SearchTopLevelOnly, "*.xl*").Count Then
                strFileName = DataPath & "Templates\" & cmbTemplate.Text
            Else
                strFileName = TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork) & "Templates\" & cmbTemplate.Text
            End If
        Else
            strFileName = dlgOpen.FileName
        End If

        '

        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        Dim Excel As New CultureSafeExcel.Application(False)
        frmMain.tmrAutosave.Enabled = False

        '        Dim oldCI As System.Globalization.CultureInfo = _
        'System.Threading.Thread.CurrentThread.CurrentCulture
        '        System.Threading.Thread.CurrentThread.CurrentCulture = _
        '            New System.Globalization.CultureInfo("en-US")

        '        Excel = CreateObject("CultureSafeExcel")



        Excel.DisplayAlerts = False

        Excel.OpenWorkbook(strFileName, UpdateLinks:=False)

        Excel.ScreenUpdating = False

        'If Not Excel.Version = Trinity.Helper.GetExcelVersionIsInstalled Then
        '    If MsgBox("The tamplate was saved with another Excel version. This might cause errors." & vbCrLf & "You you want to continue?", MsgBoxStyle.YesNo, "Wrong Excel Version") = MsgBoxResult.No Then
        '        GoTo _End
        '    End If
        'End If

        Dim Exists As Boolean = False

        Dim Progress As New frmProgress
        Progress.Show()
        For Each TmpSheet As Object In Excel.Workbooks(1).Sheets
            If TmpSheet.Name = "Setup" Then
                Exists = True
                Exit For
            End If
        Next
        If Exists Then
            Campaign.TimeShift = cmbTimeshift.SelectedIndex
            'If Windows.Forms.MessageBox.Show("Använd nya?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            If Not chkAdvanced.Checked Then
                Dim _analysis As Trinity.cPostAnalysisExcel

                If chkAdvanced.Checked Then
                    Dim _comps As New List(Of String)
                    For _r As Integer = 0 To grdChosen.RowCount - 1
                        _comps.Add(grdChosen.Rows(_r).Cells(0).Value)
                    Next
                    _analysis = New Trinity.cAdvancedPostAnalysisExcel(Campaign, Excel, _comps)
                Else
                    _analysis = New Trinity.cPostAnalysisExcel(Campaign, Excel)
                End If
                _analysis.DoNotShowTemplateCheckAlerts = True
                AddHandler _analysis.TemplateError, AddressOf Me.Analysis_Error

                'Disable the individual error messages for each error message


                _analysis.DoPreAnalysis = chkInclude.Checked
                _analysis.SetProgressWindow(Progress)
                _analysis.ShowCombinationsAsSingleRow = chkPrintCombinations.Checked
                _analysis.Run()

                ' Check if any error has occured and the cancel export flag has been raised
                If _analysis.FlagCancelExport Then
                    Me.Cursor = Windows.Forms.Cursors.Default
                    Progress.Hide()
                    Exit Sub
                End If

                If cmbHistoric.SelectedIndex > 0 Then
                    Exists = False
                    For Each TmpSheet As Object In Excel.Workbooks(1).Sheets
                        If TmpSheet.Name = "Historic" Then
                            Exists = True
                            Exit For
                        End If
                    Next
                    If Not Exists Then
                        Windows.Forms.MessageBox.Show("The chosen template does not include a sheet called 'Historic'." & vbCrLf & vbCrLf & "The historic campaign could not be included.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        Dim _historic As New Trinity.cPreAnalysisExcelCustomSheet(Campaign.History(cmbHistoric.SelectedItem), Excel, "Historic")
                        _historic.SetProgressWindow(Progress)
                        _historic.ProgressMessage = "Creating historic campaign analysis... "
                        _historic.Run()
                        If chkHide.Checked Then Excel.Sheets("Historic").Visible = False
                    End If
                End If
            Else
                If chkInclude.Checked Then
                    ExportNewPreAnalysis(Excel:=Excel, singleRowCombinations:=chkPrintCombinations.Checked, includeCompensations:=True, ProgressWindow:=Progress)
                End If
                If chkAdvanced.Checked Then
                    Dim Comps() As String
                    ReDim Comps(grdChosen.Rows.Count - 1)
                    For i As Integer = 1 To grdChosen.Rows.Count
                        Comps(i - 1) = grdChosen.Rows(i - 1).Cells(0).Value
                    Next
                    ExportNewPostAnalysis(Excel:=Excel, singleRowCombinations:=chkPrintCombinations.Checked, Extended:=True, Competitors:=Comps, ProgressWindow:=Progress)
                Else
                    ExportNewPostAnalysis(Excel:=Excel, singleRowCombinations:=chkPrintCombinations.Checked, ProgressWindow:=Progress)
                End If
            End If

        End If

        Progress.Hide()
        If chkHide.Checked Then
            Excel.Sheets("Setup").Visible = False
            Excel.Sheets("Plan").Visible = False
            Excel.Sheets("Actual").Visible = False
            Excel.Sheets("Spotlist").Visible = False
        End If
        Excel.Workbooks(1).SaveAs(Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.Temp) & Excel.Workbooks(1).Name)

_End:
        Excel.DisplayAlerts = True
        Excel.ScreenUpdating = True
        'System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Excel.Visible = True
        Me.Cursor = Windows.Forms.Cursors.Default
        frmMain.tmrAutosave.Enabled = True
    End Sub

    ' Handler when the analysis raises the error event
    Public Sub Analysis_Error(ByVal sender As Object, ByVal e As TemplateErrorArgs)

        Dim errorMessage As String = "One or more potential problems was found when checking the desired template!" & vbNewLine

        For Each tmpStr As String In e.Error
            errorMessage += vbNewLine & tmpStr
        Next

        errorMessage += vbNewLine & vbNewLine & "Do you want to continue anyway?"

        ' The sender should be a cAnalysis so lets see if we can cast it to one
        Dim TmpAnalysis As Trinity.cPostAnalysisExcel = DirectCast(sender, Trinity.cPostAnalysisExcel)

        If MessageBox.Show(errorMessage, "TRINITY", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            TmpAnalysis.FlagCancelExport = False
        Else
            TmpAnalysis.FlagCancelExport = True
        End If

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub chkAdvanced_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAdvanced.CheckedChanged
        If chkAdvanced.Checked Then
            Me.Height = 880
            Me.Width = 500
        Else
            Me.Height = 315
            Me.Width = 305
        End If
    End Sub

    Private Sub frmPostCampaignAnalysis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim File As String
        cmbTemplate.Items.Clear()
        For Each File In My.Computer.FileSystem.GetFiles(DataPath & "Templates\", FileIO.SearchOption.SearchTopLevelOnly, "*.xl*")
            cmbTemplate.Items.Add(My.Computer.FileSystem.GetName(File))
        Next
        If DataPath & "Templates\" <> TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork) & "Templates\" Then
            For Each File In My.Computer.FileSystem.GetFiles(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork) & "Templates\", FileIO.SearchOption.SearchTopLevelOnly, "*.xl*")
                cmbTemplate.Items.Add(My.Computer.FileSystem.GetName(File))
            Next
        End If
        cmbTemplate.Items.Add("Other...")
        cmbTemplate.SelectedIndex = 0
        dtFrom.Value = Date.FromOADate(Campaign.StartDate)
        dtTo.Value = Date.FromOADate(Campaign.EndDate)

        If Campaign.History.Count = 0 Then
            cmbHistoric.Enabled = False
        Else
            cmbHistoric.Enabled = True
            cmbHistoric.Items.Clear()
            cmbHistoric.Items.Add("")
            For Each _kv As KeyValuePair(Of String, Trinity.cKampanj) In Campaign.History
                cmbHistoric.Items.Add(_kv.Key)
            Next
        End If
        cmbTimeshift.SelectedIndex = Campaign.TimeShift

    End Sub

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        Adedge = New ConnectWrapper.Brands
        Dim ChanStr As String = ""
        For x As Integer = 1 To Campaign.Channels.Count
            ChanStr = ChanStr + Campaign.Channels(x).AdEdgeNames + ","
        Next
        Dim FilmStr As String = ""
        For x As Integer = 1 To Campaign.Channels(1).BookingTypes(1).Weeks(1).Films.Count
            FilmStr = FilmStr + Campaign.Channels(1).BookingTypes(1).Weeks(1).Films(x).FilmString + ","
        Next

        Adedge.clearBrandFilter()
        Adedge.clearList()
        Adedge.setBrandType("COMMERCIAL")
        Adedge.setArea(Campaign.Area)
        Adedge.setChannelsArea(ChanStr, Campaign.Area)
        Adedge.setPeriod(Format(dtFrom.Value, "ddMMyy") & "-" & Format(dtTo.Value, "ddMMyy"))
        Trinity.Helper.AddTarget(Adedge, Campaign.MainTarget)
        Adedge.Run(False, False, 0)
        SplitCount = Adedge.setSplitVar("product")
        RefreshList()
    End Sub

    Sub RefreshList()
        Me.Cursor = Cursors.WaitCursor
        grdProducts.Rows.Clear()
        For i As Integer = 0 To SplitCount - 1
            Adedge.setSplitList(i)
            If txtSearch.Text = "" Or InStr(UCase(Adedge.getSplitName(i, 0)), UCase(txtSearch.Text)) > 0 Then
                Dim TmpArray() As String = {Adedge.getSplitName(i, 0), Format(Adedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smSplit, , , Campaign.TargColl(Campaign.MainTarget.TargetName, Adedge) - 1), "N1")}
                grdProducts.Rows.Add(TmpArray)
                grdProducts.Rows(grdProducts.Rows.Count - 1).Tag = i
            End If
        Next
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        RefreshList()
    End Sub

    Private Sub cmdAddCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddCompetitor.Click
        If grdchosen.RowCount < 5 Then
            grdChosen.Rows.Add(grdProducts.SelectedRows.Item(0).Cells(0).Value)
        Else
            Windows.Forms.MessageBox.Show("You can only choose 5 products.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub cmdDeleteCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteCompetitor.Click
        For i As Integer = 0 To grdChosen.SelectedRows.Count - 1
            grdChosen.Rows.Remove(grdChosen.SelectedRows.Item(i))
        Next
    End Sub

    Private Sub grdProducts_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdProducts.CellDoubleClick
        If grdChosen.RowCount < 5 Then
            grdChosen.Rows.Add(grdProducts.SelectedRows.Item(0).Cells(0).Value)
        Else
            Windows.Forms.MessageBox.Show("You can only choose 5 products.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
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

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
