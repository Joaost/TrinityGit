<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintBooking
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintBooking))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkDefault = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbAlign = New System.Windows.Forms.ComboBox()
        Me.cmbColorScheme = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbLogo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmdColumns = New System.Windows.Forms.Button()
        Me.lblChannels = New System.Windows.Forms.Label()
        Me.lblWeeks = New System.Windows.Forms.Label()
        Me.cmdChannels = New System.Windows.Forms.Button()
        Me.cmdWeeks = New System.Windows.Forms.Button()
        Me.grpContacts = New System.Windows.Forms.GroupBox()
        Me.cmdRemoveContracts = New System.Windows.Forms.Button()
        Me.grdContacts = New System.Windows.Forms.DataGridView()
        Me.colContact = New clTrinity.ExtendedComboboxColumnWrite()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.lblOldPricelist = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.cmdLanguage = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkIncludePremiums = New System.Windows.Forms.CheckBox()
        Me.chkCustomBilling = New System.Windows.Forms.CheckBox()
        Me.chkCustomOrderPlacer = New System.Windows.Forms.CheckBox()
        Me.cmbBillingAddress = New System.Windows.Forms.ComboBox()
        Me.cmbOrderPlacer = New System.Windows.Forms.ComboBox()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.ExtendedComboboxColumnWrite1 = New clTrinity.ExtendedComboboxColumnWrite()
        Me.ExtendedComboboxColumn1 = New clTrinity.ExtendedComboboxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1.SuspendLayout
        Me.GroupBox2.SuspendLayout
        Me.grpContacts.SuspendLayout
        CType(Me.grdContacts,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Language:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkDefault)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbAlign)
        Me.GroupBox1.Controls.Add(Me.cmbColorScheme)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbLogo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 61)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(226, 127)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Appearance"
        '
        'chkDefault
        '
        Me.chkDefault.AutoSize = true
        Me.chkDefault.Location = New System.Drawing.Point(6, 103)
        Me.chkDefault.Name = "chkDefault"
        Me.chkDefault.Size = New System.Drawing.Size(103, 17)
        Me.chkDefault.TabIndex = 9
        Me.chkDefault.Text = "Save as default"
        Me.chkDefault.UseVisualStyleBackColor = true
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(153, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Align"
        '
        'cmbAlign
        '
        Me.cmbAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlign.FormattingEnabled = true
        Me.cmbAlign.Items.AddRange(New Object() {"Left", "Center", "Right"})
        Me.cmbAlign.Location = New System.Drawing.Point(152, 33)
        Me.cmbAlign.Name = "cmbAlign"
        Me.cmbAlign.Size = New System.Drawing.Size(68, 21)
        Me.cmbAlign.TabIndex = 4
        '
        'cmbColorScheme
        '
        Me.cmbColorScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbColorScheme.FormattingEnabled = true
        Me.cmbColorScheme.Location = New System.Drawing.Point(6, 75)
        Me.cmbColorScheme.Name = "cmbColorScheme"
        Me.cmbColorScheme.Size = New System.Drawing.Size(214, 21)
        Me.cmbColorScheme.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(6, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Color scheme"
        '
        'cmbLogo
        '
        Me.cmbLogo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLogo.FormattingEnabled = true
        Me.cmbLogo.Location = New System.Drawing.Point(6, 33)
        Me.cmbLogo.Name = "cmbLogo"
        Me.cmbLogo.Size = New System.Drawing.Size(140, 21)
        Me.cmbLogo.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Logo"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdColumns)
        Me.GroupBox2.Controls.Add(Me.lblChannels)
        Me.GroupBox2.Controls.Add(Me.lblWeeks)
        Me.GroupBox2.Controls.Add(Me.cmdChannels)
        Me.GroupBox2.Controls.Add(Me.cmdWeeks)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 194)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(226, 79)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = false
        Me.GroupBox2.Text = "Restrictions"
        '
        'cmdColumns
        '
        Me.cmdColumns.FlatAppearance.BorderSize = 0
        Me.cmdColumns.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdColumns.Location = New System.Drawing.Point(98, 19)
        Me.cmdColumns.Name = "cmdColumns"
        Me.cmdColumns.Size = New System.Drawing.Size(61, 23)
        Me.cmdColumns.TabIndex = 4
        Me.cmdColumns.Text = "Columns"
        Me.cmdColumns.UseVisualStyleBackColor = true
        '
        'lblChannels
        '
        Me.lblChannels.AutoSize = true
        Me.lblChannels.Location = New System.Drawing.Point(73, 52)
        Me.lblChannels.Name = "lblChannels"
        Me.lblChannels.Size = New System.Drawing.Size(20, 13)
        Me.lblChannels.TabIndex = 3
        Me.lblChannels.Text = "All"
        '
        'lblWeeks
        '
        Me.lblWeeks.AutoSize = true
        Me.lblWeeks.Location = New System.Drawing.Point(73, 23)
        Me.lblWeeks.Name = "lblWeeks"
        Me.lblWeeks.Size = New System.Drawing.Size(20, 13)
        Me.lblWeeks.TabIndex = 2
        Me.lblWeeks.Text = "All"
        '
        'cmdChannels
        '
        Me.cmdChannels.FlatAppearance.BorderSize = 0
        Me.cmdChannels.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdChannels.Location = New System.Drawing.Point(6, 48)
        Me.cmdChannels.Name = "cmdChannels"
        Me.cmdChannels.Size = New System.Drawing.Size(61, 23)
        Me.cmdChannels.TabIndex = 1
        Me.cmdChannels.Text = "Channels"
        Me.cmdChannels.UseVisualStyleBackColor = true
        '
        'cmdWeeks
        '
        Me.cmdWeeks.FlatAppearance.BorderSize = 0
        Me.cmdWeeks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdWeeks.Location = New System.Drawing.Point(6, 19)
        Me.cmdWeeks.Name = "cmdWeeks"
        Me.cmdWeeks.Size = New System.Drawing.Size(61, 23)
        Me.cmdWeeks.TabIndex = 0
        Me.cmdWeeks.Text = "Weeks"
        Me.cmdWeeks.UseVisualStyleBackColor = true
        '
        'grpContacts
        '
        Me.grpContacts.Controls.Add(Me.cmdRemoveContracts)
        Me.grpContacts.Controls.Add(Me.grdContacts)
        Me.grpContacts.Location = New System.Drawing.Point(244, 61)
        Me.grpContacts.Name = "grpContacts"
        Me.grpContacts.Size = New System.Drawing.Size(331, 142)
        Me.grpContacts.TabIndex = 5
        Me.grpContacts.TabStop = false
        Me.grpContacts.Text = "Contacts"
        '
        'cmdRemoveContracts
        '
        Me.cmdRemoveContracts.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveContracts.FlatAppearance.BorderSize = 0
        Me.cmdRemoveContracts.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRemoveContracts.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdRemoveContracts.Location = New System.Drawing.Point(303, 16)
        Me.cmdRemoveContracts.Name = "cmdRemoveContracts"
        Me.cmdRemoveContracts.Size = New System.Drawing.Size(22, 22)
        Me.cmdRemoveContracts.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmdRemoveContracts, "Remove All contracts")
        Me.cmdRemoveContracts.UseVisualStyleBackColor = true
        '
        'grdContacts
        '
        Me.grdContacts.AllowUserToAddRows = false
        Me.grdContacts.AllowUserToDeleteRows = false
        Me.grdContacts.AllowUserToOrderColumns = true
        Me.grdContacts.AllowUserToResizeColumns = false
        Me.grdContacts.AllowUserToResizeRows = false
        Me.grdContacts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdContacts.ColumnHeadersVisible = false
        Me.grdContacts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colContact})
        Me.grdContacts.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdContacts.Location = New System.Drawing.Point(6, 16)
        Me.grdContacts.Name = "grdContacts"
        Me.grdContacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdContacts.Size = New System.Drawing.Size(291, 120)
        Me.grdContacts.TabIndex = 0
        Me.grdContacts.VirtualMode = true
        '
        'colContact
        '
        Me.colContact.HeaderText = "Contact"
        Me.colContact.Name = "colContact"
        Me.colContact.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colContact.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Location = New System.Drawing.Point(12, 293)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(75, 31)
        Me.cmdOk.TabIndex = 6
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = true
        Me.cmdOk.Visible = false
        '
        'lblOldPricelist
        '
        Me.lblOldPricelist.AutoSize = true
        Me.lblOldPricelist.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblOldPricelist.ForeColor = System.Drawing.Color.Red
        Me.lblOldPricelist.Location = New System.Drawing.Point(125, 26)
        Me.lblOldPricelist.Name = "lblOldPricelist"
        Me.lblOldPricelist.Size = New System.Drawing.Size(274, 13)
        Me.lblOldPricelist.TabIndex = 8
        Me.lblOldPricelist.Text = "The pricelists of this campaign are not all up to date"
        Me.lblOldPricelist.Visible = false
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.excel_2
        Me.PictureBox1.Location = New System.Drawing.Point(551, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = false
        '
        'cmdLanguage
        '
        Me.cmdLanguage.FlatAppearance.BorderSize = 0
        Me.cmdLanguage.Image = CType(resources.GetObject("cmdLanguage.Image"),System.Drawing.Image)
        Me.cmdLanguage.Location = New System.Drawing.Point(76, 13)
        Me.cmdLanguage.Name = "cmdLanguage"
        Me.cmdLanguage.Size = New System.Drawing.Size(43, 41)
        Me.cmdLanguage.TabIndex = 2
        Me.cmdLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdLanguage.UseVisualStyleBackColor = true
        '
        'chkIncludePremiums
        '
        Me.chkIncludePremiums.AutoSize = true
        Me.chkIncludePremiums.Location = New System.Drawing.Point(250, 261)
        Me.chkIncludePremiums.Name = "chkIncludePremiums"
        Me.chkIncludePremiums.Size = New System.Drawing.Size(117, 17)
        Me.chkIncludePremiums.TabIndex = 9
        Me.chkIncludePremiums.Text = "Include premiums"
        Me.chkIncludePremiums.UseVisualStyleBackColor = true
        '
        'chkCustomBilling
        '
        Me.chkCustomBilling.AutoSize = true
        Me.chkCustomBilling.Location = New System.Drawing.Point(250, 213)
        Me.chkCustomBilling.Name = "chkCustomBilling"
        Me.chkCustomBilling.Size = New System.Drawing.Size(144, 17)
        Me.chkCustomBilling.TabIndex = 10
        Me.chkCustomBilling.Text = "Custom billing address"
        Me.chkCustomBilling.UseVisualStyleBackColor = true
        '
        'chkCustomOrderPlacer
        '
        Me.chkCustomOrderPlacer.AutoSize = true
        Me.chkCustomOrderPlacer.Location = New System.Drawing.Point(250, 237)
        Me.chkCustomOrderPlacer.Name = "chkCustomOrderPlacer"
        Me.chkCustomOrderPlacer.Size = New System.Drawing.Size(130, 17)
        Me.chkCustomOrderPlacer.TabIndex = 11
        Me.chkCustomOrderPlacer.Text = "Custom order placer"
        Me.chkCustomOrderPlacer.UseVisualStyleBackColor = true
        '
        'cmbBillingAddress
        '
        Me.cmbBillingAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmbBillingAddress.FormattingEnabled = true
        Me.cmbBillingAddress.Location = New System.Drawing.Point(380, 209)
        Me.cmbBillingAddress.Name = "cmbBillingAddress"
        Me.cmbBillingAddress.Size = New System.Drawing.Size(195, 21)
        Me.cmbBillingAddress.TabIndex = 12
        '
        'cmbOrderPlacer
        '
        Me.cmbOrderPlacer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmbOrderPlacer.FormattingEnabled = true
        Me.cmbOrderPlacer.Location = New System.Drawing.Point(380, 235)
        Me.cmbOrderPlacer.Name = "cmbOrderPlacer"
        Me.cmbOrderPlacer.Size = New System.Drawing.Size(195, 21)
        Me.cmbOrderPlacer.TabIndex = 13
        '
        'cmdPrint
        '
        Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdPrint.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.FlatAppearance.BorderSize = 0
        Me.cmdPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPrint.Location = New System.Drawing.Point(500, 293)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(75, 31)
        Me.cmdPrint.TabIndex = 14
        Me.cmdPrint.Text = "Ok"
        Me.cmdPrint.UseVisualStyleBackColor = true
        '
        'ExtendedComboboxColumnWrite1
        '
        Me.ExtendedComboboxColumnWrite1.HeaderText = "Contact"
        Me.ExtendedComboboxColumnWrite1.Name = "ExtendedComboboxColumnWrite1"
        Me.ExtendedComboboxColumnWrite1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ExtendedComboboxColumnWrite1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ExtendedComboboxColumn1
        '
        Me.ExtendedComboboxColumn1.HeaderText = "Contact"
        Me.ExtendedComboboxColumn1.Name = "ExtendedComboboxColumn1"
        Me.ExtendedComboboxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ExtendedComboboxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Contact"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'frmPrintBooking
        '
        Me.AcceptButton = Me.cmdPrint
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 336)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.cmbOrderPlacer)
        Me.Controls.Add(Me.cmbBillingAddress)
        Me.Controls.Add(Me.chkCustomOrderPlacer)
        Me.Controls.Add(Me.chkCustomBilling)
        Me.Controls.Add(Me.chkIncludePremiums)
        Me.Controls.Add(Me.lblOldPricelist)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.grpContacts)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdLanguage)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmPrintBooking"
        Me.Text = "Export booking to Excel"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.grpContacts.ResumeLayout(false)
        CType(Me.grdContacts,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdLanguage As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbColorScheme As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbLogo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdWeeks As System.Windows.Forms.Button
    Friend WithEvents lblChannels As System.Windows.Forms.Label
    Friend WithEvents lblWeeks As System.Windows.Forms.Label
    Friend WithEvents cmdChannels As System.Windows.Forms.Button
    Friend WithEvents grpContacts As System.Windows.Forms.GroupBox
    Friend WithEvents grdContacts As System.Windows.Forms.DataGridView
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbAlign As System.Windows.Forms.ComboBox
    Friend WithEvents lblOldPricelist As System.Windows.Forms.Label
    Friend WithEvents chkDefault As System.Windows.Forms.CheckBox
    Friend WithEvents colContact As clTrinity.ExtendedComboboxColumnWrite
    Friend WithEvents ExtendedComboboxColumn1 As clTrinity.ExtendedComboboxColumn
    Friend WithEvents cmdRemoveContracts As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents chkIncludePremiums As System.Windows.Forms.CheckBox
    Friend WithEvents cmdColumns As System.Windows.Forms.Button
    Friend WithEvents chkCustomBilling As System.Windows.Forms.CheckBox
    Friend WithEvents chkCustomOrderPlacer As System.Windows.Forms.CheckBox
    Friend WithEvents cmbBillingAddress As System.Windows.Forms.ComboBox
    Friend WithEvents cmbOrderPlacer As System.Windows.Forms.ComboBox
    Friend WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents ExtendedComboboxColumnWrite1 As clTrinity.ExtendedComboboxColumnWrite
End Class
