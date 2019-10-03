<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExportCampaignUnicornNorway
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExportCampaignUnicornNorway))
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.chkBundleTVN = New System.Windows.Forms.CheckBox()
        Me.chkBundleTV2 = New System.Windows.Forms.CheckBox()
        Me.lblSelectDeselect = New System.Windows.Forms.Label()
        Me.chkPrintExportAsCampaign = New System.Windows.Forms.CheckBox()
        Me.chkBundleViasat = New System.Windows.Forms.CheckBox()
        Me.chkOwnCommission = New System.Windows.Forms.CheckBox()
        Me.cmbCampaignType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbCampaignType2 = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExport.FlatAppearance.BorderSize = 0
        Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExport.Location = New System.Drawing.Point(255, 174)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(98, 35)
        Me.btnExport.TabIndex = 0
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(377, 174)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(103, 35)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'chkBundleTVN
        '
        Me.chkBundleTVN.AutoSize = True
        Me.chkBundleTVN.Checked = True
        Me.chkBundleTVN.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBundleTVN.Location = New System.Drawing.Point(12, 118)
        Me.chkBundleTVN.Name = "chkBundleTVN"
        Me.chkBundleTVN.Size = New System.Drawing.Size(90, 17)
        Me.chkBundleTVN.TabIndex = 4
        Me.chkBundleTVN.Text = "Bundle DNN"
        Me.chkBundleTVN.UseVisualStyleBackColor = True
        '
        'chkBundleTV2
        '
        Me.chkBundleTV2.AutoSize = True
        Me.chkBundleTV2.Checked = True
        Me.chkBundleTV2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBundleTV2.Location = New System.Drawing.Point(104, 118)
        Me.chkBundleTV2.Name = "chkBundleTV2"
        Me.chkBundleTV2.Size = New System.Drawing.Size(84, 17)
        Me.chkBundleTV2.TabIndex = 5
        Me.chkBundleTV2.Text = "Bundle TV2"
        Me.chkBundleTV2.UseVisualStyleBackColor = True
        '
        'lblSelectDeselect
        '
        Me.lblSelectDeselect.AutoSize = True
        Me.lblSelectDeselect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectDeselect.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblSelectDeselect.Location = New System.Drawing.Point(12, 24)
        Me.lblSelectDeselect.Name = "lblSelectDeselect"
        Me.lblSelectDeselect.Size = New System.Drawing.Size(138, 13)
        Me.lblSelectDeselect.TabIndex = 11
        Me.lblSelectDeselect.Text = "Select or deselect channels"
        '
        'chkPrintExportAsCampaign
        '
        Me.chkPrintExportAsCampaign.AutoSize = True
        Me.chkPrintExportAsCampaign.Location = New System.Drawing.Point(12, 53)
        Me.chkPrintExportAsCampaign.Name = "chkPrintExportAsCampaign"
        Me.chkPrintExportAsCampaign.Size = New System.Drawing.Size(153, 17)
        Me.chkPrintExportAsCampaign.TabIndex = 13
        Me.chkPrintExportAsCampaign.Text = "Print export as campaign"
        Me.chkPrintExportAsCampaign.UseVisualStyleBackColor = True
        '
        'chkBundleViasat
        '
        Me.chkBundleViasat.AutoSize = True
        Me.chkBundleViasat.Checked = True
        Me.chkBundleViasat.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBundleViasat.Location = New System.Drawing.Point(194, 118)
        Me.chkBundleViasat.Name = "chkBundleViasat"
        Me.chkBundleViasat.Size = New System.Drawing.Size(93, 17)
        Me.chkBundleViasat.TabIndex = 15
        Me.chkBundleViasat.Text = "Bundle NENT"
        Me.chkBundleViasat.UseVisualStyleBackColor = True
        '
        'chkOwnCommission
        '
        Me.chkOwnCommission.AutoSize = True
        Me.chkOwnCommission.Location = New System.Drawing.Point(12, 153)
        Me.chkOwnCommission.Name = "chkOwnCommission"
        Me.chkOwnCommission.Size = New System.Drawing.Size(126, 17)
        Me.chkOwnCommission.TabIndex = 12
        Me.chkOwnCommission.Text = "Use 6% commission"
        Me.chkOwnCommission.UseVisualStyleBackColor = True
        Me.chkOwnCommission.Visible = False
        '
        'cmbCampaignType
        '
        Me.cmbCampaignType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCampaignType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCampaignType.FormattingEnabled = True
        Me.cmbCampaignType.Items.AddRange(New Object() {"TV Campaign", "Spons", "Radio"})
        Me.cmbCampaignType.Location = New System.Drawing.Point(98, 77)
        Me.cmbCampaignType.Name = "cmbCampaignType"
        Me.cmbCampaignType.Size = New System.Drawing.Size(121, 21)
        Me.cmbCampaignType.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Campaign type:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(243, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Campaign type 2:"
        '
        'cmbCampaignType2
        '
        Me.cmbCampaignType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCampaignType2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCampaignType2.FormattingEnabled = True
        Me.cmbCampaignType2.Items.AddRange(New Object() {"Adults", "Kids"})
        Me.cmbCampaignType2.Location = New System.Drawing.Point(338, 77)
        Me.cmbCampaignType2.Name = "cmbCampaignType2"
        Me.cmbCampaignType2.Size = New System.Drawing.Size(121, 21)
        Me.cmbCampaignType2.TabIndex = 18
        '
        'frmExportCampaignUnicornNorway
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 221)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbCampaignType2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbCampaignType)
        Me.Controls.Add(Me.chkBundleViasat)
        Me.Controls.Add(Me.chkPrintExportAsCampaign)
        Me.Controls.Add(Me.chkOwnCommission)
        Me.Controls.Add(Me.lblSelectDeselect)
        Me.Controls.Add(Me.chkBundleTV2)
        Me.Controls.Add(Me.chkBundleTVN)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnExport)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmExportCampaignUnicornNorway"
        Me.Text = "Unicorn export"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents chkBundleTVN As System.Windows.Forms.CheckBox
    Friend WithEvents chkBundleTV2 As System.Windows.Forms.CheckBox
    Friend WithEvents lblSelectDeselect As System.Windows.Forms.Label
    Friend WithEvents chkPrintExportAsCampaign As System.Windows.Forms.CheckBox
    'Friend WithEvents chkBundleNatGeo As System.Windows.Forms.CheckBox
    Friend WithEvents chkBundleViasat As System.Windows.Forms.CheckBox
    Friend WithEvents chkOwnCommission As System.Windows.Forms.CheckBox
    Friend WithEvents cmbCampaignType As Windows.Forms.ComboBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents cmbCampaignType2 As Windows.Forms.ComboBox
End Class
