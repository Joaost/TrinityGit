<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTemplates
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
        Me.lvwTemplates = New System.Windows.Forms.ListView
        Me.cmdOpen = New System.Windows.Forms.Button
        Me.colName = New System.Windows.Forms.ColumnHeader
        Me.colCreatedBy = New System.Windows.Forms.ColumnHeader
        Me.SuspendLayout()
        '
        'lvwTemplates
        '
        Me.lvwTemplates.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colCreatedBy})
        Me.lvwTemplates.Location = New System.Drawing.Point(12, 12)
        Me.lvwTemplates.MultiSelect = False
        Me.lvwTemplates.Name = "lvwTemplates"
        Me.lvwTemplates.Size = New System.Drawing.Size(268, 230)
        Me.lvwTemplates.TabIndex = 0
        Me.lvwTemplates.UseCompatibleStateImageBehavior = False
        Me.lvwTemplates.View = System.Windows.Forms.View.Details
        '
        'cmdOpen
        '
        Me.cmdOpen.Location = New System.Drawing.Point(205, 248)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(75, 26)
        Me.cmdOpen.TabIndex = 1
        Me.cmdOpen.Text = "Open"
        Me.cmdOpen.UseVisualStyleBackColor = True
        '
        'colName
        '
        Me.colName.Text = "Name"
        Me.colName.Width = 140
        '
        'colCreatedBy
        '
        Me.colCreatedBy.Text = "Created By"
        Me.colCreatedBy.Width = 100
        '
        'frmTemplates
        '
        Me.AcceptButton = Me.cmdOpen
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 286)
        Me.Controls.Add(Me.cmdOpen)
        Me.Controls.Add(Me.lvwTemplates)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmTemplates"
        Me.Text = "Templates"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lvwTemplates As System.Windows.Forms.ListView
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colCreatedBy As System.Windows.Forms.ColumnHeader
End Class
