Public Class frmLinkCampaign

    Private Sub chkAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAuto.CheckedChanged
        pnlOptions.Enabled = chkAuto.Checked
        If Not chkAuto.Checked Then
            Campaign.AutoLinkCampaigns = Trinity.cKampanj.AutoLinkCampaignsEnum.DoNotAutoLink
        Else
            Campaign.AutoLinkCampaigns = SelectedRadio()
        End If
    End Sub

    Private Sub grdFileLinks_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdFileLinks.CellValueNeeded
        Dim _linkedCampaign As Trinity.cLinkedCampaign = grdFileLinks.Rows(e.RowIndex).Tag
        Select Case e.ColumnIndex
            Case 0
                e.Value = _linkedCampaign.Name
            Case 1
                e.Value = _linkedCampaign.Path
            Case 2
                e.Value = _linkedCampaign.Link
        End Select
    End Sub

    Private Sub grdFileLinks_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdFileLinks.CellValuePushed
        Dim _linkedCampaign As Trinity.cLinkedCampaign = grdFileLinks.Rows(e.RowIndex).Tag
        Select Case e.ColumnIndex
            Case 2
                _linkedCampaign.Link = e.Value
        End Select
    End Sub

    Public Function SelectedRadio() As Trinity.cKampanj.AutoLinkCampaignsEnum
        If optAll.Checked Then
            Return Trinity.cKampanj.AutoLinkCampaignsEnum.LinkToAllCampaigns
        ElseIf optSameClient.Checked Then
            Return Trinity.cKampanj.AutoLinkCampaignsEnum.LinkToSameClient
        ElseIf optSameProd.Checked Then
            Return Trinity.cKampanj.AutoLinkCampaignsEnum.LinkToSameProduct
        End If
    End Function

    Private Sub cmdAddLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddLink.Click
        If TrinitySettings.SaveCampaignsAsFiles Then
            Dim dlgOpen As New Windows.Forms.OpenFileDialog
            dlgOpen.Filter = "Trinity campaigns|*.cmp"
            dlgOpen.FileName = "*.cmp"
            dlgOpen.Title = "Select file to link to..."
            If dlgOpen.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim _xml As New Xml.XmlDocument
                _xml.Load(dlgOpen.FileName)

                Dim _linkedCampaign As New Trinity.cLinkedCampaign With {.Link = True, .Name = DirectCast(_xml.GetElementsByTagName("Campaign").Item(0), Xml.XmlElement).GetAttribute("Name"), .Path = dlgOpen.FileName}
                Campaign.LinkedCampaigns.Add(_linkedCampaign)
                grdFileLinks.Rows(grdFileLinks.Rows.Add).Tag = _linkedCampaign

            End If
        Else
            Dim dlgOpen As New frmOpenFromDB With {.DoNotLoadCampaign = True}
            If dlgOpen.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim _xml As New Xml.XmlDocument
                _xml.Load(DBReader.GetCampaign(dlgOpen.CampaignID, True))

                Dim _linkedCampaign As New Trinity.cLinkedCampaign With {.Link = True, .Name = DirectCast(_xml.GetElementsByTagName("Campaign").Item(0), Xml.XmlElement).GetAttribute("Name"), .DatabaseID = dlgOpen.CampaignID}
                Campaign.LinkedCampaigns.Add(_linkedCampaign)
                grdFileLinks.Rows(grdFileLinks.Rows.Add).Tag = _linkedCampaign

            End If
        End If
    End Sub

    Private Sub cmdAddNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNow.Click
        Campaign.UpdateLinkedCampaignList()
        UpdateList()
    End Sub

    Private Sub cmdDeleteLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteLink.Click
        If Windows.Forms.MessageBox.Show("A campaign that has been automatically added to a campaign will" & vbCrLf _
                                      & "re-appear at next update." & vbCrLf _
                                      & "To prevent the campaign from being linked, untick 'Link' in the list.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OKCancel, Windows.Forms.MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
            For Each _row As Windows.Forms.DataGridViewRow In grdFileLinks.SelectedRows
                Dim _linkedCampaign As Trinity.cLinkedCampaign = _row.Tag
                Campaign.LinkedCampaigns.Remove(_linkedCampaign)
                grdFileLinks.Rows.Remove(_row)
            Next
        End If
    End Sub

    Sub UpdateList()
        Dim _broken As Boolean = False
        grdFileLinks.Rows.Clear()
        For Each _link As Trinity.cLinkedCampaign In Campaign.LinkedCampaigns
            grdFileLinks.Rows(grdFileLinks.Rows.Add).Tag = _link
            If _link.BrokenLink Then
                grdFileLinks.Rows(grdFileLinks.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Red
                _broken = True
            End If
            If Not TrinitySettings.SaveCampaignsAsFiles AndAlso _link.DatabaseID = 0 Then
                grdFileLinks.Rows(grdFileLinks.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Blue
            End If
        Next
        If _broken Then
            Windows.Forms.MessageBox.Show("One or more campaigns have been (re)moved and will no" & vbCrLf & "longer be linked. These are marked with red.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        End If
        If Not TrinitySettings.SaveCampaignsAsFiles AndAlso Campaign.LinkedCampaigns.Where(Function(c) c.DatabaseID = 0).Count > 0 Then
            cmdFindInDB.Visible = True
            Windows.Forms.MessageBox.Show("One or more links points to a file when they should" & vbCrLf & "point to the database. These are marked in blue" & vbCrLf & vbCrLf & "To resolve, click the magnifying glass.")
        End If
    End Sub

    Private Sub frmLinkCampaign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If TrinitySettings.SaveCampaignsAsFiles Then
            chkAuto.Text = "Automatically add campaigns in the same folder as this campaign"
        Else
            chkAuto.Text = "Automatically add campaigns"
            optAll.Enabled = False
            optSameClient.Checked = True
        End If
        UpdateList()
        If Campaign.AutoLinkCampaigns > Trinity.cKampanj.AutoLinkCampaignsEnum.DoNotAutoLink Then
            Select Case Campaign.AutoLinkCampaigns
                Case Trinity.cKampanj.AutoLinkCampaignsEnum.LinkToAllCampaigns
                    optAll.Checked = True
                Case Trinity.cKampanj.AutoLinkCampaignsEnum.LinkToSameClient
                    optSameClient.Checked = True
                Case Trinity.cKampanj.AutoLinkCampaignsEnum.LinkToSameProduct
                    optSameProd.Checked = True
            End Select
            chkAuto.Checked = True
        Else
            chkAuto.Checked = False
        End If
    End Sub

    Private Sub RadioButtons_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAll.CheckedChanged, optSameClient.CheckedChanged, optSameProd.CheckedChanged
        Campaign.AutoLinkCampaigns = SelectedRadio()
    End Sub

    Private Sub cmdFindInDB_Click(sender As System.Object, e As System.EventArgs) Handles cmdFindInDB.Click
        Dim _replace As New Dictionary(Of Trinity.cLinkedCampaign, Trinity.cLinkedCampaign)
        For Each _link As Trinity.cLinkedCampaign In Campaign.LinkedCampaigns.Where(Function(c) c.DatabaseID = 0)
            Dim _camps As List(Of CampaignEssentials) = DBReader.GetCampaigns("SELECT * FROM campaigns WHERE originallocation='" & _link.Path & "'")
            If _camps.Count > 0 Then
                Dim _linkCamp As New Trinity.cLinkedCampaign With {.DatabaseID = _camps(0).id, .ClientID = _camps(0).client, .ProductID = _camps(0).product, .Name = _camps(0).name, .Link = True}
                _replace.Add(_link, _linkCamp)
            End If
        Next
        For Each _kv As KeyValuePair(Of Trinity.cLinkedCampaign, Trinity.cLinkedCampaign) In _replace
            Campaign.LinkedCampaigns.Remove(_kv.Key)
            Campaign.LinkedCampaigns.Add(_kv.Value)
        Next
        UpdateList()
    End Sub
End Class