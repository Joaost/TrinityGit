<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInfo
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInfo))
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlInfo = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbTarget = New System.Windows.Forms.ComboBox()
        Me.lblUpdatedTo = New System.Windows.Forms.Label()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.cmbFF = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cmdCalculate = New System.Windows.Forms.Button()
        Me.grdReach = New System.Windows.Forms.DataGridView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblThirdTarget = New System.Windows.Forms.Label()
        Me.txtThird = New System.Windows.Forms.TextBox()
        Me.cmbThirdUni = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblSecondTarget = New System.Windows.Forms.Label()
        Me.txtSec = New System.Windows.Forms.TextBox()
        Me.cmbSecondUni = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblMainTarget = New System.Windows.Forms.Label()
        Me.txtMain = New System.Windows.Forms.TextBox()
        Me.cmbMainUni = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.picPin = New System.Windows.Forms.PictureBox()
        Me.cmdEditProduct = New System.Windows.Forms.Button()
        Me.cmdAddProduct = New System.Windows.Forms.Button()
        Me.cmbProduct = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmdEditClient = New System.Windows.Forms.Button()
        Me.cmdAddClient = New System.Windows.Forms.Button()
        Me.cmbClient = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlannedReach = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colActualReach = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlInfo.SuspendLayout
        CType(Me.PictureBox7,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grdReach,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox3.SuspendLayout
        CType(Me.picPin,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'pnlInfo
        '
        Me.pnlInfo.Controls.Add(Me.Label1)
        Me.pnlInfo.Controls.Add(Me.cmbTarget)
        Me.pnlInfo.Controls.Add(Me.lblUpdatedTo)
        Me.pnlInfo.Controls.Add(Me.PictureBox7)
        Me.pnlInfo.Controls.Add(Me.cmbFF)
        Me.pnlInfo.Controls.Add(Me.Label18)
        Me.pnlInfo.Controls.Add(Me.cmdCalculate)
        Me.pnlInfo.Controls.Add(Me.grdReach)
        Me.pnlInfo.Controls.Add(Me.GroupBox3)
        Me.pnlInfo.Controls.Add(Me.picPin)
        Me.pnlInfo.Controls.Add(Me.cmdEditProduct)
        Me.pnlInfo.Controls.Add(Me.cmdAddProduct)
        Me.pnlInfo.Controls.Add(Me.cmbProduct)
        Me.pnlInfo.Controls.Add(Me.Label10)
        Me.pnlInfo.Controls.Add(Me.cmdEditClient)
        Me.pnlInfo.Controls.Add(Me.cmdAddClient)
        Me.pnlInfo.Controls.Add(Me.cmbClient)
        Me.pnlInfo.Controls.Add(Me.Label11)
        Me.pnlInfo.Controls.Add(Me.txtName)
        Me.pnlInfo.Controls.Add(Me.Label12)
        Me.pnlInfo.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlInfo.Location = New System.Drawing.Point(0, 0)
        Me.pnlInfo.Name = "pnlInfo"
        Me.pnlInfo.Size = New System.Drawing.Size(229, 482)
        Me.pnlInfo.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(142, 309)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "Target"
        '
        'cmbTarget
        '
        Me.cmbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTarget.FormattingEnabled = true
        Me.cmbTarget.Items.AddRange(New Object() {"Main", "Second"})
        Me.cmbTarget.Location = New System.Drawing.Point(142, 325)
        Me.cmbTarget.Name = "cmbTarget"
        Me.cmbTarget.Size = New System.Drawing.Size(73, 21)
        Me.cmbTarget.TabIndex = 45
        '
        'lblUpdatedTo
        '
        Me.lblUpdatedTo.AutoSize = true
        Me.lblUpdatedTo.Location = New System.Drawing.Point(8, 461)
        Me.lblUpdatedTo.Name = "lblUpdatedTo"
        Me.lblUpdatedTo.Size = New System.Drawing.Size(76, 13)
        Me.lblUpdatedTo.TabIndex = 44
        Me.lblUpdatedTo.Text = "Updated to: -"
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = Global.clTrinity.My.Resources.Resources.target_group_2
        Me.PictureBox7.Location = New System.Drawing.Point(16, 134)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(21, 20)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox7.TabIndex = 43
        Me.PictureBox7.TabStop = false
        '
        'cmbFF
        '
        Me.cmbFF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFF.FormattingEnabled = true
        Me.cmbFF.Items.AddRange(New Object() {"1+", "2+", "3+", "4+", "5+", "6+", "7+", "8+", "9+", "10+"})
        Me.cmbFF.Location = New System.Drawing.Point(170, 276)
        Me.cmbFF.Name = "cmbFF"
        Me.cmbFF.Size = New System.Drawing.Size(47, 21)
        Me.cmbFF.TabIndex = 42
        '
        'Label18
        '
        Me.Label18.AutoSize = true
        Me.Label18.Location = New System.Drawing.Point(142, 279)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(22, 13)
        Me.Label18.TabIndex = 41
        Me.Label18.Text = "FF:"
        '
        'cmdCalculate
        '
        Me.cmdCalculate.FlatAppearance.BorderSize = 0
        Me.cmdCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCalculate.Image = Global.clTrinity.My.Resources.Resources.calculator_2_small
        Me.cmdCalculate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCalculate.Location = New System.Drawing.Point(142, 244)
        Me.cmdCalculate.Name = "cmdCalculate"
        Me.cmdCalculate.Size = New System.Drawing.Size(80, 31)
        Me.cmdCalculate.TabIndex = 40
        Me.cmdCalculate.Text = "Calculate"
        Me.cmdCalculate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.cmdCalculate, "Calculate Frequency Focus")
        Me.cmdCalculate.UseVisualStyleBackColor = true
        '
        'grdReach
        '
        Me.grdReach.AllowUserToAddRows = false
        Me.grdReach.AllowUserToDeleteRows = false
        Me.grdReach.AllowUserToResizeColumns = false
        Me.grdReach.AllowUserToResizeRows = false
        Me.grdReach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdReach.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colPlannedReach, Me.colActualReach})
        Me.grdReach.Location = New System.Drawing.Point(27, 242)
        Me.grdReach.Name = "grdReach"
        Me.grdReach.ReadOnly = true
        Me.grdReach.RowHeadersVisible = false
        Me.grdReach.Size = New System.Drawing.Size(109, 208)
        Me.grdReach.TabIndex = 39
        Me.grdReach.VirtualMode = true
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblThirdTarget)
        Me.GroupBox3.Controls.Add(Me.txtThird)
        Me.GroupBox3.Controls.Add(Me.cmbThirdUni)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.lblSecondTarget)
        Me.GroupBox3.Controls.Add(Me.txtSec)
        Me.GroupBox3.Controls.Add(Me.cmbSecondUni)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.lblMainTarget)
        Me.GroupBox3.Controls.Add(Me.txtMain)
        Me.GroupBox3.Controls.Add(Me.cmbMainUni)
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 138)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(219, 98)
        Me.GroupBox3.TabIndex = 38
        Me.GroupBox3.TabStop = false
        Me.GroupBox3.Text = "        Targets"
        '
        'lblThirdTarget
        '
        Me.lblThirdTarget.AutoSize = true
        Me.lblThirdTarget.Location = New System.Drawing.Point(165, 72)
        Me.lblThirdTarget.Name = "lblThirdTarget"
        Me.lblThirdTarget.Size = New System.Drawing.Size(13, 13)
        Me.lblThirdTarget.TabIndex = 13
        Me.lblThirdTarget.Text = "0"
        '
        'txtThird
        '
        Me.txtThird.Enabled = false
        Me.txtThird.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtThird.Location = New System.Drawing.Point(51, 71)
        Me.txtThird.Name = "txtThird"
        Me.txtThird.Size = New System.Drawing.Size(46, 20)
        Me.txtThird.TabIndex = 12
        '
        'cmbThirdUni
        '
        Me.cmbThirdUni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbThirdUni.Enabled = false
        Me.cmbThirdUni.FormattingEnabled = true
        Me.cmbThirdUni.Location = New System.Drawing.Point(103, 70)
        Me.cmbThirdUni.Name = "cmbThirdUni"
        Me.cmbThirdUni.Size = New System.Drawing.Size(56, 21)
        Me.cmbThirdUni.TabIndex = 11
        '
        'Label9
        '
        Me.Label9.AutoSize = true
        Me.Label9.Location = New System.Drawing.Point(7, 72)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(33, 13)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Third"
        '
        'lblSecondTarget
        '
        Me.lblSecondTarget.AutoSize = true
        Me.lblSecondTarget.Location = New System.Drawing.Point(165, 51)
        Me.lblSecondTarget.Name = "lblSecondTarget"
        Me.lblSecondTarget.Size = New System.Drawing.Size(13, 13)
        Me.lblSecondTarget.TabIndex = 9
        Me.lblSecondTarget.Text = "0"
        '
        'txtSec
        '
        Me.txtSec.Enabled = false
        Me.txtSec.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtSec.Location = New System.Drawing.Point(51, 48)
        Me.txtSec.Name = "txtSec"
        Me.txtSec.Size = New System.Drawing.Size(46, 20)
        Me.txtSec.TabIndex = 8
        '
        'cmbSecondUni
        '
        Me.cmbSecondUni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSecondUni.Enabled = false
        Me.cmbSecondUni.FormattingEnabled = true
        Me.cmbSecondUni.Location = New System.Drawing.Point(103, 46)
        Me.cmbSecondUni.Name = "cmbSecondUni"
        Me.cmbSecondUni.Size = New System.Drawing.Size(56, 21)
        Me.cmbSecondUni.TabIndex = 7
        '
        'Label14
        '
        Me.Label14.AutoSize = true
        Me.Label14.Location = New System.Drawing.Point(7, 51)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(45, 13)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "Second"
        '
        'Label15
        '
        Me.Label15.AutoSize = true
        Me.Label15.Location = New System.Drawing.Point(168, 9)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(27, 13)
        Me.Label15.TabIndex = 5
        Me.Label15.Text = "Size"
        '
        'lblMainTarget
        '
        Me.lblMainTarget.AutoSize = true
        Me.lblMainTarget.Location = New System.Drawing.Point(165, 28)
        Me.lblMainTarget.Name = "lblMainTarget"
        Me.lblMainTarget.Size = New System.Drawing.Size(13, 13)
        Me.lblMainTarget.TabIndex = 4
        Me.lblMainTarget.Text = "0"
        '
        'txtMain
        '
        Me.txtMain.Enabled = false
        Me.txtMain.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMain.Location = New System.Drawing.Point(51, 25)
        Me.txtMain.Name = "txtMain"
        Me.txtMain.Size = New System.Drawing.Size(46, 20)
        Me.txtMain.TabIndex = 3
        '
        'cmbMainUni
        '
        Me.cmbMainUni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMainUni.Enabled = false
        Me.cmbMainUni.FormattingEnabled = true
        Me.cmbMainUni.Location = New System.Drawing.Point(103, 23)
        Me.cmbMainUni.Name = "cmbMainUni"
        Me.cmbMainUni.Size = New System.Drawing.Size(56, 21)
        Me.cmbMainUni.TabIndex = 2
        '
        'Label17
        '
        Me.Label17.AutoSize = true
        Me.Label17.Location = New System.Drawing.Point(7, 28)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(33, 13)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Main"
        '
        'picPin
        '
        Me.picPin.Image = Global.clTrinity.My.Resources.Resources.pin_2
        Me.picPin.Location = New System.Drawing.Point(209, 3)
        Me.picPin.Name = "picPin"
        Me.picPin.Size = New System.Drawing.Size(20, 20)
        Me.picPin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picPin.TabIndex = 37
        Me.picPin.TabStop = false
        '
        'cmdEditProduct
        '
        Me.cmdEditProduct.FlatAppearance.BorderSize = 0
        Me.cmdEditProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdEditProduct.Image = CType(resources.GetObject("cmdEditProduct.Image"),System.Drawing.Image)
        Me.cmdEditProduct.Location = New System.Drawing.Point(200, 98)
        Me.cmdEditProduct.Name = "cmdEditProduct"
        Me.cmdEditProduct.Size = New System.Drawing.Size(22, 20)
        Me.cmdEditProduct.TabIndex = 31
        Me.ToolTip.SetToolTip(Me.cmdEditProduct, "Edit Client")
        Me.cmdEditProduct.UseVisualStyleBackColor = true
        '
        'cmdAddProduct
        '
        Me.cmdAddProduct.FlatAppearance.BorderSize = 0
        Me.cmdAddProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddProduct.Image = CType(resources.GetObject("cmdAddProduct.Image"),System.Drawing.Image)
        Me.cmdAddProduct.Location = New System.Drawing.Point(175, 98)
        Me.cmdAddProduct.Name = "cmdAddProduct"
        Me.cmdAddProduct.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddProduct.TabIndex = 30
        Me.ToolTip.SetToolTip(Me.cmdAddProduct, "Add Client")
        Me.cmdAddProduct.UseVisualStyleBackColor = true
        '
        'cmbProduct
        '
        Me.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProduct.FormattingEnabled = true
        Me.cmbProduct.Location = New System.Drawing.Point(6, 98)
        Me.cmbProduct.Name = "cmbProduct"
        Me.cmbProduct.Size = New System.Drawing.Size(163, 21)
        Me.cmbProduct.Sorted = true
        Me.cmbProduct.TabIndex = 29
        '
        'Label10
        '
        Me.Label10.AutoSize = true
        Me.Label10.Location = New System.Drawing.Point(3, 83)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(47, 13)
        Me.Label10.TabIndex = 28
        Me.Label10.Text = "Product"
        '
        'cmdEditClient
        '
        Me.cmdEditClient.FlatAppearance.BorderSize = 0
        Me.cmdEditClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdEditClient.Image = CType(resources.GetObject("cmdEditClient.Image"),System.Drawing.Image)
        Me.cmdEditClient.Location = New System.Drawing.Point(200, 59)
        Me.cmdEditClient.Name = "cmdEditClient"
        Me.cmdEditClient.Size = New System.Drawing.Size(22, 20)
        Me.cmdEditClient.TabIndex = 27
        Me.ToolTip.SetToolTip(Me.cmdEditClient, "Edit Client")
        Me.cmdEditClient.UseVisualStyleBackColor = true
        '
        'cmdAddClient
        '
        Me.cmdAddClient.FlatAppearance.BorderSize = 0
        Me.cmdAddClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddClient.Image = CType(resources.GetObject("cmdAddClient.Image"),System.Drawing.Image)
        Me.cmdAddClient.Location = New System.Drawing.Point(175, 59)
        Me.cmdAddClient.Name = "cmdAddClient"
        Me.cmdAddClient.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddClient.TabIndex = 26
        Me.ToolTip.SetToolTip(Me.cmdAddClient, "Add Client")
        Me.cmdAddClient.UseVisualStyleBackColor = true
        '
        'cmbClient
        '
        Me.cmbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClient.FormattingEnabled = true
        Me.cmbClient.Location = New System.Drawing.Point(6, 59)
        Me.cmbClient.Name = "cmbClient"
        Me.cmbClient.Size = New System.Drawing.Size(163, 21)
        Me.cmbClient.Sorted = true
        Me.cmbClient.TabIndex = 25
        '
        'Label11
        '
        Me.Label11.AutoSize = true
        Me.Label11.Location = New System.Drawing.Point(3, 44)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(37, 13)
        Me.Label11.TabIndex = 24
        Me.Label11.Text = "Client"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(6, 19)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(163, 22)
        Me.txtName.TabIndex = 23
        '
        'Label12
        '
        Me.Label12.AutoSize = true
        Me.Label12.Location = New System.Drawing.Point(3, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(91, 13)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "Campaign Name"
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle3.Format = "N1"
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn1.HeaderText = "Planned"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = true
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle4.Format = "N1"
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn2.HeaderText = "Actual"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = true
        Me.DataGridViewTextBoxColumn2.Width = 50
        '
        'colPlannedReach
        '
        Me.colPlannedReach.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle1.Format = "N1"
        Me.colPlannedReach.DefaultCellStyle = DataGridViewCellStyle1
        Me.colPlannedReach.HeaderText = "Planned"
        Me.colPlannedReach.Name = "colPlannedReach"
        Me.colPlannedReach.ReadOnly = true
        '
        'colActualReach
        '
        Me.colActualReach.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle2.Format = "N1"
        Me.colActualReach.DefaultCellStyle = DataGridViewCellStyle2
        Me.colActualReach.HeaderText = "Actual"
        Me.colActualReach.Name = "colActualReach"
        Me.colActualReach.ReadOnly = true
        '
        'frmInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(229, 482)
        Me.Controls.Add(Me.pnlInfo)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmInfo"
        Me.Text = "Campaign Info"
        Me.pnlInfo.ResumeLayout(false)
        Me.pnlInfo.PerformLayout
        CType(Me.PictureBox7,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdReach,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        CType(Me.picPin,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents pnlInfo As System.Windows.Forms.Panel
    Friend WithEvents PictureBox7 As System.Windows.Forms.PictureBox
    Friend WithEvents cmbFF As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cmdCalculate As System.Windows.Forms.Button
    Friend WithEvents grdReach As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblThirdTarget As System.Windows.Forms.Label
    Friend WithEvents cmbThirdUni As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblSecondTarget As System.Windows.Forms.Label
    Friend WithEvents cmbSecondUni As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblMainTarget As System.Windows.Forms.Label
    Friend WithEvents cmbMainUni As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents picPin As System.Windows.Forms.PictureBox
    Friend WithEvents cmdEditProduct As System.Windows.Forms.Button
    Friend WithEvents cmdAddProduct As System.Windows.Forms.Button
    Friend WithEvents cmbProduct As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmdEditClient As System.Windows.Forms.Button
    Friend WithEvents cmdAddClient As System.Windows.Forms.Button
    Friend WithEvents cmbClient As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblUpdatedTo As System.Windows.Forms.Label
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents txtThird As System.Windows.Forms.TextBox
    Friend WithEvents txtSec As System.Windows.Forms.TextBox
    Friend WithEvents txtMain As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbTarget As System.Windows.Forms.ComboBox
    Friend WithEvents colPlannedReach As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colActualReach As Windows.Forms.DataGridViewTextBoxColumn
End Class
