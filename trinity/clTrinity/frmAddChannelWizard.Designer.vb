<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddChannelWizard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddChannelWizard))
        Me.tvwAvailable = New System.Windows.Forms.TreeView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstChosen = New System.Windows.Forms.ListBox()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.optBestFit = New System.Windows.Forms.RadioButton()
        Me.optBestPrice = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdRemoveChannel = New System.Windows.Forms.Button()
        Me.cmdAddChannel = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdOpen = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tvwAvailable
        '
        Me.tvwAvailable.Location = New System.Drawing.Point(12, 25)
        Me.tvwAvailable.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.tvwAvailable.Name = "tvwAvailable"
        Me.tvwAvailable.Size = New System.Drawing.Size(276, 516)
        Me.tvwAvailable.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 8)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Available channels"
        '
        'lstChosen
        '
        Me.lstChosen.FormattingEnabled = True
        Me.lstChosen.ItemHeight = 14
        Me.lstChosen.Location = New System.Drawing.Point(321, 25)
        Me.lstChosen.Name = "lstChosen"
        Me.lstChosen.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstChosen.Size = New System.Drawing.Size(263, 438)
        Me.lstChosen.TabIndex = 33
        '
        'cmdAdd
        '
        Me.cmdAdd.FlatAppearance.BorderSize = 0
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.Location = New System.Drawing.Point(498, 507)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(86, 34)
        Me.cmdAdd.TabIndex = 34
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'optBestFit
        '
        Me.optBestFit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.optBestFit.Checked = True
        Me.optBestFit.Location = New System.Drawing.Point(6, 19)
        Me.optBestFit.Name = "optBestFit"
        Me.optBestFit.Size = New System.Drawing.Size(155, 19)
        Me.optBestFit.TabIndex = 35
        Me.optBestFit.TabStop = True
        Me.optBestFit.Text = "Best fit"
        Me.optBestFit.UseVisualStyleBackColor = True
        '
        'optBestPrice
        '
        Me.optBestPrice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.optBestPrice.Enabled = False
        Me.optBestPrice.Location = New System.Drawing.Point(6, 44)
        Me.optBestPrice.Name = "optBestPrice"
        Me.optBestPrice.Size = New System.Drawing.Size(155, 20)
        Me.optBestPrice.TabIndex = 36
        Me.optBestPrice.Text = "Best price"
        Me.optBestPrice.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optBestFit)
        Me.GroupBox1.Controls.Add(Me.optBestPrice)
        Me.GroupBox1.Location = New System.Drawing.Point(321, 471)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(167, 70)
        Me.GroupBox1.TabIndex = 37
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Set buying target to"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(324, 8)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 14)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Chosen channels"
        '
        'cmdRemoveChannel
        '
        Me.cmdRemoveChannel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveChannel.Image = Global.clTrinity.My.Resources.Resources.arrow_left_green
        Me.cmdRemoveChannel.Location = New System.Drawing.Point(293, 279)
        Me.cmdRemoveChannel.Name = "cmdRemoveChannel"
        Me.cmdRemoveChannel.Size = New System.Drawing.Size(22, 22)
        Me.cmdRemoveChannel.TabIndex = 32
        Me.cmdRemoveChannel.UseVisualStyleBackColor = True
        '
        'cmdAddChannel
        '
        Me.cmdAddChannel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddChannel.Image = Global.clTrinity.My.Resources.Resources.arrow_right_green1
        Me.cmdAddChannel.Location = New System.Drawing.Point(293, 251)
        Me.cmdAddChannel.Name = "cmdAddChannel"
        Me.cmdAddChannel.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddChannel.TabIndex = 31
        Me.cmdAddChannel.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.FlatAppearance.BorderSize = 0
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Image = Global.clTrinity.My.Resources.Resources.disk_blue
        Me.cmdSave.Location = New System.Drawing.Point(590, 53)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(22, 22)
        Me.cmdSave.TabIndex = 40
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdOpen
        '
        Me.cmdOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOpen.FlatAppearance.BorderSize = 0
        Me.cmdOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOpen.Image = CType(resources.GetObject("cmdOpen.Image"), System.Drawing.Image)
        Me.cmdOpen.Location = New System.Drawing.Point(590, 25)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(22, 22)
        Me.cmdOpen.TabIndex = 39
        Me.cmdOpen.UseVisualStyleBackColor = True
        '
        'frmAddChannelWizard
        '
        Me.AcceptButton = Me.cmdAdd
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 553)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdOpen)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.lstChosen)
        Me.Controls.Add(Me.cmdRemoveChannel)
        Me.Controls.Add(Me.cmdAddChannel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tvwAvailable)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddChannelWizard"
        Me.Text = "Add channels"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tvwAvailable As System.Windows.Forms.TreeView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdRemoveChannel As System.Windows.Forms.Button
    Friend WithEvents cmdAddChannel As System.Windows.Forms.Button
    Friend WithEvents lstChosen As System.Windows.Forms.ListBox
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents optBestFit As System.Windows.Forms.RadioButton
    Friend WithEvents optBestPrice As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
End Class
