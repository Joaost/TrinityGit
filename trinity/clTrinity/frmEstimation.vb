Public Class frmEstimation

    Dim l As List(Of String)
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Saved = False

        Dim TmpPeriod As Trinity.cPeriod
        Dim Tmpstr As String
        Dim TmpBreak As Trinity.cBreak
        Dim TmpChannel As Trinity.cChannel
        Dim b As Long

        If rdb4W.Checked Then
            Tmpstr = "-4fw"
        ElseIf rdbLastYear.Checked Then
            Dim tmpDateS As Date = Date.FromOADate(Campaign.StartDate).AddYears(-1)
            Dim tmpDateE As Date = Date.FromOADate(Campaign.EndDate).AddYears(-1)
            Tmpstr = Format(tmpDateS, "ddMMyy") & "-" & Format(tmpDateE, "ddMMyy")
        ElseIf rdbCustom.Checked Then
            Tmpstr = txtCustomPeriod.Text
        Else
            MsgBox("You need to select a estimation period", MsgBoxStyle.Critical, "ERROR")
            Exit Sub
        End If

        System.Windows.Forms.Application.DoEvents()
        TmpPeriod = New Trinity.cPeriod
        TmpPeriod.Period = Tmpstr
        TmpPeriod.Adedge.setArea(Campaign.Area)
        TmpPeriod.Adedge.setChannelsArea(Campaign.ChannelString, Campaign.Area)
        TmpPeriod.Adedge.setPeriod(TmpPeriod.Period)
        Trinity.Helper.AddTargetsToAdedge(TmpPeriod.Adedge)
        TmpPeriod.BreakCount = TmpPeriod.Adedge.Run
        If TmpPeriod.BreakCount < 1 Then
            MsgBox("Invalid custom period", MsgBoxStyle.Critical, "ERROR")
            Exit Sub
        End If
        TmpPeriod.Breaks = New ArrayList
        For b = 0 To TmpPeriod.BreakCount - 1
            If Campaign.Area <> "SE" OrElse TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBreaktitle, b) = "Break" Then
                TmpBreak = New Trinity.cBreak(Campaign)
                TmpBreak.AirDate = Date.FromOADate(TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aDate, b))
                TmpBreak.BreakIdx = b
                TmpBreak.BreakList = TmpPeriod.Period
                TmpBreak.ID = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBreakSequenceID, b)
                TmpBreak.MaM = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aFromTime, b) \ 60
                TmpBreak.ProgAfter = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBreakProgAfter, b)
                TmpBreak.ProgBefore = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBreakProgBefore, b)
                For Each TmpChannel In Campaign.Channels
                    If InStr(UCase(TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aChannel, b) & ","), UCase(TmpChannel.AdEdgeNames & ",")) > 0 Then
                        TmpBreak.Channel = TmpChannel
                        Exit For
                    End If
                Next
                TmpPeriod.Breaks.Add(TmpBreak) ', Format(TmpBreak.AirDate, "yyMMdd") & TmpBreak.ID)
            End If
        Next

        Dim RowCount As Integer
        Dim ID As String
        Dim TotRatingMain As Single
        Dim TotRatingSecond As Single
        Dim TotRatingAll As Single
        Dim TotRatingBT As Single
        Dim Count As Integer
        Dim tmpSpot As Trinity.cPlannedSpot

        For j As Integer = 0 To l.Count - 1
            RowCount += 1
            '        If r / 3 = r \ 3 Then
            lblStatus.Text = "Estimating... " & Format(j / l.Count, "P0")
            System.Windows.Forms.Application.DoEvents()
            ID = l.Item(j)
            If ID = "" Then
                Exit Sub
            End If
            tmpSpot = Campaign.PlannedSpots(ID)

            tmpSpot.BreakList = Trinity.Helper.Estimate(Date.FromOADate(tmpSpot.AirDate), tmpSpot.MaM, tmpSpot.ProgAfter, tmpSpot.Channel.ChannelName, TmpPeriod.Breaks)

            TotRatingMain = 0
            TotRatingSecond = 0
            TotRatingAll = 0
            TotRatingBT = 0
            Count = 0
            'If Not TmpEI.BreakList Is Nothing Then
            If Not tmpSpot.BreakList Is Nothing Then
                'For b = 1 To TmpEI.BreakList.Count
                For b = 1 To tmpSpot.BreakList.Count
                    TotRatingMain += TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, tmpSpot.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, Campaign.MainTarget))
                    TotRatingSecond += TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, tmpSpot.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, Campaign.SecondaryTarget))
                    TotRatingBT += TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, tmpSpot.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, tmpSpot.Bookingtype.BuyingTarget.Target))
                    TotRatingAll += TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, tmpSpot.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, Campaign.AllAdults))

                    Count += 1
                Next
                If Count > 0 Then
                    tmpSpot.MyRating = TotRatingMain / Count
                    tmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget) = TotRatingSecond / Count
                    tmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = TotRatingBT / Count
                    tmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults) = TotRatingAll / Count
                End If
            End If
        Next
        lblStatus.Text = "Estimating...100%"
        System.Windows.Forms.Application.DoEvents()

        lblStatus.Text = ""
        Me.Cursor = Windows.Forms.Cursors.Default


        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Public Sub New(ByVal list As List(Of String))

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        l = list
    End Sub

    Private Sub rdbCustom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCustom.CheckedChanged
        If rdbCustom.Checked = True Then
            txtCustomPeriod.Enabled = True
        Else
            txtCustomPeriod.Enabled = False
        End If
    End Sub
End Class