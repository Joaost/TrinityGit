<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPickBrands
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPickBrands))
        Me.cmbDimension = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFilter = New System.Windows.Forms.TextBox()
        Me.tvwAvailable = New System.Windows.Forms.TreeView()
        Me.tvwChosen = New System.Windows.Forms.TreeView()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdRemove = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.SuspendLayout
        '
        'cmbDimension
        '
        Me.cmbDimension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDimension.FormattingEnabled = true
        Me.cmbDimension.Items.AddRange(New Object() {"Advertiser", "Product"})
        Me.cmbDimension.Location = New System.Drawing.Point(12, 11)
        Me.cmbDimension.Name = "cmbDimension"
        Me.cmbDimension.Size = New System.Drawing.Size(155, 21)
        Me.cmbDimension.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 394)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Filter"
        '
        'txtFilter
        '
        Me.txtFilter.Location = New System.Drawing.Point(60, 391)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(192, 22)
        Me.txtFilter.TabIndex = 3
        '
        'tvwAvailable
        '
        Me.tvwAvailable.Location = New System.Drawing.Point(12, 37)
        Me.tvwAvailable.Name = "tvwAvailable"
        Me.tvwAvailable.Size = New System.Drawing.Size(240, 348)
        Me.tvwAvailable.TabIndex = 4
        '
        'tvwChosen
        '
        Me.tvwChosen.Location = New System.Drawing.Point(288, 37)
        Me.tvwChosen.Name = "tvwChosen"
        Me.tvwChosen.Size = New System.Drawing.Size(240, 348)
        Me.tvwChosen.TabIndex = 5
        '
        'cmdOk
        '
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Location = New System.Drawing.Point(452, 391)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(75, 24)
        Me.cmdOk.TabIndex = 6
        Me.cmdOk.Text = "Pick"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'cmdRemove
        '
        Me.cmdRemove.FlatAppearance.BorderSize = 0
        Me.cmdRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRemove.Image = Global.clTrinity.My.Resources.Resources.arrow_left_3_16x16
        Me.cmdRemove.Location = New System.Drawing.Point(258, 210)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(24, 22)
        Me.cmdRemove.TabIndex = 8
        Me.cmdRemove.UseVisualStyleBackColor = true
        '
        'cmdAdd
        '
        Me.cmdAdd.FlatAppearance.BorderSize = 0
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.Image = Global.clTrinity.My.Resources.Resources.arrow_right_3_16x16
        Me.cmdAdd.Location = New System.Drawing.Point(258, 182)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(24, 22)
        Me.cmdAdd.TabIndex = 7
        Me.cmdAdd.UseVisualStyleBackColor = true
        '
        'frmPickBrands
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(539, 426)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.tvwChosen)
        Me.Controls.Add(Me.tvwAvailable)
        Me.Controls.Add(Me.txtFilter)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbDimension)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmPickBrands"
        Me.Text = "Pick brands"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents cmbDimension As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents tvwAvailable As System.Windows.Forms.TreeView
    Friend WithEvents tvwChosen As System.Windows.Forms.TreeView
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
End Class
