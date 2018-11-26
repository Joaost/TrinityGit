<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmXMLChannel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmXMLChannel))
        Me.cmdDeleteChannel = New System.Windows.Forms.Button
        Me.cmdAddChannel = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbChannel = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtLogo = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtBuyingUni = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtMarathon = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtConnected = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtVHS = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtDelivery = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtCommission = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtAdedge = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtListNr = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtShortName = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.grbBookingType = New System.Windows.Forms.GroupBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.cmdAddBT = New System.Windows.Forms.Button
        Me.cmdDeleteBT = New System.Windows.Forms.Button
        Me.grdBT = New System.Windows.Forms.DataGridView
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colShortName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colIsRBS = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.colIsSpec = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.colDaypart = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.colBookingcode = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Label15 = New System.Windows.Forms.Label
        Me.cmbFile = New System.Windows.Forms.ComboBox
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.chkAddIfNotFound = New System.Windows.Forms.CheckBox
        Me.cmdOK = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.lblDELETE = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.grbBookingType.SuspendLayout()
        CType(Me.grdBT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdDeleteChannel
        '
        Me.cmdDeleteChannel.Image = CType(resources.GetObject("cmdDeleteChannel.Image"), System.Drawing.Image)
        Me.cmdDeleteChannel.Location = New System.Drawing.Point(442, 22)
        Me.cmdDeleteChannel.Name = "cmdDeleteChannel"
        Me.cmdDeleteChannel.Size = New System.Drawing.Size(22, 22)
        Me.cmdDeleteChannel.TabIndex = 26
        Me.cmdDeleteChannel.UseVisualStyleBackColor = True
        '
        'cmdAddChannel
        '
        Me.cmdAddChannel.Image = CType(resources.GetObject("cmdAddChannel.Image"), System.Drawing.Image)
        Me.cmdAddChannel.Location = New System.Drawing.Point(414, 22)
        Me.cmdAddChannel.Name = "cmdAddChannel"
        Me.cmdAddChannel.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddChannel.TabIndex = 25
        Me.cmdAddChannel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(273, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Channel"
        '
        'cmbChannel
        '
        Me.cmbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChannel.FormattingEnabled = True
        Me.cmbChannel.Location = New System.Drawing.Point(273, 22)
        Me.cmbChannel.Name = "cmbChannel"
        Me.cmbChannel.Size = New System.Drawing.Size(135, 21)
        Me.cmbChannel.TabIndex = 23
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblDELETE)
        Me.GroupBox1.Controls.Add(Me.txtLogo)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtBuyingUni)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtMarathon)
        Me.GroupBox1.Controls.Add(Me.Label11)
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
        Me.GroupBox1.Location = New System.Drawing.Point(12, 56)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(450, 379)
        Me.GroupBox1.TabIndex = 28
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Info"
        '
        'txtLogo
        '
        Me.txtLogo.Location = New System.Drawing.Point(6, 112)
        Me.txtLogo.Name = "txtLogo"
        Me.txtLogo.Size = New System.Drawing.Size(195, 20)
        Me.txtLogo.TabIndex = 23
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 96)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(56, 13)
        Me.Label16.TabIndex = 22
        Me.Label16.Text = "Logo Path"
        '
        'txtBuyingUni
        '
        Me.txtBuyingUni.Location = New System.Drawing.Point(261, 113)
        Me.txtBuyingUni.Name = "txtBuyingUni"
        Me.txtBuyingUni.Size = New System.Drawing.Size(87, 20)
        Me.txtBuyingUni.TabIndex = 21
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(261, 96)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(82, 13)
        Me.Label12.TabIndex = 20
        Me.Label12.Text = "Buying universe"
        '
        'txtMarathon
        '
        Me.txtMarathon.Location = New System.Drawing.Point(108, 73)
        Me.txtMarathon.Name = "txtMarathon"
        Me.txtMarathon.Size = New System.Drawing.Size(93, 20)
        Me.txtMarathon.TabIndex = 18
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(108, 56)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(93, 13)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Marathon channel"
        '
        'txtConnected
        '
        Me.txtConnected.Location = New System.Drawing.Point(261, 73)
        Me.txtConnected.Name = "txtConnected"
        Me.txtConnected.Size = New System.Drawing.Size(87, 20)
        Me.txtConnected.TabIndex = 16
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(261, 56)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 13)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "Connected to"
        '
        'txtVHS
        '
        Me.txtVHS.Location = New System.Drawing.Point(6, 271)
        Me.txtVHS.Multiline = True
        Me.txtVHS.Name = "txtVHS"
        Me.txtVHS.Size = New System.Drawing.Size(438, 98)
        Me.txtVHS.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(7, 254)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "VHS Address"
        '
        'txtDelivery
        '
        Me.txtDelivery.Location = New System.Drawing.Point(6, 153)
        Me.txtDelivery.Multiline = True
        Me.txtDelivery.Name = "txtDelivery"
        Me.txtDelivery.Size = New System.Drawing.Size(438, 98)
        Me.txtDelivery.TabIndex = 12
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 136)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(86, 13)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Delivery Address"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(417, 76)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(15, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "%"
        '
        'txtCommission
        '
        Me.txtCommission.Location = New System.Drawing.Point(357, 73)
        Me.txtCommission.Name = "txtCommission"
        Me.txtCommission.Size = New System.Drawing.Size(54, 20)
        Me.txtCommission.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(357, 56)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Commission"
        '
        'txtAdedge
        '
        Me.txtAdedge.Location = New System.Drawing.Point(6, 73)
        Me.txtAdedge.Name = "txtAdedge"
        Me.txtAdedge.Size = New System.Drawing.Size(93, 20)
        Me.txtAdedge.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Adedge channels"
        '
        'txtListNr
        '
        Me.txtListNr.Location = New System.Drawing.Point(357, 33)
        Me.txtListNr.Name = "txtListNr"
        Me.txtListNr.Size = New System.Drawing.Size(77, 20)
        Me.txtListNr.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(357, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "List number"
        '
        'txtShortName
        '
        Me.txtShortName.Location = New System.Drawing.Point(261, 33)
        Me.txtShortName.Name = "txtShortName"
        Me.txtShortName.Size = New System.Drawing.Size(87, 20)
        Me.txtShortName.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(261, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Short name"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(6, 33)
        Me.txtName.Name = "txtName"
        Me.txtName.ReadOnly = True
        Me.txtName.Size = New System.Drawing.Size(195, 20)
        Me.txtName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Channel name"
        '
        'grbBookingType
        '
        Me.grbBookingType.Controls.Add(Me.Label14)
        Me.grbBookingType.Controls.Add(Me.Label13)
        Me.grbBookingType.Controls.Add(Me.cmdAddBT)
        Me.grbBookingType.Controls.Add(Me.cmdDeleteBT)
        Me.grbBookingType.Controls.Add(Me.grdBT)
        Me.grbBookingType.Location = New System.Drawing.Point(12, 442)
        Me.grbBookingType.Name = "grbBookingType"
        Me.grbBookingType.Size = New System.Drawing.Size(450, 149)
        Me.grbBookingType.TabIndex = 29
        Me.grbBookingType.TabStop = False
        Me.grbBookingType.Text = "Booking Types"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label14.Location = New System.Drawing.Point(191, 10)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(73, 13)
        Me.Label14.TabIndex = 16
        Me.Label14.Text = "Booking Type"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label13.Location = New System.Drawing.Point(312, 10)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(81, 13)
        Me.Label13.TabIndex = 15
        Me.Label13.Text = "Printing Options"
        '
        'cmdAddBT
        '
        Me.cmdAddBT.Image = CType(resources.GetObject("cmdAddBT.Image"), System.Drawing.Image)
        Me.cmdAddBT.Location = New System.Drawing.Point(422, 24)
        Me.cmdAddBT.Name = "cmdAddBT"
        Me.cmdAddBT.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddBT.TabIndex = 13
        Me.cmdAddBT.UseVisualStyleBackColor = True
        '
        'cmdDeleteBT
        '
        Me.cmdDeleteBT.Image = CType(resources.GetObject("cmdDeleteBT.Image"), System.Drawing.Image)
        Me.cmdDeleteBT.Location = New System.Drawing.Point(422, 52)
        Me.cmdDeleteBT.Name = "cmdDeleteBT"
        Me.cmdDeleteBT.Size = New System.Drawing.Size(22, 22)
        Me.cmdDeleteBT.TabIndex = 14
        Me.cmdDeleteBT.UseVisualStyleBackColor = True
        '
        'grdBT
        '
        Me.grdBT.AllowUserToAddRows = False
        Me.grdBT.AllowUserToDeleteRows = False
        Me.grdBT.AllowUserToResizeColumns = False
        Me.grdBT.AllowUserToResizeRows = False
        Me.grdBT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdBT.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colShortName, Me.colIsRBS, Me.colIsSpec, Me.colDaypart, Me.colBookingcode})
        Me.grdBT.Location = New System.Drawing.Point(6, 24)
        Me.grdBT.Name = "grdBT"
        Me.grdBT.RowHeadersVisible = False
        Me.grdBT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdBT.Size = New System.Drawing.Size(410, 119)
        Me.grdBT.TabIndex = 3
        '
        'colName
        '
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        Me.colName.Width = 82
        '
        'colShortName
        '
        Me.colShortName.HeaderText = "Short name"
        Me.colShortName.Name = "colShortName"
        Me.colShortName.Width = 81
        '
        'colIsRBS
        '
        Me.colIsRBS.HeaderText = "RBS"
        Me.colIsRBS.Name = "colIsRBS"
        Me.colIsRBS.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colIsRBS.Width = 60
        '
        'colIsSpec
        '
        Me.colIsSpec.HeaderText = "Specific"
        Me.colIsSpec.Name = "colIsSpec"
        Me.colIsSpec.Width = 60
        '
        'colDaypart
        '
        Me.colDaypart.HeaderText = "Daypart"
        Me.colDaypart.Name = "colDaypart"
        Me.colDaypart.Width = 60
        '
        'colBookingcode
        '
        Me.colBookingcode.HeaderText = "B. Code"
        Me.colBookingcode.Name = "colBookingcode"
        Me.colBookingcode.Width = 60
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(19, 5)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(26, 13)
        Me.Label15.TabIndex = 31
        Me.Label15.Text = "File:"
        '
        'cmbFile
        '
        Me.cmbFile.FormattingEnabled = True
        Me.cmbFile.Location = New System.Drawing.Point(18, 22)
        Me.cmbFile.Name = "cmbFile"
        Me.cmbFile.Size = New System.Drawing.Size(211, 21)
        Me.cmbFile.TabIndex = 30
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(12, 597)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(162, 23)
        Me.cmdSave.TabIndex = 32
        Me.cmdSave.Text = "Save changes on channel"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(387, 597)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 33
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'chkAddIfNotFound
        '
        Me.chkAddIfNotFound.AutoSize = True
        Me.chkAddIfNotFound.Checked = True
        Me.chkAddIfNotFound.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAddIfNotFound.Location = New System.Drawing.Point(180, 601)
        Me.chkAddIfNotFound.Name = "chkAddIfNotFound"
        Me.chkAddIfNotFound.Size = New System.Drawing.Size(98, 17)
        Me.chkAddIfNotFound.TabIndex = 34
        Me.chkAddIfNotFound.Text = "AddIfNotFound"
        Me.chkAddIfNotFound.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(306, 597)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 35
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 82
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Short name"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 81
        '
        'lblDELETE
        '
        Me.lblDELETE.AutoSize = True
        Me.lblDELETE.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lblDELETE.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDELETE.ForeColor = System.Drawing.Color.Red
        Me.lblDELETE.Location = New System.Drawing.Point(49, 56)
        Me.lblDELETE.Name = "lblDELETE"
        Me.lblDELETE.Size = New System.Drawing.Size(347, 42)
        Me.lblDELETE.TabIndex = 36
        Me.lblDELETE.Text = "WILL BE DELETED"
        Me.lblDELETE.Visible = False
        '
        'frmXMLChannel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(475, 629)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.chkAddIfNotFound)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.cmbFile)
        Me.Controls.Add(Me.grbBookingType)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdDeleteChannel)
        Me.Controls.Add(Me.cmdAddChannel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbChannel)
        Me.Name = "frmXMLChannel"
        Me.Text = "Edit Channel"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grbBookingType.ResumeLayout(False)
        Me.grbBookingType.PerformLayout()
        CType(Me.grdBT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdDeleteChannel As System.Windows.Forms.Button
    Friend WithEvents cmdAddChannel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbChannel As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtMarathon As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtConnected As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtVHS As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtDelivery As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCommission As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAdedge As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtListNr As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtShortName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grbBookingType As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmdAddBT As System.Windows.Forms.Button
    Friend WithEvents cmdDeleteBT As System.Windows.Forms.Button
    Friend WithEvents grdBT As System.Windows.Forms.DataGridView
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmbFile As System.Windows.Forms.ComboBox
    Friend WithEvents txtBuyingUni As System.Windows.Forms.TextBox
    Friend WithEvents txtLogo As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents chkAddIfNotFound As System.Windows.Forms.CheckBox
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colShortName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIsRBS As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colIsSpec As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colDaypart As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colBookingcode As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents lblDELETE As System.Windows.Forms.Label
End Class
