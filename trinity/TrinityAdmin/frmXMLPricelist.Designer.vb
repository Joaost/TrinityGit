<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmXMLPricelist
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmXMLPricelist))
        Me.txtIdx100CPP = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.grdPricelist = New System.Windows.Forms.DataGridView
        Me._Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.From = New trinityAdmin.Declarations.CalendarColumn
        Me.cTo = New trinityAdmin.Declarations.CalendarColumn
        Me.colCPP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmbTarget = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.mnuMonth = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdCopyTarget = New System.Windows.Forms.Button
        Me.cmdCalculate = New System.Windows.Forms.Button
        Me.cmdCopy = New System.Windows.Forms.Button
        Me.cmdDeleteTarget = New System.Windows.Forms.Button
        Me.cmdAddTarget = New System.Windows.Forms.Button
        Me.cmdWizard = New System.Windows.Forms.Button
        Me.cmdRemove = New System.Windows.Forms.Button
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.CreateWeeklyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuWizard = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.grpCPPorCPT = New System.Windows.Forms.GroupBox
        Me.rdbCPP = New System.Windows.Forms.RadioButton
        Me.rdbCPT = New System.Windows.Forms.RadioButton
        Me.chkCalcCPP = New System.Windows.Forms.CheckBox
        Me.chkStandard = New System.Windows.Forms.CheckBox
        Me.grbPricelist = New System.Windows.Forms.GroupBox
        Me.lblDELETE = New System.Windows.Forms.Label
        Me.txtChnUni = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtNatUni = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtAdedgeTarget = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.cmbFile = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbBT = New System.Windows.Forms.ComboBox
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.chkAddIfNotFound = New System.Windows.Forms.CheckBox
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdPricelist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuWizard.SuspendLayout()
        Me.grpCPPorCPT.SuspendLayout()
        Me.grbPricelist.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtIdx100CPP
        '
        Me.txtIdx100CPP.Location = New System.Drawing.Point(378, 16)
        Me.txtIdx100CPP.Name = "txtIdx100CPP"
        Me.txtIdx100CPP.Size = New System.Drawing.Size(71, 20)
        Me.txtIdx100CPP.TabIndex = 31
        Me.txtIdx100CPP.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(306, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "Idx 100 CPP"
        Me.Label6.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Adedge Target"
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
        Me.grdPricelist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me._Name, Me.From, Me.cTo, Me.colCPP})
        Me.grdPricelist.Location = New System.Drawing.Point(6, 111)
        Me.grdPricelist.Name = "grdPricelist"
        Me.grdPricelist.RowHeadersVisible = False
        Me.grdPricelist.Size = New System.Drawing.Size(661, 305)
        Me.grdPricelist.TabIndex = 5
        '
        '_Name
        '
        Me._Name.HeaderText = "Name"
        Me._Name.Name = "_Name"
        '
        'From
        '
        Me.From.HeaderText = "From"
        Me.From.Name = "From"
        Me.From.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.From.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'cTo
        '
        Me.cTo.HeaderText = "To"
        Me.cTo.Name = "cTo"
        Me.cTo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cTo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'colCPP
        '
        Me.colCPP.HeaderText = "CPP"
        Me.colCPP.Name = "colCPP"
        '
        'cmbTarget
        '
        Me.cmbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTarget.FormattingEnabled = True
        Me.cmbTarget.Location = New System.Drawing.Point(6, 33)
        Me.cmbTarget.Name = "cmbTarget"
        Me.cmbTarget.Size = New System.Drawing.Size(81, 21)
        Me.cmbTarget.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Target"
        '
        'mnuMonth
        '
        Me.mnuMonth.Name = "mnuMonth"
        Me.mnuMonth.Size = New System.Drawing.Size(134, 22)
        Me.mnuMonth.Text = "Create Monthly"
        '
        'cmdCopyTarget
        '
        Me.cmdCopyTarget.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCopyTarget.Image = CType(resources.GetObject("cmdCopyTarget.Image"), System.Drawing.Image)
        Me.cmdCopyTarget.Location = New System.Drawing.Point(673, 195)
        Me.cmdCopyTarget.Name = "cmdCopyTarget"
        Me.cmdCopyTarget.Size = New System.Drawing.Size(22, 22)
        Me.cmdCopyTarget.TabIndex = 28
        Me.ToolTip.SetToolTip(Me.cmdCopyTarget, "Copy price list from another target")
        Me.cmdCopyTarget.UseVisualStyleBackColor = True
        '
        'cmdCalculate
        '
        Me.cmdCalculate.Enabled = False
        Me.cmdCalculate.Image = CType(resources.GetObject("cmdCalculate.Image"), System.Drawing.Image)
        Me.cmdCalculate.Location = New System.Drawing.Point(247, 74)
        Me.cmdCalculate.Name = "cmdCalculate"
        Me.cmdCalculate.Size = New System.Drawing.Size(22, 22)
        Me.cmdCalculate.TabIndex = 27
        Me.ToolTip.SetToolTip(Me.cmdCalculate, "Get universe sizes from Advantege")
        Me.cmdCalculate.UseVisualStyleBackColor = True
        '
        'cmdCopy
        '
        Me.cmdCopy.Enabled = False
        Me.cmdCopy.Image = CType(resources.GetObject("cmdCopy.Image"), System.Drawing.Image)
        Me.cmdCopy.Location = New System.Drawing.Point(149, 33)
        Me.cmdCopy.Name = "cmdCopy"
        Me.cmdCopy.Size = New System.Drawing.Size(22, 22)
        Me.cmdCopy.TabIndex = 26
        Me.ToolTip.SetToolTip(Me.cmdCopy, "Copy price list from another channel")
        Me.cmdCopy.UseVisualStyleBackColor = True
        '
        'cmdDeleteTarget
        '
        Me.cmdDeleteTarget.Image = CType(resources.GetObject("cmdDeleteTarget.Image"), System.Drawing.Image)
        Me.cmdDeleteTarget.Location = New System.Drawing.Point(121, 33)
        Me.cmdDeleteTarget.Name = "cmdDeleteTarget"
        Me.cmdDeleteTarget.Size = New System.Drawing.Size(22, 22)
        Me.cmdDeleteTarget.TabIndex = 21
        Me.ToolTip.SetToolTip(Me.cmdDeleteTarget, "Delete Target")
        Me.cmdDeleteTarget.UseVisualStyleBackColor = True
        '
        'cmdAddTarget
        '
        Me.cmdAddTarget.Image = CType(resources.GetObject("cmdAddTarget.Image"), System.Drawing.Image)
        Me.cmdAddTarget.Location = New System.Drawing.Point(93, 33)
        Me.cmdAddTarget.Name = "cmdAddTarget"
        Me.cmdAddTarget.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddTarget.TabIndex = 20
        Me.ToolTip.SetToolTip(Me.cmdAddTarget, "Add Target")
        Me.cmdAddTarget.UseVisualStyleBackColor = True
        '
        'cmdWizard
        '
        Me.cmdWizard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdWizard.Location = New System.Drawing.Point(673, 167)
        Me.cmdWizard.Name = "cmdWizard"
        Me.cmdWizard.Size = New System.Drawing.Size(22, 22)
        Me.cmdWizard.TabIndex = 19
        Me.ToolTip.SetToolTip(Me.cmdWizard, "Price list wizard")
        Me.cmdWizard.UseVisualStyleBackColor = True
        '
        'cmdRemove
        '
        Me.cmdRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemove.Image = CType(resources.GetObject("cmdRemove.Image"), System.Drawing.Image)
        Me.cmdRemove.Location = New System.Drawing.Point(673, 139)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(22, 22)
        Me.cmdRemove.TabIndex = 18
        Me.ToolTip.SetToolTip(Me.cmdRemove, "Delete price list row")
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(673, 111)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(22, 22)
        Me.cmdAdd.TabIndex = 17
        Me.ToolTip.SetToolTip(Me.cmdAdd, "Add row  to price list ")
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'CreateWeeklyToolStripMenuItem
        '
        Me.CreateWeeklyToolStripMenuItem.Name = "CreateWeeklyToolStripMenuItem"
        Me.CreateWeeklyToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.CreateWeeklyToolStripMenuItem.Text = "Create Weekly"
        '
        'mnuWizard
        '
        Me.mnuWizard.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateWeeklyToolStripMenuItem, Me.mnuMonth})
        Me.mnuWizard.Name = "mnuWizard"
        Me.mnuWizard.ShowImageMargin = False
        Me.mnuWizard.Size = New System.Drawing.Size(135, 48)
        '
        'grpCPPorCPT
        '
        Me.grpCPPorCPT.Controls.Add(Me.rdbCPP)
        Me.grpCPPorCPT.Controls.Add(Me.rdbCPT)
        Me.grpCPPorCPT.Enabled = False
        Me.grpCPPorCPT.Location = New System.Drawing.Point(545, 47)
        Me.grpCPPorCPT.Name = "grpCPPorCPT"
        Me.grpCPPorCPT.Size = New System.Drawing.Size(122, 49)
        Me.grpCPPorCPT.TabIndex = 31
        Me.grpCPPorCPT.TabStop = False
        Me.grpCPPorCPT.Text = "Display prices in:"
        '
        'rdbCPP
        '
        Me.rdbCPP.AutoSize = True
        Me.rdbCPP.Checked = True
        Me.rdbCPP.Location = New System.Drawing.Point(12, 22)
        Me.rdbCPP.Name = "rdbCPP"
        Me.rdbCPP.Size = New System.Drawing.Size(46, 17)
        Me.rdbCPP.TabIndex = 30
        Me.rdbCPP.TabStop = True
        Me.rdbCPP.Text = "CPP"
        Me.rdbCPP.UseVisualStyleBackColor = True
        '
        'rdbCPT
        '
        Me.rdbCPT.AutoSize = True
        Me.rdbCPT.Location = New System.Drawing.Point(66, 22)
        Me.rdbCPT.Name = "rdbCPT"
        Me.rdbCPT.Size = New System.Drawing.Size(46, 17)
        Me.rdbCPT.TabIndex = 29
        Me.rdbCPT.Text = "CPT"
        Me.rdbCPT.UseVisualStyleBackColor = True
        '
        'chkCalcCPP
        '
        Me.chkCalcCPP.AutoSize = True
        Me.chkCalcCPP.Location = New System.Drawing.Point(309, 54)
        Me.chkCalcCPP.Name = "chkCalcCPP"
        Me.chkCalcCPP.Size = New System.Drawing.Size(160, 17)
        Me.chkCalcCPP.TabIndex = 23
        Me.chkCalcCPP.Text = "Calculate CPP from dayparts"
        Me.chkCalcCPP.UseVisualStyleBackColor = True
        '
        'chkStandard
        '
        Me.chkStandard.AutoSize = True
        Me.chkStandard.Location = New System.Drawing.Point(309, 78)
        Me.chkStandard.Name = "chkStandard"
        Me.chkStandard.Size = New System.Drawing.Size(99, 17)
        Me.chkStandard.TabIndex = 22
        Me.chkStandard.Text = "Standard target"
        Me.chkStandard.UseVisualStyleBackColor = True
        '
        'grbPricelist
        '
        Me.grbPricelist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grbPricelist.Controls.Add(Me.lblDELETE)
        Me.grbPricelist.Controls.Add(Me.Label6)
        Me.grbPricelist.Controls.Add(Me.txtIdx100CPP)
        Me.grbPricelist.Controls.Add(Me.grpCPPorCPT)
        Me.grbPricelist.Controls.Add(Me.cmdCopyTarget)
        Me.grbPricelist.Controls.Add(Me.cmdCalculate)
        Me.grbPricelist.Controls.Add(Me.cmdCopy)
        Me.grbPricelist.Controls.Add(Me.chkCalcCPP)
        Me.grbPricelist.Controls.Add(Me.chkStandard)
        Me.grbPricelist.Controls.Add(Me.cmdDeleteTarget)
        Me.grbPricelist.Controls.Add(Me.cmdAddTarget)
        Me.grbPricelist.Controls.Add(Me.cmdWizard)
        Me.grbPricelist.Controls.Add(Me.cmdRemove)
        Me.grbPricelist.Controls.Add(Me.cmdAdd)
        Me.grbPricelist.Controls.Add(Me.txtChnUni)
        Me.grbPricelist.Controls.Add(Me.Label5)
        Me.grbPricelist.Controls.Add(Me.txtNatUni)
        Me.grbPricelist.Controls.Add(Me.Label4)
        Me.grbPricelist.Controls.Add(Me.txtAdedgeTarget)
        Me.grbPricelist.Controls.Add(Me.Label3)
        Me.grbPricelist.Controls.Add(Me.grdPricelist)
        Me.grbPricelist.Controls.Add(Me.cmbTarget)
        Me.grbPricelist.Controls.Add(Me.Label2)
        Me.grbPricelist.Enabled = False
        Me.grbPricelist.Location = New System.Drawing.Point(12, 53)
        Me.grbPricelist.Name = "grbPricelist"
        Me.grbPricelist.Size = New System.Drawing.Size(701, 422)
        Me.grbPricelist.TabIndex = 28
        Me.grbPricelist.TabStop = False
        Me.grbPricelist.Text = "Pricelist"
        '
        'lblDELETE
        '
        Me.lblDELETE.AutoSize = True
        Me.lblDELETE.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lblDELETE.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDELETE.ForeColor = System.Drawing.Color.Red
        Me.lblDELETE.Location = New System.Drawing.Point(163, 212)
        Me.lblDELETE.Name = "lblDELETE"
        Me.lblDELETE.Size = New System.Drawing.Size(347, 42)
        Me.lblDELETE.TabIndex = 32
        Me.lblDELETE.Text = "WILL BE DELETED"
        Me.lblDELETE.Visible = False
        '
        'txtChnUni
        '
        Me.txtChnUni.Location = New System.Drawing.Point(170, 75)
        Me.txtChnUni.Name = "txtChnUni"
        Me.txtChnUni.Size = New System.Drawing.Size(71, 20)
        Me.txtChnUni.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(172, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Chn Uni"
        '
        'txtNatUni
        '
        Me.txtNatUni.Location = New System.Drawing.Point(93, 75)
        Me.txtNatUni.Name = "txtNatUni"
        Me.txtNatUni.Size = New System.Drawing.Size(71, 20)
        Me.txtNatUni.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(95, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Nat Uni"
        '
        'txtAdedgeTarget
        '
        Me.txtAdedgeTarget.Location = New System.Drawing.Point(6, 75)
        Me.txtAdedgeTarget.Name = "txtAdedgeTarget"
        Me.txtAdedgeTarget.Size = New System.Drawing.Size(81, 20)
        Me.txtAdedgeTarget.TabIndex = 7
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(20, 9)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(26, 13)
        Me.Label15.TabIndex = 33
        Me.Label15.Text = "File:"
        '
        'cmbFile
        '
        Me.cmbFile.FormattingEnabled = True
        Me.cmbFile.Location = New System.Drawing.Point(19, 26)
        Me.cmbFile.Name = "cmbFile"
        Me.cmbFile.Size = New System.Drawing.Size(365, 21)
        Me.cmbFile.TabIndex = 32
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(559, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Booking Type"
        '
        'cmbBT
        '
        Me.cmbBT.FormattingEnabled = True
        Me.cmbBT.Location = New System.Drawing.Point(557, 26)
        Me.cmbBT.Name = "cmbBT"
        Me.cmbBT.Size = New System.Drawing.Size(92, 21)
        Me.cmbBT.TabIndex = 34
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.Location = New System.Drawing.Point(638, 481)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 36
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.Location = New System.Drawing.Point(557, 481)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 37
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.Location = New System.Drawing.Point(12, 481)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(181, 23)
        Me.cmdSave.TabIndex = 38
        Me.cmdSave.Text = "Save changes on this Target"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'chkAddIfNotFound
        '
        Me.chkAddIfNotFound.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkAddIfNotFound.AutoSize = True
        Me.chkAddIfNotFound.Checked = True
        Me.chkAddIfNotFound.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAddIfNotFound.Location = New System.Drawing.Point(199, 485)
        Me.chkAddIfNotFound.Name = "chkAddIfNotFound"
        Me.chkAddIfNotFound.Size = New System.Drawing.Size(98, 17)
        Me.chkAddIfNotFound.TabIndex = 35
        Me.chkAddIfNotFound.Text = "AddIfNotFound"
        Me.chkAddIfNotFound.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 165
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "CPP"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 164
        '
        'frmXMLPricelist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(720, 516)
        Me.Controls.Add(Me.chkAddIfNotFound)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbBT)
        Me.Controls.Add(Me.cmbFile)
        Me.Controls.Add(Me.grbPricelist)
        Me.Name = "frmXMLPricelist"
        Me.Text = "frmXMLPricelist"
        CType(Me.grdPricelist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuWizard.ResumeLayout(False)
        Me.grpCPPorCPT.ResumeLayout(False)
        Me.grpCPPorCPT.PerformLayout()
        Me.grbPricelist.ResumeLayout(False)
        Me.grbPricelist.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtIdx100CPP As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grdPricelist As System.Windows.Forms.DataGridView
    Friend WithEvents cmbTarget As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mnuMonthOnNetCPP As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMonth As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents cmdCopyTarget As System.Windows.Forms.Button
    Friend WithEvents cmdCalculate As System.Windows.Forms.Button
    Friend WithEvents cmdCopy As System.Windows.Forms.Button
    Friend WithEvents cmdDeleteTarget As System.Windows.Forms.Button
    Friend WithEvents cmdAddTarget As System.Windows.Forms.Button
    Friend WithEvents cmdWizard As System.Windows.Forms.Button
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents CreateWeeklyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuWizard As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents grpCPPorCPT As System.Windows.Forms.GroupBox
    Friend WithEvents rdbCPP As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCPT As System.Windows.Forms.RadioButton
    Friend WithEvents chkCalcCPP As System.Windows.Forms.CheckBox
    Friend WithEvents chkStandard As System.Windows.Forms.CheckBox
    Friend WithEvents grbPricelist As System.Windows.Forms.GroupBox
    Friend WithEvents txtChnUni As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNatUni As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAdedgeTarget As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmbFile As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbBT As System.Windows.Forms.ComboBox
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents chkAddIfNotFound As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblDELETE As System.Windows.Forms.Label
    Friend WithEvents _Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend From As trinityAdmin.Declarations.CalendarColumn
    Friend cTo As trinityAdmin.Declarations.CalendarColumn
    Friend WithEvents colCPP As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
