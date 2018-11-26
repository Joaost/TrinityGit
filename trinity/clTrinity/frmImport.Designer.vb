<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImport))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkEvaluate = New System.Windows.Forms.CheckBox()
        Me.chkReplace = New System.Windows.Forms.CheckBox()
        Me.txtIndex = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbBookingType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.lblPath = New System.Windows.Forms.LinkLabel()
        Me.grpBudget = New System.Windows.Forms.GroupBox()
        Me.lblConfirmationBudget = New System.Windows.Forms.Label()
        Me.lblCurrentBudget = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.optReplaceBudget = New System.Windows.Forms.RadioButton()
        Me.optAddBudget = New System.Windows.Forms.RadioButton()
        Me.lblLabel = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblAmountOfSpots = New System.Windows.Forms.Label()
        Me.dtTo = New clTrinity.ExtendedDateTimePicker()
        Me.dtFrom = New clTrinity.ExtendedDateTimePicker()
        Me.GroupBox1.SuspendLayout
        CType(Me.picLogo,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpBudget.SuspendLayout
        Me.SuspendLayout
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkEvaluate)
        Me.GroupBox1.Controls.Add(Me.chkReplace)
        Me.GroupBox1.Controls.Add(Me.txtIndex)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.dtTo)
        Me.GroupBox1.Controls.Add(Me.dtFrom)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbBookingType)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 61)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(242, 203)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Settings"
        '
        'chkEvaluate
        '
        Me.chkEvaluate.AutoSize = true
        Me.chkEvaluate.Location = New System.Drawing.Point(6, 177)
        Me.chkEvaluate.Name = "chkEvaluate"
        Me.chkEvaluate.Size = New System.Drawing.Size(116, 17)
        Me.chkEvaluate.TabIndex = 9
        Me.chkEvaluate.Text = "Evaluate booking"
        Me.chkEvaluate.UseVisualStyleBackColor = true
        '
        'chkReplace
        '
        Me.chkReplace.AutoSize = true
        Me.chkReplace.Checked = true
        Me.chkReplace.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkReplace.Location = New System.Drawing.Point(6, 155)
        Me.chkReplace.Name = "chkReplace"
        Me.chkReplace.Size = New System.Drawing.Size(196, 17)
        Me.chkReplace.TabIndex = 8
        Me.chkReplace.Text = "Replace spots in the same period"
        Me.chkReplace.UseVisualStyleBackColor = true
        '
        'txtIndex
        '
        Me.txtIndex.Location = New System.Drawing.Point(6, 127)
        Me.txtIndex.Name = "txtIndex"
        Me.txtIndex.Size = New System.Drawing.Size(82, 22)
        Me.txtIndex.TabIndex = 7
        Me.txtIndex.Text = "100"
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(6, 111)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Index"
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(116, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(11, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "-"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(6, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Period"
        '
        'cmbBookingType
        '
        Me.cmbBookingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBookingType.FormattingEnabled = true
        Me.cmbBookingType.Location = New System.Drawing.Point(6, 36)
        Me.cmbBookingType.Name = "cmbBookingType"
        Me.cmbBookingType.Size = New System.Drawing.Size(230, 21)
        Me.cmbBookingType.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(6, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Bookingtype"
        '
        'picLogo
        '
        Me.picLogo.Location = New System.Drawing.Point(12, 11)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(32, 30)
        Me.picLogo.TabIndex = 0
        Me.picLogo.TabStop = false
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Location = New System.Drawing.Point(153, 410)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(94, 29)
        Me.cmdOk.TabIndex = 2
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Location = New System.Drawing.Point(14, 410)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(99, 29)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = true
        '
        'lblPath
        '
        Me.lblPath.AutoSize = true
        Me.lblPath.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblPath.Location = New System.Drawing.Point(11, 45)
        Me.lblPath.Name = "lblPath"
        Me.lblPath.Size = New System.Drawing.Size(0, 13)
        Me.lblPath.TabIndex = 4
        '
        'grpBudget
        '
        Me.grpBudget.Controls.Add(Me.lblAmountOfSpots)
        Me.grpBudget.Controls.Add(Me.Label7)
        Me.grpBudget.Controls.Add(Me.lblConfirmationBudget)
        Me.grpBudget.Controls.Add(Me.lblCurrentBudget)
        Me.grpBudget.Controls.Add(Me.Label6)
        Me.grpBudget.Controls.Add(Me.Label5)
        Me.grpBudget.Controls.Add(Me.optReplaceBudget)
        Me.grpBudget.Controls.Add(Me.optAddBudget)
        Me.grpBudget.Location = New System.Drawing.Point(11, 270)
        Me.grpBudget.Name = "grpBudget"
        Me.grpBudget.Size = New System.Drawing.Size(242, 134)
        Me.grpBudget.TabIndex = 5
        Me.grpBudget.TabStop = false
        Me.grpBudget.Text = "Budget"
        '
        'lblConfirmationBudget
        '
        Me.lblConfirmationBudget.AutoSize = true
        Me.lblConfirmationBudget.Location = New System.Drawing.Point(131, 36)
        Me.lblConfirmationBudget.Name = "lblConfirmationBudget"
        Me.lblConfirmationBudget.Size = New System.Drawing.Size(26, 13)
        Me.lblConfirmationBudget.TabIndex = 17
        Me.lblConfirmationBudget.Text = "0 kr"
        '
        'lblCurrentBudget
        '
        Me.lblCurrentBudget.AutoSize = true
        Me.lblCurrentBudget.Location = New System.Drawing.Point(131, 15)
        Me.lblCurrentBudget.Name = "lblCurrentBudget"
        Me.lblCurrentBudget.Size = New System.Drawing.Size(26, 13)
        Me.lblCurrentBudget.TabIndex = 16
        Me.lblCurrentBudget.Text = "0 kr"
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Location = New System.Drawing.Point(6, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(119, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Confirmation budget:"
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(6, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Current budget:"
        '
        'optReplaceBudget
        '
        Me.optReplaceBudget.AutoSize = true
        Me.optReplaceBudget.Checked = true
        Me.optReplaceBudget.Location = New System.Drawing.Point(9, 58)
        Me.optReplaceBudget.Name = "optReplaceBudget"
        Me.optReplaceBudget.Size = New System.Drawing.Size(146, 17)
        Me.optReplaceBudget.TabIndex = 13
        Me.optReplaceBudget.TabStop = true
        Me.optReplaceBudget.Text = "Replace current budget"
        Me.optReplaceBudget.UseVisualStyleBackColor = true
        '
        'optAddBudget
        '
        Me.optAddBudget.AutoSize = true
        Me.optAddBudget.Location = New System.Drawing.Point(9, 80)
        Me.optAddBudget.Name = "optAddBudget"
        Me.optAddBudget.Size = New System.Drawing.Size(141, 17)
        Me.optAddBudget.TabIndex = 12
        Me.optAddBudget.Text = "Add budget to current"
        Me.optAddBudget.UseVisualStyleBackColor = true
        '
        'lblLabel
        '
        Me.lblLabel.AutoSize = true
        Me.lblLabel.Location = New System.Drawing.Point(50, 19)
        Me.lblLabel.Name = "lblLabel"
        Me.lblLabel.Size = New System.Drawing.Size(0, 13)
        Me.lblLabel.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.Location = New System.Drawing.Point(6, 109)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(96, 13)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Amount of spots:"
        '
        'lblAmountOfSpots
        '
        Me.lblAmountOfSpots.AutoSize = true
        Me.lblAmountOfSpots.Location = New System.Drawing.Point(108, 109)
        Me.lblAmountOfSpots.Name = "lblAmountOfSpots"
        Me.lblAmountOfSpots.Size = New System.Drawing.Size(13, 13)
        Me.lblAmountOfSpots.TabIndex = 18
        Me.lblAmountOfSpots.Text = "0"
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(133, 80)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.ShowWeekNumbers = true
        Me.dtTo.Size = New System.Drawing.Size(103, 22)
        Me.dtTo.TabIndex = 4
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(6, 80)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.ShowWeekNumbers = true
        Me.dtFrom.Size = New System.Drawing.Size(104, 22)
        Me.dtFrom.TabIndex = 3
        '
        'frmImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(265, 450)
        Me.Controls.Add(Me.lblLabel)
        Me.Controls.Add(Me.grpBudget)
        Me.Controls.Add(Me.lblPath)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.picLogo)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmImport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import schedule"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        CType(Me.picLogo,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpBudget.ResumeLayout(false)
        Me.grpBudget.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents picLogo As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtFrom As clTrinity.ExtendedDateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbBookingType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkEvaluate As System.Windows.Forms.CheckBox
    Friend WithEvents chkReplace As System.Windows.Forms.CheckBox
    Friend WithEvents txtIndex As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtTo As clTrinity.ExtendedDateTimePicker
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents lblPath As System.Windows.Forms.LinkLabel
    Friend WithEvents grpBudget As System.Windows.Forms.GroupBox
    Friend WithEvents lblConfirmationBudget As System.Windows.Forms.Label
    Friend WithEvents lblCurrentBudget As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents optReplaceBudget As System.Windows.Forms.RadioButton
    Friend WithEvents optAddBudget As System.Windows.Forms.RadioButton
    Friend WithEvents lblLabel As System.Windows.Forms.Label
    Friend WithEvents lblAmountOfSpots As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
End Class
