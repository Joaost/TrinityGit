<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQuestionaires
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmQuestionaires))
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbQuestionaire = New System.Windows.Forms.ComboBox
        Me.cmdAddQuestionaire = New System.Windows.Forms.Button
        Me.grpEntities = New System.Windows.Forms.GroupBox
        Me.cmdImport = New System.Windows.Forms.Button
        Me.cmdSaveTemplate = New System.Windows.Forms.Button
        Me.cmdOpenTemplate = New System.Windows.Forms.Button
        Me.cmdMoveDown = New System.Windows.Forms.Button
        Me.cmdMoveUp = New System.Windows.Forms.Button
        Me.cmdRemoveEntity = New System.Windows.Forms.Button
        Me.cmdAddEntity = New System.Windows.Forms.Button
        Me.tvwEntities = New System.Windows.Forms.TreeView
        Me.grpEntity = New System.Windows.Forms.GroupBox
        Me.pnlEntity = New System.Windows.Forms.Panel
        Me.grpGroup = New System.Windows.Forms.GroupBox
        Me.cmdCopy = New System.Windows.Forms.Button
        Me.cmdRemoveMember = New System.Windows.Forms.Button
        Me.cmdAddMember = New System.Windows.Forms.Button
        Me.Label12 = New System.Windows.Forms.Label
        Me.grdGroupMembers = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn3 = New Balthazar.ExtendedComboboxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colHeader = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colGroupValue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmbRequired = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.grpRating = New System.Windows.Forms.GroupBox
        Me.txtRight = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtLeft = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtValues = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.grpInputSinglechoice = New System.Windows.Forms.GroupBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.cmdRemoveChoice = New System.Windows.Forms.Button
        Me.cmdAddChoice = New System.Windows.Forms.Button
        Me.grdChoices = New System.Windows.Forms.DataGridView
        Me.colValue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colText = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.txtUnit = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbData = New System.Windows.Forms.ComboBox
        Me.txtText = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtEntityName = New System.Windows.Forms.TextBox
        Me.grpPreview = New System.Windows.Forms.GroupBox
        Me.wbPreview = New System.Windows.Forms.WebBrowser
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chkValidate = New System.Windows.Forms.CheckBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cntVertical = New System.Windows.Forms.SplitContainer
        Me.pnlHorizontal = New System.Windows.Forms.SplitContainer
        Me.ttpQuestionaires = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdRemoveQuestionaire = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmdPublish = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.grpEntities.SuspendLayout()
        Me.grpEntity.SuspendLayout()
        Me.pnlEntity.SuspendLayout()
        Me.grpGroup.SuspendLayout()
        CType(Me.grdGroupMembers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRating.SuspendLayout()
        Me.grpInputSinglechoice.SuspendLayout()
        CType(Me.grdChoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPreview.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.cntVertical.Panel1.SuspendLayout()
        Me.cntVertical.Panel2.SuspendLayout()
        Me.cntVertical.SuspendLayout()
        Me.pnlHorizontal.Panel1.SuspendLayout()
        Me.pnlHorizontal.Panel2.SuspendLayout()
        Me.pnlHorizontal.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Questionaire"
        '
        'cmbQuestionaire
        '
        Me.cmbQuestionaire.DisplayMember = "Name"
        Me.cmbQuestionaire.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbQuestionaire.FormattingEnabled = True
        Me.cmbQuestionaire.Location = New System.Drawing.Point(12, 26)
        Me.cmbQuestionaire.Name = "cmbQuestionaire"
        Me.cmbQuestionaire.Size = New System.Drawing.Size(165, 22)
        Me.cmbQuestionaire.TabIndex = 1
        '
        'cmdAddQuestionaire
        '
        Me.cmdAddQuestionaire.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddQuestionaire.Location = New System.Drawing.Point(183, 25)
        Me.cmdAddQuestionaire.Name = "cmdAddQuestionaire"
        Me.cmdAddQuestionaire.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddQuestionaire.TabIndex = 12
        Me.ttpQuestionaires.SetToolTip(Me.cmdAddQuestionaire, "Add questionaire")
        Me.cmdAddQuestionaire.UseVisualStyleBackColor = True
        '
        'grpEntities
        '
        Me.grpEntities.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpEntities.Controls.Add(Me.cmdImport)
        Me.grpEntities.Controls.Add(Me.cmdSaveTemplate)
        Me.grpEntities.Controls.Add(Me.cmdOpenTemplate)
        Me.grpEntities.Controls.Add(Me.cmdMoveDown)
        Me.grpEntities.Controls.Add(Me.cmdMoveUp)
        Me.grpEntities.Controls.Add(Me.cmdRemoveEntity)
        Me.grpEntities.Controls.Add(Me.cmdAddEntity)
        Me.grpEntities.Controls.Add(Me.tvwEntities)
        Me.grpEntities.Location = New System.Drawing.Point(3, 79)
        Me.grpEntities.Name = "grpEntities"
        Me.grpEntities.Size = New System.Drawing.Size(434, 222)
        Me.grpEntities.TabIndex = 13
        Me.grpEntities.TabStop = False
        Me.grpEntities.Text = "Questionaire entities"
        '
        'cmdImport
        '
        Me.cmdImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdImport.Enabled = False
        Me.cmdImport.Image = Global.Balthazar.My.Resources.Resources.import1
        Me.cmdImport.Location = New System.Drawing.Point(404, 215)
        Me.cmdImport.Name = "cmdImport"
        Me.cmdImport.Size = New System.Drawing.Size(24, 24)
        Me.cmdImport.TabIndex = 19
        Me.ttpQuestionaires.SetToolTip(Me.cmdImport, "Import questionaire from another event")
        Me.cmdImport.UseVisualStyleBackColor = True
        '
        'cmdSaveTemplate
        '
        Me.cmdSaveTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSaveTemplate.Image = Global.Balthazar.My.Resources.Resources.disk_blue
        Me.cmdSaveTemplate.Location = New System.Drawing.Point(404, 185)
        Me.cmdSaveTemplate.Name = "cmdSaveTemplate"
        Me.cmdSaveTemplate.Size = New System.Drawing.Size(24, 24)
        Me.cmdSaveTemplate.TabIndex = 18
        Me.ttpQuestionaires.SetToolTip(Me.cmdSaveTemplate, "Save as questionaire template")
        Me.cmdSaveTemplate.UseVisualStyleBackColor = True
        '
        'cmdOpenTemplate
        '
        Me.cmdOpenTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOpenTemplate.Image = Global.Balthazar.My.Resources.Resources.folder
        Me.cmdOpenTemplate.Location = New System.Drawing.Point(404, 155)
        Me.cmdOpenTemplate.Name = "cmdOpenTemplate"
        Me.cmdOpenTemplate.Size = New System.Drawing.Size(24, 24)
        Me.cmdOpenTemplate.TabIndex = 17
        Me.ttpQuestionaires.SetToolTip(Me.cmdOpenTemplate, "Open questionaire template")
        Me.cmdOpenTemplate.UseVisualStyleBackColor = True
        '
        'cmdMoveDown
        '
        Me.cmdMoveDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdMoveDown.Image = Global.Balthazar.My.Resources.Resources.navigate_down
        Me.cmdMoveDown.Location = New System.Drawing.Point(404, 109)
        Me.cmdMoveDown.Name = "cmdMoveDown"
        Me.cmdMoveDown.Size = New System.Drawing.Size(24, 24)
        Me.cmdMoveDown.TabIndex = 16
        Me.ttpQuestionaires.SetToolTip(Me.cmdMoveDown, "Move entity down")
        Me.cmdMoveDown.UseVisualStyleBackColor = True
        '
        'cmdMoveUp
        '
        Me.cmdMoveUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdMoveUp.Image = Global.Balthazar.My.Resources.Resources.navigate_up
        Me.cmdMoveUp.Location = New System.Drawing.Point(404, 79)
        Me.cmdMoveUp.Name = "cmdMoveUp"
        Me.cmdMoveUp.Size = New System.Drawing.Size(24, 24)
        Me.cmdMoveUp.TabIndex = 15
        Me.ttpQuestionaires.SetToolTip(Me.cmdMoveUp, "Move entity up")
        Me.cmdMoveUp.UseVisualStyleBackColor = True
        '
        'cmdRemoveEntity
        '
        Me.cmdRemoveEntity.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveEntity.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveEntity.Location = New System.Drawing.Point(404, 49)
        Me.cmdRemoveEntity.Name = "cmdRemoveEntity"
        Me.cmdRemoveEntity.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveEntity.TabIndex = 14
        Me.ttpQuestionaires.SetToolTip(Me.cmdRemoveEntity, "Remove entity")
        Me.cmdRemoveEntity.UseVisualStyleBackColor = True
        '
        'cmdAddEntity
        '
        Me.cmdAddEntity.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddEntity.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddEntity.Location = New System.Drawing.Point(404, 19)
        Me.cmdAddEntity.Name = "cmdAddEntity"
        Me.cmdAddEntity.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddEntity.TabIndex = 13
        Me.ttpQuestionaires.SetToolTip(Me.cmdAddEntity, "Add entity")
        Me.cmdAddEntity.UseVisualStyleBackColor = True
        '
        'tvwEntities
        '
        Me.tvwEntities.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwEntities.Location = New System.Drawing.Point(8, 19)
        Me.tvwEntities.Name = "tvwEntities"
        Me.tvwEntities.Size = New System.Drawing.Size(390, 197)
        Me.tvwEntities.TabIndex = 0
        '
        'grpEntity
        '
        Me.grpEntity.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpEntity.Controls.Add(Me.pnlEntity)
        Me.grpEntity.Location = New System.Drawing.Point(3, 3)
        Me.grpEntity.Name = "grpEntity"
        Me.grpEntity.Size = New System.Drawing.Size(434, 248)
        Me.grpEntity.TabIndex = 1
        Me.grpEntity.TabStop = False
        Me.grpEntity.Text = "Entity:"
        '
        'pnlEntity
        '
        Me.pnlEntity.AutoScroll = True
        Me.pnlEntity.Controls.Add(Me.grpGroup)
        Me.pnlEntity.Controls.Add(Me.cmbRequired)
        Me.pnlEntity.Controls.Add(Me.Label11)
        Me.pnlEntity.Controls.Add(Me.grpRating)
        Me.pnlEntity.Controls.Add(Me.grpInputSinglechoice)
        Me.pnlEntity.Controls.Add(Me.txtUnit)
        Me.pnlEntity.Controls.Add(Me.Label9)
        Me.pnlEntity.Controls.Add(Me.Label3)
        Me.pnlEntity.Controls.Add(Me.cmbData)
        Me.pnlEntity.Controls.Add(Me.txtText)
        Me.pnlEntity.Controls.Add(Me.Label5)
        Me.pnlEntity.Controls.Add(Me.Label4)
        Me.pnlEntity.Controls.Add(Me.txtEntityName)
        Me.pnlEntity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlEntity.Location = New System.Drawing.Point(3, 16)
        Me.pnlEntity.Name = "pnlEntity"
        Me.pnlEntity.Size = New System.Drawing.Size(428, 229)
        Me.pnlEntity.TabIndex = 0
        '
        'grpGroup
        '
        Me.grpGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpGroup.Controls.Add(Me.cmdCopy)
        Me.grpGroup.Controls.Add(Me.cmdRemoveMember)
        Me.grpGroup.Controls.Add(Me.cmdAddMember)
        Me.grpGroup.Controls.Add(Me.Label12)
        Me.grpGroup.Controls.Add(Me.grdGroupMembers)
        Me.grpGroup.Location = New System.Drawing.Point(0, 83)
        Me.grpGroup.Name = "grpGroup"
        Me.grpGroup.Size = New System.Drawing.Size(422, 143)
        Me.grpGroup.TabIndex = 11
        Me.grpGroup.TabStop = False
        Me.grpGroup.Text = "Group"
        Me.grpGroup.Visible = False
        '
        'cmdCopy
        '
        Me.cmdCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCopy.Image = Global.Balthazar.My.Resources.Resources.copy
        Me.cmdCopy.Location = New System.Drawing.Point(392, 93)
        Me.cmdCopy.Name = "cmdCopy"
        Me.cmdCopy.Size = New System.Drawing.Size(24, 24)
        Me.cmdCopy.TabIndex = 16
        Me.ttpQuestionaires.SetToolTip(Me.cmdCopy, "Copy group above")
        Me.cmdCopy.UseVisualStyleBackColor = True
        '
        'cmdRemoveMember
        '
        Me.cmdRemoveMember.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveMember.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveMember.Location = New System.Drawing.Point(392, 63)
        Me.cmdRemoveMember.Name = "cmdRemoveMember"
        Me.cmdRemoveMember.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveMember.TabIndex = 16
        Me.ttpQuestionaires.SetToolTip(Me.cmdRemoveMember, "Remove member")
        Me.cmdRemoveMember.UseVisualStyleBackColor = True
        '
        'cmdAddMember
        '
        Me.cmdAddMember.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddMember.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddMember.Location = New System.Drawing.Point(392, 33)
        Me.cmdAddMember.Name = "cmdAddMember"
        Me.cmdAddMember.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddMember.TabIndex = 15
        Me.ttpQuestionaires.SetToolTip(Me.cmdAddMember, "Add member")
        Me.cmdAddMember.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(51, 14)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Members"
        '
        'grdGroupMembers
        '
        Me.grdGroupMembers.AllowUserToAddRows = False
        Me.grdGroupMembers.AllowUserToDeleteRows = False
        Me.grdGroupMembers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdGroupMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdGroupMembers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.colHeader, Me.colGroupValue})
        Me.grdGroupMembers.Location = New System.Drawing.Point(6, 33)
        Me.grdGroupMembers.Name = "grdGroupMembers"
        Me.grdGroupMembers.RowHeadersVisible = False
        Me.grdGroupMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdGroupMembers.Size = New System.Drawing.Size(382, 104)
        Me.grdGroupMembers.TabIndex = 1
        Me.grdGroupMembers.VirtualMode = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Type"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn4.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'colHeader
        '
        Me.colHeader.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colHeader.HeaderText = "Header"
        Me.colHeader.Name = "colHeader"
        '
        'colGroupValue
        '
        Me.colGroupValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colGroupValue.HeaderText = "Value"
        Me.colGroupValue.Name = "colGroupValue"
        '
        'cmbRequired
        '
        Me.cmbRequired.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbRequired.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRequired.FormattingEnabled = True
        Me.cmbRequired.Items.AddRange(New Object() {"Inherit", "Yes", "No"})
        Me.cmbRequired.Location = New System.Drawing.Point(324, 17)
        Me.cmbRequired.Name = "cmbRequired"
        Me.cmbRequired.Size = New System.Drawing.Size(86, 22)
        Me.cmbRequired.TabIndex = 10
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(318, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(50, 14)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "Required"
        '
        'grpRating
        '
        Me.grpRating.Controls.Add(Me.txtRight)
        Me.grpRating.Controls.Add(Me.Label8)
        Me.grpRating.Controls.Add(Me.txtLeft)
        Me.grpRating.Controls.Add(Me.Label7)
        Me.grpRating.Controls.Add(Me.txtValues)
        Me.grpRating.Controls.Add(Me.Label6)
        Me.grpRating.Location = New System.Drawing.Point(0, 83)
        Me.grpRating.Name = "grpRating"
        Me.grpRating.Size = New System.Drawing.Size(422, 69)
        Me.grpRating.TabIndex = 7
        Me.grpRating.TabStop = False
        Me.grpRating.Text = "Input: Rating"
        Me.grpRating.Visible = False
        '
        'txtRight
        '
        Me.txtRight.Location = New System.Drawing.Point(225, 33)
        Me.txtRight.Name = "txtRight"
        Me.txtRight.Size = New System.Drawing.Size(99, 20)
        Me.txtRight.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(228, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 14)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Right help text"
        '
        'txtLeft
        '
        Me.txtLeft.Location = New System.Drawing.Point(108, 33)
        Me.txtLeft.Name = "txtLeft"
        Me.txtLeft.Size = New System.Drawing.Size(111, 20)
        Me.txtLeft.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(111, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 14)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Left help text"
        '
        'txtValues
        '
        Me.txtValues.Location = New System.Drawing.Point(3, 33)
        Me.txtValues.Name = "txtValues"
        Me.txtValues.Size = New System.Drawing.Size(99, 20)
        Me.txtValues.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 14)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Values"
        '
        'grpInputSinglechoice
        '
        Me.grpInputSinglechoice.Controls.Add(Me.Label10)
        Me.grpInputSinglechoice.Controls.Add(Me.cmdRemoveChoice)
        Me.grpInputSinglechoice.Controls.Add(Me.cmdAddChoice)
        Me.grpInputSinglechoice.Controls.Add(Me.grdChoices)
        Me.grpInputSinglechoice.Location = New System.Drawing.Point(0, 83)
        Me.grpInputSinglechoice.Name = "grpInputSinglechoice"
        Me.grpInputSinglechoice.Size = New System.Drawing.Size(422, 129)
        Me.grpInputSinglechoice.TabIndex = 8
        Me.grpInputSinglechoice.TabStop = False
        Me.grpInputSinglechoice.Text = "Input: Singlechoice"
        Me.grpInputSinglechoice.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(46, 14)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Choices"
        '
        'cmdRemoveChoice
        '
        Me.cmdRemoveChoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveChoice.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveChoice.Location = New System.Drawing.Point(391, 63)
        Me.cmdRemoveChoice.Name = "cmdRemoveChoice"
        Me.cmdRemoveChoice.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveChoice.TabIndex = 16
        Me.ttpQuestionaires.SetToolTip(Me.cmdRemoveChoice, "Remove choice")
        Me.cmdRemoveChoice.UseVisualStyleBackColor = True
        '
        'cmdAddChoice
        '
        Me.cmdAddChoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddChoice.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddChoice.Location = New System.Drawing.Point(392, 33)
        Me.cmdAddChoice.Name = "cmdAddChoice"
        Me.cmdAddChoice.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddChoice.TabIndex = 15
        Me.ttpQuestionaires.SetToolTip(Me.cmdAddChoice, "Add choice")
        Me.cmdAddChoice.UseVisualStyleBackColor = True
        '
        'grdChoices
        '
        Me.grdChoices.AllowUserToAddRows = False
        Me.grdChoices.AllowUserToDeleteRows = False
        Me.grdChoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChoices.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colValue, Me.colText})
        Me.grdChoices.Location = New System.Drawing.Point(5, 33)
        Me.grdChoices.Name = "grdChoices"
        Me.grdChoices.RowHeadersVisible = False
        Me.grdChoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdChoices.Size = New System.Drawing.Size(380, 90)
        Me.grdChoices.TabIndex = 0
        Me.grdChoices.VirtualMode = True
        '
        'colValue
        '
        Me.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colValue.HeaderText = "Value"
        Me.colValue.Name = "colValue"
        '
        'colText
        '
        Me.colText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colText.HeaderText = "Text"
        Me.colText.Name = "colText"
        '
        'txtUnit
        '
        Me.txtUnit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUnit.Location = New System.Drawing.Point(324, 57)
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.Size = New System.Drawing.Size(52, 20)
        Me.txtUnit.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(327, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(25, 14)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Unit"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 14)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Text"
        '
        'cmbData
        '
        Me.cmbData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbData.FormattingEnabled = True
        Me.cmbData.Items.AddRange(New Object() {"<none>", "Location", "City", "Contact", "Salesman", "Date", "Week", "Month", "Weekday"})
        Me.cmbData.Location = New System.Drawing.Point(177, 57)
        Me.cmbData.Name = "cmbData"
        Me.cmbData.Size = New System.Drawing.Size(141, 22)
        Me.cmbData.TabIndex = 5
        '
        'txtText
        '
        Me.txtText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtText.Location = New System.Drawing.Point(0, 17)
        Me.txtText.Name = "txtText"
        Me.txtText.Size = New System.Drawing.Size(318, 20)
        Me.txtText.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(177, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 14)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Prefilled data"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 14)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Unique name"
        '
        'txtEntityName
        '
        Me.txtEntityName.Location = New System.Drawing.Point(0, 57)
        Me.txtEntityName.Name = "txtEntityName"
        Me.txtEntityName.Size = New System.Drawing.Size(171, 20)
        Me.txtEntityName.TabIndex = 3
        '
        'grpPreview
        '
        Me.grpPreview.Controls.Add(Me.wbPreview)
        Me.grpPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpPreview.Location = New System.Drawing.Point(0, 0)
        Me.grpPreview.Name = "grpPreview"
        Me.grpPreview.Size = New System.Drawing.Size(577, 562)
        Me.grpPreview.TabIndex = 14
        Me.grpPreview.TabStop = False
        Me.grpPreview.Text = "Preview"
        '
        'wbPreview
        '
        Me.wbPreview.AllowWebBrowserDrop = False
        Me.wbPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wbPreview.Location = New System.Drawing.Point(3, 16)
        Me.wbPreview.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbPreview.Name = "wbPreview"
        Me.wbPreview.ScriptErrorsSuppressed = True
        Me.wbPreview.Size = New System.Drawing.Size(571, 543)
        Me.wbPreview.TabIndex = 0
        Me.wbPreview.WebBrowserShortcutsEnabled = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.chkValidate)
        Me.GroupBox2.Controls.Add(Me.txtName)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(434, 70)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Questionaire"
        '
        'chkValidate
        '
        Me.chkValidate.AutoSize = True
        Me.chkValidate.Location = New System.Drawing.Point(216, 35)
        Me.chkValidate.Name = "chkValidate"
        Me.chkValidate.Size = New System.Drawing.Size(175, 18)
        Me.chkValidate.TabIndex = 2
        Me.chkValidate.Text = "Default all inputs to be required"
        Me.chkValidate.UseVisualStyleBackColor = True
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(6, 33)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(204, 20)
        Me.txtName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Name"
        '
        'cntVertical
        '
        Me.cntVertical.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cntVertical.Location = New System.Drawing.Point(12, 54)
        Me.cntVertical.Name = "cntVertical"
        '
        'cntVertical.Panel1
        '
        Me.cntVertical.Panel1.Controls.Add(Me.pnlHorizontal)
        '
        'cntVertical.Panel2
        '
        Me.cntVertical.Panel2.Controls.Add(Me.grpPreview)
        Me.cntVertical.Size = New System.Drawing.Size(1021, 562)
        Me.cntVertical.SplitterDistance = 440
        Me.cntVertical.TabIndex = 16
        '
        'pnlHorizontal
        '
        Me.pnlHorizontal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlHorizontal.Location = New System.Drawing.Point(0, 0)
        Me.pnlHorizontal.Name = "pnlHorizontal"
        Me.pnlHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'pnlHorizontal.Panel1
        '
        Me.pnlHorizontal.Panel1.Controls.Add(Me.grpEntities)
        Me.pnlHorizontal.Panel1.Controls.Add(Me.GroupBox2)
        '
        'pnlHorizontal.Panel2
        '
        Me.pnlHorizontal.Panel2.Controls.Add(Me.grpEntity)
        Me.pnlHorizontal.Size = New System.Drawing.Size(440, 562)
        Me.pnlHorizontal.SplitterDistance = 304
        Me.pnlHorizontal.TabIndex = 0
        '
        'cmdRemoveQuestionaire
        '
        Me.cmdRemoveQuestionaire.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveQuestionaire.Location = New System.Drawing.Point(213, 25)
        Me.cmdRemoveQuestionaire.Name = "cmdRemoveQuestionaire"
        Me.cmdRemoveQuestionaire.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveQuestionaire.TabIndex = 17
        Me.ttpQuestionaires.SetToolTip(Me.cmdRemoveQuestionaire, "Remove questionaire")
        Me.cmdRemoveQuestionaire.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.HeaderText = "Text"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'cmdPublish
        '
        Me.cmdPublish.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPublish.Image = Global.Balthazar.My.Resources.Resources.environment
        Me.cmdPublish.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPublish.Location = New System.Drawing.Point(963, 12)
        Me.cmdPublish.Name = "cmdPublish"
        Me.cmdPublish.Size = New System.Drawing.Size(67, 36)
        Me.cmdPublish.TabIndex = 18
        Me.cmdPublish.Text = "Publish"
        Me.cmdPublish.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdPublish.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Image = Global.Balthazar.My.Resources.Resources.table_preferences
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(880, 13)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(77, 36)
        Me.Button1.TabIndex = 19
        Me.Button1.Text = "Summary"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmQuestionaires
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1045, 629)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmdPublish)
        Me.Controls.Add(Me.cmdRemoveQuestionaire)
        Me.Controls.Add(Me.cntVertical)
        Me.Controls.Add(Me.cmdAddQuestionaire)
        Me.Controls.Add(Me.cmbQuestionaire)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmQuestionaires"
        Me.Text = "Questionaires"
        Me.grpEntities.ResumeLayout(False)
        Me.grpEntity.ResumeLayout(False)
        Me.pnlEntity.ResumeLayout(False)
        Me.pnlEntity.PerformLayout()
        Me.grpGroup.ResumeLayout(False)
        Me.grpGroup.PerformLayout()
        CType(Me.grdGroupMembers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRating.ResumeLayout(False)
        Me.grpRating.PerformLayout()
        Me.grpInputSinglechoice.ResumeLayout(False)
        Me.grpInputSinglechoice.PerformLayout()
        CType(Me.grdChoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPreview.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.cntVertical.Panel1.ResumeLayout(False)
        Me.cntVertical.Panel2.ResumeLayout(False)
        Me.cntVertical.ResumeLayout(False)
        Me.pnlHorizontal.Panel1.ResumeLayout(False)
        Me.pnlHorizontal.Panel2.ResumeLayout(False)
        Me.pnlHorizontal.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbQuestionaire As System.Windows.Forms.ComboBox
    Friend WithEvents cmdAddQuestionaire As System.Windows.Forms.Button
    Friend WithEvents grpEntities As System.Windows.Forms.GroupBox
    Friend WithEvents grpPreview As System.Windows.Forms.GroupBox
    Friend WithEvents wbPreview As System.Windows.Forms.WebBrowser
    Friend WithEvents tvwEntities As System.Windows.Forms.TreeView
    Friend WithEvents cmdAddEntity As System.Windows.Forms.Button
    Friend WithEvents grpEntity As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemoveEntity As System.Windows.Forms.Button
    Friend WithEvents cmdMoveDown As System.Windows.Forms.Button
    Friend WithEvents cmdMoveUp As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkValidate As System.Windows.Forms.CheckBox
    Friend WithEvents cmdSaveTemplate As System.Windows.Forms.Button
    Friend WithEvents cmdOpenTemplate As System.Windows.Forms.Button
    Friend WithEvents txtText As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEntityName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbData As System.Windows.Forms.ComboBox
    Friend WithEvents cntVertical As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlHorizontal As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlEntity As System.Windows.Forms.Panel
    Friend WithEvents cmdImport As System.Windows.Forms.Button
    Friend WithEvents ttpQuestionaires As System.Windows.Forms.ToolTip
    Friend WithEvents grpRating As System.Windows.Forms.GroupBox
    Friend WithEvents txtValues As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtRight As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtLeft As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtUnit As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents grpInputSinglechoice As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmdRemoveChoice As System.Windows.Forms.Button
    Friend WithEvents cmdAddChoice As System.Windows.Forms.Button
    Friend WithEvents grdChoices As System.Windows.Forms.DataGridView
    Friend WithEvents colValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colText As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdRemoveQuestionaire As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmbRequired As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents grpGroup As System.Windows.Forms.GroupBox
    Friend WithEvents grdGroupMembers As System.Windows.Forms.DataGridView
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn3 As Balthazar.ExtendedComboboxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colHeader As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colGroupValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdRemoveMember As System.Windows.Forms.Button
    Friend WithEvents cmdAddMember As System.Windows.Forms.Button
    Friend WithEvents cmdPublish As System.Windows.Forms.Button
    Friend WithEvents cmdCopy As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
