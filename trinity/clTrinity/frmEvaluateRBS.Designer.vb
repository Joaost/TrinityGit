<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEvaluateRBS
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEvaluateRBS))
        Me.grdEvaluate = New System.Windows.Forms.DataGridView()
        Me.colWeek = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFilm = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDaypart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBooked = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlanned = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colActual = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbChannel = New System.Windows.Forms.ComboBox()
        Me.chkHideZero = New System.Windows.Forms.CheckBox()
        CType(Me.grdEvaluate,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'grdEvaluate
        '
        Me.grdEvaluate.AllowUserToAddRows = false
        Me.grdEvaluate.AllowUserToDeleteRows = false
        Me.grdEvaluate.AllowUserToResizeColumns = false
        Me.grdEvaluate.AllowUserToResizeRows = false
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.grdEvaluate.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdEvaluate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdEvaluate.BackgroundColor = System.Drawing.Color.Silver
        Me.grdEvaluate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdEvaluate.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colWeek, Me.colFilm, Me.colDaypart, Me.colBooked, Me.colPlanned, Me.colActual})
        Me.grdEvaluate.Location = New System.Drawing.Point(12, 50)
        Me.grdEvaluate.Name = "grdEvaluate"
        Me.grdEvaluate.ReadOnly = true
        Me.grdEvaluate.RowHeadersVisible = false
        Me.grdEvaluate.Size = New System.Drawing.Size(547, 267)
        Me.grdEvaluate.TabIndex = 0
        Me.grdEvaluate.VirtualMode = true
        '
        'colWeek
        '
        Me.colWeek.HeaderText = "Week"
        Me.colWeek.Name = "colWeek"
        Me.colWeek.ReadOnly = true
        '
        'colFilm
        '
        Me.colFilm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colFilm.HeaderText = "Film"
        Me.colFilm.Name = "colFilm"
        Me.colFilm.ReadOnly = true
        '
        'colDaypart
        '
        Me.colDaypart.HeaderText = "Daypart"
        Me.colDaypart.Name = "colDaypart"
        Me.colDaypart.ReadOnly = true
        '
        'colBooked
        '
        Me.colBooked.HeaderText = "Booked"
        Me.colBooked.Name = "colBooked"
        Me.colBooked.ReadOnly = true
        Me.colBooked.Width = 70
        '
        'colPlanned
        '
        Me.colPlanned.HeaderText = "Planned"
        Me.colPlanned.Name = "colPlanned"
        Me.colPlanned.ReadOnly = true
        Me.colPlanned.Width = 70
        '
        'colActual
        '
        Me.colActual.HeaderText = "Actual"
        Me.colActual.Name = "colActual"
        Me.colActual.ReadOnly = true
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Channel"
        '
        'cmbChannel
        '
        Me.cmbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChannel.FormattingEnabled = true
        Me.cmbChannel.Location = New System.Drawing.Point(12, 24)
        Me.cmbChannel.Name = "cmbChannel"
        Me.cmbChannel.Size = New System.Drawing.Size(163, 21)
        Me.cmbChannel.TabIndex = 2
        '
        'chkHideZero
        '
        Me.chkHideZero.AutoSize = true
        Me.chkHideZero.Checked = true
        Me.chkHideZero.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkHideZero.Location = New System.Drawing.Point(181, 26)
        Me.chkHideZero.Name = "chkHideZero"
        Me.chkHideZero.Size = New System.Drawing.Size(193, 17)
        Me.chkHideZero.TabIndex = 3
        Me.chkHideZero.Text = "Hide rows without Planned TRPs"
        Me.chkHideZero.UseVisualStyleBackColor = true
        '
        'frmEvaluateRBS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(571, 329)
        Me.Controls.Add(Me.chkHideZero)
        Me.Controls.Add(Me.cmbChannel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grdEvaluate)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmEvaluateRBS"
        Me.Text = "Evaluate RBS"
        CType(Me.grdEvaluate,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents grdEvaluate As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbChannel As System.Windows.Forms.ComboBox
    Friend WithEvents chkHideZero As System.Windows.Forms.CheckBox
    Friend WithEvents colWeek As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFilm As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDaypart As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBooked As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanned As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colActual As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
