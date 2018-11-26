<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RadiobuttonList
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.opt1 = New System.Windows.Forms.RadioButton
        Me.opt2 = New System.Windows.Forms.RadioButton
        Me.opt3 = New System.Windows.Forms.RadioButton
        Me.opt4 = New System.Windows.Forms.RadioButton
        Me.opt5 = New System.Windows.Forms.RadioButton
        Me.SuspendLayout()
        '
        'opt1
        '
        Me.opt1.AutoSize = True
        Me.opt1.Location = New System.Drawing.Point(3, 3)
        Me.opt1.Name = "opt1"
        Me.opt1.Size = New System.Drawing.Size(14, 13)
        Me.opt1.TabIndex = 0
        Me.opt1.Tag = "1"
        Me.opt1.UseVisualStyleBackColor = True
        '
        'opt2
        '
        Me.opt2.AutoSize = True
        Me.opt2.Location = New System.Drawing.Point(23, 3)
        Me.opt2.Name = "opt2"
        Me.opt2.Size = New System.Drawing.Size(14, 13)
        Me.opt2.TabIndex = 1
        Me.opt2.Tag = "2"
        Me.opt2.UseVisualStyleBackColor = True
        '
        'opt3
        '
        Me.opt3.AutoSize = True
        Me.opt3.Checked = True
        Me.opt3.Location = New System.Drawing.Point(43, 3)
        Me.opt3.Name = "opt3"
        Me.opt3.Size = New System.Drawing.Size(14, 13)
        Me.opt3.TabIndex = 2
        Me.opt3.TabStop = True
        Me.opt3.Tag = "3"
        Me.opt3.UseVisualStyleBackColor = True
        '
        'opt4
        '
        Me.opt4.AutoSize = True
        Me.opt4.Location = New System.Drawing.Point(63, 3)
        Me.opt4.Name = "opt4"
        Me.opt4.Size = New System.Drawing.Size(14, 13)
        Me.opt4.TabIndex = 3
        Me.opt4.Tag = "4"
        Me.opt4.UseVisualStyleBackColor = True
        '
        'opt5
        '
        Me.opt5.AutoSize = True
        Me.opt5.Location = New System.Drawing.Point(83, 3)
        Me.opt5.Name = "opt5"
        Me.opt5.Size = New System.Drawing.Size(14, 13)
        Me.opt5.TabIndex = 4
        Me.opt5.Tag = "5"
        Me.opt5.UseVisualStyleBackColor = True
        '
        'RadiobuttonList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.opt5)
        Me.Controls.Add(Me.opt4)
        Me.Controls.Add(Me.opt3)
        Me.Controls.Add(Me.opt2)
        Me.Controls.Add(Me.opt1)
        Me.Name = "RadiobuttonList"
        Me.Size = New System.Drawing.Size(102, 20)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents opt1 As System.Windows.Forms.RadioButton
    Friend WithEvents opt2 As System.Windows.Forms.RadioButton
    Friend WithEvents opt3 As System.Windows.Forms.RadioButton
    Friend WithEvents opt4 As System.Windows.Forms.RadioButton
    Friend WithEvents opt5 As System.Windows.Forms.RadioButton

End Class
