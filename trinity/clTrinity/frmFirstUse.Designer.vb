<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFirstUse
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFirstUse))
        Me.grpCountry = New System.Windows.Forms.GroupBox()
        Me.grpCompany = New System.Windows.Forms.GroupBox()
        Me.grpAdEdge = New System.Windows.Forms.GroupBox()
        Me.lblAdEdgeFound = New System.Windows.Forms.Label()
        Me.btn_SelectAdEdge = New System.Windows.Forms.Button()
        Me.btnFinish = New System.Windows.Forms.Button()
        Me.arrow2 = New System.Windows.Forms.PictureBox()
        Me.arrow1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.grpAdEdge.SuspendLayout
        CType(Me.arrow2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.arrow1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'grpCountry
        '
        Me.grpCountry.Location = New System.Drawing.Point(22, 115)
        Me.grpCountry.Name = "grpCountry"
        Me.grpCountry.Size = New System.Drawing.Size(200, 182)
        Me.grpCountry.TabIndex = 0
        Me.grpCountry.TabStop = false
        Me.grpCountry.Text = "Country"
        '
        'grpCompany
        '
        Me.grpCompany.Location = New System.Drawing.Point(314, 115)
        Me.grpCompany.Name = "grpCompany"
        Me.grpCompany.Size = New System.Drawing.Size(200, 182)
        Me.grpCompany.TabIndex = 1
        Me.grpCompany.TabStop = false
        Me.grpCompany.Text = "Company"
        Me.grpCompany.Visible = false
        '
        'grpAdEdge
        '
        Me.grpAdEdge.Controls.Add(Me.lblAdEdgeFound)
        Me.grpAdEdge.Controls.Add(Me.btn_SelectAdEdge)
        Me.grpAdEdge.Controls.Add(Me.PictureBox1)
        Me.grpAdEdge.Location = New System.Drawing.Point(605, 115)
        Me.grpAdEdge.Name = "grpAdEdge"
        Me.grpAdEdge.Size = New System.Drawing.Size(327, 182)
        Me.grpAdEdge.TabIndex = 1
        Me.grpAdEdge.TabStop = false
        Me.grpAdEdge.Text = "AdvantEdge"
        Me.grpAdEdge.Visible = false
        '
        'lblAdEdgeFound
        '
        Me.lblAdEdgeFound.AutoSize = true
        Me.lblAdEdgeFound.Location = New System.Drawing.Point(41, 103)
        Me.lblAdEdgeFound.Name = "lblAdEdgeFound"
        Me.lblAdEdgeFound.Size = New System.Drawing.Size(104, 13)
        Me.lblAdEdgeFound.TabIndex = 2
        Me.lblAdEdgeFound.Text = "Advantedge found"
        Me.lblAdEdgeFound.Visible = false
        '
        'btn_SelectAdEdge
        '
        Me.btn_SelectAdEdge.FlatAppearance.BorderSize = 0
        Me.btn_SelectAdEdge.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_SelectAdEdge.Location = New System.Drawing.Point(29, 65)
        Me.btn_SelectAdEdge.Name = "btn_SelectAdEdge"
        Me.btn_SelectAdEdge.Size = New System.Drawing.Size(107, 35)
        Me.btn_SelectAdEdge.TabIndex = 1
        Me.btn_SelectAdEdge.Text = "Choose"
        Me.btn_SelectAdEdge.UseVisualStyleBackColor = true
        '
        'btnFinish
        '
        Me.btnFinish.Enabled = false
        Me.btnFinish.FlatAppearance.BorderSize = 0
        Me.btnFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFinish.Location = New System.Drawing.Point(938, 252)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(107, 42)
        Me.btnFinish.TabIndex = 4
        Me.btnFinish.Text = "Finish"
        Me.btnFinish.UseVisualStyleBackColor = true
        Me.btnFinish.Visible = false
        '
        'arrow2
        '
        Me.arrow2.Image = CType(resources.GetObject("arrow2.Image"),System.Drawing.Image)
        Me.arrow2.Location = New System.Drawing.Point(537, 175)
        Me.arrow2.Name = "arrow2"
        Me.arrow2.Size = New System.Drawing.Size(52, 50)
        Me.arrow2.TabIndex = 3
        Me.arrow2.TabStop = false
        Me.arrow2.Visible = false
        '
        'arrow1
        '
        Me.arrow1.Image = CType(resources.GetObject("arrow1.Image"),System.Drawing.Image)
        Me.arrow1.Location = New System.Drawing.Point(228, 175)
        Me.arrow1.Name = "arrow1"
        Me.arrow1.Size = New System.Drawing.Size(52, 50)
        Me.arrow1.TabIndex = 2
        Me.arrow1.TabStop = false
        Me.arrow1.Visible = false
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.delete_2
        Me.PictureBox1.Location = New System.Drawing.Point(163, 76)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(48, 40)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = false
        '
        'frmFirstUse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1055, 314)
        Me.Controls.Add(Me.btnFinish)
        Me.Controls.Add(Me.arrow2)
        Me.Controls.Add(Me.arrow1)
        Me.Controls.Add(Me.grpCompany)
        Me.Controls.Add(Me.grpAdEdge)
        Me.Controls.Add(Me.grpCountry)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmFirstUse"
        Me.Text = "frmFirstUse"
        Me.grpAdEdge.ResumeLayout(false)
        Me.grpAdEdge.PerformLayout
        CType(Me.arrow2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.arrow1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents grpCountry As System.Windows.Forms.GroupBox
    Friend WithEvents grpCompany As System.Windows.Forms.GroupBox
    Friend WithEvents grpAdEdge As System.Windows.Forms.GroupBox
    Friend WithEvents btn_SelectAdEdge As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents arrow1 As System.Windows.Forms.PictureBox
    Friend WithEvents arrow2 As System.Windows.Forms.PictureBox
    Friend WithEvents btnFinish As System.Windows.Forms.Button
    Friend WithEvents lblAdEdgeFound As System.Windows.Forms.Label
End Class
