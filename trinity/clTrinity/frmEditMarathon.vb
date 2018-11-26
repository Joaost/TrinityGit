Imports System.Windows.Forms

Public Class frmEditMarathon

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'If someone tries to set planNr to nothing or generalOrder then the values sets to zero 
        If txtPlanNr.Text = "" Then
            txtPlanNr.Text = 0
        End If
        If txtGeneralOrder.Text = "" Then
            txtGeneralOrder.Text = 0
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class
