<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmXML
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
        Me.components = New System.ComponentModel.Container
        Me.cmbFile = New System.Windows.Forms.ComboBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.tvNodes = New System.Windows.Forms.TreeView
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdOK = New System.Windows.Forms.Button
        Me.grdAttrib = New System.Windows.Forms.DataGridView
        Me.AttribName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkIfFound = New System.Windows.Forms.CheckBox
        Me.txtHuge = New System.Windows.Forms.TextBox
        Me.cmdSave = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmdDelAttrib = New System.Windows.Forms.Button
        Me.cmdDeleteRow = New System.Windows.Forms.Button
        Me.cmdAddRow = New System.Windows.Forms.Button
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblDELETE = New System.Windows.Forms.Label
        Me.cmdSetMarker = New System.Windows.Forms.Button
        CType(Me.grdAttrib, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbFile
        '
        Me.cmbFile.FormattingEnabled = True
        Me.cmbFile.Location = New System.Drawing.Point(43, 9)
        Me.cmbFile.Name = "cmbFile"
        Me.cmbFile.Size = New System.Drawing.Size(308, 21)
        Me.cmbFile.TabIndex = 0
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(12, 39)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(339, 20)
        Me.TextBox1.TabIndex = 1
        '
        'tvNodes
        '
        Me.tvNodes.Location = New System.Drawing.Point(12, 65)
        Me.tvNodes.Name = "tvNodes"
        Me.tvNodes.Size = New System.Drawing.Size(339, 348)
        Me.tvNodes.TabIndex = 2
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(585, 419)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(504, 420)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 4
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'grdAttrib
        '
        Me.grdAttrib.AllowUserToAddRows = False
        Me.grdAttrib.AllowUserToDeleteRows = False
        Me.grdAttrib.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grdAttrib.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAttrib.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AttribName, Me.Value})
        Me.grdAttrib.Location = New System.Drawing.Point(387, 65)
        Me.grdAttrib.Name = "grdAttrib"
        Me.grdAttrib.RowHeadersVisible = False
        Me.grdAttrib.Size = New System.Drawing.Size(273, 348)
        Me.grdAttrib.TabIndex = 5
        '
        'AttribName
        '
        Me.AttribName.HeaderText = "Name"
        Me.AttribName.Name = "AttribName"
        '
        'Value
        '
        Me.Value.HeaderText = "Value"
        Me.Value.Name = "Value"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "File:"
        '
        'chkIfFound
        '
        Me.chkIfFound.AutoSize = True
        Me.chkIfFound.Location = New System.Drawing.Point(562, 39)
        Me.chkIfFound.Name = "chkIfFound"
        Me.chkIfFound.Size = New System.Drawing.Size(98, 17)
        Me.chkIfFound.TabIndex = 8
        Me.chkIfFound.Text = "AddIfNotFound"
        Me.chkIfFound.UseVisualStyleBackColor = True
        '
        'txtHuge
        '
        Me.txtHuge.Location = New System.Drawing.Point(387, 65)
        Me.txtHuge.Multiline = True
        Me.txtHuge.Name = "txtHuge"
        Me.txtHuge.Size = New System.Drawing.Size(273, 348)
        Me.txtHuge.TabIndex = 11
        Me.txtHuge.Visible = False
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(387, 419)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(92, 23)
        Me.cmdSave.TabIndex = 12
        Me.cmdSave.Text = "Save Node"
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save the changes on this node")
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 144
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 143
        '
        'cmdDelAttrib
        '
        Me.cmdDelAttrib.Image = Global.trinityAdmin.My.Resources.Resources.delete2
        Me.cmdDelAttrib.Location = New System.Drawing.Point(666, 94)
        Me.cmdDelAttrib.Name = "cmdDelAttrib"
        Me.cmdDelAttrib.Size = New System.Drawing.Size(25, 23)
        Me.cmdDelAttrib.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.cmdDelAttrib, "Delete Attribute")
        Me.cmdDelAttrib.UseVisualStyleBackColor = True
        '
        'cmdDeleteRow
        '
        Me.cmdDeleteRow.Image = Global.trinityAdmin.My.Resources.Resources.delete2
        Me.cmdDeleteRow.Location = New System.Drawing.Point(357, 94)
        Me.cmdDeleteRow.Name = "cmdDeleteRow"
        Me.cmdDeleteRow.Size = New System.Drawing.Size(24, 23)
        Me.cmdDeleteRow.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.cmdDeleteRow, "Delete Node")
        Me.cmdDeleteRow.UseVisualStyleBackColor = True
        '
        'cmdAddRow
        '
        Me.cmdAddRow.Image = Global.trinityAdmin.My.Resources.Resources.add2
        Me.cmdAddRow.Location = New System.Drawing.Point(357, 65)
        Me.cmdAddRow.Name = "cmdAddRow"
        Me.cmdAddRow.Size = New System.Drawing.Size(24, 23)
        Me.cmdAddRow.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.cmdAddRow, "Add Node")
        Me.cmdAddRow.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Image = Global.trinityAdmin.My.Resources.Resources.add2
        Me.cmdAdd.Location = New System.Drawing.Point(666, 65)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(25, 23)
        Me.cmdAdd.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add Attribute")
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'lblDELETE
        '
        Me.lblDELETE.AutoSize = True
        Me.lblDELETE.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lblDELETE.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDELETE.ForeColor = System.Drawing.Color.Red
        Me.lblDELETE.Location = New System.Drawing.Point(389, 135)
        Me.lblDELETE.Name = "lblDELETE"
        Me.lblDELETE.Size = New System.Drawing.Size(269, 33)
        Me.lblDELETE.TabIndex = 33
        Me.lblDELETE.Text = "WILL BE DELETED"
        Me.lblDELETE.Visible = False
        '
        'cmdSetMarker
        '
        Me.cmdSetMarker.Location = New System.Drawing.Point(387, 35)
        Me.cmdSetMarker.Name = "cmdSetMarker"
        Me.cmdSetMarker.Size = New System.Drawing.Size(55, 22)
        Me.cmdSetMarker.TabIndex = 34
        Me.cmdSetMarker.Text = "Set Key"
        Me.cmdSetMarker.UseVisualStyleBackColor = True
        '
        'frmXML
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(697, 449)
        Me.Controls.Add(Me.cmdSetMarker)
        Me.Controls.Add(Me.lblDELETE)
        Me.Controls.Add(Me.cmdDelAttrib)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.txtHuge)
        Me.Controls.Add(Me.cmdDeleteRow)
        Me.Controls.Add(Me.cmdAddRow)
        Me.Controls.Add(Me.chkIfFound)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grdAttrib)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.tvNodes)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.cmbFile)
        Me.Name = "frmXML"
        Me.Text = "frmXML"
        CType(Me.grdAttrib, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbFile As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents tvNodes As System.Windows.Forms.TreeView
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents grdAttrib As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents AttribName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Value As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkIfFound As System.Windows.Forms.CheckBox
    Friend WithEvents cmdAddRow As System.Windows.Forms.Button
    Friend WithEvents cmdDeleteRow As System.Windows.Forms.Button
    Friend WithEvents txtHuge As System.Windows.Forms.TextBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdDelAttrib As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblDELETE As System.Windows.Forms.Label
    Friend WithEvents cmdSetMarker As System.Windows.Forms.Button
End Class
