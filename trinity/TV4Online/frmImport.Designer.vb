<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImport))
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.grpBudget = New System.Windows.Forms.GroupBox()
        Me.lblConfirmationBudget = New System.Windows.Forms.Label()
        Me.lblCurrentBudget = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.optReplaceBudget = New System.Windows.Forms.RadioButton()
        Me.optAddBudget = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DateTimePickerEnd = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerFrom = New System.Windows.Forms.DateTimePicker()
        Me.chkReplace = New System.Windows.Forms.CheckBox()
        Me.txtIndex = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbBookingType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnIgnore = New System.Windows.Forms.Button()
        Me.lblChannelName = New System.Windows.Forms.LinkLabel()
        Me.grpBudget.SuspendLayout
        Me.GroupBox1.SuspendLayout
        Me.SuspendLayout
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Location = New System.Drawing.Point(106, 347)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 31)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = true
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Location = New System.Drawing.Point(192, 347)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(75, 31)
        Me.cmdOk.TabIndex = 4
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'grpBudget
        '
        Me.grpBudget.Controls.Add(Me.lblConfirmationBudget)
        Me.grpBudget.Controls.Add(Me.lblCurrentBudget)
        Me.grpBudget.Controls.Add(Me.Label6)
        Me.grpBudget.Controls.Add(Me.Label5)
        Me.grpBudget.Controls.Add(Me.optReplaceBudget)
        Me.grpBudget.Controls.Add(Me.optAddBudget)
        Me.grpBudget.Location = New System.Drawing.Point(12, 223)
        Me.grpBudget.Name = "grpBudget"
        Me.grpBudget.Size = New System.Drawing.Size(255, 110)
        Me.grpBudget.TabIndex = 7
        Me.grpBudget.TabStop = false
        Me.grpBudget.Text = "Budget"
        '
        'lblConfirmationBudget
        '
        Me.lblConfirmationBudget.AutoSize = true
        Me.lblConfirmationBudget.Location = New System.Drawing.Point(124, 39)
        Me.lblConfirmationBudget.Name = "lblConfirmationBudget"
        Me.lblConfirmationBudget.Size = New System.Drawing.Size(26, 13)
        Me.lblConfirmationBudget.TabIndex = 17
        Me.lblConfirmationBudget.Text = "0 kr"
        '
        'lblCurrentBudget
        '
        Me.lblCurrentBudget.AutoSize = true
        Me.lblCurrentBudget.Location = New System.Drawing.Point(124, 16)
        Me.lblCurrentBudget.Name = "lblCurrentBudget"
        Me.lblCurrentBudget.Size = New System.Drawing.Size(26, 13)
        Me.lblCurrentBudget.TabIndex = 16
        Me.lblCurrentBudget.Text = "0 kr"
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Location = New System.Drawing.Point(6, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(119, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Confirmation budget:"
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(6, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Current budget:"
        '
        'optReplaceBudget
        '
        Me.optReplaceBudget.AutoSize = true
        Me.optReplaceBudget.Checked = true
        Me.optReplaceBudget.Location = New System.Drawing.Point(9, 62)
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
        Me.optAddBudget.Location = New System.Drawing.Point(9, 86)
        Me.optAddBudget.Name = "optAddBudget"
        Me.optAddBudget.Size = New System.Drawing.Size(141, 17)
        Me.optAddBudget.TabIndex = 12
        Me.optAddBudget.Text = "Add budget to current"
        Me.optAddBudget.UseVisualStyleBackColor = true
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DateTimePickerEnd)
        Me.GroupBox1.Controls.Add(Me.DateTimePickerFrom)
        Me.GroupBox1.Controls.Add(Me.chkReplace)
        Me.GroupBox1.Controls.Add(Me.txtIndex)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbBookingType)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 48)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(255, 169)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Settings"
        '
        'DateTimePickerEnd
        '
        Me.DateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerEnd.Location = New System.Drawing.Point(143, 75)
        Me.DateTimePickerEnd.Name = "DateTimePickerEnd"
        Me.DateTimePickerEnd.Size = New System.Drawing.Size(106, 22)
        Me.DateTimePickerEnd.TabIndex = 10
        '
        'DateTimePickerFrom
        '
        Me.DateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFrom.Location = New System.Drawing.Point(9, 75)
        Me.DateTimePickerFrom.Name = "DateTimePickerFrom"
        Me.DateTimePickerFrom.Size = New System.Drawing.Size(112, 22)
        Me.DateTimePickerFrom.TabIndex = 9
        '
        'chkReplace
        '
        Me.chkReplace.AutoSize = true
        Me.chkReplace.Checked = true
        Me.chkReplace.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkReplace.Location = New System.Drawing.Point(3, 141)
        Me.chkReplace.Name = "chkReplace"
        Me.chkReplace.Size = New System.Drawing.Size(196, 17)
        Me.chkReplace.TabIndex = 8
        Me.chkReplace.Text = "Replace spots in the same period"
        Me.chkReplace.UseVisualStyleBackColor = true
        '
        'txtIndex
        '
        Me.txtIndex.Location = New System.Drawing.Point(6, 115)
        Me.txtIndex.Name = "txtIndex"
        Me.txtIndex.Size = New System.Drawing.Size(82, 22)
        Me.txtIndex.TabIndex = 7
        Me.txtIndex.Text = "100"
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(6, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Index"
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(127, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(11, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "-"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(6, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Period"
        '
        'cmbBookingType
        '
        Me.cmbBookingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBookingType.FormattingEnabled = true
        Me.cmbBookingType.Location = New System.Drawing.Point(6, 33)
        Me.cmbBookingType.Name = "cmbBookingType"
        Me.cmbBookingType.Size = New System.Drawing.Size(243, 21)
        Me.cmbBookingType.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Bookingtype"
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.Location = New System.Drawing.Point(18, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(109, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Confirmation name:"
        '
        'btnIgnore
        '
        Me.btnIgnore.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnIgnore.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnIgnore.FlatAppearance.BorderSize = 0
        Me.btnIgnore.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnIgnore.Location = New System.Drawing.Point(12, 347)
        Me.btnIgnore.Name = "btnIgnore"
        Me.btnIgnore.Size = New System.Drawing.Size(75, 31)
        Me.btnIgnore.TabIndex = 10
        Me.btnIgnore.Text = "Ignore"
        Me.btnIgnore.UseVisualStyleBackColor = true
        '
        'lblChannelName
        '
        Me.lblChannelName.AutoSize = true
        Me.lblChannelName.Location = New System.Drawing.Point(12, 27)
        Me.lblChannelName.Name = "lblChannelName"
        Me.lblChannelName.Size = New System.Drawing.Size(61, 13)
        Me.lblChannelName.TabIndex = 11
        Me.lblChannelName.TabStop = true
        Me.lblChannelName.Text = "LinkLabel1"
        '
        'frmImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(279, 390)
        Me.Controls.Add(Me.lblChannelName)
        Me.Controls.Add(Me.btnIgnore)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.grpBudget)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MaximumSize = New System.Drawing.Size(295, 429)
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(295, 429)
        Me.Name = "frmImport"
        Me.Text = " "
        Me.grpBudget.ResumeLayout(false)
        Me.grpBudget.PerformLayout
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents cmdCancel As Windows.Forms.Button
    Friend WithEvents cmdOk As Windows.Forms.Button
    Friend WithEvents grpBudget As Windows.Forms.GroupBox
    Friend WithEvents lblConfirmationBudget As Windows.Forms.Label
    Friend WithEvents lblCurrentBudget As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents optReplaceBudget As Windows.Forms.RadioButton
    Friend WithEvents optAddBudget As Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents chkReplace As Windows.Forms.CheckBox
    Friend WithEvents txtIndex As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents cmbBookingType As Windows.Forms.ComboBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents DateTimePickerEnd As Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerFrom As Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents btnIgnore As Windows.Forms.Button
    Friend WithEvents lblChannelName As Windows.Forms.LinkLabel
End Class
