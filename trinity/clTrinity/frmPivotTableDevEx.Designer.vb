<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPivotTableDevEx
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPivotTableDevEx))
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.tsPivot = New System.Windows.Forms.ToolStrip()
        Me.cmdDefine = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdExcel = New System.Windows.Forms.ToolStripButton()
        Me.cmdPictureToCB = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tabPivot = New clTrinity.ExtendedTabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.pvtTable = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.chtPivot = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.tsPivot.SuspendLayout()
        Me.tabPivot.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.pvtTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.chtPivot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tsPivot
        '
        Me.tsPivot.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdDefine, Me.ToolStripSeparator1, Me.cmdExcel, Me.cmdPictureToCB, Me.ToolStripSeparator2})
        Me.tsPivot.Location = New System.Drawing.Point(0, 0)
        Me.tsPivot.Name = "tsPivot"
        Me.tsPivot.Size = New System.Drawing.Size(890, 25)
        Me.tsPivot.TabIndex = 4
        Me.tsPivot.Text = "ToolStrip1"
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
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'tabPivot
        '
        Me.tabPivot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabPivot.Controls.Add(Me.TabPage1)
        Me.tabPivot.Controls.Add(Me.TabPage2)
        Me.tabPivot.Location = New System.Drawing.Point(1, 27)
        Me.tabPivot.Name = "tabPivot"
        Me.tabPivot.SelectedIndex = 0
        Me.tabPivot.Size = New System.Drawing.Size(889, 449)
        Me.tabPivot.TabIndex = 5
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.pvtTable)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(881, 423)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Pivot"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'pvtTable
        '
        Me.pvtTable.Cursor = System.Windows.Forms.Cursors.Default
        Me.pvtTable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pvtTable.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.PivotGridField1})
        Me.pvtTable.Location = New System.Drawing.Point(3, 3)
        Me.pvtTable.LookAndFeel.SkinName = "Blue"
        Me.pvtTable.LookAndFeel.UseDefaultLookAndFeel = False
        Me.pvtTable.Name = "pvtTable"
        Me.pvtTable.OptionsCustomization.AllowExpand = False
        Me.pvtTable.OptionsCustomization.AllowHideFields = DevExpress.XtraPivotGrid.AllowHideFieldsType.Never
        Me.pvtTable.OptionsMenu.EnableFieldValueMenu = False
        Me.pvtTable.OptionsMenu.EnableHeaderAreaMenu = False
        Me.pvtTable.OptionsMenu.EnableHeaderMenu = False
        Me.pvtTable.OptionsView.ShowFilterHeaders = False
        Me.pvtTable.Size = New System.Drawing.Size(875, 417)
        Me.pvtTable.TabIndex = 1
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField1.AreaIndex = 0
        Me.PivotGridField1.Name = "PivotGridField1"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.chtPivot)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(881, 423)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Chart"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'chtPivot
        '
        Me.chtPivot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ChartArea1.Name = "ChartArea1"
        Me.chtPivot.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.chtPivot.Legends.Add(Legend1)
        Me.chtPivot.Location = New System.Drawing.Point(7, 6)
        Me.chtPivot.Name = "chtPivot"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.chtPivot.Series.Add(Series1)
        Me.chtPivot.Size = New System.Drawing.Size(866, 409)
        Me.chtPivot.TabIndex = 0
        Me.chtPivot.Text = "Chart1"
        '
        'frmPivotTableDevEx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(890, 476)
        Me.Controls.Add(Me.tsPivot)
        Me.Controls.Add(Me.tabPivot)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPivotTableDevEx"
        Me.Text = "Pivot"
        Me.tsPivot.ResumeLayout(False)
        Me.tsPivot.PerformLayout()
        Me.tabPivot.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.pvtTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.chtPivot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tsPivot As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdDefine As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdPictureToCB As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents tabPivot As clTrinity.ExtendedTabControl
    Private WithEvents pvtTable As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents chtPivot As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
End Class
