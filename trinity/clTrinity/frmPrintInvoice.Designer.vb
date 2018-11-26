<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintInvoice
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintInvoice))
        Me.frmPrint = New System.Windows.Forms.Button()
        Me.chkBudgetFilm = New System.Windows.Forms.CheckBox()
        Me.chkWeeklyBudget = New System.Windows.Forms.CheckBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rdbNot = New System.Windows.Forms.RadioButton()
        Me.chkCombinedSingle = New System.Windows.Forms.CheckBox()
        Me.rdbAll = New System.Windows.Forms.RadioButton()
        Me.rdbOne = New System.Windows.Forms.RadioButton()
        Me.chkSaveSettings = New System.Windows.Forms.CheckBox()
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox2.SuspendLayout
        Me.SuspendLayout
        '
        'frmPrint
        '
        Me.frmPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.frmPrint.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.frmPrint.FlatAppearance.BorderSize = 0
        Me.frmPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.frmPrint.Location = New System.Drawing.Point(285, 180)
        Me.frmPrint.Name = "frmPrint"
        Me.frmPrint.Size = New System.Drawing.Size(75, 25)
        Me.frmPrint.TabIndex = 0
        Me.frmPrint.Text = "Print"
        Me.frmPrint.UseVisualStyleBackColor = true
        '
        'chkBudgetFilm
        '
        Me.chkBudgetFilm.AutoSize = true
        Me.chkBudgetFilm.Checked = true
        Me.chkBudgetFilm.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBudgetFilm.Location = New System.Drawing.Point(231, 111)
        Me.chkBudgetFilm.Name = "chkBudgetFilm"
        Me.chkBudgetFilm.Size = New System.Drawing.Size(147, 17)
        Me.chkBudgetFilm.TabIndex = 2
        Me.chkBudgetFilm.Text = "Include budget per film"
        Me.chkBudgetFilm.UseVisualStyleBackColor = true
        '
        'chkWeeklyBudget
        '
        Me.chkWeeklyBudget.AutoSize = true
        Me.chkWeeklyBudget.Checked = true
        Me.chkWeeklyBudget.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkWeeklyBudget.Location = New System.Drawing.Point(231, 76)
        Me.chkWeeklyBudget.Name = "chkWeeklyBudget"
        Me.chkWeeklyBudget.Size = New System.Drawing.Size(143, 17)
        Me.chkWeeklyBudget.TabIndex = 3
        Me.chkWeeklyBudget.Text = "Include weekly budget"
        Me.chkWeeklyBudget.UseVisualStyleBackColor = true
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.excel_2
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = false
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rdbNot)
        Me.GroupBox2.Controls.Add(Me.chkCombinedSingle)
        Me.GroupBox2.Controls.Add(Me.rdbAll)
        Me.GroupBox2.Controls.Add(Me.rdbOne)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 43)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(209, 116)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = false
        Me.GroupBox2.Text = "Combined channels"
        '
        'rdbNot
        '
        Me.rdbNot.AutoSize = true
        Me.rdbNot.Location = New System.Drawing.Point(11, 19)
        Me.rdbNot.Name = "rdbNot"
        Me.rdbNot.Size = New System.Drawing.Size(200, 17)
        Me.rdbNot.TabIndex = 3
        Me.rdbNot.Text = "Dont print combinations together"
        Me.rdbNot.UseVisualStyleBackColor = true
        '
        'chkCombinedSingle
        '
        Me.chkCombinedSingle.AutoSize = true
        Me.chkCombinedSingle.Location = New System.Drawing.Point(11, 92)
        Me.chkCombinedSingle.Name = "chkCombinedSingle"
        Me.chkCombinedSingle.Size = New System.Drawing.Size(98, 17)
        Me.chkCombinedSingle.TabIndex = 2
        Me.chkCombinedSingle.Text = "Print as single"
        Me.chkCombinedSingle.UseVisualStyleBackColor = true
        '
        'rdbAll
        '
        Me.rdbAll.AutoSize = true
        Me.rdbAll.Location = New System.Drawing.Point(11, 68)
        Me.rdbAll.Name = "rdbAll"
        Me.rdbAll.Size = New System.Drawing.Size(185, 17)
        Me.rdbAll.TabIndex = 1
        Me.rdbAll.Text = "Print all combinations together"
        Me.rdbAll.UseVisualStyleBackColor = true
        '
        'rdbOne
        '
        Me.rdbOne.AutoSize = true
        Me.rdbOne.Checked = true
        Me.rdbOne.Location = New System.Drawing.Point(11, 43)
        Me.rdbOne.Name = "rdbOne"
        Me.rdbOne.Size = New System.Drawing.Size(217, 17)
        Me.rdbOne.TabIndex = 0
        Me.rdbOne.TabStop = true
        Me.rdbOne.Text = "Print one unit combinations together"
        Me.rdbOne.UseVisualStyleBackColor = true
        '
        'chkSaveSettings
        '
        Me.chkSaveSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chkSaveSettings.AutoSize = true
        Me.chkSaveSettings.Location = New System.Drawing.Point(14, 171)
        Me.chkSaveSettings.Name = "chkSaveSettings"
        Me.chkSaveSettings.Size = New System.Drawing.Size(147, 17)
        Me.chkSaveSettings.TabIndex = 10
        Me.chkSaveSettings.Text = "Save settings as default"
        Me.chkSaveSettings.UseVisualStyleBackColor = true
        '
        'frmPrintInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(373, 217)
        Me.Controls.Add(Me.chkSaveSettings)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.chkWeeklyBudget)
        Me.Controls.Add(Me.chkBudgetFilm)
        Me.Controls.Add(Me.frmPrint)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmPrintInvoice"
        Me.Text = "Print Invoice details"
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents frmPrint As System.Windows.Forms.Button
    Friend WithEvents chkBudgetFilm As System.Windows.Forms.CheckBox
    Friend WithEvents chkWeeklyBudget As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbAll As System.Windows.Forms.RadioButton
    Friend WithEvents rdbOne As System.Windows.Forms.RadioButton
    Friend WithEvents chkSaveSettings As System.Windows.Forms.CheckBox
    Friend WithEvents chkCombinedSingle As System.Windows.Forms.CheckBox
    Friend WithEvents rdbNot As System.Windows.Forms.RadioButton
End Class
