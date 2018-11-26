Imports System.Windows.Forms

Public Class frmMatrixExport

    Private Enum TargetEnum
        eMainTarget = 1
        eBuyingTarget = 2
    End Enum

    Private Enum BookingTypeEnum
        eRBS = 0
        eSpecifics = 1
        elastminute = 2
        eSponsorship = 3
        eOther = 4
    End Enum

    Private Enum PremiumPositionEnum
        e1stinbreak = 0
        e2ndinbreak = 1
        e2ndlast = 3
        elast = 4
    End Enum

    Private Class MatrixChannelPanel
        Inherits Control

        Private _bt As Trinity.cBookingType

        'Controls
        Private _cmbChannel As ComboBox
        Private _cmbBT As ComboBox
        Private _txtBTText As TextBox
        Private _txtBuyingTarget As TextBox
        Private _cmbContract As ComboBox
        Private _txtGrossCost As TextBox
        Private _txtNetCost As TextBox
        Private _txtPlanTRPBuying As TextBox
        Private _txtPlanTRPMain As TextBox
        Private _txtActTRPMain As TextBox
        Private _txtActTRPBuying As TextBox
        Private _txtPlannedSpotIndex As TextBox
        Private _txtActualSpotIndex As TextBox
        Private _txtPIB1st As TextBox
        Private _txtPIBSecond As TextBox
        Private _txtPIBLast As TextBox
        Private _txtPIBSecondLast As TextBox
        Private _txtPrime As TextBox
        Private _txtReach(0 To 4) As TextBox
        Private _txtActTRPCustom(0 To 7) As TextBox
        Private _excluded As Boolean



        Public ReadOnly Property Bookingtype() As Trinity.cBookingType
            Get
                Return _bt
            End Get
        End Property

        Public Property Exclude() As Boolean
            Get
                Return _excluded
            End Get
            Set(ByVal value As Boolean)
                _excluded = value
            End Set
        End Property



        Public Property BookingtypeText() As String
            Get
                Return _txtBTText.Text
            End Get
            Set(ByVal value As String)
                _txtBTText.Text = value
            End Set
        End Property

        Public Property BuyingTarget() As String
            Get
                Return _txtBuyingTarget.Text
            End Get
            Set(ByVal value As String)
                _txtBuyingTarget.Text = value
            End Set
        End Property

        Public Property PlannedSpotIndex() As Single
            Get
                Return _txtPlannedSpotIndex.Text
            End Get
            Set(ByVal value As Single)
                _txtPlannedSpotIndex.Text = value
            End Set
        End Property

        Public Property ActualSpotIndex() As Single
            Get
                Return _txtActualSpotIndex.Text
            End Get
            Set(ByVal value As Single)
                _txtActualSpotIndex.Text = value
            End Set
        End Property

        Public Property Type() As Matrix40.cCampChannel.BookingTypeEnum
            Get
                Return _cmbBT.SelectedIndex
            End Get
            Set(ByVal value As Matrix40.cCampChannel.BookingTypeEnum)
                _cmbBT.Text = value
            End Set
        End Property

        Public ReadOnly Property Contract() As Object
            Get
                Return _cmbContract.SelectedItem
            End Get
        End Property

        Public Property GrossCost() As Decimal
            Get
                Return _txtGrossCost.Text
            End Get
            Set(ByVal value As Decimal)
                _txtGrossCost.Text = Format(value, "N0")
            End Set
        End Property

        Public Property NetCost() As Decimal
            Get
                Return _txtNetCost.Text
            End Get
            Set(ByVal value As Decimal)
                _txtNetCost.Text = Format(value, "N0")
            End Set
        End Property

        Public Property PlanTRPBuying() As Decimal
            Get
                Return _txtPlanTRPBuying.Text
            End Get
            Set(ByVal value As Decimal)
                _txtPlanTRPBuying.Text = Format(value, "N1")
            End Set
        End Property

        Public Property ActualTRPBuying() As Decimal
            Get
                Return _txtActTRPBuying.Text
            End Get
            Set(ByVal value As Decimal)
                _txtActTRPBuying.Text = Format(value, "N1")
            End Set
        End Property

        Public Property ActualTRPCustomTarget(ByVal Target As Integer) As Decimal
            Get
                Return _txtActTRPCustom(Target - 1).Text
            End Get
            Set(ByVal value As Decimal)
                _txtActTRPCustom(Target - 1).Text = Format(value, "N1")
            End Set
        End Property

        Public Property PlanTRPMain() As Decimal
            Get
                Return _txtPlanTRPMain.Text
            End Get
            Set(ByVal value As Decimal)
                _txtPlanTRPMain.Text = Format(value, "N1")
            End Set
        End Property

        Public Property ActualTRPMain() As Decimal
            Get
                Return _txtActTRPMain.Text
            End Get
            Set(ByVal value As Decimal)
                _txtActTRPMain.Text = Format(value, "N1")
            End Set
        End Property

        Public Property PIB1st() As Decimal
            Get
                Return _txtPIB1st.Text
            End Get
            Set(ByVal value As Decimal)
                _txtPIB1st.Text = Format(value, "N1")
            End Set
        End Property

        Public Property PIB2nd() As Decimal
            Get
                Return _txtPIBSecond.Text
            End Get
            Set(ByVal value As Decimal)
                _txtPIBSecond.Text = Format(value, "N1")
            End Set
        End Property

        Public Property PIBLast() As Decimal
            Get
                Return _txtPIBLast.Text
            End Get
            Set(ByVal value As Decimal)
                _txtPIBLast.Text = Format(value, "N1")
            End Set
        End Property

        Public Property PIB2ndLast() As Decimal
            Get
                Return _txtPIBSecondLast.Text
            End Get
            Set(ByVal value As Decimal)
                _txtPIBSecondLast.Text = Format(value, "N1")
            End Set
        End Property

        Public Property Primetime() As Decimal
            Get
                Return _txtPrime.Text
            End Get
            Set(ByVal value As Decimal)
                _txtPrime.Text = Format(value, "N1")
            End Set
        End Property

        Public Property Reach(ByVal Freq As Integer) As Decimal
            Get
                Return _txtReach(Freq - 1).Text
            End Get
            Set(ByVal value As Decimal)
                _txtReach(Freq - 1).Text = Format(value, "N1")
            End Set
        End Property

        Public Sub Blackout()
            If _cmbChannel.SelectedItem.GetType.FullName <> "Matrix40.cChannel" AndAlso _cmbChannel.SelectedItem = "Exclude" Then
                For Each tmpCtrl As Control In Me.Controls
                    tmpCtrl.Visible = False
                Next
                _cmbChannel.Visible = True
                Exclude = True
            Else
                For Each tmpCtrl As Control In Me.Controls
                    tmpCtrl.Visible = True
                Next
                Exclude = False
            End If
        End Sub
        Public Sub New(ByVal Bookingtype As Trinity.cBookingType)
            _bt = Bookingtype

            Me.Width = 100
            Me.Height = 1000

            _cmbChannel = New ComboBox
            AddHandler _cmbChannel.SelectedIndexChanged, AddressOf Blackout

            _cmbChannel.DropDownStyle = ComboBoxStyle.DropDownList
            _cmbChannel.DisplayMember = "Name"
            Dim j As Integer = 0

            For i As Integer = 1 To Matrix.Channels.Count
                If Matrix.GetType IsNot GetType(Matrix40.cMatrix) OrElse Not DirectCast(Matrix, Matrix40.cMatrix).Channels(i).VirtualChannel Then
                    j += 1
                    _cmbChannel.Items.Add(Matrix.Channels(i))
                    If Matrix.Channels(i).Name = Bookingtype.ParentChannel.MatrixName Then
                        _cmbChannel.SelectedIndex = j - 1
                    End If
                End If
            Next

            _cmbChannel.Items.Add("Exclude")

            _cmbChannel.Width = Me.Width
            _cmbChannel.Visible = True

            _cmbBT = New ComboBox
            _cmbBT.DropDownStyle = ComboBoxStyle.DropDownList
            _cmbBT.Top = _cmbChannel.Bottom
            With _cmbBT.Items
                .Add("RBS")
                .Add("Specifics")
                .Add("Last minute")
                .Add("Sponsorship")
                .Add("Other")
            End With
            _cmbBT.SelectedItem = Bookingtype.Name
            _cmbBT.Width = Me.Width

            _txtBTText = New TextBox
            _txtBTText.Width = Me.Width
            _txtBTText.Top = _cmbBT.Bottom

            _txtBuyingTarget = New TextBox
            _txtBuyingTarget.Width = Me.Width
            _txtBuyingTarget.Top = _txtBTText.Bottom

            _cmbContract = New ComboBox
            _cmbContract.DisplayMember = "Name"
            _cmbContract.DropDownStyle = ComboBoxStyle.DropDownList
            _cmbContract.Top = _txtBuyingTarget.Bottom
            _cmbContract.Width = Me.Width

            _txtGrossCost = New TextBox
            _txtGrossCost.Width = Me.Width
            _txtGrossCost.Top = _cmbContract.Bottom

            _txtNetCost = New TextBox
            _txtNetCost.Width = Me.Width
            _txtNetCost.Top = _txtGrossCost.Bottom

            _txtPlanTRPBuying = New TextBox
            _txtPlanTRPBuying.Width = Me.Width
            _txtPlanTRPBuying.Top = _txtNetCost.Bottom

            _txtActTRPBuying = New TextBox
            _txtActTRPBuying.Width = Me.Width
            _txtActTRPBuying.Top = _txtPlanTRPBuying.Bottom

            _txtPlanTRPMain = New TextBox
            _txtPlanTRPMain.Width = Me.Width
            _txtPlanTRPMain.Top = _txtActTRPBuying.Bottom

            _txtActTRPMain = New TextBox
            _txtActTRPMain.Width = Me.Width
            _txtActTRPMain.Top = _txtPlanTRPMain.Bottom

            _txtPlannedSpotIndex = New TextBox
            _txtPlannedSpotIndex.Width = Me.Width
            _txtPlannedSpotIndex.Top = _txtActTRPMain.Bottom

            _txtActualSpotIndex = New TextBox
            _txtActualSpotIndex.Width = Me.Width
            _txtActualSpotIndex.Top = _txtPlannedSpotIndex.Bottom

            _txtPIB1st = New TextBox
            _txtPIB1st.Width = Me.Width
            _txtPIB1st.Top = _txtActualSpotIndex.Bottom

            _txtPIBSecond = New TextBox
            _txtPIBSecond.Width = Me.Width
            _txtPIBSecond.Top = _txtPIB1st.Bottom

            _txtPIBSecondLast = New TextBox
            _txtPIBSecondLast.Width = Me.Width
            _txtPIBSecondLast.Top = _txtPIBSecond.Bottom

            _txtPIBLast = New TextBox
            _txtPIBLast.Width = Me.Width
            _txtPIBLast.Top = _txtPIBSecondLast.Bottom

            _txtPrime = New TextBox
            _txtPrime.Width = Me.Width
            _txtPrime.Top = _txtPIBLast.Bottom

            For i As Integer = 0 To 4
                _txtReach(i) = New TextBox
                _txtReach(i).Width = Me.Width
                If i > 0 Then
                    _txtReach(i).Top = _txtReach(i - 1).Bottom
                Else
                    _txtReach(i).Top = _txtPrime.Bottom
                End If
            Next

            For i As Integer = 0 To 7
                _txtActTRPCustom(i) = New TextBox
                _txtActTRPCustom(i).Width = Me.Width
                If i > 0 Then
                    _txtActTRPCustom(i).Top = _txtActTRPCustom(i - 1).Bottom
                Else
                    _txtActTRPCustom(i).Top = _txtReach(4).Bottom
                End If
            Next
            Me.Controls.Add(_cmbChannel)
            Me.Controls.Add(_cmbBT)
            Me.Controls.Add(_txtBTText)
            Me.Controls.Add(_txtBuyingTarget)
            Me.Controls.Add(_cmbContract)
            Me.Controls.Add(_txtGrossCost)
            Me.Controls.Add(_txtNetCost)
            Me.Controls.Add(_txtPlanTRPBuying)
            Me.Controls.Add(_txtPlanTRPMain)
            Me.Controls.Add(_txtActTRPBuying)
            Me.Controls.Add(_txtActTRPMain)
            Me.Controls.Add(_txtPlannedSpotIndex)
            Me.Controls.Add(_txtActualSpotIndex)
            Me.Controls.Add(_txtPIB1st)
            Me.Controls.Add(_txtPIBSecond)
            Me.Controls.Add(_txtPIBSecondLast)
            Me.Controls.Add(_txtPIBLast)
            Me.Controls.Add(_txtPrime)
            For i As Integer = 0 To 4
                Me.Controls.Add(_txtReach(i))
            Next
            For i As Integer = 0 To 7
                Me.Controls.Add(_txtActTRPCustom(i))
            Next
            Me.Height = _txtActTRPCustom(7).Bottom + 3
            If Me.Height > Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height Then
                Me.Height = Screen.PrimaryScreen.WorkingArea.Height
            End If
        End Sub

        Public ReadOnly Property Channel() As Object
            Get
                Return _cmbChannel.SelectedItem
            End Get
        End Property

        Public Sub PopulateContractCombo(ByVal client As Object)
            '_cmbContract.Items.Clear()
            'For i As Integer = 1 To client.Contracts.Count
            '    If client.Contracts.Item(i).Channel.Name = _bt.ParentChannel.AdEdgeNames Or client.Contracts.Item(i).Channel.Name = _bt.ParentChannel.ChannelName Then
            '        _cmbContract.Items.Add(client.Contracts.Item(i).name)
            '    End If
            'Next
        End Sub
    End Class

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim Panel As Integer = 1
        If cmbProduct.SelectedIndex = -1 Or cmbClient.SelectedIndex = -1 Then
            Windows.Forms.MessageBox.Show("You must choose both a client and a product.", "M A T R I X", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim TmpStr As String = "These channels has suspected CPPs:" & vbCrLf
        For Each TmpPanel As MatrixChannelPanel In pnlChannels.Controls
            If (TmpPanel.ActualTRPCustomTarget(1) > 0 AndAlso (TmpPanel.NetCost / TmpPanel.ActualTRPCustomTarget(1) > 10000 Or TmpPanel.NetCost / TmpPanel.ActualTRPCustomTarget(1) < 1000)) OrElse TmpPanel.ActualTRPCustomTarget(1) = 0 AndAlso TmpPanel.NetCost > 0 Then
                If TmpPanel.ActualTRPCustomTarget(1) > 0 Then
                    TmpStr += TmpPanel.Bookingtype.ToString & ": " & Format(TmpPanel.NetCost / TmpPanel.ActualTRPCustomTarget(1), "N0") & vbCrLf
                Else
                    TmpStr += TmpPanel.Bookingtype.ToString & ": No ratings " & vbCrLf
                End If
            End If
            If TmpPanel.Type = -1 Then
                Windows.Forms.MessageBox.Show("You have not chosen a Bookingtype for " & TmpPanel.Bookingtype.ToString & "." & vbCrLf & vbCrLf & "Campaign was not exported.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        Next
        If TmpStr <> "These channels has suspected CPPs:" & vbCrLf Then
            If Windows.Forms.MessageBox.Show(TmpStr & vbCrLf & "Are you sure you want to export this campaign?", "T R I N I T Y", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Dim j As DialogResult = Windows.Forms.DialogResult.No
        Dim ReplaceCamp As Boolean = False
        For i As Integer = 1 To Matrix.Campaigns.Count
            If Matrix.Campaigns(i).ID = Campaign.MatrixID Then
                j = Windows.Forms.MessageBox.Show("This campaign seems to have been exported to Matrix already. " & vbCrLf & " You should only export it again if the campaign is now either more accurate, or if any information needs changing. " & vbCrLf & " Replace the old campaign?", "M A T R I X", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                'j = Windows.Forms.MessageBox.Show("A campaign called " & txtName.Text & " is already" & vbCrLf & "in the Matrix database." & vbCrLf & "Would you like to replace that campaign?", "M A T R I X", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If j = Windows.Forms.DialogResult.Cancel Then Exit Sub
                If j = Windows.Forms.DialogResult.Yes Then
                    ReplaceCamp = True
                    Matrix.CurrentCampaign = Matrix.Campaigns(i)
                End If
                Exit For
            End If
        Next
        If j = Windows.Forms.DialogResult.No Then
            For i As Integer = 1 To Matrix.Campaigns.Count
                If Matrix.Campaigns(i).Name = txtName.Text Then
                    j = Windows.Forms.MessageBox.Show("A campaign with this name already exists in Matrix." & vbCrLf & "Do you want to replace that campaign?", "M A T R I X", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    'j = Windows.Forms.MessageBox.Show("A campaign called " & txtName.Text & " is already" & vbCrLf & "in the Matrix database." & vbCrLf & "Would you like to replace that campaign?", "M A T R I X", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    If j = Windows.Forms.DialogResult.Cancel Then Exit Sub
                    If j = Windows.Forms.DialogResult.Yes Then
                        ReplaceCamp = True
                        Matrix.CurrentCampaign = Matrix.Campaigns(i)
                    End If
                    Exit For
                End If
            Next
        End If
        If j = Windows.Forms.DialogResult.No Then
            Dim TmpCamp As Object
            If MatrixVersion = MatrixVersionEnum.Matrix40 Then
                TmpCamp = Matrix.Campaigns.Add()
            Else
                TmpCamp = Matrix.Campaigns.Add
            End If
            Matrix.CurrentCampaign = TmpCamp
        End If

        With Matrix.CurrentCampaign
            .Name = txtName.Text
            .StartDate = dtFrom.Value
            .EndDate = dtTo.Value
            .Target = txtTarget.Text
            .Country = txtCountry.Text
            .Comments = txtComments.Text
            .IsPlanned = False
            .Product = cmbProduct.SelectedItem
            .CanBeUsedInPool = True
            For i As Integer = 1 To 10
                .Reach(i) = grdReach.Rows(i - 1).Cells(0).Value
                If MatrixVersion = MatrixVersionEnum.Matrix40 Then
                    For c As Integer = 1 To grdReach.Columns.Count - 1
                        .reach(i, c + 2) = grdReach.Rows(i - 1).Cells(c).Value
                    Next
                End If
            Next
            For Each TmpPanel As MatrixChannelPanel In pnlChannels.Controls
                If Not TmpPanel.Exclude Then
                    Dim TmpChan As Object = Nothing
                    If ReplaceCamp Then
                        'For i As Integer = 1 To .Channels.Count
                        '    If .Channels(i).Channel.Name = TmpPanel.Channel.Name Then
                        '        Dim BT As BookingTypeEnum
                        '        Select Case TmpPanel.Type
                        '            Case "RBS" : BT = BookingTypeEnum.eRBS
                        '            Case "Specifics" : BT = BookingTypeEnum.eSpecifics
                        '            Case "Specific" : BT = BookingTypeEnum.eSpecifics
                        '            Case "Last Minute" : BT = BookingTypeEnum.elastminute
                        '            Case "Sponsorship" : BT = BookingTypeEnum.eSponsorship
                        '            Case "Other" : BT = BookingTypeEnum.eOther
                        '            Case Else : BT = BookingTypeEnum.eOther
                        '        End Select
                        '        If .Channels(i).BookingType = BT Then
                        '            TmpChan = .Channels(i)
                        '            Exit For
                        '        End If
                        '    End If
                        'Next
                        TmpChan = .Channels(Panel)
                    Else
                        TmpChan = .Channels.Add
                        TmpChan.Channel = TmpPanel.Channel
                        'Select Case TmpPanel.Type
                        '    Case "RBS" : TmpChan.BookingType = BookingTypeEnum.eRBS
                        '    Case "Specifics" : TmpChan.BookingType = BookingTypeEnum.eSpecifics
                        '    Case "Specific" : TmpChan.BookingType = BookingTypeEnum.eSpecifics
                        '    Case "Last Minute" : TmpChan.BookingType = BookingTypeEnum.elastminute
                        '    Case "Sponsorship" : TmpChan.BookingType = BookingTypeEnum.eSponsorship
                        '    Case "Other" : TmpChan.BookingType = BookingTypeEnum.eOther
                        '    Case Else : TmpChan.BookingType = BookingTypeEnum.eOther
                        'End Select
                    End If
                    TmpChan.BookingType = TmpPanel.Type
                    TmpChan.BookingTypeText = TmpPanel.BookingtypeText
                    TmpChan.BuyingTarget = TmpPanel.BuyingTarget
                    Try
                        TmpChan.Contract = TmpPanel.Contract
                    Catch

                    End Try
                    TmpChan.GrossCost = TmpPanel.GrossCost.ToString.Replace(" ", "")
                    TmpChan.NetCost = TmpPanel.NetCost.ToString.Replace(" ", "")
                    TmpChan.PlannedTRP(TargetEnum.eBuyingTarget) = TmpPanel.PlanTRPBuying
                    TmpChan.ActualTRP(TargetEnum.eBuyingTarget) = TmpPanel.ActualTRPBuying
                    TmpChan.PlannedTRP(TargetEnum.eMainTarget) = TmpPanel.PlanTRPMain
                    TmpChan.ActualTRP(TargetEnum.eMainTarget) = TmpPanel.ActualTRPMain
                    TmpChan.PlannedSpotindex = TmpPanel.PlannedSpotIndex
                    TmpChan.ActualSpotindex = TmpPanel.ActualSpotIndex
                    For i As Integer = 1 To 8
                        TmpChan.actualTRP(2 + i) = CSng(TmpPanel.ActualTRPCustomTarget(i))
                    Next
                    TmpChan.PremiumPositions(PremiumPositionEnum.e1stinbreak) = TmpPanel.PIB1st / 100
                    TmpChan.PremiumPositions(PremiumPositionEnum.e2ndinbreak) = TmpPanel.PIB2nd / 100
                    TmpChan.PremiumPositions(PremiumPositionEnum.e2ndlast) = TmpPanel.PIB2ndLast / 100
                    TmpChan.PremiumPositions(PremiumPositionEnum.elast) = TmpPanel.PIBLast / 100
                    TmpChan.PrimeTime = TmpPanel.Primetime / 100
                    For f As Integer = 1 To 5
                        TmpChan.Reach(f) = TmpPanel.Reach(f) / 100
                    Next
                    Panel += 1
                End If
            Next
            If MatrixVersion = MatrixVersionEnum.Matrix30 Then
                .Save()
                Campaign.MatrixID = Matrix.CurrentCampaign.ID
            Else
                Matrix.Save()
                'Hannes added this which puts the Matrix campaign ID into the MatrixID property of the Trinity campaign after it has been saved
                Campaign.MatrixID = Matrix.CurrentCampaign.ID
            End If

            Dim TmpXML As New Xml.XmlDocument
            Dim StrippedXML As New Xml.XmlDocument
            Dim Node As Xml.XmlElement

            TmpXML.LoadXml(Campaign.SaveCampaign(DoNotSaveToFile:=True, SkipHistory:=True, SkipLab:=True, SkipReach:=True))
            StrippedXML.LoadXml(TmpXML.OuterXml)

            Node = StrippedXML.GetElementsByTagName("Contract")(0)
            If Node IsNot Nothing Then Node.RemoveAll()
            Node = StrippedXML.GetElementsByTagName("ActualSpots")(0)
            If Node IsNot Nothing Then Node.RemoveAll()
            Node = StrippedXML.GetElementsByTagName("PlannedSpots")(0)
            If Node IsNot Nothing Then Node.RemoveAll()
            Node = StrippedXML.GetElementsByTagName("BookedSpots")(0)
            If Node IsNot Nothing Then Node.RemoveAll()
            Node = StrippedXML.GetElementsByTagName("Channels")(0)
            If Node IsNot Nothing Then Node.RemoveAll()

            'Dim Enc As New System.Text.ASCIIEncoding()

            'Dim infile As New IO.MemoryStream(Enc.GetBytes(TmpXML.GetElementsByTagName("ActualSpots")(0).OuterXml.Replace("'", "''")))
            'Dim buffer(infile.Length - 1) As Byte
            'infile.Read(buffer, 0, buffer.Length)
            'infile.Close()
            'Dim ms As New IO.MemoryStream

            'Dim zip As New IO.Compression.GZipStream(ms, IO.Compression.CompressionMode.Compress, True)
            'zip.Write(buffer, 0, buffer.Length)
            'Dim TmpByte(ms.Length) As Byte
            'ms.Position = 0
            'ms.Read(TmpByte, 0, TmpByte.Length)
            'Dim TmpAC As String = New System.Text.ASCIIEncoding().GetString(TmpByte)

            Try
                .SetTrinityCampaignXML(StrippedXML.OuterXml.Replace("'", "''"), TmpXML.GetElementsByTagName("Channels")(0).OuterXml.Replace("'", "''"), TmpXML.GetElementsByTagName("ActualSpots")(0).OuterXml.Replace("'", "''"), TmpXML.GetElementsByTagName("PlannedSpots")(0).OuterXml.Replace("'", "''"), TmpXML.GetElementsByTagName("BookedSpots")(0).OuterXml.Replace("'", "''"))
                .save()
            Catch

            End Try
        End With
        If Campaign.MatrixID <> "" Then
            MessageBox.Show("Campaign was successfully saved to Matrix.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Campaign was NOT saved to Matrix.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmMatrixExport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        pnlButtons.Anchor = AnchorStyles.Top + AnchorStyles.Right
        Dim _calc As Boolean = False
        'Cache all ratings
        For Each TmpSpot As Trinity.cActualSpot In Campaign.ActualSpots
            Dim Dummy As Single = TmpSpot.Rating
        Next
        For c As Integer = 0 To 7
            If Campaign.TargColl(Matrix.CustomTarget(c + 1).Target, Campaign.Adedge) < 0 Then
                Dim TmpTarget As String = Matrix.CustomTarget(c + 1).Target
                Trinity.Helper.AddTarget(Campaign.Adedge, TmpTarget)
                _calc = True
            End If
        Next
        If _calc Then
            Campaign.Adedge.Run(True, , 10)
        End If
        Campaign.CalculateSpots(CalculateReach:=True, ForceCalculate:=_calc)
        Campaign.BlockCalculateSpots = True
        grdReach.Rows.Clear()
        grdReach.Rows.Add(10)
        For i As Integer = 0 To 9
            grdReach.Rows(i).HeaderCell.Value = i + 1 & "+"
            grdReach.Rows(i).Cells(0).Value = Format(Campaign.ReachActual(i + 1), "N1")
            If MatrixVersion = MatrixVersionEnum.Matrix40 Then
                For c As Integer = 0 To 7
                    If i = 0 Then
                        grdReach.Columns.Add("col" & Matrix.CustomTarget(c + 1).target, Matrix.CustomTarget(c + 1).Target)
                        grdReach.Columns(c + 1).Width = 50
                    End If
                    grdReach.Rows(i).Cells(c + 1).Value = Format(Campaign.ReachActual(i + 1, clTrinity.Trinity.cKampanj.ReachTargetEnum.rteCustomTarget, Matrix.CustomTarget(c + 1).Target), "N1")
                Next
            End If
        Next
        If Not MatrixVersion = MatrixVersionEnum.Matrix40 Then
            grdReach.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Else
            grdReach.Columns(0).Width = 50
            grdReach.Width = 50 * 9 + grdReach.RowHeadersWidth + 6
            grpInfo.Width = grdReach.Right + 6
            If grpInfo.Right > pnlButtons.Left Then
                Me.Width = grpInfo.Right + 6 + pnlButtons.Width + (Me.Width - pnlButtons.Right)
            End If
        End If
        grdReach.RowHeadersWidth = 57
        grdReach.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        txtName.Text = Campaign.Name
        dtFrom.Value = Date.FromOADate(Campaign.StartDate)
        dtTo.Value = Date.FromOADate(Campaign.EndDate)
        cmbClient.Items.Clear()
        cmbClient.DisplayMember = "Name"
        If MatrixVersion = MatrixVersionEnum.Matrix30 Then
            For i As Integer = 1 To Matrix.Clients.Count
                Dim TmpClient As Object = Matrix.Clients(i)
                If TmpClient.name <> "" Then cmbClient.Items.Add(TmpClient)
            Next
            For i As Integer = 0 To cmbClient.Items.Count - 1
                If cmbClient.Items(i).name = Campaign.Client Then
                    cmbClient.SelectedIndex = i
                    Exit For
                End If
            Next
        Else
            For Each TmpClient As Object In Matrix.Clients
                Try
                    If TmpClient.Name <> "" Then cmbClient.Items.Add(TmpClient)
                Catch
                    Debug.Print("ERROR with a CLIENT")
                End Try
            Next
            For i As Integer = 0 To cmbClient.Items.Count - 1
                If cmbClient.Items(i).name = Campaign.Client Then
                    cmbClient.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
        txtTarget.Text = Campaign.MainTarget.TargetName
        txtCountry.Text = Campaign.Area
        For i As Integer = 1 To 8
            Panel1.Controls("lblCustom" & i).Text = "Actual (" & Matrix.customtarget(CInt(i)).target & ")"
        Next

        Dim CountBefore As Integer = Campaign.Adedge.getTargetCount
        For i As Integer = 1 To 8
            If Trinity.Helper.TargetIndex(Campaign.Adedge, Matrix.CustomTarget(CInt(i)).Target) < 0 Then
                Campaign.Adedge.setTargetMnemonic(Matrix.CustomTarget(CInt(i)).Target, True)
            End If
        Next
        If CountBefore < Campaign.Adedge.getTargetCount Then
            Campaign.Adedge.Run(False, False, 10, True)
        End If

        'Build channel grid

        Dim LastRight As Integer = 0
        Dim TmpPanel As MatrixChannelPanel = Nothing
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    TmpPanel = New MatrixChannelPanel(TmpBT)
                    If TmpBT.IsSponsorship Then
                        TmpPanel.Type = Matrix40.cCampChannel.BookingTypeEnum.eSponsorship
                    ElseIf TmpBT.IsRBS Then
                        TmpPanel.Type = Matrix40.cCampChannel.BookingTypeEnum.eRBS
                    ElseIf TmpBT.IsSpecific Then
                        TmpPanel.Type = Matrix40.cCampChannel.BookingTypeEnum.eSpecifics
                    Else
                        TmpPanel.Type = Matrix40.cCampChannel.BookingTypeEnum.eOther
                    End If
                    TmpPanel.BookingtypeText = TmpBT.Name
                    TmpPanel.BuyingTarget = TmpBT.BuyingTarget.TargetName
                    TmpPanel.PlannedSpotIndex = TmpBT.SpotIndex
                    If TmpBT.ActualTRP(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget) > 0 Then
                        TmpPanel.ActualSpotIndex = TmpBT.ActualTRP30(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget) / TmpBT.ActualTRP(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget)
                    Else
                        TmpPanel.ActualSpotIndex = 1
                    End If
                    If TmpBT.ConfirmedNetBudget > 0 Then
                        TmpPanel.GrossCost = TmpBT.ConfirmedGrossBudget
                        TmpPanel.NetCost = TmpBT.ConfirmedNetBudget
                    Else
                        TmpPanel.GrossCost = TmpBT.PlannedGrossBudget
                        TmpPanel.NetCost = TmpBT.PlannedNetBudget
                    End If
                    TmpPanel.PlanTRPBuying = TmpBT.PlannedTRP30(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
                    TmpPanel.PlanTRPMain = TmpBT.PlannedTRP30(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget)
                    Campaign.CalculateSpots(CalculateReach:=True, UseFilters:=False, Bookingtype:=TmpBT, ForceCalculate:=True)
                    Try
                        Campaign.Adedge.recalcRF(Connect.eSumModes.smGroup)
                    Catch
                    End Try
                    TmpPanel.ActualTRPBuying = Campaign.Adedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smGroup, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, TmpBT.BuyingTarget.Target))
                    TmpPanel.ActualTRPMain = Campaign.Adedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smGroup, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.MainTarget))
                    For i As Integer = 1 To 8
                        TmpPanel.ActualTRPCustomTarget(i) = Campaign.Adedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smGroup, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Matrix.customtarget(CInt(i)).target))
                    Next
                    For i As Integer = 1 To 5
                        If Campaign.Adedge.getGroupCount > 0 Then
                            TmpPanel.Reach(i) = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.MainTarget), i)
                        Else
                            TmpPanel.Reach(i) = 0
                        End If
                    Next
                    Campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Bookingtype:=TmpBT, ForceCalculate:=True, PrimeOnly:=True)
                    If TmpPanel.ActualTRPMain > 0 Then
                        TmpPanel.Primetime = (Campaign.Adedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smGroup, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.MainTarget)) / TmpPanel.ActualTRPMain) * 100
                    Else
                        TmpPanel.Primetime = 0
                    End If
                    Campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Bookingtype:=TmpBT, PosInBreak:=Trinity.cKampanj.PIBEnum.PIBFirst, ForceCalculate:=True)
                    If TmpPanel.ActualTRPMain > 0 Then
                        TmpPanel.PIB1st = (Campaign.Adedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smGroup, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.MainTarget)) / TmpPanel.ActualTRPMain) * 100
                    Else
                        TmpPanel.PIB1st = 0
                    End If
                    Campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Bookingtype:=TmpBT, PosInBreak:=Trinity.cKampanj.PIBEnum.PIB2ndFirst, ForceCalculate:=True)
                    If TmpPanel.ActualTRPMain > 0 Then
                        TmpPanel.PIB2nd = (Campaign.Adedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smGroup, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.MainTarget)) / TmpPanel.ActualTRPMain) * 100
                    Else
                        TmpPanel.PIB2nd = 0
                    End If
                    Campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Bookingtype:=TmpBT, PosInBreak:=Trinity.cKampanj.PIBEnum.PIB2ndLast, ForceCalculate:=True)
                    If TmpPanel.ActualTRPMain > 0 Then
                        TmpPanel.PIB2ndLast = (Campaign.Adedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smGroup, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.MainTarget)) / TmpPanel.ActualTRPMain) * 100
                    Else
                        TmpPanel.PIB2ndLast = 0
                    End If
                    Campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Bookingtype:=TmpBT, PosInBreak:=Trinity.cKampanj.PIBEnum.PIBLast, ForceCalculate:=True)
                    If TmpPanel.ActualTRPMain > 0 Then
                        TmpPanel.PIBLast = (Campaign.Adedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smGroup, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.MainTarget)) / TmpPanel.ActualTRPMain) * 100
                    Else
                        TmpPanel.PIBLast = 0
                    End If
                    pnlChannels.Controls.Add(TmpPanel)
                    TmpPanel.Visible = True
                    TmpPanel.Left = LastRight + 1
                    LastRight = TmpPanel.Right
                End If
            Next
        Next
        If Not TmpPanel Is Nothing Then
            pnlChannels.Width = TmpPanel.Right + 25
        End If
        Me.Width = pnlChannels.Right + 25
        If Me.Right > Windows.Forms.Screen.PrimaryScreen.WorkingArea.Right Then
            Me.Width = Windows.Forms.Screen.PrimaryScreen.WorkingArea.Right - 50 - Me.Left
        End If
        If Me.Bottom > Windows.Forms.Screen.PrimaryScreen.WorkingArea.Bottom Then
            Me.Height = Windows.Forms.Screen.PrimaryScreen.WorkingArea.Bottom - 50 - Me.Top
        End If
        Campaign.BlockCalculateSpots = False
        frmMain.Cursor = Cursors.Default
    End Sub

    Private Sub cmbClient_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClient.SelectedIndexChanged
        Dim TmpClient As Object = cmbClient.SelectedItem

        cmbProduct.Items.Clear()
        cmbProduct.DisplayMember = "Name"
        If MatrixVersion = MatrixVersionEnum.Matrix30 Then
            For i As Integer = 1 To TmpClient.Products.Count
                cmbProduct.Items.Add(TmpClient.Products.Item(i))
            Next
            For i As Integer = 0 To cmbProduct.Items.Count - 1
                If cmbProduct.Items(i).name = Campaign.Product Then
                    cmbProduct.SelectedIndex = i
                    Exit For
                End If
            Next
        Else
            For Each TmpProd As Object In TmpClient.Products
                cmbProduct.Items.Add(TmpProd)
            Next
            For i As Integer = 0 To cmbProduct.Items.Count - 1
                If cmbProduct.Items(i).name = Campaign.Product Then
                    cmbProduct.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
        For Each TmpPnl As MatrixChannelPanel In pnlChannels.Controls
            TmpPnl.PopulateContractCombo(TmpClient)
        Next
    End Sub
End Class
