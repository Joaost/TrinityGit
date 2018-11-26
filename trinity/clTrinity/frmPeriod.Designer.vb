<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPeriod
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPeriod))
        Me.frmNormal = New System.Windows.Forms.GroupBox()
        Me.dtNormalTo = New clTrinity.ExtendedDateTimePicker()
        Me.lblNormalDates = New System.Windows.Forms.Label()
        Me.dtNormalFrom = New clTrinity.ExtendedDateTimePicker()
        Me.frmAdvanced = New System.Windows.Forms.GroupBox()
        Me.cmdAddWeek = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.dtAdvancedTo = New clTrinity.ExtendedDateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtAdvancedFrom = New clTrinity.ExtendedDateTimePicker()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.optNormal = New System.Windows.Forms.RadioButton()
        Me.optAdvanced = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdRemove = New System.Windows.Forms.Button()
        Me.grdWeeks = New System.Windows.Forms.DataGridView()
        Me.colWeek = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFrom = New clTrinity.CalendarColumn()
        Me.colTo = New clTrinity.CalendarColumn()
        Me.colHidden = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CalendarColumn1 = New clTrinity.CalendarColumn()
        Me.CalendarColumn2 = New clTrinity.CalendarColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblWarning = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.frmNormal.SuspendLayout
        Me.frmAdvanced.SuspendLayout
        Me.GroupBox1.SuspendLayout
        CType(Me.grdWeeks,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'frmNormal
        '
        Me.frmNormal.Controls.Add(Me.dtNormalTo)
        Me.frmNormal.Controls.Add(Me.lblNormalDates)
        Me.frmNormal.Controls.Add(Me.dtNormalFrom)
        Me.frmNormal.Location = New System.Drawing.Point(12, 46)
        Me.frmNormal.Name = "frmNormal"
        Me.frmNormal.Size = New System.Drawing.Size(256, 50)
        Me.frmNormal.TabIndex = 1
        Me.frmNormal.TabStop = false
        '
        'dtNormalTo
        '
        Me.dtNormalTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtNormalTo.Location = New System.Drawing.Point(139, 18)
        Me.dtNormalTo.Name = "dtNormalTo"
        Me.dtNormalTo.ShowWeekNumbers = true
        Me.dtNormalTo.Size = New System.Drawing.Size(83, 22)
        Me.dtNormalTo.TabIndex = 2
        '
        'lblNormalDates
        '
        Me.lblNormalDates.AutoSize = true
        Me.lblNormalDates.Location = New System.Drawing.Point(6, 20)
        Me.lblNormalDates.Name = "lblNormalDates"
        Me.lblNormalDates.Size = New System.Drawing.Size(39, 13)
        Me.lblNormalDates.TabIndex = 1
        Me.lblNormalDates.Text = "Dates:"
        '
        'dtNormalFrom
        '
        Me.dtNormalFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtNormalFrom.Location = New System.Drawing.Point(50, 18)
        Me.dtNormalFrom.Name = "dtNormalFrom"
        Me.dtNormalFrom.ShowWeekNumbers = true
        Me.dtNormalFrom.Size = New System.Drawing.Size(83, 22)
        Me.dtNormalFrom.TabIndex = 0
        '
        'frmAdvanced
        '
        Me.frmAdvanced.Controls.Add(Me.cmdAddWeek)
        Me.frmAdvanced.Controls.Add(Me.cmdAdd)
        Me.frmAdvanced.Controls.Add(Me.dtAdvancedTo)
        Me.frmAdvanced.Controls.Add(Me.Label2)
        Me.frmAdvanced.Controls.Add(Me.dtAdvancedFrom)
        Me.frmAdvanced.Controls.Add(Me.txtTitle)
        Me.frmAdvanced.Controls.Add(Me.Label1)
        Me.frmAdvanced.Enabled = false
        Me.frmAdvanced.Location = New System.Drawing.Point(274, 46)
        Me.frmAdvanced.Name = "frmAdvanced"
        Me.frmAdvanced.Size = New System.Drawing.Size(256, 97)
        Me.frmAdvanced.TabIndex = 3
        Me.frmAdvanced.TabStop = false
        '
        'cmdAddWeek
        '
        Me.cmdAddWeek.FlatAppearance.BorderSize = 0
        Me.cmdAddWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddWeek.Image = Global.clTrinity.My.Resources.Resources.move_to_next_2_16x16
        Me.cmdAddWeek.Location = New System.Drawing.Point(228, 39)
        Me.cmdAddWeek.Name = "cmdAddWeek"
        Me.cmdAddWeek.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddWeek.TabIndex = 7
        Me.ToolTip.SetToolTip(Me.cmdAddWeek, "Move dates one week forward")
        Me.cmdAddWeek.UseVisualStyleBackColor = true
        '
        'cmdAdd
        '
        Me.cmdAdd.FlatAppearance.BorderSize = 0
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdAdd.Location = New System.Drawing.Point(163, 64)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(59, 22)
        Me.cmdAdd.TabIndex = 6
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdAdd, "Add Week")
        Me.cmdAdd.UseVisualStyleBackColor = true
        '
        'dtAdvancedTo
        '
        Me.dtAdvancedTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtAdvancedTo.Location = New System.Drawing.Point(139, 40)
        Me.dtAdvancedTo.Name = "dtAdvancedTo"
        Me.dtAdvancedTo.ShowWeekNumbers = true
        Me.dtAdvancedTo.Size = New System.Drawing.Size(83, 22)
        Me.dtAdvancedTo.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(6, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Dates:"
        '
        'dtAdvancedFrom
        '
        Me.dtAdvancedFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtAdvancedFrom.Location = New System.Drawing.Point(50, 40)
        Me.dtAdvancedFrom.Name = "dtAdvancedFrom"
        Me.dtAdvancedFrom.ShowWeekNumbers = true
        Me.dtAdvancedFrom.Size = New System.Drawing.Size(83, 22)
        Me.dtAdvancedFrom.TabIndex = 3
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(50, 16)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(172, 22)
        Me.txtTitle.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(9, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Title"
        '
        'optNormal
        '
        Me.optNormal.AutoSize = true
        Me.optNormal.Location = New System.Drawing.Point(21, 45)
        Me.optNormal.Name = "optNormal"
        Me.optNormal.Size = New System.Drawing.Size(99, 17)
        Me.optNormal.TabIndex = 4
        Me.optNormal.TabStop = true
        Me.optNormal.Text = "Normal period"
        Me.optNormal.UseVisualStyleBackColor = true
        '
        'optAdvanced
        '
        Me.optAdvanced.AutoSize = true
        Me.optAdvanced.Location = New System.Drawing.Point(283, 45)
        Me.optAdvanced.Name = "optAdvanced"
        Me.optAdvanced.Size = New System.Drawing.Size(112, 17)
        Me.optAdvanced.TabIndex = 5
        Me.optAdvanced.TabStop = true
        Me.optAdvanced.Text = "Advanced period"
        Me.optAdvanced.UseVisualStyleBackColor = true
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdRemove)
        Me.GroupBox1.Controls.Add(Me.grdWeeks)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 149)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(518, 217)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Weeks"
        '
        'cmdRemove
        '
        Me.cmdRemove.FlatAppearance.BorderSize = 0
        Me.cmdRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRemove.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdRemove.Location = New System.Drawing.Point(440, 183)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(72, 22)
        Me.cmdRemove.TabIndex = 7
        Me.cmdRemove.Text = "Remove"
        Me.cmdRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdRemove, "Remove Week")
        Me.cmdRemove.UseVisualStyleBackColor = true
        '
        'grdWeeks
        '
        Me.grdWeeks.AllowUserToAddRows = false
        Me.grdWeeks.AllowUserToDeleteRows = false
        Me.grdWeeks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdWeeks.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colWeek, Me.colFrom, Me.colTo, Me.colHidden})
        Me.grdWeeks.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdWeeks.Location = New System.Drawing.Point(6, 18)
        Me.grdWeeks.Name = "grdWeeks"
        Me.grdWeeks.RowHeadersVisible = false
        Me.grdWeeks.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdWeeks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdWeeks.Size = New System.Drawing.Size(506, 160)
        Me.grdWeeks.TabIndex = 0
        '
        'colWeek
        '
        Me.colWeek.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colWeek.FillWeight = 30!
        Me.colWeek.HeaderText = "Week"
        Me.colWeek.Name = "colWeek"
        Me.colWeek.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colFrom
        '
        Me.colFrom.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colFrom.FillWeight = 30!
        Me.colFrom.HeaderText = "From"
        Me.colFrom.Name = "colFrom"
        Me.colFrom.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colTo
        '
        Me.colTo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colTo.FillWeight = 30!
        Me.colTo.HeaderText = "To"
        Me.colTo.Name = "colTo"
        Me.colTo.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colHidden
        '
        Me.colHidden.HeaderText = "Hidden"
        Me.colHidden.Name = "colHidden"
        Me.colHidden.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colHidden.Visible = false
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Week"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'CalendarColumn1
        '
        Me.CalendarColumn1.HeaderText = "From"
        Me.CalendarColumn1.Name = "CalendarColumn1"
        Me.CalendarColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'CalendarColumn2
        '
        Me.CalendarColumn2.HeaderText = "To"
        Me.CalendarColumn2.Name = "CalendarColumn2"
        Me.CalendarColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Hidden"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Visible = false
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdOk.Location = New System.Drawing.Point(455, 415)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(69, 27)
        Me.cmdOk.TabIndex = 7
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCancel.Location = New System.Drawing.Point(380, 415)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(69, 27)
        Me.cmdCancel.TabIndex = 8
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = true
        '
        'lblWarning
        '
        Me.lblWarning.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblWarning.Location = New System.Drawing.Point(12, 382)
        Me.lblWarning.Name = "lblWarning"
        Me.lblWarning.Size = New System.Drawing.Size(250, 60)
        Me.lblWarning.TabIndex = 9
        Me.lblWarning.Text = "If you are making a new campaign, and using this one as a template - use Campaign"& _ 
    " > Move Campaign rather than simply changing the weeks of this campaign."
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = true
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(12, 369)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Important! "
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.calender_2_32x32
        Me.PictureBox1.Location = New System.Drawing.Point(12, 9)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = false
        '
        'frmPeriod
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(546, 453)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblWarning)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.optAdvanced)
        Me.Controls.Add(Me.optNormal)
        Me.Controls.Add(Me.frmAdvanced)
        Me.Controls.Add(Me.frmNormal)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmPeriod"
        Me.Text = "Set Period"
        Me.frmNormal.ResumeLayout(false)
        Me.frmNormal.PerformLayout
        Me.frmAdvanced.ResumeLayout(false)
        Me.frmAdvanced.PerformLayout
        Me.GroupBox1.ResumeLayout(false)
        CType(Me.grdWeeks,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents frmNormal As System.Windows.Forms.GroupBox
    Friend WithEvents optNormal As System.Windows.Forms.RadioButton
    Friend WithEvents dtNormalTo As ExtendedDateTimePicker
    Friend WithEvents lblNormalDates As System.Windows.Forms.Label
    Friend WithEvents dtNormalFrom As ExtendedDateTimePicker
    Friend WithEvents frmAdvanced As System.Windows.Forms.GroupBox
    Friend WithEvents optAdvanced As System.Windows.Forms.RadioButton
    Friend WithEvents dtAdvancedTo As ExtendedDateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtAdvancedFrom As ExtendedDateTimePicker
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdAddWeek As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grdWeeks As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CalendarColumn1 As clTrinity.CalendarColumn
    Friend WithEvents CalendarColumn2 As clTrinity.CalendarColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents lblWarning As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents colWeek As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFrom As CalendarColumn
    Friend WithEvents colTo As CalendarColumn
    Friend WithEvents colHidden As Windows.Forms.DataGridViewTextBoxColumn
End Class
