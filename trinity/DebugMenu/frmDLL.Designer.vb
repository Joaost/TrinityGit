<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDLL
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
        Me.grdInstalls = New System.Windows.Forms.DataGridView()
        Me.cmdUpload = New System.Windows.Forms.Button()
        Me.cmdRollback = New System.Windows.Forms.Button()
        Me.colUse = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colInstall = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colVersion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.grdInstalls, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdInstalls
        '
        Me.grdInstalls.AllowUserToAddRows = False
        Me.grdInstalls.AllowUserToDeleteRows = False
        Me.grdInstalls.AllowUserToResizeColumns = False
        Me.grdInstalls.AllowUserToResizeRows = False
        Me.grdInstalls.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdInstalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdInstalls.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colUse, Me.colInstall, Me.colVersion, Me.colStatus})
        Me.grdInstalls.Location = New System.Drawing.Point(12, 12)
        Me.grdInstalls.Name = "grdInstalls"
        Me.grdInstalls.ReadOnly = True
        Me.grdInstalls.RowHeadersVisible = False
        Me.grdInstalls.Size = New System.Drawing.Size(407, 346)
        Me.grdInstalls.TabIndex = 0
        '
        'cmdUpload
        '
        Me.cmdUpload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdUpload.Image = Global.DebugMenu.My.Resources.Resources.document_up
        Me.cmdUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdUpload.Location = New System.Drawing.Point(329, 364)
        Me.cmdUpload.Name = "cmdUpload"
        Me.cmdUpload.Size = New System.Drawing.Size(90, 36)
        Me.cmdUpload.TabIndex = 2
        Me.cmdUpload.Text = "Upload new"
        Me.cmdUpload.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdUpload.UseVisualStyleBackColor = True
        '
        'cmdRollback
        '
        Me.cmdRollback.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRollback.Image = Global.DebugMenu.My.Resources.Resources.media_beginning
        Me.cmdRollback.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdRollback.Location = New System.Drawing.Point(250, 364)
        Me.cmdRollback.Name = "cmdRollback"
        Me.cmdRollback.Size = New System.Drawing.Size(73, 36)
        Me.cmdRollback.TabIndex = 1
        Me.cmdRollback.Text = "Rollback"
        Me.cmdRollback.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdRollback.UseVisualStyleBackColor = True
        '
        'colUse
        '
        Me.colUse.Frozen = True
        Me.colUse.HeaderText = ""
        Me.colUse.Name = "colUse"
        Me.colUse.ReadOnly = True
        Me.colUse.Width = 25
        '
        'colInstall
        '
        Me.colInstall.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colInstall.HeaderText = "Install"
        Me.colInstall.Name = "colInstall"
        Me.colInstall.ReadOnly = True
        Me.colInstall.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colInstall.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colVersion
        '
        Me.colVersion.HeaderText = "Version"
        Me.colVersion.Name = "colVersion"
        Me.colVersion.ReadOnly = True
        '
        'colStatus
        '
        Me.colStatus.HeaderText = "Status"
        Me.colStatus.Name = "colStatus"
        Me.colStatus.ReadOnly = True
        Me.colStatus.Visible = False
        '
        'frmDLL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(431, 412)
        Me.Controls.Add(Me.cmdUpload)
        Me.Controls.Add(Me.cmdRollback)
        Me.Controls.Add(Me.grdInstalls)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmDLL"
        Me.ShowIcon = False
        Me.Text = "Handle Connect.dll"
        CType(Me.grdInstalls, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdInstalls As System.Windows.Forms.DataGridView
    Friend WithEvents cmdRollback As System.Windows.Forms.Button
    Friend WithEvents cmdUpload As System.Windows.Forms.Button
    Friend WithEvents colUse As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colInstall As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colVersion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStatus As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
