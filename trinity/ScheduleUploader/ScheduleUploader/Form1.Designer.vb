<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImport))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdRemoveServer = New System.Windows.Forms.Button()
        Me.txtServer = New System.Windows.Forms.TextBox()
        Me.cmdAddServer = New System.Windows.Forms.Button()
        Me.lstServers = New System.Windows.Forms.ListBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmdAddTV4 = New System.Windows.Forms.Button()
        Me.cmdBrowse = New System.Windows.Forms.Button()
        Me.cmdRemoveFile = New System.Windows.Forms.Button()
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.cmdAddFile = New System.Windows.Forms.Button()
        Me.lstFiles = New System.Windows.Forms.ListBox()
        Me.pbImport = New System.Windows.Forms.ProgressBar()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.cmdImport = New System.Windows.Forms.Button()
        Me.lblPath = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout
        Me.GroupBox2.SuspendLayout
        Me.SuspendLayout
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cmdRemoveServer)
        Me.GroupBox1.Controls.Add(Me.txtServer)
        Me.GroupBox1.Controls.Add(Me.cmdAddServer)
        Me.GroupBox1.Controls.Add(Me.lstServers)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(459, 159)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Servers"
        '
        'cmdRemoveServer
        '
        Me.cmdRemoveServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveServer.Enabled = false
        Me.cmdRemoveServer.Image = CType(resources.GetObject("cmdRemoveServer.Image"),System.Drawing.Image)
        Me.cmdRemoveServer.Location = New System.Drawing.Point(431, 47)
        Me.cmdRemoveServer.Name = "cmdRemoveServer"
        Me.cmdRemoveServer.Size = New System.Drawing.Size(22, 22)
        Me.cmdRemoveServer.TabIndex = 13
        Me.cmdRemoveServer.UseVisualStyleBackColor = true
        '
        'txtServer
        '
        Me.txtServer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtServer.Location = New System.Drawing.Point(6, 19)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(419, 20)
        Me.txtServer.TabIndex = 1
        '
        'cmdAddServer
        '
        Me.cmdAddServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdAddServer.Enabled = false
        Me.cmdAddServer.Image = CType(resources.GetObject("cmdAddServer.Image"),System.Drawing.Image)
        Me.cmdAddServer.Location = New System.Drawing.Point(431, 19)
        Me.cmdAddServer.Name = "cmdAddServer"
        Me.cmdAddServer.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddServer.TabIndex = 11
        Me.cmdAddServer.UseVisualStyleBackColor = true
        '
        'lstServers
        '
        Me.lstServers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lstServers.FormattingEnabled = true
        Me.lstServers.ItemHeight = 14
        Me.lstServers.Location = New System.Drawing.Point(6, 47)
        Me.lstServers.Name = "lstServers"
        Me.lstServers.Size = New System.Drawing.Size(419, 102)
        Me.lstServers.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.cmdAddTV4)
        Me.GroupBox2.Controls.Add(Me.cmdBrowse)
        Me.GroupBox2.Controls.Add(Me.cmdRemoveFile)
        Me.GroupBox2.Controls.Add(Me.txtFile)
        Me.GroupBox2.Controls.Add(Me.cmdAddFile)
        Me.GroupBox2.Controls.Add(Me.lstFiles)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 178)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(459, 159)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = false
        Me.GroupBox2.Text = "Files"
        '
        'cmdAddTV4
        '
        Me.cmdAddTV4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdAddTV4.Enabled = false
        Me.cmdAddTV4.Image = Global.ScheduleUploader.My.Resources.Resources.tv4_16x16
        Me.cmdAddTV4.Location = New System.Drawing.Point(431, 75)
        Me.cmdAddTV4.Name = "cmdAddTV4"
        Me.cmdAddTV4.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddTV4.TabIndex = 15
        Me.cmdAddTV4.UseVisualStyleBackColor = true
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdBrowse.Enabled = false
        Me.cmdBrowse.Image = CType(resources.GetObject("cmdBrowse.Image"),System.Drawing.Image)
        Me.cmdBrowse.Location = New System.Drawing.Point(403, 19)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(22, 22)
        Me.cmdBrowse.TabIndex = 14
        Me.cmdBrowse.UseVisualStyleBackColor = true
        '
        'cmdRemoveFile
        '
        Me.cmdRemoveFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveFile.Enabled = false
        Me.cmdRemoveFile.Image = CType(resources.GetObject("cmdRemoveFile.Image"),System.Drawing.Image)
        Me.cmdRemoveFile.Location = New System.Drawing.Point(431, 47)
        Me.cmdRemoveFile.Name = "cmdRemoveFile"
        Me.cmdRemoveFile.Size = New System.Drawing.Size(22, 22)
        Me.cmdRemoveFile.TabIndex = 13
        Me.cmdRemoveFile.UseVisualStyleBackColor = true
        '
        'txtFile
        '
        Me.txtFile.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtFile.Location = New System.Drawing.Point(6, 19)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.Size = New System.Drawing.Size(391, 20)
        Me.txtFile.TabIndex = 1
        '
        'cmdAddFile
        '
        Me.cmdAddFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdAddFile.Enabled = false
        Me.cmdAddFile.Image = CType(resources.GetObject("cmdAddFile.Image"),System.Drawing.Image)
        Me.cmdAddFile.Location = New System.Drawing.Point(431, 19)
        Me.cmdAddFile.Name = "cmdAddFile"
        Me.cmdAddFile.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddFile.TabIndex = 11
        Me.cmdAddFile.UseVisualStyleBackColor = true
        '
        'lstFiles
        '
        Me.lstFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lstFiles.FormattingEnabled = true
        Me.lstFiles.ItemHeight = 14
        Me.lstFiles.Location = New System.Drawing.Point(6, 47)
        Me.lstFiles.Name = "lstFiles"
        Me.lstFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFiles.Size = New System.Drawing.Size(419, 102)
        Me.lstFiles.TabIndex = 1
        '
        'pbImport
        '
        Me.pbImport.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pbImport.Location = New System.Drawing.Point(18, 343)
        Me.pbImport.Name = "pbImport"
        Me.pbImport.Size = New System.Drawing.Size(377, 29)
        Me.pbImport.TabIndex = 3
        Me.pbImport.Visible = false
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = true
        Me.lblStatus.Location = New System.Drawing.Point(15, 375)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 14)
        Me.lblStatus.TabIndex = 4
        '
        'cmdImport
        '
        Me.cmdImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdImport.Image = CType(resources.GetObject("cmdImport.Image"),System.Drawing.Image)
        Me.cmdImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdImport.Location = New System.Drawing.Point(401, 343)
        Me.cmdImport.Name = "cmdImport"
        Me.cmdImport.Size = New System.Drawing.Size(64, 29)
        Me.cmdImport.TabIndex = 2
        Me.cmdImport.Text = "Import"
        Me.cmdImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdImport.UseVisualStyleBackColor = true
        '
        'lblPath
        '
        Me.lblPath.AutoSize = true
        Me.lblPath.Location = New System.Drawing.Point(18, 375)
        Me.lblPath.Name = "lblPath"
        Me.lblPath.Size = New System.Drawing.Size(39, 14)
        Me.lblPath.TabIndex = 5
        Me.lblPath.Text = "Label1"
        '
        'frmImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 14!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 395)
        Me.Controls.Add(Me.lblPath)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.pbImport)
        Me.Controls.Add(Me.cmdImport)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Name = "frmImport"
        Me.ShowIcon = false
        Me.Text = "Import schedules"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemoveServer As System.Windows.Forms.Button
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents cmdAddServer As System.Windows.Forms.Button
    Friend WithEvents lstServers As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveFile As System.Windows.Forms.Button
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents cmdAddFile As System.Windows.Forms.Button
    Friend WithEvents lstFiles As System.Windows.Forms.ListBox
    Friend WithEvents cmdImport As System.Windows.Forms.Button
    Friend WithEvents pbImport As System.Windows.Forms.ProgressBar
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents cmdAddTV4 As System.Windows.Forms.Button
    Friend WithEvents lblPath As Label
End Class
