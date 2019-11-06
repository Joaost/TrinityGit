<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPeople
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPeople))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.grdPeople = New System.Windows.Forms.DataGridView()
        Me.colStatus = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cmdDeletePeople = New System.Windows.Forms.Button()
        Me.cmdAddPeople = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPhone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEmail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPeople, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.people_2
        Me.PictureBox1.Location = New System.Drawing.Point(13, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'grdPeople
        '
        Me.grdPeople.AllowUserToAddRows = False
        Me.grdPeople.AllowUserToDeleteRows = False
        Me.grdPeople.AllowUserToResizeColumns = False
        Me.grdPeople.AllowUserToResizeRows = False
        Me.grdPeople.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPeople.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colPhone, Me.colEmail, Me.colStatus})
        Me.grdPeople.Location = New System.Drawing.Point(13, 58)
        Me.grdPeople.Name = "grdPeople"
        Me.grdPeople.RowHeadersVisible = False
        Me.grdPeople.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPeople.Size = New System.Drawing.Size(542, 192)
        Me.grdPeople.TabIndex = 1
        Me.grdPeople.VirtualMode = True
        '
        'colStatus
        '
        Me.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colStatus.FillWeight = 30.0!
        Me.colStatus.HeaderText = "Active"
        Me.colStatus.Name = "colStatus"
        Me.colStatus.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'cmdDeletePeople
        '
        Me.cmdDeletePeople.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDeletePeople.FlatAppearance.BorderSize = 0
        Me.cmdDeletePeople.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDeletePeople.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdDeletePeople.Location = New System.Drawing.Point(561, 84)
        Me.cmdDeletePeople.Name = "cmdDeletePeople"
        Me.cmdDeletePeople.Size = New System.Drawing.Size(22, 20)
        Me.cmdDeletePeople.TabIndex = 14
        Me.cmdDeletePeople.UseVisualStyleBackColor = True
        '
        'cmdAddPeople
        '
        Me.cmdAddPeople.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddPeople.FlatAppearance.BorderSize = 0
        Me.cmdAddPeople.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddPeople.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.cmdAddPeople.Location = New System.Drawing.Point(561, 58)
        Me.cmdAddPeople.Name = "cmdAddPeople"
        Me.cmdAddPeople.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddPeople.TabIndex = 13
        Me.cmdAddPeople.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.FlatAppearance.BorderSize = 0
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Location = New System.Drawing.Point(465, 256)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(90, 33)
        Me.cmdSave.TabIndex = 15
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.FillWeight = 72.41962!
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.FillWeight = 72.41962!
        Me.DataGridViewTextBoxColumn2.HeaderText = "Phone nr"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.FillWeight = 72.41962!
        Me.DataGridViewTextBoxColumn3.HeaderText = "Email"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colName.FillWeight = 72.41962!
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        '
        'colPhone
        '
        Me.colPhone.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colPhone.FillWeight = 72.41962!
        Me.colPhone.HeaderText = "Phone nr"
        Me.colPhone.Name = "colPhone"
        '
        'colEmail
        '
        Me.colEmail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colEmail.FillWeight = 72.41962!
        Me.colEmail.HeaderText = "Email"
        Me.colEmail.Name = "colEmail"
        '
        'frmPeople
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 301)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdDeletePeople)
        Me.Controls.Add(Me.cmdAddPeople)
        Me.Controls.Add(Me.grdPeople)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MaximumSize = New System.Drawing.Size(610, 340)
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(610, 340)
        Me.Name = "frmPeople"
        Me.Text = "Planners and Buyers"
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdPeople,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents grdPeople As System.Windows.Forms.DataGridView
    Friend WithEvents cmdDeletePeople As System.Windows.Forms.Button
    Friend WithEvents cmdAddPeople As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colName As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPhone As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEmail As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStatus As Windows.Forms.DataGridViewCheckBoxColumn
End Class
