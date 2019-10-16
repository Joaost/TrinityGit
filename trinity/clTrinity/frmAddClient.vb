Imports System.Windows.Forms

Public Class frmAddClient
    'This class either adds or updates a Client name
    Dim rd As Odbc.OdbcDataReader

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        'Dim com As New Odbc.OdbcCommand("SELECT * FROM Clients", DBConn)
        If txtName.Text <> "" Then ' there need to be a text in hte field
            If Me.Tag = "" Then
                If DBReader.ClientExist(txtName.Text.Trim) Then
                    If MessageBox.Show("There is already a client called '" & txtName.Text & "' in the database." & vbCrLf & vbCrLf & "Are you sure you want to add another one?", "T R I N I T Y", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
                        Exit Sub
                    End If
                End If
                'rd = com.ExecuteReader
                'While rd.Read
                '    If UCase(rd!Name) = UCase(txtName.Text) Then 'make sure the name is unique
                '        If MessageBox.Show("There is already a client called '" & txtName.Text & "' in the database." & vbCrLf & vbCrLf & "Are you sure you want to add another one?", "T R I N I T Y", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then Exit While
                '        Exit Sub
                '    End If
                'End While
                'rd.Close()
                'if all is ok then we add the new CLient
                Dim newClientTemp As New Client
                If chkBoxRestrictedBool.Checked Then
                    newClientTemp.restricted = True
                End If
                newClientTemp.name = txtName.Text
                DBReader.addClient(newClientTemp)
                'com.CommandText = "INSERT INTO Clients (Name) VALUES ('" & txtName.Text & "')"
                'com.ExecuteScalar()
            Else 'edit the client
                DBReader.updateClient(txtName.Text, Campaign.ClientID)
                'com.CommandText = "UPDATE Clients SET Name='" & txtName.Text & "' WHERE id=" & Campaign.ClientID
                'com.ExecuteScalar()
            End If
        End If
        Me.Hide()

    End Sub

    Private Sub frmAddClient_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'if edit we load the current name into the text field
        If Me.Tag = "EDIT" Then
            txtName.Text = Campaign.Client
        End If
        If DBReader.isLocal Then
            MsgBox("You are using a Local database and all changes you make will be lost when you connect to network.", MsgBoxStyle.Information, "FYI")
        End If
    End Sub
End Class