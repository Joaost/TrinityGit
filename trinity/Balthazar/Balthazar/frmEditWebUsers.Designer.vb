<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditWebUsers
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditWebUsers))
        Me.grdUsers = New System.Windows.Forms.DataGridView
        Me.colClient = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colFirstName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colLastName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colRole = New Balthazar.ExtendedComboboxColumn
        Me.colEmail = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colLogin = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colPassword = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ExtendedComboboxColumn1 = New Balthazar.ExtendedComboboxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmdDeleteUser = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tpSalesmen = New System.Windows.Forms.TabPage
        Me.tpProvider = New System.Windows.Forms.TabPage
        Me.grdProviders = New System.Windows.Forms.DataGridView
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colProvEmail = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colProvLogin = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colProvPassword = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmdRemoveProvider = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        CType(Me.grdUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.tpSalesmen.SuspendLayout()
        Me.tpProvider.SuspendLayout()
        CType(Me.grdProviders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdUsers
        '
        Me.grdUsers.AllowUserToAddRows = False
        Me.grdUsers.AllowUserToDeleteRows = False
        Me.grdUsers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdUsers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colClient, Me.colFirstName, Me.colLastName, Me.colRole, Me.colEmail, Me.colLogin, Me.colPassword})
        Me.grdUsers.Location = New System.Drawing.Point(6, 6)
        Me.grdUsers.Name = "grdUsers"
        Me.grdUsers.RowHeadersVisible = False
        Me.grdUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdUsers.Size = New System.Drawing.Size(604, 221)
        Me.grdUsers.TabIndex = 0
        Me.grdUsers.VirtualMode = True
        '
        'colClient
        '
        Me.colClient.HeaderText = "Client"
        Me.colClient.Name = "colClient"
        Me.colClient.ReadOnly = True
        '
        'colFirstName
        '
        Me.colFirstName.HeaderText = "First name"
        Me.colFirstName.Name = "colFirstName"
        '
        'colLastName
        '
        Me.colLastName.HeaderText = "Last name"
        Me.colLastName.Name = "colLastName"
        '
        'colRole
        '
        Me.colRole.HeaderText = "Role"
        Me.colRole.Name = "colRole"
        '
        'colEmail
        '
        Me.colEmail.HeaderText = "E-mail"
        Me.colEmail.Name = "colEmail"
        '
        'colLogin
        '
        Me.colLogin.HeaderText = "Login"
        Me.colLogin.Name = "colLogin"
        '
        'colPassword
        '
        Me.colPassword.HeaderText = "Password"
        Me.colPassword.Name = "colPassword"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Client"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "First name"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Last name"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'ExtendedComboboxColumn1
        '
        Me.ExtendedComboboxColumn1.HeaderText = "Role"
        Me.ExtendedComboboxColumn1.Name = "ExtendedComboboxColumn1"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "E-mail"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Login"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Password"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'cmdDeleteUser
        '
        Me.cmdDeleteUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDeleteUser.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdDeleteUser.Location = New System.Drawing.Point(616, 6)
        Me.cmdDeleteUser.Name = "cmdDeleteUser"
        Me.cmdDeleteUser.Size = New System.Drawing.Size(24, 24)
        Me.cmdDeleteUser.TabIndex = 13
        Me.cmdDeleteUser.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tpSalesmen)
        Me.TabControl1.Controls.Add(Me.tpProvider)
        Me.TabControl1.Location = New System.Drawing.Point(1, 1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(654, 260)
        Me.TabControl1.TabIndex = 14
        '
        'tpSalesmen
        '
        Me.tpSalesmen.Controls.Add(Me.grdUsers)
        Me.tpSalesmen.Controls.Add(Me.cmdDeleteUser)
        Me.tpSalesmen.Location = New System.Drawing.Point(4, 23)
        Me.tpSalesmen.Name = "tpSalesmen"
        Me.tpSalesmen.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSalesmen.Size = New System.Drawing.Size(646, 233)
        Me.tpSalesmen.TabIndex = 0
        Me.tpSalesmen.Text = "Sales persons"
        Me.tpSalesmen.UseVisualStyleBackColor = True
        '
        'tpProvider
        '
        Me.tpProvider.Controls.Add(Me.grdProviders)
        Me.tpProvider.Controls.Add(Me.cmdRemoveProvider)
        Me.tpProvider.Location = New System.Drawing.Point(4, 23)
        Me.tpProvider.Name = "tpProvider"
        Me.tpProvider.Padding = New System.Windows.Forms.Padding(3)
        Me.tpProvider.Size = New System.Drawing.Size(646, 233)
        Me.tpProvider.TabIndex = 1
        Me.tpProvider.Text = "Providers"
        Me.tpProvider.UseVisualStyleBackColor = True
        '
        'grdProviders
        '
        Me.grdProviders.AllowUserToAddRows = False
        Me.grdProviders.AllowUserToDeleteRows = False
        Me.grdProviders.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdProviders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdProviders.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colProvEmail, Me.colProvLogin, Me.colProvPassword})
        Me.grdProviders.Location = New System.Drawing.Point(6, 6)
        Me.grdProviders.Name = "grdProviders"
        Me.grdProviders.RowHeadersVisible = False
        Me.grdProviders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdProviders.Size = New System.Drawing.Size(604, 221)
        Me.grdProviders.TabIndex = 14
        Me.grdProviders.VirtualMode = True
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        '
        'colProvEmail
        '
        Me.colProvEmail.HeaderText = "E-mail"
        Me.colProvEmail.Name = "colProvEmail"
        Me.colProvEmail.Width = 175
        '
        'colProvLogin
        '
        Me.colProvLogin.HeaderText = "Login"
        Me.colProvLogin.Name = "colProvLogin"
        '
        'colProvPassword
        '
        Me.colProvPassword.HeaderText = "Password"
        Me.colProvPassword.Name = "colProvPassword"
        '
        'cmdRemoveProvider
        '
        Me.cmdRemoveProvider.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveProvider.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveProvider.Location = New System.Drawing.Point(616, 6)
        Me.cmdRemoveProvider.Name = "cmdRemoveProvider"
        Me.cmdRemoveProvider.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveProvider.TabIndex = 15
        Me.cmdRemoveProvider.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Image = Global.Balthazar.My.Resources.Resources.environment
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(570, 267)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(70, 28)
        Me.cmdSave.TabIndex = 15
        Me.cmdSave.Text = "Publish"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'frmEditWebUsers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(652, 302)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmEditWebUsers"
        Me.Text = "Edit users"
        CType(Me.grdUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.tpSalesmen.ResumeLayout(False)
        Me.tpProvider.ResumeLayout(False)
        CType(Me.grdProviders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdUsers As System.Windows.Forms.DataGridView
    Friend WithEvents colClient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFirstName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLastName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRole As Balthazar.ExtendedComboboxColumn
    Friend WithEvents colEmail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLogin As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPassword As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExtendedComboboxColumn1 As Balthazar.ExtendedComboboxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdDeleteUser As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpSalesmen As System.Windows.Forms.TabPage
    Friend WithEvents tpProvider As System.Windows.Forms.TabPage
    Friend WithEvents grdProviders As System.Windows.Forms.DataGridView
    Friend WithEvents cmdRemoveProvider As System.Windows.Forms.Button
    Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProvEmail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProvLogin As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProvPassword As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdSave As System.Windows.Forms.Button
End Class
