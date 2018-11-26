Public Class frmImport

    Public Property EvaluateSpecifics() As Boolean
        Get
            Return chkEvaluate.Checked
        End Get
        Set(ByVal value As Boolean)
            chkEvaluate.Checked = value
        End Set
    End Property

    Private Sub frmImport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
    End Sub

    Private Sub frmImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Aspect As Single
        Dim TmpBT As Trinity.cBookingType

        If Me.Tag = "TV4Lokal" Then
            chkReplace.Enabled = False
            cmbBookingType.Items.Add("Sumo")
            cmbBookingType.Items.Add("Flex")
            cmbBookingType.SelectedIndex = 0
            grpBudget.Enabled = False


            If Campaign.Channels("TV4") IsNot Nothing Then
                picLogo.Image = Campaign.Channels("TV4").GetImage
            End If

            Aspect = picLogo.Width / picLogo.Height
            picLogo.SizeMode = Windows.Forms.PictureBoxSizeMode.StretchImage
            picLogo.Height = 32
            picLogo.Width = 32 * Aspect
            Exit Sub
        End If


        picLogo.SizeMode = Windows.Forms.PictureBoxSizeMode.AutoSize
        picLogo.Image = Campaign.Channels(Me.Tag).GetImage()

        Aspect = picLogo.Width / picLogo.Height
        picLogo.SizeMode = Windows.Forms.PictureBoxSizeMode.StretchImage
        picLogo.Height = 32
        picLogo.Width = 32 * Aspect

        If cmbBookingType.Items.Count = 0 Then
            MsgBox("The channel " & Me.Tag & " is not available in this campaign.", MsgBoxStyle.Information, "Bad channel")
            Me.Close()
            Exit Sub
        End If

        Try
            If Label6.Tag = "RBS" Then
                cmbBookingType.SelectedItem = "RBS"
            ElseIf Label6.Tag = "Spec" Then
                cmbBookingType.SelectedItem = "Specifics"
            ElseIf Label6.Tag = "Last Minute" Then
                cmbBookingType.SelectedItem = "Last Minute"
            ElseIf Label6.Tag = "First Minute" Then
                cmbBookingType.SelectedItem = "First Minute"
            Else
                cmbBookingType.SelectedItem = "Please choose booking type"
            End If
        Catch
            cmbBookingType.SelectedIndex = 0
        End Try

        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmbBookingType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBookingType.SelectedIndexChanged
        If Not Me.Tag = "TV4Lokal" Then
            'If Campaign.Channels(Me.Tag).BookingTypes(cmbBookingType.Text).IsSpecific Then
            '    chkEvaluate.Enabled = True
            'Else
            '    chkEvaluate.Enabled = False
            'End If
            lblCurrentBudget.Text = Format(Campaign.Channels(Me.Tag).BookingTypes(cmbBookingType.Text).ConfirmedNetBudget, "N0")
        End If
    End Sub

    Public Sub New(Channel As Trinity.cChannel)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.Tag = Channel.ChannelName
        ' Add any initialization after the InitializeComponent() call.
        cmbBookingType.Items.Clear()
        For Each TmpBT As Trinity.cBookingType In Campaign.Channels(Me.Tag).BookingTypes
            If TmpBT.BookIt Then
                cmbBookingType.Items.Add(TmpBT.Name)
            End If
        Next
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        If cmbBookingType.SelectedItem Is Nothing Then
            Windows.Forms.MessageBox.Show("Pick the booking type to send the spots to first.")
            Exit Sub
        End If

        'System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        'System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub lblPath_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblPath.Click
        Dim Excel As New Excel.Application
        Excel.Workbooks.Open(IO.Path.Combine(lblPath.Tag, lblPath.Text))
        Excel.Visible = True
        Excel = Nothing
    End Sub
End Class