<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPlugins
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPlugins))
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.lstPlugins = New System.Windows.Forms.CheckedListBox()
        Me.btnReInstallPlugin = New System.Windows.Forms.Button()
        Me.SuspendLayout
        '
        'cmdOk
        '
        Me.cmdOk.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Location = New System.Drawing.Point(190, 238)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(82, 32)
        Me.cmdOk.TabIndex = 0
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'lstPlugins
        '
        Me.lstPlugins.FormattingEnabled = true
        Me.lstPlugins.Location = New System.Drawing.Point(12, 12)
        Me.lstPlugins.Name = "lstPlugins"
        Me.lstPlugins.Size = New System.Drawing.Size(260, 214)
        Me.lstPlugins.TabIndex = 1
        '
        'btnReInstallPlugin
        '
        Me.btnReInstallPlugin.Enabled = false
        Me.btnReInstallPlugin.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.btnReInstallPlugin.FlatAppearance.BorderSize = 0
        Me.btnReInstallPlugin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReInstallPlugin.Location = New System.Drawing.Point(12, 238)
        Me.btnReInstallPlugin.Name = "btnReInstallPlugin"
        Me.btnReInstallPlugin.Size = New System.Drawing.Size(108, 32)
        Me.btnReInstallPlugin.TabIndex = 2
        Me.btnReInstallPlugin.Text = "Re-install plugin"
        Me.btnReInstallPlugin.UseVisualStyleBackColor = true
        '
        'frmPlugins
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 14!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 282)
        Me.Controls.Add(Me.btnReInstallPlugin)
        Me.Controls.Add(Me.lstPlugins)
        Me.Controls.Add(Me.cmdOk)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmPlugins"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Installed plugins"
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents lstPlugins As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnReInstallPlugin As Windows.Forms.Button
End Class
