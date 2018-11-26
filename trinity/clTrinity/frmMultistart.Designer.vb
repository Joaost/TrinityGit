<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMultistart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMultistart))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbProfiles = New System.Windows.Forms.ComboBox()
        Me.cdmOk = New System.Windows.Forms.Button()
        Me.btnRegDLL = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(214, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Please choose the profile you want to use:"
        '
        'cmbProfiles
        '
        Me.cmbProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProfiles.FormattingEnabled = True
        Me.cmbProfiles.Location = New System.Drawing.Point(12, 31)
        Me.cmbProfiles.Name = "cmbProfiles"
        Me.cmbProfiles.Size = New System.Drawing.Size(214, 22)
        Me.cmbProfiles.TabIndex = 1
        '
        'cdmOk
        '
        Me.cdmOk.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.cdmOk.FlatAppearance.BorderSize = 0
        Me.cdmOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cdmOk.Location = New System.Drawing.Point(70, 99)
        Me.cdmOk.Name = "cdmOk"
        Me.cdmOk.Size = New System.Drawing.Size(85, 30)
        Me.cdmOk.TabIndex = 2
        Me.cdmOk.Text = "Ok"
        Me.cdmOk.UseVisualStyleBackColor = True
        '
        'btnRegDLL
        '
        Me.btnRegDLL.FlatAppearance.BorderSize = 0
        Me.btnRegDLL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRegDLL.Location = New System.Drawing.Point(70, 59)
        Me.btnRegDLL.Name = "btnRegDLL"
        Me.btnRegDLL.Size = New System.Drawing.Size(85, 30)
        Me.btnRegDLL.TabIndex = 3
        Me.btnRegDLL.Text = "Register DLL"
        Me.btnRegDLL.UseVisualStyleBackColor = True
        '
        'frmMultistart
        '
        Me.AcceptButton = Me.cdmOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(238, 141)
        Me.Controls.Add(Me.btnRegDLL)
        Me.Controls.Add(Me.cdmOk)
        Me.Controls.Add(Me.cmbProfiles)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMultistart"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "T R I N I T Y"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbProfiles As System.Windows.Forms.ComboBox
    Friend WithEvents cdmOk As System.Windows.Forms.Button
    Friend WithEvents btnRegDLL As System.Windows.Forms.Button
End Class
