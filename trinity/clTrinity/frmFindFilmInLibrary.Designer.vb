<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFindFilmInLibrary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFindFilmInLibrary))
        Me.lvwFilms = New System.Windows.Forms.ListView()
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colLength = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colDescr = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colCreated = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.txtFilm = New System.Windows.Forms.TextBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbSettings = New System.Windows.Forms.ComboBox()
        Me.cmdExport = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'lvwFilms
        '
        Me.lvwFilms.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lvwFilms.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colLength, Me.colDescr, Me.colCreated})
        Me.lvwFilms.Location = New System.Drawing.Point(12, 38)
        Me.lvwFilms.Name = "lvwFilms"
        Me.lvwFilms.Size = New System.Drawing.Size(459, 282)
        Me.lvwFilms.TabIndex = 0
        Me.lvwFilms.UseCompatibleStateImageBehavior = false
        Me.lvwFilms.View = System.Windows.Forms.View.Details
        '
        'colName
        '
        Me.colName.Text = "Name"
        Me.colName.Width = 68
        '
        'colLength
        '
        Me.colLength.Text = "Length"
        '
        'colDescr
        '
        Me.colDescr.Text = "Description"
        Me.colDescr.Width = 149
        '
        'colCreated
        '
        Me.colCreated.Text = "Created"
        '
        'txtFilm
        '
        Me.txtFilm.Location = New System.Drawing.Point(34, 12)
        Me.txtFilm.Name = "txtFilm"
        Me.txtFilm.Size = New System.Drawing.Size(140, 22)
        Me.txtFilm.TabIndex = 2
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Location = New System.Drawing.Point(397, 326)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(74, 29)
        Me.cmdOk.TabIndex = 3
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(215, 12)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(79, 22)
        Me.dtFrom.TabIndex = 4
        Me.dtFrom.Value = New Date(1900, 1, 1, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(180, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "After"
        '
        'cmbSettings
        '
        Me.cmbSettings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSettings.FormattingEnabled = true
        Me.cmbSettings.Location = New System.Drawing.Point(12, 329)
        Me.cmbSettings.Name = "cmbSettings"
        Me.cmbSettings.Size = New System.Drawing.Size(162, 21)
        Me.cmbSettings.TabIndex = 6
        '
        'cmdExport
        '
        Me.cmdExport.FlatAppearance.BorderSize = 0
        Me.cmdExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdExport.Image = Global.clTrinity.My.Resources.Resources.excel_2
        Me.cmdExport.Location = New System.Drawing.Point(363, 326)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.Size = New System.Drawing.Size(28, 29)
        Me.cmdExport.TabIndex = 7
        Me.cmdExport.UseVisualStyleBackColor = true
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.search_2_16x16
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 20)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = false
        '
        'frmFindFilmInLibrary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(480, 362)
        Me.Controls.Add(Me.cmdExport)
        Me.Controls.Add(Me.cmbSettings)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtFrom)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.txtFilm)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lvwFilms)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmFindFilmInLibrary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Find film in library"
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents lvwFilms As System.Windows.Forms.ListView
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents txtFilm As System.Windows.Forms.TextBox
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDescr As System.Windows.Forms.ColumnHeader
    Friend WithEvents colCreated As System.Windows.Forms.ColumnHeader
    Friend WithEvents colLength As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmbSettings As System.Windows.Forms.ComboBox
    Friend WithEvents cmdExport As System.Windows.Forms.Button
End Class
