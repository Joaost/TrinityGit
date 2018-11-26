<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExportSpotlist
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExportSpotlist))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tvwChosen = New System.Windows.Forms.TreeView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tvwAvailable = New System.Windows.Forms.TreeView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lstInfo = New System.Windows.Forms.CheckedListBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkSheetPerChannel = New System.Windows.Forms.CheckBox()
        Me.chkSumDaypart = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkCapOnFirst = New System.Windows.Forms.RadioButton()
        Me.chkCapital = New System.Windows.Forms.RadioButton()
        Me.chkDontChangeCap = New System.Windows.Forms.RadioButton()
        Me.chkColorCode = New System.Windows.Forms.RadioButton()
        Me.chkColorCodeFilm = New System.Windows.Forms.RadioButton()
        Me.chkNone = New System.Windows.Forms.RadioButton()
        Me.chkConvertTime = New System.Windows.Forms.CheckBox()
        Me.chkPreliminary = New System.Windows.Forms.CheckBox()
        Me.chkWeekSum = New System.Windows.Forms.CheckBox()
        Me.cmbLogo = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbHeadline = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbColorScheme = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.chkDefault = New System.Windows.Forms.CheckBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.cmdLanguage = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout
        Me.GroupBox2.SuspendLayout
        Me.GroupBox3.SuspendLayout
        Me.Panel1.SuspendLayout
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.tvwChosen)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.tvwAvailable)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(352, 210)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Columns"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(178, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Chosen"
        '
        'tvwChosen
        '
        Me.tvwChosen.AllowDrop = true
        Me.tvwChosen.Location = New System.Drawing.Point(181, 32)
        Me.tvwChosen.Name = "tvwChosen"
        Me.tvwChosen.ShowPlusMinus = false
        Me.tvwChosen.ShowRootLines = false
        Me.tvwChosen.Size = New System.Drawing.Size(160, 169)
        Me.tvwChosen.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(6, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Available"
        '
        'tvwAvailable
        '
        Me.tvwAvailable.AllowDrop = true
        Me.tvwAvailable.Location = New System.Drawing.Point(6, 32)
        Me.tvwAvailable.Name = "tvwAvailable"
        Me.tvwAvailable.ShowPlusMinus = false
        Me.tvwAvailable.ShowRootLines = false
        Me.tvwAvailable.Size = New System.Drawing.Size(160, 169)
        Me.tvwAvailable.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(12, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Language:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lstInfo)
        Me.GroupBox2.Location = New System.Drawing.Point(373, 55)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(176, 210)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = false
        Me.GroupBox2.Text = "Campaign Info"
        '
        'lstInfo
        '
        Me.lstInfo.FormattingEnabled = true
        Me.lstInfo.Items.AddRange(New Object() {"Client", "Product", "Period", "Buyer", "E-mail", "Phone Nr", "Spot count"})
        Me.lstInfo.Location = New System.Drawing.Point(9, 28)
        Me.lstInfo.Name = "lstInfo"
        Me.lstInfo.Size = New System.Drawing.Size(146, 157)
        Me.lstInfo.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkSheetPerChannel)
        Me.GroupBox3.Controls.Add(Me.chkSumDaypart)
        Me.GroupBox3.Controls.Add(Me.Panel1)
        Me.GroupBox3.Controls.Add(Me.chkColorCode)
        Me.GroupBox3.Controls.Add(Me.chkColorCodeFilm)
        Me.GroupBox3.Controls.Add(Me.chkNone)
        Me.GroupBox3.Controls.Add(Me.chkConvertTime)
        Me.GroupBox3.Controls.Add(Me.chkPreliminary)
        Me.GroupBox3.Controls.Add(Me.chkWeekSum)
        Me.GroupBox3.Controls.Add(Me.cmbLogo)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.cmbHeadline)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.cmbColorScheme)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 270)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(537, 164)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = false
        Me.GroupBox3.Text = "Other"
        '
        'chkSheetPerChannel
        '
        Me.chkSheetPerChannel.AutoSize = true
        Me.chkSheetPerChannel.Location = New System.Drawing.Point(181, 69)
        Me.chkSheetPerChannel.Name = "chkSheetPerChannel"
        Me.chkSheetPerChannel.Size = New System.Drawing.Size(143, 17)
        Me.chkSheetPerChannel.TabIndex = 18
        Me.chkSheetPerChannel.Text = "One sheet per channel"
        Me.chkSheetPerChannel.UseVisualStyleBackColor = true
        '
        'chkSumDaypart
        '
        Me.chkSumDaypart.AutoSize = true
        Me.chkSumDaypart.Location = New System.Drawing.Point(370, 31)
        Me.chkSumDaypart.Name = "chkSumDaypart"
        Me.chkSumDaypart.Size = New System.Drawing.Size(96, 17)
        Me.chkSumDaypart.TabIndex = 11
        Me.chkSumDaypart.Text = "Sum Dayparts"
        Me.chkSumDaypart.UseVisualStyleBackColor = true
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkCapOnFirst)
        Me.Panel1.Controls.Add(Me.chkCapital)
        Me.Panel1.Controls.Add(Me.chkDontChangeCap)
        Me.Panel1.Location = New System.Drawing.Point(181, 87)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(171, 64)
        Me.Panel1.TabIndex = 17
        '
        'chkCapOnFirst
        '
        Me.chkCapOnFirst.AutoSize = true
        Me.chkCapOnFirst.Location = New System.Drawing.Point(12, 25)
        Me.chkCapOnFirst.Name = "chkCapOnFirst"
        Me.chkCapOnFirst.Size = New System.Drawing.Size(115, 17)
        Me.chkCapOnFirst.TabIndex = 11
        Me.chkCapOnFirst.Text = "Cap on first letter"
        Me.chkCapOnFirst.UseVisualStyleBackColor = true
        '
        'chkCapital
        '
        Me.chkCapital.AutoSize = true
        Me.chkCapital.Checked = true
        Me.chkCapital.Location = New System.Drawing.Point(12, 7)
        Me.chkCapital.Name = "chkCapital"
        Me.chkCapital.Size = New System.Drawing.Size(148, 17)
        Me.chkCapital.TabIndex = 10
        Me.chkCapital.TabStop = true
        Me.chkCapital.Text = "All caps on programmes"
        Me.chkCapital.UseVisualStyleBackColor = true
        '
        'chkDontChangeCap
        '
        Me.chkDontChangeCap.AutoSize = true
        Me.chkDontChangeCap.Location = New System.Drawing.Point(12, 42)
        Me.chkDontChangeCap.Name = "chkDontChangeCap"
        Me.chkDontChangeCap.Size = New System.Drawing.Size(144, 17)
        Me.chkDontChangeCap.TabIndex = 12
        Me.chkDontChangeCap.Text = "Do not change caption"
        Me.chkDontChangeCap.UseVisualStyleBackColor = true
        '
        'chkColorCode
        '
        Me.chkColorCode.AutoSize = true
        Me.chkColorCode.Location = New System.Drawing.Point(370, 133)
        Me.chkColorCode.Name = "chkColorCode"
        Me.chkColorCode.Size = New System.Drawing.Size(134, 17)
        Me.chkColorCode.TabIndex = 16
        Me.chkColorCode.Text = "Color Code Channels"
        Me.chkColorCode.UseVisualStyleBackColor = true
        '
        'chkColorCodeFilm
        '
        Me.chkColorCodeFilm.AutoSize = true
        Me.chkColorCodeFilm.Location = New System.Drawing.Point(370, 115)
        Me.chkColorCodeFilm.Name = "chkColorCodeFilm"
        Me.chkColorCodeFilm.Size = New System.Drawing.Size(112, 17)
        Me.chkColorCodeFilm.TabIndex = 15
        Me.chkColorCodeFilm.Text = "Color Code Films"
        Me.chkColorCodeFilm.UseVisualStyleBackColor = true
        '
        'chkNone
        '
        Me.chkNone.AutoSize = true
        Me.chkNone.Checked = true
        Me.chkNone.Location = New System.Drawing.Point(370, 97)
        Me.chkNone.Name = "chkNone"
        Me.chkNone.Size = New System.Drawing.Size(110, 17)
        Me.chkNone.TabIndex = 14
        Me.chkNone.TabStop = true
        Me.chkNone.Text = "No Color coding"
        Me.chkNone.UseVisualStyleBackColor = true
        '
        'chkConvertTime
        '
        Me.chkConvertTime.AutoSize = true
        Me.chkConvertTime.Location = New System.Drawing.Point(181, 50)
        Me.chkConvertTime.Name = "chkConvertTime"
        Me.chkConvertTime.Size = New System.Drawing.Size(157, 17)
        Me.chkConvertTime.TabIndex = 9
        Me.chkConvertTime.Text = "Convert times to real time"
        Me.chkConvertTime.UseVisualStyleBackColor = true
        '
        'chkPreliminary
        '
        Me.chkPreliminary.AutoSize = true
        Me.chkPreliminary.Location = New System.Drawing.Point(181, 31)
        Me.chkPreliminary.Name = "chkPreliminary"
        Me.chkPreliminary.Size = New System.Drawing.Size(82, 17)
        Me.chkPreliminary.TabIndex = 7
        Me.chkPreliminary.Text = "Preliminary"
        Me.chkPreliminary.UseVisualStyleBackColor = true
        '
        'chkWeekSum
        '
        Me.chkWeekSum.AutoSize = true
        Me.chkWeekSum.Location = New System.Drawing.Point(370, 49)
        Me.chkWeekSum.Name = "chkWeekSum"
        Me.chkWeekSum.Size = New System.Drawing.Size(85, 17)
        Me.chkWeekSum.TabIndex = 6
        Me.chkWeekSum.Text = "Sum Weeks"
        Me.chkWeekSum.UseVisualStyleBackColor = true
        '
        'cmbLogo
        '
        Me.cmbLogo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLogo.FormattingEnabled = true
        Me.cmbLogo.Location = New System.Drawing.Point(6, 109)
        Me.cmbLogo.Name = "cmbLogo"
        Me.cmbLogo.Size = New System.Drawing.Size(160, 21)
        Me.cmbLogo.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Location = New System.Drawing.Point(6, 93)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Logotype"
        '
        'cmbHeadline
        '
        Me.cmbHeadline.FormattingEnabled = true
        Me.cmbHeadline.Location = New System.Drawing.Point(6, 70)
        Me.cmbHeadline.Name = "cmbHeadline"
        Me.cmbHeadline.Size = New System.Drawing.Size(160, 21)
        Me.cmbHeadline.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(6, 54)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Headline"
        '
        'cmbColorScheme
        '
        Me.cmbColorScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbColorScheme.FormattingEnabled = true
        Me.cmbColorScheme.Location = New System.Drawing.Point(6, 31)
        Me.cmbColorScheme.Name = "cmbColorScheme"
        Me.cmbColorScheme.Size = New System.Drawing.Size(160, 21)
        Me.cmbColorScheme.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(6, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Color scheme"
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdOk.FlatAppearance.BorderSize = 0
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Location = New System.Drawing.Point(473, 433)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(73, 20)
        Me.cmdOk.TabIndex = 7
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Location = New System.Drawing.Point(392, 433)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 20)
        Me.cmdCancel.TabIndex = 8
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = true
        '
        'chkDefault
        '
        Me.chkDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chkDefault.Location = New System.Drawing.Point(190, 427)
        Me.chkDefault.Name = "chkDefault"
        Me.chkDefault.Size = New System.Drawing.Size(174, 34)
        Me.chkDefault.TabIndex = 9
        Me.chkDefault.Text = "Save setup as default"
        Me.chkDefault.UseVisualStyleBackColor = true
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.excel_2
        Me.PictureBox1.Location = New System.Drawing.Point(522, 11)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = false
        '
        'cmdLanguage
        '
        Me.cmdLanguage.FlatAppearance.BorderSize = 0
        Me.cmdLanguage.Image = CType(resources.GetObject("cmdLanguage.Image"),System.Drawing.Image)
        Me.cmdLanguage.Location = New System.Drawing.Point(76, 11)
        Me.cmdLanguage.Name = "cmdLanguage"
        Me.cmdLanguage.Size = New System.Drawing.Size(43, 38)
        Me.cmdLanguage.TabIndex = 4
        Me.cmdLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdLanguage.UseVisualStyleBackColor = true
        '
        'frmExportSpotlist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(567, 461)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.chkDefault)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cmdLanguage)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmExportSpotlist"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Export Spotlist"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        Me.Panel1.ResumeLayout(false)
        Me.Panel1.PerformLayout
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tvwChosen As System.Windows.Forms.TreeView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tvwAvailable As System.Windows.Forms.TreeView
    Friend WithEvents cmdLanguage As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lstInfo As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkConvertTime As System.Windows.Forms.CheckBox
    Friend WithEvents chkPreliminary As System.Windows.Forms.CheckBox
    Friend WithEvents chkWeekSum As System.Windows.Forms.CheckBox
    Friend WithEvents cmbLogo As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbHeadline As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbColorScheme As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents chkDefault As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chkDontChangeCap As System.Windows.Forms.RadioButton
    Friend WithEvents chkCapOnFirst As System.Windows.Forms.RadioButton
    Friend WithEvents chkCapital As System.Windows.Forms.RadioButton
    Friend WithEvents chkColorCode As System.Windows.Forms.RadioButton
    Friend WithEvents chkColorCodeFilm As System.Windows.Forms.RadioButton
    Friend WithEvents chkNone As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkSumDaypart As System.Windows.Forms.CheckBox
    Friend WithEvents chkSheetPerChannel As System.Windows.Forms.CheckBox
End Class
