<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPreCampaignAnalysis
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPreCampaignAnalysis))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbTemplate = New System.Windows.Forms.ComboBox()
        Me.chkHide = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.dlgOpen = New System.Windows.Forms.OpenFileDialog()
        Me.chkCompensations = New System.Windows.Forms.CheckBox()
        Me.chkPrintCombinations = New System.Windows.Forms.CheckBox()
        Me.chkPlannedNet = New System.Windows.Forms.CheckBox()
        Me.chkPlannedGrossConfirmedNet = New System.Windows.Forms.CheckBox()
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.TableLayoutPanel1.SuspendLayout
        Me.SuspendLayout
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.analysis_2_30x30
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = false
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Template"
        '
        'cmbTemplate
        '
        Me.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplate.FormattingEnabled = true
        Me.cmbTemplate.Location = New System.Drawing.Point(12, 64)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Size = New System.Drawing.Size(291, 21)
        Me.cmbTemplate.TabIndex = 10
        '
        'chkHide
        '
        Me.chkHide.AutoSize = true
        Me.chkHide.Checked = true
        Me.chkHide.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkHide.Location = New System.Drawing.Point(15, 140)
        Me.chkHide.Name = "chkHide"
        Me.chkHide.Size = New System.Drawing.Size(112, 17)
        Me.chkHide.TabIndex = 11
        Me.chkHide.Text = "Hide data sheets"
        Me.chkHide.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(157, 218)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 31)
        Me.TableLayoutPanel1.TabIndex = 12
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.FlatAppearance.BorderSize = 0
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 25)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.FlatAppearance.BorderSize = 0
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 25)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'dlgOpen
        '
        Me.dlgOpen.FileName = "*.xls"
        Me.dlgOpen.Filter = "Excel Workbooks|*.xls"
        '
        'chkCompensations
        '
        Me.chkCompensations.AutoSize = true
        Me.chkCompensations.Checked = true
        Me.chkCompensations.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCompensations.Location = New System.Drawing.Point(15, 117)
        Me.chkCompensations.Name = "chkCompensations"
        Me.chkCompensations.Size = New System.Drawing.Size(145, 17)
        Me.chkCompensations.TabIndex = 13
        Me.chkCompensations.Text = "Include compensations"
        Me.chkCompensations.UseVisualStyleBackColor = true
        '
        'chkPrintCombinations
        '
        Me.chkPrintCombinations.AutoSize = true
        Me.chkPrintCombinations.Checked = true
        Me.chkPrintCombinations.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPrintCombinations.Location = New System.Drawing.Point(15, 92)
        Me.chkPrintCombinations.Name = "chkPrintCombinations"
        Me.chkPrintCombinations.Size = New System.Drawing.Size(227, 17)
        Me.chkPrintCombinations.TabIndex = 14
        Me.chkPrintCombinations.Text = "Add combinations with joint allocation"
        Me.chkPrintCombinations.UseVisualStyleBackColor = true
        '
        'chkPlannedNet
        '
        Me.chkPlannedNet.AutoSize = true
        Me.chkPlannedNet.Checked = true
        Me.chkPlannedNet.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPlannedNet.Location = New System.Drawing.Point(15, 164)
        Me.chkPlannedNet.Name = "chkPlannedNet"
        Me.chkPlannedNet.Size = New System.Drawing.Size(88, 17)
        Me.chkPlannedNet.TabIndex = 15
        Me.chkPlannedNet.Text = "Planned net"
        Me.chkPlannedNet.UseVisualStyleBackColor = true
        '
        'chkPlannedGrossConfirmedNet
        '
        Me.chkPlannedGrossConfirmedNet.AutoSize = true
        Me.chkPlannedGrossConfirmedNet.Location = New System.Drawing.Point(15, 187)
        Me.chkPlannedGrossConfirmedNet.Name = "chkPlannedGrossConfirmedNet"
        Me.chkPlannedGrossConfirmedNet.Size = New System.Drawing.Size(177, 17)
        Me.chkPlannedGrossConfirmedNet.TabIndex = 28
        Me.chkPlannedGrossConfirmedNet.Text = "Planned gross, confirmed net"
        Me.chkPlannedGrossConfirmedNet.UseVisualStyleBackColor = true
        '
        'frmPreCampaignAnalysis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(315, 261)
        Me.Controls.Add(Me.chkPlannedGrossConfirmedNet)
        Me.Controls.Add(Me.chkPlannedNet)
        Me.Controls.Add(Me.chkPrintCombinations)
        Me.Controls.Add(Me.chkCompensations)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.chkHide)
        Me.Controls.Add(Me.cmbTemplate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmPreCampaignAnalysis"
        Me.Text = "Export Pre-campaign analysis"
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents chkHide As System.Windows.Forms.CheckBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents dlgOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents chkCompensations As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrintCombinations As System.Windows.Forms.CheckBox
    Friend WithEvents chkPlannedNet As System.Windows.Forms.CheckBox
    Friend WithEvents chkPlannedGrossConfirmedNet As Windows.Forms.CheckBox
End Class
