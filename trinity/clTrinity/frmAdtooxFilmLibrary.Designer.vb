<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdtooxFilmLibrary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdtooxFilmLibrary))
        Me.grdAdtooxFilms = New System.Windows.Forms.DataGridView()
        Me.colAdvertiser = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBrand = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFilmLength = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCopyCode = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.colFirstAirDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdPick = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.txtFindFilm = New System.Windows.Forms.TextBox()
        Me.lblFindFilm = New System.Windows.Forms.Label()
        Me.lblFirstAirdate = New System.Windows.Forms.Label()
        Me.dtPickFirstAirdate = New System.Windows.Forms.DateTimePicker()
        Me.cmbShow = New System.Windows.Forms.ComboBox()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.grdAdtooxFilms,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'grdAdtooxFilms
        '
        Me.grdAdtooxFilms.AllowUserToAddRows = false
        Me.grdAdtooxFilms.AllowUserToDeleteRows = false
        Me.grdAdtooxFilms.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdAdtooxFilms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAdtooxFilms.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colAdvertiser, Me.colBrand, Me.colTitle, Me.colFilmLength, Me.colCopyCode, Me.colFirstAirDate})
        Me.grdAdtooxFilms.Location = New System.Drawing.Point(12, 31)
        Me.grdAdtooxFilms.Name = "grdAdtooxFilms"
        Me.grdAdtooxFilms.ReadOnly = true
        Me.grdAdtooxFilms.RowHeadersVisible = false
        Me.grdAdtooxFilms.Size = New System.Drawing.Size(636, 172)
        Me.grdAdtooxFilms.TabIndex = 0
        '
        'colAdvertiser
        '
        Me.colAdvertiser.HeaderText = "Advertiser"
        Me.colAdvertiser.Name = "colAdvertiser"
        Me.colAdvertiser.ReadOnly = true
        Me.colAdvertiser.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colAdvertiser.Width = 120
        '
        'colBrand
        '
        Me.colBrand.HeaderText = "Brand"
        Me.colBrand.Name = "colBrand"
        Me.colBrand.ReadOnly = true
        Me.colBrand.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colTitle
        '
        Me.colTitle.HeaderText = "Title"
        Me.colTitle.Name = "colTitle"
        Me.colTitle.ReadOnly = true
        Me.colTitle.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colTitle.Width = 150
        '
        'colFilmLength
        '
        Me.colFilmLength.HeaderText = "Length"
        Me.colFilmLength.Name = "colFilmLength"
        Me.colFilmLength.ReadOnly = true
        Me.colFilmLength.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colFilmLength.Width = 50
        '
        'colCopyCode
        '
        Me.colCopyCode.HeaderText = "Copy Code"
        Me.colCopyCode.Name = "colCopyCode"
        Me.colCopyCode.ReadOnly = true
        Me.colCopyCode.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colCopyCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colCopyCode.Width = 83
        '
        'colFirstAirDate
        '
        Me.colFirstAirDate.HeaderText = "First Airdate"
        Me.colFirstAirDate.Name = "colFirstAirDate"
        Me.colFirstAirDate.ReadOnly = true
        Me.colFirstAirDate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'cmdPick
        '
        Me.cmdPick.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdPick.FlatAppearance.BorderSize = 0
        Me.cmdPick.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPick.Location = New System.Drawing.Point(482, 205)
        Me.cmdPick.Name = "cmdPick"
        Me.cmdPick.Size = New System.Drawing.Size(80, 30)
        Me.cmdPick.TabIndex = 1
        Me.cmdPick.Text = "Pick"
        Me.cmdPick.UseVisualStyleBackColor = true
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Location = New System.Drawing.Point(568, 205)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(80, 30)
        Me.cmdCancel.TabIndex = 2
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = true
        '
        'txtFindFilm
        '
        Me.txtFindFilm.Location = New System.Drawing.Point(48, 5)
        Me.txtFindFilm.Name = "txtFindFilm"
        Me.txtFindFilm.Size = New System.Drawing.Size(139, 22)
        Me.txtFindFilm.TabIndex = 3
        '
        'lblFindFilm
        '
        Me.lblFindFilm.AutoSize = true
        Me.lblFindFilm.Location = New System.Drawing.Point(12, 8)
        Me.lblFindFilm.Name = "lblFindFilm"
        Me.lblFindFilm.Size = New System.Drawing.Size(33, 13)
        Me.lblFindFilm.TabIndex = 4
        Me.lblFindFilm.Text = "Find:"
        '
        'lblFirstAirdate
        '
        Me.lblFirstAirdate.AutoSize = true
        Me.lblFirstAirdate.Location = New System.Drawing.Point(193, 8)
        Me.lblFirstAirdate.Name = "lblFirstAirdate"
        Me.lblFirstAirdate.Size = New System.Drawing.Size(98, 13)
        Me.lblFirstAirdate.TabIndex = 5
        Me.lblFirstAirdate.Text = "First airdate from:"
        '
        'dtPickFirstAirdate
        '
        Me.dtPickFirstAirdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtPickFirstAirdate.Location = New System.Drawing.Point(286, 4)
        Me.dtPickFirstAirdate.Name = "dtPickFirstAirdate"
        Me.dtPickFirstAirdate.Size = New System.Drawing.Size(85, 22)
        Me.dtPickFirstAirdate.TabIndex = 6
        '
        'cmbShow
        '
        Me.cmbShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbShow.FormattingEnabled = true
        Me.cmbShow.Items.AddRange(New Object() {"Show all films", "Show films for advertiser", "Show films for brand"})
        Me.cmbShow.Location = New System.Drawing.Point(377, 3)
        Me.cmbShow.Name = "cmbShow"
        Me.cmbShow.Size = New System.Drawing.Size(121, 21)
        Me.cmbShow.TabIndex = 7
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Advertiser"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = true
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewTextBoxColumn1.Width = 120
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Brand"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = true
        Me.DataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Title"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = true
        Me.DataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Length"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = true
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewTextBoxColumn4.Width = 50
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Copy Code"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = true
        Me.DataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewTextBoxColumn5.Width = 83
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "First Airdate"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = true
        Me.DataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'frmAdtooxFilmLibrary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = true
        Me.AutoSize = true
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(660, 236)
        Me.Controls.Add(Me.cmbShow)
        Me.Controls.Add(Me.dtPickFirstAirdate)
        Me.Controls.Add(Me.lblFirstAirdate)
        Me.Controls.Add(Me.lblFindFilm)
        Me.Controls.Add(Me.txtFindFilm)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdPick)
        Me.Controls.Add(Me.grdAdtooxFilms)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmAdtooxFilmLibrary"
        Me.Text = "Adtoox Filmcodes"
        CType(Me.grdAdtooxFilms,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents grdAdtooxFilms As System.Windows.Forms.DataGridView
    Friend WithEvents cmdPick As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAdvertiser As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBrand As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFilmLength As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCopyCode As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents colFirstAirDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtFindFilm As System.Windows.Forms.TextBox
    Friend WithEvents lblFindFilm As System.Windows.Forms.Label
    Friend WithEvents lblFirstAirdate As System.Windows.Forms.Label
    Friend WithEvents dtPickFirstAirdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbShow As System.Windows.Forms.ComboBox
End Class
