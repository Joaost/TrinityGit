<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPeriod
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPeriod))
        Me.colHidden = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colWeek = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grdWeeks = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdRemove = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdAddWeek = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optAdvanced = New System.Windows.Forms.RadioButton()
        Me.frmAdvanced = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblNormalDates = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.optNormal = New System.Windows.Forms.RadioButton()
        Me.frmNormal = New System.Windows.Forms.GroupBox()
        Me.ExtendedDateTimePicker1 = New TrinityAnalysis.ExtendedDateTimePicker()
        Me.ExtendedDateTimePicker2 = New TrinityAnalysis.ExtendedDateTimePicker()
        Me.ExtendedDateTimePicker3 = New TrinityAnalysis.ExtendedDateTimePicker()
        Me.ExtendedDateTimePicker4 = New TrinityAnalysis.ExtendedDateTimePicker()
        CType(Me.grdWeeks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.frmAdvanced.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.frmNormal.SuspendLayout()
        Me.SuspendLayout()
        '
        'colHidden
        '
        Me.colHidden.HeaderText = "Hidden"
        Me.colHidden.Name = "colHidden"
        Me.colHidden.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colHidden.Visible = False
        '
        'colWeek
        '
        Me.colWeek.HeaderText = "Week"
        Me.colWeek.Name = "colWeek"
        Me.colWeek.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'grdWeeks
        '
        Me.grdWeeks.AllowUserToAddRows = False
        Me.grdWeeks.AllowUserToDeleteRows = False
        Me.grdWeeks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdWeeks.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colWeek, Me.colHidden})
        Me.grdWeeks.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdWeeks.Location = New System.Drawing.Point(6, 19)
        Me.grdWeeks.Name = "grdWeeks"
        Me.grdWeeks.RowHeadersVisible = False
        Me.grdWeeks.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdWeeks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdWeeks.Size = New System.Drawing.Size(225, 172)
        Me.grdWeeks.TabIndex = 0
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Week"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'cmdRemove
        '
        Me.cmdRemove.Image = CType(resources.GetObject("cmdRemove.Image"), System.Drawing.Image)
        Me.cmdRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdRemove.Location = New System.Drawing.Point(161, 197)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(70, 24)
        Me.cmdRemove.TabIndex = 7
        Me.cmdRemove.Text = "Remove"
        Me.cmdRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdRemove, "Remove Week")
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdAdd.Location = New System.Drawing.Point(198, 70)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(52, 24)
        Me.cmdAdd.TabIndex = 6
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdAdd, "Add Week")
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'cmdAddWeek
        '
        Me.cmdAddWeek.Location = New System.Drawing.Point(256, 42)
        Me.cmdAddWeek.Name = "cmdAddWeek"
        Me.cmdAddWeek.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddWeek.TabIndex = 7
        Me.ToolTip.SetToolTip(Me.cmdAddWeek, "Move dates one week forward")
        Me.cmdAddWeek.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCancel.Location = New System.Drawing.Point(470, 292)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(69, 29)
        Me.cmdCancel.TabIndex = 18
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOk
        '
        Me.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdOk.Location = New System.Drawing.Point(395, 292)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(69, 29)
        Me.cmdOk.TabIndex = 17
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Hidden"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdRemove)
        Me.GroupBox1.Controls.Add(Me.grdWeeks)
        Me.GroupBox1.Location = New System.Drawing.Point(302, 59)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(237, 227)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Weeks"
        '
        'optAdvanced
        '
        Me.optAdvanced.AutoSize = True
        Me.optAdvanced.Location = New System.Drawing.Point(21, 120)
        Me.optAdvanced.Name = "optAdvanced"
        Me.optAdvanced.Size = New System.Drawing.Size(106, 17)
        Me.optAdvanced.TabIndex = 15
        Me.optAdvanced.TabStop = True
        Me.optAdvanced.Text = "Advanced period"
        Me.optAdvanced.UseVisualStyleBackColor = True
        '
        'frmAdvanced
        '
        Me.frmAdvanced.Controls.Add(Me.ExtendedDateTimePicker4)
        Me.frmAdvanced.Controls.Add(Me.ExtendedDateTimePicker3)
        Me.frmAdvanced.Controls.Add(Me.cmdAddWeek)
        Me.frmAdvanced.Controls.Add(Me.cmdAdd)
        Me.frmAdvanced.Controls.Add(Me.Label2)
        Me.frmAdvanced.Controls.Add(Me.txtTitle)
        Me.frmAdvanced.Controls.Add(Me.Label1)
        Me.frmAdvanced.Enabled = False
        Me.frmAdvanced.Location = New System.Drawing.Point(12, 121)
        Me.frmAdvanced.Name = "frmAdvanced"
        Me.frmAdvanced.Size = New System.Drawing.Size(284, 104)
        Me.frmAdvanced.TabIndex = 13
        Me.frmAdvanced.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Dates:"
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(50, 17)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(200, 20)
        Me.txtTitle.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Title"
        '
        'lblNormalDates
        '
        Me.lblNormalDates.AutoSize = True
        Me.lblNormalDates.Location = New System.Drawing.Point(6, 22)
        Me.lblNormalDates.Name = "lblNormalDates"
        Me.lblNormalDates.Size = New System.Drawing.Size(38, 13)
        Me.lblNormalDates.TabIndex = 1
        Me.lblNormalDates.Text = "Dates:"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 11
        Me.PictureBox1.TabStop = False
        '
        'optNormal
        '
        Me.optNormal.AutoSize = True
        Me.optNormal.Location = New System.Drawing.Point(21, 59)
        Me.optNormal.Name = "optNormal"
        Me.optNormal.Size = New System.Drawing.Size(90, 17)
        Me.optNormal.TabIndex = 14
        Me.optNormal.TabStop = True
        Me.optNormal.Text = "Normal period"
        Me.optNormal.UseVisualStyleBackColor = True
        '
        'frmNormal
        '
        Me.frmNormal.Controls.Add(Me.ExtendedDateTimePicker2)
        Me.frmNormal.Controls.Add(Me.ExtendedDateTimePicker1)
        Me.frmNormal.Controls.Add(Me.lblNormalDates)
        Me.frmNormal.Location = New System.Drawing.Point(12, 61)
        Me.frmNormal.Name = "frmNormal"
        Me.frmNormal.Size = New System.Drawing.Size(284, 54)
        Me.frmNormal.TabIndex = 12
        Me.frmNormal.TabStop = False
        '
        'ExtendedDateTimePicker1
        '
        Me.ExtendedDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ExtendedDateTimePicker1.Location = New System.Drawing.Point(50, 19)
        Me.ExtendedDateTimePicker1.Name = "ExtendedDateTimePicker1"
        Me.ExtendedDateTimePicker1.ShowWeekNumbers = True
        Me.ExtendedDateTimePicker1.Size = New System.Drawing.Size(96, 20)
        Me.ExtendedDateTimePicker1.TabIndex = 19
        Me.ExtendedDateTimePicker1.Value = New Date(2012, 6, 4, 13, 31, 2, 0)
        '
        'ExtendedDateTimePicker2
        '
        Me.ExtendedDateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ExtendedDateTimePicker2.Location = New System.Drawing.Point(154, 19)
        Me.ExtendedDateTimePicker2.Name = "ExtendedDateTimePicker2"
        Me.ExtendedDateTimePicker2.ShowWeekNumbers = True
        Me.ExtendedDateTimePicker2.Size = New System.Drawing.Size(96, 20)
        Me.ExtendedDateTimePicker2.TabIndex = 20
        Me.ExtendedDateTimePicker2.Value = New Date(2012, 6, 4, 13, 31, 2, 0)
        '
        'ExtendedDateTimePicker3
        '
        Me.ExtendedDateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ExtendedDateTimePicker3.Location = New System.Drawing.Point(50, 44)
        Me.ExtendedDateTimePicker3.Name = "ExtendedDateTimePicker3"
        Me.ExtendedDateTimePicker3.ShowWeekNumbers = True
        Me.ExtendedDateTimePicker3.Size = New System.Drawing.Size(96, 20)
        Me.ExtendedDateTimePicker3.TabIndex = 20
        Me.ExtendedDateTimePicker3.Value = New Date(2012, 6, 4, 13, 31, 2, 0)
        '
        'ExtendedDateTimePicker4
        '
        Me.ExtendedDateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ExtendedDateTimePicker4.Location = New System.Drawing.Point(154, 44)
        Me.ExtendedDateTimePicker4.Name = "ExtendedDateTimePicker4"
        Me.ExtendedDateTimePicker4.ShowWeekNumbers = True
        Me.ExtendedDateTimePicker4.Size = New System.Drawing.Size(96, 20)
        Me.ExtendedDateTimePicker4.TabIndex = 21
        Me.ExtendedDateTimePicker4.Value = New Date(2012, 6, 4, 13, 31, 2, 0)
        '
        'frmPeriod
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(545, 338)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.optAdvanced)
        Me.Controls.Add(Me.frmAdvanced)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.optNormal)
        Me.Controls.Add(Me.frmNormal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmPeriod"
        Me.Text = "Set Period"
        CType(Me.grdWeeks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.frmAdvanced.ResumeLayout(False)
        Me.frmAdvanced.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.frmNormal.ResumeLayout(False)
        Me.frmNormal.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents colHidden As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colWeek As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdWeeks As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optAdvanced As System.Windows.Forms.RadioButton
    Friend WithEvents frmAdvanced As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblNormalDates As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents optNormal As System.Windows.Forms.RadioButton
    Friend WithEvents frmNormal As System.Windows.Forms.GroupBox
    Friend WithEvents cmdAddWeek As System.Windows.Forms.Button
    Friend WithEvents ExtendedDateTimePicker4 As TrinityAnalysis.ExtendedDateTimePicker
    Friend WithEvents ExtendedDateTimePicker3 As TrinityAnalysis.ExtendedDateTimePicker
    Friend WithEvents ExtendedDateTimePicker2 As TrinityAnalysis.ExtendedDateTimePicker
    Friend WithEvents ExtendedDateTimePicker1 As TrinityAnalysis.ExtendedDateTimePicker
End Class
