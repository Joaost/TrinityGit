<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExtranet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExtranet))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.lstUsers = New System.Windows.Forms.CheckedListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lstAuthorizers = New System.Windows.Forms.CheckedListBox()
        Me.lvwDocuments = New System.Windows.Forms.ListView()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.imlDocIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdRemoveDoc = New System.Windows.Forms.Button()
        Me.cmdAddDoc = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.cmdEditUser = New System.Windows.Forms.Button()
        Me.cmdAddUser = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(495, 318)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.FlatAppearance.BorderSize = 0
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Publish"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.FlatAppearance.BorderSize = 0
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'lstUsers
        '
        Me.lstUsers.FormattingEnabled = true
        Me.lstUsers.Location = New System.Drawing.Point(9, 68)
        Me.lstUsers.Name = "lstUsers"
        Me.lstUsers.Size = New System.Drawing.Size(189, 225)
        Me.lstUsers.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(6, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(172, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Users allowed to view campaign"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(229, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(198, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Users allowed to authorize campaign"
        '
        'lstAuthorizers
        '
        Me.lstAuthorizers.FormattingEnabled = true
        Me.lstAuthorizers.Location = New System.Drawing.Point(232, 68)
        Me.lstAuthorizers.Name = "lstAuthorizers"
        Me.lstAuthorizers.Size = New System.Drawing.Size(173, 225)
        Me.lstAuthorizers.TabIndex = 3
        '
        'lvwDocuments
        '
        Me.lvwDocuments.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
        Me.lvwDocuments.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvwDocuments.LargeImageList = Me.imlDocIcons
        Me.lvwDocuments.Location = New System.Drawing.Point(427, 68)
        Me.lvwDocuments.Name = "lvwDocuments"
        Me.lvwDocuments.Size = New System.Drawing.Size(189, 225)
        Me.lvwDocuments.SmallImageList = Me.imlDocIcons
        Me.lvwDocuments.TabIndex = 15
        Me.lvwDocuments.UseCompatibleStateImageBehavior = false
        Me.lvwDocuments.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "File"
        Me.ColumnHeader2.Width = 185
        '
        'imlDocIcons
        '
        Me.imlDocIcons.ImageStream = CType(resources.GetObject("imlDocIcons.ImageStream"),System.Windows.Forms.ImageListStreamer)
        Me.imlDocIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.imlDocIcons.Images.SetKeyName(0, "application/msexcel")
        Me.imlDocIcons.Images.SetKeyName(1, "application/pdf")
        Me.imlDocIcons.Images.SetKeyName(2, "application/msword")
        Me.imlDocIcons.Images.SetKeyName(3, "application/mspowerpoint")
        Me.imlDocIcons.Images.SetKeyName(4, "unknown")
        Me.imlDocIcons.Images.SetKeyName(5, "text/plain")
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(424, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Documents"
        '
        'cmdRemoveDoc
        '
        Me.cmdRemoveDoc.FlatAppearance.BorderSize = 0
        Me.cmdRemoveDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRemoveDoc.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdRemoveDoc.Location = New System.Drawing.Point(622, 94)
        Me.cmdRemoveDoc.Name = "cmdRemoveDoc"
        Me.cmdRemoveDoc.Size = New System.Drawing.Size(22, 20)
        Me.cmdRemoveDoc.TabIndex = 18
        Me.cmdRemoveDoc.UseVisualStyleBackColor = true
        '
        'cmdAddDoc
        '
        Me.cmdAddDoc.FlatAppearance.BorderSize = 0
        Me.cmdAddDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddDoc.Image = CType(resources.GetObject("cmdAddDoc.Image"),System.Drawing.Image)
        Me.cmdAddDoc.Location = New System.Drawing.Point(622, 68)
        Me.cmdAddDoc.Name = "cmdAddDoc"
        Me.cmdAddDoc.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddDoc.TabIndex = 17
        Me.cmdAddDoc.UseVisualStyleBackColor = true
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.world_2_24x24
        Me.PictureBox1.Location = New System.Drawing.Point(12, 11)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 14
        Me.PictureBox1.TabStop = false
        '
        'cmdEditUser
        '
        Me.cmdEditUser.Image = Global.clTrinity.My.Resources.Resources.edit_note_3
        Me.cmdEditUser.Location = New System.Drawing.Point(204, 94)
        Me.cmdEditUser.Name = "cmdEditUser"
        Me.cmdEditUser.Size = New System.Drawing.Size(22, 20)
        Me.cmdEditUser.TabIndex = 13
        Me.cmdEditUser.UseVisualStyleBackColor = true
        '
        'cmdAddUser
        '
        Me.cmdAddUser.Image = CType(resources.GetObject("cmdAddUser.Image"),System.Drawing.Image)
        Me.cmdAddUser.Location = New System.Drawing.Point(204, 68)
        Me.cmdAddUser.Name = "cmdAddUser"
        Me.cmdAddUser.Size = New System.Drawing.Size(22, 20)
        Me.cmdAddUser.TabIndex = 12
        Me.cmdAddUser.UseVisualStyleBackColor = true
        '
        'frmExtranet
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(653, 358)
        Me.Controls.Add(Me.cmdRemoveDoc)
        Me.Controls.Add(Me.cmdAddDoc)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lvwDocuments)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.cmdEditUser)
        Me.Controls.Add(Me.cmdAddUser)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lstAuthorizers)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstUsers)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmExtranet"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Publish to Extranet"
        Me.TableLayoutPanel1.ResumeLayout(false)
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents lstUsers As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lstAuthorizers As System.Windows.Forms.CheckedListBox
    Friend WithEvents cmdEditUser As System.Windows.Forms.Button
    Friend WithEvents cmdAddUser As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lvwDocuments As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdRemoveDoc As System.Windows.Forms.Button
    Friend WithEvents cmdAddDoc As System.Windows.Forms.Button
    Friend WithEvents imlDocIcons As System.Windows.Forms.ImageList

End Class
