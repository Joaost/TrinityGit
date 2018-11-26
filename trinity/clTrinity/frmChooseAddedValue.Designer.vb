<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChooseAddedValue
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChooseAddedValue))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.optSetAsOld = New System.Windows.Forms.RadioButton()
        Me.grpSetAs = New System.Windows.Forms.GroupBox()
        Me.cmbAV = New System.Windows.Forms.ComboBox()
        Me.optCreateNew = New System.Windows.Forms.RadioButton()
        Me.grpCreateNew = New System.Windows.Forms.GroupBox()
        Me.txtNetIdx = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtGrossIdx = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel1.SuspendLayout
        Me.grpSetAs.SuspendLayout
        Me.grpCreateNew.SuspendLayout
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(171, 270)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.FlatAppearance.BorderSize = 0
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
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
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(9, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(271, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "This remark was found on at least one of the spots:"
        '
        'lblRemark
        '
        Me.lblRemark.AutoSize = true
        Me.lblRemark.Location = New System.Drawing.Point(266, 9)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(0, 13)
        Me.lblRemark.TabIndex = 2
        '
        'optSetAsOld
        '
        Me.optSetAsOld.AutoSize = true
        Me.optSetAsOld.Checked = true
        Me.optSetAsOld.Location = New System.Drawing.Point(21, 55)
        Me.optSetAsOld.Name = "optSetAsOld"
        Me.optSetAsOld.Size = New System.Drawing.Size(64, 17)
        Me.optSetAsOld.TabIndex = 6
        Me.optSetAsOld.TabStop = true
        Me.optSetAsOld.Text = "Set as..."
        Me.optSetAsOld.UseVisualStyleBackColor = true
        '
        'grpSetAs
        '
        Me.grpSetAs.Controls.Add(Me.cmbAV)
        Me.grpSetAs.Location = New System.Drawing.Point(12, 57)
        Me.grpSetAs.Name = "grpSetAs"
        Me.grpSetAs.Size = New System.Drawing.Size(200, 59)
        Me.grpSetAs.TabIndex = 7
        Me.grpSetAs.TabStop = false
        Me.grpSetAs.Text = "grpOldFilm"
        '
        'cmbAV
        '
        Me.cmbAV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAV.FormattingEnabled = true
        Me.cmbAV.Location = New System.Drawing.Point(9, 22)
        Me.cmbAV.Name = "cmbAV"
        Me.cmbAV.Size = New System.Drawing.Size(182, 21)
        Me.cmbAV.TabIndex = 0
        '
        'optCreateNew
        '
        Me.optCreateNew.AutoSize = true
        Me.optCreateNew.Location = New System.Drawing.Point(21, 120)
        Me.optCreateNew.Name = "optCreateNew"
        Me.optCreateNew.Size = New System.Drawing.Size(154, 17)
        Me.optCreateNew.TabIndex = 8
        Me.optCreateNew.Text = "Create new Added Value:"
        Me.optCreateNew.UseVisualStyleBackColor = true
        '
        'grpCreateNew
        '
        Me.grpCreateNew.Controls.Add(Me.txtNetIdx)
        Me.grpCreateNew.Controls.Add(Me.Label4)
        Me.grpCreateNew.Controls.Add(Me.txtGrossIdx)
        Me.grpCreateNew.Controls.Add(Me.Label3)
        Me.grpCreateNew.Controls.Add(Me.txtName)
        Me.grpCreateNew.Controls.Add(Me.Label2)
        Me.grpCreateNew.Enabled = false
        Me.grpCreateNew.Location = New System.Drawing.Point(12, 122)
        Me.grpCreateNew.Name = "grpCreateNew"
        Me.grpCreateNew.Size = New System.Drawing.Size(200, 142)
        Me.grpCreateNew.TabIndex = 9
        Me.grpCreateNew.TabStop = false
        Me.grpCreateNew.Text = "grpOldFilm"
        '
        'txtNetIdx
        '
        Me.txtNetIdx.Location = New System.Drawing.Point(6, 112)
        Me.txtNetIdx.Name = "txtNetIdx"
        Me.txtNetIdx.Size = New System.Drawing.Size(134, 22)
        Me.txtNetIdx.TabIndex = 14
        Me.txtNetIdx.Text = "100"
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(6, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Net index"
        '
        'txtGrossIdx
        '
        Me.txtGrossIdx.Location = New System.Drawing.Point(6, 73)
        Me.txtGrossIdx.Name = "txtGrossIdx"
        Me.txtGrossIdx.Size = New System.Drawing.Size(134, 22)
        Me.txtGrossIdx.TabIndex = 12
        Me.txtGrossIdx.Text = "100"
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(6, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Gross index"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(6, 34)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(134, 22)
        Me.txtName.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(6, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Name"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.added_value_3_30x32
        Me.PictureBox1.Location = New System.Drawing.Point(12, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(30, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = false
        '
        'frmChooseAddedValue
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(329, 311)
        Me.Controls.Add(Me.optCreateNew)
        Me.Controls.Add(Me.grpCreateNew)
        Me.Controls.Add(Me.optSetAsOld)
        Me.Controls.Add(Me.grpSetAs)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblRemark)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmChooseAddedValue"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Choose added value"
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.grpSetAs.ResumeLayout(false)
        Me.grpCreateNew.ResumeLayout(false)
        Me.grpCreateNew.PerformLayout
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblRemark As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents optSetAsOld As System.Windows.Forms.RadioButton
    Friend WithEvents grpSetAs As System.Windows.Forms.GroupBox
    Friend WithEvents cmbAV As System.Windows.Forms.ComboBox
    Friend WithEvents optCreateNew As System.Windows.Forms.RadioButton
    Friend WithEvents grpCreateNew As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNetIdx As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtGrossIdx As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox

End Class
