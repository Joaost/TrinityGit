<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsPicker
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OptionsPicker))
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.rdbSkip = New System.Windows.Forms.RadioButton()
        Me.rdbRename = New System.Windows.Forms.RadioButton()
        Me.cmb = New System.Windows.Forms.ComboBox()
        Me.lbl = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK_Button.Location = New System.Drawing.Point(171, 88)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(81, 25)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'rdbSkip
        '
        Me.rdbSkip.AutoSize = True
        Me.rdbSkip.Checked = True
        Me.rdbSkip.Location = New System.Drawing.Point(12, 30)
        Me.rdbSkip.Name = "rdbSkip"
        Me.rdbSkip.Size = New System.Drawing.Size(45, 18)
        Me.rdbSkip.TabIndex = 1
        Me.rdbSkip.TabStop = True
        Me.rdbSkip.Text = "Skip"
        Me.rdbSkip.UseVisualStyleBackColor = True
        '
        'rdbRename
        '
        Me.rdbRename.AutoSize = True
        Me.rdbRename.Location = New System.Drawing.Point(12, 55)
        Me.rdbRename.Name = "rdbRename"
        Me.rdbRename.Size = New System.Drawing.Size(87, 18)
        Me.rdbRename.TabIndex = 2
        Me.rdbRename.Text = "Rename into:"
        Me.rdbRename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdbRename.UseVisualStyleBackColor = True
        '
        'cmb
        '
        Me.cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb.Enabled = False
        Me.cmb.FormattingEnabled = True
        Me.cmb.Location = New System.Drawing.Point(142, 55)
        Me.cmb.Name = "cmb"
        Me.cmb.Size = New System.Drawing.Size(110, 22)
        Me.cmb.TabIndex = 3
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.Location = New System.Drawing.Point(9, 10)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(0, 14)
        Me.lbl.TabIndex = 4
        '
        'OptionsPicker
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(264, 126)
        Me.Controls.Add(Me.lbl)
        Me.Controls.Add(Me.cmb)
        Me.Controls.Add(Me.rdbRename)
        Me.Controls.Add(Me.rdbSkip)
        Me.Controls.Add(Me.OK_Button)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OptionsPicker"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Options"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents rdbSkip As System.Windows.Forms.RadioButton
    Friend WithEvents rdbRename As System.Windows.Forms.RadioButton
    Friend WithEvents cmb As System.Windows.Forms.ComboBox
    Friend WithEvents lbl As System.Windows.Forms.Label

End Class
