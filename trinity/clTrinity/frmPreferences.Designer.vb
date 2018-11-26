<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPreferences
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPreferences))
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.chkBetaUser = New System.Windows.Forms.CheckBox()
        Me.chkTrustedUser = New System.Windows.Forms.CheckBox()
        Me.lblPwd = New System.Windows.Forms.Label()
        Me.cmdSavePwd = New System.Windows.Forms.Button()
        Me.txtPwd = New System.Windows.Forms.TextBox()
        Me.cmdChangePwd = New System.Windows.Forms.Button()
        Me.txtMarathonUser = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPhoneNr = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.cmdCampaignFiles = New System.Windows.Forms.Button()
        Me.cmdChannelSchedules = New System.Windows.Forms.Button()
        Me.cmdBrowseNetworkPath = New System.Windows.Forms.Button()
        Me.txtCampaignFiles = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtChannelSchedules = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDataPath = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dlgColor = New System.Windows.Forms.ColorDialog()
        Me.grpScheme = New System.Windows.Forms.GroupBox()
        Me.cmdFont = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.picHeadline = New System.Windows.Forms.PictureBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.picText = New System.Windows.Forms.PictureBox()
        Me.picPanelBG = New System.Windows.Forms.PictureBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.picPanelFG = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbColorScheme = New System.Windows.Forms.ComboBox()
        Me.chkMarathon = New System.Windows.Forms.CheckBox()
        Me.dlgBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.chkDebug = New System.Windows.Forms.CheckBox()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkAutosave = New System.Windows.Forms.CheckBox()
        Me.cmdSync = New System.Windows.Forms.Button()
        Me.cmdAddColorScheme = New System.Windows.Forms.Button()
        Me.tabPref = New System.Windows.Forms.TabControl()
        Me.tpUserInfo = New System.Windows.Forms.TabPage()
        Me.tpSettings = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.chkErrorChecking = New System.Windows.Forms.CheckBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.cmbMonitor = New System.Windows.Forms.ComboBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.chkLoadCombinations = New System.Windows.Forms.CheckBox()
        Me.chkLoadAV = New System.Windows.Forms.CheckBox()
        Me.chkLoadIndexes = New System.Windows.Forms.CheckBox()
        Me.chkLoadPrices = New System.Windows.Forms.CheckBox()
        Me.chkLoadCosts = New System.Windows.Forms.CheckBox()
        Me.cmdBrowseContract = New System.Windows.Forms.Button()
        Me.txtContractPath = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtPIBLast = New System.Windows.Forms.TextBox()
        Me.txtPIBFirst = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.tpLayout = New System.Windows.Forms.TabPage()
        Me.grpColoring = New System.Windows.Forms.GroupBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.pbc10 = New System.Windows.Forms.PictureBox()
        Me.pbc9 = New System.Windows.Forms.PictureBox()
        Me.pbc8 = New System.Windows.Forms.PictureBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.pbc7 = New System.Windows.Forms.PictureBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.pbc6 = New System.Windows.Forms.PictureBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.pbc5 = New System.Windows.Forms.PictureBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.pbc4 = New System.Windows.Forms.PictureBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.pbc3 = New System.Windows.Forms.PictureBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.pbc2 = New System.Windows.Forms.PictureBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.pbc1 = New System.Windows.Forms.PictureBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.tpPaths = New System.Windows.Forms.TabPage()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.cmdBrowseSharedPath = New System.Windows.Forms.Button()
        Me.txtSharedDataPath = New System.Windows.Forms.TextBox()
        Me.tpDatabase = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtMatrixDB = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtMatrixServer = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.cmdSyncNow = New System.Windows.Forms.Button()
        Me.cmbSync = New System.Windows.Forms.ComboBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.cmdBrowseLocalDB = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmdBrowseNetworkDB = New System.Windows.Forms.Button()
        Me.txtNetworkDB = New System.Windows.Forms.TextBox()
        Me.txtLocalDB = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.tpAdtoox = New System.Windows.Forms.TabPage()
        Me.lblAdtooxUsername = New System.Windows.Forms.Label()
        Me.txtAdtooxUsername = New System.Windows.Forms.TextBox()
        Me.lblAdtooxPassword = New System.Windows.Forms.Label()
        Me.txtAdtooxPassword = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout
        Me.GroupBox7.SuspendLayout
        Me.grpScheme.SuspendLayout
        CType(Me.picHeadline,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picText,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picPanelBG,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picPanelFG,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tabPref.SuspendLayout
        Me.tpUserInfo.SuspendLayout
        Me.tpSettings.SuspendLayout
        Me.GroupBox4.SuspendLayout
        Me.GroupBox6.SuspendLayout
        Me.GroupBox5.SuspendLayout
        Me.GroupBox3.SuspendLayout
        Me.tpLayout.SuspendLayout
        Me.grpColoring.SuspendLayout
        CType(Me.pbc10,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbc9,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbc8,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbc7,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbc6,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbc5,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbc4,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbc3,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbc2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbc1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tpPaths.SuspendLayout
        Me.tpDatabase.SuspendLayout
        Me.GroupBox2.SuspendLayout
        Me.tpAdtoox.SuspendLayout
        Me.SuspendLayout
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.FlatAppearance.BorderSize = 0
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Location = New System.Drawing.Point(366, 315)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 25)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Save"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.FlatAppearance.BorderSize = 0
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Button.Location = New System.Drawing.Point(293, 315)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 25)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox7)
        Me.GroupBox1.Controls.Add(Me.lblPwd)
        Me.GroupBox1.Controls.Add(Me.cmdSavePwd)
        Me.GroupBox1.Controls.Add(Me.txtPwd)
        Me.GroupBox1.Controls.Add(Me.cmdChangePwd)
        Me.GroupBox1.Controls.Add(Me.txtMarathonUser)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtPhoneNr)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtEmail)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(426, 262)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "User info"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.chkBetaUser)
        Me.GroupBox7.Controls.Add(Me.chkTrustedUser)
        Me.GroupBox7.Location = New System.Drawing.Point(320, 19)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(100, 62)
        Me.GroupBox7.TabIndex = 14
        Me.GroupBox7.TabStop = false
        Me.GroupBox7.Text = "Privileges"
        '
        'chkBetaUser
        '
        Me.chkBetaUser.AutoSize = true
        Me.chkBetaUser.Location = New System.Drawing.Point(6, 19)
        Me.chkBetaUser.Name = "chkBetaUser"
        Me.chkBetaUser.Size = New System.Drawing.Size(74, 17)
        Me.chkBetaUser.TabIndex = 12
        Me.chkBetaUser.Text = "Beta user"
        Me.chkBetaUser.UseVisualStyleBackColor = true
        '
        'chkTrustedUser
        '
        Me.chkTrustedUser.AutoSize = true
        Me.chkTrustedUser.Location = New System.Drawing.Point(6, 40)
        Me.chkTrustedUser.Name = "chkTrustedUser"
        Me.chkTrustedUser.Size = New System.Drawing.Size(88, 17)
        Me.chkTrustedUser.TabIndex = 13
        Me.chkTrustedUser.Text = "Trusted user"
        Me.chkTrustedUser.UseVisualStyleBackColor = true
        '
        'lblPwd
        '
        Me.lblPwd.AutoSize = true
        Me.lblPwd.Location = New System.Drawing.Point(16, 215)
        Me.lblPwd.Name = "lblPwd"
        Me.lblPwd.Size = New System.Drawing.Size(0, 13)
        Me.lblPwd.TabIndex = 11
        Me.lblPwd.Visible = false
        '
        'cmdSavePwd
        '
        Me.cmdSavePwd.FlatAppearance.BorderSize = 0
        Me.cmdSavePwd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSavePwd.Location = New System.Drawing.Point(166, 215)
        Me.cmdSavePwd.Name = "cmdSavePwd"
        Me.cmdSavePwd.Size = New System.Drawing.Size(61, 23)
        Me.cmdSavePwd.TabIndex = 10
        Me.cmdSavePwd.Text = "Submit"
        Me.cmdSavePwd.UseVisualStyleBackColor = true
        Me.cmdSavePwd.Visible = false
        '
        'txtPwd
        '
        Me.txtPwd.Location = New System.Drawing.Point(16, 216)
        Me.txtPwd.Name = "txtPwd"
        Me.txtPwd.Size = New System.Drawing.Size(144, 22)
        Me.txtPwd.TabIndex = 9
        Me.txtPwd.Visible = false
        '
        'cmdChangePwd
        '
        Me.cmdChangePwd.FlatAppearance.BorderSize = 0
        Me.cmdChangePwd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdChangePwd.Location = New System.Drawing.Point(342, 213)
        Me.cmdChangePwd.Name = "cmdChangePwd"
        Me.cmdChangePwd.Size = New System.Drawing.Size(78, 49)
        Me.cmdChangePwd.TabIndex = 8
        Me.cmdChangePwd.Text = "Change Password"
        Me.cmdChangePwd.UseVisualStyleBackColor = true
        Me.cmdChangePwd.Visible = False
        '
        'txtMarathonUser
        '
        Me.txtMarathonUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMarathonUser.Location = New System.Drawing.Point(16, 174)
        Me.txtMarathonUser.Name = "txtMarathonUser"
        Me.txtMarathonUser.Size = New System.Drawing.Size(271, 22)
        Me.txtMarathonUser.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(16, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name"
        '
        'txtPhoneNr
        '
        Me.txtPhoneNr.Location = New System.Drawing.Point(16, 85)
        Me.txtPhoneNr.Name = "txtPhoneNr"
        Me.txtPhoneNr.Size = New System.Drawing.Size(271, 22)
        Me.txtPhoneNr.TabIndex = 3
        '
        'Label15
        '
        Me.Label15.AutoSize = true
        Me.Label15.Location = New System.Drawing.Point(16, 157)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(114, 13)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "Marathon user name"
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(16, 112)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "E-mail"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(16, 41)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(271, 22)
        Me.txtName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(16, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Phone Nr"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(16, 129)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(271, 22)
        Me.txtEmail.TabIndex = 5
        '
        'cmdCampaignFiles
        '
        Me.cmdCampaignFiles.FlatAppearance.BorderSize = 0
        Me.cmdCampaignFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCampaignFiles.Location = New System.Drawing.Point(350, 150)
        Me.cmdCampaignFiles.Name = "cmdCampaignFiles"
        Me.cmdCampaignFiles.Size = New System.Drawing.Size(57, 23)
        Me.cmdCampaignFiles.TabIndex = 8
        Me.cmdCampaignFiles.Text = "Browse"
        Me.cmdCampaignFiles.UseVisualStyleBackColor = true
        '
        'cmdChannelSchedules
        '
        Me.cmdChannelSchedules.FlatAppearance.BorderSize = 0
        Me.cmdChannelSchedules.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdChannelSchedules.Location = New System.Drawing.Point(350, 110)
        Me.cmdChannelSchedules.Name = "cmdChannelSchedules"
        Me.cmdChannelSchedules.Size = New System.Drawing.Size(57, 23)
        Me.cmdChannelSchedules.TabIndex = 7
        Me.cmdChannelSchedules.Text = "Browse"
        Me.cmdChannelSchedules.UseVisualStyleBackColor = true
        '
        'cmdBrowseNetworkPath
        '
        Me.cmdBrowseNetworkPath.FlatAppearance.BorderSize = 0
        Me.cmdBrowseNetworkPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdBrowseNetworkPath.Location = New System.Drawing.Point(350, 29)
        Me.cmdBrowseNetworkPath.Name = "cmdBrowseNetworkPath"
        Me.cmdBrowseNetworkPath.Size = New System.Drawing.Size(57, 23)
        Me.cmdBrowseNetworkPath.TabIndex = 6
        Me.cmdBrowseNetworkPath.Text = "Browse"
        Me.cmdBrowseNetworkPath.UseVisualStyleBackColor = true
        '
        'txtCampaignFiles
        '
        Me.txtCampaignFiles.Location = New System.Drawing.Point(6, 150)
        Me.txtCampaignFiles.Name = "txtCampaignFiles"
        Me.txtCampaignFiles.Size = New System.Drawing.Size(338, 22)
        Me.txtCampaignFiles.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(6, 133)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Campaign files"
        '
        'txtChannelSchedules
        '
        Me.txtChannelSchedules.Location = New System.Drawing.Point(6, 110)
        Me.txtChannelSchedules.Name = "txtChannelSchedules"
        Me.txtChannelSchedules.Size = New System.Drawing.Size(338, 22)
        Me.txtChannelSchedules.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(6, 93)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Channel schedules"
        '
        'txtDataPath
        '
        Me.txtDataPath.Location = New System.Drawing.Point(6, 30)
        Me.txtDataPath.Name = "txtDataPath"
        Me.txtDataPath.Size = New System.Drawing.Size(338, 22)
        Me.txtDataPath.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Location = New System.Drawing.Point(6, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Network data path"
        '
        'dlgColor
        '
        Me.dlgColor.AnyColor = true
        Me.dlgColor.FullOpen = true
        '
        'grpScheme
        '
        Me.grpScheme.Controls.Add(Me.cmdFont)
        Me.grpScheme.Controls.Add(Me.Label11)
        Me.grpScheme.Controls.Add(Me.Label12)
        Me.grpScheme.Controls.Add(Me.picHeadline)
        Me.grpScheme.Controls.Add(Me.Label10)
        Me.grpScheme.Controls.Add(Me.picText)
        Me.grpScheme.Controls.Add(Me.picPanelBG)
        Me.grpScheme.Controls.Add(Me.Label8)
        Me.grpScheme.Controls.Add(Me.Label9)
        Me.grpScheme.Controls.Add(Me.picPanelFG)
        Me.grpScheme.Location = New System.Drawing.Point(6, 91)
        Me.grpScheme.Name = "grpScheme"
        Me.grpScheme.Size = New System.Drawing.Size(187, 176)
        Me.grpScheme.TabIndex = 3
        Me.grpScheme.TabStop = false
        Me.grpScheme.Text = "Text Settings"
        '
        'cmdFont
        '
        Me.cmdFont.FlatAppearance.BorderSize = 0
        Me.cmdFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFont.Location = New System.Drawing.Point(61, 27)
        Me.cmdFont.Name = "cmdFont"
        Me.cmdFont.Size = New System.Drawing.Size(82, 21)
        Me.cmdFont.TabIndex = 23
        Me.cmdFont.Text = "Font"
        Me.cmdFont.UseVisualStyleBackColor = true
        '
        'Label11
        '
        Me.Label11.AutoSize = true
        Me.Label11.Location = New System.Drawing.Point(21, 139)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(29, 13)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Text:"
        '
        'Label12
        '
        Me.Label12.AutoSize = true
        Me.Label12.Location = New System.Drawing.Point(21, 30)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(34, 13)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "Font:"
        '
        'picHeadline
        '
        Me.picHeadline.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picHeadline.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picHeadline.Location = New System.Drawing.Point(129, 62)
        Me.picHeadline.Name = "picHeadline"
        Me.picHeadline.Size = New System.Drawing.Size(14, 14)
        Me.picHeadline.TabIndex = 18
        Me.picHeadline.TabStop = false
        Me.picHeadline.Tag = "Color"
        '
        'Label10
        '
        Me.Label10.AutoSize = true
        Me.Label10.Location = New System.Drawing.Point(21, 114)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(60, 13)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Panel text:"
        '
        'picText
        '
        Me.picText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picText.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picText.Location = New System.Drawing.Point(129, 139)
        Me.picText.Name = "picText"
        Me.picText.Size = New System.Drawing.Size(14, 14)
        Me.picText.TabIndex = 21
        Me.picText.TabStop = false
        Me.picText.Tag = "Color"
        '
        'picPanelBG
        '
        Me.picPanelBG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPanelBG.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picPanelBG.Location = New System.Drawing.Point(129, 88)
        Me.picPanelBG.Name = "picPanelBG"
        Me.picPanelBG.Size = New System.Drawing.Size(14, 14)
        Me.picPanelBG.TabIndex = 19
        Me.picPanelBG.TabStop = false
        Me.picPanelBG.Tag = "Color"
        '
        'Label8
        '
        Me.Label8.AutoSize = true
        Me.Label8.Location = New System.Drawing.Point(21, 62)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Headline:"
        '
        'Label9
        '
        Me.Label9.AutoSize = true
        Me.Label9.Location = New System.Drawing.Point(21, 88)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(104, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Panel background:"
        '
        'picPanelFG
        '
        Me.picPanelFG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPanelFG.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picPanelFG.Location = New System.Drawing.Point(129, 114)
        Me.picPanelFG.Name = "picPanelFG"
        Me.picPanelFG.Size = New System.Drawing.Size(14, 14)
        Me.picPanelFG.TabIndex = 20
        Me.picPanelFG.TabStop = false
        Me.picPanelFG.Tag = "Color"
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.Location = New System.Drawing.Point(12, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Scheme"
        '
        'cmbColorScheme
        '
        Me.cmbColorScheme.FormattingEnabled = true
        Me.cmbColorScheme.Location = New System.Drawing.Point(12, 34)
        Me.cmbColorScheme.Name = "cmbColorScheme"
        Me.cmbColorScheme.Size = New System.Drawing.Size(125, 21)
        Me.cmbColorScheme.TabIndex = 1
        '
        'chkMarathon
        '
        Me.chkMarathon.AutoSize = true
        Me.chkMarathon.Location = New System.Drawing.Point(91, 309)
        Me.chkMarathon.Name = "chkMarathon"
        Me.chkMarathon.Size = New System.Drawing.Size(99, 17)
        Me.chkMarathon.TabIndex = 6
        Me.chkMarathon.Text = "Use Marathon"
        Me.ToolTip.SetToolTip(Me.chkMarathon, "Logg all events for debuging (slows the program down)")
        Me.chkMarathon.UseVisualStyleBackColor = true
        Me.chkMarathon.Visible = false
        '
        'dlgBrowse
        '
        Me.dlgBrowse.ShowNewFolderButton = false
        '
        'chkDebug
        '
        Me.chkDebug.AutoSize = true
        Me.chkDebug.Location = New System.Drawing.Point(6, 309)
        Me.chkDebug.Name = "chkDebug"
        Me.chkDebug.Size = New System.Drawing.Size(81, 17)
        Me.chkDebug.TabIndex = 4
        Me.chkDebug.Text = "Create Log"
        Me.ToolTip.SetToolTip(Me.chkDebug, "Logg all events for debuging (slows the program down)")
        Me.chkDebug.UseVisualStyleBackColor = true
        '
        'chkAutosave
        '
        Me.chkAutosave.AutoSize = true
        Me.chkAutosave.Location = New System.Drawing.Point(195, 37)
        Me.chkAutosave.Name = "chkAutosave"
        Me.chkAutosave.Size = New System.Drawing.Size(126, 17)
        Me.chkAutosave.TabIndex = 7
        Me.chkAutosave.Text = "Autosave campaign"
        Me.ToolTip.SetToolTip(Me.chkAutosave, "Campaign will be autosaved as 'autosave.cmp' in Trinity-directory")
        Me.chkAutosave.UseVisualStyleBackColor = true
        '
        'cmdSync
        '
        Me.cmdSync.FlatAppearance.BorderSize = 0
        Me.cmdSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSync.Location = New System.Drawing.Point(340, 34)
        Me.cmdSync.Name = "cmdSync"
        Me.cmdSync.Size = New System.Drawing.Size(57, 23)
        Me.cmdSync.TabIndex = 14
        Me.cmdSync.Text = "Sync"
        Me.ToolTip.SetToolTip(Me.cmdSync, "Syncronize local path with network path")
        Me.cmdSync.UseVisualStyleBackColor = true
        Me.cmdSync.Visible = false
        '
        'cmdAddColorScheme
        '
        Me.cmdAddColorScheme.FlatAppearance.BorderSize = 0
        Me.cmdAddColorScheme.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddColorScheme.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.cmdAddColorScheme.Location = New System.Drawing.Point(143, 33)
        Me.cmdAddColorScheme.Name = "cmdAddColorScheme"
        Me.cmdAddColorScheme.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddColorScheme.TabIndex = 12
        Me.ToolTip.SetToolTip(Me.cmdAddColorScheme, "Add Color Scheme")
        Me.cmdAddColorScheme.UseVisualStyleBackColor = true
        '
        'tabPref
        '
        Me.tabPref.Controls.Add(Me.tpUserInfo)
        Me.tabPref.Controls.Add(Me.tpSettings)
        Me.tabPref.Controls.Add(Me.tpLayout)
        Me.tabPref.Controls.Add(Me.tpPaths)
        Me.tabPref.Controls.Add(Me.tpDatabase)
        Me.tabPref.Controls.Add(Me.tpAdtoox)
        Me.tabPref.Location = New System.Drawing.Point(2, 2)
        Me.tabPref.Name = "tabPref"
        Me.tabPref.SelectedIndex = 0
        Me.tabPref.Size = New System.Drawing.Size(443, 301)
        Me.tabPref.TabIndex = 6
        '
        'tpUserInfo
        '
        Me.tpUserInfo.Controls.Add(Me.GroupBox1)
        Me.tpUserInfo.Location = New System.Drawing.Point(4, 22)
        Me.tpUserInfo.Name = "tpUserInfo"
        Me.tpUserInfo.Padding = New System.Windows.Forms.Padding(3)
        Me.tpUserInfo.Size = New System.Drawing.Size(435, 275)
        Me.tpUserInfo.TabIndex = 0
        Me.tpUserInfo.Text = "User info"
        Me.tpUserInfo.UseVisualStyleBackColor = true
        '
        'tpSettings
        '
        Me.tpSettings.Controls.Add(Me.GroupBox4)
        Me.tpSettings.Controls.Add(Me.GroupBox6)
        Me.tpSettings.Controls.Add(Me.GroupBox5)
        Me.tpSettings.Controls.Add(Me.GroupBox3)
        Me.tpSettings.Location = New System.Drawing.Point(4, 22)
        Me.tpSettings.Name = "tpSettings"
        Me.tpSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSettings.Size = New System.Drawing.Size(435, 275)
        Me.tpSettings.TabIndex = 3
        Me.tpSettings.Text = "Settings"
        Me.tpSettings.UseVisualStyleBackColor = true
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label34)
        Me.GroupBox4.Controls.Add(Me.chkErrorChecking)
        Me.GroupBox4.Location = New System.Drawing.Point(236, 6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(191, 74)
        Me.GroupBox4.TabIndex = 9
        Me.GroupBox4.TabStop = false
        Me.GroupBox4.Text = "Error checking"
        Me.GroupBox4.Visible = false
        '
        'Label34
        '
        Me.Label34.Location = New System.Drawing.Point(6, 40)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(150, 31)
        Me.Label34.TabIndex = 2
        Me.Label34.Text = "Disabling this may speed up Trinity slightly"
        Me.Label34.Visible = false
        '
        'chkErrorChecking
        '
        Me.chkErrorChecking.AutoSize = true
        Me.chkErrorChecking.Location = New System.Drawing.Point(6, 19)
        Me.chkErrorChecking.Name = "chkErrorChecking"
        Me.chkErrorChecking.Size = New System.Drawing.Size(61, 17)
        Me.chkErrorChecking.TabIndex = 1
        Me.chkErrorChecking.Text = "Enable"
        Me.chkErrorChecking.UseVisualStyleBackColor = true
        Me.chkErrorChecking.Visible = false
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label29)
        Me.GroupBox6.Controls.Add(Me.cmbMonitor)
        Me.GroupBox6.Controls.Add(Me.chkAutosave)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 202)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(421, 66)
        Me.GroupBox6.TabIndex = 8
        Me.GroupBox6.TabStop = false
        Me.GroupBox6.Text = "General preferences"
        '
        'Label29
        '
        Me.Label29.AutoSize = true
        Me.Label29.Location = New System.Drawing.Point(6, 16)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(119, 13)
        Me.Label29.TabIndex = 8
        Me.Label29.Text = "Default Monitor chart"
        '
        'cmbMonitor
        '
        Me.cmbMonitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonitor.FormattingEnabled = true
        Me.cmbMonitor.Items.AddRange(New Object() {"Total", "Allocation", "Channels", "Weekdays", "Dayparts", "Films", "Weeks", "Pos In Break", "Break Type", "Reach"})
        Me.cmbMonitor.Location = New System.Drawing.Point(6, 33)
        Me.cmbMonitor.Name = "cmbMonitor"
        Me.cmbMonitor.Size = New System.Drawing.Size(149, 21)
        Me.cmbMonitor.TabIndex = 0
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.chkLoadCombinations)
        Me.GroupBox5.Controls.Add(Me.chkLoadAV)
        Me.GroupBox5.Controls.Add(Me.chkLoadIndexes)
        Me.GroupBox5.Controls.Add(Me.chkLoadPrices)
        Me.GroupBox5.Controls.Add(Me.chkLoadCosts)
        Me.GroupBox5.Controls.Add(Me.cmdBrowseContract)
        Me.GroupBox5.Controls.Add(Me.txtContractPath)
        Me.GroupBox5.Controls.Add(Me.Label28)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 86)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(421, 110)
        Me.GroupBox5.TabIndex = 1
        Me.GroupBox5.TabStop = false
        Me.GroupBox5.Text = "Default Contract"
        '
        'chkLoadCombinations
        '
        Me.chkLoadCombinations.AutoSize = true
        Me.chkLoadCombinations.Location = New System.Drawing.Point(218, 61)
        Me.chkLoadCombinations.Name = "chkLoadCombinations"
        Me.chkLoadCombinations.Size = New System.Drawing.Size(126, 17)
        Me.chkLoadCombinations.TabIndex = 20
        Me.chkLoadCombinations.Text = "Load Combinations"
        Me.chkLoadCombinations.UseVisualStyleBackColor = true
        '
        'chkLoadAV
        '
        Me.chkLoadAV.AutoSize = true
        Me.chkLoadAV.Location = New System.Drawing.Point(95, 83)
        Me.chkLoadAV.Name = "chkLoadAV"
        Me.chkLoadAV.Size = New System.Drawing.Size(124, 17)
        Me.chkLoadAV.TabIndex = 19
        Me.chkLoadAV.Text = "Load Added Values"
        Me.chkLoadAV.UseVisualStyleBackColor = true
        '
        'chkLoadIndexes
        '
        Me.chkLoadIndexes.AutoSize = true
        Me.chkLoadIndexes.Location = New System.Drawing.Point(95, 59)
        Me.chkLoadIndexes.Name = "chkLoadIndexes"
        Me.chkLoadIndexes.Size = New System.Drawing.Size(93, 17)
        Me.chkLoadIndexes.TabIndex = 18
        Me.chkLoadIndexes.Text = "Load Indexes"
        Me.chkLoadIndexes.UseVisualStyleBackColor = true
        '
        'chkLoadPrices
        '
        Me.chkLoadPrices.AutoSize = true
        Me.chkLoadPrices.Location = New System.Drawing.Point(6, 83)
        Me.chkLoadPrices.Name = "chkLoadPrices"
        Me.chkLoadPrices.Size = New System.Drawing.Size(90, 17)
        Me.chkLoadPrices.TabIndex = 17
        Me.chkLoadPrices.Text = "Load Targets"
        Me.chkLoadPrices.UseVisualStyleBackColor = true
        '
        'chkLoadCosts
        '
        Me.chkLoadCosts.AutoSize = true
        Me.chkLoadCosts.Location = New System.Drawing.Point(6, 59)
        Me.chkLoadCosts.Name = "chkLoadCosts"
        Me.chkLoadCosts.Size = New System.Drawing.Size(82, 17)
        Me.chkLoadCosts.TabIndex = 16
        Me.chkLoadCosts.Text = "Load Costs"
        Me.chkLoadCosts.UseVisualStyleBackColor = true
        '
        'cmdBrowseContract
        '
        Me.cmdBrowseContract.FlatAppearance.BorderSize = 0
        Me.cmdBrowseContract.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdBrowseContract.Location = New System.Drawing.Point(276, 32)
        Me.cmdBrowseContract.Name = "cmdBrowseContract"
        Me.cmdBrowseContract.Size = New System.Drawing.Size(57, 23)
        Me.cmdBrowseContract.TabIndex = 15
        Me.cmdBrowseContract.Text = "Browse"
        Me.cmdBrowseContract.UseVisualStyleBackColor = true
        '
        'txtContractPath
        '
        Me.txtContractPath.Location = New System.Drawing.Point(6, 33)
        Me.txtContractPath.Name = "txtContractPath"
        Me.txtContractPath.Size = New System.Drawing.Size(264, 22)
        Me.txtContractPath.TabIndex = 14
        '
        'Label28
        '
        Me.Label28.AutoSize = true
        Me.Label28.Location = New System.Drawing.Point(6, 16)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(30, 13)
        Me.Label28.TabIndex = 13
        Me.Label28.Text = "Path"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtPIBLast)
        Me.GroupBox3.Controls.Add(Me.txtPIBFirst)
        Me.GroupBox3.Controls.Add(Me.Label27)
        Me.GroupBox3.Controls.Add(Me.Label26)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(219, 74)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = false
        Me.GroupBox3.Text = "Position in break chart"
        '
        'txtPIBLast
        '
        Me.txtPIBLast.Location = New System.Drawing.Point(59, 45)
        Me.txtPIBLast.Name = "txtPIBLast"
        Me.txtPIBLast.Size = New System.Drawing.Size(42, 22)
        Me.txtPIBLast.TabIndex = 3
        '
        'txtPIBFirst
        '
        Me.txtPIBFirst.Location = New System.Drawing.Point(59, 19)
        Me.txtPIBFirst.Name = "txtPIBFirst"
        Me.txtPIBFirst.Size = New System.Drawing.Size(42, 22)
        Me.txtPIBFirst.TabIndex = 2
        '
        'Label27
        '
        Me.Label27.AutoSize = true
        Me.Label27.Location = New System.Drawing.Point(6, 48)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(153, 13)
        Me.Label27.TabIndex = 1
        Me.Label27.Text = "Show the                last spots"
        '
        'Label26
        '
        Me.Label26.AutoSize = true
        Me.Label26.Location = New System.Drawing.Point(6, 22)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(155, 13)
        Me.Label26.TabIndex = 0
        Me.Label26.Text = "Show the                first spots"
        '
        'tpLayout
        '
        Me.tpLayout.Controls.Add(Me.Label7)
        Me.tpLayout.Controls.Add(Me.grpColoring)
        Me.tpLayout.Controls.Add(Me.cmbColorScheme)
        Me.tpLayout.Controls.Add(Me.grpScheme)
        Me.tpLayout.Controls.Add(Me.cmdAddColorScheme)
        Me.tpLayout.Location = New System.Drawing.Point(4, 22)
        Me.tpLayout.Name = "tpLayout"
        Me.tpLayout.Padding = New System.Windows.Forms.Padding(3)
        Me.tpLayout.Size = New System.Drawing.Size(435, 275)
        Me.tpLayout.TabIndex = 1
        Me.tpLayout.Text = "Layout"
        Me.tpLayout.UseVisualStyleBackColor = true
        '
        'grpColoring
        '
        Me.grpColoring.Controls.Add(Me.Label24)
        Me.grpColoring.Controls.Add(Me.Label25)
        Me.grpColoring.Controls.Add(Me.pbc10)
        Me.grpColoring.Controls.Add(Me.pbc9)
        Me.grpColoring.Controls.Add(Me.pbc8)
        Me.grpColoring.Controls.Add(Me.Label20)
        Me.grpColoring.Controls.Add(Me.pbc7)
        Me.grpColoring.Controls.Add(Me.Label21)
        Me.grpColoring.Controls.Add(Me.pbc6)
        Me.grpColoring.Controls.Add(Me.Label22)
        Me.grpColoring.Controls.Add(Me.pbc5)
        Me.grpColoring.Controls.Add(Me.Label23)
        Me.grpColoring.Controls.Add(Me.pbc4)
        Me.grpColoring.Controls.Add(Me.Label18)
        Me.grpColoring.Controls.Add(Me.pbc3)
        Me.grpColoring.Controls.Add(Me.Label19)
        Me.grpColoring.Controls.Add(Me.pbc2)
        Me.grpColoring.Controls.Add(Me.Label17)
        Me.grpColoring.Controls.Add(Me.pbc1)
        Me.grpColoring.Controls.Add(Me.Label16)
        Me.grpColoring.Location = New System.Drawing.Point(217, 8)
        Me.grpColoring.Name = "grpColoring"
        Me.grpColoring.Size = New System.Drawing.Size(210, 261)
        Me.grpColoring.TabIndex = 4
        Me.grpColoring.TabStop = false
        Me.grpColoring.Text = "Diagram coloring"
        '
        'Label24
        '
        Me.Label24.AutoSize = true
        Me.Label24.Location = New System.Drawing.Point(26, 243)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(94, 13)
        Me.Label24.TabIndex = 59
        Me.Label24.Text = "Diagram color 10"
        '
        'Label25
        '
        Me.Label25.AutoSize = true
        Me.Label25.Location = New System.Drawing.Point(26, 217)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(88, 13)
        Me.Label25.TabIndex = 58
        Me.Label25.Text = "Diagram color 9"
        '
        'pbc10
        '
        Me.pbc10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbc10.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbc10.Location = New System.Drawing.Point(141, 243)
        Me.pbc10.Name = "pbc10"
        Me.pbc10.Size = New System.Drawing.Size(14, 14)
        Me.pbc10.TabIndex = 57
        Me.pbc10.TabStop = false
        Me.pbc10.Tag = "Color"
        '
        'pbc9
        '
        Me.pbc9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbc9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbc9.Location = New System.Drawing.Point(141, 217)
        Me.pbc9.Name = "pbc9"
        Me.pbc9.Size = New System.Drawing.Size(14, 14)
        Me.pbc9.TabIndex = 56
        Me.pbc9.TabStop = false
        Me.pbc9.Tag = "Color"
        '
        'pbc8
        '
        Me.pbc8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbc8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbc8.Location = New System.Drawing.Point(141, 192)
        Me.pbc8.Name = "pbc8"
        Me.pbc8.Size = New System.Drawing.Size(14, 14)
        Me.pbc8.TabIndex = 55
        Me.pbc8.TabStop = false
        Me.pbc8.Tag = "Color"
        '
        'Label20
        '
        Me.Label20.AutoSize = true
        Me.Label20.Location = New System.Drawing.Point(26, 192)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(88, 13)
        Me.Label20.TabIndex = 54
        Me.Label20.Text = "Diagram color 8"
        '
        'pbc7
        '
        Me.pbc7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbc7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbc7.Location = New System.Drawing.Point(141, 169)
        Me.pbc7.Name = "pbc7"
        Me.pbc7.Size = New System.Drawing.Size(14, 14)
        Me.pbc7.TabIndex = 53
        Me.pbc7.TabStop = false
        Me.pbc7.Tag = "Color"
        '
        'Label21
        '
        Me.Label21.AutoSize = true
        Me.Label21.Location = New System.Drawing.Point(26, 169)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(88, 13)
        Me.Label21.TabIndex = 52
        Me.Label21.Text = "Diagram color 7"
        '
        'pbc6
        '
        Me.pbc6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbc6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbc6.Location = New System.Drawing.Point(141, 143)
        Me.pbc6.Name = "pbc6"
        Me.pbc6.Size = New System.Drawing.Size(14, 14)
        Me.pbc6.TabIndex = 51
        Me.pbc6.TabStop = false
        Me.pbc6.Tag = "Color"
        '
        'Label22
        '
        Me.Label22.AutoSize = true
        Me.Label22.Location = New System.Drawing.Point(26, 143)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(88, 13)
        Me.Label22.TabIndex = 50
        Me.Label22.Text = "Diagram color 6"
        '
        'pbc5
        '
        Me.pbc5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbc5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbc5.Location = New System.Drawing.Point(141, 117)
        Me.pbc5.Name = "pbc5"
        Me.pbc5.Size = New System.Drawing.Size(14, 14)
        Me.pbc5.TabIndex = 49
        Me.pbc5.TabStop = false
        Me.pbc5.Tag = "Color"
        '
        'Label23
        '
        Me.Label23.AutoSize = true
        Me.Label23.Location = New System.Drawing.Point(26, 117)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(88, 13)
        Me.Label23.TabIndex = 48
        Me.Label23.Text = "Diagram color 5"
        '
        'pbc4
        '
        Me.pbc4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbc4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbc4.Location = New System.Drawing.Point(141, 92)
        Me.pbc4.Name = "pbc4"
        Me.pbc4.Size = New System.Drawing.Size(14, 14)
        Me.pbc4.TabIndex = 47
        Me.pbc4.TabStop = false
        Me.pbc4.Tag = "Color"
        '
        'Label18
        '
        Me.Label18.AutoSize = true
        Me.Label18.Location = New System.Drawing.Point(26, 92)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(88, 13)
        Me.Label18.TabIndex = 46
        Me.Label18.Text = "Diagram color 4"
        '
        'pbc3
        '
        Me.pbc3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbc3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbc3.Location = New System.Drawing.Point(141, 67)
        Me.pbc3.Name = "pbc3"
        Me.pbc3.Size = New System.Drawing.Size(14, 14)
        Me.pbc3.TabIndex = 45
        Me.pbc3.TabStop = false
        Me.pbc3.Tag = "Color"
        '
        'Label19
        '
        Me.Label19.AutoSize = true
        Me.Label19.Location = New System.Drawing.Point(26, 67)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(88, 13)
        Me.Label19.TabIndex = 44
        Me.Label19.Text = "Diagram color 3"
        '
        'pbc2
        '
        Me.pbc2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbc2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbc2.Location = New System.Drawing.Point(141, 42)
        Me.pbc2.Name = "pbc2"
        Me.pbc2.Size = New System.Drawing.Size(14, 14)
        Me.pbc2.TabIndex = 43
        Me.pbc2.TabStop = false
        Me.pbc2.Tag = "Color"
        '
        'Label17
        '
        Me.Label17.AutoSize = true
        Me.Label17.Location = New System.Drawing.Point(26, 42)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(88, 13)
        Me.Label17.TabIndex = 42
        Me.Label17.Text = "Diagram color 2"
        '
        'pbc1
        '
        Me.pbc1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbc1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbc1.Location = New System.Drawing.Point(141, 17)
        Me.pbc1.Name = "pbc1"
        Me.pbc1.Size = New System.Drawing.Size(14, 14)
        Me.pbc1.TabIndex = 41
        Me.pbc1.TabStop = false
        Me.pbc1.Tag = "Color"
        '
        'Label16
        '
        Me.Label16.AutoSize = true
        Me.Label16.Location = New System.Drawing.Point(26, 17)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(88, 13)
        Me.Label16.TabIndex = 40
        Me.Label16.Text = "Diagram color 1"
        '
        'tpPaths
        '
        Me.tpPaths.Controls.Add(Me.Label33)
        Me.tpPaths.Controls.Add(Me.cmdBrowseSharedPath)
        Me.tpPaths.Controls.Add(Me.txtSharedDataPath)
        Me.tpPaths.Controls.Add(Me.cmdCampaignFiles)
        Me.tpPaths.Controls.Add(Me.cmdChannelSchedules)
        Me.tpPaths.Controls.Add(Me.Label6)
        Me.tpPaths.Controls.Add(Me.cmdBrowseNetworkPath)
        Me.tpPaths.Controls.Add(Me.txtDataPath)
        Me.tpPaths.Controls.Add(Me.txtCampaignFiles)
        Me.tpPaths.Controls.Add(Me.Label5)
        Me.tpPaths.Controls.Add(Me.Label4)
        Me.tpPaths.Controls.Add(Me.txtChannelSchedules)
        Me.tpPaths.Location = New System.Drawing.Point(4, 22)
        Me.tpPaths.Name = "tpPaths"
        Me.tpPaths.Size = New System.Drawing.Size(435, 275)
        Me.tpPaths.TabIndex = 2
        Me.tpPaths.Text = "Paths"
        Me.tpPaths.UseVisualStyleBackColor = true
        '
        'Label33
        '
        Me.Label33.AutoSize = true
        Me.Label33.Location = New System.Drawing.Point(6, 53)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(96, 13)
        Me.Label33.TabIndex = 9
        Me.Label33.Text = "Shared data path"
        '
        'cmdBrowseSharedPath
        '
        Me.cmdBrowseSharedPath.FlatAppearance.BorderSize = 0
        Me.cmdBrowseSharedPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdBrowseSharedPath.Location = New System.Drawing.Point(350, 69)
        Me.cmdBrowseSharedPath.Name = "cmdBrowseSharedPath"
        Me.cmdBrowseSharedPath.Size = New System.Drawing.Size(57, 23)
        Me.cmdBrowseSharedPath.TabIndex = 11
        Me.cmdBrowseSharedPath.Text = "Browse"
        Me.cmdBrowseSharedPath.UseVisualStyleBackColor = true
        '
        'txtSharedDataPath
        '
        Me.txtSharedDataPath.Location = New System.Drawing.Point(6, 70)
        Me.txtSharedDataPath.Name = "txtSharedDataPath"
        Me.txtSharedDataPath.Size = New System.Drawing.Size(338, 22)
        Me.txtSharedDataPath.TabIndex = 10
        '
        'tpDatabase
        '
        Me.tpDatabase.Controls.Add(Me.GroupBox2)
        Me.tpDatabase.Controls.Add(Me.cmdSyncNow)
        Me.tpDatabase.Controls.Add(Me.cmbSync)
        Me.tpDatabase.Controls.Add(Me.Label30)
        Me.tpDatabase.Controls.Add(Me.cmdSync)
        Me.tpDatabase.Controls.Add(Me.cmdBrowseLocalDB)
        Me.tpDatabase.Controls.Add(Me.Label14)
        Me.tpDatabase.Controls.Add(Me.cmdBrowseNetworkDB)
        Me.tpDatabase.Controls.Add(Me.txtNetworkDB)
        Me.tpDatabase.Controls.Add(Me.txtLocalDB)
        Me.tpDatabase.Controls.Add(Me.Label13)
        Me.tpDatabase.Location = New System.Drawing.Point(4, 22)
        Me.tpDatabase.Name = "tpDatabase"
        Me.tpDatabase.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDatabase.Size = New System.Drawing.Size(435, 275)
        Me.tpDatabase.TabIndex = 4
        Me.tpDatabase.Text = "Database"
        Me.tpDatabase.UseVisualStyleBackColor = true
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtMatrixDB)
        Me.GroupBox2.Controls.Add(Me.Label32)
        Me.GroupBox2.Controls.Add(Me.txtMatrixServer)
        Me.GroupBox2.Controls.Add(Me.Label31)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 148)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(262, 100)
        Me.GroupBox2.TabIndex = 18
        Me.GroupBox2.TabStop = false
        Me.GroupBox2.Text = "Matrix"
        '
        'txtMatrixDB
        '
        Me.txtMatrixDB.Location = New System.Drawing.Point(6, 73)
        Me.txtMatrixDB.Name = "txtMatrixDB"
        Me.txtMatrixDB.Size = New System.Drawing.Size(250, 22)
        Me.txtMatrixDB.TabIndex = 3
        '
        'Label32
        '
        Me.Label32.AutoSize = true
        Me.Label32.Location = New System.Drawing.Point(6, 56)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(55, 13)
        Me.Label32.TabIndex = 2
        Me.Label32.Text = "Database"
        '
        'txtMatrixServer
        '
        Me.txtMatrixServer.Location = New System.Drawing.Point(6, 33)
        Me.txtMatrixServer.Name = "txtMatrixServer"
        Me.txtMatrixServer.Size = New System.Drawing.Size(250, 22)
        Me.txtMatrixServer.TabIndex = 1
        '
        'Label31
        '
        Me.Label31.AutoSize = true
        Me.Label31.Location = New System.Drawing.Point(6, 16)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(38, 13)
        Me.Label31.TabIndex = 0
        Me.Label31.Text = "Server"
        '
        'cmdSyncNow
        '
        Me.cmdSyncNow.FlatAppearance.BorderSize = 0
        Me.cmdSyncNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSyncNow.Location = New System.Drawing.Point(201, 119)
        Me.cmdSyncNow.Name = "cmdSyncNow"
        Me.cmdSyncNow.Size = New System.Drawing.Size(74, 23)
        Me.cmdSyncNow.TabIndex = 17
        Me.cmdSyncNow.Text = "Update now"
        Me.cmdSyncNow.UseVisualStyleBackColor = true
        '
        'cmbSync
        '
        Me.cmbSync.FormattingEnabled = true
        Me.cmbSync.Items.AddRange(New Object() {"Never", "Every time Trinity starts", "Daily", "Weekly"})
        Me.cmbSync.Location = New System.Drawing.Point(13, 120)
        Me.cmbSync.Name = "cmbSync"
        Me.cmbSync.Size = New System.Drawing.Size(182, 21)
        Me.cmbSync.TabIndex = 16
        '
        'Label30
        '
        Me.Label30.AutoSize = true
        Me.Label30.Location = New System.Drawing.Point(13, 103)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(169, 13)
        Me.Label30.TabIndex = 15
        Me.Label30.Text = "Automatically update local data"
        '
        'cmdBrowseLocalDB
        '
        Me.cmdBrowseLocalDB.FlatAppearance.BorderSize = 0
        Me.cmdBrowseLocalDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdBrowseLocalDB.Location = New System.Drawing.Point(340, 80)
        Me.cmdBrowseLocalDB.Name = "cmdBrowseLocalDB"
        Me.cmdBrowseLocalDB.Size = New System.Drawing.Size(57, 23)
        Me.cmdBrowseLocalDB.TabIndex = 13
        Me.cmdBrowseLocalDB.Text = "Browse"
        Me.cmdBrowseLocalDB.UseVisualStyleBackColor = true
        '
        'Label14
        '
        Me.Label14.AutoSize = true
        Me.Label14.Location = New System.Drawing.Point(13, 17)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(101, 13)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "Network database"
        '
        'cmdBrowseNetworkDB
        '
        Me.cmdBrowseNetworkDB.FlatAppearance.BorderSize = 0
        Me.cmdBrowseNetworkDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdBrowseNetworkDB.Location = New System.Drawing.Point(265, 32)
        Me.cmdBrowseNetworkDB.Name = "cmdBrowseNetworkDB"
        Me.cmdBrowseNetworkDB.Size = New System.Drawing.Size(57, 23)
        Me.cmdBrowseNetworkDB.TabIndex = 12
        Me.cmdBrowseNetworkDB.Text = "Browse"
        Me.cmdBrowseNetworkDB.UseVisualStyleBackColor = true
        '
        'txtNetworkDB
        '
        Me.txtNetworkDB.Location = New System.Drawing.Point(13, 34)
        Me.txtNetworkDB.Name = "txtNetworkDB"
        Me.txtNetworkDB.Size = New System.Drawing.Size(246, 22)
        Me.txtNetworkDB.TabIndex = 9
        '
        'txtLocalDB
        '
        Me.txtLocalDB.Location = New System.Drawing.Point(13, 80)
        Me.txtLocalDB.Name = "txtLocalDB"
        Me.txtLocalDB.Size = New System.Drawing.Size(309, 22)
        Me.txtLocalDB.TabIndex = 11
        '
        'Label13
        '
        Me.Label13.AutoSize = true
        Me.Label13.Location = New System.Drawing.Point(13, 63)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(83, 13)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "Local database"
        '
        'tpAdtoox
        '
        Me.tpAdtoox.Controls.Add(Me.lblAdtooxUsername)
        Me.tpAdtoox.Controls.Add(Me.txtAdtooxUsername)
        Me.tpAdtoox.Controls.Add(Me.lblAdtooxPassword)
        Me.tpAdtoox.Controls.Add(Me.txtAdtooxPassword)
        Me.tpAdtoox.Location = New System.Drawing.Point(4, 22)
        Me.tpAdtoox.Name = "tpAdtoox"
        Me.tpAdtoox.Padding = New System.Windows.Forms.Padding(3)
        Me.tpAdtoox.Size = New System.Drawing.Size(435, 275)
        Me.tpAdtoox.TabIndex = 5
        Me.tpAdtoox.Text = "Adtoox"
        Me.tpAdtoox.UseVisualStyleBackColor = true
        '
        'lblAdtooxUsername
        '
        Me.lblAdtooxUsername.AutoSize = true
        Me.lblAdtooxUsername.Location = New System.Drawing.Point(6, 15)
        Me.lblAdtooxUsername.Name = "lblAdtooxUsername"
        Me.lblAdtooxUsername.Size = New System.Drawing.Size(98, 13)
        Me.lblAdtooxUsername.TabIndex = 12
        Me.lblAdtooxUsername.Text = "Adtoox Username"
        '
        'txtAdtooxUsername
        '
        Me.txtAdtooxUsername.Location = New System.Drawing.Point(6, 32)
        Me.txtAdtooxUsername.Name = "txtAdtooxUsername"
        Me.txtAdtooxUsername.Size = New System.Drawing.Size(199, 22)
        Me.txtAdtooxUsername.TabIndex = 13
        '
        'lblAdtooxPassword
        '
        Me.lblAdtooxPassword.AutoSize = true
        Me.lblAdtooxPassword.Location = New System.Drawing.Point(6, 59)
        Me.lblAdtooxPassword.Name = "lblAdtooxPassword"
        Me.lblAdtooxPassword.Size = New System.Drawing.Size(96, 13)
        Me.lblAdtooxPassword.TabIndex = 10
        Me.lblAdtooxPassword.Text = "Adtoox Password"
        '
        'txtAdtooxPassword
        '
        Me.txtAdtooxPassword.Location = New System.Drawing.Point(6, 76)
        Me.txtAdtooxPassword.Name = "txtAdtooxPassword"
        Me.txtAdtooxPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtAdtooxPassword.Size = New System.Drawing.Size(199, 22)
        Me.txtAdtooxPassword.TabIndex = 11
        '
        'frmPreferences
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(445, 353)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.tabPref)
        Me.Controls.Add(Me.chkDebug)
        Me.Controls.Add(Me.chkMarathon)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmPreferences"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Preferences"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.GroupBox7.ResumeLayout(false)
        Me.GroupBox7.PerformLayout
        Me.grpScheme.ResumeLayout(false)
        Me.grpScheme.PerformLayout
        CType(Me.picHeadline,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picText,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picPanelBG,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picPanelFG,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabPref.ResumeLayout(false)
        Me.tpUserInfo.ResumeLayout(false)
        Me.tpSettings.ResumeLayout(false)
        Me.GroupBox4.ResumeLayout(false)
        Me.GroupBox4.PerformLayout
        Me.GroupBox6.ResumeLayout(false)
        Me.GroupBox6.PerformLayout
        Me.GroupBox5.ResumeLayout(false)
        Me.GroupBox5.PerformLayout
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        Me.tpLayout.ResumeLayout(false)
        Me.tpLayout.PerformLayout
        Me.grpColoring.ResumeLayout(false)
        Me.grpColoring.PerformLayout
        CType(Me.pbc10,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbc9,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbc8,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbc7,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbc6,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbc5,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbc4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbc3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbc2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbc1,System.ComponentModel.ISupportInitialize).EndInit
        Me.tpPaths.ResumeLayout(false)
        Me.tpPaths.PerformLayout
        Me.tpDatabase.ResumeLayout(false)
        Me.tpDatabase.PerformLayout
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.tpAdtoox.ResumeLayout(false)
        Me.tpAdtoox.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPhoneNr As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCampaignFiles As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtChannelSchedules As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDataPath As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdCampaignFiles As System.Windows.Forms.Button
    Friend WithEvents cmdChannelSchedules As System.Windows.Forms.Button
    Friend WithEvents cmdBrowseNetworkPath As System.Windows.Forms.Button
    Friend WithEvents dlgColor As System.Windows.Forms.ColorDialog
    Friend WithEvents grpScheme As System.Windows.Forms.GroupBox
    Friend WithEvents cmbColorScheme As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmdAddColorScheme As System.Windows.Forms.Button
    Friend WithEvents picText As System.Windows.Forms.PictureBox
    Friend WithEvents picPanelFG As System.Windows.Forms.PictureBox
    Friend WithEvents picPanelBG As System.Windows.Forms.PictureBox
    Friend WithEvents picHeadline As System.Windows.Forms.PictureBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dlgBrowse As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents cmdFont As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents chkDebug As System.Windows.Forms.CheckBox
    Friend WithEvents txtMarathonUser As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents chkMarathon As System.Windows.Forms.CheckBox
    Friend WithEvents tabPref As System.Windows.Forms.TabControl
    Friend WithEvents tpUserInfo As System.Windows.Forms.TabPage
    Friend WithEvents tpLayout As System.Windows.Forms.TabPage
    Friend WithEvents tpPaths As System.Windows.Forms.TabPage
    Friend WithEvents grpColoring As System.Windows.Forms.GroupBox
    Friend WithEvents pbc8 As System.Windows.Forms.PictureBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents pbc7 As System.Windows.Forms.PictureBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents pbc6 As System.Windows.Forms.PictureBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents pbc5 As System.Windows.Forms.PictureBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents pbc4 As System.Windows.Forms.PictureBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents pbc3 As System.Windows.Forms.PictureBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents pbc2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents pbc1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents pbc10 As System.Windows.Forms.PictureBox
    Friend WithEvents pbc9 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdChangePwd As System.Windows.Forms.Button
    Friend WithEvents lblPwd As System.Windows.Forms.Label
    Friend WithEvents cmdSavePwd As System.Windows.Forms.Button
    Friend WithEvents txtPwd As System.Windows.Forms.TextBox
    Friend WithEvents tpSettings As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPIBLast As System.Windows.Forms.TextBox
    Friend WithEvents txtPIBFirst As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents chkLoadAV As System.Windows.Forms.CheckBox
    Friend WithEvents chkLoadIndexes As System.Windows.Forms.CheckBox
    Friend WithEvents chkLoadPrices As System.Windows.Forms.CheckBox
    Friend WithEvents chkLoadCosts As System.Windows.Forms.CheckBox
    Friend WithEvents cmdBrowseContract As System.Windows.Forms.Button
    Friend WithEvents txtContractPath As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents chkLoadCombinations As System.Windows.Forms.CheckBox
    Friend WithEvents chkAutosave As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents cmbMonitor As System.Windows.Forms.ComboBox
    Friend WithEvents tpDatabase As System.Windows.Forms.TabPage
    Friend WithEvents cmdSync As System.Windows.Forms.Button
    Friend WithEvents cmdBrowseLocalDB As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmdBrowseNetworkDB As System.Windows.Forms.Button
    Friend WithEvents txtNetworkDB As System.Windows.Forms.TextBox
    Friend WithEvents txtLocalDB As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbSync As System.Windows.Forms.ComboBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents cmdSyncNow As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtMatrixServer As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtMatrixDB As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents cmdBrowseSharedPath As System.Windows.Forms.Button
    Friend WithEvents txtSharedDataPath As System.Windows.Forms.TextBox
    Friend WithEvents tpAdtoox As System.Windows.Forms.TabPage
    Friend WithEvents lblAdtooxUsername As System.Windows.Forms.Label
    Friend WithEvents txtAdtooxUsername As System.Windows.Forms.TextBox
    Friend WithEvents lblAdtooxPassword As System.Windows.Forms.Label
    Friend WithEvents txtAdtooxPassword As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents chkErrorChecking As System.Windows.Forms.CheckBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents chkBetaUser As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents chkTrustedUser As System.Windows.Forms.CheckBox

End Class
