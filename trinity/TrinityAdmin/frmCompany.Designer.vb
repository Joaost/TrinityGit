<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCompany
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
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.chkAll = New System.Windows.Forms.RadioButton
        Me.chkPickCountry = New System.Windows.Forms.RadioButton
        Me.chkAllCompanies = New System.Windows.Forms.RadioButton
        Me.chkPickCompany = New System.Windows.Forms.RadioButton
        Me.grpCountry = New System.Windows.Forms.GroupBox
        Me.grpCompanies = New System.Windows.Forms.GroupBox
        Me.grdCountries = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grdCompanies = New System.Windows.Forms.DataGridView
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grpCountry.SuspendLayout()
        Me.grpCompanies.SuspendLayout()
        CType(Me.grdCountries, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCompanies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.Location = New System.Drawing.Point(156, 232)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 0
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.Location = New System.Drawing.Point(237, 232)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Checked = True
        Me.chkAll.Location = New System.Drawing.Point(10, 19)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(36, 17)
        Me.chkAll.TabIndex = 0
        Me.chkAll.TabStop = True
        Me.chkAll.Text = "All"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'chkPickCountry
        '
        Me.chkPickCountry.AutoSize = True
        Me.chkPickCountry.Location = New System.Drawing.Point(51, 19)
        Me.chkPickCountry.Name = "chkPickCountry"
        Me.chkPickCountry.Size = New System.Drawing.Size(93, 17)
        Me.chkPickCountry.TabIndex = 2
        Me.chkPickCountry.Text = "Pick Countries"
        Me.chkPickCountry.UseVisualStyleBackColor = True
        '
        'chkAllCompanies
        '
        Me.chkAllCompanies.AutoSize = True
        Me.chkAllCompanies.Checked = True
        Me.chkAllCompanies.Location = New System.Drawing.Point(8, 19)
        Me.chkAllCompanies.Name = "chkAllCompanies"
        Me.chkAllCompanies.Size = New System.Drawing.Size(36, 17)
        Me.chkAllCompanies.TabIndex = 3
        Me.chkAllCompanies.TabStop = True
        Me.chkAllCompanies.Text = "All"
        Me.chkAllCompanies.UseVisualStyleBackColor = True
        '
        'chkPickCompany
        '
        Me.chkPickCompany.AutoSize = True
        Me.chkPickCompany.Location = New System.Drawing.Point(50, 19)
        Me.chkPickCompany.Name = "chkPickCompany"
        Me.chkPickCompany.Size = New System.Drawing.Size(93, 17)
        Me.chkPickCompany.TabIndex = 4
        Me.chkPickCompany.Text = "Pick Company"
        Me.chkPickCompany.UseVisualStyleBackColor = True
        '
        'grpCountry
        '
        Me.grpCountry.Controls.Add(Me.chkAll)
        Me.grpCountry.Controls.Add(Me.chkPickCountry)
        Me.grpCountry.Location = New System.Drawing.Point(12, 12)
        Me.grpCountry.Name = "grpCountry"
        Me.grpCountry.Size = New System.Drawing.Size(149, 43)
        Me.grpCountry.TabIndex = 5
        Me.grpCountry.TabStop = False
        Me.grpCountry.Text = "Country"
        '
        'grpCompanies
        '
        Me.grpCompanies.Controls.Add(Me.chkAllCompanies)
        Me.grpCompanies.Controls.Add(Me.chkPickCompany)
        Me.grpCompanies.Location = New System.Drawing.Point(167, 12)
        Me.grpCompanies.Name = "grpCompanies"
        Me.grpCompanies.Size = New System.Drawing.Size(144, 43)
        Me.grpCompanies.TabIndex = 6
        Me.grpCompanies.TabStop = False
        Me.grpCompanies.Text = "Companies"
        Me.grpCompanies.Visible = False
        '
        'grdCountries
        '
        Me.grdCountries.AllowUserToAddRows = False
        Me.grdCountries.AllowUserToDeleteRows = False
        Me.grdCountries.AllowUserToResizeColumns = False
        Me.grdCountries.AllowUserToResizeRows = False
        Me.grdCountries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grdCountries.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.grdCountries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCountries.ColumnHeadersVisible = False
        Me.grdCountries.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1})
        Me.grdCountries.Location = New System.Drawing.Point(12, 61)
        Me.grdCountries.MultiSelect = False
        Me.grdCountries.Name = "grdCountries"
        Me.grdCountries.ReadOnly = True
        Me.grdCountries.RowHeadersVisible = False
        Me.grdCountries.Size = New System.Drawing.Size(149, 165)
        Me.grdCountries.TabIndex = 7
        Me.grdCountries.Visible = False
        '
        'Column1
        '
        Me.Column1.HeaderText = "Column1"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'grdCompanies
        '
        Me.grdCompanies.AllowUserToAddRows = False
        Me.grdCompanies.AllowUserToResizeColumns = False
        Me.grdCompanies.AllowUserToResizeRows = False
        Me.grdCompanies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grdCompanies.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.grdCompanies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCompanies.ColumnHeadersVisible = False
        Me.grdCompanies.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column2})
        Me.grdCompanies.Location = New System.Drawing.Point(167, 61)
        Me.grdCompanies.MultiSelect = False
        Me.grdCompanies.Name = "grdCompanies"
        Me.grdCompanies.ReadOnly = True
        Me.grdCompanies.RowHeadersVisible = False
        Me.grdCompanies.Size = New System.Drawing.Size(143, 165)
        Me.grdCompanies.TabIndex = 8
        Me.grdCompanies.Visible = False
        '
        'Column2
        '
        Me.Column2.HeaderText = "Column2"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'frmCompany
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(324, 267)
        Me.Controls.Add(Me.grdCompanies)
        Me.Controls.Add(Me.grdCountries)
        Me.Controls.Add(Me.grpCompanies)
        Me.Controls.Add(Me.grpCountry)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCompany"
        Me.Text = "frmCompany"
        Me.grpCountry.ResumeLayout(False)
        Me.grpCountry.PerformLayout()
        Me.grpCompanies.ResumeLayout(False)
        Me.grpCompanies.PerformLayout()
        CType(Me.grdCountries, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCompanies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents chkAll As System.Windows.Forms.RadioButton
    Friend WithEvents chkPickCountry As System.Windows.Forms.RadioButton
    Friend WithEvents chkAllCompanies As System.Windows.Forms.RadioButton
    Friend WithEvents chkPickCompany As System.Windows.Forms.RadioButton
    Friend WithEvents grpCountry As System.Windows.Forms.GroupBox
    Friend WithEvents grpCompanies As System.Windows.Forms.GroupBox
    Friend WithEvents grdCountries As System.Windows.Forms.DataGridView
    Friend WithEvents grdCompanies As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
