<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddProduct
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
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdOk = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmdRemoveInternalContact = New System.Windows.Forms.Button
        Me.cmdAddInternalContact = New System.Windows.Forms.Button
        Me.grdInternalContacts = New System.Windows.Forms.DataGridView
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colRole = New Balthazar.ExtendedComboboxColumn
        Me.colPhone = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDefault = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmdRemoveExternalContact = New System.Windows.Forms.Button
        Me.cmdAddExternalContact = New System.Windows.Forms.Button
        Me.grdExternalContacts = New System.Windows.Forms.DataGridView
        Me.colExName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colExRole = New Balthazar.ExtendedComboboxColumn
        Me.colExPhoneNr = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colExDefault = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdInternalContacts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdExternalContacts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(12, 26)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(268, 20)
        Me.txtName.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Product name"
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOk.Location = New System.Drawing.Point(357, 317)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(75, 26)
        Me.cmdOk.TabIndex = 4
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdRemoveInternalContact)
        Me.GroupBox1.Controls.Add(Me.cmdAddInternalContact)
        Me.GroupBox1.Controls.Add(Me.grdInternalContacts)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 52)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(422, 125)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Internal contacts"
        '
        'cmdRemoveInternalContact
        '
        Me.cmdRemoveInternalContact.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveInternalContact.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveInternalContact.Location = New System.Drawing.Point(392, 48)
        Me.cmdRemoveInternalContact.Name = "cmdRemoveInternalContact"
        Me.cmdRemoveInternalContact.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveInternalContact.TabIndex = 5
        Me.cmdRemoveInternalContact.UseVisualStyleBackColor = True
        '
        'cmdAddInternalContact
        '
        Me.cmdAddInternalContact.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddInternalContact.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddInternalContact.Location = New System.Drawing.Point(392, 19)
        Me.cmdAddInternalContact.Name = "cmdAddInternalContact"
        Me.cmdAddInternalContact.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddInternalContact.TabIndex = 4
        Me.cmdAddInternalContact.UseVisualStyleBackColor = True
        '
        'grdInternalContacts
        '
        Me.grdInternalContacts.AllowUserToAddRows = False
        Me.grdInternalContacts.AllowUserToDeleteRows = False
        Me.grdInternalContacts.AllowUserToResizeColumns = False
        Me.grdInternalContacts.AllowUserToResizeRows = False
        Me.grdInternalContacts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdInternalContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdInternalContacts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colRole, Me.colPhone, Me.colDefault})
        Me.grdInternalContacts.Location = New System.Drawing.Point(6, 19)
        Me.grdInternalContacts.Name = "grdInternalContacts"
        Me.grdInternalContacts.RowHeadersVisible = False
        Me.grdInternalContacts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdInternalContacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdInternalContacts.Size = New System.Drawing.Size(380, 100)
        Me.grdInternalContacts.TabIndex = 3
        Me.grdInternalContacts.VirtualMode = True
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        '
        'colRole
        '
        Me.colRole.HeaderText = "Role"
        Me.colRole.Name = "colRole"
        Me.colRole.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colRole.Width = 125
        '
        'colPhone
        '
        Me.colPhone.FillWeight = 75.0!
        Me.colPhone.HeaderText = "Phone Nr"
        Me.colPhone.Name = "colPhone"
        Me.colPhone.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colPhone.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colDefault
        '
        Me.colDefault.HeaderText = "Default"
        Me.colDefault.Name = "colDefault"
        Me.colDefault.Visible = False
        Me.colDefault.Width = 60
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdRemoveExternalContact)
        Me.GroupBox2.Controls.Add(Me.cmdAddExternalContact)
        Me.GroupBox2.Controls.Add(Me.grdExternalContacts)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 183)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(422, 125)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "External contacts"
        '
        'cmdRemoveExternalContact
        '
        Me.cmdRemoveExternalContact.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveExternalContact.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveExternalContact.Location = New System.Drawing.Point(392, 48)
        Me.cmdRemoveExternalContact.Name = "cmdRemoveExternalContact"
        Me.cmdRemoveExternalContact.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveExternalContact.TabIndex = 5
        Me.cmdRemoveExternalContact.UseVisualStyleBackColor = True
        '
        'cmdAddExternalContact
        '
        Me.cmdAddExternalContact.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddExternalContact.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddExternalContact.Location = New System.Drawing.Point(392, 19)
        Me.cmdAddExternalContact.Name = "cmdAddExternalContact"
        Me.cmdAddExternalContact.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddExternalContact.TabIndex = 4
        Me.cmdAddExternalContact.UseVisualStyleBackColor = True
        '
        'grdExternalContacts
        '
        Me.grdExternalContacts.AllowUserToAddRows = False
        Me.grdExternalContacts.AllowUserToDeleteRows = False
        Me.grdExternalContacts.AllowUserToResizeColumns = False
        Me.grdExternalContacts.AllowUserToResizeRows = False
        Me.grdExternalContacts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdExternalContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdExternalContacts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colExName, Me.colExRole, Me.colExPhoneNr, Me.colExDefault})
        Me.grdExternalContacts.Location = New System.Drawing.Point(6, 19)
        Me.grdExternalContacts.Name = "grdExternalContacts"
        Me.grdExternalContacts.RowHeadersVisible = False
        Me.grdExternalContacts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdExternalContacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdExternalContacts.Size = New System.Drawing.Size(380, 100)
        Me.grdExternalContacts.TabIndex = 3
        Me.grdExternalContacts.VirtualMode = True
        '
        'colExName
        '
        Me.colExName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colExName.HeaderText = "Name"
        Me.colExName.Name = "colExName"
        '
        'colExRole
        '
        Me.colExRole.HeaderText = "Role"
        Me.colExRole.Name = "colExRole"
        Me.colExRole.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colExRole.Width = 125
        '
        'colExPhoneNr
        '
        Me.colExPhoneNr.FillWeight = 75.0!
        Me.colExPhoneNr.HeaderText = "Phone Nr"
        Me.colExPhoneNr.Name = "colExPhoneNr"
        Me.colExPhoneNr.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colExPhoneNr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colExDefault
        '
        Me.colExDefault.HeaderText = "Default"
        Me.colExDefault.Name = "colExDefault"
        Me.colExDefault.Visible = False
        Me.colExDefault.Width = 60
        '
        'frmAddProduct
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(444, 355)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmAddProduct"
        Me.Text = "Add product"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grdInternalContacts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdExternalContacts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemoveInternalContact As System.Windows.Forms.Button
    Friend WithEvents cmdAddInternalContact As System.Windows.Forms.Button
    Friend WithEvents grdInternalContacts As System.Windows.Forms.DataGridView
    Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRole As Balthazar.ExtendedComboboxColumn
    Friend WithEvents colPhone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDefault As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemoveExternalContact As System.Windows.Forms.Button
    Friend WithEvents cmdAddExternalContact As System.Windows.Forms.Button
    Friend WithEvents grdExternalContacts As System.Windows.Forms.DataGridView
    Friend WithEvents colExName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colExRole As Balthazar.ExtendedComboboxColumn
    Friend WithEvents colExPhoneNr As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colExDefault As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
