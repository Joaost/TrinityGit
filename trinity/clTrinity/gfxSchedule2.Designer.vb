<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gfxSchedule2
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.topPanel = New System.Windows.Forms.Panel
        Me.topInnerPanel = New System.Windows.Forms.Panel
        Me.leftPanel = New System.Windows.Forms.Panel
        Me.leftInnerPanel = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.sePanel = New clTrinity.ScrollEventPanel
        Me.itemPanel = New System.Windows.Forms.Panel
        Me.topPanel.SuspendLayout()
        Me.leftPanel.SuspendLayout()
        Me.sePanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'topPanel
        '
        Me.topPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.topPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.topPanel.Controls.Add(Me.topInnerPanel)
        Me.topPanel.Location = New System.Drawing.Point(60, 5)
        Me.topPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.topPanel.Name = "topPanel"
        Me.topPanel.Size = New System.Drawing.Size(447, 55)
        Me.topPanel.TabIndex = 2
        '
        'topInnerPanel
        '
        Me.topInnerPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.topInnerPanel.Location = New System.Drawing.Point(0, 0)
        Me.topInnerPanel.Name = "topInnerPanel"
        Me.topInnerPanel.Size = New System.Drawing.Size(365, 55)
        Me.topInnerPanel.TabIndex = 0
        '
        'leftPanel
        '
        Me.leftPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.leftPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.leftPanel.Controls.Add(Me.leftInnerPanel)
        Me.leftPanel.Location = New System.Drawing.Point(5, 60)
        Me.leftPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.leftPanel.Name = "leftPanel"
        Me.leftPanel.Size = New System.Drawing.Size(55, 157)
        Me.leftPanel.TabIndex = 3
        '
        'leftInnerPanel
        '
        Me.leftInnerPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.leftInnerPanel.Location = New System.Drawing.Point(0, 0)
        Me.leftInnerPanel.Name = "leftInnerPanel"
        Me.leftInnerPanel.Size = New System.Drawing.Size(55, 132)
        Me.leftInnerPanel.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Menu
        Me.Label1.Location = New System.Drawing.Point(29, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Day"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Menu
        Me.Label2.Location = New System.Drawing.Point(13, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Time"
        '
        'sePanel
        '
        Me.sePanel.AutoScroll = True
        Me.sePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.sePanel.Controls.Add(Me.itemPanel)
        Me.sePanel.Location = New System.Drawing.Point(60, 60)
        Me.sePanel.Margin = New System.Windows.Forms.Padding(0)
        Me.sePanel.Name = "sePanel"
        Me.sePanel.Size = New System.Drawing.Size(447, 157)
        Me.sePanel.TabIndex = 7
        '
        'itemPanel
        '
        Me.itemPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.itemPanel.Location = New System.Drawing.Point(0, 0)
        Me.itemPanel.Name = "itemPanel"
        Me.itemPanel.Size = New System.Drawing.Size(447, 157)
        Me.itemPanel.TabIndex = 6
        '
        'gfxSchedule2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Menu
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.sePanel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.leftPanel)
        Me.Controls.Add(Me.topPanel)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "gfxSchedule2"
        Me.Size = New System.Drawing.Size(516, 226)
        Me.topPanel.ResumeLayout(False)
        Me.leftPanel.ResumeLayout(False)
        Me.sePanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents topPanel As System.Windows.Forms.Panel
    Friend WithEvents leftPanel As System.Windows.Forms.Panel
    Friend WithEvents topInnerPanel As System.Windows.Forms.Panel
    Friend WithEvents leftInnerPanel As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents itemPanel As System.Windows.Forms.Panel
    Friend WithEvents sePanel As clTrinity.ScrollEventPanel

End Class
