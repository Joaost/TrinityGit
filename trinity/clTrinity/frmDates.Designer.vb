<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDates
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDates))
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.lblNormalDates = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.dateTo = New clTrinity.ExtendedDateTimePicker()
        Me.dateFrom = New clTrinity.ExtendedDateTimePicker()
        Me.dtNormalTo = New clTrinity.ExtendedDateTimePicker()
        Me.dtNormalFrom = New clTrinity.ExtendedDateTimePicker()
        Me.SuspendLayout
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCancel.Location = New System.Drawing.Point(137, 55)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(62, 23)
        Me.cmdCancel.TabIndex = 13
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = true
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdOk.Location = New System.Drawing.Point(74, 55)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(57, 22)
        Me.cmdOk.TabIndex = 12
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'lblNormalDates
        '
        Me.lblNormalDates.AutoSize = true
        Me.lblNormalDates.Location = New System.Drawing.Point(-102, -68)
        Me.lblNormalDates.Name = "lblNormalDates"
        Me.lblNormalDates.Size = New System.Drawing.Size(39, 13)
        Me.lblNormalDates.TabIndex = 10
        Me.lblNormalDates.Text = "Dates:"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "From:"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(113, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "To:"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = true
        Me.lblHeader.Location = New System.Drawing.Point(12, 8)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.lblHeader.MaximumSize = New System.Drawing.Size(219, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(0, 13)
        Me.lblHeader.TabIndex = 18
        '
        'dateTo
        '
        Me.dateTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateTo.Location = New System.Drawing.Point(116, 24)
        Me.dateTo.Name = "dateTo"
        Me.dateTo.ShowWeekNumbers = true
        Me.dateTo.Size = New System.Drawing.Size(83, 22)
        Me.dateTo.TabIndex = 15
        '
        'dateFrom
        '
        Me.dateFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateFrom.Location = New System.Drawing.Point(12, 24)
        Me.dateFrom.Name = "dateFrom"
        Me.dateFrom.ShowWeekNumbers = true
        Me.dateFrom.Size = New System.Drawing.Size(83, 22)
        Me.dateFrom.TabIndex = 14
        '
        'dtNormalTo
        '
        Me.dtNormalTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtNormalTo.Location = New System.Drawing.Point(31, -71)
        Me.dtNormalTo.Name = "dtNormalTo"
        Me.dtNormalTo.ShowWeekNumbers = true
        Me.dtNormalTo.Size = New System.Drawing.Size(83, 22)
        Me.dtNormalTo.TabIndex = 11
        '
        'dtNormalFrom
        '
        Me.dtNormalFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtNormalFrom.Location = New System.Drawing.Point(-58, -71)
        Me.dtNormalFrom.Name = "dtNormalFrom"
        Me.dtNormalFrom.ShowWeekNumbers = true
        Me.dtNormalFrom.Size = New System.Drawing.Size(83, 22)
        Me.dtNormalFrom.TabIndex = 9
        '
        'frmDates
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(213, 90)
        Me.Controls.Add(Me.lblHeader)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dateTo)
        Me.Controls.Add(Me.dateFrom)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.dtNormalTo)
        Me.Controls.Add(Me.lblNormalDates)
        Me.Controls.Add(Me.dtNormalFrom)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmDates"
        Me.Text = "Pick dates"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents dtNormalTo As clTrinity.ExtendedDateTimePicker
    Friend WithEvents lblNormalDates As System.Windows.Forms.Label
    Friend WithEvents dtNormalFrom As clTrinity.ExtendedDateTimePicker
    Friend WithEvents dateTo As clTrinity.ExtendedDateTimePicker
    Friend WithEvents dateFrom As clTrinity.ExtendedDateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
End Class
