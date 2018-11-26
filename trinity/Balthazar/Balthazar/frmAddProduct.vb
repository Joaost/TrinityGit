Public Class frmAddProduct

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub


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
            Case "colExPhoneNr"
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
            Case "colExPhoneNr"
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

End Class