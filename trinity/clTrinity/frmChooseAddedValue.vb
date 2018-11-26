Imports System.Windows.Forms

Public Class frmChooseAddedValue

    Private mvarBT As Trinity.cBookingType
    Private mvarRemark As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmChooseAddedValue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lblRemark.Text = mvarRemark
        cmbAV.Items.Clear()
        cmbAV.Items.Add("None")
        For Each TmpAV As Trinity.cAddedValue In mvarBT.AddedValues
            cmbAV.Items.Add(TmpAV.Name)
        Next
        cmbAV.SelectedIndex = 0

    End Sub

    Public Sub New(ByVal Bookingtype As Trinity.cBookingType, ByVal Remark As String)

        mvarBT = Bookingtype
        mvarRemark = Remark

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub optSetAsOld_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSetAsOld.CheckedChanged
        grpSetAs.Enabled = optSetAsOld.Checked
    End Sub

    Private Sub optCreateNew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCreateNew.CheckedChanged
        grpCreateNew.Enabled = optCreateNew.Checked
    End Sub
End Class
