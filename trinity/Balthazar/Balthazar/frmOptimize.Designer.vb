<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptimize
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
        Me.cmdOk = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.RadiobuttonList3 = New Balthazar.RadiobuttonList
        Me.Label5 = New System.Windows.Forms.Label
        Me.RadiobuttonList2 = New Balthazar.RadiobuttonList
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.RadiobuttonList1 = New Balthazar.RadiobuttonList
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.RadiobuttonList7 = New Balthazar.RadiobuttonList
        Me.Label12 = New System.Windows.Forms.Label
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.RadiobuttonList4 = New Balthazar.RadiobuttonList
        Me.Label6 = New System.Windows.Forms.Label
        Me.RadiobuttonList5 = New Balthazar.RadiobuttonList
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.RadiobuttonList6 = New Balthazar.RadiobuttonList
        Me.Label10 = New System.Windows.Forms.Label
        Me.pnlProgress = New System.Windows.Forms.Panel
        Me.pbStatus = New System.Windows.Forms.ProgressBar
        Me.lblStatus = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtCrossovers = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtMutation = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtGenerations = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtIndividuals = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.pnlProgress.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOk.Location = New System.Drawing.Point(278, 445)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(83, 33)
        Me.cmdOk.TabIndex = 5
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadiobuttonList3)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.RadiobuttonList2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.RadiobuttonList1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 51)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(353, 122)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Global citeria"
        '
        'RadiobuttonList3
        '
        Me.RadiobuttonList3.Location = New System.Drawing.Point(224, 87)
        Me.RadiobuttonList3.Name = "RadiobuttonList3"
        Me.RadiobuttonList3.Size = New System.Drawing.Size(102, 24)
        Me.RadiobuttonList3.TabIndex = 7
        Me.RadiobuttonList3.Value = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 92)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(153, 14)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "One role per person per event"
        '
        'RadiobuttonList2
        '
        Me.RadiobuttonList2.Location = New System.Drawing.Point(224, 59)
        Me.RadiobuttonList2.Name = "RadiobuttonList2"
        Me.RadiobuttonList2.Size = New System.Drawing.Size(102, 22)
        Me.RadiobuttonList2.TabIndex = 5
        Me.RadiobuttonList2.Value = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(144, 14)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "One role per person per day"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(195, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 14)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Not important"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(292, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Important"
        '
        'RadiobuttonList1
        '
        Me.RadiobuttonList1.Location = New System.Drawing.Point(224, 33)
        Me.RadiobuttonList1.Name = "RadiobuttonList1"
        Me.RadiobuttonList1.Size = New System.Drawing.Size(102, 20)
        Me.RadiobuttonList1.TabIndex = 1
        Me.RadiobuttonList1.Value = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(155, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "As few individuals as possible"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Balthazar.My.Resources.Resources.magic_wand1
        Me.PictureBox1.Location = New System.Drawing.Point(12, 13)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadiobuttonList7)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.TextBox2)
        Me.GroupBox2.Controls.Add(Me.TextBox1)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.ComboBox1)
        Me.GroupBox2.Controls.Add(Me.RadiobuttonList4)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.RadiobuttonList5)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.RadiobuttonList6)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 179)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(353, 188)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Role specific citeria"
        '
        'RadiobuttonList7
        '
        Me.RadiobuttonList7.Location = New System.Drawing.Point(224, 153)
        Me.RadiobuttonList7.Name = "RadiobuttonList7"
        Me.RadiobuttonList7.Size = New System.Drawing.Size(102, 26)
        Me.RadiobuttonList7.TabIndex = 12
        Me.RadiobuttonList7.Value = 1
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 159)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(74, 14)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "No dead days"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(144, 96)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(44, 20)
        Me.TextBox2.TabIndex = 10
        Me.TextBox2.Text = "5"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(144, 69)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(44, 20)
        Me.TextBox1.TabIndex = 9
        Me.TextBox1.Text = "1"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 22)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(28, 14)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "Role"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(40, 19)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(148, 22)
        Me.ComboBox1.TabIndex = 8
        '
        'RadiobuttonList4
        '
        Me.RadiobuttonList4.Location = New System.Drawing.Point(224, 123)
        Me.RadiobuttonList4.Name = "RadiobuttonList4"
        Me.RadiobuttonList4.Size = New System.Drawing.Size(102, 24)
        Me.RadiobuttonList4.TabIndex = 7
        Me.RadiobuttonList4.Value = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 128)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 14)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "No dead shifts"
        '
        'RadiobuttonList5
        '
        Me.RadiobuttonList5.Location = New System.Drawing.Point(224, 95)
        Me.RadiobuttonList5.Name = "RadiobuttonList5"
        Me.RadiobuttonList5.Size = New System.Drawing.Size(102, 22)
        Me.RadiobuttonList5.TabIndex = 5
        Me.RadiobuttonList5.Value = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 99)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(110, 14)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Max shifts in one day"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(195, 52)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 14)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "Not important"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(292, 52)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 14)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "Important"
        '
        'RadiobuttonList6
        '
        Me.RadiobuttonList6.Location = New System.Drawing.Point(224, 69)
        Me.RadiobuttonList6.Name = "RadiobuttonList6"
        Me.RadiobuttonList6.Size = New System.Drawing.Size(102, 20)
        Me.RadiobuttonList6.TabIndex = 1
        Me.RadiobuttonList6.Value = 1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 72)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(106, 14)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Min shifts in one day"
        '
        'pnlProgress
        '
        Me.pnlProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlProgress.Controls.Add(Me.pbStatus)
        Me.pnlProgress.Controls.Add(Me.lblStatus)
        Me.pnlProgress.Location = New System.Drawing.Point(12, 441)
        Me.pnlProgress.Name = "pnlProgress"
        Me.pnlProgress.Size = New System.Drawing.Size(260, 37)
        Me.pnlProgress.TabIndex = 9
        Me.pnlProgress.Visible = False
        '
        'pbStatus
        '
        Me.pbStatus.Location = New System.Drawing.Point(3, 21)
        Me.pbStatus.Name = "pbStatus"
        Me.pbStatus.Size = New System.Drawing.Size(254, 13)
        Me.pbStatus.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pbStatus.TabIndex = 10
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(6, 4)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 14)
        Me.lblStatus.TabIndex = 10
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtCrossovers)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.txtMutation)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.txtGenerations)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.txtIndividuals)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 373)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(353, 62)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Optimization settings"
        '
        'txtCrossovers
        '
        Me.txtCrossovers.Location = New System.Drawing.Point(255, 33)
        Me.txtCrossovers.Name = "txtCrossovers"
        Me.txtCrossovers.Size = New System.Drawing.Size(77, 20)
        Me.txtCrossovers.TabIndex = 7
        Me.txtCrossovers.Text = "5"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(255, 16)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(87, 14)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "Max Crossovers"
        '
        'txtMutation
        '
        Me.txtMutation.Location = New System.Drawing.Point(172, 33)
        Me.txtMutation.Name = "txtMutation"
        Me.txtMutation.Size = New System.Drawing.Size(77, 20)
        Me.txtMutation.TabIndex = 5
        Me.txtMutation.Text = "10"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(172, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(72, 14)
        Me.Label16.TabIndex = 4
        Me.Label16.Text = "Mutation prob"
        '
        'txtGenerations
        '
        Me.txtGenerations.Location = New System.Drawing.Point(89, 33)
        Me.txtGenerations.Name = "txtGenerations"
        Me.txtGenerations.Size = New System.Drawing.Size(77, 20)
        Me.txtGenerations.TabIndex = 3
        Me.txtGenerations.Text = "1"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(89, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(66, 14)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "Generations"
        '
        'txtIndividuals
        '
        Me.txtIndividuals.Location = New System.Drawing.Point(6, 33)
        Me.txtIndividuals.Name = "txtIndividuals"
        Me.txtIndividuals.Size = New System.Drawing.Size(77, 20)
        Me.txtIndividuals.TabIndex = 1
        Me.txtIndividuals.Text = "100"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(57, 14)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Individuals"
        '
        'frmOptimize
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(373, 490)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.pnlProgress)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOptimize"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Optimize staff schedule"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.pnlProgress.ResumeLayout(False)
        Me.pnlProgress.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RadiobuttonList1 As Balthazar.RadiobuttonList
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents RadiobuttonList3 As Balthazar.RadiobuttonList
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents RadiobuttonList2 As Balthazar.RadiobuttonList
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents RadiobuttonList4 As Balthazar.RadiobuttonList
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents RadiobuttonList5 As Balthazar.RadiobuttonList
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents RadiobuttonList6 As Balthazar.RadiobuttonList
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents RadiobuttonList7 As Balthazar.RadiobuttonList
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents pnlProgress As System.Windows.Forms.Panel
    Friend WithEvents pbStatus As System.Windows.Forms.ProgressBar
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtGenerations As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtIndividuals As System.Windows.Forms.TextBox
    Friend WithEvents txtCrossovers As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtMutation As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
End Class
