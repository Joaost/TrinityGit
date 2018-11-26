<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefineDayparts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDefineDayparts))
        Me.grpDayparts = New System.Windows.Forms.GroupBox()
        Me.cmbChannel = New System.Windows.Forms.ComboBox()
        Me.grdDayparts = New System.Windows.Forms.DataGridView()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEnd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPrime = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdSaveToFile = New System.Windows.Forms.Button()
        Me.cmdCopyDaypart = New System.Windows.Forms.Button()
        Me.cmdDown = New System.Windows.Forms.Button()
        Me.cmdUp = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.grpDayparts.SuspendLayout
        CType(Me.grdDayparts,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'grpDayparts
        '
        Me.grpDayparts.Controls.Add(Me.cmdCopyDaypart)
        Me.grpDayparts.Controls.Add(Me.cmbChannel)
        Me.grpDayparts.Controls.Add(Me.cmdDown)
        Me.grpDayparts.Controls.Add(Me.cmdUp)
        Me.grpDayparts.Controls.Add(Me.cmdDelete)
        Me.grpDayparts.Controls.Add(Me.cmdAdd)
        Me.grpDayparts.Controls.Add(Me.grdDayparts)
        Me.grpDayparts.Location = New System.Drawing.Point(12, 50)
        Me.grpDayparts.Name = "grpDayparts"
        Me.grpDayparts.Size = New System.Drawing.Size(334, 171)
        Me.grpDayparts.TabIndex = 1
        Me.grpDayparts.TabStop = false
        Me.grpDayparts.Text = "Dayparts"
        '
        'cmbChannel
        '
        Me.cmbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChannel.FormattingEnabled = true
        Me.cmbChannel.Location = New System.Drawing.Point(6, 18)
        Me.cmbChannel.Name = "cmbChannel"
        Me.cmbChannel.Size = New System.Drawing.Size(175, 21)
        Me.cmbChannel.TabIndex = 17
        '
        'grdDayparts
        '
        Me.grdDayparts.AllowUserToAddRows = false
        Me.grdDayparts.AllowUserToDeleteRows = false
        Me.grdDayparts.AllowUserToResizeColumns = false
        Me.grdDayparts.AllowUserToResizeRows = false
        Me.grdDayparts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdDayparts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDayparts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colStart, Me.colEnd, Me.colPrime})
        Me.grdDayparts.Location = New System.Drawing.Point(6, 44)
        Me.grdDayparts.Name = "grdDayparts"
        Me.grdDayparts.RowHeadersVisible = false
        Me.grdDayparts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdDayparts.Size = New System.Drawing.Size(294, 121)
        Me.grdDayparts.TabIndex = 0
        Me.grdDayparts.VirtualMode = true
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        '
        'colStart
        '
        Me.colStart.HeaderText = "Start time"
        Me.colStart.Name = "colStart"
        Me.colStart.Width = 75
        '
        'colEnd
        '
        Me.colEnd.HeaderText = "End time"
        Me.colEnd.Name = "colEnd"
        Me.colEnd.Width = 75
        '
        'colPrime
        '
        Me.colPrime.HeaderText = "Prime"
        Me.colPrime.Name = "colPrime"
        Me.colPrime.Width = 35
        '
        'cmdOK
        '
        Me.cmdOK.FlatAppearance.BorderSize = 0
        Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOK.Location = New System.Drawing.Point(271, 227)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 26)
        Me.cmdOK.TabIndex = 4
        Me.cmdOK.Text = "Apply"
        Me.cmdOK.UseVisualStyleBackColor = true
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Start time"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 75
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "End time"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 75
        '
        'cmdSaveToFile
        '
        Me.cmdSaveToFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdSaveToFile.FlatAppearance.BorderSize = 0
        Me.cmdSaveToFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSaveToFile.Image = Global.clTrinity.My.Resources.Resources.save_2
        Me.cmdSaveToFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSaveToFile.Location = New System.Drawing.Point(247, 16)
        Me.cmdSaveToFile.Name = "cmdSaveToFile"
        Me.cmdSaveToFile.Size = New System.Drawing.Size(99, 29)
        Me.cmdSaveToFile.TabIndex = 6
        Me.cmdSaveToFile.Text = "Save to file"
        Me.cmdSaveToFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSaveToFile.UseVisualStyleBackColor = true
        '
        'cmdCopyDaypart
        '
        Me.cmdCopyDaypart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdCopyDaypart.FlatAppearance.BorderSize = 0
        Me.cmdCopyDaypart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCopyDaypart.Image = Global.clTrinity.My.Resources.Resources.copy_3_16x16
        Me.cmdCopyDaypart.Location = New System.Drawing.Point(306, 148)
        Me.cmdCopyDaypart.Name = "cmdCopyDaypart"
        Me.cmdCopyDaypart.Size = New System.Drawing.Size(22, 20)
        Me.cmdCopyDaypart.TabIndex = 18
        Me.cmdCopyDaypart.UseVisualStyleBackColor = true
        '
        'cmdDown
        '
        Me.cmdDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdDown.FlatAppearance.BorderSize = 0
        Me.cmdDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDown.Image = Global.clTrinity.My.Resources.Resources.arrow_down_20x20
        Me.cmdDown.Location = New System.Drawing.Point(306, 122)
        Me.cmdDown.Name = "cmdDown"
        Me.cmdDown.Size = New System.Drawing.Size(22, 20)
        Me.cmdDown.TabIndex = 16
        Me.cmdDown.UseVisualStyleBackColor = true
        '
        'cmdUp
        '
        Me.cmdUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdUp.FlatAppearance.BorderSize = 0
        Me.cmdUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUp.Image = Global.clTrinity.My.Resources.Resources.arrow_up_20x20
        Me.cmdUp.Location = New System.Drawing.Point(306, 96)
        Me.cmdUp.Name = "cmdUp"
        Me.cmdUp.Size = New System.Drawing.Size(22, 20)
        Me.cmdUp.TabIndex = 15
        Me.cmdUp.UseVisualStyleBackColor = true
        '
        'cmdDelete
        '
        Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdDelete.FlatAppearance.BorderSize = 0
        Me.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDelete.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdDelete.Location = New System.Drawing.Point(306, 70)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(22, 20)
        Me.cmdDelete.TabIndex = 14
        Me.cmdDelete.UseVisualStyleBackColor = true
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.FlatAppearance.BorderSize = 0
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.cmdAdd.Location = New System.Drawing.Point(306, 44)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(22, 20)
        Me.cmdAdd.TabIndex = 13
        Me.cmdAdd.UseVisualStyleBackColor = true
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.clock_2_24x24
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = false
        '
        'frmDefineDayparts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(358, 264)
        Me.Controls.Add(Me.cmdSaveToFile)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.grpDayparts)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmDefineDayparts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Define dayparts"
        Me.grpDayparts.ResumeLayout(false)
        CType(Me.grdDayparts,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents grpDayparts As System.Windows.Forms.GroupBox
    Friend WithEvents grdDayparts As System.Windows.Forms.DataGridView
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdSaveToFile As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdDown As System.Windows.Forms.Button
    Friend WithEvents cmdUp As System.Windows.Forms.Button
    Friend WithEvents cmbChannel As System.Windows.Forms.ComboBox
    Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStart As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEnd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPrime As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cmdCopyDaypart As System.Windows.Forms.Button
End Class
