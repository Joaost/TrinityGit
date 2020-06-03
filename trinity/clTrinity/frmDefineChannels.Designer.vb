<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefineChannels
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDefineChannels))
        Me.cmbChannel = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtChannelGroup = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chkBid = New System.Windows.Forms.CheckBox()
        Me.btnGetAdtooxMediaID = New System.Windows.Forms.Button()
        Me.txtAdtooxID = New System.Windows.Forms.TextBox()
        Me.lblAdTooxId = New System.Windows.Forms.Label()
        Me.chkUseBreakB = New System.Windows.Forms.CheckBox()
        Me.chkUseBillB = New System.Windows.Forms.CheckBox()
        Me.txtMatrix = New System.Windows.Forms.TextBox()
        Me.lblMatrix = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbUniverse = New System.Windows.Forms.ComboBox()
        Me.txtMarathon = New System.Windows.Forms.TextBox()
        Me.lblMarathon = New System.Windows.Forms.Label()
        Me.txtConnected = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtVHS = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtDelivery = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCommission = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAdedge = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtListNr = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtShortName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grdBT = New System.Windows.Forms.DataGridView()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colShortName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEFactor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIsRBS = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colIsSpec = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colPremium = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colComp = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colSpons = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colDaypart = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colBookingcode = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSaveUser = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdAddBT = New System.Windows.Forms.Button()
        Me.cmdDeleteBT = New System.Windows.Forms.Button()
        Me.cmdSaveToFile = New System.Windows.Forms.Button()
        Me.cmdDeleteChannel = New System.Windows.Forms.Button()
        Me.cmdAddChannel = New System.Windows.Forms.Button()
        Me.grbBookingType = New System.Windows.Forms.GroupBox()
        Me.typeSpeedTimer = New System.Windows.Forms.Timer(Me.components)
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdBT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbBookingType.SuspendLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbChannel
        '
        Me.cmbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChannel.FormattingEnabled = True
        Me.cmbChannel.Location = New System.Drawing.Point(65, 24)
        Me.cmbChannel.Name = "cmbChannel"
        Me.cmbChannel.Size = New System.Drawing.Size(120, 21)
        Me.cmbChannel.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtChannelGroup)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.chkBid)
        Me.GroupBox1.Controls.Add(Me.btnGetAdtooxMediaID)
        Me.GroupBox1.Controls.Add(Me.txtAdtooxID)
        Me.GroupBox1.Controls.Add(Me.lblAdTooxId)
        Me.GroupBox1.Controls.Add(Me.chkUseBreakB)
        Me.GroupBox1.Controls.Add(Me.chkUseBillB)
        Me.GroupBox1.Controls.Add(Me.txtMatrix)
        Me.GroupBox1.Controls.Add(Me.lblMatrix)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.cmbUniverse)
        Me.GroupBox1.Controls.Add(Me.txtMarathon)
        Me.GroupBox1.Controls.Add(Me.lblMarathon)
        Me.GroupBox1.Controls.Add(Me.txtConnected)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtVHS)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtDelivery)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtCommission)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtAdedge)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtListNr)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtShortName)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(534, 116)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Info"
        '
        'txtChannelGroup
        '
        Me.txtChannelGroup.Location = New System.Drawing.Point(256, 31)
        Me.txtChannelGroup.Name = "txtChannelGroup"
        Me.txtChannelGroup.Size = New System.Drawing.Size(77, 22)
        Me.txtChannelGroup.TabIndex = 30
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(256, 15)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(85, 13)
        Me.Label11.TabIndex = 29
        Me.Label11.Text = "Channel group"
        '
        'chkBid
        '
        Me.chkBid.AutoSize = True
        Me.chkBid.Enabled = False
        Me.chkBid.Location = New System.Drawing.Point(322, 134)
        Me.chkBid.Name = "chkBid"
        Me.chkBid.Size = New System.Drawing.Size(65, 17)
        Me.chkBid.TabIndex = 28
        Me.chkBid.Text = "Use bid"
        Me.chkBid.UseVisualStyleBackColor = True
        Me.chkBid.Visible = False
        '
        'btnGetAdtooxMediaID
        '
        Me.btnGetAdtooxMediaID.Enabled = False
        Me.btnGetAdtooxMediaID.FlatAppearance.BorderSize = 0
        Me.btnGetAdtooxMediaID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGetAdtooxMediaID.Location = New System.Drawing.Point(474, 80)
        Me.btnGetAdtooxMediaID.Name = "btnGetAdtooxMediaID"
        Me.btnGetAdtooxMediaID.Size = New System.Drawing.Size(20, 20)
        Me.btnGetAdtooxMediaID.TabIndex = 27
        Me.btnGetAdtooxMediaID.Text = "..."
        Me.btnGetAdtooxMediaID.UseVisualStyleBackColor = True
        Me.btnGetAdtooxMediaID.Visible = False
        '
        'txtAdtooxID
        '
        Me.txtAdtooxID.Enabled = False
        Me.txtAdtooxID.Location = New System.Drawing.Point(418, 81)
        Me.txtAdtooxID.Name = "txtAdtooxID"
        Me.txtAdtooxID.ReadOnly = True
        Me.txtAdtooxID.Size = New System.Drawing.Size(54, 22)
        Me.txtAdtooxID.TabIndex = 26
        Me.txtAdtooxID.Visible = False
        '
        'lblAdTooxId
        '
        Me.lblAdTooxId.AutoSize = True
        Me.lblAdTooxId.Location = New System.Drawing.Point(418, 65)
        Me.lblAdTooxId.Name = "lblAdTooxId"
        Me.lblAdTooxId.Size = New System.Drawing.Size(58, 13)
        Me.lblAdTooxId.TabIndex = 25
        Me.lblAdTooxId.Text = "Adtoox ID"
        Me.lblAdTooxId.Visible = False
        '
        'chkUseBreakB
        '
        Me.chkUseBreakB.AutoSize = True
        Me.chkUseBreakB.Enabled = False
        Me.chkUseBreakB.Location = New System.Drawing.Point(198, 134)
        Me.chkUseBreakB.Name = "chkUseBreakB"
        Me.chkUseBreakB.Size = New System.Drawing.Size(122, 17)
        Me.chkUseBreakB.TabIndex = 24
        Me.chkUseBreakB.Text = "Use Breakbumpers"
        Me.chkUseBreakB.UseVisualStyleBackColor = True
        Me.chkUseBreakB.Visible = False
        '
        'chkUseBillB
        '
        Me.chkUseBillB.AutoSize = True
        Me.chkUseBillB.Enabled = False
        Me.chkUseBillB.Location = New System.Drawing.Point(97, 134)
        Me.chkUseBillB.Name = "chkUseBillB"
        Me.chkUseBillB.Size = New System.Drawing.Size(100, 17)
        Me.chkUseBillB.TabIndex = 23
        Me.chkUseBillB.Text = "Use Billboards"
        Me.chkUseBillB.UseVisualStyleBackColor = True
        Me.chkUseBillB.Visible = False
        '
        'txtMatrix
        '
        Me.txtMatrix.Enabled = False
        Me.txtMatrix.Location = New System.Drawing.Point(339, 31)
        Me.txtMatrix.Name = "txtMatrix"
        Me.txtMatrix.Size = New System.Drawing.Size(73, 22)
        Me.txtMatrix.TabIndex = 22
        '
        'lblMatrix
        '
        Me.lblMatrix.AutoSize = True
        Me.lblMatrix.Location = New System.Drawing.Point(339, 15)
        Me.lblMatrix.Name = "lblMatrix"
        Me.lblMatrix.Size = New System.Drawing.Size(83, 13)
        Me.lblMatrix.TabIndex = 21
        Me.lblMatrix.Text = "Matrix channel"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 116)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(89, 13)
        Me.Label12.TabIndex = 20
        Me.Label12.Text = "Buying universe"
        Me.Label12.Visible = False
        '
        'cmbUniverse
        '
        Me.cmbUniverse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUniverse.Enabled = False
        Me.cmbUniverse.FormattingEnabled = True
        Me.cmbUniverse.Location = New System.Drawing.Point(6, 132)
        Me.cmbUniverse.Name = "cmbUniverse"
        Me.cmbUniverse.Size = New System.Drawing.Size(85, 21)
        Me.cmbUniverse.TabIndex = 19
        Me.cmbUniverse.Visible = False
        '
        'txtMarathon
        '
        Me.txtMarathon.Enabled = False
        Me.txtMarathon.Location = New System.Drawing.Point(418, 31)
        Me.txtMarathon.Name = "txtMarathon"
        Me.txtMarathon.Size = New System.Drawing.Size(73, 22)
        Me.txtMarathon.TabIndex = 18
        '
        'lblMarathon
        '
        Me.lblMarathon.AutoSize = True
        Me.lblMarathon.Location = New System.Drawing.Point(418, 15)
        Me.lblMarathon.Name = "lblMarathon"
        Me.lblMarathon.Size = New System.Drawing.Size(80, 13)
        Me.lblMarathon.TabIndex = 17
        Me.lblMarathon.Text = "Marathon chn"
        '
        'txtConnected
        '
        Me.txtConnected.Enabled = False
        Me.txtConnected.Location = New System.Drawing.Point(179, 81)
        Me.txtConnected.Name = "txtConnected"
        Me.txtConnected.Size = New System.Drawing.Size(71, 22)
        Me.txtConnected.TabIndex = 16
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(179, 65)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(77, 13)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "Connected to"
        '
        'txtVHS
        '
        Me.txtVHS.Enabled = False
        Me.txtVHS.Location = New System.Drawing.Point(6, 241)
        Me.txtVHS.Multiline = True
        Me.txtVHS.Name = "txtVHS"
        Me.txtVHS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtVHS.Size = New System.Drawing.Size(488, 26)
        Me.txtVHS.TabIndex = 14
        Me.txtVHS.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(3, 225)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 13)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Second Address"
        Me.Label9.Visible = False
        '
        'txtDelivery
        '
        Me.txtDelivery.Enabled = False
        Me.txtDelivery.Location = New System.Drawing.Point(6, 184)
        Me.txtDelivery.Multiline = True
        Me.txtDelivery.Name = "txtDelivery"
        Me.txtDelivery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDelivery.Size = New System.Drawing.Size(488, 22)
        Me.txtDelivery.TabIndex = 12
        Me.txtDelivery.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 168)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(91, 13)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Delivery Address"
        Me.Label8.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(316, 84)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(16, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "%"
        '
        'txtCommission
        '
        Me.txtCommission.Enabled = False
        Me.txtCommission.Location = New System.Drawing.Point(256, 81)
        Me.txtCommission.Name = "txtCommission"
        Me.txtCommission.Size = New System.Drawing.Size(54, 22)
        Me.txtCommission.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(256, 65)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Commission"
        '
        'txtAdedge
        '
        Me.txtAdedge.Enabled = False
        Me.txtAdedge.Location = New System.Drawing.Point(6, 81)
        Me.txtAdedge.Name = "txtAdedge"
        Me.txtAdedge.Size = New System.Drawing.Size(167, 22)
        Me.txtAdedge.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 65)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Adedge channels"
        '
        'txtListNr
        '
        Me.txtListNr.Enabled = False
        Me.txtListNr.Location = New System.Drawing.Point(339, 81)
        Me.txtListNr.Name = "txtListNr"
        Me.txtListNr.Size = New System.Drawing.Size(73, 22)
        Me.txtListNr.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(339, 65)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "List number"
        '
        'txtShortName
        '
        Me.txtShortName.Enabled = False
        Me.txtShortName.Location = New System.Drawing.Point(179, 31)
        Me.txtShortName.Name = "txtShortName"
        Me.txtShortName.Size = New System.Drawing.Size(71, 22)
        Me.txtShortName.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(179, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Short name"
        '
        'txtName
        '
        Me.txtName.Enabled = False
        Me.txtName.Location = New System.Drawing.Point(6, 31)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(167, 22)
        Me.txtName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Channel name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(65, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Channel"
        '
        'grdBT
        '
        Me.grdBT.AllowUserToAddRows = False
        Me.grdBT.AllowUserToDeleteRows = False
        Me.grdBT.AllowUserToResizeColumns = False
        Me.grdBT.AllowUserToResizeRows = False
        Me.grdBT.BackgroundColor = System.Drawing.Color.Silver
        Me.grdBT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdBT.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colShortName, Me.colEFactor, Me.colIsRBS, Me.colIsSpec, Me.colPremium, Me.colComp, Me.colSpons, Me.colDaypart, Me.colBookingcode})
        Me.grdBT.Location = New System.Drawing.Point(6, 22)
        Me.grdBT.Name = "grdBT"
        Me.grdBT.RowHeadersVisible = False
        Me.grdBT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdBT.Size = New System.Drawing.Size(494, 204)
        Me.grdBT.TabIndex = 3
        Me.grdBT.VirtualMode = True
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        '
        'colShortName
        '
        Me.colShortName.HeaderText = "Short name"
        Me.colShortName.Name = "colShortName"
        Me.colShortName.Width = 68
        '
        'colEFactor
        '
        Me.colEFactor.HeaderText = "Factor"
        Me.colEFactor.Name = "colEFactor"
        Me.colEFactor.Width = 45
        '
        'colIsRBS
        '
        Me.colIsRBS.HeaderText = "RBS"
        Me.colIsRBS.Name = "colIsRBS"
        Me.colIsRBS.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colIsRBS.Width = 35
        '
        'colIsSpec
        '
        Me.colIsSpec.HeaderText = "Spec"
        Me.colIsSpec.Name = "colIsSpec"
        Me.colIsSpec.Width = 35
        '
        'colPremium
        '
        Me.colPremium.HeaderText = "Prem"
        Me.colPremium.Name = "colPremium"
        Me.colPremium.Width = 35
        '
        'colComp
        '
        Me.colComp.HeaderText = "Comp"
        Me.colComp.Name = "colComp"
        Me.colComp.Width = 35
        '
        'colSpons
        '
        Me.colSpons.HeaderText = "Spns"
        Me.colSpons.Name = "colSpons"
        Me.colSpons.Width = 35
        '
        'colDaypart
        '
        Me.colDaypart.HeaderText = "Daypart"
        Me.colDaypart.Name = "colDaypart"
        Me.colDaypart.Width = 50
        '
        'colBookingcode
        '
        Me.colBookingcode.HeaderText = "B.Code"
        Me.colBookingcode.Name = "colBookingcode"
        Me.colBookingcode.Visible = False
        Me.colBookingcode.Width = 50
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Location = New System.Drawing.Point(456, 339)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(90, 32)
        Me.cmdOk.TabIndex = 17
        Me.cmdOk.Text = "Apply"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'cmdSaveUser
        '
        Me.cmdSaveUser.FlatAppearance.BorderSize = 0
        Me.cmdSaveUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSaveUser.Image = Global.clTrinity.My.Resources.Resources.save_2_small
        Me.cmdSaveUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSaveUser.Location = New System.Drawing.Point(452, 20)
        Me.cmdSaveUser.Name = "cmdSaveUser"
        Me.cmdSaveUser.Size = New System.Drawing.Size(94, 29)
        Me.cmdSaveUser.TabIndex = 23
        Me.cmdSaveUser.Text = "Save to DB"
        Me.cmdSaveUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdSaveUser, "Save as default on all users")
        Me.cmdSaveUser.UseVisualStyleBackColor = True
        '
        'cmdUpdate
        '
        Me.cmdUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdUpdate.FlatAppearance.BorderSize = 0
        Me.cmdUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUpdate.Image = Global.clTrinity.My.Resources.Resources.sync_2_24x23
        Me.cmdUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdUpdate.Location = New System.Drawing.Point(360, 20)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(89, 29)
        Me.cmdUpdate.TabIndex = 21
        Me.cmdUpdate.Text = "Update"
        Me.cmdUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdUpdate, "Update present channels from database (not prices)")
        Me.cmdUpdate.UseVisualStyleBackColor = True
        '
        'cmdAddBT
        '
        Me.cmdAddBT.FlatAppearance.BorderSize = 0
        Me.cmdAddBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddBT.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.cmdAddBT.Location = New System.Drawing.Point(505, 20)
        Me.cmdAddBT.Name = "cmdAddBT"
        Me.cmdAddBT.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddBT.TabIndex = 13
        Me.ToolTip.SetToolTip(Me.cmdAddBT, "Add BookingType")
        Me.cmdAddBT.UseVisualStyleBackColor = True
        '
        'cmdDeleteBT
        '
        Me.cmdDeleteBT.FlatAppearance.BorderSize = 0
        Me.cmdDeleteBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDeleteBT.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdDeleteBT.Location = New System.Drawing.Point(506, 46)
        Me.cmdDeleteBT.Name = "cmdDeleteBT"
        Me.cmdDeleteBT.Size = New System.Drawing.Size(22, 20)
        Me.cmdDeleteBT.TabIndex = 14
        Me.ToolTip.SetToolTip(Me.cmdDeleteBT, "Delete Booking Type")
        Me.cmdDeleteBT.UseVisualStyleBackColor = True
        '
        'cmdSaveToFile
        '
        Me.cmdSaveToFile.Enabled = False
        Me.cmdSaveToFile.FlatAppearance.BorderSize = 0
        Me.cmdSaveToFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSaveToFile.Image = Global.clTrinity.My.Resources.Resources.save_2
        Me.cmdSaveToFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSaveToFile.Location = New System.Drawing.Point(247, 20)
        Me.cmdSaveToFile.Name = "cmdSaveToFile"
        Me.cmdSaveToFile.Size = New System.Drawing.Size(106, 29)
        Me.cmdSaveToFile.TabIndex = 19
        Me.cmdSaveToFile.Text = "Save to file"
        Me.cmdSaveToFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdSaveToFile, "Save as default on all users")
        Me.cmdSaveToFile.UseVisualStyleBackColor = True
        '
        'cmdDeleteChannel
        '
        Me.cmdDeleteChannel.Enabled = False
        Me.cmdDeleteChannel.FlatAppearance.BorderSize = 0
        Me.cmdDeleteChannel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDeleteChannel.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdDeleteChannel.Location = New System.Drawing.Point(219, 24)
        Me.cmdDeleteChannel.Name = "cmdDeleteChannel"
        Me.cmdDeleteChannel.Size = New System.Drawing.Size(22, 20)
        Me.cmdDeleteChannel.TabIndex = 16
        Me.ToolTip.SetToolTip(Me.cmdDeleteChannel, "Delete channel")
        Me.cmdDeleteChannel.UseVisualStyleBackColor = True
        '
        'cmdAddChannel
        '
        Me.cmdAddChannel.Enabled = False
        Me.cmdAddChannel.FlatAppearance.BorderSize = 0
        Me.cmdAddChannel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddChannel.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.cmdAddChannel.Location = New System.Drawing.Point(191, 24)
        Me.cmdAddChannel.Name = "cmdAddChannel"
        Me.cmdAddChannel.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddChannel.TabIndex = 15
        Me.ToolTip.SetToolTip(Me.cmdAddChannel, "Add channel")
        Me.cmdAddChannel.UseVisualStyleBackColor = True
        '
        'grbBookingType
        '
        Me.grbBookingType.Controls.Add(Me.cmdAddBT)
        Me.grbBookingType.Controls.Add(Me.cmdDeleteBT)
        Me.grbBookingType.Controls.Add(Me.grdBT)
        Me.grbBookingType.Location = New System.Drawing.Point(12, 177)
        Me.grbBookingType.Name = "grbBookingType"
        Me.grbBookingType.Size = New System.Drawing.Size(534, 240)
        Me.grbBookingType.TabIndex = 20
        Me.grbBookingType.TabStop = False
        Me.grbBookingType.Text = "Booking Types"
        '
        'typeSpeedTimer
        '
        Me.typeSpeedTimer.Interval = 1500
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Location = New System.Drawing.Point(369, 339)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(90, 32)
        Me.cmdCancel.TabIndex = 24
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'picLogo
        '
        Me.picLogo.Location = New System.Drawing.Point(21, 15)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(32, 30)
        Me.picLogo.TabIndex = 22
        Me.picLogo.TabStop = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Short name"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 85
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Factor"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 45
        '
        'frmDefineChannels
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(558, 475)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSaveUser)
        Me.Controls.Add(Me.picLogo)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.grbBookingType)
        Me.Controls.Add(Me.cmdSaveToFile)
        Me.Controls.Add(Me.cmdDeleteChannel)
        Me.Controls.Add(Me.cmdAddChannel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmbChannel)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MaximumSize = New System.Drawing.Size(574, 514)
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(574, 514)
        Me.Name = "frmDefineChannels"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Define channels"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        CType(Me.grdBT,System.ComponentModel.ISupportInitialize).EndInit
        Me.grbBookingType.ResumeLayout(false)
        CType(Me.picLogo,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents cmbChannel As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCommission As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAdedge As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtListNr As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtShortName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDelivery As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtVHS As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents grdBT As System.Windows.Forms.DataGridView
    Friend WithEvents cmdDeleteBT As System.Windows.Forms.Button
    Friend WithEvents cmdAddBT As System.Windows.Forms.Button
    Friend WithEvents cmdAddChannel As System.Windows.Forms.Button
    Friend WithEvents cmdDeleteChannel As System.Windows.Forms.Button
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents cmdSaveToFile As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtConnected As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtMarathon As System.Windows.Forms.TextBox
    Friend WithEvents lblMarathon As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbUniverse As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents grbBookingType As System.Windows.Forms.GroupBox
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents picLogo As System.Windows.Forms.PictureBox
    Friend WithEvents txtMatrix As System.Windows.Forms.TextBox
    Friend WithEvents lblMatrix As System.Windows.Forms.Label
    Friend WithEvents chkUseBreakB As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseBillB As System.Windows.Forms.CheckBox
    Friend WithEvents txtAdtooxID As System.Windows.Forms.TextBox
    Friend WithEvents lblAdTooxId As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnGetAdtooxMediaID As System.Windows.Forms.Button
    Friend WithEvents chkBid As System.Windows.Forms.CheckBox
    Friend WithEvents cmdSaveUser As System.Windows.Forms.Button
    Friend WithEvents typeSpeedTimer As System.Windows.Forms.Timer
    Friend WithEvents txtChannelGroup As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents colName As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colShortName As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEFactor As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIsRBS As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colIsSpec As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colPremium As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colComp As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colSpons As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colDaypart As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colBookingcode As Windows.Forms.DataGridViewCheckBoxColumn
End Class
