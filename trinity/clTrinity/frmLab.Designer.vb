<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLab
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLab))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.mnuLastWeeks = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSetup = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuLastYear = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFindReach = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuFindReachLastWeeks = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFindReachLastYear = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdEditName = New System.Windows.Forms.Button()
        Me.cmdSetup = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.cmdDeleteCampaign = New System.Windows.Forms.Button()
        Me.cmdAddCampaign = New System.Windows.Forms.Button()
        Me.tabLab = New clTrinity.ExtendedTabControl()
        Me.tpCampaigns = New System.Windows.Forms.TabPage()
        Me.grpCompensation = New System.Windows.Forms.GroupBox()
        Me.chkIncludeInSums = New System.Windows.Forms.CheckBox()
        Me.cmdRemoveChannel = New System.Windows.Forms.Button()
        Me.cmdAddChannel = New System.Windows.Forms.Button()
        Me.grdCompensation = New System.Windows.Forms.DataGridView()
        Me.colChannel = New clTrinity.ExtendedComboboxColumn()
        Me.colFrom = New clTrinity.CalendarColumn()
        Me.colTo = New clTrinity.CalendarColumn()
        Me.colTRPs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colExpense = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colComment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpLoading = New System.Windows.Forms.GroupBox()
        Me.cmbChannelLoading = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmdResetLoading = New System.Windows.Forms.Button()
        Me.cmbLoading = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmdApplyLoading = New System.Windows.Forms.Button()
        Me.grdLoading = New System.Windows.Forms.DataGridView()
        Me.picCollapseLoading = New System.Windows.Forms.PictureBox()
        Me.picExpandLoading = New System.Windows.Forms.PictureBox()
        Me.grpFindReach = New System.Windows.Forms.GroupBox()
        Me.cmdGo = New System.Windows.Forms.Button()
        Me.lblCurrentReach = New System.Windows.Forms.Label()
        Me.txtTolerance = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbFF = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtReach = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtSteps = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbTRPBudget = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmbCampaigns = New System.Windows.Forms.ComboBox()
        Me.grpIndex = New System.Windows.Forms.GroupBox()
        Me.cmdIndexSettings = New System.Windows.Forms.Button()
        Me.cmdNaturalDelivery = New System.Windows.Forms.Button()
        Me.grdIndex = New System.Windows.Forms.DataGridView()
        Me.colMainTarget = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSecTarget = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAllAdults = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.grpFilms = New System.Windows.Forms.GroupBox()
        Me.pnlChoose = New System.Windows.Forms.Panel()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.btnBudget = New System.Windows.Forms.RadioButton()
        Me.btnTRP = New System.Windows.Forms.RadioButton()
        Me.grdFilms = New System.Windows.Forms.DataGridView()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.cmdCopyToAll = New System.Windows.Forms.Button()
        Me.cmbFilmChannel = New System.Windows.Forms.ComboBox()
        Me.grpTRP = New System.Windows.Forms.GroupBox()
        Me.cmdLockOnTRP = New System.Windows.Forms.Button()
        Me.cmbTargets = New System.Windows.Forms.ComboBox()
        Me.lblExplain = New System.Windows.Forms.Label()
        Me.grdGrandSum = New System.Windows.Forms.DataGridView()
        Me.colGrandSum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grdSumChannels = New System.Windows.Forms.DataGridView()
        Me.colSumNat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSumChn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grdSumWeeks = New System.Windows.Forms.DataGridView()
        Me.cmbDisplay = New System.Windows.Forms.ComboBox()
        Me.cmbUniverse = New System.Windows.Forms.ComboBox()
        Me.grdTRP = New System.Windows.Forms.DataGridView()
        Me.grpDiscounts = New System.Windows.Forms.GroupBox()
        Me.grdDiscounts = New System.Windows.Forms.DataGridView()
        Me.picCollapseDiscounts = New System.Windows.Forms.PictureBox()
        Me.picExpandDiscounts = New System.Windows.Forms.PictureBox()
        Me.grpAV = New System.Windows.Forms.GroupBox()
        Me.grdAV = New System.Windows.Forms.DataGridView()
        Me.picCollapseAV = New System.Windows.Forms.PictureBox()
        Me.picExpandAV = New System.Windows.Forms.PictureBox()
        Me.grpBudget = New System.Windows.Forms.GroupBox()
        Me.cmdLockOnBudget = New System.Windows.Forms.Button()
        Me.cmdEditCTC = New System.Windows.Forms.Button()
        Me.lblCTC = New System.Windows.Forms.Label()
        Me.lblCTCLabel = New System.Windows.Forms.Label()
        Me.grdBudget = New System.Windows.Forms.DataGridView()
        Me.colTRP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBudget = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tpReach = New System.Windows.Forms.TabPage()
        Me.grdReach = New System.Windows.Forms.DataGridView()
        Me.cmbFreq = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dtCustomDate = New clTrinity.ExtendedDateTimePicker()
        Me.optCustomDate = New System.Windows.Forms.RadioButton()
        Me.optLastYear = New System.Windows.Forms.RadioButton()
        Me.optLastWeeks = New System.Windows.Forms.RadioButton()
        Me.chtKarma = New clTrinity.Charts()
        Me.cmdCalculate = New System.Windows.Forms.Button()
        Me.tpProfile = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbProfileCampaign = New System.Windows.Forms.ComboBox()
        Me.chtProfile = New clTrinity.ProfileChart()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mnuSetup.SuspendLayout
        Me.mnuFindReach.SuspendLayout
        Me.tabLab.SuspendLayout
        Me.tpCampaigns.SuspendLayout
        Me.grpCompensation.SuspendLayout
        CType(Me.grdCompensation,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpLoading.SuspendLayout
        CType(Me.grdLoading,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picCollapseLoading,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picExpandLoading,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpFindReach.SuspendLayout
        Me.grpIndex.SuspendLayout
        CType(Me.grdIndex,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpFilms.SuspendLayout
        Me.pnlChoose.SuspendLayout
        CType(Me.PictureBox4,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox3,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grdFilms,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox2,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpTRP.SuspendLayout
        CType(Me.grdGrandSum,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grdSumChannels,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grdSumWeeks,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grdTRP,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpDiscounts.SuspendLayout
        CType(Me.grdDiscounts,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picCollapseDiscounts,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picExpandDiscounts,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpAV.SuspendLayout
        CType(Me.grdAV,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picCollapseAV,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picExpandAV,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpBudget.SuspendLayout
        CType(Me.grdBudget,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tpReach.SuspendLayout
        CType(Me.grdReach,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox1.SuspendLayout
        Me.tpProfile.SuspendLayout
        Me.SuspendLayout
        '
        'mnuLastWeeks
        '
        Me.mnuLastWeeks.Checked = true
        Me.mnuLastWeeks.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuLastWeeks.Name = "mnuLastWeeks"
        Me.mnuLastWeeks.Size = New System.Drawing.Size(207, 22)
        Me.mnuLastWeeks.Text = "Use last weeks of data"
        '
        'mnuSetup
        '
        Me.mnuSetup.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuLastWeeks, Me.mnuLastYear})
        Me.mnuSetup.Name = "mnuSetup"
        Me.mnuSetup.ShowCheckMargin = true
        Me.mnuSetup.ShowImageMargin = false
        Me.mnuSetup.Size = New System.Drawing.Size(208, 48)
        '
        'mnuLastYear
        '
        Me.mnuLastYear.Name = "mnuLastYear"
        Me.mnuLastYear.Size = New System.Drawing.Size(207, 22)
        Me.mnuLastYear.Text = "Use same period last year"
        '
        'mnuFindReach
        '
        Me.mnuFindReach.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFindReachLastWeeks, Me.mnuFindReachLastYear})
        Me.mnuFindReach.Name = "mnuFindReach"
        Me.mnuFindReach.Size = New System.Drawing.Size(187, 48)
        '
        'mnuFindReachLastWeeks
        '
        Me.mnuFindReachLastWeeks.Name = "mnuFindReachLastWeeks"
        Me.mnuFindReachLastWeeks.Size = New System.Drawing.Size(186, 22)
        Me.mnuFindReachLastWeeks.Text = "Last weeks of data"
        '
        'mnuFindReachLastYear
        '
        Me.mnuFindReachLastYear.Name = "mnuFindReachLastYear"
        Me.mnuFindReachLastYear.Size = New System.Drawing.Size(186, 22)
        Me.mnuFindReachLastYear.Text = "Same period last year"
        '
        'cmdEditName
        '
        Me.cmdEditName.Image = Global.clTrinity.My.Resources.Resources.edit_note_3
        Me.cmdEditName.Location = New System.Drawing.Point(341, 10)
        Me.cmdEditName.Name = "cmdEditName"
        Me.cmdEditName.Size = New System.Drawing.Size(24, 25)
        Me.cmdEditName.TabIndex = 21
        Me.ToolTip1.SetToolTip(Me.cmdEditName, "Edit the name of this scenario")
        Me.cmdEditName.UseVisualStyleBackColor = true
        '
        'cmdSetup
        '
        Me.cmdSetup.Image = Global.clTrinity.My.Resources.Resources.calender_2_small
        Me.cmdSetup.Location = New System.Drawing.Point(277, 10)
        Me.cmdSetup.Name = "cmdSetup"
        Me.cmdSetup.Size = New System.Drawing.Size(28, 25)
        Me.cmdSetup.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.cmdSetup, "Change setup on campaign")
        Me.cmdSetup.UseVisualStyleBackColor = true
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Image = Global.clTrinity.My.Resources.Resources.sync_2_small
        Me.cmdRefresh.Location = New System.Drawing.Point(311, 10)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(24, 25)
        Me.cmdRefresh.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.cmdRefresh, "Refresh this lab campaign to match setup of main campaign")
        Me.cmdRefresh.UseVisualStyleBackColor = true
        '
        'cmdDeleteCampaign
        '
        Me.cmdDeleteCampaign.Image = CType(resources.GetObject("cmdDeleteCampaign.Image"),System.Drawing.Image)
        Me.cmdDeleteCampaign.Location = New System.Drawing.Point(247, 10)
        Me.cmdDeleteCampaign.Name = "cmdDeleteCampaign"
        Me.cmdDeleteCampaign.Size = New System.Drawing.Size(24, 25)
        Me.cmdDeleteCampaign.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.cmdDeleteCampaign, "Remove this lab campaign")
        Me.cmdDeleteCampaign.UseVisualStyleBackColor = true
        '
        'cmdAddCampaign
        '
        Me.cmdAddCampaign.Image = CType(resources.GetObject("cmdAddCampaign.Image"),System.Drawing.Image)
        Me.cmdAddCampaign.Location = New System.Drawing.Point(217, 10)
        Me.cmdAddCampaign.Name = "cmdAddCampaign"
        Me.cmdAddCampaign.Size = New System.Drawing.Size(24, 25)
        Me.cmdAddCampaign.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.cmdAddCampaign, "Add new lab campaign")
        Me.cmdAddCampaign.UseVisualStyleBackColor = true
        '
        'tabLab
        '
        Me.tabLab.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.tabLab.Controls.Add(Me.tpCampaigns)
        Me.tabLab.Controls.Add(Me.tpReach)
        Me.tabLab.Controls.Add(Me.tpProfile)
        Me.tabLab.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.tabLab.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tabLab.Location = New System.Drawing.Point(0, 3)
        Me.tabLab.Name = "tabLab"
        Me.tabLab.SelectedIndex = 0
        Me.tabLab.Size = New System.Drawing.Size(993, 561)
        Me.tabLab.TabIndex = 0
        '
        'tpCampaigns
        '
        Me.tpCampaigns.AutoScroll = true
        Me.tpCampaigns.Controls.Add(Me.cmdEditName)
        Me.tpCampaigns.Controls.Add(Me.cmdSetup)
        Me.tpCampaigns.Controls.Add(Me.grpCompensation)
        Me.tpCampaigns.Controls.Add(Me.cmdRefresh)
        Me.tpCampaigns.Controls.Add(Me.grpLoading)
        Me.tpCampaigns.Controls.Add(Me.cmdDeleteCampaign)
        Me.tpCampaigns.Controls.Add(Me.grpFindReach)
        Me.tpCampaigns.Controls.Add(Me.cmdAddCampaign)
        Me.tpCampaigns.Controls.Add(Me.cmbCampaigns)
        Me.tpCampaigns.Controls.Add(Me.grpIndex)
        Me.tpCampaigns.Controls.Add(Me.PictureBox1)
        Me.tpCampaigns.Controls.Add(Me.grpFilms)
        Me.tpCampaigns.Controls.Add(Me.grpTRP)
        Me.tpCampaigns.Controls.Add(Me.grpDiscounts)
        Me.tpCampaigns.Controls.Add(Me.grpAV)
        Me.tpCampaigns.Controls.Add(Me.grpBudget)
        Me.tpCampaigns.Location = New System.Drawing.Point(4, 23)
        Me.tpCampaigns.Name = "tpCampaigns"
        Me.tpCampaigns.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCampaigns.Size = New System.Drawing.Size(985, 534)
        Me.tpCampaigns.TabIndex = 0
        Me.tpCampaigns.Text = "Campaigns"
        Me.tpCampaigns.UseVisualStyleBackColor = true
        '
        'grpCompensation
        '
        Me.grpCompensation.Controls.Add(Me.chkIncludeInSums)
        Me.grpCompensation.Controls.Add(Me.cmdRemoveChannel)
        Me.grpCompensation.Controls.Add(Me.cmdAddChannel)
        Me.grpCompensation.Controls.Add(Me.grdCompensation)
        Me.grpCompensation.Location = New System.Drawing.Point(437, 341)
        Me.grpCompensation.Name = "grpCompensation"
        Me.grpCompensation.Size = New System.Drawing.Size(511, 135)
        Me.grpCompensation.TabIndex = 10
        Me.grpCompensation.TabStop = false
        Me.grpCompensation.Text = "Compensations"
        '
        'chkIncludeInSums
        '
        Me.chkIncludeInSums.AutoSize = true
        Me.chkIncludeInSums.Checked = true
        Me.chkIncludeInSums.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludeInSums.Location = New System.Drawing.Point(6, 112)
        Me.chkIncludeInSums.Name = "chkIncludeInSums"
        Me.chkIncludeInSums.Size = New System.Drawing.Size(122, 18)
        Me.chkIncludeInSums.TabIndex = 17
        Me.chkIncludeInSums.Text = "Include in TRP sums"
        Me.chkIncludeInSums.UseVisualStyleBackColor = true
        '
        'cmdRemoveChannel
        '
        Me.cmdRemoveChannel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveChannel.FlatAppearance.BorderSize = 0
        Me.cmdRemoveChannel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRemoveChannel.Image = CType(resources.GetObject("cmdRemoveChannel.Image"),System.Drawing.Image)
        Me.cmdRemoveChannel.Location = New System.Drawing.Point(483, 44)
        Me.cmdRemoveChannel.Name = "cmdRemoveChannel"
        Me.cmdRemoveChannel.Size = New System.Drawing.Size(22, 20)
        Me.cmdRemoveChannel.TabIndex = 16
        Me.cmdRemoveChannel.UseVisualStyleBackColor = true
        '
        'cmdAddChannel
        '
        Me.cmdAddChannel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdAddChannel.FlatAppearance.BorderSize = 0
        Me.cmdAddChannel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddChannel.Image = CType(resources.GetObject("cmdAddChannel.Image"),System.Drawing.Image)
        Me.cmdAddChannel.Location = New System.Drawing.Point(483, 18)
        Me.cmdAddChannel.Name = "cmdAddChannel"
        Me.cmdAddChannel.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddChannel.TabIndex = 15
        Me.cmdAddChannel.UseVisualStyleBackColor = true
        '
        'grdCompensation
        '
        Me.grdCompensation.AllowUserToAddRows = false
        Me.grdCompensation.AllowUserToDeleteRows = false
        Me.grdCompensation.AllowUserToResizeColumns = false
        Me.grdCompensation.AllowUserToResizeRows = false
        Me.grdCompensation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCompensation.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colChannel, Me.colFrom, Me.colTo, Me.colTRPs, Me.colExpense, Me.colComment})
        Me.grdCompensation.Location = New System.Drawing.Point(6, 18)
        Me.grdCompensation.Name = "grdCompensation"
        Me.grdCompensation.RowHeadersVisible = false
        Me.grdCompensation.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.grdCompensation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdCompensation.Size = New System.Drawing.Size(471, 89)
        Me.grdCompensation.TabIndex = 0
        Me.grdCompensation.VirtualMode = true
        '
        'colChannel
        '
        Me.colChannel.Frozen = true
        Me.colChannel.HeaderText = "Channel"
        Me.colChannel.Name = "colChannel"
        Me.colChannel.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'colFrom
        '
        Me.colFrom.Frozen = true
        Me.colFrom.HeaderText = "From"
        Me.colFrom.Name = "colFrom"
        Me.colFrom.Width = 85
        '
        'colTo
        '
        Me.colTo.Frozen = true
        Me.colTo.HeaderText = "To"
        Me.colTo.Name = "colTo"
        Me.colTo.Width = 85
        '
        'colTRPs
        '
        Me.colTRPs.Frozen = true
        Me.colTRPs.HeaderText = "TRPs"
        Me.colTRPs.Name = "colTRPs"
        Me.colTRPs.Width = 40
        '
        'colExpense
        '
        Me.colExpense.Frozen = true
        Me.colExpense.HeaderText = "Expense"
        Me.colExpense.Name = "colExpense"
        Me.colExpense.Width = 70
        '
        'colComment
        '
        Me.colComment.Frozen = true
        Me.colComment.HeaderText = "Comment"
        Me.colComment.Name = "colComment"
        '
        'grpLoading
        '
        Me.grpLoading.Controls.Add(Me.cmbChannelLoading)
        Me.grpLoading.Controls.Add(Me.Label9)
        Me.grpLoading.Controls.Add(Me.cmdResetLoading)
        Me.grpLoading.Controls.Add(Me.cmbLoading)
        Me.grpLoading.Controls.Add(Me.Label8)
        Me.grpLoading.Controls.Add(Me.cmdApplyLoading)
        Me.grpLoading.Controls.Add(Me.grdLoading)
        Me.grpLoading.Controls.Add(Me.picCollapseLoading)
        Me.grpLoading.Controls.Add(Me.picExpandLoading)
        Me.grpLoading.Location = New System.Drawing.Point(8, 393)
        Me.grpLoading.Name = "grpLoading"
        Me.grpLoading.Size = New System.Drawing.Size(343, 113)
        Me.grpLoading.TabIndex = 18
        Me.grpLoading.TabStop = false
        Me.grpLoading.Text = "Loading       "
        '
        'cmbChannelLoading
        '
        Me.cmbChannelLoading.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChannelLoading.FormattingEnabled = true
        Me.cmbChannelLoading.Items.AddRange(New Object() {"TRPs", "Budget"})
        Me.cmbChannelLoading.Location = New System.Drawing.Point(105, 44)
        Me.cmbChannelLoading.Name = "cmbChannelLoading"
        Me.cmbChannelLoading.Size = New System.Drawing.Size(69, 22)
        Me.cmbChannelLoading.TabIndex = 11
        '
        'Label9
        '
        Me.Label9.AutoSize = true
        Me.Label9.Location = New System.Drawing.Point(6, 46)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(93, 14)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Load channels on"
        '
        'cmdResetLoading
        '
        Me.cmdResetLoading.FlatAppearance.BorderSize = 0
        Me.cmdResetLoading.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdResetLoading.Image = Global.clTrinity.My.Resources.Resources.sync_2_small
        Me.cmdResetLoading.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdResetLoading.Location = New System.Drawing.Point(180, 83)
        Me.cmdResetLoading.Name = "cmdResetLoading"
        Me.cmdResetLoading.Size = New System.Drawing.Size(63, 21)
        Me.cmdResetLoading.TabIndex = 9
        Me.cmdResetLoading.Text = "Reset"
        Me.cmdResetLoading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdResetLoading.UseVisualStyleBackColor = true
        '
        'cmbLoading
        '
        Me.cmbLoading.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLoading.FormattingEnabled = true
        Me.cmbLoading.Items.AddRange(New Object() {"TRPs", "Budget"})
        Me.cmbLoading.Location = New System.Drawing.Point(105, 18)
        Me.cmbLoading.Name = "cmbLoading"
        Me.cmbLoading.Size = New System.Drawing.Size(69, 22)
        Me.cmbLoading.TabIndex = 8
        '
        'Label8
        '
        Me.Label8.AutoSize = true
        Me.Label8.Location = New System.Drawing.Point(6, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(82, 14)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Load weeks on"
        '
        'cmdApplyLoading
        '
        Me.cmdApplyLoading.FlatAppearance.BorderSize = 0
        Me.cmdApplyLoading.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdApplyLoading.Image = Global.clTrinity.My.Resources.Resources.apply_2_small
        Me.cmdApplyLoading.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdApplyLoading.Location = New System.Drawing.Point(249, 83)
        Me.cmdApplyLoading.Name = "cmdApplyLoading"
        Me.cmdApplyLoading.Size = New System.Drawing.Size(63, 21)
        Me.cmdApplyLoading.TabIndex = 6
        Me.cmdApplyLoading.Text = "Apply"
        Me.cmdApplyLoading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdApplyLoading.UseVisualStyleBackColor = true
        '
        'grdLoading
        '
        Me.grdLoading.AllowUserToAddRows = false
        Me.grdLoading.AllowUserToDeleteRows = false
        Me.grdLoading.AllowUserToResizeColumns = false
        Me.grdLoading.AllowUserToResizeRows = false
        Me.grdLoading.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdLoading.DefaultCellStyle = DataGridViewCellStyle1
        Me.grdLoading.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.grdLoading.Location = New System.Drawing.Point(152, 68)
        Me.grdLoading.Name = "grdLoading"
        Me.grdLoading.RowHeadersVisible = false
        Me.grdLoading.Size = New System.Drawing.Size(160, 9)
        Me.grdLoading.TabIndex = 5
        Me.grdLoading.VirtualMode = true
        '
        'picCollapseLoading
        '
        Me.picCollapseLoading.Image = CType(resources.GetObject("picCollapseLoading.Image"),System.Drawing.Image)
        Me.picCollapseLoading.Location = New System.Drawing.Point(50, 0)
        Me.picCollapseLoading.Name = "picCollapseLoading"
        Me.picCollapseLoading.Size = New System.Drawing.Size(20, 20)
        Me.picCollapseLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picCollapseLoading.TabIndex = 3
        Me.picCollapseLoading.TabStop = false
        Me.picCollapseLoading.Visible = false
        '
        'picExpandLoading
        '
        Me.picExpandLoading.Image = CType(resources.GetObject("picExpandLoading.Image"),System.Drawing.Image)
        Me.picExpandLoading.Location = New System.Drawing.Point(50, 0)
        Me.picExpandLoading.Name = "picExpandLoading"
        Me.picExpandLoading.Size = New System.Drawing.Size(16, 16)
        Me.picExpandLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picExpandLoading.TabIndex = 4
        Me.picExpandLoading.TabStop = false
        '
        'grpFindReach
        '
        Me.grpFindReach.Controls.Add(Me.cmdGo)
        Me.grpFindReach.Controls.Add(Me.lblCurrentReach)
        Me.grpFindReach.Controls.Add(Me.txtTolerance)
        Me.grpFindReach.Controls.Add(Me.Label6)
        Me.grpFindReach.Controls.Add(Me.cmbFF)
        Me.grpFindReach.Controls.Add(Me.Label5)
        Me.grpFindReach.Controls.Add(Me.txtReach)
        Me.grpFindReach.Controls.Add(Me.Label4)
        Me.grpFindReach.Controls.Add(Me.txtSteps)
        Me.grpFindReach.Controls.Add(Me.Label3)
        Me.grpFindReach.Controls.Add(Me.cmbTRPBudget)
        Me.grpFindReach.Controls.Add(Me.Label2)
        Me.grpFindReach.Controls.Add(Me.cmdCancel)
        Me.grpFindReach.Location = New System.Drawing.Point(437, 266)
        Me.grpFindReach.Name = "grpFindReach"
        Me.grpFindReach.Size = New System.Drawing.Size(443, 69)
        Me.grpFindReach.TabIndex = 16
        Me.grpFindReach.TabStop = false
        Me.grpFindReach.Text = "Find reach"
        '
        'cmdGo
        '
        Me.cmdGo.Image = Global.clTrinity.My.Resources.Resources.calculator_2_small
        Me.cmdGo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdGo.Location = New System.Drawing.Point(381, 38)
        Me.cmdGo.Name = "cmdGo"
        Me.cmdGo.Size = New System.Drawing.Size(54, 25)
        Me.cmdGo.TabIndex = 11
        Me.cmdGo.Text = "Go!"
        Me.cmdGo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdGo.UseVisualStyleBackColor = true
        '
        'lblCurrentReach
        '
        Me.lblCurrentReach.AutoSize = true
        Me.lblCurrentReach.Location = New System.Drawing.Point(248, 45)
        Me.lblCurrentReach.Name = "lblCurrentReach"
        Me.lblCurrentReach.Size = New System.Drawing.Size(105, 14)
        Me.lblCurrentReach.TabIndex = 10
        Me.lblCurrentReach.Text = "Current reach: 0.0%"
        '
        'txtTolerance
        '
        Me.txtTolerance.Location = New System.Drawing.Point(110, 36)
        Me.txtTolerance.Multiline = true
        Me.txtTolerance.Name = "txtTolerance"
        Me.txtTolerance.Size = New System.Drawing.Size(23, 21)
        Me.txtTolerance.TabIndex = 9
        Me.txtTolerance.Text = "1"
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Location = New System.Drawing.Point(6, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 14)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "with a tolerance of"
        '
        'cmbFF
        '
        Me.cmbFF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFF.FormattingEnabled = true
        Me.cmbFF.Items.AddRange(New Object() {"1+", "2+", "3+", "4+", "5+"})
        Me.cmbFF.Location = New System.Drawing.Point(395, 12)
        Me.cmbFF.Name = "cmbFF"
        Me.cmbFF.Size = New System.Drawing.Size(40, 22)
        Me.cmbFF.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(374, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(15, 14)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "in"
        '
        'txtReach
        '
        Me.txtReach.Location = New System.Drawing.Point(322, 12)
        Me.txtReach.Multiline = true
        Me.txtReach.Name = "txtReach"
        Me.txtReach.Size = New System.Drawing.Size(46, 21)
        Me.txtReach.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(248, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 14)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "until reach is"
        '
        'txtSteps
        '
        Me.txtSteps.Location = New System.Drawing.Point(197, 12)
        Me.txtSteps.Multiline = true
        Me.txtSteps.Name = "txtSteps"
        Me.txtSteps.Size = New System.Drawing.Size(45, 21)
        Me.txtSteps.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(133, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "in steps of"
        '
        'cmbTRPBudget
        '
        Me.cmbTRPBudget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTRPBudget.FormattingEnabled = true
        Me.cmbTRPBudget.Items.AddRange(New Object() {"TRP", "Budget"})
        Me.cmbTRPBudget.Location = New System.Drawing.Point(60, 12)
        Me.cmbTRPBudget.Name = "cmbTRPBudget"
        Me.cmbTRPBudget.Size = New System.Drawing.Size(67, 22)
        Me.cmbTRPBudget.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(6, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Increase"
        '
        'cmdCancel
        '
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCancel.Location = New System.Drawing.Point(381, 42)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(54, 21)
        Me.cmdCancel.TabIndex = 12
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = true
        '
        'cmbCampaigns
        '
        Me.cmbCampaigns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCampaigns.FormattingEnabled = true
        Me.cmbCampaigns.Location = New System.Drawing.Point(51, 10)
        Me.cmbCampaigns.Name = "cmbCampaigns"
        Me.cmbCampaigns.Size = New System.Drawing.Size(160, 22)
        Me.cmbCampaigns.TabIndex = 14
        '
        'grpIndex
        '
        Me.grpIndex.Controls.Add(Me.cmdIndexSettings)
        Me.grpIndex.Controls.Add(Me.cmdNaturalDelivery)
        Me.grpIndex.Controls.Add(Me.grdIndex)
        Me.grpIndex.Location = New System.Drawing.Point(437, 162)
        Me.grpIndex.MinimumSize = New System.Drawing.Size(0, 93)
        Me.grpIndex.Name = "grpIndex"
        Me.grpIndex.Size = New System.Drawing.Size(406, 94)
        Me.grpIndex.TabIndex = 13
        Me.grpIndex.TabStop = false
        Me.grpIndex.Text = "Target Indexes"
        '
        'cmdIndexSettings
        '
        Me.cmdIndexSettings.FlatAppearance.BorderSize = 0
        Me.cmdIndexSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdIndexSettings.Image = Global.clTrinity.My.Resources.Resources.play_2_small
        Me.cmdIndexSettings.Location = New System.Drawing.Point(325, 61)
        Me.cmdIndexSettings.Name = "cmdIndexSettings"
        Me.cmdIndexSettings.Size = New System.Drawing.Size(26, 24)
        Me.cmdIndexSettings.TabIndex = 7
        Me.cmdIndexSettings.UseVisualStyleBackColor = true
        '
        'cmdNaturalDelivery
        '
        Me.cmdNaturalDelivery.FlatAppearance.BorderSize = 0
        Me.cmdNaturalDelivery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdNaturalDelivery.Location = New System.Drawing.Point(325, 18)
        Me.cmdNaturalDelivery.Name = "cmdNaturalDelivery"
        Me.cmdNaturalDelivery.Size = New System.Drawing.Size(75, 38)
        Me.cmdNaturalDelivery.TabIndex = 6
        Me.cmdNaturalDelivery.Text = "Natural delivery"
        Me.cmdNaturalDelivery.UseVisualStyleBackColor = true
        '
        'grdIndex
        '
        Me.grdIndex.AllowUserToAddRows = false
        Me.grdIndex.AllowUserToDeleteRows = false
        Me.grdIndex.AllowUserToResizeColumns = false
        Me.grdIndex.AllowUserToResizeRows = false
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdIndex.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdIndex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdIndex.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colMainTarget, Me.colSecTarget, Me.colAllAdults})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = "0"
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdIndex.DefaultCellStyle = DataGridViewCellStyle3
        Me.grdIndex.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.grdIndex.Location = New System.Drawing.Point(136, 18)
        Me.grdIndex.Name = "grdIndex"
        Me.grdIndex.RowHeadersVisible = false
        Me.grdIndex.ShowEditingIcon = false
        Me.grdIndex.Size = New System.Drawing.Size(183, 71)
        Me.grdIndex.TabIndex = 5
        Me.grdIndex.VirtualMode = true
        '
        'colMainTarget
        '
        Me.colMainTarget.HeaderText = "Main"
        Me.colMainTarget.Name = "colMainTarget"
        Me.colMainTarget.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colMainTarget.Width = 60
        '
        'colSecTarget
        '
        Me.colSecTarget.HeaderText = "Sec"
        Me.colSecTarget.Name = "colSecTarget"
        Me.colSecTarget.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colSecTarget.Width = 60
        '
        'colAllAdults
        '
        Me.colAllAdults.HeaderText = "All adults"
        Me.colAllAdults.Name = "colAllAdults"
        Me.colAllAdults.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colAllAdults.Width = 60
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.money_bag_2
        Me.PictureBox1.Location = New System.Drawing.Point(8, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = false
        '
        'grpFilms
        '
        Me.grpFilms.Controls.Add(Me.pnlChoose)
        Me.grpFilms.Controls.Add(Me.grdFilms)
        Me.grpFilms.Controls.Add(Me.PictureBox2)
        Me.grpFilms.Controls.Add(Me.cmdCopyToAll)
        Me.grpFilms.Controls.Add(Me.cmbFilmChannel)
        Me.grpFilms.Location = New System.Drawing.Point(437, 41)
        Me.grpFilms.MinimumSize = New System.Drawing.Size(215, 0)
        Me.grpFilms.Name = "grpFilms"
        Me.grpFilms.Size = New System.Drawing.Size(316, 116)
        Me.grpFilms.TabIndex = 12
        Me.grpFilms.TabStop = false
        Me.grpFilms.Text = "Films"
        '
        'pnlChoose
        '
        Me.pnlChoose.Controls.Add(Me.PictureBox4)
        Me.pnlChoose.Controls.Add(Me.PictureBox3)
        Me.pnlChoose.Controls.Add(Me.btnBudget)
        Me.pnlChoose.Controls.Add(Me.btnTRP)
        Me.pnlChoose.Location = New System.Drawing.Point(259, 45)
        Me.pnlChoose.Name = "pnlChoose"
        Me.pnlChoose.Size = New System.Drawing.Size(51, 50)
        Me.pnlChoose.TabIndex = 11
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.clTrinity.My.Resources.Resources.target_group_2
        Me.PictureBox4.Location = New System.Drawing.Point(23, 0)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(24, 22)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 13
        Me.PictureBox4.TabStop = false
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.clTrinity.My.Resources.Resources.money_bag_2
        Me.PictureBox3.Location = New System.Drawing.Point(23, 28)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox3.TabIndex = 12
        Me.PictureBox3.TabStop = false
        '
        'btnBudget
        '
        Me.btnBudget.AutoSize = true
        Me.btnBudget.Location = New System.Drawing.Point(4, 33)
        Me.btnBudget.Name = "btnBudget"
        Me.btnBudget.Size = New System.Drawing.Size(14, 13)
        Me.btnBudget.TabIndex = 11
        Me.btnBudget.Tag = "1"
        Me.btnBudget.UseVisualStyleBackColor = true
        '
        'btnTRP
        '
        Me.btnTRP.AutoSize = true
        Me.btnTRP.Checked = true
        Me.btnTRP.Location = New System.Drawing.Point(3, 6)
        Me.btnTRP.Name = "btnTRP"
        Me.btnTRP.Size = New System.Drawing.Size(14, 13)
        Me.btnTRP.TabIndex = 10
        Me.btnTRP.TabStop = true
        Me.btnTRP.Tag = "0"
        Me.btnTRP.UseVisualStyleBackColor = true
        '
        'grdFilms
        '
        Me.grdFilms.AllowUserToAddRows = false
        Me.grdFilms.AllowUserToDeleteRows = false
        Me.grdFilms.AllowUserToResizeColumns = false
        Me.grdFilms.AllowUserToResizeRows = false
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdFilms.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdFilms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.Format = "P0"
        DataGridViewCellStyle5.NullValue = "0"
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdFilms.DefaultCellStyle = DataGridViewCellStyle5
        Me.grdFilms.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.grdFilms.Location = New System.Drawing.Point(6, 45)
        Me.grdFilms.Name = "grdFilms"
        Me.grdFilms.ShowEditingIcon = false
        Me.grdFilms.Size = New System.Drawing.Size(246, 65)
        Me.grdFilms.TabIndex = 4
        Me.grdFilms.VirtualMode = true
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.clTrinity.My.Resources.Resources.film_3_24x24
        Me.PictureBox2.Location = New System.Drawing.Point(151, 16)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(20, 20)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox2.TabIndex = 2
        Me.PictureBox2.TabStop = false
        '
        'cmdCopyToAll
        '
        Me.cmdCopyToAll.Image = Global.clTrinity.My.Resources.Resources.copy_3_16x16
        Me.cmdCopyToAll.Location = New System.Drawing.Point(181, 16)
        Me.cmdCopyToAll.Name = "cmdCopyToAll"
        Me.cmdCopyToAll.Size = New System.Drawing.Size(26, 24)
        Me.cmdCopyToAll.TabIndex = 1
        Me.cmdCopyToAll.UseVisualStyleBackColor = true
        '
        'cmbFilmChannel
        '
        Me.cmbFilmChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFilmChannel.FormattingEnabled = true
        Me.cmbFilmChannel.Location = New System.Drawing.Point(6, 18)
        Me.cmbFilmChannel.Name = "cmbFilmChannel"
        Me.cmbFilmChannel.Size = New System.Drawing.Size(139, 22)
        Me.cmbFilmChannel.TabIndex = 0
        '
        'grpTRP
        '
        Me.grpTRP.Controls.Add(Me.cmdLockOnTRP)
        Me.grpTRP.Controls.Add(Me.cmbTargets)
        Me.grpTRP.Controls.Add(Me.lblExplain)
        Me.grpTRP.Controls.Add(Me.grdGrandSum)
        Me.grpTRP.Controls.Add(Me.grdSumChannels)
        Me.grpTRP.Controls.Add(Me.grdSumWeeks)
        Me.grpTRP.Controls.Add(Me.cmbDisplay)
        Me.grpTRP.Controls.Add(Me.cmbUniverse)
        Me.grpTRP.Controls.Add(Me.grdTRP)
        Me.grpTRP.Location = New System.Drawing.Point(8, 41)
        Me.grpTRP.Name = "grpTRP"
        Me.grpTRP.Size = New System.Drawing.Size(423, 253)
        Me.grpTRP.TabIndex = 8
        Me.grpTRP.TabStop = false
        Me.grpTRP.Text = "TRP Allocation"
        '
        'cmdLockOnTRP
        '
        Me.cmdLockOnTRP.Image = Global.clTrinity.My.Resources.Resources.lock_2_small
        Me.cmdLockOnTRP.Location = New System.Drawing.Point(363, 15)
        Me.cmdLockOnTRP.Name = "cmdLockOnTRP"
        Me.cmdLockOnTRP.Size = New System.Drawing.Size(28, 26)
        Me.cmdLockOnTRP.TabIndex = 18
        Me.cmdLockOnTRP.UseVisualStyleBackColor = true
        '
        'cmbTargets
        '
        Me.cmbTargets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTargets.FormattingEnabled = true
        Me.cmbTargets.Items.AddRange(New Object() {"Buying/Main", "Buying/Second"})
        Me.cmbTargets.Location = New System.Drawing.Point(163, 18)
        Me.cmbTargets.Name = "cmbTargets"
        Me.cmbTargets.Size = New System.Drawing.Size(113, 22)
        Me.cmbTargets.TabIndex = 12
        '
        'lblExplain
        '
        Me.lblExplain.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblExplain.AutoSize = true
        Me.lblExplain.Location = New System.Drawing.Point(1, 233)
        Me.lblExplain.Name = "lblExplain"
        Me.lblExplain.Size = New System.Drawing.Size(383, 14)
        Me.lblExplain.TabIndex = 11
        Me.lblExplain.Text = "TRPs and budgets shown in this color are calculated from the set TRP/budget."
        '
        'grdGrandSum
        '
        Me.grdGrandSum.AllowUserToAddRows = false
        Me.grdGrandSum.AllowUserToDeleteRows = false
        Me.grdGrandSum.AllowUserToResizeColumns = false
        Me.grdGrandSum.AllowUserToResizeRows = false
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdGrandSum.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grdGrandSum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdGrandSum.ColumnHeadersVisible = false
        Me.grdGrandSum.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colGrandSum})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.Format = "N1"
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdGrandSum.DefaultCellStyle = DataGridViewCellStyle8
        Me.grdGrandSum.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.grdGrandSum.Location = New System.Drawing.Point(313, 161)
        Me.grdGrandSum.Name = "grdGrandSum"
        Me.grdGrandSum.RowHeadersVisible = false
        Me.grdGrandSum.Size = New System.Drawing.Size(53, 23)
        Me.grdGrandSum.TabIndex = 6
        Me.grdGrandSum.VirtualMode = true
        '
        'colGrandSum
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.Format = "N1"
        DataGridViewCellStyle7.NullValue = "0"
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colGrandSum.DefaultCellStyle = DataGridViewCellStyle7
        Me.colGrandSum.HeaderText = ""
        Me.colGrandSum.Name = "colGrandSum"
        Me.colGrandSum.Width = 50
        '
        'grdSumChannels
        '
        Me.grdSumChannels.AllowUserToAddRows = false
        Me.grdSumChannels.AllowUserToDeleteRows = false
        Me.grdSumChannels.AllowUserToResizeColumns = false
        Me.grdSumChannels.AllowUserToResizeRows = false
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.Format = "N1"
        DataGridViewCellStyle9.NullValue = "0"
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.grdSumChannels.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle9
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSumChannels.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.grdSumChannels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSumChannels.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSumNat, Me.colSumChn})
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(CType(CType(64,Byte),Integer), CType(CType(64,Byte),Integer), CType(CType(64,Byte),Integer))
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64,Byte),Integer), CType(CType(64,Byte),Integer), CType(CType(64,Byte),Integer))
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(64,Byte),Integer), CType(CType(64,Byte),Integer), CType(CType(64,Byte),Integer))
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(64,Byte),Integer), CType(CType(64,Byte),Integer), CType(CType(64,Byte),Integer))
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdSumChannels.DefaultCellStyle = DataGridViewCellStyle12
        Me.grdSumChannels.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.grdSumChannels.Location = New System.Drawing.Point(313, 45)
        Me.grdSumChannels.Name = "grdSumChannels"
        Me.grdSumChannels.RowHeadersVisible = false
        Me.grdSumChannels.Size = New System.Drawing.Size(104, 116)
        Me.grdSumChannels.TabIndex = 5
        Me.grdSumChannels.VirtualMode = true
        '
        'colSumNat
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colSumNat.DefaultCellStyle = DataGridViewCellStyle11
        Me.colSumNat.HeaderText = "Cmp"
        Me.colSumNat.Name = "colSumNat"
        Me.colSumNat.Width = 50
        '
        'colSumChn
        '
        Me.colSumChn.HeaderText = "Chn"
        Me.colSumChn.Name = "colSumChn"
        Me.colSumChn.Width = 50
        '
        'grdSumWeeks
        '
        Me.grdSumWeeks.AllowUserToAddRows = false
        Me.grdSumWeeks.AllowUserToDeleteRows = false
        Me.grdSumWeeks.AllowUserToResizeColumns = false
        Me.grdSumWeeks.AllowUserToResizeRows = false
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSumWeeks.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle13
        Me.grdSumWeeks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSumWeeks.ColumnHeadersVisible = false
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle14.Format = "N1"
        DataGridViewCellStyle14.NullValue = "0"
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdSumWeeks.DefaultCellStyle = DataGridViewCellStyle14
        Me.grdSumWeeks.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.grdSumWeeks.Location = New System.Drawing.Point(152, 161)
        Me.grdSumWeeks.Name = "grdSumWeeks"
        Me.grdSumWeeks.RowHeadersVisible = false
        Me.grdSumWeeks.Size = New System.Drawing.Size(160, 69)
        Me.grdSumWeeks.TabIndex = 4
        Me.grdSumWeeks.VirtualMode = true
        '
        'cmbDisplay
        '
        Me.cmbDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDisplay.FormattingEnabled = true
        Me.cmbDisplay.Items.AddRange(New Object() {"TRP", "% of week", "% of channel"})
        Me.cmbDisplay.Location = New System.Drawing.Point(285, 18)
        Me.cmbDisplay.Name = "cmbDisplay"
        Me.cmbDisplay.Size = New System.Drawing.Size(72, 22)
        Me.cmbDisplay.TabIndex = 1
        '
        'cmbUniverse
        '
        Me.cmbUniverse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUniverse.FormattingEnabled = true
        Me.cmbUniverse.Items.AddRange(New Object() {"Campaign universe", "Channel universe"})
        Me.cmbUniverse.Location = New System.Drawing.Point(6, 18)
        Me.cmbUniverse.Name = "cmbUniverse"
        Me.cmbUniverse.Size = New System.Drawing.Size(146, 22)
        Me.cmbUniverse.TabIndex = 0
        '
        'grdTRP
        '
        Me.grdTRP.AllowUserToAddRows = false
        Me.grdTRP.AllowUserToDeleteRows = false
        Me.grdTRP.AllowUserToResizeColumns = false
        Me.grdTRP.AllowUserToResizeRows = false
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTRP.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle15
        Me.grdTRP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle16.Format = "N1"
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdTRP.DefaultCellStyle = DataGridViewCellStyle16
        Me.grdTRP.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.grdTRP.Location = New System.Drawing.Point(152, 45)
        Me.grdTRP.Name = "grdTRP"
        Me.grdTRP.RowHeadersVisible = false
        Me.grdTRP.Size = New System.Drawing.Size(160, 116)
        Me.grdTRP.TabIndex = 3
        Me.grdTRP.VirtualMode = true
        '
        'grpDiscounts
        '
        Me.grpDiscounts.Controls.Add(Me.grdDiscounts)
        Me.grpDiscounts.Controls.Add(Me.picCollapseDiscounts)
        Me.grpDiscounts.Controls.Add(Me.picExpandDiscounts)
        Me.grpDiscounts.Location = New System.Drawing.Point(8, 341)
        Me.grpDiscounts.Name = "grpDiscounts"
        Me.grpDiscounts.Size = New System.Drawing.Size(323, 46)
        Me.grpDiscounts.TabIndex = 10
        Me.grpDiscounts.TabStop = false
        Me.grpDiscounts.Text = "Discounts       "
        '
        'grdDiscounts
        '
        Me.grdDiscounts.AllowUserToAddRows = false
        Me.grdDiscounts.AllowUserToDeleteRows = false
        Me.grdDiscounts.AllowUserToResizeColumns = false
        Me.grdDiscounts.AllowUserToResizeRows = false
        Me.grdDiscounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdDiscounts.DefaultCellStyle = DataGridViewCellStyle17
        Me.grdDiscounts.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.grdDiscounts.Location = New System.Drawing.Point(152, 18)
        Me.grdDiscounts.Name = "grdDiscounts"
        Me.grdDiscounts.ReadOnly = true
        Me.grdDiscounts.RowHeadersVisible = false
        Me.grdDiscounts.Size = New System.Drawing.Size(160, 93)
        Me.grdDiscounts.TabIndex = 4
        Me.grdDiscounts.VirtualMode = true
        '
        'picCollapseDiscounts
        '
        Me.picCollapseDiscounts.Image = CType(resources.GetObject("picCollapseDiscounts.Image"),System.Drawing.Image)
        Me.picCollapseDiscounts.Location = New System.Drawing.Point(60, 0)
        Me.picCollapseDiscounts.Name = "picCollapseDiscounts"
        Me.picCollapseDiscounts.Size = New System.Drawing.Size(20, 20)
        Me.picCollapseDiscounts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picCollapseDiscounts.TabIndex = 2
        Me.picCollapseDiscounts.TabStop = false
        Me.picCollapseDiscounts.Visible = false
        '
        'picExpandDiscounts
        '
        Me.picExpandDiscounts.Image = CType(resources.GetObject("picExpandDiscounts.Image"),System.Drawing.Image)
        Me.picExpandDiscounts.Location = New System.Drawing.Point(60, 0)
        Me.picExpandDiscounts.Name = "picExpandDiscounts"
        Me.picExpandDiscounts.Size = New System.Drawing.Size(16, 16)
        Me.picExpandDiscounts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picExpandDiscounts.TabIndex = 1
        Me.picExpandDiscounts.TabStop = false
        '
        'grpAV
        '
        Me.grpAV.Controls.Add(Me.grdAV)
        Me.grpAV.Controls.Add(Me.picCollapseAV)
        Me.grpAV.Controls.Add(Me.picExpandAV)
        Me.grpAV.Location = New System.Drawing.Point(8, 304)
        Me.grpAV.Name = "grpAV"
        Me.grpAV.Size = New System.Drawing.Size(323, 32)
        Me.grpAV.TabIndex = 9
        Me.grpAV.TabStop = false
        Me.grpAV.Text = "Added Values       "
        '
        'grdAV
        '
        Me.grdAV.AllowUserToAddRows = false
        Me.grdAV.AllowUserToDeleteRows = false
        Me.grdAV.AllowUserToResizeColumns = false
        Me.grdAV.AllowUserToResizeRows = false
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdAV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle18
        Me.grdAV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle19.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle19.Format = "P0"
        DataGridViewCellStyle19.NullValue = Nothing
        DataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdAV.DefaultCellStyle = DataGridViewCellStyle19
        Me.grdAV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.grdAV.Location = New System.Drawing.Point(152, 18)
        Me.grdAV.Name = "grdAV"
        Me.grdAV.RowHeadersVisible = false
        Me.grdAV.Size = New System.Drawing.Size(160, 51)
        Me.grdAV.TabIndex = 5
        Me.grdAV.VirtualMode = true
        '
        'picCollapseAV
        '
        Me.picCollapseAV.Image = CType(resources.GetObject("picCollapseAV.Image"),System.Drawing.Image)
        Me.picCollapseAV.Location = New System.Drawing.Point(81, 0)
        Me.picCollapseAV.Name = "picCollapseAV"
        Me.picCollapseAV.Size = New System.Drawing.Size(20, 20)
        Me.picCollapseAV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picCollapseAV.TabIndex = 1
        Me.picCollapseAV.TabStop = false
        Me.picCollapseAV.Visible = false
        '
        'picExpandAV
        '
        Me.picExpandAV.Image = CType(resources.GetObject("picExpandAV.Image"),System.Drawing.Image)
        Me.picExpandAV.Location = New System.Drawing.Point(81, 0)
        Me.picExpandAV.Name = "picExpandAV"
        Me.picExpandAV.Size = New System.Drawing.Size(16, 16)
        Me.picExpandAV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picExpandAV.TabIndex = 0
        Me.picExpandAV.TabStop = false
        '
        'grpBudget
        '
        Me.grpBudget.Controls.Add(Me.cmdLockOnBudget)
        Me.grpBudget.Controls.Add(Me.cmdEditCTC)
        Me.grpBudget.Controls.Add(Me.lblCTC)
        Me.grpBudget.Controls.Add(Me.lblCTCLabel)
        Me.grpBudget.Controls.Add(Me.grdBudget)
        Me.grpBudget.Location = New System.Drawing.Point(8, 502)
        Me.grpBudget.Name = "grpBudget"
        Me.grpBudget.Size = New System.Drawing.Size(423, 93)
        Me.grpBudget.TabIndex = 11
        Me.grpBudget.TabStop = false
        Me.grpBudget.Text = "Budget"
        '
        'cmdLockOnBudget
        '
        Me.cmdLockOnBudget.Image = Global.clTrinity.My.Resources.Resources.lock_2_small
        Me.cmdLockOnBudget.Location = New System.Drawing.Point(313, 18)
        Me.cmdLockOnBudget.Name = "cmdLockOnBudget"
        Me.cmdLockOnBudget.Size = New System.Drawing.Size(22, 23)
        Me.cmdLockOnBudget.TabIndex = 22
        Me.cmdLockOnBudget.UseVisualStyleBackColor = true
        '
        'cmdEditCTC
        '
        Me.cmdEditCTC.FlatAppearance.BorderSize = 0
        Me.cmdEditCTC.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdEditCTC.Image = CType(resources.GetObject("cmdEditCTC.Image"),System.Drawing.Image)
        Me.cmdEditCTC.Location = New System.Drawing.Point(313, 58)
        Me.cmdEditCTC.Name = "cmdEditCTC"
        Me.cmdEditCTC.Size = New System.Drawing.Size(22, 20)
        Me.cmdEditCTC.TabIndex = 17
        Me.cmdEditCTC.UseVisualStyleBackColor = true
        '
        'lblCTC
        '
        Me.lblCTC.AutoSize = true
        Me.lblCTC.ForeColor = System.Drawing.Color.Lime
        Me.lblCTC.Location = New System.Drawing.Point(282, 61)
        Me.lblCTC.Name = "lblCTC"
        Me.lblCTC.Size = New System.Drawing.Size(25, 14)
        Me.lblCTC.TabIndex = 6
        Me.lblCTC.Text = "0 kr"
        '
        'lblCTCLabel
        '
        Me.lblCTCLabel.AutoSize = true
        Me.lblCTCLabel.ForeColor = System.Drawing.Color.Blue
        Me.lblCTCLabel.Location = New System.Drawing.Point(246, 61)
        Me.lblCTCLabel.Name = "lblCTCLabel"
        Me.lblCTCLabel.Size = New System.Drawing.Size(30, 14)
        Me.lblCTCLabel.TabIndex = 5
        Me.lblCTCLabel.Text = "CTC:"
        '
        'grdBudget
        '
        Me.grdBudget.AllowUserToAddRows = false
        Me.grdBudget.AllowUserToDeleteRows = false
        Me.grdBudget.AllowUserToResizeColumns = false
        Me.grdBudget.AllowUserToResizeRows = false
        Me.grdBudget.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdBudget.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTRP, Me.colBudget})
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle22.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle22.Format = "N0"
        DataGridViewCellStyle22.NullValue = "0"
        DataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdBudget.DefaultCellStyle = DataGridViewCellStyle22
        Me.grdBudget.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.grdBudget.Location = New System.Drawing.Point(71, 18)
        Me.grdBudget.Name = "grdBudget"
        Me.grdBudget.RowHeadersVisible = false
        Me.grdBudget.Size = New System.Drawing.Size(241, 41)
        Me.grdBudget.TabIndex = 4
        Me.grdBudget.VirtualMode = true
        '
        'colTRP
        '
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle20.Format = "P0"
        DataGridViewCellStyle20.NullValue = "0"
        Me.colTRP.DefaultCellStyle = DataGridViewCellStyle20
        Me.colTRP.HeaderText = "TRP"
        Me.colTRP.Name = "colTRP"
        Me.colTRP.ReadOnly = true
        Me.colTRP.Width = 45
        '
        'colBudget
        '
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle21.Format = "P0"
        DataGridViewCellStyle21.NullValue = "0"
        Me.colBudget.DefaultCellStyle = DataGridViewCellStyle21
        Me.colBudget.HeaderText = "Budget"
        Me.colBudget.Name = "colBudget"
        Me.colBudget.ReadOnly = true
        Me.colBudget.Width = 45
        '
        'tpReach
        '
        Me.tpReach.Controls.Add(Me.grdReach)
        Me.tpReach.Controls.Add(Me.cmbFreq)
        Me.tpReach.Controls.Add(Me.Label1)
        Me.tpReach.Controls.Add(Me.GroupBox1)
        Me.tpReach.Controls.Add(Me.chtKarma)
        Me.tpReach.Controls.Add(Me.cmdCalculate)
        Me.tpReach.Location = New System.Drawing.Point(4, 23)
        Me.tpReach.Name = "tpReach"
        Me.tpReach.Padding = New System.Windows.Forms.Padding(3)
        Me.tpReach.Size = New System.Drawing.Size(985, 534)
        Me.tpReach.TabIndex = 1
        Me.tpReach.Text = "Reach"
        Me.tpReach.UseVisualStyleBackColor = true
        '
        'grdReach
        '
        Me.grdReach.AllowUserToAddRows = false
        Me.grdReach.AllowUserToDeleteRows = false
        Me.grdReach.AllowUserToResizeColumns = false
        Me.grdReach.AllowUserToResizeRows = false
        Me.grdReach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdReach.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdReach.Location = New System.Drawing.Point(237, 6)
        Me.grdReach.Name = "grdReach"
        Me.grdReach.ReadOnly = true
        Me.grdReach.Size = New System.Drawing.Size(144, 88)
        Me.grdReach.TabIndex = 5
        '
        'cmbFreq
        '
        Me.cmbFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFreq.FormattingEnabled = true
        Me.cmbFreq.Items.AddRange(New Object() {"1+", "2+", "3+", "4+", "5+"})
        Me.cmbFreq.Location = New System.Drawing.Point(163, 104)
        Me.cmbFreq.Name = "cmbFreq"
        Me.cmbFreq.Size = New System.Drawing.Size(68, 22)
        Me.cmbFreq.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(95, 107)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 14)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Frequency:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dtCustomDate)
        Me.GroupBox1.Controls.Add(Me.optCustomDate)
        Me.GroupBox1.Controls.Add(Me.optLastYear)
        Me.GroupBox1.Controls.Add(Me.optLastWeeks)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(223, 88)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Reference period"
        '
        'dtCustomDate
        '
        Me.dtCustomDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtCustomDate.Location = New System.Drawing.Point(112, 61)
        Me.dtCustomDate.Name = "dtCustomDate"
        Me.dtCustomDate.ShowWeekNumbers = true
        Me.dtCustomDate.Size = New System.Drawing.Size(80, 20)
        Me.dtCustomDate.TabIndex = 3
        Me.dtCustomDate.Visible = false
        '
        'optCustomDate
        '
        Me.optCustomDate.AutoSize = true
        Me.optCustomDate.Enabled = false
        Me.optCustomDate.Location = New System.Drawing.Point(6, 62)
        Me.optCustomDate.Name = "optCustomDate"
        Me.optCustomDate.Size = New System.Drawing.Size(109, 18)
        Me.optCustomDate.TabIndex = 2
        Me.optCustomDate.Text = "Use custom date:"
        Me.optCustomDate.UseVisualStyleBackColor = true
        '
        'optLastYear
        '
        Me.optLastYear.AutoSize = true
        Me.optLastYear.Location = New System.Drawing.Point(6, 40)
        Me.optLastYear.Name = "optLastYear"
        Me.optLastYear.Size = New System.Drawing.Size(151, 18)
        Me.optLastYear.TabIndex = 1
        Me.optLastYear.Text = "Use same period last year"
        Me.optLastYear.UseVisualStyleBackColor = true
        '
        'optLastWeeks
        '
        Me.optLastWeeks.AutoSize = true
        Me.optLastWeeks.Checked = true
        Me.optLastWeeks.Location = New System.Drawing.Point(6, 18)
        Me.optLastWeeks.Name = "optLastWeeks"
        Me.optLastWeeks.Size = New System.Drawing.Size(137, 18)
        Me.optLastWeeks.TabIndex = 0
        Me.optLastWeeks.TabStop = true
        Me.optLastWeeks.Text = "Use last weeks of data"
        Me.optLastWeeks.UseVisualStyleBackColor = true
        '
        'chtKarma
        '
        Me.chtKarma.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chtKarma.BackColor = System.Drawing.Color.White
        Me.chtKarma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.chtKarma.Campaigns = Nothing
        Me.chtKarma.ChartColors = CType(resources.GetObject("chtKarma.ChartColors"),System.Collections.Generic.List(Of System.Drawing.Color))
        Me.chtKarma.Cursor = System.Windows.Forms.Cursors.Default
        Me.chtKarma.DrawFrequency = 0
        Me.chtKarma.Karma = Nothing
        Me.chtKarma.Location = New System.Drawing.Point(6, 252)
        Me.chtKarma.Name = "chtKarma"
        Me.chtKarma.Size = New System.Drawing.Size(968, 279)
        Me.chtKarma.TabIndex = 0
        Me.chtKarma.TotalTRP = 500!
        '
        'cmdCalculate
        '
        Me.cmdCalculate.Image = Global.clTrinity.My.Resources.Resources.calculator_2_small
        Me.cmdCalculate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCalculate.Location = New System.Drawing.Point(14, 100)
        Me.cmdCalculate.Name = "cmdCalculate"
        Me.cmdCalculate.Size = New System.Drawing.Size(75, 26)
        Me.cmdCalculate.TabIndex = 2
        Me.cmdCalculate.Text = "Calculate"
        Me.cmdCalculate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdCalculate.UseVisualStyleBackColor = true
        '
        'tpProfile
        '
        Me.tpProfile.Controls.Add(Me.Label7)
        Me.tpProfile.Controls.Add(Me.cmbProfileCampaign)
        Me.tpProfile.Controls.Add(Me.chtProfile)
        Me.tpProfile.Location = New System.Drawing.Point(4, 23)
        Me.tpProfile.Name = "tpProfile"
        Me.tpProfile.Padding = New System.Windows.Forms.Padding(3)
        Me.tpProfile.Size = New System.Drawing.Size(985, 534)
        Me.tpProfile.TabIndex = 2
        Me.tpProfile.Text = "Profile"
        Me.tpProfile.UseVisualStyleBackColor = true
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.Location = New System.Drawing.Point(8, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 14)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Campaign"
        '
        'cmbProfileCampaign
        '
        Me.cmbProfileCampaign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProfileCampaign.FormattingEnabled = true
        Me.cmbProfileCampaign.Location = New System.Drawing.Point(68, 6)
        Me.cmbProfileCampaign.Name = "cmbProfileCampaign"
        Me.cmbProfileCampaign.Size = New System.Drawing.Size(168, 22)
        Me.cmbProfileCampaign.TabIndex = 1
        '
        'chtProfile
        '
        Me.chtProfile.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chtProfile.AverageRating = 0!
        Me.chtProfile.BackColor = System.Drawing.Color.White
        Me.chtProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.chtProfile.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.chtProfile.Location = New System.Drawing.Point(8, 32)
        Me.chtProfile.Name = "chtProfile"
        Me.chtProfile.ShowAverageRating = true
        Me.chtProfile.Size = New System.Drawing.Size(971, 469)
        Me.chtProfile.TabIndex = 0
        Me.chtProfile.Target = Nothing
        Me.chtProfile.Text = "ProfileChart1"
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Main"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn5.Width = 60
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle23.Format = "P0"
        DataGridViewCellStyle23.NullValue = "0"
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle23
        Me.DataGridViewTextBoxColumn4.HeaderText = "Budget"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = true
        Me.DataGridViewTextBoxColumn4.Width = 34
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle24.Format = "P0"
        DataGridViewCellStyle24.NullValue = "0"
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle24
        Me.DataGridViewTextBoxColumn3.HeaderText = "TRP"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = true
        Me.DataGridViewTextBoxColumn3.Width = 34
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "Third"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn7.Width = 60
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Sec"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn6.Width = 60
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Chn"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 50
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle25
        Me.DataGridViewTextBoxColumn1.HeaderText = "Nat"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'frmLab
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = true
        Me.ClientSize = New System.Drawing.Size(992, 563)
        Me.Controls.Add(Me.tabLab)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmLab"
        Me.Text = " "
        Me.mnuSetup.ResumeLayout(false)
        Me.mnuFindReach.ResumeLayout(false)
        Me.tabLab.ResumeLayout(false)
        Me.tpCampaigns.ResumeLayout(false)
        Me.tpCampaigns.PerformLayout
        Me.grpCompensation.ResumeLayout(false)
        Me.grpCompensation.PerformLayout
        CType(Me.grdCompensation,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpLoading.ResumeLayout(false)
        Me.grpLoading.PerformLayout
        CType(Me.grdLoading,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picCollapseLoading,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picExpandLoading,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpFindReach.ResumeLayout(false)
        Me.grpFindReach.PerformLayout
        Me.grpIndex.ResumeLayout(false)
        CType(Me.grdIndex,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpFilms.ResumeLayout(false)
        Me.grpFilms.PerformLayout
        Me.pnlChoose.ResumeLayout(false)
        Me.pnlChoose.PerformLayout
        CType(Me.PictureBox4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdFilms,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox2,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpTRP.ResumeLayout(false)
        Me.grpTRP.PerformLayout
        CType(Me.grdGrandSum,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdSumChannels,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdSumWeeks,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdTRP,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpDiscounts.ResumeLayout(false)
        Me.grpDiscounts.PerformLayout
        CType(Me.grdDiscounts,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picCollapseDiscounts,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picExpandDiscounts,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpAV.ResumeLayout(false)
        Me.grpAV.PerformLayout
        CType(Me.grdAV,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picCollapseAV,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picExpandAV,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpBudget.ResumeLayout(false)
        Me.grpBudget.PerformLayout
        CType(Me.grdBudget,System.ComponentModel.ISupportInitialize).EndInit
        Me.tpReach.ResumeLayout(false)
        Me.tpReach.PerformLayout
        CType(Me.grdReach,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.tpProfile.ResumeLayout(false)
        Me.tpProfile.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents tabLab As clTrinity.ExtendedTabControl
    Friend WithEvents tpCampaigns As System.Windows.Forms.TabPage
    Friend WithEvents tpReach As System.Windows.Forms.TabPage
    Friend WithEvents tpProfile As System.Windows.Forms.TabPage
    Friend WithEvents cmbCampaigns As System.Windows.Forms.ComboBox
    Friend WithEvents grpIndex As System.Windows.Forms.GroupBox
    Friend WithEvents cmdIndexSettings As System.Windows.Forms.Button
    Friend WithEvents cmdNaturalDelivery As System.Windows.Forms.Button
    Friend WithEvents grdIndex As System.Windows.Forms.DataGridView
    Friend WithEvents colMainTarget As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSecTarget As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAllAdults As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents grpFilms As System.Windows.Forms.GroupBox
    Friend WithEvents grdFilms As System.Windows.Forms.DataGridView
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdCopyToAll As System.Windows.Forms.Button
    Friend WithEvents cmbFilmChannel As System.Windows.Forms.ComboBox
    Friend WithEvents grpTRP As System.Windows.Forms.GroupBox
    Friend WithEvents grdSumChannels As System.Windows.Forms.DataGridView
    Friend WithEvents grdSumWeeks As System.Windows.Forms.DataGridView
    Friend WithEvents cmbDisplay As System.Windows.Forms.ComboBox
    Friend WithEvents cmbUniverse As System.Windows.Forms.ComboBox
    Friend WithEvents grdTRP As System.Windows.Forms.DataGridView
    Friend WithEvents grpDiscounts As System.Windows.Forms.GroupBox
    Friend WithEvents grdDiscounts As System.Windows.Forms.DataGridView
    Friend WithEvents picCollapseDiscounts As System.Windows.Forms.PictureBox
    Friend WithEvents picExpandDiscounts As System.Windows.Forms.PictureBox
    Friend WithEvents grpAV As System.Windows.Forms.GroupBox
    Friend WithEvents grdAV As System.Windows.Forms.DataGridView
    Friend WithEvents picCollapseAV As System.Windows.Forms.PictureBox
    Friend WithEvents picExpandAV As System.Windows.Forms.PictureBox
    Friend WithEvents grpBudget As System.Windows.Forms.GroupBox
    Friend WithEvents lblCTC As System.Windows.Forms.Label
    Friend WithEvents lblCTCLabel As System.Windows.Forms.Label
    Friend WithEvents grdBudget As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnuLastWeeks As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSetup As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuLastYear As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdAddCampaign As System.Windows.Forms.Button
    Friend WithEvents chtKarma As clTrinity.Charts
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtCustomDate As clTrinity.ExtendedDateTimePicker
    Friend WithEvents optCustomDate As System.Windows.Forms.RadioButton
    Friend WithEvents optLastYear As System.Windows.Forms.RadioButton
    Friend WithEvents optLastWeeks As System.Windows.Forms.RadioButton
    Friend WithEvents cmbFreq As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdCalculate As System.Windows.Forms.Button
    Friend WithEvents grdReach As System.Windows.Forms.DataGridView
    Friend WithEvents grdGrandSum As System.Windows.Forms.DataGridView
    Friend WithEvents colGrandSum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpFindReach As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbTRPBudget As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSteps As System.Windows.Forms.TextBox
    Friend WithEvents cmdGo As System.Windows.Forms.Button
    Friend WithEvents lblCurrentReach As System.Windows.Forms.Label
    Friend WithEvents txtTolerance As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbFF As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtReach As System.Windows.Forms.TextBox
    Friend WithEvents mnuFindReach As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuFindReachLastWeeks As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFindReachLastYear As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chtProfile As clTrinity.ProfileChart
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbProfileCampaign As System.Windows.Forms.ComboBox
    Friend WithEvents colSumNat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSumChn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdDeleteCampaign As System.Windows.Forms.Button
    Friend WithEvents pnlChoose As System.Windows.Forms.Panel
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents btnBudget As System.Windows.Forms.RadioButton
    Friend WithEvents btnTRP As System.Windows.Forms.RadioButton
    Friend WithEvents lblExplain As System.Windows.Forms.Label
    Friend WithEvents cmbTargets As System.Windows.Forms.ComboBox
    Friend WithEvents cmdEditCTC As System.Windows.Forms.Button
    Friend WithEvents colTRP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBudget As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpLoading As System.Windows.Forms.GroupBox
    Friend WithEvents picCollapseLoading As System.Windows.Forms.PictureBox
    Friend WithEvents picExpandLoading As System.Windows.Forms.PictureBox
    Friend WithEvents cmdApplyLoading As System.Windows.Forms.Button
    Friend WithEvents grdLoading As System.Windows.Forms.DataGridView
    Friend WithEvents cmbLoading As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmdResetLoading As System.Windows.Forms.Button
    Friend WithEvents cmbChannelLoading As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents cmdSetup As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmdEditName As System.Windows.Forms.Button
    Friend WithEvents grpCompensation As System.Windows.Forms.GroupBox
    Friend WithEvents chkIncludeInSums As System.Windows.Forms.CheckBox
    Friend WithEvents cmdRemoveChannel As System.Windows.Forms.Button
    Friend WithEvents cmdAddChannel As System.Windows.Forms.Button
    Friend WithEvents grdCompensation As System.Windows.Forms.DataGridView
    Friend WithEvents colChannel As clTrinity.ExtendedComboboxColumn
    Friend WithEvents colFrom As clTrinity.CalendarColumn
    Friend WithEvents colTo As clTrinity.CalendarColumn
    Friend WithEvents colTRPs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colExpense As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colComment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdLockOnTRP As System.Windows.Forms.Button
    Friend WithEvents cmdLockOnBudget As System.Windows.Forms.Button
End Class
