<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateCampaign
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCreateCampaign))
        Me.cmdCreate = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.lblSelected = New System.Windows.Forms.Label()
        Me.grpAd = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbProducts = New System.Windows.Forms.ComboBox()
        Me.cmbAdvertisers = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbxChannels = New System.Windows.Forms.ListBox()
        Me.grpChannel = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbxFilmcodes = New System.Windows.Forms.ListBox()
        Me.cmdCountry = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.mnuArea = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.dtEnd = New clTrinity.ExtendedDateTimePicker()
        Me.dtStart = New clTrinity.ExtendedDateTimePicker()
        Me.grpAd.SuspendLayout
        Me.grpChannel.SuspendLayout
        Me.SuspendLayout
        '
        'cmdCreate
        '
        Me.cmdCreate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdCreate.Enabled = false
        Me.cmdCreate.FlatAppearance.BorderSize = 0
        Me.cmdCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCreate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmdCreate.Location = New System.Drawing.Point(192, 461)
        Me.cmdCreate.Name = "cmdCreate"
        Me.cmdCreate.Size = New System.Drawing.Size(75, 23)
        Me.cmdCreate.TabIndex = 0
        Me.cmdCreate.Text = "Create"
        Me.cmdCreate.UseVisualStyleBackColor = true
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmdCancel.Location = New System.Drawing.Point(273, 461)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = true
        '
        'lblSelected
        '
        Me.lblSelected.AutoSize = true
        Me.lblSelected.Location = New System.Drawing.Point(20, 114)
        Me.lblSelected.Name = "lblSelected"
        Me.lblSelected.Size = New System.Drawing.Size(26, 13)
        Me.lblSelected.TabIndex = 3
        Me.lblSelected.Text = "Set "
        '
        'grpAd
        '
        Me.grpAd.Controls.Add(Me.Label4)
        Me.grpAd.Controls.Add(Me.Label3)
        Me.grpAd.Controls.Add(Me.lblSelected)
        Me.grpAd.Controls.Add(Me.Label2)
        Me.grpAd.Controls.Add(Me.cmbProducts)
        Me.grpAd.Controls.Add(Me.cmbAdvertisers)
        Me.grpAd.Enabled = false
        Me.grpAd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grpAd.Location = New System.Drawing.Point(12, 61)
        Me.grpAd.Name = "grpAd"
        Me.grpAd.Size = New System.Drawing.Size(335, 136)
        Me.grpAd.TabIndex = 5
        Me.grpAd.TabStop = false
        Me.grpAd.Text = "Advertiser/Product"
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(342, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Select a Advertiser/Product in the drop down lists or search for it."
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label3.Location = New System.Drawing.Point(173, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 14)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Product"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 14)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Advertiser"
        '
        'cmbProducts
        '
        Me.cmbProducts.DropDownHeight = 200
        Me.cmbProducts.FormattingEnabled = true
        Me.cmbProducts.IntegralHeight = false
        Me.cmbProducts.Location = New System.Drawing.Point(176, 79)
        Me.cmbProducts.Name = "cmbProducts"
        Me.cmbProducts.Size = New System.Drawing.Size(138, 21)
        Me.cmbProducts.TabIndex = 7
        '
        'cmbAdvertisers
        '
        Me.cmbAdvertisers.DropDownHeight = 200
        Me.cmbAdvertisers.FormattingEnabled = true
        Me.cmbAdvertisers.IntegralHeight = false
        Me.cmbAdvertisers.Location = New System.Drawing.Point(20, 79)
        Me.cmbAdvertisers.Name = "cmbAdvertisers"
        Me.cmbAdvertisers.Size = New System.Drawing.Size(136, 21)
        Me.cmbAdvertisers.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(209, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "End Date"
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label5.Location = New System.Drawing.Point(95, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Start Date"
        '
        'lbxChannels
        '
        Me.lbxChannels.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbxChannels.FormattingEnabled = true
        Me.lbxChannels.Location = New System.Drawing.Point(23, 66)
        Me.lbxChannels.Name = "lbxChannels"
        Me.lbxChannels.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbxChannels.Size = New System.Drawing.Size(115, 173)
        Me.lbxChannels.Sorted = true
        Me.lbxChannels.TabIndex = 11
        '
        'grpChannel
        '
        Me.grpChannel.Controls.Add(Me.Label8)
        Me.grpChannel.Controls.Add(Me.Label7)
        Me.grpChannel.Controls.Add(Me.Label6)
        Me.grpChannel.Controls.Add(Me.lbxFilmcodes)
        Me.grpChannel.Controls.Add(Me.lbxChannels)
        Me.grpChannel.Enabled = false
        Me.grpChannel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grpChannel.Location = New System.Drawing.Point(12, 202)
        Me.grpChannel.Name = "grpChannel"
        Me.grpChannel.Size = New System.Drawing.Size(335, 250)
        Me.grpChannel.TabIndex = 12
        Me.grpChannel.TabStop = false
        Me.grpChannel.Text = "Channels and Films"
        '
        'Label8
        '
        Me.Label8.AutoSize = true
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label8.Location = New System.Drawing.Point(191, 50)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(58, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Filmcodes"
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label7.Location = New System.Drawing.Point(17, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Channels"
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(291, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Select the Channels and Filmcodes you want to include"
        '
        'lbxFilmcodes
        '
        Me.lbxFilmcodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbxFilmcodes.FormattingEnabled = true
        Me.lbxFilmcodes.Location = New System.Drawing.Point(194, 66)
        Me.lbxFilmcodes.Name = "lbxFilmcodes"
        Me.lbxFilmcodes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbxFilmcodes.Size = New System.Drawing.Size(120, 173)
        Me.lbxFilmcodes.TabIndex = 12
        '
        'cmdCountry
        '
        Me.cmdCountry.FlatAppearance.BorderSize = 0
        Me.cmdCountry.Image = CType(resources.GetObject("cmdCountry.Image"),System.Drawing.Image)
        Me.cmdCountry.Location = New System.Drawing.Point(12, 12)
        Me.cmdCountry.Name = "cmdCountry"
        Me.cmdCountry.Size = New System.Drawing.Size(43, 43)
        Me.cmdCountry.TabIndex = 13
        Me.cmdCountry.Tag = "SEMMS"
        Me.cmdCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdCountry.UseVisualStyleBackColor = true
        '
        'mnuArea
        '
        Me.mnuArea.Name = "mnuArea"
        Me.mnuArea.Size = New System.Drawing.Size(61, 4)
        '
        'dtEnd
        '
        Me.dtEnd.CalendarFont = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.dtEnd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEnd.Location = New System.Drawing.Point(212, 32)
        Me.dtEnd.Name = "dtEnd"
        Me.dtEnd.ShowWeekNumbers = false
        Me.dtEnd.Size = New System.Drawing.Size(87, 22)
        Me.dtEnd.TabIndex = 7
        '
        'dtStart
        '
        Me.dtStart.CalendarFont = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.dtStart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtStart.Location = New System.Drawing.Point(96, 32)
        Me.dtStart.Name = "dtStart"
        Me.dtStart.ShowWeekNumbers = false
        Me.dtStart.Size = New System.Drawing.Size(85, 22)
        Me.dtStart.TabIndex = 6
        '
        'frmCreateCampaign
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(360, 497)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dtEnd)
        Me.Controls.Add(Me.grpAd)
        Me.Controls.Add(Me.cmdCountry)
        Me.Controls.Add(Me.dtStart)
        Me.Controls.Add(Me.grpChannel)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdCreate)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmCreateCampaign"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Create Campaign"
        Me.TopMost = true
        Me.grpAd.ResumeLayout(false)
        Me.grpAd.PerformLayout
        Me.grpChannel.ResumeLayout(false)
        Me.grpChannel.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents cmdCreate As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents lblSelected As System.Windows.Forms.Label
    Friend WithEvents grpAd As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbProducts As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAdvertisers As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtStart As clTrinity.ExtendedDateTimePicker
    Friend WithEvents dtEnd As clTrinity.ExtendedDateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lbxChannels As System.Windows.Forms.ListBox
    Friend WithEvents grpChannel As System.Windows.Forms.GroupBox
    Friend WithEvents lbxFilmcodes As System.Windows.Forms.ListBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdCountry As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents mnuArea As System.Windows.Forms.ContextMenuStrip
End Class
