Imports System.Linq

Public Class frmQuestionaires

    Private Sub cmbQuestionaire_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbQuestionaire.SelectedIndexChanged
        If cmbQuestionaire.Tag = "" Then
            UpdateList()
            UpdatePreview()
        End If

        Dim _questionaire As cQuestionaire = cmbQuestionaire.SelectedItem
        txtName.Text = _questionaire.Name
        chkValidate.Checked = (_questionaire.Validate = cQuestionaire.ValidateEnum.True)

    End Sub

    Private Sub frmQuestionaires_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbQuestionaire.Items.Clear()
        For Each _questionaire As cQuestionaire In MyEvent.Questionaires
            cmbQuestionaire.Items.Add(_questionaire)
        Next
    End Sub

    Private Sub cmdAddEntity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddEntity.Click
        Dim mnuAddEntity As New ContextMenu()

        With mnuAddEntity.MenuItems.Add("Inputs")
            .MenuItems.Add("Text", AddressOf AddInputEntity)
            .MenuItems.Add("Number", AddressOf AddInputEntity)
            .MenuItems.Add("Free text", AddressOf AddInputEntity)
            .MenuItems.Add("Single choice", AddressOf AddInputEntity)
            .MenuItems.Add("Rating", AddressOf AddInputEntity)
            .MenuItems.Add("Group", AddressOf AddSpecialEntity)
        End With
        With mnuAddEntity.MenuItems.Add("Outputs")
            .MenuItems.Add("Text", AddressOf AddOutputEntity)
            .MenuItems.Add("Headline", AddressOf AddOutputEntity)
            .MenuItems.Add("Horizontal line", AddressOf AddOutputEntity)
        End With
        mnuAddEntity.Show(cmdAddEntity, New Point(0, cmdAddEntity.Height))
    End Sub

    Sub AddInputEntity(ByVal sender As Object, ByVal e As EventArgs)
        Dim _questionaire As cQuestionaire = cmbQuestionaire.SelectedItem
        Dim _entity As New cQuestionaireEntityInput(_questionaire)
        Dim _node As TreeNode = Nothing
        Select Case sender.text
            Case "Text"
                _entity.Type = cQuestionaireEntityInput.InputTypeEnum.Text
                _node = tvwEntities.Nodes.Add(": <Text>")
            Case "Number"
                _entity.Type = cQuestionaireEntityInput.InputTypeEnum.Number
                _node = tvwEntities.Nodes.Add(": <Number>")
            Case "Free text"
                _entity.Type = cQuestionaireEntityInput.InputTypeEnum.TextArea
                _node = tvwEntities.Nodes.Add(": <Free text>")
            Case "Single choice"
                _entity.Type = cQuestionaireEntityInput.InputTypeEnum.SingleChoice
                _node = tvwEntities.Nodes.Add(": <Single choice>")
            Case "Rating"
                _entity.Type = cQuestionaireEntityInput.InputTypeEnum.Rating
                _node = tvwEntities.Nodes.Add(": <Rating (1 to " & _entity.Values & ")>")
        End Select
        _node.Tag = _entity
        _node.ForeColor = Color.Red
        _questionaire.Entities.Add(_entity)
        _node.EnsureVisible()
        tvwEntities.Focus()
        tvwEntities.SelectedNode = _node
        UpdatePreview()
    End Sub

    Sub AddOutputEntity(ByVal sender As Object, ByVal e As EventArgs)
        Dim _questionaire As cQuestionaire = cmbQuestionaire.SelectedItem
        Dim _entity As New cQuestionaireEntityOutput(_questionaire)
        Dim _node As TreeNode = Nothing
        Select Case sender.text
            Case "Text"
                _entity.Type = cQuestionaireEntityOutput.OutputTypeEnum.Text
                _node = tvwEntities.Nodes.Add("")
            Case "Headline"
                _entity.Type = cQuestionaireEntityOutput.OutputTypeEnum.Headline
                _node = tvwEntities.Nodes.Add("Headline:")
            Case "Horizontal line"
                _entity.Type = cQuestionaireEntityOutput.OutputTypeEnum.Line
                _node = tvwEntities.Nodes.Add("---------------------")
        End Select
        _node.Tag = _entity
        _node.ForeColor = Color.Blue
        _questionaire.Entities.Add(_entity)
        _node.EnsureVisible()
        tvwEntities.Focus()
        tvwEntities.SelectedNode = _node
        UpdatePreview()
    End Sub

    Sub AddSpecialEntity(ByVal sender As Object, ByVal e As EventArgs)
        Dim _questionaire As cQuestionaire = cmbQuestionaire.SelectedItem
        Dim _entity As New cQuestionaireEntityGroup(_questionaire)
        Dim _node As TreeNode = Nothing
        _node = tvwEntities.Nodes.Add("Group")
        _node.ForeColor = Color.Green
        _questionaire.Entities.Add(_entity)
        _node.Tag = _entity
        _node.EnsureVisible()
        tvwEntities.Focus()
        tvwEntities.SelectedNode = _node
        UpdatePreview()
    End Sub

    Sub UpdateList()
        tvwEntities.SuspendLayout()
        Dim _questionaire As cQuestionaire = cmbQuestionaire.SelectedItem
        tvwEntities.Nodes.Clear()
        For Each _entity As cQuestionaireEntity In _questionaire.Entities
            With tvwEntities.Nodes.Add("")
                If _entity.GetType Is GetType(cQuestionaireEntityInput) Then
                    Select Case DirectCast(_entity, cQuestionaireEntityInput).Type
                        Case cQuestionaireEntityInput.InputTypeEnum.Text
                            .Text = _entity.Text & ": <Text>"
                        Case cQuestionaireEntityInput.InputTypeEnum.Number
                            .Text = _entity.Text & ": <Number>"
                        Case cQuestionaireEntityInput.InputTypeEnum.Rating
                            .Text = _entity.Text & ": <Rating (1 to " & DirectCast(_entity, cQuestionaireEntityInput).Values & ")>"
                        Case cQuestionaireEntityInput.InputTypeEnum.SingleChoice
                            .Text = _entity.Text & ": <Single choice>"
                            For Each _choice As KeyValuePair(Of Byte, String) In DirectCast(_entity, cQuestionaireEntityInput).Choices
                                .Nodes.Add(_choice.Value & " (" & _choice.Key & ")")
                            Next
                        Case cQuestionaireEntityInput.InputTypeEnum.TextArea
                            .Text = _entity.Text & ": <Free text>"
                    End Select
                    .ForeColor = Color.Red
                    If _entity.Validate = cQuestionaireEntity.ValidateEnum.True OrElse (_questionaire.Validate = cQuestionaire.ValidateEnum.True AndAlso Not _entity.Validate = cQuestionaireEntity.ValidateEnum.False) Then
                        .Text &= "*"
                    End If
                ElseIf _entity.GetType Is GetType(cQuestionaireEntityOutput) Then
                    Select Case DirectCast(_entity, cQuestionaireEntityOutput).Type
                        Case cQuestionaireEntityOutput.OutputTypeEnum.Text
                            .Text = _entity.Value
                        Case cQuestionaireEntityOutput.OutputTypeEnum.Headline
                            .Text = "Headline: " & _entity.Value
                        Case cQuestionaireEntityOutput.OutputTypeEnum.Data
                            .Text = _entity.Text & ": <" & _entity.Value & ">"
                        Case cQuestionaireEntityOutput.OutputTypeEnum.Line
                            .Text = "---------------------"
                    End Select
                    .ForeColor = Color.Blue
                ElseIf _entity.GetType Is GetType(cQuestionaireEntityGroup) Then
                    .Text = "Group (|"
                    .ForeColor = Color.Green
                    For Each _member As cQuestionaireEntity In DirectCast(_entity, cQuestionaireEntityGroup).Members
                        .Text &= _member.Text & "|"
                    Next
                    .Text &= ")"
                    For Each _member As cQuestionaireEntity In DirectCast(_entity, cQuestionaireEntityGroup).Members
                        If _member.GetType Is GetType(cQuestionaireEntityOutput) Then
                            With .Nodes.Add(_member.Text)
                                .ForeColor = Color.Blue
                            End With
                        ElseIf _member.GetType Is GetType(cQuestionaireEntityInput) Then
                            With .Nodes.Add("Input (" & _member.Text & ")")
                                .ForeColor = Color.Blue
                            End With
                        End If
                    Next
                End If
                .Tag = _entity
            End With
        Next
        tvwEntities.ResumeLayout(True)
    End Sub

    Dim rememberOffset As Point = Nothing
    Sub UpdatePreview()
        Dim _questionaire As cQuestionaire = cmbQuestionaire.SelectedItem
        If wbPreview.Document IsNot Nothing AndAlso wbPreview.Document.Body IsNot Nothing Then
            rememberOffset = New Point(wbPreview.Document.Body.ScrollLeft, wbPreview.Document.Body.ScrollTop)
        End If
        wbPreview.DocumentText = _questionaire.CreatePreviewHTML
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        Dim _questionaire As cQuestionaire = cmbQuestionaire.SelectedItem
        _questionaire.Name = txtName.Text
        cmbQuestionaire.Tag = "SKIP"
        cmbQuestionaire.Items(cmbQuestionaire.SelectedIndex) = _questionaire
        cmbQuestionaire.Tag = ""
        UpdatePreview()
    End Sub

    Private Sub chkValidate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkValidate.CheckedChanged
        Dim _questionaire As cQuestionaire = cmbQuestionaire.SelectedItem
        If chkValidate.Checked Then
            _questionaire.Validate = cQuestionaire.ValidateEnum.True
        Else
            _questionaire.Validate = cQuestionaire.ValidateEnum.False
        End If
        UpdatePreview()
        UpdateList()
    End Sub

    Private Sub tvwEntities_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwEntities.AfterSelect
        Dim _entity As cQuestionaireEntity
        If e.Node.Parent Is Nothing Then
            _entity = e.Node.Tag
        Else
            _entity = e.Node.Parent.Tag
        End If
        'If _entity Is Nothing Then Exit Sub
        If _entity.GetType Is GetType(cQuestionaireEntityOutput) Then
            With DirectCast(_entity, cQuestionaireEntityOutput)
                txtUnit.Enabled = False
                cmbRequired.Enabled = False
                grpInputSinglechoice.Visible = False
                grpRating.Visible = False
                grpGroup.Visible = False
                Select Case .Type
                    Case cQuestionaireEntityOutput.OutputTypeEnum.Data, cQuestionaireEntityOutput.OutputTypeEnum.Text
                        cmbData.Enabled = True
                        txtText.Enabled = True
                        txtEntityName.Enabled = True
                        If .Type = cQuestionaireEntityOutput.OutputTypeEnum.Data Then
                            txtText.Text = .Text
                            cmbData.SelectedItem = .Value.Substring(0, 1).ToUpper & .Value.Substring(1)
                        Else
                            txtText.Text = .Value
                            cmbData.SelectedIndex = 0
                        End If
                        grpEntity.Text = "Entity: Text output"
                    Case cQuestionaireEntityOutput.OutputTypeEnum.Line
                        cmbData.Enabled = False
                        txtText.Enabled = False
                        txtEntityName.Enabled = False
                        grpEntity.Text = "Entity: Line ouput"
                        cmbData.SelectedIndex = 0
                    Case cQuestionaireEntityOutput.OutputTypeEnum.Headline
                        cmbData.Enabled = False
                        txtText.Enabled = True
                        txtEntityName.Enabled = True
                        grpEntity.Text = "Entity: Headline text output"
                        txtText.Text = .Value
                        cmbData.SelectedIndex = 0
                End Select
                txtEntityName.Text = .Name
            End With
        ElseIf _entity.GetType Is GetType(cQuestionaireEntityInput) Then
            With DirectCast(_entity, cQuestionaireEntityInput)
                Select Case .Type
                    Case cQuestionaireEntityInput.InputTypeEnum.Number
                        txtUnit.Enabled = True
                        cmbData.Enabled = True
                        txtText.Enabled = True
                        txtEntityName.Enabled = True
                        cmbRequired.Enabled = True
                        grpInputSinglechoice.Visible = False
                        grpRating.Visible = False
                        grpGroup.Visible = False
                        grpEntity.Text = "Entity: Number input"
                    Case cQuestionaireEntityInput.InputTypeEnum.Text, cQuestionaireEntityInput.InputTypeEnum.TextArea
                        txtUnit.Enabled = False
                        cmbData.Enabled = True
                        txtText.Enabled = True
                        txtEntityName.Enabled = True
                        cmbRequired.Enabled = True
                        grpInputSinglechoice.Visible = False
                        grpGroup.Visible = False
                        grpRating.Visible = False
                        If .Type = cQuestionaireEntityInput.InputTypeEnum.Text Then
                            grpEntity.Text = "Entity: Text input"
                        Else
                            grpEntity.Text = "Entity: Long text input"
                        End If
                    Case cQuestionaireEntityInput.InputTypeEnum.Rating
                        txtUnit.Enabled = False
                        cmbData.Enabled = False
                        txtText.Enabled = True
                        txtEntityName.Enabled = True
                        cmbRequired.Enabled = True
                        grpInputSinglechoice.Visible = False
                        grpRating.Visible = True
                        grpGroup.Visible = False
                        grpEntity.Text = "Entity: Rating input"
                    Case cQuestionaireEntityInput.InputTypeEnum.SingleChoice
                        txtUnit.Enabled = False
                        cmbData.Enabled = False
                        txtText.Enabled = True
                        txtEntityName.Enabled = True
                        cmbRequired.Enabled = True
                        grpInputSinglechoice.Visible = True
                        grpRating.Visible = False
                        grpGroup.Visible = False

                        UpdateChoices(_entity)
                        grpEntity.Text = "Entity: Single choice input"
                End Select
                txtUnit.Text = .Unit
                cmbData.SelectedIndex = -1
                If .Data <> "" Then
                    cmbData.SelectedItem = .Data
                End If
                txtText.Text = .Text
                txtEntityName.Text = .Name
                txtValues.Text = .Values
                txtLeft.Text = .LeftText
                txtRight.Text = .RightText
                cmbRequired.SelectedIndex = 0
                Select Case .Validate
                    Case cQuestionaireEntity.ValidateEnum.True
                        cmbRequired.SelectedIndex = 1
                    Case cQuestionaireEntity.ValidateEnum.False
                        cmbRequired.SelectedIndex = 2
                End Select
            End With
            If cmbData.SelectedItem Is Nothing Then cmbData.SelectedIndex = 0
        ElseIf _entity.GetType Is GetType(cQuestionaireEntityGroup) Then
            grdGroupMembers.Rows.Clear()

            With DirectCast(_entity, cQuestionaireEntityGroup)
                For Each _member As cQuestionaireEntity In .Members
                    With grdGroupMembers.Rows(grdGroupMembers.Rows.Add)
                        .Tag = _member
                        With DirectCast(.Cells(0), ExtendedComboBoxCell)
                            .DropDownStyle = ComboBoxStyle.DropDownList
                            .Items.Clear()
                            .Items.Add("Output")
                            .Items.Add("Input")
                        End With
                    End With
                Next
            End With
            cmbData.SelectedIndex = 0
            txtUnit.Enabled = False
            cmbRequired.Enabled = False
            grpInputSinglechoice.Visible = False
            grpRating.Visible = False
            grpGroup.Visible = True

            txtUnit.Text = ""
            txtEntityName.Text = _entity.Name
            txtText.Text = _entity.Text
            cmbData.SelectedIndex = 0

            cmbData.Enabled = False
            txtEntityName.Enabled = True
            txtText.Enabled = False
            cmbRequired.Enabled = False

            If e.Node.PrevNode IsNot Nothing AndAlso e.Node.PrevNode.Tag.GetType Is GetType(cQuestionaireEntityGroup) Then
                cmdCopy.Enabled = True
            Else
                cmdCopy.Enabled = False
            End If
        End If
    End Sub

    Private Sub grdChoices_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdChoices.CellValueNeeded
        Dim kv As KeyValuePair(Of Byte, String) = grdChoices.Rows(e.RowIndex).Tag

        Select Case e.ColumnIndex
            Case 0
                e.Value = kv.Key
            Case 1
                e.Value = kv.Value
        End Select
    End Sub

    Private Sub grdChoices_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdChoices.CellValuePushed
        Dim _entity As cQuestionaireEntityInput
        Dim _node As TreeNode = tvwEntities.SelectedNode
        Dim _entNode As TreeNode
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
            _entNode = _node
        Else
            _entity = _node.Parent.Tag
            _entNode = _node.Parent
        End If
        Dim kv As KeyValuePair(Of Byte, String) = grdChoices.Rows(e.RowIndex).Tag
        Select Case e.ColumnIndex
            Case 0
                _entity.Choices.Remove(kv.Key)
                _entity.Choices.Add(e.Value, kv.Value)
                grdChoices.Rows(e.RowIndex).Tag = kv
            Case 1
                _entity.Choices(kv.Key) = e.Value
        End Select
        Dim i As Integer = 0
        For Each _choice As KeyValuePair(Of Byte, String) In _entity.Choices
            grdChoices.Rows(i).Tag = _choice
            i += 1
        Next
        _entNode.Nodes.Clear()
        For Each _choice As KeyValuePair(Of Byte, String) In _entity.Choices
            _entNode.Nodes.Add(_choice.Value & " (" & _choice.Key & ")")
        Next
        UpdatePreview()
        tvwEntities.SelectedNode = _node
    End Sub

    Sub UpdateChoices(ByVal entity As cQuestionaireEntityInput)
        grdChoices.Rows.Clear()
        For Each _choice As KeyValuePair(Of Byte, String) In entity.Choices
            grdChoices.Rows(grdChoices.Rows.Add).Tag = _choice
        Next
    End Sub

    Private Sub txtText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtText.TextChanged
        Dim _entity As cQuestionaireEntity
        Dim _node As TreeNode = tvwEntities.SelectedNode
        Dim _entNode As TreeNode
        If _node Is Nothing Then Exit Sub
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
            _entNode = _node
        Else
            _entity = _node.Parent.Tag
            _entNode = _node.Parent
        End If
        If _entity.GetType Is GetType(cQuestionaireEntityOutput) AndAlso DirectCast(_entity, cQuestionaireEntityOutput).Type <> cQuestionaireEntityOutput.OutputTypeEnum.Data Then
            _entity.Value = txtText.Text
        Else
            _entity.Text = txtText.Text
        End If
        UpdatePreview()
        If _entity.GetType Is GetType(cQuestionaireEntityInput) Then
            Select Case DirectCast(_entity, cQuestionaireEntityInput).Type
                Case cQuestionaireEntityInput.InputTypeEnum.Text
                    _entNode.Text = _entity.Text & ": <Text>"
                Case cQuestionaireEntityInput.InputTypeEnum.Number
                    _entNode.Text = _entity.Text & ": <Number>"
                Case cQuestionaireEntityInput.InputTypeEnum.Rating
                    _entNode.Text = _entity.Text & ": <Rating (1 to " & DirectCast(_entity, cQuestionaireEntityInput).Values & ")>"
                Case cQuestionaireEntityInput.InputTypeEnum.SingleChoice
                    _entNode.Text = _entity.Text & ": <Single choice>"
                Case cQuestionaireEntityInput.InputTypeEnum.TextArea
                    _entNode.Text = _entity.Text & ": <Free text>"
            End Select
        ElseIf _entity.GetType Is GetType(cQuestionaireEntityOutput) Then
            Select Case DirectCast(_entity, cQuestionaireEntityOutput).Type
                Case cQuestionaireEntityOutput.OutputTypeEnum.Text
                    _entNode.Text = txtText.Text
                Case cQuestionaireEntityOutput.OutputTypeEnum.Headline
                    _entNode.Text = "Headline: " & txtText.Text
                Case cQuestionaireEntityOutput.OutputTypeEnum.Data
                    _entNode.Text = _entity.Text & ": <" & _entity.Value & ">"
                Case cQuestionaireEntityOutput.OutputTypeEnum.Line
                    _entNode.Text = "---------------------"
            End Select
        ElseIf _entity.GetType Is GetType(cQuestionaireEntityGroup) Then

        End If

    End Sub

    Private Sub txtEntityName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEntityName.TextChanged
        Dim _entity As cQuestionaireEntity
        Dim _node As TreeNode = tvwEntities.SelectedNode
        If _node Is Nothing Then Exit Sub
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
        Else
            _entity = _node.Parent.Tag
        End If
        _entity.Name = txtEntityName.Text
    End Sub

    Private Sub cmbData_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbData.SelectedIndexChanged
        If cmbData.SelectedIndex = -1 Then Exit Sub
        Dim _entity As cQuestionaireEntity
        Dim _node As TreeNode = tvwEntities.SelectedNode
        Dim _entNode As TreeNode
        If _node Is Nothing Then Exit Sub
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
            _entNode = _node
        Else
            _entity = _node.Parent.Tag
            _entNode = _node.Parent
        End If
        If _entity.GetType Is GetType(cQuestionaireEntityInput) Then
            With DirectCast(_entity, cQuestionaireEntityInput)
                If cmbData.SelectedIndex = 0 Then
                    .Data = cmbData.SelectedItem
                Else
                    .Data = cmbData.SelectedItem
                End If
            End With
        ElseIf _entity.GetType Is GetType(cQuestionaireEntityOutput) Then
            With DirectCast(_entity, cQuestionaireEntityOutput)
                Select Case .Type
                    Case cQuestionaireEntityOutput.OutputTypeEnum.Text, cQuestionaireEntityOutput.OutputTypeEnum.Data
                        If cmbData.SelectedIndex = 0 Then
                            .Type = cQuestionaireEntityOutput.OutputTypeEnum.Text
                            .Value = txtText.Text
                            .Text = ""
                            _entNode.Text = _entity.Value
                        Else
                            .Type = cQuestionaireEntityOutput.OutputTypeEnum.Data
                            .Text = txtText.Text
                            .Value = cmbData.SelectedItem.ToString.ToLower
                            _entNode.Text = _entity.Text & ": <" & _entity.Value & ">"
                        End If
                End Select
            End With
        End If
        UpdatePreview()
    End Sub

    Private Sub chkRequired_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtUnit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUnit.TextChanged
        Dim _entity As cQuestionaireEntityInput
        Dim _node As TreeNode = tvwEntities.SelectedNode
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
        Else
            _entity = _node.Parent.Tag
        End If
        _entity.Unit = txtUnit.Text
        UpdatePreview()
    End Sub

    Private Sub cmdRemoveEntity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveEntity.Click
        Dim _questionaire As cQuestionaire = cmbQuestionaire.SelectedItem
        Dim _entity As cQuestionaireEntity
        Dim _node As TreeNode = tvwEntities.SelectedNode
        Dim _entNode As TreeNode
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
            _entNode = _node
        Else
            _entity = _node.Parent.Tag
            _entNode = _node.Parent
        End If
        _questionaire.Entities.Remove(_entity)
        tvwEntities.Nodes.Remove(_entNode)
        UpdatePreview()
    End Sub

    Private Sub cmdMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveUp.Click
        Dim _entity As cQuestionaireEntity
        Dim _node As TreeNode = tvwEntities.SelectedNode
        Dim _entNode As TreeNode
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
            _entNode = _node
        Else
            _entity = _node.Parent.Tag
            _entNode = _node.Parent
        End If
        If _entNode.Index = 0 Then Exit Sub
        Dim TmpEntity As cQuestionaireEntity = _entity
        DirectCast(cmbQuestionaire.SelectedItem, cQuestionaire).Entities(_entNode.Index) = DirectCast(cmbQuestionaire.SelectedItem, cQuestionaire).Entities(_entNode.Index - 1)
        DirectCast(cmbQuestionaire.SelectedItem, cQuestionaire).Entities(_entNode.Index - 1) = TmpEntity

        UpdateList()
        UpdatePreview()

        tvwEntities.SelectedNode = tvwEntities.Nodes(_entNode.Index - 1)
    End Sub

    Private Sub cmdMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveDown.Click
        Dim _entity As cQuestionaireEntity
        Dim _node As TreeNode = tvwEntities.SelectedNode
        Dim _entNode As TreeNode
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
            _entNode = _node
        Else
            _entity = _node.Parent.Tag
            _entNode = _node.Parent
        End If
        If _entNode.Index = tvwEntities.Nodes.Count - 1 Then Exit Sub
        Dim TmpEntity As cQuestionaireEntity = _entity
        DirectCast(cmbQuestionaire.SelectedItem, cQuestionaire).Entities(_entNode.Index) = DirectCast(cmbQuestionaire.SelectedItem, cQuestionaire).Entities(_entNode.Index + 1)
        DirectCast(cmbQuestionaire.SelectedItem, cQuestionaire).Entities(_entNode.Index + 1) = TmpEntity

        UpdateList()
        UpdatePreview()

        tvwEntities.SelectedNode = tvwEntities.Nodes(_entNode.Index + 1)
    End Sub

    Private Sub cmdAddQuestionaire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddQuestionaire.Click
        Dim _questionaire As New cQuestionaire With {.Name = "New Questionaire"}
        MyEvent.Questionaires.Add(_questionaire)
        cmbQuestionaire.Items.Add(_questionaire)
        cmbQuestionaire.SelectedItem = _questionaire
    End Sub

    Private Sub cmdRemoveQuestionaire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveQuestionaire.Click

    End Sub

    Private Sub wbPreview_DocumentCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles wbPreview.DocumentCompleted
        wbPreview.Document.Body.ScrollTop = rememberOffset.Y
    End Sub

    Private Sub wbPreview_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles wbPreview.Navigated

    End Sub

    Private Sub tvwEntities_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvwEntities.LostFocus
        If tvwEntities.SelectedNode Is Nothing Then
            'Stop
        End If
    End Sub

    Private Sub cmdAddChoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddChoice.Click
        Dim _entity As cQuestionaireEntityInput
        Dim _node As TreeNode = tvwEntities.SelectedNode
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
        Else
            _entity = _node.Parent.Tag
        End If
        Dim _key As Integer = _entity.Choices.Count + 1
        While _entity.Choices.ContainsKey(_key)
            _key += 1
        End While
        _entity.Choices.Add(_key, "")
        UpdateChoices(_entity)
    End Sub

    Private Sub cmdRemoveChoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveChoice.Click

    End Sub

    Private Sub txtLeft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLeft.TextChanged
        Dim _entity As cQuestionaireEntityInput
        Dim _node As TreeNode = tvwEntities.SelectedNode
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
        Else
            _entity = _node.Parent.Tag
        End If
        _entity.LeftText = txtLeft.Text
        UpdatePreview()
    End Sub

    Private Sub txtRight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRight.TextChanged
        Dim _entity As cQuestionaireEntityInput
        Dim _node As TreeNode = tvwEntities.SelectedNode
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
        Else
            _entity = _node.Parent.Tag
        End If
        _entity.RightText = txtRight.Text
        UpdatePreview()
    End Sub

    Private Sub txtValues_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtValues.TextChanged
        Dim _questionaire As cQuestionaire = cmbQuestionaire.SelectedItem
        Dim _entity As cQuestionaireEntityInput
        Dim _node As TreeNode = tvwEntities.SelectedNode
        Dim _entNode As TreeNode
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
            _entNode = _node
        Else
            _entity = _node.Parent.Tag
            _entNode = _node.Parent
        End If
        If _entity.Type = cQuestionaireEntityInput.InputTypeEnum.Rating Then
            _entity.Values = Val(txtValues.Text)
            _entNode.Text = _entity.Text & ": <Rating (1 to " & _entity.Values & ")>"
        End If
        UpdatePreview()
    End Sub

    Private Sub cmdAddMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddMember.Click
        Dim mnuAddEntity As New ContextMenu()

        mnuAddEntity.MenuItems.Add("Text input", AddressOf AddGroupMember)
        mnuAddEntity.MenuItems.Add("Text output", AddressOf AddGroupMember)

        mnuAddEntity.Show(cmdAddMember, New Point(0, cmdAddMember.Height))
    End Sub

    Private Sub cmdRemoveMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveMember.Click
        Dim _questionaire As cQuestionaire = cmbQuestionaire.SelectedItem
        Dim _entity As cQuestionaireEntityGroup
        Dim _node As TreeNode = tvwEntities.SelectedNode
        Dim _entNode As TreeNode
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
            _entNode = _node
        Else
            _entity = _node.Parent.Tag
            _entNode = _node.Parent
        End If
        _entity.Members.RemoveAt(grdGroupMembers.SelectedRows(0).Index)
        grdGroupMembers.Rows.Remove(grdGroupMembers.SelectedRows(0))
        UpdatePreview()
    End Sub

    Sub AddGroupMember(ByVal sender As Object, ByVal e As EventArgs)
        Dim _questionaire As cQuestionaire = cmbQuestionaire.SelectedItem
        Dim _entity As cQuestionaireEntityGroup
        Dim _node As TreeNode = tvwEntities.SelectedNode
        Dim _entNode As TreeNode
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
            _entNode = _node
        Else
            _entity = _node.Parent.Tag
            _entNode = _node.Parent
        End If

        Dim _member As cQuestionaireEntity
        If sender.text.ToString.Contains("input") Then
            _member = New cQuestionaireEntityInput(_questionaire)
        Else
            _member = New cQuestionaireEntityOutput(_questionaire)
        End If
        _entity.Members.Add(_member)
        grdGroupMembers.Rows(grdGroupMembers.Rows.Add).Tag = _member
        UpdatePreview()
    End Sub


    Private Sub grdGroupMembers_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdGroupMembers.CellValueNeeded
        Dim _entity As cQuestionaireEntity = grdGroupMembers.Rows(e.RowIndex).Tag
        Select Case e.ColumnIndex
            Case 0
                Select Case _entity.GetType.ToString
                    Case "Balthazar.cQuestionaireEntityInput"
                        e.Value = "Input"
                    Case "Balthazar.cQuestionaireEntityOutput"
                        e.Value = "Output"
                End Select
            Case 1
                e.Value = _entity.Name
            Case 2
                e.Value = _entity.Text
            Case 3
                e.Value = _entity.Value
        End Select
    End Sub

    Private Sub grdGroupMembers_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdGroupMembers.CellValuePushed
        Dim _entity As cQuestionaireEntity = grdGroupMembers.Rows(e.RowIndex).Tag
        Dim _questionaire As cQuestionaire = cmbQuestionaire.SelectedItem
        Dim _parEntity As cQuestionaireEntityGroup
        Dim _node As TreeNode = tvwEntities.SelectedNode
        If _node.Parent Is Nothing Then
            _parEntity = _node.Tag
        Else
            _parEntity = _node.Parent.Tag
        End If
        Select Case e.ColumnIndex
            Case 0
                Select Case e.Value
                    Case "Input"
                        Dim _newEntity As New cQuestionaireEntityInput(_questionaire)
                        _newEntity.Value = _entity.Value
                        _newEntity.Text = _entity.Text
                        _newEntity.Name = _entity.Name
                        grdGroupMembers.Rows(e.RowIndex).Tag = _newEntity
                        _parEntity.Members(e.RowIndex) = _newEntity
                    Case "Output"
                        Dim _newEntity As New cQuestionaireEntityOutput(_questionaire)
                        _newEntity.Value = _entity.Value
                        _newEntity.Text = _entity.Text
                        _newEntity.Name = _entity.Name
                        grdGroupMembers.Rows(e.RowIndex).Tag = _newEntity
                        _parEntity.Members(e.RowIndex) = _newEntity
                End Select
            Case 1
                _entity.Name = e.Value
            Case 2
                _entity.Text = e.Value
            Case 3
                _entity.Value = e.Value
        End Select
        UpdatePreview()
    End Sub

    Private Sub cmdPublish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPublish.Click

        Try
            Database.SaveQuestionaireToDB(cmbQuestionaire.SelectedItem, MyEvent.DatabaseID)
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("Publish failed." & vbCrLf & vbCrLf & ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        Windows.Forms.MessageBox.Show("Questionaire was successfully published.", "BALTHAZAR", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub cmbRequired_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRequired.SelectedIndexChanged
        Dim _entity As cQuestionaireEntity
        Dim _node As TreeNode = tvwEntities.SelectedNode
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
        Else
            _entity = _node.Parent.Tag
        End If
        If _entity.GetType Is GetType(cQuestionaireEntityInput) Then
            With DirectCast(_entity, cQuestionaireEntityInput)
                Select Case cmbRequired.SelectedIndex
                    Case 0
                        .Validate = cQuestionaireEntity.ValidateEnum.Inherit
                    Case 1
                        .Validate = cQuestionaireEntity.ValidateEnum.True
                    Case 2
                        .Validate = cQuestionaireEntity.ValidateEnum.False
                End Select
            End With
        End If
    End Sub

    Private Sub cmdSaveTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveTemplate.Click
        Dim _name As String = InputBox("Template name:", "BALTHAZAR", "")

        Database.SaveQuestionaireTemplate(_name, cmbQuestionaire.SelectedItem)
    End Sub

    Private Sub cmdOpenTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpenTemplate.Click
        Dim frmTemplate As New frmOpenTemplate
        Dim _questionaires As List(Of cQuestionaire) = Database.GetQuestionaireTemplates()

        For Each _quest As cQuestionaire In _questionaires
            frmTemplate.lstQuestionaires.Items.Add(_quest)
        Next
        If frmTemplate.ShowDialog = Windows.Forms.DialogResult.OK Then
            MyEvent.Questionaires.Add(frmTemplate.lstQuestionaires.SelectedItem)
            cmbQuestionaire.Items.Add(frmTemplate.lstQuestionaires.SelectedItem)
            cmbQuestionaire.SelectedItem = frmTemplate.lstQuestionaires.SelectedItem
        End If
    End Sub

    Private Sub cmdImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImport.Click

    End Sub

    Private Sub cmdCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopy.Click
        Dim _questionaire As cQuestionaire = cmbQuestionaire.SelectedItem
        Dim _entity As cQuestionaireEntityGroup
        Dim _node As TreeNode = tvwEntities.SelectedNode
        Dim _entNode As TreeNode
        If _node.Parent Is Nothing Then
            _entity = _node.Tag
            _entNode = _node
        Else
            _entity = _node.Parent.Tag
            _entNode = _node.Parent
        End If

        Dim _prevEntity As cQuestionaireEntityGroup = _entNode.PrevNode.Tag

        For Each _member As cQuestionaireEntity In _prevEntity.Members
            Dim _newMember As cQuestionaireEntity
            If _member.GetType Is GetType(cQuestionaireEntityInput) Then
                _newMember = New cQuestionaireEntityInput(_questionaire)
            Else
                _newMember = New cQuestionaireEntityOutput(_questionaire)
            End If
            _newMember.Text = _member.Text
            _newMember.Value = _member.Value
            _newMember.Name = _member.Name
            _entity.Members.Add(_newMember)
            grdGroupMembers.Rows(grdGroupMembers.Rows.Add).Tag = _newMember
        Next
        UpdatePreview()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frmSummary.Show()
        Exit Sub
        'Dim fs As New System.IO.FileStream("c:\out.xls", IO.FileMode.Create)
        'Dim sw As New System.IO.StreamWriter(fs, System.Text.Encoding.Unicode)


        'Dim tmp As String = Database.CampaignSummary
        'wbPreview.DocumentText = "<pre>" & tmp & "</pre>"
        'Dim doc As New Xml.XmlDocument
        'doc.LoadXml(tmp)
        'lb1.Items.Clear()

        ''The items which are of type RATING
        'Dim tmpnodelist As Xml.XmlNodeList = doc.SelectNodes("/questionnaires/questionaire/input[@type='rating']")

        'Dim answerCollection As New Dictionary(Of String, Double)
        'Dim averageCollection As New Dictionary(Of String, Double)

        'For Each answer As Xml.XmlNode In tmpnodelist
        '    Dim nm As String
        '    Dim ans As Integer
        '    Debug.Print(answer.Attributes("name").Value & " - " & answer.Attributes("answer").Value)
        '    If answerCollection.ContainsKey(answer.Attributes("name").Value) Then
        '        answerCollection(answer.Attributes("name").Value) += answer.Attributes("answer").Value
        '    Else
        '        answerCollection.Add(answer.Attributes("name").Value, answer.Attributes("answer").Value)
        '    End If
        'Next

        'For Each kvp As KeyValuePair(Of String, Double) In answerCollection

        '    averageCollection.Add(kvp.Key, kvp.Value / answerCollection.Count)
        '    lb1.Items.Add(kvp.Key & " - " & kvp.Value / answerCollection.Count)
        'Next

        ''The items which are of type TEXT
        'tmpnodelist = doc.SelectNodes("/questionnaires/questionaire/group/input[@type='text']")

        'answerCollection = New Dictionary(Of String, Double)()
        'averageCollection = New Dictionary(Of String, Double)()
        'Dim badkeys As New List(Of String)

        'For Each answer As Xml.XmlNode In tmpnodelist
        '    Dim nm As String
        '    Dim ans As Integer
        '    Dim testdouble As Double
        '    ' If Not badkeys.Contains(answer.Attributes("name").Value) Then
        '    Try
        '        sw.WriteLine(answer.Attributes("name").Value & vbTab & answer.Attributes("answer").Value)
        '        If answerCollection.ContainsKey(answer.Attributes("name").Value) Then
        '            answerCollection(answer.Attributes("name").Value) += CDbl(answer.Attributes("answer").Value)

        '        Else
        '            answerCollection.Add(answer.Attributes("name").Value, CDbl(answer.Attributes("answer").Value))

        '        End If
        '    Catch
        '        ' badkeys.Add(answer.Attributes("name").Value)
        '    End Try
        '    '  End If
        '    sw.Flush()
        'Next
        'sw.Close()
        'fs.Close()
        ''For Each item As Object In badkeys
        ''    answerCollection.Remove(item)
        ''Next

        'For Each kvp As KeyValuePair(Of String, Double) In answerCollection
        '    Debug.Print(kvp.Key & " - " & kvp.Value)
        '    averageCollection.Add(kvp.Key, kvp.Value / answerCollection.Count)
        '    lb1.Items.Add(kvp.Key & " - " & kvp.Value / answerCollection.Count)
        'Next




    End Sub
End Class