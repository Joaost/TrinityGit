<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportCampaigns
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImportCampaigns))
        Me.cmdPrevious = New System.Windows.Forms.Button()
        Me.cmdNext = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.pnlViewport = New System.Windows.Forms.Panel()
        Me.pnlMultipleFiles = New System.Windows.Forms.Panel()
        Me.dtDate = New clTrinity.ExtendedDateTimePicker()
        Me.chkDate = New System.Windows.Forms.CheckBox()
        Me.txtExcludeFolders = New System.Windows.Forms.TextBox()
        Me.chkExcludeFolders = New System.Windows.Forms.CheckBox()
        Me.pbFiles = New System.Windows.Forms.ProgressBar()
        Me.chkOnlyWithRatings = New System.Windows.Forms.CheckBox()
        Me.chkOnlyWithBudget = New System.Windows.Forms.CheckBox()
        Me.chkOnlyWhereIAmPlannerOrBuyer = New System.Windows.Forms.CheckBox()
        Me.chkDoNotSearchSubFolders = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmdBrowseFolder = New System.Windows.Forms.Button()
        Me.txtStartFolder = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlChooseFiles = New System.Windows.Forms.Panel()
        Me.grdChooseFiles = New System.Windows.Forms.DataGridView()
        Me.colImport = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPath = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAction = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.pbImportFiles = New System.Windows.Forms.ProgressBar()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblDuplicate = New System.Windows.Forms.Label()
        Me.pnlSingleFile = New System.Windows.Forms.Panel()
        Me.cmdBrowseSingleFile = New System.Windows.Forms.Button()
        Me.txtSingleFileName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlWelcome = New System.Windows.Forms.Panel()
        Me.optMultipleFiles = New System.Windows.Forms.RadioButton()
        Me.optSingleFile = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pnlViewport.SuspendLayout
        Me.pnlMultipleFiles.SuspendLayout
        Me.pnlChooseFiles.SuspendLayout
        CType(Me.grdChooseFiles,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlSingleFile.SuspendLayout
        Me.pnlWelcome.SuspendLayout
        CType(Me.PictureBox3,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox4,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'cmdPrevious
        '
        Me.cmdPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdPrevious.Enabled = false
        Me.cmdPrevious.FlatAppearance.BorderSize = 0
        Me.cmdPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPrevious.Location = New System.Drawing.Point(262, 283)
        Me.cmdPrevious.Name = "cmdPrevious"
        Me.cmdPrevious.Size = New System.Drawing.Size(75, 21)
        Me.cmdPrevious.TabIndex = 0
        Me.cmdPrevious.Text = "< Previous"
        Me.cmdPrevious.UseVisualStyleBackColor = true
        '
        'cmdNext
        '
        Me.cmdNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdNext.FlatAppearance.BorderSize = 0
        Me.cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdNext.Location = New System.Drawing.Point(343, 283)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.Size = New System.Drawing.Size(75, 21)
        Me.cmdNext.TabIndex = 1
        Me.cmdNext.Text = "Next >"
        Me.cmdNext.UseVisualStyleBackColor = true
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Location = New System.Drawing.Point(424, 283)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 21)
        Me.cmdCancel.TabIndex = 2
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = true
        '
        'pnlViewport
        '
        Me.pnlViewport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pnlViewport.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.pnlViewport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlViewport.Controls.Add(Me.pnlMultipleFiles)
        Me.pnlViewport.Controls.Add(Me.pnlChooseFiles)
        Me.pnlViewport.Controls.Add(Me.pnlSingleFile)
        Me.pnlViewport.Controls.Add(Me.pnlWelcome)
        Me.pnlViewport.Location = New System.Drawing.Point(-1, -2)
        Me.pnlViewport.Name = "pnlViewport"
        Me.pnlViewport.Size = New System.Drawing.Size(512, 279)
        Me.pnlViewport.TabIndex = 3
        '
        'pnlMultipleFiles
        '
        Me.pnlMultipleFiles.Controls.Add(Me.dtDate)
        Me.pnlMultipleFiles.Controls.Add(Me.chkDate)
        Me.pnlMultipleFiles.Controls.Add(Me.txtExcludeFolders)
        Me.pnlMultipleFiles.Controls.Add(Me.chkExcludeFolders)
        Me.pnlMultipleFiles.Controls.Add(Me.pbFiles)
        Me.pnlMultipleFiles.Controls.Add(Me.chkOnlyWithRatings)
        Me.pnlMultipleFiles.Controls.Add(Me.chkOnlyWithBudget)
        Me.pnlMultipleFiles.Controls.Add(Me.chkOnlyWhereIAmPlannerOrBuyer)
        Me.pnlMultipleFiles.Controls.Add(Me.chkDoNotSearchSubFolders)
        Me.pnlMultipleFiles.Controls.Add(Me.Label5)
        Me.pnlMultipleFiles.Controls.Add(Me.Label7)
        Me.pnlMultipleFiles.Controls.Add(Me.cmdBrowseFolder)
        Me.pnlMultipleFiles.Controls.Add(Me.txtStartFolder)
        Me.pnlMultipleFiles.Controls.Add(Me.PictureBox3)
        Me.pnlMultipleFiles.Controls.Add(Me.Label6)
        Me.pnlMultipleFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMultipleFiles.Location = New System.Drawing.Point(0, 0)
        Me.pnlMultipleFiles.Name = "pnlMultipleFiles"
        Me.pnlMultipleFiles.Size = New System.Drawing.Size(508, 275)
        Me.pnlMultipleFiles.TabIndex = 2
        Me.pnlMultipleFiles.Visible = false
        '
        'dtDate
        '
        Me.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDate.Location = New System.Drawing.Point(197, 219)
        Me.dtDate.Name = "dtDate"
        Me.dtDate.ShowWeekNumbers = true
        Me.dtDate.Size = New System.Drawing.Size(85, 22)
        Me.dtDate.TabIndex = 17
        Me.dtDate.Value = New Date(2011, 1, 1, 0, 0, 0, 0)
        '
        'chkDate
        '
        Me.chkDate.AutoSize = true
        Me.chkDate.Checked = true
        Me.chkDate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDate.Location = New System.Drawing.Point(11, 221)
        Me.chkDate.Name = "chkDate"
        Me.chkDate.Size = New System.Drawing.Size(321, 17)
        Me.chkDate.TabIndex = 16
        Me.chkDate.Text = "Only import campaigns starting                                or later"
        Me.chkDate.UseVisualStyleBackColor = true
        '
        'txtExcludeFolders
        '
        Me.txtExcludeFolders.Location = New System.Drawing.Point(214, 191)
        Me.txtExcludeFolders.Name = "txtExcludeFolders"
        Me.txtExcludeFolders.Size = New System.Drawing.Size(284, 22)
        Me.txtExcludeFolders.TabIndex = 15
        Me.txtExcludeFolders.Text = "slask"
        '
        'chkExcludeFolders
        '
        Me.chkExcludeFolders.AutoSize = true
        Me.chkExcludeFolders.Checked = true
        Me.chkExcludeFolders.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkExcludeFolders.Location = New System.Drawing.Point(11, 193)
        Me.chkExcludeFolders.Name = "chkExcludeFolders"
        Me.chkExcludeFolders.Size = New System.Drawing.Size(209, 17)
        Me.chkExcludeFolders.TabIndex = 14
        Me.chkExcludeFolders.Text = "Do not import files in these folders:"
        Me.chkExcludeFolders.UseVisualStyleBackColor = true
        '
        'pbFiles
        '
        Me.pbFiles.Location = New System.Drawing.Point(11, 245)
        Me.pbFiles.Name = "pbFiles"
        Me.pbFiles.Size = New System.Drawing.Size(487, 21)
        Me.pbFiles.TabIndex = 13
        Me.pbFiles.Visible = false
        '
        'chkOnlyWithRatings
        '
        Me.chkOnlyWithRatings.AutoSize = true
        Me.chkOnlyWithRatings.Checked = true
        Me.chkOnlyWithRatings.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOnlyWithRatings.Location = New System.Drawing.Point(11, 167)
        Me.chkOnlyWithRatings.Name = "chkOnlyWithRatings"
        Me.chkOnlyWithRatings.Size = New System.Drawing.Size(268, 17)
        Me.chkOnlyWithRatings.TabIndex = 12
        Me.chkOnlyWithRatings.Text = "Only find finished campaigns with actual spots"
        Me.chkOnlyWithRatings.UseVisualStyleBackColor = true
        '
        'chkOnlyWithBudget
        '
        Me.chkOnlyWithBudget.AutoSize = true
        Me.chkOnlyWithBudget.Checked = true
        Me.chkOnlyWithBudget.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOnlyWithBudget.Location = New System.Drawing.Point(11, 143)
        Me.chkOnlyWithBudget.Name = "chkOnlyWithBudget"
        Me.chkOnlyWithBudget.Size = New System.Drawing.Size(208, 17)
        Me.chkOnlyWithBudget.TabIndex = 11
        Me.chkOnlyWithBudget.Text = "Only find campaigns with a budget"
        Me.chkOnlyWithBudget.UseVisualStyleBackColor = true
        '
        'chkOnlyWhereIAmPlannerOrBuyer
        '
        Me.chkOnlyWhereIAmPlannerOrBuyer.AutoSize = true
        Me.chkOnlyWhereIAmPlannerOrBuyer.Checked = true
        Me.chkOnlyWhereIAmPlannerOrBuyer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOnlyWhereIAmPlannerOrBuyer.Location = New System.Drawing.Point(11, 120)
        Me.chkOnlyWhereIAmPlannerOrBuyer.Name = "chkOnlyWhereIAmPlannerOrBuyer"
        Me.chkOnlyWhereIAmPlannerOrBuyer.Size = New System.Drawing.Size(324, 17)
        Me.chkOnlyWhereIAmPlannerOrBuyer.TabIndex = 10
        Me.chkOnlyWhereIAmPlannerOrBuyer.Text = "Only find campaigns where I am either 'Planner' or 'Buyer'"
        Me.chkOnlyWhereIAmPlannerOrBuyer.UseVisualStyleBackColor = true
        '
        'chkDoNotSearchSubFolders
        '
        Me.chkDoNotSearchSubFolders.AutoSize = true
        Me.chkDoNotSearchSubFolders.Location = New System.Drawing.Point(11, 96)
        Me.chkDoNotSearchSubFolders.Name = "chkDoNotSearchSubFolders"
        Me.chkDoNotSearchSubFolders.Size = New System.Drawing.Size(160, 17)
        Me.chkDoNotSearchSubFolders.TabIndex = 9
        Me.chkDoNotSearchSubFolders.Text = "Do not search sub-folders"
        Me.chkDoNotSearchSubFolders.UseVisualStyleBackColor = true
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(12, 53)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Folder"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(12, 37)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(360, 16)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Select the folder to start searching for campaign files in."
        '
        'cmdBrowseFolder
        '
        Me.cmdBrowseFolder.FlatAppearance.BorderSize = 0
        Me.cmdBrowseFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdBrowseFolder.Location = New System.Drawing.Point(244, 68)
        Me.cmdBrowseFolder.Name = "cmdBrowseFolder"
        Me.cmdBrowseFolder.Size = New System.Drawing.Size(75, 21)
        Me.cmdBrowseFolder.TabIndex = 6
        Me.cmdBrowseFolder.Text = "Browse"
        Me.cmdBrowseFolder.UseVisualStyleBackColor = true
        '
        'txtStartFolder
        '
        Me.txtStartFolder.Location = New System.Drawing.Point(11, 69)
        Me.txtStartFolder.Name = "txtStartFolder"
        Me.txtStartFolder.Size = New System.Drawing.Size(227, 22)
        Me.txtStartFolder.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label6.Location = New System.Drawing.Point(11, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 19)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Select folder"
        '
        'pnlChooseFiles
        '
        Me.pnlChooseFiles.Controls.Add(Me.grdChooseFiles)
        Me.pnlChooseFiles.Controls.Add(Me.pbImportFiles)
        Me.pnlChooseFiles.Controls.Add(Me.Label9)
        Me.pnlChooseFiles.Controls.Add(Me.PictureBox4)
        Me.pnlChooseFiles.Controls.Add(Me.Label10)
        Me.pnlChooseFiles.Controls.Add(Me.lblDuplicate)
        Me.pnlChooseFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlChooseFiles.Location = New System.Drawing.Point(0, 0)
        Me.pnlChooseFiles.Name = "pnlChooseFiles"
        Me.pnlChooseFiles.Size = New System.Drawing.Size(508, 275)
        Me.pnlChooseFiles.TabIndex = 3
        Me.pnlChooseFiles.Visible = false
        '
        'grdChooseFiles
        '
        Me.grdChooseFiles.AllowUserToAddRows = false
        Me.grdChooseFiles.AllowUserToDeleteRows = false
        Me.grdChooseFiles.AllowUserToResizeColumns = false
        Me.grdChooseFiles.AllowUserToResizeRows = false
        Me.grdChooseFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChooseFiles.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colImport, Me.colName, Me.colPath, Me.colAction})
        Me.grdChooseFiles.Location = New System.Drawing.Point(11, 53)
        Me.grdChooseFiles.Name = "grdChooseFiles"
        Me.grdChooseFiles.RowHeadersVisible = false
        Me.grdChooseFiles.Size = New System.Drawing.Size(487, 175)
        Me.grdChooseFiles.TabIndex = 16
        '
        'colImport
        '
        Me.colImport.HeaderText = ""
        Me.colImport.Name = "colImport"
        Me.colImport.Width = 20
        '
        'colName
        '
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        Me.colName.Width = 200
        '
        'colPath
        '
        Me.colPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colPath.HeaderText = "Path"
        Me.colPath.Name = "colPath"
        Me.colPath.ReadOnly = true
        '
        'colAction
        '
        Me.colAction.HeaderText = "Action"
        Me.colAction.Items.AddRange(New Object() {"Do nothing", "Set 'Cancelled'", "Rename"})
        Me.colAction.Name = "colAction"
        '
        'pbImportFiles
        '
        Me.pbImportFiles.Location = New System.Drawing.Point(11, 233)
        Me.pbImportFiles.Name = "pbImportFiles"
        Me.pbImportFiles.Size = New System.Drawing.Size(487, 21)
        Me.pbImportFiles.TabIndex = 13
        Me.pbImportFiles.Visible = false
        '
        'Label9
        '
        Me.Label9.AutoSize = true
        Me.Label9.Location = New System.Drawing.Point(12, 37)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(132, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Select the files to import"
        '
        'Label10
        '
        Me.Label10.AutoSize = true
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label10.Location = New System.Drawing.Point(11, 8)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(91, 19)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Select files"
        '
        'lblDuplicate
        '
        Me.lblDuplicate.AutoSize = true
        Me.lblDuplicate.ForeColor = System.Drawing.Color.Red
        Me.lblDuplicate.Location = New System.Drawing.Point(12, 237)
        Me.lblDuplicate.Name = "lblDuplicate"
        Me.lblDuplicate.Size = New System.Drawing.Size(214, 13)
        Me.lblDuplicate.TabIndex = 17
        Me.lblDuplicate.Text = "There are duplicate campaigns in the list"
        Me.lblDuplicate.Visible = false
        '
        'pnlSingleFile
        '
        Me.pnlSingleFile.Controls.Add(Me.cmdBrowseSingleFile)
        Me.pnlSingleFile.Controls.Add(Me.txtSingleFileName)
        Me.pnlSingleFile.Controls.Add(Me.PictureBox2)
        Me.pnlSingleFile.Controls.Add(Me.Label3)
        Me.pnlSingleFile.Controls.Add(Me.Label4)
        Me.pnlSingleFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSingleFile.Location = New System.Drawing.Point(0, 0)
        Me.pnlSingleFile.Name = "pnlSingleFile"
        Me.pnlSingleFile.Size = New System.Drawing.Size(508, 275)
        Me.pnlSingleFile.TabIndex = 1
        Me.pnlSingleFile.Visible = false
        '
        'cmdBrowseSingleFile
        '
        Me.cmdBrowseSingleFile.Location = New System.Drawing.Point(248, 124)
        Me.cmdBrowseSingleFile.Name = "cmdBrowseSingleFile"
        Me.cmdBrowseSingleFile.Size = New System.Drawing.Size(75, 21)
        Me.cmdBrowseSingleFile.TabIndex = 6
        Me.cmdBrowseSingleFile.Text = "Browse"
        Me.cmdBrowseSingleFile.UseVisualStyleBackColor = true
        '
        'txtSingleFileName
        '
        Me.txtSingleFileName.Location = New System.Drawing.Point(15, 125)
        Me.txtSingleFileName.Name = "txtSingleFileName"
        Me.txtSingleFileName.Size = New System.Drawing.Size(227, 22)
        Me.txtSingleFileName.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(12, 111)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "File"
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label4.Location = New System.Drawing.Point(11, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 19)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Choose file"
        '
        'pnlWelcome
        '
        Me.pnlWelcome.Controls.Add(Me.PictureBox1)
        Me.pnlWelcome.Controls.Add(Me.optMultipleFiles)
        Me.pnlWelcome.Controls.Add(Me.optSingleFile)
        Me.pnlWelcome.Controls.Add(Me.Label2)
        Me.pnlWelcome.Controls.Add(Me.Label1)
        Me.pnlWelcome.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWelcome.Location = New System.Drawing.Point(0, 0)
        Me.pnlWelcome.Name = "pnlWelcome"
        Me.pnlWelcome.Size = New System.Drawing.Size(508, 275)
        Me.pnlWelcome.TabIndex = 0
        '
        'optMultipleFiles
        '
        Me.optMultipleFiles.AutoSize = true
        Me.optMultipleFiles.Location = New System.Drawing.Point(23, 149)
        Me.optMultipleFiles.Name = "optMultipleFiles"
        Me.optMultipleFiles.Size = New System.Drawing.Size(128, 17)
        Me.optMultipleFiles.TabIndex = 3
        Me.optMultipleFiles.Text = "Import multiple files"
        Me.optMultipleFiles.UseVisualStyleBackColor = true
        '
        'optSingleFile
        '
        Me.optSingleFile.AutoSize = true
        Me.optSingleFile.Checked = true
        Me.optSingleFile.Location = New System.Drawing.Point(23, 114)
        Me.optSingleFile.Name = "optSingleFile"
        Me.optSingleFile.Size = New System.Drawing.Size(121, 17)
        Me.optSingleFile.TabIndex = 2
        Me.optSingleFile.TabStop = true
        Me.optSingleFile.Text = "Import a single file"
        Me.optSingleFile.UseVisualStyleBackColor = true
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(405, 36)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "This wizard will help you import Trinity campaigns from old .cmp-files into the n"& _ 
    "ew database."
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(172, 19)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Import campaign files"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 200
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.HeaderText = "Path"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = true
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.clTrinity.My.Resources.Resources.db_2_24x32
        Me.PictureBox3.Location = New System.Drawing.Point(466, 11)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(32, 34)
        Me.PictureBox3.TabIndex = 4
        Me.PictureBox3.TabStop = false
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.clTrinity.My.Resources.Resources.data_into1
        Me.PictureBox4.Location = New System.Drawing.Point(466, 11)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(32, 34)
        Me.PictureBox4.TabIndex = 4
        Me.PictureBox4.TabStop = false
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.clTrinity.My.Resources.Resources.data_into1
        Me.PictureBox2.Location = New System.Drawing.Point(466, 11)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(32, 34)
        Me.PictureBox2.TabIndex = 4
        Me.PictureBox2.TabStop = false
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.data_into1
        Me.PictureBox1.Location = New System.Drawing.Point(466, 11)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 34)
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = false
        '
        'frmImportCampaigns
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(511, 315)
        Me.Controls.Add(Me.pnlViewport)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdNext)
        Me.Controls.Add(Me.cmdPrevious)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmImportCampaigns"
        Me.Text = "Import campaign files"
        Me.pnlViewport.ResumeLayout(false)
        Me.pnlMultipleFiles.ResumeLayout(false)
        Me.pnlMultipleFiles.PerformLayout
        Me.pnlChooseFiles.ResumeLayout(false)
        Me.pnlChooseFiles.PerformLayout
        CType(Me.grdChooseFiles,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlSingleFile.ResumeLayout(false)
        Me.pnlSingleFile.PerformLayout
        Me.pnlWelcome.ResumeLayout(false)
        Me.pnlWelcome.PerformLayout
        CType(Me.PictureBox3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents cmdPrevious As System.Windows.Forms.Button
    Friend WithEvents cmdNext As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents pnlViewport As System.Windows.Forms.Panel
    Friend WithEvents pnlWelcome As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents optMultipleFiles As System.Windows.Forms.RadioButton
    Friend WithEvents optSingleFile As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlSingleFile As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdBrowseSingleFile As System.Windows.Forms.Button
    Friend WithEvents txtSingleFileName As System.Windows.Forms.TextBox
    Friend WithEvents pnlMultipleFiles As System.Windows.Forms.Panel
    Friend WithEvents chkDoNotSearchSubFolders As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmdBrowseFolder As System.Windows.Forms.Button
    Friend WithEvents txtStartFolder As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkOnlyWhereIAmPlannerOrBuyer As System.Windows.Forms.CheckBox
    Friend WithEvents chkOnlyWithRatings As System.Windows.Forms.CheckBox
    Friend WithEvents chkOnlyWithBudget As System.Windows.Forms.CheckBox
    Friend WithEvents pbFiles As System.Windows.Forms.ProgressBar
    Friend WithEvents pnlChooseFiles As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pbImportFiles As System.Windows.Forms.ProgressBar
    Friend WithEvents grdChooseFiles As System.Windows.Forms.DataGridView
    Friend WithEvents colImport As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPath As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAction As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents txtExcludeFolders As System.Windows.Forms.TextBox
    Friend WithEvents chkExcludeFolders As System.Windows.Forms.CheckBox
    Friend WithEvents lblDuplicate As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dtDate As clTrinity.ExtendedDateTimePicker
    Friend WithEvents chkDate As System.Windows.Forms.CheckBox
End Class
