<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSetup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetup))
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.mnuArea = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuContract = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuNoContract = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewContract = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOpenContract = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuEditContract = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdContract = New System.Windows.Forms.Button()
        Me.cmdDeleteCost = New System.Windows.Forms.Button()
        Me.cmdAddCost = New System.Windows.Forms.Button()
        Me.cmdEditProduct = New System.Windows.Forms.Button()
        Me.cmdAddProduct = New System.Windows.Forms.Button()
        Me.cmdEditClient = New System.Windows.Forms.Button()
        Me.cmdAddClient = New System.Windows.Forms.Button()
        Me.cmdPeriod = New System.Windows.Forms.Button()
        Me.cmdCountry = New System.Windows.Forms.Button()
        Me.cmdAddChannelWizard = New System.Windows.Forms.Button()
        Me.cmdQuickCopy = New System.Windows.Forms.Button()
        Me.cmdCalculateDayparts = New System.Windows.Forms.Button()
        Me.cmdRemoveChannel = New System.Windows.Forms.Button()
        Me.cmdAddChannel = New System.Windows.Forms.Button()
        Me.cmdComboND = New System.Windows.Forms.Button()
        Me.cmdDeleteChannelFromCombo = New System.Windows.Forms.Button()
        Me.cmdAddChannelToCombo = New System.Windows.Forms.Button()
        Me.cmdDeleteCombo = New System.Windows.Forms.Button()
        Me.cmdAddCombo = New System.Windows.Forms.Button()
        Me.chkAutoFilmCode = New System.Windows.Forms.CheckBox()
        Me.chkFilmIdxAsDiscount = New System.Windows.Forms.CheckBox()
        Me.cmdSaveToAdtoox = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdPlayMovie = New System.Windows.Forms.Button()
        Me.cmdFindInAdtoox = New System.Windows.Forms.Button()
        Me.cmdFind = New System.Windows.Forms.Button()
        Me.cmdRemoveFilm = New System.Windows.Forms.Button()
        Me.cmdAddFilm = New System.Windows.Forms.Button()
        Me.cmdAddCopy = New System.Windows.Forms.Button()
        Me.cmdCopyIndex = New System.Windows.Forms.Button()
        Me.cmdEditEnhancement = New System.Windows.Forms.Button()
        Me.cmdRemoveIndex = New System.Windows.Forms.Button()
        Me.cmdAddIndex = New System.Windows.Forms.Button()
        Me.cmdCopyAV = New System.Windows.Forms.Button()
        Me.cmdRemoveAV = New System.Windows.Forms.Button()
        Me.cmdAddAV = New System.Windows.Forms.Button()
        Me.mnuCalculateDaypart = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuLastWeek = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLastYear = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCalculcateComboND = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuLastWeekND = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSamePeriodND = New System.Windows.Forms.ToolStripMenuItem()
        Me.UseMTGChannelSplitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFF36810MTVCC = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuUseActualTRPs = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmrKeyPressTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tabSetup = New clTrinity.ExtendedTabControl()
        Me.tpGeneral = New System.Windows.Forms.TabPage()
        Me.lblRestrictedClientBool = New System.Windows.Forms.Label()
        Me.cmdGeneralNext = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grdCosts = New System.Windows.Forms.DataGridView()
        Me.colCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colType = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.colAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCostOn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.colMarathonID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblCurrency = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtBudget = New System.Windows.Forms.TextBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblThirdSize = New System.Windows.Forms.Label()
        Me.txtThird = New System.Windows.Forms.TextBox()
        Me.cmbThirdUni = New System.Windows.Forms.ComboBox()
        Me.lblThirdTarget = New System.Windows.Forms.Label()
        Me.lblSecondSize = New System.Windows.Forms.Label()
        Me.txtSec = New System.Windows.Forms.TextBox()
        Me.cmbSecondUni = New System.Windows.Forms.ComboBox()
        Me.lblSecondTarget = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblMainSize = New System.Windows.Forms.Label()
        Me.txtMain = New System.Windows.Forms.TextBox()
        Me.cmbMainUni = New System.Windows.Forms.ComboBox()
        Me.lblMainTarget = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.cmbPlanner = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbBuyer = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbProduct = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbClient = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblContract = New System.Windows.Forms.Label()
        Me.lblPeriod = New System.Windows.Forms.Label()
        Me.lblArea = New System.Windows.Forms.Label()
        Me.tpChannels = New System.Windows.Forms.TabPage()
        Me.lblOldPricelist = New System.Windows.Forms.Label()
        Me.cmdChannelsNext = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lblNetCPP = New System.Windows.Forms.Label()
        Me.lblGrossCPP = New System.Windows.Forms.Label()
        Me.grdChannelInfo = New System.Windows.Forms.DataGridView()
        Me.colInfoChannel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colInfoNat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colInfoChn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmbCPT = New System.Windows.Forms.ComboBox()
        Me.cmdQuickAdd = New System.Windows.Forms.Button()
        Me.grdChannels = New System.Windows.Forms.DataGridView()
        Me.colChannel = New clTrinity.ExtendedComboboxColumn()
        Me.colBuyingTarget = New clTrinity.ExtendedComboboxColumn()
        Me.colCPT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMax = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.tpCombinations = New System.Windows.Forms.TabPage()
        Me.cmdCombinationsNext = New System.Windows.Forms.Button()
        Me.grpCombo = New System.Windows.Forms.GroupBox()
        Me.chkSendAsUnitMarathon = New System.Windows.Forms.CheckBox()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtMarathonIDCombo = New System.Windows.Forms.TextBox()
        Me.chkPrintAsOne = New System.Windows.Forms.CheckBox()
        Me.chkShowAsOne = New System.Windows.Forms.CheckBox()
        Me.txtComboName = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.optTRP = New System.Windows.Forms.RadioButton()
        Me.optBudget = New System.Windows.Forms.RadioButton()
        Me.grdCombo = New System.Windows.Forms.DataGridView()
        Me.colComboChannel = New clTrinity.ExtendedComboboxColumn()
        Me.colRelation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpCombos = New System.Windows.Forms.GroupBox()
        Me.grdCombos = New System.Windows.Forms.DataGridView()
        Me.colComboName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChannels = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.tpFilms = New System.Windows.Forms.TabPage()
        Me.cmdFilmsNext = New System.Windows.Forms.Button()
        Me.grpFilm = New System.Windows.Forms.GroupBox()
        Me.lblIndexWarning = New System.Windows.Forms.Label()
        Me.grdFilmDetails = New System.Windows.Forms.DataGridView()
        Me.colChannelFilmcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colGrossFilmIndex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChannelIndex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chkFilmAutoIndex = New System.Windows.Forms.CheckBox()
        Me.txtFilmLength = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtFilmDescription = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtFilmName = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.grdFilms = New System.Windows.Forms.DataGridView()
        Me.colFilmName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFilmDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFilmFilmcodes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFilmLength = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.tpIndex = New System.Windows.Forms.TabPage()
        Me.lblSeasonal = New System.Windows.Forms.Label()
        Me.cmdApply = New System.Windows.Forms.Button()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.grdIndexes = New System.Windows.Forms.DataGridView()
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.colFrom = New clTrinity.CalendarColumn()
        Me.colTo = New clTrinity.CalendarColumn()
        Me.colIndexAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUse = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.chkMultiply = New System.Windows.Forms.CheckBox()
        Me.grdAddedValues = New System.Windows.Forms.DataGridView()
        Me.colAVName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colGrossIndex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNetIndex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colShowIn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.colUseAV = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cmbIndexChannel = New System.Windows.Forms.ComboBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mnuContract.SuspendLayout()
        Me.mnuCalculateDaypart.SuspendLayout()
        Me.mnuCalculcateComboND.SuspendLayout()
        Me.tabSetup.SuspendLayout()
        Me.tpGeneral.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdCosts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpChannels.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.grdChannelInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.grdChannels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpCombinations.SuspendLayout()
        Me.grpCombo.SuspendLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCombo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCombos.SuspendLayout()
        CType(Me.grdCombos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpFilms.SuspendLayout()
        Me.grpFilm.SuspendLayout()
        CType(Me.grdFilmDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.grdFilms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpIndex.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        CType(Me.grdIndexes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        CType(Me.grdAddedValues, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'mnuArea
        '
        Me.mnuArea.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuArea.Name = "ContextMenuStrip1"
        Me.mnuArea.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.mnuArea.Size = New System.Drawing.Size(61, 4)
        '
        'mnuContract
        '
        Me.mnuContract.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuContract.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNoContract, Me.mnuNewContract, Me.mnuOpenContract, Me.ToolStripSeparator1, Me.mnuEditContract})
        Me.mnuContract.Name = "mnuContract"
        Me.mnuContract.Size = New System.Drawing.Size(149, 98)
        '
        'mnuNoContract
        '
        Me.mnuNoContract.Name = "mnuNoContract"
        Me.mnuNoContract.Size = New System.Drawing.Size(148, 22)
        Me.mnuNoContract.Text = "No contract"
        '
        'mnuNewContract
        '
        Me.mnuNewContract.Image = Global.clTrinity.My.Resources.Resources.new_file_4_16x16
        Me.mnuNewContract.Name = "mnuNewContract"
        Me.mnuNewContract.Size = New System.Drawing.Size(148, 22)
        Me.mnuNewContract.Text = "New Contract"
        '
        'mnuOpenContract
        '
        Me.mnuOpenContract.Image = Global.clTrinity.My.Resources.Resources.open_2
        Me.mnuOpenContract.Name = "mnuOpenContract"
        Me.mnuOpenContract.Size = New System.Drawing.Size(148, 22)
        Me.mnuOpenContract.Text = "Open contract"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(145, 6)
        '
        'mnuEditContract
        '
        Me.mnuEditContract.Image = Global.clTrinity.My.Resources.Resources.edit_note_3
        Me.mnuEditContract.Name = "mnuEditContract"
        Me.mnuEditContract.Size = New System.Drawing.Size(148, 22)
        Me.mnuEditContract.Text = "Edit Contract"
        '
        'cmdContract
        '
        Me.cmdContract.FlatAppearance.BorderSize = 0
        Me.cmdContract.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdContract.Image = Global.clTrinity.My.Resources.Resources.handshake_2
        Me.cmdContract.Location = New System.Drawing.Point(179, 6)
        Me.cmdContract.Name = "cmdContract"
        Me.cmdContract.Size = New System.Drawing.Size(43, 40)
        Me.cmdContract.TabIndex = 2
        Me.cmdContract.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdContract, "Manage Contract")
        Me.cmdContract.UseVisualStyleBackColor = True
        '
        'cmdDeleteCost
        '
        Me.cmdDeleteCost.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDeleteCost.FlatAppearance.BorderSize = 0
        Me.cmdDeleteCost.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDeleteCost.Image = Global.clTrinity.My.Resources.Resources.delete_3
        Me.cmdDeleteCost.Location = New System.Drawing.Point(588, 81)
        Me.cmdDeleteCost.Name = "cmdDeleteCost"
        Me.cmdDeleteCost.Size = New System.Drawing.Size(24, 28)
        Me.cmdDeleteCost.TabIndex = 12
        Me.ToolTip.SetToolTip(Me.cmdDeleteCost, "Delete cost")
        Me.cmdDeleteCost.UseVisualStyleBackColor = True
        '
        'cmdAddCost
        '
        Me.cmdAddCost.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddCost.FlatAppearance.BorderSize = 0
        Me.cmdAddCost.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddCost.Image = CType(resources.GetObject("cmdAddCost.Image"), System.Drawing.Image)
        Me.cmdAddCost.Location = New System.Drawing.Point(592, 58)
        Me.cmdAddCost.Name = "cmdAddCost"
        Me.cmdAddCost.Size = New System.Drawing.Size(18, 17)
        Me.cmdAddCost.TabIndex = 11
        Me.ToolTip.SetToolTip(Me.cmdAddCost, "Add a cost to Budget")
        Me.cmdAddCost.UseVisualStyleBackColor = True
        '
        'cmdEditProduct
        '
        Me.cmdEditProduct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdEditProduct.FlatAppearance.BorderSize = 0
        Me.cmdEditProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdEditProduct.Image = CType(resources.GetObject("cmdEditProduct.Image"), System.Drawing.Image)
        Me.cmdEditProduct.Location = New System.Drawing.Point(611, 101)
        Me.cmdEditProduct.Name = "cmdEditProduct"
        Me.cmdEditProduct.Size = New System.Drawing.Size(22, 20)
        Me.cmdEditProduct.TabIndex = 15
        Me.ToolTip.SetToolTip(Me.cmdEditProduct, "Edit Product")
        Me.cmdEditProduct.UseVisualStyleBackColor = True
        '
        'cmdAddProduct
        '
        Me.cmdAddProduct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddProduct.FlatAppearance.BorderSize = 0
        Me.cmdAddProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddProduct.Image = CType(resources.GetObject("cmdAddProduct.Image"), System.Drawing.Image)
        Me.cmdAddProduct.Location = New System.Drawing.Point(588, 105)
        Me.cmdAddProduct.Name = "cmdAddProduct"
        Me.cmdAddProduct.Size = New System.Drawing.Size(18, 17)
        Me.cmdAddProduct.TabIndex = 14
        Me.ToolTip.SetToolTip(Me.cmdAddProduct, "Add Product")
        Me.cmdAddProduct.UseVisualStyleBackColor = True
        '
        'cmdEditClient
        '
        Me.cmdEditClient.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdEditClient.FlatAppearance.BorderSize = 0
        Me.cmdEditClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdEditClient.Image = CType(resources.GetObject("cmdEditClient.Image"), System.Drawing.Image)
        Me.cmdEditClient.Location = New System.Drawing.Point(612, 61)
        Me.cmdEditClient.Name = "cmdEditClient"
        Me.cmdEditClient.Size = New System.Drawing.Size(22, 20)
        Me.cmdEditClient.TabIndex = 11
        Me.ToolTip.SetToolTip(Me.cmdEditClient, "Edit Client")
        Me.cmdEditClient.UseVisualStyleBackColor = True
        '
        'cmdAddClient
        '
        Me.cmdAddClient.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddClient.FlatAppearance.BorderSize = 0
        Me.cmdAddClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddClient.Image = CType(resources.GetObject("cmdAddClient.Image"), System.Drawing.Image)
        Me.cmdAddClient.Location = New System.Drawing.Point(588, 65)
        Me.cmdAddClient.Name = "cmdAddClient"
        Me.cmdAddClient.Size = New System.Drawing.Size(18, 17)
        Me.cmdAddClient.TabIndex = 10
        Me.ToolTip.SetToolTip(Me.cmdAddClient, "Add Client")
        Me.cmdAddClient.UseVisualStyleBackColor = True
        '
        'cmdPeriod
        '
        Me.cmdPeriod.FlatAppearance.BorderSize = 0
        Me.cmdPeriod.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPeriod.Image = Global.clTrinity.My.Resources.Resources.calender_2_32x32
        Me.cmdPeriod.Location = New System.Drawing.Point(3, 45)
        Me.cmdPeriod.Name = "cmdPeriod"
        Me.cmdPeriod.Size = New System.Drawing.Size(51, 51)
        Me.cmdPeriod.TabIndex = 1
        Me.cmdPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdPeriod, "Set period for campaign")
        Me.cmdPeriod.UseVisualStyleBackColor = True
        '
        'cmdCountry
        '
        Me.cmdCountry.FlatAppearance.BorderSize = 0
        Me.cmdCountry.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCountry.Image = CType(resources.GetObject("cmdCountry.Image"), System.Drawing.Image)
        Me.cmdCountry.Location = New System.Drawing.Point(7, 6)
        Me.cmdCountry.Name = "cmdCountry"
        Me.cmdCountry.Size = New System.Drawing.Size(43, 40)
        Me.cmdCountry.TabIndex = 0
        Me.cmdCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdCountry, "Set area")
        Me.cmdCountry.UseVisualStyleBackColor = True
        '
        'cmdAddChannelWizard
        '
        Me.cmdAddChannelWizard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddChannelWizard.FlatAppearance.BorderSize = 0
        Me.cmdAddChannelWizard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddChannelWizard.Image = Global.clTrinity.My.Resources.Resources.add_more_2_small
        Me.cmdAddChannelWizard.Location = New System.Drawing.Point(608, 44)
        Me.cmdAddChannelWizard.Name = "cmdAddChannelWizard"
        Me.cmdAddChannelWizard.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddChannelWizard.TabIndex = 30
        Me.ToolTip.SetToolTip(Me.cmdAddChannelWizard, "Add Channel")
        Me.cmdAddChannelWizard.UseVisualStyleBackColor = True
        '
        'cmdQuickCopy
        '
        Me.cmdQuickCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdQuickCopy.FlatAppearance.BorderSize = 0
        Me.cmdQuickCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdQuickCopy.Image = Global.clTrinity.My.Resources.Resources.flash_2_small
        Me.cmdQuickCopy.Location = New System.Drawing.Point(608, 122)
        Me.cmdQuickCopy.Name = "cmdQuickCopy"
        Me.cmdQuickCopy.Size = New System.Drawing.Size(22, 20)
        Me.cmdQuickCopy.TabIndex = 29
        Me.ToolTip.SetToolTip(Me.cmdQuickCopy, "Quick copy a bookingtype")
        Me.cmdQuickCopy.UseVisualStyleBackColor = True
        '
        'cmdCalculateDayparts
        '
        Me.cmdCalculateDayparts.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCalculateDayparts.FlatAppearance.BorderSize = 0
        Me.cmdCalculateDayparts.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCalculateDayparts.Image = CType(resources.GetObject("cmdCalculateDayparts.Image"), System.Drawing.Image)
        Me.cmdCalculateDayparts.Location = New System.Drawing.Point(608, 96)
        Me.cmdCalculateDayparts.Name = "cmdCalculateDayparts"
        Me.cmdCalculateDayparts.Size = New System.Drawing.Size(22, 20)
        Me.cmdCalculateDayparts.TabIndex = 28
        Me.ToolTip.SetToolTip(Me.cmdCalculateDayparts, "Get Calculate daypart split")
        Me.cmdCalculateDayparts.UseVisualStyleBackColor = True
        '
        'cmdRemoveChannel
        '
        Me.cmdRemoveChannel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveChannel.FlatAppearance.BorderSize = 0
        Me.cmdRemoveChannel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRemoveChannel.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdRemoveChannel.Location = New System.Drawing.Point(608, 70)
        Me.cmdRemoveChannel.Name = "cmdRemoveChannel"
        Me.cmdRemoveChannel.Size = New System.Drawing.Size(22, 20)
        Me.cmdRemoveChannel.TabIndex = 14
        Me.ToolTip.SetToolTip(Me.cmdRemoveChannel, "Delete Channel")
        Me.cmdRemoveChannel.UseVisualStyleBackColor = True
        '
        'cmdAddChannel
        '
        Me.cmdAddChannel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddChannel.FlatAppearance.BorderSize = 0
        Me.cmdAddChannel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddChannel.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.cmdAddChannel.Location = New System.Drawing.Point(608, 18)
        Me.cmdAddChannel.Name = "cmdAddChannel"
        Me.cmdAddChannel.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddChannel.TabIndex = 13
        Me.ToolTip.SetToolTip(Me.cmdAddChannel, "Add Channel")
        Me.cmdAddChannel.UseVisualStyleBackColor = True
        '
        'cmdComboND
        '
        Me.cmdComboND.FlatAppearance.BorderSize = 0
        Me.cmdComboND.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdComboND.Image = CType(resources.GetObject("cmdComboND.Image"), System.Drawing.Image)
        Me.cmdComboND.Location = New System.Drawing.Point(311, 84)
        Me.cmdComboND.Name = "cmdComboND"
        Me.cmdComboND.Size = New System.Drawing.Size(22, 20)
        Me.cmdComboND.TabIndex = 30
        Me.ToolTip.SetToolTip(Me.cmdComboND, "Get natural delivery split")
        Me.cmdComboND.UseVisualStyleBackColor = True
        '
        'cmdDeleteChannelFromCombo
        '
        Me.cmdDeleteChannelFromCombo.FlatAppearance.BorderSize = 0
        Me.cmdDeleteChannelFromCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDeleteChannelFromCombo.Image = CType(resources.GetObject("cmdDeleteChannelFromCombo.Image"), System.Drawing.Image)
        Me.cmdDeleteChannelFromCombo.Location = New System.Drawing.Point(311, 58)
        Me.cmdDeleteChannelFromCombo.Name = "cmdDeleteChannelFromCombo"
        Me.cmdDeleteChannelFromCombo.Size = New System.Drawing.Size(22, 20)
        Me.cmdDeleteChannelFromCombo.TabIndex = 23
        Me.ToolTip.SetToolTip(Me.cmdDeleteChannelFromCombo, "Delete Channel")
        Me.cmdDeleteChannelFromCombo.UseVisualStyleBackColor = True
        '
        'cmdAddChannelToCombo
        '
        Me.cmdAddChannelToCombo.FlatAppearance.BorderSize = 0
        Me.cmdAddChannelToCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddChannelToCombo.Image = CType(resources.GetObject("cmdAddChannelToCombo.Image"), System.Drawing.Image)
        Me.cmdAddChannelToCombo.Location = New System.Drawing.Point(311, 32)
        Me.cmdAddChannelToCombo.Name = "cmdAddChannelToCombo"
        Me.cmdAddChannelToCombo.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddChannelToCombo.TabIndex = 22
        Me.ToolTip.SetToolTip(Me.cmdAddChannelToCombo, "Add Channel")
        Me.cmdAddChannelToCombo.UseVisualStyleBackColor = True
        '
        'cmdDeleteCombo
        '
        Me.cmdDeleteCombo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDeleteCombo.FlatAppearance.BorderSize = 0
        Me.cmdDeleteCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDeleteCombo.Image = CType(resources.GetObject("cmdDeleteCombo.Image"), System.Drawing.Image)
        Me.cmdDeleteCombo.Location = New System.Drawing.Point(614, 44)
        Me.cmdDeleteCombo.Name = "cmdDeleteCombo"
        Me.cmdDeleteCombo.Size = New System.Drawing.Size(22, 20)
        Me.cmdDeleteCombo.TabIndex = 20
        Me.ToolTip.SetToolTip(Me.cmdDeleteCombo, "Delete Combination")
        Me.cmdDeleteCombo.UseVisualStyleBackColor = True
        '
        'cmdAddCombo
        '
        Me.cmdAddCombo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddCombo.FlatAppearance.BorderSize = 0
        Me.cmdAddCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddCombo.Image = CType(resources.GetObject("cmdAddCombo.Image"), System.Drawing.Image)
        Me.cmdAddCombo.Location = New System.Drawing.Point(614, 18)
        Me.cmdAddCombo.Name = "cmdAddCombo"
        Me.cmdAddCombo.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddCombo.TabIndex = 19
        Me.ToolTip.SetToolTip(Me.cmdAddCombo, "Add Combination")
        Me.cmdAddCombo.UseVisualStyleBackColor = True
        '
        'chkAutoFilmCode
        '
        Me.chkAutoFilmCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAutoFilmCode.AutoSize = True
        Me.chkAutoFilmCode.Checked = True
        Me.chkAutoFilmCode.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAutoFilmCode.Location = New System.Drawing.Point(478, 154)
        Me.chkAutoFilmCode.Name = "chkAutoFilmCode"
        Me.chkAutoFilmCode.Size = New System.Drawing.Size(130, 17)
        Me.chkAutoFilmCode.TabIndex = 16
        Me.chkAutoFilmCode.Text = "Auto copy filmcodes"
        Me.ToolTip.SetToolTip(Me.chkAutoFilmCode, "Use film index as dicount")
        Me.chkAutoFilmCode.UseVisualStyleBackColor = True
        '
        'chkFilmIdxAsDiscount
        '
        Me.chkFilmIdxAsDiscount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFilmIdxAsDiscount.AutoSize = True
        Me.chkFilmIdxAsDiscount.Location = New System.Drawing.Point(301, 155)
        Me.chkFilmIdxAsDiscount.Name = "chkFilmIdxAsDiscount"
        Me.chkFilmIdxAsDiscount.Size = New System.Drawing.Size(170, 17)
        Me.chkFilmIdxAsDiscount.TabIndex = 13
        Me.chkFilmIdxAsDiscount.Text = "Count filmindex as discount"
        Me.ToolTip.SetToolTip(Me.chkFilmIdxAsDiscount, "Use film index as dicount")
        Me.chkFilmIdxAsDiscount.UseVisualStyleBackColor = True
        '
        'cmdSaveToAdtoox
        '
        Me.cmdSaveToAdtoox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSaveToAdtoox.FlatAppearance.BorderSize = 0
        Me.cmdSaveToAdtoox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSaveToAdtoox.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.cmdSaveToAdtoox.Image = CType(resources.GetObject("cmdSaveToAdtoox.Image"), System.Drawing.Image)
        Me.cmdSaveToAdtoox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSaveToAdtoox.Location = New System.Drawing.Point(520, 7)
        Me.cmdSaveToAdtoox.Name = "cmdSaveToAdtoox"
        Me.cmdSaveToAdtoox.Size = New System.Drawing.Size(116, 20)
        Me.cmdSaveToAdtoox.TabIndex = 15
        Me.cmdSaveToAdtoox.Text = "Create filmcodes"
        Me.cmdSaveToAdtoox.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdSaveToAdtoox, "Save to E.C. Express")
        Me.cmdSaveToAdtoox.UseVisualStyleBackColor = True
        Me.cmdSaveToAdtoox.Visible = False
        '
        'cmdSave
        '
        Me.cmdSave.FlatAppearance.BorderSize = 0
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Image = Global.clTrinity.My.Resources.Resources.save_2_small
        Me.cmdSave.Location = New System.Drawing.Point(6, 147)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(22, 20)
        Me.cmdSave.TabIndex = 12
        Me.ToolTip.SetToolTip(Me.cmdSave, "Save to Library")
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdPlayMovie
        '
        Me.cmdPlayMovie.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPlayMovie.FlatAppearance.BorderSize = 0
        Me.cmdPlayMovie.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPlayMovie.Image = Global.clTrinity.My.Resources.Resources.play_2_small
        Me.cmdPlayMovie.Location = New System.Drawing.Point(614, 122)
        Me.cmdPlayMovie.Name = "cmdPlayMovie"
        Me.cmdPlayMovie.Size = New System.Drawing.Size(22, 20)
        Me.cmdPlayMovie.TabIndex = 28
        Me.ToolTip.SetToolTip(Me.cmdPlayMovie, "Play film")
        Me.cmdPlayMovie.UseVisualStyleBackColor = True
        Me.cmdPlayMovie.Visible = False
        '
        'cmdFindInAdtoox
        '
        Me.cmdFindInAdtoox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdFindInAdtoox.FlatAppearance.BorderSize = 0
        Me.cmdFindInAdtoox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFindInAdtoox.Image = CType(resources.GetObject("cmdFindInAdtoox.Image"), System.Drawing.Image)
        Me.cmdFindInAdtoox.Location = New System.Drawing.Point(614, 96)
        Me.cmdFindInAdtoox.Name = "cmdFindInAdtoox"
        Me.cmdFindInAdtoox.Size = New System.Drawing.Size(22, 20)
        Me.cmdFindInAdtoox.TabIndex = 18
        Me.ToolTip.SetToolTip(Me.cmdFindInAdtoox, "Get film(s) from your Adtoox library")
        Me.cmdFindInAdtoox.UseVisualStyleBackColor = True
        Me.cmdFindInAdtoox.Visible = False
        '
        'cmdFind
        '
        Me.cmdFind.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdFind.FlatAppearance.BorderSize = 0
        Me.cmdFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFind.Image = Global.clTrinity.My.Resources.Resources.find_2_small
        Me.cmdFind.Location = New System.Drawing.Point(614, 70)
        Me.cmdFind.Name = "cmdFind"
        Me.cmdFind.Size = New System.Drawing.Size(22, 20)
        Me.cmdFind.TabIndex = 17
        Me.ToolTip.SetToolTip(Me.cmdFind, "Get film from Library")
        Me.cmdFind.UseVisualStyleBackColor = True
        '
        'cmdRemoveFilm
        '
        Me.cmdRemoveFilm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveFilm.FlatAppearance.BorderSize = 0
        Me.cmdRemoveFilm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRemoveFilm.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdRemoveFilm.Location = New System.Drawing.Point(614, 44)
        Me.cmdRemoveFilm.Name = "cmdRemoveFilm"
        Me.cmdRemoveFilm.Size = New System.Drawing.Size(22, 20)
        Me.cmdRemoveFilm.TabIndex = 16
        Me.ToolTip.SetToolTip(Me.cmdRemoveFilm, "Delete Film")
        Me.cmdRemoveFilm.UseVisualStyleBackColor = True
        '
        'cmdAddFilm
        '
        Me.cmdAddFilm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddFilm.FlatAppearance.BorderSize = 0
        Me.cmdAddFilm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddFilm.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.cmdAddFilm.Location = New System.Drawing.Point(614, 18)
        Me.cmdAddFilm.Name = "cmdAddFilm"
        Me.cmdAddFilm.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddFilm.TabIndex = 15
        Me.ToolTip.SetToolTip(Me.cmdAddFilm, "Add Film")
        Me.cmdAddFilm.UseVisualStyleBackColor = True
        '
        'cmdAddCopy
        '
        Me.cmdAddCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddCopy.FlatAppearance.BorderSize = 0
        Me.cmdAddCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddCopy.Image = Global.clTrinity.My.Resources.Resources.add_more_2_small
        Me.cmdAddCopy.Location = New System.Drawing.Point(612, 44)
        Me.cmdAddCopy.Name = "cmdAddCopy"
        Me.cmdAddCopy.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddCopy.TabIndex = 30
        Me.ToolTip.SetToolTip(Me.cmdAddCopy, "Add copy of last Index")
        Me.cmdAddCopy.UseVisualStyleBackColor = True
        '
        'cmdCopyIndex
        '
        Me.cmdCopyIndex.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCopyIndex.FlatAppearance.BorderSize = 0
        Me.cmdCopyIndex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCopyIndex.Image = CType(resources.GetObject("cmdCopyIndex.Image"), System.Drawing.Image)
        Me.cmdCopyIndex.Location = New System.Drawing.Point(612, 96)
        Me.cmdCopyIndex.Name = "cmdCopyIndex"
        Me.cmdCopyIndex.Size = New System.Drawing.Size(22, 20)
        Me.cmdCopyIndex.TabIndex = 29
        Me.ToolTip.SetToolTip(Me.cmdCopyIndex, "Copy indexes from another channel")
        Me.cmdCopyIndex.UseVisualStyleBackColor = True
        '
        'cmdEditEnhancement
        '
        Me.cmdEditEnhancement.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdEditEnhancement.FlatAppearance.BorderSize = 0
        Me.cmdEditEnhancement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdEditEnhancement.Image = Global.clTrinity.My.Resources.Resources.edit_note_3
        Me.cmdEditEnhancement.Location = New System.Drawing.Point(612, 122)
        Me.cmdEditEnhancement.Name = "cmdEditEnhancement"
        Me.cmdEditEnhancement.Size = New System.Drawing.Size(22, 20)
        Me.cmdEditEnhancement.TabIndex = 21
        Me.ToolTip.SetToolTip(Me.cmdEditEnhancement, "Edit Enhancement")
        Me.cmdEditEnhancement.UseVisualStyleBackColor = True
        Me.cmdEditEnhancement.Visible = False
        '
        'cmdRemoveIndex
        '
        Me.cmdRemoveIndex.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveIndex.FlatAppearance.BorderSize = 0
        Me.cmdRemoveIndex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRemoveIndex.Image = CType(resources.GetObject("cmdRemoveIndex.Image"), System.Drawing.Image)
        Me.cmdRemoveIndex.Location = New System.Drawing.Point(612, 70)
        Me.cmdRemoveIndex.Name = "cmdRemoveIndex"
        Me.cmdRemoveIndex.Size = New System.Drawing.Size(22, 20)
        Me.cmdRemoveIndex.TabIndex = 18
        Me.ToolTip.SetToolTip(Me.cmdRemoveIndex, "Delete Index")
        Me.cmdRemoveIndex.UseVisualStyleBackColor = True
        '
        'cmdAddIndex
        '
        Me.cmdAddIndex.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddIndex.FlatAppearance.BorderSize = 0
        Me.cmdAddIndex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddIndex.Image = CType(resources.GetObject("cmdAddIndex.Image"), System.Drawing.Image)
        Me.cmdAddIndex.Location = New System.Drawing.Point(612, 18)
        Me.cmdAddIndex.Name = "cmdAddIndex"
        Me.cmdAddIndex.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddIndex.TabIndex = 17
        Me.ToolTip.SetToolTip(Me.cmdAddIndex, "Add Index")
        Me.cmdAddIndex.UseVisualStyleBackColor = True
        '
        'cmdCopyAV
        '
        Me.cmdCopyAV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCopyAV.FlatAppearance.BorderSize = 0
        Me.cmdCopyAV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCopyAV.Image = CType(resources.GetObject("cmdCopyAV.Image"), System.Drawing.Image)
        Me.cmdCopyAV.Location = New System.Drawing.Point(612, 70)
        Me.cmdCopyAV.Name = "cmdCopyAV"
        Me.cmdCopyAV.Size = New System.Drawing.Size(22, 20)
        Me.cmdCopyAV.TabIndex = 30
        Me.ToolTip.SetToolTip(Me.cmdCopyAV, "Copy added values from another channel")
        Me.cmdCopyAV.UseVisualStyleBackColor = True
        '
        'cmdRemoveAV
        '
        Me.cmdRemoveAV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveAV.FlatAppearance.BorderSize = 0
        Me.cmdRemoveAV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRemoveAV.Image = CType(resources.GetObject("cmdRemoveAV.Image"), System.Drawing.Image)
        Me.cmdRemoveAV.Location = New System.Drawing.Point(612, 44)
        Me.cmdRemoveAV.Name = "cmdRemoveAV"
        Me.cmdRemoveAV.Size = New System.Drawing.Size(22, 20)
        Me.cmdRemoveAV.TabIndex = 18
        Me.ToolTip.SetToolTip(Me.cmdRemoveAV, "Delete Value")
        Me.cmdRemoveAV.UseVisualStyleBackColor = True
        '
        'cmdAddAV
        '
        Me.cmdAddAV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddAV.FlatAppearance.BorderSize = 0
        Me.cmdAddAV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddAV.Image = CType(resources.GetObject("cmdAddAV.Image"), System.Drawing.Image)
        Me.cmdAddAV.Location = New System.Drawing.Point(612, 18)
        Me.cmdAddAV.Name = "cmdAddAV"
        Me.cmdAddAV.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddAV.TabIndex = 17
        Me.ToolTip.SetToolTip(Me.cmdAddAV, "Add Value")
        Me.cmdAddAV.UseVisualStyleBackColor = True
        '
        'mnuCalculateDaypart
        '
        Me.mnuCalculateDaypart.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuLastWeek, Me.mnuLastYear})
        Me.mnuCalculateDaypart.Name = "mnuCalculateDaypart"
        Me.mnuCalculateDaypart.ShowImageMargin = False
        Me.mnuCalculateDaypart.Size = New System.Drawing.Size(183, 48)
        '
        'mnuLastWeek
        '
        Me.mnuLastWeek.Name = "mnuLastWeek"
        Me.mnuLastWeek.Size = New System.Drawing.Size(182, 22)
        Me.mnuLastWeek.Tag = "0"
        Me.mnuLastWeek.Text = "Use last week of data"
        '
        'mnuLastYear
        '
        Me.mnuLastYear.Name = "mnuLastYear"
        Me.mnuLastYear.Size = New System.Drawing.Size(182, 22)
        Me.mnuLastYear.Tag = "1"
        Me.mnuLastYear.Text = "Use same period last year"
        '
        'mnuCalculcateComboND
        '
        Me.mnuCalculcateComboND.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuLastWeekND, Me.mnuSamePeriodND, Me.UseMTGChannelSplitToolStripMenuItem, Me.ToolStripMenuItem1, Me.mnuUseActualTRPs})
        Me.mnuCalculcateComboND.Name = "mnuCalculateDaypart"
        Me.mnuCalculcateComboND.ShowImageMargin = False
        Me.mnuCalculcateComboND.Size = New System.Drawing.Size(183, 98)
        '
        'mnuLastWeekND
        '
        Me.mnuLastWeekND.Name = "mnuLastWeekND"
        Me.mnuLastWeekND.Size = New System.Drawing.Size(182, 22)
        Me.mnuLastWeekND.Tag = "0"
        Me.mnuLastWeekND.Text = "Use last week of data"
        '
        'mnuSamePeriodND
        '
        Me.mnuSamePeriodND.Name = "mnuSamePeriodND"
        Me.mnuSamePeriodND.Size = New System.Drawing.Size(182, 22)
        Me.mnuSamePeriodND.Tag = "1"
        Me.mnuSamePeriodND.Text = "Use same period last year"
        '
        'UseMTGChannelSplitToolStripMenuItem
        '
        Me.UseMTGChannelSplitToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFF36810MTVCC})
        Me.UseMTGChannelSplitToolStripMenuItem.Name = "UseMTGChannelSplitToolStripMenuItem"
        Me.UseMTGChannelSplitToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.UseMTGChannelSplitToolStripMenuItem.Text = "Use MTG Channel split"
        Me.UseMTGChannelSplitToolStripMenuItem.Visible = False
        '
        'mnuFF36810MTVCC
        '
        Me.mnuFF36810MTVCC.Name = "mnuFF36810MTVCC"
        Me.mnuFF36810MTVCC.Size = New System.Drawing.Size(185, 22)
        Me.mnuFF36810MTVCC.Text = "FF 3-6-8-10-MTV-CC"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(179, 6)
        '
        'mnuUseActualTRPs
        '
        Me.mnuUseActualTRPs.Enabled = False
        Me.mnuUseActualTRPs.Name = "mnuUseActualTRPs"
        Me.mnuUseActualTRPs.Size = New System.Drawing.Size(182, 22)
        Me.mnuUseActualTRPs.Text = "Use actual TRPs"
        '
        'tmrKeyPressTimer
        '
        Me.tmrKeyPressTimer.Enabled = True
        Me.tmrKeyPressTimer.Interval = 1500
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'tabSetup
        '
        Me.tabSetup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabSetup.Controls.Add(Me.tpGeneral)
        Me.tabSetup.Controls.Add(Me.tpChannels)
        Me.tabSetup.Controls.Add(Me.tpCombinations)
        Me.tabSetup.Controls.Add(Me.tpFilms)
        Me.tabSetup.Controls.Add(Me.tpIndex)
        Me.tabSetup.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.tabSetup.Location = New System.Drawing.Point(1, 2)
        Me.tabSetup.Name = "tabSetup"
        Me.tabSetup.SelectedIndex = 0
        Me.tabSetup.Size = New System.Drawing.Size(647, 443)
        Me.tabSetup.TabIndex = 0
        '
        'tpGeneral
        '
        Me.tpGeneral.Controls.Add(Me.lblRestrictedClientBool)
        Me.tpGeneral.Controls.Add(Me.cmdContract)
        Me.tpGeneral.Controls.Add(Me.cmdGeneralNext)
        Me.tpGeneral.Controls.Add(Me.GroupBox2)
        Me.tpGeneral.Controls.Add(Me.PictureBox3)
        Me.tpGeneral.Controls.Add(Me.GroupBox1)
        Me.tpGeneral.Controls.Add(Me.PictureBox2)
        Me.tpGeneral.Controls.Add(Me.PictureBox1)
        Me.tpGeneral.Controls.Add(Me.cmbPlanner)
        Me.tpGeneral.Controls.Add(Me.Label5)
        Me.tpGeneral.Controls.Add(Me.cmbBuyer)
        Me.tpGeneral.Controls.Add(Me.Label4)
        Me.tpGeneral.Controls.Add(Me.cmdEditProduct)
        Me.tpGeneral.Controls.Add(Me.cmdAddProduct)
        Me.tpGeneral.Controls.Add(Me.cmbProduct)
        Me.tpGeneral.Controls.Add(Me.Label3)
        Me.tpGeneral.Controls.Add(Me.cmdEditClient)
        Me.tpGeneral.Controls.Add(Me.cmdAddClient)
        Me.tpGeneral.Controls.Add(Me.cmbClient)
        Me.tpGeneral.Controls.Add(Me.Label2)
        Me.tpGeneral.Controls.Add(Me.txtName)
        Me.tpGeneral.Controls.Add(Me.Label1)
        Me.tpGeneral.Controls.Add(Me.lblContract)
        Me.tpGeneral.Controls.Add(Me.lblPeriod)
        Me.tpGeneral.Controls.Add(Me.lblArea)
        Me.tpGeneral.Controls.Add(Me.cmdPeriod)
        Me.tpGeneral.Controls.Add(Me.cmdCountry)
        Me.tpGeneral.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tpGeneral.Location = New System.Drawing.Point(4, 22)
        Me.tpGeneral.Name = "tpGeneral"
        Me.tpGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.tpGeneral.Size = New System.Drawing.Size(639, 417)
        Me.tpGeneral.TabIndex = 1
        Me.tpGeneral.Text = "General"
        Me.tpGeneral.UseVisualStyleBackColor = True
        '
        'lblRestrictedClientBool
        '
        Me.lblRestrictedClientBool.AutoSize = True
        Me.lblRestrictedClientBool.Location = New System.Drawing.Point(392, 47)
        Me.lblRestrictedClientBool.Name = "lblRestrictedClientBool"
        Me.lblRestrictedClientBool.Size = New System.Drawing.Size(58, 13)
        Me.lblRestrictedClientBool.TabIndex = 26
        Me.lblRestrictedClientBool.Text = "Restricted"
        Me.lblRestrictedClientBool.Visible = False
        '
        'cmdGeneralNext
        '
        Me.cmdGeneralNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdGeneralNext.FlatAppearance.BorderSize = 0
        Me.cmdGeneralNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdGeneralNext.Location = New System.Drawing.Point(558, 381)
        Me.cmdGeneralNext.Name = "cmdGeneralNext"
        Me.cmdGeneralNext.Size = New System.Drawing.Size(75, 29)
        Me.cmdGeneralNext.TabIndex = 25
        Me.cmdGeneralNext.Text = "Next"
        Me.cmdGeneralNext.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.cmdDeleteCost)
        Me.GroupBox2.Controls.Add(Me.cmdAddCost)
        Me.GroupBox2.Controls.Add(Me.grdCosts)
        Me.GroupBox2.Controls.Add(Me.lblCurrency)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtBudget)
        Me.GroupBox2.Controls.Add(Me.PictureBox4)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 207)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(625, 158)
        Me.GroupBox2.TabIndex = 24
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Budget"
        '
        'grdCosts
        '
        Me.grdCosts.AllowUserToAddRows = False
        Me.grdCosts.AllowUserToDeleteRows = False
        Me.grdCosts.AllowUserToResizeColumns = False
        Me.grdCosts.AllowUserToResizeRows = False
        Me.grdCosts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdCosts.BackgroundColor = System.Drawing.Color.Silver
        Me.grdCosts.ColumnHeadersHeight = 26
        Me.grdCosts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdCosts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCost, Me.colType, Me.colAmount, Me.colCostOn, Me.colMarathonID})
        Me.grdCosts.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdCosts.GridColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdCosts.Location = New System.Drawing.Point(7, 58)
        Me.grdCosts.Name = "grdCosts"
        Me.grdCosts.RowHeadersVisible = False
        Me.grdCosts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdCosts.Size = New System.Drawing.Size(575, 86)
        Me.grdCosts.TabIndex = 4
        Me.grdCosts.VirtualMode = True
        '
        'colCost
        '
        Me.colCost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colCost.HeaderText = "Cost"
        Me.colCost.Name = "colCost"
        Me.colCost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colType
        '
        Me.colType.HeaderText = "Type"
        Me.colType.Items.AddRange(New Object() {"Fixed", "Percent", "Per Unit", "On Discount"})
        Me.colType.Name = "colType"
        Me.colType.Width = 92
        '
        'colAmount
        '
        DataGridViewCellStyle4.Format = "C0"
        DataGridViewCellStyle4.NullValue = "0"
        Me.colAmount.DefaultCellStyle = DataGridViewCellStyle4
        Me.colAmount.HeaderText = "Amount"
        Me.colAmount.Name = "colAmount"
        Me.colAmount.Width = 93
        '
        'colCostOn
        '
        Me.colCostOn.HeaderText = "On"
        Me.colCostOn.Items.AddRange(New Object() {"Media net", "Net", "Net Net"})
        Me.colCostOn.Name = "colCostOn"
        Me.colCostOn.Width = 92
        '
        'colMarathonID
        '
        Me.colMarathonID.HeaderText = "Marathon ID"
        Me.colMarathonID.Name = "colMarathonID"
        Me.colMarathonID.Width = 93
        '
        'lblCurrency
        '
        Me.lblCurrency.AutoSize = True
        Me.lblCurrency.Location = New System.Drawing.Point(198, 32)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(17, 13)
        Me.lblCurrency.TabIndex = 3
        Me.lblCurrency.Text = "kr"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(39, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(115, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Total TV-Budget (CTC)"
        '
        'txtBudget
        '
        Me.txtBudget.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtBudget.Location = New System.Drawing.Point(42, 30)
        Me.txtBudget.Name = "txtBudget"
        Me.txtBudget.Size = New System.Drawing.Size(154, 22)
        Me.txtBudget.TabIndex = 1
        Me.txtBudget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.clTrinity.My.Resources.Resources.money_bag_2
        Me.PictureBox4.Location = New System.Drawing.Point(7, 27)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(29, 25)
        Me.PictureBox4.TabIndex = 0
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.clTrinity.My.Resources.Resources.target_group_2
        Me.PictureBox3.Location = New System.Drawing.Point(18, 97)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(21, 20)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 23
        Me.PictureBox3.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblThirdSize)
        Me.GroupBox1.Controls.Add(Me.txtThird)
        Me.GroupBox1.Controls.Add(Me.cmbThirdUni)
        Me.GroupBox1.Controls.Add(Me.lblThirdTarget)
        Me.GroupBox1.Controls.Add(Me.lblSecondSize)
        Me.GroupBox1.Controls.Add(Me.txtSec)
        Me.GroupBox1.Controls.Add(Me.cmbSecondUni)
        Me.GroupBox1.Controls.Add(Me.lblSecondTarget)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblMainSize)
        Me.GroupBox1.Controls.Add(Me.txtMain)
        Me.GroupBox1.Controls.Add(Me.cmbMainUni)
        Me.GroupBox1.Controls.Add(Me.lblMainTarget)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 102)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(282, 98)
        Me.GroupBox1.TabIndex = 22
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "        Targets"
        '
        'lblThirdSize
        '
        Me.lblThirdSize.AutoSize = True
        Me.lblThirdSize.Location = New System.Drawing.Point(220, 74)
        Me.lblThirdSize.Name = "lblThirdSize"
        Me.lblThirdSize.Size = New System.Drawing.Size(13, 13)
        Me.lblThirdSize.TabIndex = 13
        Me.lblThirdSize.Text = "0"
        '
        'txtThird
        '
        Me.txtThird.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtThird.Location = New System.Drawing.Point(56, 71)
        Me.txtThird.Name = "txtThird"
        Me.txtThird.Size = New System.Drawing.Size(97, 22)
        Me.txtThird.TabIndex = 12
        Me.txtThird.Text = "3+"
        '
        'cmbThirdUni
        '
        Me.cmbThirdUni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbThirdUni.FormattingEnabled = True
        Me.cmbThirdUni.Location = New System.Drawing.Point(159, 71)
        Me.cmbThirdUni.Name = "cmbThirdUni"
        Me.cmbThirdUni.Size = New System.Drawing.Size(56, 21)
        Me.cmbThirdUni.TabIndex = 11
        '
        'lblThirdTarget
        '
        Me.lblThirdTarget.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblThirdTarget.Location = New System.Drawing.Point(7, 73)
        Me.lblThirdTarget.Name = "lblThirdTarget"
        Me.lblThirdTarget.Size = New System.Drawing.Size(46, 15)
        Me.lblThirdTarget.TabIndex = 10
        Me.lblThirdTarget.Text = "Third"
        '
        'lblSecondSize
        '
        Me.lblSecondSize.AutoSize = True
        Me.lblSecondSize.Location = New System.Drawing.Point(220, 51)
        Me.lblSecondSize.Name = "lblSecondSize"
        Me.lblSecondSize.Size = New System.Drawing.Size(13, 13)
        Me.lblSecondSize.TabIndex = 9
        Me.lblSecondSize.Text = "0"
        '
        'txtSec
        '
        Me.txtSec.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSec.Location = New System.Drawing.Point(56, 48)
        Me.txtSec.Name = "txtSec"
        Me.txtSec.Size = New System.Drawing.Size(97, 22)
        Me.txtSec.TabIndex = 8
        Me.txtSec.Text = "3+"
        '
        'cmbSecondUni
        '
        Me.cmbSecondUni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSecondUni.FormattingEnabled = True
        Me.cmbSecondUni.Location = New System.Drawing.Point(159, 47)
        Me.cmbSecondUni.Name = "cmbSecondUni"
        Me.cmbSecondUni.Size = New System.Drawing.Size(56, 21)
        Me.cmbSecondUni.TabIndex = 7
        '
        'lblSecondTarget
        '
        Me.lblSecondTarget.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblSecondTarget.Location = New System.Drawing.Point(7, 50)
        Me.lblSecondTarget.Name = "lblSecondTarget"
        Me.lblSecondTarget.Size = New System.Drawing.Size(46, 15)
        Me.lblSecondTarget.TabIndex = 6
        Me.lblSecondTarget.Text = "Second"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(221, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Uni size"
        '
        'lblMainSize
        '
        Me.lblMainSize.AutoSize = True
        Me.lblMainSize.Location = New System.Drawing.Point(220, 27)
        Me.lblMainSize.Name = "lblMainSize"
        Me.lblMainSize.Size = New System.Drawing.Size(13, 13)
        Me.lblMainSize.TabIndex = 4
        Me.lblMainSize.Text = "0"
        '
        'txtMain
        '
        Me.txtMain.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMain.Location = New System.Drawing.Point(56, 25)
        Me.txtMain.Name = "txtMain"
        Me.txtMain.Size = New System.Drawing.Size(97, 22)
        Me.txtMain.TabIndex = 3
        Me.txtMain.Text = "3+"
        '
        'cmbMainUni
        '
        Me.cmbMainUni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMainUni.FormattingEnabled = True
        Me.cmbMainUni.Location = New System.Drawing.Point(159, 24)
        Me.cmbMainUni.Name = "cmbMainUni"
        Me.cmbMainUni.Size = New System.Drawing.Size(56, 21)
        Me.cmbMainUni.TabIndex = 2
        '
        'lblMainTarget
        '
        Me.lblMainTarget.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblMainTarget.Location = New System.Drawing.Point(7, 27)
        Me.lblMainTarget.Name = "lblMainTarget"
        Me.lblMainTarget.Size = New System.Drawing.Size(46, 15)
        Me.lblMainTarget.TabIndex = 0
        Me.lblMainTarget.Text = "Main"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.clTrinity.My.Resources.Resources.planner_buyer_2
        Me.PictureBox2.Location = New System.Drawing.Point(310, 177)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(21, 20)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 21
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.planner_buyer_2_2
        Me.PictureBox1.Location = New System.Drawing.Point(310, 141)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(21, 20)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 20
        Me.PictureBox1.TabStop = False
        '
        'cmbPlanner
        '
        Me.cmbPlanner.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbPlanner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPlanner.FormattingEnabled = True
        Me.cmbPlanner.Location = New System.Drawing.Point(337, 180)
        Me.cmbPlanner.Name = "cmbPlanner"
        Me.cmbPlanner.Size = New System.Drawing.Size(296, 21)
        Me.cmbPlanner.Sorted = True
        Me.cmbPlanner.TabIndex = 19
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(334, 164)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Planner"
        '
        'cmbBuyer
        '
        Me.cmbBuyer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbBuyer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBuyer.FormattingEnabled = True
        Me.cmbBuyer.Location = New System.Drawing.Point(337, 141)
        Me.cmbBuyer.Name = "cmbBuyer"
        Me.cmbBuyer.Size = New System.Drawing.Size(296, 21)
        Me.cmbBuyer.Sorted = True
        Me.cmbBuyer.TabIndex = 17
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(334, 125)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Buyer"
        '
        'cmbProduct
        '
        Me.cmbProduct.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProduct.FormattingEnabled = True
        Me.cmbProduct.Location = New System.Drawing.Point(337, 102)
        Me.cmbProduct.Name = "cmbProduct"
        Me.cmbProduct.Size = New System.Drawing.Size(200, 21)
        Me.cmbProduct.Sorted = True
        Me.cmbProduct.TabIndex = 13
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(334, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Product"
        '
        'cmbClient
        '
        Me.cmbClient.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClient.FormattingEnabled = True
        Me.cmbClient.Location = New System.Drawing.Point(337, 63)
        Me.cmbClient.Name = "cmbClient"
        Me.cmbClient.Size = New System.Drawing.Size(200, 21)
        Me.cmbClient.Sorted = True
        Me.cmbClient.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(334, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Client"
        '
        'txtName
        '
        Me.txtName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtName.Location = New System.Drawing.Point(337, 22)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(266, 22)
        Me.txtName.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(334, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Campaign Name"
        '
        'lblContract
        '
        Me.lblContract.AutoSize = True
        Me.lblContract.Location = New System.Drawing.Point(228, 19)
        Me.lblContract.MaximumSize = New System.Drawing.Size(60, 0)
        Me.lblContract.Name = "lblContract"
        Me.lblContract.Size = New System.Drawing.Size(51, 13)
        Me.lblContract.TabIndex = 5
        Me.lblContract.Text = "<None>"
        '
        'lblPeriod
        '
        Me.lblPeriod.AutoSize = True
        Me.lblPeriod.ForeColor = System.Drawing.Color.Red
        Me.lblPeriod.Location = New System.Drawing.Point(56, 64)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(88, 13)
        Me.lblPeriod.TabIndex = 4
        Me.lblPeriod.Text = "No period is set"
        '
        'lblArea
        '
        Me.lblArea.AutoSize = True
        Me.lblArea.Location = New System.Drawing.Point(56, 19)
        Me.lblArea.Name = "lblArea"
        Me.lblArea.Size = New System.Drawing.Size(48, 13)
        Me.lblArea.TabIndex = 3
        Me.lblArea.Text = "Sweden"
        '
        'tpChannels
        '
        Me.tpChannels.Controls.Add(Me.lblOldPricelist)
        Me.tpChannels.Controls.Add(Me.cmdChannelsNext)
        Me.tpChannels.Controls.Add(Me.GroupBox4)
        Me.tpChannels.Controls.Add(Me.GroupBox3)
        Me.tpChannels.Controls.Add(Me.PictureBox5)
        Me.tpChannels.Location = New System.Drawing.Point(4, 22)
        Me.tpChannels.Name = "tpChannels"
        Me.tpChannels.Padding = New System.Windows.Forms.Padding(3)
        Me.tpChannels.Size = New System.Drawing.Size(639, 417)
        Me.tpChannels.TabIndex = 2
        Me.tpChannels.Text = "Channels"
        Me.tpChannels.UseVisualStyleBackColor = True
        '
        'lblOldPricelist
        '
        Me.lblOldPricelist.AutoSize = True
        Me.lblOldPricelist.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOldPricelist.ForeColor = System.Drawing.Color.Red
        Me.lblOldPricelist.Location = New System.Drawing.Point(61, 14)
        Me.lblOldPricelist.Name = "lblOldPricelist"
        Me.lblOldPricelist.Size = New System.Drawing.Size(112, 13)
        Me.lblOldPricelist.TabIndex = 27
        Me.lblOldPricelist.Text = "Checking pricelists..."
        Me.lblOldPricelist.Visible = False
        '
        'cmdChannelsNext
        '
        Me.cmdChannelsNext.FlatAppearance.BorderSize = 0
        Me.cmdChannelsNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdChannelsNext.Location = New System.Drawing.Point(558, 381)
        Me.cmdChannelsNext.Name = "cmdChannelsNext"
        Me.cmdChannelsNext.Size = New System.Drawing.Size(75, 29)
        Me.cmdChannelsNext.TabIndex = 26
        Me.cmdChannelsNext.Text = "Next"
        Me.cmdChannelsNext.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.lblNetCPP)
        Me.GroupBox4.Controls.Add(Me.lblGrossCPP)
        Me.GroupBox4.Controls.Add(Me.grdChannelInfo)
        Me.GroupBox4.Location = New System.Drawing.Point(3, 214)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(636, 161)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Channel info"
        '
        'lblNetCPP
        '
        Me.lblNetCPP.Location = New System.Drawing.Point(300, 15)
        Me.lblNetCPP.Name = "lblNetCPP"
        Me.lblNetCPP.Size = New System.Drawing.Size(84, 13)
        Me.lblNetCPP.TabIndex = 19
        Me.lblNetCPP.Text = "Net CPP"
        Me.lblNetCPP.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblGrossCPP
        '
        Me.lblGrossCPP.Location = New System.Drawing.Point(210, 14)
        Me.lblGrossCPP.Name = "lblGrossCPP"
        Me.lblGrossCPP.Size = New System.Drawing.Size(84, 13)
        Me.lblGrossCPP.TabIndex = 18
        Me.lblGrossCPP.Text = "Gross CPP"
        Me.lblGrossCPP.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'grdChannelInfo
        '
        Me.grdChannelInfo.AllowUserToAddRows = False
        Me.grdChannelInfo.AllowUserToDeleteRows = False
        Me.grdChannelInfo.AllowUserToResizeColumns = False
        Me.grdChannelInfo.AllowUserToResizeRows = False
        Me.grdChannelInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdChannelInfo.BackgroundColor = System.Drawing.Color.Silver
        Me.grdChannelInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChannelInfo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colInfoChannel, Me.colInfoNat, Me.colInfoChn})
        Me.grdChannelInfo.GridColor = System.Drawing.Color.LightGray
        Me.grdChannelInfo.Location = New System.Drawing.Point(6, 30)
        Me.grdChannelInfo.Name = "grdChannelInfo"
        Me.grdChannelInfo.ReadOnly = True
        Me.grdChannelInfo.RowHeadersVisible = False
        Me.grdChannelInfo.Size = New System.Drawing.Size(624, 125)
        Me.grdChannelInfo.TabIndex = 15
        '
        'colInfoChannel
        '
        Me.colInfoChannel.HeaderText = "Channel"
        Me.colInfoChannel.Name = "colInfoChannel"
        Me.colInfoChannel.ReadOnly = True
        '
        'colInfoNat
        '
        Me.colInfoNat.HeaderText = "Nat"
        Me.colInfoNat.Name = "colInfoNat"
        Me.colInfoNat.ReadOnly = True
        Me.colInfoNat.Width = 50
        '
        'colInfoChn
        '
        Me.colInfoChn.HeaderText = "Chn"
        Me.colInfoChn.Name = "colInfoChn"
        Me.colInfoChn.ReadOnly = True
        Me.colInfoChn.Width = 50
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.cmdAddChannelWizard)
        Me.GroupBox3.Controls.Add(Me.cmdQuickCopy)
        Me.GroupBox3.Controls.Add(Me.cmdCalculateDayparts)
        Me.GroupBox3.Controls.Add(Me.cmbCPT)
        Me.GroupBox3.Controls.Add(Me.cmdRemoveChannel)
        Me.GroupBox3.Controls.Add(Me.cmdQuickAdd)
        Me.GroupBox3.Controls.Add(Me.cmdAddChannel)
        Me.GroupBox3.Controls.Add(Me.grdChannels)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 41)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(636, 175)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Channels"
        '
        'cmbCPT
        '
        Me.cmbCPT.BackColor = System.Drawing.SystemColors.MenuBar
        Me.cmbCPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCPT.FormattingEnabled = True
        Me.cmbCPT.Items.AddRange(New Object() {"Discount", "CPT", "CPP", "Enhancement"})
        Me.cmbCPT.Location = New System.Drawing.Point(331, 19)
        Me.cmbCPT.Name = "cmbCPT"
        Me.cmbCPT.Size = New System.Drawing.Size(61, 21)
        Me.cmbCPT.TabIndex = 15
        '
        'cmdQuickAdd
        '
        Me.cmdQuickAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdQuickAdd.FlatAppearance.BorderSize = 0
        Me.cmdQuickAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdQuickAdd.Image = Global.clTrinity.My.Resources.Resources.add_more_2_small_org
        Me.cmdQuickAdd.Location = New System.Drawing.Point(608, 142)
        Me.cmdQuickAdd.Name = "cmdQuickAdd"
        Me.cmdQuickAdd.Size = New System.Drawing.Size(22, 20)
        Me.cmdQuickAdd.TabIndex = 13
        Me.cmdQuickAdd.UseVisualStyleBackColor = True
        Me.cmdQuickAdd.Visible = False
        '
        'grdChannels
        '
        Me.grdChannels.AllowUserToAddRows = False
        Me.grdChannels.AllowUserToDeleteRows = False
        Me.grdChannels.AllowUserToResizeColumns = False
        Me.grdChannels.AllowUserToResizeRows = False
        Me.grdChannels.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdChannels.BackgroundColor = System.Drawing.Color.Silver
        Me.grdChannels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChannels.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colChannel, Me.colBuyingTarget, Me.colCPT, Me.colMax, Me.colDP})
        Me.grdChannels.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdChannels.GridColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdChannels.Location = New System.Drawing.Point(6, 18)
        Me.grdChannels.Name = "grdChannels"
        Me.grdChannels.RowHeadersVisible = False
        Me.grdChannels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdChannels.Size = New System.Drawing.Size(596, 152)
        Me.grdChannels.TabIndex = 0
        '
        'colChannel
        '
        Me.colChannel.HeaderText = "Channel"
        Me.colChannel.Name = "colChannel"
        Me.colChannel.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colChannel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.colChannel.Width = 240
        '
        'colBuyingTarget
        '
        Me.colBuyingTarget.HeaderText = "Buying Target"
        Me.colBuyingTarget.Name = "colBuyingTarget"
        Me.colBuyingTarget.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colBuyingTarget.Width = 85
        '
        'colCPT
        '
        Me.colCPT.HeaderText = "CPT"
        Me.colCPT.Name = "colCPT"
        Me.colCPT.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colCPT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colCPT.Width = 60
        '
        'colMax
        '
        DataGridViewCellStyle5.Format = "P"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.colMax.DefaultCellStyle = DataGridViewCellStyle5
        Me.colMax.HeaderText = "Max"
        Me.colMax.Name = "colMax"
        Me.colMax.Width = 60
        '
        'colDP
        '
        Me.colDP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colDP.HeaderText = "Daypart split"
        Me.colDP.Name = "colDP"
        Me.colDP.ReadOnly = True
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = Global.clTrinity.My.Resources.Resources.monitor_3
        Me.PictureBox5.Location = New System.Drawing.Point(7, 6)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox5.TabIndex = 0
        Me.PictureBox5.TabStop = False
        '
        'tpCombinations
        '
        Me.tpCombinations.Controls.Add(Me.cmdCombinationsNext)
        Me.tpCombinations.Controls.Add(Me.grpCombo)
        Me.tpCombinations.Controls.Add(Me.grpCombos)
        Me.tpCombinations.Controls.Add(Me.PictureBox8)
        Me.tpCombinations.Location = New System.Drawing.Point(4, 22)
        Me.tpCombinations.Name = "tpCombinations"
        Me.tpCombinations.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCombinations.Size = New System.Drawing.Size(639, 417)
        Me.tpCombinations.TabIndex = 5
        Me.tpCombinations.Text = "Combinations   "
        Me.tpCombinations.UseVisualStyleBackColor = True
        '
        'cmdCombinationsNext
        '
        Me.cmdCombinationsNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCombinationsNext.FlatAppearance.BorderSize = 0
        Me.cmdCombinationsNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCombinationsNext.Location = New System.Drawing.Point(558, 381)
        Me.cmdCombinationsNext.Name = "cmdCombinationsNext"
        Me.cmdCombinationsNext.Size = New System.Drawing.Size(75, 29)
        Me.cmdCombinationsNext.TabIndex = 27
        Me.cmdCombinationsNext.Text = "Next"
        Me.cmdCombinationsNext.UseVisualStyleBackColor = True
        '
        'grpCombo
        '
        Me.grpCombo.Controls.Add(Me.chkSendAsUnitMarathon)
        Me.grpCombo.Controls.Add(Me.PictureBox9)
        Me.grpCombo.Controls.Add(Me.Label13)
        Me.grpCombo.Controls.Add(Me.txtMarathonIDCombo)
        Me.grpCombo.Controls.Add(Me.chkPrintAsOne)
        Me.grpCombo.Controls.Add(Me.cmdComboND)
        Me.grpCombo.Controls.Add(Me.chkShowAsOne)
        Me.grpCombo.Controls.Add(Me.txtComboName)
        Me.grpCombo.Controls.Add(Me.Label12)
        Me.grpCombo.Controls.Add(Me.optTRP)
        Me.grpCombo.Controls.Add(Me.optBudget)
        Me.grpCombo.Controls.Add(Me.grdCombo)
        Me.grpCombo.Controls.Add(Me.cmdDeleteChannelFromCombo)
        Me.grpCombo.Controls.Add(Me.cmdAddChannelToCombo)
        Me.grpCombo.Location = New System.Drawing.Point(3, 214)
        Me.grpCombo.Name = "grpCombo"
        Me.grpCombo.Size = New System.Drawing.Size(524, 196)
        Me.grpCombo.TabIndex = 3
        Me.grpCombo.TabStop = False
        Me.grpCombo.Text = "Combination"
        Me.grpCombo.Visible = False
        '
        'chkSendAsUnitMarathon
        '
        Me.chkSendAsUnitMarathon.AutoSize = True
        Me.chkSendAsUnitMarathon.Location = New System.Drawing.Point(339, 122)
        Me.chkSendAsUnitMarathon.Name = "chkSendAsUnitMarathon"
        Me.chkSendAsUnitMarathon.Size = New System.Drawing.Size(181, 17)
        Me.chkSendAsUnitMarathon.TabIndex = 34
        Me.chkSendAsUnitMarathon.Text = "Send as one unit to Marathon"
        Me.chkSendAsUnitMarathon.UseVisualStyleBackColor = True
        '
        'PictureBox9
        '
        Me.PictureBox9.Image = CType(resources.GetObject("PictureBox9.Image"), System.Drawing.Image)
        Me.PictureBox9.Location = New System.Drawing.Point(312, 140)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(21, 20)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox9.TabIndex = 28
        Me.PictureBox9.TabStop = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(336, 145)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(72, 13)
        Me.Label13.TabIndex = 33
        Me.Label13.Text = "Marathon ID"
        '
        'txtMarathonIDCombo
        '
        Me.txtMarathonIDCombo.Location = New System.Drawing.Point(311, 168)
        Me.txtMarathonIDCombo.Name = "txtMarathonIDCombo"
        Me.txtMarathonIDCombo.Size = New System.Drawing.Size(156, 22)
        Me.txtMarathonIDCombo.TabIndex = 32
        '
        'chkPrintAsOne
        '
        Me.chkPrintAsOne.AutoSize = True
        Me.chkPrintAsOne.Location = New System.Drawing.Point(339, 99)
        Me.chkPrintAsOne.Name = "chkPrintAsOne"
        Me.chkPrintAsOne.Size = New System.Drawing.Size(134, 17)
        Me.chkPrintAsOne.TabIndex = 31
        Me.chkPrintAsOne.Text = "Print as one booking"
        Me.chkPrintAsOne.UseVisualStyleBackColor = True
        '
        'chkShowAsOne
        '
        Me.chkShowAsOne.AutoSize = True
        Me.chkShowAsOne.Location = New System.Drawing.Point(339, 76)
        Me.chkShowAsOne.Name = "chkShowAsOne"
        Me.chkShowAsOne.Size = New System.Drawing.Size(128, 17)
        Me.chkShowAsOne.TabIndex = 29
        Me.chkShowAsOne.Text = "Allocate as one unit"
        Me.chkShowAsOne.UseVisualStyleBackColor = True
        '
        'txtComboName
        '
        Me.txtComboName.Location = New System.Drawing.Point(6, 31)
        Me.txtComboName.Name = "txtComboName"
        Me.txtComboName.Size = New System.Drawing.Size(299, 22)
        Me.txtComboName.TabIndex = 28
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(36, 13)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "Name"
        '
        'optTRP
        '
        Me.optTRP.AutoSize = True
        Me.optTRP.Location = New System.Drawing.Point(339, 54)
        Me.optTRP.Name = "optTRP"
        Me.optTRP.Size = New System.Drawing.Size(96, 17)
        Me.optTRP.TabIndex = 26
        Me.optTRP.Text = "Work on TRPs"
        Me.optTRP.UseVisualStyleBackColor = True
        '
        'optBudget
        '
        Me.optBudget.AutoSize = True
        Me.optBudget.Checked = True
        Me.optBudget.Location = New System.Drawing.Point(339, 32)
        Me.optBudget.Name = "optBudget"
        Me.optBudget.Size = New System.Drawing.Size(111, 17)
        Me.optBudget.TabIndex = 25
        Me.optBudget.TabStop = True
        Me.optBudget.Text = "Work on budget"
        Me.optBudget.UseVisualStyleBackColor = True
        '
        'grdCombo
        '
        Me.grdCombo.AllowUserToAddRows = False
        Me.grdCombo.AllowUserToDeleteRows = False
        Me.grdCombo.AllowUserToResizeColumns = False
        Me.grdCombo.AllowUserToResizeRows = False
        Me.grdCombo.BackgroundColor = System.Drawing.Color.Silver
        Me.grdCombo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCombo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colComboChannel, Me.colRelation})
        Me.grdCombo.Location = New System.Drawing.Point(6, 55)
        Me.grdCombo.MultiSelect = False
        Me.grdCombo.Name = "grdCombo"
        Me.grdCombo.RowHeadersVisible = False
        Me.grdCombo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdCombo.Size = New System.Drawing.Size(299, 135)
        Me.grdCombo.TabIndex = 24
        Me.grdCombo.VirtualMode = True
        '
        'colComboChannel
        '
        Me.colComboChannel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colComboChannel.HeaderText = "Channel"
        Me.colComboChannel.Name = "colComboChannel"
        '
        'colRelation
        '
        Me.colRelation.HeaderText = "Relation"
        Me.colRelation.Name = "colRelation"
        '
        'grpCombos
        '
        Me.grpCombos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpCombos.Controls.Add(Me.grdCombos)
        Me.grpCombos.Controls.Add(Me.cmdDeleteCombo)
        Me.grpCombos.Controls.Add(Me.cmdAddCombo)
        Me.grpCombos.Location = New System.Drawing.Point(3, 41)
        Me.grpCombos.Name = "grpCombos"
        Me.grpCombos.Size = New System.Drawing.Size(642, 169)
        Me.grpCombos.TabIndex = 2
        Me.grpCombos.TabStop = False
        Me.grpCombos.Text = "Combinations"
        '
        'grdCombos
        '
        Me.grdCombos.AllowUserToAddRows = False
        Me.grdCombos.AllowUserToDeleteRows = False
        Me.grdCombos.AllowUserToResizeColumns = False
        Me.grdCombos.AllowUserToResizeRows = False
        Me.grdCombos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdCombos.BackgroundColor = System.Drawing.Color.Silver
        Me.grdCombos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCombos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colComboName, Me.colChannels})
        Me.grdCombos.Location = New System.Drawing.Point(6, 18)
        Me.grdCombos.MultiSelect = False
        Me.grdCombos.Name = "grdCombos"
        Me.grdCombos.RowHeadersVisible = False
        Me.grdCombos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdCombos.Size = New System.Drawing.Size(602, 146)
        Me.grdCombos.TabIndex = 21
        Me.grdCombos.VirtualMode = True
        '
        'colComboName
        '
        Me.colComboName.HeaderText = "Name"
        Me.colComboName.Name = "colComboName"
        Me.colComboName.ReadOnly = True
        Me.colComboName.Width = 150
        '
        'colChannels
        '
        Me.colChannels.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colChannels.HeaderText = "Included Channels"
        Me.colChannels.Name = "colChannels"
        Me.colChannels.ReadOnly = True
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = Global.clTrinity.My.Resources.Resources.people_2
        Me.PictureBox8.Location = New System.Drawing.Point(7, 6)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox8.TabIndex = 1
        Me.PictureBox8.TabStop = False
        '
        'tpFilms
        '
        Me.tpFilms.Controls.Add(Me.cmdFilmsNext)
        Me.tpFilms.Controls.Add(Me.grpFilm)
        Me.tpFilms.Controls.Add(Me.GroupBox5)
        Me.tpFilms.Controls.Add(Me.PictureBox6)
        Me.tpFilms.Location = New System.Drawing.Point(4, 22)
        Me.tpFilms.Name = "tpFilms"
        Me.tpFilms.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFilms.Size = New System.Drawing.Size(639, 417)
        Me.tpFilms.TabIndex = 3
        Me.tpFilms.Text = "Films"
        Me.tpFilms.UseVisualStyleBackColor = True
        '
        'cmdFilmsNext
        '
        Me.cmdFilmsNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdFilmsNext.FlatAppearance.BorderSize = 0
        Me.cmdFilmsNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFilmsNext.Location = New System.Drawing.Point(558, 381)
        Me.cmdFilmsNext.Name = "cmdFilmsNext"
        Me.cmdFilmsNext.Size = New System.Drawing.Size(75, 29)
        Me.cmdFilmsNext.TabIndex = 27
        Me.cmdFilmsNext.Text = "Next"
        Me.cmdFilmsNext.UseVisualStyleBackColor = True
        '
        'grpFilm
        '
        Me.grpFilm.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFilm.Controls.Add(Me.chkAutoFilmCode)
        Me.grpFilm.Controls.Add(Me.lblIndexWarning)
        Me.grpFilm.Controls.Add(Me.chkFilmIdxAsDiscount)
        Me.grpFilm.Controls.Add(Me.cmdSaveToAdtoox)
        Me.grpFilm.Controls.Add(Me.cmdSave)
        Me.grpFilm.Controls.Add(Me.grdFilmDetails)
        Me.grpFilm.Controls.Add(Me.Label11)
        Me.grpFilm.Controls.Add(Me.chkFilmAutoIndex)
        Me.grpFilm.Controls.Add(Me.txtFilmLength)
        Me.grpFilm.Controls.Add(Me.Label10)
        Me.grpFilm.Controls.Add(Me.txtFilmDescription)
        Me.grpFilm.Controls.Add(Me.Label9)
        Me.grpFilm.Controls.Add(Me.txtFilmName)
        Me.grpFilm.Controls.Add(Me.Label8)
        Me.grpFilm.Location = New System.Drawing.Point(3, 198)
        Me.grpFilm.Name = "grpFilm"
        Me.grpFilm.Size = New System.Drawing.Size(642, 177)
        Me.grpFilm.TabIndex = 2
        Me.grpFilm.TabStop = False
        Me.grpFilm.Text = "Film"
        Me.grpFilm.Visible = False
        '
        'lblIndexWarning
        '
        Me.lblIndexWarning.AutoSize = True
        Me.lblIndexWarning.ForeColor = System.Drawing.Color.Red
        Me.lblIndexWarning.Location = New System.Drawing.Point(4, 133)
        Me.lblIndexWarning.Name = "lblIndexWarning"
        Me.lblIndexWarning.Size = New System.Drawing.Size(205, 13)
        Me.lblIndexWarning.TabIndex = 14
        Me.lblIndexWarning.Text = "One or more indexes are likely too low"
        Me.lblIndexWarning.Visible = False
        '
        'grdFilmDetails
        '
        Me.grdFilmDetails.AllowUserToAddRows = False
        Me.grdFilmDetails.AllowUserToDeleteRows = False
        Me.grdFilmDetails.AllowUserToResizeColumns = False
        Me.grdFilmDetails.AllowUserToResizeRows = False
        Me.grdFilmDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdFilmDetails.BackgroundColor = System.Drawing.Color.Silver
        Me.grdFilmDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdFilmDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colChannelFilmcode, Me.colGrossFilmIndex, Me.colChannelIndex})
        Me.grdFilmDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdFilmDetails.Location = New System.Drawing.Point(221, 30)
        Me.grdFilmDetails.Name = "grdFilmDetails"
        Me.grdFilmDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.grdFilmDetails.Size = New System.Drawing.Size(415, 119)
        Me.grdFilmDetails.TabIndex = 8
        Me.grdFilmDetails.VirtualMode = True
        '
        'colChannelFilmcode
        '
        Me.colChannelFilmcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colChannelFilmcode.HeaderText = "Filmcode"
        Me.colChannelFilmcode.Name = "colChannelFilmcode"
        '
        'colGrossFilmIndex
        '
        Me.colGrossFilmIndex.HeaderText = "Gross Idx"
        Me.colGrossFilmIndex.Name = "colGrossFilmIndex"
        Me.colGrossFilmIndex.Visible = False
        Me.colGrossFilmIndex.Width = 60
        '
        'colChannelIndex
        '
        Me.colChannelIndex.HeaderText = "Index"
        Me.colChannelIndex.Name = "colChannelIndex"
        Me.colChannelIndex.Width = 60
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(218, 11)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 13)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "Film details:"
        '
        'chkFilmAutoIndex
        '
        Me.chkFilmAutoIndex.AutoSize = True
        Me.chkFilmAutoIndex.Checked = True
        Me.chkFilmAutoIndex.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFilmAutoIndex.Location = New System.Drawing.Point(92, 112)
        Me.chkFilmAutoIndex.Name = "chkFilmAutoIndex"
        Me.chkFilmAutoIndex.Size = New System.Drawing.Size(82, 17)
        Me.chkFilmAutoIndex.TabIndex = 6
        Me.chkFilmAutoIndex.Text = "Auto index"
        Me.chkFilmAutoIndex.UseVisualStyleBackColor = True
        '
        'txtFilmLength
        '
        Me.txtFilmLength.Location = New System.Drawing.Point(6, 110)
        Me.txtFilmLength.Name = "txtFilmLength"
        Me.txtFilmLength.Size = New System.Drawing.Size(80, 22)
        Me.txtFilmLength.TabIndex = 5
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.Label10.Location = New System.Drawing.Point(6, 94)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 13)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Spot length:"
        '
        'txtFilmDescription
        '
        Me.txtFilmDescription.Location = New System.Drawing.Point(6, 70)
        Me.txtFilmDescription.Name = "txtFilmDescription"
        Me.txtFilmDescription.Size = New System.Drawing.Size(185, 22)
        Me.txtFilmDescription.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.Label9.Location = New System.Drawing.Point(6, 54)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 13)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "Film description:"
        '
        'txtFilmName
        '
        Me.txtFilmName.Location = New System.Drawing.Point(6, 31)
        Me.txtFilmName.Name = "txtFilmName"
        Me.txtFilmName.Size = New System.Drawing.Size(185, 22)
        Me.txtFilmName.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.Label8.Location = New System.Drawing.Point(6, 15)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Film name"
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.cmdPlayMovie)
        Me.GroupBox5.Controls.Add(Me.cmdFindInAdtoox)
        Me.GroupBox5.Controls.Add(Me.cmdFind)
        Me.GroupBox5.Controls.Add(Me.cmdRemoveFilm)
        Me.GroupBox5.Controls.Add(Me.cmdAddFilm)
        Me.GroupBox5.Controls.Add(Me.grdFilms)
        Me.GroupBox5.Location = New System.Drawing.Point(3, 41)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(642, 151)
        Me.GroupBox5.TabIndex = 1
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Films"
        '
        'grdFilms
        '
        Me.grdFilms.AllowUserToAddRows = False
        Me.grdFilms.AllowUserToDeleteRows = False
        Me.grdFilms.AllowUserToResizeRows = False
        Me.grdFilms.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdFilms.BackgroundColor = System.Drawing.Color.Silver
        Me.grdFilms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFilms.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colFilmName, Me.colFilmDescription, Me.colFilmFilmcodes, Me.colFilmLength})
        Me.grdFilms.Location = New System.Drawing.Point(6, 18)
        Me.grdFilms.Name = "grdFilms"
        Me.grdFilms.ReadOnly = True
        Me.grdFilms.RowHeadersVisible = False
        Me.grdFilms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdFilms.Size = New System.Drawing.Size(602, 127)
        Me.grdFilms.TabIndex = 0
        '
        'colFilmName
        '
        Me.colFilmName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colFilmName.HeaderText = "Name"
        Me.colFilmName.Name = "colFilmName"
        Me.colFilmName.ReadOnly = True
        Me.colFilmName.Width = 119
        '
        'colFilmDescription
        '
        Me.colFilmDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colFilmDescription.HeaderText = "Description"
        Me.colFilmDescription.Name = "colFilmDescription"
        Me.colFilmDescription.ReadOnly = True
        '
        'colFilmFilmcodes
        '
        Me.colFilmFilmcodes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colFilmFilmcodes.HeaderText = "Filmcodes"
        Me.colFilmFilmcodes.Name = "colFilmFilmcodes"
        Me.colFilmFilmcodes.ReadOnly = True
        Me.colFilmFilmcodes.Width = 110
        '
        'colFilmLength
        '
        Me.colFilmLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colFilmLength.HeaderText = "Length"
        Me.colFilmLength.Name = "colFilmLength"
        Me.colFilmLength.ReadOnly = True
        Me.colFilmLength.Width = 90
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = Global.clTrinity.My.Resources.Resources.film_2_small
        Me.PictureBox6.Location = New System.Drawing.Point(7, 6)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox6.TabIndex = 0
        Me.PictureBox6.TabStop = False
        '
        'tpIndex
        '
        Me.tpIndex.Controls.Add(Me.lblSeasonal)
        Me.tpIndex.Controls.Add(Me.cmdApply)
        Me.tpIndex.Controls.Add(Me.GroupBox7)
        Me.tpIndex.Controls.Add(Me.GroupBox6)
        Me.tpIndex.Controls.Add(Me.cmbIndexChannel)
        Me.tpIndex.Controls.Add(Me.PictureBox7)
        Me.tpIndex.Location = New System.Drawing.Point(4, 22)
        Me.tpIndex.Name = "tpIndex"
        Me.tpIndex.Padding = New System.Windows.Forms.Padding(3)
        Me.tpIndex.Size = New System.Drawing.Size(639, 417)
        Me.tpIndex.TabIndex = 4
        Me.tpIndex.Text = "Index / Added value"
        Me.tpIndex.UseVisualStyleBackColor = True
        '
        'lblSeasonal
        '
        Me.lblSeasonal.AutoSize = True
        Me.lblSeasonal.ForeColor = System.Drawing.Color.Red
        Me.lblSeasonal.Location = New System.Drawing.Point(169, 44)
        Me.lblSeasonal.Name = "lblSeasonal"
        Me.lblSeasonal.Size = New System.Drawing.Size(370, 13)
        Me.lblSeasonal.TabIndex = 32
        Me.lblSeasonal.Text = "This bookingtype does not have seasonal indexes saved in the pricelist"
        Me.lblSeasonal.Visible = False
        '
        'cmdApply
        '
        Me.cmdApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdApply.FlatAppearance.BorderSize = 0
        Me.cmdApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdApply.Location = New System.Drawing.Point(558, 381)
        Me.cmdApply.Name = "cmdApply"
        Me.cmdApply.Size = New System.Drawing.Size(75, 29)
        Me.cmdApply.TabIndex = 28
        Me.cmdApply.Text = "Apply"
        Me.cmdApply.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox7.Controls.Add(Me.cmdAddCopy)
        Me.GroupBox7.Controls.Add(Me.cmdCopyIndex)
        Me.GroupBox7.Controls.Add(Me.cmdEditEnhancement)
        Me.GroupBox7.Controls.Add(Me.grdIndexes)
        Me.GroupBox7.Controls.Add(Me.cmdRemoveIndex)
        Me.GroupBox7.Controls.Add(Me.cmdAddIndex)
        Me.GroupBox7.Location = New System.Drawing.Point(3, 223)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(642, 150)
        Me.GroupBox7.TabIndex = 3
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Indexes"
        '
        'grdIndexes
        '
        Me.grdIndexes.AllowUserToAddRows = False
        Me.grdIndexes.AllowUserToDeleteRows = False
        Me.grdIndexes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdIndexes.BackgroundColor = System.Drawing.Color.Silver
        Me.grdIndexes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdIndexes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDescription, Me.colOn, Me.colFrom, Me.colTo, Me.colIndexAmount, Me.colUse})
        Me.grdIndexes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdIndexes.Location = New System.Drawing.Point(6, 18)
        Me.grdIndexes.Name = "grdIndexes"
        Me.grdIndexes.RowHeadersVisible = False
        Me.grdIndexes.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdIndexes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdIndexes.Size = New System.Drawing.Size(598, 127)
        Me.grdIndexes.TabIndex = 19
        '
        'colDescription
        '
        Me.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colDescription.HeaderText = "Description"
        Me.colDescription.Name = "colDescription"
        '
        'colOn
        '
        Me.colOn.HeaderText = "On"
        Me.colOn.Items.AddRange(New Object() {"Net CPP", "Gross CPP", "TRP", "Min. CPP"})
        Me.colOn.Name = "colOn"
        Me.colOn.Width = 95
        '
        'colFrom
        '
        Me.colFrom.HeaderText = "From"
        Me.colFrom.Name = "colFrom"
        Me.colFrom.Width = 94
        '
        'colTo
        '
        Me.colTo.HeaderText = "To"
        Me.colTo.Name = "colTo"
        Me.colTo.Width = 95
        '
        'colIndexAmount
        '
        Me.colIndexAmount.HeaderText = "Index"
        Me.colIndexAmount.Name = "colIndexAmount"
        Me.colIndexAmount.Width = 60
        '
        'colUse
        '
        Me.colUse.HeaderText = "Use"
        Me.colUse.Name = "colUse"
        Me.colUse.Width = 30
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.Controls.Add(Me.chkMultiply)
        Me.GroupBox6.Controls.Add(Me.cmdCopyAV)
        Me.GroupBox6.Controls.Add(Me.grdAddedValues)
        Me.GroupBox6.Controls.Add(Me.cmdRemoveAV)
        Me.GroupBox6.Controls.Add(Me.cmdAddAV)
        Me.GroupBox6.Location = New System.Drawing.Point(3, 67)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(642, 150)
        Me.GroupBox6.TabIndex = 2
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Added Value"
        '
        'chkMultiply
        '
        Me.chkMultiply.AutoSize = True
        Me.chkMultiply.Location = New System.Drawing.Point(6, 125)
        Me.chkMultiply.Name = "chkMultiply"
        Me.chkMultiply.Size = New System.Drawing.Size(141, 17)
        Me.chkMultiply.TabIndex = 31
        Me.chkMultiply.Text = "Multiply Added Values"
        Me.chkMultiply.UseVisualStyleBackColor = True
        '
        'grdAddedValues
        '
        Me.grdAddedValues.AllowUserToAddRows = False
        Me.grdAddedValues.AllowUserToDeleteRows = False
        Me.grdAddedValues.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAddedValues.BackgroundColor = System.Drawing.Color.Silver
        Me.grdAddedValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAddedValues.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colAVName, Me.colGrossIndex, Me.colNetIndex, Me.colShowIn, Me.colUseAV})
        Me.grdAddedValues.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdAddedValues.Location = New System.Drawing.Point(4, 17)
        Me.grdAddedValues.Name = "grdAddedValues"
        Me.grdAddedValues.RowHeadersVisible = False
        Me.grdAddedValues.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdAddedValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdAddedValues.Size = New System.Drawing.Size(600, 90)
        Me.grdAddedValues.TabIndex = 19
        '
        'colAVName
        '
        Me.colAVName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colAVName.HeaderText = "Name"
        Me.colAVName.Name = "colAVName"
        '
        'colGrossIndex
        '
        Me.colGrossIndex.HeaderText = "Gross Index"
        Me.colGrossIndex.Name = "colGrossIndex"
        '
        'colNetIndex
        '
        Me.colNetIndex.HeaderText = "Net Index"
        Me.colNetIndex.Name = "colNetIndex"
        '
        'colShowIn
        '
        Me.colShowIn.HeaderText = "Show In"
        Me.colShowIn.Items.AddRange(New Object() {"Both", "Allocate", "Booking"})
        Me.colShowIn.Name = "colShowIn"
        Me.colShowIn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'colUseAV
        '
        Me.colUseAV.HeaderText = "Use"
        Me.colUseAV.Name = "colUseAV"
        Me.colUseAV.Width = 30
        '
        'cmbIndexChannel
        '
        Me.cmbIndexChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIndexChannel.FormattingEnabled = True
        Me.cmbIndexChannel.Location = New System.Drawing.Point(3, 41)
        Me.cmbIndexChannel.Name = "cmbIndexChannel"
        Me.cmbIndexChannel.Size = New System.Drawing.Size(160, 21)
        Me.cmbIndexChannel.TabIndex = 1
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = Global.clTrinity.My.Resources.Resources.people_2
        Me.PictureBox7.Location = New System.Drawing.Point(7, 6)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox7.TabIndex = 0
        Me.PictureBox7.TabStop = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Cost"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 130
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle6.Format = "C0"
        DataGridViewCellStyle6.NullValue = "0"
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn2.HeaderText = "Amount"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 70
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Marathon ID"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 70
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "CPT"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn4.Width = 60
        '
        'frmSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(647, 446)
        Me.Controls.Add(Me.tabSetup)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(663, 485)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(663, 485)
        Me.Name = "frmSetup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Setup"
        Me.mnuContract.ResumeLayout(False)
        Me.mnuCalculateDaypart.ResumeLayout(False)
        Me.mnuCalculcateComboND.ResumeLayout(False)
        Me.tabSetup.ResumeLayout(False)
        Me.tpGeneral.ResumeLayout(False)
        Me.tpGeneral.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grdCosts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpChannels.ResumeLayout(False)
        Me.tpChannels.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.grdChannelInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.grdChannels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpCombinations.ResumeLayout(False)
        Me.tpCombinations.PerformLayout()
        Me.grpCombo.ResumeLayout(False)
        Me.grpCombo.PerformLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCombo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCombos.ResumeLayout(False)
        CType(Me.grdCombos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpFilms.ResumeLayout(False)
        Me.tpFilms.PerformLayout()
        Me.grpFilm.ResumeLayout(False)
        Me.grpFilm.PerformLayout()
        CType(Me.grdFilmDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.grdFilms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpIndex.ResumeLayout(False)
        Me.tpIndex.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        CType(Me.grdIndexes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.grdAddedValues, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabSetup As ExtendedTabControl
    Friend WithEvents tpGeneral As System.Windows.Forms.TabPage
    Friend WithEvents tpChannels As System.Windows.Forms.TabPage
    Friend WithEvents tpFilms As System.Windows.Forms.TabPage
    Friend WithEvents tpIndex As System.Windows.Forms.TabPage
    Friend WithEvents cmdCountry As System.Windows.Forms.Button
    Friend WithEvents cmdContract As System.Windows.Forms.Button
    Friend WithEvents cmdPeriod As System.Windows.Forms.Button
    Friend WithEvents lblContract As System.Windows.Forms.Label
    Friend WithEvents lblPeriod As System.Windows.Forms.Label
    Friend WithEvents lblArea As System.Windows.Forms.Label
    Friend WithEvents cmdAddClient As System.Windows.Forms.Button
    Friend WithEvents cmbClient As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdEditClient As System.Windows.Forms.Button
    Friend WithEvents cmdEditProduct As System.Windows.Forms.Button
    Friend WithEvents cmdAddProduct As System.Windows.Forms.Button
    Friend WithEvents cmbProduct As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmbPlanner As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbBuyer As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents lblMainTarget As System.Windows.Forms.Label
    Friend WithEvents lblThirdSize As System.Windows.Forms.Label
    Friend WithEvents txtThird As System.Windows.Forms.TextBox
    Friend WithEvents cmbThirdUni As System.Windows.Forms.ComboBox
    Friend WithEvents lblThirdTarget As System.Windows.Forms.Label
    Friend WithEvents lblSecondSize As System.Windows.Forms.Label
    Friend WithEvents txtSec As System.Windows.Forms.TextBox
    Friend WithEvents cmbSecondUni As System.Windows.Forms.ComboBox
    Friend WithEvents lblSecondTarget As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblMainSize As System.Windows.Forms.Label
    Friend WithEvents txtMain As System.Windows.Forms.TextBox
    Friend WithEvents cmbMainUni As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtBudget As System.Windows.Forms.TextBox
    Friend WithEvents grdCosts As System.Windows.Forms.DataGridView
    Friend WithEvents lblCurrency As System.Windows.Forms.Label
    Friend WithEvents cmdDeleteCost As System.Windows.Forms.Button
    Friend WithEvents cmdAddCost As System.Windows.Forms.Button
    Friend WithEvents cmdGeneralNext As System.Windows.Forms.Button
    Friend WithEvents mnuArea As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuContract As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuNewContract As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOpenContract As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEditContract As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdChannelsNext As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents grdChannelInfo As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemoveChannel As System.Windows.Forms.Button
    Friend WithEvents cmdAddChannel As System.Windows.Forms.Button
    Friend WithEvents grdChannels As System.Windows.Forms.DataGridView
    Friend WithEvents cmbCPT As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblGrossCPP As System.Windows.Forms.Label
    Friend WithEvents lblNetCPP As System.Windows.Forms.Label
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdFilmsNext As System.Windows.Forms.Button
    Friend WithEvents grpFilm As System.Windows.Forms.GroupBox
    Friend WithEvents txtFilmName As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemoveFilm As System.Windows.Forms.Button
    Friend WithEvents cmdAddFilm As System.Windows.Forms.Button
    Friend WithEvents grdFilms As System.Windows.Forms.DataGridView
    Friend WithEvents grdFilmDetails As System.Windows.Forms.DataGridView
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkFilmAutoIndex As System.Windows.Forms.CheckBox
    Friend WithEvents txtFilmLength As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtFilmDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbIndexChannel As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox7 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdApply As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveIndex As System.Windows.Forms.Button
    Friend WithEvents cmdAddIndex As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveAV As System.Windows.Forms.Button
    Friend WithEvents cmdAddAV As System.Windows.Forms.Button
    Friend WithEvents grdIndexes As System.Windows.Forms.DataGridView
    Friend WithEvents grdAddedValues As System.Windows.Forms.DataGridView
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdFind As System.Windows.Forms.Button
    Friend WithEvents colInfoChannel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colInfoNat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colInfoChn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents cmdCalculateDayparts As System.Windows.Forms.Button
    Friend WithEvents mnuCalculateDaypart As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuLastWeek As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLastYear As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkFilmIdxAsDiscount As System.Windows.Forms.CheckBox
    Friend WithEvents colFilmName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFilmDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFilmFilmcodes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFilmLength As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblOldPricelist As System.Windows.Forms.Label
    Friend WithEvents cmdEditEnhancement As System.Windows.Forms.Button
    Friend WithEvents colChannelFilmcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colGrossFilmIndex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChannelIndex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tpCombinations As System.Windows.Forms.TabPage
    Friend WithEvents grpCombos As System.Windows.Forms.GroupBox
    Friend WithEvents grdCombos As System.Windows.Forms.DataGridView
    Friend WithEvents cmdDeleteCombo As System.Windows.Forms.Button
    Friend WithEvents cmdAddCombo As System.Windows.Forms.Button
    Friend WithEvents PictureBox8 As System.Windows.Forms.PictureBox
    Friend WithEvents grpCombo As System.Windows.Forms.GroupBox
    Friend WithEvents grdCombo As System.Windows.Forms.DataGridView
    Friend WithEvents cmdDeleteChannelFromCombo As System.Windows.Forms.Button
    Friend WithEvents cmdAddChannelToCombo As System.Windows.Forms.Button
    Friend WithEvents colComboName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChannels As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colComboChannel As clTrinity.ExtendedComboboxColumn
    Friend WithEvents colRelation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtComboName As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents optTRP As System.Windows.Forms.RadioButton
    Friend WithEvents optBudget As System.Windows.Forms.RadioButton
    Friend WithEvents cmdCombinationsNext As System.Windows.Forms.Button
    Friend WithEvents cmdCopyIndex As System.Windows.Forms.Button
    Friend WithEvents cmdCopyAV As System.Windows.Forms.Button
    Friend WithEvents chkShowAsOne As System.Windows.Forms.CheckBox
    Friend WithEvents cmdComboND As System.Windows.Forms.Button
    Friend WithEvents mnuCalculcateComboND As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuLastWeekND As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSamePeriodND As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkMultiply As System.Windows.Forms.CheckBox





    Friend WithEvents lblSeasonal As System.Windows.Forms.Label
    Friend WithEvents cmdQuickCopy As System.Windows.Forms.Button





    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colOn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents colFrom As clTrinity.CalendarColumn
    Friend WithEvents colTo As clTrinity.CalendarColumn
    Friend WithEvents colIndexAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUse As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents lblIndexWarning As System.Windows.Forms.Label
    Friend WithEvents cmdFindInAdtoox As System.Windows.Forms.Button
    Friend WithEvents cmdSaveToAdtoox As System.Windows.Forms.Button
    Friend WithEvents cmdPlayMovie As System.Windows.Forms.Button
    Friend WithEvents cmdAddCopy As System.Windows.Forms.Button
    Friend WithEvents chkAutoFilmCode As System.Windows.Forms.CheckBox
    Friend WithEvents tmrKeyPressTimer As System.Windows.Forms.Timer
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuUseActualTRPs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents colCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colType As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents colAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCostOn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents colMarathonID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAVName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colGrossIndex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNetIndex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colShowIn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents colUseAV As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cmdQuickAdd As System.Windows.Forms.Button
    Friend WithEvents chkPrintAsOne As System.Windows.Forms.CheckBox
    Friend WithEvents cmdAddChannelWizard As System.Windows.Forms.Button

    Private Sub tmrKeyPressTimer_Disposed(sender As Object, e As System.EventArgs) Handles tmrKeyPressTimer.Disposed

    End Sub
    Friend WithEvents mnuNoContract As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As Windows.Forms.ContextMenuStrip
    Friend WithEvents UseMTGChannelSplitToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFF36810MTVCC As Windows.Forms.ToolStripMenuItem
    Friend WithEvents colChannel As ExtendedComboboxColumn
    Friend WithEvents colBuyingTarget As ExtendedComboboxColumn
    Friend WithEvents colCPT As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMax As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDP As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label13 As Windows.Forms.Label
    Friend WithEvents txtMarathonIDCombo As Windows.Forms.TextBox
    Friend WithEvents PictureBox9 As Windows.Forms.PictureBox
    Friend WithEvents chkSendAsUnitMarathon As Windows.Forms.CheckBox
    Friend WithEvents lblRestrictedClientBool As Windows.Forms.Label
End Class
