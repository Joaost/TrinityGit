<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUploadSchedule
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUploadSchedule))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.grdSchedule = New System.Windows.Forms.DataGridView()
        Me.cmdImportToDB = New System.Windows.Forms.Button()
        Me.ofdOpen = New System.Windows.Forms.OpenFileDialog()
        Me.cmdOpen = New System.Windows.Forms.Button()
        Me.cmdSynchAllTV4 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.grdSchedule)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 50)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(820, 343)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Schedule"
        '
        'grdSchedule
        '
        Me.grdSchedule.AllowUserToAddRows = False
        Me.grdSchedule.AllowUserToDeleteRows = False
        Me.grdSchedule.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSchedule.Location = New System.Drawing.Point(6, 19)
        Me.grdSchedule.Name = "grdSchedule"
        Me.grdSchedule.ReadOnly = True
        Me.grdSchedule.RowHeadersVisible = False
        Me.grdSchedule.Size = New System.Drawing.Size(808, 318)
        Me.grdSchedule.TabIndex = 0
        '
        'cmdImportToDB
        '
        Me.cmdImportToDB.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdImportToDB.Enabled = False
        Me.cmdImportToDB.FlatAppearance.BorderSize = 0
        Me.cmdImportToDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdImportToDB.Location = New System.Drawing.Point(736, 399)
        Me.cmdImportToDB.Name = "cmdImportToDB"
        Me.cmdImportToDB.Size = New System.Drawing.Size(96, 32)
        Me.cmdImportToDB.TabIndex = 3
        Me.cmdImportToDB.Text = "Import to DB"
        Me.cmdImportToDB.UseVisualStyleBackColor = True
        '
        'ofdOpen
        '
        Me.ofdOpen.Filter = "All readable formats|*.xml;*.xls;*.xlsx;*.txt;*.csv|XML Schedule|*.xml|Excel sche" & _
    "dule|*.xls;*.xlsx;*xltx;|Tab-separated text file|*.txt|Semicolon separated text " & _
    "file (CSV)|*.csv"
        Me.ofdOpen.Title = "Open channel schedule"
        '
        'cmdOpen
        '
        Me.cmdOpen.FlatAppearance.BorderSize = 0
        Me.cmdOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOpen.Location = New System.Drawing.Point(12, 12)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(111, 32)
        Me.cmdOpen.TabIndex = 4
        Me.cmdOpen.Text = "Open schedule file"
        Me.cmdOpen.UseVisualStyleBackColor = True
        '
        'cmdSynchAllTV4
        '
        Me.cmdSynchAllTV4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdSynchAllTV4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdSynchAllTV4.Location = New System.Drawing.Point(693, 5)
        Me.cmdSynchAllTV4.Name = "cmdSynchAllTV4"
        Me.cmdSynchAllTV4.Size = New System.Drawing.Size(48, 44)
        Me.cmdSynchAllTV4.TabIndex = 5
        Me.cmdSynchAllTV4.UseVisualStyleBackColor = True
        '
        'frmUploadSchedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(844, 443)
        Me.Controls.Add(Me.cmdSynchAllTV4)
        Me.Controls.Add(Me.cmdOpen)
        Me.Controls.Add(Me.cmdImportToDB)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmUploadSchedule"
        Me.Text = "Upload channel schedule"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grdSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grdSchedule As System.Windows.Forms.DataGridView
    Friend WithEvents cmdImportToDB As System.Windows.Forms.Button
    Friend WithEvents ofdOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents cmdSynchAllTV4 As System.Windows.Forms.Button
    Friend WithEvents tmPckStart As clTrinity.ExtendedDateTimePicker
    Friend WithEvents tmPckEnd As clTrinity.ExtendedDateTimePicker
End Class
