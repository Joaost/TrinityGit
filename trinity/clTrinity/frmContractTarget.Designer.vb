<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmContractTarget
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmContractTarget))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdCalculate = New System.Windows.Forms.Button()
        Me.cmdSetPriceType = New System.Windows.Forms.Button()
        Me.grpCPPorCPT = New System.Windows.Forms.GroupBox()
        Me.rdbCPP = New System.Windows.Forms.RadioButton()
        Me.rdbCPT = New System.Windows.Forms.RadioButton()
        Me.cmdDeleteTarget = New System.Windows.Forms.Button()
        Me.cmdCopy = New System.Windows.Forms.Button()
        Me.cmdAddTarget = New System.Windows.Forms.Button()
        Me.chkCalcCPP = New System.Windows.Forms.CheckBox()
        Me.cmbTarget = New System.Windows.Forms.ComboBox()
        Me.cmdWizard = New System.Windows.Forms.Button()
        Me.cmdRemove = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.txtAdedgeTarget = New System.Windows.Forms.TextBox()
        Me.lblTarget = New System.Windows.Forms.Label()
        Me.grdPricelist = New System.Windows.Forms.DataGridView()
        Me.colFrom = New clTrinity.CalendarColumn()
        Me.colTo = New clTrinity.CalendarColumn()
        Me.colNatUni = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUni = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCPP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CalendarColumn1 = New clTrinity.CalendarColumn()
        Me.CalendarColumn2 = New clTrinity.CalendarColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.mnuWizard = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CreateWeeklyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuWeekOnCPT = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuWeekOnCPP = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMonth = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMonthOnCPT = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMonthOnCPP = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout
        Me.grpCPPorCPT.SuspendLayout
        CType(Me.grdPricelist,System.ComponentModel.ISupportInitialize).BeginInit
        Me.mnuWizard.SuspendLayout
        Me.SuspendLayout
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cmdCalculate)
        Me.GroupBox1.Controls.Add(Me.cmdSetPriceType)
        Me.GroupBox1.Controls.Add(Me.grpCPPorCPT)
        Me.GroupBox1.Controls.Add(Me.cmdDeleteTarget)
        Me.GroupBox1.Controls.Add(Me.cmdCopy)
        Me.GroupBox1.Controls.Add(Me.cmdAddTarget)
        Me.GroupBox1.Controls.Add(Me.chkCalcCPP)
        Me.GroupBox1.Controls.Add(Me.cmbTarget)
        Me.GroupBox1.Controls.Add(Me.cmdWizard)
        Me.GroupBox1.Controls.Add(Me.cmdRemove)
        Me.GroupBox1.Controls.Add(Me.cmdAdd)
        Me.GroupBox1.Controls.Add(Me.txtAdedgeTarget)
        Me.GroupBox1.Controls.Add(Me.lblTarget)
        Me.GroupBox1.Controls.Add(Me.grdPricelist)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(720, 275)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Pricelist"
        '
        'cmdCalculate
        '
        Me.cmdCalculate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdCalculate.FlatAppearance.BorderSize = 0
        Me.cmdCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCalculate.Image = Global.clTrinity.My.Resources.Resources.calculator_2_small
        Me.cmdCalculate.Location = New System.Drawing.Point(694, 177)
        Me.cmdCalculate.Name = "cmdCalculate"
        Me.cmdCalculate.Size = New System.Drawing.Size(22, 22)
        Me.cmdCalculate.TabIndex = 33
        Me.cmdCalculate.UseVisualStyleBackColor = true
        '
        'cmdSetPriceType
        '
        Me.cmdSetPriceType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdSetPriceType.FlatAppearance.BorderSize = 0
        Me.cmdSetPriceType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSetPriceType.Image = Global.clTrinity.My.Resources.Resources.db_2_16x16
        Me.cmdSetPriceType.Location = New System.Drawing.Point(694, 149)
        Me.cmdSetPriceType.Name = "cmdSetPriceType"
        Me.cmdSetPriceType.Size = New System.Drawing.Size(22, 22)
        Me.cmdSetPriceType.TabIndex = 32
        Me.cmdSetPriceType.UseVisualStyleBackColor = true
        '
        'grpCPPorCPT
        '
        Me.grpCPPorCPT.Controls.Add(Me.rdbCPP)
        Me.grpCPPorCPT.Controls.Add(Me.rdbCPT)
        Me.grpCPPorCPT.Location = New System.Drawing.Point(398, 10)
        Me.grpCPPorCPT.Name = "grpCPPorCPT"
        Me.grpCPPorCPT.Size = New System.Drawing.Size(108, 43)
        Me.grpCPPorCPT.TabIndex = 31
        Me.grpCPPorCPT.TabStop = false
        Me.grpCPPorCPT.Text = "Display options:"
        '
        'rdbCPP
        '
        Me.rdbCPP.AutoSize = true
        Me.rdbCPP.Checked = true
        Me.rdbCPP.Location = New System.Drawing.Point(6, 20)
        Me.rdbCPP.Name = "rdbCPP"
        Me.rdbCPP.Size = New System.Drawing.Size(44, 17)
        Me.rdbCPP.TabIndex = 30
        Me.rdbCPP.TabStop = true
        Me.rdbCPP.Text = "CPP"
        Me.rdbCPP.UseVisualStyleBackColor = true
        '
        'rdbCPT
        '
        Me.rdbCPT.AutoSize = true
        Me.rdbCPT.Location = New System.Drawing.Point(58, 20)
        Me.rdbCPT.Name = "rdbCPT"
        Me.rdbCPT.Size = New System.Drawing.Size(43, 17)
        Me.rdbCPT.TabIndex = 29
        Me.rdbCPT.Text = "CPT"
        Me.rdbCPT.UseVisualStyleBackColor = true
        '
        'cmdDeleteTarget
        '
        Me.cmdDeleteTarget.FlatAppearance.BorderSize = 0
        Me.cmdDeleteTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDeleteTarget.Image = CType(resources.GetObject("cmdDeleteTarget.Image"),System.Drawing.Image)
        Me.cmdDeleteTarget.Location = New System.Drawing.Point(125, 33)
        Me.cmdDeleteTarget.Name = "cmdDeleteTarget"
        Me.cmdDeleteTarget.Size = New System.Drawing.Size(22, 22)
        Me.cmdDeleteTarget.TabIndex = 29
        Me.cmdDeleteTarget.UseVisualStyleBackColor = true
        '
        'cmdCopy
        '
        Me.cmdCopy.FlatAppearance.BorderSize = 0
        Me.cmdCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCopy.Image = Global.clTrinity.My.Resources.Resources.copy_3_16x16
        Me.cmdCopy.Location = New System.Drawing.Point(153, 33)
        Me.cmdCopy.Name = "cmdCopy"
        Me.cmdCopy.Size = New System.Drawing.Size(22, 22)
        Me.cmdCopy.TabIndex = 26
        Me.cmdCopy.UseVisualStyleBackColor = true
        '
        'cmdAddTarget
        '
        Me.cmdAddTarget.FlatAppearance.BorderSize = 0
        Me.cmdAddTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddTarget.Image = CType(resources.GetObject("cmdAddTarget.Image"),System.Drawing.Image)
        Me.cmdAddTarget.Location = New System.Drawing.Point(97, 33)
        Me.cmdAddTarget.Name = "cmdAddTarget"
        Me.cmdAddTarget.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddTarget.TabIndex = 28
        Me.cmdAddTarget.UseVisualStyleBackColor = true
        '
        'chkCalcCPP
        '
        Me.chkCalcCPP.AutoSize = true
        Me.chkCalcCPP.Location = New System.Drawing.Point(531, 30)
        Me.chkCalcCPP.Name = "chkCalcCPP"
        Me.chkCalcCPP.Size = New System.Drawing.Size(169, 17)
        Me.chkCalcCPP.TabIndex = 23
        Me.chkCalcCPP.Text = "Calculate CPP from dayparts"
        Me.chkCalcCPP.UseVisualStyleBackColor = true
        '
        'cmbTarget
        '
        Me.cmbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTarget.FormattingEnabled = true
        Me.cmbTarget.Location = New System.Drawing.Point(10, 33)
        Me.cmbTarget.Name = "cmbTarget"
        Me.cmbTarget.Size = New System.Drawing.Size(81, 21)
        Me.cmbTarget.TabIndex = 27
        '
        'cmdWizard
        '
        Me.cmdWizard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdWizard.FlatAppearance.BorderSize = 0
        Me.cmdWizard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdWizard.Image = Global.clTrinity.My.Resources.Resources.flash_2
        Me.cmdWizard.Location = New System.Drawing.Point(694, 121)
        Me.cmdWizard.Name = "cmdWizard"
        Me.cmdWizard.Size = New System.Drawing.Size(22, 22)
        Me.cmdWizard.TabIndex = 19
        Me.cmdWizard.UseVisualStyleBackColor = true
        '
        'cmdRemove
        '
        Me.cmdRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdRemove.FlatAppearance.BorderSize = 0
        Me.cmdRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRemove.Image = CType(resources.GetObject("cmdRemove.Image"),System.Drawing.Image)
        Me.cmdRemove.Location = New System.Drawing.Point(694, 93)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(22, 22)
        Me.cmdRemove.TabIndex = 18
        Me.cmdRemove.UseVisualStyleBackColor = true
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.FlatAppearance.BorderSize = 0
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"),System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(694, 65)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(22, 22)
        Me.cmdAdd.TabIndex = 17
        Me.cmdAdd.UseVisualStyleBackColor = true
        '
        'txtAdedgeTarget
        '
        Me.txtAdedgeTarget.Location = New System.Drawing.Point(254, 34)
        Me.txtAdedgeTarget.Name = "txtAdedgeTarget"
        Me.txtAdedgeTarget.Size = New System.Drawing.Size(81, 22)
        Me.txtAdedgeTarget.TabIndex = 7
        '
        'lblTarget
        '
        Me.lblTarget.AutoSize = true
        Me.lblTarget.Location = New System.Drawing.Point(256, 17)
        Me.lblTarget.Name = "lblTarget"
        Me.lblTarget.Size = New System.Drawing.Size(81, 13)
        Me.lblTarget.TabIndex = 6
        Me.lblTarget.Text = "Adedge Target"
        '
        'grdPricelist
        '
        Me.grdPricelist.AllowUserToAddRows = false
        Me.grdPricelist.AllowUserToDeleteRows = false
        Me.grdPricelist.AllowUserToResizeColumns = false
        Me.grdPricelist.AllowUserToResizeRows = false
        Me.grdPricelist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdPricelist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grdPricelist.BackgroundColor = System.Drawing.Color.Silver
        Me.grdPricelist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPricelist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colFrom, Me.colTo, Me.colNatUni, Me.colUni, Me.colCPP})
        Me.grdPricelist.Location = New System.Drawing.Point(11, 65)
        Me.grdPricelist.Name = "grdPricelist"
        Me.grdPricelist.RowHeadersVisible = false
        Me.grdPricelist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPricelist.Size = New System.Drawing.Size(680, 204)
        Me.grdPricelist.TabIndex = 5
        Me.grdPricelist.VirtualMode = true
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
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(11, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Target Name"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = true
        Me.Label6.Location = New System.Drawing.Point(100, 299)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(373, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "text has calculated prices (change display option to get the saved price)"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = true
        Me.Label5.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label5.Location = New System.Drawing.Point(76, 299)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "blue"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(20, 299)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Rows with"
        '
        'CalendarColumn1
        '
        Me.CalendarColumn1.HeaderText = "From"
        Me.CalendarColumn1.Name = "CalendarColumn1"
        Me.CalendarColumn1.Width = 137
        '
        'CalendarColumn2
        '
        Me.CalendarColumn2.HeaderText = "To"
        Me.CalendarColumn2.Name = "CalendarColumn2"
        Me.CalendarColumn2.Width = 128
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "NatUni"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 137
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Uni"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 138
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "CPP"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 137
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOK.FlatAppearance.BorderSize = 0
        Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOK.Location = New System.Drawing.Point(657, 299)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 29)
        Me.cmdOK.TabIndex = 16
        Me.cmdOK.Text = "Apply"
        Me.cmdOK.UseVisualStyleBackColor = true
        '
        'mnuWizard
        '
        Me.mnuWizard.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateWeeklyToolStripMenuItem, Me.mnuMonth, Me.ToolStripSeparator1})
        Me.mnuWizard.Name = "mnuWizard"
        Me.mnuWizard.ShowImageMargin = false
        Me.mnuWizard.Size = New System.Drawing.Size(132, 54)
        '
        'CreateWeeklyToolStripMenuItem
        '
        Me.CreateWeeklyToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuWeekOnCPT, Me.mnuWeekOnCPP})
        Me.CreateWeeklyToolStripMenuItem.Name = "CreateWeeklyToolStripMenuItem"
        Me.CreateWeeklyToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
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
        Me.mnuMonth.Size = New System.Drawing.Size(131, 22)
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
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(128, 6)
        '
        'frmContractTarget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(739, 340)
        Me.ControlBox = false
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmContractTarget"
        Me.Text = "Contract Targets"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.grpCPPorCPT.ResumeLayout(false)
        Me.grpCPPorCPT.PerformLayout
        CType(Me.grdPricelist,System.ComponentModel.ISupportInitialize).EndInit
        Me.mnuWizard.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSetPriceType As System.Windows.Forms.Button
    Friend WithEvents grpCPPorCPT As System.Windows.Forms.GroupBox
    Friend WithEvents rdbCPP As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCPT As System.Windows.Forms.RadioButton
    Friend WithEvents cmdCopy As System.Windows.Forms.Button
    Friend WithEvents chkCalcCPP As System.Windows.Forms.CheckBox
    Friend WithEvents cmdWizard As System.Windows.Forms.Button
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents txtAdedgeTarget As System.Windows.Forms.TextBox
    Friend WithEvents lblTarget As System.Windows.Forms.Label
    Friend WithEvents grdPricelist As System.Windows.Forms.DataGridView
    Friend WithEvents colFrom As clTrinity.CalendarColumn
    Friend WithEvents colTo As clTrinity.CalendarColumn
    Friend WithEvents colNatUni As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUni As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCPP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CalendarColumn1 As clTrinity.CalendarColumn
    Friend WithEvents CalendarColumn2 As clTrinity.CalendarColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents mnuWizard As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CreateWeeklyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuWeekOnCPT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuWeekOnCPP As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMonth As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMonthOnCPT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMonthOnCPP As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmdDeleteTarget As System.Windows.Forms.Button
    Friend WithEvents cmdAddTarget As System.Windows.Forms.Button
    Friend WithEvents cmbTarget As System.Windows.Forms.ComboBox
    Friend WithEvents cmdCalculate As System.Windows.Forms.Button
End Class
