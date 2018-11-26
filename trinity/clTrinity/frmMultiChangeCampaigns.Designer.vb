<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMultiChangeCampaigns
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grdCampaigns = New System.Windows.Forms.DataGridView()
        Me.colOriginalID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colLastSaved = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpFilter = New System.Windows.Forms.GroupBox()
        Me.cmbProduct = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbChangeDB = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbBy = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbClient = New System.Windows.Forms.ComboBox()
        Me.cmbMonth = New System.Windows.Forms.ComboBox()
        Me.cmbYear = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnExtractNewCampaign = New System.Windows.Forms.Button()
        Me.btnReplaceCampaign = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.tmrKeypress = New System.Windows.Forms.Timer(Me.components)
        Me.btnReloadCampaigns = New System.Windows.Forms.Button()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnMagic = New System.Windows.Forms.Button()
        CType(Me.grdCampaigns,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpFilter.SuspendLayout
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'grdCampaigns
        '
        Me.grdCampaigns.AllowUserToAddRows = false
        Me.grdCampaigns.AllowUserToDeleteRows = false
        Me.grdCampaigns.AllowUserToResizeColumns = false
        Me.grdCampaigns.AllowUserToResizeRows = false
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.grdCampaigns.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdCampaigns.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdCampaigns.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grdCampaigns.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        Me.grdCampaigns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCampaigns.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colOriginalID, Me.colName, Me.colLastSaved})
        Me.grdCampaigns.Location = New System.Drawing.Point(18, 170)
        Me.grdCampaigns.Name = "grdCampaigns"
        Me.grdCampaigns.RowHeadersVisible = false
        Me.grdCampaigns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdCampaigns.Size = New System.Drawing.Size(865, 356)
        Me.grdCampaigns.TabIndex = 22
        Me.grdCampaigns.TabStop = false
        Me.grdCampaigns.VirtualMode = true
        '
        'colOriginalID
        '
        Me.colOriginalID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colOriginalID.FillWeight = 10!
        Me.colOriginalID.HeaderText = "ID"
        Me.colOriginalID.Name = "colOriginalID"
        '
        'colName
        '
        Me.colName.FillWeight = 50!
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        Me.colName.ReadOnly = true
        Me.colName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colLastSaved
        '
        Me.colLastSaved.HeaderText = "Last saved"
        Me.colLastSaved.Name = "colLastSaved"
        '
        'grpFilter
        '
        Me.grpFilter.Controls.Add(Me.cmbProduct)
        Me.grpFilter.Controls.Add(Me.Label2)
        Me.grpFilter.Controls.Add(Me.Label4)
        Me.grpFilter.Controls.Add(Me.cmbChangeDB)
        Me.grpFilter.Controls.Add(Me.Label7)
        Me.grpFilter.Controls.Add(Me.cmbStatus)
        Me.grpFilter.Controls.Add(Me.Label6)
        Me.grpFilter.Controls.Add(Me.cmbBy)
        Me.grpFilter.Controls.Add(Me.Label5)
        Me.grpFilter.Controls.Add(Me.cmbClient)
        Me.grpFilter.Controls.Add(Me.cmbMonth)
        Me.grpFilter.Controls.Add(Me.cmbYear)
        Me.grpFilter.Controls.Add(Me.Label1)
        Me.grpFilter.Controls.Add(Me.Label3)
        Me.grpFilter.Location = New System.Drawing.Point(18, 12)
        Me.grpFilter.Name = "grpFilter"
        Me.grpFilter.Size = New System.Drawing.Size(544, 107)
        Me.grpFilter.TabIndex = 23
        Me.grpFilter.TabStop = false
        Me.grpFilter.Text = "Filter"
        '
        'cmbProduct
        '
        Me.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProduct.FormattingEnabled = true
        Me.cmbProduct.Location = New System.Drawing.Point(53, 75)
        Me.cmbProduct.Name = "cmbProduct"
        Me.cmbProduct.Size = New System.Drawing.Size(121, 21)
        Me.cmbProduct.TabIndex = 43
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(6, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "Product"
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(362, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 42
        Me.Label4.Text = " DB:"
        '
        'cmbChangeDB
        '
        Me.cmbChangeDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChangeDB.FormattingEnabled = true
        Me.cmbChangeDB.Items.AddRange(New Object() {"RegularDB", "BackupDB"})
        Me.cmbChangeDB.Location = New System.Drawing.Point(403, 14)
        Me.cmbChangeDB.Name = "cmbChangeDB"
        Me.cmbChangeDB.Size = New System.Drawing.Size(121, 21)
        Me.cmbChangeDB.TabIndex = 41
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.Location = New System.Drawing.Point(182, 78)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Status"
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = true
        Me.cmbStatus.Location = New System.Drawing.Point(225, 74)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(121, 21)
        Me.cmbStatus.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Location = New System.Drawing.Point(197, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(19, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "By"
        '
        'cmbBy
        '
        Me.cmbBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBy.FormattingEnabled = true
        Me.cmbBy.Location = New System.Drawing.Point(225, 45)
        Me.cmbBy.Name = "cmbBy"
        Me.cmbBy.Size = New System.Drawing.Size(121, 21)
        Me.cmbBy.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(182, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Month"
        '
        'cmbClient
        '
        Me.cmbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClient.FormattingEnabled = true
        Me.cmbClient.Location = New System.Drawing.Point(53, 45)
        Me.cmbClient.Name = "cmbClient"
        Me.cmbClient.Size = New System.Drawing.Size(121, 21)
        Me.cmbClient.TabIndex = 40
        '
        'cmbMonth
        '
        Me.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonth.FormattingEnabled = true
        Me.cmbMonth.Location = New System.Drawing.Point(225, 14)
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(121, 21)
        Me.cmbMonth.TabIndex = 12
        '
        'cmbYear
        '
        Me.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbYear.FormattingEnabled = true
        Me.cmbYear.Location = New System.Drawing.Point(53, 14)
        Me.cmbYear.Name = "cmbYear"
        Me.cmbYear.Size = New System.Drawing.Size(121, 21)
        Me.cmbYear.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(6, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Client"
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(6, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Year"
        '
        'btnExtractNewCampaign
        '
        Me.btnExtractNewCampaign.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnExtractNewCampaign.Enabled = false
        Me.btnExtractNewCampaign.FlatAppearance.BorderSize = 0
        Me.btnExtractNewCampaign.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExtractNewCampaign.Image = Global.clTrinity.My.Resources.Resources.db_2_18x24
        Me.btnExtractNewCampaign.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExtractNewCampaign.Location = New System.Drawing.Point(685, 58)
        Me.btnExtractNewCampaign.Name = "btnExtractNewCampaign"
        Me.btnExtractNewCampaign.Size = New System.Drawing.Size(198, 32)
        Me.btnExtractNewCampaign.TabIndex = 24
        Me.btnExtractNewCampaign.Text = "Extract as new campaign to DB"
        Me.btnExtractNewCampaign.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExtractNewCampaign.UseVisualStyleBackColor = true
        '
        'btnReplaceCampaign
        '
        Me.btnReplaceCampaign.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnReplaceCampaign.Enabled = false
        Me.btnReplaceCampaign.FlatAppearance.BorderSize = 0
        Me.btnReplaceCampaign.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReplaceCampaign.Image = Global.clTrinity.My.Resources.Resources.db_2_18x24
        Me.btnReplaceCampaign.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReplaceCampaign.Location = New System.Drawing.Point(668, 19)
        Me.btnReplaceCampaign.Name = "btnReplaceCampaign"
        Me.btnReplaceCampaign.Size = New System.Drawing.Size(215, 32)
        Me.btnReplaceCampaign.TabIndex = 25
        Me.btnReplaceCampaign.Text = "Replace exicisting campaign to DB"
        Me.btnReplaceCampaign.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnReplaceCampaign.UseVisualStyleBackColor = true
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.search_2_16x16
        Me.PictureBox1.Location = New System.Drawing.Point(18, 137)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 27
        Me.PictureBox1.TabStop = false
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(40, 134)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(332, 22)
        Me.txtSearch.TabIndex = 26
        '
        'tmrKeypress
        '
        Me.tmrKeypress.Enabled = true
        Me.tmrKeypress.Interval = 1000
        '
        'btnReloadCampaigns
        '
        Me.btnReloadCampaigns.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnReloadCampaigns.Enabled = false
        Me.btnReloadCampaigns.FlatAppearance.BorderSize = 0
        Me.btnReloadCampaigns.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReloadCampaigns.Image = Global.clTrinity.My.Resources.Resources.sync_2_small
        Me.btnReloadCampaigns.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnReloadCampaigns.Location = New System.Drawing.Point(757, 98)
        Me.btnReloadCampaigns.Name = "btnReloadCampaigns"
        Me.btnReloadCampaigns.Size = New System.Drawing.Size(126, 32)
        Me.btnReloadCampaigns.TabIndex = 28
        Me.btnReloadCampaigns.Text = "Reload campaigns"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)
        Me.btnReloadCampaigns.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReloadCampaigns.UseVisualStyleBackColor = true
        '
        'lblCount
        '
        Me.lblCount.Location = New System.Drawing.Point(380, 137)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(71, 14)
        Me.lblCount.TabIndex = 29
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.FillWeight = 50!
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = true
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.FillWeight = 15!
        Me.DataGridViewTextBoxColumn2.HeaderText = "Status"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = true
        Me.DataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn2.Width = 131
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.FillWeight = 20!
        Me.DataGridViewTextBoxColumn3.HeaderText = "Planner"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = true
        Me.DataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn3.Width = 174
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.FillWeight = 20!
        Me.DataGridViewTextBoxColumn4.HeaderText = "Buyer"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = true
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn4.Width = 175
        '
        'btnMagic
        '
        Me.btnMagic.FlatAppearance.BorderSize = 0
        Me.btnMagic.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMagic.Location = New System.Drawing.Point(668, 137)
        Me.btnMagic.Name = "btnMagic"
        Me.btnMagic.Size = New System.Drawing.Size(215, 27)
        Me.btnMagic.TabIndex = 30
        Me.btnMagic.Text = "Magic"
        Me.btnMagic.UseVisualStyleBackColor = true
        '
        'frmMultiChangeCampaigns
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(904, 538)
        Me.Controls.Add(Me.btnMagic)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.btnReloadCampaigns)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.btnReplaceCampaign)
        Me.Controls.Add(Me.btnExtractNewCampaign)
        Me.Controls.Add(Me.grpFilter)
        Me.Controls.Add(Me.grdCampaigns)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.MinimumSize = New System.Drawing.Size(920, 576)
        Me.Name = "frmMultiChangeCampaigns"
        Me.Text = "Mulitwindow"
        CType(Me.grdCampaigns,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpFilter.ResumeLayout(false)
        Me.grpFilter.PerformLayout
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents grdCampaigns As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpFilter As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbChangeDB As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbBy As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbClient As System.Windows.Forms.ComboBox
    Friend WithEvents cmbMonth As System.Windows.Forms.ComboBox
    Friend WithEvents cmbYear As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnExtractNewCampaign As System.Windows.Forms.Button
    Friend WithEvents btnReplaceCampaign As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents tmrKeypress As System.Windows.Forms.Timer
    Friend WithEvents btnReloadCampaigns As System.Windows.Forms.Button
    Friend WithEvents cmbProduct As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents colOriginalID As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colName As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLastSaved As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnMagic As Windows.Forms.Button
End Class
