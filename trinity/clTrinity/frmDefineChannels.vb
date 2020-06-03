Public Class frmDefineChannels
    Dim Aspect As Single

    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hwndLock As Long) As Long
    Dim validChannels As New Connect.Brands
    Dim inList As New System.Windows.Forms.AutoCompleteStringCollection

    Private Sub frmDefineChannels_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        cmbChannel.Items.Clear()
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            cmbChannel.Items.Add(TmpChan)
        Next
        If TrinitySettings.AdtooxEnabled Then
            lblAdTooxId.Visible = True
            txtAdtooxID.Visible = True
            btnGetAdtooxMediaID.Visible = True
        End If
        cmbChannel.DisplayMember = "ChannelName"
        cmbChannel.SelectedIndex = 0
        'cmdSaveToFile.Enabled = True
        colEFactor.Visible = (Campaign.Area = "DK")

        'validChannels.setArea("Norway PPM")
        'validChannels.setArea("SR")
        validChannels.setArea(TrinitySettings.DefaultArea)

        validChannels.setPeriod("-1d")
        validChannels.setTargetMnemonic("3+", False)
        validChannels.setChannelsAll()

        Try
            Dim spotcount As Integer = validChannels.Run(False, False, 10)
            For i As Integer = 0 To spotcount - 1
                Dim tmpChan As String = validChannels.getAttrib(Connect.eAttribs.aChannel, i)
                If Not inList.Contains(tmpChan) Then inList.Add(tmpChan)
            Next
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("A problem with the Advantedge connection occured.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try

        txtAdedge.AutoCompleteMode = Windows.Forms.AutoCompleteMode.Suggest
        txtAdedge.AutoCompleteSource = Windows.Forms.AutoCompleteSource.CustomSource
        txtAdedge.AutoCompleteCustomSource = inList


        If (Not TrinitySettings.Developer) AndAlso (Not TrinitySettings.ConnectionStringCommon = "") Then
            btnGetAdtooxMediaID.Enabled = False
            cmdAddChannel.Enabled = False
            cmdDeleteChannel.Enabled = False
            cmdSaveToFile.Enabled = False
            txtAdtooxID.Enabled = False
            txtCommission.Enabled = False
            txtConnected.Enabled = False
            txtDelivery.Enabled = False
            txtListNr.Enabled = False
            txtMarathon.Enabled = False
            txtMatrix.Enabled = False
            txtName.Enabled = False
            txtShortName.Enabled = False
            txtVHS.Enabled = False
            chkBid.Enabled = False
            chkUseBillB.Enabled = False
            chkUseBreakB.Enabled = False

            cmbUniverse.Enabled = False
            txtAdedge.Enabled = False
        Else
            btnGetAdtooxMediaID.Enabled = True
            cmdAddChannel.Enabled = True
            cmdDeleteChannel.Enabled = True
            cmdSaveToFile.Enabled = True
            txtAdtooxID.Enabled = True
            txtCommission.Enabled = True
            txtConnected.Enabled = True
            txtDelivery.Enabled = True
            txtListNr.Enabled = True
            txtMarathon.Enabled = True
            txtMatrix.Enabled = True
            txtName.Enabled = True
            txtShortName.Enabled = True
            txtVHS.Enabled = True
            chkBid.Enabled = True
            chkUseBillB.Enabled = True
            chkUseBreakB.Enabled = True

            cmbUniverse.Enabled = True
            txtAdedge.Enabled = True
        End If

    End Sub


    Private Sub cmbChannel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbChannel.SelectedIndexChanged

        txtAdedge.BackColor = Color.White

        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem

        txtName.Text = TmpChan.ChannelName
        txtShortName.Text = TmpChan.Shortname
        txtListNr.Text = TmpChan.ListNumber
        txtAdedge.Text = TmpChan.AdEdgeNames
        txtCommission.Text = TmpChan.AgencyCommission * 100
        txtDelivery.Text = TmpChan.DeliveryAddress
        txtVHS.Text = TmpChan.VHSAddress
        txtConnected.Text = TmpChan.ConnectedChannel
        txtMatrix.Text = TmpChan.MatrixName
        txtAdtooxID.Text = TmpChan.AdTooxChannelID
        txtChannelGroup.Text = TmpChan.ChannelGroup

        If TrinitySettings.MarathonEnabled Then
            txtMarathon.Visible = True
            lblMarathon.Visible = True
            txtMarathon.Text = TmpChan.MarathonName
        Else
            txtMarathon.Visible = False
            lblMarathon.Visible = False
        End If
        grdBT.Rows.Clear()
        Dim row As System.Windows.Forms.DataGridViewRow

        For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
            row = New System.Windows.Forms.DataGridViewRow()
            row.Tag = TmpBT
            grdBT.Rows.Add(row)
        Next
        chkUseBillB.Checked = TmpChan.UseBillboards
        chkUseBreakB.Checked = TmpChan.UseBreakBumpers
        chkBid.Checked = TmpChan.UseBid

        cmbUniverse.Items.Clear()
        cmbUniverse.Items.Add("<National>")
        For i As Integer = 0 To Campaign.Adedge.lookupNoUniverses - 1
            cmbUniverse.Items.Add(Campaign.Adedge.lookupUniverses(i))
            If Campaign.Adedge.lookupUniverses(i) = TmpChan.BuyingUniverse Then
                cmbUniverse.SelectedIndex = i + 1
            End If
        Next
        If cmbUniverse.SelectedIndex = -1 Then cmbUniverse.SelectedIndex = 0

        'sets the logo of the channel
        picLogo.SizeMode = Windows.Forms.PictureBoxSizeMode.AutoSize
        picLogo.Image = Campaign.Channels(TmpChan.ChannelName).GetImage

        Aspect = picLogo.Width / picLogo.Height
        picLogo.SizeMode = Windows.Forms.PictureBoxSizeMode.StretchImage
        picLogo.Height = 32
        picLogo.Width = 32 * Aspect
    End Sub

    Private Sub grdBT_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdBT.CellEndEdit
        Saved = False
    End Sub


    Private Sub grdBT_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdBT.CellValueNeeded
        Dim TmpBT As Trinity.cBookingType = grdBT.Rows(e.RowIndex).Tag
        Select Case e.ColumnIndex
            Case 0
                e.Value = TmpBT.Name
            Case 1
                e.Value = TmpBT.Shortname
            Case 2
                e.Value = TmpBT.EnhancementFactor
            Case 3
                e.Value = TmpBT.IsRBS
            Case 4
                e.Value = TmpBT.IsSpecific
            Case 5
                e.Value = TmpBT.IsPremium
            Case 6
                e.Value = TmpBT.IsCompensation
            Case 7
                e.Value = TmpBT.IsSponsorship
            Case 8
                e.Value = TmpBT.PrintDayparts
            Case 9
                e.Value = TmpBT.PrintBookingCode
        End Select
    End Sub

    Private Sub grdBT_CellValuePushed(ByVal sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdBT.CellValuePushed
        Dim TmpBT As Trinity.cBookingType = grdBT.Rows(e.RowIndex).Tag
        If TmpBT.IsUserEditable Then
            Select Case e.ColumnIndex
                Case 0
                    Try
                        TmpBT.Name = e.Value
                    Catch ex As Exception
                        System.Windows.Forms.MessageBox.Show("You can not use an already used name on a bookingtype.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                        grdBT.Invalidate()
                        Exit Sub
                    End Try
                Case 1
                    If LongName.ContainsKey(e.Value) Then
                        System.Windows.Forms.MessageBox.Show("You can not use an already used shortname on a bookingtype.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                        grdBT.Invalidate()
                        Exit Sub
                    End If
                    TmpBT.Shortname = e.Value
                Case 2
                    TmpBT.EnhancementFactor = e.Value
                Case 3
                    TmpBT.IsRBS = e.Value
                    TmpBT.IsSpecific = Not e.Value
                Case 4
                    TmpBT.IsSpecific = e.Value
                    TmpBT.IsRBS = Not e.Value
                Case 5
                    TmpBT.IsPremium = e.Value
                Case 6
                    TmpBT.IsCompensation = e.Value
                Case 7
                    TmpBT.IsSponsorship = e.Value
                Case 8
                    TmpBT.PrintDayparts = e.Value
                Case 9
                    TmpBT.PrintBookingCode = e.Value
            End Select
        Else
            Windows.Forms.MessageBox.Show("Cannot change information about a standard booking type.")
        End If

        ' Update the code
        grdBT.Invalidate()

    End Sub

    Private Sub txtName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.LostFocus
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        Dim Chan As String = TmpChan.ChannelName

        Try
            TmpChan.ChannelName = sender.text
        Catch ex As Exception
            If ex.TargetSite.Name = "Raise" Then
                System.Windows.Forms.MessageBox.Show("Two channels can not share the same name.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                txtName.Text = Chan
                TmpChan.ChannelName = Chan
            Else
                Throw ex
            End If
        End Try
        cmbChannel.BeginUpdate()
        cmbChannel.Items.Clear()
        For Each TmpChan In Campaign.Channels
            cmbChannel.Items.Add(TmpChan)
        Next
        cmbChannel.DisplayMember = "ChannelName"
        cmbChannel.SelectedItem = Campaign.Channels(txtName.Text)
        cmbChannel.EndUpdate()
    End Sub

    Private Sub txtShortName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtShortName.TextChanged
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        TmpChan.Shortname = Trim(sender.text)
    End Sub

    Private Sub txtListNr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtListNr.TextChanged
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        TmpChan.ListNumber = Trim(sender.text)
    End Sub

    Private Sub txtAdedge_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdedge.Leave
        If Not inList.Contains(txtAdedge.Text) Then
            txtAdedge.BackColor = Color.Red
        Else
            txtAdedge.BackColor = Color.White
        End If
    End Sub

    Private Sub txtAdedge_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtAdedge.MouseClick

    End Sub

    Private Sub txtadedge_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdedge.TextChanged


        Dim tmpChan As Trinity.cChannel


        Saved = False
        tmpChan = cmbChannel.SelectedItem
        tmpChan.AdEdgeNames = Trim(sender.text)
        'If validChannelsList.Count > 0 Then
        '    If Not validChannelsList.Contains(tmpChan.AdEdgeNames) Then
        '        txtAdedge.BackColor = Color.Red
        '    Else
        '        txtAdedge.BackColor = Color.White
        '    End If
        'End If
    End Sub

    Private Sub txtCommission_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCommission.TextChanged
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        TmpChan.AgencyCommission = Val(sender.text.ToString.Replace(",", ".")) / 100
    End Sub

    Private Sub txtDelivery_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDelivery.TextChanged
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        TmpChan.DeliveryAddress = sender.text
    End Sub

    Private Sub txtVHS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVHS.TextChanged
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        TmpChan.VHSAddress = sender.text
    End Sub

    Private Sub cmdAddChannel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddChannel.Click
        Saved = False
        Dim str As String
        Dim frm As New frmAddChannel()
        frm.ShowDialog()

        If (TrinitySettings.ConnectionStringCommon <> "") Then
            If frm.DialogResult = Windows.Forms.DialogResult.OK Then
                Dim NewIdx As Integer = cmbChannel.Items.Add(Campaign.Channels.Add(frm.txtName.Text, frm.cmbFile.SelectedItem.Value))
                cmbChannel.SelectedIndex = NewIdx
            End If
        Else
            If frm.DialogResult = Windows.Forms.DialogResult.OK Then
                If frm.cmbFile.SelectedItem.value = "Default" Then
                    str = ""
                Else
                    str = frm.cmbFile.SelectedItem
                End If
                Dim NewIdx As Integer = cmbChannel.Items.Add(Campaign.Channels.Add(frm.txtName.Text, str))
                cmbChannel.SelectedIndex = NewIdx
            End If
        End If
    End Sub

    Private Sub cmdDeleteChannel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteChannel.Click
        Saved = False
        If cmbChannel.SelectedItem Is Nothing Then Exit Sub
        If cmbChannel.SelectedItem.IsUserEditable Then

            ' Make sure them current campaign has no spots in any booking types this channel has
            Dim _foundSpots As Boolean = False

            For Each bookingType As Trinity.cBookingType In Campaign.Channels(cmbChannel.SelectedItem.channelname).BookingTypes

                If (bookingType.SpotsExist()) Then
                    _foundSpots = True
                End If
            Next

            ' Make sure there are no spots attached to this channel
            If (_foundSpots) Then
                If System.Windows.Forms.MessageBox.Show("There are spots attached to this channel. Removing it will remove any spots too!" & vbNewLine & "Do you want to continue?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.No Then

                    ' Do nothing
                    Exit Sub
                End If
            End If

            ' Remove all those spots
            For Each bookingType As Trinity.cBookingType In Campaign.Channels(cmbChannel.SelectedItem.channelname).BookingTypes
                bookingType.RemoveAllSpots()
            Next

            Campaign.Channels.Remove(cmbChannel.SelectedItem.channelname)
            cmbChannel.Items.RemoveAt(cmbChannel.SelectedIndex)

            If cmbChannel.Items.Count > 0 Then
                cmbChannel.SelectedIndex = 0
            End If
        Else
            Windows.Forms.MessageBox.Show("Cannot delete a standard channel.")
        End If
    End Sub

    Private Sub cmdAddBT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddBT.Click
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        Dim BTName As String = "New Bookingtype"
        Dim i As Integer = 2

        Dim row As Integer = grdBT.Rows.Add()

        While TmpChan.BookingTypes.Contains(BTName)
            BTName = "New Bookingtype (" & i & ")"
            i += 1
        End While

        Dim TmpBT As Trinity.cBookingType = TmpChan.BookingTypes.Add(BTName)
        grdBT.Rows(grdBT.Rows.Count - 1).Tag = TmpBT

        'set the default film index
        For i = 1 To 500
            TmpBT.FilmIndex(i) = Campaign.Channels.DefaultFilmIndex(i)
        Next

        If Campaign.Channels(1).BookingTypes(1).Weeks.Count > 0 Then
            For Each week As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                TmpChan.BookingTypes(BTName).Weeks.Add(week.Name)
                TmpChan.BookingTypes(BTName).Weeks(week.Name).StartDate = week.StartDate
                TmpChan.BookingTypes(BTName).Weeks(week.Name).EndDate = week.EndDate
                For Each f As Trinity.cFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
                    TmpChan.BookingTypes(BTName).Weeks(week.Name).Films.Add(f.Name)
                    TmpChan.BookingTypes(BTName).Weeks(week.Name).Films(f.Name).Filmcode = f.Filmcode
                    TmpChan.BookingTypes(BTName).Weeks(week.Name).Films(f.Name).FilmLength = f.FilmLength
                    TmpChan.BookingTypes(BTName).Weeks(week.Name).Films(f.Name).Index = f.Index
                Next
            Next
        End If
        TmpBT.Dayparts = TrinitySettings.DefaultDayparts(TmpBT)

        ' Make the deafult selection RBS
        Dim tmpChk As Windows.Forms.DataGridViewCheckBoxCell = DirectCast(grdBT.Rows(row).Cells("colIsRBS"), Windows.Forms.DataGridViewCheckBoxCell)

        tmpChk.Value = True

    End Sub

    Private Sub cmdDeleteBT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteBT.Click
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        If grdBT.SelectedRows.Item(0).Tag.IsUserEditable Then

            ' Check if there any spots attached to the booking type
            Dim TmpBt As Trinity.cBookingType = TmpChan.BookingTypes(grdBT.SelectedRows.Item(0).Tag.name)

            If TmpBt.SpotsExist() Then

                If Windows.Forms.MessageBox.Show("There are spots attached to this booking type. Removing it will also removing the spots!" & vbNewLine & "Do you want to continue?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.No Then

                    ' Do nothing
                    Exit Sub

                End If
            End If

            ' Remove the spots
            TmpBt.RemoveAllSpots()

            TmpChan.BookingTypes.Remove(grdBT.SelectedRows.Item(0).Tag.name)
            grdBT.Rows.Remove(grdBT.SelectedRows.Item(0))
        Else
            Windows.Forms.MessageBox.Show("Cannot remove a standard booking type.")
        End If
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If (TmpBT.IsSpecific OrElse TmpBT.IsPremium) AndAlso TmpBT.BookIt Then
                    frmMain.cmdBooking.Enabled = True
                End If
            Next
        Next
        If _adTooxIDChanged Then
            Campaign.AdToox = New Trinity.cAdtoox
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub txtConnected_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtConnected.MouseDown
        ' If e.Button = Windows.Forms.MouseButtons.Right Then
        ' Avoid the 'disabled' gray text by locking updates
        LockWindowUpdate(DirectCast(sender, Windows.Forms.TextBox).Handle)
        DirectCast(sender, Windows.Forms.TextBox).Enabled = False
        My.Application.DoEvents()
        Dim channelsMenu As New System.Windows.Forms.ToolStripDropDownMenu
        With channelsMenu.Items.Add("None")
            AddHandler .Click, AddressOf ConnectedChannel
            .Tag = ""
        End With
        channelsMenu.Items.Add("-")
        For Each tmpChan As Trinity.cChannel In Campaign.Channels
            Dim item As New System.Windows.Forms.ToolStripMenuItem
            If tmpChan.ChannelName <> DirectCast(cmbChannel.SelectedItem, Trinity.cChannel).ChannelName Then
                With channelsMenu.Items.Add(tmpChan.ChannelName)
                    AddHandler .Click, AddressOf ConnectedChannel
                    .Tag = tmpChan.ChannelName
                End With
            End If
        Next
        channelsMenu.Show(MousePosition)
        DirectCast(sender, Windows.Forms.TextBox).Enabled = True
        LockWindowUpdate(0&)
        '  End If

    End Sub

    Private Sub txtConnected_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtConnected.MouseUp
    End Sub

    Private Sub txtConnected_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtConnected.TextChanged
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        TmpChan.ConnectedChannel = Trim(txtConnected.Text)
    End Sub

    Public Sub ConnectedChannel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        If sender.tag = "" Then
            txtConnected.Text = ""
        Else
            tmpChan.ConnectedChannel = sender.tag
            txtConnected.Text = tmpChan.ConnectedChannel
        End If
    End Sub

    Private Sub txtMarathon_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMarathon.TextChanged
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        TmpChan.MarathonName = Trim(txtMarathon.Text)
    End Sub

    Private Sub cmdSaveToFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveToFile.Click
        Dim frm As New frmAddChannel()
        frm.Tag = "EDIT"
        frm.ShowDialog()

        If frm.DialogResult = Windows.Forms.DialogResult.Cancel Then Exit Sub

        If InputBox("This function is protected with a password." & vbCrLf & "Please enter the password required:", "T R I N I T Y") <> "orange2010" Then
            System.Windows.Forms.MessageBox.Show("Wrong password!", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Exit Sub
        End If
        If Windows.Forms.MessageBox.Show("This will overwrite the channel definitions for all users." & vbCrLf & vbCrLf & "Are you sure you want to proceed?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        Campaign.Channels.saveChannels(frm.cmbFile.SelectedItem.Value)

        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmbUniverse_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUniverse.SelectedIndexChanged
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        If cmbUniverse.SelectedIndex = 0 Then
            TmpChan.BuyingUniverse = ""
        Else
            TmpChan.BuyingUniverse = cmbUniverse.Text
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        Saved = False
    End Sub

    Dim _daypartsChangedShowed As Boolean = False
    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        Saved = False
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        Campaign.ReloadDeletedChannels()

        _daypartsChangedShowed = False
        AddHandler Campaign.DaypartDefinitionsChanged, Sub()
                                                           If Not _daypartsChangedShowed Then
                                                               Windows.Forms.MessageBox.Show("Daypart definitions has been changed on one or more channels." & vbNewLine & vbNewLine & "1. Update all pricelists in Settings->Edit pricelists" & vbNewLine & "2. Review your dayparts splits in the Setup-window", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                                               _daypartsChangedShowed = True
                                                           End If
                                                       End Sub

        If Windows.Forms.MessageBox.Show("Keep the current agency commissions on channels?", "Keep commissions?", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Campaign.Channels.updateAllChannels(True)
        Else
            Campaign.Channels.updateAllChannels(False)
        End If

        cmbChannel.Items.Clear()
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            cmbChannel.Items.Add(TmpChan)
        Next
        cmbChannel.DisplayMember = "ChannelName"
        cmbChannel.SelectedIndex = 0

        cmbChannel_SelectedIndexChanged(New Object, New EventArgs)
        Me.Cursor = Windows.Forms.Cursors.Default

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub txtMatrix_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMatrix.TextChanged
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        TmpChan.MatrixName = Trim(txtMatrix.Text)
    End Sub

    Private Sub grdBT_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdBT.CellContentClick
        grdBT.CommitEdit(Windows.Forms.DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub chkUseBillB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseBillB.CheckedChanged
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        TmpChan.UseBillboards = chkUseBillB.Checked
    End Sub

    Private Sub chkUseBreakB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseBreakB.CheckedChanged
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        TmpChan.UseBreakBumpers = chkUseBreakB.Checked
    End Sub

    Private _adTooxIDChanged As Boolean = False

    Private Sub ChannelClick(ByVal sender As Object, ByVal e As System.EventArgs)
        txtAdtooxID.Text = CType(sender.tag, Trinity.cAdtooxMedia).ID
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        TmpChan.AdTooxChannelID = Trim(txtAdtooxID.Text)
        _adTooxIDChanged = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAdtooxMediaID.Click
        Dim channelMenu As New Windows.Forms.ToolStripDropDownMenu
        For Each Channel As Trinity.cAdtooxMedia In Campaign.AdToox.GetMediaForCountry(Campaign.Area)
            Dim channelItem As New Windows.Forms.ToolStripMenuItem
            channelItem.Tag = Channel
            channelItem.Text = Channel.Name & " (" & Channel.ID & ")"
            AddHandler channelItem.Click, AddressOf ChannelClick
            channelMenu.Items.Add(channelItem)
        Next
        channelMenu.Show(MousePosition.X, MousePosition.Y)
    End Sub

    Private Sub chkBid_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBid.CheckedChanged
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        TmpChan.UseBid = chkBid.Checked
    End Sub

    Private Sub grdBT_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdBT.SelectionChanged
        If grdBT.SelectedRows.Count = 0 Then Exit Sub
        Dim TmpBT As Trinity.cBookingType = grdBT.SelectedRows(0).Tag
        If TmpBT Is Nothing Then Exit Sub
        If TmpBT.writeProtected AndAlso Not TrinitySettings.Developer Then
            cmdDeleteBT.Enabled = False
            grdBT.SelectedRows(0).ReadOnly = True
        Else
            cmdDeleteBT.Enabled = True
            grdBT.SelectedRows(0).ReadOnly = False
        End If
    End Sub

    Private Sub cmdSaveUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveUser.Click
        If MsgBox("This will only affect non standard bookingtypes. Proceed?", MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then Exit Sub

        If InputBox("This function is protected with a password." & vbCrLf & "Please enter the password required:", "T R I N I T Y") <> "orange2020" Then
            System.Windows.Forms.MessageBox.Show("Wrong password!", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim TmpBT As Trinity.cBookingType
        For Each row As System.Windows.Forms.DataGridViewRow In grdBT.Rows
            TmpBT = row.Tag
            If Not TmpBT.writeProtected Then
                DBReader.saveBookingTypeInfo(TmpBT)
            End If
        Next
    End Sub

    Private Sub txtChannelGroup_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtChannelGroup.TextChanged
        Saved = False
        Dim TmpChan As Trinity.cChannel = cmbChannel.SelectedItem
        TmpChan.ChannelGroup = Trim(sender.text)
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class