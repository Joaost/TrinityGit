<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPricelist
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPricelist))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbTarget = New clTrinity.StyleableCombobox()
        Me.lblMaxRatings = New System.Windows.Forms.Label()
        Me.txtMaxRatings = New System.Windows.Forms.TextBox()
        Me.grpCPPorCPT = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbYear = New System.Windows.Forms.ComboBox()
        Me.rdbCPP = New System.Windows.Forms.RadioButton()
        Me.rdbCPT = New System.Windows.Forms.RadioButton()
        Me.cmdCopy = New System.Windows.Forms.Button()
        Me.chkCalcCPP = New System.Windows.Forms.CheckBox()
        Me.chkStandard = New System.Windows.Forms.CheckBox()
        Me.cmdDeleteTarget = New System.Windows.Forms.Button()
        Me.cmdAddTarget = New System.Windows.Forms.Button()
        Me.txtAdedgeTarget = New System.Windows.Forms.TextBox()
        Me.lblTarget = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbc = New System.Windows.Forms.TabControl()
        Me.tpPricelist = New System.Windows.Forms.TabPage()
        Me.cmdSetPriceType = New System.Windows.Forms.Button()
        Me.grdPricelist = New System.Windows.Forms.DataGridView()
        Me.colFrom = New clTrinity.CalendarColumn()
        Me.colTo = New clTrinity.CalendarColumn()
        Me.colNatUni = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUni = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCPP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mnuRightClick = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuItemCopyColumn = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdCopyTarget = New System.Windows.Forms.Button()
        Me.cmdRemove = New System.Windows.Forms.Button()
        Me.cmdCalculate = New System.Windows.Forms.Button()
        Me.cmdWizard = New System.Windows.Forms.Button()
        Me.cmbTarget1 = New System.Windows.Forms.ComboBox()
        Me.tpIndexes = New System.Windows.Forms.TabPage()
        Me.cmdIndexWizard = New System.Windows.Forms.Button()
        Me.cmdCopyIndex = New System.Windows.Forms.Button()
        Me.cmdEditEnhancement = New System.Windows.Forms.Button()
        Me.cmdEnhancements = New System.Windows.Forms.Button()
        Me.grdIndexes = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.CalendarColumn3 = New clTrinity.CalendarColumn()
        Me.CalendarColumn4 = New clTrinity.CalendarColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdAddIndex = New System.Windows.Forms.Button()
        Me.cmdDelIndex = New System.Windows.Forms.Button()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.mnuWizard = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CreateWeeklyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuWeekOnCPT = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuWeekOnCPP = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMonth = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMonthOnCPT = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMonthOnCPP = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuCopyOneYear = New System.Windows.Forms.ToolStripMenuItem()
        Me.SameDateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SameDayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyForAllTargetsToNextYearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllTargetsSameDateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllTargetsSameDayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdUpdateChosenPricelist = New System.Windows.Forms.Button()
        Me.cmdCompress = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdUpdateAllUniverses = New System.Windows.Forms.Button()
        Me.cmdSaveAs = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CalendarColumn1 = New clTrinity.CalendarColumn()
        Me.CalendarColumn2 = New clTrinity.CalendarColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbBookingType = New clTrinity.StyleableCombobox()
        Me.cmbChannel = New clTrinity.StyleableCombobox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbPricelist = New System.Windows.Forms.ComboBox()
        Me.cmbCopyToNextYear = New System.Windows.Forms.Button()
        Me.cmdFixNorway = New System.Windows.Forms.Button()
        Me.cmdRefreshPricelist = New System.Windows.Forms.Button()
        Me.cmdImport = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1.SuspendLayout()
        Me.grpCPPorCPT.SuspendLayout()
        Me.tbc.SuspendLayout()
        Me.tpPricelist.SuspendLayout()
        CType(Me.grdPricelist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuRightClick.SuspendLayout()
        Me.tpIndexes.SuspendLayout()
        CType(Me.grdIndexes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuWizard.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Channel"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cmbTarget)
        Me.GroupBox1.Controls.Add(Me.lblMaxRatings)
        Me.GroupBox1.Controls.Add(Me.txtMaxRatings)
        Me.GroupBox1.Controls.Add(Me.grpCPPorCPT)
        Me.GroupBox1.Controls.Add(Me.cmdCopy)
        Me.GroupBox1.Controls.Add(Me.chkCalcCPP)
        Me.GroupBox1.Controls.Add(Me.chkStandard)
        Me.GroupBox1.Controls.Add(Me.cmdDeleteTarget)
        Me.GroupBox1.Controls.Add(Me.cmdAddTarget)
        Me.GroupBox1.Controls.Add(Me.txtAdedgeTarget)
        Me.GroupBox1.Controls.Add(Me.lblTarget)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.tbc)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 114)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(752, 301)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Pricelist"
        '
        'cmbTarget
        '
        Me.cmbTarget.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cmbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTarget.FormattingEnabled = True
        Me.cmbTarget.Location = New System.Drawing.Point(6, 30)
        Me.cmbTarget.Name = "cmbTarget"
        Me.cmbTarget.Size = New System.Drawing.Size(112, 23)
        Me.cmbTarget.TabIndex = 35
        '
        'lblMaxRatings
        '
        Me.lblMaxRatings.AutoSize = True
        Me.lblMaxRatings.Location = New System.Drawing.Point(344, 66)
        Me.lblMaxRatings.Name = "lblMaxRatings"
        Me.lblMaxRatings.Size = New System.Drawing.Size(88, 13)
        Me.lblMaxRatings.TabIndex = 35
        Me.lblMaxRatings.Text = "Max TRP / Week"
        '
        'txtMaxRatings
        '
        Me.txtMaxRatings.Location = New System.Drawing.Point(434, 63)
        Me.txtMaxRatings.Name = "txtMaxRatings"
        Me.txtMaxRatings.Size = New System.Drawing.Size(43, 22)
        Me.txtMaxRatings.TabIndex = 34
        '
        'grpCPPorCPT
        '
        Me.grpCPPorCPT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpCPPorCPT.Controls.Add(Me.Label3)
        Me.grpCPPorCPT.Controls.Add(Me.cmbYear)
        Me.grpCPPorCPT.Controls.Add(Me.rdbCPP)
        Me.grpCPPorCPT.Controls.Add(Me.rdbCPT)
        Me.grpCPPorCPT.Location = New System.Drawing.Point(563, 15)
        Me.grpCPPorCPT.Name = "grpCPPorCPT"
        Me.grpCPPorCPT.Size = New System.Drawing.Size(183, 67)
        Me.grpCPPorCPT.TabIndex = 31
        Me.grpCPPorCPT.TabStop = False
        Me.grpCPPorCPT.Text = "Display options:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Year"
        '
        'cmbYear
        '
        Me.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbYear.FormattingEnabled = True
        Me.cmbYear.Location = New System.Drawing.Point(6, 31)
        Me.cmbYear.Name = "cmbYear"
        Me.cmbYear.Size = New System.Drawing.Size(121, 21)
        Me.cmbYear.Sorted = True
        Me.cmbYear.TabIndex = 31
        '
        'rdbCPP
        '
        Me.rdbCPP.AutoSize = True
        Me.rdbCPP.Checked = True
        Me.rdbCPP.Location = New System.Drawing.Point(133, 22)
        Me.rdbCPP.Name = "rdbCPP"
        Me.rdbCPP.Size = New System.Drawing.Size(44, 17)
        Me.rdbCPP.TabIndex = 30
        Me.rdbCPP.TabStop = True
        Me.rdbCPP.Text = "CPP"
        Me.rdbCPP.UseVisualStyleBackColor = True
        '
        'rdbCPT
        '
        Me.rdbCPT.AutoSize = True
        Me.rdbCPT.Location = New System.Drawing.Point(133, 41)
        Me.rdbCPT.Name = "rdbCPT"
        Me.rdbCPT.Size = New System.Drawing.Size(43, 17)
        Me.rdbCPT.TabIndex = 29
        Me.rdbCPT.Text = "CPT"
        Me.rdbCPT.UseVisualStyleBackColor = True
        '
        'cmdCopy
        '
        Me.cmdCopy.FlatAppearance.BorderSize = 0
        Me.cmdCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCopy.Image = Global.clTrinity.My.Resources.Resources.copy_3_16x16
        Me.cmdCopy.Location = New System.Drawing.Point(180, 30)
        Me.cmdCopy.Name = "cmdCopy"
        Me.cmdCopy.Size = New System.Drawing.Size(22, 20)
        Me.cmdCopy.TabIndex = 26
        Me.ToolTip.SetToolTip(Me.cmdCopy, "Copy price list from another channel")
        Me.cmdCopy.UseVisualStyleBackColor = True
        '
        'chkCalcCPP
        '
        Me.chkCalcCPP.AutoSize = True
        Me.chkCalcCPP.Location = New System.Drawing.Point(345, 19)
        Me.chkCalcCPP.Name = "chkCalcCPP"
        Me.chkCalcCPP.Size = New System.Drawing.Size(169, 17)
        Me.chkCalcCPP.TabIndex = 23
        Me.chkCalcCPP.Text = "Calculate CPP from dayparts"
        Me.chkCalcCPP.UseVisualStyleBackColor = True
        '
        'chkStandard
        '
        Me.chkStandard.AutoSize = True
        Me.chkStandard.Enabled = False
        Me.chkStandard.Location = New System.Drawing.Point(345, 42)
        Me.chkStandard.Name = "chkStandard"
        Me.chkStandard.Size = New System.Drawing.Size(107, 17)
        Me.chkStandard.TabIndex = 22
        Me.chkStandard.Text = "Standard target"
        Me.chkStandard.UseVisualStyleBackColor = True
        '
        'cmdDeleteTarget
        '
        Me.cmdDeleteTarget.FlatAppearance.BorderSize = 0
        Me.cmdDeleteTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDeleteTarget.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdDeleteTarget.Location = New System.Drawing.Point(152, 30)
        Me.cmdDeleteTarget.Name = "cmdDeleteTarget"
        Me.cmdDeleteTarget.Size = New System.Drawing.Size(22, 20)
        Me.cmdDeleteTarget.TabIndex = 21
        Me.ToolTip.SetToolTip(Me.cmdDeleteTarget, "Delete Target")
        Me.cmdDeleteTarget.UseVisualStyleBackColor = True
        '
        'cmdAddTarget
        '
        Me.cmdAddTarget.FlatAppearance.BorderSize = 0
        Me.cmdAddTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddTarget.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.cmdAddTarget.Location = New System.Drawing.Point(124, 30)
        Me.cmdAddTarget.Name = "cmdAddTarget"
        Me.cmdAddTarget.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddTarget.TabIndex = 20
        Me.ToolTip.SetToolTip(Me.cmdAddTarget, "Add Target")
        Me.cmdAddTarget.UseVisualStyleBackColor = True
        '
        'txtAdedgeTarget
        '
        Me.txtAdedgeTarget.Location = New System.Drawing.Point(208, 31)
        Me.txtAdedgeTarget.Name = "txtAdedgeTarget"
        Me.txtAdedgeTarget.Size = New System.Drawing.Size(81, 22)
        Me.txtAdedgeTarget.TabIndex = 7
        '
        'lblTarget
        '
        Me.lblTarget.AutoSize = True
        Me.lblTarget.Location = New System.Drawing.Point(210, 15)
        Me.lblTarget.Name = "lblTarget"
        Me.lblTarget.Size = New System.Drawing.Size(81, 13)
        Me.lblTarget.TabIndex = 6
        Me.lblTarget.Text = "Adedge Target"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Target"
        '
        'tbc
        '
        Me.tbc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbc.Controls.Add(Me.tpPricelist)
        Me.tbc.Controls.Add(Me.tpIndexes)
        Me.tbc.Location = New System.Drawing.Point(6, 66)
        Me.tbc.Name = "tbc"
        Me.tbc.SelectedIndex = 0
        Me.tbc.Size = New System.Drawing.Size(740, 230)
        Me.tbc.TabIndex = 33
        '
        'tpPricelist
        '
        Me.tpPricelist.Controls.Add(Me.cmdSetPriceType)
        Me.tpPricelist.Controls.Add(Me.grdPricelist)
        Me.tpPricelist.Controls.Add(Me.cmdAdd)
        Me.tpPricelist.Controls.Add(Me.cmdCopyTarget)
        Me.tpPricelist.Controls.Add(Me.cmdRemove)
        Me.tpPricelist.Controls.Add(Me.cmdCalculate)
        Me.tpPricelist.Controls.Add(Me.cmdWizard)
        Me.tpPricelist.Controls.Add(Me.cmbTarget1)
        Me.tpPricelist.Location = New System.Drawing.Point(4, 22)
        Me.tpPricelist.Name = "tpPricelist"
        Me.tpPricelist.Padding = New System.Windows.Forms.Padding(3)
        Me.tpPricelist.Size = New System.Drawing.Size(732, 204)
        Me.tpPricelist.TabIndex = 0
        Me.tpPricelist.Text = "Pricelist"
        Me.tpPricelist.UseVisualStyleBackColor = True
        '
        'cmdSetPriceType
        '
        Me.cmdSetPriceType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSetPriceType.FlatAppearance.BorderSize = 0
        Me.cmdSetPriceType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSetPriceType.Image = Global.clTrinity.My.Resources.Resources.db_2_16x16
        Me.cmdSetPriceType.Location = New System.Drawing.Point(704, 136)
        Me.cmdSetPriceType.Name = "cmdSetPriceType"
        Me.cmdSetPriceType.Size = New System.Drawing.Size(22, 20)
        Me.cmdSetPriceType.TabIndex = 32
        Me.ToolTip.SetToolTip(Me.cmdSetPriceType, "Save price as:")
        Me.cmdSetPriceType.UseVisualStyleBackColor = True
        '
        'grdPricelist
        '
        Me.grdPricelist.AllowUserToAddRows = False
        Me.grdPricelist.AllowUserToDeleteRows = False
        Me.grdPricelist.AllowUserToResizeColumns = False
        Me.grdPricelist.AllowUserToResizeRows = False
        Me.grdPricelist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdPricelist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grdPricelist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPricelist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colFrom, Me.colTo, Me.colNatUni, Me.colUni, Me.colCPP})
        Me.grdPricelist.ContextMenuStrip = Me.mnuRightClick
        Me.grdPricelist.Location = New System.Drawing.Point(0, 0)
        Me.grdPricelist.Name = "grdPricelist"
        Me.grdPricelist.RowHeadersVisible = False
        Me.grdPricelist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPricelist.Size = New System.Drawing.Size(698, 199)
        Me.grdPricelist.TabIndex = 5
        Me.grdPricelist.VirtualMode = True
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
        'colNatUni
        '
        Me.colNatUni.HeaderText = "NatUni"
        Me.colNatUni.Name = "colNatUni"
        '
        'colUni
        '
        Me.colUni.HeaderText = "Uni"
        Me.colUni.Name = "colUni"
        '
        'colCPP
        '
        Me.colCPP.HeaderText = "CPP"
        Me.colCPP.Name = "colCPP"
        '
        'mnuRightClick
        '
        Me.mnuRightClick.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuItemCopyColumn})
        Me.mnuRightClick.Name = "mnuRightClick"
        Me.mnuRightClick.Size = New System.Drawing.Size(194, 26)
        '
        'mnuItemCopyColumn
        '
        Me.mnuItemCopyColumn.Name = "mnuItemCopyColumn"
        Me.mnuItemCopyColumn.Size = New System.Drawing.Size(193, 22)
        Me.mnuItemCopyColumn.Text = "Copy to entire column"
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.FlatAppearance.BorderSize = 0
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.cmdAdd.Location = New System.Drawing.Point(704, 6)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(22, 20)
        Me.cmdAdd.TabIndex = 17
        Me.ToolTip.SetToolTip(Me.cmdAdd, "Add row  to price list ")
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'cmdCopyTarget
        '
        Me.cmdCopyTarget.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCopyTarget.FlatAppearance.BorderSize = 0
        Me.cmdCopyTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCopyTarget.Image = Global.clTrinity.My.Resources.Resources.copy_3_16x16
        Me.cmdCopyTarget.Location = New System.Drawing.Point(704, 84)
        Me.cmdCopyTarget.Name = "cmdCopyTarget"
        Me.cmdCopyTarget.Size = New System.Drawing.Size(22, 20)
        Me.cmdCopyTarget.TabIndex = 28
        Me.ToolTip.SetToolTip(Me.cmdCopyTarget, "Copy price list from another target")
        Me.cmdCopyTarget.UseVisualStyleBackColor = True
        '
        'cmdRemove
        '
        Me.cmdRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemove.FlatAppearance.BorderSize = 0
        Me.cmdRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRemove.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdRemove.Location = New System.Drawing.Point(704, 32)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(22, 20)
        Me.cmdRemove.TabIndex = 18
        Me.ToolTip.SetToolTip(Me.cmdRemove, "Delete price list row")
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'cmdCalculate
        '
        Me.cmdCalculate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCalculate.FlatAppearance.BorderSize = 0
        Me.cmdCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCalculate.Image = Global.clTrinity.My.Resources.Resources.calculator_2_small
        Me.cmdCalculate.Location = New System.Drawing.Point(704, 110)
        Me.cmdCalculate.Name = "cmdCalculate"
        Me.cmdCalculate.Size = New System.Drawing.Size(22, 20)
        Me.cmdCalculate.TabIndex = 27
        Me.ToolTip.SetToolTip(Me.cmdCalculate, "Get universe sizes from Advantege")
        Me.cmdCalculate.UseVisualStyleBackColor = True
        '
        'cmdWizard
        '
        Me.cmdWizard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdWizard.FlatAppearance.BorderSize = 0
        Me.cmdWizard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdWizard.Image = Global.clTrinity.My.Resources.Resources.flash_2
        Me.cmdWizard.Location = New System.Drawing.Point(704, 58)
        Me.cmdWizard.Name = "cmdWizard"
        Me.cmdWizard.Size = New System.Drawing.Size(22, 20)
        Me.cmdWizard.TabIndex = 19
        Me.ToolTip.SetToolTip(Me.cmdWizard, "Price list wizard")
        Me.cmdWizard.UseVisualStyleBackColor = True
        '
        'cmbTarget1
        '
        Me.cmbTarget1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTarget1.FormattingEnabled = True
        Me.cmbTarget1.Location = New System.Drawing.Point(96, 58)
        Me.cmbTarget1.Name = "cmbTarget1"
        Me.cmbTarget1.Size = New System.Drawing.Size(112, 21)
        Me.cmbTarget1.TabIndex = 4
        '
        'tpIndexes
        '
        Me.tpIndexes.Controls.Add(Me.cmdIndexWizard)
        Me.tpIndexes.Controls.Add(Me.cmdCopyIndex)
        Me.tpIndexes.Controls.Add(Me.cmdEditEnhancement)
        Me.tpIndexes.Controls.Add(Me.cmdEnhancements)
        Me.tpIndexes.Controls.Add(Me.grdIndexes)
        Me.tpIndexes.Controls.Add(Me.cmdAddIndex)
        Me.tpIndexes.Controls.Add(Me.cmdDelIndex)
        Me.tpIndexes.Location = New System.Drawing.Point(4, 22)
        Me.tpIndexes.Name = "tpIndexes"
        Me.tpIndexes.Padding = New System.Windows.Forms.Padding(3)
        Me.tpIndexes.Size = New System.Drawing.Size(732, 204)
        Me.tpIndexes.TabIndex = 1
        Me.tpIndexes.Text = "Indexes"
        Me.tpIndexes.UseVisualStyleBackColor = True
        '
        'cmdIndexWizard
        '
        Me.cmdIndexWizard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdIndexWizard.Image = CType(resources.GetObject("cmdIndexWizard.Image"), System.Drawing.Image)
        Me.cmdIndexWizard.Location = New System.Drawing.Point(704, 136)
        Me.cmdIndexWizard.Name = "cmdIndexWizard"
        Me.cmdIndexWizard.Size = New System.Drawing.Size(22, 20)
        Me.cmdIndexWizard.TabIndex = 33
        Me.ToolTip.SetToolTip(Me.cmdIndexWizard, "Price list wizard")
        Me.cmdIndexWizard.UseVisualStyleBackColor = True
        '
        'cmdCopyIndex
        '
        Me.cmdCopyIndex.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCopyIndex.Image = CType(resources.GetObject("cmdCopyIndex.Image"), System.Drawing.Image)
        Me.cmdCopyIndex.Location = New System.Drawing.Point(704, 110)
        Me.cmdCopyIndex.Name = "cmdCopyIndex"
        Me.cmdCopyIndex.Size = New System.Drawing.Size(22, 20)
        Me.cmdCopyIndex.TabIndex = 32
        Me.ToolTip.SetToolTip(Me.cmdCopyIndex, "Copy indexes from another channel")
        Me.cmdCopyIndex.UseVisualStyleBackColor = True
        '
        'cmdEditEnhancement
        '
        Me.cmdEditEnhancement.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdEditEnhancement.Image = CType(resources.GetObject("cmdEditEnhancement.Image"), System.Drawing.Image)
        Me.cmdEditEnhancement.Location = New System.Drawing.Point(704, 84)
        Me.cmdEditEnhancement.Name = "cmdEditEnhancement"
        Me.cmdEditEnhancement.Size = New System.Drawing.Size(22, 20)
        Me.cmdEditEnhancement.TabIndex = 31
        Me.ToolTip.SetToolTip(Me.cmdEditEnhancement, "Edit Enhancement")
        Me.cmdEditEnhancement.UseVisualStyleBackColor = True
        Me.cmdEditEnhancement.Visible = False
        '
        'cmdEnhancements
        '
        Me.cmdEnhancements.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdEnhancements.Image = CType(resources.GetObject("cmdEnhancements.Image"), System.Drawing.Image)
        Me.cmdEnhancements.Location = New System.Drawing.Point(704, 58)
        Me.cmdEnhancements.Name = "cmdEnhancements"
        Me.cmdEnhancements.Size = New System.Drawing.Size(22, 20)
        Me.cmdEnhancements.TabIndex = 30
        Me.ToolTip.SetToolTip(Me.cmdEnhancements, "Add Enhancement")
        Me.cmdEnhancements.UseVisualStyleBackColor = True
        Me.cmdEnhancements.Visible = False
        '
        'grdIndexes
        '
        Me.grdIndexes.AllowUserToAddRows = False
        Me.grdIndexes.AllowUserToDeleteRows = False
        Me.grdIndexes.AllowUserToResizeColumns = False
        Me.grdIndexes.AllowUserToResizeRows = False
        Me.grdIndexes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdIndexes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grdIndexes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdIndexes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.CalendarColumn3, Me.CalendarColumn4, Me.DataGridViewTextBoxColumn3})
        Me.grdIndexes.ContextMenuStrip = Me.mnuRightClick
        Me.grdIndexes.Location = New System.Drawing.Point(0, 0)
        Me.grdIndexes.Name = "grdIndexes"
        Me.grdIndexes.RowHeadersVisible = False
        Me.grdIndexes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdIndexes.Size = New System.Drawing.Size(698, 211)
        Me.grdIndexes.TabIndex = 19
        Me.grdIndexes.VirtualMode = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "On"
        Me.DataGridViewTextBoxColumn2.Items.AddRange(New Object() {"Net CPP", "Gross CPP", "TRP"})
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'CalendarColumn3
        '
        Me.CalendarColumn3.HeaderText = "From"
        Me.CalendarColumn3.Name = "CalendarColumn3"
        '
        'CalendarColumn4
        '
        Me.CalendarColumn4.HeaderText = "To"
        Me.CalendarColumn4.Name = "CalendarColumn4"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Index"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'cmdAddIndex
        '
        Me.cmdAddIndex.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddIndex.Image = CType(resources.GetObject("cmdAddIndex.Image"), System.Drawing.Image)
        Me.cmdAddIndex.Location = New System.Drawing.Point(704, 6)
        Me.cmdAddIndex.Name = "cmdAddIndex"
        Me.cmdAddIndex.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddIndex.TabIndex = 20
        Me.ToolTip.SetToolTip(Me.cmdAddIndex, "Add row  to price list ")
        Me.cmdAddIndex.UseVisualStyleBackColor = True
        '
        'cmdDelIndex
        '
        Me.cmdDelIndex.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDelIndex.Image = CType(resources.GetObject("cmdDelIndex.Image"), System.Drawing.Image)
        Me.cmdDelIndex.Location = New System.Drawing.Point(704, 32)
        Me.cmdDelIndex.Name = "cmdDelIndex"
        Me.cmdDelIndex.Size = New System.Drawing.Size(22, 20)
        Me.cmdDelIndex.TabIndex = 21
        Me.ToolTip.SetToolTip(Me.cmdDelIndex, "Delete price list row")
        Me.cmdDelIndex.UseVisualStyleBackColor = True
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Location = New System.Drawing.Point(683, 421)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(75, 27)
        Me.cmdOk.TabIndex = 6
        Me.cmdOk.Text = "&Apply"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'mnuWizard
        '
        Me.mnuWizard.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateWeeklyToolStripMenuItem, Me.mnuMonth, Me.ToolStripSeparator1, Me.mnuCopyOneYear, Me.CopyForAllTargetsToNextYearToolStripMenuItem})
        Me.mnuWizard.Name = "mnuWizard"
        Me.mnuWizard.ShowImageMargin = False
        Me.mnuWizard.Size = New System.Drawing.Size(214, 98)
        '
        'CreateWeeklyToolStripMenuItem
        '
        Me.CreateWeeklyToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuWeekOnCPT, Me.mnuWeekOnCPP})
        Me.CreateWeeklyToolStripMenuItem.Name = "CreateWeeklyToolStripMenuItem"
        Me.CreateWeeklyToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.CreateWeeklyToolStripMenuItem.Text = "Create Weekly"
        '
        'mnuWeekOnCPT
        '
        Me.mnuWeekOnCPT.Name = "mnuWeekOnCPT"
        Me.mnuWeekOnCPT.Size = New System.Drawing.Size(115, 22)
        Me.mnuWeekOnCPT.Text = "On CPT"
        '
        'mnuWeekOnCPP
        '
        Me.mnuWeekOnCPP.Name = "mnuWeekOnCPP"
        Me.mnuWeekOnCPP.Size = New System.Drawing.Size(115, 22)
        Me.mnuWeekOnCPP.Text = "On CPP"
        '
        'mnuMonth
        '
        Me.mnuMonth.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMonthOnCPT, Me.mnuMonthOnCPP})
        Me.mnuMonth.Name = "mnuMonth"
        Me.mnuMonth.Size = New System.Drawing.Size(213, 22)
        Me.mnuMonth.Text = "Create Monthly"
        '
        'mnuMonthOnCPT
        '
        Me.mnuMonthOnCPT.Name = "mnuMonthOnCPT"
        Me.mnuMonthOnCPT.Size = New System.Drawing.Size(115, 22)
        Me.mnuMonthOnCPT.Text = "On CPT"
        '
        'mnuMonthOnCPP
        '
        Me.mnuMonthOnCPP.Name = "mnuMonthOnCPP"
        Me.mnuMonthOnCPP.Size = New System.Drawing.Size(115, 22)
        Me.mnuMonthOnCPP.Text = "On CPP"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(210, 6)
        '
        'mnuCopyOneYear
        '
        Me.mnuCopyOneYear.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SameDateToolStripMenuItem, Me.SameDayToolStripMenuItem})
        Me.mnuCopyOneYear.Name = "mnuCopyOneYear"
        Me.mnuCopyOneYear.Size = New System.Drawing.Size(213, 22)
        Me.mnuCopyOneYear.Text = "Copy selected to next year"
        '
        'SameDateToolStripMenuItem
        '
        Me.SameDateToolStripMenuItem.Name = "SameDateToolStripMenuItem"
        Me.SameDateToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.SameDateToolStripMenuItem.Text = "Same date"
        '
        'SameDayToolStripMenuItem
        '
        Me.SameDayToolStripMenuItem.Name = "SameDayToolStripMenuItem"
        Me.SameDayToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.SameDayToolStripMenuItem.Text = "Same day"
        '
        'CopyForAllTargetsToNextYearToolStripMenuItem
        '
        Me.CopyForAllTargetsToNextYearToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AllTargetsSameDateToolStripMenuItem, Me.AllTargetsSameDayToolStripMenuItem})
        Me.CopyForAllTargetsToNextYearToolStripMenuItem.Name = "CopyForAllTargetsToNextYearToolStripMenuItem"
        Me.CopyForAllTargetsToNextYearToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.CopyForAllTargetsToNextYearToolStripMenuItem.Text = "Copy for all targets to next year"
        '
        'AllTargetsSameDateToolStripMenuItem
        '
        Me.AllTargetsSameDateToolStripMenuItem.Name = "AllTargetsSameDateToolStripMenuItem"
        Me.AllTargetsSameDateToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.AllTargetsSameDateToolStripMenuItem.Text = "Same date"
        '
        'AllTargetsSameDayToolStripMenuItem
        '
        Me.AllTargetsSameDayToolStripMenuItem.Name = "AllTargetsSameDayToolStripMenuItem"
        Me.AllTargetsSameDayToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.AllTargetsSameDayToolStripMenuItem.Text = "Same day"
        '
        'cmdUpdateChosenPricelist
        '
        Me.cmdUpdateChosenPricelist.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdUpdateChosenPricelist.FlatAppearance.BorderSize = 0
        Me.cmdUpdateChosenPricelist.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUpdateChosenPricelist.Image = CType(resources.GetObject("cmdUpdateChosenPricelist.Image"), System.Drawing.Image)
        Me.cmdUpdateChosenPricelist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdUpdateChosenPricelist.Location = New System.Drawing.Point(597, 11)
        Me.cmdUpdateChosenPricelist.Name = "cmdUpdateChosenPricelist"
        Me.cmdUpdateChosenPricelist.Size = New System.Drawing.Size(167, 36)
        Me.cmdUpdateChosenPricelist.TabIndex = 46
        Me.cmdUpdateChosenPricelist.Text = "Reload chosen pricelist"
        Me.cmdUpdateChosenPricelist.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdUpdateChosenPricelist, "Update prices from database")
        Me.cmdUpdateChosenPricelist.UseVisualStyleBackColor = True
        '
        'cmdCompress
        '
        Me.cmdCompress.FlatAppearance.BorderSize = 0
        Me.cmdCompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCompress.Image = Global.clTrinity.My.Resources.Resources.compress_2_24x32
        Me.cmdCompress.Location = New System.Drawing.Point(147, 10)
        Me.cmdCompress.Name = "cmdCompress"
        Me.cmdCompress.Size = New System.Drawing.Size(45, 37)
        Me.cmdCompress.TabIndex = 44
        Me.ToolTip.SetToolTip(Me.cmdCompress, "Compress pricelist")
        Me.cmdCompress.UseVisualStyleBackColor = True
        Me.cmdCompress.Visible = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdUpdate.FlatAppearance.BorderSize = 0
        Me.cmdUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUpdate.Image = CType(resources.GetObject("cmdUpdate.Image"), System.Drawing.Image)
        Me.cmdUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdUpdate.Location = New System.Drawing.Point(613, 66)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(151, 36)
        Me.cmdUpdate.TabIndex = 8
        Me.cmdUpdate.Text = "Reload all pricelists"
        Me.cmdUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdUpdate, "Update prices from database")
        Me.cmdUpdate.UseVisualStyleBackColor = True
        '
        'cmdUpdateAllUniverses
        '
        Me.cmdUpdateAllUniverses.FlatAppearance.BorderSize = 0
        Me.cmdUpdateAllUniverses.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUpdateAllUniverses.Image = Global.clTrinity.My.Resources.Resources.calculator_2_32x32
        Me.cmdUpdateAllUniverses.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdUpdateAllUniverses.Location = New System.Drawing.Point(265, 10)
        Me.cmdUpdateAllUniverses.Name = "cmdUpdateAllUniverses"
        Me.cmdUpdateAllUniverses.Size = New System.Drawing.Size(42, 37)
        Me.cmdUpdateAllUniverses.TabIndex = 42
        Me.cmdUpdateAllUniverses.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdUpdateAllUniverses, "Update universesize")
        Me.cmdUpdateAllUniverses.UseVisualStyleBackColor = True
        Me.cmdUpdateAllUniverses.Visible = False
        '
        'cmdSaveAs
        '
        Me.cmdSaveAs.FlatAppearance.BorderSize = 0
        Me.cmdSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSaveAs.Image = Global.clTrinity.My.Resources.Resources.save_2
        Me.cmdSaveAs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSaveAs.Location = New System.Drawing.Point(474, 10)
        Me.cmdSaveAs.Name = "cmdSaveAs"
        Me.cmdSaveAs.Size = New System.Drawing.Size(103, 37)
        Me.cmdSaveAs.TabIndex = 39
        Me.cmdSaveAs.Text = "Save to file"
        Me.cmdSaveAs.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdSaveAs, "Save pricelist")
        Me.cmdSaveAs.UseVisualStyleBackColor = True
        Me.cmdSaveAs.Visible = False
        '
        'cmdSave
        '
        Me.cmdSave.FlatAppearance.BorderSize = 0
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Image = Global.clTrinity.My.Resources.Resources.save_2_small
        Me.cmdSave.Location = New System.Drawing.Point(583, 74)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(24, 22)
        Me.cmdSave.TabIndex = 38
        Me.ToolTip.SetToolTip(Me.cmdSave, "Save data coupling between Bookingtype and Pricelist")
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 428)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Rows with"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label5.Location = New System.Drawing.Point(76, 428)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "blue"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(100, 428)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(373, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "text has calculated prices (change display option to get the saved price)"
        '
        'CalendarColumn1
        '
        Me.CalendarColumn1.HeaderText = "From"
        Me.CalendarColumn1.Name = "CalendarColumn1"
        Me.CalendarColumn1.Width = 132
        '
        'CalendarColumn2
        '
        Me.CalendarColumn2.HeaderText = "To"
        Me.CalendarColumn2.Name = "CalendarColumn2"
        Me.CalendarColumn2.Width = 131
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 132
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "On"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 131
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Index"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Width = 132
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(189, 60)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 13)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "Bookingtype"
        '
        'cmbBookingType
        '
        Me.cmbBookingType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cmbBookingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBookingType.FormattingEnabled = True
        Me.cmbBookingType.Location = New System.Drawing.Point(192, 75)
        Me.cmbBookingType.Name = "cmbBookingType"
        Me.cmbBookingType.Size = New System.Drawing.Size(158, 23)
        Me.cmbBookingType.TabIndex = 34
        '
        'cmbChannel
        '
        Me.cmbChannel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cmbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChannel.FormattingEnabled = True
        Me.cmbChannel.Location = New System.Drawing.Point(12, 75)
        Me.cmbChannel.Name = "cmbChannel"
        Me.cmbChannel.Size = New System.Drawing.Size(158, 23)
        Me.cmbChannel.TabIndex = 35
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(359, 59)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 13)
        Me.Label8.TabIndex = 36
        Me.Label8.Text = "Pricelist"
        '
        'cmbPricelist
        '
        Me.cmbPricelist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPricelist.FormattingEnabled = True
        Me.cmbPricelist.Location = New System.Drawing.Point(359, 74)
        Me.cmbPricelist.Name = "cmbPricelist"
        Me.cmbPricelist.Size = New System.Drawing.Size(188, 21)
        Me.cmbPricelist.Sorted = True
        Me.cmbPricelist.TabIndex = 37
        '
        'cmbCopyToNextYear
        '
        Me.cmbCopyToNextYear.Location = New System.Drawing.Point(64, 11)
        Me.cmbCopyToNextYear.Name = "cmbCopyToNextYear"
        Me.cmbCopyToNextYear.Size = New System.Drawing.Size(75, 38)
        Me.cmbCopyToNextYear.TabIndex = 45
        Me.cmbCopyToNextYear.Text = "Copy to next year"
        Me.cmbCopyToNextYear.UseVisualStyleBackColor = True
        Me.cmbCopyToNextYear.Visible = False
        '
        'cmdFixNorway
        '
        Me.cmdFixNorway.FlatAppearance.BorderSize = 0
        Me.cmdFixNorway.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFixNorway.Image = Global.clTrinity.My.Resources.Resources.flag_norway
        Me.cmdFixNorway.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdFixNorway.Location = New System.Drawing.Point(198, 10)
        Me.cmdFixNorway.Name = "cmdFixNorway"
        Me.cmdFixNorway.Size = New System.Drawing.Size(61, 37)
        Me.cmdFixNorway.TabIndex = 43
        Me.cmdFixNorway.Text = "Fix"
        Me.cmdFixNorway.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdFixNorway.UseVisualStyleBackColor = True
        Me.cmdFixNorway.Visible = False
        '
        'cmdRefreshPricelist
        '
        Me.cmdRefreshPricelist.FlatAppearance.BorderSize = 0
        Me.cmdRefreshPricelist.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRefreshPricelist.Image = Global.clTrinity.My.Resources.Resources.sync_2_small
        Me.cmdRefreshPricelist.Location = New System.Drawing.Point(553, 74)
        Me.cmdRefreshPricelist.Name = "cmdRefreshPricelist"
        Me.cmdRefreshPricelist.Size = New System.Drawing.Size(24, 22)
        Me.cmdRefreshPricelist.TabIndex = 41
        Me.ToolTip.SetToolTip(Me.cmdRefreshPricelist, "Update bookingtypes connected to channels")
        Me.cmdRefreshPricelist.UseVisualStyleBackColor = True
        '
        'cmdImport
        '
        Me.cmdImport.FlatAppearance.BorderSize = 0
        Me.cmdImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdImport.Image = Global.clTrinity.My.Resources.Resources.db_2_18x24
        Me.cmdImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdImport.Location = New System.Drawing.Point(313, 10)
        Me.cmdImport.Name = "cmdImport"
        Me.cmdImport.Size = New System.Drawing.Size(148, 37)
        Me.cmdImport.TabIndex = 40
        Me.cmdImport.Text = "Import excel pricelist"
        Me.cmdImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdImport.UseVisualStyleBackColor = True
        Me.cmdImport.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.bank_2_32x32
        Me.PictureBox1.Location = New System.Drawing.Point(12, 11)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = false
        '
        'frmPricelist
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(776, 458)
        Me.ControlBox = false
        Me.Controls.Add(Me.cmdUpdateChosenPricelist)
        Me.Controls.Add(Me.cmbCopyToNextYear)
        Me.Controls.Add(Me.cmdFixNorway)
        Me.Controls.Add(Me.cmdUpdateAllUniverses)
        Me.Controls.Add(Me.cmdCompress)
        Me.Controls.Add(Me.cmdRefreshPricelist)
        Me.Controls.Add(Me.cmdImport)
        Me.Controls.Add(Me.cmdSaveAs)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmbPricelist)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cmbChannel)
        Me.Controls.Add(Me.cmbBookingType)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmPricelist"
        Me.Text = "Pricelist"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.grpCPPorCPT.ResumeLayout(false)
        Me.grpCPPorCPT.PerformLayout
        Me.tbc.ResumeLayout(false)
        Me.tpPricelist.ResumeLayout(false)
        CType(Me.grdPricelist,System.ComponentModel.ISupportInitialize).EndInit
        Me.mnuRightClick.ResumeLayout(false)
        Me.tpIndexes.ResumeLayout(false)
        CType(Me.grdIndexes,System.ComponentModel.ISupportInitialize).EndInit
        Me.mnuWizard.ResumeLayout(false)
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grdPricelist As System.Windows.Forms.DataGridView
    Friend WithEvents cmbTarget1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAdedgeTarget As System.Windows.Forms.TextBox
    Friend WithEvents lblTarget As System.Windows.Forms.Label
    Friend WithEvents cmdWizard As System.Windows.Forms.Button
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdDeleteTarget As System.Windows.Forms.Button
    Friend WithEvents cmdAddTarget As System.Windows.Forms.Button
    Friend WithEvents CalendarColumn1 As clTrinity.CalendarColumn
    Friend WithEvents CalendarColumn2 As clTrinity.CalendarColumn
    Friend WithEvents chkCalcCPP As System.Windows.Forms.CheckBox
    Friend WithEvents chkStandard As System.Windows.Forms.CheckBox
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents mnuWizard As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CreateWeeklyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuWeekOnCPT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuWeekOnCPP As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMonth As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMonthOnCPT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMonthOnCPP As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdCopy As System.Windows.Forms.Button
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents cmdCalculate As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents cmdCopyTarget As System.Windows.Forms.Button
    Friend WithEvents rdbCPP As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCPT As System.Windows.Forms.RadioButton
    Friend WithEvents grpCPPorCPT As System.Windows.Forms.GroupBox
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuCopyOneYear As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SameDateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SameDayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbYear As System.Windows.Forms.ComboBox
    Friend WithEvents cmdSetPriceType As System.Windows.Forms.Button
    Friend WithEvents colFrom As clTrinity.CalendarColumn
    Friend WithEvents colTo As clTrinity.CalendarColumn
    Friend WithEvents colNatUni As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUni As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCPP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnuRightClick As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuItemCopyColumn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tbc As System.Windows.Forms.TabControl
    Friend WithEvents tpPricelist As System.Windows.Forms.TabPage
    Friend WithEvents tpIndexes As System.Windows.Forms.TabPage
    Friend WithEvents grdIndexes As System.Windows.Forms.DataGridView
    Friend WithEvents cmdAddIndex As System.Windows.Forms.Button
    Friend WithEvents cmdDelIndex As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents CalendarColumn3 As clTrinity.CalendarColumn
    Friend WithEvents CalendarColumn4 As clTrinity.CalendarColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdCopyIndex As System.Windows.Forms.Button
    Friend WithEvents cmdEditEnhancement As System.Windows.Forms.Button
    Friend WithEvents cmdEnhancements As System.Windows.Forms.Button
    Friend WithEvents cmdIndexWizard As System.Windows.Forms.Button
    Friend WithEvents CopyForAllTargetsToNextYearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AllTargetsSameDateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AllTargetsSameDayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtMaxRatings As System.Windows.Forms.TextBox
    Friend WithEvents lblMaxRatings As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbBookingType As clTrinity.StyleableCombobox
    Friend WithEvents cmbTarget As clTrinity.StyleableCombobox
    Friend WithEvents cmbChannel As clTrinity.StyleableCombobox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbPricelist As System.Windows.Forms.ComboBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdSaveAs As System.Windows.Forms.Button
    Friend WithEvents cmdImport As System.Windows.Forms.Button
    Friend WithEvents cmdRefreshPricelist As System.Windows.Forms.Button
    Friend WithEvents cmdUpdateAllUniverses As System.Windows.Forms.Button
    Friend WithEvents cmdFixNorway As System.Windows.Forms.Button
    Friend WithEvents cmdCompress As System.Windows.Forms.Button
    Friend WithEvents cmbCopyToNextYear As System.Windows.Forms.Button
    Friend WithEvents cmdUpdateChosenPricelist As System.Windows.Forms.Button
End Class
