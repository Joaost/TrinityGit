<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInStore
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtMaxBookings = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtTo = New Balthazar.ExtendedDateTimePicker
        Me.dtFrom = New Balthazar.ExtendedDateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grdChosenSP = New System.Windows.Forms.DataGridView
        Me.colSalesperson = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colMaxDays = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmdRemoveSalesperson = New System.Windows.Forms.Button
        Me.cmdAddSalesperson = New System.Windows.Forms.Button
        Me.cmdRemove = New System.Windows.Forms.Button
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.lstSalespersons = New System.Windows.Forms.ListBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.cmdWizard = New System.Windows.Forms.Button
        Me.cmdRemoveDate = New System.Windows.Forms.Button
        Me.cmdAddDate = New System.Windows.Forms.Button
        Me.lstExcludedDates = New System.Windows.Forms.ListBox
        Me.calExcluded = New System.Windows.Forms.MonthCalendar
        Me.pnlCalendar = New System.Windows.Forms.Panel
        Me.cmdAddIt = New System.Windows.Forms.Button
        Me.cmdPublish = New System.Windows.Forms.Button
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.cmbDemoInstruction = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmdDeleteProvider = New System.Windows.Forms.Button
        Me.cmdAddProvider = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmdRemoveProvider = New System.Windows.Forms.Button
        Me.cmdChooseProvider = New System.Windows.Forms.Button
        Me.lstChosenProviders = New System.Windows.Forms.ListBox
        Me.lstProviders = New System.Windows.Forms.ListBox
        Me.tabInStore = New System.Windows.Forms.TabControl
        Me.tpSetup = New System.Windows.Forms.TabPage
        Me.cmdDeleteCampaign = New System.Windows.Forms.Button
        Me.tpBookings = New System.Windows.Forms.TabPage
        Me.cmdAddBooking = New System.Windows.Forms.Button
        Me.chkNotInvoiced = New System.Windows.Forms.CheckBox
        Me.chkNotConfirmed = New System.Windows.Forms.CheckBox
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.cmdExcel = New System.Windows.Forms.Button
        Me.lblDates = New System.Windows.Forms.Label
        Me.grdBookings = New System.Windows.Forms.DataGridView
        Me.colDates = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDays = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colSalesman = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colStore = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colCity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colRequestedProvider = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colProvider = New Balthazar.ExtendedComboboxColumn
        Me.colConfirmed = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.mnuQdays = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colInvoiced = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ExtendedComboboxColumn1 = New Balthazar.ExtendedComboboxColumn
        Me.tmrCheckForChanges = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdChosenSP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.pnlCalendar.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.tabInStore.SuspendLayout()
        Me.tpSetup.SuspendLayout()
        Me.tpBookings.SuspendLayout()
        CType(Me.grdBookings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtMaxBookings)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtTo)
        Me.GroupBox1.Controls.Add(Me.dtFrom)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(253, 103)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "General"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(99, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 14)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "(0 = Infinite)"
        '
        'txtMaxBookings
        '
        Me.txtMaxBookings.Location = New System.Drawing.Point(6, 73)
        Me.txtMaxBookings.Name = "txtMaxBookings"
        Me.txtMaxBookings.Size = New System.Drawing.Size(87, 20)
        Me.txtMaxBookings.TabIndex = 5
        Me.txtMaxBookings.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(137, 14)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Maximum bookings per day"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(99, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "-"
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(116, 33)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.ShowWeekNumbers = True
        Me.dtTo.Size = New System.Drawing.Size(87, 20)
        Me.dtTo.TabIndex = 2
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(6, 33)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.ShowWeekNumbers = True
        Me.dtFrom.Size = New System.Drawing.Size(87, 20)
        Me.dtFrom.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Period"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdChosenSP)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.cmdRemoveSalesperson)
        Me.GroupBox2.Controls.Add(Me.cmdAddSalesperson)
        Me.GroupBox2.Controls.Add(Me.cmdRemove)
        Me.GroupBox2.Controls.Add(Me.cmdAdd)
        Me.GroupBox2.Controls.Add(Me.lstSalespersons)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 118)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(443, 276)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Sales persons"
        '
        'grdChosenSP
        '
        Me.grdChosenSP.AllowUserToAddRows = False
        Me.grdChosenSP.AllowUserToDeleteRows = False
        Me.grdChosenSP.AllowUserToResizeColumns = False
        Me.grdChosenSP.AllowUserToResizeRows = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdChosenSP.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdChosenSP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChosenSP.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSalesperson, Me.colMaxDays})
        Me.grdChosenSP.Location = New System.Drawing.Point(239, 33)
        Me.grdChosenSP.Name = "grdChosenSP"
        Me.grdChosenSP.RowHeadersVisible = False
        Me.grdChosenSP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdChosenSP.Size = New System.Drawing.Size(195, 228)
        Me.grdChosenSP.TabIndex = 15
        Me.grdChosenSP.VirtualMode = True
        '
        'colSalesperson
        '
        Me.colSalesperson.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colSalesperson.HeaderText = "Sales person"
        Me.colSalesperson.Name = "colSalesperson"
        '
        'colMaxDays
        '
        Me.colMaxDays.HeaderText = "Max days"
        Me.colMaxDays.Name = "colMaxDays"
        Me.colMaxDays.Width = 60
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(236, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 14)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Chosen"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 14)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Available"
        '
        'cmdRemoveSalesperson
        '
        Me.cmdRemoveSalesperson.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveSalesperson.Location = New System.Drawing.Point(209, 63)
        Me.cmdRemoveSalesperson.Name = "cmdRemoveSalesperson"
        Me.cmdRemoveSalesperson.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveSalesperson.TabIndex = 12
        Me.cmdRemoveSalesperson.UseVisualStyleBackColor = True
        '
        'cmdAddSalesperson
        '
        Me.cmdAddSalesperson.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddSalesperson.Location = New System.Drawing.Point(209, 33)
        Me.cmdAddSalesperson.Name = "cmdAddSalesperson"
        Me.cmdAddSalesperson.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddSalesperson.TabIndex = 11
        Me.cmdAddSalesperson.UseVisualStyleBackColor = True
        '
        'cmdRemove
        '
        Me.cmdRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdRemove.Image = Global.Balthazar.My.Resources.Resources.navigate_left1
        Me.cmdRemove.Location = New System.Drawing.Point(209, 142)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemove.TabIndex = 8
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.Image = Global.Balthazar.My.Resources.Resources.navigate_right
        Me.cmdAdd.Location = New System.Drawing.Point(209, 109)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(24, 24)
        Me.cmdAdd.TabIndex = 7
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'lstSalespersons
        '
        Me.lstSalespersons.FormattingEnabled = True
        Me.lstSalespersons.ItemHeight = 14
        Me.lstSalespersons.Location = New System.Drawing.Point(9, 33)
        Me.lstSalespersons.Name = "lstSalespersons"
        Me.lstSalespersons.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.lstSalespersons.Size = New System.Drawing.Size(194, 228)
        Me.lstSalespersons.Sorted = True
        Me.lstSalespersons.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmdWizard)
        Me.GroupBox3.Controls.Add(Me.cmdRemoveDate)
        Me.GroupBox3.Controls.Add(Me.cmdAddDate)
        Me.GroupBox3.Controls.Add(Me.lstExcludedDates)
        Me.GroupBox3.Location = New System.Drawing.Point(455, 118)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(242, 213)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Excluded dates"
        '
        'cmdWizard
        '
        Me.cmdWizard.Image = Global.Balthazar.My.Resources.Resources.flash
        Me.cmdWizard.Location = New System.Drawing.Point(209, 79)
        Me.cmdWizard.Name = "cmdWizard"
        Me.cmdWizard.Size = New System.Drawing.Size(24, 24)
        Me.cmdWizard.TabIndex = 15
        Me.cmdWizard.UseVisualStyleBackColor = True
        '
        'cmdRemoveDate
        '
        Me.cmdRemoveDate.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveDate.Location = New System.Drawing.Point(209, 49)
        Me.cmdRemoveDate.Name = "cmdRemoveDate"
        Me.cmdRemoveDate.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveDate.TabIndex = 14
        Me.cmdRemoveDate.UseVisualStyleBackColor = True
        '
        'cmdAddDate
        '
        Me.cmdAddDate.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddDate.Location = New System.Drawing.Point(209, 19)
        Me.cmdAddDate.Name = "cmdAddDate"
        Me.cmdAddDate.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddDate.TabIndex = 13
        Me.cmdAddDate.UseVisualStyleBackColor = True
        '
        'lstExcludedDates
        '
        Me.lstExcludedDates.FormattingEnabled = True
        Me.lstExcludedDates.ItemHeight = 14
        Me.lstExcludedDates.Location = New System.Drawing.Point(9, 19)
        Me.lstExcludedDates.Name = "lstExcludedDates"
        Me.lstExcludedDates.Size = New System.Drawing.Size(194, 186)
        Me.lstExcludedDates.Sorted = True
        Me.lstExcludedDates.TabIndex = 3
        '
        'calExcluded
        '
        Me.calExcluded.Location = New System.Drawing.Point(-1, -1)
        Me.calExcluded.Name = "calExcluded"
        Me.calExcluded.ShowWeekNumbers = True
        Me.calExcluded.TabIndex = 3
        '
        'pnlCalendar
        '
        Me.pnlCalendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCalendar.Controls.Add(Me.cmdAddIt)
        Me.pnlCalendar.Controls.Add(Me.calExcluded)
        Me.pnlCalendar.Location = New System.Drawing.Point(514, 160)
        Me.pnlCalendar.Name = "pnlCalendar"
        Me.pnlCalendar.Size = New System.Drawing.Size(171, 200)
        Me.pnlCalendar.TabIndex = 4
        Me.pnlCalendar.Visible = False
        '
        'cmdAddIt
        '
        Me.cmdAddIt.Location = New System.Drawing.Point(113, 169)
        Me.cmdAddIt.Name = "cmdAddIt"
        Me.cmdAddIt.Size = New System.Drawing.Size(53, 23)
        Me.cmdAddIt.TabIndex = 4
        Me.cmdAddIt.Text = "Add"
        Me.cmdAddIt.UseVisualStyleBackColor = True
        '
        'cmdPublish
        '
        Me.cmdPublish.Image = Global.Balthazar.My.Resources.Resources.environment
        Me.cmdPublish.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPublish.Location = New System.Drawing.Point(12, 641)
        Me.cmdPublish.Name = "cmdPublish"
        Me.cmdPublish.Size = New System.Drawing.Size(70, 28)
        Me.cmdPublish.TabIndex = 5
        Me.cmdPublish.Text = "Publish"
        Me.cmdPublish.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdPublish.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cmbDemoInstruction)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.cmdDeleteProvider)
        Me.GroupBox4.Controls.Add(Me.cmdAddProvider)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.cmdRemoveProvider)
        Me.GroupBox4.Controls.Add(Me.cmdChooseProvider)
        Me.GroupBox4.Controls.Add(Me.lstChosenProviders)
        Me.GroupBox4.Controls.Add(Me.lstProviders)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 400)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(443, 235)
        Me.GroupBox4.TabIndex = 6
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Providers"
        '
        'cmbDemoInstruction
        '
        Me.cmbDemoInstruction.DisplayMember = "Name"
        Me.cmbDemoInstruction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDemoInstruction.FormattingEnabled = True
        Me.cmbDemoInstruction.Location = New System.Drawing.Point(102, 207)
        Me.cmbDemoInstruction.Name = "cmbDemoInstruction"
        Me.cmbDemoInstruction.Size = New System.Drawing.Size(101, 22)
        Me.cmbDemoInstruction.TabIndex = 20
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 210)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(90, 14)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Demo instruction:"
        '
        'cmdDeleteProvider
        '
        Me.cmdDeleteProvider.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdDeleteProvider.Location = New System.Drawing.Point(209, 63)
        Me.cmdDeleteProvider.Name = "cmdDeleteProvider"
        Me.cmdDeleteProvider.Size = New System.Drawing.Size(24, 24)
        Me.cmdDeleteProvider.TabIndex = 18
        Me.cmdDeleteProvider.UseVisualStyleBackColor = True
        '
        'cmdAddProvider
        '
        Me.cmdAddProvider.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddProvider.Location = New System.Drawing.Point(209, 33)
        Me.cmdAddProvider.Name = "cmdAddProvider"
        Me.cmdAddProvider.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddProvider.TabIndex = 17
        Me.cmdAddProvider.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(237, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 14)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Chosen"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 14)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Available"
        '
        'cmdRemoveProvider
        '
        Me.cmdRemoveProvider.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveProvider.Image = Global.Balthazar.My.Resources.Resources.navigate_left1
        Me.cmdRemoveProvider.Location = New System.Drawing.Point(209, 123)
        Me.cmdRemoveProvider.Name = "cmdRemoveProvider"
        Me.cmdRemoveProvider.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveProvider.TabIndex = 12
        Me.cmdRemoveProvider.UseVisualStyleBackColor = True
        '
        'cmdChooseProvider
        '
        Me.cmdChooseProvider.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdChooseProvider.Image = Global.Balthazar.My.Resources.Resources.navigate_right
        Me.cmdChooseProvider.Location = New System.Drawing.Point(209, 93)
        Me.cmdChooseProvider.Name = "cmdChooseProvider"
        Me.cmdChooseProvider.Size = New System.Drawing.Size(24, 24)
        Me.cmdChooseProvider.TabIndex = 11
        Me.cmdChooseProvider.UseVisualStyleBackColor = True
        '
        'lstChosenProviders
        '
        Me.lstChosenProviders.DisplayMember = "Firstname"
        Me.lstChosenProviders.FormattingEnabled = True
        Me.lstChosenProviders.ItemHeight = 14
        Me.lstChosenProviders.Location = New System.Drawing.Point(240, 33)
        Me.lstChosenProviders.Name = "lstChosenProviders"
        Me.lstChosenProviders.Size = New System.Drawing.Size(194, 172)
        Me.lstChosenProviders.Sorted = True
        Me.lstChosenProviders.TabIndex = 10
        '
        'lstProviders
        '
        Me.lstProviders.DisplayMember = "Firstname"
        Me.lstProviders.FormattingEnabled = True
        Me.lstProviders.ItemHeight = 14
        Me.lstProviders.Location = New System.Drawing.Point(9, 33)
        Me.lstProviders.Name = "lstProviders"
        Me.lstProviders.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.lstProviders.Size = New System.Drawing.Size(194, 172)
        Me.lstProviders.Sorted = True
        Me.lstProviders.TabIndex = 9
        '
        'tabInStore
        '
        Me.tabInStore.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabInStore.Controls.Add(Me.tpSetup)
        Me.tabInStore.Controls.Add(Me.tpBookings)
        Me.tabInStore.Location = New System.Drawing.Point(3, 1)
        Me.tabInStore.Name = "tabInStore"
        Me.tabInStore.SelectedIndex = 0
        Me.tabInStore.Size = New System.Drawing.Size(740, 704)
        Me.tabInStore.TabIndex = 7
        '
        'tpSetup
        '
        Me.tpSetup.AutoScroll = True
        Me.tpSetup.Controls.Add(Me.cmdDeleteCampaign)
        Me.tpSetup.Controls.Add(Me.GroupBox1)
        Me.tpSetup.Controls.Add(Me.cmdPublish)
        Me.tpSetup.Controls.Add(Me.GroupBox4)
        Me.tpSetup.Controls.Add(Me.pnlCalendar)
        Me.tpSetup.Controls.Add(Me.GroupBox2)
        Me.tpSetup.Controls.Add(Me.GroupBox3)
        Me.tpSetup.Location = New System.Drawing.Point(4, 23)
        Me.tpSetup.Name = "tpSetup"
        Me.tpSetup.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSetup.Size = New System.Drawing.Size(732, 677)
        Me.tpSetup.TabIndex = 1
        Me.tpSetup.Text = "Setup"
        Me.tpSetup.UseVisualStyleBackColor = True
        '
        'cmdDeleteCampaign
        '
        Me.cmdDeleteCampaign.Location = New System.Drawing.Point(594, 641)
        Me.cmdDeleteCampaign.Name = "cmdDeleteCampaign"
        Me.cmdDeleteCampaign.Size = New System.Drawing.Size(103, 28)
        Me.cmdDeleteCampaign.TabIndex = 7
        Me.cmdDeleteCampaign.Text = "Delete campaign"
        Me.cmdDeleteCampaign.UseVisualStyleBackColor = True
        '
        'tpBookings
        '
        Me.tpBookings.Controls.Add(Me.cmdAddBooking)
        Me.tpBookings.Controls.Add(Me.chkNotInvoiced)
        Me.tpBookings.Controls.Add(Me.chkNotConfirmed)
        Me.tpBookings.Controls.Add(Me.cmdDelete)
        Me.tpBookings.Controls.Add(Me.cmdExcel)
        Me.tpBookings.Controls.Add(Me.lblDates)
        Me.tpBookings.Controls.Add(Me.grdBookings)
        Me.tpBookings.Location = New System.Drawing.Point(4, 23)
        Me.tpBookings.Name = "tpBookings"
        Me.tpBookings.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBookings.Size = New System.Drawing.Size(732, 677)
        Me.tpBookings.TabIndex = 0
        Me.tpBookings.Text = "Bookings"
        Me.tpBookings.UseVisualStyleBackColor = True
        '
        'cmdAddBooking
        '
        Me.cmdAddBooking.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddBooking.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddBooking.Location = New System.Drawing.Point(702, 30)
        Me.cmdAddBooking.Name = "cmdAddBooking"
        Me.cmdAddBooking.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddBooking.TabIndex = 19
        Me.cmdAddBooking.UseVisualStyleBackColor = True
        '
        'chkNotInvoiced
        '
        Me.chkNotInvoiced.AutoSize = True
        Me.chkNotInvoiced.Location = New System.Drawing.Point(240, 5)
        Me.chkNotInvoiced.Name = "chkNotInvoiced"
        Me.chkNotInvoiced.Size = New System.Drawing.Size(140, 18)
        Me.chkNotInvoiced.TabIndex = 18
        Me.chkNotInvoiced.Text = "Only show not invoiced"
        Me.chkNotInvoiced.UseVisualStyleBackColor = True
        '
        'chkNotConfirmed
        '
        Me.chkNotConfirmed.AutoSize = True
        Me.chkNotConfirmed.Location = New System.Drawing.Point(6, 6)
        Me.chkNotConfirmed.Name = "chkNotConfirmed"
        Me.chkNotConfirmed.Size = New System.Drawing.Size(228, 18)
        Me.chkNotConfirmed.TabIndex = 17
        Me.chkNotConfirmed.Text = "Only show bookings pending confirmation"
        Me.chkNotConfirmed.UseVisualStyleBackColor = True
        '
        'cmdDelete
        '
        Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDelete.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdDelete.Location = New System.Drawing.Point(702, 60)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(24, 24)
        Me.cmdDelete.TabIndex = 16
        Me.cmdDelete.UseVisualStyleBackColor = True
        '
        'cmdExcel
        '
        Me.cmdExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExcel.Image = Global.Balthazar.My.Resources.Resources.excel
        Me.cmdExcel.Location = New System.Drawing.Point(702, 90)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(24, 24)
        Me.cmdExcel.TabIndex = 13
        Me.cmdExcel.UseVisualStyleBackColor = True
        '
        'lblDates
        '
        Me.lblDates.AutoSize = True
        Me.lblDates.BackColor = System.Drawing.Color.White
        Me.lblDates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDates.Location = New System.Drawing.Point(16, 35)
        Me.lblDates.Name = "lblDates"
        Me.lblDates.Size = New System.Drawing.Size(41, 16)
        Me.lblDates.TabIndex = 1
        Me.lblDates.Text = "Label4"
        Me.lblDates.Visible = False
        '
        'grdBookings
        '
        Me.grdBookings.AllowUserToAddRows = False
        Me.grdBookings.AllowUserToDeleteRows = False
        Me.grdBookings.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdBookings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDates, Me.colDays, Me.colSalesman, Me.colStore, Me.colCity, Me.colRequestedProvider, Me.colProvider, Me.colConfirmed, Me.mnuQdays, Me.colInvoiced})
        Me.grdBookings.Location = New System.Drawing.Point(6, 29)
        Me.grdBookings.Name = "grdBookings"
        Me.grdBookings.RowHeadersVisible = False
        Me.grdBookings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdBookings.Size = New System.Drawing.Size(690, 640)
        Me.grdBookings.TabIndex = 0
        Me.grdBookings.VirtualMode = True
        '
        'colDates
        '
        Me.colDates.HeaderText = "Dates"
        Me.colDates.Name = "colDates"
        Me.colDates.ReadOnly = True
        Me.colDates.Width = 120
        '
        'colDays
        '
        Me.colDays.HeaderText = "Days"
        Me.colDays.Name = "colDays"
        Me.colDays.ReadOnly = True
        Me.colDays.Width = 40
        '
        'colSalesman
        '
        Me.colSalesman.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colSalesman.HeaderText = "Sales person"
        Me.colSalesman.MinimumWidth = 50
        Me.colSalesman.Name = "colSalesman"
        Me.colSalesman.ReadOnly = True
        '
        'colStore
        '
        Me.colStore.HeaderText = "Store"
        Me.colStore.Name = "colStore"
        Me.colStore.ReadOnly = True
        '
        'colCity
        '
        Me.colCity.HeaderText = "City"
        Me.colCity.Name = "colCity"
        Me.colCity.ReadOnly = True
        '
        'colRequestedProvider
        '
        Me.colRequestedProvider.HeaderText = "Req.Provider"
        Me.colRequestedProvider.Name = "colRequestedProvider"
        Me.colRequestedProvider.ReadOnly = True
        '
        'colProvider
        '
        Me.colProvider.HeaderText = "Provider"
        Me.colProvider.Name = "colProvider"
        Me.colProvider.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'colConfirmed
        '
        Me.colConfirmed.HeaderText = "Confirmed"
        Me.colConfirmed.Items.AddRange(New Object() {"", "Confirmed", "Rejected"})
        Me.colConfirmed.Name = "colConfirmed"
        Me.colConfirmed.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colConfirmed.Width = 60
        '
        'mnuQdays
        '
        Me.mnuQdays.HeaderText = "Q-days"
        Me.mnuQdays.Name = "mnuQdays"
        Me.mnuQdays.ReadOnly = True
        Me.mnuQdays.Width = 45
        '
        'colInvoiced
        '
        Me.colInvoiced.HeaderText = "Invoiced"
        Me.colInvoiced.Name = "colInvoiced"
        Me.colInvoiced.Width = 55
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Dates"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Sales person"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 60
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.HeaderText = "Store"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "City"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Req.Provider"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "City"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "Req.Provider"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        '
        'ExtendedComboboxColumn1
        '
        Me.ExtendedComboboxColumn1.HeaderText = "Provider"
        Me.ExtendedComboboxColumn1.Name = "ExtendedComboboxColumn1"
        Me.ExtendedComboboxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'tmrCheckForChanges
        '
        Me.tmrCheckForChanges.Enabled = True
        Me.tmrCheckForChanges.Interval = 2000
        '
        'frmInStore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(742, 705)
        Me.Controls.Add(Me.tabInStore)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmInStore"
        Me.Text = "In-store"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grdChosenSP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.pnlCalendar.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.tabInStore.ResumeLayout(False)
        Me.tpSetup.ResumeLayout(False)
        Me.tpBookings.ResumeLayout(False)
        Me.tpBookings.PerformLayout()
        CType(Me.grdBookings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtFrom As Balthazar.ExtendedDateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtTo As Balthazar.ExtendedDateTimePicker
    Friend WithEvents txtMaxBookings As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lstSalespersons As System.Windows.Forms.ListBox
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveSalesperson As System.Windows.Forms.Button
    Friend WithEvents cmdAddSalesperson As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemoveDate As System.Windows.Forms.Button
    Friend WithEvents cmdAddDate As System.Windows.Forms.Button
    Friend WithEvents lstExcludedDates As System.Windows.Forms.ListBox
    Friend WithEvents cmdWizard As System.Windows.Forms.Button
    Friend WithEvents calExcluded As System.Windows.Forms.MonthCalendar
    Friend WithEvents pnlCalendar As System.Windows.Forms.Panel
    Friend WithEvents cmdAddIt As System.Windows.Forms.Button
    Friend WithEvents cmdPublish As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemoveProvider As System.Windows.Forms.Button
    Friend WithEvents cmdChooseProvider As System.Windows.Forms.Button
    Friend WithEvents lstChosenProviders As System.Windows.Forms.ListBox
    Friend WithEvents lstProviders As System.Windows.Forms.ListBox
    Friend WithEvents tabInStore As System.Windows.Forms.TabControl
    Friend WithEvents tpSetup As System.Windows.Forms.TabPage
    Friend WithEvents tpBookings As System.Windows.Forms.TabPage
    Friend WithEvents grdBookings As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblDates As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grdChosenSP As System.Windows.Forms.DataGridView
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents colSalesperson As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMaxDays As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExtendedComboboxColumn1 As Balthazar.ExtendedComboboxColumn
    Friend WithEvents cmdExcel As System.Windows.Forms.Button
    Friend WithEvents cmdDeleteProvider As System.Windows.Forms.Button
    Friend WithEvents cmdAddProvider As System.Windows.Forms.Button
    Friend WithEvents cmbDemoInstruction As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmdDeleteCampaign As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents tmrCheckForChanges As System.Windows.Forms.Timer
    Friend WithEvents chkNotInvoiced As System.Windows.Forms.CheckBox
    Friend WithEvents chkNotConfirmed As System.Windows.Forms.CheckBox
    Friend WithEvents cmdAddBooking As System.Windows.Forms.Button
    Friend WithEvents colDates As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDays As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSalesman As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStore As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRequestedProvider As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProvider As Balthazar.ExtendedComboboxColumn
    Friend WithEvents colConfirmed As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents mnuQdays As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colInvoiced As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
