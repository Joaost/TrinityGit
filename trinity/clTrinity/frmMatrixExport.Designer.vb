<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMatrixExport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMatrixExport))
        Me.pnlButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.grpInfo = New System.Windows.Forms.GroupBox()
        Me.grdReach = New System.Windows.Forms.DataGridView()
        Me.colReach = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtTo = New clTrinity.ExtendedDateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtFrom = New clTrinity.ExtendedDateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCountry = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTarget = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbProduct = New System.Windows.Forms.ComboBox()
        Me.lblProduct = New System.Windows.Forms.Label()
        Me.cmbClient = New System.Windows.Forms.ComboBox()
        Me.lblClient = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlChannels = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblCustom4 = New System.Windows.Forms.Label()
        Me.lblCustom5 = New System.Windows.Forms.Label()
        Me.lblCustom7 = New System.Windows.Forms.Label()
        Me.lblCustom1 = New System.Windows.Forms.Label()
        Me.lblCustom6 = New System.Windows.Forms.Label()
        Me.lblCustom2 = New System.Windows.Forms.Label()
        Me.lblCustom3 = New System.Windows.Forms.Label()
        Me.lblCustom8 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlButtons.SuspendLayout()
        Me.grpInfo.SuspendLayout()
        CType(Me.grdReach, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlButtons
        '
        Me.pnlButtons.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlButtons.ColumnCount = 1
        Me.pnlButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.pnlButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.pnlButtons.Controls.Add(Me.OK_Button, 0, 0)
        Me.pnlButtons.Controls.Add(Me.Cancel_Button, 0, 1)
        Me.pnlButtons.Location = New System.Drawing.Point(899, 13)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.RowCount = 2
        Me.pnlButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.pnlButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.pnlButtons.Size = New System.Drawing.Size(83, 69)
        Me.pnlButtons.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.FlatAppearance.BorderSize = 0
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(77, 28)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.FlatAppearance.BorderSize = 0
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Button.Location = New System.Drawing.Point(8, 39)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 25)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'grpInfo
        '
        Me.grpInfo.Controls.Add(Me.grdReach)
        Me.grpInfo.Controls.Add(Me.Label9)
        Me.grpInfo.Controls.Add(Me.txtComments)
        Me.grpInfo.Controls.Add(Me.Label8)
        Me.grpInfo.Controls.Add(Me.dtTo)
        Me.grpInfo.Controls.Add(Me.Label7)
        Me.grpInfo.Controls.Add(Me.dtFrom)
        Me.grpInfo.Controls.Add(Me.Label6)
        Me.grpInfo.Controls.Add(Me.txtCountry)
        Me.grpInfo.Controls.Add(Me.Label5)
        Me.grpInfo.Controls.Add(Me.txtTarget)
        Me.grpInfo.Controls.Add(Me.Label4)
        Me.grpInfo.Controls.Add(Me.cmbProduct)
        Me.grpInfo.Controls.Add(Me.lblProduct)
        Me.grpInfo.Controls.Add(Me.cmbClient)
        Me.grpInfo.Controls.Add(Me.lblClient)
        Me.grpInfo.Controls.Add(Me.txtName)
        Me.grpInfo.Controls.Add(Me.Label1)
        Me.grpInfo.Location = New System.Drawing.Point(12, 12)
        Me.grpInfo.Name = "grpInfo"
        Me.grpInfo.Size = New System.Drawing.Size(411, 289)
        Me.grpInfo.TabIndex = 1
        Me.grpInfo.TabStop = False
        Me.grpInfo.Text = "General Info"
        '
        'grdReach
        '
        Me.grdReach.AllowUserToAddRows = False
        Me.grdReach.AllowUserToDeleteRows = False
        Me.grdReach.AllowUserToResizeColumns = False
        Me.grdReach.AllowUserToResizeRows = False
        Me.grdReach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdReach.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colReach})
        Me.grdReach.Location = New System.Drawing.Point(279, 33)
        Me.grdReach.Name = "grdReach"
        Me.grdReach.Size = New System.Drawing.Size(119, 245)
        Me.grdReach.TabIndex = 17
        '
        'colReach
        '
        Me.colReach.HeaderText = "Reach"
        Me.colReach.Name = "colReach"
        Me.colReach.Width = 75
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(276, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 14)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Reach"
        '
        'txtComments
        '
        Me.txtComments.Location = New System.Drawing.Point(6, 237)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(267, 41)
        Me.txtComments.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 220)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 14)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Comments"
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(150, 73)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.ShowWeekNumbers = True
        Me.dtTo.Size = New System.Drawing.Size(123, 20)
        Me.dtTo.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(133, 76)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(11, 14)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "-"
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(6, 73)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.ShowWeekNumbers = True
        Me.dtFrom.Size = New System.Drawing.Size(121, 20)
        Me.dtFrom.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 56)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 14)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Period"
        '
        'txtCountry
        '
        Me.txtCountry.Location = New System.Drawing.Point(145, 197)
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(128, 20)
        Me.txtCountry.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(145, 180)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 14)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Country"
        '
        'txtTarget
        '
        Me.txtTarget.Location = New System.Drawing.Point(6, 197)
        Me.txtTarget.Name = "txtTarget"
        Me.txtTarget.Size = New System.Drawing.Size(128, 20)
        Me.txtTarget.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 180)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 14)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Target"
        '
        'cmbProduct
        '
        Me.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProduct.FormattingEnabled = True
        Me.cmbProduct.Location = New System.Drawing.Point(6, 155)
        Me.cmbProduct.Name = "cmbProduct"
        Me.cmbProduct.Size = New System.Drawing.Size(267, 22)
        Me.cmbProduct.Sorted = True
        Me.cmbProduct.TabIndex = 5
        '
        'lblProduct
        '
        Me.lblProduct.AutoSize = True
        Me.lblProduct.Location = New System.Drawing.Point(6, 138)
        Me.lblProduct.Name = "lblProduct"
        Me.lblProduct.Size = New System.Drawing.Size(44, 14)
        Me.lblProduct.TabIndex = 4
        Me.lblProduct.Text = "Product"
        '
        'cmbClient
        '
        Me.cmbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClient.FormattingEnabled = True
        Me.cmbClient.Location = New System.Drawing.Point(6, 113)
        Me.cmbClient.Name = "cmbClient"
        Me.cmbClient.Size = New System.Drawing.Size(267, 22)
        Me.cmbClient.Sorted = True
        Me.cmbClient.TabIndex = 3
        '
        'lblClient
        '
        Me.lblClient.AutoSize = True
        Me.lblClient.Location = New System.Drawing.Point(6, 96)
        Me.lblClient.Name = "lblClient"
        Me.lblClient.Size = New System.Drawing.Size(33, 14)
        Me.lblClient.TabIndex = 2
        Me.lblClient.Text = "Client"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(6, 33)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(267, 20)
        Me.txtName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Campaign name"
        '
        'pnlChannels
        '
        Me.pnlChannels.Location = New System.Drawing.Point(106, 307)
        Me.pnlChannels.Name = "pnlChannels"
        Me.pnlChannels.Size = New System.Drawing.Size(402, 621)
        Me.pnlChannels.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label30)
        Me.Panel1.Controls.Add(Me.Label29)
        Me.Panel1.Controls.Add(Me.lblCustom4)
        Me.Panel1.Controls.Add(Me.lblCustom5)
        Me.Panel1.Controls.Add(Me.lblCustom7)
        Me.Panel1.Controls.Add(Me.lblCustom1)
        Me.Panel1.Controls.Add(Me.lblCustom6)
        Me.Panel1.Controls.Add(Me.lblCustom2)
        Me.Panel1.Controls.Add(Me.lblCustom3)
        Me.Panel1.Controls.Add(Me.lblCustom8)
        Me.Panel1.Controls.Add(Me.Label27)
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label24)
        Me.Panel1.Controls.Add(Me.Label25)
        Me.Panel1.Controls.Add(Me.Label28)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(12, 307)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(89, 621)
        Me.Panel1.TabIndex = 3
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(4, 245)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(75, 14)
        Me.Label30.TabIndex = 31
        Me.Label30.Text = "Act Spotindex"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(4, 65)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(37, 14)
        Me.Label29.TabIndex = 30
        Me.Label29.Text = "Target"
        '
        'lblCustom4
        '
        Me.lblCustom4.AutoSize = True
        Me.lblCustom4.Location = New System.Drawing.Point(3, 523)
        Me.lblCustom4.Name = "lblCustom4"
        Me.lblCustom4.Size = New System.Drawing.Size(38, 14)
        Me.lblCustom4.TabIndex = 29
        Me.lblCustom4.Text = "Actual"
        '
        'lblCustom5
        '
        Me.lblCustom5.AutoSize = True
        Me.lblCustom5.Location = New System.Drawing.Point(3, 543)
        Me.lblCustom5.Name = "lblCustom5"
        Me.lblCustom5.Size = New System.Drawing.Size(38, 14)
        Me.lblCustom5.TabIndex = 28
        Me.lblCustom5.Text = "Actual"
        '
        'lblCustom7
        '
        Me.lblCustom7.AutoSize = True
        Me.lblCustom7.Location = New System.Drawing.Point(3, 583)
        Me.lblCustom7.Name = "lblCustom7"
        Me.lblCustom7.Size = New System.Drawing.Size(38, 14)
        Me.lblCustom7.TabIndex = 27
        Me.lblCustom7.Text = "Actual"
        '
        'lblCustom1
        '
        Me.lblCustom1.AutoSize = True
        Me.lblCustom1.Location = New System.Drawing.Point(4, 463)
        Me.lblCustom1.Name = "lblCustom1"
        Me.lblCustom1.Size = New System.Drawing.Size(38, 14)
        Me.lblCustom1.TabIndex = 26
        Me.lblCustom1.Text = "Actual"
        '
        'lblCustom6
        '
        Me.lblCustom6.AutoSize = True
        Me.lblCustom6.Location = New System.Drawing.Point(3, 563)
        Me.lblCustom6.Name = "lblCustom6"
        Me.lblCustom6.Size = New System.Drawing.Size(38, 14)
        Me.lblCustom6.TabIndex = 25
        Me.lblCustom6.Text = "Actual"
        '
        'lblCustom2
        '
        Me.lblCustom2.AutoSize = True
        Me.lblCustom2.Location = New System.Drawing.Point(3, 483)
        Me.lblCustom2.Name = "lblCustom2"
        Me.lblCustom2.Size = New System.Drawing.Size(38, 14)
        Me.lblCustom2.TabIndex = 24
        Me.lblCustom2.Text = "Actual"
        '
        'lblCustom3
        '
        Me.lblCustom3.AutoSize = True
        Me.lblCustom3.Location = New System.Drawing.Point(3, 503)
        Me.lblCustom3.Name = "lblCustom3"
        Me.lblCustom3.Size = New System.Drawing.Size(38, 14)
        Me.lblCustom3.TabIndex = 23
        Me.lblCustom3.Text = "Actual"
        '
        'lblCustom8
        '
        Me.lblCustom8.AutoSize = True
        Me.lblCustom8.Location = New System.Drawing.Point(3, 603)
        Me.lblCustom8.Name = "lblCustom8"
        Me.lblCustom8.Size = New System.Drawing.Size(38, 14)
        Me.lblCustom8.TabIndex = 22
        Me.lblCustom8.Text = "Actual"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(4, 385)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(53, 14)
        Me.Label27.TabIndex = 21
        Me.Label27.Text = "Reach 2+"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(4, 405)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(53, 14)
        Me.Label26.TabIndex = 20
        Me.Label26.Text = "Reach 3+"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(4, 425)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(53, 14)
        Me.Label16.TabIndex = 19
        Me.Label16.Text = "Reach 4+"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 445)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 14)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Reach 5+"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(4, 185)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(68, 14)
        Me.Label24.TabIndex = 17
        Me.Label24.Text = "Pln TRP Main"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(4, 205)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(71, 14)
        Me.Label25.TabIndex = 16
        Me.Label25.Text = "Act TRP Main"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(4, 45)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(51, 14)
        Me.Label28.TabIndex = 15
        Me.Label28.Text = "Type text"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(4, 25)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(30, 14)
        Me.Label23.TabIndex = 15
        Me.Label23.Text = "Type"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(4, 85)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(48, 14)
        Me.Label22.TabIndex = 14
        Me.Label22.Text = "Contract"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(4, 285)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(64, 14)
        Me.Label21.TabIndex = 13
        Me.Label21.Text = "PIB 2nd (%)"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(4, 125)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(59, 14)
        Me.Label20.TabIndex = 12
        Me.Label20.Text = "Net budget"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(4, 265)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(61, 14)
        Me.Label19.TabIndex = 11
        Me.Label19.Text = "PIB 1st (%)"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(4, 345)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(76, 14)
        Me.Label18.TabIndex = 10
        Me.Label18.Text = "Prime time (%)"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(4, 365)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(53, 14)
        Me.Label17.TabIndex = 9
        Me.Label17.Text = "Reach 1+"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(4, 105)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(74, 14)
        Me.Label15.TabIndex = 7
        Me.Label15.Text = "Gross Budget"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(4, 145)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(65, 14)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "Pln TRP Buy"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(4, 165)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 14)
        Me.Label13.TabIndex = 5
        Me.Label13.Text = "Act TRP Buy"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(4, 325)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(67, 14)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "PIB Last (%)"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(4, 305)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(84, 14)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "PIB 2nd last (%)"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(4, 225)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 14)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Pln Spotindex"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Channel"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Reach"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 75
        '
        'frmMatrixExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1161, 678)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlChannels)
        Me.Controls.Add(Me.grpInfo)
        Me.Controls.Add(Me.pnlButtons)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(531, 38)
        Me.Name = "frmMatrixExport"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Export to Matrix"
        Me.pnlButtons.ResumeLayout(False)
        Me.grpInfo.ResumeLayout(False)
        Me.grpInfo.PerformLayout()
        CType(Me.grdReach, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlButtons As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents grpInfo As System.Windows.Forms.GroupBox
    Friend WithEvents dtFrom As clTrinity.ExtendedDateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTarget As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbProduct As System.Windows.Forms.ComboBox
    Friend WithEvents lblProduct As System.Windows.Forms.Label
    Friend WithEvents cmbClient As System.Windows.Forms.ComboBox
    Friend WithEvents lblClient As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdReach As System.Windows.Forms.DataGridView
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtTo As clTrinity.ExtendedDateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents colReach As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pnlChannels As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblCustom4 As System.Windows.Forms.Label
    Friend WithEvents lblCustom5 As System.Windows.Forms.Label
    Friend WithEvents lblCustom7 As System.Windows.Forms.Label
    Friend WithEvents lblCustom1 As System.Windows.Forms.Label
    Friend WithEvents lblCustom6 As System.Windows.Forms.Label
    Friend WithEvents lblCustom2 As System.Windows.Forms.Label
    Friend WithEvents lblCustom3 As System.Windows.Forms.Label
    Friend WithEvents lblCustom8 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label

End Class
