<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHelp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHelp))
        Me.webHelpText = New System.Windows.Forms.WebBrowser()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.cmdDoNotShow = New System.Windows.Forms.ToolStripButton()
        Me.cmdShow = New System.Windows.Forms.ToolStripButton()
        Me.cmdAutoFix = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip1.SuspendLayout
        Me.SuspendLayout
        '
        'webHelpText
        '
        Me.webHelpText.AllowNavigation = false
        Me.webHelpText.AllowWebBrowserDrop = false
        Me.webHelpText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.webHelpText.IsWebBrowserContextMenuEnabled = false
        Me.webHelpText.Location = New System.Drawing.Point(0, 26)
        Me.webHelpText.MinimumSize = New System.Drawing.Size(20, 20)
        Me.webHelpText.Name = "webHelpText"
        Me.webHelpText.Size = New System.Drawing.Size(472, 245)
        Me.webHelpText.TabIndex = 0
        Me.webHelpText.WebBrowserShortcutsEnabled = false
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdDoNotShow, Me.cmdShow, Me.cmdAutoFix})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(472, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmdDoNotShow
        '
        Me.cmdDoNotShow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdDoNotShow.Image = Global.clTrinity.My.Resources.Resources.help_2_16x16
        Me.cmdDoNotShow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDoNotShow.Name = "cmdDoNotShow"
        Me.cmdDoNotShow.Size = New System.Drawing.Size(23, 22)
        Me.cmdDoNotShow.Text = "Do not show errors of this type in the future"
        '
        'cmdShow
        '
        Me.cmdShow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"),System.Drawing.Image)
        Me.cmdShow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.Size = New System.Drawing.Size(23, 22)
        Me.cmdShow.Text = "Show errors of this type in the future"
        Me.cmdShow.Visible = false
        '
        'cmdAutoFix
        '
        Me.cmdAutoFix.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdAutoFix.Image = Global.clTrinity.My.Resources.Resources.settings_2_20x20
        Me.cmdAutoFix.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAutoFix.Name = "cmdAutoFix"
        Me.cmdAutoFix.Size = New System.Drawing.Size(23, 22)
        Me.cmdAutoFix.Text = "ToolStripButton1"
        Me.cmdAutoFix.ToolTipText = "Always autofix this kind of problem"
        '
        'frmHelp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(472, 268)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.webHelpText)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmHelp"
        Me.Text = "Help"
        Me.ToolStrip1.ResumeLayout(false)
        Me.ToolStrip1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents webHelpText As System.Windows.Forms.WebBrowser
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdDoNotShow As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdShow As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdAutoFix As System.Windows.Forms.ToolStripButton
End Class
