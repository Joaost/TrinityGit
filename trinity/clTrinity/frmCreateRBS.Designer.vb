<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateRBS
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCreateRBS))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbFilmChannel = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTRP = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grdFilmsplit = New System.Windows.Forms.DataGridView()
        Me.grdDaypartSplit = New System.Windows.Forms.DataGridView()
        Me.day = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.prime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.night = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.OK = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.radBT = New System.Windows.Forms.RadioButton()
        Me.radMT = New System.Windows.Forms.RadioButton()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dtTo = New clTrinity.ExtendedDateTimePicker()
        Me.dtFrom = New clTrinity.ExtendedDateTimePicker()
        Me.ExtendedComboboxColumn1 = New clTrinity.ExtendedComboboxColumn()
        Me.ExtendedComboboxColumn2 = New clTrinity.ExtendedComboboxColumn()
        Me.ExtendedComboboxColumn3 = New clTrinity.ExtendedComboboxColumn()
        Me.ExtendedComboboxColumn4 = New clTrinity.ExtendedComboboxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.grdFilmsplit,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grdDaypartSplit,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Channel"
        '
        'cmbFilmChannel
        '
        Me.cmbFilmChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFilmChannel.FormattingEnabled = true
        Me.cmbFilmChannel.Location = New System.Drawing.Point(12, 68)
        Me.cmbFilmChannel.Name = "cmbFilmChannel"
        Me.cmbFilmChannel.Size = New System.Drawing.Size(93, 21)
        Me.cmbFilmChannel.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(12, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Period"
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(120, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(18, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "To"
        '
        'txtTRP
        '
        Me.txtTRP.Location = New System.Drawing.Point(123, 68)
        Me.txtTRP.Name = "txtTRP"
        Me.txtTRP.Size = New System.Drawing.Size(94, 22)
        Me.txtTRP.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(123, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(25, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "TRP"
        '
        'grdFilmsplit
        '
        Me.grdFilmsplit.AllowUserToAddRows = false
        Me.grdFilmsplit.AllowUserToDeleteRows = false
        Me.grdFilmsplit.AllowUserToResizeColumns = false
        Me.grdFilmsplit.AllowUserToResizeRows = false
        Me.grdFilmsplit.BackgroundColor = System.Drawing.Color.Silver
        Me.grdFilmsplit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFilmsplit.Location = New System.Drawing.Point(11, 215)
        Me.grdFilmsplit.Name = "grdFilmsplit"
        Me.grdFilmsplit.Size = New System.Drawing.Size(248, 129)
        Me.grdFilmsplit.TabIndex = 8
        '
        'grdDaypartSplit
        '
        Me.grdDaypartSplit.AllowUserToAddRows = false
        Me.grdDaypartSplit.AllowUserToDeleteRows = false
        Me.grdDaypartSplit.AllowUserToResizeColumns = false
        Me.grdDaypartSplit.AllowUserToResizeRows = false
        Me.grdDaypartSplit.BackgroundColor = System.Drawing.Color.Silver
        Me.grdDaypartSplit.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.day, Me.prime, Me.night})
        Me.grdDaypartSplit.Location = New System.Drawing.Point(12, 142)
        Me.grdDaypartSplit.Name = "grdDaypartSplit"
        Me.grdDaypartSplit.RowHeadersVisible = false
        Me.grdDaypartSplit.Size = New System.Drawing.Size(153, 47)
        Me.grdDaypartSplit.TabIndex = 7
        '
        'day
        '
        Me.day.HeaderText = "Day"
        Me.day.Name = "day"
        Me.day.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.day.Width = 50
        '
        'prime
        '
        Me.prime.HeaderText = "Prime"
        Me.prime.Name = "prime"
        Me.prime.Width = 50
        '
        'night
        '
        Me.night.HeaderText = "Night"
        Me.night.Name = "night"
        Me.night.Width = 50
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(12, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Daypart spit"
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Cancel.FlatAppearance.BorderSize = 0
        Me.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel.Location = New System.Drawing.Point(184, 353)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 10
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = true
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.OK.FlatAppearance.BorderSize = 0
        Me.OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK.Location = New System.Drawing.Point(103, 353)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 9
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = true
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Location = New System.Drawing.Point(11, 200)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Film split"
        '
        'radBT
        '
        Me.radBT.AutoSize = true
        Me.radBT.Checked = true
        Me.radBT.Location = New System.Drawing.Point(12, 101)
        Me.radBT.Name = "radBT"
        Me.radBT.Size = New System.Drawing.Size(95, 17)
        Me.radBT.TabIndex = 5
        Me.radBT.TabStop = true
        Me.radBT.Text = "Buying Target"
        Me.radBT.UseVisualStyleBackColor = true
        '
        'radMT
        '
        Me.radMT.AutoSize = true
        Me.radMT.Location = New System.Drawing.Point(126, 101)
        Me.radMT.Name = "radMT"
        Me.radMT.Size = New System.Drawing.Size(85, 17)
        Me.radMT.TabIndex = 6
        Me.radMT.Text = "Main Target"
        Me.radMT.UseVisualStyleBackColor = true
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Day"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Prime"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 50
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Night"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 50
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(123, 26)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.ShowWeekNumbers = true
        Me.dtTo.Size = New System.Drawing.Size(94, 22)
        Me.dtTo.TabIndex = 2
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(12, 26)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.ShowWeekNumbers = true
        Me.dtFrom.Size = New System.Drawing.Size(93, 22)
        Me.dtFrom.TabIndex = 1
        '
        'ExtendedComboboxColumn1
        '
        Me.ExtendedComboboxColumn1.HeaderText = "Channel"
        Me.ExtendedComboboxColumn1.Name = "ExtendedComboboxColumn1"
        Me.ExtendedComboboxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'ExtendedComboboxColumn2
        '
        Me.ExtendedComboboxColumn2.HeaderText = "Buying Target"
        Me.ExtendedComboboxColumn2.Name = "ExtendedComboboxColumn2"
        Me.ExtendedComboboxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ExtendedComboboxColumn2.Width = 80
        '
        'ExtendedComboboxColumn3
        '
        Me.ExtendedComboboxColumn3.HeaderText = "Channel"
        Me.ExtendedComboboxColumn3.Name = "ExtendedComboboxColumn3"
        Me.ExtendedComboboxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'ExtendedComboboxColumn4
        '
        Me.ExtendedComboboxColumn4.HeaderText = "Buying Target"
        Me.ExtendedComboboxColumn4.Name = "ExtendedComboboxColumn4"
        Me.ExtendedComboboxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ExtendedComboboxColumn4.Width = 80
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "CPT"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn4.Width = 60
        '
        'frmCreateRBS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(270, 383)
        Me.Controls.Add(Me.radMT)
        Me.Controls.Add(Me.radBT)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.grdDaypartSplit)
        Me.Controls.Add(Me.grdFilmsplit)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtTRP)
        Me.Controls.Add(Me.dtTo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtFrom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbFilmChannel)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmCreateRBS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Create marker spots"
        CType(Me.grdFilmsplit,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdDaypartSplit,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbFilmChannel As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtFrom As clTrinity.ExtendedDateTimePicker
    Friend WithEvents dtTo As clTrinity.ExtendedDateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTRP As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grdFilmsplit As System.Windows.Forms.DataGridView
    Friend WithEvents grdDaypartSplit As System.Windows.Forms.DataGridView
    Friend WithEvents day As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents prime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents night As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ExtendedComboboxColumn1 As clTrinity.ExtendedComboboxColumn
    Friend WithEvents ExtendedComboboxColumn2 As clTrinity.ExtendedComboboxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExtendedComboboxColumn3 As clTrinity.ExtendedComboboxColumn
    Friend WithEvents ExtendedComboboxColumn4 As clTrinity.ExtendedComboboxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents radBT As System.Windows.Forms.RadioButton
    Friend WithEvents radMT As System.Windows.Forms.RadioButton
End Class
