<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLinkCampaign
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLinkCampaign))
        Me.chkAuto = New System.Windows.Forms.CheckBox()
        Me.pnlOptions = New System.Windows.Forms.Panel()
        Me.cmdAddNow = New System.Windows.Forms.Button()
        Me.optSameProd = New System.Windows.Forms.RadioButton()
        Me.optAll = New System.Windows.Forms.RadioButton()
        Me.optSameClient = New System.Windows.Forms.RadioButton()
        Me.grdFileLinks = New System.Windows.Forms.DataGridView()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPath = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colLink = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdFindInDB = New System.Windows.Forms.Button()
        Me.cmdDeleteLink = New System.Windows.Forms.Button()
        Me.cmdAddLink = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pnlOptions.SuspendLayout
        CType(Me.grdFileLinks,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'chkAuto
        '
        Me.chkAuto.AutoSize = true
        Me.chkAuto.Location = New System.Drawing.Point(12, 50)
        Me.chkAuto.Name = "chkAuto"
        Me.chkAuto.Size = New System.Drawing.Size(361, 17)
        Me.chkAuto.TabIndex = 1
        Me.chkAuto.Text = "Automatically add campaigns in the same folder as this campaign"
        Me.chkAuto.UseVisualStyleBackColor = true
        '
        'pnlOptions
        '
        Me.pnlOptions.Controls.Add(Me.cmdAddNow)
        Me.pnlOptions.Controls.Add(Me.optSameProd)
        Me.pnlOptions.Controls.Add(Me.optAll)
        Me.pnlOptions.Controls.Add(Me.optSameClient)
        Me.pnlOptions.Enabled = false
        Me.pnlOptions.Location = New System.Drawing.Point(31, 72)
        Me.pnlOptions.Name = "pnlOptions"
        Me.pnlOptions.Size = New System.Drawing.Size(164, 97)
        Me.pnlOptions.TabIndex = 2
        '
        'cmdAddNow
        '
        Me.cmdAddNow.FlatAppearance.BorderSize = 0
        Me.cmdAddNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddNow.Location = New System.Drawing.Point(3, 70)
        Me.cmdAddNow.Name = "cmdAddNow"
        Me.cmdAddNow.Size = New System.Drawing.Size(62, 21)
        Me.cmdAddNow.TabIndex = 13
        Me.cmdAddNow.Text = "Add now"
        Me.cmdAddNow.UseVisualStyleBackColor = true
        '
        'optSameProd
        '
        Me.optSameProd.AutoSize = true
        Me.optSameProd.Location = New System.Drawing.Point(3, 47)
        Me.optSameProd.Name = "optSameProd"
        Me.optSameProd.Size = New System.Drawing.Size(149, 17)
        Me.optSameProd.TabIndex = 5
        Me.optSameProd.Text = "Only from same product"
        Me.optSameProd.UseVisualStyleBackColor = true
        '
        'optAll
        '
        Me.optAll.AutoSize = true
        Me.optAll.Checked = true
        Me.optAll.Location = New System.Drawing.Point(3, 3)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(119, 17)
        Me.optAll.TabIndex = 3
        Me.optAll.TabStop = true
        Me.optAll.Text = "Add all campaigns"
        Me.optAll.UseVisualStyleBackColor = true
        '
        'optSameClient
        '
        Me.optSameClient.AutoSize = true
        Me.optSameClient.Location = New System.Drawing.Point(3, 25)
        Me.optSameClient.Name = "optSameClient"
        Me.optSameClient.Size = New System.Drawing.Size(136, 17)
        Me.optSameClient.TabIndex = 4
        Me.optSameClient.Text = "Only from same client"
        Me.optSameClient.UseVisualStyleBackColor = true
        '
        'grdFileLinks
        '
        Me.grdFileLinks.AllowUserToAddRows = false
        Me.grdFileLinks.AllowUserToDeleteRows = false
        Me.grdFileLinks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFileLinks.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colPath, Me.colLink})
        Me.grdFileLinks.Location = New System.Drawing.Point(12, 175)
        Me.grdFileLinks.Name = "grdFileLinks"
        Me.grdFileLinks.RowHeadersVisible = false
        Me.grdFileLinks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdFileLinks.Size = New System.Drawing.Size(719, 247)
        Me.grdFileLinks.TabIndex = 3
        Me.grdFileLinks.VirtualMode = true
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colName.FillWeight = 40!
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        Me.colName.ReadOnly = true
        '
        'colPath
        '
        Me.colPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colPath.FillWeight = 60!
        Me.colPath.HeaderText = "File path"
        Me.colPath.Name = "colPath"
        Me.colPath.ReadOnly = true
        '
        'colLink
        '
        Me.colLink.HeaderText = "Link"
        Me.colLink.Name = "colLink"
        Me.colLink.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colLink.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colLink.Width = 40
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.FillWeight = 40!
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = true
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.FillWeight = 60!
        Me.DataGridViewTextBoxColumn2.HeaderText = "File path"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = true
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Link"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 40
        '
        'cmdFindInDB
        '
        Me.cmdFindInDB.FlatAppearance.BorderSize = 0
        Me.cmdFindInDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFindInDB.Image = Global.clTrinity.My.Resources.Resources.search_2_16x16
        Me.cmdFindInDB.Location = New System.Drawing.Point(737, 227)
        Me.cmdFindInDB.Name = "cmdFindInDB"
        Me.cmdFindInDB.Size = New System.Drawing.Size(22, 20)
        Me.cmdFindInDB.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.cmdFindInDB, "Remove link to campaign")
        Me.cmdFindInDB.UseVisualStyleBackColor = true
        Me.cmdFindInDB.Visible = false
        '
        'cmdDeleteLink
        '
        Me.cmdDeleteLink.FlatAppearance.BorderSize = 0
        Me.cmdDeleteLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDeleteLink.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdDeleteLink.Location = New System.Drawing.Point(737, 201)
        Me.cmdDeleteLink.Name = "cmdDeleteLink"
        Me.cmdDeleteLink.Size = New System.Drawing.Size(22, 20)
        Me.cmdDeleteLink.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.cmdDeleteLink, "Remove link to campaign")
        Me.cmdDeleteLink.UseVisualStyleBackColor = true
        '
        'cmdAddLink
        '
        Me.cmdAddLink.FlatAppearance.BorderSize = 0
        Me.cmdAddLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddLink.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.cmdAddLink.Location = New System.Drawing.Point(737, 175)
        Me.cmdAddLink.Name = "cmdAddLink"
        Me.cmdAddLink.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddLink.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.cmdAddLink, "Manually add campaign")
        Me.cmdAddLink.UseVisualStyleBackColor = true
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.conect_2_29x32
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(29, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = false
        '
        'frmLinkCampaign
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(771, 434)
        Me.Controls.Add(Me.cmdFindInDB)
        Me.Controls.Add(Me.cmdDeleteLink)
        Me.Controls.Add(Me.cmdAddLink)
        Me.Controls.Add(Me.grdFileLinks)
        Me.Controls.Add(Me.pnlOptions)
        Me.Controls.Add(Me.chkAuto)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmLinkCampaign"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Link campaign"
        Me.pnlOptions.ResumeLayout(false)
        Me.pnlOptions.PerformLayout
        CType(Me.grdFileLinks,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chkAuto As System.Windows.Forms.CheckBox
    Friend WithEvents pnlOptions As System.Windows.Forms.Panel
    Friend WithEvents optSameProd As System.Windows.Forms.RadioButton
    Friend WithEvents optAll As System.Windows.Forms.RadioButton
    Friend WithEvents optSameClient As System.Windows.Forms.RadioButton
    Friend WithEvents grdFileLinks As System.Windows.Forms.DataGridView
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmdAddLink As System.Windows.Forms.Button
    Friend WithEvents cmdAddNow As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdDeleteLink As System.Windows.Forms.Button
    Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPath As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLink As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cmdFindInDB As System.Windows.Forms.Button
End Class
