Public Class frmRFEst

    Private Sub frmRFEst_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim i As Integer

        grdReference.Rows.Clear()
        If Campaign.RFEstimation.ReferencePeriods.Count > 0 Then
            grdReference.Rows.Add(Campaign.RFEstimation.ReferencePeriods.Count)
            For i = 1 To Campaign.RFEstimation.ReferencePeriods.Count
                grdReference.Rows(i - 1).Cells(0).Value = Format(Campaign.RFEstimation.ReferencePeriods(i).StartDate, "Short Date")
                grdReference.Rows(i - 1).Cells(1).Value = Campaign.RFEstimation.ReferencePeriods(i).Name
            Next
        End If

    End Sub

    Private Sub frmRFEst_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim TmpDate As Date

        Campaign.RFEstimation.StartDate = Date.FromOADate(Campaign.StartDate)
        Campaign.RFEstimation.EndDate = Date.FromOADate(Campaign.EndDate)
        Campaign.RFEstimation.Target = Campaign.MainTarget

        dtStart.MaxDate = Date.FromOADate(Campaign.Adedge.getDataRangeTo(Connect.eDataType.mSpot) - (Campaign.EndDate - Campaign.StartDate + 1))
        TmpDate = Date.FromOADate(Campaign.Adedge.getDataRangeTo(Connect.eDataType.mSpot) - (Campaign.EndDate - Campaign.StartDate + 1))
        While Weekday(TmpDate, vbMonday) > Weekday(Date.FromOADate(Campaign.StartDate), vbMonday)
            TmpDate = TmpDate.AddDays(-1)
        End While
        dtStart.Value = TmpDate
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim a As Integer

        On Error GoTo ErrHandle

        If Campaign.RFEstimation.ReferencePeriods.Contains(txtName.Text) Then
            MsgBox("That name is already used on another reference period.", vbCritical, "T R I N I T Y")
            Exit Sub
        End If

        If Weekday(dtStart.Value, vbMonday) <> Weekday(Date.FromOADate(Campaign.StartDate), vbMonday) Then
            a = MsgBox("The weekday of the reference date you have chosen is not the same" & Chr(10) & "as the weekday of the first day of your campaign." & Chr(10) & Chr(10) & "Are you sure you want to use that reference date?", vbQuestion + vbYesNoCancel, "TRINITY")
            If a <> vbYes Then Exit Sub
        End If
        'Changes the cursor to a "loading" symbol
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        Campaign.RFEstimation.Target = Campaign.MainTarget
        Campaign.RFEstimation.ReferencePeriods.Add(dtStart.Value, txtName.Text)

        Campaign.RFEstimation.Calculate()

        frmRFEst_Activated(New Object, New EventArgs)

        'Changes the cursor
        Me.Cursor = Windows.Forms.Cursors.Default
        Exit Sub

ErrHandle:
        MsgBox("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & " in cmdAdd_Click.", vbCritical, "Error")
        frmRFEst_Activated(New Object, New EventArgs)
        Resume
    End Sub


    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Dim TmpSpot As Trinity.cBookedSpot

        Campaign.RFEstimation.Spots.Clear()

        For Each TmpSpot In Campaign.BookedSpots
            Campaign.RFEstimation.Spots.Add(TmpSpot.AirDate, TmpSpot.MaM, TmpSpot.Channel.ChannelName, TmpSpot.ID)
        Next
        Campaign.RFEstimation.Calculate()

        Me.DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Campaign.RFEstimation.ReferencePeriods.Remove(grdReference.SelectedRows.Item(0).Index + 1)
        frmRFEst_Activated(New Object, New EventArgs)
    End Sub
End Class