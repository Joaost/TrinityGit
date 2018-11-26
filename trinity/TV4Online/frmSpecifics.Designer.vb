<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSpecifics
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSpecifics))
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.cmdAutoHide = New System.Windows.Forms.ToolStripButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCurrentRelease = New System.Windows.Forms.Label()
        Me.grdSpecifics = New TV4Online.SummaryDataGridView()
        Me.colSelected = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colProg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRating = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNetPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFilmcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFilmLength = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSurcharges = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip1.SuspendLayout
        CType(Me.grdSpecifics,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle1.Format = "d"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewTextBoxColumn1.Frozen = true
        Me.DataGridViewTextBoxColumn1.HeaderText = "Date"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = true
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Time"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = true
        Me.DataGridViewTextBoxColumn2.Width = 60
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.HeaderText = "Programme"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = true
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn4.HeaderText = "Est"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = true
        Me.DataGridViewTextBoxColumn4.Width = 50
        '
        'DataGridViewTextBoxColumn5
        '
        DataGridViewCellStyle3.Format = "P1"
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn5.HeaderText = "Disc."
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = true
        Me.DataGridViewTextBoxColumn5.Width = 50
        '
        'DataGridViewTextBoxColumn6
        '
        DataGridViewCellStyle4.Format = "C0"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn6.HeaderText = "Net price"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = true
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "Film code"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = true
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "Length"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = true
        Me.DataGridViewTextBoxColumn8.Width = 60
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "Surcharges"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = true
        Me.DataGridViewTextBoxColumn9.Width = 150
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdAutoHide})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(846, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmdAutoHide
        '
        Me.cmdAutoHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdAutoHide.Image = Global.TV4Online.My.Resources.Resources.calender_2_small
        Me.cmdAutoHide.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAutoHide.Name = "cmdAutoHide"
        Me.cmdAutoHide.Size = New System.Drawing.Size(23, 22)
        Me.cmdAutoHide.ToolTipText = "Hide all spots not belonging to this release period"
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(94, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Current release:"
        '
        'lblCurrentRelease
        '
        Me.lblCurrentRelease.AutoSize = true
        Me.lblCurrentRelease.Location = New System.Drawing.Point(188, 9)
        Me.lblCurrentRelease.Name = "lblCurrentRelease"
        Me.lblCurrentRelease.Size = New System.Drawing.Size(13, 13)
        Me.lblCurrentRelease.TabIndex = 3
        Me.lblCurrentRelease.Text = "0"
        '
        'grdSpecifics
        '
        Me.grdSpecifics.AllowUserToAddRows = false
        Me.grdSpecifics.AllowUserToDeleteRows = false
        Me.grdSpecifics.AllowUserToResizeColumns = false
        Me.grdSpecifics.AllowUserToResizeRows = false
        Me.grdSpecifics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdSpecifics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSpecifics.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSelected, Me.colDate, Me.colTime, Me.colProg, Me.colRating, Me.colDiscount, Me.colNetPrice, Me.colFilmcode, Me.colFilmLength, Me.colSurcharges})
        Me.grdSpecifics.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdSpecifics.HasSummaryRow = false
        Me.grdSpecifics.Location = New System.Drawing.Point(0, 28)
        Me.grdSpecifics.Name = "grdSpecifics"
        Me.grdSpecifics.RowHeadersVisible = false
        Me.grdSpecifics.SelectColumn = Nothing
        Me.grdSpecifics.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSpecifics.Size = New System.Drawing.Size(846, 364)
        Me.grdSpecifics.TabIndex = 0
        Me.grdSpecifics.VirtualMode = true
        '
        'colSelected
        '
        Me.colSelected.Frozen = true
        Me.colSelected.HeaderText = ""
        Me.colSelected.Name = "colSelected"
        Me.colSelected.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colSelected.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colSelected.Width = 20
        '
        'colDate
        '
        DataGridViewCellStyle5.Format = "d"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.colDate.DefaultCellStyle = DataGridViewCellStyle5
        Me.colDate.Frozen = true
        Me.colDate.HeaderText = "Date"
        Me.colDate.Name = "colDate"
        Me.colDate.ReadOnly = true
        '
        'colTime
        '
        Me.colTime.HeaderText = "Time"
        Me.colTime.Name = "colTime"
        Me.colTime.ReadOnly = true
        Me.colTime.Width = 60
        '
        'colProg
        '
        Me.colProg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colProg.HeaderText = "Programme"
        Me.colProg.Name = "colProg"
        Me.colProg.ReadOnly = true
        '
        'colRating
        '
        DataGridViewCellStyle6.Format = "N1"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.colRating.DefaultCellStyle = DataGridViewCellStyle6
        Me.colRating.HeaderText = "Est"
        Me.colRating.Name = "colRating"
        Me.colRating.ReadOnly = true
        Me.colRating.Width = 50
        '
        'colDiscount
        '
        DataGridViewCellStyle7.Format = "P1"
        Me.colDiscount.DefaultCellStyle = DataGridViewCellStyle7
        Me.colDiscount.HeaderText = "Disc."
        Me.colDiscount.Name = "colDiscount"
        Me.colDiscount.ReadOnly = true
        Me.colDiscount.Width = 50
        '
        'colNetPrice
        '
        DataGridViewCellStyle8.Format = "C0"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.colNetPrice.DefaultCellStyle = DataGridViewCellStyle8
        Me.colNetPrice.HeaderText = "Net price"
        Me.colNetPrice.Name = "colNetPrice"
        Me.colNetPrice.ReadOnly = true
        '
        'colFilmcode
        '
        Me.colFilmcode.HeaderText = "Film code"
        Me.colFilmcode.Name = "colFilmcode"
        Me.colFilmcode.ReadOnly = true
        '
        'colFilmLength
        '
        Me.colFilmLength.HeaderText = "Length"
        Me.colFilmLength.Name = "colFilmLength"
        Me.colFilmLength.ReadOnly = true
        Me.colFilmLength.Width = 60
        '
        'colSurcharges
        '
        Me.colSurcharges.HeaderText = "Surcharges"
        Me.colSurcharges.Name = "colSurcharges"
        Me.colSurcharges.ReadOnly = true
        Me.colSurcharges.Width = 150
        '
        'frmSpecifics
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(846, 392)
        Me.Controls.Add(Me.lblCurrentRelease)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.grdSpecifics)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmSpecifics"
        Me.Text = "Specifics"
        Me.ToolStrip1.ResumeLayout(false)
        Me.ToolStrip1.PerformLayout
        CType(Me.grdSpecifics,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents grdSpecifics As SummaryDataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSelected As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRating As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDiscount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNetPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFilmcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFilmLength As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSurcharges As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdAutoHide As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblCurrentRelease As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
End Class
