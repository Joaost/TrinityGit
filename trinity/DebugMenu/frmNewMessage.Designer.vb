<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewMessage
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkSweden = New System.Windows.Forms.CheckBox()
        Me.chkNorway = New System.Windows.Forms.CheckBox()
        Me.chkDenmark = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtHeadline = New System.Windows.Forms.TextBox()
        Me.txtIngress = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBody = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdCreate = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkDenmark)
        Me.GroupBox1.Controls.Add(Me.chkNorway)
        Me.GroupBox1.Controls.Add(Me.chkSweden)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(260, 96)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Countries"
        '
        'chkSweden
        '
        Me.chkSweden.AutoSize = True
        Me.chkSweden.Checked = True
        Me.chkSweden.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSweden.Location = New System.Drawing.Point(7, 20)
        Me.chkSweden.Name = "chkSweden"
        Me.chkSweden.Size = New System.Drawing.Size(67, 18)
        Me.chkSweden.TabIndex = 0
        Me.chkSweden.Text = "Sweden"
        Me.chkSweden.UseVisualStyleBackColor = True
        '
        'chkNorway
        '
        Me.chkNorway.AutoSize = True
        Me.chkNorway.Enabled = False
        Me.chkNorway.Location = New System.Drawing.Point(7, 44)
        Me.chkNorway.Name = "chkNorway"
        Me.chkNorway.Size = New System.Drawing.Size(65, 18)
        Me.chkNorway.TabIndex = 1
        Me.chkNorway.Text = "Norway"
        Me.chkNorway.UseVisualStyleBackColor = True
        '
        'chkDenmark
        '
        Me.chkDenmark.AutoSize = True
        Me.chkDenmark.Enabled = False
        Me.chkDenmark.Location = New System.Drawing.Point(7, 68)
        Me.chkDenmark.Name = "chkDenmark"
        Me.chkDenmark.Size = New System.Drawing.Size(68, 18)
        Me.chkDenmark.TabIndex = 2
        Me.chkDenmark.Text = "Denmark"
        Me.chkDenmark.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 111)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Headline"
        '
        'txtHeadline
        '
        Me.txtHeadline.Location = New System.Drawing.Point(12, 128)
        Me.txtHeadline.Name = "txtHeadline"
        Me.txtHeadline.Size = New System.Drawing.Size(260, 20)
        Me.txtHeadline.TabIndex = 2
        '
        'txtIngress
        '
        Me.txtIngress.Location = New System.Drawing.Point(12, 168)
        Me.txtIngress.Multiline = True
        Me.txtIngress.Name = "txtIngress"
        Me.txtIngress.Size = New System.Drawing.Size(260, 68)
        Me.txtIngress.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 151)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Ingress"
        '
        'txtBody
        '
        Me.txtBody.Location = New System.Drawing.Point(12, 256)
        Me.txtBody.Multiline = True
        Me.txtBody.Name = "txtBody"
        Me.txtBody.Size = New System.Drawing.Size(260, 239)
        Me.txtBody.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 239)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 14)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Body"
        '
        'cmdCreate
        '
        Me.cmdCreate.Location = New System.Drawing.Point(197, 501)
        Me.cmdCreate.Name = "cmdCreate"
        Me.cmdCreate.Size = New System.Drawing.Size(75, 32)
        Me.cmdCreate.TabIndex = 7
        Me.cmdCreate.Text = "Create"
        Me.cmdCreate.UseVisualStyleBackColor = True
        '
        'frmNewMessage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 545)
        Me.Controls.Add(Me.cmdCreate)
        Me.Controls.Add(Me.txtBody)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtIngress)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtHeadline)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmNewMessage"
        Me.Text = "Create message"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkDenmark As System.Windows.Forms.CheckBox
    Friend WithEvents chkNorway As System.Windows.Forms.CheckBox
    Friend WithEvents chkSweden As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtHeadline As System.Windows.Forms.TextBox
    Friend WithEvents txtIngress As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtBody As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdCreate As System.Windows.Forms.Button
End Class
