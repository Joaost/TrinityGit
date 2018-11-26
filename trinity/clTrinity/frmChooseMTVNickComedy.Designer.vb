<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChooseMTVNickComedy
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChooseMTVNickComedy))
        Me.optMTV = New System.Windows.Forms.RadioButton()
        Me.optCC = New System.Windows.Forms.RadioButton()
        Me.optNick = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout
        Me.SuspendLayout
        '
        'optMTV
        '
        Me.optMTV.AutoSize = true
        Me.optMTV.Location = New System.Drawing.Point(19, 19)
        Me.optMTV.Name = "optMTV"
        Me.optMTV.Size = New System.Drawing.Size(47, 17)
        Me.optMTV.TabIndex = 3
        Me.optMTV.TabStop = true
        Me.optMTV.Text = "MTV"
        Me.optMTV.UseVisualStyleBackColor = true
        '
        'optCC
        '
        Me.optCC.AutoSize = true
        Me.optCC.Location = New System.Drawing.Point(19, 58)
        Me.optCC.Name = "optCC"
        Me.optCC.Size = New System.Drawing.Size(106, 17)
        Me.optCC.TabIndex = 4
        Me.optCC.TabStop = true
        Me.optCC.Text = "Comedy Central"
        Me.optCC.UseVisualStyleBackColor = true
        '
        'optNick
        '
        Me.optNick.AutoSize = true
        Me.optNick.Location = New System.Drawing.Point(19, 100)
        Me.optNick.Name = "optNick"
        Me.optNick.Size = New System.Drawing.Size(90, 17)
        Me.optNick.TabIndex = 5
        Me.optNick.TabStop = true
        Me.optNick.Text = "Nickelodeon"
        Me.optNick.UseVisualStyleBackColor = true
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optMTV)
        Me.GroupBox1.Controls.Add(Me.optNick)
        Me.GroupBox1.Controls.Add(Me.optCC)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 138)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Pick channel"
        '
        'Button1
        '
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(72, 165)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = true
        '
        'frmChooseMTVNickComedy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(225, 210)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmChooseMTVNickComedy"
        Me.Text = "Pick channel"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents optMTV As System.Windows.Forms.RadioButton
    Friend WithEvents optCC As System.Windows.Forms.RadioButton
    Friend WithEvents optNick As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
