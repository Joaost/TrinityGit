<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFilmNotFound
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFilmNotFound))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFilm = New System.Windows.Forms.Label()
        Me.grpSetAs = New System.Windows.Forms.GroupBox()
        Me.optChangeOnFilm = New System.Windows.Forms.RadioButton()
        Me.optChangeOnSpots = New System.Windows.Forms.RadioButton()
        Me.cmbFilm = New System.Windows.Forms.ComboBox()
        Me.optSetAsOld = New System.Windows.Forms.RadioButton()
        Me.grpFilm = New System.Windows.Forms.GroupBox()
        Me.grdFilmDetails = New System.Windows.Forms.DataGridView()
        Me.colChannelFilmcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChannelIndex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chkFilmAutoIndex = New System.Windows.Forms.CheckBox()
        Me.txtFilmLength = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtFilmDescription = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtFilmName = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.optSetAsNew = New System.Windows.Forms.RadioButton()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpSetAs.SuspendLayout
        Me.grpFilm.SuspendLayout
        CType(Me.grdFilmDetails,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(128, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Could not find the film:"
        '
        'lblFilm
        '
        Me.lblFilm.AutoSize = true
        Me.lblFilm.Location = New System.Drawing.Point(129, 9)
        Me.lblFilm.Name = "lblFilm"
        Me.lblFilm.Size = New System.Drawing.Size(0, 13)
        Me.lblFilm.TabIndex = 1
        '
        'grpSetAs
        '
        Me.grpSetAs.Controls.Add(Me.optChangeOnFilm)
        Me.grpSetAs.Controls.Add(Me.optChangeOnSpots)
        Me.grpSetAs.Controls.Add(Me.cmbFilm)
        Me.grpSetAs.Location = New System.Drawing.Point(12, 25)
        Me.grpSetAs.Name = "grpSetAs"
        Me.grpSetAs.Size = New System.Drawing.Size(200, 84)
        Me.grpSetAs.TabIndex = 2
        Me.grpSetAs.TabStop = false
        Me.grpSetAs.Text = "grpOldFilm"
        '
        'optChangeOnFilm
        '
        Me.optChangeOnFilm.AutoSize = true
        Me.optChangeOnFilm.Location = New System.Drawing.Point(9, 62)
        Me.optChangeOnFilm.Name = "optChangeOnFilm"
        Me.optChangeOnFilm.Size = New System.Drawing.Size(151, 17)
        Me.optChangeOnFilm.TabIndex = 2
        Me.optChangeOnFilm.Text = "Change filmcode on film"
        Me.optChangeOnFilm.UseVisualStyleBackColor = true
        '
        'optChangeOnSpots
        '
        Me.optChangeOnSpots.AutoSize = true
        Me.optChangeOnSpots.Checked = true
        Me.optChangeOnSpots.Location = New System.Drawing.Point(9, 46)
        Me.optChangeOnSpots.Name = "optChangeOnSpots"
        Me.optChangeOnSpots.Size = New System.Drawing.Size(160, 17)
        Me.optChangeOnSpots.TabIndex = 1
        Me.optChangeOnSpots.TabStop = true
        Me.optChangeOnSpots.Text = "Change filmcode on spots"
        Me.optChangeOnSpots.UseVisualStyleBackColor = true
        '
        'cmbFilm
        '
        Me.cmbFilm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFilm.FormattingEnabled = true
        Me.cmbFilm.Location = New System.Drawing.Point(9, 20)
        Me.cmbFilm.Name = "cmbFilm"
        Me.cmbFilm.Size = New System.Drawing.Size(182, 21)
        Me.cmbFilm.TabIndex = 0
        '
        'optSetAsOld
        '
        Me.optSetAsOld.AutoSize = true
        Me.optSetAsOld.Checked = true
        Me.optSetAsOld.Location = New System.Drawing.Point(21, 23)
        Me.optSetAsOld.Name = "optSetAsOld"
        Me.optSetAsOld.Size = New System.Drawing.Size(64, 17)
        Me.optSetAsOld.TabIndex = 0
        Me.optSetAsOld.TabStop = true
        Me.optSetAsOld.Text = "Set as..."
        Me.optSetAsOld.UseVisualStyleBackColor = true
        '
        'grpFilm
        '
        Me.grpFilm.Controls.Add(Me.grdFilmDetails)
        Me.grpFilm.Controls.Add(Me.Label11)
        Me.grpFilm.Controls.Add(Me.chkFilmAutoIndex)
        Me.grpFilm.Controls.Add(Me.txtFilmLength)
        Me.grpFilm.Controls.Add(Me.Label10)
        Me.grpFilm.Controls.Add(Me.txtFilmDescription)
        Me.grpFilm.Controls.Add(Me.Label9)
        Me.grpFilm.Controls.Add(Me.txtFilmName)
        Me.grpFilm.Controls.Add(Me.Label8)
        Me.grpFilm.Enabled = false
        Me.grpFilm.Location = New System.Drawing.Point(12, 117)
        Me.grpFilm.Name = "grpFilm"
        Me.grpFilm.Size = New System.Drawing.Size(490, 154)
        Me.grpFilm.TabIndex = 3
        Me.grpFilm.TabStop = false
        Me.grpFilm.Text = "grpNewFilm"
        '
        'grdFilmDetails
        '
        Me.grdFilmDetails.AllowUserToAddRows = false
        Me.grdFilmDetails.AllowUserToDeleteRows = false
        Me.grdFilmDetails.AllowUserToResizeColumns = false
        Me.grdFilmDetails.AllowUserToResizeRows = false
        Me.grdFilmDetails.BackgroundColor = System.Drawing.Color.Silver
        Me.grdFilmDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFilmDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colChannelFilmcode, Me.colChannelIndex})
        Me.grdFilmDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdFilmDetails.Location = New System.Drawing.Point(228, 32)
        Me.grdFilmDetails.Name = "grdFilmDetails"
        Me.grdFilmDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.grdFilmDetails.Size = New System.Drawing.Size(256, 116)
        Me.grdFilmDetails.TabIndex = 17
        '
        'colChannelFilmcode
        '
        Me.colChannelFilmcode.HeaderText = "Filmcode"
        Me.colChannelFilmcode.Name = "colChannelFilmcode"
        '
        'colChannelIndex
        '
        Me.colChannelIndex.HeaderText = "Index"
        Me.colChannelIndex.Name = "colChannelIndex"
        Me.colChannelIndex.Width = 50
        '
        'Label11
        '
        Me.Label11.AutoSize = true
        Me.Label11.Location = New System.Drawing.Point(225, 18)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 13)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "Film details:"
        '
        'chkFilmAutoIndex
        '
        Me.chkFilmAutoIndex.AutoSize = true
        Me.chkFilmAutoIndex.Checked = true
        Me.chkFilmAutoIndex.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFilmAutoIndex.Location = New System.Drawing.Point(92, 121)
        Me.chkFilmAutoIndex.Name = "chkFilmAutoIndex"
        Me.chkFilmAutoIndex.Size = New System.Drawing.Size(82, 17)
        Me.chkFilmAutoIndex.TabIndex = 15
        Me.chkFilmAutoIndex.Text = "Auto index"
        Me.chkFilmAutoIndex.UseVisualStyleBackColor = true
        '
        'txtFilmLength
        '
        Me.txtFilmLength.Location = New System.Drawing.Point(6, 119)
        Me.txtFilmLength.Name = "txtFilmLength"
        Me.txtFilmLength.Size = New System.Drawing.Size(80, 22)
        Me.txtFilmLength.TabIndex = 14
        '
        'Label10
        '
        Me.Label10.AutoSize = true
        Me.Label10.Location = New System.Drawing.Point(6, 103)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 13)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "Film length:"
        '
        'txtFilmDescription
        '
        Me.txtFilmDescription.Location = New System.Drawing.Point(6, 76)
        Me.txtFilmDescription.Name = "txtFilmDescription"
        Me.txtFilmDescription.Size = New System.Drawing.Size(185, 22)
        Me.txtFilmDescription.TabIndex = 12
        '
        'Label9
        '
        Me.Label9.AutoSize = true
        Me.Label9.Location = New System.Drawing.Point(6, 60)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 13)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Film description:"
        '
        'txtFilmName
        '
        Me.txtFilmName.Location = New System.Drawing.Point(6, 34)
        Me.txtFilmName.Name = "txtFilmName"
        Me.txtFilmName.Size = New System.Drawing.Size(185, 22)
        Me.txtFilmName.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.AutoSize = true
        Me.Label8.Location = New System.Drawing.Point(6, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Film name"
        '
        'optSetAsNew
        '
        Me.optSetAsNew.AutoSize = true
        Me.optSetAsNew.Location = New System.Drawing.Point(21, 115)
        Me.optSetAsNew.Name = "optSetAsNew"
        Me.optSetAsNew.Size = New System.Drawing.Size(107, 17)
        Me.optSetAsNew.TabIndex = 0
        Me.optSetAsNew.Text = "Add as new film"
        Me.optSetAsNew.UseVisualStyleBackColor = true
        '
        'cmdOk
        '
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Location = New System.Drawing.Point(421, 285)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(75, 27)
        Me.cmdOk.TabIndex = 4
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Filmcode"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Index"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 50
        '
        'frmFilmNotFound
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(511, 323)
        Me.ControlBox = false
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.lblFilm)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.optSetAsOld)
        Me.Controls.Add(Me.grpSetAs)
        Me.Controls.Add(Me.optSetAsNew)
        Me.Controls.Add(Me.grpFilm)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmFilmNotFound"
        Me.Text = "Film not found..."
        Me.grpSetAs.ResumeLayout(false)
        Me.grpSetAs.PerformLayout
        Me.grpFilm.ResumeLayout(false)
        Me.grpFilm.PerformLayout
        CType(Me.grdFilmDetails,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblFilm As System.Windows.Forms.Label
    Friend WithEvents grpSetAs As System.Windows.Forms.GroupBox
    Friend WithEvents optSetAsOld As System.Windows.Forms.RadioButton
    Friend WithEvents grpFilm As System.Windows.Forms.GroupBox
    Friend WithEvents optSetAsNew As System.Windows.Forms.RadioButton
    Friend WithEvents grdFilmDetails As System.Windows.Forms.DataGridView
    Friend WithEvents colChannelFilmcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChannelIndex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkFilmAutoIndex As System.Windows.Forms.CheckBox
    Friend WithEvents txtFilmLength As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtFilmDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtFilmName As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbFilm As System.Windows.Forms.ComboBox
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents optChangeOnFilm As System.Windows.Forms.RadioButton
    Friend WithEvents optChangeOnSpots As System.Windows.Forms.RadioButton
End Class
