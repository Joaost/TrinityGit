<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPostCampaignAnalysis
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPostCampaignAnalysis))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.chkHide = New System.Windows.Forms.CheckBox()
        Me.cmbTemplate = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkInclude = New System.Windows.Forms.CheckBox()
        Me.chkAdvanced = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdDeleteCompetitor = New System.Windows.Forms.Button()
        Me.cmdAddCompetitor = New System.Windows.Forms.Button()
        Me.grdChosen = New System.Windows.Forms.DataGridView()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.grdProducts = New System.Windows.Forms.DataGridView()
        Me.cmdFind = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dlgOpen = New System.Windows.Forms.OpenFileDialog()
        Me.pbStatus = New System.Windows.Forms.ProgressBar()
        Me.chkPrintCombinations = New System.Windows.Forms.CheckBox()
        Me.cmbHistoric = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbTimeshift = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkPlannedNet = New System.Windows.Forms.CheckBox()
        Me.chkConfirmedNet = New System.Windows.Forms.CheckBox()
        Me.chkPlannedGrossConfirmedNet = New System.Windows.Forms.CheckBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colProduct = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRating = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dtTo = New clTrinity.ExtendedDateTimePicker()
        Me.dtFrom = New clTrinity.ExtendedDateTimePicker()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdChosen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(149, 365)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 31)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.FlatAppearance.BorderSize = 0
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 25)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.FlatAppearance.BorderSize = 0
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 25)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'chkHide
        '
        Me.chkHide.AutoSize = True
        Me.chkHide.Checked = True
        Me.chkHide.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkHide.Location = New System.Drawing.Point(15, 209)
        Me.chkHide.Name = "chkHide"
        Me.chkHide.Size = New System.Drawing.Size(112, 17)
        Me.chkHide.TabIndex = 15
        Me.chkHide.Text = "Hide data sheets"
        Me.chkHide.UseVisualStyleBackColor = True
        '
        'cmbTemplate
        '
        Me.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplate.FormattingEnabled = True
        Me.cmbTemplate.Location = New System.Drawing.Point(12, 69)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Size = New System.Drawing.Size(275, 21)
        Me.cmbTemplate.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Template"
        '
        'chkInclude
        '
        Me.chkInclude.AutoSize = True
        Me.chkInclude.Checked = True
        Me.chkInclude.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkInclude.Location = New System.Drawing.Point(15, 234)
        Me.chkInclude.Name = "chkInclude"
        Me.chkInclude.Size = New System.Drawing.Size(110, 17)
        Me.chkInclude.TabIndex = 16
        Me.chkInclude.Text = "Include planned"
        Me.chkInclude.UseVisualStyleBackColor = True
        '
        'chkAdvanced
        '
        Me.chkAdvanced.AutoSize = True
        Me.chkAdvanced.Location = New System.Drawing.Point(15, 258)
        Me.chkAdvanced.Name = "chkAdvanced"
        Me.chkAdvanced.Size = New System.Drawing.Size(76, 17)
        Me.chkAdvanced.TabIndex = 17
        Me.chkAdvanced.Text = "Advanced"
        Me.chkAdvanced.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cmdDeleteCompetitor)
        Me.GroupBox1.Controls.Add(Me.cmdAddCompetitor)
        Me.GroupBox1.Controls.Add(Me.grdChosen)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.grdProducts)
        Me.GroupBox1.Controls.Add(Me.cmdFind)
        Me.GroupBox1.Controls.Add(Me.dtTo)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.dtFrom)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 455)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 328)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Competition"
        '
        'cmdDeleteCompetitor
        '
        Me.cmdDeleteCompetitor.Image = CType(resources.GetObject("cmdDeleteCompetitor.Image"), System.Drawing.Image)
        Me.cmdDeleteCompetitor.Location = New System.Drawing.Point(232, 232)
        Me.cmdDeleteCompetitor.Name = "cmdDeleteCompetitor"
        Me.cmdDeleteCompetitor.Size = New System.Drawing.Size(22, 22)
        Me.cmdDeleteCompetitor.TabIndex = 14
        Me.cmdDeleteCompetitor.UseVisualStyleBackColor = True
        '
        'cmdAddCompetitor
        '
        Me.cmdAddCompetitor.Image = CType(resources.GetObject("cmdAddCompetitor.Image"), System.Drawing.Image)
        Me.cmdAddCompetitor.Location = New System.Drawing.Point(232, 204)
        Me.cmdAddCompetitor.Name = "cmdAddCompetitor"
        Me.cmdAddCompetitor.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddCompetitor.TabIndex = 13
        Me.cmdAddCompetitor.UseVisualStyleBackColor = True
        '
        'grdChosen
        '
        Me.grdChosen.AllowUserToAddRows = False
        Me.grdChosen.AllowUserToDeleteRows = False
        Me.grdChosen.AllowUserToResizeColumns = False
        Me.grdChosen.AllowUserToResizeRows = False
        Me.grdChosen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChosen.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn4})
        Me.grdChosen.Location = New System.Drawing.Point(260, 145)
        Me.grdChosen.Name = "grdChosen"
        Me.grdChosen.ReadOnly = True
        Me.grdChosen.RowHeadersVisible = False
        Me.grdChosen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdChosen.Size = New System.Drawing.Size(144, 138)
        Me.grdChosen.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 122)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Filter:"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(47, 119)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(179, 22)
        Me.txtSearch.TabIndex = 7
        '
        'grdProducts
        '
        Me.grdProducts.AllowUserToAddRows = False
        Me.grdProducts.AllowUserToDeleteRows = False
        Me.grdProducts.AllowUserToResizeColumns = False
        Me.grdProducts.AllowUserToResizeRows = False
        Me.grdProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdProducts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colProduct, Me.colRating})
        Me.grdProducts.Location = New System.Drawing.Point(9, 145)
        Me.grdProducts.Name = "grdProducts"
        Me.grdProducts.ReadOnly = True
        Me.grdProducts.RowHeadersVisible = False
        Me.grdProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdProducts.Size = New System.Drawing.Size(217, 176)
        Me.grdProducts.TabIndex = 6
        '
        'cmdFind
        '
        Me.cmdFind.Location = New System.Drawing.Point(9, 59)
        Me.cmdFind.Name = "cmdFind"
        Me.cmdFind.Size = New System.Drawing.Size(75, 23)
        Me.cmdFind.TabIndex = 5
        Me.cmdFind.Text = "Find"
        Me.cmdFind.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(102, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "and"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(146, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Find competition between:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(250, 31)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Please specify up to 5 products to be considered as the main competitors in this " &
    "campaign" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'dlgOpen
        '
        Me.dlgOpen.FileName = "*.xls"
        Me.dlgOpen.Filter = "Excel Workbooks|*.xls|Excel 2007 Workbooks|*.xlsx |Excel 2007 Templates|*.xltx"
        '
        'pbStatus
        '
        Me.pbStatus.Location = New System.Drawing.Point(67, 17)
        Me.pbStatus.Name = "pbStatus"
        Me.pbStatus.Size = New System.Drawing.Size(220, 22)
        Me.pbStatus.TabIndex = 19
        Me.pbStatus.Visible = False
        '
        'chkPrintCombinations
        '
        Me.chkPrintCombinations.AutoSize = True
        Me.chkPrintCombinations.Location = New System.Drawing.Point(15, 183)
        Me.chkPrintCombinations.Name = "chkPrintCombinations"
        Me.chkPrintCombinations.Size = New System.Drawing.Size(227, 17)
        Me.chkPrintCombinations.TabIndex = 20
        Me.chkPrintCombinations.Text = "Add combinations with joint allocation"
        Me.chkPrintCombinations.UseVisualStyleBackColor = True
        '
        'cmbHistoric
        '
        Me.cmbHistoric.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHistoric.Enabled = False
        Me.cmbHistoric.FormattingEnabled = True
        Me.cmbHistoric.Location = New System.Drawing.Point(12, 112)
        Me.cmbHistoric.Name = "cmbHistoric"
        Me.cmbHistoric.Size = New System.Drawing.Size(275, 21)
        Me.cmbHistoric.TabIndex = 22
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 94)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(139, 13)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Include historic campaign"
        '
        'cmbTimeshift
        '
        Me.cmbTimeshift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTimeshift.FormattingEnabled = True
        Me.cmbTimeshift.Items.AddRange(New Object() {"Default", "Live", "VOSDAL+7"})
        Me.cmbTimeshift.Location = New System.Drawing.Point(12, 155)
        Me.cmbTimeshift.Name = "cmbTimeshift"
        Me.cmbTimeshift.Size = New System.Drawing.Size(275, 21)
        Me.cmbTimeshift.TabIndex = 24
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 137)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "Time shift"
        '
        'chkPlannedNet
        '
        Me.chkPlannedNet.AutoSize = True
        Me.chkPlannedNet.Checked = True
        Me.chkPlannedNet.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPlannedNet.Location = New System.Drawing.Point(15, 280)
        Me.chkPlannedNet.Name = "chkPlannedNet"
        Me.chkPlannedNet.Size = New System.Drawing.Size(88, 17)
        Me.chkPlannedNet.TabIndex = 25
        Me.chkPlannedNet.Text = "Planned net"
        Me.chkPlannedNet.UseVisualStyleBackColor = True
        '
        'chkConfirmedNet
        '
        Me.chkConfirmedNet.AutoSize = True
        Me.chkConfirmedNet.Location = New System.Drawing.Point(15, 303)
        Me.chkConfirmedNet.Name = "chkConfirmedNet"
        Me.chkConfirmedNet.Size = New System.Drawing.Size(100, 17)
        Me.chkConfirmedNet.TabIndex = 26
        Me.chkConfirmedNet.Text = "Confirmed net"
        Me.chkConfirmedNet.UseVisualStyleBackColor = True
        '
        'chkPlannedGrossConfirmedNet
        '
        Me.chkPlannedGrossConfirmedNet.AutoSize = True
        Me.chkPlannedGrossConfirmedNet.Location = New System.Drawing.Point(15, 326)
        Me.chkPlannedGrossConfirmedNet.Name = "chkPlannedGrossConfirmedNet"
        Me.chkPlannedGrossConfirmedNet.Size = New System.Drawing.Size(177, 17)
        Me.chkPlannedGrossConfirmedNet.TabIndex = 27
        Me.chkPlannedGrossConfirmedNet.Text = "Planned gross, confirmed net"
        Me.chkPlannedGrossConfirmedNet.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.analysis_2_30x30
        Me.PictureBox1.Location = New System.Drawing.Point(12, 13)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 12
        Me.PictureBox1.TabStop = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = "Advertiser"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 115
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.Frozen = True
        Me.DataGridViewTextBoxColumn2.HeaderText = "Product"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 115
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.Frozen = True
        Me.DataGridViewTextBoxColumn3.HeaderText = "Rating"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 50
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.Frozen = True
        Me.DataGridViewTextBoxColumn4.HeaderText = "Product"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 140
        '
        'colProduct
        '
        Me.colProduct.Frozen = True
        Me.colProduct.HeaderText = "Product"
        Me.colProduct.Name = "colProduct"
        Me.colProduct.ReadOnly = True
        Me.colProduct.Width = 140
        '
        'colRating
        '
        Me.colRating.Frozen = True
        Me.colRating.HeaderText = "Rating"
        Me.colRating.Name = "colRating"
        Me.colRating.ReadOnly = True
        Me.colRating.Width = 50
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(133, 33)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.ShowWeekNumbers = True
        Me.dtTo.Size = New System.Drawing.Size(87, 22)
        Me.dtTo.TabIndex = 4
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(9, 33)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.ShowWeekNumbers = True
        Me.dtFrom.Size = New System.Drawing.Size(87, 22)
        Me.dtFrom.TabIndex = 2
        '
        'frmPostCampaignAnalysis
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(304, 403)
        Me.Controls.Add(Me.chkPlannedGrossConfirmedNet)
        Me.Controls.Add(Me.chkConfirmedNet)
        Me.Controls.Add(Me.chkPlannedNet)
        Me.Controls.Add(Me.cmbTimeshift)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmbHistoric)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.chkPrintCombinations)
        Me.Controls.Add(Me.pbStatus)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.chkAdvanced)
        Me.Controls.Add(Me.chkInclude)
        Me.Controls.Add(Me.chkHide)
        Me.Controls.Add(Me.cmbTemplate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmPostCampaignAnalysis"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Create Post-campaign analysis"
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        CType(Me.grdChosen,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdProducts,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents chkHide As System.Windows.Forms.CheckBox
    Friend WithEvents cmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chkInclude As System.Windows.Forms.CheckBox
    Friend WithEvents chkAdvanced As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtFrom As clTrinity.ExtendedDateTimePicker
    Friend WithEvents grdProducts As System.Windows.Forms.DataGridView
    Friend WithEvents cmdFind As System.Windows.Forms.Button
    Friend WithEvents dtTo As clTrinity.ExtendedDateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdChosen As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProduct As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRating As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdDeleteCompetitor As System.Windows.Forms.Button
    Friend WithEvents cmdAddCompetitor As System.Windows.Forms.Button
    Friend WithEvents dlgOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents pbStatus As System.Windows.Forms.ProgressBar
    Friend WithEvents chkPrintCombinations As System.Windows.Forms.CheckBox

    Private Sub dlgOpen_FileOk(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dlgOpen.FileOk

    End Sub
    Friend WithEvents cmbHistoric As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbTimeshift As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkPlannedNet As System.Windows.Forms.CheckBox
    Friend WithEvents chkConfirmedNet As System.Windows.Forms.CheckBox
    Friend WithEvents chkPlannedGrossConfirmedNet As Windows.Forms.CheckBox
End Class
