<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUncaughtException
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUncaughtException))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblError = New System.Windows.Forms.Label()
        Me.cmdSendErrorReport = New System.Windows.Forms.Button()
        Me.cmdContinue = New System.Windows.Forms.Button()
        Me.cmdQuit = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(514, 130)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'lblError
        '
        Me.lblError.AutoSize = true
        Me.lblError.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblError.Location = New System.Drawing.Point(45, 15)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(48, 17)
        Me.lblError.TabIndex = 2
        Me.lblError.Text = "Label2"
        '
        'cmdSendErrorReport
        '
        Me.cmdSendErrorReport.FlatAppearance.BorderSize = 0
        Me.cmdSendErrorReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSendErrorReport.Location = New System.Drawing.Point(184, 183)
        Me.cmdSendErrorReport.Name = "cmdSendErrorReport"
        Me.cmdSendErrorReport.Size = New System.Drawing.Size(110, 30)
        Me.cmdSendErrorReport.TabIndex = 3
        Me.cmdSendErrorReport.Text = "Send Error Report"
        Me.cmdSendErrorReport.UseVisualStyleBackColor = true
        '
        'cmdContinue
        '
        Me.cmdContinue.FlatAppearance.BorderSize = 0
        Me.cmdContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdContinue.Location = New System.Drawing.Point(300, 183)
        Me.cmdContinue.Name = "cmdContinue"
        Me.cmdContinue.Size = New System.Drawing.Size(110, 30)
        Me.cmdContinue.TabIndex = 4
        Me.cmdContinue.Text = "Continue"
        Me.cmdContinue.UseVisualStyleBackColor = true
        '
        'cmdQuit
        '
        Me.cmdQuit.FlatAppearance.BorderSize = 0
        Me.cmdQuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdQuit.Location = New System.Drawing.Point(416, 183)
        Me.cmdQuit.Name = "cmdQuit"
        Me.cmdQuit.Size = New System.Drawing.Size(110, 30)
        Me.cmdQuit.TabIndex = 5
        Me.cmdQuit.Text = "Quit"
        Me.cmdQuit.UseVisualStyleBackColor = true
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources._error
        Me.PictureBox1.Location = New System.Drawing.Point(15, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 23)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = false
        '
        'frmUncaughtException
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(538, 225)
        Me.Controls.Add(Me.cmdQuit)
        Me.Controls.Add(Me.cmdContinue)
        Me.Controls.Add(Me.cmdSendErrorReport)
        Me.Controls.Add(Me.lblError)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmUncaughtException"
        Me.Text = "T R I N I T Y"
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents cmdSendErrorReport As System.Windows.Forms.Button
    Friend WithEvents cmdContinue As System.Windows.Forms.Button
    Friend WithEvents cmdQuit As System.Windows.Forms.Button
End Class
