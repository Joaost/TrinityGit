<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrint
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
        Me.components = New System.ComponentModel.Container
        Me.lstAvailable = New System.Windows.Forms.ListBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmdMoveDown = New System.Windows.Forms.Button
        Me.cmdMoveUp = New System.Windows.Forms.Button
        Me.cmdOpenTemplate = New System.Windows.Forms.Button
        Me.cmdSaveTemplate = New System.Windows.Forms.Button
        Me.lstChosen = New System.Windows.Forms.ListBox
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.tpPrint = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdRemove = New System.Windows.Forms.Button
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstAvailable
        '
        Me.lstAvailable.FormattingEnabled = True
        Me.lstAvailable.ItemHeight = 14
        Me.lstAvailable.Location = New System.Drawing.Point(6, 19)
        Me.lstAvailable.Name = "lstAvailable"
        Me.lstAvailable.Size = New System.Drawing.Size(188, 326)
        Me.lstAvailable.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lstAvailable)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 355)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Available elements"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdMoveDown)
        Me.GroupBox2.Controls.Add(Me.cmdMoveUp)
        Me.GroupBox2.Controls.Add(Me.cmdOpenTemplate)
        Me.GroupBox2.Controls.Add(Me.cmdSaveTemplate)
        Me.GroupBox2.Controls.Add(Me.lstChosen)
        Me.GroupBox2.Location = New System.Drawing.Point(252, 14)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(235, 376)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Chosen elements"
        '
        'cmdMoveDown
        '
        Me.cmdMoveDown.Image = Global.Balthazar.My.Resources.Resources.arrow_down_blue
        Me.cmdMoveDown.Location = New System.Drawing.Point(200, 180)
        Me.cmdMoveDown.Name = "cmdMoveDown"
        Me.cmdMoveDown.Size = New System.Drawing.Size(28, 27)
        Me.cmdMoveDown.TabIndex = 11
        Me.cmdMoveDown.UseVisualStyleBackColor = True
        '
        'cmdMoveUp
        '
        Me.cmdMoveUp.Image = Global.Balthazar.My.Resources.Resources.arrow_up_blue
        Me.cmdMoveUp.Location = New System.Drawing.Point(200, 147)
        Me.cmdMoveUp.Name = "cmdMoveUp"
        Me.cmdMoveUp.Size = New System.Drawing.Size(28, 27)
        Me.cmdMoveUp.TabIndex = 10
        Me.cmdMoveUp.UseVisualStyleBackColor = True
        '
        'cmdOpenTemplate
        '
        Me.cmdOpenTemplate.Image = Global.Balthazar.My.Resources.Resources.folder
        Me.cmdOpenTemplate.Location = New System.Drawing.Point(6, 347)
        Me.cmdOpenTemplate.Name = "cmdOpenTemplate"
        Me.cmdOpenTemplate.Size = New System.Drawing.Size(22, 23)
        Me.cmdOpenTemplate.TabIndex = 9
        Me.tpPrint.SetToolTip(Me.cmdOpenTemplate, "Load template")
        Me.cmdOpenTemplate.UseVisualStyleBackColor = True
        '
        'cmdSaveTemplate
        '
        Me.cmdSaveTemplate.Image = Global.Balthazar.My.Resources.Resources.disk_blue
        Me.cmdSaveTemplate.Location = New System.Drawing.Point(34, 347)
        Me.cmdSaveTemplate.Name = "cmdSaveTemplate"
        Me.cmdSaveTemplate.Size = New System.Drawing.Size(22, 23)
        Me.cmdSaveTemplate.TabIndex = 8
        Me.tpPrint.SetToolTip(Me.cmdSaveTemplate, "Save as template")
        Me.cmdSaveTemplate.UseVisualStyleBackColor = True
        '
        'lstChosen
        '
        Me.lstChosen.FormattingEnabled = True
        Me.lstChosen.ItemHeight = 14
        Me.lstChosen.Location = New System.Drawing.Point(6, 19)
        Me.lstChosen.Name = "lstChosen"
        Me.lstChosen.Size = New System.Drawing.Size(188, 326)
        Me.lstChosen.TabIndex = 0
        '
        'cmdPrint
        '
        Me.cmdPrint.Location = New System.Drawing.Point(493, 32)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(93, 36)
        Me.cmdPrint.TabIndex = 9
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'cmdRemove
        '
        Me.cmdRemove.Image = Global.Balthazar.My.Resources.Resources.navigate_left1
        Me.cmdRemove.Location = New System.Drawing.Point(218, 194)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(28, 27)
        Me.cmdRemove.TabIndex = 8
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Image = Global.Balthazar.My.Resources.Resources.navigate_right
        Me.cmdAdd.Location = New System.Drawing.Point(218, 161)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(28, 27)
        Me.cmdAdd.TabIndex = 7
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'frmPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(598, 471)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmPrint"
        Me.Text = "Print"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstAvailable As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lstChosen As System.Windows.Forms.ListBox
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents cmdOpenTemplate As System.Windows.Forms.Button
    Friend WithEvents cmdSaveTemplate As System.Windows.Forms.Button
    Friend WithEvents tpPrint As System.Windows.Forms.ToolTip
    Friend WithEvents cmdMoveDown As System.Windows.Forms.Button
    Friend WithEvents cmdMoveUp As System.Windows.Forms.Button
End Class
