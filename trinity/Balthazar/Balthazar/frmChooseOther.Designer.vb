<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChooseOther
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
        Me.lblStatus = New System.Windows.Forms.Label
        Me.lstList = New System.Windows.Forms.ListBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdRename = New System.Windows.Forms.Button
        Me.cmdReplace = New System.Windows.Forms.Button
        Me.cmdSkip = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(12, 9)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 14)
        Me.lblStatus.TabIndex = 0
        '
        'lstList
        '
        Me.lstList.FormattingEnabled = True
        Me.lstList.ItemHeight = 14
        Me.lstList.Location = New System.Drawing.Point(12, 40)
        Me.lstList.Name = "lstList"
        Me.lstList.Size = New System.Drawing.Size(243, 116)
        Me.lstList.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(143, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Please choose replacement:"
        '
        'cmdRename
        '
        Me.cmdRename.Location = New System.Drawing.Point(96, 162)
        Me.cmdRename.Name = "cmdRename"
        Me.cmdRename.Size = New System.Drawing.Size(75, 26)
        Me.cmdRename.TabIndex = 3
        Me.cmdRename.Text = "Rename"
        Me.cmdRename.UseVisualStyleBackColor = True
        '
        'cmdReplace
        '
        Me.cmdReplace.Location = New System.Drawing.Point(177, 162)
        Me.cmdReplace.Name = "cmdReplace"
        Me.cmdReplace.Size = New System.Drawing.Size(75, 26)
        Me.cmdReplace.TabIndex = 4
        Me.cmdReplace.Text = "Replace"
        Me.cmdReplace.UseVisualStyleBackColor = True
        '
        'cmdSkip
        '
        Me.cmdSkip.Location = New System.Drawing.Point(15, 162)
        Me.cmdSkip.Name = "cmdSkip"
        Me.cmdSkip.Size = New System.Drawing.Size(75, 26)
        Me.cmdSkip.TabIndex = 5
        Me.cmdSkip.Text = "Skip"
        Me.cmdSkip.UseVisualStyleBackColor = True
        '
        'frmChooseOther
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(267, 200)
        Me.Controls.Add(Me.cmdSkip)
        Me.Controls.Add(Me.cmdReplace)
        Me.Controls.Add(Me.cmdRename)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lstList)
        Me.Controls.Add(Me.lblStatus)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmChooseOther"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Choose..."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lstList As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdRename As System.Windows.Forms.Button
    Friend WithEvents cmdReplace As System.Windows.Forms.Button
    Friend WithEvents cmdSkip As System.Windows.Forms.Button
End Class
