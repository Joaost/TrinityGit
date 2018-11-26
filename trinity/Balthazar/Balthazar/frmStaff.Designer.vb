<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStaff
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStaff))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.tabStaff = New System.Windows.Forms.TabControl
        Me.tabAssign = New System.Windows.Forms.TabPage
        Me.cmdPublishAllowed = New System.Windows.Forms.Button
        Me.cmdRemoveFromAll = New System.Windows.Forms.Button
        Me.cmbAllowed = New System.Windows.Forms.ComboBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.cmbFilterRole = New System.Windows.Forms.ComboBox
        Me.chkFilterRole = New System.Windows.Forms.CheckBox
        Me.picHideCampaign = New System.Windows.Forms.PictureBox
        Me.picShowCampaign = New System.Windows.Forms.PictureBox
        Me.cmdRemove = New System.Windows.Forms.Button
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.Label17 = New System.Windows.Forms.Label
        Me.tvwAllowedShifts = New System.Windows.Forms.TreeView
        Me.imgIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.Label16 = New System.Windows.Forms.Label
        Me.lvwAvailable = New System.Windows.Forms.ListView
        Me.colName = New System.Windows.Forms.ColumnHeader
        Me.colAge = New System.Windows.Forms.ColumnHeader
        Me.colDriver = New System.Windows.Forms.ColumnHeader
        Me.tpDetails = New System.Windows.Forms.TabPage
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label23 = New System.Windows.Forms.Label
        Me.grdStaff = New System.Windows.Forms.DataGridView
        Me.colGender = New System.Windows.Forms.DataGridViewImageColumn
        Me.colStaffName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colStaffAge = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.cmdPublishChosen = New System.Windows.Forms.Button
        Me.grdChosen = New System.Windows.Forms.DataGridView
        Me.colChosenName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colChosenAge = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmdWizard = New System.Windows.Forms.Button
        Me.Label24 = New System.Windows.Forms.Label
        Me.cmdUnChoose = New System.Windows.Forms.Button
        Me.cmdChoose = New System.Windows.Forms.Button
        Me.Label22 = New System.Windows.Forms.Label
        Me.grdSchedule = New System.Windows.Forms.DataGridView
        Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colFrom = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colTo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colRole = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colLocation = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colQuantity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.tpCatalog = New System.Windows.Forms.TabPage
        Me.pnlStaffInfo = New System.Windows.Forms.Panel
        Me.grpUser = New System.Windows.Forms.GroupBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.grdCV = New System.Windows.Forms.DataGridView
        Me.colEventName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colCVRole = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colCategory = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colResponsible = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.lstDriver = New System.Windows.Forms.CheckedListBox
        Me.cmdDriver = New System.Windows.Forms.Button
        Me.lblDriver = New System.Windows.Forms.Label
        Me.picPicture = New System.Windows.Forms.PictureBox
        Me.dtBirthday = New System.Windows.Forms.DateTimePicker
        Me.Label21 = New System.Windows.Forms.Label
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.cmdSave = New System.Windows.Forms.Button
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.lstAvailableForCategories = New System.Windows.Forms.CheckedListBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtExternalInfo = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtInternalInfo = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtAccount = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtClearing = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtBank = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtMobilePhone = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtWorkPhone = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtHomePhone = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtCity = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtZipCode = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtAddress2 = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtAddress1 = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.optMale = New System.Windows.Forms.RadioButton
        Me.optFemale = New System.Windows.Forms.RadioButton
        Me.txtAge = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtLastName = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtFirstName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdDeleteStaff = New System.Windows.Forms.Button
        Me.cmdAddStaff = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lvwStaff = New System.Windows.Forms.ListView
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.tabStaff.SuspendLayout()
        Me.tabAssign.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.picHideCampaign, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picShowCampaign, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpDetails.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.grdStaff, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.grdChosen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpCatalog.SuspendLayout()
        Me.pnlStaffInfo.SuspendLayout()
        Me.grpUser.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        CType(Me.grdCV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabStaff
        '
        Me.tabStaff.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabStaff.Controls.Add(Me.tabAssign)
        Me.tabStaff.Controls.Add(Me.tpDetails)
        Me.tabStaff.Controls.Add(Me.tpCatalog)
        Me.tabStaff.Location = New System.Drawing.Point(2, 2)
        Me.tabStaff.Name = "tabStaff"
        Me.tabStaff.SelectedIndex = 0
        Me.tabStaff.Size = New System.Drawing.Size(864, 829)
        Me.tabStaff.TabIndex = 0
        '
        'tabAssign
        '
        Me.tabAssign.Controls.Add(Me.cmdPublishAllowed)
        Me.tabAssign.Controls.Add(Me.cmdRemoveFromAll)
        Me.tabAssign.Controls.Add(Me.cmbAllowed)
        Me.tabAssign.Controls.Add(Me.GroupBox5)
        Me.tabAssign.Controls.Add(Me.cmdRemove)
        Me.tabAssign.Controls.Add(Me.cmdAdd)
        Me.tabAssign.Controls.Add(Me.Label17)
        Me.tabAssign.Controls.Add(Me.tvwAllowedShifts)
        Me.tabAssign.Controls.Add(Me.Label16)
        Me.tabAssign.Controls.Add(Me.lvwAvailable)
        Me.tabAssign.Location = New System.Drawing.Point(4, 23)
        Me.tabAssign.Name = "tabAssign"
        Me.tabAssign.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAssign.Size = New System.Drawing.Size(856, 802)
        Me.tabAssign.TabIndex = 0
        Me.tabAssign.Text = "Assign staff"
        Me.tabAssign.UseVisualStyleBackColor = True
        '
        'cmdPublishAllowed
        '
        Me.cmdPublishAllowed.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPublishAllowed.Image = Global.Balthazar.My.Resources.Resources.environment
        Me.cmdPublishAllowed.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPublishAllowed.Location = New System.Drawing.Point(628, 3)
        Me.cmdPublishAllowed.Name = "cmdPublishAllowed"
        Me.cmdPublishAllowed.Size = New System.Drawing.Size(69, 29)
        Me.cmdPublishAllowed.TabIndex = 10
        Me.cmdPublishAllowed.Text = "Publish"
        Me.cmdPublishAllowed.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdPublishAllowed.UseVisualStyleBackColor = True
        '
        'cmdRemoveFromAll
        '
        Me.cmdRemoveFromAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveFromAll.Image = Global.Balthazar.My.Resources.Resources.navigate_left2
        Me.cmdRemoveFromAll.Location = New System.Drawing.Point(377, 349)
        Me.cmdRemoveFromAll.Name = "cmdRemoveFromAll"
        Me.cmdRemoveFromAll.Size = New System.Drawing.Size(28, 27)
        Me.cmdRemoveFromAll.TabIndex = 9
        Me.cmdRemoveFromAll.UseVisualStyleBackColor = True
        '
        'cmbAllowed
        '
        Me.cmbAllowed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAllowed.FormattingEnabled = True
        Me.cmbAllowed.Items.AddRange(New Object() {"By Role", "By Location"})
        Me.cmbAllowed.Location = New System.Drawing.Point(491, 6)
        Me.cmbAllowed.Name = "cmbAllowed"
        Me.cmbAllowed.Size = New System.Drawing.Size(121, 22)
        Me.cmbAllowed.TabIndex = 8
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.cmbFilterRole)
        Me.GroupBox5.Controls.Add(Me.chkFilterRole)
        Me.GroupBox5.Controls.Add(Me.picHideCampaign)
        Me.GroupBox5.Controls.Add(Me.picShowCampaign)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 597)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(285, 54)
        Me.GroupBox5.TabIndex = 7
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "       Filter"
        '
        'cmbFilterRole
        '
        Me.cmbFilterRole.DisplayMember = "Name"
        Me.cmbFilterRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFilterRole.FormattingEnabled = True
        Me.cmbFilterRole.Location = New System.Drawing.Point(170, 19)
        Me.cmbFilterRole.Name = "cmbFilterRole"
        Me.cmbFilterRole.Size = New System.Drawing.Size(109, 22)
        Me.cmbFilterRole.TabIndex = 12
        '
        'chkFilterRole
        '
        Me.chkFilterRole.AutoSize = True
        Me.chkFilterRole.Location = New System.Drawing.Point(6, 21)
        Me.chkFilterRole.Name = "chkFilterRole"
        Me.chkFilterRole.Size = New System.Drawing.Size(158, 18)
        Me.chkFilterRole.TabIndex = 11
        Me.chkFilterRole.Text = "Only show staff fit for role:"
        Me.chkFilterRole.UseVisualStyleBackColor = True
        '
        'picHideCampaign
        '
        Me.picHideCampaign.Image = Global.Balthazar.My.Resources.Resources.navigate_down
        Me.picHideCampaign.Location = New System.Drawing.Point(11, -1)
        Me.picHideCampaign.Name = "picHideCampaign"
        Me.picHideCampaign.Size = New System.Drawing.Size(16, 16)
        Me.picHideCampaign.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picHideCampaign.TabIndex = 10
        Me.picHideCampaign.TabStop = False
        '
        'picShowCampaign
        '
        Me.picShowCampaign.Image = Global.Balthazar.My.Resources.Resources.navigate_right
        Me.picShowCampaign.Location = New System.Drawing.Point(11, -1)
        Me.picShowCampaign.Name = "picShowCampaign"
        Me.picShowCampaign.Size = New System.Drawing.Size(16, 16)
        Me.picShowCampaign.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picShowCampaign.TabIndex = 9
        Me.picShowCampaign.TabStop = False
        '
        'cmdRemove
        '
        Me.cmdRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdRemove.Image = Global.Balthazar.My.Resources.Resources.navigate_left1
        Me.cmdRemove.Location = New System.Drawing.Point(377, 316)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(28, 27)
        Me.cmdRemove.TabIndex = 6
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.Image = Global.Balthazar.My.Resources.Resources.navigate_right
        Me.cmdAdd.Location = New System.Drawing.Point(377, 283)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(28, 27)
        Me.cmdAdd.TabIndex = 5
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(408, 9)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(74, 14)
        Me.Label17.TabIndex = 4
        Me.Label17.Text = "Allowed roles"
        '
        'tvwAllowedShifts
        '
        Me.tvwAllowedShifts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tvwAllowedShifts.ImageIndex = 2
        Me.tvwAllowedShifts.ImageList = Me.imgIcons
        Me.tvwAllowedShifts.Location = New System.Drawing.Point(411, 34)
        Me.tvwAllowedShifts.Name = "tvwAllowedShifts"
        Me.tvwAllowedShifts.SelectedImageIndex = 2
        Me.tvwAllowedShifts.Size = New System.Drawing.Size(286, 617)
        Me.tvwAllowedShifts.TabIndex = 3
        '
        'imgIcons
        '
        Me.imgIcons.ImageStream = CType(resources.GetObject("imgIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.imgIcons.Images.SetKeyName(0, "W")
        Me.imgIcons.Images.SetKeyName(1, "M")
        Me.imgIcons.Images.SetKeyName(2, "Folder")
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 9)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(77, 14)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "Available staff"
        '
        'lvwAvailable
        '
        Me.lvwAvailable.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lvwAvailable.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colAge, Me.colDriver})
        Me.lvwAvailable.LargeImageList = Me.imgIcons
        Me.lvwAvailable.Location = New System.Drawing.Point(6, 34)
        Me.lvwAvailable.Name = "lvwAvailable"
        Me.lvwAvailable.Size = New System.Drawing.Size(365, 557)
        Me.lvwAvailable.SmallImageList = Me.imgIcons
        Me.lvwAvailable.TabIndex = 1
        Me.lvwAvailable.UseCompatibleStateImageBehavior = False
        Me.lvwAvailable.View = System.Windows.Forms.View.Details
        '
        'colName
        '
        Me.colName.Text = "Name"
        '
        'colAge
        '
        Me.colAge.Text = "Age"
        '
        'colDriver
        '
        Me.colDriver.Text = "Driver"
        '
        'tpDetails
        '
        Me.tpDetails.Controls.Add(Me.TableLayoutPanel1)
        Me.tpDetails.Controls.Add(Me.Label22)
        Me.tpDetails.Controls.Add(Me.grdSchedule)
        Me.tpDetails.Location = New System.Drawing.Point(4, 23)
        Me.tpDetails.Name = "tpDetails"
        Me.tpDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDetails.Size = New System.Drawing.Size(856, 802)
        Me.tpDetails.TabIndex = 2
        Me.tpDetails.Text = "Available staff"
        Me.tpDetails.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(534, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.83784!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.16216!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(309, 652)
        Me.TableLayoutPanel1.TabIndex = 9
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Controls.Add(Me.grdStaff)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(303, 403)
        Me.Panel1.TabIndex = 0
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(3, 3)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(77, 14)
        Me.Label23.TabIndex = 3
        Me.Label23.Text = "Available staff"
        '
        'grdStaff
        '
        Me.grdStaff.AllowUserToAddRows = False
        Me.grdStaff.AllowUserToDeleteRows = False
        Me.grdStaff.AllowUserToResizeColumns = False
        Me.grdStaff.AllowUserToResizeRows = False
        Me.grdStaff.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdStaff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdStaff.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colGender, Me.colStaffName, Me.colStaffAge})
        Me.grdStaff.Location = New System.Drawing.Point(0, 20)
        Me.grdStaff.Name = "grdStaff"
        Me.grdStaff.ReadOnly = True
        Me.grdStaff.RowHeadersVisible = False
        Me.grdStaff.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdStaff.Size = New System.Drawing.Size(297, 380)
        Me.grdStaff.TabIndex = 1
        Me.grdStaff.VirtualMode = True
        '
        'colGender
        '
        Me.colGender.HeaderText = ""
        Me.colGender.Image = Global.Balthazar.My.Resources.Resources.copy
        Me.colGender.Name = "colGender"
        Me.colGender.ReadOnly = True
        Me.colGender.Width = 20
        '
        'colStaffName
        '
        Me.colStaffName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colStaffName.HeaderText = "Name"
        Me.colStaffName.Name = "colStaffName"
        Me.colStaffName.ReadOnly = True
        '
        'colStaffAge
        '
        Me.colStaffAge.HeaderText = "Age"
        Me.colStaffAge.Name = "colStaffAge"
        Me.colStaffAge.ReadOnly = True
        Me.colStaffAge.Width = 80
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.cmdPublishChosen)
        Me.Panel2.Controls.Add(Me.grdChosen)
        Me.Panel2.Controls.Add(Me.cmdWizard)
        Me.Panel2.Controls.Add(Me.Label24)
        Me.Panel2.Controls.Add(Me.cmdUnChoose)
        Me.Panel2.Controls.Add(Me.cmdChoose)
        Me.Panel2.Location = New System.Drawing.Point(3, 412)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(303, 237)
        Me.Panel2.TabIndex = 1
        '
        'cmdPublishChosen
        '
        Me.cmdPublishChosen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPublishChosen.Image = Global.Balthazar.My.Resources.Resources.environment
        Me.cmdPublishChosen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPublishChosen.Location = New System.Drawing.Point(273, 3)
        Me.cmdPublishChosen.Name = "cmdPublishChosen"
        Me.cmdPublishChosen.Size = New System.Drawing.Size(24, 23)
        Me.cmdPublishChosen.TabIndex = 11
        Me.cmdPublishChosen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdPublishChosen.UseVisualStyleBackColor = True
        '
        'grdChosen
        '
        Me.grdChosen.AllowUserToAddRows = False
        Me.grdChosen.AllowUserToDeleteRows = False
        Me.grdChosen.AllowUserToResizeColumns = False
        Me.grdChosen.AllowUserToResizeRows = False
        Me.grdChosen.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdChosen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChosen.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colChosenName, Me.colChosenAge})
        Me.grdChosen.Location = New System.Drawing.Point(0, 29)
        Me.grdChosen.Name = "grdChosen"
        Me.grdChosen.ReadOnly = True
        Me.grdChosen.RowHeadersVisible = False
        Me.grdChosen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdChosen.Size = New System.Drawing.Size(297, 207)
        Me.grdChosen.TabIndex = 4
        Me.grdChosen.VirtualMode = True
        '
        'colChosenName
        '
        Me.colChosenName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colChosenName.HeaderText = "Name"
        Me.colChosenName.Name = "colChosenName"
        Me.colChosenName.ReadOnly = True
        '
        'colChosenAge
        '
        Me.colChosenAge.HeaderText = "Age"
        Me.colChosenAge.Name = "colChosenAge"
        Me.colChosenAge.ReadOnly = True
        Me.colChosenAge.Width = 65
        '
        'cmdWizard
        '
        Me.cmdWizard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdWizard.Image = Global.Balthazar.My.Resources.Resources.magic_wand
        Me.cmdWizard.Location = New System.Drawing.Point(244, 3)
        Me.cmdWizard.Name = "cmdWizard"
        Me.cmdWizard.Size = New System.Drawing.Size(23, 23)
        Me.cmdWizard.TabIndex = 8
        Me.cmdWizard.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(3, 12)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(70, 14)
        Me.Label24.TabIndex = 5
        Me.Label24.Text = "Chosen staff"
        '
        'cmdUnChoose
        '
        Me.cmdUnChoose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdUnChoose.Image = Global.Balthazar.My.Resources.Resources.navigate_up
        Me.cmdUnChoose.Location = New System.Drawing.Point(215, 3)
        Me.cmdUnChoose.Name = "cmdUnChoose"
        Me.cmdUnChoose.Size = New System.Drawing.Size(23, 23)
        Me.cmdUnChoose.TabIndex = 7
        Me.cmdUnChoose.UseVisualStyleBackColor = True
        '
        'cmdChoose
        '
        Me.cmdChoose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdChoose.Image = Global.Balthazar.My.Resources.Resources.navigate_down
        Me.cmdChoose.Location = New System.Drawing.Point(186, 3)
        Me.cmdChoose.Name = "cmdChoose"
        Me.cmdChoose.Size = New System.Drawing.Size(23, 23)
        Me.cmdChoose.TabIndex = 6
        Me.cmdChoose.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(6, 6)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(35, 14)
        Me.Label22.TabIndex = 2
        Me.Label22.Text = "Shifts"
        '
        'grdSchedule
        '
        Me.grdSchedule.AllowUserToAddRows = False
        Me.grdSchedule.AllowUserToDeleteRows = False
        Me.grdSchedule.AllowUserToResizeColumns = False
        Me.grdSchedule.AllowUserToResizeRows = False
        Me.grdSchedule.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSchedule.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDate, Me.colFrom, Me.colTo, Me.colRole, Me.colLocation, Me.colQuantity})
        Me.grdSchedule.Location = New System.Drawing.Point(3, 23)
        Me.grdSchedule.Name = "grdSchedule"
        Me.grdSchedule.ReadOnly = True
        Me.grdSchedule.RowHeadersVisible = False
        Me.grdSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSchedule.Size = New System.Drawing.Size(525, 629)
        Me.grdSchedule.TabIndex = 0
        Me.grdSchedule.VirtualMode = True
        '
        'colDate
        '
        Me.colDate.HeaderText = "Date"
        Me.colDate.Name = "colDate"
        Me.colDate.ReadOnly = True
        Me.colDate.Width = 65
        '
        'colFrom
        '
        Me.colFrom.HeaderText = "From"
        Me.colFrom.Name = "colFrom"
        Me.colFrom.ReadOnly = True
        Me.colFrom.Width = 55
        '
        'colTo
        '
        Me.colTo.HeaderText = "To"
        Me.colTo.Name = "colTo"
        Me.colTo.ReadOnly = True
        Me.colTo.Width = 55
        '
        'colRole
        '
        Me.colRole.HeaderText = "Role"
        Me.colRole.Name = "colRole"
        Me.colRole.ReadOnly = True
        Me.colRole.Width = 120
        '
        'colLocation
        '
        Me.colLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colLocation.HeaderText = "Location"
        Me.colLocation.Name = "colLocation"
        Me.colLocation.ReadOnly = True
        '
        'colQuantity
        '
        Me.colQuantity.HeaderText = "Qty"
        Me.colQuantity.Name = "colQuantity"
        Me.colQuantity.ReadOnly = True
        Me.colQuantity.Width = 40
        '
        'tpCatalog
        '
        Me.tpCatalog.Controls.Add(Me.pnlStaffInfo)
        Me.tpCatalog.Controls.Add(Me.cmdDeleteStaff)
        Me.tpCatalog.Controls.Add(Me.cmdAddStaff)
        Me.tpCatalog.Controls.Add(Me.Label1)
        Me.tpCatalog.Controls.Add(Me.lvwStaff)
        Me.tpCatalog.Location = New System.Drawing.Point(4, 23)
        Me.tpCatalog.Name = "tpCatalog"
        Me.tpCatalog.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCatalog.Size = New System.Drawing.Size(856, 802)
        Me.tpCatalog.TabIndex = 1
        Me.tpCatalog.Text = "Staff catalog"
        Me.tpCatalog.UseVisualStyleBackColor = True
        '
        'pnlStaffInfo
        '
        Me.pnlStaffInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlStaffInfo.AutoScroll = True
        Me.pnlStaffInfo.Controls.Add(Me.grpUser)
        Me.pnlStaffInfo.Location = New System.Drawing.Point(327, 20)
        Me.pnlStaffInfo.Name = "pnlStaffInfo"
        Me.pnlStaffInfo.Size = New System.Drawing.Size(524, 776)
        Me.pnlStaffInfo.TabIndex = 11
        '
        'grpUser
        '
        Me.grpUser.Controls.Add(Me.GroupBox7)
        Me.grpUser.Controls.Add(Me.lstDriver)
        Me.grpUser.Controls.Add(Me.cmdDriver)
        Me.grpUser.Controls.Add(Me.lblDriver)
        Me.grpUser.Controls.Add(Me.picPicture)
        Me.grpUser.Controls.Add(Me.dtBirthday)
        Me.grpUser.Controls.Add(Me.Label21)
        Me.grpUser.Controls.Add(Me.GroupBox6)
        Me.grpUser.Controls.Add(Me.cmdSave)
        Me.grpUser.Controls.Add(Me.GroupBox4)
        Me.grpUser.Controls.Add(Me.GroupBox3)
        Me.grpUser.Controls.Add(Me.Label12)
        Me.grpUser.Controls.Add(Me.GroupBox2)
        Me.grpUser.Controls.Add(Me.GroupBox1)
        Me.grpUser.Controls.Add(Me.optMale)
        Me.grpUser.Controls.Add(Me.optFemale)
        Me.grpUser.Controls.Add(Me.txtAge)
        Me.grpUser.Controls.Add(Me.Label4)
        Me.grpUser.Controls.Add(Me.txtLastName)
        Me.grpUser.Controls.Add(Me.Label3)
        Me.grpUser.Controls.Add(Me.txtFirstName)
        Me.grpUser.Controls.Add(Me.Label2)
        Me.grpUser.Location = New System.Drawing.Point(3, 3)
        Me.grpUser.Name = "grpUser"
        Me.grpUser.Size = New System.Drawing.Size(516, 770)
        Me.grpUser.TabIndex = 1
        Me.grpUser.TabStop = False
        Me.grpUser.Text = "Lastname, Firstname"
        Me.grpUser.Visible = False
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.grdCV)
        Me.GroupBox7.Location = New System.Drawing.Point(6, 643)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(501, 121)
        Me.GroupBox7.TabIndex = 21
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "CV"
        '
        'grdCV
        '
        Me.grdCV.AllowUserToAddRows = False
        Me.grdCV.AllowUserToDeleteRows = False
        Me.grdCV.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray
        Me.grdCV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdCV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.grdCV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colEventName, Me.colCVRole, Me.colCategory, Me.colResponsible, Me.colTime})
        Me.grdCV.Location = New System.Drawing.Point(6, 19)
        Me.grdCV.Name = "grdCV"
        Me.grdCV.ReadOnly = True
        Me.grdCV.RowHeadersVisible = False
        Me.grdCV.Size = New System.Drawing.Size(489, 96)
        Me.grdCV.TabIndex = 0
        Me.grdCV.VirtualMode = True
        '
        'colEventName
        '
        Me.colEventName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colEventName.HeaderText = "Event"
        Me.colEventName.Name = "colEventName"
        Me.colEventName.ReadOnly = True
        '
        'colCVRole
        '
        Me.colCVRole.HeaderText = "Role"
        Me.colCVRole.Name = "colCVRole"
        Me.colCVRole.ReadOnly = True
        '
        'colCategory
        '
        Me.colCategory.HeaderText = "Category"
        Me.colCategory.Name = "colCategory"
        Me.colCategory.ReadOnly = True
        '
        'colResponsible
        '
        Me.colResponsible.HeaderText = "Responsible"
        Me.colResponsible.Name = "colResponsible"
        Me.colResponsible.ReadOnly = True
        Me.colResponsible.Width = 125
        '
        'colTime
        '
        Me.colTime.HeaderText = "Hours"
        Me.colTime.Name = "colTime"
        Me.colTime.ReadOnly = True
        Me.colTime.Width = 75
        '
        'lstDriver
        '
        Me.lstDriver.CheckOnClick = True
        Me.lstDriver.FormattingEnabled = True
        Me.lstDriver.Items.AddRange(New Object() {"B", "C", "D", "E"})
        Me.lstDriver.Location = New System.Drawing.Point(56, 134)
        Me.lstDriver.Name = "lstDriver"
        Me.lstDriver.Size = New System.Drawing.Size(91, 64)
        Me.lstDriver.TabIndex = 19
        Me.lstDriver.Visible = False
        '
        'cmdDriver
        '
        Me.cmdDriver.Location = New System.Drawing.Point(55, 112)
        Me.cmdDriver.Name = "cmdDriver"
        Me.cmdDriver.Size = New System.Drawing.Size(62, 23)
        Me.cmdDriver.TabIndex = 20
        Me.cmdDriver.Text = "Change"
        Me.cmdDriver.UseVisualStyleBackColor = True
        '
        'lblDriver
        '
        Me.lblDriver.AutoSize = True
        Me.lblDriver.Location = New System.Drawing.Point(6, 116)
        Me.lblDriver.Name = "lblDriver"
        Me.lblDriver.Size = New System.Drawing.Size(43, 14)
        Me.lblDriver.TabIndex = 19
        Me.lblDriver.Text = "B,C,D,E"
        '
        'picPicture
        '
        Me.picPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPicture.Location = New System.Drawing.Point(289, 20)
        Me.picPicture.Name = "picPicture"
        Me.picPicture.Size = New System.Drawing.Size(109, 113)
        Me.picPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picPicture.TabIndex = 18
        Me.picPicture.TabStop = False
        '
        'dtBirthday
        '
        Me.dtBirthday.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtBirthday.Location = New System.Drawing.Point(123, 113)
        Me.dtBirthday.Name = "dtBirthday"
        Me.dtBirthday.Size = New System.Drawing.Size(84, 20)
        Me.dtBirthday.TabIndex = 2
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(123, 95)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(47, 14)
        Me.Label21.TabIndex = 17
        Me.Label21.Text = "Birthday"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtPassword)
        Me.GroupBox6.Controls.Add(Me.Label19)
        Me.GroupBox6.Controls.Add(Me.txtUserName)
        Me.GroupBox6.Controls.Add(Me.Label20)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 334)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(246, 102)
        Me.GroupBox6.TabIndex = 9
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Login info"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(6, 73)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(230, 20)
        Me.txtPassword.TabIndex = 1
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 56)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(57, 14)
        Me.Label19.TabIndex = 8
        Me.Label19.Text = "Password"
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(6, 33)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(230, 20)
        Me.txtUserName.TabIndex = 0
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(6, 16)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(56, 14)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "Username"
        '
        'cmdSave
        '
        Me.cmdSave.Image = Global.Balthazar.My.Resources.Resources.disk_blue
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(450, 16)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(60, 31)
        Me.cmdSave.TabIndex = 14
        Me.cmdSave.Text = "Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label27)
        Me.GroupBox4.Controls.Add(Me.lstAvailableForCategories)
        Me.GroupBox4.Controls.Add(Me.Label26)
        Me.GroupBox4.Controls.Add(Me.txtExternalInfo)
        Me.GroupBox4.Controls.Add(Me.Label25)
        Me.GroupBox4.Controls.Add(Me.txtInternalInfo)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 442)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(501, 195)
        Me.GroupBox4.TabIndex = 11
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Info"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(337, 16)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(59, 14)
        Me.Label27.TabIndex = 5
        Me.Label27.Text = "Categories"
        '
        'lstAvailableForCategories
        '
        Me.lstAvailableForCategories.FormattingEnabled = True
        Me.lstAvailableForCategories.Location = New System.Drawing.Point(340, 33)
        Me.lstAvailableForCategories.Name = "lstAvailableForCategories"
        Me.lstAvailableForCategories.Size = New System.Drawing.Size(155, 154)
        Me.lstAvailableForCategories.TabIndex = 4
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(7, 98)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(46, 14)
        Me.Label26.TabIndex = 3
        Me.Label26.Text = "External"
        '
        'txtExternalInfo
        '
        Me.txtExternalInfo.Location = New System.Drawing.Point(6, 115)
        Me.txtExternalInfo.Multiline = True
        Me.txtExternalInfo.Name = "txtExternalInfo"
        Me.txtExternalInfo.Size = New System.Drawing.Size(329, 72)
        Me.txtExternalInfo.TabIndex = 2
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(6, 16)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(42, 14)
        Me.Label25.TabIndex = 1
        Me.Label25.Text = "Internal"
        '
        'txtInternalInfo
        '
        Me.txtInternalInfo.Location = New System.Drawing.Point(6, 33)
        Me.txtInternalInfo.Multiline = True
        Me.txtInternalInfo.Name = "txtInternalInfo"
        Me.txtInternalInfo.Size = New System.Drawing.Size(328, 62)
        Me.txtInternalInfo.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtAccount)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.txtClearing)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.txtBank)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Location = New System.Drawing.Point(261, 291)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(246, 145)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Salary info"
        '
        'txtAccount
        '
        Me.txtAccount.Location = New System.Drawing.Point(6, 113)
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.Size = New System.Drawing.Size(230, 20)
        Me.txtAccount.TabIndex = 2
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 96)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(87, 14)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "Account number"
        '
        'txtClearing
        '
        Me.txtClearing.Location = New System.Drawing.Point(6, 73)
        Me.txtClearing.Name = "txtClearing"
        Me.txtClearing.Size = New System.Drawing.Size(230, 20)
        Me.txtClearing.TabIndex = 1
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 56)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(85, 14)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "Clearing number"
        '
        'txtBank
        '
        Me.txtBank.Location = New System.Drawing.Point(6, 33)
        Me.txtBank.Name = "txtBank"
        Me.txtBank.Size = New System.Drawing.Size(230, 20)
        Me.txtBank.TabIndex = 0
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 16)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(31, 14)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "Bank"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 96)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(79, 14)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Drivers licence"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtMobilePhone)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.txtWorkPhone)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtHomePhone)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Location = New System.Drawing.Point(258, 140)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(249, 145)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Phone"
        '
        'txtMobilePhone
        '
        Me.txtMobilePhone.Location = New System.Drawing.Point(6, 113)
        Me.txtMobilePhone.Name = "txtMobilePhone"
        Me.txtMobilePhone.Size = New System.Drawing.Size(230, 20)
        Me.txtMobilePhone.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 96)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(37, 14)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Mobile"
        '
        'txtWorkPhone
        '
        Me.txtWorkPhone.Location = New System.Drawing.Point(6, 73)
        Me.txtWorkPhone.Name = "txtWorkPhone"
        Me.txtWorkPhone.Size = New System.Drawing.Size(230, 20)
        Me.txtWorkPhone.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 56)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(32, 14)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Work"
        '
        'txtHomePhone
        '
        Me.txtHomePhone.Location = New System.Drawing.Point(6, 33)
        Me.txtHomePhone.Name = "txtHomePhone"
        Me.txtHomePhone.Size = New System.Drawing.Size(230, 20)
        Me.txtHomePhone.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(34, 14)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Home"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtEmail)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.txtCity)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtZipCode)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtAddress2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtAddress1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 139)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(246, 189)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Address"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(6, 33)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(230, 20)
        Me.txtEmail.TabIndex = 0
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 16)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(35, 14)
        Me.Label18.TabIndex = 12
        Me.Label18.Text = "E-Mail"
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(110, 154)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(126, 20)
        Me.txtCity.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(107, 137)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(25, 14)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "City"
        '
        'txtZipCode
        '
        Me.txtZipCode.Location = New System.Drawing.Point(6, 154)
        Me.txtZipCode.Name = "txtZipCode"
        Me.txtZipCode.Size = New System.Drawing.Size(98, 20)
        Me.txtZipCode.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 137)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 14)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Zip code"
        '
        'txtAddress2
        '
        Me.txtAddress2.Location = New System.Drawing.Point(6, 114)
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(230, 20)
        Me.txtAddress2.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 97)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 14)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Street Address 2"
        '
        'txtAddress1
        '
        Me.txtAddress1.Location = New System.Drawing.Point(6, 74)
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(230, 20)
        Me.txtAddress1.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 57)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 14)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Street Address 1"
        '
        'optMale
        '
        Me.optMale.AutoSize = True
        Me.optMale.ImageIndex = 1
        Me.optMale.Location = New System.Drawing.Point(213, 119)
        Me.optMale.Name = "optMale"
        Me.optMale.Size = New System.Drawing.Size(47, 18)
        Me.optMale.TabIndex = 5
        Me.optMale.TabStop = True
        Me.optMale.Text = "Male"
        Me.optMale.UseVisualStyleBackColor = True
        '
        'optFemale
        '
        Me.optFemale.AutoSize = True
        Me.optFemale.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.optFemale.ImageIndex = 0
        Me.optFemale.Location = New System.Drawing.Point(213, 99)
        Me.optFemale.Name = "optFemale"
        Me.optFemale.Size = New System.Drawing.Size(59, 18)
        Me.optFemale.TabIndex = 4
        Me.optFemale.TabStop = True
        Me.optFemale.Text = "Female"
        Me.optFemale.UseVisualStyleBackColor = True
        '
        'txtAge
        '
        Me.txtAge.Enabled = False
        Me.txtAge.Location = New System.Drawing.Point(450, 113)
        Me.txtAge.Name = "txtAge"
        Me.txtAge.Size = New System.Drawing.Size(57, 20)
        Me.txtAge.TabIndex = 3
        Me.txtAge.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(450, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 14)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Age"
        Me.Label4.Visible = False
        '
        'txtLastName
        '
        Me.txtLastName.Location = New System.Drawing.Point(6, 73)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(230, 20)
        Me.txtLastName.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Last name"
        '
        'txtFirstName
        '
        Me.txtFirstName.Location = New System.Drawing.Point(6, 33)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(230, 20)
        Me.txtFirstName.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "First name"
        '
        'cmdDeleteStaff
        '
        Me.cmdDeleteStaff.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdDeleteStaff.Location = New System.Drawing.Point(297, 50)
        Me.cmdDeleteStaff.Name = "cmdDeleteStaff"
        Me.cmdDeleteStaff.Size = New System.Drawing.Size(24, 24)
        Me.cmdDeleteStaff.TabIndex = 10
        Me.cmdDeleteStaff.UseVisualStyleBackColor = True
        '
        'cmdAddStaff
        '
        Me.cmdAddStaff.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddStaff.Location = New System.Drawing.Point(297, 20)
        Me.cmdAddStaff.Name = "cmdAddStaff"
        Me.cmdAddStaff.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddStaff.TabIndex = 9
        Me.cmdAddStaff.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Staff"
        '
        'lvwStaff
        '
        Me.lvwStaff.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lvwStaff.LargeImageList = Me.imgIcons
        Me.lvwStaff.Location = New System.Drawing.Point(6, 20)
        Me.lvwStaff.Name = "lvwStaff"
        Me.lvwStaff.Size = New System.Drawing.Size(285, 776)
        Me.lvwStaff.SmallImageList = Me.imgIcons
        Me.lvwStaff.TabIndex = 0
        Me.lvwStaff.UseCompatibleStateImageBehavior = False
        Me.lvwStaff.View = System.Windows.Forms.View.List
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.HeaderText = ""
        Me.DataGridViewImageColumn1.Image = Global.Balthazar.My.Resources.Resources.copy
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.ReadOnly = True
        Me.DataGridViewImageColumn1.Width = 20
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Age"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 80
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.HeaderText = "Date"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "From"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 60
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "To"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 60
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Role"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 120
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn7.HeaderText = "Location"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn8.FillWeight = 147.8827!
        Me.DataGridViewTextBoxColumn8.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn9.FillWeight = 52.11726!
        Me.DataGridViewTextBoxColumn9.HeaderText = "Age"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "Qty"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Width = 40
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Category"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.HeaderText = "Responsible"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.Width = 125
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.HeaderText = "Hours"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.Width = 75
        '
        'frmStaff
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(866, 746)
        Me.Controls.Add(Me.tabStaff)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmStaff"
        Me.Text = "Staff"
        Me.tabStaff.ResumeLayout(False)
        Me.tabAssign.ResumeLayout(False)
        Me.tabAssign.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.picHideCampaign, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picShowCampaign, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpDetails.ResumeLayout(False)
        Me.tpDetails.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.grdStaff, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.grdChosen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpCatalog.ResumeLayout(False)
        Me.tpCatalog.PerformLayout()
        Me.pnlStaffInfo.ResumeLayout(False)
        Me.grpUser.ResumeLayout(False)
        Me.grpUser.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        CType(Me.grdCV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabStaff As System.Windows.Forms.TabControl
    Friend WithEvents tabAssign As System.Windows.Forms.TabPage
    Friend WithEvents tpCatalog As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lvwStaff As System.Windows.Forms.ListView
    Friend WithEvents imgIcons As System.Windows.Forms.ImageList
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lvwAvailable As System.Windows.Forms.ListView
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents tvwAllowedShifts As System.Windows.Forms.TreeView
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colAge As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDriver As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents picShowCampaign As System.Windows.Forms.PictureBox
    Friend WithEvents picHideCampaign As System.Windows.Forms.PictureBox
    Friend WithEvents cmdDeleteStaff As System.Windows.Forms.Button
    Friend WithEvents cmdAddStaff As System.Windows.Forms.Button
    Friend WithEvents cmbAllowed As System.Windows.Forms.ComboBox
    Friend WithEvents cmdRemoveFromAll As System.Windows.Forms.Button
    Friend WithEvents cmbFilterRole As System.Windows.Forms.ComboBox
    Friend WithEvents chkFilterRole As System.Windows.Forms.CheckBox
    Friend WithEvents cmdPublishAllowed As System.Windows.Forms.Button
    Friend WithEvents tpDetails As System.Windows.Forms.TabPage
    Friend WithEvents grdSchedule As System.Windows.Forms.DataGridView
    Friend WithEvents grdStaff As System.Windows.Forms.DataGridView
    Friend WithEvents colGender As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents colStaffName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStaffAge As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdChosen As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents colDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFrom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRole As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLocation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colQuantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdUnChoose As System.Windows.Forms.Button
    Friend WithEvents cmdChoose As System.Windows.Forms.Button
    Friend WithEvents cmdWizard As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmdPublishChosen As System.Windows.Forms.Button
    Friend WithEvents grpUser As System.Windows.Forms.GroupBox
    Friend WithEvents lstDriver As System.Windows.Forms.CheckedListBox
    Friend WithEvents cmdDriver As System.Windows.Forms.Button
    Friend WithEvents lblDriver As System.Windows.Forms.Label
    Friend WithEvents picPicture As System.Windows.Forms.PictureBox
    Friend WithEvents dtBirthday As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents lstAvailableForCategories As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtExternalInfo As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtInternalInfo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtAccount As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtClearing As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtBank As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtMobilePhone As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtWorkPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtHomePhone As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtZipCode As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents optMale As System.Windows.Forms.RadioButton
    Friend WithEvents optFemale As System.Windows.Forms.RadioButton
    Friend WithEvents txtAge As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pnlStaffInfo As System.Windows.Forms.Panel
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents grdCV As System.Windows.Forms.DataGridView
    Friend WithEvents colEventName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCVRole As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCategory As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colResponsible As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChosenName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChosenAge As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
