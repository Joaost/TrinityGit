<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPivottable
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPivottable))
        Me.tsPivot = New System.Windows.Forms.ToolStrip()
        Me.cmdChartLayout = New System.Windows.Forms.ToolStripButton()
        Me.cmdDefine = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdExcel = New System.Windows.Forms.ToolStripButton()
        Me.cmdPictureToCB = New System.Windows.Forms.ToolStripButton()
        Me.cmdSaveToFile = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdReports = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdAddReport = New System.Windows.Forms.ToolStripButton()
        Me.tabPivot = New clTrinity.ExtendedTabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.pvt = New AxMicrosoft.Office.Interop.Owc11.AxPivotTable()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.chtPivot = New AxMicrosoft.Office.Interop.Owc11.AxChartSpace()
        Me.calcChart = New System.Windows.Forms.TabPage()
        Me.panChart = New System.Windows.Forms.Panel()
        Me.grdCalcChart = New System.Windows.Forms.DataGridView()
        Me.tsPivot.SuspendLayout()
        Me.tabPivot.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.pvt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.chtPivot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.calcChart.SuspendLayout()
        Me.panChart.SuspendLayout()
        CType(Me.grdCalcChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tsPivot
        '
        Me.tsPivot.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdChartLayout, Me.cmdDefine, Me.ToolStripSeparator1, Me.cmdExcel, Me.cmdPictureToCB, Me.cmdSaveToFile, Me.ToolStripSeparator2, Me.cmdReports, Me.cmdAddReport})
        Me.tsPivot.Location = New System.Drawing.Point(0, 0)
        Me.tsPivot.Name = "tsPivot"
        Me.tsPivot.Size = New System.Drawing.Size(706, 25)
        Me.tsPivot.TabIndex = 3
        Me.tsPivot.Text = "ToolStrip1"
        '
        'cmdChartLayout
        '
        Me.cmdChartLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdChartLayout.Image = CType(resources.GetObject("cmdChartLayout.Image"), System.Drawing.Image)
        Me.cmdChartLayout.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdChartLayout.Name = "cmdChartLayout"
        Me.cmdChartLayout.Size = New System.Drawing.Size(23, 22)
        Me.cmdChartLayout.Text = "Change chart layout"
        Me.cmdChartLayout.ToolTipText = "Change chart layout"
        Me.cmdChartLayout.Visible = False
        '
        'cmdDefine
        '
        Me.cmdDefine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdDefine.Image = CType(resources.GetObject("cmdDefine.Image"), System.Drawing.Image)
        Me.cmdDefine.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDefine.Name = "cmdDefine"
        Me.cmdDefine.Size = New System.Drawing.Size(23, 22)
        Me.cmdDefine.Text = "ToolStripButton2"
        Me.cmdDefine.ToolTipText = "Define pivot"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'cmdExcel
        '
        Me.cmdExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdExcel.Image = CType(resources.GetObject("cmdExcel.Image"), System.Drawing.Image)
        Me.cmdExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(23, 22)
        Me.cmdExcel.Text = "ToolStripButton1"
        Me.cmdExcel.ToolTipText = "Export data to Excel"
        '
        'cmdPictureToCB
        '
        Me.cmdPictureToCB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdPictureToCB.Image = CType(resources.GetObject("cmdPictureToCB.Image"), System.Drawing.Image)
        Me.cmdPictureToCB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdPictureToCB.Name = "cmdPictureToCB"
        Me.cmdPictureToCB.Size = New System.Drawing.Size(23, 22)
        Me.cmdPictureToCB.Text = "Copy to clipboard as picture"
        '
        'cmdSaveToFile
        '
        Me.cmdSaveToFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSaveToFile.Image = CType(resources.GetObject("cmdSaveToFile.Image"), System.Drawing.Image)
        Me.cmdSaveToFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSaveToFile.Name = "cmdSaveToFile"
        Me.cmdSaveToFile.Size = New System.Drawing.Size(23, 22)
        Me.cmdSaveToFile.Text = "Save as file"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'cmdReports
        '
        Me.cmdReports.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReportsToolStripMenuItem})
        Me.cmdReports.Image = CType(resources.GetObject("cmdReports.Image"), System.Drawing.Image)
        Me.cmdReports.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdReports.Name = "cmdReports"
        Me.cmdReports.Size = New System.Drawing.Size(29, 22)
        Me.cmdReports.Text = "Saved reports"
        '
        'ReportsToolStripMenuItem
        '
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        Me.ReportsToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.ReportsToolStripMenuItem.Text = "Reports"
        '
        'cmdAddReport
        '
        Me.cmdAddReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdAddReport.Image = CType(resources.GetObject("cmdAddReport.Image"), System.Drawing.Image)
        Me.cmdAddReport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAddReport.Name = "cmdAddReport"
        Me.cmdAddReport.Size = New System.Drawing.Size(23, 22)
        Me.cmdAddReport.Text = "Add to saved reports"
        '
        'tabPivot
        '
        Me.tabPivot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabPivot.Controls.Add(Me.TabPage1)
        Me.tabPivot.Controls.Add(Me.TabPage2)
        Me.tabPivot.Controls.Add(Me.calcChart)
        Me.tabPivot.Location = New System.Drawing.Point(0, 30)
        Me.tabPivot.Name = "tabPivot"
        Me.tabPivot.SelectedIndex = 0
        Me.tabPivot.Size = New System.Drawing.Size(706, 534)
        Me.tabPivot.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.pvt)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(698, 507)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Pivot"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'pvt
        '
        Me.pvt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pvt.DataSource = Nothing
        Me.pvt.Enabled = True
        Me.pvt.Location = New System.Drawing.Point(6, 6)
        Me.pvt.Name = "pvt"
        Me.pvt.OcxState = CType(resources.GetObject("pvt.OcxState"), System.Windows.Forms.AxHost.State)
        Me.pvt.Size = New System.Drawing.Size(686, 498)
        Me.pvt.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.chtPivot)
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(698, 507)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Chart"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'chtPivot
        '
        Me.chtPivot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chtPivot.DataSource = Nothing
        Me.chtPivot.Enabled = True
        Me.chtPivot.Location = New System.Drawing.Point(6, 6)
        Me.chtPivot.Name = "chtPivot"
        Me.chtPivot.OcxState = CType(resources.GetObject("chtPivot.OcxState"), System.Windows.Forms.AxHost.State)
        Me.chtPivot.Size = New System.Drawing.Size(686, 492)
        Me.chtPivot.TabIndex = 0
        '
        'calcChart
        '
        Me.calcChart.Controls.Add(Me.panChart)
        Me.calcChart.Location = New System.Drawing.Point(4, 23)
        Me.calcChart.Name = "calcChart"
        Me.calcChart.Padding = New System.Windows.Forms.Padding(3)
        Me.calcChart.Size = New System.Drawing.Size(698, 507)
        Me.calcChart.TabIndex = 2
        Me.calcChart.Text = "Calculated chart"
        Me.calcChart.UseVisualStyleBackColor = True
        '
        'panChart
        '
        Me.panChart.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panChart.AutoScroll = True
        Me.panChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panChart.Controls.Add(Me.grdCalcChart)
        Me.panChart.Location = New System.Drawing.Point(8, 6)
        Me.panChart.Name = "panChart"
        Me.panChart.Size = New System.Drawing.Size(682, 489)
        Me.panChart.TabIndex = 1
        '
        'grdCalcChart
        '
        Me.grdCalcChart.AllowUserToAddRows = False
        Me.grdCalcChart.AllowUserToDeleteRows = False
        Me.grdCalcChart.AllowUserToResizeColumns = False
        Me.grdCalcChart.AllowUserToResizeRows = False
        Me.grdCalcChart.ColumnHeadersHeight = 15
        Me.grdCalcChart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdCalcChart.ColumnHeadersVisible = False
        Me.grdCalcChart.Location = New System.Drawing.Point(119, 85)
        Me.grdCalcChart.Name = "grdCalcChart"
        Me.grdCalcChart.ReadOnly = True
        Me.grdCalcChart.RowHeadersVisible = False
        Me.grdCalcChart.RowHeadersWidth = 70
        Me.grdCalcChart.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdCalcChart.Size = New System.Drawing.Size(240, 162)
        Me.grdCalcChart.TabIndex = 0
        Me.grdCalcChart.VirtualMode = True
        '
        'frmPivottable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(706, 563)
        Me.Controls.Add(Me.tsPivot)
        Me.Controls.Add(Me.tabPivot)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPivottable"
        Me.Text = "Pivot"
        Me.tsPivot.ResumeLayout(False)
        Me.tsPivot.PerformLayout()
        Me.tabPivot.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.pvt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.chtPivot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.calcChart.ResumeLayout(False)
        Me.panChart.ResumeLayout(False)
        CType(Me.grdCalcChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tabPivot As clTrinity.ExtendedTabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents pvt As AxMicrosoft.Office.Interop.Owc11.AxPivotTable
    Friend WithEvents chtPivot As AxMicrosoft.Office.Interop.Owc11.AxChartSpace
    Friend WithEvents tsPivot As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdPictureToCB As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdSaveToFile As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdChartLayout As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdDefine As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdAddReport As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdReports As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents calcChart As System.Windows.Forms.TabPage
    Friend WithEvents grdCalcChart As System.Windows.Forms.DataGridView
    Friend WithEvents panChart As System.Windows.Forms.Panel
End Class
