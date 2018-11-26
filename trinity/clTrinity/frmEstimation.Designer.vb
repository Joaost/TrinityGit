<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEstimation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEstimation))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtCustomPeriod = New System.Windows.Forms.TextBox()
        Me.rdbCustom = New System.Windows.Forms.RadioButton()
        Me.rdbLastYear = New System.Windows.Forms.RadioButton()
        Me.rdb4W = New System.Windows.Forms.RadioButton()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1.SuspendLayout
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtCustomPeriod)
        Me.GroupBox1.Controls.Add(Me.rdbCustom)
        Me.GroupBox1.Controls.Add(Me.rdbLastYear)
        Me.GroupBox1.Controls.Add(Me.rdb4W)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 43)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(268, 90)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Estimation Period"
        '
        'txtCustomPeriod
        '
        Me.txtCustomPeriod.Enabled = false
        Me.txtCustomPeriod.Location = New System.Drawing.Point(155, 62)
        Me.txtCustomPeriod.Name = "txtCustomPeriod"
        Me.txtCustomPeriod.Size = New System.Drawing.Size(107, 22)
        Me.txtCustomPeriod.TabIndex = 3
        '
        'rdbCustom
        '
        Me.rdbCustom.AutoSize = true
        Me.rdbCustom.Location = New System.Drawing.Point(21, 65)
        Me.rdbCustom.Name = "rdbCustom"
        Me.rdbCustom.Size = New System.Drawing.Size(101, 17)
        Me.rdbCustom.TabIndex = 2
        Me.rdbCustom.Text = "Custom period"
        Me.rdbCustom.UseVisualStyleBackColor = true
        '
        'rdbLastYear
        '
        Me.rdbLastYear.AutoSize = true
        Me.rdbLastYear.Location = New System.Drawing.Point(21, 42)
        Me.rdbLastYear.Name = "rdbLastYear"
        Me.rdbLastYear.Size = New System.Drawing.Size(134, 17)
        Me.rdbLastYear.TabIndex = 1
        Me.rdbLastYear.Text = "Same period last year"
        Me.rdbLastYear.UseVisualStyleBackColor = true
        '
        'rdb4W
        '
        Me.rdb4W.AutoSize = true
        Me.rdb4W.Checked = true
        Me.rdb4W.Location = New System.Drawing.Point(21, 19)
        Me.rdb4W.Name = "rdb4W"
        Me.rdb4W.Size = New System.Drawing.Size(89, 17)
        Me.rdb4W.TabIndex = 0
        Me.rdb4W.TabStop = true
        Me.rdb4W.Text = "Last 4 weeks"
        Me.rdb4W.UseVisualStyleBackColor = true
        '
        'cmdOK
        '
        Me.cmdOK.FlatAppearance.BorderSize = 0
        Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOK.Location = New System.Drawing.Point(205, 139)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 28)
        Me.cmdOK.TabIndex = 2
        Me.cmdOK.Text = "Estimate"
        Me.cmdOK.UseVisualStyleBackColor = true
        '
        'cmdCancel
        '
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Location = New System.Drawing.Point(124, 139)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 28)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = true
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = true
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.Black
        Me.lblStatus.Location = New System.Drawing.Point(43, 23)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 14)
        Me.lblStatus.TabIndex = 4
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.magic_wand_2_24x24
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = false
        '
        'frmEstimation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 175)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmEstimation"
        Me.Text = "Estimate confirmed spots"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents txtCustomPeriod As System.Windows.Forms.TextBox
    Friend WithEvents rdbCustom As System.Windows.Forms.RadioButton
    Friend WithEvents rdbLastYear As System.Windows.Forms.RadioButton
    Friend WithEvents rdb4W As System.Windows.Forms.RadioButton
    Friend WithEvents lblStatus As System.Windows.Forms.Label
End Class
