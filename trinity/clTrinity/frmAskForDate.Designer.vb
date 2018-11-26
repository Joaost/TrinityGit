<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAskForDate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAskForDate))
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtNormalFrom = New clTrinity.ExtendedDateTimePicker()
        Me.chkAdvanced = New System.Windows.Forms.CheckBox()
        Me.grdPeriod = New System.Windows.Forms.DataGridView()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFrom = New clTrinity.CalendarColumn()
        Me.colTo = New clTrinity.CalendarColumn()
        CType(Me.grdPeriod,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdOk.Location = New System.Drawing.Point(213, 43)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(83, 30)
        Me.cmdOk.TabIndex = 10
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(194, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Set new start date for the campaign:"
        '
        'dtNormalFrom
        '
        Me.dtNormalFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtNormalFrom.Location = New System.Drawing.Point(12, 27)
        Me.dtNormalFrom.Name = "dtNormalFrom"
        Me.dtNormalFrom.ShowWeekNumbers = true
        Me.dtNormalFrom.Size = New System.Drawing.Size(83, 22)
        Me.dtNormalFrom.TabIndex = 9
        '
        'chkAdvanced
        '
        Me.chkAdvanced.AutoSize = true
        Me.chkAdvanced.Location = New System.Drawing.Point(101, 29)
        Me.chkAdvanced.Name = "chkAdvanced"
        Me.chkAdvanced.Size = New System.Drawing.Size(76, 17)
        Me.chkAdvanced.TabIndex = 13
        Me.chkAdvanced.Text = "Advanced"
        Me.chkAdvanced.UseVisualStyleBackColor = true
        '
        'grdPeriod
        '
        Me.grdPeriod.AllowUserToAddRows = false
        Me.grdPeriod.AllowUserToDeleteRows = false
        Me.grdPeriod.AllowUserToResizeColumns = false
        Me.grdPeriod.AllowUserToResizeRows = false
        Me.grdPeriod.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPeriod.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colFrom, Me.colTo})
        Me.grdPeriod.Location = New System.Drawing.Point(12, 51)
        Me.grdPeriod.Name = "grdPeriod"
        Me.grdPeriod.RowHeadersVisible = false
        Me.grdPeriod.Size = New System.Drawing.Size(284, 0)
        Me.grdPeriod.TabIndex = 14
        Me.grdPeriod.Visible = false
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        '
        'colFrom
        '
        Me.colFrom.HeaderText = "From"
        Me.colFrom.Name = "colFrom"
        '
        'colTo
        '
        Me.colTo.HeaderText = "To"
        Me.colTo.Name = "colTo"
        '
        'frmAskForDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(308, 84)
        Me.Controls.Add(Me.grdPeriod)
        Me.Controls.Add(Me.chkAdvanced)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtNormalFrom)
        Me.Controls.Add(Me.cmdOk)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(314, 112)
        Me.Name = "frmAskForDate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Set new start date"
        CType(Me.grdPeriod,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents dtNormalFrom As clTrinity.ExtendedDateTimePicker
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkAdvanced As System.Windows.Forms.CheckBox
    Friend WithEvents grdPeriod As System.Windows.Forms.DataGridView
    Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFrom As clTrinity.CalendarColumn
    Friend WithEvents colTo As clTrinity.CalendarColumn
End Class
