<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRemoveSpotts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRemoveSpotts))
        Me.grdSpotts = New System.Windows.Forms.DataGridView()
        Me.colSelected = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTRP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdRemoveSpotts = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.grdSpotts,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'grdSpotts
        '
        Me.grdSpotts.AllowDrop = true
        Me.grdSpotts.AllowUserToAddRows = false
        Me.grdSpotts.AllowUserToDeleteRows = false
        Me.grdSpotts.AllowUserToResizeColumns = false
        Me.grdSpotts.AllowUserToResizeRows = false
        Me.grdSpotts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdSpotts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSpotts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSelected, Me.colDate, Me.colTRP})
        Me.grdSpotts.Location = New System.Drawing.Point(0, 0)
        Me.grdSpotts.Name = "grdSpotts"
        Me.grdSpotts.RowHeadersVisible = false
        Me.grdSpotts.Size = New System.Drawing.Size(492, 305)
        Me.grdSpotts.TabIndex = 0
        Me.grdSpotts.VirtualMode = true
        '
        'colSelected
        '
        Me.colSelected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colSelected.FillWeight = 20!
        Me.colSelected.HeaderText = ""
        Me.colSelected.Name = "colSelected"
        Me.colSelected.ReadOnly = true
        Me.colSelected.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colSelected.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'colDate
        '
        Me.colDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colDate.HeaderText = "Date"
        Me.colDate.Name = "colDate"
        Me.colDate.ReadOnly = true
        '
        'colTRP
        '
        Me.colTRP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colTRP.HeaderText = "TRP"
        Me.colTRP.Name = "colTRP"
        Me.colTRP.ReadOnly = true
        '
        'cmdRemoveSpotts
        '
        Me.cmdRemoveSpotts.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveSpotts.FlatAppearance.BorderSize = 0
        Me.cmdRemoveSpotts.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRemoveSpotts.Location = New System.Drawing.Point(361, 319)
        Me.cmdRemoveSpotts.Name = "cmdRemoveSpotts"
        Me.cmdRemoveSpotts.Size = New System.Drawing.Size(119, 35)
        Me.cmdRemoveSpotts.TabIndex = 1
        Me.cmdRemoveSpotts.Text = "Remove spotts"
        Me.cmdRemoveSpotts.UseVisualStyleBackColor = true
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Date"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = true
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.HeaderText = "TRP"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = true
        '
        'frmRemoveSpotts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 371)
        Me.Controls.Add(Me.cmdRemoveSpotts)
        Me.Controls.Add(Me.grdSpotts)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MaximumSize = New System.Drawing.Size(508, 410)
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(508, 410)
        Me.Name = "frmRemoveSpotts"
        Me.Text = "Remove spotts"
        CType(Me.grdSpotts,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents grdSpotts As Windows.Forms.DataGridView
    Friend WithEvents cmdRemoveSpotts As Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSelected As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colDate As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTRP As Windows.Forms.DataGridViewTextBoxColumn
End Class
