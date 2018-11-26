Public Class frmAskForDate

    Private m_failed As Boolean = False
    Public Property Failed() As String
        Get
            Return m_failed
        End Get
        Set(ByVal value As String)
            m_failed = value
        End Set
    End Property


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        grdPeriod.Rows.Clear()
        Dim _diff As Integer = dtNormalFrom.Value.ToOADate - Campaign.StartDate
        For Each _week As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
            With grdPeriod.Rows(grdPeriod.Rows.Add)
                .Cells(0).Value = DatePart(DateInterval.WeekOfYear, Date.FromOADate(_week.StartDate + _diff), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                .Cells(1).Value = Date.FromOADate(_week.StartDate + _diff)
                .Cells(2).Value = Date.FromOADate(_week.EndDate + _diff)
            End With
        Next

    End Sub
    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub chkAdvanced_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAdvanced.CheckedChanged
        If chkAdvanced.Checked And grdPeriod.RowCount > 0 Then
            grdPeriod.Height = 10000
            grdPeriod.Height = grdPeriod.RowCount * grdPeriod.Rows(0).Height + grdPeriod.ColumnHeadersHeight + 2
            Me.Height = grdPeriod.Bottom + 80
        Else
            grdPeriod.Height = 0
            Me.Height = 118
        End If
        grdPeriod.Visible = chkAdvanced.Checked
    End Sub

    Private Sub dtNormalFrom_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtNormalFrom.ValueChanged
        grdPeriod.Rows.Clear()
        Dim _diff As Integer = dtNormalFrom.Value.ToOADate - Campaign.StartDate
        For Each _week As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
            With grdPeriod.Rows(grdPeriod.Rows.Add)
                .Cells(0).Value = DatePart(DateInterval.WeekOfYear, Date.FromOADate(_week.StartDate + _diff), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                .Cells(1).Value = Date.FromOADate(_week.StartDate + _diff)
                .Cells(2).Value = Date.FromOADate(_week.EndDate + _diff)
            End With
        Next
    End Sub

    Private Sub frmAskForDate_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    End Sub
End Class