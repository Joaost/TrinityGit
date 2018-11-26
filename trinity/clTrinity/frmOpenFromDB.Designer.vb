<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpenFromDB
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOpenFromDB))
        Me.cmbClient = New System.Windows.Forms.ComboBox()
        Me.cmbProduct = New System.Windows.Forms.ComboBox()
        Me.cmbYear = New System.Windows.Forms.ComboBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbMonth = New System.Windows.Forms.ComboBox()
        Me.grpFilter = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbBy = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tmrKeypress = New System.Windows.Forms.Timer(Me.components)
        Me.lblCount = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.grdCampaigns = New System.Windows.Forms.DataGridView()
        Me.colStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEnd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlanner = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBuyer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSaved = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colLocked = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDelete = New System.Windows.Forms.DataGridViewImageColumn()
        Me.grdRecent = New System.Windows.Forms.DataGridView()
        Me.chkDefault = New System.Windows.Forms.CheckBox()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnMultiChangeCampaigns = New System.Windows.Forms.Button()
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.cmdImport = New System.Windows.Forms.Button()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.colrecentstartdate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colrecentenddate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colrecentname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colrecentstatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRecentSaved = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colrecentplanner = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colrecentbuyer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpFilter.SuspendLayout
        CType(Me.grdCampaigns,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grdRecent,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'cmbClient
        '
        Me.cmbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClient.FormattingEnabled = true
        Me.cmbClient.Location = New System.Drawing.Point(53, 42)
        Me.cmbClient.Name = "cmbClient"
        Me.cmbClient.Size = New System.Drawing.Size(121, 21)
        Me.cmbClient.TabIndex = 40
        '
        'cmbProduct
        '
        Me.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProduct.FormattingEnabled = true
        Me.cmbProduct.Location = New System.Drawing.Point(53, 69)
        Me.cmbProduct.Name = "cmbProduct"
        Me.cmbProduct.Size = New System.Drawing.Size(121, 21)
        Me.cmbProduct.TabIndex = 2
        '
        'cmbYear
        '
        Me.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbYear.FormattingEnabled = true
        Me.cmbYear.Location = New System.Drawing.Point(53, 13)
        Me.cmbYear.Name = "cmbYear"
        Me.cmbYear.Size = New System.Drawing.Size(121, 21)
        Me.cmbYear.TabIndex = 3
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(40, 112)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(318, 22)
        Me.txtSearch.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Client"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(3, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Product"
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(16, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Year"
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(182, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Month"
        '
        'cmbMonth
        '
        Me.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonth.FormattingEnabled = true
        Me.cmbMonth.Location = New System.Drawing.Point(225, 13)
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(121, 21)
        Me.cmbMonth.TabIndex = 12
        '
        'grpFilter
        '
        Me.grpFilter.Controls.Add(Me.Label7)
        Me.grpFilter.Controls.Add(Me.cmbStatus)
        Me.grpFilter.Controls.Add(Me.Label6)
        Me.grpFilter.Controls.Add(Me.cmbBy)
        Me.grpFilter.Controls.Add(Me.cmbProduct)
        Me.grpFilter.Controls.Add(Me.Label5)
        Me.grpFilter.Controls.Add(Me.cmbClient)
        Me.grpFilter.Controls.Add(Me.cmbMonth)
        Me.grpFilter.Controls.Add(Me.cmbYear)
        Me.grpFilter.Controls.Add(Me.Label1)
        Me.grpFilter.Controls.Add(Me.Label3)
        Me.grpFilter.Controls.Add(Me.Label2)
        Me.grpFilter.Location = New System.Drawing.Point(12, 7)
        Me.grpFilter.Name = "grpFilter"
        Me.grpFilter.Size = New System.Drawing.Size(356, 99)
        Me.grpFilter.TabIndex = 14
        Me.grpFilter.TabStop = false
        Me.grpFilter.Text = "Filter"
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.Location = New System.Drawing.Point(182, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Status"
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = true
        Me.cmbStatus.Location = New System.Drawing.Point(225, 69)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(121, 21)
        Me.cmbStatus.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Location = New System.Drawing.Point(197, 45)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(19, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "By"
        '
        'cmbBy
        '
        Me.cmbBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBy.FormattingEnabled = true
        Me.cmbBy.Location = New System.Drawing.Point(225, 42)
        Me.cmbBy.Name = "cmbBy"
        Me.cmbBy.Size = New System.Drawing.Size(121, 21)
        Me.cmbBy.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = true
        Me.Label8.Location = New System.Drawing.Point(688, 118)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(115, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "My recent campaigns"
        '
        'tmrKeypress
        '
        Me.tmrKeypress.Enabled = true
        Me.tmrKeypress.Interval = 1000
        '
        'lblCount
        '
        Me.lblCount.Location = New System.Drawing.Point(367, 115)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(71, 13)
        Me.lblCount.TabIndex = 18
        '
        'btnCancel
        '
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(905, 474)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 28)
        Me.btnCancel.TabIndex = 20
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = true
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
        Me.grdCampaigns.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colStart, Me.colEnd, Me.colName, Me.colStatus, Me.colPlanner, Me.colBuyer, Me.colSaved, Me.colLocked, Me.colDelete})
        Me.grdCampaigns.Location = New System.Drawing.Point(4, 137)
        Me.grdCampaigns.MultiSelect = false
        Me.grdCampaigns.Name = "grdCampaigns"
        Me.grdCampaigns.ReadOnly = true
        Me.grdCampaigns.RowHeadersVisible = false
        Me.grdCampaigns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdCampaigns.Size = New System.Drawing.Size(681, 331)
        Me.grdCampaigns.TabIndex = 21
        Me.grdCampaigns.TabStop = false
        Me.grdCampaigns.VirtualMode = true
        '
        'colStart
        '
        Me.colStart.FillWeight = 20!
        Me.colStart.HeaderText = "Start"
        Me.colStart.Name = "colStart"
        Me.colStart.ReadOnly = true
        Me.colStart.Visible = false
        '
        'colEnd
        '
        Me.colEnd.FillWeight = 20!
        Me.colEnd.HeaderText = "End"
        Me.colEnd.Name = "colEnd"
        Me.colEnd.ReadOnly = true
        Me.colEnd.Visible = false
        '
        'colName
        '
        Me.colName.FillWeight = 50!
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        Me.colName.ReadOnly = true
        Me.colName.Visible = false
        '
        'colStatus
        '
        Me.colStatus.FillWeight = 15!
        Me.colStatus.HeaderText = "Status"
        Me.colStatus.Name = "colStatus"
        Me.colStatus.ReadOnly = true
        Me.colStatus.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colStatus.Visible = false
        '
        'colPlanner
        '
        Me.colPlanner.FillWeight = 20!
        Me.colPlanner.HeaderText = "Planner"
        Me.colPlanner.Name = "colPlanner"
        Me.colPlanner.ReadOnly = true
        Me.colPlanner.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colPlanner.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colPlanner.Visible = false
        '
        'colBuyer
        '
        Me.colBuyer.FillWeight = 20!
        Me.colBuyer.HeaderText = "Buyer"
        Me.colBuyer.Name = "colBuyer"
        Me.colBuyer.ReadOnly = true
        Me.colBuyer.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colBuyer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colBuyer.Visible = false
        '
        'colSaved
        '
        Me.colSaved.FillWeight = 35!
        Me.colSaved.HeaderText = "Saved"
        Me.colSaved.Name = "colSaved"
        Me.colSaved.ReadOnly = true
        Me.colSaved.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colSaved.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colSaved.Visible = false
        '
        'colLocked
        '
        Me.colLocked.FillWeight = 15!
        Me.colLocked.HeaderText = "Locked"
        Me.colLocked.Name = "colLocked"
        Me.colLocked.ReadOnly = true
        Me.colLocked.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colLocked.Visible = false
        '
        'colDelete
        '
        Me.colDelete.FillWeight = 15!
        Me.colDelete.HeaderText = "Delete"
        Me.colDelete.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.colDelete.MinimumWidth = 20
        Me.colDelete.Name = "colDelete"
        Me.colDelete.ReadOnly = true
        Me.colDelete.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'grdRecent
        '
        Me.grdRecent.AllowUserToAddRows = false
        Me.grdRecent.AllowUserToDeleteRows = false
        Me.grdRecent.AllowUserToResizeColumns = false
        Me.grdRecent.AllowUserToResizeRows = false
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.grdRecent.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.grdRecent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdRecent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grdRecent.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        Me.grdRecent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdRecent.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colrecentstartdate, Me.colrecentenddate, Me.colrecentname, Me.colrecentstatus, Me.colRecentSaved, Me.colrecentplanner, Me.colrecentbuyer})
        Me.grdRecent.Location = New System.Drawing.Point(691, 137)
        Me.grdRecent.MultiSelect = false
        Me.grdRecent.Name = "grdRecent"
        Me.grdRecent.ReadOnly = true
        Me.grdRecent.RowHeadersVisible = false
        Me.grdRecent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdRecent.Size = New System.Drawing.Size(362, 331)
        Me.grdRecent.TabIndex = 22
        Me.grdRecent.TabStop = false
        Me.grdRecent.VirtualMode = true
        '
        'chkDefault
        '
        Me.chkDefault.AutoSize = true
        Me.chkDefault.Location = New System.Drawing.Point(543, 474)
        Me.chkDefault.Name = "chkDefault"
        Me.chkDefault.Size = New System.Drawing.Size(142, 17)
        Me.chkDefault.TabIndex = 26
        Me.chkDefault.Text = "Set columns as default"
        Me.chkDefault.UseVisualStyleBackColor = true
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.FillWeight = 20!
        Me.DataGridViewTextBoxColumn1.HeaderText = "Start"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = true
        Me.DataGridViewTextBoxColumn1.Visible = false
        Me.DataGridViewTextBoxColumn1.Width = 105
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.FillWeight = 20!
        Me.DataGridViewTextBoxColumn2.HeaderText = "End"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = true
        Me.DataGridViewTextBoxColumn2.Visible = false
        Me.DataGridViewTextBoxColumn2.Width = 104
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.FillWeight = 50!
        Me.DataGridViewTextBoxColumn3.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = true
        Me.DataGridViewTextBoxColumn3.Visible = false
        Me.DataGridViewTextBoxColumn3.Width = 105
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.FillWeight = 15!
        Me.DataGridViewTextBoxColumn4.HeaderText = "Status"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = true
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn4.Visible = false
        Me.DataGridViewTextBoxColumn4.Width = 104
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.FillWeight = 20!
        Me.DataGridViewTextBoxColumn5.HeaderText = "Planner"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = true
        Me.DataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn5.Visible = false
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.FillWeight = 20!
        Me.DataGridViewTextBoxColumn6.HeaderText = "Last saved"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = true
        Me.DataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn6.Visible = false
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.FillWeight = 30!
        Me.DataGridViewTextBoxColumn7.HeaderText = "Buyer"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = true
        Me.DataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn7.Visible = false
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.FillWeight = 20!
        Me.DataGridViewTextBoxColumn8.HeaderText = "Start"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = true
        Me.DataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewTextBoxColumn8.Visible = false
        Me.DataGridViewTextBoxColumn8.Width = 98
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.FillWeight = 20!
        Me.DataGridViewTextBoxColumn9.HeaderText = "End"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = true
        Me.DataGridViewTextBoxColumn9.Visible = false
        Me.DataGridViewTextBoxColumn9.Width = 98
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.FillWeight = 50!
        Me.DataGridViewTextBoxColumn10.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = true
        Me.DataGridViewTextBoxColumn10.Width = 245
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.FillWeight = 15!
        Me.DataGridViewTextBoxColumn11.HeaderText = "Status"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = true
        Me.DataGridViewTextBoxColumn11.Width = 73
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.FillWeight = 30!
        Me.DataGridViewTextBoxColumn12.HeaderText = "Last saved"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = true
        Me.DataGridViewTextBoxColumn12.Visible = false
        Me.DataGridViewTextBoxColumn12.Width = 73
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.FillWeight = 20!
        Me.DataGridViewTextBoxColumn13.HeaderText = "Planner"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = true
        Me.DataGridViewTextBoxColumn13.Visible = false
        Me.DataGridViewTextBoxColumn13.Width = 73
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.FillWeight = 20!
        Me.DataGridViewTextBoxColumn14.HeaderText = "Buyer"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = true
        Me.DataGridViewTextBoxColumn14.Visible = false
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.FillWeight = 20!
        Me.DataGridViewTextBoxColumn15.HeaderText = "Buyer"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = true
        Me.DataGridViewTextBoxColumn15.Visible = false
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.FillWeight = 20!
        Me.DataGridViewTextBoxColumn16.HeaderText = "Buyer"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = true
        Me.DataGridViewTextBoxColumn16.Visible = false
        '
        'btnMultiChangeCampaigns
        '
        Me.btnMultiChangeCampaigns.FlatAppearance.BorderSize = 0
        Me.btnMultiChangeCampaigns.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMultiChangeCampaigns.Location = New System.Drawing.Point(155, 474)
        Me.btnMultiChangeCampaigns.Name = "btnMultiChangeCampaigns"
        Me.btnMultiChangeCampaigns.Size = New System.Drawing.Size(115, 28)
        Me.btnMultiChangeCampaigns.TabIndex = 27
        Me.btnMultiChangeCampaigns.Text = "Edit campagins"
        Me.btnMultiChangeCampaigns.UseVisualStyleBackColor = true
        Me.btnMultiChangeCampaigns.Visible = false
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.FillWeight = 15!
        Me.DataGridViewImageColumn1.HeaderText = "Delete"
        Me.DataGridViewImageColumn1.Image = CType(resources.GetObject("DataGridViewImageColumn1.Image"),System.Drawing.Image)
        Me.DataGridViewImageColumn1.MinimumWidth = 20
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.ReadOnly = true
        Me.DataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewImageColumn1.Width = 523
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.search_2_16x16
        Me.PictureBox1.Location = New System.Drawing.Point(18, 115)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 25
        Me.PictureBox1.TabStop = false
        '
        'cmdImport
        '
        Me.cmdImport.FlatAppearance.BorderSize = 0
        Me.cmdImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdImport.Image = Global.clTrinity.My.Resources.Resources.db_2_16x16
        Me.cmdImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdImport.Location = New System.Drawing.Point(4, 474)
        Me.cmdImport.Name = "cmdImport"
        Me.cmdImport.Size = New System.Drawing.Size(145, 28)
        Me.cmdImport.TabIndex = 24
        Me.cmdImport.Text = "Import campaign files"
        Me.cmdImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdImport.UseVisualStyleBackColor = true
        '
        'btnOpen
        '
        Me.btnOpen.FlatAppearance.BorderSize = 0
        Me.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpen.Image = Global.clTrinity.My.Resources.Resources.open_2
        Me.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOpen.Location = New System.Drawing.Point(983, 474)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(70, 28)
        Me.btnOpen.TabIndex = 23
        Me.btnOpen.Text = "Open"
        Me.btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnOpen.UseVisualStyleBackColor = true
        '
        'colrecentstartdate
        '
        Me.colrecentstartdate.FillWeight = 20!
        Me.colrecentstartdate.HeaderText = "Start"
        Me.colrecentstartdate.Name = "colrecentstartdate"
        Me.colrecentstartdate.ReadOnly = true
        Me.colrecentstartdate.Visible = false
        '
        'colrecentenddate
        '
        Me.colrecentenddate.FillWeight = 20!
        Me.colrecentenddate.HeaderText = "End"
        Me.colrecentenddate.Name = "colrecentenddate"
        Me.colrecentenddate.ReadOnly = true
        Me.colrecentenddate.Visible = false
        '
        'colrecentname
        '
        Me.colrecentname.FillWeight = 50!
        Me.colrecentname.HeaderText = "Name"
        Me.colrecentname.Name = "colrecentname"
        Me.colrecentname.ReadOnly = true
        '
        'colrecentstatus
        '
        Me.colrecentstatus.FillWeight = 15!
        Me.colrecentstatus.HeaderText = "Status"
        Me.colrecentstatus.Name = "colrecentstatus"
        Me.colrecentstatus.ReadOnly = true
        Me.colrecentstatus.Visible = false
        '
        'colRecentSaved
        '
        Me.colRecentSaved.FillWeight = 30!
        Me.colRecentSaved.HeaderText = "Saved"
        Me.colRecentSaved.Name = "colRecentSaved"
        Me.colRecentSaved.ReadOnly = true
        '
        'colrecentplanner
        '
        Me.colrecentplanner.FillWeight = 20!
        Me.colrecentplanner.HeaderText = "Planner"
        Me.colrecentplanner.Name = "colrecentplanner"
        Me.colrecentplanner.ReadOnly = true
        Me.colrecentplanner.Visible = false
        '
        'colrecentbuyer
        '
        Me.colrecentbuyer.FillWeight = 20!
        Me.colrecentbuyer.HeaderText = "Buyer"
        Me.colrecentbuyer.Name = "colrecentbuyer"
        Me.colrecentbuyer.ReadOnly = true
        Me.colrecentbuyer.Visible = false
        '
        'frmOpenFromDB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1065, 513)
        Me.Controls.Add(Me.btnMultiChangeCampaigns)
        Me.Controls.Add(Me.chkDefault)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.cmdImport)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.grdRecent)
        Me.Controls.Add(Me.grdCampaigns)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.grpFilter)
        Me.Controls.Add(Me.txtSearch)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmOpenFromDB"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Open from Database"
        Me.grpFilter.ResumeLayout(false)
        Me.grpFilter.PerformLayout
        CType(Me.grdCampaigns,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdRecent,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents cmbClient As System.Windows.Forms.ComboBox
    Friend WithEvents cmbProduct As System.Windows.Forms.ComboBox
    Friend WithEvents cmbYear As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbMonth As System.Windows.Forms.ComboBox
    Friend WithEvents grpFilter As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbBy As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tmrKeypress As System.Windows.Forms.Timer
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents grdCampaigns As System.Windows.Forms.DataGridView
    Friend WithEvents grdRecent As System.Windows.Forms.DataGridView
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents cmdImport As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
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
    Friend WithEvents chkDefault As System.Windows.Forms.CheckBox
    Friend WithEvents btnMultiChangeCampaigns As System.Windows.Forms.Button
    Friend WithEvents DataGridViewImageColumn1 As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents colStart As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEnd As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colName As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStatus As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanner As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBuyer As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSaved As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLocked As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDelete As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents colrecentstartdate As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colrecentenddate As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colrecentname As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colrecentstatus As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRecentSaved As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colrecentplanner As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colrecentbuyer As Windows.Forms.DataGridViewTextBoxColumn
End Class
