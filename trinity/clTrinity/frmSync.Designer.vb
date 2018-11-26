<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSync
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSync))
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdSync = New System.Windows.Forms.Button()
        Me.rdbAll = New System.Windows.Forms.RadioButton()
        Me.rdbLastYear = New System.Windows.Forms.RadioButton()
        Me.rdbSixMonths = New System.Windows.Forms.RadioButton()
        Me.rdbMonth = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Location = New System.Drawing.Point(131, 167)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 0
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = true
        '
        'cmdSync
        '
        Me.cmdSync.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdSync.FlatAppearance.BorderSize = 0
        Me.cmdSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSync.Location = New System.Drawing.Point(50, 167)
        Me.cmdSync.Name = "cmdSync"
        Me.cmdSync.Size = New System.Drawing.Size(75, 23)
        Me.cmdSync.TabIndex = 1
        Me.cmdSync.Text = "Sync"
        Me.cmdSync.UseVisualStyleBackColor = true
        '
        'rdbAll
        '
        Me.rdbAll.AutoSize = true
        Me.rdbAll.Location = New System.Drawing.Point(13, 63)
        Me.rdbAll.Name = "rdbAll"
        Me.rdbAll.Size = New System.Drawing.Size(113, 17)
        Me.rdbAll.TabIndex = 2
        Me.rdbAll.Text = "As far as Possible"
        Me.rdbAll.UseVisualStyleBackColor = true
        '
        'rdbLastYear
        '
        Me.rdbLastYear.AutoSize = true
        Me.rdbLastYear.Location = New System.Drawing.Point(13, 86)
        Me.rdbLastYear.Name = "rdbLastYear"
        Me.rdbLastYear.Size = New System.Drawing.Size(74, 17)
        Me.rdbLastYear.TabIndex = 3
        Me.rdbLastYear.Text = "Last years"
        Me.rdbLastYear.UseVisualStyleBackColor = true
        '
        'rdbSixMonths
        '
        Me.rdbSixMonths.AutoSize = true
        Me.rdbSixMonths.Location = New System.Drawing.Point(13, 109)
        Me.rdbSixMonths.Name = "rdbSixMonths"
        Me.rdbSixMonths.Size = New System.Drawing.Size(96, 17)
        Me.rdbSixMonths.TabIndex = 4
        Me.rdbSixMonths.Text = "Last 6 months"
        Me.rdbSixMonths.UseVisualStyleBackColor = true
        '
        'rdbMonth
        '
        Me.rdbMonth.AutoSize = true
        Me.rdbMonth.Checked = true
        Me.rdbMonth.Location = New System.Drawing.Point(13, 132)
        Me.rdbMonth.Name = "rdbMonth"
        Me.rdbMonth.Size = New System.Drawing.Size(83, 17)
        Me.rdbMonth.TabIndex = 5
        Me.rdbMonth.TabStop = true
        Me.rdbMonth.Text = "Last Month"
        Me.rdbMonth.UseVisualStyleBackColor = true
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(10, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(179, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "How far back in time do you wish"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(8, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(159, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = " to copy the ""Specifics"" spots?"
        '
        'frmSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(218, 202)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.rdbMonth)
        Me.Controls.Add(Me.rdbSixMonths)
        Me.Controls.Add(Me.rdbLastYear)
        Me.Controls.Add(Me.rdbAll)
        Me.Controls.Add(Me.cmdSync)
        Me.Controls.Add(Me.cmdCancel)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmSync"
        Me.Text = "frmSync"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdSync As System.Windows.Forms.Button
    Friend WithEvents rdbAll As System.Windows.Forms.RadioButton
    Friend WithEvents rdbLastYear As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSixMonths As System.Windows.Forms.RadioButton
    Friend WithEvents rdbMonth As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
