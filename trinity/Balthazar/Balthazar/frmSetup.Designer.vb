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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.tabSetup = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.grpContacts = New System.Windows.Forms.GroupBox
        Me.cmdRemoveExternalContact = New System.Windows.Forms.Button
        Me.cmdAddExternalContact = New System.Windows.Forms.Button
        Me.cmdRemoveInternalContact = New System.Windows.Forms.Button
        Me.cmdAddInternalContact = New System.Windows.Forms.Button
        Me.grdExternalContacts = New System.Windows.Forms.DataGridView
        Me.colExName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colExRole = New Balthazar.ExtendedComboboxColumn
        Me.colExPhone = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colExDefault = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Label2 = New System.Windows.Forms.Label
        Me.grdInternalContacts = New System.Windows.Forms.DataGridView
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colRole = New Balthazar.ExtendedComboboxColumn
        Me.colPhone = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDefault = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Label1 = New System.Windows.Forms.Label
        Me.picShowContacts = New System.Windows.Forms.PictureBox
        Me.picHideContacts = New System.Windows.Forms.PictureBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chkInStore = New System.Windows.Forms.CheckBox
        Me.cmdAddProduct = New System.Windows.Forms.Button
        Me.cmdAddClient = New System.Windows.Forms.Button
        Me.cmbProducts = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbClients = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.grpCampaign = New System.Windows.Forms.GroupBox
        Me.txtWhen = New System.Windows.Forms.TextBox
        Me.txtHow = New System.Windows.Forms.TextBox
        Me.txtPurpose = New System.Windows.Forms.TextBox
        Me.txtWhat = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtBackground = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.picShowCampaign = New System.Windows.Forms.PictureBox
        Me.picHideCampaign = New System.Windows.Forms.PictureBox
        Me.grpProject = New System.Windows.Forms.GroupBox
        Me.cmdRemoveGoal = New System.Windows.Forms.Button
        Me.cmdAddGoal = New System.Windows.Forms.Button
        Me.grdGoals = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label16 = New System.Windows.Forms.Label
        Me.cmdRemovePurpose = New System.Windows.Forms.Button
        Me.cmdAddPurpose = New System.Windows.Forms.Button
        Me.grdPurpose = New System.Windows.Forms.DataGridView
        Me.colPurpose = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtCoreValues = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtMessage = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtTarget = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtMission = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.picShowProject = New System.Windows.Forms.PictureBox
        Me.picHideProject = New System.Windows.Forms.PictureBox
        Me.tpQA = New System.Windows.Forms.TabPage
        Me.cmdRemoveQuestion = New System.Windows.Forms.Button
        Me.cmdAddQuestion = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.grdQuestions = New System.Windows.Forms.DataGridView
        Me.colQuestion = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colAnswer = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.tpTemplates = New System.Windows.Forms.TabPage
        Me.grdTemplates = New System.Windows.Forms.DataGridView
        Me.colTemplateName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colTemplateDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colEdit = New System.Windows.Forms.DataGridViewButtonColumn
        Me.cmdRemoveTemplate = New System.Windows.Forms.Button
        Me.cmdAddTemplate = New System.Windows.Forms.Button
        Me.ExtendedComboboxColumn1 = New Balthazar.ExtendedComboboxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ExtendedComboboxColumn2 = New Balthazar.ExtendedComboboxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CalendarColumn1 = New Balthazar.CalendarColumn
        Me.CalendarColumn2 = New Balthazar.CalendarColumn
        Me.BottomToolStripPanel = New System.Windows.Forms.ToolStripPanel
        Me.TopToolStripPanel = New System.Windows.Forms.ToolStripPanel
        Me.RightToolStripPanel = New System.Windows.Forms.ToolStripPanel
        Me.LeftToolStripPanel = New System.Windows.Forms.ToolStripPanel
        Me.ContentPanel = New System.Windows.Forms.ToolStripContentPanel
        Me.tabSetup.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.grpContacts.SuspendLayout()
        CType(Me.grdExternalContacts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdInternalContacts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picShowContacts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picHideContacts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.grpCampaign.SuspendLayout()
        CType(Me.picShowCampaign, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picHideCampaign, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProject.SuspendLayout()
        CType(Me.grdGoals, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPurpose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picShowProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picHideProject, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpQA.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdQuestions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpTemplates.SuspendLayout()
        CType(Me.grdTemplates, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tabSetup
        '
        Me.tabSetup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabSetup.Controls.Add(Me.TabPage1)
        Me.tabSetup.Controls.Add(Me.tpQA)
        Me.tabSetup.Controls.Add(Me.tpTemplates)
        Me.tabSetup.Location = New System.Drawing.Point(1, 1)
        Me.tabSetup.Name = "tabSetup"
        Me.tabSetup.SelectedIndex = 0
        Me.tabSetup.Size = New System.Drawing.Size(772, 753)
        Me.tabSetup.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = True
        Me.TabPage1.Controls.Add(Me.grpContacts)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.grpCampaign)
        Me.TabPage1.Controls.Add(Me.grpProject)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(764, 726)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'grpContacts
        '
        Me.grpContacts.Controls.Add(Me.cmdRemoveExternalContact)
        Me.grpContacts.Controls.Add(Me.cmdAddExternalContact)
        Me.grpContacts.Controls.Add(Me.cmdRemoveInternalContact)
        Me.grpContacts.Controls.Add(Me.cmdAddInternalContact)
        Me.grpContacts.Controls.Add(Me.grdExternalContacts)
        Me.grpContacts.Controls.Add(Me.Label2)
        Me.grpContacts.Controls.Add(Me.grdInternalContacts)
        Me.grpContacts.Controls.Add(Me.Label1)
        Me.grpContacts.Controls.Add(Me.picShowContacts)
        Me.grpContacts.Controls.Add(Me.picHideContacts)
        Me.grpContacts.Location = New System.Drawing.Point(6, 182)
        Me.grpContacts.Name = "grpContacts"
        Me.grpContacts.Size = New System.Drawing.Size(466, 16)
        Me.grpContacts.TabIndex = 4
        Me.grpContacts.TabStop = False
        Me.grpContacts.Text = "       Contacts"
        '
        'cmdRemoveExternalContact
        '
        Me.cmdRemoveExternalContact.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveExternalContact.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveExternalContact.Location = New System.Drawing.Point(429, 198)
        Me.cmdRemoveExternalContact.Name = "cmdRemoveExternalContact"
        Me.cmdRemoveExternalContact.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveExternalContact.TabIndex = 5
        Me.cmdRemoveExternalContact.UseVisualStyleBackColor = True
        '
        'cmdAddExternalContact
        '
        Me.cmdAddExternalContact.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddExternalContact.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddExternalContact.Location = New System.Drawing.Point(429, 168)
        Me.cmdAddExternalContact.Name = "cmdAddExternalContact"
        Me.cmdAddExternalContact.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddExternalContact.TabIndex = 4
        Me.cmdAddExternalContact.UseVisualStyleBackColor = True
        '
        'cmdRemoveInternalContact
        '
        Me.cmdRemoveInternalContact.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveInternalContact.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveInternalContact.Location = New System.Drawing.Point(429, 63)
        Me.cmdRemoveInternalContact.Name = "cmdRemoveInternalContact"
        Me.cmdRemoveInternalContact.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveInternalContact.TabIndex = 2
        Me.cmdRemoveInternalContact.UseVisualStyleBackColor = True
        '
        'cmdAddInternalContact
        '
        Me.cmdAddInternalContact.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddInternalContact.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddInternalContact.Location = New System.Drawing.Point(429, 33)
        Me.cmdAddInternalContact.Name = "cmdAddInternalContact"
        Me.cmdAddInternalContact.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddInternalContact.TabIndex = 1
        Me.cmdAddInternalContact.UseVisualStyleBackColor = True
        '
        'grdExternalContacts
        '
        Me.grdExternalContacts.AllowUserToAddRows = False
        Me.grdExternalContacts.AllowUserToDeleteRows = False
        Me.grdExternalContacts.AllowUserToResizeColumns = False
        Me.grdExternalContacts.AllowUserToResizeRows = False
        Me.grdExternalContacts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdExternalContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdExternalContacts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colExName, Me.colExRole, Me.colExPhone, Me.colExDefault})
        Me.grdExternalContacts.Location = New System.Drawing.Point(6, 168)
        Me.grdExternalContacts.Name = "grdExternalContacts"
        Me.grdExternalContacts.RowHeadersVisible = False
        Me.grdExternalContacts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.grdExternalContacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdExternalContacts.Size = New System.Drawing.Size(416, 115)
        Me.grdExternalContacts.TabIndex = 0
        Me.grdExternalContacts.VirtualMode = True
        '
        'colExName
        '
        Me.colExName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colExName.HeaderText = "Name"
        Me.colExName.Name = "colExName"
        '
        'colExRole
        '
        Me.colExRole.HeaderText = "Role"
        Me.colExRole.Name = "colExRole"
        Me.colExRole.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'colExPhone
        '
        Me.colExPhone.FillWeight = 75.0!
        Me.colExPhone.HeaderText = "Phone Nr"
        Me.colExPhone.Name = "colExPhone"
        Me.colExPhone.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colExPhone.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colExDefault
        '
        Me.colExDefault.HeaderText = "Default"
        Me.colExDefault.Name = "colExDefault"
        Me.colExDefault.Width = 60
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 151)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "External"
        '
        'grdInternalContacts
        '
        Me.grdInternalContacts.AllowUserToAddRows = False
        Me.grdInternalContacts.AllowUserToDeleteRows = False
        Me.grdInternalContacts.AllowUserToResizeColumns = False
        Me.grdInternalContacts.AllowUserToResizeRows = False
        Me.grdInternalContacts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdInternalContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdInternalContacts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colRole, Me.colPhone, Me.colDefault})
        Me.grdInternalContacts.Location = New System.Drawing.Point(6, 33)
        Me.grdInternalContacts.Name = "grdInternalContacts"
        Me.grdInternalContacts.RowHeadersVisible = False
        Me.grdInternalContacts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdInternalContacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdInternalContacts.Size = New System.Drawing.Size(416, 115)
        Me.grdInternalContacts.TabIndex = 0
        Me.grdInternalContacts.VirtualMode = True
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        '
        'colRole
        '
        Me.colRole.HeaderText = "Role"
        Me.colRole.Name = "colRole"
        Me.colRole.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colRole.Width = 125
        '
        'colPhone
        '
        Me.colPhone.FillWeight = 75.0!
        Me.colPhone.HeaderText = "Phone Nr"
        Me.colPhone.Name = "colPhone"
        Me.colPhone.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colPhone.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colDefault
        '
        Me.colDefault.HeaderText = "Default"
        Me.colDefault.Name = "colDefault"
        Me.colDefault.Width = 60
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Internal"
        '
        'picShowContacts
        '
        Me.picShowContacts.Image = Global.Balthazar.My.Resources.Resources.navigate_right
        Me.picShowContacts.Location = New System.Drawing.Point(11, -1)
        Me.picShowContacts.Name = "picShowContacts"
        Me.picShowContacts.Size = New System.Drawing.Size(16, 16)
        Me.picShowContacts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picShowContacts.TabIndex = 8
        Me.picShowContacts.TabStop = False
        '
        'picHideContacts
        '
        Me.picHideContacts.Image = Global.Balthazar.My.Resources.Resources.navigate_down
        Me.picHideContacts.Location = New System.Drawing.Point(11, -1)
        Me.picHideContacts.Name = "picHideContacts"
        Me.picHideContacts.Size = New System.Drawing.Size(16, 16)
        Me.picHideContacts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picHideContacts.TabIndex = 9
        Me.picHideContacts.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkInStore)
        Me.GroupBox2.Controls.Add(Me.cmdAddProduct)
        Me.GroupBox2.Controls.Add(Me.cmdAddClient)
        Me.GroupBox2.Controls.Add(Me.cmbProducts)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.cmbClients)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtName)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(330, 170)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "General info"
        '
        'chkInStore
        '
        Me.chkInStore.AutoSize = True
        Me.chkInStore.Location = New System.Drawing.Point(7, 143)
        Me.chkInStore.Name = "chkInStore"
        Me.chkInStore.Size = New System.Drawing.Size(85, 18)
        Me.chkInStore.TabIndex = 5
        Me.chkInStore.Text = "Use In-store"
        Me.chkInStore.UseVisualStyleBackColor = True
        '
        'cmdAddProduct
        '
        Me.cmdAddProduct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddProduct.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddProduct.Location = New System.Drawing.Point(300, 115)
        Me.cmdAddProduct.Name = "cmdAddProduct"
        Me.cmdAddProduct.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddProduct.TabIndex = 4
        Me.cmdAddProduct.UseVisualStyleBackColor = True
        '
        'cmdAddClient
        '
        Me.cmdAddClient.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddClient.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddClient.Location = New System.Drawing.Point(300, 73)
        Me.cmdAddClient.Name = "cmdAddClient"
        Me.cmdAddClient.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddClient.TabIndex = 2
        Me.cmdAddClient.UseVisualStyleBackColor = True
        '
        'cmbProducts
        '
        Me.cmbProducts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbProducts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProducts.FormattingEnabled = True
        Me.cmbProducts.Location = New System.Drawing.Point(6, 115)
        Me.cmbProducts.Name = "cmbProducts"
        Me.cmbProducts.Size = New System.Drawing.Size(288, 22)
        Me.cmbProducts.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 98)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 14)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Product"
        '
        'cmbClients
        '
        Me.cmbClients.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClients.FormattingEnabled = True
        Me.cmbClients.Location = New System.Drawing.Point(6, 73)
        Me.cmbClients.Name = "cmbClients"
        Me.cmbClients.Size = New System.Drawing.Size(288, 22)
        Me.cmbClients.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 14)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Client"
        '
        'txtName
        '
        Me.txtName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtName.Location = New System.Drawing.Point(6, 33)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(318, 20)
        Me.txtName.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 14)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Project name"
        '
        'grpCampaign
        '
        Me.grpCampaign.Controls.Add(Me.txtWhen)
        Me.grpCampaign.Controls.Add(Me.txtHow)
        Me.grpCampaign.Controls.Add(Me.txtPurpose)
        Me.grpCampaign.Controls.Add(Me.txtWhat)
        Me.grpCampaign.Controls.Add(Me.Label9)
        Me.grpCampaign.Controls.Add(Me.Label10)
        Me.grpCampaign.Controls.Add(Me.Label8)
        Me.grpCampaign.Controls.Add(Me.Label7)
        Me.grpCampaign.Controls.Add(Me.txtBackground)
        Me.grpCampaign.Controls.Add(Me.Label6)
        Me.grpCampaign.Controls.Add(Me.picShowCampaign)
        Me.grpCampaign.Controls.Add(Me.picHideCampaign)
        Me.grpCampaign.Location = New System.Drawing.Point(7, 204)
        Me.grpCampaign.Name = "grpCampaign"
        Me.grpCampaign.Size = New System.Drawing.Size(466, 16)
        Me.grpCampaign.TabIndex = 6
        Me.grpCampaign.TabStop = False
        Me.grpCampaign.Text = "       Campaign"
        '
        'txtWhen
        '
        Me.txtWhen.Location = New System.Drawing.Point(59, 173)
        Me.txtWhen.Name = "txtWhen"
        Me.txtWhen.Size = New System.Drawing.Size(401, 20)
        Me.txtWhen.TabIndex = 4
        '
        'txtHow
        '
        Me.txtHow.Location = New System.Drawing.Point(59, 147)
        Me.txtHow.Name = "txtHow"
        Me.txtHow.Size = New System.Drawing.Size(401, 20)
        Me.txtHow.TabIndex = 3
        '
        'txtPurpose
        '
        Me.txtPurpose.Location = New System.Drawing.Point(59, 121)
        Me.txtPurpose.Name = "txtPurpose"
        Me.txtPurpose.Size = New System.Drawing.Size(401, 20)
        Me.txtPurpose.TabIndex = 2
        '
        'txtWhat
        '
        Me.txtWhat.Location = New System.Drawing.Point(59, 95)
        Me.txtWhat.Name = "txtWhat"
        Me.txtWhat.Size = New System.Drawing.Size(401, 20)
        Me.txtWhat.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 176)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 14)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "When"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 124)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(47, 14)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "Purpose"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 150)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(30, 14)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "How"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 98)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 14)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "What"
        '
        'txtBackground
        '
        Me.txtBackground.Location = New System.Drawing.Point(6, 35)
        Me.txtBackground.Multiline = True
        Me.txtBackground.Name = "txtBackground"
        Me.txtBackground.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBackground.Size = New System.Drawing.Size(454, 47)
        Me.txtBackground.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 14)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Background"
        '
        'picShowCampaign
        '
        Me.picShowCampaign.Image = Global.Balthazar.My.Resources.Resources.navigate_right
        Me.picShowCampaign.Location = New System.Drawing.Point(11, -1)
        Me.picShowCampaign.Name = "picShowCampaign"
        Me.picShowCampaign.Size = New System.Drawing.Size(16, 16)
        Me.picShowCampaign.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picShowCampaign.TabIndex = 8
        Me.picShowCampaign.TabStop = False
        '
        'picHideCampaign
        '
        Me.picHideCampaign.Image = Global.Balthazar.My.Resources.Resources.navigate_down
        Me.picHideCampaign.Location = New System.Drawing.Point(11, -1)
        Me.picHideCampaign.Name = "picHideCampaign"
        Me.picHideCampaign.Size = New System.Drawing.Size(16, 16)
        Me.picHideCampaign.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picHideCampaign.TabIndex = 9
        Me.picHideCampaign.TabStop = False
        '
        'grpProject
        '
        Me.grpProject.Controls.Add(Me.cmdRemoveGoal)
        Me.grpProject.Controls.Add(Me.cmdAddGoal)
        Me.grpProject.Controls.Add(Me.grdGoals)
        Me.grpProject.Controls.Add(Me.Label16)
        Me.grpProject.Controls.Add(Me.cmdRemovePurpose)
        Me.grpProject.Controls.Add(Me.cmdAddPurpose)
        Me.grpProject.Controls.Add(Me.grdPurpose)
        Me.grpProject.Controls.Add(Me.Label13)
        Me.grpProject.Controls.Add(Me.txtCoreValues)
        Me.grpProject.Controls.Add(Me.Label12)
        Me.grpProject.Controls.Add(Me.txtMessage)
        Me.grpProject.Controls.Add(Me.Label11)
        Me.grpProject.Controls.Add(Me.txtTarget)
        Me.grpProject.Controls.Add(Me.Label14)
        Me.grpProject.Controls.Add(Me.txtMission)
        Me.grpProject.Controls.Add(Me.Label15)
        Me.grpProject.Controls.Add(Me.picShowProject)
        Me.grpProject.Controls.Add(Me.picHideProject)
        Me.grpProject.Location = New System.Drawing.Point(7, 226)
        Me.grpProject.Name = "grpProject"
        Me.grpProject.Size = New System.Drawing.Size(466, 16)
        Me.grpProject.TabIndex = 7
        Me.grpProject.TabStop = False
        Me.grpProject.Text = "       Project"
        '
        'cmdRemoveGoal
        '
        Me.cmdRemoveGoal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveGoal.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveGoal.Location = New System.Drawing.Point(436, 351)
        Me.cmdRemoveGoal.Name = "cmdRemoveGoal"
        Me.cmdRemoveGoal.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveGoal.TabIndex = 9
        Me.cmdRemoveGoal.UseVisualStyleBackColor = True
        '
        'cmdAddGoal
        '
        Me.cmdAddGoal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddGoal.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddGoal.Location = New System.Drawing.Point(436, 321)
        Me.cmdAddGoal.Name = "cmdAddGoal"
        Me.cmdAddGoal.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddGoal.TabIndex = 8
        Me.cmdAddGoal.UseVisualStyleBackColor = True
        '
        'grdGoals
        '
        Me.grdGoals.AllowUserToAddRows = False
        Me.grdGoals.AllowUserToDeleteRows = False
        Me.grdGoals.AllowUserToResizeColumns = False
        Me.grdGoals.AllowUserToResizeRows = False
        Me.grdGoals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdGoals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdGoals.ColumnHeadersVisible = False
        Me.grdGoals.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1})
        Me.grdGoals.Location = New System.Drawing.Point(6, 321)
        Me.grdGoals.Name = "grdGoals"
        Me.grdGoals.RowHeadersVisible = False
        Me.grdGoals.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdGoals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdGoals.Size = New System.Drawing.Size(424, 79)
        Me.grdGoals.TabIndex = 7
        Me.grdGoals.VirtualMode = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 304)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(35, 14)
        Me.Label16.TabIndex = 24
        Me.Label16.Text = "Goals"
        '
        'cmdRemovePurpose
        '
        Me.cmdRemovePurpose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemovePurpose.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemovePurpose.Location = New System.Drawing.Point(436, 252)
        Me.cmdRemovePurpose.Name = "cmdRemovePurpose"
        Me.cmdRemovePurpose.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemovePurpose.TabIndex = 6
        Me.cmdRemovePurpose.UseVisualStyleBackColor = True
        '
        'cmdAddPurpose
        '
        Me.cmdAddPurpose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddPurpose.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddPurpose.Location = New System.Drawing.Point(436, 222)
        Me.cmdAddPurpose.Name = "cmdAddPurpose"
        Me.cmdAddPurpose.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddPurpose.TabIndex = 5
        Me.cmdAddPurpose.UseVisualStyleBackColor = True
        '
        'grdPurpose
        '
        Me.grdPurpose.AllowUserToAddRows = False
        Me.grdPurpose.AllowUserToDeleteRows = False
        Me.grdPurpose.AllowUserToResizeColumns = False
        Me.grdPurpose.AllowUserToResizeRows = False
        Me.grdPurpose.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdPurpose.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdPurpose.ColumnHeadersVisible = False
        Me.grdPurpose.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colPurpose})
        Me.grdPurpose.Location = New System.Drawing.Point(6, 222)
        Me.grdPurpose.Name = "grdPurpose"
        Me.grdPurpose.RowHeadersVisible = False
        Me.grdPurpose.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdPurpose.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPurpose.Size = New System.Drawing.Size(424, 79)
        Me.grdPurpose.TabIndex = 4
        Me.grdPurpose.VirtualMode = True
        '
        'colPurpose
        '
        Me.colPurpose.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colPurpose.HeaderText = "Name"
        Me.colPurpose.Name = "colPurpose"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 205)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(47, 14)
        Me.Label13.TabIndex = 20
        Me.Label13.Text = "Purpose"
        '
        'txtCoreValues
        '
        Me.txtCoreValues.Location = New System.Drawing.Point(6, 182)
        Me.txtCoreValues.Name = "txtCoreValues"
        Me.txtCoreValues.Size = New System.Drawing.Size(454, 20)
        Me.txtCoreValues.TabIndex = 3
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 165)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(65, 14)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "Core values"
        '
        'txtMessage
        '
        Me.txtMessage.Location = New System.Drawing.Point(6, 142)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(454, 20)
        Me.txtMessage.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 125)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(51, 14)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "Message"
        '
        'txtTarget
        '
        Me.txtTarget.Location = New System.Drawing.Point(6, 102)
        Me.txtTarget.Name = "txtTarget"
        Me.txtTarget.Size = New System.Drawing.Size(454, 20)
        Me.txtTarget.TabIndex = 1
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 85)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(38, 14)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "Target"
        '
        'txtMission
        '
        Me.txtMission.Location = New System.Drawing.Point(6, 35)
        Me.txtMission.Multiline = True
        Me.txtMission.Name = "txtMission"
        Me.txtMission.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMission.Size = New System.Drawing.Size(454, 47)
        Me.txtMission.TabIndex = 0
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 18)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(64, 14)
        Me.Label15.TabIndex = 10
        Me.Label15.Text = "Our mission"
        '
        'picShowProject
        '
        Me.picShowProject.Image = Global.Balthazar.My.Resources.Resources.navigate_right
        Me.picShowProject.Location = New System.Drawing.Point(11, -1)
        Me.picShowProject.Name = "picShowProject"
        Me.picShowProject.Size = New System.Drawing.Size(16, 16)
        Me.picShowProject.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picShowProject.TabIndex = 8
        Me.picShowProject.TabStop = False
        '
        'picHideProject
        '
        Me.picHideProject.Image = Global.Balthazar.My.Resources.Resources.navigate_down
        Me.picHideProject.Location = New System.Drawing.Point(11, -1)
        Me.picHideProject.Name = "picHideProject"
        Me.picHideProject.Size = New System.Drawing.Size(16, 16)
        Me.picHideProject.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picHideProject.TabIndex = 9
        Me.picHideProject.TabStop = False
        '
        'tpQA
        '
        Me.tpQA.Controls.Add(Me.cmdRemoveQuestion)
        Me.tpQA.Controls.Add(Me.cmdAddQuestion)
        Me.tpQA.Controls.Add(Me.PictureBox1)
        Me.tpQA.Controls.Add(Me.grdQuestions)
        Me.tpQA.Location = New System.Drawing.Point(4, 23)
        Me.tpQA.Name = "tpQA"
        Me.tpQA.Padding = New System.Windows.Forms.Padding(3)
        Me.tpQA.Size = New System.Drawing.Size(764, 726)
        Me.tpQA.TabIndex = 1
        Me.tpQA.Text = "Q&A"
        Me.tpQA.UseVisualStyleBackColor = True
        '
        'cmdRemoveQuestion
        '
        Me.cmdRemoveQuestion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveQuestion.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveQuestion.Location = New System.Drawing.Point(732, 74)
        Me.cmdRemoveQuestion.Name = "cmdRemoveQuestion"
        Me.cmdRemoveQuestion.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveQuestion.TabIndex = 10
        Me.cmdRemoveQuestion.UseVisualStyleBackColor = True
        '
        'cmdAddQuestion
        '
        Me.cmdAddQuestion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddQuestion.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddQuestion.Location = New System.Drawing.Point(732, 44)
        Me.cmdAddQuestion.Name = "cmdAddQuestion"
        Me.cmdAddQuestion.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddQuestion.TabIndex = 9
        Me.cmdAddQuestion.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Balthazar.My.Resources.Resources.question_and_answer1
        Me.PictureBox1.Location = New System.Drawing.Point(7, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'grdQuestions
        '
        Me.grdQuestions.AllowUserToAddRows = False
        Me.grdQuestions.AllowUserToDeleteRows = False
        Me.grdQuestions.AllowUserToResizeColumns = False
        Me.grdQuestions.AllowUserToResizeRows = False
        Me.grdQuestions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdQuestions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdQuestions.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colQuestion, Me.colAnswer})
        Me.grdQuestions.Location = New System.Drawing.Point(7, 44)
        Me.grdQuestions.Name = "grdQuestions"
        Me.grdQuestions.RowHeadersVisible = False
        Me.grdQuestions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdQuestions.Size = New System.Drawing.Size(719, 673)
        Me.grdQuestions.TabIndex = 0
        Me.grdQuestions.VirtualMode = True
        '
        'colQuestion
        '
        Me.colQuestion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colQuestion.FillWeight = 30.0!
        Me.colQuestion.HeaderText = "Question"
        Me.colQuestion.Name = "colQuestion"
        '
        'colAnswer
        '
        Me.colAnswer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colAnswer.FillWeight = 70.0!
        Me.colAnswer.HeaderText = "Answer"
        Me.colAnswer.Name = "colAnswer"
        '
        'tpTemplates
        '
        Me.tpTemplates.Controls.Add(Me.grdTemplates)
        Me.tpTemplates.Controls.Add(Me.cmdRemoveTemplate)
        Me.tpTemplates.Controls.Add(Me.cmdAddTemplate)
        Me.tpTemplates.Location = New System.Drawing.Point(4, 23)
        Me.tpTemplates.Name = "tpTemplates"
        Me.tpTemplates.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTemplates.Size = New System.Drawing.Size(764, 726)
        Me.tpTemplates.TabIndex = 2
        Me.tpTemplates.Text = "Templates"
        Me.tpTemplates.UseVisualStyleBackColor = True
        '
        'grdTemplates
        '
        Me.grdTemplates.AllowUserToAddRows = False
        Me.grdTemplates.AllowUserToDeleteRows = False
        Me.grdTemplates.AllowUserToResizeColumns = False
        Me.grdTemplates.AllowUserToResizeRows = False
        Me.grdTemplates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdTemplates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTemplates.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTemplateName, Me.colTemplateDescription, Me.colEdit})
        Me.grdTemplates.Location = New System.Drawing.Point(7, 6)
        Me.grdTemplates.Name = "grdTemplates"
        Me.grdTemplates.RowHeadersVisible = False
        Me.grdTemplates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdTemplates.Size = New System.Drawing.Size(720, 711)
        Me.grdTemplates.TabIndex = 11
        Me.grdTemplates.VirtualMode = True
        '
        'colTemplateName
        '
        Me.colTemplateName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colTemplateName.FillWeight = 30.0!
        Me.colTemplateName.HeaderText = "Name"
        Me.colTemplateName.Name = "colTemplateName"
        '
        'colTemplateDescription
        '
        Me.colTemplateDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colTemplateDescription.FillWeight = 70.0!
        Me.colTemplateDescription.HeaderText = "Description"
        Me.colTemplateDescription.Name = "colTemplateDescription"
        '
        'colEdit
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Format = "Edit"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.colEdit.DefaultCellStyle = DataGridViewCellStyle2
        Me.colEdit.HeaderText = "Text"
        Me.colEdit.Name = "colEdit"
        '
        'cmdRemoveTemplate
        '
        Me.cmdRemoveTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveTemplate.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveTemplate.Location = New System.Drawing.Point(733, 36)
        Me.cmdRemoveTemplate.Name = "cmdRemoveTemplate"
        Me.cmdRemoveTemplate.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveTemplate.TabIndex = 13
        Me.cmdRemoveTemplate.UseVisualStyleBackColor = True
        '
        'cmdAddTemplate
        '
        Me.cmdAddTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddTemplate.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddTemplate.Location = New System.Drawing.Point(733, 6)
        Me.cmdAddTemplate.Name = "cmdAddTemplate"
        Me.cmdAddTemplate.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddTemplate.TabIndex = 12
        Me.cmdAddTemplate.UseVisualStyleBackColor = True
        '
        'ExtendedComboboxColumn1
        '
        Me.ExtendedComboboxColumn1.HeaderText = "Role"
        Me.ExtendedComboboxColumn1.Name = "ExtendedComboboxColumn1"
        Me.ExtendedComboboxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.FillWeight = 75.0!
        Me.DataGridViewTextBoxColumn2.HeaderText = "Phone Nr"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'ExtendedComboboxColumn2
        '
        Me.ExtendedComboboxColumn2.HeaderText = "Role"
        Me.ExtendedComboboxColumn2.Name = "ExtendedComboboxColumn2"
        Me.ExtendedComboboxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ExtendedComboboxColumn2.Width = 125
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.FillWeight = 75.0!
        Me.DataGridViewTextBoxColumn4.HeaderText = "Phone Nr"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn5.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn7.HeaderText = "Location"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        '
        'CalendarColumn1
        '
        Me.CalendarColumn1.HeaderText = "From"
        Me.CalendarColumn1.Name = "CalendarColumn1"
        '
        'CalendarColumn2
        '
        Me.CalendarColumn2.HeaderText = "To"
        Me.CalendarColumn2.Name = "CalendarColumn2"
        '
        'BottomToolStripPanel
        '
        Me.BottomToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.BottomToolStripPanel.Name = "BottomToolStripPanel"
        Me.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.BottomToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.BottomToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'TopToolStripPanel
        '
        Me.TopToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopToolStripPanel.Name = "TopToolStripPanel"
        Me.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.TopToolStripPanel.Padding = New System.Windows.Forms.Padding(0, 0, 25, 25)
        Me.TopToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.TopToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'RightToolStripPanel
        '
        Me.RightToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.RightToolStripPanel.Name = "RightToolStripPanel"
        Me.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.RightToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.RightToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'LeftToolStripPanel
        '
        Me.LeftToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.LeftToolStripPanel.Name = "LeftToolStripPanel"
        Me.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.LeftToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LeftToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'ContentPanel
        '
        Me.ContentPanel.Size = New System.Drawing.Size(758, 692)
        '
        'frmSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(773, 753)
        Me.Controls.Add(Me.tabSetup)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmSetup"
        Me.Text = "Setup"
        Me.tabSetup.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.grpContacts.ResumeLayout(False)
        Me.grpContacts.PerformLayout()
        CType(Me.grdExternalContacts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdInternalContacts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picShowContacts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picHideContacts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grpCampaign.ResumeLayout(False)
        Me.grpCampaign.PerformLayout()
        CType(Me.picShowCampaign, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picHideCampaign, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpProject.ResumeLayout(False)
        Me.grpProject.PerformLayout()
        CType(Me.grdGoals, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPurpose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picShowProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picHideProject, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpQA.ResumeLayout(False)
        Me.tpQA.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdQuestions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpTemplates.ResumeLayout(False)
        CType(Me.grdTemplates, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabSetup As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents grpContacts As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemoveExternalContact As System.Windows.Forms.Button
    Friend WithEvents cmdAddExternalContact As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveInternalContact As System.Windows.Forms.Button
    Friend WithEvents cmdAddInternalContact As System.Windows.Forms.Button
    Friend WithEvents grdExternalContacts As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grdInternalContacts As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents picShowContacts As System.Windows.Forms.PictureBox
    Friend WithEvents picHideContacts As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdAddProduct As System.Windows.Forms.Button
    Friend WithEvents cmdAddClient As System.Windows.Forms.Button
    Friend WithEvents cmbProducts As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbClients As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grpCampaign As System.Windows.Forms.GroupBox
    Friend WithEvents txtWhen As System.Windows.Forms.TextBox
    Friend WithEvents txtHow As System.Windows.Forms.TextBox
    Friend WithEvents txtPurpose As System.Windows.Forms.TextBox
    Friend WithEvents txtWhat As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtBackground As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents picShowCampaign As System.Windows.Forms.PictureBox
    Friend WithEvents picHideCampaign As System.Windows.Forms.PictureBox
    Friend WithEvents grpProject As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemoveGoal As System.Windows.Forms.Button
    Friend WithEvents cmdAddGoal As System.Windows.Forms.Button
    Friend WithEvents grdGoals As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmdRemovePurpose As System.Windows.Forms.Button
    Friend WithEvents cmdAddPurpose As System.Windows.Forms.Button
    Friend WithEvents grdPurpose As System.Windows.Forms.DataGridView
    Friend WithEvents colPurpose As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtCoreValues As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtTarget As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtMission As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents picShowProject As System.Windows.Forms.PictureBox
    Friend WithEvents picHideProject As System.Windows.Forms.PictureBox
    Friend WithEvents ExtendedComboboxColumn1 As Balthazar.ExtendedComboboxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExtendedComboboxColumn2 As Balthazar.ExtendedComboboxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CalendarColumn1 As Balthazar.CalendarColumn
    Friend WithEvents CalendarColumn2 As Balthazar.CalendarColumn
    Friend WithEvents colExName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colExRole As Balthazar.ExtendedComboboxColumn
    Friend WithEvents colExPhone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colExDefault As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRole As Balthazar.ExtendedComboboxColumn
    Friend WithEvents colPhone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDefault As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents tpQA As System.Windows.Forms.TabPage
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents grdQuestions As System.Windows.Forms.DataGridView
    Friend WithEvents cmdRemoveQuestion As System.Windows.Forms.Button
    Friend WithEvents cmdAddQuestion As System.Windows.Forms.Button
    Friend WithEvents colQuestion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAnswer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BottomToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents TopToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents RightToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents LeftToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents ContentPanel As System.Windows.Forms.ToolStripContentPanel
    Friend WithEvents tpTemplates As System.Windows.Forms.TabPage
    Friend WithEvents grdTemplates As System.Windows.Forms.DataGridView
    Friend WithEvents colTemplateName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTemplateDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEdit As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents cmdRemoveTemplate As System.Windows.Forms.Button
    Friend WithEvents cmdAddTemplate As System.Windows.Forms.Button
    Friend WithEvents chkInStore As System.Windows.Forms.CheckBox

End Class
