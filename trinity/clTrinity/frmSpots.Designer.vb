<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSpots
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSpots))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.grdConfirmed = New System.Windows.Forms.DataGridView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmbTarget = New System.Windows.Forms.ToolStripComboBox()
        Me.lblConfirmedFiltered = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.grdActual = New System.Windows.Forms.DataGridView()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblActualFiltered = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdConfirmedFilter = New System.Windows.Forms.ToolStripDropDownButton()
        Me.FiltersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdConfirmedColumns = New System.Windows.Forms.ToolStripButton()
        Me.cmdConfirmedExcel = New System.Windows.Forms.ToolStripButton()
        Me.cmdImportSchedule = New System.Windows.Forms.ToolStripButton()
        Me.cmdCreateRBS = New System.Windows.Forms.ToolStripButton()
        Me.cmdAutoMatch = New System.Windows.Forms.ToolStripButton()
        Me.cmdMatch = New System.Windows.Forms.ToolStripButton()
        Me.cmdBreakMatch = New System.Windows.Forms.ToolStripButton()
        Me.cmdBreakAll = New System.Windows.Forms.ToolStripButton()
        Me.cmdConfirmedEstimate = New System.Windows.Forms.ToolStripButton()
        Me.cmdBookingtype = New System.Windows.Forms.ToolStripDropDownButton()
        Me.BookingtypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdFilm = New System.Windows.Forms.ToolStripDropDownButton()
        Me.FilmsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdConfirmedDelete = New System.Windows.Forms.ToolStripButton()
        Me.cmdCheckDuplicates = New System.Windows.Forms.ToolStripButton()
        Me.cmdTimeshift = New System.Windows.Forms.ToolStripDropDownButton()
        Me.DefaultToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.LiveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VOSDAL7ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdIgnoreFaultySpots = New System.Windows.Forms.ToolStripButton()
        Me.cmdActualFilter = New System.Windows.Forms.ToolStripButton()
        Me.cmdActualColumns = New System.Windows.Forms.ToolStripButton()
        Me.cmdActualExcel = New System.Windows.Forms.ToolStripButton()
        Me.cmdActualAutomatch = New System.Windows.Forms.ToolStripButton()
        Me.cmdActualMatch = New System.Windows.Forms.ToolStripButton()
        Me.cmdAddAndMatch = New System.Windows.Forms.ToolStripButton()
        Me.cmdActualBreakMatch = New System.Windows.Forms.ToolStripButton()
        Me.cmdActualBreakMatches = New System.Windows.Forms.ToolStripButton()
        Me.cmdRemoveUnmatchedSpots = New System.Windows.Forms.ToolStripButton()
        Me.cmdActualBookingtype = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton()
        Me.cmdDeleteActual = New System.Windows.Forms.ToolStripButton()
        Me.lblHelp = New System.Windows.Forms.ToolStripLabel()
        Me.cmdFilmcodeFound = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer1.Panel1.SuspendLayout
        Me.SplitContainer1.Panel2.SuspendLayout
        Me.SplitContainer1.SuspendLayout
        CType(Me.grdConfirmed,System.ComponentModel.ISupportInitialize).BeginInit
        Me.ToolStrip1.SuspendLayout
        CType(Me.grdActual,System.ComponentModel.ISupportInitialize).BeginInit
        Me.ToolStrip2.SuspendLayout
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.grdConfirmed)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ToolStrip1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.grdActual)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ToolStrip2)
        Me.SplitContainer1.Size = New System.Drawing.Size(916, 401)
        Me.SplitContainer1.SplitterDistance = 201
        Me.SplitContainer1.TabIndex = 0
        '
        'grdConfirmed
        '
        Me.grdConfirmed.AllowUserToAddRows = false
        Me.grdConfirmed.AllowUserToDeleteRows = false
        Me.grdConfirmed.AllowUserToOrderColumns = true
        Me.grdConfirmed.AllowUserToResizeRows = false
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.grdConfirmed.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdConfirmed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdConfirmed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdConfirmed.Location = New System.Drawing.Point(0, 25)
        Me.grdConfirmed.Name = "grdConfirmed"
        Me.grdConfirmed.RowHeadersVisible = false
        Me.grdConfirmed.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdConfirmed.Size = New System.Drawing.Size(916, 176)
        Me.grdConfirmed.TabIndex = 1
        Me.grdConfirmed.VirtualMode = true
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.ToolStripSeparator5, Me.cmdConfirmedFilter, Me.cmdConfirmedColumns, Me.cmdConfirmedExcel, Me.ToolStripSeparator1, Me.cmdImportSchedule, Me.cmdCreateRBS, Me.cmdAutoMatch, Me.cmdMatch, Me.cmdBreakMatch, Me.cmdBreakAll, Me.cmdConfirmedEstimate, Me.ToolStripSeparator2, Me.cmdBookingtype, Me.cmdFilm, Me.cmdConfirmedDelete, Me.cmdCheckDuplicates, Me.ToolStripSeparator3, Me.cmdTimeshift, Me.cmbTarget, Me.lblConfirmedFiltered, Me.ToolStripLabel3, Me.cmdIgnoreFaultySpots, Me.ToolStripSeparator4})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(916, 25)
        Me.ToolStrip1.TabIndex = 0
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
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'cmbTarget
        '
        Me.cmbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTarget.Items.AddRange(New Object() {"Main Target", "Secondary Target", "Third Target", "Buying Target", "All adults"})
        Me.cmbTarget.Name = "cmbTarget"
        Me.cmbTarget.Size = New System.Drawing.Size(121, 25)
        '
        'lblConfirmedFiltered
        '
        Me.lblConfirmedFiltered.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblConfirmedFiltered.ForeColor = System.Drawing.Color.Red
        Me.lblConfirmedFiltered.Name = "lblConfirmedFiltered"
        Me.lblConfirmedFiltered.Size = New System.Drawing.Size(0, 22)
        Me.lblConfirmedFiltered.Tag = "0"
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(129, 22)
        Me.ToolStripLabel3.Text = "Ignore faulty filmcodes"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'grdActual
        '
        Me.grdActual.AllowUserToAddRows = false
        Me.grdActual.AllowUserToDeleteRows = false
        Me.grdActual.AllowUserToOrderColumns = true
        Me.grdActual.AllowUserToResizeRows = false
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlLight
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.grdActual.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.grdActual.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdActual.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdActual.Location = New System.Drawing.Point(0, 25)
        Me.grdActual.Name = "grdActual"
        Me.grdActual.ReadOnly = true
        Me.grdActual.RowHeadersVisible = false
        Me.grdActual.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdActual.Size = New System.Drawing.Size(916, 171)
        Me.grdActual.TabIndex = 1
        Me.grdActual.VirtualMode = true
        '
        'ToolStrip2
        '
        Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel2, Me.ToolStripSeparator6, Me.cmdActualFilter, Me.cmdActualColumns, Me.cmdActualExcel, Me.ToolStripSeparator7, Me.cmdActualAutomatch, Me.cmdActualMatch, Me.cmdAddAndMatch, Me.cmdActualBreakMatch, Me.cmdActualBreakMatches, Me.cmdRemoveUnmatchedSpots, Me.ToolStripSeparator8, Me.lblActualFiltered, Me.cmdActualBookingtype, Me.ToolStripButton7, Me.cmdDeleteActual, Me.ToolStripSeparator10, Me.lblHelp, Me.cmdFilmcodeFound})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(916, 25)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.AutoSize = false
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(56, 22)
        Me.ToolStripLabel2.Text = "Actual"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'lblActualFiltered
        '
        Me.lblActualFiltered.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblActualFiltered.ForeColor = System.Drawing.Color.Red
        Me.lblActualFiltered.Name = "lblActualFiltered"
        Me.lblActualFiltered.Size = New System.Drawing.Size(0, 22)
        Me.lblActualFiltered.Tag = "0"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(6, 25)
        '
        'ToolTip
        '
        Me.ToolTip.ShowAlways = true
        '
        'cmdConfirmedFilter
        '
        Me.cmdConfirmedFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdConfirmedFilter.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FiltersToolStripMenuItem})
        Me.cmdConfirmedFilter.Image = CType(resources.GetObject("cmdConfirmedFilter.Image"),System.Drawing.Image)
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
        'cmdConfirmedExcel
        '
        Me.cmdConfirmedExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdConfirmedExcel.Image = CType(resources.GetObject("cmdConfirmedExcel.Image"),System.Drawing.Image)
        Me.cmdConfirmedExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdConfirmedExcel.Name = "cmdConfirmedExcel"
        Me.cmdConfirmedExcel.Size = New System.Drawing.Size(23, 22)
        Me.cmdConfirmedExcel.Text = "ToolStripButton3"
        Me.cmdConfirmedExcel.ToolTipText = "Export to Excel"
        '
        'cmdImportSchedule
        '
        Me.cmdImportSchedule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdImportSchedule.Image = Global.clTrinity.My.Resources.Resources.import_schedule_2_16x16
        Me.cmdImportSchedule.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdImportSchedule.Name = "cmdImportSchedule"
        Me.cmdImportSchedule.Size = New System.Drawing.Size(23, 22)
        Me.cmdImportSchedule.Text = "ToolStripButton7"
        Me.cmdImportSchedule.ToolTipText = "Import channel schedule"
        '
        'cmdCreateRBS
        '
        Me.cmdCreateRBS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdCreateRBS.Image = Global.clTrinity.My.Resources.Resources.add_more_2_small_org
        Me.cmdCreateRBS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdCreateRBS.Name = "cmdCreateRBS"
        Me.cmdCreateRBS.Size = New System.Drawing.Size(23, 22)
        Me.cmdCreateRBS.Text = "Add marker spots for channels with no spotlist"
        '
        'cmdAutoMatch
        '
        Me.cmdAutoMatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdAutoMatch.Image = CType(resources.GetObject("cmdAutoMatch.Image"),System.Drawing.Image)
        Me.cmdAutoMatch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAutoMatch.Name = "cmdAutoMatch"
        Me.cmdAutoMatch.Size = New System.Drawing.Size(23, 22)
        Me.cmdAutoMatch.Text = "ToolStripButton4"
        Me.cmdAutoMatch.ToolTipText = "Automatch confirmed and actual spots"
        '
        'cmdMatch
        '
        Me.cmdMatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdMatch.Image = CType(resources.GetObject("cmdMatch.Image"),System.Drawing.Image)
        Me.cmdMatch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdMatch.Name = "cmdMatch"
        Me.cmdMatch.Size = New System.Drawing.Size(23, 22)
        Me.cmdMatch.Text = "ToolStripButton5"
        Me.cmdMatch.ToolTipText = "Match a single confirmed spot with a single actual spot"
        '
        'cmdBreakMatch
        '
        Me.cmdBreakMatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdBreakMatch.Image = CType(resources.GetObject("cmdBreakMatch.Image"),System.Drawing.Image)
        Me.cmdBreakMatch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdBreakMatch.Name = "cmdBreakMatch"
        Me.cmdBreakMatch.Size = New System.Drawing.Size(23, 22)
        Me.cmdBreakMatch.Text = "Break marked match"
        '
        'cmdBreakAll
        '
        Me.cmdBreakAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdBreakAll.Image = CType(resources.GetObject("cmdBreakAll.Image"),System.Drawing.Image)
        Me.cmdBreakAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdBreakAll.Name = "cmdBreakAll"
        Me.cmdBreakAll.Size = New System.Drawing.Size(23, 22)
        Me.cmdBreakAll.Text = "ToolStripButton9"
        Me.cmdBreakAll.ToolTipText = "Break all matches"
        '
        'cmdConfirmedEstimate
        '
        Me.cmdConfirmedEstimate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdConfirmedEstimate.Image = Global.clTrinity.My.Resources.Resources.magic_wand_2_16_x16
        Me.cmdConfirmedEstimate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdConfirmedEstimate.Name = "cmdConfirmedEstimate"
        Me.cmdConfirmedEstimate.Size = New System.Drawing.Size(23, 22)
        Me.cmdConfirmedEstimate.Text = "ToolStripButton6"
        Me.cmdConfirmedEstimate.ToolTipText = "Estimate confirmed spots"
        '
        'cmdBookingtype
        '
        Me.cmdBookingtype.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdBookingtype.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BookingtypeToolStripMenuItem})
        Me.cmdBookingtype.Image = CType(resources.GetObject("cmdBookingtype.Image"),System.Drawing.Image)
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
        'cmdFilm
        '
        Me.cmdFilm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdFilm.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilmsToolStripMenuItem})
        Me.cmdFilm.Image = Global.clTrinity.My.Resources.Resources.film_2_small
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
        'cmdConfirmedDelete
        '
        Me.cmdConfirmedDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdConfirmedDelete.Image = CType(resources.GetObject("cmdConfirmedDelete.Image"),System.Drawing.Image)
        Me.cmdConfirmedDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdConfirmedDelete.Name = "cmdConfirmedDelete"
        Me.cmdConfirmedDelete.Size = New System.Drawing.Size(23, 22)
        Me.cmdConfirmedDelete.Text = "ToolStripButton9"
        Me.cmdConfirmedDelete.ToolTipText = "Delete spot"
        '
        'cmdCheckDuplicates
        '
        Me.cmdCheckDuplicates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdCheckDuplicates.Image = Global.clTrinity.My.Resources.Resources.link1
        Me.cmdCheckDuplicates.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdCheckDuplicates.Name = "cmdCheckDuplicates"
        Me.cmdCheckDuplicates.Size = New System.Drawing.Size(23, 22)
        Me.cmdCheckDuplicates.Text = "Mark spots in same break"
        Me.cmdCheckDuplicates.ToolTipText = "Click to color spots that share a break with others"
        '
        'cmdTimeshift
        '
        Me.cmdTimeshift.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdTimeshift.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DefaultToolStripMenuItem, Me.ToolStripMenuItem2, Me.LiveToolStripMenuItem, Me.VOSDAL7ToolStripMenuItem})
        Me.cmdTimeshift.Image = Global.clTrinity.My.Resources.Resources.world_2_16x16
        Me.cmdTimeshift.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdTimeshift.Name = "cmdTimeshift"
        Me.cmdTimeshift.Size = New System.Drawing.Size(29, 22)
        Me.cmdTimeshift.Text = "ToolStripDropDownButton1"
        Me.cmdTimeshift.ToolTipText = "Timeshift"
        '
        'DefaultToolStripMenuItem
        '
        Me.DefaultToolStripMenuItem.Name = "DefaultToolStripMenuItem"
        Me.DefaultToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.DefaultToolStripMenuItem.Tag = "0"
        Me.DefaultToolStripMenuItem.Text = "Default"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(129, 6)
        '
        'LiveToolStripMenuItem
        '
        Me.LiveToolStripMenuItem.MergeIndex = 8
        Me.LiveToolStripMenuItem.Name = "LiveToolStripMenuItem"
        Me.LiveToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.LiveToolStripMenuItem.Tag = "1"
        Me.LiveToolStripMenuItem.Text = "Live"
        '
        'VOSDAL7ToolStripMenuItem
        '
        Me.VOSDAL7ToolStripMenuItem.Name = "VOSDAL7ToolStripMenuItem"
        Me.VOSDAL7ToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.VOSDAL7ToolStripMenuItem.Tag = "2"
        Me.VOSDAL7ToolStripMenuItem.Text = "VOSDAL+7"
        '
        'cmdIgnoreFaultySpots
        '
        Me.cmdIgnoreFaultySpots.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.cmdIgnoreFaultySpots.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdIgnoreFaultySpots.Image = Global.clTrinity.My.Resources.Resources.sync_2_24x23
        Me.cmdIgnoreFaultySpots.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdIgnoreFaultySpots.Name = "cmdIgnoreFaultySpots"
        Me.cmdIgnoreFaultySpots.Size = New System.Drawing.Size(23, 22)
        Me.cmdIgnoreFaultySpots.Text = "Click to filter spots with missing film codes back in"
        '
        'cmdActualFilter
        '
        Me.cmdActualFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdActualFilter.Enabled = false
        Me.cmdActualFilter.Image = CType(resources.GetObject("cmdActualFilter.Image"),System.Drawing.Image)
        Me.cmdActualFilter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdActualFilter.Name = "cmdActualFilter"
        Me.cmdActualFilter.Size = New System.Drawing.Size(23, 22)
        Me.cmdActualFilter.Text = "mnuConfirmedFilter"
        Me.cmdActualFilter.ToolTipText = "Set filter"
        '
        'cmdActualColumns
        '
        Me.cmdActualColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdActualColumns.Image = CType(resources.GetObject("cmdActualColumns.Image"),System.Drawing.Image)
        Me.cmdActualColumns.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdActualColumns.Name = "cmdActualColumns"
        Me.cmdActualColumns.Size = New System.Drawing.Size(23, 22)
        Me.cmdActualColumns.Text = "ToolStripButton2"
        Me.cmdActualColumns.ToolTipText = "Select columns"
        '
        'cmdActualExcel
        '
        Me.cmdActualExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdActualExcel.Image = CType(resources.GetObject("cmdActualExcel.Image"),System.Drawing.Image)
        Me.cmdActualExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdActualExcel.Name = "cmdActualExcel"
        Me.cmdActualExcel.Size = New System.Drawing.Size(23, 22)
        Me.cmdActualExcel.Text = "ToolStripButton3"
        Me.cmdActualExcel.ToolTipText = "Export to Excel"
        '
        'cmdActualAutomatch
        '
        Me.cmdActualAutomatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdActualAutomatch.Image = CType(resources.GetObject("cmdActualAutomatch.Image"),System.Drawing.Image)
        Me.cmdActualAutomatch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdActualAutomatch.Name = "cmdActualAutomatch"
        Me.cmdActualAutomatch.Size = New System.Drawing.Size(23, 22)
        Me.cmdActualAutomatch.Text = "ToolStripButton4"
        Me.cmdActualAutomatch.ToolTipText = "Automatch confirmed and actual spots"
        '
        'cmdActualMatch
        '
        Me.cmdActualMatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdActualMatch.Image = CType(resources.GetObject("cmdActualMatch.Image"),System.Drawing.Image)
        Me.cmdActualMatch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdActualMatch.Name = "cmdActualMatch"
        Me.cmdActualMatch.Size = New System.Drawing.Size(23, 22)
        Me.cmdActualMatch.Text = "ToolStripButton5"
        Me.cmdActualMatch.ToolTipText = "Match a single confirmed spot with a single actual spot"
        '
        'cmdAddAndMatch
        '
        Me.cmdAddAndMatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdAddAndMatch.Image = Global.clTrinity.My.Resources.Resources.add_added_values_2_16x16
        Me.cmdAddAndMatch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAddAndMatch.Name = "cmdAddAndMatch"
        Me.cmdAddAndMatch.Size = New System.Drawing.Size(23, 22)
        Me.cmdAddAndMatch.Text = "ToolStripButton1"
        Me.cmdAddAndMatch.ToolTipText = "Add the spot to confirmed and match it"
        '
        'cmdActualBreakMatch
        '
        Me.cmdActualBreakMatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdActualBreakMatch.Image = CType(resources.GetObject("cmdActualBreakMatch.Image"),System.Drawing.Image)
        Me.cmdActualBreakMatch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdActualBreakMatch.Name = "cmdActualBreakMatch"
        Me.cmdActualBreakMatch.Size = New System.Drawing.Size(23, 22)
        Me.cmdActualBreakMatch.Text = "Break marked match"
        '
        'cmdActualBreakMatches
        '
        Me.cmdActualBreakMatches.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdActualBreakMatches.Image = CType(resources.GetObject("cmdActualBreakMatches.Image"),System.Drawing.Image)
        Me.cmdActualBreakMatches.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdActualBreakMatches.Name = "cmdActualBreakMatches"
        Me.cmdActualBreakMatches.Size = New System.Drawing.Size(23, 22)
        Me.cmdActualBreakMatches.Text = "Break ALL matches"
        Me.cmdActualBreakMatches.ToolTipText = "Break all matches"
        '
        'cmdRemoveUnmatchedSpots
        '
        Me.cmdRemoveUnmatchedSpots.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdRemoveUnmatchedSpots.Image = Global.clTrinity.My.Resources.Resources.delete_3_16x16
        Me.cmdRemoveUnmatchedSpots.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdRemoveUnmatchedSpots.Name = "cmdRemoveUnmatchedSpots"
        Me.cmdRemoveUnmatchedSpots.Size = New System.Drawing.Size(23, 22)
        Me.cmdRemoveUnmatchedSpots.Text = "Remove unmatched spots"
        '
        'cmdActualBookingtype
        '
        Me.cmdActualBookingtype.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdActualBookingtype.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.cmdActualBookingtype.Image = CType(resources.GetObject("cmdActualBookingtype.Image"),System.Drawing.Image)
        Me.cmdActualBookingtype.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdActualBookingtype.Name = "cmdActualBookingtype"
        Me.cmdActualBookingtype.Size = New System.Drawing.Size(29, 22)
        Me.cmdActualBookingtype.Text = "ToolStripDropDownButton2"
        Me.cmdActualBookingtype.ToolTipText = "Change bookingtype on spot"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(146, 22)
        Me.ToolStripMenuItem1.Text = "Bookingtypes"
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton7.Enabled = false
        Me.ToolStripButton7.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton7.Text = "ToolStripButton7"
        '
        'cmdDeleteActual
        '
        Me.cmdDeleteActual.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdDeleteActual.Image = CType(resources.GetObject("cmdDeleteActual.Image"),System.Drawing.Image)
        Me.cmdDeleteActual.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDeleteActual.Name = "cmdDeleteActual"
        Me.cmdDeleteActual.Size = New System.Drawing.Size(23, 22)
        Me.cmdDeleteActual.Text = "Delete actual spots"
        '
        'lblHelp
        '
        Me.lblHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.lblHelp.Image = Global.clTrinity.My.Resources.Resources.info_2_20x20
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(16, 22)
        Me.lblHelp.Text = "lblHelp"
        '
        'cmdFilmcodeFound
        '
        Me.cmdFilmcodeFound.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.cmdFilmcodeFound.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdFilmcodeFound.Enabled = false
        Me.cmdFilmcodeFound.Image = CType(resources.GetObject("cmdFilmcodeFound.Image"),System.Drawing.Image)
        Me.cmdFilmcodeFound.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdFilmcodeFound.Name = "cmdFilmcodeFound"
        Me.cmdFilmcodeFound.Size = New System.Drawing.Size(23, 22)
        Me.cmdFilmcodeFound.Text = "Filmcodes not found"
        '
        'frmSpots
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(916, 401)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmSpots"
        Me.Text = "Spots"
        Me.SplitContainer1.Panel1.ResumeLayout(false)
        Me.SplitContainer1.Panel1.PerformLayout
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        Me.SplitContainer1.Panel2.PerformLayout
        Me.SplitContainer1.ResumeLayout(false)
        CType(Me.grdConfirmed,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolStrip1.ResumeLayout(false)
        Me.ToolStrip1.PerformLayout
        CType(Me.grdActual,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolStrip2.ResumeLayout(false)
        Me.ToolStrip2.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmdConfirmedColumns As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdConfirmedExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdAutoMatch As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdMatch As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdConfirmedEstimate As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdImportSchedule As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdConfirmedDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbTarget As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdBookingtype As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents cmdFilm As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents grdActual As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdActualFilter As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdActualColumns As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdActualExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdActualAutomatch As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdActualMatch As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton7 As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdDeleteActual As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdConfirmedFilter As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents FiltersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdBreakAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents BookingtypeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilmsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdActualBreakMatches As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdAddAndMatch As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdCreateRBS As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblConfirmedFiltered As System.Windows.Forms.ToolStripLabel
    Friend WithEvents lblActualFiltered As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents grdConfirmed As System.Windows.Forms.DataGridView
    Friend WithEvents cmdActualBreakMatch As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdBreakMatch As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHelp As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmdActualBookingtype As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdFilmcodeFound As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripLabel3 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmdIgnoreFaultySpots As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdCheckDuplicates As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdRemoveUnmatchedSpots As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdTimeshift As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents LiveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VOSDAL7ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DefaultToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
End Class
