<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConnectContract
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConnectContract))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.optConnect = New System.Windows.Forms.RadioButton()
        Me.optImport = New System.Windows.Forms.RadioButton()
        Me.optDisconnected = New System.Windows.Forms.RadioButton()
        Me.cmbContract = New System.Windows.Forms.ComboBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(379, 148)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.handshake_2
        Me.PictureBox1.Location = New System.Drawing.Point(12, 11)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = false
        '
        'optConnect
        '
        Me.optConnect.AutoSize = true
        Me.optConnect.Checked = true
        Me.optConnect.Location = New System.Drawing.Point(15, 214)
        Me.optConnect.Name = "optConnect"
        Me.optConnect.Size = New System.Drawing.Size(130, 17)
        Me.optConnect.TabIndex = 2
        Me.optConnect.TabStop = true
        Me.optConnect.Text = "Connect to contract:"
        Me.optConnect.UseVisualStyleBackColor = true
        '
        'optImport
        '
        Me.optImport.AutoSize = true
        Me.optImport.Location = New System.Drawing.Point(15, 237)
        Me.optImport.Name = "optImport"
        Me.optImport.Size = New System.Drawing.Size(124, 17)
        Me.optImport.TabIndex = 4
        Me.optImport.Text = "Import the contract"
        Me.optImport.UseVisualStyleBackColor = true
        '
        'optDisconnected
        '
        Me.optDisconnected.AutoSize = true
        Me.optDisconnected.Location = New System.Drawing.Point(15, 259)
        Me.optDisconnected.Name = "optDisconnected"
        Me.optDisconnected.Size = New System.Drawing.Size(125, 17)
        Me.optDisconnected.TabIndex = 5
        Me.optDisconnected.Text = "Work disconnected"
        Me.optDisconnected.UseVisualStyleBackColor = true
        '
        'cmbContract
        '
        Me.cmbContract.DisplayMember = "Name"
        Me.cmbContract.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbContract.FormattingEnabled = true
        Me.cmbContract.Location = New System.Drawing.Point(144, 214)
        Me.cmbContract.Name = "cmbContract"
        Me.cmbContract.Size = New System.Drawing.Size(247, 21)
        Me.cmbContract.TabIndex = 6
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Location = New System.Drawing.Point(298, 289)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(93, 29)
        Me.cmdOk.TabIndex = 7
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'frmConnectContract
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(403, 329)
        Me.ControlBox = false
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.cmbContract)
        Me.Controls.Add(Me.optDisconnected)
        Me.Controls.Add(Me.optImport)
        Me.Controls.Add(Me.optConnect)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmConnectContract"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Connect contract"
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents optConnect As System.Windows.Forms.RadioButton
    Friend WithEvents optImport As System.Windows.Forms.RadioButton
    Friend WithEvents optDisconnected As System.Windows.Forms.RadioButton
    Friend WithEvents cmbContract As System.Windows.Forms.ComboBox
    Friend WithEvents cmdOk As System.Windows.Forms.Button
End Class
