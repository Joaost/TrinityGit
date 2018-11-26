<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBudget
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
        Me.colStaffCount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colStaffDays = New System.Windows.Forms.DataGridViewTextBoxColumn
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
        Me.grpTotal = New System.Windows.Forms.GroupBox
        Me.lblPercent = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblProfit = New System.Windows.Forms.Label
        Me.lblActualCost = New System.Windows.Forms.Label
        Me.lblCTC = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
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
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn24 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn25 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn26 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn28 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.pnlLayout.SuspendLayout()
        Me.grpLogistics.SuspendLayout()
        CType(Me.grdLogistics, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMaterial.SuspendLayout()
        CType(Me.grdMaterial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpStaff.SuspendLayout()
        CType(Me.grdStaff, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPlanning.SuspendLayout()
        CType(Me.grdPlanning, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTotal.SuspendLayout()
        Me.SuspendLayout()
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
        Me.pnlLayout.Location = New System.Drawing.Point(12, 12)
        Me.pnlLayout.Name = "pnlLayout"
        Me.pnlLayout.RowCount = 4
        Me.pnlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.pnlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.pnlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.pnlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.pnlLayout.Size = New System.Drawing.Size(815, 600)
        Me.pnlLayout.TabIndex = 0
        '
        'grpLogistics
        '
        Me.grpLogistics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpLogistics.Controls.Add(Me.grdLogistics)
        Me.grpLogistics.Controls.Add(Me.cmdRemoveLog)
        Me.grpLogistics.Controls.Add(Me.cmdAddLog)
        Me.grpLogistics.Location = New System.Drawing.Point(3, 453)
        Me.grpLogistics.Name = "grpLogistics"
        Me.grpLogistics.Size = New System.Drawing.Size(809, 144)
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
        Me.grdLogistics.Size = New System.Drawing.Size(767, 122)
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
        Me.cmdRemoveLog.Location = New System.Drawing.Point(779, 49)
        Me.cmdRemoveLog.Name = "cmdRemoveLog"
        Me.cmdRemoveLog.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveLog.TabIndex = 10
        Me.cmdRemoveLog.UseVisualStyleBackColor = True
        '
        'cmdAddLog
        '
        Me.cmdAddLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddLog.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddLog.Location = New System.Drawing.Point(779, 19)
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
        Me.grpMaterial.Location = New System.Drawing.Point(3, 303)
        Me.grpMaterial.Name = "grpMaterial"
        Me.grpMaterial.Size = New System.Drawing.Size(809, 144)
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
        Me.grdMaterial.Size = New System.Drawing.Size(767, 122)
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
        Me.cmdRemoveMaterial.Location = New System.Drawing.Point(779, 49)
        Me.cmdRemoveMaterial.Name = "cmdRemoveMaterial"
        Me.cmdRemoveMaterial.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveMaterial.TabIndex = 10
        Me.cmdRemoveMaterial.UseVisualStyleBackColor = True
        '
        'cmdAddMaterial
        '
        Me.cmdAddMaterial.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddMaterial.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddMaterial.Location = New System.Drawing.Point(779, 19)
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
        Me.grpStaff.Location = New System.Drawing.Point(3, 153)
        Me.grpStaff.Name = "grpStaff"
        Me.grpStaff.Size = New System.Drawing.Size(809, 144)
        Me.grpStaff.TabIndex = 2
        Me.grpStaff.TabStop = False
        Me.grpStaff.Text = "Staff"
        '
        'cmdRemoveStaff
        '
        Me.cmdRemoveStaff.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveStaff.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveStaff.Location = New System.Drawing.Point(779, 49)
        Me.cmdRemoveStaff.Name = "cmdRemoveStaff"
        Me.cmdRemoveStaff.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveStaff.TabIndex = 10
        Me.cmdRemoveStaff.UseVisualStyleBackColor = True
        '
        'cmdAddStaff
        '
        Me.cmdAddStaff.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddStaff.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddStaff.Location = New System.Drawing.Point(779, 19)
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
        Me.grdStaff.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colStaffName, Me.colStaffDescription, Me.colStaffCount, Me.colStaffDays, Me.colStaffHours, Me.colStaffPrice, Me.colStaffCTC, Me.colStaffACHour, Me.colStaffActualPrice, Me.colStaffProfit})
        Me.grdStaff.Location = New System.Drawing.Point(6, 19)
        Me.grdStaff.Name = "grdStaff"
        Me.grdStaff.RowHeadersVisible = False
        Me.grdStaff.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdStaff.Size = New System.Drawing.Size(767, 119)
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
        'colStaffCount
        '
        Me.colStaffCount.HeaderText = "Quantity"
        Me.colStaffCount.Name = "colStaffCount"
        Me.colStaffCount.Width = 50
        '
        'colStaffDays
        '
        Me.colStaffDays.HeaderText = "Days"
        Me.colStaffDays.Name = "colStaffDays"
        Me.colStaffDays.Width = 50
        '
        'colStaffHours
        '
        Me.colStaffHours.HeaderText = "Hrs/day"
        Me.colStaffHours.Name = "colStaffHours"
        Me.colStaffHours.Width = 60
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
        Me.grpPlanning.Size = New System.Drawing.Size(809, 144)
        Me.grpPlanning.TabIndex = 1
        Me.grpPlanning.TabStop = False
        Me.grpPlanning.Text = "Planning"
        '
        'cmdRemovePlanning
        '
        Me.cmdRemovePlanning.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemovePlanning.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemovePlanning.Location = New System.Drawing.Point(779, 49)
        Me.cmdRemovePlanning.Name = "cmdRemovePlanning"
        Me.cmdRemovePlanning.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemovePlanning.TabIndex = 10
        Me.cmdRemovePlanning.UseVisualStyleBackColor = True
        '
        'cmdAddPlanning
        '
        Me.cmdAddPlanning.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddPlanning.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddPlanning.Location = New System.Drawing.Point(779, 19)
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
        Me.grdPlanning.Size = New System.Drawing.Size(767, 119)
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
        Me.grpTotal.Controls.Add(Me.Label1)
        Me.grpTotal.Location = New System.Drawing.Point(602, 618)
        Me.grpTotal.Name = "grpTotal"
        Me.grpTotal.Size = New System.Drawing.Size(225, 78)
        Me.grpTotal.TabIndex = 1
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "CTC"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 220
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "CTC"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 60
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Actual Price"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Profit"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 70
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.Width = 220
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "CTC"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Width = 60
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "Actual Price"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "Profit"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Width = 70
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn11.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.Width = 120
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.HeaderText = "Quantity"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.Width = 50
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.HeaderText = "Days"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.Width = 50
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.HeaderText = "Hrs/day"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.Width = 60
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.HeaderText = "CTC/Hour"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.Width = 60
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.HeaderText = "CTC"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.Width = 60
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.HeaderText = "AC/Hour"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.Width = 60
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.HeaderText = "Actual Cost"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.Width = 90
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.HeaderText = "Profit"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.Width = 70
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn21.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        Me.DataGridViewTextBoxColumn22.Width = 220
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.HeaderText = "Hours"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        Me.DataGridViewTextBoxColumn23.Width = 60
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.HeaderText = "CTC/Hour"
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        Me.DataGridViewTextBoxColumn24.Width = 60
        '
        'DataGridViewTextBoxColumn25
        '
        Me.DataGridViewTextBoxColumn25.HeaderText = "CTC"
        Me.DataGridViewTextBoxColumn25.Name = "DataGridViewTextBoxColumn25"
        Me.DataGridViewTextBoxColumn25.Width = 60
        '
        'DataGridViewTextBoxColumn26
        '
        Me.DataGridViewTextBoxColumn26.HeaderText = "AC/Hour"
        Me.DataGridViewTextBoxColumn26.Name = "DataGridViewTextBoxColumn26"
        Me.DataGridViewTextBoxColumn26.Width = 60
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.HeaderText = "Actual Price"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.Width = 70
        '
        'DataGridViewTextBoxColumn28
        '
        Me.DataGridViewTextBoxColumn28.HeaderText = "Profit"
        Me.DataGridViewTextBoxColumn28.Name = "DataGridViewTextBoxColumn28"
        Me.DataGridViewTextBoxColumn28.Width = 70
        '
        'frmBudget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(839, 708)
        Me.Controls.Add(Me.grpTotal)
        Me.Controls.Add(Me.pnlLayout)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmBudget"
        Me.Text = "Budget"
        Me.pnlLayout.ResumeLayout(False)
        Me.grpLogistics.ResumeLayout(False)
        CType(Me.grdLogistics, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMaterial.ResumeLayout(False)
        CType(Me.grdMaterial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpStaff.ResumeLayout(False)
        CType(Me.grdStaff, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPlanning.ResumeLayout(False)
        CType(Me.grdPlanning, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTotal.ResumeLayout(False)
        Me.grpTotal.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grpPlanning As System.Windows.Forms.GroupBox
    Friend WithEvents grdPlanning As System.Windows.Forms.DataGridView
    Friend WithEvents cmdRemovePlanning As System.Windows.Forms.Button
    Friend WithEvents cmdAddPlanning As System.Windows.Forms.Button
    Friend WithEvents grpStaff As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemoveStaff As System.Windows.Forms.Button
    Friend WithEvents cmdAddStaff As System.Windows.Forms.Button
    Friend WithEvents grdStaff As System.Windows.Forms.DataGridView
    Friend WithEvents grpMaterial As System.Windows.Forms.GroupBox
    Friend WithEvents grdMaterial As System.Windows.Forms.DataGridView
    Friend WithEvents cmdRemoveMaterial As System.Windows.Forms.Button
    Friend WithEvents cmdAddMaterial As System.Windows.Forms.Button
    Friend WithEvents grpLogistics As System.Windows.Forms.GroupBox
    Friend WithEvents grdLogistics As System.Windows.Forms.DataGridView
    Friend WithEvents colLogName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLogDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLogCTC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLogActualPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLogProfit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdRemoveLog As System.Windows.Forms.Button
    Friend WithEvents cmdAddLog As System.Windows.Forms.Button
    Friend WithEvents colMaterialName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMaterialDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMaterialCTC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMaterialActualPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMaterialProfit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpTotal As System.Windows.Forms.GroupBox
    Friend WithEvents lblProfit As System.Windows.Forms.Label
    Friend WithEvents lblActualCost As System.Windows.Forms.Label
    Friend WithEvents lblCTC As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents colStaffName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffCount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffDays As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffHours As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffCTC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffACHour As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffActualPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffProfit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanningName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanningDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanningHours As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanningPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanningCTC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanningActualCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanningProfit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
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
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblPercent As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
