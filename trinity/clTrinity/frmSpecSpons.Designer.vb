<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSpecSpons
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSpecSpons))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdFind = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtTo = New clTrinity.ExtendedDateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtFrom = New clTrinity.ExtendedDateTimePicker()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmdPick = New System.Windows.Forms.Button()
        Me.cmdUnPick = New System.Windows.Forms.Button()
        Me.lstChosen = New System.Windows.Forms.ListBox()
        Me.lstAvailable = New System.Windows.Forms.ListBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkGetTRP = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout
        Me.GroupBox2.SuspendLayout
        Me.GroupBox3.SuspendLayout
        Me.SuspendLayout
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdFind)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtTo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtFrom)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(270, 66)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Find program titles"
        '
        'cmdFind
        '
        Me.cmdFind.FlatAppearance.BorderSize = 0
        Me.cmdFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFind.Location = New System.Drawing.Point(185, 32)
        Me.cmdFind.Name = "cmdFind"
        Me.cmdFind.Size = New System.Drawing.Size(75, 23)
        Me.cmdFind.TabIndex = 4
        Me.cmdFind.Text = "Find"
        Me.cmdFind.UseVisualStyleBackColor = true
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(97, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(18, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To"
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(97, 33)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.ShowWeekNumbers = true
        Me.dtTo.Size = New System.Drawing.Size(82, 22)
        Me.dtTo.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(9, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "From"
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(9, 33)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.ShowWeekNumbers = true
        Me.dtFrom.Size = New System.Drawing.Size(82, 22)
        Me.dtFrom.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdPick)
        Me.GroupBox2.Controls.Add(Me.cmdUnPick)
        Me.GroupBox2.Controls.Add(Me.lstChosen)
        Me.GroupBox2.Controls.Add(Me.lstAvailable)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 84)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(364, 262)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = false
        Me.GroupBox2.Text = "Programs"
        '
        'cmdPick
        '
        Me.cmdPick.FlatAppearance.BorderSize = 0
        Me.cmdPick.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPick.Image = Global.clTrinity.My.Resources.Resources.arrow_right_3_16x16
        Me.cmdPick.Location = New System.Drawing.Point(168, 104)
        Me.cmdPick.Name = "cmdPick"
        Me.cmdPick.Size = New System.Drawing.Size(24, 24)
        Me.cmdPick.TabIndex = 3
        Me.cmdPick.UseVisualStyleBackColor = true
        '
        'cmdUnPick
        '
        Me.cmdUnPick.FlatAppearance.BorderSize = 0
        Me.cmdUnPick.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUnPick.Image = Global.clTrinity.My.Resources.Resources.arrow_left_3_16x16
        Me.cmdUnPick.Location = New System.Drawing.Point(168, 134)
        Me.cmdUnPick.Name = "cmdUnPick"
        Me.cmdUnPick.Size = New System.Drawing.Size(24, 24)
        Me.cmdUnPick.TabIndex = 2
        Me.cmdUnPick.UseVisualStyleBackColor = true
        '
        'lstChosen
        '
        Me.lstChosen.FormattingEnabled = true
        Me.lstChosen.Location = New System.Drawing.Point(198, 19)
        Me.lstChosen.Name = "lstChosen"
        Me.lstChosen.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstChosen.Size = New System.Drawing.Size(153, 225)
        Me.lstChosen.TabIndex = 1
        '
        'lstAvailable
        '
        Me.lstAvailable.FormattingEnabled = true
        Me.lstAvailable.Location = New System.Drawing.Point(9, 19)
        Me.lstAvailable.Name = "lstAvailable"
        Me.lstAvailable.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstAvailable.Size = New System.Drawing.Size(153, 225)
        Me.lstAvailable.TabIndex = 0
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Location = New System.Drawing.Point(288, 451)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(75, 28)
        Me.cmdOk.TabIndex = 4
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Location = New System.Drawing.Point(207, 451)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 28)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = true
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbPeriod)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.chkGetTRP)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 353)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(364, 92)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = false
        Me.GroupBox3.Text = "TRPs"
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = true
        Me.cmbPeriod.Items.AddRange(New Object() {"Last weeks of data", "Same period last year", "Program title period"})
        Me.cmbPeriod.Location = New System.Drawing.Point(9, 57)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(183, 21)
        Me.cmbPeriod.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(9, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Reference period"
        '
        'chkGetTRP
        '
        Me.chkGetTRP.AutoSize = true
        Me.chkGetTRP.Location = New System.Drawing.Point(9, 19)
        Me.chkGetTRP.Name = "chkGetTRP"
        Me.chkGetTRP.Size = New System.Drawing.Size(185, 17)
        Me.chkGetTRP.TabIndex = 6
        Me.chkGetTRP.Text = "Get TRPs and add to Campaign"
        Me.chkGetTRP.UseVisualStyleBackColor = true
        '
        'frmSpecSpons
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(384, 491)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmSpecSpons"
        Me.Text = "Specific Sponsring"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtFrom As clTrinity.ExtendedDateTimePicker
    Friend WithEvents cmdFind As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtTo As clTrinity.ExtendedDateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdUnPick As System.Windows.Forms.Button
    Friend WithEvents lstChosen As System.Windows.Forms.ListBox
    Friend WithEvents lstAvailable As System.Windows.Forms.ListBox
    Friend WithEvents cmdPick As System.Windows.Forms.Button
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkGetTRP As System.Windows.Forms.CheckBox
End Class
