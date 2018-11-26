<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSchedule
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSchedule))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Test", 0)
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.imgColors = New System.Windows.Forms.ImageList(Me.components)
        Me.ttpSchedule = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdCopyToAll = New System.Windows.Forms.Button
        Me.cmdRemoveShift = New System.Windows.Forms.Button
        Me.cmdAddShift = New System.Windows.Forms.Button
        Me.cmdExportNotes = New System.Windows.Forms.Button
        Me.tabSchedule = New System.Windows.Forms.TabControl
        Me.tpDates = New System.Windows.Forms.TabPage
        Me.grpDates = New System.Windows.Forms.GroupBox
        Me.grdImportantDates = New System.Windows.Forms.DataGridView
        Me.cmdRemoveImportantDate = New System.Windows.Forms.Button
        Me.cmdAddImportantDate = New System.Windows.Forms.Button
        Me.tpLocations = New System.Windows.Forms.TabPage
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.grdRoles = New System.Windows.Forms.DataGridView
        Me.colRoleName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colCategory = New Balthazar.ExtendedComboboxColumn
        Me.colRolePerDiem = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colRoleMinAge = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colRoleMaxAge = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colRoleGender = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.colDriver = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.cmdRemoveRole = New System.Windows.Forms.Button
        Me.cmdAddRole = New System.Windows.Forms.Button
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.grdLocations = New System.Windows.Forms.DataGridView
        Me.colLocation = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colFrom = New Balthazar.CalendarColumn
        Me.colTo = New Balthazar.CalendarColumn
        Me.cmdRemoveLocation = New System.Windows.Forms.Button
        Me.cmdAddLocation = New System.Windows.Forms.Button
        Me.tpDayTemplates = New System.Windows.Forms.TabPage
        Me.grpShiftRoles = New System.Windows.Forms.GroupBox
        Me.grdShiftRoles = New System.Windows.Forms.DataGridView
        Me.colShiftRoleName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colShiftRoleQuantity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grpShifts = New System.Windows.Forms.GroupBox
        Me.grdShifts = New System.Windows.Forms.DataGridView
        Me.colShiftName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colShiftDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colShiftStart = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colShiftEnd = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colHeadCount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grpDayTemplates = New System.Windows.Forms.GroupBox
        Me.grdDays = New System.Windows.Forms.DataGridView
        Me.colDayName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDayDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmdRemoveDayTemplate = New System.Windows.Forms.Button
        Me.cmdAddDayTemplate = New System.Windows.Forms.Button
        Me.tpSchedule = New System.Windows.Forms.TabPage
        Me.grpTotalRoles = New System.Windows.Forms.GroupBox
        Me.grdHeadcount = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colHeadcountShifts = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colCTC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.tlpSchedule = New System.Windows.Forms.TableLayoutPanel
        Me.lvwTemplates = New System.Windows.Forms.ListView
        Me.pnlScroll = New System.Windows.Forms.Panel
        Me.pnlLocations = New System.Windows.Forms.TableLayoutPanel
        Me.Label1 = New System.Windows.Forms.Label
        Me.tpBudget = New System.Windows.Forms.TabPage
        Me.grpTotal = New System.Windows.Forms.GroupBox
        Me.lblPercent = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblProfit = New System.Windows.Forms.Label
        Me.lblActualCost = New System.Windows.Forms.Label
        Me.lblCTC = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.pnlLayout = New System.Windows.Forms.TableLayoutPanel
        Me.grpLogistics = New System.Windows.Forms.GroupBox
        Me.grdLogistics = New System.Windows.Forms.DataGridView
        Me.colLogName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colLogDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colLogCTC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colLogActualPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colLogProfit = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmdRemoveLog = New System.Windows.Forms.Button
        Me.cmdAddLog = New System.Windows.Forms.Button
        Me.grpMaterial = New System.Windows.Forms.GroupBox
        Me.grdMaterial = New System.Windows.Forms.DataGridView
        Me.colMaterialName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colMaterialDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colMaterialCTC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colMaterialActualPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colMaterialProfit = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmdRemoveMaterial = New System.Windows.Forms.Button
        Me.cmdAddMaterial = New System.Windows.Forms.Button
        Me.grpStaff = New System.Windows.Forms.GroupBox
        Me.cmdRemoveStaff = New System.Windows.Forms.Button
        Me.cmdAddStaff = New System.Windows.Forms.Button
        Me.grdStaff = New System.Windows.Forms.DataGridView
        Me.colStaffName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colStaffDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colStaffHours = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colStaffPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colStaffCTC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colStaffACHour = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colStaffActualPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colStaffProfit = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grpPlanning = New System.Windows.Forms.GroupBox
        Me.cmdRemovePlanning = New System.Windows.Forms.Button
        Me.cmdAddPlanning = New System.Windows.Forms.Button
        Me.grdPlanning = New System.Windows.Forms.DataGridView
        Me.colPlanningName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colPlanningDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colPlanningHours = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colPlanningPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colPlanningCTC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colPlanningActualCost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colPlanningProfit = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ExtendedComboboxColumn1 = New Balthazar.ExtendedComboboxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CalendarColumn1 = New Balthazar.CalendarColumn
        Me.CalendarColumn2 = New Balthazar.CalendarColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDate = New Balthazar.CalendarColumn
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDateDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colRemindMe = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.tabSchedule.SuspendLayout()
        Me.tpDates.SuspendLayout()
        Me.grpDates.SuspendLayout()
        CType(Me.grdImportantDates, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpLocations.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.grdRoles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.grdLocations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpDayTemplates.SuspendLayout()
        Me.grpShiftRoles.SuspendLayout()
        CType(Me.grdShiftRoles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpShifts.SuspendLayout()
        CType(Me.grdShifts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDayTemplates.SuspendLayout()
        CType(Me.grdDays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpSchedule.SuspendLayout()
        Me.grpTotalRoles.SuspendLayout()
        CType(Me.grdHeadcount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.tlpSchedule.SuspendLayout()
        Me.pnlScroll.SuspendLayout()
        Me.pnlLocations.SuspendLayout()
        Me.tpBudget.SuspendLayout()
        Me.grpTotal.SuspendLayout()
        Me.pnlLayout.SuspendLayout()
        Me.grpLogistics.SuspendLayout()
        CType(Me.grdLogistics, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMaterial.SuspendLayout()
        CType(Me.grdMaterial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpStaff.SuspendLayout()
        CType(Me.grdStaff, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPlanning.SuspendLayout()
        CType(Me.grdPlanning, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'imgColors
        '
        Me.imgColors.ImageStream = CType(resources.GetObject("imgColors.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgColors.TransparentColor = System.Drawing.Color.Transparent
        Me.imgColors.Images.SetKeyName(0, "briefcase.gif")
        '
        'cmdCopyToAll
        '
        Me.cmdCopyToAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCopyToAll.Image = Global.Balthazar.My.Resources.Resources.copy
        Me.cmdCopyToAll.Location = New System.Drawing.Point(450, 79)
        Me.cmdCopyToAll.Name = "cmdCopyToAll"
        Me.cmdCopyToAll.Size = New System.Drawing.Size(24, 24)
        Me.cmdCopyToAll.TabIndex = 12
        Me.ttpSchedule.SetToolTip(Me.cmdCopyToAll, "Copy to all shifts")
        Me.cmdCopyToAll.UseVisualStyleBackColor = True
        '
        'cmdRemoveShift
        '
        Me.cmdRemoveShift.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveShift.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveShift.Location = New System.Drawing.Point(450, 49)
        Me.cmdRemoveShift.Name = "cmdRemoveShift"
        Me.cmdRemoveShift.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveShift.TabIndex = 11
        Me.ttpSchedule.SetToolTip(Me.cmdRemoveShift, "Remove shift")
        Me.cmdRemoveShift.UseVisualStyleBackColor = True
        '
        'cmdAddShift
        '
        Me.cmdAddShift.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddShift.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddShift.Location = New System.Drawing.Point(450, 19)
        Me.cmdAddShift.Name = "cmdAddShift"
        Me.cmdAddShift.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddShift.TabIndex = 10
        Me.ttpSchedule.SetToolTip(Me.cmdAddShift, "Add shift")
        Me.cmdAddShift.UseVisualStyleBackColor = True
        '
        'cmdExportNotes
        '
        Me.cmdExportNotes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExportNotes.Image = Global.Balthazar.My.Resources.Resources.notes
        Me.cmdExportNotes.Location = New System.Drawing.Point(732, 79)
        Me.cmdExportNotes.Name = "cmdExportNotes"
        Me.cmdExportNotes.Size = New System.Drawing.Size(24, 24)
        Me.cmdExportNotes.TabIndex = 11
        Me.ttpSchedule.SetToolTip(Me.cmdExportNotes, "Export to Lotus Notes calendar")
        Me.cmdExportNotes.UseVisualStyleBackColor = True
        '
        'tabSchedule
        '
        Me.tabSchedule.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabSchedule.Controls.Add(Me.tpDates)
        Me.tabSchedule.Controls.Add(Me.tpLocations)
        Me.tabSchedule.Controls.Add(Me.tpDayTemplates)
        Me.tabSchedule.Controls.Add(Me.tpSchedule)
        Me.tabSchedule.Controls.Add(Me.tpBudget)
        Me.tabSchedule.Location = New System.Drawing.Point(1, 1)
        Me.tabSchedule.Name = "tabSchedule"
        Me.tabSchedule.SelectedIndex = 0
        Me.tabSchedule.Size = New System.Drawing.Size(785, 692)
        Me.tabSchedule.TabIndex = 5
        '
        'tpDates
        '
        Me.tpDates.Controls.Add(Me.grpDates)
        Me.tpDates.Location = New System.Drawing.Point(4, 23)
        Me.tpDates.Name = "tpDates"
        Me.tpDates.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDates.Size = New System.Drawing.Size(777, 665)
        Me.tpDates.TabIndex = 5
        Me.tpDates.Text = "Important dates"
        Me.tpDates.UseVisualStyleBackColor = True
        '
        'grpDates
        '
        Me.grpDates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpDates.Controls.Add(Me.cmdExportNotes)
        Me.grpDates.Controls.Add(Me.grdImportantDates)
        Me.grpDates.Controls.Add(Me.cmdRemoveImportantDate)
        Me.grpDates.Controls.Add(Me.cmdAddImportantDate)
        Me.grpDates.Location = New System.Drawing.Point(7, 6)
        Me.grpDates.Name = "grpDates"
        Me.grpDates.Size = New System.Drawing.Size(762, 652)
        Me.grpDates.TabIndex = 11
        Me.grpDates.TabStop = False
        Me.grpDates.Text = "Dates"
        '
        'grdImportantDates
        '
        Me.grdImportantDates.AllowUserToAddRows = False
        Me.grdImportantDates.AllowUserToDeleteRows = False
        Me.grdImportantDates.AllowUserToResizeColumns = False
        Me.grdImportantDates.AllowUserToResizeRows = False
        Me.grdImportantDates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdImportantDates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdImportantDates.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDate, Me.colName, Me.colDateDescription, Me.colRemindMe})
        Me.grdImportantDates.Location = New System.Drawing.Point(6, 19)
        Me.grdImportantDates.Name = "grdImportantDates"
        Me.grdImportantDates.RowHeadersVisible = False
        Me.grdImportantDates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdImportantDates.Size = New System.Drawing.Size(720, 627)
        Me.grdImportantDates.TabIndex = 0
        Me.grdImportantDates.VirtualMode = True
        '
        'cmdRemoveImportantDate
        '
        Me.cmdRemoveImportantDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveImportantDate.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveImportantDate.Location = New System.Drawing.Point(732, 49)
        Me.cmdRemoveImportantDate.Name = "cmdRemoveImportantDate"
        Me.cmdRemoveImportantDate.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveImportantDate.TabIndex = 10
        Me.cmdRemoveImportantDate.UseVisualStyleBackColor = True
        '
        'cmdAddImportantDate
        '
        Me.cmdAddImportantDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddImportantDate.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddImportantDate.Location = New System.Drawing.Point(732, 19)
        Me.cmdAddImportantDate.Name = "cmdAddImportantDate"
        Me.cmdAddImportantDate.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddImportantDate.TabIndex = 9
        Me.cmdAddImportantDate.UseVisualStyleBackColor = True
        '
        'tpLocations
        '
        Me.tpLocations.Controls.Add(Me.GroupBox3)
        Me.tpLocations.Controls.Add(Me.GroupBox4)
        Me.tpLocations.Location = New System.Drawing.Point(4, 23)
        Me.tpLocations.Name = "tpLocations"
        Me.tpLocations.Padding = New System.Windows.Forms.Padding(3)
        Me.tpLocations.Size = New System.Drawing.Size(777, 665)
        Me.tpLocations.TabIndex = 1
        Me.tpLocations.Text = "Locations and roles"
        Me.tpLocations.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.grdRoles)
        Me.GroupBox3.Controls.Add(Me.cmdRemoveRole)
        Me.GroupBox3.Controls.Add(Me.cmdAddRole)
        Me.GroupBox3.Location = New System.Drawing.Point(7, 249)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(762, 237)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Roles"
        '
        'grdRoles
        '
        Me.grdRoles.AllowUserToAddRows = False
        Me.grdRoles.AllowUserToDeleteRows = False
        Me.grdRoles.AllowUserToResizeColumns = False
        Me.grdRoles.AllowUserToResizeRows = False
        Me.grdRoles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdRoles.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRoleName, Me.colDescription, Me.colCategory, Me.colRolePerDiem, Me.colRoleMinAge, Me.colRoleMaxAge, Me.colRoleGender, Me.colDriver})
        Me.grdRoles.Location = New System.Drawing.Point(6, 19)
        Me.grdRoles.Name = "grdRoles"
        Me.grdRoles.RowHeadersVisible = False
        Me.grdRoles.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdRoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdRoles.Size = New System.Drawing.Size(720, 212)
        Me.grdRoles.TabIndex = 6
        Me.grdRoles.VirtualMode = True
        '
        'colRoleName
        '
        Me.colRoleName.HeaderText = "Name"
        Me.colRoleName.Name = "colRoleName"
        Me.colRoleName.Width = 140
        '
        'colDescription
        '
        Me.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colDescription.HeaderText = "Description"
        Me.colDescription.Name = "colDescription"
        '
        'colCategory
        '
        Me.colCategory.HeaderText = "Category"
        Me.colCategory.Name = "colCategory"
        Me.colCategory.Width = 150
        '
        'colRolePerDiem
        '
        Me.colRolePerDiem.HeaderText = "Per Diem"
        Me.colRolePerDiem.Name = "colRolePerDiem"
        Me.colRolePerDiem.Width = 80
        '
        'colRoleMinAge
        '
        Me.colRoleMinAge.HeaderText = "Min Age"
        Me.colRoleMinAge.Name = "colRoleMinAge"
        Me.colRoleMinAge.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colRoleMinAge.Width = 60
        '
        'colRoleMaxAge
        '
        Me.colRoleMaxAge.HeaderText = "Max Age"
        Me.colRoleMaxAge.Name = "colRoleMaxAge"
        Me.colRoleMaxAge.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colRoleMaxAge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colRoleMaxAge.Width = 60
        '
        'colRoleGender
        '
        Me.colRoleGender.HeaderText = "Gender"
        Me.colRoleGender.Items.AddRange(New Object() {"Both", "Male", "Female"})
        Me.colRoleGender.Name = "colRoleGender"
        Me.colRoleGender.Width = 60
        '
        'colDriver
        '
        Me.colDriver.HeaderText = "Driver"
        Me.colDriver.Items.AddRange(New Object() {"-", "B", "C"})
        Me.colDriver.Name = "colDriver"
        Me.colDriver.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colDriver.Width = 40
        '
        'cmdRemoveRole
        '
        Me.cmdRemoveRole.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveRole.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveRole.Location = New System.Drawing.Point(732, 49)
        Me.cmdRemoveRole.Name = "cmdRemoveRole"
        Me.cmdRemoveRole.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveRole.TabIndex = 8
        Me.cmdRemoveRole.UseVisualStyleBackColor = True
        '
        'cmdAddRole
        '
        Me.cmdAddRole.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddRole.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddRole.Location = New System.Drawing.Point(732, 19)
        Me.cmdAddRole.Name = "cmdAddRole"
        Me.cmdAddRole.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddRole.TabIndex = 7
        Me.cmdAddRole.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.grdLocations)
        Me.GroupBox4.Controls.Add(Me.cmdRemoveLocation)
        Me.GroupBox4.Controls.Add(Me.cmdAddLocation)
        Me.GroupBox4.Location = New System.Drawing.Point(7, 6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(762, 237)
        Me.GroupBox4.TabIndex = 9
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Locations"
        '
        'grdLocations
        '
        Me.grdLocations.AllowUserToAddRows = False
        Me.grdLocations.AllowUserToDeleteRows = False
        Me.grdLocations.AllowUserToResizeColumns = False
        Me.grdLocations.AllowUserToResizeRows = False
        Me.grdLocations.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLocations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdLocations.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colLocation, Me.colFrom, Me.colTo})
        Me.grdLocations.Location = New System.Drawing.Point(6, 19)
        Me.grdLocations.Name = "grdLocations"
        Me.grdLocations.RowHeadersVisible = False
        Me.grdLocations.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdLocations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdLocations.Size = New System.Drawing.Size(720, 212)
        Me.grdLocations.TabIndex = 6
        Me.grdLocations.VirtualMode = True
        '
        'colLocation
        '
        Me.colLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colLocation.HeaderText = "Location"
        Me.colLocation.Name = "colLocation"
        '
        'colFrom
        '
        Me.colFrom.HeaderText = "From"
        Me.colFrom.Name = "colFrom"
        '
        'colTo
        '
        Me.colTo.HeaderText = "To"
        Me.colTo.Name = "colTo"
        '
        'cmdRemoveLocation
        '
        Me.cmdRemoveLocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveLocation.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveLocation.Location = New System.Drawing.Point(732, 49)
        Me.cmdRemoveLocation.Name = "cmdRemoveLocation"
        Me.cmdRemoveLocation.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveLocation.TabIndex = 8
        Me.cmdRemoveLocation.UseVisualStyleBackColor = True
        '
        'cmdAddLocation
        '
        Me.cmdAddLocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddLocation.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddLocation.Location = New System.Drawing.Point(732, 19)
        Me.cmdAddLocation.Name = "cmdAddLocation"
        Me.cmdAddLocation.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddLocation.TabIndex = 7
        Me.cmdAddLocation.UseVisualStyleBackColor = True
        '
        'tpDayTemplates
        '
        Me.tpDayTemplates.Controls.Add(Me.grpShiftRoles)
        Me.tpDayTemplates.Controls.Add(Me.grpShifts)
        Me.tpDayTemplates.Controls.Add(Me.grpDayTemplates)
        Me.tpDayTemplates.Location = New System.Drawing.Point(4, 23)
        Me.tpDayTemplates.Name = "tpDayTemplates"
        Me.tpDayTemplates.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDayTemplates.Size = New System.Drawing.Size(777, 665)
        Me.tpDayTemplates.TabIndex = 2
        Me.tpDayTemplates.Text = "Event day templates"
        Me.tpDayTemplates.UseVisualStyleBackColor = True
        '
        'grpShiftRoles
        '
        Me.grpShiftRoles.Controls.Add(Me.grdShiftRoles)
        Me.grpShiftRoles.Location = New System.Drawing.Point(493, 252)
        Me.grpShiftRoles.Name = "grpShiftRoles"
        Me.grpShiftRoles.Size = New System.Drawing.Size(276, 180)
        Me.grpShiftRoles.TabIndex = 2
        Me.grpShiftRoles.TabStop = False
        Me.grpShiftRoles.Text = "Roles"
        Me.grpShiftRoles.Visible = False
        '
        'grdShiftRoles
        '
        Me.grdShiftRoles.AllowUserToAddRows = False
        Me.grdShiftRoles.AllowUserToDeleteRows = False
        Me.grdShiftRoles.AllowUserToResizeColumns = False
        Me.grdShiftRoles.AllowUserToResizeRows = False
        Me.grdShiftRoles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdShiftRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdShiftRoles.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colShiftRoleName, Me.colShiftRoleQuantity})
        Me.grdShiftRoles.Location = New System.Drawing.Point(6, 19)
        Me.grdShiftRoles.Name = "grdShiftRoles"
        Me.grdShiftRoles.RowHeadersVisible = False
        Me.grdShiftRoles.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdShiftRoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdShiftRoles.Size = New System.Drawing.Size(264, 155)
        Me.grdShiftRoles.TabIndex = 10
        Me.grdShiftRoles.VirtualMode = True
        '
        'colShiftRoleName
        '
        Me.colShiftRoleName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colShiftRoleName.FillWeight = 40.0!
        Me.colShiftRoleName.HeaderText = "Name"
        Me.colShiftRoleName.Name = "colShiftRoleName"
        Me.colShiftRoleName.ReadOnly = True
        '
        'colShiftRoleQuantity
        '
        Me.colShiftRoleQuantity.FillWeight = 70.0!
        Me.colShiftRoleQuantity.HeaderText = "Quantity"
        Me.colShiftRoleQuantity.Name = "colShiftRoleQuantity"
        '
        'grpShifts
        '
        Me.grpShifts.Controls.Add(Me.cmdCopyToAll)
        Me.grpShifts.Controls.Add(Me.grdShifts)
        Me.grpShifts.Controls.Add(Me.cmdRemoveShift)
        Me.grpShifts.Controls.Add(Me.cmdAddShift)
        Me.grpShifts.Location = New System.Drawing.Point(7, 252)
        Me.grpShifts.Name = "grpShifts"
        Me.grpShifts.Size = New System.Drawing.Size(480, 180)
        Me.grpShifts.TabIndex = 1
        Me.grpShifts.TabStop = False
        Me.grpShifts.Text = "Shifts"
        Me.grpShifts.Visible = False
        '
        'grdShifts
        '
        Me.grdShifts.AllowUserToAddRows = False
        Me.grdShifts.AllowUserToDeleteRows = False
        Me.grdShifts.AllowUserToResizeColumns = False
        Me.grdShifts.AllowUserToResizeRows = False
        Me.grdShifts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdShifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdShifts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colShiftName, Me.colShiftDescription, Me.colShiftStart, Me.colShiftEnd, Me.colHeadCount})
        Me.grdShifts.Location = New System.Drawing.Point(6, 19)
        Me.grdShifts.Name = "grdShifts"
        Me.grdShifts.RowHeadersVisible = False
        Me.grdShifts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdShifts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdShifts.Size = New System.Drawing.Size(438, 155)
        Me.grdShifts.TabIndex = 9
        Me.grdShifts.VirtualMode = True
        '
        'colShiftName
        '
        Me.colShiftName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colShiftName.FillWeight = 40.0!
        Me.colShiftName.HeaderText = "Name"
        Me.colShiftName.Name = "colShiftName"
        '
        'colShiftDescription
        '
        Me.colShiftDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colShiftDescription.FillWeight = 60.0!
        Me.colShiftDescription.HeaderText = "Description"
        Me.colShiftDescription.Name = "colShiftDescription"
        '
        'colShiftStart
        '
        Me.colShiftStart.HeaderText = "Starts"
        Me.colShiftStart.Name = "colShiftStart"
        Me.colShiftStart.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colShiftStart.Width = 60
        '
        'colShiftEnd
        '
        Me.colShiftEnd.HeaderText = "Ends"
        Me.colShiftEnd.Name = "colShiftEnd"
        Me.colShiftEnd.Width = 60
        '
        'colHeadCount
        '
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray
        Me.colHeadCount.DefaultCellStyle = DataGridViewCellStyle1
        Me.colHeadCount.HeaderText = "Heads"
        Me.colHeadCount.Name = "colHeadCount"
        Me.colHeadCount.ReadOnly = True
        Me.colHeadCount.Width = 70
        '
        'grpDayTemplates
        '
        Me.grpDayTemplates.Controls.Add(Me.grdDays)
        Me.grpDayTemplates.Controls.Add(Me.cmdRemoveDayTemplate)
        Me.grpDayTemplates.Controls.Add(Me.cmdAddDayTemplate)
        Me.grpDayTemplates.Location = New System.Drawing.Point(7, 6)
        Me.grpDayTemplates.Name = "grpDayTemplates"
        Me.grpDayTemplates.Size = New System.Drawing.Size(762, 240)
        Me.grpDayTemplates.TabIndex = 0
        Me.grpDayTemplates.TabStop = False
        Me.grpDayTemplates.Text = "Day templates"
        '
        'grdDays
        '
        Me.grdDays.AllowUserToAddRows = False
        Me.grdDays.AllowUserToDeleteRows = False
        Me.grdDays.AllowUserToResizeColumns = False
        Me.grdDays.AllowUserToResizeRows = False
        Me.grdDays.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdDays.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdDays.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDayName, Me.colDayDescription})
        Me.grdDays.Location = New System.Drawing.Point(6, 19)
        Me.grdDays.Name = "grdDays"
        Me.grdDays.RowHeadersVisible = False
        Me.grdDays.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdDays.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdDays.Size = New System.Drawing.Size(720, 215)
        Me.grdDays.TabIndex = 9
        Me.grdDays.VirtualMode = True
        '
        'colDayName
        '
        Me.colDayName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colDayName.FillWeight = 40.0!
        Me.colDayName.HeaderText = "Name"
        Me.colDayName.Name = "colDayName"
        '
        'colDayDescription
        '
        Me.colDayDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colDayDescription.FillWeight = 60.0!
        Me.colDayDescription.HeaderText = "Description"
        Me.colDayDescription.Name = "colDayDescription"
        '
        'cmdRemoveDayTemplate
        '
        Me.cmdRemoveDayTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveDayTemplate.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveDayTemplate.Location = New System.Drawing.Point(732, 49)
        Me.cmdRemoveDayTemplate.Name = "cmdRemoveDayTemplate"
        Me.cmdRemoveDayTemplate.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveDayTemplate.TabIndex = 11
        Me.cmdRemoveDayTemplate.UseVisualStyleBackColor = True
        '
        'cmdAddDayTemplate
        '
        Me.cmdAddDayTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddDayTemplate.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddDayTemplate.Location = New System.Drawing.Point(732, 19)
        Me.cmdAddDayTemplate.Name = "cmdAddDayTemplate"
        Me.cmdAddDayTemplate.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddDayTemplate.TabIndex = 10
        Me.cmdAddDayTemplate.UseVisualStyleBackColor = True
        '
        'tpSchedule
        '
        Me.tpSchedule.Controls.Add(Me.grpTotalRoles)
        Me.tpSchedule.Controls.Add(Me.GroupBox1)
        Me.tpSchedule.Location = New System.Drawing.Point(4, 23)
        Me.tpSchedule.Name = "tpSchedule"
        Me.tpSchedule.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSchedule.Size = New System.Drawing.Size(777, 665)
        Me.tpSchedule.TabIndex = 3
        Me.tpSchedule.Text = "Schedule"
        Me.tpSchedule.UseVisualStyleBackColor = True
        '
        'grpTotalRoles
        '
        Me.grpTotalRoles.Controls.Add(Me.grdHeadcount)
        Me.grpTotalRoles.Location = New System.Drawing.Point(7, 266)
        Me.grpTotalRoles.Name = "grpTotalRoles"
        Me.grpTotalRoles.Size = New System.Drawing.Size(421, 180)
        Me.grpTotalRoles.TabIndex = 3
        Me.grpTotalRoles.TabStop = False
        Me.grpTotalRoles.Text = "Total hours per role"
        '
        'grdHeadcount
        '
        Me.grdHeadcount.AllowUserToAddRows = False
        Me.grdHeadcount.AllowUserToDeleteRows = False
        Me.grdHeadcount.AllowUserToResizeColumns = False
        Me.grdHeadcount.AllowUserToResizeRows = False
        Me.grdHeadcount.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdHeadcount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdHeadcount.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.colHeadcountShifts, Me.colCTC})
        Me.grdHeadcount.Location = New System.Drawing.Point(6, 19)
        Me.grdHeadcount.Name = "grdHeadcount"
        Me.grdHeadcount.RowHeadersVisible = False
        Me.grdHeadcount.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdHeadcount.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdHeadcount.Size = New System.Drawing.Size(409, 155)
        Me.grdHeadcount.TabIndex = 10
        Me.grdHeadcount.VirtualMode = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.FillWeight = 40.0!
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.FillWeight = 70.0!
        Me.DataGridViewTextBoxColumn2.HeaderText = "Hours"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'colHeadcountShifts
        '
        Me.colHeadcountShifts.HeaderText = "Shifts"
        Me.colHeadcountShifts.Name = "colHeadcountShifts"
        Me.colHeadcountShifts.ReadOnly = True
        Me.colHeadcountShifts.Width = 70
        '
        'colCTC
        '
        DataGridViewCellStyle2.Format = "C0"
        Me.colCTC.DefaultCellStyle = DataGridViewCellStyle2
        Me.colCTC.HeaderText = "CTC"
        Me.colCTC.Name = "colCTC"
        Me.colCTC.ReadOnly = True
        Me.colCTC.Width = 80
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.tlpSchedule)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(762, 254)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Create schedule"
        '
        'tlpSchedule
        '
        Me.tlpSchedule.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpSchedule.ColumnCount = 2
        Me.tlpSchedule.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180.0!))
        Me.tlpSchedule.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpSchedule.Controls.Add(Me.lvwTemplates, 0, 0)
        Me.tlpSchedule.Controls.Add(Me.pnlScroll, 1, 0)
        Me.tlpSchedule.Location = New System.Drawing.Point(6, 19)
        Me.tlpSchedule.Name = "tlpSchedule"
        Me.tlpSchedule.RowCount = 1
        Me.tlpSchedule.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpSchedule.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 229.0!))
        Me.tlpSchedule.Size = New System.Drawing.Size(750, 229)
        Me.tlpSchedule.TabIndex = 0
        '
        'lvwTemplates
        '
        Me.lvwTemplates.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.lvwTemplates.Location = New System.Drawing.Point(3, 3)
        Me.lvwTemplates.Name = "lvwTemplates"
        Me.lvwTemplates.Size = New System.Drawing.Size(174, 138)
        Me.lvwTemplates.SmallImageList = Me.imgColors
        Me.lvwTemplates.TabIndex = 0
        Me.lvwTemplates.UseCompatibleStateImageBehavior = False
        Me.lvwTemplates.View = System.Windows.Forms.View.List
        '
        'pnlScroll
        '
        Me.pnlScroll.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlScroll.AutoScroll = True
        Me.pnlScroll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlScroll.Controls.Add(Me.pnlLocations)
        Me.pnlScroll.Location = New System.Drawing.Point(183, 3)
        Me.pnlScroll.Name = "pnlScroll"
        Me.pnlScroll.Size = New System.Drawing.Size(564, 223)
        Me.pnlScroll.TabIndex = 1
        '
        'pnlLocations
        '
        Me.pnlLocations.AutoSize = True
        Me.pnlLocations.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.pnlLocations.ColumnCount = 2
        Me.pnlLocations.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.pnlLocations.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.pnlLocations.Controls.Add(Me.Label1, 0, 0)
        Me.pnlLocations.Location = New System.Drawing.Point(3, 3)
        Me.pnlLocations.Name = "pnlLocations"
        Me.pnlLocations.RowCount = 1
        Me.pnlLocations.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.pnlLocations.Size = New System.Drawing.Size(48, 18)
        Me.pnlLocations.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'tpBudget
        '
        Me.tpBudget.Controls.Add(Me.grpTotal)
        Me.tpBudget.Controls.Add(Me.pnlLayout)
        Me.tpBudget.Location = New System.Drawing.Point(4, 23)
        Me.tpBudget.Name = "tpBudget"
        Me.tpBudget.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBudget.Size = New System.Drawing.Size(777, 665)
        Me.tpBudget.TabIndex = 4
        Me.tpBudget.Text = "Budget"
        Me.tpBudget.UseVisualStyleBackColor = True
        '
        'grpTotal
        '
        Me.grpTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpTotal.Controls.Add(Me.lblPercent)
        Me.grpTotal.Controls.Add(Me.Label4)
        Me.grpTotal.Controls.Add(Me.lblProfit)
        Me.grpTotal.Controls.Add(Me.lblActualCost)
        Me.grpTotal.Controls.Add(Me.lblCTC)
        Me.grpTotal.Controls.Add(Me.Label3)
        Me.grpTotal.Controls.Add(Me.Label2)
        Me.grpTotal.Controls.Add(Me.Label5)
        Me.grpTotal.Location = New System.Drawing.Point(546, 580)
        Me.grpTotal.Name = "grpTotal"
        Me.grpTotal.Size = New System.Drawing.Size(225, 78)
        Me.grpTotal.TabIndex = 3
        Me.grpTotal.TabStop = False
        Me.grpTotal.Text = "Total"
        '
        'lblPercent
        '
        Me.lblPercent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPercent.AutoSize = True
        Me.lblPercent.ForeColor = System.Drawing.Color.Red
        Me.lblPercent.Location = New System.Drawing.Point(192, 58)
        Me.lblPercent.Name = "lblPercent"
        Me.lblPercent.Size = New System.Drawing.Size(26, 14)
        Me.lblPercent.TabIndex = 7
        Me.lblPercent.Text = "0 %"
        Me.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 14)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "% Profit"
        '
        'lblProfit
        '
        Me.lblProfit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblProfit.AutoSize = True
        Me.lblProfit.Location = New System.Drawing.Point(192, 44)
        Me.lblProfit.Name = "lblProfit"
        Me.lblProfit.Size = New System.Drawing.Size(25, 14)
        Me.lblProfit.TabIndex = 5
        Me.lblProfit.Text = "0 kr"
        Me.lblProfit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblActualCost
        '
        Me.lblActualCost.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblActualCost.AutoSize = True
        Me.lblActualCost.Location = New System.Drawing.Point(192, 30)
        Me.lblActualCost.Name = "lblActualCost"
        Me.lblActualCost.Size = New System.Drawing.Size(25, 14)
        Me.lblActualCost.TabIndex = 4
        Me.lblActualCost.Text = "0 kr"
        Me.lblActualCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCTC
        '
        Me.lblCTC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCTC.AutoSize = True
        Me.lblCTC.Location = New System.Drawing.Point(192, 16)
        Me.lblCTC.Name = "lblCTC"
        Me.lblCTC.Size = New System.Drawing.Size(25, 14)
        Me.lblCTC.TabIndex = 3
        Me.lblCTC.Text = "0 kr"
        Me.lblCTC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Profit"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Actual cost"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(27, 14)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "CTC"
        '
        'pnlLayout
        '
        Me.pnlLayout.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlLayout.ColumnCount = 1
        Me.pnlLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.pnlLayout.Controls.Add(Me.grpLogistics, 0, 3)
        Me.pnlLayout.Controls.Add(Me.grpMaterial, 0, 2)
        Me.pnlLayout.Controls.Add(Me.grpStaff, 0, 1)
        Me.pnlLayout.Controls.Add(Me.grpPlanning, 0, 0)
        Me.pnlLayout.Location = New System.Drawing.Point(7, 3)
        Me.pnlLayout.Name = "pnlLayout"
        Me.pnlLayout.RowCount = 4
        Me.pnlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.pnlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.pnlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.pnlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.pnlLayout.Size = New System.Drawing.Size(764, 571)
        Me.pnlLayout.TabIndex = 2
        '
        'grpLogistics
        '
        Me.grpLogistics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpLogistics.Controls.Add(Me.grdLogistics)
        Me.grpLogistics.Controls.Add(Me.cmdRemoveLog)
        Me.grpLogistics.Controls.Add(Me.cmdAddLog)
        Me.grpLogistics.Location = New System.Drawing.Point(3, 429)
        Me.grpLogistics.Name = "grpLogistics"
        Me.grpLogistics.Size = New System.Drawing.Size(758, 139)
        Me.grpLogistics.TabIndex = 4
        Me.grpLogistics.TabStop = False
        Me.grpLogistics.Text = "Logistics"
        '
        'grdLogistics
        '
        Me.grdLogistics.AllowUserToAddRows = False
        Me.grdLogistics.AllowUserToDeleteRows = False
        Me.grdLogistics.AllowUserToResizeColumns = False
        Me.grdLogistics.AllowUserToResizeRows = False
        Me.grdLogistics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLogistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdLogistics.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colLogName, Me.colLogDescription, Me.colLogCTC, Me.colLogActualPrice, Me.colLogProfit})
        Me.grdLogistics.Location = New System.Drawing.Point(6, 16)
        Me.grdLogistics.Name = "grdLogistics"
        Me.grdLogistics.RowHeadersVisible = False
        Me.grdLogistics.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdLogistics.Size = New System.Drawing.Size(716, 117)
        Me.grdLogistics.TabIndex = 11
        Me.grdLogistics.VirtualMode = True
        '
        'colLogName
        '
        Me.colLogName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colLogName.HeaderText = "Name"
        Me.colLogName.Name = "colLogName"
        '
        'colLogDescription
        '
        Me.colLogDescription.HeaderText = "Description"
        Me.colLogDescription.Name = "colLogDescription"
        Me.colLogDescription.Width = 220
        '
        'colLogCTC
        '
        Me.colLogCTC.HeaderText = "CTC"
        Me.colLogCTC.Name = "colLogCTC"
        Me.colLogCTC.Width = 60
        '
        'colLogActualPrice
        '
        Me.colLogActualPrice.HeaderText = "Actual Price"
        Me.colLogActualPrice.Name = "colLogActualPrice"
        '
        'colLogProfit
        '
        Me.colLogProfit.HeaderText = "Profit"
        Me.colLogProfit.Name = "colLogProfit"
        Me.colLogProfit.Width = 70
        '
        'cmdRemoveLog
        '
        Me.cmdRemoveLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveLog.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveLog.Location = New System.Drawing.Point(728, 49)
        Me.cmdRemoveLog.Name = "cmdRemoveLog"
        Me.cmdRemoveLog.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveLog.TabIndex = 10
        Me.cmdRemoveLog.UseVisualStyleBackColor = True
        '
        'cmdAddLog
        '
        Me.cmdAddLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddLog.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddLog.Location = New System.Drawing.Point(728, 19)
        Me.cmdAddLog.Name = "cmdAddLog"
        Me.cmdAddLog.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddLog.TabIndex = 9
        Me.cmdAddLog.UseVisualStyleBackColor = True
        '
        'grpMaterial
        '
        Me.grpMaterial.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpMaterial.Controls.Add(Me.grdMaterial)
        Me.grpMaterial.Controls.Add(Me.cmdRemoveMaterial)
        Me.grpMaterial.Controls.Add(Me.cmdAddMaterial)
        Me.grpMaterial.Location = New System.Drawing.Point(3, 287)
        Me.grpMaterial.Name = "grpMaterial"
        Me.grpMaterial.Size = New System.Drawing.Size(758, 136)
        Me.grpMaterial.TabIndex = 3
        Me.grpMaterial.TabStop = False
        Me.grpMaterial.Text = "Material"
        '
        'grdMaterial
        '
        Me.grdMaterial.AllowUserToAddRows = False
        Me.grdMaterial.AllowUserToDeleteRows = False
        Me.grdMaterial.AllowUserToResizeColumns = False
        Me.grdMaterial.AllowUserToResizeRows = False
        Me.grdMaterial.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdMaterial.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colMaterialName, Me.colMaterialDescription, Me.colMaterialCTC, Me.colMaterialActualPrice, Me.colMaterialProfit})
        Me.grdMaterial.Location = New System.Drawing.Point(6, 16)
        Me.grdMaterial.Name = "grdMaterial"
        Me.grdMaterial.RowHeadersVisible = False
        Me.grdMaterial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdMaterial.Size = New System.Drawing.Size(716, 114)
        Me.grdMaterial.TabIndex = 11
        Me.grdMaterial.VirtualMode = True
        '
        'colMaterialName
        '
        Me.colMaterialName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colMaterialName.HeaderText = "Name"
        Me.colMaterialName.Name = "colMaterialName"
        '
        'colMaterialDescription
        '
        Me.colMaterialDescription.HeaderText = "Description"
        Me.colMaterialDescription.Name = "colMaterialDescription"
        Me.colMaterialDescription.Width = 220
        '
        'colMaterialCTC
        '
        Me.colMaterialCTC.HeaderText = "CTC"
        Me.colMaterialCTC.Name = "colMaterialCTC"
        Me.colMaterialCTC.Width = 60
        '
        'colMaterialActualPrice
        '
        Me.colMaterialActualPrice.HeaderText = "Actual Price"
        Me.colMaterialActualPrice.Name = "colMaterialActualPrice"
        '
        'colMaterialProfit
        '
        Me.colMaterialProfit.HeaderText = "Profit"
        Me.colMaterialProfit.Name = "colMaterialProfit"
        Me.colMaterialProfit.Width = 70
        '
        'cmdRemoveMaterial
        '
        Me.cmdRemoveMaterial.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveMaterial.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveMaterial.Location = New System.Drawing.Point(728, 49)
        Me.cmdRemoveMaterial.Name = "cmdRemoveMaterial"
        Me.cmdRemoveMaterial.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveMaterial.TabIndex = 10
        Me.cmdRemoveMaterial.UseVisualStyleBackColor = True
        '
        'cmdAddMaterial
        '
        Me.cmdAddMaterial.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddMaterial.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddMaterial.Location = New System.Drawing.Point(728, 19)
        Me.cmdAddMaterial.Name = "cmdAddMaterial"
        Me.cmdAddMaterial.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddMaterial.TabIndex = 9
        Me.cmdAddMaterial.UseVisualStyleBackColor = True
        '
        'grpStaff
        '
        Me.grpStaff.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpStaff.Controls.Add(Me.cmdRemoveStaff)
        Me.grpStaff.Controls.Add(Me.cmdAddStaff)
        Me.grpStaff.Controls.Add(Me.grdStaff)
        Me.grpStaff.Location = New System.Drawing.Point(3, 145)
        Me.grpStaff.Name = "grpStaff"
        Me.grpStaff.Size = New System.Drawing.Size(758, 136)
        Me.grpStaff.TabIndex = 2
        Me.grpStaff.TabStop = False
        Me.grpStaff.Text = "Staff"
        '
        'cmdRemoveStaff
        '
        Me.cmdRemoveStaff.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveStaff.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveStaff.Location = New System.Drawing.Point(728, 49)
        Me.cmdRemoveStaff.Name = "cmdRemoveStaff"
        Me.cmdRemoveStaff.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveStaff.TabIndex = 10
        Me.cmdRemoveStaff.UseVisualStyleBackColor = True
        '
        'cmdAddStaff
        '
        Me.cmdAddStaff.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddStaff.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddStaff.Location = New System.Drawing.Point(728, 19)
        Me.cmdAddStaff.Name = "cmdAddStaff"
        Me.cmdAddStaff.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddStaff.TabIndex = 9
        Me.cmdAddStaff.UseVisualStyleBackColor = True
        '
        'grdStaff
        '
        Me.grdStaff.AllowUserToAddRows = False
        Me.grdStaff.AllowUserToDeleteRows = False
        Me.grdStaff.AllowUserToResizeColumns = False
        Me.grdStaff.AllowUserToResizeRows = False
        Me.grdStaff.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdStaff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdStaff.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colStaffName, Me.colStaffDescription, Me.colStaffHours, Me.colStaffPrice, Me.colStaffCTC, Me.colStaffACHour, Me.colStaffActualPrice, Me.colStaffProfit})
        Me.grdStaff.Location = New System.Drawing.Point(6, 19)
        Me.grdStaff.Name = "grdStaff"
        Me.grdStaff.RowHeadersVisible = False
        Me.grdStaff.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdStaff.Size = New System.Drawing.Size(716, 111)
        Me.grdStaff.TabIndex = 0
        Me.grdStaff.VirtualMode = True
        '
        'colStaffName
        '
        Me.colStaffName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colStaffName.HeaderText = "Name"
        Me.colStaffName.Name = "colStaffName"
        '
        'colStaffDescription
        '
        Me.colStaffDescription.HeaderText = "Description"
        Me.colStaffDescription.Name = "colStaffDescription"
        Me.colStaffDescription.Width = 120
        '
        'colStaffHours
        '
        Me.colStaffHours.HeaderText = "Man hours"
        Me.colStaffHours.Name = "colStaffHours"
        Me.colStaffHours.Width = 90
        '
        'colStaffPrice
        '
        Me.colStaffPrice.HeaderText = "CTC/Hour"
        Me.colStaffPrice.Name = "colStaffPrice"
        Me.colStaffPrice.Width = 60
        '
        'colStaffCTC
        '
        Me.colStaffCTC.HeaderText = "CTC"
        Me.colStaffCTC.Name = "colStaffCTC"
        Me.colStaffCTC.Width = 60
        '
        'colStaffACHour
        '
        Me.colStaffACHour.HeaderText = "AC/Hour"
        Me.colStaffACHour.Name = "colStaffACHour"
        Me.colStaffACHour.Width = 60
        '
        'colStaffActualPrice
        '
        Me.colStaffActualPrice.HeaderText = "Actual Cost"
        Me.colStaffActualPrice.Name = "colStaffActualPrice"
        Me.colStaffActualPrice.Width = 90
        '
        'colStaffProfit
        '
        Me.colStaffProfit.HeaderText = "Profit"
        Me.colStaffProfit.Name = "colStaffProfit"
        Me.colStaffProfit.Width = 70
        '
        'grpPlanning
        '
        Me.grpPlanning.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpPlanning.Controls.Add(Me.cmdRemovePlanning)
        Me.grpPlanning.Controls.Add(Me.cmdAddPlanning)
        Me.grpPlanning.Controls.Add(Me.grdPlanning)
        Me.grpPlanning.Location = New System.Drawing.Point(3, 3)
        Me.grpPlanning.Name = "grpPlanning"
        Me.grpPlanning.Size = New System.Drawing.Size(758, 136)
        Me.grpPlanning.TabIndex = 1
        Me.grpPlanning.TabStop = False
        Me.grpPlanning.Text = "Planning"
        '
        'cmdRemovePlanning
        '
        Me.cmdRemovePlanning.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemovePlanning.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemovePlanning.Location = New System.Drawing.Point(728, 49)
        Me.cmdRemovePlanning.Name = "cmdRemovePlanning"
        Me.cmdRemovePlanning.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemovePlanning.TabIndex = 10
        Me.cmdRemovePlanning.UseVisualStyleBackColor = True
        '
        'cmdAddPlanning
        '
        Me.cmdAddPlanning.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddPlanning.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddPlanning.Location = New System.Drawing.Point(728, 19)
        Me.cmdAddPlanning.Name = "cmdAddPlanning"
        Me.cmdAddPlanning.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddPlanning.TabIndex = 9
        Me.cmdAddPlanning.UseVisualStyleBackColor = True
        '
        'grdPlanning
        '
        Me.grdPlanning.AllowUserToAddRows = False
        Me.grdPlanning.AllowUserToDeleteRows = False
        Me.grdPlanning.AllowUserToResizeColumns = False
        Me.grdPlanning.AllowUserToResizeRows = False
        Me.grdPlanning.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdPlanning.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPlanning.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colPlanningName, Me.colPlanningDescription, Me.colPlanningHours, Me.colPlanningPrice, Me.colPlanningCTC, Me.colPlanningActualCost, Me.colPlanningProfit})
        Me.grdPlanning.Location = New System.Drawing.Point(6, 19)
        Me.grdPlanning.Name = "grdPlanning"
        Me.grdPlanning.RowHeadersVisible = False
        Me.grdPlanning.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPlanning.Size = New System.Drawing.Size(716, 111)
        Me.grdPlanning.TabIndex = 0
        Me.grdPlanning.VirtualMode = True
        '
        'colPlanningName
        '
        Me.colPlanningName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colPlanningName.HeaderText = "Name"
        Me.colPlanningName.Name = "colPlanningName"
        '
        'colPlanningDescription
        '
        Me.colPlanningDescription.HeaderText = "Description"
        Me.colPlanningDescription.Name = "colPlanningDescription"
        Me.colPlanningDescription.Width = 220
        '
        'colPlanningHours
        '
        Me.colPlanningHours.HeaderText = "Hours"
        Me.colPlanningHours.Name = "colPlanningHours"
        Me.colPlanningHours.Width = 60
        '
        'colPlanningPrice
        '
        Me.colPlanningPrice.HeaderText = "CTC/Hour"
        Me.colPlanningPrice.Name = "colPlanningPrice"
        Me.colPlanningPrice.Width = 60
        '
        'colPlanningCTC
        '
        Me.colPlanningCTC.HeaderText = "CTC"
        Me.colPlanningCTC.Name = "colPlanningCTC"
        Me.colPlanningCTC.Width = 60
        '
        'colPlanningActualCost
        '
        Me.colPlanningActualCost.HeaderText = "Actual Price"
        Me.colPlanningActualCost.Name = "colPlanningActualCost"
        '
        'colPlanningProfit
        '
        Me.colPlanningProfit.HeaderText = "Profit"
        Me.colPlanningProfit.Name = "colPlanningProfit"
        Me.colPlanningProfit.Width = 70
        '
        'ExtendedComboboxColumn1
        '
        Me.ExtendedComboboxColumn1.HeaderText = "Category"
        Me.ExtendedComboboxColumn1.Name = "ExtendedComboboxColumn1"
        Me.ExtendedComboboxColumn1.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Min Age"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn3.Width = 60
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Max Age"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn4.Width = 60
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn5.HeaderText = "Location"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        '
        'CalendarColumn1
        '
        Me.CalendarColumn1.HeaderText = "From"
        Me.CalendarColumn1.Name = "CalendarColumn1"
        '
        'CalendarColumn2
        '
        Me.CalendarColumn2.HeaderText = "To"
        Me.CalendarColumn2.Name = "CalendarColumn2"
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.FillWeight = 40.0!
        Me.DataGridViewTextBoxColumn6.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.FillWeight = 70.0!
        Me.DataGridViewTextBoxColumn7.HeaderText = "Quantity"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn8.FillWeight = 40.0!
        Me.DataGridViewTextBoxColumn8.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn9.FillWeight = 60.0!
        Me.DataGridViewTextBoxColumn9.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "Starts"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn10.Width = 60
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Ends"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.Width = 60
        '
        'DataGridViewTextBoxColumn12
        '
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Gray
        Me.DataGridViewTextBoxColumn12.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn12.HeaderText = "Heads"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Width = 70
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn13.FillWeight = 40.0!
        Me.DataGridViewTextBoxColumn13.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn14.FillWeight = 60.0!
        Me.DataGridViewTextBoxColumn14.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn15.FillWeight = 40.0!
        Me.DataGridViewTextBoxColumn15.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.FillWeight = 70.0!
        Me.DataGridViewTextBoxColumn16.HeaderText = "Hours"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.HeaderText = "Shifts"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        Me.DataGridViewTextBoxColumn17.Width = 70
        '
        'DataGridViewTextBoxColumn18
        '
        DataGridViewCellStyle4.Format = "C0"
        Me.DataGridViewTextBoxColumn18.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn18.HeaderText = "CTC"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        Me.DataGridViewTextBoxColumn18.Width = 80
        '
        'colDate
        '
        Me.colDate.HeaderText = "Date"
        Me.colDate.Name = "colDate"
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colName.FillWeight = 30.0!
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        '
        'colDateDescription
        '
        Me.colDateDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colDateDescription.FillWeight = 70.0!
        Me.colDateDescription.HeaderText = "Description"
        Me.colDateDescription.Name = "colDateDescription"
        '
        'colRemindMe
        '
        Me.colRemindMe.HeaderText = "Remind me"
        Me.colRemindMe.Name = "colRemindMe"
        Me.colRemindMe.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colRemindMe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'frmSchedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(786, 694)
        Me.Controls.Add(Me.tabSchedule)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmSchedule"
        Me.Text = "Schedule"
        Me.tabSchedule.ResumeLayout(False)
        Me.tpDates.ResumeLayout(False)
        Me.grpDates.ResumeLayout(False)
        CType(Me.grdImportantDates, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpLocations.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.grdRoles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.grdLocations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpDayTemplates.ResumeLayout(False)
        Me.grpShiftRoles.ResumeLayout(False)
        CType(Me.grdShiftRoles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpShifts.ResumeLayout(False)
        CType(Me.grdShifts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDayTemplates.ResumeLayout(False)
        CType(Me.grdDays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpSchedule.ResumeLayout(False)
        Me.grpTotalRoles.ResumeLayout(False)
        CType(Me.grdHeadcount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.tlpSchedule.ResumeLayout(False)
        Me.pnlScroll.ResumeLayout(False)
        Me.pnlScroll.PerformLayout()
        Me.pnlLocations.ResumeLayout(False)
        Me.pnlLocations.PerformLayout()
        Me.tpBudget.ResumeLayout(False)
        Me.grpTotal.ResumeLayout(False)
        Me.grpTotal.PerformLayout()
        Me.pnlLayout.ResumeLayout(False)
        Me.grpLogistics.ResumeLayout(False)
        CType(Me.grdLogistics, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMaterial.ResumeLayout(False)
        CType(Me.grdMaterial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpStaff.ResumeLayout(False)
        CType(Me.grdStaff, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPlanning.ResumeLayout(False)
        CType(Me.grdPlanning, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents imgColors As System.Windows.Forms.ImageList
    Friend WithEvents ttpSchedule As System.Windows.Forms.ToolTip
    Friend WithEvents tabSchedule As System.Windows.Forms.TabControl
    Friend WithEvents tpLocations As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents grdRoles As System.Windows.Forms.DataGridView
    Friend WithEvents cmdRemoveRole As System.Windows.Forms.Button
    Friend WithEvents cmdAddRole As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents grdLocations As System.Windows.Forms.DataGridView
    Friend WithEvents colLocation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFrom As Balthazar.CalendarColumn
    Friend WithEvents colTo As Balthazar.CalendarColumn
    Friend WithEvents cmdRemoveLocation As System.Windows.Forms.Button
    Friend WithEvents cmdAddLocation As System.Windows.Forms.Button
    Friend WithEvents tpDayTemplates As System.Windows.Forms.TabPage
    Friend WithEvents grpShifts As System.Windows.Forms.GroupBox
    Friend WithEvents grdShifts As System.Windows.Forms.DataGridView
    Friend WithEvents cmdRemoveShift As System.Windows.Forms.Button
    Friend WithEvents cmdAddShift As System.Windows.Forms.Button
    Friend WithEvents grpDayTemplates As System.Windows.Forms.GroupBox
    Friend WithEvents grdDays As System.Windows.Forms.DataGridView
    Friend WithEvents colDayName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDayDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdRemoveDayTemplate As System.Windows.Forms.Button
    Friend WithEvents cmdAddDayTemplate As System.Windows.Forms.Button
    Friend WithEvents tpSchedule As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tlpSchedule As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lvwTemplates As System.Windows.Forms.ListView
    Friend WithEvents pnlScroll As System.Windows.Forms.Panel
    Friend WithEvents pnlLocations As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents colShiftName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colShiftDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colShiftStart As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colShiftEnd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colHeadCount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpShiftRoles As System.Windows.Forms.GroupBox
    Friend WithEvents grdShiftRoles As System.Windows.Forms.DataGridView
    Friend WithEvents colShiftRoleName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colShiftRoleQuantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpTotalRoles As System.Windows.Forms.GroupBox
    Friend WithEvents grdHeadcount As System.Windows.Forms.DataGridView
    Friend WithEvents cmdCopyToAll As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colHeadcountShifts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCTC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExtendedComboboxColumn1 As Balthazar.ExtendedComboboxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CalendarColumn1 As Balthazar.CalendarColumn
    Friend WithEvents CalendarColumn2 As Balthazar.CalendarColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tpBudget As System.Windows.Forms.TabPage
    Friend WithEvents grpTotal As System.Windows.Forms.GroupBox
    Friend WithEvents lblPercent As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblProfit As System.Windows.Forms.Label
    Friend WithEvents lblActualCost As System.Windows.Forms.Label
    Friend WithEvents lblCTC As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grpLogistics As System.Windows.Forms.GroupBox
    Friend WithEvents grdLogistics As System.Windows.Forms.DataGridView
    Friend WithEvents colLogName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLogDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLogCTC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLogActualPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLogProfit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdRemoveLog As System.Windows.Forms.Button
    Friend WithEvents cmdAddLog As System.Windows.Forms.Button
    Friend WithEvents grpMaterial As System.Windows.Forms.GroupBox
    Friend WithEvents grdMaterial As System.Windows.Forms.DataGridView
    Friend WithEvents colMaterialName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMaterialDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMaterialCTC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMaterialActualPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMaterialProfit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdRemoveMaterial As System.Windows.Forms.Button
    Friend WithEvents cmdAddMaterial As System.Windows.Forms.Button
    Friend WithEvents grpStaff As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemoveStaff As System.Windows.Forms.Button
    Friend WithEvents cmdAddStaff As System.Windows.Forms.Button
    Friend WithEvents grdStaff As System.Windows.Forms.DataGridView
    Friend WithEvents grpPlanning As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemovePlanning As System.Windows.Forms.Button
    Friend WithEvents cmdAddPlanning As System.Windows.Forms.Button
    Friend WithEvents grdPlanning As System.Windows.Forms.DataGridView
    Friend WithEvents colPlanningName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanningDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanningHours As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanningPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanningCTC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanningActualCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanningProfit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRoleName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCategory As Balthazar.ExtendedComboboxColumn
    Friend WithEvents colRolePerDiem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRoleMinAge As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRoleMaxAge As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRoleGender As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents colDriver As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents colStaffName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffHours As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffCTC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffACHour As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffActualPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffProfit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tpDates As System.Windows.Forms.TabPage
    Friend WithEvents grpDates As System.Windows.Forms.GroupBox
    Friend WithEvents grdImportantDates As System.Windows.Forms.DataGridView
    Friend WithEvents cmdRemoveImportantDate As System.Windows.Forms.Button
    Friend WithEvents cmdAddImportantDate As System.Windows.Forms.Button
    Friend WithEvents cmdExportNotes As System.Windows.Forms.Button
    Friend WithEvents colDate As Balthazar.CalendarColumn
    Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDateDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRemindMe As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
