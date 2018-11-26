<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQuickCopy
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmQuickCopy))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbChannel = New System.Windows.Forms.ComboBox()
        Me.txtShortname = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.chkIsSponsorship = New System.Windows.Forms.CheckBox()
        Me.chkIsPremium = New System.Windows.Forms.CheckBox()
        Me.chkIsCompensation = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnRBS = New System.Windows.Forms.RadioButton()
        Me.btnSpecifics = New System.Windows.Forms.RadioButton()
        Me.pnlAdd = New System.Windows.Forms.Panel()
        Me.cmbBookingType = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbPricelist = New System.Windows.Forms.ComboBox()
        Me.optNewPrice = New System.Windows.Forms.RadioButton()
        Me.optPricelist = New System.Windows.Forms.RadioButton()
        Me.grpNew = New System.Windows.Forms.GroupBox()
        Me.grdPrice = New System.Windows.Forms.DataGridView()
        Me.colUniSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtAdedgeTarget = New System.Windows.Forms.TextBox()
        Me.lblTarget = New System.Windows.Forms.Label()
        Me.txtTargetName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.pnlAdd.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpNew.SuspendLayout()
        CType(Me.grdPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(177, 445)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 31)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
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
        Me.Cancel_Button.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.Cancel_Button.FlatAppearance.BorderSize = 0
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 25)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Channel"
        '
        'cmbChannel
        '
        Me.cmbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChannel.FormattingEnabled = True
        Me.cmbChannel.Location = New System.Drawing.Point(12, 26)
        Me.cmbChannel.Name = "cmbChannel"
        Me.cmbChannel.Size = New System.Drawing.Size(138, 22)
        Me.cmbChannel.TabIndex = 2
        '
        'txtShortname
        '
        Me.txtShortname.Location = New System.Drawing.Point(92, 17)
        Me.txtShortname.Name = "txtShortname"
        Me.txtShortname.Size = New System.Drawing.Size(74, 20)
        Me.txtShortname.TabIndex = 3
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(2, 17)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(84, 20)
        Me.txtName.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(89, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 14)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Short name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(-1, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Name"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox5)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 273)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(311, 162)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Set properties:"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.chkIsSponsorship)
        Me.GroupBox5.Controls.Add(Me.chkIsPremium)
        Me.GroupBox5.Controls.Add(Me.chkIsCompensation)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 67)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(299, 87)
        Me.GroupBox5.TabIndex = 6
        Me.GroupBox5.TabStop = False
        '
        'chkIsSponsorship
        '
        Me.chkIsSponsorship.AutoSize = True
        Me.chkIsSponsorship.Location = New System.Drawing.Point(6, 16)
        Me.chkIsSponsorship.Name = "chkIsSponsorship"
        Me.chkIsSponsorship.Size = New System.Drawing.Size(87, 18)
        Me.chkIsSponsorship.TabIndex = 1
        Me.chkIsSponsorship.Text = "Sponsorship"
        Me.chkIsSponsorship.UseVisualStyleBackColor = True
        '
        'chkIsPremium
        '
        Me.chkIsPremium.AutoSize = True
        Me.chkIsPremium.Location = New System.Drawing.Point(6, 40)
        Me.chkIsPremium.Name = "chkIsPremium"
        Me.chkIsPremium.Size = New System.Drawing.Size(66, 18)
        Me.chkIsPremium.TabIndex = 0
        Me.chkIsPremium.Text = "Premium"
        Me.chkIsPremium.UseVisualStyleBackColor = True
        '
        'chkIsCompensation
        '
        Me.chkIsCompensation.AutoSize = True
        Me.chkIsCompensation.Location = New System.Drawing.Point(6, 64)
        Me.chkIsCompensation.Name = "chkIsCompensation"
        Me.chkIsCompensation.Size = New System.Drawing.Size(94, 18)
        Me.chkIsCompensation.TabIndex = 0
        Me.chkIsCompensation.Text = "Compensation"
        Me.chkIsCompensation.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnRBS)
        Me.GroupBox3.Controls.Add(Me.btnSpecifics)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 19)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(299, 42)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        '
        'btnRBS
        '
        Me.btnRBS.AutoSize = True
        Me.btnRBS.Checked = True
        Me.btnRBS.Location = New System.Drawing.Point(10, 14)
        Me.btnRBS.Name = "btnRBS"
        Me.btnRBS.Size = New System.Drawing.Size(46, 18)
        Me.btnRBS.TabIndex = 3
        Me.btnRBS.TabStop = True
        Me.btnRBS.Text = "RBS"
        Me.btnRBS.UseVisualStyleBackColor = True
        '
        'btnSpecifics
        '
        Me.btnSpecifics.AutoSize = True
        Me.btnSpecifics.Location = New System.Drawing.Point(62, 14)
        Me.btnSpecifics.Name = "btnSpecifics"
        Me.btnSpecifics.Size = New System.Drawing.Size(70, 18)
        Me.btnSpecifics.TabIndex = 4
        Me.btnSpecifics.Text = "Specifics"
        Me.btnSpecifics.UseVisualStyleBackColor = True
        '
        'pnlAdd
        '
        Me.pnlAdd.Controls.Add(Me.txtName)
        Me.pnlAdd.Controls.Add(Me.txtShortname)
        Me.pnlAdd.Controls.Add(Me.Label2)
        Me.pnlAdd.Controls.Add(Me.Label3)
        Me.pnlAdd.Location = New System.Drawing.Point(156, 51)
        Me.pnlAdd.Name = "pnlAdd"
        Me.pnlAdd.Size = New System.Drawing.Size(171, 41)
        Me.pnlAdd.TabIndex = 5
        Me.pnlAdd.Visible = False
        '
        'cmbBookingType
        '
        Me.cmbBookingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBookingType.FormattingEnabled = True
        Me.cmbBookingType.Location = New System.Drawing.Point(12, 68)
        Me.cmbBookingType.Name = "cmbBookingType"
        Me.cmbBookingType.Size = New System.Drawing.Size(138, 22)
        Me.cmbBookingType.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 14)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Bookingtype"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbPricelist)
        Me.GroupBox1.Controls.Add(Me.optNewPrice)
        Me.GroupBox1.Controls.Add(Me.optPricelist)
        Me.GroupBox1.Controls.Add(Me.grpNew)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 96)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(311, 171)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Price"
        '
        'cmbPricelist
        '
        Me.cmbPricelist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPricelist.FormattingEnabled = True
        Me.cmbPricelist.Location = New System.Drawing.Point(108, 17)
        Me.cmbPricelist.Name = "cmbPricelist"
        Me.cmbPricelist.Size = New System.Drawing.Size(197, 22)
        Me.cmbPricelist.TabIndex = 8
        '
        'optNewPrice
        '
        Me.optNewPrice.AutoSize = True
        Me.optNewPrice.Location = New System.Drawing.Point(15, 43)
        Me.optNewPrice.Name = "optNewPrice"
        Me.optNewPrice.Size = New System.Drawing.Size(48, 18)
        Me.optNewPrice.TabIndex = 9
        Me.optNewPrice.Text = "New"
        Me.optNewPrice.UseVisualStyleBackColor = True
        '
        'optPricelist
        '
        Me.optPricelist.AutoSize = True
        Me.optPricelist.Checked = True
        Me.optPricelist.Location = New System.Drawing.Point(15, 19)
        Me.optPricelist.Name = "optPricelist"
        Me.optPricelist.Size = New System.Drawing.Size(87, 18)
        Me.optPricelist.TabIndex = 0
        Me.optPricelist.TabStop = True
        Me.optPricelist.Text = "Use pricelist:"
        Me.optPricelist.UseVisualStyleBackColor = True
        '
        'grpNew
        '
        Me.grpNew.Controls.Add(Me.grdPrice)
        Me.grpNew.Controls.Add(Me.txtAdedgeTarget)
        Me.grpNew.Controls.Add(Me.lblTarget)
        Me.grpNew.Controls.Add(Me.txtTargetName)
        Me.grpNew.Controls.Add(Me.Label5)
        Me.grpNew.Enabled = False
        Me.grpNew.Location = New System.Drawing.Point(6, 45)
        Me.grpNew.Name = "grpNew"
        Me.grpNew.Size = New System.Drawing.Size(299, 118)
        Me.grpNew.TabIndex = 10
        Me.grpNew.TabStop = False
        Me.grpNew.Text = "GroupBo"
        '
        'grdPrice
        '
        Me.grdPrice.AllowUserToAddRows = False
        Me.grdPrice.AllowUserToDeleteRows = False
        Me.grdPrice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPrice.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colUniSize})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.Format = "N0"
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdPrice.DefaultCellStyle = DataGridViewCellStyle1
        Me.grdPrice.Location = New System.Drawing.Point(6, 63)
        Me.grdPrice.Name = "grdPrice"
        Me.grdPrice.RowHeadersVisible = False
        Me.grdPrice.Size = New System.Drawing.Size(287, 47)
        Me.grdPrice.TabIndex = 6
        Me.grdPrice.VirtualMode = True
        '
        'colUniSize
        '
        Me.colUniSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colUniSize.HeaderText = "Uni Size"
        Me.colUniSize.Name = "colUniSize"
        '
        'txtAdedgeTarget
        '
        Me.txtAdedgeTarget.Location = New System.Drawing.Point(194, 37)
        Me.txtAdedgeTarget.Name = "txtAdedgeTarget"
        Me.txtAdedgeTarget.Size = New System.Drawing.Size(99, 20)
        Me.txtAdedgeTarget.TabIndex = 5
        '
        'lblTarget
        '
        Me.lblTarget.AutoSize = True
        Me.lblTarget.Location = New System.Drawing.Point(195, 20)
        Me.lblTarget.Name = "lblTarget"
        Me.lblTarget.Size = New System.Drawing.Size(78, 14)
        Me.lblTarget.TabIndex = 4
        Me.lblTarget.Text = "AdEdge Target"
        '
        'txtTargetName
        '
        Me.txtTargetName.Location = New System.Drawing.Point(6, 37)
        Me.txtTargetName.Name = "txtTargetName"
        Me.txtTargetName.Size = New System.Drawing.Size(182, 20)
        Me.txtTargetName.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 14)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Name"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Uni Size"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'frmQuickCopy
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(335, 489)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmbBookingType)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.pnlAdd)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cmbChannel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmQuickCopy"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Quick add"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.pnlAdd.ResumeLayout(False)
        Me.pnlAdd.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpNew.ResumeLayout(False)
        Me.grpNew.PerformLayout()
        CType(Me.grdPrice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbChannel As System.Windows.Forms.ComboBox
    Friend WithEvents txtShortname As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkIsCompensation As System.Windows.Forms.CheckBox
    Friend WithEvents chkIsPremium As System.Windows.Forms.CheckBox
    Friend WithEvents chkIsSponsorship As System.Windows.Forms.CheckBox
    Friend WithEvents btnSpecifics As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRBS As System.Windows.Forms.RadioButton
    Friend WithEvents pnlAdd As System.Windows.Forms.Panel
    Friend WithEvents cmbBookingType As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbPricelist As System.Windows.Forms.ComboBox
    Friend WithEvents optNewPrice As System.Windows.Forms.RadioButton
    Friend WithEvents optPricelist As System.Windows.Forms.RadioButton
    Friend WithEvents grpNew As System.Windows.Forms.GroupBox
    Friend WithEvents grdPrice As System.Windows.Forms.DataGridView
    Friend WithEvents colUniSize As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtAdedgeTarget As System.Windows.Forms.TextBox
    Friend WithEvents lblTarget As System.Windows.Forms.Label
    Friend WithEvents txtTargetName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
