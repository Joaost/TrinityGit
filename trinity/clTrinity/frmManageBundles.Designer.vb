<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManageBundles
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmManageBundles))
        Me.lstContents = New System.Windows.Forms.ListBox()
        Me.lstAvailable = New System.Windows.Forms.ListBox()
        Me.cmbPackages = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'lstContents
        '
        Me.lstContents.FormattingEnabled = true
        Me.lstContents.Location = New System.Drawing.Point(12, 101)
        Me.lstContents.Name = "lstContents"
        Me.lstContents.Size = New System.Drawing.Size(207, 225)
        Me.lstContents.TabIndex = 0
        '
        'lstAvailable
        '
        Me.lstAvailable.FormattingEnabled = true
        Me.lstAvailable.Location = New System.Drawing.Point(225, 101)
        Me.lstAvailable.Name = "lstAvailable"
        Me.lstAvailable.Size = New System.Drawing.Size(203, 225)
        Me.lstAvailable.TabIndex = 1
        '
        'cmbPackages
        '
        Me.cmbPackages.FormattingEnabled = true
        Me.cmbPackages.Location = New System.Drawing.Point(12, 74)
        Me.cmbPackages.Name = "cmbPackages"
        Me.cmbPackages.Size = New System.Drawing.Size(173, 21)
        Me.cmbPackages.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Package"
        '
        'Button1
        '
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Image = Global.clTrinity.My.Resources.Resources.save_2_small
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Button1.Location = New System.Drawing.Point(353, 72)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Save"
        Me.Button1.UseVisualStyleBackColor = true
        '
        'cmdDelete
        '
        Me.cmdDelete.FlatAppearance.BorderSize = 0
        Me.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDelete.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdDelete.Location = New System.Drawing.Point(285, 72)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(52, 23)
        Me.cmdDelete.TabIndex = 4
        Me.cmdDelete.UseVisualStyleBackColor = true
        '
        'cmdAdd
        '
        Me.cmdAdd.FlatAppearance.BorderSize = 0
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.cmdAdd.Location = New System.Drawing.Point(214, 72)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(52, 23)
        Me.cmdAdd.TabIndex = 3
        Me.cmdAdd.UseVisualStyleBackColor = true
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(416, 46)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "This window lets you configure packages of bookingtypes. Configure them here, the"& _ 
    "n select them in the Channels part of the Setup window. This way, you can add se"& _ 
    "veral booking types at the same time."
        '
        'frmManageBundles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 333)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.cmbPackages)
        Me.Controls.Add(Me.lstAvailable)
        Me.Controls.Add(Me.lstContents)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmManageBundles"
        Me.Text = "Packages"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents lstContents As System.Windows.Forms.ListBox
    Friend WithEvents lstAvailable As System.Windows.Forms.ListBox
    Friend WithEvents cmbPackages As System.Windows.Forms.ComboBox
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
