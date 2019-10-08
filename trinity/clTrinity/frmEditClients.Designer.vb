<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditClients
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditClients))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.grdClients = New System.Windows.Forms.DataGridView()
        Me.colStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEnd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlanner = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBuyer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSaved = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colLocked = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDelete = New System.Windows.Forms.DataGridViewImageColumn()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdClients, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.search_2_16x16
        Me.PictureBox1.Location = New System.Drawing.Point(20, 33)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 30
        Me.PictureBox1.TabStop = False
        '
        'btnSave
        '
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Image = Global.clTrinity.My.Resources.Resources.save_2_small
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(493, 392)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 28)
        Me.btnSave.TabIndex = 29
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'grdClients
        '
        Me.grdClients.AllowUserToAddRows = False
        Me.grdClients.AllowUserToDeleteRows = False
        Me.grdClients.AllowUserToResizeColumns = False
        Me.grdClients.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdClients.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdClients.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdClients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grdClients.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        Me.grdClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdClients.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colStart, Me.colEnd, Me.colName, Me.colStatus, Me.colPlanner, Me.colBuyer, Me.colSaved, Me.colLocked, Me.colDelete})
        Me.grdClients.Location = New System.Drawing.Point(6, 55)
        Me.grdClients.MultiSelect = False
        Me.grdClients.Name = "grdClients"
        Me.grdClients.ReadOnly = True
        Me.grdClients.RowHeadersVisible = False
        Me.grdClients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdClients.Size = New System.Drawing.Size(553, 331)
        Me.grdClients.TabIndex = 28
        Me.grdClients.TabStop = False
        Me.grdClients.VirtualMode = True
        '
        'colStart
        '
        Me.colStart.FillWeight = 20.0!
        Me.colStart.HeaderText = "Start"
        Me.colStart.Name = "colStart"
        Me.colStart.ReadOnly = True
        Me.colStart.Visible = False
        '
        'colEnd
        '
        Me.colEnd.FillWeight = 20.0!
        Me.colEnd.HeaderText = "End"
        Me.colEnd.Name = "colEnd"
        Me.colEnd.ReadOnly = True
        Me.colEnd.Visible = False
        '
        'colName
        '
        Me.colName.FillWeight = 50.0!
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        Me.colName.ReadOnly = True
        Me.colName.Visible = False
        '
        'colStatus
        '
        Me.colStatus.FillWeight = 15.0!
        Me.colStatus.HeaderText = "Status"
        Me.colStatus.Name = "colStatus"
        Me.colStatus.ReadOnly = True
        Me.colStatus.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colStatus.Visible = False
        '
        'colPlanner
        '
        Me.colPlanner.FillWeight = 20.0!
        Me.colPlanner.HeaderText = "Planner"
        Me.colPlanner.Name = "colPlanner"
        Me.colPlanner.ReadOnly = True
        Me.colPlanner.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colPlanner.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colPlanner.Visible = False
        '
        'colBuyer
        '
        Me.colBuyer.FillWeight = 20.0!
        Me.colBuyer.HeaderText = "Buyer"
        Me.colBuyer.Name = "colBuyer"
        Me.colBuyer.ReadOnly = True
        Me.colBuyer.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colBuyer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colBuyer.Visible = False
        '
        'colSaved
        '
        Me.colSaved.FillWeight = 35.0!
        Me.colSaved.HeaderText = "Saved"
        Me.colSaved.Name = "colSaved"
        Me.colSaved.ReadOnly = True
        Me.colSaved.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colSaved.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colSaved.Visible = False
        '
        'colLocked
        '
        Me.colLocked.FillWeight = 15.0!
        Me.colLocked.HeaderText = "Locked"
        Me.colLocked.Name = "colLocked"
        Me.colLocked.ReadOnly = True
        Me.colLocked.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colLocked.Visible = False
        '
        'colDelete
        '
        Me.colDelete.FillWeight = 15.0!
        Me.colDelete.HeaderText = "Delete"
        Me.colDelete.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.colDelete.MinimumWidth = 20
        Me.colDelete.Name = "colDelete"
        Me.colDelete.ReadOnly = True
        Me.colDelete.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'btnCancel
        '
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(415, 392)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(68, 28)
        Me.btnCancel.TabIndex = 27
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(42, 30)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(328, 20)
        Me.txtSearch.TabIndex = 26
        '
        'frmEditClients
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(585, 450)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grdClients)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtSearch)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmEditClients"
        Me.Text = "Edit clients"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdClients, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As Windows.Forms.PictureBox
    Friend WithEvents btnSave As Windows.Forms.Button
    Friend WithEvents grdClients As System.Windows.Forms.DataGridView
    Friend WithEvents colStart As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEnd As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colName As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStatus As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanner As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBuyer As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSaved As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLocked As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDelete As Windows.Forms.DataGridViewImageColumn
    Friend WithEvents btnCancel As Windows.Forms.Button
    Friend WithEvents txtSearch As Windows.Forms.TextBox
    Friend WithEvents tmrKeypress As System.Windows.Forms.Timer
End Class
