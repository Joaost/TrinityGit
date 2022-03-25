<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManageChannelsUnicorn
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmManageChannelsUnicorn))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCHName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnRemoveChannelHouse = New System.Windows.Forms.Button()
        Me.btnAddChannelHouse = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.tvwChannelHouse = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.lblUpdateInfo = New System.Windows.Forms.Label()
        Me.btnAddChannel = New System.Windows.Forms.Button()
        Me.btnRemoveChannel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 275)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Channel name"
        '
        'txtCHName
        '
        Me.txtCHName.Enabled = False
        Me.txtCHName.Location = New System.Drawing.Point(12, 291)
        Me.txtCHName.Name = "txtCHName"
        Me.txtCHName.Size = New System.Drawing.Size(216, 20)
        Me.txtCHName.TabIndex = 31
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 13)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Select channel house below:"
        '
        'btnRemoveChannelHouse
        '
        Me.btnRemoveChannelHouse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveChannelHouse.Enabled = False
        Me.btnRemoveChannelHouse.FlatAppearance.BorderSize = 0
        Me.btnRemoveChannelHouse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveChannelHouse.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.btnRemoveChannelHouse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemoveChannelHouse.Location = New System.Drawing.Point(268, 91)
        Me.btnRemoveChannelHouse.Name = "btnRemoveChannelHouse"
        Me.btnRemoveChannelHouse.Size = New System.Drawing.Size(171, 35)
        Me.btnRemoveChannelHouse.TabIndex = 35
        Me.btnRemoveChannelHouse.Text = "Remove channel house"
        Me.btnRemoveChannelHouse.UseVisualStyleBackColor = True
        '
        'btnAddChannelHouse
        '
        Me.btnAddChannelHouse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddChannelHouse.FlatAppearance.BorderSize = 0
        Me.btnAddChannelHouse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddChannelHouse.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.btnAddChannelHouse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddChannelHouse.Location = New System.Drawing.Point(268, 52)
        Me.btnAddChannelHouse.Name = "btnAddChannelHouse"
        Me.btnAddChannelHouse.Size = New System.Drawing.Size(142, 33)
        Me.btnAddChannelHouse.TabIndex = 34
        Me.btnAddChannelHouse.Text = "Add channel house"
        Me.btnAddChannelHouse.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.Location = New System.Drawing.Point(247, 291)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 20)
        Me.btnSave.TabIndex = 36
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(340, 275)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(83, 36)
        Me.btnExit.TabIndex = 40
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'tvwChannelHouse
        '
        Me.tvwChannelHouse.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwChannelHouse.Location = New System.Drawing.Point(12, 52)
        Me.tvwChannelHouse.Name = "tvwChannelHouse"
        Me.tvwChannelHouse.Size = New System.Drawing.Size(250, 210)
        Me.tvwChannelHouse.TabIndex = 41
        Me.tvwChannelHouse.Tag = "Identify"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'lblUpdateInfo
        '
        Me.lblUpdateInfo.AutoSize = True
        Me.lblUpdateInfo.Location = New System.Drawing.Point(9, 317)
        Me.lblUpdateInfo.Name = "lblUpdateInfo"
        Me.lblUpdateInfo.Size = New System.Drawing.Size(0, 13)
        Me.lblUpdateInfo.TabIndex = 42
        '
        'btnAddChannel
        '
        Me.btnAddChannel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddChannel.Enabled = False
        Me.btnAddChannel.FlatAppearance.BorderSize = 0
        Me.btnAddChannel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddChannel.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.btnAddChannel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddChannel.Location = New System.Drawing.Point(268, 132)
        Me.btnAddChannel.Name = "btnAddChannel"
        Me.btnAddChannel.Size = New System.Drawing.Size(110, 33)
        Me.btnAddChannel.TabIndex = 43
        Me.btnAddChannel.Text = "Add channel"
        Me.btnAddChannel.UseVisualStyleBackColor = True
        '
        'btnRemoveChannel
        '
        Me.btnRemoveChannel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveChannel.Enabled = False
        Me.btnRemoveChannel.FlatAppearance.BorderSize = 0
        Me.btnRemoveChannel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveChannel.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.btnRemoveChannel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemoveChannel.Location = New System.Drawing.Point(268, 171)
        Me.btnRemoveChannel.Name = "btnRemoveChannel"
        Me.btnRemoveChannel.Size = New System.Drawing.Size(130, 35)
        Me.btnRemoveChannel.TabIndex = 44
        Me.btnRemoveChannel.Text = "Remove channel"
        Me.btnRemoveChannel.UseVisualStyleBackColor = True
        '
        'frmManageChannelsUnicorn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(451, 339)
        Me.Controls.Add(Me.btnRemoveChannel)
        Me.Controls.Add(Me.btnAddChannel)
        Me.Controls.Add(Me.lblUpdateInfo)
        Me.Controls.Add(Me.tvwChannelHouse)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnRemoveChannelHouse)
        Me.Controls.Add(Me.btnAddChannelHouse)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCHName)
        Me.Controls.Add(Me.Label2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmManageChannelsUnicorn"
        Me.Text = "Unicorn - Manage channels"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents txtCHName As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents btnRemoveChannelHouse As Windows.Forms.Button
    Friend WithEvents btnAddChannelHouse As Windows.Forms.Button
    Friend WithEvents btnSave As Windows.Forms.Button
    Friend WithEvents btnExit As Windows.Forms.Button
    Friend WithEvents tvwChannelHouse As Windows.Forms.TreeView
    Friend WithEvents ImageList1 As Windows.Forms.ImageList
    Friend WithEvents lblUpdateInfo As Windows.Forms.Label
    Friend WithEvents btnAddChannel As Windows.Forms.Button
    Friend WithEvents btnRemoveChannel As Windows.Forms.Button
End Class
