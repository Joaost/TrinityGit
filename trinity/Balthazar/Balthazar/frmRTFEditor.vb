Public Class frmRTFEditor
    Private _template As cTemplate

    Private Sub cmdBold_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBold.Click
        rtxOutline.SelectionFont = New Font(rtxOutline.SelectionFont, rtxOutline.SelectionFont.Style - (rtxOutline.SelectionFont.Style And FontStyle.Bold) + (FontStyle.Bold - (rtxOutline.SelectionFont.Style And FontStyle.Bold)))
        UpdateToolbar()
    End Sub

    Private Sub cmdItalics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdItalics.Click
        rtxOutline.SelectionFont = New Font(rtxOutline.SelectionFont, rtxOutline.SelectionFont.Style - (rtxOutline.SelectionFont.Style And FontStyle.Italic) + (FontStyle.Italic - (rtxOutline.SelectionFont.Style And FontStyle.Italic)))
        UpdateToolbar()
    End Sub

    Private Sub cmdUnderlined_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnderlined.Click
        rtxOutline.SelectionFont = New Font(rtxOutline.SelectionFont, rtxOutline.SelectionFont.Style - (rtxOutline.SelectionFont.Style And FontStyle.Underline) + (FontStyle.Underline - (rtxOutline.SelectionFont.Style And FontStyle.Underline)))
        UpdateToolbar()
    End Sub

    Private Sub cmdLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLeft.Click
        rtxOutline.SelectionAlignment = HorizontalAlignment.Left
    End Sub

    Private Sub cmdCentered_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCentered.Click
        rtxOutline.SelectionAlignment = HorizontalAlignment.Center
    End Sub

    Private Sub cmdRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRight.Click
        rtxOutline.SelectionAlignment = HorizontalAlignment.Right
    End Sub

    Private Updating As Boolean = False
    Private Sub UpdateToolbar()
        Updating = True
        If rtxOutline.SelectionFont IsNot Nothing Then
            cmdBold.Checked = rtxOutline.SelectionFont.Style And FontStyle.Bold
            cmdItalics.Checked = rtxOutline.SelectionFont.Style And FontStyle.Italic
            cmdUnderlined.Checked = rtxOutline.SelectionFont.Style And FontStyle.Underline
            cmbSize.Text = CInt(rtxOutline.SelectionFont.Size)
        End If
        cmdBullets.Checked = rtxOutline.SelectionBullet
        Updating = False
    End Sub

    Private Sub rtxOutline_CursorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtxOutline.CursorChanged
        Stop
    End Sub

    Private Sub rtxOutline_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles rtxOutline.KeyPress
    End Sub

    Private Sub rtxOutline_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rtxOutline.KeyUp
        If e.KeyCode = Keys.F3 Then
            rtxOutline.Find(rtxOutline.SelectedText, rtxOutline.SelectionStart + rtxOutline.SelectionLength, RichTextBoxFinds.None)
        End If
    End Sub

    Private Sub rtxOutline_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtxOutline.SelectionChanged
        UpdateToolbar()
    End Sub

    Private Sub cmbSize_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbSize.KeyUp
        rtxOutline.SelectionFont = New Font(rtxOutline.SelectionFont.FontFamily, CSng(cmbSize.Text), rtxOutline.SelectionFont.Style)
    End Sub

    Private Sub cmbSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSize.SelectedIndexChanged
        If Updating Then Exit Sub
        rtxOutline.SelectionFont = New Font(rtxOutline.SelectionFont.FontFamily, CSng(cmbSize.Text), rtxOutline.SelectionFont.Style)
    End Sub

    Private Sub rtxOutline_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtxOutline.TextChanged
        _template.Text = rtxOutline.Rtf
        cmdUndo.ToolTipText = "Undo " & rtxOutline.UndoActionName
        cmdRedo.ToolTipText = "Redo " & rtxOutline.RedoActionName
        cmdUndo.Enabled = (rtxOutline.UndoActionName <> "")
        cmdRedo.Enabled = (rtxOutline.RedoActionName <> "")
    End Sub

    Private Sub cmdBullets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBullets.Click
        rtxOutline.SelectionBullet = Not rtxOutline.SelectionBullet
    End Sub

    Public Sub New(ByRef Template As cTemplate)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _template = Template
        rtxOutline.Rtf = Template.Text
    End Sub

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        Dim TmpString As String = InputBox("Search string:", "BALTHAZAR", "")
        If TmpString = "" Then Exit Sub
        rtxOutline.Find(TmpString, rtxOutline.SelectionStart + rtxOutline.SelectionLength, RichTextBoxFinds.None)
    End Sub

    Private Sub cmdInsertImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInsertImage.Click
        If ofdRTF.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim TmpBMP As New Bitmap(ofdRTF.FileName)
            Clipboard.SetImage(TmpBMP)
            rtxOutline.Paste()
        End If
    End Sub

    Private Sub cmdTextColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTextColor.Click
        cdlRTF.Color = rtxOutline.SelectionColor
        If cdlRTF.ShowDialog() = Windows.Forms.DialogResult.OK Then
            rtxOutline.SelectionColor = cdlRTF.Color
        End If
    End Sub

    Private Sub cmdUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUndo.Click
        rtxOutline.Undo()
    End Sub

    Private Sub cmdRedo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRedo.Click
        rtxOutline.Redo()
    End Sub
End Class