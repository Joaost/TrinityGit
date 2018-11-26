<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFindTarget
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFindTarget))
        Me.tvwTargets = New System.Windows.Forms.TreeView()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.chkSortByUser = New System.Windows.Forms.CheckBox()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'tvwTargets
        '
        Me.tvwTargets.Location = New System.Drawing.Point(12, 27)
        Me.tvwTargets.Name = "tvwTargets"
        Me.tvwTargets.Size = New System.Drawing.Size(441, 351)
        Me.tvwTargets.TabIndex = 0
        '
        'cmdOk
        '
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Location = New System.Drawing.Point(378, 383)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(75, 24)
        Me.cmdOk.TabIndex = 1
        Me.cmdOk.Text = "Pick"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'chkSortByUser
        '
        Me.chkSortByUser.AutoSize = true
        Me.chkSortByUser.Location = New System.Drawing.Point(12, 388)
        Me.chkSortByUser.Name = "chkSortByUser"
        Me.chkSortByUser.Size = New System.Drawing.Size(87, 17)
        Me.chkSortByUser.TabIndex = 2
        Me.chkSortByUser.Text = "Sort by user"
        Me.chkSortByUser.UseVisualStyleBackColor = true
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = true
        Me.lblMessage.Location = New System.Drawing.Point(12, 11)
        Me.lblMessage.MaximumSize = New System.Drawing.Size(441, 0)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(0, 13)
        Me.lblMessage.TabIndex = 3
        '
        'frmFindTarget
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(465, 419)
        Me.Controls.Add(Me.chkSortByUser)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.tvwTargets)
        Me.Controls.Add(Me.lblMessage)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmFindTarget"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Pick target..."
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents tvwTargets As System.Windows.Forms.TreeView
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents chkSortByUser As System.Windows.Forms.CheckBox
    Friend WithEvents lblMessage As System.Windows.Forms.Label
End Class
