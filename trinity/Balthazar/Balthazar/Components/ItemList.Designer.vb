<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemList
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
        Me.pnlText = New System.Windows.Forms.Panel
        Me.pnlValue = New System.Windows.Forms.Panel
        Me.SuspendLayout()
        '
        'pnlText
        '
        Me.pnlText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlText.AutoSize = True
        Me.pnlText.Location = New System.Drawing.Point(3, 3)
        Me.pnlText.Name = "pnlText"
        Me.pnlText.Size = New System.Drawing.Size(133, 11)
        Me.pnlText.TabIndex = 0
        '
        'pnlValue
        '
        Me.pnlValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlValue.AutoSize = True
        Me.pnlValue.Location = New System.Drawing.Point(142, 3)
        Me.pnlValue.Name = "pnlValue"
        Me.pnlValue.Size = New System.Drawing.Size(24, 11)
        Me.pnlValue.TabIndex = 1
        '
        'ItemList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Controls.Add(Me.pnlValue)
        Me.Controls.Add(Me.pnlText)
        Me.Name = "ItemList"
        Me.Size = New System.Drawing.Size(171, 17)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlText As System.Windows.Forms.Panel
    Friend WithEvents pnlValue As System.Windows.Forms.Panel

End Class
