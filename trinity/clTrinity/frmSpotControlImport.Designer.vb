<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSpotControlImport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSpotControlImport))
        Me.cmdBrowse = New System.Windows.Forms.Button()
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.txtFromDate = New System.Windows.Forms.TextBox()
        Me.txtToDate = New System.Windows.Forms.TextBox()
        Me.grdAdvantEdge = New System.Windows.Forms.DataGridView()
        Me.col_Date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_Time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_Channel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_Prolgram = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_Spot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_Advertiser = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_Duration = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_FilmCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grdSpotControl = New System.Windows.Forms.DataGridView()
        Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChannel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colProgram = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colReason = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAdvertiser = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSpot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDuration = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdMatch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdClear = New System.Windows.Forms.Button()
        CType(Me.grdAdvantEdge,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grdSpotControl,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'cmdBrowse
        '
        Me.cmdBrowse.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.cmdBrowse.FlatAppearance.BorderSize = 0
        Me.cmdBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdBrowse.Location = New System.Drawing.Point(314, 25)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(75, 25)
        Me.cmdBrowse.TabIndex = 0
        Me.cmdBrowse.Text = "Browse"
        Me.cmdBrowse.UseVisualStyleBackColor = true
        '
        'txtFile
        '
        Me.txtFile.Location = New System.Drawing.Point(12, 27)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.Size = New System.Drawing.Size(296, 22)
        Me.txtFile.TabIndex = 1
        '
        'txtFromDate
        '
        Me.txtFromDate.Location = New System.Drawing.Point(555, 27)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.Size = New System.Drawing.Size(99, 22)
        Me.txtFromDate.TabIndex = 2
        '
        'txtToDate
        '
        Me.txtToDate.Location = New System.Drawing.Point(677, 27)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.Size = New System.Drawing.Size(93, 22)
        Me.txtToDate.TabIndex = 3
        '
        'grdAdvantEdge
        '
        Me.grdAdvantEdge.AllowUserToAddRows = false
        Me.grdAdvantEdge.AllowUserToDeleteRows = false
        Me.grdAdvantEdge.AllowUserToResizeColumns = false
        Me.grdAdvantEdge.AllowUserToResizeRows = false
        Me.grdAdvantEdge.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdAdvantEdge.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAdvantEdge.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Date, Me.col_Time, Me.col_Channel, Me.col_Prolgram, Me.col_Spot, Me.col_Advertiser, Me.col_Duration, Me.col_FilmCode})
        Me.grdAdvantEdge.Location = New System.Drawing.Point(12, 321)
        Me.grdAdvantEdge.Name = "grdAdvantEdge"
        Me.grdAdvantEdge.RowHeadersVisible = false
        Me.grdAdvantEdge.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdAdvantEdge.Size = New System.Drawing.Size(763, 232)
        Me.grdAdvantEdge.TabIndex = 4
        '
        'col_Date
        '
        Me.col_Date.HeaderText = "Date"
        Me.col_Date.Name = "col_Date"
        Me.col_Date.Width = 80
        '
        'col_Time
        '
        Me.col_Time.HeaderText = "Time"
        Me.col_Time.Name = "col_Time"
        Me.col_Time.Width = 80
        '
        'col_Channel
        '
        Me.col_Channel.HeaderText = "Channel"
        Me.col_Channel.Name = "col_Channel"
        '
        'col_Prolgram
        '
        Me.col_Prolgram.HeaderText = "Program"
        Me.col_Prolgram.Name = "col_Prolgram"
        '
        'col_Spot
        '
        Me.col_Spot.HeaderText = "Spot"
        Me.col_Spot.Name = "col_Spot"
        '
        'col_Advertiser
        '
        Me.col_Advertiser.HeaderText = "Advertiser"
        Me.col_Advertiser.Name = "col_Advertiser"
        '
        'col_Duration
        '
        Me.col_Duration.HeaderText = "Duration"
        Me.col_Duration.Name = "col_Duration"
        '
        'col_FilmCode
        '
        Me.col_FilmCode.HeaderText = "FilmCode"
        Me.col_FilmCode.Name = "col_FilmCode"
        '
        'grdSpotControl
        '
        Me.grdSpotControl.AllowUserToAddRows = false
        Me.grdSpotControl.AllowUserToDeleteRows = false
        Me.grdSpotControl.AllowUserToResizeColumns = false
        Me.grdSpotControl.AllowUserToResizeRows = false
        Me.grdSpotControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdSpotControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSpotControl.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDate, Me.colTime, Me.colChannel, Me.colProgram, Me.colReason, Me.colAdvertiser, Me.colSpot, Me.colDuration})
        Me.grdSpotControl.Location = New System.Drawing.Point(12, 56)
        Me.grdSpotControl.Name = "grdSpotControl"
        Me.grdSpotControl.RowHeadersVisible = false
        Me.grdSpotControl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSpotControl.Size = New System.Drawing.Size(763, 258)
        Me.grdSpotControl.TabIndex = 5
        '
        'colDate
        '
        Me.colDate.HeaderText = "Date"
        Me.colDate.Name = "colDate"
        Me.colDate.Width = 80
        '
        'colTime
        '
        Me.colTime.HeaderText = "Time"
        Me.colTime.Name = "colTime"
        Me.colTime.Width = 80
        '
        'colChannel
        '
        Me.colChannel.HeaderText = "Channel"
        Me.colChannel.Name = "colChannel"
        '
        'colProgram
        '
        Me.colProgram.HeaderText = "Program"
        Me.colProgram.Name = "colProgram"
        '
        'colReason
        '
        Me.colReason.HeaderText = "Reason"
        Me.colReason.Name = "colReason"
        '
        'colAdvertiser
        '
        Me.colAdvertiser.HeaderText = "Advertiser"
        Me.colAdvertiser.Name = "colAdvertiser"
        '
        'colSpot
        '
        Me.colSpot.HeaderText = "Spot"
        Me.colSpot.Name = "colSpot"
        '
        'colDuration
        '
        Me.colDuration.HeaderText = "Duration"
        Me.colDuration.Name = "colDuration"
        '
        'cmdMatch
        '
        Me.cmdMatch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdMatch.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.cmdMatch.FlatAppearance.BorderSize = 0
        Me.cmdMatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdMatch.Image = Global.clTrinity.My.Resources.Resources.settings_2_20x20
        Me.cmdMatch.Location = New System.Drawing.Point(781, 56)
        Me.cmdMatch.Name = "cmdMatch"
        Me.cmdMatch.Size = New System.Drawing.Size(26, 25)
        Me.cmdMatch.TabIndex = 6
        Me.cmdMatch.UseVisualStyleBackColor = true
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "File:"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(552, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Dates"
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(660, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(11, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "-"
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Location = New System.Drawing.Point(700, 559)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
        Me.cmdCancel.TabIndex = 10
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = true
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdSave.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.cmdSave.FlatAppearance.BorderSize = 0
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Location = New System.Drawing.Point(619, 559)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 25)
        Me.cmdSave.TabIndex = 11
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = true
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Date"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 80
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Time"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 80
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Channel"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Program"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Spot"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Duration"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "FilmCode"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "Date"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Width = 80
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "Time"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.Width = 80
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "Channel"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Width = 80
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Program"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.HeaderText = "Reason"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.HeaderText = "Spot"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.HeaderText = "Duration"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.HeaderText = "Spot"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.HeaderText = "Duration"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        '
        'cmdClear
        '
        Me.cmdClear.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.cmdClear.FlatAppearance.BorderSize = 0
        Me.cmdClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClear.Location = New System.Drawing.Point(395, 25)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(75, 25)
        Me.cmdClear.TabIndex = 12
        Me.cmdClear.Text = "Clear list"
        Me.cmdClear.UseVisualStyleBackColor = true
        '
        'frmSpotControlImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(819, 591)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdMatch)
        Me.Controls.Add(Me.grdSpotControl)
        Me.Controls.Add(Me.grdAdvantEdge)
        Me.Controls.Add(Me.txtToDate)
        Me.Controls.Add(Me.txtFromDate)
        Me.Controls.Add(Me.txtFile)
        Me.Controls.Add(Me.cmdBrowse)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmSpotControlImport"
        Me.Text = "Import Spot control file"
        CType(Me.grdAdvantEdge,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdSpotControl,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents txtFromDate As System.Windows.Forms.TextBox
    Friend WithEvents txtToDate As System.Windows.Forms.TextBox
    Friend WithEvents grdAdvantEdge As System.Windows.Forms.DataGridView
    Friend WithEvents grdSpotControl As System.Windows.Forms.DataGridView
    Friend WithEvents cmdMatch As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents col_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Channel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Prolgram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Spot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Advertiser As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Duration As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_FilmCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChannel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProgram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colReason As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAdvertiser As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSpot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDuration As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdClear As System.Windows.Forms.Button
End Class
