<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEvaluateSpecifics
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEvaluateSpecifics))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.grdSpotlist = New System.Windows.Forms.DataGridView()
        Me.stpSpotlist = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.cmbChannel = New System.Windows.Forms.ToolStripComboBox()
        Me.grdConfirmed = New System.Windows.Forms.DataGridView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdSpotlistColumns = New System.Windows.Forms.ToolStripButton()
        Me.cmdRemoveMatchings = New System.Windows.Forms.ToolStripButton()
        Me.cmdConfirmedFilter = New System.Windows.Forms.ToolStripDropDownButton()
        Me.FiltersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdConfirmedColumns = New System.Windows.Forms.ToolStripButton()
        Me.cmdImportSchedule = New System.Windows.Forms.ToolStripButton()
        Me.cmdEstimate = New System.Windows.Forms.ToolStripDropDownButton()
        Me.UseSamePeriodLastYearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UseLastWeeksOfDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdFilm = New System.Windows.Forms.ToolStripDropDownButton()
        Me.FilmsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdBookingtype = New System.Windows.Forms.ToolStripDropDownButton()
        Me.BookingtypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdAccept = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer1.Panel1.SuspendLayout
        Me.SplitContainer1.Panel2.SuspendLayout
        Me.SplitContainer1.SuspendLayout
        CType(Me.grdSpotlist,System.ComponentModel.ISupportInitialize).BeginInit
        Me.stpSpotlist.SuspendLayout
        CType(Me.grdConfirmed,System.ComponentModel.ISupportInitialize).BeginInit
        Me.ToolStrip1.SuspendLayout
        Me.SuspendLayout
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.grdSpotlist)
        Me.SplitContainer1.Panel1.Controls.Add(Me.stpSpotlist)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.grdConfirmed)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ToolStrip1)
        Me.SplitContainer1.Size = New System.Drawing.Size(750, 500)
        Me.SplitContainer1.SplitterDistance = 245
        Me.SplitContainer1.TabIndex = 0
        '
        'grdSpotlist
        '
        Me.grdSpotlist.AllowUserToAddRows = false
        Me.grdSpotlist.AllowUserToDeleteRows = false
        Me.grdSpotlist.AllowUserToOrderColumns = true
        Me.grdSpotlist.AllowUserToResizeRows = false
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ControlLight
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.grdSpotlist.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.grdSpotlist.BackgroundColor = System.Drawing.Color.Silver
        Me.grdSpotlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSpotlist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSpotlist.Location = New System.Drawing.Point(0, 25)
        Me.grdSpotlist.Name = "grdSpotlist"
        Me.grdSpotlist.ReadOnly = true
        Me.grdSpotlist.RowHeadersVisible = false
        Me.grdSpotlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSpotlist.Size = New System.Drawing.Size(750, 220)
        Me.grdSpotlist.TabIndex = 3
        Me.grdSpotlist.VirtualMode = true
        '
        'stpSpotlist
        '
        Me.stpSpotlist.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.stpSpotlist.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel2, Me.ToolStripSeparator8, Me.cmdSpotlistColumns, Me.ToolStripSeparator2, Me.ToolStripLabel3, Me.cmbChannel, Me.cmdRemoveMatchings})
        Me.stpSpotlist.Location = New System.Drawing.Point(0, 0)
        Me.stpSpotlist.Name = "stpSpotlist"
        Me.stpSpotlist.Size = New System.Drawing.Size(750, 25)
        Me.stpSpotlist.TabIndex = 2
        Me.stpSpotlist.Text = "ToolStrip2"
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.AutoSize = false
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(56, 22)
        Me.ToolStripLabel2.Text = "Booked"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(54, 22)
        Me.ToolStripLabel3.Text = "Channel:"
        '
        'cmbChannel
        '
        Me.cmbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChannel.Name = "cmbChannel"
        Me.cmbChannel.Size = New System.Drawing.Size(121, 25)
        '
        'grdConfirmed
        '
        Me.grdConfirmed.AllowUserToAddRows = false
        Me.grdConfirmed.AllowUserToDeleteRows = false
        Me.grdConfirmed.AllowUserToOrderColumns = true
        Me.grdConfirmed.AllowUserToResizeRows = false
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlLight
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.grdConfirmed.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        Me.grdConfirmed.BackgroundColor = System.Drawing.Color.Silver
        Me.grdConfirmed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdConfirmed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdConfirmed.Location = New System.Drawing.Point(0, 25)
        Me.grdConfirmed.Name = "grdConfirmed"
        Me.grdConfirmed.RowHeadersVisible = false
        Me.grdConfirmed.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdConfirmed.Size = New System.Drawing.Size(750, 226)
        Me.grdConfirmed.TabIndex = 3
        Me.grdConfirmed.VirtualMode = true
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.ToolStripSeparator5, Me.cmdConfirmedFilter, Me.cmdConfirmedColumns, Me.ToolStripSeparator3, Me.cmdImportSchedule, Me.cmdEstimate, Me.ToolStripSeparator4, Me.cmdFilm, Me.cmdBookingtype, Me.ToolStripSeparator1, Me.cmdAccept})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(750, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(64, 22)
        Me.ToolStripLabel1.Text = "Confirmed"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'cmdSpotlistColumns
        '
        Me.cmdSpotlistColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSpotlistColumns.Image = CType(resources.GetObject("cmdSpotlistColumns.Image"),System.Drawing.Image)
        Me.cmdSpotlistColumns.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSpotlistColumns.Name = "cmdSpotlistColumns"
        Me.cmdSpotlistColumns.Size = New System.Drawing.Size(23, 22)
        Me.cmdSpotlistColumns.Text = "ToolStripButton5"
        Me.cmdSpotlistColumns.ToolTipText = "Define columns for spotlist"
        '
        'cmdRemoveMatchings
        '
        Me.cmdRemoveMatchings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdRemoveMatchings.Image = Global.clTrinity.My.Resources.Resources.delete_3
        Me.cmdRemoveMatchings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdRemoveMatchings.Name = "cmdRemoveMatchings"
        Me.cmdRemoveMatchings.Size = New System.Drawing.Size(23, 22)
        Me.cmdRemoveMatchings.Text = "ToolStripButton1"
        '
        'cmdConfirmedFilter
        '
        Me.cmdConfirmedFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdConfirmedFilter.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FiltersToolStripMenuItem})
        Me.cmdConfirmedFilter.Image = Global.clTrinity.My.Resources.Resources.miscellaneous_2_16x16
        Me.cmdConfirmedFilter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdConfirmedFilter.Name = "cmdConfirmedFilter"
        Me.cmdConfirmedFilter.Size = New System.Drawing.Size(29, 22)
        Me.cmdConfirmedFilter.Text = "ToolStripDropDownButton1"
        Me.cmdConfirmedFilter.ToolTipText = "Filter spots"
        '
        'FiltersToolStripMenuItem
        '
        Me.FiltersToolStripMenuItem.Name = "FiltersToolStripMenuItem"
        Me.FiltersToolStripMenuItem.Size = New System.Drawing.Size(105, 22)
        Me.FiltersToolStripMenuItem.Text = "Filters"
        '
        'cmdConfirmedColumns
        '
        Me.cmdConfirmedColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdConfirmedColumns.Image = CType(resources.GetObject("cmdConfirmedColumns.Image"),System.Drawing.Image)
        Me.cmdConfirmedColumns.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdConfirmedColumns.Name = "cmdConfirmedColumns"
        Me.cmdConfirmedColumns.Size = New System.Drawing.Size(23, 22)
        Me.cmdConfirmedColumns.Text = "ToolStripButton2"
        Me.cmdConfirmedColumns.ToolTipText = "Select columns"
        '
        'cmdImportSchedule
        '
        Me.cmdImportSchedule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdImportSchedule.Image = Global.clTrinity.My.Resources.Resources.add_more_2_small_org
        Me.cmdImportSchedule.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdImportSchedule.Name = "cmdImportSchedule"
        Me.cmdImportSchedule.Size = New System.Drawing.Size(23, 22)
        Me.cmdImportSchedule.Text = "ToolStripButton7"
        Me.cmdImportSchedule.ToolTipText = "Import channel schedule"
        '
        'cmdEstimate
        '
        Me.cmdEstimate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdEstimate.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UseSamePeriodLastYearToolStripMenuItem, Me.UseLastWeeksOfDataToolStripMenuItem})
        Me.cmdEstimate.Enabled = false
        Me.cmdEstimate.Image = Global.clTrinity.My.Resources.Resources.magic_wand_2_16_x16
        Me.cmdEstimate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdEstimate.Name = "cmdEstimate"
        Me.cmdEstimate.Size = New System.Drawing.Size(29, 22)
        Me.cmdEstimate.Text = "ToolStripDropDownButton1"
        '
        'UseSamePeriodLastYearToolStripMenuItem
        '
        Me.UseSamePeriodLastYearToolStripMenuItem.Name = "UseSamePeriodLastYearToolStripMenuItem"
        Me.UseSamePeriodLastYearToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
        Me.UseSamePeriodLastYearToolStripMenuItem.Text = "Use same period last year"
        '
        'UseLastWeeksOfDataToolStripMenuItem
        '
        Me.UseLastWeeksOfDataToolStripMenuItem.Name = "UseLastWeeksOfDataToolStripMenuItem"
        Me.UseLastWeeksOfDataToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
        Me.UseLastWeeksOfDataToolStripMenuItem.Text = "Use last weeks of data"
        '
        'cmdFilm
        '
        Me.cmdFilm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdFilm.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilmsToolStripMenuItem})
        Me.cmdFilm.Image = Global.clTrinity.My.Resources.Resources.film_3_small
        Me.cmdFilm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdFilm.Name = "cmdFilm"
        Me.cmdFilm.Size = New System.Drawing.Size(29, 22)
        Me.cmdFilm.Text = "ToolStripDropDownButton1"
        Me.cmdFilm.ToolTipText = "Change film on spot"
        '
        'FilmsToolStripMenuItem
        '
        Me.FilmsToolStripMenuItem.Name = "FilmsToolStripMenuItem"
        Me.FilmsToolStripMenuItem.Size = New System.Drawing.Size(102, 22)
        Me.FilmsToolStripMenuItem.Text = "Films"
        '
        'cmdBookingtype
        '
        Me.cmdBookingtype.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdBookingtype.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BookingtypeToolStripMenuItem})
        Me.cmdBookingtype.Image = Global.clTrinity.My.Resources.Resources.display_2_20x20
        Me.cmdBookingtype.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdBookingtype.Name = "cmdBookingtype"
        Me.cmdBookingtype.Size = New System.Drawing.Size(29, 22)
        Me.cmdBookingtype.Text = "ToolStripDropDownButton2"
        Me.cmdBookingtype.ToolTipText = "Change bookingtype on spot"
        '
        'BookingtypeToolStripMenuItem
        '
        Me.BookingtypeToolStripMenuItem.Name = "BookingtypeToolStripMenuItem"
        Me.BookingtypeToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.BookingtypeToolStripMenuItem.Text = "Bookingtypes"
        '
        'cmdAccept
        '
        Me.cmdAccept.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdAccept.Image = Global.clTrinity.My.Resources.Resources.check_2_16x16
        Me.cmdAccept.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAccept.Name = "cmdAccept"
        Me.cmdAccept.Size = New System.Drawing.Size(23, 22)
        Me.cmdAccept.Text = "ToolStripButton1"
        Me.cmdAccept.ToolTipText = "Accept confirmed spots and transfer them to booking"
        '
        'frmEvaluateSpecifics
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(750, 500)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmEvaluateSpecifics"
        Me.Text = "Evaluate specifics"
        Me.SplitContainer1.Panel1.ResumeLayout(false)
        Me.SplitContainer1.Panel1.PerformLayout
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        Me.SplitContainer1.Panel2.PerformLayout
        Me.SplitContainer1.ResumeLayout(false)
        CType(Me.grdSpotlist,System.ComponentModel.ISupportInitialize).EndInit
        Me.stpSpotlist.ResumeLayout(false)
        Me.stpSpotlist.PerformLayout
        CType(Me.grdConfirmed,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolStrip1.ResumeLayout(false)
        Me.ToolStrip1.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents grdSpotlist As System.Windows.Forms.DataGridView
    Friend WithEvents stpSpotlist As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdSpotlistColumns As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents grdConfirmed As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdConfirmedFilter As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents FiltersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdConfirmedColumns As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdImportSchedule As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdAccept As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripLabel3 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmbChannel As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents cmdEstimate As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents UseSamePeriodLastYearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UseLastWeeksOfDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdFilm As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents FilmsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdBookingtype As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents BookingtypeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdRemoveMatchings As System.Windows.Forms.ToolStripButton
End Class
