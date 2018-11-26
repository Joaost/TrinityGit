Imports System.Windows.Forms

Public Class OptionsPicker

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If rdbSkip.Checked Then
            Me.DialogResult = Windows.Forms.DialogResult.No
        Else
            Me.DialogResult = cmb.SelectedIndex + 1
        End If
        Me.Close()
    End Sub

    Private Sub rdbRename_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbRename.CheckedChanged
        If rdbRename.Checked Then
            cmb.Enabled = True
        Else
            cmb.Enabled = False
        End If
    End Sub

    Public Sub New(ByVal l As List(Of String))

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        For Each s As String In l
            cmb.Items.Add(s)
        Next
    End Sub

End Class
