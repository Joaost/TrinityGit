<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPremiums
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPremiums))
        Me.btnImportPremiumsFile = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdViewHoldsPremiums = New System.Windows.Forms.DataGridView
        Me.btnExportToDatabase = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnAddRow = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.Channel = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.Title = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Weeks = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Time = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Mon = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Tue = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Wed = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Thu = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Fri = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Sat = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Sun = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.GrossPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Comment = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Delete = New System.Windows.Forms.DataGridViewImageColumn
        CType(Me.grdViewHoldsPremiums, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnImportPremiumsFile
        '
        Me.btnImportPremiumsFile.Location = New System.Drawing.Point(181, 12)
        Me.btnImportPremiumsFile.Name = "btnImportPremiumsFile"
        Me.btnImportPremiumsFile.Size = New System.Drawing.Size(64, 27)
        Me.btnImportPremiumsFile.TabIndex = 3
        Me.btnImportPremiumsFile.Text = "Import"
        Me.btnImportPremiumsFile.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(69, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Choose premiums file"
        '
        'grdViewHoldsPremiums
        '
        Me.grdViewHoldsPremiums.AllowUserToAddRows = False
        Me.grdViewHoldsPremiums.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdViewHoldsPremiums.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdViewHoldsPremiums.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Channel, Me.Title, Me.Weeks, Me.Time, Me.Mon, Me.Tue, Me.Wed, Me.Thu, Me.Fri, Me.Sat, Me.Sun, Me.GrossPrice, Me.Comment, Me.Delete})
        Me.grdViewHoldsPremiums.Location = New System.Drawing.Point(1, 98)
        Me.grdViewHoldsPremiums.Name = "grdViewHoldsPremiums"
        Me.grdViewHoldsPremiums.RowHeadersVisible = False
        Me.grdViewHoldsPremiums.Size = New System.Drawing.Size(992, 418)
        Me.grdViewHoldsPremiums.TabIndex = 5
        Me.grdViewHoldsPremiums.VirtualMode = True
        '
        'btnExportToDatabase
        '
        Me.btnExportToDatabase.Location = New System.Drawing.Point(181, 45)
        Me.btnExportToDatabase.Name = "btnExportToDatabase"
        Me.btnExportToDatabase.Size = New System.Drawing.Size(64, 27)
        Me.btnExportToDatabase.TabIndex = 6
        Me.btnExportToDatabase.Text = "Export"
        Me.btnExportToDatabase.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(163, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Export the below to the database"
        '
        'btnAddRow
        '
        Me.btnAddRow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddRow.Location = New System.Drawing.Point(934, 69)
        Me.btnAddRow.Name = "btnAddRow"
        Me.btnAddRow.Size = New System.Drawing.Size(59, 23)
        Me.btnAddRow.TabIndex = 8
        Me.btnAddRow.Text = "Add Row"
        Me.btnAddRow.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(251, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(59, 60)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Channel
        '
        Me.Channel.HeaderText = "Channel"
        Me.Channel.Name = "Channel"
        Me.Channel.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Channel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Title
        '
        Me.Title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Title.HeaderText = "Title"
        Me.Title.Name = "Title"
        '
        'Weeks
        '
        Me.Weeks.HeaderText = "Weeks"
        Me.Weeks.Name = "Weeks"
        Me.Weeks.ToolTipText = "Write week numbers separated by a comma with no space. Eg. 51 or 23,24,26."
        '
        'Time
        '
        Me.Time.HeaderText = "Time"
        Me.Time.Name = "Time"
        Me.Time.Width = 50
        '
        'Mon
        '
        Me.Mon.HeaderText = "Mon"
        Me.Mon.Name = "Mon"
        Me.Mon.Width = 40
        '
        'Tue
        '
        Me.Tue.HeaderText = "Tue"
        Me.Tue.Name = "Tue"
        Me.Tue.Width = 40
        '
        'Wed
        '
        Me.Wed.HeaderText = "Wed"
        Me.Wed.Name = "Wed"
        Me.Wed.Width = 40
        '
        'Thu
        '
        Me.Thu.HeaderText = "Thu"
        Me.Thu.Name = "Thu"
        Me.Thu.Width = 40
        '
        'Fri
        '
        Me.Fri.HeaderText = "Fri"
        Me.Fri.Name = "Fri"
        Me.Fri.Width = 40
        '
        'Sat
        '
        Me.Sat.HeaderText = "Sat"
        Me.Sat.Name = "Sat"
        Me.Sat.Width = 40
        '
        'Sun
        '
        Me.Sun.HeaderText = "Sun"
        Me.Sun.Name = "Sun"
        Me.Sun.Width = 40
        '
        'GrossPrice
        '
        Me.GrossPrice.HeaderText = "Gross Price"
        Me.GrossPrice.Name = "GrossPrice"
        '
        'Comment
        '
        Me.Comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Comment.HeaderText = "Comment"
        Me.Comment.Name = "Comment"
        '
        'Delete
        '
        Me.Delete.HeaderText = ""
        Me.Delete.Image = Global.trinityAdmin.My.Resources.Resources.delete2
        Me.Delete.Name = "Delete"
        Me.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Delete.Width = 30
        '
        'frmPremiums
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(994, 515)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnAddRow)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnExportToDatabase)
        Me.Controls.Add(Me.grdViewHoldsPremiums)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnImportPremiumsFile)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPremiums"
        Me.Text = "Import Premiums"
        CType(Me.grdViewHoldsPremiums, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnImportPremiumsFile As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdViewHoldsPremiums As System.Windows.Forms.DataGridView
    Friend WithEvents btnExportToDatabase As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnAddRow As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Channel As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Title As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Weeks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Mon As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Tue As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Wed As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Thu As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Fri As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Sat As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Sun As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents GrossPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Comment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Delete As System.Windows.Forms.DataGridViewImageColumn
End Class
