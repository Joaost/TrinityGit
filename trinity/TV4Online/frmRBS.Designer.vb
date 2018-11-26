<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRBS
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRBS))
        Me.grdRBS = New TV4Online.SummaryDataGridView()
        Me.colSelected = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colWeek = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEnd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDaypart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFilmcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colLength = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCPP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIndices = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTRP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNetPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.cmdAutoHide = New System.Windows.Forms.ToolStripButton()
        Me.lblCurrentRelease = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.grdRBS,System.ComponentModel.ISupportInitialize).BeginInit
        Me.ToolStrip1.SuspendLayout
        Me.SuspendLayout
        '
        'grdRBS
        '
        Me.grdRBS.AllowUserToAddRows = false
        Me.grdRBS.AllowUserToDeleteRows = false
        Me.grdRBS.AllowUserToResizeColumns = false
        Me.grdRBS.AllowUserToResizeRows = false
        Me.grdRBS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdRBS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdRBS.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSelected, Me.colWeek, Me.colStart, Me.colEnd, Me.colDaypart, Me.colFilmcode, Me.colLength, Me.colCPP, Me.colDiscount, Me.colIndices, Me.colTRP, Me.colNetPrice})
        Me.grdRBS.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdRBS.HasSummaryRow = false
        Me.grdRBS.Location = New System.Drawing.Point(0, 28)
        Me.grdRBS.Name = "grdRBS"
        Me.grdRBS.RowHeadersVisible = false
        Me.grdRBS.SelectColumn = Nothing
        Me.grdRBS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdRBS.Size = New System.Drawing.Size(881, 294)
        Me.grdRBS.TabIndex = 0
        Me.grdRBS.VirtualMode = true
        '
        'colSelected
        '
        Me.colSelected.Frozen = true
        Me.colSelected.HeaderText = ""
        Me.colSelected.Name = "colSelected"
        Me.colSelected.Width = 20
        '
        'colWeek
        '
        Me.colWeek.HeaderText = "Week"
        Me.colWeek.Name = "colWeek"
        Me.colWeek.ReadOnly = true
        Me.colWeek.Width = 40
        '
        'colStart
        '
        Me.colStart.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle1.Format = "d"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.colStart.DefaultCellStyle = DataGridViewCellStyle1
        Me.colStart.HeaderText = "Start"
        Me.colStart.Name = "colStart"
        '
        'colEnd
        '
        Me.colEnd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle2.Format = "d"
        Me.colEnd.DefaultCellStyle = DataGridViewCellStyle2
        Me.colEnd.HeaderText = "End"
        Me.colEnd.Name = "colEnd"
        '
        'colDaypart
        '
        Me.colDaypart.HeaderText = "Daypart"
        Me.colDaypart.Name = "colDaypart"
        Me.colDaypart.ReadOnly = true
        Me.colDaypart.Width = 75
        '
        'colFilmcode
        '
        Me.colFilmcode.HeaderText = "Filmcode"
        Me.colFilmcode.Name = "colFilmcode"
        Me.colFilmcode.ReadOnly = true
        '
        'colLength
        '
        Me.colLength.HeaderText = "Length"
        Me.colLength.Name = "colLength"
        Me.colLength.ReadOnly = true
        Me.colLength.Width = 60
        '
        'colCPP
        '
        DataGridViewCellStyle3.Format = "C0"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.colCPP.DefaultCellStyle = DataGridViewCellStyle3
        Me.colCPP.HeaderText = "CPP30"
        Me.colCPP.Name = "colCPP"
        Me.colCPP.ReadOnly = true
        Me.colCPP.Width = 75
        '
        'colDiscount
        '
        DataGridViewCellStyle4.Format = "P1"
        Me.colDiscount.DefaultCellStyle = DataGridViewCellStyle4
        Me.colDiscount.HeaderText = "Discount"
        Me.colDiscount.Name = "colDiscount"
        Me.colDiscount.ReadOnly = true
        Me.colDiscount.Width = 60
        '
        'colIndices
        '
        Me.colIndices.HeaderText = "Indices"
        Me.colIndices.Name = "colIndices"
        Me.colIndices.ReadOnly = true
        '
        'colTRP
        '
        DataGridViewCellStyle5.Format = "N1"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.colTRP.DefaultCellStyle = DataGridViewCellStyle5
        Me.colTRP.HeaderText = "TRP"
        Me.colTRP.Name = "colTRP"
        Me.colTRP.ReadOnly = true
        Me.colTRP.Width = 60
        '
        'colNetPrice
        '
        DataGridViewCellStyle6.Format = "C0"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.colNetPrice.DefaultCellStyle = DataGridViewCellStyle6
        Me.colNetPrice.HeaderText = "Net price"
        Me.colNetPrice.Name = "colNetPrice"
        Me.colNetPrice.ReadOnly = true
        Me.colNetPrice.Width = 80
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdAutoHide})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(881, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmdAutoHide
        '
        Me.cmdAutoHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdAutoHide.Image = Global.TV4Online.My.Resources.Resources.calender_2_small
        Me.cmdAutoHide.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAutoHide.Name = "cmdAutoHide"
        Me.cmdAutoHide.Size = New System.Drawing.Size(23, 22)
        Me.cmdAutoHide.ToolTipText = "Hide all films not belonging to this release period"
        '
        'lblCurrentRelease
        '
        Me.lblCurrentRelease.AutoSize = true
        Me.lblCurrentRelease.Location = New System.Drawing.Point(176, 9)
        Me.lblCurrentRelease.Name = "lblCurrentRelease"
        Me.lblCurrentRelease.Size = New System.Drawing.Size(13, 13)
        Me.lblCurrentRelease.TabIndex = 5
        Me.lblCurrentRelease.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(82, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Current release:"
        '
        'frmRBS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(881, 322)
        Me.Controls.Add(Me.lblCurrentRelease)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.grdRBS)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmRBS"
        Me.Text = "RBS"
        CType(Me.grdRBS,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolStrip1.ResumeLayout(false)
        Me.ToolStrip1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents grdRBS As SummaryDataGridView
    Friend WithEvents colSelected As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colWeek As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStart As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEnd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDaypart As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFilmcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLength As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCPP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDiscount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIndices As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTRP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNetPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdAutoHide As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblCurrentRelease As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
End Class
