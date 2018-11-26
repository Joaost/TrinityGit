<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOtherMediaperiod
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOtherMediaperiod))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdRemove = New System.Windows.Forms.Button()
        Me.grdWeeks = New System.Windows.Forms.DataGridView()
        Me.colWeek = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFrom = New clTrinity.CalendarColumn()
        Me.colTo = New clTrinity.CalendarColumn()
        Me.colHidden = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.frmAdvanced = New System.Windows.Forms.GroupBox()
        Me.cmdAddWeek = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.dtAdvancedTo = New clTrinity.ExtendedDateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtAdvancedFrom = New clTrinity.ExtendedDateTimePicker()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOk = New System.Windows.Forms.Button()
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox1.SuspendLayout
        CType(Me.grdWeeks,System.ComponentModel.ISupportInitialize).BeginInit
        Me.frmAdvanced.SuspendLayout
        Me.SuspendLayout
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.calender_2_32x32
        Me.PictureBox1.InitialImage = Global.clTrinity.My.Resources.Resources.calender_2_32x32
        Me.PictureBox1.Location = New System.Drawing.Point(13, 13)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = false
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdRemove)
        Me.GroupBox1.Controls.Add(Me.grdWeeks)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 161)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(257, 227)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Weeks"
        '
        'cmdRemove
        '
        Me.cmdRemove.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.cmdRemove.FlatAppearance.BorderSize = 0
        Me.cmdRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRemove.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdRemove.Location = New System.Drawing.Point(181, 197)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(70, 24)
        Me.cmdRemove.TabIndex = 7
        Me.cmdRemove.Text = "Remove"
        Me.cmdRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdRemove.UseVisualStyleBackColor = true
        '
        'grdWeeks
        '
        Me.grdWeeks.AllowUserToAddRows = false
        Me.grdWeeks.AllowUserToDeleteRows = false
        Me.grdWeeks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdWeeks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdWeeks.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colWeek, Me.colFrom, Me.colTo, Me.colHidden})
        Me.grdWeeks.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdWeeks.Location = New System.Drawing.Point(6, 19)
        Me.grdWeeks.Name = "grdWeeks"
        Me.grdWeeks.RowHeadersVisible = false
        Me.grdWeeks.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdWeeks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdWeeks.Size = New System.Drawing.Size(245, 172)
        Me.grdWeeks.TabIndex = 0
        '
        'colWeek
        '
        Me.colWeek.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colWeek.HeaderText = "Week"
        Me.colWeek.Name = "colWeek"
        Me.colWeek.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colFrom
        '
        Me.colFrom.HeaderText = "From"
        Me.colFrom.Name = "colFrom"
        Me.colFrom.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colFrom.Width = 80
        '
        'colTo
        '
        Me.colTo.HeaderText = "To"
        Me.colTo.Name = "colTo"
        Me.colTo.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colTo.Width = 80
        '
        'colHidden
        '
        Me.colHidden.HeaderText = "Hidden"
        Me.colHidden.Name = "colHidden"
        Me.colHidden.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colHidden.Visible = false
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
        Me.frmAdvanced.Location = New System.Drawing.Point(13, 51)
        Me.frmAdvanced.Name = "frmAdvanced"
        Me.frmAdvanced.Size = New System.Drawing.Size(256, 104)
        Me.frmAdvanced.TabIndex = 8
        Me.frmAdvanced.TabStop = false
        Me.frmAdvanced.Text = "Add week"
        '
        'cmdAddWeek
        '
        Me.cmdAddWeek.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.cmdAddWeek.FlatAppearance.BorderSize = 0
        Me.cmdAddWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddWeek.Image = Global.clTrinity.My.Resources.Resources.move_to_next_2_16x16
        Me.cmdAddWeek.Location = New System.Drawing.Point(228, 42)
        Me.cmdAddWeek.Name = "cmdAddWeek"
        Me.cmdAddWeek.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddWeek.TabIndex = 7
        Me.cmdAddWeek.UseVisualStyleBackColor = true
        '
        'cmdAdd
        '
        Me.cmdAdd.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.cmdAdd.FlatAppearance.BorderSize = 0
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdAdd.Location = New System.Drawing.Point(170, 69)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(52, 24)
        Me.cmdAdd.TabIndex = 6
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdAdd.UseVisualStyleBackColor = true
        '
        'dtAdvancedTo
        '
        Me.dtAdvancedTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtAdvancedTo.Location = New System.Drawing.Point(139, 43)
        Me.dtAdvancedTo.Name = "dtAdvancedTo"
        Me.dtAdvancedTo.ShowWeekNumbers = true
        Me.dtAdvancedTo.Size = New System.Drawing.Size(83, 22)
        Me.dtAdvancedTo.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(6, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Dates:"
        '
        'dtAdvancedFrom
        '
        Me.dtAdvancedFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtAdvancedFrom.Location = New System.Drawing.Point(50, 43)
        Me.dtAdvancedFrom.Name = "dtAdvancedFrom"
        Me.dtAdvancedFrom.ShowWeekNumbers = true
        Me.dtAdvancedFrom.Size = New System.Drawing.Size(83, 22)
        Me.dtAdvancedFrom.TabIndex = 3
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(50, 17)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(172, 22)
        Me.txtTitle.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(9, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Title"
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCancel.Location = New System.Drawing.Point(198, 394)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(69, 29)
        Me.cmdCancel.TabIndex = 10
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = true
        '
        'cmdOk
        '
        Me.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOk.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdOk.Location = New System.Drawing.Point(123, 394)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(69, 29)
        Me.cmdOk.TabIndex = 9
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'frmOtherMediaperiod
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(278, 435)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.frmAdvanced)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmOtherMediaperiod"
        Me.Text = "Set period"
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox1.ResumeLayout(false)
        CType(Me.grdWeeks,System.ComponentModel.ISupportInitialize).EndInit
        Me.frmAdvanced.ResumeLayout(false)
        Me.frmAdvanced.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents grdWeeks As System.Windows.Forms.DataGridView
    Friend WithEvents frmAdvanced As System.Windows.Forms.GroupBox
    Friend WithEvents cmdAddWeek As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents dtAdvancedTo As clTrinity.ExtendedDateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtAdvancedFrom As clTrinity.ExtendedDateTimePicker
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents colWeek As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFrom As clTrinity.CalendarColumn
    Friend WithEvents colTo As clTrinity.CalendarColumn
    Friend WithEvents colHidden As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
