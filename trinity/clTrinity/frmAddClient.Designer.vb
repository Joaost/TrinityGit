<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddClient
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddClient))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.lblNote = New System.Windows.Forms.Label()
        Me.chkBoxRestrictedBool = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name"
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.Location = New System.Drawing.Point(12, 25)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(230, 22)
        Me.txtName.TabIndex = 1
        '
        'cmdOk
        '
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.Location = New System.Drawing.Point(185, 51)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(57, 23)
        Me.cmdOk.TabIndex = 2
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'lblNote
        '
        Me.lblNote.AutoSize = True
        Me.lblNote.Location = New System.Drawing.Point(15, 52)
        Me.lblNote.Name = "lblNote"
        Me.lblNote.Size = New System.Drawing.Size(0, 13)
        Me.lblNote.TabIndex = 3
        '
        'chkBoxRestrictedBool
        '
        Me.chkBoxRestrictedBool.AutoSize = True
        Me.chkBoxRestrictedBool.Location = New System.Drawing.Point(12, 57)
        Me.chkBoxRestrictedBool.Name = "chkBoxRestrictedBool"
        Me.chkBoxRestrictedBool.Size = New System.Drawing.Size(108, 17)
        Me.chkBoxRestrictedBool.TabIndex = 4
        Me.chkBoxRestrictedBool.Text = "Restricted client"
        Me.chkBoxRestrictedBool.UseVisualStyleBackColor = True
        '
        'frmAddClient
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(254, 84)
        Me.Controls.Add(Me.chkBoxRestrictedBool)
        Me.Controls.Add(Me.lblNote)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmAddClient"
        Me.Text = "Add client"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents lblNote As System.Windows.Forms.Label
    Friend WithEvents chkBoxRestrictedBool As Windows.Forms.CheckBox
End Class
