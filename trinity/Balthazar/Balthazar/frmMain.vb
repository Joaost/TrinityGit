Imports System.Windows.Forms

Public Class frmMain
    Implements IMessageFilter

    Private WithEvents UpdateMe As cUpdateMe
    Public WithEvents tmrCheckNewVersion As Timer

    Private newVersion As Boolean = False

    Private Sub UpdateMe_FoundNewVersion() Handles UpdateMe.FoundNewVersion
        newVersion = True
    End Sub

    Private Sub disposeIcon(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, System.Windows.Forms.NotifyIcon).Dispose()
    End Sub

    Public Sub Clicked(ByVal sender As Object, ByVal e As System.EventArgs)
        'we get the latest version
        DirectCast(sender, System.Windows.Forms.NotifyIcon).Dispose()

        If MessageBox.Show("Save campaign?", "BALTHAZAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            MyEvent.Save()
        End If

        If Windows.Forms.MessageBox.Show("This will restart Balthazar.", "BALTHAZAR", Windows.Forms.MessageBoxButtons.OKCancel, Windows.Forms.MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
            UpdateMe.GetNewVersion()
            End
        End If

    End Sub

    Private Sub tmrCheckNewVersion_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrCheckNewVersion.Tick
        If newVersion Then
            Dim baloon As New System.Windows.Forms.NotifyIcon()
            AddHandler baloon.BalloonTipClicked, AddressOf Me.Clicked
            AddHandler baloon.BalloonTipClosed, AddressOf Me.disposeIcon
            Try
                baloon.Icon = New Icon(My.Resources.balthazar, 16, 16)
            Catch
                Dim bit As New Bitmap(20, 20)
                Dim hIcon As IntPtr = bit.GetHicon()
                Dim icn As Icon = Icon.FromHandle(hIcon)

                baloon.Icon = icn
            End Try
            baloon.BalloonTipTitle = "New Balthazar Version"
            baloon.BalloonTipText = "Click here to download new version."
            baloon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
            baloon.Visible = True
            baloon.ShowBalloonTip(30000)
            tmrCheckNewVersion.Enabled = False
        End If
    End Sub

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs) Handles NewToolStripMenuItem.Click, NewToolStripButton.Click, NewWindowToolStripMenuItem.Click
        ' Create a new instance of the child form.
        Dim frmNew As New frmNew
        For Each TmpForm As Form In Me.MdiChildren
            TmpForm.Close()
            TmpForm.Dispose()
        Next
        If frmNew.ShowDialog() = Windows.Forms.DialogResult.OK Then
            MyEvent = New cEvent
            If frmNew.lvwEvents.SelectedItems(0).Tag IsNot Nothing Then
                MyEvent = Database.GetEventTemplate(frmNew.lvwEvents.SelectedItems(0).Tag.ID)
            End If
        End If
        cmdInstore.Enabled = MyEvent.UseInStore
        cmdSchedule.Enabled = Not MyEvent.UseInStore
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripMenuItem.Click, OpenToolStripButton.Click
        'Dim OpenFileDialog As New OpenFileDialog
        'OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        'OpenFileDialog.Filter = "Balthazar Event files (*.bef)|*.bef|All Files (*.*)|*.*"
        'OpenFileDialog.FileName = "*.bef"
        'If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
        '    Dim FileName As String = OpenFileDialog.FileName
        '    ' TODO: Add code here to open the file.
        '    MyEvent = New cEvent
        '    MyEvent.Load(FileName)

        'End If
        frmOpen.ShowDialog()
        cmdInstore.Enabled = MyEvent.UseInStore
        cmdSchedule.Enabled = Not MyEvent.UseInStore
        Me.Text = "Balthazar - " & MyEvent.Name
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        'Dim SaveFileDialog As New SaveFileDialog
        'SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        'SaveFileDialog.Filter = "Balthazar Event files (*.bef)|*.bef|All Files (*.*)|*.*"

        'If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
        '    Dim FileName As String = SaveFileDialog.FileName
        MyEvent.DatabaseID = -1
        MyEvent.Save()
        'End If
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CutToolStripMenuItem.Click
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CopyToolStripMenuItem.Click
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PasteToolStripMenuItem.Click
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer

    Private Sub cmdSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetup.Click
        frmSetup.MdiParent = Me
        frmSetup.Show()
        frmSetup.BringToFront()
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click, SaveToolStripMenuItem.Click
        frmProgress.Progress = 0
        frmProgress.Show()
        MyEvent.Save()
        frmProgress.Hide()
    End Sub

    Private Sub cmdBudget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBudget.Click
        frmBudget.MdiParent = Me
        frmBudget.Show()
        frmBudget.BringToFront()
    End Sub

    Private Sub cmdExternalBudget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExternalBudget.Click
        Dim Colors As New Dictionary(Of String, Color)
        Dim Lang As New cLanguage

        Colors.Add("Green", Color.FromArgb(0, 153, 153))
        Colors.Add("Pink", Color.FromArgb(255, 0, 153))
        Colors.Add("Yellow", Color.FromArgb(255, 204, 0))
        Colors.Add("Orange", Color.FromArgb(255, 102, 0))

        Dim oldCI As System.Globalization.CultureInfo = _
    System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")

        Dim Excel As Object = CreateObject("excel.application")
        Dim WB As Object = Excel.Workbooks.add

        With WB.Sheets(1)
            .cells.interior.color = Helper.ARGB2Excel(Color.White.ToArgb)
            .rows.rowheight = 15

            'Headline
            With .range("A1:K2")
                .merge()
                .value = Lang("Budget indication") & " - " & MyEvent.Name
                .interior.color = Helper.ARGB2Excel(Colors("Green").ToArgb)
                '.font.color = ARGB2Excel(Color.White.ToArgb)
                .HorizontalAlignment = -4131
                .VerticalAlignment = -4108
                For i As Integer = 7 To 10
                    With .Borders(i)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                Next
            End With
            With .cells(1, 1)
                .font.color = Helper.ARGB2Excel(Color.White.ToArgb)
                .font.size = 16
                .font.bold = True
            End With

            'Info section (client, product etc)
            .cells(4, 1) = Lang("Client")
            .cells(5, 1) = Lang("Our reference")
            .cells(4, 6) = Lang("Period")
            .cells(5, 6) = Lang("Your reference")

            If MyEvent.Client IsNot Nothing Then .cells(4, 3) = MyEvent.Client.Name
            If MyEvent.Product IsNot Nothing Then .cells(4, 9) = MyEvent.Product.Name

            If MyEvent.InternalContacts.DefaultContact Is Nothing Then
                If MyEvent.InternalContacts.Count > 0 Then .cells(5, 3) = MyEvent.InternalContacts(1).Name
            Else
                .cells(5, 3) = MyEvent.InternalContacts.DefaultContact.Name
            End If
            If MyEvent.ExternalContacts.DefaultContact Is Nothing Then
                If MyEvent.ExternalContacts.Count > 0 Then .cells(5, 9) = MyEvent.ExternalContacts(1).Name
            Else
                .cells(5, 9) = MyEvent.ExternalContacts.DefaultContact.Name
            End If

            With .range("A4:B4")
                .merge()
                .interior.color = Helper.ARGB2Excel(Colors("Orange").ToArgb)
            End With
            With .range("C4:E4")
                .merge()
            End With
            With .range("F4:H4")
                .merge()
                .interior.color = Helper.ARGB2Excel(Colors("Orange").ToArgb)
            End With
            With .range("I4:K4")
                .merge()
            End With
            With .range("A5:B5")
                .merge()
                .interior.color = Helper.ARGB2Excel(Colors("Orange").ToArgb)
            End With
            With .range("C5:E5")
                .merge()
            End With
            With .range("F5:H5")
                .merge()
                .interior.color = Helper.ARGB2Excel(Colors("Orange").ToArgb)
            End With
            With .range("I5:K5")
                .merge()
            End With
            With .range("A4:K5")
                For i As Integer = 7 To 12
                    With .Borders(i)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                Next
            End With

            'Planning section
            Dim TopRow As Integer = 7
            Dim r As Integer = TopRow + 1

            .cells(7, 1) = Lang("Planning")
            .cells(7, 5) = Lang("Description")
            .cells(7, 9) = Lang("Hours")
            .cells(7, 10) = Lang("Price")
            .cells(7, 11) = Lang("Sum")
            With .range("A" & TopRow & ":K" & TopRow)
                .interior.color = Helper.ARGB2Excel(Colors("Yellow").ToArgb)
                .font.bold = True
                For i As Integer = 7 To 10
                    With .Borders(i)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                Next
            End With
            For Each TmpCost As cHourCost In MyEvent.Budget.PlanningCosts
                .range("A" & r & ":D" & r).merge()
                .cells(r, 1) = TmpCost.Name
                .range("E" & r & ":H" & r).merge()
                .cells(r, 5) = TmpCost.Description
                .cells(r, 9) = TmpCost.Hours
                .cells(r, 10) = TmpCost.CostPerHourCTC
                .cells(r, 11).NumberFormat = "#,##0 $"
                .cells(r, 11) = TmpCost.CTC
                r += 1
            Next
            .range("A" & r & ":K" & r).merge()
            r += 1
            .range("A" & r & ":H" & r).merge()
            .cells(r, 1) = Lang("Total")
            With .range("A" & r & ":K" & r)
                .interior.color = Helper.ARGB2Excel(Colors("Green").ToArgb)
                .font.bold = True
                .font.color = Helper.ARGB2Excel(Color.White.ToArgb)
            End With
            With .range("A" & TopRow + 1 & ":K" & r)
                For i As Integer = 7 To 12
                    With .Borders(i)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                Next
            End With
            .range("I" & TopRow & ":K" & r).HorizontalAlignment = -4108
            .cells(r, 9).formula = "=SUM(R[" & -MyEvent.Budget.PlanningCosts.Count & "]C:R[-1]C)"
            .cells(r, 11).formula = "=SUM(R[" & -MyEvent.Budget.PlanningCosts.Count & "]C:R[-1]C)"
            .cells(r, 11).numberformat = "#,##0 $"

            'Staff section
            TopRow = r + 2
            r = TopRow + 1

            .cells(TopRow, 1) = Lang("Staff")
            .cells(TopRow, 5) = Lang("Description")
            .cells(TopRow, 7) = Lang("Count")
            .cells(TopRow, 8) = Lang("Days")
            .cells(TopRow, 9) = Lang("Hours")
            .cells(TopRow, 10) = Lang("Price")
            .cells(TopRow, 11) = Lang("Sum")

            With .range("A" & TopRow & ":K" & TopRow)
                .interior.color = Helper.ARGB2Excel(Colors("Yellow").ToArgb)
                .font.bold = True
                For i As Integer = 7 To 10
                    With .Borders(i)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                Next
            End With
            For Each TmpCost As cStaffCategory In MyEvent.Budget.StaffCosts
                .range("A" & r & ":D" & r).merge()
                .cells(r, 1) = TmpCost.Name
                .range("E" & r & ":F" & r).merge()
                .cells(r, 5) = TmpCost.Description
                .cells(r, 7) = TmpCost.Quantity
                .cells(r, 8) = TmpCost.Days
                .cells(r, 9) = TmpCost.HoursPerDay
                .cells(r, 10) = TmpCost.CostPerHourCTC
                .cells(r, 11).NumberFormat = "#,##0 $"
                .cells(r, 11) = TmpCost.CTC
                r += 1
            Next
            .range("A" & r & ":K" & r).merge()
            r += 1
            .range("A" & r & ":F" & r).merge()
            .cells(r, 1) = Lang("Total")
            With .range("A" & r & ":K" & r)
                .interior.color = Helper.ARGB2Excel(Colors("Green").ToArgb)
                .font.bold = True
                .font.color = Helper.ARGB2Excel(Color.White.ToArgb)
            End With
            With .range("A" & TopRow + 1 & ":K" & r)
                For i As Integer = 7 To 12
                    With .Borders(i)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                Next
            End With
            .range("G" & TopRow & ":K" & r).HorizontalAlignment = -4108
            .cells(r, 7).formula = "=SUM(R[" & -MyEvent.Budget.StaffCosts.Count & "]C:R[-1]C)"
            .cells(r, 8).formula = "=SUM(R[" & -MyEvent.Budget.StaffCosts.Count & "]C:R[-1]C)"
            .cells(r, 9).formula = "=SUM(R[" & -MyEvent.Budget.StaffCosts.Count & "]C:R[-1]C)"
            .cells(r, 11).formula = "=SUM(R[" & -MyEvent.Budget.StaffCosts.Count & "]C:R[-1]C)"
            .cells(r, 11).numberformat = "#,##0 $"


            'Material section
            TopRow = r + 2
            r = TopRow + 1

            .cells(TopRow, 1) = Lang("Material")
            .cells(TopRow, 5) = Lang("Description")
            .cells(TopRow, 11) = Lang("Sum")
            With .range("A" & TopRow & ":K" & TopRow)
                .interior.color = Helper.ARGB2Excel(Colors("Yellow").ToArgb)
                .font.bold = True
                For i As Integer = 7 To 10
                    With .Borders(i)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                Next
            End With
            For Each TmpCost As cCost In MyEvent.Budget.MaterialCosts
                .range("A" & r & ":D" & r).merge()
                .cells(r, 1) = TmpCost.Name
                .range("E" & r & ":J" & r).merge()
                .cells(r, 5) = TmpCost.Description
                .cells(r, 11).NumberFormat = "#,##0 $"
                .cells(r, 11) = TmpCost.CTC
                r += 1
            Next
            .range("A" & r & ":K" & r).merge()
            r += 1
            .range("A" & r & ":J" & r).merge()
            .cells(r, 1) = Lang("Total")
            With .range("A" & r & ":K" & r)
                .interior.color = Helper.ARGB2Excel(Colors("Green").ToArgb)
                .font.bold = True
                .font.color = Helper.ARGB2Excel(Color.White.ToArgb)
            End With
            With .range("A" & TopRow + 1 & ":K" & r)
                For i As Integer = 7 To 12
                    With .Borders(i)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                Next
            End With
            .range("K" & TopRow & ":K" & r).HorizontalAlignment = -4108
            .cells(r, 11).formula = "=SUM(R[" & -MyEvent.Budget.PlanningCosts.Count & "]C:R[-1]C)"
            .cells(r, 11).numberformat = "#,##0 $"

            'Logistics section
            TopRow = r + 2
            r = TopRow + 1

            .cells(TopRow, 1) = Lang("Logistics")
            .cells(TopRow, 5) = Lang("Description")
            .cells(TopRow, 11) = Lang("Sum")
            With .range("A" & TopRow & ":K" & TopRow)
                .interior.color = Helper.ARGB2Excel(Colors("Yellow").ToArgb)
                .font.bold = True
                For i As Integer = 7 To 10
                    With .Borders(i)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                Next
            End With
            For Each TmpCost As cCost In MyEvent.Budget.LogisticsCosts
                .range("A" & r & ":D" & r).merge()
                .cells(r, 1) = TmpCost.Name
                .range("E" & r & ":J" & r).merge()
                .cells(r, 5) = TmpCost.Description
                .cells(r, 11).NumberFormat = "#,##0 $"
                .cells(r, 11) = TmpCost.CTC
                r += 1
            Next
            .range("A" & r & ":K" & r).merge()
            r += 1
            .range("A" & r & ":J" & r).merge()
            .cells(r, 1) = Lang("Total")
            With .range("A" & r & ":K" & r)
                .interior.color = Helper.ARGB2Excel(Colors("Green").ToArgb)
                .font.bold = True
                .font.color = Helper.ARGB2Excel(Color.White.ToArgb)
            End With
            With .range("A" & TopRow + 1 & ":K" & r)
                For i As Integer = 7 To 12
                    With .Borders(i)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                Next
            End With
            .range("K" & TopRow & ":K" & r).HorizontalAlignment = -4108
            .cells(r, 11).formula = "=SUM(R[" & -MyEvent.Budget.PlanningCosts.Count & "]C:R[-1]C)"
            .cells(r, 11).numberformat = "#,##0 $"


            .rows(1).Entirerow.rowheight = 30
            .rows(2).Entirerow.rowheight = 30
            .rows(3).Entirerow.rowheight = 30
            .rows(6).Entirerow.rowheight = 30

            .columns(1).ColumnWidth = 26.71
            .columns(2).ColumnWidth = 6
            .columns(3).ColumnWidth = 6
            .columns(4).ColumnWidth = 12
            .columns(5).ColumnWidth = 8.29
            .columns(6).ColumnWidth = 12
            .columns(7).ColumnWidth = 6
            .columns(8).ColumnWidth = 6
            .columns(9).ColumnWidth = 7.43
            .columns(10).ColumnWidth = 10.57
            .columns(11).ColumnWidth = 15.29

        End With

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI

        Excel.ActiveWindow.Zoom = 75
        Excel.Visible = True
    End Sub

    Private Sub cmdSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSchedule.Click
        frmSchedule.MdiParent = Me
        frmSchedule.Show()
        frmSchedule.BringToFront()
    End Sub

    Private Sub cmdStaff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStaff.Click
        If MyEvent.Schedule Is Nothing Then
            MyEvent.Schedule = New cStaffSchedule(MyEvent, True)
        Else
            MyEvent.Schedule.Rebuild()
        End If
        frmStaff.MdiParent = Me
        frmStaff.Show()
        frmStaff.BringToFront()
    End Sub

    Private Sub cmdFollowUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFollowUp.Click
        'frmFollowUp.MdiParent = Me
        'frmFollowUp.Show()
        'frmFollowUp.BringToFront()
        frmQuestionaires.MdiParent = Me
        frmQuestionaires.Show()
    End Sub

    Private Sub PrintStaffScheduleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintStaffSchedule.Click
        Dim Colors As New Dictionary(Of String, Color)
        Dim Lang As New cLanguage

        Colors.Add("Green", Color.FromArgb(0, 153, 153))
        Colors.Add("Pink", Color.FromArgb(255, 0, 153))
        Colors.Add("Yellow", Color.FromArgb(255, 204, 0))
        Colors.Add("Orange", Color.FromArgb(255, 102, 0))

        Dim oldCI As System.Globalization.CultureInfo = _
    System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")

        Dim Excel As Object = CreateObject("excel.application")
        Dim WB As Object = Excel.Workbooks.add

        For Each TmpLocation As cLocation In MyEvent.Locations
            With WB.sheets.add
                .activate()
                .name = TmpLocation.Name
                With .cells(1, 1)
                    .Value = Lang("Work schedule") & " " & TmpLocation.Name
                    .Font.Size = 14
                    .font.bold = True
                End With
                Dim r As Integer = 4
                Dim c As Integer = 7
                .cells(3, 1) = Lang("Date")
                .cells(3, 2) = Lang("Week")
                .cells(3, 3) = Lang("Day")
                .cells(3, 4) = Lang("Start time")
                .cells(3, 5) = Lang("End time")
                .cells(3, 6) = Lang("Shift")

                For Each TmpRole As cRole In MyEvent.Roles
                    .cells(3, c) = Lang("Qty") & " " & TmpRole.Name
                    c += 1
                Next
                For Each TmpDay As cEventDay In TmpLocation.Days
                    For Each TmpShift As cShift In TmpDay.Template.Shifts.Values
                        Dim WD() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
                        .cells(r, 1).value = TmpDay.DayDate
                        .cells(r, 2).value = DatePart(DateInterval.WeekOfYear, TmpDay.DayDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                        .cells(r, 3).value = Lang(WD(Weekday(TmpDay.DayDate, FirstDayOfWeek.Monday) - 1))
                        .cells(r, 4).value = TmpShift.StartTime
                        .cells(r, 5).value = TmpShift.EndTime
                        .cells(r, 6).value = TmpShift.Name
                        c = 7
                        For Each TmpRole As cRole In MyEvent.Roles
                            If TmpShift.Roles.Contains(TmpRole.ID) Then
                                .cells(r, c).value = TmpShift.Roles(TmpRole.ID).Quantity
                            Else
                                .cells(r, c).value = 0
                            End If
                            c += 1
                        Next
                        r += 1
                    Next
                    r += 1
                Next
                For i As Integer = r To 5 Step -1
                    If .cells(i, 2).value = .cells(i - 1, 2).value Then
                        .cells(i, 2).value = ""
                    End If
                Next
                .Columns("A:A").ColumnWidth = 10.14
                For i As Integer = 2 To 6 + MyEvent.Roles.Count
                    .columns(i).EntireColumn.AutoFit()
                Next
            End With
            Excel.ActiveWindow.Zoom = 75
        Next
        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI

        Excel.Visible = True

    End Sub

    Private Sub PrintToolStripButton_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.DropDownOpening
        If Not My.Computer.FileSystem.FileExists(BalthazarSettings.DataFolder & "Docs\externalstaff.doc") Then
            mnuPrintExternalStaff.Enabled = False
        Else
            mnuPrintExternalStaff.Enabled = True
        End If
        If Not My.Computer.FileSystem.FileExists(BalthazarSettings.DataFolder & "Docs\internalstaff.doc") Then
            mnuPrintInternalStaff.Enabled = False
        Else
            mnuPrintInternalStaff.Enabled = True
        End If
        If Not My.Computer.FileSystem.FileExists(BalthazarSettings.DataFolder & "Docs\materials.doc") Then
            mnuPrintMaterial.Enabled = False
        Else
            mnuPrintMaterial.Enabled = True
        End If
    End Sub

    Private Sub PrintExternalStaffBriefToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintExternalStaff.Click
        Dim Word As Object = CreateObject("word.application")
        Dim Lang As New cLanguage
        Dim Driver() As String = {"-", "B", "C"}
        Dim Gender() As String = {Lang("Man"), Lang("Woman"), Lang("Any")}
        'Word.visible = True

        For Each TmpRole As cRole In MyEvent.Roles
            Dim Doc As Object = Word.Documents.Add(Template:="""" & BalthazarSettings.DataFolder & "Docs\externalstaff.doc" & """", NewTemplate:=False, DocumentType:=0)
            Doc.FormFields(1).Result = MyEvent.Client.Name
            Doc.FormFields(2).Result = MyEvent.Product.Name
            Doc.FormFields(3).result = MyEvent.Target
            Doc.FormFields(4).result = TmpRole.Name
            Doc.FormFields(8).result = TmpRole.Description
            Doc.FormFields(9).result = Driver(TmpRole.Driver)
            Doc.FormFields(10).result = TmpRole.MinAge & "-" & TmpRole.MaxAge
            Doc.FormFields(11).result = Gender(TmpRole.Gender - 1)
        Next
        Word.visible = True
    End Sub

    Private Sub mnuPrintInternalStaff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintInternalStaff.Click
        Dim Word As Object = CreateObject("word.application")
        Dim Lang As New cLanguage
        Dim Driver() As String = {"-", "B", "C"}
        Dim Gender() As String = {Lang("Woman"), Lang("Man"), Lang("Any")}
        'Word.visible = True

        For Each TmpRole As cRole In MyEvent.Roles
            Dim Doc As Object = Word.Documents.Add(Template:="""" & BalthazarSettings.DataFolder & "Docs\internalstaff.doc" & """", NewTemplate:=False, DocumentType:=0)
            Doc.FormFields(1).Result = MyEvent.Client.Name
            Doc.FormFields(2).Result = MyEvent.Product.Name
            Doc.FormFields(3).result = MyEvent.Target
            Doc.FormFields(4).result = TmpRole.Name
            Doc.FormFields(8).result = TmpRole.Description
            Doc.FormFields(9).result = Driver(TmpRole.Driver)
            Doc.FormFields(10).result = TmpRole.MinAge & "-" & TmpRole.MaxAge
            Doc.FormFields(11).result = Gender(TmpRole.Gender - 1)
        Next
        Word.visible = True
    End Sub

    Private Sub mnuPrintMaterial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintMaterial.Click
        Dim Word As Object = CreateObject("word.application")
        Word.Documents.Add(Template:="""" & BalthazarSettings.DataFolder & "Docs\materials.doc" & """", NewTemplate:=False, DocumentType:=0)
        Word.visible = True
    End Sub

    Private Sub mnuPreferences_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPreferences.Click

    End Sub

    Private Sub mnuEditDefaults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditDefaults.Click

    End Sub

    Private Sub PrintExtensiveDocumentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintExtensiveDocumentToolStripMenuItem.Click
        frmPrint.ShowDialog()
    End Sub

    Private Sub cmdDocuments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDocuments.Click
        frmDocuments.MdiParent = Me
        frmDocuments.Show()
    End Sub

    Private _animation As New Animation.BalthazarAnimation
    Private Sub Animate()
        _animation.Start(Me)
    End Sub

    Private Sub frmMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Application.AddMessageFilter(Me)
        tmrCheckNewVersion = New Timer With {.Interval = 2000, .Enabled = True}
        UpdateMe = New cUpdateMe("Balthazar.exe", "http://instore.mecaccess.se/desktopapp/") With {.AutoUpdateInterval = 10000, .CheckAutomaticallyForUpdates = True}
    End Sub

    Public Function PreFilterMessage(ByRef m As System.Windows.Forms.Message) As Boolean Implements System.Windows.Forms.IMessageFilter.PreFilterMessage
        Dim WM_KEYDOWN As Integer = &H100
        Dim WM_KEYUP As Integer = &H101

        Dim keyCode As Keys = CType(CInt(m.WParam), Keys) And Keys.KeyCode
        If m.Msg = WM_KEYDOWN And keyCode = 220 Then
            Animate()
        End If
        Return False

    End Function

    Private Sub cmdInstore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInstore.Click
        If MyEvent.Product Is Nothing Then
            Windows.Forms.MessageBox.Show("You have to choose a product before you can change InStore settings." & vbCrLf & vbCrLf & "Please click the Setup button.", "BALTHAZAR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        frmInStore.MdiParent = Me
        frmInStore.Show()
        frmInStore.BringToFront()
    End Sub

    Private Sub BookingSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BookingSummaryToolStripMenuItem.Click
        frmAllBookings.MdiParent = Me
        frmAllBookings.Show()
    End Sub

    Private Sub EditUsersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditUsersToolStripMenuItem.Click
        frmEditWebUsers.MdiParent = Me
        frmEditWebUsers.Show()
    End Sub

    Private Sub RequestRemoteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestRemoteToolStripMenuItem.Click
        Dim TmpRA As New cRemoteAssistance
        Try
            TmpRA.CheckAndSetRegistry()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("Unable to set registry parameters to enable unsolicited remote assistance." & vbCrLf & _
            "This application will now exit" & vbCrLf & ex.Message & vbCrLf & ex.StackTrace, _
            "Error!", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try
        Try
            If Not TmpRA.RequestAssistance() Then
                Windows.Forms.MessageBox.Show("Unable to create the Remote assistance ticket. Please consult the system admin.", "Remote Assistance", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Else
                Windows.Forms.MessageBox.Show("A Remote assistance request was sent. Please wait for a connection" & vbCrLf & "from one of the system administrators.", "Remote assistance", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message, "ERROR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        frmAbout.lblBuild.Text = "Build: " & UpdateMe.CurrentAppVersion
        frmAbout.ShowDialog()
    End Sub
End Class


