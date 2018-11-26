<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Me.CampaignBox = New System.Windows.Forms.PictureBox
        Me.AdEdgeBox = New System.Windows.Forms.PictureBox
        Me.TV10Box = New System.Windows.Forms.PictureBox
        Me.TV8Box = New System.Windows.Forms.PictureBox
        Me.TV6Box = New System.Windows.Forms.PictureBox
        Me.K9Box = New System.Windows.Forms.PictureBox
        Me.K5Box = New System.Windows.Forms.PictureBox
        Me.TV3Box = New System.Windows.Forms.PictureBox
        Me.TV4Box = New System.Windows.Forms.PictureBox
        Me.prgProgress = New System.Windows.Forms.ProgressBar
        Me.lblTimeleft = New System.Windows.Forms.Label
        Me.lblCampaignsleft = New System.Windows.Forms.Label
        CType(Me.CampaignBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AdEdgeBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TV10Box, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TV8Box, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TV6Box, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.K9Box, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.K5Box, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TV3Box, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TV4Box, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CampaignBox
        '
        Me.CampaignBox.Image = Global.TablåSkyffel.My.Resources.Resources.files
        Me.CampaignBox.Location = New System.Drawing.Point(12, 616)
        Me.CampaignBox.Name = "CampaignBox"
        Me.CampaignBox.Size = New System.Drawing.Size(200, 82)
        Me.CampaignBox.TabIndex = 8
        Me.CampaignBox.TabStop = False
        '
        'AdEdgeBox
        '
        Me.AdEdgeBox.Image = Global.TablåSkyffel.My.Resources.Resources.techedge
        Me.AdEdgeBox.Location = New System.Drawing.Point(12, 528)
        Me.AdEdgeBox.Name = "AdEdgeBox"
        Me.AdEdgeBox.Size = New System.Drawing.Size(200, 82)
        Me.AdEdgeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.AdEdgeBox.TabIndex = 7
        Me.AdEdgeBox.TabStop = False
        '
        'TV10Box
        '
        Me.TV10Box.Image = Global.TablåSkyffel.My.Resources.Resources.tv10
        Me.TV10Box.Location = New System.Drawing.Point(112, 306)
        Me.TV10Box.Name = "TV10Box"
        Me.TV10Box.Size = New System.Drawing.Size(112, 107)
        Me.TV10Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.TV10Box.TabIndex = 6
        Me.TV10Box.TabStop = False
        '
        'TV8Box
        '
        Me.TV8Box.Image = Global.TablåSkyffel.My.Resources.Resources.tv8
        Me.TV8Box.Location = New System.Drawing.Point(0, 306)
        Me.TV8Box.Name = "TV8Box"
        Me.TV8Box.Size = New System.Drawing.Size(112, 107)
        Me.TV8Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.TV8Box.TabIndex = 5
        Me.TV8Box.TabStop = False
        '
        'TV6Box
        '
        Me.TV6Box.Image = Global.TablåSkyffel.My.Resources.Resources.tv6
        Me.TV6Box.Location = New System.Drawing.Point(112, 197)
        Me.TV6Box.Name = "TV6Box"
        Me.TV6Box.Size = New System.Drawing.Size(112, 107)
        Me.TV6Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.TV6Box.TabIndex = 4
        Me.TV6Box.TabStop = False
        '
        'K9Box
        '
        Me.K9Box.Image = Global.TablåSkyffel.My.Resources.Resources.k9
        Me.K9Box.Location = New System.Drawing.Point(112, 422)
        Me.K9Box.Name = "K9Box"
        Me.K9Box.Size = New System.Drawing.Size(100, 100)
        Me.K9Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.K9Box.TabIndex = 3
        Me.K9Box.TabStop = False
        '
        'K5Box
        '
        Me.K5Box.Image = Global.TablåSkyffel.My.Resources.Resources.k5
        Me.K5Box.Location = New System.Drawing.Point(12, 422)
        Me.K5Box.Name = "K5Box"
        Me.K5Box.Size = New System.Drawing.Size(100, 100)
        Me.K5Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.K5Box.TabIndex = 2
        Me.K5Box.TabStop = False
        '
        'TV3Box
        '
        Me.TV3Box.Image = Global.TablåSkyffel.My.Resources.Resources.tv3
        Me.TV3Box.Location = New System.Drawing.Point(0, 197)
        Me.TV3Box.Name = "TV3Box"
        Me.TV3Box.Size = New System.Drawing.Size(112, 107)
        Me.TV3Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.TV3Box.TabIndex = 1
        Me.TV3Box.TabStop = False
        '
        'TV4Box
        '
        Me.TV4Box.Image = Global.TablåSkyffel.My.Resources.Resources.tv4
        Me.TV4Box.Location = New System.Drawing.Point(12, 0)
        Me.TV4Box.Name = "TV4Box"
        Me.TV4Box.Size = New System.Drawing.Size(200, 191)
        Me.TV4Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.TV4Box.TabIndex = 0
        Me.TV4Box.TabStop = False
        '
        'prgProgress
        '
        Me.prgProgress.Location = New System.Drawing.Point(12, 704)
        Me.prgProgress.Name = "prgProgress"
        Me.prgProgress.Size = New System.Drawing.Size(212, 23)
        Me.prgProgress.TabIndex = 9
        '
        'lblTimeleft
        '
        Me.lblTimeleft.AutoSize = True
        Me.lblTimeleft.Location = New System.Drawing.Point(12, 616)
        Me.lblTimeleft.Name = "lblTimeleft"
        Me.lblTimeleft.Size = New System.Drawing.Size(0, 13)
        Me.lblTimeleft.TabIndex = 10
        '
        'lblCampaignsleft
        '
        Me.lblCampaignsleft.AutoSize = True
        Me.lblCampaignsleft.Location = New System.Drawing.Point(12, 638)
        Me.lblCampaignsleft.Name = "lblCampaignsleft"
        Me.lblCampaignsleft.Size = New System.Drawing.Size(0, 13)
        Me.lblCampaignsleft.TabIndex = 11
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(228, 732)
        Me.Controls.Add(Me.lblCampaignsleft)
        Me.Controls.Add(Me.lblTimeleft)
        Me.Controls.Add(Me.prgProgress)
        Me.Controls.Add(Me.CampaignBox)
        Me.Controls.Add(Me.AdEdgeBox)
        Me.Controls.Add(Me.TV10Box)
        Me.Controls.Add(Me.TV8Box)
        Me.Controls.Add(Me.TV6Box)
        Me.Controls.Add(Me.K9Box)
        Me.Controls.Add(Me.K5Box)
        Me.Controls.Add(Me.TV3Box)
        Me.Controls.Add(Me.TV4Box)
        Me.Name = "Form2"
        Me.Text = "Tablåskyffel V1.0"
        CType(Me.CampaignBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AdEdgeBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TV10Box, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TV8Box, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TV6Box, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.K9Box, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.K5Box, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TV3Box, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TV4Box, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TV4Box As System.Windows.Forms.PictureBox
    Friend WithEvents TV3Box As System.Windows.Forms.PictureBox
    Friend WithEvents K5Box As System.Windows.Forms.PictureBox
    Friend WithEvents K9Box As System.Windows.Forms.PictureBox
    Friend WithEvents TV6Box As System.Windows.Forms.PictureBox
    Friend WithEvents TV8Box As System.Windows.Forms.PictureBox
    Friend WithEvents TV10Box As System.Windows.Forms.PictureBox
    Friend WithEvents AdEdgeBox As System.Windows.Forms.PictureBox
    Friend WithEvents CampaignBox As System.Windows.Forms.PictureBox
    Friend WithEvents prgProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents lblTimeleft As System.Windows.Forms.Label
    Friend WithEvents lblCampaignsleft As System.Windows.Forms.Label
End Class
