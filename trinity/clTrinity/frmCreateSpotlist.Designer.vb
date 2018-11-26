<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateSpotlist
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCreateSpotlist))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.tvwAvailable = New System.Windows.Forms.TreeView()
        Me.tvwChosen = New System.Windows.Forms.TreeView()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(0, 262)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(583, 522)
        Me.DataGridView1.TabIndex = 0
        '
        'tvwAvailable
        '
        Me.tvwAvailable.Location = New System.Drawing.Point(12, 12)
        Me.tvwAvailable.Name = "tvwAvailable"
        Me.tvwAvailable.Size = New System.Drawing.Size(121, 157)
        Me.tvwAvailable.TabIndex = 1
        '
        'tvwChosen
        '
        Me.tvwChosen.Location = New System.Drawing.Point(139, 12)
        Me.tvwChosen.Name = "tvwChosen"
        Me.tvwChosen.Size = New System.Drawing.Size(121, 157)
        Me.tvwChosen.TabIndex = 2
        '
        'frmCreateSpotlist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(583, 784)
        Me.Controls.Add(Me.tvwChosen)
        Me.Controls.Add(Me.tvwAvailable)
        Me.Controls.Add(Me.DataGridView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCreateSpotlist"
        Me.Text = "frmCreateSpotlist"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents tvwAvailable As System.Windows.Forms.TreeView
    Friend WithEvents tvwChosen As System.Windows.Forms.TreeView
End Class
