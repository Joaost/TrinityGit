Imports System.Xml
Imports System.Windows.Forms


Public Class frmExportCampaignUnicornNorway

    Dim SelectDeselect As Boolean = False

    Dim bundleSBS As Boolean = False
    Dim bundleTV2 As Boolean = False
    Dim bundleNatGeo As Boolean = False
    Dim bundleMTG As Boolean = False
    Dim printExportAsCampaign As Boolean = False
    Dim bundleDiscovery As Boolean = False
    Dim ownCommission As Boolean = False
    Dim useCommissionAmount As Single = 0
    Dim _campaignType As String = ""
    Dim _campaignType2 As String = ""

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click

        If chkBundleTVN.Checked Then
            bundleSBS = True
        Else
            bundleSBS = False
        End If
        If chkBundleTV2.Checked Then
            bundleTV2 = True
        Else
            bundleTV2 = False
        End If
        If chkBundleNatGeo.Checked Then
            bundleNatGeo = True
        Else
            bundleNatGeo = False
        End If
        If chkBundleViasat.Checked Then
            bundleMTG = True
        Else
            bundleMTG = False
        End If

        If chkOwnCommission.Checked Then
            ownCommission = True
            useCommissionAmount = 6
        Else
            ownCommission = False
        End If
        If chkPrintExportAsCampaign.Checked Then
            printExportAsCampaign = True
        Else
            printExportAsCampaign = False
        End If

        _campaignType = cmbCampaignType.SelectedItem
        _campaignType2 = cmbCampaignType2.SelectedItem

        Dim export As New CExportUnicornFileNewNorway(Campaign)

        export.printUnicornFile(bundleTV2, bundleMTG, bundleSBS, bundleNatGeo, False, False, printExportAsCampaign, _campaignType, _campaignType2)

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        cmbCampaignType.SelectedIndex = 0
        cmbCampaignType2.SelectedIndex = 0

    End Sub

    Private Sub lblSelectDeselect_Click(sender As Object, e As EventArgs) Handles lblSelectDeselect.Click
        For Each tmpChkBox As Control In Me.Controls
            Try
                Dim chk As CheckBox = TryCast(tmpChkBox, CheckBox)
                If chk.Name <> "chkOwnCommission" And chk.Name <> "chkPrintExportAsCampaign" Then
                    If chk.Checked Then
                        chk.Checked = False
                    Else
                        chk.Checked = True
                    End If
                End If
            Catch ex As Exception

            End Try
        Next
    End Sub

    Private Sub chkOwnCommission_CheckedChanged(sender As Object, e As EventArgs) Handles chkOwnCommission.CheckedChanged
    End Sub

    Private Sub chkPrintExportAsCampaign_CheckedChanged(sender As Object, e As EventArgs) Handles chkPrintExportAsCampaign.CheckedChanged
        If chkPrintExportAsCampaign.Checked Then
            For Each tmpChkBox As Control In Me.Controls
                Try
                    Dim chk As CheckBox = TryCast(tmpChkBox, CheckBox)
                    If chk.Name <> "chkOwnCommission" And chk.Name <> "chkPrintExportAsCampaign" Then
                        If chk.Enabled Then
                            chk.Enabled = False
                            chk.Checked = False
                        End If
                    End If
                Catch ex As Exception

                End Try
            Next
        Else
            For Each tmpChkBox As Control In Me.Controls
                Try
                    Dim chk As CheckBox = TryCast(tmpChkBox, CheckBox)
                    If chk.Name <> "chkOwnCommission" And chk.Name <> "chkPrintExportAsCampaign" Then
                        If chk.Enabled = False Then
                            chk.Enabled = True
                            chk.Checked = True
                        End If
                    End If
                Catch ex As Exception

                End Try
            Next

        End If
    End Sub
End Class