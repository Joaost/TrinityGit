Public Class frmExportCampaignToDatoramaExport
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim export As new cExportDatoramaFile(Campaign)
        export.exportDatoramaFile()
        
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class