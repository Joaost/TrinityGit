<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGetSchedule
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGetSchedule))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdGetSchedule = New System.Windows.Forms.Button()
        Me.grdScheduleList = New System.Windows.Forms.DataGridView()
        Me.colBDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChannel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coLProgram = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colGrossPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEstTrp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnImportSchedule = New System.Windows.Forms.Button()
        Me.prgBar1 = New System.Windows.Forms.ProgressBar()
        Me.lblProgressText = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblAmountOfScheduleSpots = New System.Windows.Forms.Label()
        Me.cmbChannelPick = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtPickerTo = New TV4Online.ExtendedDateTimePicker()
        Me.dtPickerFrom = New TV4Online.ExtendedDateTimePicker()
        CType(Me.grdScheduleList,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(839, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(10, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "From"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(151, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(18, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "To"
        '
        'cmdGetSchedule
        '
        Me.cmdGetSchedule.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.cmdGetSchedule.FlatAppearance.BorderSize = 0
        Me.cmdGetSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdGetSchedule.Location = New System.Drawing.Point(12, 573)
        Me.cmdGetSchedule.Name = "cmdGetSchedule"
        Me.cmdGetSchedule.Size = New System.Drawing.Size(117, 38)
        Me.cmdGetSchedule.TabIndex = 5
        Me.cmdGetSchedule.Text = "Read schedules"
        Me.cmdGetSchedule.UseVisualStyleBackColor = true
        '
        'grdScheduleList
        '
        Me.grdScheduleList.AllowUserToAddRows = false
        Me.grdScheduleList.AllowUserToDeleteRows = false
        Me.grdScheduleList.AllowUserToResizeColumns = false
        Me.grdScheduleList.AllowUserToResizeRows = false
        Me.grdScheduleList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdScheduleList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdScheduleList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colBDate, Me.colChannel, Me.colTime, Me.coLProgram, Me.colGrossPrice, Me.colEstTrp})
        Me.grdScheduleList.Location = New System.Drawing.Point(0, 26)
        Me.grdScheduleList.Name = "grdScheduleList"
        Me.grdScheduleList.RowHeadersVisible = false
        Me.grdScheduleList.Size = New System.Drawing.Size(839, 514)
        Me.grdScheduleList.TabIndex = 6
        Me.grdScheduleList.VirtualMode = true
        '
        'colBDate
        '
        Me.colBDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colBDate.HeaderText = "Date"
        Me.colBDate.Name = "colBDate"
        Me.colBDate.ReadOnly = true
        '
        'colChannel
        '
        Me.colChannel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colChannel.HeaderText = "Channel"
        Me.colChannel.Name = "colChannel"
        Me.colChannel.ReadOnly = true
        '
        'colTime
        '
        Me.colTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colTime.HeaderText = "Time"
        Me.colTime.Name = "colTime"
        Me.colTime.ReadOnly = true
        '
        'coLProgram
        '
        Me.coLProgram.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.coLProgram.FillWeight = 250!
        Me.coLProgram.HeaderText = "Program"
        Me.coLProgram.Name = "coLProgram"
        Me.coLProgram.ReadOnly = true
        '
        'colGrossPrice
        '
        Me.colGrossPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colGrossPrice.HeaderText = "Gross price"
        Me.colGrossPrice.Name = "colGrossPrice"
        Me.colGrossPrice.ReadOnly = true
        '
        'colEstTrp
        '
        Me.colEstTrp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colEstTrp.HeaderText = "Est. Trp"
        Me.colEstTrp.Name = "colEstTrp"
        Me.colEstTrp.ReadOnly = true
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Date"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = true
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.HeaderText = "DP"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = true
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.HeaderText = "Time"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = true
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn4.FillWeight = 250!
        Me.DataGridViewTextBoxColumn4.HeaderText = "Program"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = true
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn5.HeaderText = "Gross price"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = true
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.HeaderText = "Est. Trp"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = true
        '
        'btnImportSchedule
        '
        Me.btnImportSchedule.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnImportSchedule.Enabled = false
        Me.btnImportSchedule.FlatAppearance.BorderSize = 0
        Me.btnImportSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImportSchedule.Location = New System.Drawing.Point(710, 573)
        Me.btnImportSchedule.Name = "btnImportSchedule"
        Me.btnImportSchedule.Size = New System.Drawing.Size(117, 38)
        Me.btnImportSchedule.TabIndex = 9
        Me.btnImportSchedule.Text = "Import schedule"
        Me.btnImportSchedule.UseVisualStyleBackColor = true
        '
        'prgBar1
        '
        Me.prgBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.prgBar1.Location = New System.Drawing.Point(0, 541)
        Me.prgBar1.Name = "prgBar1"
        Me.prgBar1.Size = New System.Drawing.Size(839, 24)
        Me.prgBar1.TabIndex = 10
        Me.prgBar1.Visible = false
        '
        'lblProgressText
        '
        Me.lblProgressText.AutoSize = true
        Me.lblProgressText.Location = New System.Drawing.Point(362, 586)
        Me.lblProgressText.Name = "lblProgressText"
        Me.lblProgressText.Size = New System.Drawing.Size(71, 13)
        Me.lblProgressText.TabIndex = 11
        Me.lblProgressText.Text = "progressText"
        Me.lblProgressText.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblProgressText.Visible = false
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(484, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Amount of breaks:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAmountOfScheduleSpots
        '
        Me.lblAmountOfScheduleSpots.AutoSize = true
        Me.lblAmountOfScheduleSpots.Location = New System.Drawing.Point(583, 6)
        Me.lblAmountOfScheduleSpots.Name = "lblAmountOfScheduleSpots"
        Me.lblAmountOfScheduleSpots.Size = New System.Drawing.Size(13, 13)
        Me.lblAmountOfScheduleSpots.TabIndex = 13
        Me.lblAmountOfScheduleSpots.Text = "0"
        Me.lblAmountOfScheduleSpots.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmbChannelPick
        '
        Me.cmbChannelPick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChannelPick.Location = New System.Drawing.Point(332, 1)
        Me.cmbChannelPick.Name = "cmbChannelPick"
        Me.cmbChannelPick.Size = New System.Drawing.Size(121, 21)
        Me.cmbChannelPick.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(280, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Channel"
        '
        'dtPickerTo
        '
        Me.dtPickerTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtPickerTo.Location = New System.Drawing.Point(177, 2)
        Me.dtPickerTo.Name = "dtPickerTo"
        Me.dtPickerTo.ShowWeekNumbers = true
        Me.dtPickerTo.Size = New System.Drawing.Size(83, 22)
        Me.dtPickerTo.TabIndex = 8
        '
        'dtPickerFrom
        '
        Me.dtPickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtPickerFrom.Location = New System.Drawing.Point(46, 2)
        Me.dtPickerFrom.Name = "dtPickerFrom"
        Me.dtPickerFrom.ShowWeekNumbers = true
        Me.dtPickerFrom.Size = New System.Drawing.Size(83, 22)
        Me.dtPickerFrom.TabIndex = 7
        '
        'frmGetSchedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(839, 623)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbChannelPick)
        Me.Controls.Add(Me.lblAmountOfScheduleSpots)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblProgressText)
        Me.Controls.Add(Me.prgBar1)
        Me.Controls.Add(Me.btnImportSchedule)
        Me.Controls.Add(Me.dtPickerTo)
        Me.Controls.Add(Me.dtPickerFrom)
        Me.Controls.Add(Me.grdScheduleList)
        Me.Controls.Add(Me.cmdGetSchedule)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmGetSchedule"
        Me.Text = "Get schedule"
        CType(Me.grdScheduleList,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents cmdGetSchedule As Windows.Forms.Button
    Friend WithEvents grdScheduleList As Windows.Forms.DataGridView
    Friend WithEvents dtPickerFrom As TV4Online.ExtendedDateTimePicker
    Friend WithEvents dtPickerTo As ExtendedDateTimePicker
    Friend WithEvents DataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnImportSchedule As Windows.Forms.Button
    Friend WithEvents prgBar1 As Windows.Forms.ProgressBar
    Friend WithEvents lblProgressText As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents lblAmountOfScheduleSpots As Windows.Forms.Label
    Friend WithEvents cmbChannelPick As Windows.Forms.ComboBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents colBDate As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChannel As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTime As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coLProgram As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colGrossPrice As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEstTrp As Windows.Forms.DataGridViewTextBoxColumn
End Class
