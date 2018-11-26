<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpenTemplate
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
        Me.lstQuestionaires = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdOpen = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lstQuestionaires
        '
        Me.lstQuestionaires.DisplayMember = "Name"
        Me.lstQuestionaires.FormattingEnabled = True
        Me.lstQuestionaires.ItemHeight = 14
        Me.lstQuestionaires.Location = New System.Drawing.Point(12, 26)
        Me.lstQuestionaires.Name = "lstQuestionaires"
        Me.lstQuestionaires.Size = New System.Drawing.Size(202, 270)
        Me.lstQuestionaires.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Questionaires"
        '
        'cmdOpen
        '
        Me.cmdOpen.Location = New System.Drawing.Point(220, 26)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(60, 27)
        Me.cmdOpen.TabIndex = 2
        Me.cmdOpen.Text = "Open"
        Me.cmdOpen.UseVisualStyleBackColor = True
        '
        'frmOpenTemplate
        '
        Me.AcceptButton = Me.cmdOpen
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 308)
        Me.Controls.Add(Me.cmdOpen)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstQuestionaires)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmOpenTemplate"
        Me.Text = "Open template"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstQuestionaires As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
End Class
