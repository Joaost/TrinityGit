<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfirm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfirm))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbTarget = New System.Windows.Forms.ComboBox()
        Me.lnkRBS = New System.Windows.Forms.LinkLabel()
        Me.chkRBS = New System.Windows.Forms.CheckBox()
        Me.chkSpecifics = New System.Windows.Forms.CheckBox()
        Me.lnkSpecifics = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtMaxBudget = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.lblBookingType = New System.Windows.Forms.Label()
        Me.cmdSkip = New System.Windows.Forms.Button()
        Me.txtContact = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.imgSpecWarning = New System.Windows.Forms.PictureBox()
        Me.imgRBSWarning = New System.Windows.Forms.PictureBox()
        Me.imgLogo = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbBookingType = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        CType(Me.imgSpecWarning, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgRBSWarning, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 149)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Target"
        '
        'cmbTarget
        '
        Me.cmbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTarget.FormattingEnabled = True
        Me.cmbTarget.Location = New System.Drawing.Point(8, 166)
        Me.cmbTarget.Name = "cmbTarget"
        Me.cmbTarget.Size = New System.Drawing.Size(105, 22)
        Me.cmbTarget.TabIndex = 1
        '
        'lnkRBS
        '
        Me.lnkRBS.AutoSize = True
        Me.lnkRBS.Location = New System.Drawing.Point(31, 240)
        Me.lnkRBS.Name = "lnkRBS"
        Me.lnkRBS.Size = New System.Drawing.Size(28, 14)
        Me.lnkRBS.TabIndex = 2
        Me.lnkRBS.TabStop = True
        Me.lnkRBS.Text = "RBS"
        '
        'chkRBS
        '
        Me.chkRBS.AutoSize = True
        Me.chkRBS.Location = New System.Drawing.Point(13, 241)
        Me.chkRBS.Name = "chkRBS"
        Me.chkRBS.Size = New System.Drawing.Size(15, 14)
        Me.chkRBS.TabIndex = 3
        Me.chkRBS.UseVisualStyleBackColor = True
        '
        'chkSpecifics
        '
        Me.chkSpecifics.AutoSize = True
        Me.chkSpecifics.Location = New System.Drawing.Point(13, 261)
        Me.chkSpecifics.Name = "chkSpecifics"
        Me.chkSpecifics.Size = New System.Drawing.Size(15, 14)
        Me.chkSpecifics.TabIndex = 5
        Me.chkSpecifics.UseVisualStyleBackColor = True
        '
        'lnkSpecifics
        '
        Me.lnkSpecifics.AutoSize = True
        Me.lnkSpecifics.Location = New System.Drawing.Point(31, 260)
        Me.lnkSpecifics.Name = "lnkSpecifics"
        Me.lnkSpecifics.Size = New System.Drawing.Size(52, 14)
        Me.lnkSpecifics.TabIndex = 4
        Me.lnkSpecifics.TabStop = True
        Me.lnkSpecifics.Text = "Specifics"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 191)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 14)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Max budget"
        '
        'txtMaxBudget
        '
        Me.txtMaxBudget.Location = New System.Drawing.Point(8, 208)
        Me.txtMaxBudget.Name = "txtMaxBudget"
        Me.txtMaxBudget.Size = New System.Drawing.Size(105, 20)
        Me.txtMaxBudget.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(117, 211)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(16, 14)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "kr"
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOk.Location = New System.Drawing.Point(195, 291)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(75, 33)
        Me.cmdOk.TabIndex = 10
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(33, 291)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 33)
        Me.cmdCancel.TabIndex = 11
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'lblBookingType
        '
        Me.lblBookingType.AutoSize = True
        Me.lblBookingType.Location = New System.Drawing.Point(69, 29)
        Me.lblBookingType.Name = "lblBookingType"
        Me.lblBookingType.Size = New System.Drawing.Size(39, 14)
        Me.lblBookingType.TabIndex = 12
        Me.lblBookingType.Text = "Label4"
        '
        'cmdSkip
        '
        Me.cmdSkip.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSkip.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdSkip.Location = New System.Drawing.Point(114, 291)
        Me.cmdSkip.Name = "cmdSkip"
        Me.cmdSkip.Size = New System.Drawing.Size(75, 33)
        Me.cmdSkip.TabIndex = 13
        Me.cmdSkip.Text = "Skip"
        Me.cmdSkip.UseVisualStyleBackColor = True
        '
        'txtContact
        '
        Me.txtContact.Location = New System.Drawing.Point(8, 79)
        Me.txtContact.Name = "txtContact"
        Me.txtContact.Size = New System.Drawing.Size(182, 20)
        Me.txtContact.TabIndex = 17
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(122, 14)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Channel contact person"
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Image = Global.TV4Online.My.Resources.Resources.refresh
        Me.cmdRefresh.Location = New System.Drawing.Point(139, 207)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(23, 23)
        Me.cmdRefresh.TabIndex = 18
        Me.cmdRefresh.UseVisualStyleBackColor = True
        Me.cmdRefresh.Visible = False
        '
        'imgSpecWarning
        '
        Me.imgSpecWarning.Image = Global.TV4Online.My.Resources.Resources.warning
        Me.imgSpecWarning.Location = New System.Drawing.Point(88, 258)
        Me.imgSpecWarning.Name = "imgSpecWarning"
        Me.imgSpecWarning.Size = New System.Drawing.Size(16, 16)
        Me.imgSpecWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.imgSpecWarning.TabIndex = 15
        Me.imgSpecWarning.TabStop = False
        Me.imgSpecWarning.Visible = False
        '
        'imgRBSWarning
        '
        Me.imgRBSWarning.Image = Global.TV4Online.My.Resources.Resources.warning
        Me.imgRBSWarning.Location = New System.Drawing.Point(88, 241)
        Me.imgRBSWarning.Name = "imgRBSWarning"
        Me.imgRBSWarning.Size = New System.Drawing.Size(16, 16)
        Me.imgRBSWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.imgRBSWarning.TabIndex = 14
        Me.imgRBSWarning.TabStop = False
        Me.imgRBSWarning.Visible = False
        '
        'imgLogo
        '
        Me.imgLogo.Location = New System.Drawing.Point(12, 9)
        Me.imgLogo.Name = "imgLogo"
        Me.imgLogo.Size = New System.Drawing.Size(51, 50)
        Me.imgLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.imgLogo.TabIndex = 9
        Me.imgLogo.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(1825, 228)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 14)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Label5"
        '
        'cmbBookingType
        '
        Me.cmbBookingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBookingType.FormattingEnabled = True
        Me.cmbBookingType.Location = New System.Drawing.Point(8, 123)
        Me.cmbBookingType.Name = "cmbBookingType"
        Me.cmbBookingType.Size = New System.Drawing.Size(105, 22)
        Me.cmbBookingType.TabIndex = 23
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 106)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 14)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "TV4 Bookingtype"
        '
        'frmConfirm
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(282, 336)
        Me.Controls.Add(Me.cmbBookingType)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmdRefresh)
        Me.Controls.Add(Me.txtContact)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.imgSpecWarning)
        Me.Controls.Add(Me.imgRBSWarning)
        Me.Controls.Add(Me.cmdSkip)
        Me.Controls.Add(Me.lblBookingType)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.imgLogo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtMaxBudget)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.chkSpecifics)
        Me.Controls.Add(Me.lnkSpecifics)
        Me.Controls.Add(Me.chkRBS)
        Me.Controls.Add(Me.lnkRBS)
        Me.Controls.Add(Me.cmbTarget)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfirm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Confirm booking - "
        CType(Me.imgSpecWarning, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgRBSWarning, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbTarget As System.Windows.Forms.ComboBox
    Friend WithEvents lnkRBS As System.Windows.Forms.LinkLabel
    Friend WithEvents chkRBS As System.Windows.Forms.CheckBox
    Friend WithEvents chkSpecifics As System.Windows.Forms.CheckBox
    Friend WithEvents lnkSpecifics As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMaxBudget As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents imgLogo As System.Windows.Forms.PictureBox
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents lblBookingType As System.Windows.Forms.Label
    Friend WithEvents cmdSkip As System.Windows.Forms.Button
    Friend WithEvents imgRBSWarning As System.Windows.Forms.PictureBox
    Friend WithEvents imgSpecWarning As System.Windows.Forms.PictureBox
    Friend WithEvents txtContact As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbBookingType As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
