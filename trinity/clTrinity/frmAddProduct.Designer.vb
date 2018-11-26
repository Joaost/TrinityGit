<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddProduct
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddProduct))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.grpMarathon = New System.Windows.Forms.GroupBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtContract = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDepartment = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtClientID = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCompany = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmdPick = New System.Windows.Forms.Button()
        Me.txtProducts = New System.Windows.Forms.TextBox()
        Me.grpAdToox = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbAdtooxBrands = New System.Windows.Forms.ComboBox()
        Me.cmbAdtooxDivisions = New System.Windows.Forms.ComboBox()
        Me.cmbAdtooxCustomers = New System.Windows.Forms.ComboBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.txtAdtooxProductType = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.grpMarathon.SuspendLayout
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpAdToox.SuspendLayout
        CType(Me.PictureBox2,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name"
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtName.Location = New System.Drawing.Point(12, 29)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(211, 22)
        Me.txtName.TabIndex = 1
        '
        'grpMarathon
        '
        Me.grpMarathon.Controls.Add(Me.PictureBox1)
        Me.grpMarathon.Controls.Add(Me.txtContract)
        Me.grpMarathon.Controls.Add(Me.Label5)
        Me.grpMarathon.Controls.Add(Me.txtDepartment)
        Me.grpMarathon.Controls.Add(Me.Label4)
        Me.grpMarathon.Controls.Add(Me.txtClientID)
        Me.grpMarathon.Controls.Add(Me.Label3)
        Me.grpMarathon.Controls.Add(Me.txtCompany)
        Me.grpMarathon.Controls.Add(Me.Label2)
        Me.grpMarathon.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grpMarathon.Location = New System.Drawing.Point(12, 96)
        Me.grpMarathon.Name = "grpMarathon"
        Me.grpMarathon.Size = New System.Drawing.Size(211, 106)
        Me.grpMarathon.TabIndex = 2
        Me.grpMarathon.TabStop = false
        Me.grpMarathon.Text = "Marathon"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"),System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(172, 19)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = false
        '
        'txtContract
        '
        Me.txtContract.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtContract.Location = New System.Drawing.Point(142, 75)
        Me.txtContract.Name = "txtContract"
        Me.txtContract.Size = New System.Drawing.Size(62, 22)
        Me.txtContract.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label5.Location = New System.Drawing.Point(143, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Contract"
        '
        'txtDepartment
        '
        Me.txtDepartment.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtDepartment.Location = New System.Drawing.Point(74, 75)
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.Size = New System.Drawing.Size(62, 22)
        Me.txtDepartment.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label4.Location = New System.Drawing.Point(75, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Department"
        '
        'txtClientID
        '
        Me.txtClientID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtClientID.Location = New System.Drawing.Point(6, 75)
        Me.txtClientID.Name = "txtClientID"
        Me.txtClientID.Size = New System.Drawing.Size(62, 22)
        Me.txtClientID.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Client ID"
        '
        'txtCompany
        '
        Me.txtCompany.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtCompany.Location = New System.Drawing.Point(6, 36)
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.Size = New System.Drawing.Size(62, 22)
        Me.txtCompany.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Company"
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmdOk.Location = New System.Drawing.Point(167, 407)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(57, 23)
        Me.cmdOk.TabIndex = 3
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "AdvantEdge brands"
        '
        'cmdPick
        '
        Me.cmdPick.FlatAppearance.BorderSize = 0
        Me.cmdPick.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPick.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmdPick.Location = New System.Drawing.Point(166, 67)
        Me.cmdPick.Name = "cmdPick"
        Me.cmdPick.Size = New System.Drawing.Size(57, 23)
        Me.cmdPick.TabIndex = 5
        Me.cmdPick.Text = "Pick"
        Me.cmdPick.UseVisualStyleBackColor = true
        '
        'txtProducts
        '
        Me.txtProducts.BackColor = System.Drawing.SystemColors.Control
        Me.txtProducts.Enabled = false
        Me.txtProducts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtProducts.ForeColor = System.Drawing.Color.Red
        Me.txtProducts.Location = New System.Drawing.Point(12, 69)
        Me.txtProducts.Name = "txtProducts"
        Me.txtProducts.Size = New System.Drawing.Size(148, 22)
        Me.txtProducts.TabIndex = 6
        Me.txtProducts.Text = "No brands chosen"
        '
        'grpAdToox
        '
        Me.grpAdToox.Controls.Add(Me.Label11)
        Me.grpAdToox.Controls.Add(Me.Label10)
        Me.grpAdToox.Controls.Add(Me.Label9)
        Me.grpAdToox.Controls.Add(Me.cmbAdtooxBrands)
        Me.grpAdToox.Controls.Add(Me.cmbAdtooxDivisions)
        Me.grpAdToox.Controls.Add(Me.cmbAdtooxCustomers)
        Me.grpAdToox.Controls.Add(Me.PictureBox2)
        Me.grpAdToox.Controls.Add(Me.txtAdtooxProductType)
        Me.grpAdToox.Controls.Add(Me.Label8)
        Me.grpAdToox.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.grpAdToox.Location = New System.Drawing.Point(12, 208)
        Me.grpAdToox.Name = "grpAdToox"
        Me.grpAdToox.Size = New System.Drawing.Size(211, 194)
        Me.grpAdToox.TabIndex = 7
        Me.grpAdToox.TabStop = false
        Me.grpAdToox.Text = "E.C. Express"
        Me.grpAdToox.Visible = false
        '
        'Label11
        '
        Me.Label11.AutoSize = true
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(83, 13)
        Me.Label11.TabIndex = 15
        Me.Label11.Text = "Pick advertiser:"
        '
        'Label10
        '
        Me.Label10.AutoSize = true
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 65)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 13)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "Pick division:"
        '
        'Label9
        '
        Me.Label9.AutoSize = true
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 106)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 13)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Pick brand:"
        '
        'cmbAdtooxBrands
        '
        Me.cmbAdtooxBrands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAdtooxBrands.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbAdtooxBrands.FormattingEnabled = true
        Me.cmbAdtooxBrands.Location = New System.Drawing.Point(3, 123)
        Me.cmbAdtooxBrands.Name = "cmbAdtooxBrands"
        Me.cmbAdtooxBrands.Size = New System.Drawing.Size(139, 21)
        Me.cmbAdtooxBrands.TabIndex = 12
        '
        'cmbAdtooxDivisions
        '
        Me.cmbAdtooxDivisions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAdtooxDivisions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbAdtooxDivisions.FormattingEnabled = true
        Me.cmbAdtooxDivisions.Location = New System.Drawing.Point(3, 82)
        Me.cmbAdtooxDivisions.Name = "cmbAdtooxDivisions"
        Me.cmbAdtooxDivisions.Size = New System.Drawing.Size(139, 21)
        Me.cmbAdtooxDivisions.TabIndex = 11
        '
        'cmbAdtooxCustomers
        '
        Me.cmbAdtooxCustomers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAdtooxCustomers.FormattingEnabled = true
        Me.cmbAdtooxCustomers.Location = New System.Drawing.Point(3, 41)
        Me.cmbAdtooxCustomers.Name = "cmbAdtooxCustomers"
        Me.cmbAdtooxCustomers.Size = New System.Drawing.Size(139, 21)
        Me.cmbAdtooxCustomers.TabIndex = 10
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"),System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(172, 16)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = false
        '
        'txtAdtooxProductType
        '
        Me.txtAdtooxProductType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtAdtooxProductType.Location = New System.Drawing.Point(3, 164)
        Me.txtAdtooxProductType.Name = "txtAdtooxProductType"
        Me.txtAdtooxProductType.Size = New System.Drawing.Size(198, 22)
        Me.txtAdtooxProductType.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = true
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label8.Location = New System.Drawing.Point(2, 147)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 13)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Type of product"
        '
        'frmAddProduct
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(236, 442)
        Me.Controls.Add(Me.grpAdToox)
        Me.Controls.Add(Me.txtProducts)
        Me.Controls.Add(Me.cmdPick)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.grpMarathon)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmAddProduct"
        Me.Text = "Add product"
        Me.grpMarathon.ResumeLayout(false)
        Me.grpMarathon.PerformLayout
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpAdToox.ResumeLayout(false)
        Me.grpAdToox.PerformLayout
        CType(Me.PictureBox2,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents grpMarathon As System.Windows.Forms.GroupBox
    Friend WithEvents txtClientID As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCompany As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtContract As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDepartment As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdPick As System.Windows.Forms.Button
    Friend WithEvents txtProducts As System.Windows.Forms.TextBox
    Friend WithEvents grpAdToox As System.Windows.Forms.GroupBox
    Friend WithEvents txtAdtooxProductType As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents cmbAdtooxBrands As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAdtooxDivisions As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAdtooxCustomers As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
