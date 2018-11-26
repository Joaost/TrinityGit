<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetup))
        Me.lblPeriod = New System.Windows.Forms.Label()
        Me.cmdPeriod = New System.Windows.Forms.Button()
        Me.cmdDeleteValue = New System.Windows.Forms.Button()
        Me.cmdAddValue = New System.Windows.Forms.Button()
        Me.cmdCreateAnalysis = New System.Windows.Forms.Button()
        Me.cmdCopyTarget = New System.Windows.Forms.Button()
        Me.grpSearchvalues = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grdSearchValues = New System.Windows.Forms.DataGridView()
        Me.grpChannels = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmdAddChannelWizard = New System.Windows.Forms.Button()
        Me.grdChannels = New System.Windows.Forms.DataGridView()
        Me.cmdDeleteChannel = New System.Windows.Forms.Button()
        Me.cmdAddChannel = New System.Windows.Forms.Button()
        Me.ExtendedComboboxColumn1 = New TrinityAnalysis.ExtendedComboboxColumn()
        Me.ExtendedComboboxColumn2 = New TrinityAnalysis.ExtendedComboboxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChannel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSearch = New TrinityAnalysis.ExtendedComboboxColumn()
        Me.colCriteria = New TrinityAnalysis.ExtendedComboboxColumn()
        Me.colValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpSearchvalues.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.grdSearchValues, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpChannels.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.grdChannels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblPeriod
        '
        Me.lblPeriod.AutoSize = True
        Me.lblPeriod.ForeColor = System.Drawing.Color.Red
        Me.lblPeriod.Location = New System.Drawing.Point(64, 26)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(80, 13)
        Me.lblPeriod.TabIndex = 6
        Me.lblPeriod.Text = "No period is set"
        '
        'cmdPeriod
        '
        Me.cmdPeriod.FlatAppearance.BorderSize = 0
        Me.cmdPeriod.Image = CType(resources.GetObject("cmdPeriod.Image"), System.Drawing.Image)
        Me.cmdPeriod.Location = New System.Drawing.Point(15, 12)
        Me.cmdPeriod.Name = "cmdPeriod"
        Me.cmdPeriod.Size = New System.Drawing.Size(43, 43)
        Me.cmdPeriod.TabIndex = 5
        Me.cmdPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdPeriod.UseVisualStyleBackColor = True
        '
        'cmdDeleteValue
        '
        Me.cmdDeleteValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDeleteValue.Image = CType(resources.GetObject("cmdDeleteValue.Image"), System.Drawing.Image)
        Me.cmdDeleteValue.Location = New System.Drawing.Point(583, 31)
        Me.cmdDeleteValue.Name = "cmdDeleteValue"
        Me.cmdDeleteValue.Size = New System.Drawing.Size(22, 22)
        Me.cmdDeleteValue.TabIndex = 16
        Me.cmdDeleteValue.UseVisualStyleBackColor = True
        '
        'cmdAddValue
        '
        Me.cmdAddValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddValue.Image = CType(resources.GetObject("cmdAddValue.Image"), System.Drawing.Image)
        Me.cmdAddValue.Location = New System.Drawing.Point(582, 3)
        Me.cmdAddValue.Name = "cmdAddValue"
        Me.cmdAddValue.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddValue.TabIndex = 15
        Me.cmdAddValue.UseVisualStyleBackColor = True
        '
        'cmdCreateAnalysis
        '
        Me.cmdCreateAnalysis.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCreateAnalysis.Location = New System.Drawing.Point(534, 479)
        Me.cmdCreateAnalysis.Name = "cmdCreateAnalysis"
        Me.cmdCreateAnalysis.Size = New System.Drawing.Size(100, 30)
        Me.cmdCreateAnalysis.TabIndex = 2
        Me.cmdCreateAnalysis.Text = "Create analysis"
        Me.cmdCreateAnalysis.UseVisualStyleBackColor = True
        '
        'cmdCopyTarget
        '
        Me.cmdCopyTarget.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCopyTarget.Image = CType(resources.GetObject("cmdCopyTarget.Image"), System.Drawing.Image)
        Me.cmdCopyTarget.Location = New System.Drawing.Point(583, 59)
        Me.cmdCopyTarget.Name = "cmdCopyTarget"
        Me.cmdCopyTarget.Size = New System.Drawing.Size(22, 22)
        Me.cmdCopyTarget.TabIndex = 29
        Me.cmdCopyTarget.UseVisualStyleBackColor = True
        '
        'grpSearchvalues
        '
        Me.grpSearchvalues.Controls.Add(Me.Panel1)
        Me.grpSearchvalues.Location = New System.Drawing.Point(12, 249)
        Me.grpSearchvalues.Name = "grpSearchvalues"
        Me.grpSearchvalues.Size = New System.Drawing.Size(619, 225)
        Me.grpSearchvalues.TabIndex = 30
        Me.grpSearchvalues.TabStop = False
        Me.grpSearchvalues.Text = "Search values:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel1.Controls.Add(Me.grdSearchValues)
        Me.Panel1.Controls.Add(Me.cmdCopyTarget)
        Me.Panel1.Controls.Add(Me.cmdDeleteValue)
        Me.Panel1.Controls.Add(Me.cmdAddValue)
        Me.Panel1.Location = New System.Drawing.Point(6, 19)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(607, 195)
        Me.Panel1.TabIndex = 31
        '
        'grdSearchValues
        '
        Me.grdSearchValues.AllowUserToAddRows = False
        Me.grdSearchValues.AllowUserToDeleteRows = False
        Me.grdSearchValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSearchValues.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSearch, Me.colCriteria, Me.colValue})
        Me.grdSearchValues.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdSearchValues.Location = New System.Drawing.Point(3, 3)
        Me.grdSearchValues.Name = "grdSearchValues"
        Me.grdSearchValues.RowHeadersVisible = False
        Me.grdSearchValues.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdSearchValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSearchValues.Size = New System.Drawing.Size(576, 189)
        Me.grdSearchValues.TabIndex = 17
        '
        'grpChannels
        '
        Me.grpChannels.Controls.Add(Me.Panel2)
        Me.grpChannels.Location = New System.Drawing.Point(12, 61)
        Me.grpChannels.Name = "grpChannels"
        Me.grpChannels.Size = New System.Drawing.Size(619, 182)
        Me.grpChannels.TabIndex = 31
        Me.grpChannels.TabStop = False
        Me.grpChannels.Text = "Channels:"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel2.Controls.Add(Me.cmdAddChannelWizard)
        Me.Panel2.Controls.Add(Me.grdChannels)
        Me.Panel2.Controls.Add(Me.cmdDeleteChannel)
        Me.Panel2.Controls.Add(Me.cmdAddChannel)
        Me.Panel2.Location = New System.Drawing.Point(6, 19)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(607, 159)
        Me.Panel2.TabIndex = 31
        '
        'cmdAddChannelWizard
        '
        Me.cmdAddChannelWizard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddChannelWizard.Image = CType(resources.GetObject("cmdAddChannelWizard.Image"), System.Drawing.Image)
        Me.cmdAddChannelWizard.Location = New System.Drawing.Point(582, 31)
        Me.cmdAddChannelWizard.Name = "cmdAddChannelWizard"
        Me.cmdAddChannelWizard.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddChannelWizard.TabIndex = 31
        Me.cmdAddChannelWizard.UseVisualStyleBackColor = True
        '
        'grdChannels
        '
        Me.grdChannels.AllowUserToAddRows = False
        Me.grdChannels.AllowUserToDeleteRows = False
        Me.grdChannels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChannels.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colChannel})
        Me.grdChannels.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdChannels.Location = New System.Drawing.Point(3, 3)
        Me.grdChannels.Name = "grdChannels"
        Me.grdChannels.RowHeadersVisible = False
        Me.grdChannels.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdChannels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdChannels.Size = New System.Drawing.Size(576, 153)
        Me.grdChannels.TabIndex = 17
        '
        'cmdDeleteChannel
        '
        Me.cmdDeleteChannel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDeleteChannel.Image = CType(resources.GetObject("cmdDeleteChannel.Image"), System.Drawing.Image)
        Me.cmdDeleteChannel.Location = New System.Drawing.Point(582, 59)
        Me.cmdDeleteChannel.Name = "cmdDeleteChannel"
        Me.cmdDeleteChannel.Size = New System.Drawing.Size(22, 22)
        Me.cmdDeleteChannel.TabIndex = 16
        Me.cmdDeleteChannel.UseVisualStyleBackColor = True
        '
        'cmdAddChannel
        '
        Me.cmdAddChannel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddChannel.Image = CType(resources.GetObject("cmdAddChannel.Image"), System.Drawing.Image)
        Me.cmdAddChannel.Location = New System.Drawing.Point(582, 3)
        Me.cmdAddChannel.Name = "cmdAddChannel"
        Me.cmdAddChannel.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddChannel.TabIndex = 15
        Me.cmdAddChannel.UseVisualStyleBackColor = True
        '
        'ExtendedComboboxColumn1
        '
        Me.ExtendedComboboxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ExtendedComboboxColumn1.FillWeight = 81.47208!
        Me.ExtendedComboboxColumn1.HeaderText = "Search:"
        Me.ExtendedComboboxColumn1.Name = "ExtendedComboboxColumn1"
        Me.ExtendedComboboxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ExtendedComboboxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ExtendedComboboxColumn2
        '
        Me.ExtendedComboboxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ExtendedComboboxColumn2.FillWeight = 50.0!
        Me.ExtendedComboboxColumn2.HeaderText = "Criteria"
        Me.ExtendedComboboxColumn2.Name = "ExtendedComboboxColumn2"
        Me.ExtendedComboboxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ExtendedComboboxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.FillWeight = 81.47208!
        Me.DataGridViewTextBoxColumn1.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn1.MinimumWidth = 50
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.HeaderText = "Channel"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'colChannel
        '
        Me.colChannel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colChannel.HeaderText = "Channel"
        Me.colChannel.Name = "colChannel"
        '
        'colSearch
        '
        Me.colSearch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colSearch.FillWeight = 81.47208!
        Me.colSearch.HeaderText = "Search:"
        Me.colSearch.Name = "colSearch"
        Me.colSearch.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colSearch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'colCriteria
        '
        Me.colCriteria.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colCriteria.FillWeight = 50.0!
        Me.colCriteria.HeaderText = "Criteria"
        Me.colCriteria.Name = "colCriteria"
        Me.colCriteria.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colCriteria.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'colValue
        '
        Me.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colValue.FillWeight = 81.47208!
        Me.colValue.HeaderText = "Value"
        Me.colValue.MinimumWidth = 50
        Me.colValue.Name = "colValue"
        '
        'Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(645, 521)
        Me.Controls.Add(Me.grpChannels)
        Me.Controls.Add(Me.grpSearchvalues)
        Me.Controls.Add(Me.lblPeriod)
        Me.Controls.Add(Me.cmdPeriod)
        Me.Controls.Add(Me.cmdCreateAnalysis)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Setup"
        Me.Text = "frmSetup"
        Me.grpSearchvalues.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdSearchValues, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpChannels.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.grdChannels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblPeriod As System.Windows.Forms.Label
    Friend WithEvents cmdPeriod As System.Windows.Forms.Button
    Friend WithEvents cmdDeleteValue As System.Windows.Forms.Button
    Friend WithEvents cmdAddValue As System.Windows.Forms.Button
    Friend WithEvents cmdCreateAnalysis As System.Windows.Forms.Button
    Friend WithEvents cmdCopyTarget As System.Windows.Forms.Button
    Friend WithEvents grpSearchvalues As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grdSearchValues As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSearch As TrinityAnalysis.ExtendedComboboxColumn
    Friend WithEvents colCriteria As TrinityAnalysis.ExtendedComboboxColumn
    Friend WithEvents colValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpChannels As System.Windows.Forms.GroupBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmdAddChannelWizard As System.Windows.Forms.Button
    Friend WithEvents grdChannels As System.Windows.Forms.DataGridView
    Friend WithEvents colChannel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdDeleteChannel As System.Windows.Forms.Button
    Friend WithEvents cmdAddChannel As System.Windows.Forms.Button
    Friend WithEvents ExtendedComboboxColumn1 As TrinityAnalysis.ExtendedComboboxColumn
    Friend WithEvents ExtendedComboboxColumn2 As TrinityAnalysis.ExtendedComboboxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
