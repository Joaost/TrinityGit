Imports System.Xml
Imports System.Windows.Forms


Public Class frmExportCampaignUnicornNew

    Dim _selectDeselect As Boolean = False

    Dim _bundleMtg As Boolean = False
    Dim _printExportAsCampaign As Boolean = False
    Dim _bundleSbs As Boolean = False
    Dim _bundleFox As Boolean = False
    Dim _bundleCmore As Boolean = False
    Dim _bundleDisney As Boolean = False
    Dim _bundleTnt As Boolean = False
    Dim _bundleMTGSpecial As Boolean = False

    Dim _ownCommission As Boolean = False
    Dim _useCommissionAmount As Single = 0

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click

        If chkBundleMTG.Checked Then
            _bundleMtg = True
        Else
            _bundleMtg = False
        End If
        If chkBundleSBS.Checked Then
            _bundleSbs = True
        Else
            _bundleSbs = False
        End If
        If chkBundleFOX.Checked Then
            _bundleFox = True
        Else
            _bundleFox = False
        End If
        If chkBundleCMORE.Checked Then
            _bundleCmore = True
        Else
            _bundleCmore = False
        End If
        If chkBundleDisney.Checked Then
            _bundleDisney = True
        Else
            _bundleDisney = False
        End If
        If chkBundleTNT.Checked Then
            _bundleTnt = True
        Else
            _bundleTnt = False
        End If
        If chkBundleTNT.Checked Then
            _bundleTnt = True
        Else
            _bundleTnt = False
        End If
        If chkOwnCommission.Checked Then
            _ownCommission = True
            _useCommissionAmount = 6
        Else
            _ownCommission = False
        End If
        If chkPrintExportAsCampaign.Checked Then
            _printExportAsCampaign = True
        Else
            _printExportAsCampaign = False
        End If
        'Dim export As New cExportUnicornFile(Campaign)
        Dim export As New CExportUnicornFileNew(Campaign)

        export.printUnicornFile(_bundleMtg, _bundleSbs, _bundleFox, _bundleCmore, _bundleDisney, _bundleTnt, _ownCommission, _useCommissionAmount, _printExportAsCampaign)

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub


    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

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
                        chk.Enabled = False
                        chk.Checked = False
                    End If
                Catch ex As Exception

                End Try
            Next
        Else
            For Each tmpChkBox As Control In Me.Controls
                Try
                    Dim chk As CheckBox = TryCast(tmpChkBox, CheckBox)
                    If chk.Name <> "chkOwnCommission" And chk.Name <> "chkPrintExportAsCampaign" Then
                        chk.Enabled = True
                        chk.Checked = True
                    End If
                Catch ex As Exception

                End Try
            Next

        End If
    End Sub
End Class