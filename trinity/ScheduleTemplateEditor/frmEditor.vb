Public Class frmEditor

    Private _clipboard As Object

#Region "Shared subs and functions"

    Private Sub mnuCopy_Click(sender As System.Object, e As System.EventArgs) Handles mnuCopy.Click
        Dim _view As TreeView = mnuEdit.Tag
        If _view.SelectedNode Is Nothing Then Exit Sub
        _clipboard = _view.SelectedNode.Tag
    End Sub

    Private Sub mnuPaste_Click(sender As System.Object, e As System.EventArgs) Handles mnuPaste.Click
        Dim _view As TreeView = mnuEdit.Tag
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        If TryCast(_clipboard, cRule) IsNot Nothing Then
            If TryCast(Rules(_view.Tag), cMutableRuleList) IsNot Nothing Then
                DirectCast(Rules(_view.Tag), cMutableRuleList).Add(_clipboard.Clone(_template))
            End If
            UpdateNodes(_view, Rules(_view.Tag))
        Else
            Dim _search As cSearch = _clipboard
            If _view.SelectedNode Is Nothing Then
                If _view.Nodes.Count = 0 Then
                    Dim _rule As New cRule(_template)
                    With _view.Nodes.Add("Rules")
                        .Tag = _rule
                        .SelectedImageKey = "unknown"
                        .ImageKey = "unknown"
                    End With
                    If TryCast(Rules(_view.Tag), cMutableRuleList) IsNot Nothing Then
                        DirectCast(Rules(_view.Tag), cMutableRuleList).Add(_rule)
                    End If
                End If
                _search.Clone(_view.Nodes(_view.Nodes.Count - 1).Tag, _template)
            ElseIf TryCast(_view.SelectedNode.Tag, cRule) IsNot Nothing Then
                _search.Clone(_view.SelectedNode.Tag, _template)
            Else
                If _search Is DirectCast(_view.SelectedNode.Tag, cSearch) Then
                    Windows.Forms.MessageBox.Show("You can not paste a node to itself.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                _search.Clone(_view.SelectedNode.Tag.Rule, _template, DirectCast(_view.SelectedNode.Tag, cSearch))
            End If
            UpdateNodes(_view, Rules(_view.Tag))
            End If
        AutoValidateSchedule()
    End Sub

    Private Sub mnuEdit_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles mnuEdit.Opening
        Dim _view As TreeView = mnuEdit.Tag
        mnuPaste.Enabled = Not _clipboard Is Nothing
        mnuCopy.Enabled = Not _view.SelectedNode Is Nothing
    End Sub

    Sub ValidateSchedule()
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        If _template Is Nothing Then Exit Sub
        Dim _schedule As cSchedule = grdTemplate.Tag

        _template.Validate(_schedule)

        SetTabIcons()
        UpdateNodeIcons()
        grdTemplate.Invalidate()

    End Sub


    Sub SetTabIcon(TabPage As TabPage, RuleList As cRuleList)
        If RuleList.ValidationResult Is Nothing Then
            TabPage.ImageKey = "unknown"
        ElseIf RuleList.ValidationResult.Succeeded Then
            TabPage.ImageKey = "ok"
        Else
            TabPage.ImageKey = "failed"
        End If
    End Sub

    Sub AutoValidateSchedule()
        If chkAutoValidate.Checked Then
            ValidateSchedule()
        End If
    End Sub

    Function GetSelectedTreeView() As TreeView
        For Each _view As TreeView In GetAll(GetType(TreeView))
            If _view.Focused Then Return _view
        Next
        Return Nothing
    End Function

    Private Function FindControl(name As String) As Control
        If name.Length = 0 OrElse Controls.Find(name, True).Length = 0 Then
            Return Nothing
        End If

        Return Controls.Find(name, True)(0)
    End Function

    Public Function GetAll(type As Type, Optional control As Control = Nothing) As IEnumerable(Of Control)
        If control Is Nothing Then control = Me
        Dim FindControl = control.Controls.Cast(Of Control)()

        Return FindControl.SelectMany(Function(ctrl) GetAll(type, ctrl)).Concat(FindControl).Where(Function(c) c.[GetType]() = type)
    End Function

    Sub SelectRule(ViewIdentifier As String)
        Dim _view As TreeView = FindControl("tvw" & ViewIdentifier)
        If _view.SelectedNode Is Nothing OrElse TryCast(_view.SelectedNode.Tag, cSearch) Is Nothing Then Exit Sub
        Dim _search As cSearch = _view.SelectedNode.Tag
        _search.RuleType = DirectCast(FindControl("cmb" & ViewIdentifier & "Rule"), ComboBox).SelectedIndex

        Select Case _search.RuleType
            Case cSearch.RuleTypeEnum.Check

                DirectCast(FindControl("txt" & ViewIdentifier & "FindFromCol"), Control).Visible = True
                DirectCast(FindControl("txt" & ViewIdentifier & "FindFromRow"), Control).Visible = True
                DirectCast(FindControl("txt" & ViewIdentifier & "FindToCol"), Control).Visible = False
                DirectCast(FindControl("txt" & ViewIdentifier & "FindToRow"), Control).Visible = False
                DirectCast(FindControl("lbl" & ViewIdentifier & "ColumnTo"), Control).Visible = False
                DirectCast(FindControl("lbl" & ViewIdentifier & "RowTo"), Control).Visible = False
                DirectCast(FindControl("txt" & ViewIdentifier & "SearchValue"), Control).Visible = True
                DirectCast(FindControl("cmb" & ViewIdentifier & "SearchType"), Control).Visible = True
            Case cSearch.RuleTypeEnum.Find
                DirectCast(FindControl("txt" & ViewIdentifier & "FindFromCol"), Control).Visible = True
                DirectCast(FindControl("txt" & ViewIdentifier & "FindFromRow"), Control).Visible = True
                DirectCast(FindControl("txt" & ViewIdentifier & "FindToCol"), Control).Visible = True
                DirectCast(FindControl("txt" & ViewIdentifier & "FindToRow"), Control).Visible = True
                DirectCast(FindControl("lbl" & ViewIdentifier & "ColumnTo"), Control).Visible = True
                DirectCast(FindControl("lbl" & ViewIdentifier & "RowTo"), Control).Visible = True
                DirectCast(FindControl("txt" & ViewIdentifier & "SearchValue"), Control).Visible = True
                DirectCast(FindControl("cmb" & ViewIdentifier & "SearchType"), Control).Visible = True
            Case cSearch.RuleTypeEnum.Step
                DirectCast(FindControl("txt" & ViewIdentifier & "FindFromCol"), Control).Visible = True
                DirectCast(FindControl("txt" & ViewIdentifier & "FindFromRow"), Control).Visible = True
                DirectCast(FindControl("txt" & ViewIdentifier & "FindToCol"), Control).Visible = False
                DirectCast(FindControl("txt" & ViewIdentifier & "FindToRow"), Control).Visible = False
                DirectCast(FindControl("lbl" & ViewIdentifier & "ColumnTo"), Control).Visible = False
                DirectCast(FindControl("lbl" & ViewIdentifier & "RowTo"), Control).Visible = False
                DirectCast(FindControl("txt" & ViewIdentifier & "SearchValue"), Control).Visible = False
                DirectCast(FindControl("cmb" & ViewIdentifier & "SearchType"), Control).Visible = False
        End Select

        _view.SelectedNode.Text = _search.ToString
    End Sub

    Sub UpdateNodeIcons()

        Dim _node As TreeNode
        For Each _view As TreeView In GetAll(GetType(TreeView))
            If _view.Tag IsNot Nothing AndAlso _view.Tag <> "" Then
                If _view.Nodes.Count > 0 Then

                    _node = _view.Nodes(0)

                    While _node IsNot Nothing
                        Dim _res As cSearchResult
                        If TryCast(_node.Tag, cSearch) IsNot Nothing Then
                            _res = DirectCast(_node.Tag, cSearch).Result

                        Else
                            _res = DirectCast(_node.Tag, cRule).ValidationResult
                        End If
                        If _res Is Nothing Then
                            _node.ImageKey = "unknown"
                        ElseIf _res.Succeeded Then
                            _node.ImageKey = "ok"
                        Else
                            _node.ImageKey = "failed"
                        End If
                        _node.SelectedImageKey = _node.ImageKey
                        _node.Text = _node.Tag.ToString
                        _node = _node.NextVisibleNode
                    End While
                End If
            End If
        Next
        For Each _node In tvwHeadlines.Nodes
            Dim _res As cSearchResult = _node.Tag.ValidationResult
            If _res Is Nothing Then
                _node.ImageKey = "unknown"
            ElseIf _res.Succeeded Then
                _node.ImageKey = "ok"
            Else
                _node.ImageKey = "failed"
            End If
            For Each _headlineNode As TreeNode In _node.Nodes
                If _res Is Nothing Then
                    _headlineNode.ImageKey = _node.ImageKey
                ElseIf _res.Succeeded AndAlso _res.Result.ToUpper = DirectCast(_node.Tag, cColumnHeadline).Headlines(_headlineNode.Index).ToUpper Then
                    _headlineNode.ImageKey = "ok"
                Else
                    _headlineNode.ImageKey = "failed"
                End If
                _headlineNode.SelectedImageKey = _headlineNode.ImageKey
            Next
            _node.SelectedImageKey = _node.ImageKey
        Next
    End Sub

    Sub UpdateNodes(View As TreeView, ByRef RuleList As cRuleList)
        Dim _selectedNode As TreeNode = View.SelectedNode
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem

        If FindControl("chk" & View.Tag & "Required") IsNot Nothing Then
            DirectCast(FindControl("chk" & View.Tag & "Required"), CheckBox).Checked = Rules(View.Tag).IsRequired
            DirectCast(FindControl("txt" & View.Tag & "Fallback"), TextBox).Text = Rules(View.Tag).Fallback
        End If

        View.Nodes.Clear()
        For Each _rule As cRule In RuleList
            Dim _tmpNode As TreeNode = View.Nodes.Add("Rules")
            _tmpNode.Tag = _rule
            If _rule.ValidationResult IsNot Nothing Then
                If _rule.ValidationResult.Succeeded Then
                    _tmpNode.ImageKey = "ok"
                Else
                    _tmpNode.ImageKey = "failed"
                End If
            Else
                _tmpNode.ImageKey = "unknown"
            End If
            _tmpNode.SelectedImageKey = _tmpNode.ImageKey
            For Each _search As cSearch In _rule.Searches.Where(Function(r) r.ParentSearch Is Nothing)
                AddSearch(_search, _tmpNode)
            Next
        Next
        View.ExpandAll()
        View.SelectedNode = _selectedNode
        View.Focus()
    End Sub

    Private Sub AddSearch(Search As cSearch, ParentNode As TreeNode)
        Dim _tmpNode As TreeNode = ParentNode.Nodes.Add(Search.ToString)
        _tmpNode.Tag = Search
        If Search.Result IsNot Nothing Then
            If Search.Result.Succeeded Then
                _tmpNode.ImageKey = "ok"
            Else
                _tmpNode.ImageKey = "failed"
            End If
        Else
            _tmpNode.ImageKey = "unknown"
        End If
        _tmpNode.SelectedImageKey = _tmpNode.ImageKey
        For Each _search As cSearch In Search.ChildSearches
            AddSearch(_search, _tmpNode)
        Next
    End Sub

    Sub AddRule(View As TreeView)
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        Dim _node As TreeNode = Nothing
        If View.SelectedNode IsNot Nothing Then
            If TryCast(View.SelectedNode.Tag, cRule) IsNot Nothing Then
                _node = View.SelectedNode
            ElseIf View.SelectedNode.Parent IsNot Nothing AndAlso TryCast(View.SelectedNode.Parent.Tag, cRule) IsNot Nothing Then
                _node = View.SelectedNode.Parent
            End If
        End If
        If View.Nodes.Count = 0 Then
            Dim _rule As New cRule(_template)
            With View.Nodes.Add("Rules")
                .Tag = _rule
                .SelectedImageKey = "unknown"
                .ImageKey = "unknown"
            End With
            If TryCast(Rules(View.Tag), cMutableRuleList) IsNot Nothing Then
                DirectCast(Rules(View.Tag), cMutableRuleList).Add(_rule)
            End If
        End If
        If _node Is Nothing Then
            _node = View.Nodes(0)
        End If
        With _node.Nodes.Add("")
            Dim _rule As cRule = _node.Tag
            Dim _check As New cSearch(_rule, _template)
            .Tag = _check
            .Text = _check.ToString
            .SelectedImageKey = "unknown"
            .ImageKey = "unknown"
        End With
        AutoValidateSchedule()
    End Sub

    Function AddRuleGroup(View As TreeView, Optional Headline As String = "Rule") As cRule
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem

        Dim _rule As New cRule(_template) With {.Name = Headline}
        With View.Nodes.Add(Headline)
            .Tag = _rule
            .SelectedImageKey = "unknown"
            .ImageKey = "unknown"
        End With
        AutoValidateSchedule()
        Return _rule
    End Function

    Sub MoveNodeUp(View As TreeView)
        Dim _node As TreeNode = View.SelectedNode
        Dim _newNode As TreeNode
        If TryCast(_node.Tag, cSearch) IsNot Nothing Then
            Dim _search As cSearch = _node.Tag

            If _node.PrevNode IsNot Nothing Then
                _newNode = _node.PrevNode
                _node.Remove()
                _newNode.Nodes.Add(_node)
            Else
                If _node.Parent.Parent IsNot Nothing Then
                    Dim _before As TreeNode = _node.Parent
                    _newNode = _node.Parent.Parent
                    _node.Remove()
                    _newNode.Nodes.Insert(_before.Index, _node)
                ElseIf _node.Parent.PrevNode IsNot Nothing Then
                    _newNode = _node.Parent.PrevNode
                    _node.Remove()
                    _newNode.Nodes.Add(_node)
                    _search.Rule = _newNode.Tag
                Else
                    _newNode = _node.Parent
                End If
            End If
            _search.ParentSearch = TryCast(_newNode.Tag, cSearch)

            View.SelectedNode = _node
            View.Focus()
        End If
        AutoValidateSchedule()
    End Sub

    Sub MoveNodeDown(View As TreeView)
        Dim _node As TreeNode = View.SelectedNode
        Dim _newNode As TreeNode
        If TryCast(_node.Tag, cSearch) IsNot Nothing Then
            Dim _search As cSearch = _node.Tag

            If _node.NextNode IsNot Nothing Then
                _newNode = _node.NextNode
                _node.Remove()
                _newNode.Nodes.Insert(0, _node)
            Else
                If _node.Parent.Parent IsNot Nothing Then
                    Dim _after As TreeNode = _node.Parent
                    _newNode = _node.Parent.Parent
                    _node.Remove()
                    _newNode.Nodes.Insert(_after.Index + 1, _node)
                ElseIf _node.Parent.NextNode IsNot Nothing Then
                    _newNode = _node.Parent.NextNode
                    _node.Remove()
                    _newNode.Nodes.Insert(0, _node)
                    _search.Rule = _newNode.Tag
                Else
                    _newNode = _node.Parent
                End If
            End If
            _search.ParentSearch = TryCast(_newNode.Tag, cSearch)

            View.SelectedNode = _node
            View.Focus()
        End If
        AutoValidateSchedule()
    End Sub
#End Region

#Region "Common events and functions (that needs to be edited if a control is added)"

    Sub SetTabIcons()
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        SetTabIcon(tpIdentify, _template.IdentificationRules)
        SetTabIcon(tpContract, _template.ContractRules)
        SetTabIcon(tpChannel, _template.ChannelRules)
        SetTabIcon(tpTarget, _template.TargetRules)
        SetTabIcon(tpColumns, _template.HeadlineRowRules)
    End Sub

    Public Function Rules(Identifier As String) As cRuleList
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        Select Case Identifier
            Case "Identify"
                Return _template.IdentificationRules
            Case "Target"
                Return _template.TargetRules
            Case "Contract"
                Return _template.ContractRules
            Case "Channel"
                Return _template.ChannelRules
            Case "Columns"
                Return _template.HeadlineRowRules
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub TreeView_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles tvwIdentify.MouseDown, tvwTarget.MouseDown, tvwContract.MouseDown, tvwChannel.MouseDown, tvwColumns.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            mnuEdit.Tag = sender
            mnuEdit.Show(sender, e.X, e.Y)
        End If
    End Sub

    Private Sub cmdUp_Click(sender As System.Object, e As System.EventArgs) Handles cmdIdentifyUp.Click, cmdTargetUp.Click, cmdContractUp.Click, cmdChannelUp.Click, cmdHeadlineUp.Click
        MoveNodeUp(FindControl("tvw" & DirectCast(sender, Button).Parent.Tag))
    End Sub

    Private Sub cmdDown_Click(sender As System.Object, e As System.EventArgs) Handles cmdIdentifyDown.Click, cmdTargetDown.Click, cmdTargetDown.Click, cmdChannelDown.Click, cmdHeadlineDown.Click
        MoveNodeDown(FindControl("tvw" & DirectCast(sender, Button).Parent.Tag))
    End Sub

    Private Sub cmdAddRule_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddIdentifyRule.Click, cmdAddTargetRule.Click, cmdAddContractRule.Click, cmdAddChannelRule.Click, cmdAddHeadlineRule.Click
        AddRule(FindControl("tvw" & DirectCast(sender, Button).Parent.Tag))
    End Sub

    Private Sub cmdRemoveRule_Click(sender As System.Object, e As System.EventArgs) Handles cmdRemoveChannelRule.Click, cmdRemoveContractRule.Click, cmdRemoveHeadlineRule.Click, cmdRemoveIdentifyRule.Click, cmdRemoveTargetRule.Click
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        Dim _view As TreeView = FindControl("tvw" & sender.parent.tag)
        If _view.SelectedNode Is Nothing Then Exit Sub

        If TryCast(_view.SelectedNode.Tag, cSearch) Is Nothing Then
            Dim _rule As cRule = _view.SelectedNode.Tag
            If TryCast(Rules(sender.parent.tag), cMutableRuleList) IsNot Nothing Then
                If Windows.Forms.MessageBox.Show("Are you sure you want to delete this rule?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    DirectCast(Rules(sender.parent.tag), cMutableRuleList).Remove(_view.SelectedNode.Tag)
                    _view.SelectedNode.Remove()
                End If
            End If
        Else
            Dim _search As cSearch = _view.SelectedNode.Tag
            If Windows.Forms.MessageBox.Show("Are you sure you want to delete this node?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If _search.ParentSearch IsNot Nothing Then
                    _search.ParentSearch = Nothing
                End If
                _search.Rule = Nothing
                _view.SelectedNode.Remove()
            End If
        End If
        AutoValidateSchedule()
    End Sub

    Private Sub TreeView_AfterExpand(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles tvwIdentify.AfterExpand, tvwTarget.AfterExpand, tvwContract.AfterExpand, tvwChannel.AfterExpand, tvwColumns.AfterExpand
        UpdateNodeIcons()
    End Sub

    Private Sub TreeView_AfterSelect(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles tvwIdentify.AfterSelect, tvwTarget.AfterSelect, tvwContract.AfterSelect, tvwChannel.AfterSelect, tvwColumns.AfterSelect
        If TryCast(e.Node.Tag, cSearch) IsNot Nothing Then
            DirectCast(FindControl("grp" & sender.tag & "Rule"), GroupBox).Visible = True
            Select Case e.Node.Tag.RuleType
                Case cSearch.RuleTypeEnum.Check
                    DirectCast(FindControl("cmb" & sender.tag & "SearchType"), ComboBox).SelectedItem = "Check"
                Case cSearch.RuleTypeEnum.Find
                    DirectCast(FindControl("cmb" & sender.tag & "SearchType"), ComboBox).SelectedItem = "Find"
                Case cSearch.RuleTypeEnum.Step
                    DirectCast(FindControl("cmb" & sender.tag & "SearchType"), ComboBox).SelectedItem = "Step"
            End Select
            Dim _search As cSearch = e.Node.Tag
            DirectCast(FindControl("txt" & sender.tag & "FindFromCol"), TextBox).Text = _search.Column
            DirectCast(FindControl("txt" & sender.tag & "FindFromRow"), TextBox).Text = _search.Row
            DirectCast(FindControl("txt" & sender.tag & "FindToCol"), TextBox).Text = _search.ToColumn
            DirectCast(FindControl("txt" & sender.tag & "FindToRow"), TextBox).Text = _search.ToRow
            DirectCast(FindControl("txt" & sender.tag & "SearchValue"), TextBox).Text = _search.SearchValue
            DirectCast(FindControl("cmb" & sender.tag & "SearchType"), ComboBox).SelectedIndex = DirectCast(_search, cSearch).SearchType
            DirectCast(FindControl("cmb" & sender.tag & "Rule"), ComboBox).SelectedIndex = DirectCast(_search, cSearch).RuleType
        Else
            DirectCast(FindControl("grp" & sender.tag & "Rule"), GroupBox).Visible = False
        End If
        grdTemplate.Invalidate()
    End Sub

    Private Sub cmbRule_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbIdentifyRule.SelectedIndexChanged, cmbTargetRule.SelectedIndexChanged, cmbContractRule.SelectedIndexChanged, cmbChannelRule.SelectedIndexChanged, cmbColumnsRule.SelectedIndexChanged
        SelectRule(sender.parent.parent.tag)
    End Sub

    Private Sub txtSearchValue_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtIdentifySearchValue.TextChanged, txtTargetSearchValue.TextChanged, txtContractSearchValue.TextChanged, txtChannelSearchValue.TextChanged, txtColumnsSearchValue.TextChanged
        Dim _view As TreeView = FindControl("tvw" & sender.parent.parent.tag)
        If _view.SelectedNode Is Nothing OrElse TryCast(_view.SelectedNode.Tag, cSearch) Is Nothing Then Exit Sub
        Dim _search As cSearch = _view.SelectedNode.Tag
        _search.SearchValue = sender.Text
        AutoValidateSchedule()
    End Sub

    Private Sub cmbSearchType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbIdentifySearchType.SelectedIndexChanged, cmbTargetSearchType.SelectedIndexChanged, cmbContractSearchType.SelectedIndexChanged, cmbChannelSearchType.SelectedIndexChanged, cmbColumnsSearchType.SelectedIndexChanged
        Dim _view As TreeView = FindControl("tvw" & sender.parent.parent.tag)
        If _view.SelectedNode Is Nothing OrElse TryCast(_view.SelectedNode.Tag, cSearch) Is Nothing Then Exit Sub
        Dim _search As cSearch = _view.SelectedNode.Tag
        _search.SearchType = sender.SelectedIndex
        AutoValidateSchedule()
    End Sub

    Private Sub txtFindFromRow_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtIdentifyFindFromRow.TextChanged, txtTargetFindFromRow.TextChanged, txtContractFindFromRow.TextChanged, txtChannelFindFromRow.TextChanged, txtColumnsFindFromRow.TextChanged
        Dim _view As TreeView = FindControl("tvw" & sender.parent.parent.tag)
        If _view.SelectedNode Is Nothing OrElse TryCast(_view.SelectedNode.Tag, cSearch) Is Nothing Then Exit Sub
        Dim _search As cSearch = _view.SelectedNode.Tag
        _search.Row = sender.Text
        AutoValidateSchedule()
    End Sub

    Private Sub txtFindFromCol_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtIdentifyFindFromCol.TextChanged, txtTargetFindFromCol.TextChanged, txtContractFindFromCol.TextChanged, txtChannelFindFromCol.TextChanged, txtColumnsFindFromCol.TextChanged
        Dim _view As TreeView = FindControl("tvw" & sender.parent.parent.tag)
        If _view.SelectedNode Is Nothing OrElse TryCast(_view.SelectedNode.Tag, cSearch) Is Nothing Then Exit Sub
        Dim _search As cSearch = _view.SelectedNode.Tag
        _search.Column = sender.Text
        AutoValidateSchedule()
    End Sub

    Private Sub txtFindToRow_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtIdentifyFindToRow.TextChanged, txtTargetFindToRow.TextChanged, txtContractFindToRow.TextChanged, txtChannelFindToRow.TextChanged, txtColumnsFindToRow.TextChanged
        Dim _view As TreeView = FindControl("tvw" & sender.parent.parent.tag)
        If _view.SelectedNode Is Nothing OrElse TryCast(_view.SelectedNode.Tag, cSearch) Is Nothing Then Exit Sub
        Dim _search As cSearch = _view.SelectedNode.Tag
        _search.ToRow = sender.Text
        AutoValidateSchedule()
    End Sub

    Private Sub txtFindToCol_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtIdentifyFindToCol.TextChanged, txtTargetFindToCol.TextChanged, txtContractFindToCol.TextChanged, txtChannelFindToCol.TextChanged, txtColumnsFindToCol.TextChanged
        Dim _view As TreeView = FindControl("tvw" & sender.parent.parent.tag)
        If _view.SelectedNode Is Nothing OrElse TryCast(_view.SelectedNode.Tag, cSearch) Is Nothing Then Exit Sub
        Dim _search As cSearch = _view.SelectedNode.Tag
        _search.ToColumn = sender.Text
        AutoValidateSchedule()
    End Sub

    Private Sub cmdGet_Click(sender As System.Object, e As System.EventArgs) Handles cmdGetIdentify.Click, cmdGetTarget.Click, cmdGetContract.Click, cmdGetChannel.Click, cmdGetColumns.Click, cmdGetColumns.Click
        If grdTemplate.SelectedCells.Count = 0 Then Exit Sub
        Dim _maxCol As Integer = 1
        Dim _minCol As Integer = grdTemplate.Columns.Count + 1
        Dim _maxRow As Integer = 1
        Dim _minRow As Integer = grdTemplate.Rows.Count + 1

        For Each _cell As DataGridViewCell In grdTemplate.SelectedCells
            If _cell.ColumnIndex > _maxCol Then _maxCol = _cell.ColumnIndex
            If _cell.ColumnIndex < _minCol Then _minCol = _cell.ColumnIndex
            If _cell.RowIndex > _maxRow Then _maxRow = _cell.RowIndex
            If _cell.RowIndex < _minRow Then _minRow = _cell.RowIndex
        Next
        FindControl("txt" & DirectCast(sender, Button).Parent.Parent.Tag & "FindFromCol").Text = _minCol + 1
        FindControl("txt" & DirectCast(sender, Button).Parent.Parent.Tag & "FindToCol").Text = _maxCol + 1
        FindControl("txt" & DirectCast(sender, Button).Parent.Parent.Tag & "FindFromRow").Text = _minRow + 1
        FindControl("txt" & DirectCast(sender, Button).Parent.Parent.Tag & "FindToRow").Text = _maxRow + 1
        If FindControl("txt" & DirectCast(sender, Button).Parent.Parent.Tag & "SearchValue").Text = "" Then
            FindControl("txt" & DirectCast(sender, Button).Parent.Parent.Tag & "SearchValue").Text = grdTemplate.SelectedCells(0).Value
        End If
        AutoValidateSchedule()
    End Sub

    Private Sub chkRequired_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkContractRequired.CheckedChanged, chkTargetRequired.CheckedChanged
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        If _template Is Nothing Then Exit Sub
        FindControl("lbl" & sender.parent.tag & "Required").Visible = Not DirectCast(sender, CheckBox).Checked
        FindControl("txt" & sender.parent.tag & "Fallback").Visible = Not DirectCast(sender, CheckBox).Checked
        Rules(sender.parent.tag).IsRequired = DirectCast(sender, CheckBox).Checked
        AutoValidateSchedule()
    End Sub

    Private Sub txtFallback_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtContractFallback.TextChanged, txtTargetFallback.TextChanged
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        If _template Is Nothing Then Exit Sub
        Rules(sender.parent.tag).Fallback = sender.text
        AutoValidateSchedule()
    End Sub
#End Region

#Region "Events for single controls"

    Private Sub cmdValidate_Click(sender As System.Object, e As System.EventArgs) Handles cmdValidate.Click
        ValidateSchedule()
    End Sub

    Private Sub grdTemplate_CellValueNeeded(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdTemplate.CellValueNeeded
        Dim _schedule As cSchedule = grdTemplate.Tag
        If grdTemplate.Columns(0).Tag Is Nothing OrElse Not _schedule.Sheets.ContainsKey(grdTemplate.Columns(0).Tag) Then
            Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
            grdTemplate.Columns(0).Tag = _template.UseSheet(_schedule)
        End If
        Dim _useSheet As String = grdTemplate.Columns(0).Tag
        If e.RowIndex < _schedule.Sheets(_useSheet).Rows AndAlso e.ColumnIndex < _schedule.Sheets(_useSheet).Columns Then
            e.Value = _schedule.Sheets(_useSheet).Cells(e.RowIndex + 1, e.ColumnIndex + 1)
        Else
            e.Value = ""
        End If
    End Sub

    Private Sub cmdAddTemplate_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddTemplate.Click
        Dim _template As New cScheduleTemplate

        cmbTemplates.Items.Add(_template)
        cmbTemplates.SelectedItem = _template

    End Sub

    Private Sub cmbTemplates_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbTemplates.SelectedIndexChanged
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        txtName.Text = _template.Name

        For Each _view As TreeView In GetAll(GetType(TreeView))
            If _view.Tag IsNot Nothing AndAlso _view.Tag <> "" Then
                UpdateNodes(_view, Rules(_view.Tag))
            End If
        Next

        LoadSheet()
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As System.EventArgs) Handles txtName.TextChanged
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        _template.Name = txtName.Text
        cmbTemplates.Items(cmbTemplates.SelectedIndex) = _template
    End Sub

    Private Sub cmdBrowse_Click(sender As System.Object, e As System.EventArgs) Handles cmdBrowse.Click
        Dim dlgOpen As New System.Windows.Forms.OpenFileDialog

        dlgOpen.CheckFileExists = True
        dlgOpen.DefaultExt = ""
        dlgOpen.FileName = ""
        dlgOpen.Filter = "All readable formats|*.xls;*.doc;*.docx;*.xlsx;*.rtf|Excel workbook|*.xls|Word document|*.doc;*.rtf"
        dlgOpen.Multiselect = True
        dlgOpen.Title = "Open channel schedule"

        If dlgOpen.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtSchedule.Text = dlgOpen.FileName
        End If
    End Sub

    Private Sub cmdAddSheet_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddSheet.Click
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        Dim _sheetName As String = InputBox("Name of sheet:", "Add sheet name", "")
        If _sheetName = "" Then Exit Sub
        lstSheets.Items.Add(_sheetName)
        _template.PossibleSheetNames.Add(_sheetName)
        LoadSheet()
        AutoValidateSchedule()
    End Sub

    Private Sub cmdRemoveSheet_Click(sender As System.Object, e As System.EventArgs) Handles cmdRemoveSheet.Click
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        _template.PossibleSheetNames.RemoveAt(lstSheets.SelectedIndex)
        lstSheets.Items.RemoveAt(lstSheets.SelectedIndex)
    End Sub

    Private Sub chkSheets_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSheets.CheckedChanged
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        If _template Is Nothing Then Exit Sub
        _template.UseFirstSheetIfNotFound = chkSheets.Checked
        LoadSheet()
        AutoValidateSchedule()
    End Sub

    Private Sub cmdLoadSchedule_Click(sender As System.Object, e As System.EventArgs) Handles cmdLoadSchedule.Click
        Dim _schedule As New cSchedule

        _schedule.Load(txtSchedule.Text)

        grdTemplate.Tag = _schedule

        LoadSheet()
        AutoValidateSchedule()

    End Sub

    Sub LoadSheet()
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        grdTemplate.Rows.Clear()
        grdTemplate.Columns.Clear()
        Dim _schedule As cSchedule = grdTemplate.Tag
        If _schedule Is Nothing OrElse _template Is Nothing Then
            Exit Sub
        End If
        Dim _useSheet As String = _template.UseSheet(_schedule)
        For Each _sheet In lstSheets.Items
            If _schedule.Sheets.ContainsKey(_sheet) Then
                _useSheet = _sheet
            End If
        Next
        If _useSheet = "" Then
            Windows.Forms.MessageBox.Show("Could not find any of the specified sheets in this workbook.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        For _col As Integer = 1 To _schedule.Sheets(_useSheet).Columns
            grdTemplate.Columns.Add("col" & _col, _col)
        Next
        grdTemplate.Rows.Add(_schedule.Sheets(_useSheet).Rows)

    End Sub

    Private Sub grdTemplate_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTemplate.CellContentClick

    End Sub

    Private Sub grdTemplate_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdTemplate.CellFormatting
        Dim _treeView As TreeView = GetSelectedTreeView()
        If _treeView IsNot Nothing Then
            If _treeView.SelectedNode IsNot Nothing Then
                Dim _search As cSearch = TryCast(_treeView.SelectedNode.Tag, cSearch)
                Dim _res As cSearchResult
                If _search IsNot Nothing Then
                    _res = _search.Result
                ElseIf _treeView.SelectedNode.Tag IsNot Nothing Then
                    If TryCast(_treeView.SelectedNode.Tag, cRule) IsNot Nothing Then
                        _res = DirectCast(_treeView.SelectedNode.Tag, cRule).ValidationResult
                    Else
                        _res = DirectCast(_treeView.SelectedNode.Tag, cColumnHeadline).ValidationResult
                    End If
                ElseIf _treeView.SelectedNode.Tag Is Nothing Then
                    _res = DirectCast(_treeView.SelectedNode.Parent.Tag, cColumnHeadline).ValidationResult
                End If
                If _res IsNot Nothing AndAlso _res.Row = e.RowIndex + 1 AndAlso _res.Column = e.ColumnIndex + 1 AndAlso _res.Succeeded Then
                    e.CellStyle.BackColor = Color.LightGreen
                    Exit Sub
                End If

            End If
            End If
        e.CellStyle = grdTemplate.DefaultCellStyle
    End Sub

#End Region

    Private Sub cmdAddHeadlineRow_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddHeadlineRow.Click
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        _template.HeadlineRowRules.Add(AddRuleGroup(tvwColumns, "Row"))
    End Sub

    Private Sub cmdAddRuleGroup_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddIdentifyRuleGroup.Click
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        DirectCast(Rules(sender.parent.tag), cMutableRuleList).Add(AddRuleGroup(FindControl("tvw" & DirectCast(sender, Button).Parent.Tag)))
    End Sub

    Private Sub cmdAddHeadline_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddHeadline.Click
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        If tvwHeadlines.SelectedNode IsNot Nothing Then
            Dim _node As TreeNode = tvwHeadlines.SelectedNode
            While _node.Parent IsNot Nothing
                _node = _node.Parent
            End While
            With _node.Nodes.Add("")
                .SelectedImageKey = .ImageKey
                DirectCast(_node.Tag, cColumnHeadline).Headlines.Add("")
            End With
        End If
    End Sub

    Private Sub cmdRemoveHeadline_Click(sender As System.Object, e As System.EventArgs) Handles cmdRemoveHeadline.Click
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        If tvwHeadlines.SelectedNode Is Nothing OrElse tvwHeadlines.SelectedNode.Parent Is Nothing Then
            Return
        End If
        Dim _node As TreeNode = tvwHeadlines.SelectedNode
        While _node.Parent IsNot Nothing
            _node = _node.Parent
        End While
        DirectCast(_node.Tag, cColumnHeadline).Headlines.RemoveAt(tvwHeadlines.SelectedNode.Index)
        tvwHeadlines.SelectedNode.Remove()
    End Sub

    Private Sub tpColumns_Click(sender As System.Object, e As System.EventArgs) Handles tpColumns.Click

    End Sub

    Private Sub tpColumns_Enter(sender As Object, e As System.EventArgs) Handles tpColumns.Enter
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        If _template Is Nothing Then Exit Sub
        tvwHeadlines.Nodes.Clear()
        lstRequired.Items.Clear()
        For Each _column As cColumnHeadline In _template.ColumnHeadlines.Values
            With tvwHeadlines.Nodes.Add(_column.ColumnName)
                .Tag = _column
                For Each _headline As String In _column.Headlines
                    Dim _node As TreeNode = .Nodes.Add(_headline)
                    _node.SelectedImageKey = _node.ImageKey
                Next
                .SelectedImageKey = .ImageKey
            End With
            If _column.Required Then
                lstRequired.Items.Add(_column)
            End If
        Next
        tvwHeadlines.ExpandAll()
        AutoValidateSchedule()
    End Sub

    Private Sub tvwHeadlines_AfterLabelEdit(sender As Object, e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvwHeadlines.AfterLabelEdit
        Dim _node As TreeNode = e.Node
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        While _node.Parent IsNot Nothing
            _node = _node.Parent
        End While
        If e.Label IsNot Nothing Then DirectCast(_node.Tag, cColumnHeadline).Headlines(e.Node.Index) = e.Label
        AutoValidateSchedule()
    End Sub

    Private Sub tvwHeadlines_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles tvwHeadlines.AfterSelect
        grdTemplate.Invalidate()
    End Sub

    Private Sub tvwHeadlines_BeforeLabelEdit(sender As Object, e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvwHeadlines.BeforeLabelEdit
        If e.Node.Parent Is Nothing Then
            e.CancelEdit = True
        End If
    End Sub

    Private Sub cmdSetRequiredColumn_Click(sender As System.Object, e As System.EventArgs) Handles cmdSetRequiredColumn.Click
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        If tvwHeadlines.SelectedNode IsNot Nothing Then
            Dim _node As TreeNode = tvwHeadlines.SelectedNode
            While _node.Parent IsNot Nothing
                _node = _node.Parent
            End While
            If Not lstRequired.Items.Contains(_node.Text) Then
                lstRequired.Items.Add(_node.Tag)
                DirectCast(_node.Tag, cColumnHeadline).Required = True
            End If
        End If
        AutoValidateSchedule()
    End Sub

    Private Sub cmdUnsetRequiredColumn_Click(sender As System.Object, e As System.EventArgs) Handles cmdUnsetRequiredColumn.Click
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        If lstRequired.SelectedIndex > -1 Then
            DirectCast(lstRequired.SelectedItem, cColumnHeadline).Required = False
            lstRequired.Items.RemoveAt(lstRequired.SelectedIndex)
        End If
        AutoValidateSchedule()
    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Dim _template As cScheduleTemplate = cmbTemplates.SelectedItem
        Dim _xml As XElement = _template.GetXML()

        Dim _save As New SaveFileDialog() With {.FileName = "*.xml", .Filter = "XML-files|*.xml", .Title = "Save template"}

        If _save.ShowDialog = Windows.Forms.DialogResult.OK Then
            _xml.Save(_save.FileName)
        End If
    End Sub

    Private Sub cmdLoad_Click(sender As System.Object, e As System.EventArgs) Handles cmdLoad.Click
        Dim _template As New cScheduleTemplate(False)
        Dim _load As New OpenFileDialog() With {.FileName = "*.xml", .Filter = "XML-files|*.xml", .Title = "Open template"}

        If _load.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim _xml As XDocument
            _xml = XDocument.Load(_load.FileName)
            _template.Parse(_xml)
        End If
        cmbTemplates.Items.Add(_template)
    End Sub
End Class
