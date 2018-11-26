<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMonitor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMonitor))
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.cmbChart = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.cmbTarget = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmbShowGoal = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.cmbDisplayType = New System.Windows.Forms.ToolStripComboBox()
        Me.lblUpdatedTo = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.chtMain = New clTrinity.MonitorChart()
        Me.chtReach = New clTrinity.ReachChart()
        Me.cmdFilter = New System.Windows.Forms.ToolStripDropDownButton()
        Me.FilterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdPictureToCB = New System.Windows.Forms.ToolStripButton()
        Me.cmdTimeshift = New System.Windows.Forms.ToolStripDropDownButton()
        Me.DefaultToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.LiveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VOSDAL7ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdFrequency = New System.Windows.Forms.ToolStripDropDownButton()
        Me.FrequencyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1.SuspendLayout
        Me.SuspendLayout
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(36, 22)
        Me.ToolStripLabel1.Text = "Chart"
        '
        'cmbChart
        '
        Me.cmbChart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChart.Items.AddRange(New Object() {"Total", "Allocation", "Channels", "Weekdays", "Dayparts", "Films", "Weeks", "Pos In Break", "Break Type", "Reach"})
        Me.cmbChart.Name = "cmbChart"
        Me.cmbChart.Size = New System.Drawing.Size(121, 25)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(40, 22)
        Me.ToolStripLabel2.Text = "Target"
        '
        'cmbTarget
        '
        Me.cmbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTarget.Items.AddRange(New Object() {"Main Target", "Secondary Target", "Buying Target", "All adults"})
        Me.cmbTarget.Name = "cmbTarget"
        Me.cmbTarget.Size = New System.Drawing.Size(121, 25)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'cmbShowGoal
        '
        Me.cmbShowGoal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbShowGoal.Items.AddRange(New Object() {"Show Buildup", "Show Intersect", "Show Both"})
        Me.cmbShowGoal.Name = "cmbShowGoal"
        Me.cmbShowGoal.Size = New System.Drawing.Size(121, 25)
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(45, 22)
        Me.ToolStripLabel3.Text = "Display"
        '
        'cmbDisplayType
        '
        Me.cmbDisplayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDisplayType.Items.AddRange(New Object() {"TRP", "Percent", "TRP 30"""})
        Me.cmbDisplayType.Name = "cmbDisplayType"
        Me.cmbDisplayType.Size = New System.Drawing.Size(75, 25)
        '
        'lblUpdatedTo
        '
        Me.lblUpdatedTo.Name = "lblUpdatedTo"
        Me.lblUpdatedTo.Size = New System.Drawing.Size(79, 22)
        Me.lblUpdatedTo.Text = "Updated To: -"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdFilter, Me.ToolStripSeparator4, Me.cmdPictureToCB, Me.ToolStripSeparator1, Me.ToolStripLabel1, Me.cmbChart, Me.ToolStripSeparator2, Me.ToolStripLabel2, Me.cmbTarget, Me.cmdTimeshift, Me.ToolStripSeparator3, Me.cmdFrequency, Me.cmbShowGoal, Me.ToolStripLabel3, Me.cmbDisplayType, Me.lblUpdatedTo})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(784, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'chtMain
        '
        Me.chtMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chtMain.BackColor = System.Drawing.Color.White
        Me.chtMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.chtMain.Cursor = System.Windows.Forms.Cursors.Default
        Me.chtMain.Dimensions = 1
        Me.chtMain.Headline = "Channels"
        Me.chtMain.HeadlineFont = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chtMain.Location = New System.Drawing.Point(0, 28)
        Me.chtMain.Name = "chtMain"
        Me.chtMain.Size = New System.Drawing.Size(784, 376)
        Me.chtMain.TabIndex = 1
        '
        'chtReach
        '
        Me.chtReach.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chtReach.BackColor = System.Drawing.Color.White
        Me.chtReach.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.chtReach.Cursor = System.Windows.Forms.Cursors.Default
        Me.chtReach.Days = 0
        Me.chtReach.Headline = "Reach"
        Me.chtReach.HeadlineFont = New System.Drawing.Font("Segoe UI", 8!)
        Me.chtReach.Location = New System.Drawing.Point(0, 28)
        Me.chtReach.Name = "chtReach"
        Me.chtReach.ShowBuildup = false
        Me.chtReach.ShowIntersect = false
        Me.chtReach.Size = New System.Drawing.Size(784, 376)
        Me.chtReach.TabIndex = 2
        Me.chtReach.Text = "ReachChart1"
        '
        'cmdFilter
        '
        Me.cmdFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdFilter.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilterToolStripMenuItem})
        Me.cmdFilter.Image = Global.clTrinity.My.Resources.Resources.miscellaneous_2_16x16
        Me.cmdFilter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdFilter.Name = "cmdFilter"
        Me.cmdFilter.Size = New System.Drawing.Size(29, 22)
        Me.cmdFilter.Text = "ToolStripDropDownButton1"
        '
        'FilterToolStripMenuItem
        '
        Me.FilterToolStripMenuItem.Name = "FilterToolStripMenuItem"
        Me.FilterToolStripMenuItem.Size = New System.Drawing.Size(100, 22)
        Me.FilterToolStripMenuItem.Text = "Filter"
        '
        'cmdPictureToCB
        '
        Me.cmdPictureToCB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdPictureToCB.Image = Global.clTrinity.My.Resources.Resources.image_2_16x16
        Me.cmdPictureToCB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdPictureToCB.Name = "cmdPictureToCB"
        Me.cmdPictureToCB.Size = New System.Drawing.Size(23, 22)
        Me.cmdPictureToCB.Text = "Copy to clipboard as picture"
        '
        'cmdTimeshift
        '
        Me.cmdTimeshift.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdTimeshift.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DefaultToolStripMenuItem, Me.ToolStripMenuItem1, Me.LiveToolStripMenuItem, Me.VOSDAL7ToolStripMenuItem})
        Me.cmdTimeshift.Image = Global.clTrinity.My.Resources.Resources.broadcast_2
        Me.cmdTimeshift.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdTimeshift.Name = "cmdTimeshift"
        Me.cmdTimeshift.Size = New System.Drawing.Size(29, 22)
        Me.cmdTimeshift.Text = "ToolStripDropDownButton1"
        Me.cmdTimeshift.ToolTipText = "Timeshift"
        '
        'DefaultToolStripMenuItem
        '
        Me.DefaultToolStripMenuItem.Name = "DefaultToolStripMenuItem"
        Me.DefaultToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DefaultToolStripMenuItem.Tag = "0"
        Me.DefaultToolStripMenuItem.Text = "Default"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(149, 6)
        '
        'LiveToolStripMenuItem
        '
        Me.LiveToolStripMenuItem.MergeIndex = 8
        Me.LiveToolStripMenuItem.Name = "LiveToolStripMenuItem"
        Me.LiveToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.LiveToolStripMenuItem.Tag = "1"
        Me.LiveToolStripMenuItem.Text = "Live"
        '
        'VOSDAL7ToolStripMenuItem
        '
        Me.VOSDAL7ToolStripMenuItem.Name = "VOSDAL7ToolStripMenuItem"
        Me.VOSDAL7ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.VOSDAL7ToolStripMenuItem.Tag = "2"
        Me.VOSDAL7ToolStripMenuItem.Text = "VOSDAL+7"
        '
        'cmdFrequency
        '
        Me.cmdFrequency.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdFrequency.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FrequencyToolStripMenuItem})
        Me.cmdFrequency.Image = Global.clTrinity.My.Resources.Resources.frequncy_2_16x16
        Me.cmdFrequency.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdFrequency.Name = "cmdFrequency"
        Me.cmdFrequency.Size = New System.Drawing.Size(29, 22)
        Me.cmdFrequency.Text = "ToolStripDropDownButton1"
        '
        'FrequencyToolStripMenuItem
        '
        Me.FrequencyToolStripMenuItem.Name = "FrequencyToolStripMenuItem"
        Me.FrequencyToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.FrequencyToolStripMenuItem.Text = "Frequency"
        '
        'frmMonitor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 14!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 405)
        Me.Controls.Add(Me.chtMain)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.chtReach)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmMonitor"
        Me.Text = "Monitor campaign"
        Me.ToolStrip1.ResumeLayout(false)
        Me.ToolStrip1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents chtMain As clTrinity.MonitorChart
    Friend WithEvents chtReach As clTrinity.ReachChart
    Friend WithEvents cmdFilter As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents FilterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmbChart As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmbTarget As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdFrequency As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents FrequencyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbShowGoal As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripLabel3 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmbDisplayType As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents lblUpdatedTo As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdTimeshift As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents LiveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VOSDAL7ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DefaultToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdPictureToCB As Windows.Forms.ToolStripButton
End Class
