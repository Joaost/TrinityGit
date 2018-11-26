Public Class frmSetup

    Private Sub cmdAddInternalContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddInternalContact.Click
        With grdInternalContacts.Rows(grdInternalContacts.Rows.Add)
            .Tag = MyEvent.InternalContacts.Add
        End With
    End Sub

    Private Sub grdInternalContacts_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdInternalContacts.RowEnter
        MyEvent.InternalContacts.Roles.Clear()
        MyEvent.InternalContacts.Roles.Add("Kundansvarig", "Kundansvarig")
        MyEvent.InternalContacts.Roles.Add("Projektledare", "Projektledare")
        MyEvent.InternalContacts.Roles.Add("Produktionsledare", "Produktionsledare")
        MyEvent.InternalContacts.Roles.Add("Teamledare", "Teamledare")
        For Each TmpContact As cContact In MyEvent.InternalContacts
            If Not MyEvent.InternalContacts.Roles.Contains(TmpContact.Role) AndAlso Not TmpContact.Role Is Nothing AndAlso Not TmpContact.Role = "" Then
                MyEvent.InternalContacts.Roles.Add(TmpContact.Role, TmpContact.Role)
            End If
        Next
        With grdInternalContacts.Rows(e.RowIndex)
            With CType(.Cells("colRole"), Balthazar.ExtendedComboBoxCell)
                .ComboBox.DropDownStyle = ComboBoxStyle.DropDown
                .Items.clear()
                For Each TmpRole As cRole In MyEvent.InternalContacts.Roles
                    .Items.add(TmpRole.Name)
                Next
            End With
        End With
    End Sub

    Private Sub grdInternalContacts_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdInternalContacts.CellValueNeeded
        Dim TmpContact As cContact = grdInternalContacts.Rows(e.RowIndex).Tag

        Select Case grdInternalContacts.Columns(e.ColumnIndex).Name
            Case "colRole"
                e.Value = TmpContact.Role
            Case "colName"
                e.Value = TmpContact.Name
            Case "colPhone"
                e.Value = TmpContact.PhoneNr
            Case "colDefault"
                e.Value = TmpContact Is MyEvent.InternalContacts.DefaultContact
        End Select

    End Sub


    Private Sub grdInternalContacts_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdInternalContacts.CellValuePushed
        Dim TmpContact As cContact = grdInternalContacts.Rows(e.RowIndex).Tag

        Select Case grdInternalContacts.Columns(e.ColumnIndex).Name
            Case "colRole"
                TmpContact.Role = e.Value
            Case "colName"
                TmpContact.Name = e.Value
            Case "colPhone"
                TmpContact.PhoneNr = e.Value
            Case "colDefault"
                If e.Value Then
                    MyEvent.InternalContacts.DefaultContact = TmpContact
                Else
                    MyEvent.InternalContacts.DefaultContact = Nothing
                End If
                grdInternalContacts.Invalidate()
        End Select

    End Sub

    Private Sub cmdAddExternalContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddExternalContact.Click
        With grdExternalContacts.Rows(grdExternalContacts.Rows.Add)
            .Tag = MyEvent.ExternalContacts.Add
        End With
    End Sub

    Private Sub grdExternalContacts_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdExternalContacts.RowEnter
        MyEvent.ExternalContacts.Roles.Clear()
        For Each TmpContact As cContact In MyEvent.ExternalContacts
            If Not MyEvent.ExternalContacts.Roles.Contains(TmpContact.Role) AndAlso Not TmpContact.Role Is Nothing AndAlso Not TmpContact.Role = "" Then
                MyEvent.ExternalContacts.Roles.Add(TmpContact.Role, TmpContact.Role)
            End If
        Next
        With grdExternalContacts.Rows(e.RowIndex)
            With CType(.Cells("colExRole"), Balthazar.ExtendedComboBoxCell)
                .ComboBox.DropDownStyle = ComboBoxStyle.DropDown
                .Items.clear()
                For Each TmpRole As cRole In MyEvent.ExternalContacts.Roles
                    .Items.add(TmpRole.Name)
                Next
            End With
        End With
    End Sub

    Private Sub grdExternalContacts_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdExternalContacts.CellValueNeeded
        Dim TmpContact As cContact = grdExternalContacts.Rows(e.RowIndex).Tag

        Select Case grdExternalContacts.Columns(e.ColumnIndex).Name
            Case "colExRole"
                e.Value = TmpContact.Role
            Case "colExName"
                e.Value = TmpContact.Name
            Case "colExPhone"
                e.Value = TmpContact.PhoneNr
            Case "colExDefault"
                e.Value = TmpContact Is MyEvent.ExternalContacts.DefaultContact
        End Select

    End Sub


    Private Sub grdExternalContacts_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdExternalContacts.CellValuePushed
        Dim TmpContact As cContact = grdExternalContacts.Rows(e.RowIndex).Tag

        Select Case grdExternalContacts.Columns(e.ColumnIndex).Name
            Case "colExRole"
                TmpContact.Role = e.Value
            Case "colExName"
                TmpContact.Name = e.Value
            Case "colExPhone"
                TmpContact.PhoneNr = e.Value
            Case "colExDefault"
                If e.Value Then
                    MyEvent.ExternalContacts.DefaultContact = TmpContact
                Else
                    MyEvent.ExternalContacts.DefaultContact = Nothing
                End If
                grdExternalContacts.Invalidate()
        End Select

    End Sub

    Private Sub cmdRemoveInternalContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveInternalContact.Click
        For Each TmpRow As DataGridViewRow In grdInternalContacts.SelectedRows
            MyEvent.InternalContacts.Remove(TmpRow.Tag)
            grdInternalContacts.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub cmdRemoveExternalContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveExternalContact.Click
        For Each TmpRow As DataGridViewRow In grdExternalContacts.SelectedRows
            MyEvent.ExternalContacts.Remove(TmpRow.Tag)
            grdExternalContacts.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub frmSetup_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        txtName.Text = MyEvent.Name
        cmbClients.Items.Clear()
        grdInternalContacts.Rows.Clear()
        grdExternalContacts.Rows.Clear()
        For Each TmpContact As cContact In MyEvent.InternalContacts
            With grdInternalContacts.Rows(grdInternalContacts.Rows.Add)
                .Tag = TmpContact
            End With
        Next
        For Each TmpContact As cContact In MyEvent.ExternalContacts
            With grdExternalContacts.Rows(grdExternalContacts.Rows.Add)
                .Tag = TmpContact
            End With
        Next

        For Each TmpClient As cClient In Database.Clients
            cmbClients.Items.Add(TmpClient)
            If Not MyEvent.Client Is Nothing Then
                If TmpClient.ID = MyEvent.Client.ID Then
                    cmbClients.SelectedItem = TmpClient
                End If
            End If
        Next

        txtBackground.Text = MyEvent.Campaign.Background
        txtWhat.Text = MyEvent.Campaign.What
        txtPurpose.Text = MyEvent.Campaign.Purpose
        txtHow.Text = MyEvent.Campaign.How
        txtWhen.Text = MyEvent.Campaign.WhenIsIt

        txtMission.Text = MyEvent.OurMission
        txtTarget.Text = MyEvent.Target
        txtCoreValues.Text = MyEvent.CoreValues
        txtMessage.Text = MyEvent.Message

        grdPurpose.Rows.Clear()
        For Each s As String In MyEvent.Purposes
            grdPurpose.Rows.Add()
        Next

        grdGoals.Rows.Clear()
        For Each s As String In MyEvent.Goals
            grdGoals.Rows.Add()
        Next

        chkInStore.Checked = MyEvent.UseInStore
    End Sub

    Private Sub frmSetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Private Sub cmbClients_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClients.SelectedIndexChanged
        MyEvent.Client = cmbClients.SelectedItem
        cmbProducts.Items.Clear()
        For Each TmpProduct As cProduct In MyEvent.Client.Products
            cmbProducts.Items.Add(TmpProduct)
            If Not MyEvent.Product Is Nothing Then
                If TmpProduct.ID = MyEvent.Product.ID Then
                    cmbProducts.SelectedItem = TmpProduct
                End If
            End If
        Next
    End Sub

    Private Sub cmbProducts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProducts.SelectedIndexChanged
        MyEvent.Product = cmbProducts.SelectedItem
        If grdInternalContacts.Rows.Count = 0 Then
            For Each TmpContact As cContact In MyEvent.Product.InternalContacts
                With grdInternalContacts.Rows(grdInternalContacts.Rows.Add)
                    .Tag = MyEvent.InternalContacts.Add
                    With CType(.Tag, cContact)
                        .Name = TmpContact.Name
                        .PhoneNr = TmpContact.PhoneNr
                        .Role = TmpContact.Role
                    End With
                End With
            Next
        End If
        If grdExternalContacts.Rows.Count = 0 Then
            For Each TmpContact As cContact In MyEvent.Product.ExternalContacts
                With grdExternalContacts.Rows(grdExternalContacts.Rows.Add)
                    .Tag = MyEvent.ExternalContacts.Add
                    With CType(.Tag, cContact)
                        .Name = TmpContact.Name
                        .PhoneNr = TmpContact.PhoneNr
                        .Role = TmpContact.Role
                    End With
                End With
            Next
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        MyEvent.Name = txtName.Text
        Me.Text = "Balthazar - " & txtName.Text
    End Sub

    Private Sub picHideContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picHideContacts.Click
        picShowContacts.Visible = True
        grpContacts.Height = 16
        picHideContacts.Visible = False
        grpCampaign.Top = grpContacts.Bottom + 6
        grpProject.Top = grpCampaign.Bottom + 6
    End Sub

    Private Sub picShowContacts_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picShowContacts.Click
        picHideContacts.Visible = True
        grpContacts.Height = grdExternalContacts.Bottom + 6
        picShowContacts.Visible = False
        grpCampaign.Top = grpContacts.Bottom + 6
        grpProject.Top = grpCampaign.Bottom + 6
    End Sub

    Private Sub picHideCampaign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picHideCampaign.Click
        picShowCampaign.Visible = True
        grpCampaign.Height = 16
        picHideCampaign.Visible = False
        grpProject.Top = grpCampaign.Bottom + 6
    End Sub

    Private Sub picShowCampaign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picShowCampaign.Click
        picHideCampaign.Visible = True
        grpCampaign.Height = txtWhen.Bottom + 6
        picShowCampaign.Visible = False
        grpProject.Top = grpCampaign.Bottom + 6
    End Sub

    Private Sub picHideProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picHideProject.Click
        picShowProject.Visible = True
        grpProject.Height = 16
        picHideProject.Visible = False
    End Sub

    Private Sub picShowProject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picShowProject.Click
        picHideProject.Visible = True
        grpProject.Height = grdGoals.Bottom + 6
        picShowProject.Visible = False
    End Sub

    Private Sub txtWhat_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWhat.TextChanged
        MyEvent.Campaign.What = txtWhat.Text
    End Sub

    Private Sub txtPurpose_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPurpose.TextChanged
        MyEvent.Campaign.Purpose = txtPurpose.Text
    End Sub

    Private Sub txtHow_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHow.TextChanged
        MyEvent.Campaign.How = txtHow.Text
    End Sub

    Private Sub txtWhen_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWhen.TextChanged
        MyEvent.Campaign.WhenIsIt = txtWhen.Text
    End Sub

    Private Sub txtBackground_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBackground.TextChanged
        MyEvent.Campaign.Background = txtBackground.Text
    End Sub

    Private Sub cmdAddGoal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddGoal.Click
        MyEvent.Goals.Add("")
        grdGoals.Rows.Add()
    End Sub

    Private Sub cmdAddPurpose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddPurpose.Click
        MyEvent.Purposes.Add("")
        grdPurpose.Rows.Add()
    End Sub

    Private Sub grdGoals_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdGoals.CellValueNeeded
        e.Value = MyEvent.Goals(e.RowIndex)
    End Sub

    Private Sub grdGoals_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdGoals.CellValuePushed
        MyEvent.Goals(e.RowIndex) = e.Value
    End Sub

    Private Sub grdPurpose_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdPurpose.CellValueNeeded
        e.Value = MyEvent.Purposes(e.RowIndex)
    End Sub

    Private Sub grdPurpose_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdPurpose.CellValuePushed
        MyEvent.Purposes(e.RowIndex) = e.Value
    End Sub

    Private Sub txtMission_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMission.TextChanged
        MyEvent.OurMission = txtMission.Text
    End Sub

    Private Sub txtTarget_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTarget.TextChanged
        MyEvent.Target = txtTarget.Text
    End Sub

    Private Sub cmdRemoveGoal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveGoal.Click
        For Each TmpRow As DataGridViewRow In grdGoals.SelectedRows
            MyEvent.Goals.RemoveAt(TmpRow.Index)
            grdGoals.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub cmdRemovePurpose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemovePurpose.Click
        For Each TmpRow As DataGridViewRow In grdPurpose.SelectedRows
            MyEvent.Purposes.RemoveAt(TmpRow.Index)
            grdPurpose.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub txtMessage_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMessage.TextChanged
        MyEvent.Message = txtMessage.Text
    End Sub

    Private Sub txtCoreValues_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCoreValues.TextChanged
        MyEvent.CoreValues = txtCoreValues.Text
    End Sub

    Private Sub tpQA_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpQA.Enter
        grdQuestions.Rows.Clear()
        For Each TmpQA As cQuestionAndAnswer In MyEvent.QuestionAndAnswers
            grdQuestions.Rows(grdQuestions.Rows.Add).Tag = TmpQA
        Next
    End Sub

    Private Sub cmdAddQuestion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddQuestion.Click
        Dim TmpQA As New cQuestionAndAnswer
        grdQuestions.Rows(grdQuestions.Rows.Add).Tag = TmpQA
        MyEvent.QuestionAndAnswers.Add(TmpQA)
    End Sub

    Private Sub grdQuestions_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdQuestions.CellValueNeeded
        Dim TmpQA As cQuestionAndAnswer = grdQuestions.Rows(e.RowIndex).Tag

        Select Case grdQuestions.Columns(e.ColumnIndex).HeaderText
            Case "Question"
                e.Value = TmpQA.Question
            Case "Answer"
                e.Value = TmpQA.Answer
        End Select

    End Sub

    Private Sub grdQuestions_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdQuestions.CellValuePushed
        Dim TmpQA As cQuestionAndAnswer = grdQuestions.Rows(e.RowIndex).Tag

        Select Case grdQuestions.Columns(e.ColumnIndex).HeaderText
            Case "Question"
                TmpQA.Question = e.Value
            Case "Answer"
                TmpQA.Answer = e.Value
        End Select
    End Sub

    Private Sub cmdAddTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddTemplate.Click
        Dim TmpTemplate As New cTemplate
        grdTemplates.Rows(grdTemplates.Rows.Add()).Tag = TmpTemplate
        MyEvent.Templates.Add(TmpTemplate)
    End Sub

    Private Sub cmdRemoveTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveTemplate.Click
        For Each TmpRow As DataGridViewRow In grdTemplates.SelectedRows
            MyEvent.Templates.Remove(TmpRow.Tag)
            grdTemplates.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub grdTemplates_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTemplates.CellContentClick
        Dim TmpTemplate As cTemplate = grdTemplates.Rows(e.RowIndex).Tag
        If e.ColumnIndex = 2 Then
            Dim frmEdit As New frmRTFEditor(TmpTemplate)
            frmEdit.ShowDialog()
        End If
    End Sub

    Private Sub grdTemplates_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdTemplates.CellValueNeeded
        Dim TmpTemplate As cTemplate = grdTemplates.Rows(e.RowIndex).Tag
        Select Case grdTemplates.Columns(e.ColumnIndex).HeaderText
            Case "Text"
                e.Value = "Edit"
            Case "Name"
                e.Value = TmpTemplate.Name
            Case "Description"
                e.Value = TmpTemplate.Description
        End Select
    End Sub

    Private Sub grdTemplates_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdTemplates.CellValuePushed
        Dim TmpTemplate As cTemplate = grdTemplates.Rows(e.RowIndex).Tag
        Select Case grdTemplates.Columns(e.ColumnIndex).HeaderText
            Case "Name"
                TmpTemplate.Name = e.Value
            Case "Description"
                TmpTemplate.Description = e.Value
        End Select

    End Sub

    Private Sub tpTemplates_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpTemplates.Enter
        grdTemplates.Rows.Clear()
        For Each TmpTemplate As cTemplate In MyEvent.Templates
            grdTemplates.Rows(grdTemplates.Rows.Add).Tag = TmpTemplate
        Next
    End Sub

    Private Sub cmdRemoveQuestion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveQuestion.Click
        For Each TmpRow As DataGridViewRow In grdQuestions.SelectedRows
            MyEvent.QuestionAndAnswers.Remove(TmpRow.Tag)
            grdQuestions.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub cmdAddClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddClient.Click
        If frmAddClient.ShowDialog() = Windows.Forms.DialogResult.OK Then
            cmbClients.Items.Add(Database.AddClient(frmAddClient.txtName.Text))
        End If
    End Sub

    Private Sub cmdAddProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddProduct.Click
        If frmAddProduct.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim TmpProd As cProduct = Database.AddProduct(frmAddProduct.txtName.Text, DirectCast(cmbClients.SelectedItem, cClient).ID)
            For Each TmpRow As DataGridViewRow In frmAddProduct.grdInternalContacts.Rows
                Database.AddContact(TmpRow.Cells(0).Value, TmpRow.Cells(1).Value, TmpRow.Cells(2).Value, TmpProd.ID, True)
            Next
            For Each TmpRow As DataGridViewRow In frmAddProduct.grdExternalContacts.Rows
                Database.AddContact(TmpRow.Cells(0).Value, TmpRow.Cells(1).Value, TmpRow.Cells(2).Value, TmpProd.ID, False)
            Next
            cmbProducts.Items.Add(Database.GetProductByID(TmpProd.ID))
        End If
    End Sub

    Private Sub chkInStore_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInStore.CheckedChanged
        frmMain.cmdInstore.Enabled = chkInStore.Checked
        frmMain.cmdSchedule.Enabled = Not chkInStore.Checked
        MyEvent.UseInStore = chkInStore.Checked
    End Sub
End Class
