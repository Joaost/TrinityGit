<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRTFEditor
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
        Me.rtxOutline = New System.Windows.Forms.RichTextBox
        Me.tsOutline = New System.Windows.Forms.ToolStrip
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.cmbSize = New System.Windows.Forms.ToolStripComboBox
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.cdlRTF = New System.Windows.Forms.ColorDialog
        Me.ofdRTF = New System.Windows.Forms.OpenFileDialog
        Me.cmdBold = New System.Windows.Forms.ToolStripButton
        Me.cmdItalics = New System.Windows.Forms.ToolStripButton
        Me.cmdUnderlined = New System.Windows.Forms.ToolStripButton
        Me.cmdLeft = New System.Windows.Forms.ToolStripButton
        Me.cmdCentered = New System.Windows.Forms.ToolStripButton
        Me.cmdRight = New System.Windows.Forms.ToolStripButton
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
        Me.cmdTextColor = New System.Windows.Forms.ToolStripButton
        Me.cmdBullets = New System.Windows.Forms.ToolStripButton
        Me.cmdInsertImage = New System.Windows.Forms.ToolStripButton
        Me.cmdFind = New System.Windows.Forms.ToolStripButton
        Me.cmdUndo = New System.Windows.Forms.ToolStripButton
        Me.cmdRedo = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.tsOutline.SuspendLayout()
        Me.SuspendLayout()
        '
        'rtxOutline
        '
        Me.rtxOutline.AcceptsTab = True
        Me.rtxOutline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtxOutline.Location = New System.Drawing.Point(0, 25)
        Me.rtxOutline.Name = "rtxOutline"
        Me.rtxOutline.Size = New System.Drawing.Size(699, 614)
        Me.rtxOutline.TabIndex = 0
        Me.rtxOutline.Text = ""
        '
        'tsOutline
        '
        Me.tsOutline.AutoSize = False
        Me.tsOutline.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdBold, Me.cmdItalics, Me.cmdUnderlined, Me.ToolStripSeparator1, Me.cmdLeft, Me.cmdCentered, Me.cmdRight, Me.ToolStripSeparator2, Me.ToolStripLabel1, Me.cmbSize, Me.cmdTextColor, Me.cmdBullets, Me.ToolStripSeparator3, Me.cmdInsertImage, Me.ToolStripSeparator4, Me.cmdFind, Me.ToolStripSeparator5, Me.cmdUndo, Me.cmdRedo})
        Me.tsOutline.Location = New System.Drawing.Point(0, 0)
        Me.tsOutline.Name = "tsOutline"
        Me.tsOutline.Size = New System.Drawing.Size(699, 25)
        Me.tsOutline.TabIndex = 2
        Me.tsOutline.Text = "ToolStrip1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'cmbSize
        '
        Me.cmbSize.Items.AddRange(New Object() {"8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72"})
        Me.cmbSize.Name = "cmbSize"
        Me.cmbSize.Size = New System.Drawing.Size(75, 25)
        Me.cmbSize.ToolTipText = "Font size"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'ofdRTF
        '
        Me.ofdRTF.DefaultExt = "*.jpg"
        Me.ofdRTF.FileName = "*.jpg"
        Me.ofdRTF.Filter = "JPEG|*.jpg|GIF|*.gif|Bitmap|*.bmp"
        Me.ofdRTF.Title = "Open image"
        '
        'cmdBold
        '
        Me.cmdBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdBold.Image = Global.Balthazar.My.Resources.Resources.text_bold
        Me.cmdBold.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdBold.Name = "cmdBold"
        Me.cmdBold.Size = New System.Drawing.Size(23, 22)
        Me.cmdBold.Text = "ToolStripButton1"
        Me.cmdBold.ToolTipText = "Bold"
        '
        'cmdItalics
        '
        Me.cmdItalics.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdItalics.Image = Global.Balthazar.My.Resources.Resources.text_italics
        Me.cmdItalics.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdItalics.Name = "cmdItalics"
        Me.cmdItalics.Size = New System.Drawing.Size(23, 22)
        Me.cmdItalics.Text = "ToolStripButton2"
        Me.cmdItalics.ToolTipText = "Italic"
        '
        'cmdUnderlined
        '
        Me.cmdUnderlined.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdUnderlined.Image = Global.Balthazar.My.Resources.Resources.text_underlined
        Me.cmdUnderlined.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdUnderlined.Name = "cmdUnderlined"
        Me.cmdUnderlined.Size = New System.Drawing.Size(23, 22)
        Me.cmdUnderlined.Text = "ToolStripButton3"
        Me.cmdUnderlined.ToolTipText = "Underlined"
        '
        'cmdLeft
        '
        Me.cmdLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdLeft.Image = Global.Balthazar.My.Resources.Resources.text_align_left
        Me.cmdLeft.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdLeft.Name = "cmdLeft"
        Me.cmdLeft.Size = New System.Drawing.Size(23, 22)
        Me.cmdLeft.Text = "ToolStripButton1"
        Me.cmdLeft.ToolTipText = "Left justified"
        '
        'cmdCentered
        '
        Me.cmdCentered.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdCentered.Image = Global.Balthazar.My.Resources.Resources.text_align_center
        Me.cmdCentered.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdCentered.Name = "cmdCentered"
        Me.cmdCentered.Size = New System.Drawing.Size(23, 22)
        Me.cmdCentered.Text = "ToolStripButton2"
        Me.cmdCentered.ToolTipText = "Centered"
        '
        'cmdRight
        '
        Me.cmdRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdRight.Image = Global.Balthazar.My.Resources.Resources.text_align_right
        Me.cmdRight.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdRight.Name = "cmdRight"
        Me.cmdRight.Size = New System.Drawing.Size(23, 22)
        Me.cmdRight.Text = "ToolStripButton3"
        Me.cmdRight.ToolTipText = "Right justified"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Image = Global.Balthazar.My.Resources.Resources.font
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(70, 22)
        Me.ToolStripLabel1.Text = "Font size:"
        '
        'cmdTextColor
        '
        Me.cmdTextColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdTextColor.Image = Global.Balthazar.My.Resources.Resources.palette_text
        Me.cmdTextColor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdTextColor.Name = "cmdTextColor"
        Me.cmdTextColor.Size = New System.Drawing.Size(23, 22)
        Me.cmdTextColor.Text = "ToolStripButton2"
        Me.cmdTextColor.ToolTipText = "Font color"
        '
        'cmdBullets
        '
        Me.cmdBullets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdBullets.Image = Global.Balthazar.My.Resources.Resources.text_bullets
        Me.cmdBullets.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdBullets.Name = "cmdBullets"
        Me.cmdBullets.Size = New System.Drawing.Size(23, 22)
        Me.cmdBullets.Text = "ToolStripButton1"
        Me.cmdBullets.ToolTipText = "Bulleted list"
        '
        'cmdInsertImage
        '
        Me.cmdInsertImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdInsertImage.Image = Global.Balthazar.My.Resources.Resources.photo_scenery
        Me.cmdInsertImage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdInsertImage.Name = "cmdInsertImage"
        Me.cmdInsertImage.Size = New System.Drawing.Size(23, 22)
        Me.cmdInsertImage.Text = "ToolStripButton1"
        Me.cmdInsertImage.ToolTipText = "Insert picture"
        '
        'cmdFind
        '
        Me.cmdFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdFind.Image = Global.Balthazar.My.Resources.Resources.find_text
        Me.cmdFind.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdFind.Name = "cmdFind"
        Me.cmdFind.Size = New System.Drawing.Size(23, 22)
        Me.cmdFind.Text = "ToolStripButton1"
        Me.cmdFind.ToolTipText = "Find text"
        '
        'cmdUndo
        '
        Me.cmdUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdUndo.Image = Global.Balthazar.My.Resources.Resources.undo
        Me.cmdUndo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdUndo.Name = "cmdUndo"
        Me.cmdUndo.Size = New System.Drawing.Size(23, 22)
        Me.cmdUndo.Text = "ToolStripButton1"
        Me.cmdUndo.ToolTipText = "Undo"
        '
        'cmdRedo
        '
        Me.cmdRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdRedo.Image = Global.Balthazar.My.Resources.Resources.redo
        Me.cmdRedo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdRedo.Name = "cmdRedo"
        Me.cmdRedo.Size = New System.Drawing.Size(23, 22)
        Me.cmdRedo.Text = "ToolStripButton2"
        Me.cmdRedo.ToolTipText = "Redo"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'frmRTFEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(699, 639)
        Me.Controls.Add(Me.rtxOutline)
        Me.Controls.Add(Me.tsOutline)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmRTFEditor"
        Me.Text = "Edit template"
        Me.tsOutline.ResumeLayout(False)
        Me.tsOutline.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rtxOutline As System.Windows.Forms.RichTextBox
    Friend WithEvents tsOutline As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdBold As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdItalics As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdUnderlined As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdLeft As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdCentered As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdRight As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmbSize As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdBullets As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdFind As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdInsertImage As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdTextColor As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cdlRTF As System.Windows.Forms.ColorDialog
    Friend WithEvents ofdRTF As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmdUndo As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdRedo As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
End Class
