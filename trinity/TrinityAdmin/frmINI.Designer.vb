<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmINI
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
        Me.cmbFile = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbSection = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbKey = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtValue = New System.Windows.Forms.TextBox
        Me.cmdSetValue = New System.Windows.Forms.Button
        Me.cmdAddSection = New System.Windows.Forms.Button
        Me.cmdAddKey = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdDeleteKey = New System.Windows.Forms.Button
        Me.cmdDeleteSection = New System.Windows.Forms.Button
        Me.chkFrom = New System.Windows.Forms.CheckBox
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbFile
        '
        Me.cmbFile.FormattingEnabled = True
        Me.cmbFile.Location = New System.Drawing.Point(64, 19)
        Me.cmbFile.Name = "cmbFile"
        Me.cmbFile.Size = New System.Drawing.Size(245, 21)
        Me.cmbFile.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "File:"
        '
        'cmbSection
        '
        Me.cmbSection.FormattingEnabled = True
        Me.cmbSection.Location = New System.Drawing.Point(64, 54)
        Me.cmbSection.Name = "cmbSection"
        Me.cmbSection.Size = New System.Drawing.Size(217, 21)
        Me.cmbSection.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Section:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Key:"
        '
        'cmbKey
        '
        Me.cmbKey.FormattingEnabled = True
        Me.cmbKey.Location = New System.Drawing.Point(64, 89)
        Me.cmbKey.Name = "cmbKey"
        Me.cmbKey.Size = New System.Drawing.Size(217, 21)
        Me.cmbKey.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 131)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Value:"
        '
        'txtValue
        '
        Me.txtValue.Location = New System.Drawing.Point(64, 126)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(245, 20)
        Me.txtValue.TabIndex = 7
        '
        'cmdSetValue
        '
        Me.cmdSetValue.Location = New System.Drawing.Point(234, 175)
        Me.cmdSetValue.Name = "cmdSetValue"
        Me.cmdSetValue.Size = New System.Drawing.Size(75, 23)
        Me.cmdSetValue.TabIndex = 8
        Me.cmdSetValue.Text = "Set Value"
        Me.ToolTip1.SetToolTip(Me.cmdSetValue, "Set the value on the local file")
        Me.cmdSetValue.UseVisualStyleBackColor = True
        '
        'cmdAddSection
        '
        Me.cmdAddSection.Location = New System.Drawing.Point(285, 53)
        Me.cmdAddSection.Name = "cmdAddSection"
        Me.cmdAddSection.Size = New System.Drawing.Size(24, 23)
        Me.cmdAddSection.TabIndex = 9
        Me.cmdAddSection.Text = "Button1"
        Me.ToolTip1.SetToolTip(Me.cmdAddSection, "Add new Section")
        Me.cmdAddSection.UseVisualStyleBackColor = True
        '
        'cmdAddKey
        '
        Me.cmdAddKey.Location = New System.Drawing.Point(285, 88)
        Me.cmdAddKey.Name = "cmdAddKey"
        Me.cmdAddKey.Size = New System.Drawing.Size(24, 23)
        Me.cmdAddKey.TabIndex = 10
        Me.cmdAddKey.Text = "Button2"
        Me.ToolTip1.SetToolTip(Me.cmdAddKey, "Add new Key")
        Me.cmdAddKey.UseVisualStyleBackColor = True
        '
        'cmdDeleteKey
        '
        Me.cmdDeleteKey.Location = New System.Drawing.Point(149, 175)
        Me.cmdDeleteKey.Name = "cmdDeleteKey"
        Me.cmdDeleteKey.Size = New System.Drawing.Size(75, 23)
        Me.cmdDeleteKey.TabIndex = 11
        Me.cmdDeleteKey.Text = "Delete Key"
        Me.cmdDeleteKey.UseVisualStyleBackColor = True
        '
        'cmdDeleteSection
        '
        Me.cmdDeleteSection.Location = New System.Drawing.Point(15, 175)
        Me.cmdDeleteSection.Name = "cmdDeleteSection"
        Me.cmdDeleteSection.Size = New System.Drawing.Size(124, 23)
        Me.cmdDeleteSection.TabIndex = 12
        Me.cmdDeleteSection.Text = "Delete Section"
        Me.cmdDeleteSection.UseVisualStyleBackColor = True
        '
        'chkFrom
        '
        Me.chkFrom.AutoSize = True
        Me.chkFrom.Enabled = False
        Me.chkFrom.Location = New System.Drawing.Point(64, 152)
        Me.chkFrom.Name = "chkFrom"
        Me.chkFrom.Size = New System.Drawing.Size(97, 17)
        Me.chkFrom.TabIndex = 13
        Me.chkFrom.Text = "Use from value"
        Me.chkFrom.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(239, 217)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 14
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(154, 217)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 15
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbFile)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbSection)
        Me.GroupBox1.Controls.Add(Me.chkFrom)
        Me.GroupBox1.Controls.Add(Me.cmdDeleteSection)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmdDeleteKey)
        Me.GroupBox1.Controls.Add(Me.cmdAddSection)
        Me.GroupBox1.Controls.Add(Me.cmdSetValue)
        Me.GroupBox1.Controls.Add(Me.cmbKey)
        Me.GroupBox1.Controls.Add(Me.cmdAddKey)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtValue)
        Me.GroupBox1.Location = New System.Drawing.Point(5, -1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(319, 209)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        '
        'frmINI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(330, 248)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdCancel)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmINI"
        Me.Text = "frmINI"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbFile As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbSection As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbKey As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents cmdSetValue As System.Windows.Forms.Button
    Friend WithEvents cmdAddSection As System.Windows.Forms.Button
    Friend WithEvents cmdAddKey As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmdDeleteKey As System.Windows.Forms.Button
    Friend WithEvents cmdDeleteSection As System.Windows.Forms.Button
    Friend WithEvents chkFrom As System.Windows.Forms.CheckBox
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
