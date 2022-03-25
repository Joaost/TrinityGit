<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTv4Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTv4Main))
        Me.grdBookings = New System.Windows.Forms.DataGridView()
        Me.colSelected = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEnd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTarget = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBudget = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTv4BT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colError = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.txtContact = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblBookingType = New System.Windows.Forms.Label()
        Me.chkSpecifics = New System.Windows.Forms.CheckBox()
        Me.lnkSpecifics = New System.Windows.Forms.LinkLabel()
        Me.chkRBS = New System.Windows.Forms.CheckBox()
        Me.lnkRBS = New System.Windows.Forms.LinkLabel()
        Me.cmbBookingType = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbTarget = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBudget = New System.Windows.Forms.TextBox()
        Me.lnkSelectDeselect = New System.Windows.Forms.LinkLabel()
        Me.lblRbsWrn = New System.Windows.Forms.Label()
        Me.lblSpecWrn = New System.Windows.Forms.Label()
        Me.cmbOrganizations = New System.Windows.Forms.ComboBox()
        Me.lblLoginName = New System.Windows.Forms.Label()
        Me.lblBookingUrlSpotlight = New System.Windows.Forms.LinkLabel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.imgLogo = New System.Windows.Forms.PictureBox()
        Me.wrnTarg = New System.Windows.Forms.PictureBox()
        Me.wrnBt = New System.Windows.Forms.PictureBox()
        Me.cmdRefreshBudget = New System.Windows.Forms.Button()
        Me.imgSpecWarning = New System.Windows.Forms.PictureBox()
        Me.imgRBSWarning = New System.Windows.Forms.PictureBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmbClient = New System.Windows.Forms.ComboBox()
        Me.lblClient = New System.Windows.Forms.Label()
        Me.cmbAgencies = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        CType(Me.grdBookings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wrnTarg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wrnBt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgSpecWarning, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgRBSWarning, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdBookings
        '
        Me.grdBookings.AllowUserToAddRows = False
        Me.grdBookings.AllowUserToDeleteRows = False
        Me.grdBookings.AllowUserToResizeColumns = False
        Me.grdBookings.AllowUserToResizeRows = False
        Me.grdBookings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdBookings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSelected, Me.colStart, Me.colEnd, Me.colTarget, Me.colBudget, Me.colName, Me.colType, Me.colTv4BT, Me.colError})
        Me.grdBookings.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdBookings.Location = New System.Drawing.Point(0, 27)
        Me.grdBookings.Name = "grdBookings"
        Me.grdBookings.RowHeadersVisible = False
        Me.grdBookings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdBookings.Size = New System.Drawing.Size(730, 341)
        Me.grdBookings.TabIndex = 28
        Me.grdBookings.VirtualMode = True
        '
        'colSelected
        '
        Me.colSelected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colSelected.FillWeight = 15.0!
        Me.colSelected.HeaderText = ""
        Me.colSelected.Name = "colSelected"
        Me.colSelected.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colSelected.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'colStart
        '
        Me.colStart.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colStart.FillWeight = 40.0!
        Me.colStart.HeaderText = "Start"
        Me.colStart.Name = "colStart"
        Me.colStart.ReadOnly = True
        Me.colStart.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colEnd
        '
        Me.colEnd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colEnd.FillWeight = 40.0!
        Me.colEnd.HeaderText = "End"
        Me.colEnd.Name = "colEnd"
        Me.colEnd.ReadOnly = True
        Me.colEnd.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colTarget
        '
        Me.colTarget.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colTarget.FillWeight = 30.0!
        Me.colTarget.HeaderText = "Target"
        Me.colTarget.Name = "colTarget"
        Me.colTarget.ReadOnly = True
        Me.colTarget.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colBudget
        '
        Me.colBudget.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colBudget.FillWeight = 45.0!
        Me.colBudget.HeaderText = "Budget"
        Me.colBudget.Name = "colBudget"
        Me.colBudget.ReadOnly = True
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colName.FillWeight = 60.0!
        Me.colName.HeaderText = "Channel Name"
        Me.colName.Name = "colName"
        Me.colName.ReadOnly = True
        Me.colName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colType
        '
        Me.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colType.FillWeight = 50.0!
        Me.colType.HeaderText = "Trinity"
        Me.colType.Name = "colType"
        Me.colType.ReadOnly = True
        '
        'colTv4BT
        '
        Me.colTv4BT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colTv4BT.FillWeight = 50.0!
        Me.colTv4BT.HeaderText = "TV4"
        Me.colTv4BT.Name = "colTv4BT"
        Me.colTv4BT.ReadOnly = True
        Me.colTv4BT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colError
        '
        Me.colError.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colError.FillWeight = 11.0!
        Me.colError.HeaderText = ""
        Me.colError.Name = "colError"
        Me.colError.ReadOnly = True
        Me.colError.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Location = New System.Drawing.Point(508, 559)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(65, 33)
        Me.cmdCancel.TabIndex = 26
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'txtContact
        '
        Me.txtContact.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtContact.Location = New System.Drawing.Point(109, 402)
        Me.txtContact.Name = "txtContact"
        Me.txtContact.Size = New System.Drawing.Size(155, 22)
        Me.txtContact.TabIndex = 38
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(106, 385)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(130, 13)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Channel contact person"
        '
        'lblBookingType
        '
        Me.lblBookingType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblBookingType.AutoSize = True
        Me.lblBookingType.Location = New System.Drawing.Point(12, 385)
        Me.lblBookingType.MaximumSize = New System.Drawing.Size(80, 0)
        Me.lblBookingType.Name = "lblBookingType"
        Me.lblBookingType.Size = New System.Drawing.Size(40, 13)
        Me.lblBookingType.TabIndex = 34
        Me.lblBookingType.Text = "Label4"
        '
        'chkSpecifics
        '
        Me.chkSpecifics.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSpecifics.AutoSize = True
        Me.chkSpecifics.Location = New System.Drawing.Point(508, 456)
        Me.chkSpecifics.Name = "chkSpecifics"
        Me.chkSpecifics.Size = New System.Drawing.Size(15, 14)
        Me.chkSpecifics.TabIndex = 32
        Me.chkSpecifics.UseVisualStyleBackColor = True
        '
        'lnkSpecifics
        '
        Me.lnkSpecifics.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lnkSpecifics.AutoSize = True
        Me.lnkSpecifics.Location = New System.Drawing.Point(526, 455)
        Me.lnkSpecifics.Name = "lnkSpecifics"
        Me.lnkSpecifics.Size = New System.Drawing.Size(51, 13)
        Me.lnkSpecifics.TabIndex = 31
        Me.lnkSpecifics.TabStop = True
        Me.lnkSpecifics.Text = "Specifics"
        '
        'chkRBS
        '
        Me.chkRBS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkRBS.AutoSize = True
        Me.chkRBS.Location = New System.Drawing.Point(508, 436)
        Me.chkRBS.Name = "chkRBS"
        Me.chkRBS.Size = New System.Drawing.Size(15, 14)
        Me.chkRBS.TabIndex = 30
        Me.chkRBS.UseVisualStyleBackColor = True
        '
        'lnkRBS
        '
        Me.lnkRBS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lnkRBS.AutoSize = True
        Me.lnkRBS.Location = New System.Drawing.Point(526, 435)
        Me.lnkRBS.Name = "lnkRBS"
        Me.lnkRBS.Size = New System.Drawing.Size(27, 13)
        Me.lnkRBS.TabIndex = 29
        Me.lnkRBS.TabStop = True
        Me.lnkRBS.Text = "RBS"
        '
        'cmbBookingType
        '
        Me.cmbBookingType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbBookingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBookingType.FormattingEnabled = True
        Me.cmbBookingType.Location = New System.Drawing.Point(109, 446)
        Me.cmbBookingType.Name = "cmbBookingType"
        Me.cmbBookingType.Size = New System.Drawing.Size(155, 21)
        Me.cmbBookingType.TabIndex = 46
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(106, 429)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(94, 13)
        Me.Label6.TabIndex = 45
        Me.Label6.Text = "TV4 Bookingtype"
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(451, 406)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(17, 13)
        Me.Label3.TabIndex = 43
        Me.Label3.Text = "kr"
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(292, 386)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "Max budget"
        '
        'cmbTarget
        '
        Me.cmbTarget.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTarget.FormattingEnabled = True
        Me.cmbTarget.Location = New System.Drawing.Point(295, 447)
        Me.cmbTarget.Name = "cmbTarget"
        Me.cmbTarget.Size = New System.Drawing.Size(179, 21)
        Me.cmbTarget.TabIndex = 40
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(292, 429)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "Target"
        '
        'txtBudget
        '
        Me.txtBudget.Location = New System.Drawing.Point(295, 402)
        Me.txtBudget.Name = "txtBudget"
        Me.txtBudget.Size = New System.Drawing.Size(150, 22)
        Me.txtBudget.TabIndex = 47
        '
        'lnkSelectDeselect
        '
        Me.lnkSelectDeselect.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lnkSelectDeselect.AutoSize = True
        Me.lnkSelectDeselect.Location = New System.Drawing.Point(12, 7)
        Me.lnkSelectDeselect.Name = "lnkSelectDeselect"
        Me.lnkSelectDeselect.Size = New System.Drawing.Size(99, 13)
        Me.lnkSelectDeselect.TabIndex = 53
        Me.lnkSelectDeselect.TabStop = True
        Me.lnkSelectDeselect.Text = "Select/Deselect all"
        '
        'lblRbsWrn
        '
        Me.lblRbsWrn.AutoSize = True
        Me.lblRbsWrn.ForeColor = System.Drawing.Color.Red
        Me.lblRbsWrn.Location = New System.Drawing.Point(596, 436)
        Me.lblRbsWrn.Name = "lblRbsWrn"
        Me.lblRbsWrn.Size = New System.Drawing.Size(72, 13)
        Me.lblRbsWrn.TabIndex = 54
        Me.lblRbsWrn.Text = "RBS is empty"
        Me.lblRbsWrn.Visible = False
        '
        'lblSpecWrn
        '
        Me.lblSpecWrn.AutoSize = True
        Me.lblSpecWrn.ForeColor = System.Drawing.Color.Red
        Me.lblSpecWrn.Location = New System.Drawing.Point(596, 455)
        Me.lblSpecWrn.Name = "lblSpecWrn"
        Me.lblSpecWrn.Size = New System.Drawing.Size(96, 13)
        Me.lblSpecWrn.TabIndex = 55
        Me.lblSpecWrn.Text = "Specifics is empty"
        Me.lblSpecWrn.Visible = False
        '
        'cmbOrganizations
        '
        Me.cmbOrganizations.Location = New System.Drawing.Point(508, 402)
        Me.cmbOrganizations.Name = "cmbOrganizations"
        Me.cmbOrganizations.Size = New System.Drawing.Size(193, 21)
        Me.cmbOrganizations.TabIndex = 56
        Me.cmbOrganizations.Visible = False
        '
        'lblLoginName
        '
        Me.lblLoginName.AutoSize = True
        Me.lblLoginName.Location = New System.Drawing.Point(543, 385)
        Me.lblLoginName.Name = "lblLoginName"
        Me.lblLoginName.Size = New System.Drawing.Size(0, 13)
        Me.lblLoginName.TabIndex = 57
        '
        'lblBookingUrlSpotlight
        '
        Me.lblBookingUrlSpotlight.AutoSize = True
        Me.lblBookingUrlSpotlight.Location = New System.Drawing.Point(106, 546)
        Me.lblBookingUrlSpotlight.MaximumSize = New System.Drawing.Size(400, 0)
        Me.lblBookingUrlSpotlight.Name = "lblBookingUrlSpotlight"
        Me.lblBookingUrlSpotlight.Size = New System.Drawing.Size(49, 13)
        Me.lblBookingUrlSpotlight.TabIndex = 58
        Me.lblBookingUrlSpotlight.TabStop = True
        Me.lblBookingUrlSpotlight.Text = "UrlLabel"
        Me.lblBookingUrlSpotlight.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(106, 530)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(140, 13)
        Me.Label5.TabIndex = 59
        Me.Label5.Text = "Booking url confirmation:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Location = New System.Drawing.Point(505, 385)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 13)
        Me.Label7.TabIndex = 60
        Me.Label7.Text = "User:"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(730, 25)
        Me.ToolStrip1.TabIndex = 61
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(730, 24)
        Me.MenuStrip1.TabIndex = 62
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'imgLogo
        '
        Me.imgLogo.Location = New System.Drawing.Point(12, 412)
        Me.imgLogo.Name = "imgLogo"
        Me.imgLogo.Size = New System.Drawing.Size(62, 57)
        Me.imgLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.imgLogo.TabIndex = 52
        Me.imgLogo.TabStop = False
        '
        'wrnTarg
        '
        Me.wrnTarg.Image = CType(resources.GetObject("wrnTarg.Image"), System.Drawing.Image)
        Me.wrnTarg.Location = New System.Drawing.Point(480, 448)
        Me.wrnTarg.Name = "wrnTarg"
        Me.wrnTarg.Size = New System.Drawing.Size(16, 16)
        Me.wrnTarg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.wrnTarg.TabIndex = 51
        Me.wrnTarg.TabStop = False
        Me.wrnTarg.Visible = False
        '
        'wrnBt
        '
        Me.wrnBt.Image = CType(resources.GetObject("wrnBt.Image"), System.Drawing.Image)
        Me.wrnBt.Location = New System.Drawing.Point(270, 446)
        Me.wrnBt.Name = "wrnBt"
        Me.wrnBt.Size = New System.Drawing.Size(16, 16)
        Me.wrnBt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.wrnBt.TabIndex = 50
        Me.wrnBt.TabStop = False
        Me.wrnBt.Visible = False
        '
        'cmdRefreshBudget
        '
        Me.cmdRefreshBudget.Image = Global.TV4Online.My.Resources.Resources.sync_2_small
        Me.cmdRefreshBudget.Location = New System.Drawing.Point(473, 401)
        Me.cmdRefreshBudget.Name = "cmdRefreshBudget"
        Me.cmdRefreshBudget.Size = New System.Drawing.Size(23, 23)
        Me.cmdRefreshBudget.TabIndex = 48
        Me.cmdRefreshBudget.UseVisualStyleBackColor = True
        Me.cmdRefreshBudget.Visible = False
        '
        'imgSpecWarning
        '
        Me.imgSpecWarning.Image = CType(resources.GetObject("imgSpecWarning.Image"), System.Drawing.Image)
        Me.imgSpecWarning.Location = New System.Drawing.Point(579, 453)
        Me.imgSpecWarning.Name = "imgSpecWarning"
        Me.imgSpecWarning.Size = New System.Drawing.Size(16, 16)
        Me.imgSpecWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.imgSpecWarning.TabIndex = 36
        Me.imgSpecWarning.TabStop = False
        Me.imgSpecWarning.Visible = False
        '
        'imgRBSWarning
        '
        Me.imgRBSWarning.Image = CType(resources.GetObject("imgRBSWarning.Image"), System.Drawing.Image)
        Me.imgRBSWarning.Location = New System.Drawing.Point(579, 436)
        Me.imgRBSWarning.Name = "imgRBSWarning"
        Me.imgRBSWarning.Size = New System.Drawing.Size(16, 16)
        Me.imgRBSWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.imgRBSWarning.TabIndex = 35
        Me.imgRBSWarning.TabStop = False
        Me.imgRBSWarning.Visible = False
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Image = Global.TV4Online.My.Resources.Resources.upload_2_32x32
        Me.cmdOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdOk.Location = New System.Drawing.Point(579, 559)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(139, 33)
        Me.cmdOk.TabIndex = 25
        Me.cmdOk.Text = "Upload bookings"
        Me.cmdOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewImageColumn1.FillWeight = 10.0!
        Me.DataGridViewImageColumn1.HeaderText = ""
        Me.DataGridViewImageColumn1.Image = Global.TV4Online.My.Resources.Resources.check
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.ReadOnly = True
        Me.DataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewImageColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.FillWeight = 50.0!
        Me.DataGridViewTextBoxColumn1.HeaderText = "Start"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.FillWeight = 50.0!
        Me.DataGridViewTextBoxColumn2.HeaderText = "End"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.FillWeight = 45.0!
        Me.DataGridViewTextBoxColumn3.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn4.FillWeight = 60.0!
        Me.DataGridViewTextBoxColumn4.HeaderText = "Budget"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn5.FillWeight = 60.0!
        Me.DataGridViewTextBoxColumn5.HeaderText = "Type"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.FillWeight = 60.0!
        Me.DataGridViewTextBoxColumn6.HeaderText = "Trinity Type"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn7.FillWeight = 70.0!
        Me.DataGridViewTextBoxColumn7.HeaderText = "TV4 Type"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn8.FillWeight = 11.0!
        Me.DataGridViewTextBoxColumn8.HeaderText = ""
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'cmbClient
        '
        Me.cmbClient.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClient.FormattingEnabled = True
        Me.cmbClient.Location = New System.Drawing.Point(109, 490)
        Me.cmbClient.Name = "cmbClient"
        Me.cmbClient.Size = New System.Drawing.Size(155, 21)
        Me.cmbClient.TabIndex = 64
        '
        'lblClient
        '
        Me.lblClient.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblClient.AutoSize = True
        Me.lblClient.Location = New System.Drawing.Point(106, 473)
        Me.lblClient.Name = "lblClient"
        Me.lblClient.Size = New System.Drawing.Size(37, 13)
        Me.lblClient.TabIndex = 63
        Me.lblClient.Text = "Client"
        '
        'cmbAgencies
        '
        Me.cmbAgencies.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbAgencies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgencies.FormattingEnabled = True
        Me.cmbAgencies.Location = New System.Drawing.Point(295, 490)
        Me.cmbAgencies.Name = "cmbAgencies"
        Me.cmbAgencies.Size = New System.Drawing.Size(155, 21)
        Me.cmbAgencies.TabIndex = 66
        '
        'Label8
        '
        Me.Label8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(292, 473)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 13)
        Me.Label8.TabIndex = 65
        Me.Label8.Text = "Agency"
        '
        'frmTv4Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(730, 604)
        Me.Controls.Add(Me.cmbAgencies)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cmbClient)
        Me.Controls.Add(Me.lblClient)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblBookingUrlSpotlight)
        Me.Controls.Add(Me.lblLoginName)
        Me.Controls.Add(Me.cmbOrganizations)
        Me.Controls.Add(Me.lblSpecWrn)
        Me.Controls.Add(Me.lblRbsWrn)
        Me.Controls.Add(Me.lnkSelectDeselect)
        Me.Controls.Add(Me.imgLogo)
        Me.Controls.Add(Me.wrnTarg)
        Me.Controls.Add(Me.wrnBt)
        Me.Controls.Add(Me.cmdRefreshBudget)
        Me.Controls.Add(Me.txtBudget)
        Me.Controls.Add(Me.cmbBookingType)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbTarget)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtContact)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.imgSpecWarning)
        Me.Controls.Add(Me.imgRBSWarning)
        Me.Controls.Add(Me.lblBookingType)
        Me.Controls.Add(Me.chkSpecifics)
        Me.Controls.Add(Me.lnkSpecifics)
        Me.Controls.Add(Me.chkRBS)
        Me.Controls.Add(Me.lnkRBS)
        Me.Controls.Add(Me.grdBookings)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmTv4Main"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "TV4 Spotlight"
        CType(Me.grdBookings,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.imgLogo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.wrnTarg,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.wrnBt,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.imgSpecWarning,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.imgRBSWarning,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents grdBookings As System.Windows.Forms.DataGridView
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents txtContact As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblBookingType As System.Windows.Forms.Label
    Friend WithEvents chkSpecifics As System.Windows.Forms.CheckBox
    Friend WithEvents lnkSpecifics As System.Windows.Forms.LinkLabel
    Friend WithEvents chkRBS As System.Windows.Forms.CheckBox
    Friend WithEvents lnkRBS As System.Windows.Forms.LinkLabel
    Friend WithEvents cmbBookingType As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbTarget As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents txtBudget As System.Windows.Forms.TextBox
    Friend WithEvents cmdRefreshBudget As System.Windows.Forms.Button
    Friend WithEvents wrnBt As System.Windows.Forms.PictureBox
    Friend WithEvents wrnTarg As System.Windows.Forms.PictureBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents imgSpecWarning As System.Windows.Forms.PictureBox
    Friend WithEvents imgRBSWarning As System.Windows.Forms.PictureBox
    Friend WithEvents imgLogo As System.Windows.Forms.PictureBox
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lnkSelectDeselect As System.Windows.Forms.LinkLabel
    Friend WithEvents lblRbsWrn As System.Windows.Forms.Label
    Friend WithEvents lblSpecWrn As System.Windows.Forms.Label
    Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmbOrganizations As System.Windows.Forms.ComboBox
    Friend WithEvents lblLoginName As System.Windows.Forms.Label
    Friend WithEvents lblBookingUrlSpotlight As Windows.Forms.LinkLabel
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents colSelected As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colStart As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEnd As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTarget As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBudget As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colName As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colType As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTv4BT As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colError As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents MenuStrip1 As Windows.Forms.MenuStrip
    Friend WithEvents cmbClient As Windows.Forms.ComboBox
    Friend WithEvents lblClient As Windows.Forms.Label
    Friend WithEvents cmbAgencies As Windows.Forms.ComboBox
    Friend WithEvents Label8 As Windows.Forms.Label
End Class
