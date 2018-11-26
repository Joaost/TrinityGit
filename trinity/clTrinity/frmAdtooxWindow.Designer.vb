<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdtooxWindow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdtooxWindow))
        Me.bwsAdtoox = New System.Windows.Forms.WebBrowser()
        Me.lblWaitLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'bwsAdtoox
        '
        Me.bwsAdtoox.AllowWebBrowserDrop = False
        Me.bwsAdtoox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.bwsAdtoox.IsWebBrowserContextMenuEnabled = False
        Me.bwsAdtoox.Location = New System.Drawing.Point(0, 0)
        Me.bwsAdtoox.MinimumSize = New System.Drawing.Size(20, 22)
        Me.bwsAdtoox.Name = "bwsAdtoox"
        Me.bwsAdtoox.ScriptErrorsSuppressed = True
        Me.bwsAdtoox.Size = New System.Drawing.Size(365, 297)
        Me.bwsAdtoox.TabIndex = 0
        Me.bwsAdtoox.Visible = False
        Me.bwsAdtoox.WebBrowserShortcutsEnabled = False
        '
        'lblWaitLabel
        '
        Me.lblWaitLabel.AutoSize = True
        Me.lblWaitLabel.BackColor = System.Drawing.Color.White
        Me.lblWaitLabel.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWaitLabel.ForeColor = System.Drawing.Color.Silver
        Me.lblWaitLabel.Location = New System.Drawing.Point(109, 124)
        Me.lblWaitLabel.Name = "lblWaitLabel"
        Me.lblWaitLabel.Size = New System.Drawing.Size(129, 22)
        Me.lblWaitLabel.TabIndex = 1
        Me.lblWaitLabel.Text = "Please wait..."
        '
        'frmAdtooxWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(365, 297)
        Me.Controls.Add(Me.lblWaitLabel)
        Me.Controls.Add(Me.bwsAdtoox)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAdtooxWindow"
        Me.Text = "E.C. Express"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bwsAdtoox As System.Windows.Forms.WebBrowser
    Friend WithEvents lblWaitLabel As System.Windows.Forms.Label
End Class
