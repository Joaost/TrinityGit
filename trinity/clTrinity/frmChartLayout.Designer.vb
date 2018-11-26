<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChartLayout
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChartLayout))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.grdSeries = New System.Windows.Forms.DataGridView()
        Me.colSeries = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colColor = New System.Windows.Forms.DataGridViewImageColumn()
        Me.colType = New clTrinity.ExtendedComboboxColumn()
        Me.colAxis = New clTrinity.ExtendedComboboxColumn()
        Me.ColorColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExtendedComboboxColumn1 = New clTrinity.ExtendedComboboxColumn()
        Me.ExtendedComboboxColumn2 = New clTrinity.ExtendedComboboxColumn()
        Me.cmbColorScheme = New System.Windows.Forms.ComboBox()
        Me.cmdApplyScheme = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel1.SuspendLayout
        CType(Me.grdSeries,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(378, 347)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.FlatAppearance.BorderSize = 0
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.FlatAppearance.BorderSize = 0
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'grdSeries
        '
        Me.grdSeries.AllowUserToAddRows = false
        Me.grdSeries.AllowUserToDeleteRows = false
        Me.grdSeries.AllowUserToOrderColumns = true
        Me.grdSeries.AllowUserToResizeColumns = false
        Me.grdSeries.AllowUserToResizeRows = false
        Me.grdSeries.BackgroundColor = System.Drawing.Color.Silver
        Me.grdSeries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSeries.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSeries, Me.colColor, Me.colType, Me.colAxis})
        Me.grdSeries.Location = New System.Drawing.Point(12, 84)
        Me.grdSeries.Name = "grdSeries"
        Me.grdSeries.RowHeadersVisible = false
        Me.grdSeries.Size = New System.Drawing.Size(512, 257)
        Me.grdSeries.TabIndex = 2
        '
        'colSeries
        '
        Me.colSeries.HeaderText = "Series"
        Me.colSeries.Name = "colSeries"
        Me.colSeries.ReadOnly = true
        '
        'colColor
        '
        Me.colColor.HeaderText = "Color"
        Me.colColor.Name = "colColor"
        Me.colColor.ReadOnly = true
        Me.colColor.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colColor.Width = 35
        '
        'colType
        '
        Me.colType.HeaderText = "Type"
        Me.colType.Name = "colType"
        Me.colType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'colAxis
        '
        Me.colAxis.HeaderText = "Axis"
        Me.colAxis.Name = "colAxis"
        Me.colAxis.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'ColorColumn1
        '
        Me.ColorColumn1.HeaderText = "Color"
        Me.ColorColumn1.Name = "ColorColumn1"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Series"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = true
        '
        'ExtendedComboboxColumn1
        '
        Me.ExtendedComboboxColumn1.HeaderText = "Type"
        Me.ExtendedComboboxColumn1.Name = "ExtendedComboboxColumn1"
        Me.ExtendedComboboxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'ExtendedComboboxColumn2
        '
        Me.ExtendedComboboxColumn2.HeaderText = "Axis"
        Me.ExtendedComboboxColumn2.Name = "ExtendedComboboxColumn2"
        Me.ExtendedComboboxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'cmbColorScheme
        '
        Me.cmbColorScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbColorScheme.FormattingEnabled = true
        Me.cmbColorScheme.Location = New System.Drawing.Point(12, 352)
        Me.cmbColorScheme.Name = "cmbColorScheme"
        Me.cmbColorScheme.Size = New System.Drawing.Size(121, 21)
        Me.cmbColorScheme.TabIndex = 3
        '
        'cmdApplyScheme
        '
        Me.cmdApplyScheme.Location = New System.Drawing.Point(139, 350)
        Me.cmdApplyScheme.Name = "cmdApplyScheme"
        Me.cmdApplyScheme.Size = New System.Drawing.Size(84, 23)
        Me.cmdApplyScheme.TabIndex = 4
        Me.cmdApplyScheme.Text = "Apply scheme"
        Me.cmdApplyScheme.UseVisualStyleBackColor = true
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Chart title"
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(12, 60)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(211, 22)
        Me.txtTitle.TabIndex = 6
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.chart_2_24x24
        Me.PictureBox1.Location = New System.Drawing.Point(12, 10)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = false
        '
        'frmChartLayout
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(536, 388)
        Me.Controls.Add(Me.txtTitle)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdApplyScheme)
        Me.Controls.Add(Me.cmbColorScheme)
        Me.Controls.Add(Me.grdSeries)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmChartLayout"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Chart Layout"
        Me.TableLayoutPanel1.ResumeLayout(false)
        CType(Me.grdSeries,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents grdSeries As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColorColumn1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents ExtendedComboboxColumn1 As clTrinity.ExtendedComboboxColumn
    Friend WithEvents ExtendedComboboxColumn2 As clTrinity.ExtendedComboboxColumn
    Friend WithEvents cmbColorScheme As System.Windows.Forms.ComboBox
    Friend WithEvents cmdApplyScheme As System.Windows.Forms.Button
    Friend WithEvents colSeries As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colColor As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents colType As clTrinity.ExtendedComboboxColumn
    Friend WithEvents colAxis As clTrinity.ExtendedComboboxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox

End Class
