<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMarathon
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMarathon))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.grpMarathon = New System.Windows.Forms.GroupBox()
        Me.grdMarathon = New System.Windows.Forms.DataGridView()
        Me.colType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlanned = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colConfirmed = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colToMarathon = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grdSummary = New System.Windows.Forms.DataGridView()
        Me.colCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpSummary = New System.Windows.Forms.GroupBox()
        Me.pnlSignature = New System.Windows.Forms.Panel()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.pbMarathon = New System.Windows.Forms.ProgressBar()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpMarathon.SuspendLayout
        CType(Me.grdMarathon,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grdSummary,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpSummary.SuspendLayout
        Me.pnlSignature.SuspendLayout
        Me.SuspendLayout
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"),System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = false
        '
        'grpMarathon
        '
        Me.grpMarathon.Controls.Add(Me.grdMarathon)
        Me.grpMarathon.Location = New System.Drawing.Point(12, 50)
        Me.grpMarathon.Name = "grpMarathon"
        Me.grpMarathon.Size = New System.Drawing.Size(490, 163)
        Me.grpMarathon.TabIndex = 1
        Me.grpMarathon.TabStop = false
        Me.grpMarathon.Text = "Per channel"
        '
        'grdMarathon
        '
        Me.grdMarathon.AllowUserToAddRows = false
        Me.grdMarathon.AllowUserToDeleteRows = false
        Me.grdMarathon.AllowUserToResizeColumns = false
        Me.grdMarathon.AllowUserToResizeRows = false
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grdMarathon.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdMarathon.BackgroundColor = System.Drawing.Color.Silver
        Me.grdMarathon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdMarathon.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colType, Me.colPlanned, Me.colConfirmed, Me.colToMarathon})
        Me.grdMarathon.Location = New System.Drawing.Point(134, 18)
        Me.grdMarathon.Name = "grdMarathon"
        Me.grdMarathon.RowHeadersVisible = false
        Me.grdMarathon.Size = New System.Drawing.Size(350, 140)
        Me.grdMarathon.TabIndex = 0
        Me.grdMarathon.VirtualMode = true
        '
        'colType
        '
        Me.colType.Frozen = true
        Me.colType.HeaderText = "Type"
        Me.colType.Name = "colType"
        Me.colType.ReadOnly = true
        Me.colType.Width = 85
        '
        'colPlanned
        '
        Me.colPlanned.Frozen = true
        Me.colPlanned.HeaderText = "Planned"
        Me.colPlanned.Name = "colPlanned"
        Me.colPlanned.ReadOnly = true
        Me.colPlanned.Width = 85
        '
        'colConfirmed
        '
        Me.colConfirmed.Frozen = true
        Me.colConfirmed.HeaderText = "Confirmed"
        Me.colConfirmed.Name = "colConfirmed"
        Me.colConfirmed.ReadOnly = true
        Me.colConfirmed.Width = 85
        '
        'colToMarathon
        '
        Me.colToMarathon.Frozen = true
        Me.colToMarathon.HeaderText = "To Marathon"
        Me.colToMarathon.Name = "colToMarathon"
        Me.colToMarathon.Width = 90
        '
        'grdSummary
        '
        Me.grdSummary.AllowUserToAddRows = false
        Me.grdSummary.AllowUserToDeleteRows = false
        Me.grdSummary.AllowUserToResizeColumns = false
        Me.grdSummary.AllowUserToResizeRows = false
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        Me.grdSummary.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.grdSummary.BackgroundColor = System.Drawing.Color.Silver
        Me.grdSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSummary.ColumnHeadersVisible = false
        Me.grdSummary.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCost})
        Me.grdSummary.Location = New System.Drawing.Point(6, 17)
        Me.grdSummary.Name = "grdSummary"
        Me.grdSummary.ReadOnly = true
        Me.grdSummary.Size = New System.Drawing.Size(198, 71)
        Me.grdSummary.TabIndex = 2
        Me.grdSummary.VirtualMode = true
        '
        'colCost
        '
        Me.colCost.Frozen = true
        Me.colCost.HeaderText = ""
        Me.colCost.Name = "colCost"
        Me.colCost.ReadOnly = true
        Me.colCost.Width = 60
        '
        'grpSummary
        '
        Me.grpSummary.Controls.Add(Me.grdSummary)
        Me.grpSummary.Location = New System.Drawing.Point(292, 219)
        Me.grpSummary.Name = "grpSummary"
        Me.grpSummary.Size = New System.Drawing.Size(210, 93)
        Me.grpSummary.TabIndex = 3
        Me.grpSummary.TabStop = false
        Me.grpSummary.Text = "Summary"
        '
        'pnlSignature
        '
        Me.pnlSignature.Controls.Add(Me.txtUser)
        Me.pnlSignature.Controls.Add(Me.Label1)
        Me.pnlSignature.Controls.Add(Me.cmdOk)
        Me.pnlSignature.Location = New System.Drawing.Point(292, 12)
        Me.pnlSignature.Name = "pnlSignature"
        Me.pnlSignature.Size = New System.Drawing.Size(210, 38)
        Me.pnlSignature.TabIndex = 4
        '
        'txtUser
        '
        Me.txtUser.Location = New System.Drawing.Point(68, 9)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(53, 22)
        Me.txtUser.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(3, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Username:"
        '
        'cmdOk
        '
        Me.cmdOk.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Location = New System.Drawing.Point(127, 3)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(77, 32)
        Me.cmdOk.TabIndex = 0
        Me.cmdOk.Text = "Confirm"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'pbMarathon
        '
        Me.pbMarathon.Location = New System.Drawing.Point(12, 334)
        Me.pbMarathon.Name = "pbMarathon"
        Me.pbMarathon.Size = New System.Drawing.Size(484, 21)
        Me.pbMarathon.TabIndex = 5
        Me.pbMarathon.Visible = false
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.Frozen = true
        Me.DataGridViewTextBoxColumn1.HeaderText = ""
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = true
        Me.DataGridViewTextBoxColumn1.Width = 60
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.Frozen = true
        Me.DataGridViewTextBoxColumn2.HeaderText = "Type"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = true
        Me.DataGridViewTextBoxColumn2.Width = 85
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.Frozen = true
        Me.DataGridViewTextBoxColumn3.HeaderText = "Planned"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = true
        Me.DataGridViewTextBoxColumn3.Width = 85
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.Frozen = true
        Me.DataGridViewTextBoxColumn4.HeaderText = "Confirmed"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = true
        Me.DataGridViewTextBoxColumn4.Width = 85
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.Frozen = true
        Me.DataGridViewTextBoxColumn5.HeaderText = "To Marathon"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 90
        '
        'frmMarathon
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = true
        Me.AutoSize = true
        Me.ClientSize = New System.Drawing.Size(517, 400)
        Me.Controls.Add(Me.pbMarathon)
        Me.Controls.Add(Me.pnlSignature)
        Me.Controls.Add(Me.grpMarathon)
        Me.Controls.Add(Me.grpSummary)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmMarathon"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "Marathon"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpMarathon.ResumeLayout(false)
        CType(Me.grdMarathon,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdSummary,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpSummary.ResumeLayout(false)
        Me.pnlSignature.ResumeLayout(false)
        Me.pnlSignature.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents grpMarathon As System.Windows.Forms.GroupBox
    Friend WithEvents grdMarathon As System.Windows.Forms.DataGridView
    Friend WithEvents grdSummary As System.Windows.Forms.DataGridView
    Friend WithEvents colCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpSummary As System.Windows.Forms.GroupBox
    Friend WithEvents colType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanned As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colConfirmed As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colToMarathon As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pnlSignature As System.Windows.Forms.Panel
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents pbMarathon As System.Windows.Forms.ProgressBar
End Class
