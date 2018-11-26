<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGetConfirmations
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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGetConfirmations))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.grdConfBooking = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.cmdApply = New System.Windows.Forms.Button()
        Me.btnPreviewConf = New System.Windows.Forms.Button()
        Me.lnkSelectDeselect = New System.Windows.Forms.LinkLabel()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSelected = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colFromDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colToDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAgencyRef = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCampaignRefNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAvailConf = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.cmdCheckConf = New System.Windows.Forms.Button()
        CType(Me.grdConfBooking,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(731, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'grdConfBooking
        '
        Me.grdConfBooking.AllowUserToAddRows = false
        Me.grdConfBooking.AllowUserToDeleteRows = false
        Me.grdConfBooking.AllowUserToResizeColumns = false
        Me.grdConfBooking.AllowUserToResizeRows = false
        Me.grdConfBooking.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdConfBooking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdConfBooking.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSelected, Me.colFromDate, Me.colToDate, Me.colAgencyRef, Me.colCampaignRefNo, Me.colName, Me.colAvailConf})
        Me.grdConfBooking.Location = New System.Drawing.Point(0, 23)
        Me.grdConfBooking.MultiSelect = false
        Me.grdConfBooking.Name = "grdConfBooking"
        Me.grdConfBooking.RowHeadersVisible = false
        Me.grdConfBooking.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdConfBooking.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdConfBooking.Size = New System.Drawing.Size(731, 270)
        Me.grdConfBooking.TabIndex = 2
        Me.grdConfBooking.VirtualMode = true
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label1.Location = New System.Drawing.Point(631, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "User:"
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = true
        Me.lblUserName.BackColor = System.Drawing.SystemColors.ControlLight
        Me.lblUserName.Location = New System.Drawing.Point(669, 7)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(59, 13)
        Me.lblUserName.TabIndex = 4
        Me.lblUserName.Text = "UserName"
        Me.lblUserName.Visible = false
        '
        'cmdApply
        '
        Me.cmdApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdApply.FlatAppearance.BorderSize = 0
        Me.cmdApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdApply.Location = New System.Drawing.Point(585, 302)
        Me.cmdApply.Name = "cmdApply"
        Me.cmdApply.Size = New System.Drawing.Size(134, 34)
        Me.cmdApply.TabIndex = 27
        Me.cmdApply.Text = "Apply on campaign"
        Me.cmdApply.UseVisualStyleBackColor = true
        '
        'btnPreviewConf
        '
        Me.btnPreviewConf.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnPreviewConf.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnPreviewConf.Enabled = false
        Me.btnPreviewConf.FlatAppearance.BorderSize = 0
        Me.btnPreviewConf.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreviewConf.Image = Global.TV4Online.My.Resources.Resources.preview_2_24x24
        Me.btnPreviewConf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreviewConf.Location = New System.Drawing.Point(179, 302)
        Me.btnPreviewConf.Name = "btnPreviewConf"
        Me.btnPreviewConf.Size = New System.Drawing.Size(157, 34)
        Me.btnPreviewConf.TabIndex = 30
        Me.btnPreviewConf.Text = "Preview confirmations"
        Me.btnPreviewConf.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPreviewConf.UseVisualStyleBackColor = true
        '
        'lnkSelectDeselect
        '
        Me.lnkSelectDeselect.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lnkSelectDeselect.AutoSize = true
        Me.lnkSelectDeselect.Location = New System.Drawing.Point(12, 7)
        Me.lnkSelectDeselect.Name = "lnkSelectDeselect"
        Me.lnkSelectDeselect.Size = New System.Drawing.Size(99, 13)
        Me.lnkSelectDeselect.TabIndex = 54
        Me.lnkSelectDeselect.TabStop = true
        Me.lnkSelectDeselect.Text = "Select/Deselect all"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn1.HeaderText = "From"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = true
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.White
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewTextBoxColumn2.HeaderText = "To"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = true
        Me.DataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.White
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn3.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = true
        Me.DataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.White
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn4.HeaderText = "Avail. conf."
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = true
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.White
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn5.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = true
        Me.DataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colSelected
        '
        Me.colSelected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colSelected.FillWeight = 20!
        Me.colSelected.HeaderText = ""
        Me.colSelected.Name = "colSelected"
        '
        'colFromDate
        '
        Me.colFromDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.colFromDate.DefaultCellStyle = DataGridViewCellStyle1
        Me.colFromDate.HeaderText = "From"
        Me.colFromDate.Name = "colFromDate"
        Me.colFromDate.ReadOnly = true
        Me.colFromDate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colToDate
        '
        Me.colToDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        Me.colToDate.DefaultCellStyle = DataGridViewCellStyle2
        Me.colToDate.HeaderText = "To"
        Me.colToDate.Name = "colToDate"
        Me.colToDate.ReadOnly = true
        Me.colToDate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colAgencyRef
        '
        Me.colAgencyRef.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        Me.colAgencyRef.DefaultCellStyle = DataGridViewCellStyle3
        Me.colAgencyRef.HeaderText = "Agency ref. no"
        Me.colAgencyRef.Name = "colAgencyRef"
        '
        'colCampaignRefNo
        '
        Me.colCampaignRefNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        Me.colCampaignRefNo.DefaultCellStyle = DataGridViewCellStyle4
        Me.colCampaignRefNo.FillWeight = 110!
        Me.colCampaignRefNo.HeaderText = "Campaign ref. no."
        Me.colCampaignRefNo.Name = "colCampaignRefNo"
        Me.colCampaignRefNo.ReadOnly = true
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        Me.colName.DefaultCellStyle = DataGridViewCellStyle5
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        Me.colName.ReadOnly = true
        Me.colName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colAvailConf
        '
        Me.colAvailConf.ActiveLinkColor = System.Drawing.Color.Purple
        Me.colAvailConf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colAvailConf.HeaderText = "Avail. conf."
        Me.colAvailConf.LinkColor = System.Drawing.Color.Black
        Me.colAvailConf.Name = "colAvailConf"
        Me.colAvailConf.ReadOnly = true
        Me.colAvailConf.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colAvailConf.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'cmdCheckConf
        '
        Me.cmdCheckConf.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.cmdCheckConf.FlatAppearance.BorderSize = 0
        Me.cmdCheckConf.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCheckConf.Image = Global.TV4Online.My.Resources.Resources.search_2_20x20
        Me.cmdCheckConf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCheckConf.Location = New System.Drawing.Point(12, 302)
        Me.cmdCheckConf.Name = "cmdCheckConf"
        Me.cmdCheckConf.Size = New System.Drawing.Size(161, 34)
        Me.cmdCheckConf.TabIndex = 31
        Me.cmdCheckConf.Text = "Check for booking conf."
        Me.cmdCheckConf.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdCheckConf.UseVisualStyleBackColor = true
        '
        'frmGetConfirmations
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(731, 348)
        Me.Controls.Add(Me.lnkSelectDeselect)
        Me.Controls.Add(Me.cmdCheckConf)
        Me.Controls.Add(Me.btnPreviewConf)
        Me.Controls.Add(Me.cmdApply)
        Me.Controls.Add(Me.lblUserName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grdConfBooking)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmGetConfirmations"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Get confirmations"
        CType(Me.grdConfBooking,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents DataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents lblUserName As Windows.Forms.Label
    Friend WithEvents grdConfBooking As Windows.Forms.DataGridView
    Friend WithEvents cmdApply As Windows.Forms.Button
    Friend WithEvents btnPreviewConf As Windows.Forms.Button
    Friend WithEvents cmdCheckConf As Windows.Forms.Button
    Friend WithEvents lnkSelectDeselect As Windows.Forms.LinkLabel
    Friend WithEvents DataGridViewTextBoxColumn5 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSelected As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colFromDate As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colToDate As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAgencyRef As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCampaignRefNo As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colName As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAvailConf As Windows.Forms.DataGridViewLinkColumn
End Class
