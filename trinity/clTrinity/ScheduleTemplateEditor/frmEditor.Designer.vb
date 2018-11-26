<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditor))
        Me.imlIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.cmdLoad = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.spcTemplates = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.tabTemplate = New System.Windows.Forms.TabControl()
        Me.tpIdentify = New System.Windows.Forms.TabPage()
        Me.tvwIdentify = New System.Windows.Forms.TreeView()
        Me.cmdAddIdentifyRule = New System.Windows.Forms.Button()
        Me.cmdAddIdentifyRuleGroup = New System.Windows.Forms.Button()
        Me.cmdRemoveIdentifyRule = New System.Windows.Forms.Button()
        Me.grpIdentifyRule = New System.Windows.Forms.GroupBox()
        Me.cmdGetIdentify = New System.Windows.Forms.Button()
        Me.txtIdentifySearchValue = New System.Windows.Forms.TextBox()
        Me.cmbIdentifySearchType = New System.Windows.Forms.ComboBox()
        Me.cmbIdentifyRule = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtIdentifyFindFromCol = New System.Windows.Forms.TextBox()
        Me.txtIdentifyFindFromRow = New System.Windows.Forms.TextBox()
        Me.txtIdentifyFindToCol = New System.Windows.Forms.TextBox()
        Me.lblIdentifyRowTo = New System.Windows.Forms.Label()
        Me.lblIdentifyColumnTo = New System.Windows.Forms.Label()
        Me.txtIdentifyFindToRow = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdIdentifyUp = New System.Windows.Forms.Button()
        Me.cmdIdentifyDown = New System.Windows.Forms.Button()
        Me.tpTarget = New System.Windows.Forms.TabPage()
        Me.txtTargetFallback = New System.Windows.Forms.TextBox()
        Me.chkTargetRequired = New System.Windows.Forms.CheckBox()
        Me.lblTargetRequired = New System.Windows.Forms.Label()
        Me.tvwTarget = New System.Windows.Forms.TreeView()
        Me.grpTargetRule = New System.Windows.Forms.GroupBox()
        Me.cmdGetTarget = New System.Windows.Forms.Button()
        Me.txtTargetSearchValue = New System.Windows.Forms.TextBox()
        Me.cmbTargetSearchType = New System.Windows.Forms.ComboBox()
        Me.cmbTargetRule = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTargetFindFromCol = New System.Windows.Forms.TextBox()
        Me.txtTargetFindFromRow = New System.Windows.Forms.TextBox()
        Me.txtTargetFindToCol = New System.Windows.Forms.TextBox()
        Me.lblTargetRowTo = New System.Windows.Forms.Label()
        Me.lblTargetColumnTo = New System.Windows.Forms.Label()
        Me.txtTargetFindToRow = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmdAddTargetRule = New System.Windows.Forms.Button()
        Me.cmdRemoveTargetRule = New System.Windows.Forms.Button()
        Me.cmdTargetUp = New System.Windows.Forms.Button()
        Me.cmdTargetDown = New System.Windows.Forms.Button()
        Me.tpContract = New System.Windows.Forms.TabPage()
        Me.txtContractFallback = New System.Windows.Forms.TextBox()
        Me.lblContractRequired = New System.Windows.Forms.Label()
        Me.tvwContract = New System.Windows.Forms.TreeView()
        Me.chkContractRequired = New System.Windows.Forms.CheckBox()
        Me.grpContractRule = New System.Windows.Forms.GroupBox()
        Me.cmdGetContract = New System.Windows.Forms.Button()
        Me.txtContractSearchValue = New System.Windows.Forms.TextBox()
        Me.cmbContractSearchType = New System.Windows.Forms.ComboBox()
        Me.cmbContractRule = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtContractFindFromCol = New System.Windows.Forms.TextBox()
        Me.txtContractFindFromRow = New System.Windows.Forms.TextBox()
        Me.txtContractFindToCol = New System.Windows.Forms.TextBox()
        Me.lblContractRowTo = New System.Windows.Forms.Label()
        Me.lblContractColumnTo = New System.Windows.Forms.Label()
        Me.txtContractFindToRow = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmdAddContractRule = New System.Windows.Forms.Button()
        Me.cmdRemoveContractRule = New System.Windows.Forms.Button()
        Me.cmdContractUp = New System.Windows.Forms.Button()
        Me.cmdContractDown = New System.Windows.Forms.Button()
        Me.tpChannel = New System.Windows.Forms.TabPage()
        Me.tvwChannel = New System.Windows.Forms.TreeView()
        Me.grpChannelRule = New System.Windows.Forms.GroupBox()
        Me.cmdGetChannel = New System.Windows.Forms.Button()
        Me.txtChannelSearchValue = New System.Windows.Forms.TextBox()
        Me.cmbChannelSearchType = New System.Windows.Forms.ComboBox()
        Me.cmbChannelRule = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtChannelFindFromCol = New System.Windows.Forms.TextBox()
        Me.txtChannelFindFromRow = New System.Windows.Forms.TextBox()
        Me.txtChannelFindToCol = New System.Windows.Forms.TextBox()
        Me.lblChannelRowTo = New System.Windows.Forms.Label()
        Me.lblChannelColumnTo = New System.Windows.Forms.Label()
        Me.txtChannelFindToRow = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmdAddChannelRule = New System.Windows.Forms.Button()
        Me.cmdRemoveChannelRule = New System.Windows.Forms.Button()
        Me.cmdChannelUp = New System.Windows.Forms.Button()
        Me.cmdChannelDown = New System.Windows.Forms.Button()
        Me.tpColumns = New System.Windows.Forms.TabPage()
        Me.lstRequired = New System.Windows.Forms.ListBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.tvwHeadlines = New System.Windows.Forms.TreeView()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tvwColumns = New System.Windows.Forms.TreeView()
        Me.grpColumnsRule = New System.Windows.Forms.GroupBox()
        Me.cmdGetColumns = New System.Windows.Forms.Button()
        Me.txtColumnsSearchValue = New System.Windows.Forms.TextBox()
        Me.cmbColumnsSearchType = New System.Windows.Forms.ComboBox()
        Me.cmbColumnsRule = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtColumnsFindFromCol = New System.Windows.Forms.TextBox()
        Me.txtColumnsFindFromRow = New System.Windows.Forms.TextBox()
        Me.txtColumnsFindToCol = New System.Windows.Forms.TextBox()
        Me.lblColumnsRowTo = New System.Windows.Forms.Label()
        Me.lblColumnsColumnTo = New System.Windows.Forms.Label()
        Me.txtColumnsFindToRow = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cmdUnsetRequiredColumn = New System.Windows.Forms.Button()
        Me.cmdSetRequiredColumn = New System.Windows.Forms.Button()
        Me.cmdAddHeadline = New System.Windows.Forms.Button()
        Me.cmdRemoveHeadline = New System.Windows.Forms.Button()
        Me.cmdAddHeadlineRule = New System.Windows.Forms.Button()
        Me.cmdAddHeadlineRow = New System.Windows.Forms.Button()
        Me.cmdRemoveHeadlineRule = New System.Windows.Forms.Button()
        Me.cmdHeadlineUp = New System.Windows.Forms.Button()
        Me.cmdHeadlineDown = New System.Windows.Forms.Button()
        Me.tpSpotlist = New System.Windows.Forms.TabPage()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.grpSheets = New System.Windows.Forms.GroupBox()
        Me.chkSheets = New System.Windows.Forms.CheckBox()
        Me.cmdRemoveSheet = New System.Windows.Forms.Button()
        Me.cmdAddSheet = New System.Windows.Forms.Button()
        Me.lstSheets = New System.Windows.Forms.ListBox()
        Me.grpSchedule = New System.Windows.Forms.GroupBox()
        Me.chkAutoValidate = New System.Windows.Forms.CheckBox()
        Me.cmdValidate = New System.Windows.Forms.Button()
        Me.cmdLoadSchedule = New System.Windows.Forms.Button()
        Me.grdTemplate = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdBrowse = New System.Windows.Forms.Button()
        Me.txtSchedule = New System.Windows.Forms.TextBox()
        Me.cmbTemplates = New System.Windows.Forms.ComboBox()
        Me.cmdDeleteTemplate = New System.Windows.Forms.Button()
        Me.cmdAddTemplate = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.mnuEdit = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPaste = New System.Windows.Forms.ToolStripMenuItem()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.spcTemplates, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spcTemplates.Panel1.SuspendLayout()
        Me.spcTemplates.Panel2.SuspendLayout()
        Me.spcTemplates.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.tabTemplate.SuspendLayout()
        Me.tpIdentify.SuspendLayout()
        Me.grpIdentifyRule.SuspendLayout()
        Me.tpTarget.SuspendLayout()
        Me.grpTargetRule.SuspendLayout()
        Me.tpContract.SuspendLayout()
        Me.grpContractRule.SuspendLayout()
        Me.tpChannel.SuspendLayout()
        Me.grpChannelRule.SuspendLayout()
        Me.tpColumns.SuspendLayout()
        Me.grpColumnsRule.SuspendLayout()
        Me.tpSpotlist.SuspendLayout()
        Me.grpSheets.SuspendLayout()
        Me.grpSchedule.SuspendLayout()
        CType(Me.grdTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.mnuEdit.SuspendLayout()
        Me.SuspendLayout()
        '
        'imlIcons
        '
        Me.imlIcons.ImageStream = CType(resources.GetObject("imlIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.imlIcons.Images.SetKeyName(0, "ok")
        Me.imlIcons.Images.SetKeyName(1, "failed")
        Me.imlIcons.Images.SetKeyName(2, "unknown")
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1191, 579)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.cmdLoad)
        Me.TabPage1.Controls.Add(Me.cmdSave)
        Me.TabPage1.Controls.Add(Me.spcTemplates)
        Me.TabPage1.Controls.Add(Me.cmbTemplates)
        Me.TabPage1.Controls.Add(Me.cmdDeleteTemplate)
        Me.TabPage1.Controls.Add(Me.cmdAddTemplate)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1183, 552)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Templates"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'cmdLoad
        '
        Me.cmdLoad.Image = Global.ScheduleTemplateEditor.My.Resources.Resources.folder
        Me.cmdLoad.Location = New System.Drawing.Point(495, 7)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(40, 44)
        Me.cmdLoad.TabIndex = 35
        Me.cmdLoad.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Image = Global.ScheduleTemplateEditor.My.Resources.Resources.disk_blue
        Me.cmdSave.Location = New System.Drawing.Point(541, 7)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(40, 44)
        Me.cmdSave.TabIndex = 34
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'spcTemplates
        '
        Me.spcTemplates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spcTemplates.Location = New System.Drawing.Point(6, 35)
        Me.spcTemplates.Name = "spcTemplates"
        '
        'spcTemplates.Panel1
        '
        Me.spcTemplates.Panel1.Controls.Add(Me.Panel1)
        '
        'spcTemplates.Panel2
        '
        Me.spcTemplates.Panel2.Controls.Add(Me.grpSchedule)
        Me.spcTemplates.Size = New System.Drawing.Size(1171, 511)
        Me.spcTemplates.SplitterDistance = 586
        Me.spcTemplates.TabIndex = 30
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.tabTemplate)
        Me.Panel1.Controls.Add(Me.txtName)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.grpSheets)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(586, 511)
        Me.Panel1.TabIndex = 31
        '
        'tabTemplate
        '
        Me.tabTemplate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabTemplate.Controls.Add(Me.tpIdentify)
        Me.tabTemplate.Controls.Add(Me.tpTarget)
        Me.tabTemplate.Controls.Add(Me.tpContract)
        Me.tabTemplate.Controls.Add(Me.tpChannel)
        Me.tabTemplate.Controls.Add(Me.tpColumns)
        Me.tabTemplate.Controls.Add(Me.tpSpotlist)
        Me.tabTemplate.ImageList = Me.imlIcons
        Me.tabTemplate.Location = New System.Drawing.Point(3, 160)
        Me.tabTemplate.Name = "tabTemplate"
        Me.tabTemplate.SelectedIndex = 0
        Me.tabTemplate.Size = New System.Drawing.Size(580, 351)
        Me.tabTemplate.TabIndex = 33
        '
        'tpIdentify
        '
        Me.tpIdentify.Controls.Add(Me.tvwIdentify)
        Me.tpIdentify.Controls.Add(Me.cmdAddIdentifyRule)
        Me.tpIdentify.Controls.Add(Me.cmdAddIdentifyRuleGroup)
        Me.tpIdentify.Controls.Add(Me.cmdRemoveIdentifyRule)
        Me.tpIdentify.Controls.Add(Me.grpIdentifyRule)
        Me.tpIdentify.Controls.Add(Me.cmdIdentifyUp)
        Me.tpIdentify.Controls.Add(Me.cmdIdentifyDown)
        Me.tpIdentify.ImageKey = "failed"
        Me.tpIdentify.Location = New System.Drawing.Point(4, 23)
        Me.tpIdentify.Name = "tpIdentify"
        Me.tpIdentify.Padding = New System.Windows.Forms.Padding(3)
        Me.tpIdentify.Size = New System.Drawing.Size(572, 324)
        Me.tpIdentify.TabIndex = 0
        Me.tpIdentify.Tag = "Identify"
        Me.tpIdentify.Text = "Identify"
        Me.tpIdentify.UseVisualStyleBackColor = True
        '
        'tvwIdentify
        '
        Me.tvwIdentify.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwIdentify.ImageIndex = 2
        Me.tvwIdentify.ImageList = Me.imlIcons
        Me.tvwIdentify.Location = New System.Drawing.Point(2, 6)
        Me.tvwIdentify.Name = "tvwIdentify"
        Me.tvwIdentify.SelectedImageIndex = 0
        Me.tvwIdentify.Size = New System.Drawing.Size(314, 134)
        Me.tvwIdentify.TabIndex = 22
        Me.tvwIdentify.Tag = "Identify"
        '
        'cmdAddIdentifyRule
        '
        Me.cmdAddIdentifyRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddIdentifyRule.Image = CType(resources.GetObject("cmdAddIdentifyRule.Image"), System.Drawing.Image)
        Me.cmdAddIdentifyRule.Location = New System.Drawing.Point(322, 6)
        Me.cmdAddIdentifyRule.Name = "cmdAddIdentifyRule"
        Me.cmdAddIdentifyRule.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddIdentifyRule.TabIndex = 23
        Me.cmdAddIdentifyRule.UseVisualStyleBackColor = True
        '
        'cmdAddIdentifyRuleGroup
        '
        Me.cmdAddIdentifyRuleGroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddIdentifyRuleGroup.Image = CType(resources.GetObject("cmdAddIdentifyRuleGroup.Image"), System.Drawing.Image)
        Me.cmdAddIdentifyRuleGroup.Location = New System.Drawing.Point(322, 34)
        Me.cmdAddIdentifyRuleGroup.Name = "cmdAddIdentifyRuleGroup"
        Me.cmdAddIdentifyRuleGroup.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddIdentifyRuleGroup.TabIndex = 28
        Me.cmdAddIdentifyRuleGroup.UseVisualStyleBackColor = True
        '
        'cmdRemoveIdentifyRule
        '
        Me.cmdRemoveIdentifyRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveIdentifyRule.Image = CType(resources.GetObject("cmdRemoveIdentifyRule.Image"), System.Drawing.Image)
        Me.cmdRemoveIdentifyRule.Location = New System.Drawing.Point(322, 62)
        Me.cmdRemoveIdentifyRule.Name = "cmdRemoveIdentifyRule"
        Me.cmdRemoveIdentifyRule.Size = New System.Drawing.Size(22, 22)
        Me.cmdRemoveIdentifyRule.TabIndex = 24
        Me.cmdRemoveIdentifyRule.UseVisualStyleBackColor = True
        '
        'grpIdentifyRule
        '
        Me.grpIdentifyRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpIdentifyRule.Controls.Add(Me.cmdGetIdentify)
        Me.grpIdentifyRule.Controls.Add(Me.txtIdentifySearchValue)
        Me.grpIdentifyRule.Controls.Add(Me.cmbIdentifySearchType)
        Me.grpIdentifyRule.Controls.Add(Me.cmbIdentifyRule)
        Me.grpIdentifyRule.Controls.Add(Me.Label1)
        Me.grpIdentifyRule.Controls.Add(Me.txtIdentifyFindFromCol)
        Me.grpIdentifyRule.Controls.Add(Me.txtIdentifyFindFromRow)
        Me.grpIdentifyRule.Controls.Add(Me.txtIdentifyFindToCol)
        Me.grpIdentifyRule.Controls.Add(Me.lblIdentifyRowTo)
        Me.grpIdentifyRule.Controls.Add(Me.lblIdentifyColumnTo)
        Me.grpIdentifyRule.Controls.Add(Me.txtIdentifyFindToRow)
        Me.grpIdentifyRule.Controls.Add(Me.Label4)
        Me.grpIdentifyRule.Location = New System.Drawing.Point(350, 0)
        Me.grpIdentifyRule.Name = "grpIdentifyRule"
        Me.grpIdentifyRule.Size = New System.Drawing.Size(216, 140)
        Me.grpIdentifyRule.TabIndex = 27
        Me.grpIdentifyRule.TabStop = False
        Me.grpIdentifyRule.Text = "Rule"
        Me.grpIdentifyRule.Visible = False
        '
        'cmdGetIdentify
        '
        Me.cmdGetIdentify.Image = CType(resources.GetObject("cmdGetIdentify.Image"), System.Drawing.Image)
        Me.cmdGetIdentify.Location = New System.Drawing.Point(188, 56)
        Me.cmdGetIdentify.Name = "cmdGetIdentify"
        Me.cmdGetIdentify.Size = New System.Drawing.Size(22, 22)
        Me.cmdGetIdentify.TabIndex = 19
        Me.cmdGetIdentify.UseVisualStyleBackColor = True
        '
        'txtIdentifySearchValue
        '
        Me.txtIdentifySearchValue.Location = New System.Drawing.Point(110, 105)
        Me.txtIdentifySearchValue.Name = "txtIdentifySearchValue"
        Me.txtIdentifySearchValue.Size = New System.Drawing.Size(72, 20)
        Me.txtIdentifySearchValue.TabIndex = 9
        Me.txtIdentifySearchValue.Tag = ""
        '
        'cmbIdentifySearchType
        '
        Me.cmbIdentifySearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIdentifySearchType.FormattingEnabled = True
        Me.cmbIdentifySearchType.Items.AddRange(New Object() {"Value is", "Value is not", "Contains", "Does not contain"})
        Me.cmbIdentifySearchType.Location = New System.Drawing.Point(6, 105)
        Me.cmbIdentifySearchType.Name = "cmbIdentifySearchType"
        Me.cmbIdentifySearchType.Size = New System.Drawing.Size(98, 22)
        Me.cmbIdentifySearchType.TabIndex = 8
        Me.cmbIdentifySearchType.Tag = ""
        '
        'cmbIdentifyRule
        '
        Me.cmbIdentifyRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIdentifyRule.FormattingEnabled = True
        Me.cmbIdentifyRule.Items.AddRange(New Object() {"Check", "Find", "Step"})
        Me.cmbIdentifyRule.Location = New System.Drawing.Point(6, 19)
        Me.cmbIdentifyRule.Name = "cmbIdentifyRule"
        Me.cmbIdentifyRule.Size = New System.Drawing.Size(176, 22)
        Me.cmbIdentifyRule.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Row"
        '
        'txtIdentifyFindFromCol
        '
        Me.txtIdentifyFindFromCol.Location = New System.Drawing.Point(54, 79)
        Me.txtIdentifyFindFromCol.Name = "txtIdentifyFindFromCol"
        Me.txtIdentifyFindFromCol.Size = New System.Drawing.Size(50, 20)
        Me.txtIdentifyFindFromCol.TabIndex = 5
        Me.txtIdentifyFindFromCol.Tag = ""
        '
        'txtIdentifyFindFromRow
        '
        Me.txtIdentifyFindFromRow.Location = New System.Drawing.Point(54, 53)
        Me.txtIdentifyFindFromRow.Name = "txtIdentifyFindFromRow"
        Me.txtIdentifyFindFromRow.Size = New System.Drawing.Size(50, 20)
        Me.txtIdentifyFindFromRow.TabIndex = 1
        Me.txtIdentifyFindFromRow.Tag = ""
        '
        'txtIdentifyFindToCol
        '
        Me.txtIdentifyFindToCol.Location = New System.Drawing.Point(132, 79)
        Me.txtIdentifyFindToCol.Name = "txtIdentifyFindToCol"
        Me.txtIdentifyFindToCol.Size = New System.Drawing.Size(50, 20)
        Me.txtIdentifyFindToCol.TabIndex = 7
        Me.txtIdentifyFindToCol.Tag = ""
        '
        'lblIdentifyRowTo
        '
        Me.lblIdentifyRowTo.AutoSize = True
        Me.lblIdentifyRowTo.Location = New System.Drawing.Point(110, 56)
        Me.lblIdentifyRowTo.Name = "lblIdentifyRowTo"
        Me.lblIdentifyRowTo.Size = New System.Drawing.Size(16, 14)
        Me.lblIdentifyRowTo.TabIndex = 2
        Me.lblIdentifyRowTo.Text = "to"
        '
        'lblIdentifyColumnTo
        '
        Me.lblIdentifyColumnTo.AutoSize = True
        Me.lblIdentifyColumnTo.Location = New System.Drawing.Point(110, 82)
        Me.lblIdentifyColumnTo.Name = "lblIdentifyColumnTo"
        Me.lblIdentifyColumnTo.Size = New System.Drawing.Size(16, 14)
        Me.lblIdentifyColumnTo.TabIndex = 6
        Me.lblIdentifyColumnTo.Text = "to"
        '
        'txtIdentifyFindToRow
        '
        Me.txtIdentifyFindToRow.Location = New System.Drawing.Point(132, 53)
        Me.txtIdentifyFindToRow.Name = "txtIdentifyFindToRow"
        Me.txtIdentifyFindToRow.Size = New System.Drawing.Size(50, 20)
        Me.txtIdentifyFindToRow.TabIndex = 3
        Me.txtIdentifyFindToRow.Tag = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 14)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Column"
        '
        'cmdIdentifyUp
        '
        Me.cmdIdentifyUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdIdentifyUp.Image = CType(resources.GetObject("cmdIdentifyUp.Image"), System.Drawing.Image)
        Me.cmdIdentifyUp.Location = New System.Drawing.Point(322, 90)
        Me.cmdIdentifyUp.Name = "cmdIdentifyUp"
        Me.cmdIdentifyUp.Size = New System.Drawing.Size(22, 22)
        Me.cmdIdentifyUp.TabIndex = 25
        Me.cmdIdentifyUp.UseVisualStyleBackColor = True
        '
        'cmdIdentifyDown
        '
        Me.cmdIdentifyDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdIdentifyDown.Image = CType(resources.GetObject("cmdIdentifyDown.Image"), System.Drawing.Image)
        Me.cmdIdentifyDown.Location = New System.Drawing.Point(322, 118)
        Me.cmdIdentifyDown.Name = "cmdIdentifyDown"
        Me.cmdIdentifyDown.Size = New System.Drawing.Size(22, 22)
        Me.cmdIdentifyDown.TabIndex = 26
        Me.cmdIdentifyDown.UseVisualStyleBackColor = True
        '
        'tpTarget
        '
        Me.tpTarget.Controls.Add(Me.txtTargetFallback)
        Me.tpTarget.Controls.Add(Me.chkTargetRequired)
        Me.tpTarget.Controls.Add(Me.lblTargetRequired)
        Me.tpTarget.Controls.Add(Me.tvwTarget)
        Me.tpTarget.Controls.Add(Me.grpTargetRule)
        Me.tpTarget.Controls.Add(Me.cmdAddTargetRule)
        Me.tpTarget.Controls.Add(Me.cmdRemoveTargetRule)
        Me.tpTarget.Controls.Add(Me.cmdTargetUp)
        Me.tpTarget.Controls.Add(Me.cmdTargetDown)
        Me.tpTarget.ImageKey = "failed"
        Me.tpTarget.Location = New System.Drawing.Point(4, 23)
        Me.tpTarget.Name = "tpTarget"
        Me.tpTarget.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTarget.Size = New System.Drawing.Size(572, 324)
        Me.tpTarget.TabIndex = 1
        Me.tpTarget.Tag = "Target"
        Me.tpTarget.Text = "Target"
        Me.tpTarget.UseVisualStyleBackColor = True
        '
        'txtTargetFallback
        '
        Me.txtTargetFallback.Location = New System.Drawing.Point(209, 120)
        Me.txtTargetFallback.Name = "txtTargetFallback"
        Me.txtTargetFallback.Size = New System.Drawing.Size(107, 20)
        Me.txtTargetFallback.TabIndex = 38
        Me.txtTargetFallback.Visible = False
        '
        'chkTargetRequired
        '
        Me.chkTargetRequired.AutoSize = True
        Me.chkTargetRequired.Checked = True
        Me.chkTargetRequired.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTargetRequired.Location = New System.Drawing.Point(6, 122)
        Me.chkTargetRequired.Name = "chkTargetRequired"
        Me.chkTargetRequired.Size = New System.Drawing.Size(69, 18)
        Me.chkTargetRequired.TabIndex = 36
        Me.chkTargetRequired.Text = "Required"
        Me.chkTargetRequired.UseVisualStyleBackColor = True
        '
        'lblTargetRequired
        '
        Me.lblTargetRequired.AutoSize = True
        Me.lblTargetRequired.Location = New System.Drawing.Point(79, 123)
        Me.lblTargetRequired.Name = "lblTargetRequired"
        Me.lblTargetRequired.Size = New System.Drawing.Size(124, 14)
        Me.lblTargetRequired.TabIndex = 37
        Me.lblTargetRequired.Text = "If not found, set value to"
        Me.lblTargetRequired.Visible = False
        '
        'tvwTarget
        '
        Me.tvwTarget.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwTarget.ImageIndex = 2
        Me.tvwTarget.ImageList = Me.imlIcons
        Me.tvwTarget.Location = New System.Drawing.Point(2, 6)
        Me.tvwTarget.Name = "tvwTarget"
        Me.tvwTarget.SelectedImageIndex = 0
        Me.tvwTarget.Size = New System.Drawing.Size(314, 106)
        Me.tvwTarget.TabIndex = 22
        Me.tvwTarget.Tag = "Target"
        '
        'grpTargetRule
        '
        Me.grpTargetRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpTargetRule.Controls.Add(Me.cmdGetTarget)
        Me.grpTargetRule.Controls.Add(Me.txtTargetSearchValue)
        Me.grpTargetRule.Controls.Add(Me.cmbTargetSearchType)
        Me.grpTargetRule.Controls.Add(Me.cmbTargetRule)
        Me.grpTargetRule.Controls.Add(Me.Label3)
        Me.grpTargetRule.Controls.Add(Me.txtTargetFindFromCol)
        Me.grpTargetRule.Controls.Add(Me.txtTargetFindFromRow)
        Me.grpTargetRule.Controls.Add(Me.txtTargetFindToCol)
        Me.grpTargetRule.Controls.Add(Me.lblTargetRowTo)
        Me.grpTargetRule.Controls.Add(Me.lblTargetColumnTo)
        Me.grpTargetRule.Controls.Add(Me.txtTargetFindToRow)
        Me.grpTargetRule.Controls.Add(Me.Label8)
        Me.grpTargetRule.Location = New System.Drawing.Point(350, 0)
        Me.grpTargetRule.Name = "grpTargetRule"
        Me.grpTargetRule.Size = New System.Drawing.Size(216, 140)
        Me.grpTargetRule.TabIndex = 27
        Me.grpTargetRule.TabStop = False
        Me.grpTargetRule.Text = "Rule"
        Me.grpTargetRule.Visible = False
        '
        'cmdGetTarget
        '
        Me.cmdGetTarget.Image = CType(resources.GetObject("cmdGetTarget.Image"), System.Drawing.Image)
        Me.cmdGetTarget.Location = New System.Drawing.Point(188, 56)
        Me.cmdGetTarget.Name = "cmdGetTarget"
        Me.cmdGetTarget.Size = New System.Drawing.Size(22, 22)
        Me.cmdGetTarget.TabIndex = 19
        Me.cmdGetTarget.UseVisualStyleBackColor = True
        '
        'txtTargetSearchValue
        '
        Me.txtTargetSearchValue.Location = New System.Drawing.Point(110, 105)
        Me.txtTargetSearchValue.Name = "txtTargetSearchValue"
        Me.txtTargetSearchValue.Size = New System.Drawing.Size(72, 20)
        Me.txtTargetSearchValue.TabIndex = 9
        Me.txtTargetSearchValue.Tag = ""
        '
        'cmbTargetSearchType
        '
        Me.cmbTargetSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTargetSearchType.FormattingEnabled = True
        Me.cmbTargetSearchType.Items.AddRange(New Object() {"Value is", "Value is not", "Contains", "Does not contain"})
        Me.cmbTargetSearchType.Location = New System.Drawing.Point(6, 105)
        Me.cmbTargetSearchType.Name = "cmbTargetSearchType"
        Me.cmbTargetSearchType.Size = New System.Drawing.Size(98, 22)
        Me.cmbTargetSearchType.TabIndex = 8
        Me.cmbTargetSearchType.Tag = ""
        '
        'cmbTargetRule
        '
        Me.cmbTargetRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTargetRule.FormattingEnabled = True
        Me.cmbTargetRule.Items.AddRange(New Object() {"Check", "Find", "Step"})
        Me.cmbTargetRule.Location = New System.Drawing.Point(6, 19)
        Me.cmbTargetRule.Name = "cmbTargetRule"
        Me.cmbTargetRule.Size = New System.Drawing.Size(176, 22)
        Me.cmbTargetRule.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 14)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Row"
        '
        'txtTargetFindFromCol
        '
        Me.txtTargetFindFromCol.Location = New System.Drawing.Point(54, 79)
        Me.txtTargetFindFromCol.Name = "txtTargetFindFromCol"
        Me.txtTargetFindFromCol.Size = New System.Drawing.Size(50, 20)
        Me.txtTargetFindFromCol.TabIndex = 5
        Me.txtTargetFindFromCol.Tag = ""
        '
        'txtTargetFindFromRow
        '
        Me.txtTargetFindFromRow.Location = New System.Drawing.Point(54, 53)
        Me.txtTargetFindFromRow.Name = "txtTargetFindFromRow"
        Me.txtTargetFindFromRow.Size = New System.Drawing.Size(50, 20)
        Me.txtTargetFindFromRow.TabIndex = 1
        Me.txtTargetFindFromRow.Tag = ""
        '
        'txtTargetFindToCol
        '
        Me.txtTargetFindToCol.Location = New System.Drawing.Point(132, 79)
        Me.txtTargetFindToCol.Name = "txtTargetFindToCol"
        Me.txtTargetFindToCol.Size = New System.Drawing.Size(50, 20)
        Me.txtTargetFindToCol.TabIndex = 7
        Me.txtTargetFindToCol.Tag = ""
        '
        'lblTargetRowTo
        '
        Me.lblTargetRowTo.AutoSize = True
        Me.lblTargetRowTo.Location = New System.Drawing.Point(110, 56)
        Me.lblTargetRowTo.Name = "lblTargetRowTo"
        Me.lblTargetRowTo.Size = New System.Drawing.Size(16, 14)
        Me.lblTargetRowTo.TabIndex = 2
        Me.lblTargetRowTo.Text = "to"
        '
        'lblTargetColumnTo
        '
        Me.lblTargetColumnTo.AutoSize = True
        Me.lblTargetColumnTo.Location = New System.Drawing.Point(110, 82)
        Me.lblTargetColumnTo.Name = "lblTargetColumnTo"
        Me.lblTargetColumnTo.Size = New System.Drawing.Size(16, 14)
        Me.lblTargetColumnTo.TabIndex = 6
        Me.lblTargetColumnTo.Text = "to"
        '
        'txtTargetFindToRow
        '
        Me.txtTargetFindToRow.Location = New System.Drawing.Point(132, 53)
        Me.txtTargetFindToRow.Name = "txtTargetFindToRow"
        Me.txtTargetFindToRow.Size = New System.Drawing.Size(50, 20)
        Me.txtTargetFindToRow.TabIndex = 3
        Me.txtTargetFindToRow.Tag = ""
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 82)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 14)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Column"
        '
        'cmdAddTargetRule
        '
        Me.cmdAddTargetRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddTargetRule.Image = CType(resources.GetObject("cmdAddTargetRule.Image"), System.Drawing.Image)
        Me.cmdAddTargetRule.Location = New System.Drawing.Point(322, 6)
        Me.cmdAddTargetRule.Name = "cmdAddTargetRule"
        Me.cmdAddTargetRule.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddTargetRule.TabIndex = 23
        Me.cmdAddTargetRule.UseVisualStyleBackColor = True
        '
        'cmdRemoveTargetRule
        '
        Me.cmdRemoveTargetRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveTargetRule.Image = CType(resources.GetObject("cmdRemoveTargetRule.Image"), System.Drawing.Image)
        Me.cmdRemoveTargetRule.Location = New System.Drawing.Point(322, 34)
        Me.cmdRemoveTargetRule.Name = "cmdRemoveTargetRule"
        Me.cmdRemoveTargetRule.Size = New System.Drawing.Size(22, 22)
        Me.cmdRemoveTargetRule.TabIndex = 24
        Me.cmdRemoveTargetRule.UseVisualStyleBackColor = True
        '
        'cmdTargetUp
        '
        Me.cmdTargetUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdTargetUp.Image = CType(resources.GetObject("cmdTargetUp.Image"), System.Drawing.Image)
        Me.cmdTargetUp.Location = New System.Drawing.Point(322, 62)
        Me.cmdTargetUp.Name = "cmdTargetUp"
        Me.cmdTargetUp.Size = New System.Drawing.Size(22, 22)
        Me.cmdTargetUp.TabIndex = 25
        Me.cmdTargetUp.UseVisualStyleBackColor = True
        '
        'cmdTargetDown
        '
        Me.cmdTargetDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdTargetDown.Image = CType(resources.GetObject("cmdTargetDown.Image"), System.Drawing.Image)
        Me.cmdTargetDown.Location = New System.Drawing.Point(322, 90)
        Me.cmdTargetDown.Name = "cmdTargetDown"
        Me.cmdTargetDown.Size = New System.Drawing.Size(22, 22)
        Me.cmdTargetDown.TabIndex = 26
        Me.cmdTargetDown.UseVisualStyleBackColor = True
        '
        'tpContract
        '
        Me.tpContract.Controls.Add(Me.txtContractFallback)
        Me.tpContract.Controls.Add(Me.lblContractRequired)
        Me.tpContract.Controls.Add(Me.tvwContract)
        Me.tpContract.Controls.Add(Me.chkContractRequired)
        Me.tpContract.Controls.Add(Me.grpContractRule)
        Me.tpContract.Controls.Add(Me.cmdAddContractRule)
        Me.tpContract.Controls.Add(Me.cmdRemoveContractRule)
        Me.tpContract.Controls.Add(Me.cmdContractUp)
        Me.tpContract.Controls.Add(Me.cmdContractDown)
        Me.tpContract.ImageKey = "failed"
        Me.tpContract.Location = New System.Drawing.Point(4, 23)
        Me.tpContract.Name = "tpContract"
        Me.tpContract.Padding = New System.Windows.Forms.Padding(3)
        Me.tpContract.Size = New System.Drawing.Size(572, 324)
        Me.tpContract.TabIndex = 5
        Me.tpContract.Tag = "Contract"
        Me.tpContract.Text = "Contract number"
        Me.tpContract.UseVisualStyleBackColor = True
        '
        'txtContractFallback
        '
        Me.txtContractFallback.Location = New System.Drawing.Point(209, 120)
        Me.txtContractFallback.Name = "txtContractFallback"
        Me.txtContractFallback.Size = New System.Drawing.Size(107, 20)
        Me.txtContractFallback.TabIndex = 35
        Me.txtContractFallback.Visible = False
        '
        'lblContractRequired
        '
        Me.lblContractRequired.AutoSize = True
        Me.lblContractRequired.Location = New System.Drawing.Point(79, 123)
        Me.lblContractRequired.Name = "lblContractRequired"
        Me.lblContractRequired.Size = New System.Drawing.Size(124, 14)
        Me.lblContractRequired.TabIndex = 34
        Me.lblContractRequired.Text = "If not found, set value to"
        Me.lblContractRequired.Visible = False
        '
        'tvwContract
        '
        Me.tvwContract.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwContract.ImageIndex = 2
        Me.tvwContract.ImageList = Me.imlIcons
        Me.tvwContract.Location = New System.Drawing.Point(2, 6)
        Me.tvwContract.Name = "tvwContract"
        Me.tvwContract.SelectedImageIndex = 0
        Me.tvwContract.Size = New System.Drawing.Size(314, 106)
        Me.tvwContract.TabIndex = 22
        Me.tvwContract.Tag = "Contract"
        '
        'chkContractRequired
        '
        Me.chkContractRequired.AutoSize = True
        Me.chkContractRequired.Checked = True
        Me.chkContractRequired.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkContractRequired.Location = New System.Drawing.Point(6, 122)
        Me.chkContractRequired.Name = "chkContractRequired"
        Me.chkContractRequired.Size = New System.Drawing.Size(69, 18)
        Me.chkContractRequired.TabIndex = 33
        Me.chkContractRequired.Text = "Required"
        Me.chkContractRequired.UseVisualStyleBackColor = True
        '
        'grpContractRule
        '
        Me.grpContractRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpContractRule.Controls.Add(Me.cmdGetContract)
        Me.grpContractRule.Controls.Add(Me.txtContractSearchValue)
        Me.grpContractRule.Controls.Add(Me.cmbContractSearchType)
        Me.grpContractRule.Controls.Add(Me.cmbContractRule)
        Me.grpContractRule.Controls.Add(Me.Label6)
        Me.grpContractRule.Controls.Add(Me.txtContractFindFromCol)
        Me.grpContractRule.Controls.Add(Me.txtContractFindFromRow)
        Me.grpContractRule.Controls.Add(Me.txtContractFindToCol)
        Me.grpContractRule.Controls.Add(Me.lblContractRowTo)
        Me.grpContractRule.Controls.Add(Me.lblContractColumnTo)
        Me.grpContractRule.Controls.Add(Me.txtContractFindToRow)
        Me.grpContractRule.Controls.Add(Me.Label10)
        Me.grpContractRule.Location = New System.Drawing.Point(350, 0)
        Me.grpContractRule.Name = "grpContractRule"
        Me.grpContractRule.Size = New System.Drawing.Size(216, 140)
        Me.grpContractRule.TabIndex = 27
        Me.grpContractRule.TabStop = False
        Me.grpContractRule.Text = "Rule"
        Me.grpContractRule.Visible = False
        '
        'cmdGetContract
        '
        Me.cmdGetContract.Image = CType(resources.GetObject("cmdGetContract.Image"), System.Drawing.Image)
        Me.cmdGetContract.Location = New System.Drawing.Point(188, 56)
        Me.cmdGetContract.Name = "cmdGetContract"
        Me.cmdGetContract.Size = New System.Drawing.Size(22, 22)
        Me.cmdGetContract.TabIndex = 19
        Me.cmdGetContract.UseVisualStyleBackColor = True
        '
        'txtContractSearchValue
        '
        Me.txtContractSearchValue.Location = New System.Drawing.Point(110, 105)
        Me.txtContractSearchValue.Name = "txtContractSearchValue"
        Me.txtContractSearchValue.Size = New System.Drawing.Size(72, 20)
        Me.txtContractSearchValue.TabIndex = 9
        Me.txtContractSearchValue.Tag = ""
        '
        'cmbContractSearchType
        '
        Me.cmbContractSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbContractSearchType.FormattingEnabled = True
        Me.cmbContractSearchType.Items.AddRange(New Object() {"Value is", "Value is not", "Contains", "Does not contain"})
        Me.cmbContractSearchType.Location = New System.Drawing.Point(6, 105)
        Me.cmbContractSearchType.Name = "cmbContractSearchType"
        Me.cmbContractSearchType.Size = New System.Drawing.Size(98, 22)
        Me.cmbContractSearchType.TabIndex = 8
        Me.cmbContractSearchType.Tag = ""
        '
        'cmbContractRule
        '
        Me.cmbContractRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbContractRule.FormattingEnabled = True
        Me.cmbContractRule.Items.AddRange(New Object() {"Check", "Find", "Step"})
        Me.cmbContractRule.Location = New System.Drawing.Point(6, 19)
        Me.cmbContractRule.Name = "cmbContractRule"
        Me.cmbContractRule.Size = New System.Drawing.Size(176, 22)
        Me.cmbContractRule.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 56)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 14)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Row"
        '
        'txtContractFindFromCol
        '
        Me.txtContractFindFromCol.Location = New System.Drawing.Point(54, 79)
        Me.txtContractFindFromCol.Name = "txtContractFindFromCol"
        Me.txtContractFindFromCol.Size = New System.Drawing.Size(50, 20)
        Me.txtContractFindFromCol.TabIndex = 5
        Me.txtContractFindFromCol.Tag = ""
        '
        'txtContractFindFromRow
        '
        Me.txtContractFindFromRow.Location = New System.Drawing.Point(54, 53)
        Me.txtContractFindFromRow.Name = "txtContractFindFromRow"
        Me.txtContractFindFromRow.Size = New System.Drawing.Size(50, 20)
        Me.txtContractFindFromRow.TabIndex = 1
        Me.txtContractFindFromRow.Tag = ""
        '
        'txtContractFindToCol
        '
        Me.txtContractFindToCol.Location = New System.Drawing.Point(132, 79)
        Me.txtContractFindToCol.Name = "txtContractFindToCol"
        Me.txtContractFindToCol.Size = New System.Drawing.Size(50, 20)
        Me.txtContractFindToCol.TabIndex = 7
        Me.txtContractFindToCol.Tag = ""
        '
        'lblContractRowTo
        '
        Me.lblContractRowTo.AutoSize = True
        Me.lblContractRowTo.Location = New System.Drawing.Point(110, 56)
        Me.lblContractRowTo.Name = "lblContractRowTo"
        Me.lblContractRowTo.Size = New System.Drawing.Size(16, 14)
        Me.lblContractRowTo.TabIndex = 2
        Me.lblContractRowTo.Text = "to"
        '
        'lblContractColumnTo
        '
        Me.lblContractColumnTo.AutoSize = True
        Me.lblContractColumnTo.Location = New System.Drawing.Point(110, 82)
        Me.lblContractColumnTo.Name = "lblContractColumnTo"
        Me.lblContractColumnTo.Size = New System.Drawing.Size(16, 14)
        Me.lblContractColumnTo.TabIndex = 6
        Me.lblContractColumnTo.Text = "to"
        '
        'txtContractFindToRow
        '
        Me.txtContractFindToRow.Location = New System.Drawing.Point(132, 53)
        Me.txtContractFindToRow.Name = "txtContractFindToRow"
        Me.txtContractFindToRow.Size = New System.Drawing.Size(50, 20)
        Me.txtContractFindToRow.TabIndex = 3
        Me.txtContractFindToRow.Tag = ""
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 82)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(42, 14)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Column"
        '
        'cmdAddContractRule
        '
        Me.cmdAddContractRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddContractRule.Image = CType(resources.GetObject("cmdAddContractRule.Image"), System.Drawing.Image)
        Me.cmdAddContractRule.Location = New System.Drawing.Point(322, 6)
        Me.cmdAddContractRule.Name = "cmdAddContractRule"
        Me.cmdAddContractRule.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddContractRule.TabIndex = 23
        Me.cmdAddContractRule.UseVisualStyleBackColor = True
        '
        'cmdRemoveContractRule
        '
        Me.cmdRemoveContractRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveContractRule.Image = CType(resources.GetObject("cmdRemoveContractRule.Image"), System.Drawing.Image)
        Me.cmdRemoveContractRule.Location = New System.Drawing.Point(322, 34)
        Me.cmdRemoveContractRule.Name = "cmdRemoveContractRule"
        Me.cmdRemoveContractRule.Size = New System.Drawing.Size(22, 22)
        Me.cmdRemoveContractRule.TabIndex = 24
        Me.cmdRemoveContractRule.UseVisualStyleBackColor = True
        '
        'cmdContractUp
        '
        Me.cmdContractUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdContractUp.Image = CType(resources.GetObject("cmdContractUp.Image"), System.Drawing.Image)
        Me.cmdContractUp.Location = New System.Drawing.Point(322, 62)
        Me.cmdContractUp.Name = "cmdContractUp"
        Me.cmdContractUp.Size = New System.Drawing.Size(22, 22)
        Me.cmdContractUp.TabIndex = 25
        Me.cmdContractUp.UseVisualStyleBackColor = True
        '
        'cmdContractDown
        '
        Me.cmdContractDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdContractDown.Image = CType(resources.GetObject("cmdContractDown.Image"), System.Drawing.Image)
        Me.cmdContractDown.Location = New System.Drawing.Point(322, 90)
        Me.cmdContractDown.Name = "cmdContractDown"
        Me.cmdContractDown.Size = New System.Drawing.Size(22, 22)
        Me.cmdContractDown.TabIndex = 26
        Me.cmdContractDown.UseVisualStyleBackColor = True
        '
        'tpChannel
        '
        Me.tpChannel.Controls.Add(Me.tvwChannel)
        Me.tpChannel.Controls.Add(Me.grpChannelRule)
        Me.tpChannel.Controls.Add(Me.cmdAddChannelRule)
        Me.tpChannel.Controls.Add(Me.cmdRemoveChannelRule)
        Me.tpChannel.Controls.Add(Me.cmdChannelUp)
        Me.tpChannel.Controls.Add(Me.cmdChannelDown)
        Me.tpChannel.ImageKey = "ok"
        Me.tpChannel.Location = New System.Drawing.Point(4, 23)
        Me.tpChannel.Name = "tpChannel"
        Me.tpChannel.Padding = New System.Windows.Forms.Padding(3)
        Me.tpChannel.Size = New System.Drawing.Size(572, 324)
        Me.tpChannel.TabIndex = 2
        Me.tpChannel.Tag = "Channel"
        Me.tpChannel.Text = "Channel"
        Me.tpChannel.UseVisualStyleBackColor = True
        '
        'tvwChannel
        '
        Me.tvwChannel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwChannel.ImageIndex = 2
        Me.tvwChannel.ImageList = Me.imlIcons
        Me.tvwChannel.Location = New System.Drawing.Point(2, 6)
        Me.tvwChannel.Name = "tvwChannel"
        Me.tvwChannel.SelectedImageIndex = 0
        Me.tvwChannel.Size = New System.Drawing.Size(314, 106)
        Me.tvwChannel.TabIndex = 22
        Me.tvwChannel.Tag = "Channel"
        '
        'grpChannelRule
        '
        Me.grpChannelRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpChannelRule.Controls.Add(Me.cmdGetChannel)
        Me.grpChannelRule.Controls.Add(Me.txtChannelSearchValue)
        Me.grpChannelRule.Controls.Add(Me.cmbChannelSearchType)
        Me.grpChannelRule.Controls.Add(Me.cmbChannelRule)
        Me.grpChannelRule.Controls.Add(Me.Label7)
        Me.grpChannelRule.Controls.Add(Me.txtChannelFindFromCol)
        Me.grpChannelRule.Controls.Add(Me.txtChannelFindFromRow)
        Me.grpChannelRule.Controls.Add(Me.txtChannelFindToCol)
        Me.grpChannelRule.Controls.Add(Me.lblChannelRowTo)
        Me.grpChannelRule.Controls.Add(Me.lblChannelColumnTo)
        Me.grpChannelRule.Controls.Add(Me.txtChannelFindToRow)
        Me.grpChannelRule.Controls.Add(Me.Label12)
        Me.grpChannelRule.Location = New System.Drawing.Point(350, 0)
        Me.grpChannelRule.Name = "grpChannelRule"
        Me.grpChannelRule.Size = New System.Drawing.Size(216, 140)
        Me.grpChannelRule.TabIndex = 27
        Me.grpChannelRule.TabStop = False
        Me.grpChannelRule.Text = "Rule"
        Me.grpChannelRule.Visible = False
        '
        'cmdGetChannel
        '
        Me.cmdGetChannel.Image = CType(resources.GetObject("cmdGetChannel.Image"), System.Drawing.Image)
        Me.cmdGetChannel.Location = New System.Drawing.Point(188, 56)
        Me.cmdGetChannel.Name = "cmdGetChannel"
        Me.cmdGetChannel.Size = New System.Drawing.Size(22, 22)
        Me.cmdGetChannel.TabIndex = 19
        Me.cmdGetChannel.UseVisualStyleBackColor = True
        '
        'txtChannelSearchValue
        '
        Me.txtChannelSearchValue.Location = New System.Drawing.Point(110, 105)
        Me.txtChannelSearchValue.Name = "txtChannelSearchValue"
        Me.txtChannelSearchValue.Size = New System.Drawing.Size(72, 20)
        Me.txtChannelSearchValue.TabIndex = 9
        Me.txtChannelSearchValue.Tag = ""
        '
        'cmbChannelSearchType
        '
        Me.cmbChannelSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChannelSearchType.FormattingEnabled = True
        Me.cmbChannelSearchType.Items.AddRange(New Object() {"Value is", "Value is not", "Contains", "Does not contain"})
        Me.cmbChannelSearchType.Location = New System.Drawing.Point(6, 105)
        Me.cmbChannelSearchType.Name = "cmbChannelSearchType"
        Me.cmbChannelSearchType.Size = New System.Drawing.Size(98, 22)
        Me.cmbChannelSearchType.TabIndex = 8
        Me.cmbChannelSearchType.Tag = ""
        '
        'cmbChannelRule
        '
        Me.cmbChannelRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChannelRule.FormattingEnabled = True
        Me.cmbChannelRule.Items.AddRange(New Object() {"Check", "Find", "Step"})
        Me.cmbChannelRule.Location = New System.Drawing.Point(6, 19)
        Me.cmbChannelRule.Name = "cmbChannelRule"
        Me.cmbChannelRule.Size = New System.Drawing.Size(176, 22)
        Me.cmbChannelRule.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 56)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(30, 14)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Row"
        '
        'txtChannelFindFromCol
        '
        Me.txtChannelFindFromCol.Location = New System.Drawing.Point(54, 79)
        Me.txtChannelFindFromCol.Name = "txtChannelFindFromCol"
        Me.txtChannelFindFromCol.Size = New System.Drawing.Size(50, 20)
        Me.txtChannelFindFromCol.TabIndex = 5
        Me.txtChannelFindFromCol.Tag = ""
        '
        'txtChannelFindFromRow
        '
        Me.txtChannelFindFromRow.Location = New System.Drawing.Point(54, 53)
        Me.txtChannelFindFromRow.Name = "txtChannelFindFromRow"
        Me.txtChannelFindFromRow.Size = New System.Drawing.Size(50, 20)
        Me.txtChannelFindFromRow.TabIndex = 1
        Me.txtChannelFindFromRow.Tag = ""
        '
        'txtChannelFindToCol
        '
        Me.txtChannelFindToCol.Location = New System.Drawing.Point(132, 79)
        Me.txtChannelFindToCol.Name = "txtChannelFindToCol"
        Me.txtChannelFindToCol.Size = New System.Drawing.Size(50, 20)
        Me.txtChannelFindToCol.TabIndex = 7
        Me.txtChannelFindToCol.Tag = ""
        '
        'lblChannelRowTo
        '
        Me.lblChannelRowTo.AutoSize = True
        Me.lblChannelRowTo.Location = New System.Drawing.Point(110, 56)
        Me.lblChannelRowTo.Name = "lblChannelRowTo"
        Me.lblChannelRowTo.Size = New System.Drawing.Size(16, 14)
        Me.lblChannelRowTo.TabIndex = 2
        Me.lblChannelRowTo.Text = "to"
        '
        'lblChannelColumnTo
        '
        Me.lblChannelColumnTo.AutoSize = True
        Me.lblChannelColumnTo.Location = New System.Drawing.Point(110, 82)
        Me.lblChannelColumnTo.Name = "lblChannelColumnTo"
        Me.lblChannelColumnTo.Size = New System.Drawing.Size(16, 14)
        Me.lblChannelColumnTo.TabIndex = 6
        Me.lblChannelColumnTo.Text = "to"
        '
        'txtChannelFindToRow
        '
        Me.txtChannelFindToRow.Location = New System.Drawing.Point(132, 53)
        Me.txtChannelFindToRow.Name = "txtChannelFindToRow"
        Me.txtChannelFindToRow.Size = New System.Drawing.Size(50, 20)
        Me.txtChannelFindToRow.TabIndex = 3
        Me.txtChannelFindToRow.Tag = ""
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 82)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(42, 14)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "Column"
        '
        'cmdAddChannelRule
        '
        Me.cmdAddChannelRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddChannelRule.Image = CType(resources.GetObject("cmdAddChannelRule.Image"), System.Drawing.Image)
        Me.cmdAddChannelRule.Location = New System.Drawing.Point(322, 6)
        Me.cmdAddChannelRule.Name = "cmdAddChannelRule"
        Me.cmdAddChannelRule.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddChannelRule.TabIndex = 23
        Me.cmdAddChannelRule.UseVisualStyleBackColor = True
        '
        'cmdRemoveChannelRule
        '
        Me.cmdRemoveChannelRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveChannelRule.Image = CType(resources.GetObject("cmdRemoveChannelRule.Image"), System.Drawing.Image)
        Me.cmdRemoveChannelRule.Location = New System.Drawing.Point(322, 34)
        Me.cmdRemoveChannelRule.Name = "cmdRemoveChannelRule"
        Me.cmdRemoveChannelRule.Size = New System.Drawing.Size(22, 22)
        Me.cmdRemoveChannelRule.TabIndex = 24
        Me.cmdRemoveChannelRule.UseVisualStyleBackColor = True
        '
        'cmdChannelUp
        '
        Me.cmdChannelUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdChannelUp.Image = CType(resources.GetObject("cmdChannelUp.Image"), System.Drawing.Image)
        Me.cmdChannelUp.Location = New System.Drawing.Point(322, 62)
        Me.cmdChannelUp.Name = "cmdChannelUp"
        Me.cmdChannelUp.Size = New System.Drawing.Size(22, 22)
        Me.cmdChannelUp.TabIndex = 25
        Me.cmdChannelUp.UseVisualStyleBackColor = True
        '
        'cmdChannelDown
        '
        Me.cmdChannelDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdChannelDown.Image = CType(resources.GetObject("cmdChannelDown.Image"), System.Drawing.Image)
        Me.cmdChannelDown.Location = New System.Drawing.Point(322, 90)
        Me.cmdChannelDown.Name = "cmdChannelDown"
        Me.cmdChannelDown.Size = New System.Drawing.Size(22, 22)
        Me.cmdChannelDown.TabIndex = 26
        Me.cmdChannelDown.UseVisualStyleBackColor = True
        '
        'tpColumns
        '
        Me.tpColumns.Controls.Add(Me.lstRequired)
        Me.tpColumns.Controls.Add(Me.Label13)
        Me.tpColumns.Controls.Add(Me.Label16)
        Me.tpColumns.Controls.Add(Me.tvwHeadlines)
        Me.tpColumns.Controls.Add(Me.Label9)
        Me.tpColumns.Controls.Add(Me.tvwColumns)
        Me.tpColumns.Controls.Add(Me.grpColumnsRule)
        Me.tpColumns.Controls.Add(Me.cmdUnsetRequiredColumn)
        Me.tpColumns.Controls.Add(Me.cmdSetRequiredColumn)
        Me.tpColumns.Controls.Add(Me.cmdAddHeadline)
        Me.tpColumns.Controls.Add(Me.cmdRemoveHeadline)
        Me.tpColumns.Controls.Add(Me.cmdAddHeadlineRule)
        Me.tpColumns.Controls.Add(Me.cmdAddHeadlineRow)
        Me.tpColumns.Controls.Add(Me.cmdRemoveHeadlineRule)
        Me.tpColumns.Controls.Add(Me.cmdHeadlineUp)
        Me.tpColumns.Controls.Add(Me.cmdHeadlineDown)
        Me.tpColumns.ImageKey = "failed"
        Me.tpColumns.Location = New System.Drawing.Point(4, 23)
        Me.tpColumns.Name = "tpColumns"
        Me.tpColumns.Padding = New System.Windows.Forms.Padding(3)
        Me.tpColumns.Size = New System.Drawing.Size(572, 324)
        Me.tpColumns.TabIndex = 3
        Me.tpColumns.Tag = "Columns"
        Me.tpColumns.Text = "Columns"
        Me.tpColumns.UseVisualStyleBackColor = True
        '
        'lstRequired
        '
        Me.lstRequired.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstRequired.FormattingEnabled = True
        Me.lstRequired.ItemHeight = 14
        Me.lstRequired.Location = New System.Drawing.Point(350, 178)
        Me.lstRequired.Name = "lstRequired"
        Me.lstRequired.Size = New System.Drawing.Size(216, 130)
        Me.lstRequired.TabIndex = 37
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(350, 161)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(50, 14)
        Me.Label13.TabIndex = 36
        Me.Label13.Text = "Required"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(3, 161)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(91, 14)
        Me.Label16.TabIndex = 31
        Me.Label16.Text = "Column headlines"
        '
        'tvwHeadlines
        '
        Me.tvwHeadlines.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwHeadlines.ImageKey = "unknown"
        Me.tvwHeadlines.ImageList = Me.imlIcons
        Me.tvwHeadlines.LabelEdit = True
        Me.tvwHeadlines.Location = New System.Drawing.Point(2, 178)
        Me.tvwHeadlines.Name = "tvwHeadlines"
        Me.tvwHeadlines.SelectedImageKey = "unknown"
        Me.tvwHeadlines.Size = New System.Drawing.Size(314, 130)
        Me.tvwHeadlines.TabIndex = 30
        Me.tvwHeadlines.Tag = ""
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(3, 7)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(77, 14)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "Headline rows"
        '
        'tvwColumns
        '
        Me.tvwColumns.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwColumns.ImageIndex = 2
        Me.tvwColumns.ImageList = Me.imlIcons
        Me.tvwColumns.Location = New System.Drawing.Point(2, 24)
        Me.tvwColumns.Name = "tvwColumns"
        Me.tvwColumns.SelectedImageIndex = 0
        Me.tvwColumns.Size = New System.Drawing.Size(314, 134)
        Me.tvwColumns.TabIndex = 22
        Me.tvwColumns.Tag = "Columns"
        '
        'grpColumnsRule
        '
        Me.grpColumnsRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpColumnsRule.Controls.Add(Me.cmdGetColumns)
        Me.grpColumnsRule.Controls.Add(Me.txtColumnsSearchValue)
        Me.grpColumnsRule.Controls.Add(Me.cmbColumnsSearchType)
        Me.grpColumnsRule.Controls.Add(Me.cmbColumnsRule)
        Me.grpColumnsRule.Controls.Add(Me.Label11)
        Me.grpColumnsRule.Controls.Add(Me.txtColumnsFindFromCol)
        Me.grpColumnsRule.Controls.Add(Me.txtColumnsFindFromRow)
        Me.grpColumnsRule.Controls.Add(Me.txtColumnsFindToCol)
        Me.grpColumnsRule.Controls.Add(Me.lblColumnsRowTo)
        Me.grpColumnsRule.Controls.Add(Me.lblColumnsColumnTo)
        Me.grpColumnsRule.Controls.Add(Me.txtColumnsFindToRow)
        Me.grpColumnsRule.Controls.Add(Me.Label15)
        Me.grpColumnsRule.Location = New System.Drawing.Point(350, 24)
        Me.grpColumnsRule.Name = "grpColumnsRule"
        Me.grpColumnsRule.Size = New System.Drawing.Size(216, 134)
        Me.grpColumnsRule.TabIndex = 27
        Me.grpColumnsRule.TabStop = False
        Me.grpColumnsRule.Text = "Rule"
        Me.grpColumnsRule.Visible = False
        '
        'cmdGetColumns
        '
        Me.cmdGetColumns.Image = CType(resources.GetObject("cmdGetColumns.Image"), System.Drawing.Image)
        Me.cmdGetColumns.Location = New System.Drawing.Point(188, 56)
        Me.cmdGetColumns.Name = "cmdGetColumns"
        Me.cmdGetColumns.Size = New System.Drawing.Size(22, 22)
        Me.cmdGetColumns.TabIndex = 19
        Me.cmdGetColumns.UseVisualStyleBackColor = True
        '
        'txtColumnsSearchValue
        '
        Me.txtColumnsSearchValue.Location = New System.Drawing.Point(110, 105)
        Me.txtColumnsSearchValue.Name = "txtColumnsSearchValue"
        Me.txtColumnsSearchValue.Size = New System.Drawing.Size(72, 20)
        Me.txtColumnsSearchValue.TabIndex = 9
        Me.txtColumnsSearchValue.Tag = ""
        '
        'cmbColumnsSearchType
        '
        Me.cmbColumnsSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbColumnsSearchType.FormattingEnabled = True
        Me.cmbColumnsSearchType.Items.AddRange(New Object() {"Value is", "Value is not", "Contains", "Does not contain"})
        Me.cmbColumnsSearchType.Location = New System.Drawing.Point(6, 105)
        Me.cmbColumnsSearchType.Name = "cmbColumnsSearchType"
        Me.cmbColumnsSearchType.Size = New System.Drawing.Size(98, 22)
        Me.cmbColumnsSearchType.TabIndex = 8
        Me.cmbColumnsSearchType.Tag = ""
        '
        'cmbColumnsRule
        '
        Me.cmbColumnsRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbColumnsRule.FormattingEnabled = True
        Me.cmbColumnsRule.Items.AddRange(New Object() {"Check", "Find", "Step"})
        Me.cmbColumnsRule.Location = New System.Drawing.Point(6, 19)
        Me.cmbColumnsRule.Name = "cmbColumnsRule"
        Me.cmbColumnsRule.Size = New System.Drawing.Size(176, 22)
        Me.cmbColumnsRule.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 56)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(30, 14)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Row"
        '
        'txtColumnsFindFromCol
        '
        Me.txtColumnsFindFromCol.Location = New System.Drawing.Point(54, 79)
        Me.txtColumnsFindFromCol.Name = "txtColumnsFindFromCol"
        Me.txtColumnsFindFromCol.Size = New System.Drawing.Size(50, 20)
        Me.txtColumnsFindFromCol.TabIndex = 5
        Me.txtColumnsFindFromCol.Tag = ""
        '
        'txtColumnsFindFromRow
        '
        Me.txtColumnsFindFromRow.Location = New System.Drawing.Point(54, 53)
        Me.txtColumnsFindFromRow.Name = "txtColumnsFindFromRow"
        Me.txtColumnsFindFromRow.Size = New System.Drawing.Size(50, 20)
        Me.txtColumnsFindFromRow.TabIndex = 1
        Me.txtColumnsFindFromRow.Tag = ""
        '
        'txtColumnsFindToCol
        '
        Me.txtColumnsFindToCol.Location = New System.Drawing.Point(132, 79)
        Me.txtColumnsFindToCol.Name = "txtColumnsFindToCol"
        Me.txtColumnsFindToCol.Size = New System.Drawing.Size(50, 20)
        Me.txtColumnsFindToCol.TabIndex = 7
        Me.txtColumnsFindToCol.Tag = ""
        '
        'lblColumnsRowTo
        '
        Me.lblColumnsRowTo.AutoSize = True
        Me.lblColumnsRowTo.Location = New System.Drawing.Point(110, 56)
        Me.lblColumnsRowTo.Name = "lblColumnsRowTo"
        Me.lblColumnsRowTo.Size = New System.Drawing.Size(16, 14)
        Me.lblColumnsRowTo.TabIndex = 2
        Me.lblColumnsRowTo.Text = "to"
        '
        'lblColumnsColumnTo
        '
        Me.lblColumnsColumnTo.AutoSize = True
        Me.lblColumnsColumnTo.Location = New System.Drawing.Point(110, 82)
        Me.lblColumnsColumnTo.Name = "lblColumnsColumnTo"
        Me.lblColumnsColumnTo.Size = New System.Drawing.Size(16, 14)
        Me.lblColumnsColumnTo.TabIndex = 6
        Me.lblColumnsColumnTo.Text = "to"
        '
        'txtColumnsFindToRow
        '
        Me.txtColumnsFindToRow.Location = New System.Drawing.Point(132, 53)
        Me.txtColumnsFindToRow.Name = "txtColumnsFindToRow"
        Me.txtColumnsFindToRow.Size = New System.Drawing.Size(50, 20)
        Me.txtColumnsFindToRow.TabIndex = 3
        Me.txtColumnsFindToRow.Tag = ""
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 82)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(42, 14)
        Me.Label15.TabIndex = 4
        Me.Label15.Text = "Column"
        '
        'cmdUnsetRequiredColumn
        '
        Me.cmdUnsetRequiredColumn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdUnsetRequiredColumn.Image = CType(resources.GetObject("cmdUnsetRequiredColumn.Image"), System.Drawing.Image)
        Me.cmdUnsetRequiredColumn.Location = New System.Drawing.Point(322, 262)
        Me.cmdUnsetRequiredColumn.Name = "cmdUnsetRequiredColumn"
        Me.cmdUnsetRequiredColumn.Size = New System.Drawing.Size(22, 22)
        Me.cmdUnsetRequiredColumn.TabIndex = 35
        Me.cmdUnsetRequiredColumn.UseVisualStyleBackColor = True
        '
        'cmdSetRequiredColumn
        '
        Me.cmdSetRequiredColumn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSetRequiredColumn.Image = CType(resources.GetObject("cmdSetRequiredColumn.Image"), System.Drawing.Image)
        Me.cmdSetRequiredColumn.Location = New System.Drawing.Point(322, 234)
        Me.cmdSetRequiredColumn.Name = "cmdSetRequiredColumn"
        Me.cmdSetRequiredColumn.Size = New System.Drawing.Size(22, 22)
        Me.cmdSetRequiredColumn.TabIndex = 34
        Me.cmdSetRequiredColumn.UseVisualStyleBackColor = True
        '
        'cmdAddHeadline
        '
        Me.cmdAddHeadline.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddHeadline.Image = CType(resources.GetObject("cmdAddHeadline.Image"), System.Drawing.Image)
        Me.cmdAddHeadline.Location = New System.Drawing.Point(322, 178)
        Me.cmdAddHeadline.Name = "cmdAddHeadline"
        Me.cmdAddHeadline.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddHeadline.TabIndex = 32
        Me.cmdAddHeadline.UseVisualStyleBackColor = True
        '
        'cmdRemoveHeadline
        '
        Me.cmdRemoveHeadline.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveHeadline.Image = CType(resources.GetObject("cmdRemoveHeadline.Image"), System.Drawing.Image)
        Me.cmdRemoveHeadline.Location = New System.Drawing.Point(322, 206)
        Me.cmdRemoveHeadline.Name = "cmdRemoveHeadline"
        Me.cmdRemoveHeadline.Size = New System.Drawing.Size(22, 22)
        Me.cmdRemoveHeadline.TabIndex = 33
        Me.cmdRemoveHeadline.UseVisualStyleBackColor = True
        '
        'cmdAddHeadlineRule
        '
        Me.cmdAddHeadlineRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddHeadlineRule.Image = CType(resources.GetObject("cmdAddHeadlineRule.Image"), System.Drawing.Image)
        Me.cmdAddHeadlineRule.Location = New System.Drawing.Point(322, 24)
        Me.cmdAddHeadlineRule.Name = "cmdAddHeadlineRule"
        Me.cmdAddHeadlineRule.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddHeadlineRule.TabIndex = 23
        Me.cmdAddHeadlineRule.UseVisualStyleBackColor = True
        '
        'cmdAddHeadlineRow
        '
        Me.cmdAddHeadlineRow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddHeadlineRow.Image = CType(resources.GetObject("cmdAddHeadlineRow.Image"), System.Drawing.Image)
        Me.cmdAddHeadlineRow.Location = New System.Drawing.Point(322, 52)
        Me.cmdAddHeadlineRow.Name = "cmdAddHeadlineRow"
        Me.cmdAddHeadlineRow.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddHeadlineRow.TabIndex = 28
        Me.cmdAddHeadlineRow.UseVisualStyleBackColor = True
        '
        'cmdRemoveHeadlineRule
        '
        Me.cmdRemoveHeadlineRule.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveHeadlineRule.Image = CType(resources.GetObject("cmdRemoveHeadlineRule.Image"), System.Drawing.Image)
        Me.cmdRemoveHeadlineRule.Location = New System.Drawing.Point(322, 80)
        Me.cmdRemoveHeadlineRule.Name = "cmdRemoveHeadlineRule"
        Me.cmdRemoveHeadlineRule.Size = New System.Drawing.Size(22, 22)
        Me.cmdRemoveHeadlineRule.TabIndex = 24
        Me.cmdRemoveHeadlineRule.UseVisualStyleBackColor = True
        '
        'cmdHeadlineUp
        '
        Me.cmdHeadlineUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdHeadlineUp.Image = CType(resources.GetObject("cmdHeadlineUp.Image"), System.Drawing.Image)
        Me.cmdHeadlineUp.Location = New System.Drawing.Point(322, 108)
        Me.cmdHeadlineUp.Name = "cmdHeadlineUp"
        Me.cmdHeadlineUp.Size = New System.Drawing.Size(22, 22)
        Me.cmdHeadlineUp.TabIndex = 25
        Me.cmdHeadlineUp.UseVisualStyleBackColor = True
        '
        'cmdHeadlineDown
        '
        Me.cmdHeadlineDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdHeadlineDown.Image = CType(resources.GetObject("cmdHeadlineDown.Image"), System.Drawing.Image)
        Me.cmdHeadlineDown.Location = New System.Drawing.Point(322, 136)
        Me.cmdHeadlineDown.Name = "cmdHeadlineDown"
        Me.cmdHeadlineDown.Size = New System.Drawing.Size(22, 22)
        Me.cmdHeadlineDown.TabIndex = 26
        Me.cmdHeadlineDown.UseVisualStyleBackColor = True
        '
        'tpSpotlist
        '
        Me.tpSpotlist.Controls.Add(Me.TreeView1)
        Me.tpSpotlist.Controls.Add(Me.Button1)
        Me.tpSpotlist.Controls.Add(Me.Button2)
        Me.tpSpotlist.Controls.Add(Me.Button3)
        Me.tpSpotlist.Controls.Add(Me.Button4)
        Me.tpSpotlist.ImageKey = "failed"
        Me.tpSpotlist.Location = New System.Drawing.Point(4, 23)
        Me.tpSpotlist.Name = "tpSpotlist"
        Me.tpSpotlist.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSpotlist.Size = New System.Drawing.Size(572, 324)
        Me.tpSpotlist.TabIndex = 4
        Me.tpSpotlist.Tag = "Spotlist"
        Me.tpSpotlist.Text = "Spotlist"
        Me.tpSpotlist.UseVisualStyleBackColor = True
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(11, 28)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(323, 20)
        Me.txtName.TabIndex = 23
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 14)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Name"
        '
        'grpSheets
        '
        Me.grpSheets.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpSheets.Controls.Add(Me.chkSheets)
        Me.grpSheets.Controls.Add(Me.cmdRemoveSheet)
        Me.grpSheets.Controls.Add(Me.cmdAddSheet)
        Me.grpSheets.Controls.Add(Me.lstSheets)
        Me.grpSheets.Location = New System.Drawing.Point(3, 54)
        Me.grpSheets.Name = "grpSheets"
        Me.grpSheets.Size = New System.Drawing.Size(580, 100)
        Me.grpSheets.TabIndex = 28
        Me.grpSheets.TabStop = False
        Me.grpSheets.Text = "Possible sheet names"
        '
        'chkSheets
        '
        Me.chkSheets.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkSheets.Checked = True
        Me.chkSheets.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSheets.Location = New System.Drawing.Point(354, 34)
        Me.chkSheets.Name = "chkSheets"
        Me.chkSheets.Size = New System.Drawing.Size(203, 50)
        Me.chkSheets.TabIndex = 27
        Me.chkSheets.Text = "If none of these sheets are found, use the first sheet in the Workbook"
        Me.chkSheets.UseVisualStyleBackColor = True
        '
        'cmdRemoveSheet
        '
        Me.cmdRemoveSheet.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveSheet.Image = CType(resources.GetObject("cmdRemoveSheet.Image"), System.Drawing.Image)
        Me.cmdRemoveSheet.Location = New System.Drawing.Point(326, 47)
        Me.cmdRemoveSheet.Name = "cmdRemoveSheet"
        Me.cmdRemoveSheet.Size = New System.Drawing.Size(22, 22)
        Me.cmdRemoveSheet.TabIndex = 26
        Me.cmdRemoveSheet.UseVisualStyleBackColor = True
        '
        'cmdAddSheet
        '
        Me.cmdAddSheet.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddSheet.Image = CType(resources.GetObject("cmdAddSheet.Image"), System.Drawing.Image)
        Me.cmdAddSheet.Location = New System.Drawing.Point(326, 19)
        Me.cmdAddSheet.Name = "cmdAddSheet"
        Me.cmdAddSheet.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddSheet.TabIndex = 25
        Me.cmdAddSheet.UseVisualStyleBackColor = True
        '
        'lstSheets
        '
        Me.lstSheets.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstSheets.FormattingEnabled = True
        Me.lstSheets.ItemHeight = 14
        Me.lstSheets.Location = New System.Drawing.Point(6, 19)
        Me.lstSheets.Name = "lstSheets"
        Me.lstSheets.Size = New System.Drawing.Size(314, 74)
        Me.lstSheets.TabIndex = 0
        '
        'grpSchedule
        '
        Me.grpSchedule.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpSchedule.Controls.Add(Me.chkAutoValidate)
        Me.grpSchedule.Controls.Add(Me.cmdValidate)
        Me.grpSchedule.Controls.Add(Me.cmdLoadSchedule)
        Me.grpSchedule.Controls.Add(Me.grdTemplate)
        Me.grpSchedule.Controls.Add(Me.Label2)
        Me.grpSchedule.Controls.Add(Me.cmdBrowse)
        Me.grpSchedule.Controls.Add(Me.txtSchedule)
        Me.grpSchedule.Location = New System.Drawing.Point(0, 0)
        Me.grpSchedule.Name = "grpSchedule"
        Me.grpSchedule.Size = New System.Drawing.Size(561, 508)
        Me.grpSchedule.TabIndex = 27
        Me.grpSchedule.TabStop = False
        Me.grpSchedule.Text = "Schedule"
        '
        'chkAutoValidate
        '
        Me.chkAutoValidate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAutoValidate.AutoSize = True
        Me.chkAutoValidate.Checked = True
        Me.chkAutoValidate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAutoValidate.Location = New System.Drawing.Point(389, 475)
        Me.chkAutoValidate.Name = "chkAutoValidate"
        Me.chkAutoValidate.Size = New System.Drawing.Size(89, 18)
        Me.chkAutoValidate.TabIndex = 30
        Me.chkAutoValidate.Text = "Auto validate"
        Me.chkAutoValidate.UseVisualStyleBackColor = True
        '
        'cmdValidate
        '
        Me.cmdValidate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdValidate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdValidate.ImageIndex = 0
        Me.cmdValidate.ImageList = Me.imlIcons
        Me.cmdValidate.Location = New System.Drawing.Point(484, 464)
        Me.cmdValidate.Name = "cmdValidate"
        Me.cmdValidate.Size = New System.Drawing.Size(71, 38)
        Me.cmdValidate.TabIndex = 29
        Me.cmdValidate.Text = "Validate"
        Me.cmdValidate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdValidate.UseVisualStyleBackColor = True
        '
        'cmdLoadSchedule
        '
        Me.cmdLoadSchedule.Location = New System.Drawing.Point(296, 33)
        Me.cmdLoadSchedule.Name = "cmdLoadSchedule"
        Me.cmdLoadSchedule.Size = New System.Drawing.Size(75, 27)
        Me.cmdLoadSchedule.TabIndex = 28
        Me.cmdLoadSchedule.Text = "Load"
        Me.cmdLoadSchedule.UseVisualStyleBackColor = True
        '
        'grdTemplate
        '
        Me.grdTemplate.AllowUserToAddRows = False
        Me.grdTemplate.AllowUserToDeleteRows = False
        Me.grdTemplate.AllowUserToResizeRows = False
        Me.grdTemplate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdTemplate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTemplate.Location = New System.Drawing.Point(9, 66)
        Me.grdTemplate.Name = "grdTemplate"
        Me.grdTemplate.ReadOnly = True
        Me.grdTemplate.Size = New System.Drawing.Size(546, 392)
        Me.grdTemplate.TabIndex = 27
        Me.grdTemplate.VirtualMode = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 14)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Load schedule:"
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Location = New System.Drawing.Point(215, 33)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(75, 27)
        Me.cmdBrowse.TabIndex = 26
        Me.cmdBrowse.Text = "Browse"
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'txtSchedule
        '
        Me.txtSchedule.Location = New System.Drawing.Point(9, 36)
        Me.txtSchedule.Name = "txtSchedule"
        Me.txtSchedule.Size = New System.Drawing.Size(200, 20)
        Me.txtSchedule.TabIndex = 24
        '
        'cmbTemplates
        '
        Me.cmbTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplates.FormattingEnabled = True
        Me.cmbTemplates.Location = New System.Drawing.Point(6, 7)
        Me.cmbTemplates.Name = "cmbTemplates"
        Me.cmbTemplates.Size = New System.Drawing.Size(275, 22)
        Me.cmbTemplates.TabIndex = 20
        '
        'cmdDeleteTemplate
        '
        Me.cmdDeleteTemplate.Image = CType(resources.GetObject("cmdDeleteTemplate.Image"), System.Drawing.Image)
        Me.cmdDeleteTemplate.Location = New System.Drawing.Point(315, 7)
        Me.cmdDeleteTemplate.Name = "cmdDeleteTemplate"
        Me.cmdDeleteTemplate.Size = New System.Drawing.Size(22, 22)
        Me.cmdDeleteTemplate.TabIndex = 19
        Me.cmdDeleteTemplate.UseVisualStyleBackColor = True
        '
        'cmdAddTemplate
        '
        Me.cmdAddTemplate.Image = CType(resources.GetObject("cmdAddTemplate.Image"), System.Drawing.Image)
        Me.cmdAddTemplate.Location = New System.Drawing.Point(287, 7)
        Me.cmdAddTemplate.Name = "cmdAddTemplate"
        Me.cmdAddTemplate.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddTemplate.TabIndex = 18
        Me.cmdAddTemplate.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ListView1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1183, 552)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Test schedule"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Location = New System.Drawing.Point(6, 6)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(242, 418)
        Me.ListView1.TabIndex = 15
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'mnuEdit
        '
        Me.mnuEdit.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCopy, Me.mnuPaste})
        Me.mnuEdit.Name = "mnuEdit"
        Me.mnuEdit.Size = New System.Drawing.Size(113, 48)
        '
        'mnuCopy
        '
        Me.mnuCopy.Image = Global.ScheduleTemplateEditor.My.Resources.Resources.copy
        Me.mnuCopy.Name = "mnuCopy"
        Me.mnuCopy.Size = New System.Drawing.Size(112, 22)
        Me.mnuCopy.Text = "Copy"
        '
        'mnuPaste
        '
        Me.mnuPaste.Image = Global.ScheduleTemplateEditor.My.Resources.Resources.paste
        Me.mnuPaste.Name = "mnuPaste"
        Me.mnuPaste.Size = New System.Drawing.Size(112, 22)
        Me.mnuPaste.Text = "Paste"
        '
        'TreeView1
        '
        Me.TreeView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeView1.ImageIndex = 2
        Me.TreeView1.ImageList = Me.imlIcons
        Me.TreeView1.Location = New System.Drawing.Point(2, 6)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.SelectedImageIndex = 0
        Me.TreeView1.Size = New System.Drawing.Size(314, 106)
        Me.TreeView1.TabIndex = 27
        Me.TreeView1.Tag = "Channel"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(322, 6)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(22, 22)
        Me.Button1.TabIndex = 28
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(322, 34)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(22, 22)
        Me.Button2.TabIndex = 29
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.Location = New System.Drawing.Point(322, 62)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(22, 22)
        Me.Button3.TabIndex = 30
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.Location = New System.Drawing.Point(322, 90)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(22, 22)
        Me.Button4.TabIndex = 31
        Me.Button4.UseVisualStyleBackColor = True
        '
        'frmEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1191, 579)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmEditor"
        Me.Text = "Schedule template editor"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.spcTemplates.Panel1.ResumeLayout(False)
        Me.spcTemplates.Panel2.ResumeLayout(False)
        CType(Me.spcTemplates, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spcTemplates.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tabTemplate.ResumeLayout(False)
        Me.tpIdentify.ResumeLayout(False)
        Me.grpIdentifyRule.ResumeLayout(False)
        Me.grpIdentifyRule.PerformLayout()
        Me.tpTarget.ResumeLayout(False)
        Me.tpTarget.PerformLayout()
        Me.grpTargetRule.ResumeLayout(False)
        Me.grpTargetRule.PerformLayout()
        Me.tpContract.ResumeLayout(False)
        Me.tpContract.PerformLayout()
        Me.grpContractRule.ResumeLayout(False)
        Me.grpContractRule.PerformLayout()
        Me.tpChannel.ResumeLayout(False)
        Me.grpChannelRule.ResumeLayout(False)
        Me.grpChannelRule.PerformLayout()
        Me.tpColumns.ResumeLayout(False)
        Me.tpColumns.PerformLayout()
        Me.grpColumnsRule.ResumeLayout(False)
        Me.grpColumnsRule.PerformLayout()
        Me.tpSpotlist.ResumeLayout(False)
        Me.grpSheets.ResumeLayout(False)
        Me.grpSchedule.ResumeLayout(False)
        Me.grpSchedule.PerformLayout()
        CType(Me.grdTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.mnuEdit.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents imlIcons As System.Windows.Forms.ImageList
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents cmbTemplates As System.Windows.Forms.ComboBox
    Friend WithEvents cmdDeleteTemplate As System.Windows.Forms.Button
    Friend WithEvents cmdAddTemplate As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents grpIdentifyRule As System.Windows.Forms.GroupBox
    Friend WithEvents txtIdentifyFindToCol As System.Windows.Forms.TextBox
    Friend WithEvents lblIdentifyColumnTo As System.Windows.Forms.Label
    Friend WithEvents txtIdentifyFindFromCol As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtIdentifyFindToRow As System.Windows.Forms.TextBox
    Friend WithEvents lblIdentifyRowTo As System.Windows.Forms.Label
    Friend WithEvents txtIdentifyFindFromRow As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbIdentifyRule As System.Windows.Forms.ComboBox
    Friend WithEvents cmdIdentifyDown As System.Windows.Forms.Button
    Friend WithEvents cmdIdentifyUp As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveIdentifyRule As System.Windows.Forms.Button
    Friend WithEvents cmdAddIdentifyRule As System.Windows.Forms.Button
    Friend WithEvents tvwIdentify As System.Windows.Forms.TreeView
    Friend WithEvents cmdAddIdentifyRuleGroup As System.Windows.Forms.Button
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtIdentifySearchValue As System.Windows.Forms.TextBox
    Friend WithEvents cmbIdentifySearchType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSchedule As System.Windows.Forms.TextBox
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents cmdGetIdentify As System.Windows.Forms.Button
    Friend WithEvents grpSchedule As System.Windows.Forms.GroupBox
    Friend WithEvents cmdLoadSchedule As System.Windows.Forms.Button
    Friend WithEvents grdTemplate As System.Windows.Forms.DataGridView
    Friend WithEvents grpSheets As System.Windows.Forms.GroupBox
    Friend WithEvents chkSheets As System.Windows.Forms.CheckBox
    Friend WithEvents cmdRemoveSheet As System.Windows.Forms.Button
    Friend WithEvents cmdAddSheet As System.Windows.Forms.Button
    Friend WithEvents lstSheets As System.Windows.Forms.ListBox
    Friend WithEvents cmdValidate As System.Windows.Forms.Button
    Friend WithEvents chkAutoValidate As System.Windows.Forms.CheckBox
    Friend WithEvents mnuEdit As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPaste As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents spcTemplates As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grpTargetRule As System.Windows.Forms.GroupBox
    Friend WithEvents cmdGetTarget As System.Windows.Forms.Button
    Friend WithEvents txtTargetSearchValue As System.Windows.Forms.TextBox
    Friend WithEvents cmbTargetSearchType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTargetRule As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTargetFindFromCol As System.Windows.Forms.TextBox
    Friend WithEvents txtTargetFindFromRow As System.Windows.Forms.TextBox
    Friend WithEvents txtTargetFindToCol As System.Windows.Forms.TextBox
    Friend WithEvents lblTargetRowTo As System.Windows.Forms.Label
    Friend WithEvents lblTargetColumnTo As System.Windows.Forms.Label
    Friend WithEvents txtTargetFindToRow As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmdTargetDown As System.Windows.Forms.Button
    Friend WithEvents cmdTargetUp As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveTargetRule As System.Windows.Forms.Button
    Friend WithEvents cmdAddTargetRule As System.Windows.Forms.Button
    Friend WithEvents tvwTarget As System.Windows.Forms.TreeView
    Friend WithEvents grpContractRule As System.Windows.Forms.GroupBox
    Friend WithEvents cmdGetContract As System.Windows.Forms.Button
    Friend WithEvents txtContractSearchValue As System.Windows.Forms.TextBox
    Friend WithEvents cmbContractSearchType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbContractRule As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtContractFindFromCol As System.Windows.Forms.TextBox
    Friend WithEvents txtContractFindFromRow As System.Windows.Forms.TextBox
    Friend WithEvents txtContractFindToCol As System.Windows.Forms.TextBox
    Friend WithEvents lblContractRowTo As System.Windows.Forms.Label
    Friend WithEvents lblContractColumnTo As System.Windows.Forms.Label
    Friend WithEvents txtContractFindToRow As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmdContractDown As System.Windows.Forms.Button
    Friend WithEvents cmdContractUp As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveContractRule As System.Windows.Forms.Button
    Friend WithEvents cmdAddContractRule As System.Windows.Forms.Button
    Friend WithEvents tvwContract As System.Windows.Forms.TreeView
    Friend WithEvents grpChannelRule As System.Windows.Forms.GroupBox
    Friend WithEvents cmdGetChannel As System.Windows.Forms.Button
    Friend WithEvents txtChannelSearchValue As System.Windows.Forms.TextBox
    Friend WithEvents cmbChannelSearchType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbChannelRule As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtChannelFindFromCol As System.Windows.Forms.TextBox
    Friend WithEvents txtChannelFindFromRow As System.Windows.Forms.TextBox
    Friend WithEvents txtChannelFindToCol As System.Windows.Forms.TextBox
    Friend WithEvents lblChannelRowTo As System.Windows.Forms.Label
    Friend WithEvents lblChannelColumnTo As System.Windows.Forms.Label
    Friend WithEvents txtChannelFindToRow As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmdChannelDown As System.Windows.Forms.Button
    Friend WithEvents cmdChannelUp As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveChannelRule As System.Windows.Forms.Button
    Friend WithEvents cmdAddChannelRule As System.Windows.Forms.Button
    Friend WithEvents tvwChannel As System.Windows.Forms.TreeView
    Friend WithEvents chkContractRequired As System.Windows.Forms.CheckBox
    Friend WithEvents lblContractRequired As System.Windows.Forms.Label
    Friend WithEvents txtContractFallback As System.Windows.Forms.TextBox
    Friend WithEvents txtTargetFallback As System.Windows.Forms.TextBox
    Friend WithEvents lblTargetRequired As System.Windows.Forms.Label
    Friend WithEvents chkTargetRequired As System.Windows.Forms.CheckBox
    Friend WithEvents cmdAddHeadlineRow As System.Windows.Forms.Button
    Friend WithEvents grpColumnsRule As System.Windows.Forms.GroupBox
    Friend WithEvents cmdGetColumns As System.Windows.Forms.Button
    Friend WithEvents txtColumnsSearchValue As System.Windows.Forms.TextBox
    Friend WithEvents cmbColumnsSearchType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbColumnsRule As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtColumnsFindFromCol As System.Windows.Forms.TextBox
    Friend WithEvents txtColumnsFindFromRow As System.Windows.Forms.TextBox
    Friend WithEvents txtColumnsFindToCol As System.Windows.Forms.TextBox
    Friend WithEvents lblColumnsRowTo As System.Windows.Forms.Label
    Friend WithEvents lblColumnsColumnTo As System.Windows.Forms.Label
    Friend WithEvents txtColumnsFindToRow As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmdHeadlineDown As System.Windows.Forms.Button
    Friend WithEvents cmdHeadlineUp As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveHeadlineRule As System.Windows.Forms.Button
    Friend WithEvents cmdAddHeadlineRule As System.Windows.Forms.Button
    Friend WithEvents tvwColumns As System.Windows.Forms.TreeView
    Friend WithEvents tabTemplate As System.Windows.Forms.TabControl
    Friend WithEvents tpIdentify As System.Windows.Forms.TabPage
    Friend WithEvents tpTarget As System.Windows.Forms.TabPage
    Friend WithEvents tpContract As System.Windows.Forms.TabPage
    Friend WithEvents tpChannel As System.Windows.Forms.TabPage
    Friend WithEvents tpColumns As System.Windows.Forms.TabPage
    Friend WithEvents tpSpotlist As System.Windows.Forms.TabPage
    Friend WithEvents cmdAddHeadline As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveHeadline As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents tvwHeadlines As System.Windows.Forms.TreeView
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lstRequired As System.Windows.Forms.ListBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmdUnsetRequiredColumn As System.Windows.Forms.Button
    Friend WithEvents cmdSetRequiredColumn As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdLoad As System.Windows.Forms.Button
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button

End Class
