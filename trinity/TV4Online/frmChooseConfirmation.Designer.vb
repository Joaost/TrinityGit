<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChooseConfirmation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChooseConfirmation))
        Me.grdConfirmations = New System.Windows.Forms.DataGridView()
        Me.colName = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.colBookingType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStartDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEndDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBookedBudget = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colVersionNr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChangedDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.grdConfirmations,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'grdConfirmations
        '
        Me.grdConfirmations.AllowUserToAddRows = false
        Me.grdConfirmations.AllowUserToDeleteRows = false
        Me.grdConfirmations.AllowUserToResizeColumns = false
        Me.grdConfirmations.AllowUserToResizeRows = false
        Me.grdConfirmations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdConfirmations.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colBookingType, Me.colStartDate, Me.colEndDate, Me.colBookedBudget, Me.colVersionNr, Me.colChangedDate})
        Me.grdConfirmations.GridColor = System.Drawing.SystemColors.ControlDarkDark
        Me.grdConfirmations.Location = New System.Drawing.Point(0, 26)
        Me.grdConfirmations.MultiSelect = false
        Me.grdConfirmations.Name = "grdConfirmations"
        Me.grdConfirmations.RowHeadersVisible = false
        Me.grdConfirmations.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdConfirmations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdConfirmations.Size = New System.Drawing.Size(989, 260)
        Me.grdConfirmations.TabIndex = 0
        Me.grdConfirmations.VirtualMode = true
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colName.FillWeight = 200!
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        Me.colName.ReadOnly = true
        Me.colName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'colBookingType
        '
        Me.colBookingType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colBookingType.FillWeight = 35!
        Me.colBookingType.HeaderText = "Type"
        Me.colBookingType.Name = "colBookingType"
        Me.colBookingType.ReadOnly = true
        '
        'colStartDate
        '
        Me.colStartDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colStartDate.HeaderText = "Start date"
        Me.colStartDate.Name = "colStartDate"
        Me.colStartDate.ReadOnly = true
        '
        'colEndDate
        '
        Me.colEndDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colEndDate.HeaderText = "End date"
        Me.colEndDate.Name = "colEndDate"
        Me.colEndDate.ReadOnly = true
        '
        'colBookedBudget
        '
        Me.colBookedBudget.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle1.Format = "#,###"
        Me.colBookedBudget.DefaultCellStyle = DataGridViewCellStyle1
        Me.colBookedBudget.HeaderText = "Booked budget"
        Me.colBookedBudget.Name = "colBookedBudget"
        Me.colBookedBudget.ReadOnly = true
        '
        'colVersionNr
        '
        Me.colVersionNr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colVersionNr.FillWeight = 60!
        Me.colVersionNr.HeaderText = "Version"
        Me.colVersionNr.Name = "colVersionNr"
        Me.colVersionNr.ReadOnly = true
        '
        'colChangedDate
        '
        Me.colChangedDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colChangedDate.HeaderText = "Changed date"
        Me.colChangedDate.Name = "colChangedDate"
        Me.colChangedDate.ReadOnly = true
        Me.colChangedDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(989, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.FillWeight = 200!
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = true
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.FillWeight = 35!
        Me.DataGridViewTextBoxColumn2.HeaderText = "End date"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = true
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.HeaderText = "Start date"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = true
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn4.HeaderText = "Net budget"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = true
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn5.HeaderText = "Latest changed date"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = true
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.HeaderText = "Changed date"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'frmChooseConfirmation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(989, 287)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.grdConfirmations)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MaximumSize = New System.Drawing.Size(1005, 326)
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(1005, 326)
        Me.Name = "frmChooseConfirmation"
        Me.Text = "Confirmations"
        CType(Me.grdConfirmations,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents grdConfirmations As Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents DataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colName As Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents colBookingType As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStartDate As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEndDate As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBookedBudget As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colVersionNr As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChangedDate As Windows.Forms.DataGridViewTextBoxColumn
End Class
