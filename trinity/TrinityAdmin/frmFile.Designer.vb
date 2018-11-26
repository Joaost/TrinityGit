<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFile
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
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblFile = New System.Windows.Forms.Label
        Me.cmdBrowse = New System.Windows.Forms.Button
        Me.rdbNone = New System.Windows.Forms.RadioButton
        Me.chkOverwrite = New System.Windows.Forms.CheckBox
        Me.rdbProg = New System.Windows.Forms.RadioButton
        Me.rdbVersion = New System.Windows.Forms.RadioButton
        Me.chkNew = New System.Windows.Forms.CheckBox
        Me.rdbSetup = New System.Windows.Forms.RadioButton
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(176, 162)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 0
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(95, 162)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 1
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "File name:"
        '
        'lblFile
        '
        Me.lblFile.AutoSize = True
        Me.lblFile.Location = New System.Drawing.Point(12, 27)
        Me.lblFile.Name = "lblFile"
        Me.lblFile.Size = New System.Drawing.Size(0, 13)
        Me.lblFile.TabIndex = 5
        Me.lblFile.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Location = New System.Drawing.Point(12, 43)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(52, 22)
        Me.cmdBrowse.TabIndex = 6
        Me.cmdBrowse.Text = "Browse"
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'rdbNone
        '
        Me.rdbNone.AutoSize = True
        Me.rdbNone.Checked = True
        Me.rdbNone.Location = New System.Drawing.Point(12, 104)
        Me.rdbNone.Name = "rdbNone"
        Me.rdbNone.Size = New System.Drawing.Size(59, 17)
        Me.rdbNone.TabIndex = 7
        Me.rdbNone.Text = "Neither"
        Me.rdbNone.UseVisualStyleBackColor = True
        '
        'chkOverwrite
        '
        Me.chkOverwrite.AutoSize = True
        Me.chkOverwrite.Location = New System.Drawing.Point(12, 162)
        Me.chkOverwrite.Name = "chkOverwrite"
        Me.chkOverwrite.Size = New System.Drawing.Size(71, 17)
        Me.chkOverwrite.TabIndex = 10
        Me.chkOverwrite.Text = "Overwrite"
        Me.chkOverwrite.UseVisualStyleBackColor = True
        '
        'rdbProg
        '
        Me.rdbProg.AutoSize = True
        Me.rdbProg.Location = New System.Drawing.Point(136, 81)
        Me.rdbProg.Name = "rdbProg"
        Me.rdbProg.Size = New System.Drawing.Size(96, 17)
        Me.rdbProg.TabIndex = 11
        Me.rdbProg.Text = "Program Folder"
        Me.rdbProg.UseVisualStyleBackColor = True
        '
        'rdbVersion
        '
        Me.rdbVersion.AutoSize = True
        Me.rdbVersion.Location = New System.Drawing.Point(12, 81)
        Me.rdbVersion.Name = "rdbVersion"
        Me.rdbVersion.Size = New System.Drawing.Size(104, 17)
        Me.rdbVersion.TabIndex = 12
        Me.rdbVersion.Text = "New Version File"
        Me.rdbVersion.UseVisualStyleBackColor = True
        '
        'chkNew
        '
        Me.chkNew.AutoSize = True
        Me.chkNew.Location = New System.Drawing.Point(12, 139)
        Me.chkNew.Name = "chkNew"
        Me.chkNew.Size = New System.Drawing.Size(64, 17)
        Me.chkNew.TabIndex = 13
        Me.chkNew.Text = "New file"
        Me.chkNew.UseVisualStyleBackColor = True
        '
        'rdbSetup
        '
        Me.rdbSetup.AutoSize = True
        Me.rdbSetup.Location = New System.Drawing.Point(136, 104)
        Me.rdbSetup.Name = "rdbSetup"
        Me.rdbSetup.Size = New System.Drawing.Size(72, 17)
        Me.rdbSetup.TabIndex = 14
        Me.rdbSetup.Text = "Set up file"
        Me.rdbSetup.UseVisualStyleBackColor = True
        '
        'frmFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(263, 197)
        Me.Controls.Add(Me.rdbSetup)
        Me.Controls.Add(Me.chkNew)
        Me.Controls.Add(Me.rdbVersion)
        Me.Controls.Add(Me.rdbProg)
        Me.Controls.Add(Me.chkOverwrite)
        Me.Controls.Add(Me.rdbNone)
        Me.Controls.Add(Me.cmdBrowse)
        Me.Controls.Add(Me.lblFile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdCancel)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFile"
        Me.Text = "frmFile"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblFile As System.Windows.Forms.Label
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents rdbNone As System.Windows.Forms.RadioButton
    Friend WithEvents chkOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents rdbProg As System.Windows.Forms.RadioButton
    Friend WithEvents rdbVersion As System.Windows.Forms.RadioButton
    Friend WithEvents chkNew As System.Windows.Forms.CheckBox
    Friend WithEvents rdbSetup As System.Windows.Forms.RadioButton
End Class
