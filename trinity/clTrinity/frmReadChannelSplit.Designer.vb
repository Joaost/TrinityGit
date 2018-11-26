<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReadChannelSplit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReadChannelSplit))
        Me.cmbChannel = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbBookingtype = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdRemove = New System.Windows.Forms.Button()
        Me.cmdSaveAs = New System.Windows.Forms.Button()
        Me.grdChannelSplitview = New System.Windows.Forms.DataGridView()
        Me.GroupBox1.SuspendLayout
        CType(Me.grdChannelSplitview,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'cmbChannel
        '
        Me.cmbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChannel.FormattingEnabled = true
        Me.cmbChannel.Location = New System.Drawing.Point(77, 31)
        Me.cmbChannel.Name = "cmbChannel"
        Me.cmbChannel.Size = New System.Drawing.Size(170, 21)
        Me.cmbChannel.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(21, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Channel"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(264, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Bookingtype:"
        '
        'cmbBookingtype
        '
        Me.cmbBookingtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBookingtype.FormattingEnabled = true
        Me.cmbBookingtype.Location = New System.Drawing.Point(346, 31)
        Me.cmbBookingtype.Name = "cmbBookingtype"
        Me.cmbBookingtype.Size = New System.Drawing.Size(198, 21)
        Me.cmbBookingtype.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbBookingtype)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbChannel)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(574, 73)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Choose channel and bookingtype"
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.FlatAppearance.BorderSize = 0
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.Image = Global.clTrinity.My.Resources.Resources.add_2_small
        Me.cmdAdd.Location = New System.Drawing.Point(1049, 93)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(22, 20)
        Me.cmdAdd.TabIndex = 19
        Me.cmdAdd.UseVisualStyleBackColor = true
        '
        'cmdRemove
        '
        Me.cmdRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdRemove.FlatAppearance.BorderSize = 0
        Me.cmdRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRemove.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdRemove.Location = New System.Drawing.Point(1049, 119)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(22, 20)
        Me.cmdRemove.TabIndex = 20
        Me.cmdRemove.UseVisualStyleBackColor = true
        '
        'cmdSaveAs
        '
        Me.cmdSaveAs.FlatAppearance.BorderSize = 0
        Me.cmdSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSaveAs.Image = Global.clTrinity.My.Resources.Resources.save_2
        Me.cmdSaveAs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSaveAs.Location = New System.Drawing.Point(12, 431)
        Me.cmdSaveAs.Name = "cmdSaveAs"
        Me.cmdSaveAs.Size = New System.Drawing.Size(103, 37)
        Me.cmdSaveAs.TabIndex = 40
        Me.cmdSaveAs.Text = "Save to file"
        Me.cmdSaveAs.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSaveAs.UseVisualStyleBackColor = true
        '
        'grdChannelSplitview
        '
        Me.grdChannelSplitview.AllowUserToAddRows = false
        Me.grdChannelSplitview.AllowUserToDeleteRows = false
        Me.grdChannelSplitview.AllowUserToResizeColumns = false
        Me.grdChannelSplitview.AllowUserToResizeRows = false
        Me.grdChannelSplitview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grdChannelSplitview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChannelSplitview.Location = New System.Drawing.Point(11, 93)
        Me.grdChannelSplitview.Name = "grdChannelSplitview"
        Me.grdChannelSplitview.RowHeadersVisible = false
        Me.grdChannelSplitview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdChannelSplitview.Size = New System.Drawing.Size(1032, 332)
        Me.grdChannelSplitview.TabIndex = 42
        '
        'frmReadChannelSplit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1083, 480)
        Me.Controls.Add(Me.grdChannelSplitview)
        Me.Controls.Add(Me.cmdSaveAs)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmReadChannelSplit"
        Me.Text = "Read channel split"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        CType(Me.grdChannelSplitview,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents cmbChannel As Windows.Forms.ComboBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents cmbBookingtype As Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents cmdAdd As Windows.Forms.Button
    Friend WithEvents cmdRemove As Windows.Forms.Button
    Friend WithEvents cmdSaveAs As Windows.Forms.Button
    Friend WithEvents grdChannelSplitview As Windows.Forms.DataGridView
End Class
