<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmErrorReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmErrorReport))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dlgOpen = New System.Windows.Forms.OpenFileDialog()
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.cmdBrowse = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdSend = New System.Windows.Forms.Button()
        Me.chkScreenShot = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Reported by"
        '
        'txtName
        '
        Me.txtName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtName.Location = New System.Drawing.Point(12, 24)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(382, 22)
        Me.txtName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(382, 58)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "What where you doing?"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"(Be as specific as you can. Instead of ""Changed buying tar"& _ 
    "get"", please write ""Changed buying target on TV3 from A20-44 to W20-44"", which c"& _ 
    "ampaign were you working with?)"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(12, 110)
        Me.txtDescription.Multiline = true
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(382, 195)
        Me.txtDescription.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(12, 311)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(109, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Attached campaign:"
        '
        'dlgOpen
        '
        Me.dlgOpen.FileName = "*.*"
        Me.dlgOpen.Filter = "All files|*.*"
        '
        'txtFile
        '
        Me.txtFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.txtFile.Enabled = false
        Me.txtFile.Location = New System.Drawing.Point(12, 329)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.Size = New System.Drawing.Size(220, 22)
        Me.txtFile.TabIndex = 5
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.cmdBrowse.FlatAppearance.BorderSize = 0
        Me.cmdBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdBrowse.Location = New System.Drawing.Point(319, 328)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(75, 21)
        Me.cmdBrowse.TabIndex = 6
        Me.cmdBrowse.Text = "Browse"
        Me.cmdBrowse.UseVisualStyleBackColor = true
        Me.cmdBrowse.Visible = false
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label4.AutoSize = true
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 354)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(220, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Note: Campaigns are automatically attached"
        Me.Label4.Visible = false
        '
        'cmdSend
        '
        Me.cmdSend.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.cmdSend.FlatAppearance.BorderSize = 0
        Me.cmdSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSend.Location = New System.Drawing.Point(301, 370)
        Me.cmdSend.Name = "cmdSend"
        Me.cmdSend.Size = New System.Drawing.Size(93, 28)
        Me.cmdSend.TabIndex = 8
        Me.cmdSend.Text = "Send"
        Me.cmdSend.UseVisualStyleBackColor = true
        '
        'chkScreenShot
        '
        Me.chkScreenShot.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.chkScreenShot.AutoSize = true
        Me.chkScreenShot.Checked = true
        Me.chkScreenShot.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkScreenShot.Location = New System.Drawing.Point(12, 376)
        Me.chkScreenShot.Name = "chkScreenShot"
        Me.chkScreenShot.Size = New System.Drawing.Size(126, 17)
        Me.chkScreenShot.TabIndex = 9
        Me.chkScreenShot.Text = "Include screen shot"
        Me.chkScreenShot.UseVisualStyleBackColor = true
        '
        'frmErrorReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(406, 405)
        Me.Controls.Add(Me.chkScreenShot)
        Me.Controls.Add(Me.cmdSend)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmdBrowse)
        Me.Controls.Add(Me.txtFile)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmErrorReport"
        Me.Text = "Send Error Report"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dlgOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdSend As System.Windows.Forms.Button
    Friend WithEvents chkScreenShot As System.Windows.Forms.CheckBox
End Class
