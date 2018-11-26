Public Class frmDates

    'Empty constructor to use when the form is supposed to show all components
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

    End Sub

    Public Sub New(ByVal displayTo As Boolean, ByVal headerText As String, Optional RestrictDatesToData As Boolean = True)

        ' This call is required by the designer.
        InitializeComponent()

        'Set the header text
        Me.lblHeader.Text = headerText

        ' Hide the to label and and the datepicker associated with it
        Me.Label2.Visible = False
        Me.dateTo.Visible = False

        ' Also make the form smaller but leave a small offset by 20 pixels
        Me.Width -= Me.dateTo.Width - 20
        Me.Height += Me.lblHeader.Height + Me.lblHeader.Top * 2

        Me.lblHeader.MaximumSize = New Size(Me.Width - Me.lblHeader.Left * 2, Me.lblHeader.MaximumSize.Height)

        If RestrictDatesToData Then
            dateFrom.MaxDate = Date.FromOADate(Campaign.Adedge.getDataRangeTo(Connect.eDataType.mSpot))
            dateTo.MaxDate = Date.FromOADate(Campaign.Adedge.getDataRangeTo(Connect.eDataType.mSpot))
        End If
    End Sub

    Private Sub frmDates_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            dateFrom.Value = Date.Now
            dateTo.Value = Date.Now
        Catch
            dateFrom.Value = Date.FromOADate(Campaign.Adedge.getDataRangeTo(Connect.eDataType.mSpot))
            dateTo.Value = Date.FromOADate(Campaign.Adedge.getDataRangeTo(Connect.eDataType.mSpot))
        End Try
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub dateFrom_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dateFrom.ValueChanged
        dateTo.MinDate = dateFrom.Value
    End Sub
End Class