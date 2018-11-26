<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPreviewConfirmation
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPreviewConfirmation))
        Me.grdConfSpots = New System.Windows.Forms.DataGridView()
        Me.colBroadcastDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colWeek = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBroadcastTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colProgramName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colGrossPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNetPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFilmCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFilmLength = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEstimatedTRP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSurcharges = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblAmountOfSpots = New System.Windows.Forms.Label()
        Me.lblSpottsText = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblVersionNm = New System.Windows.Forms.Label()
        Me.lblLastEdited = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.grdConfSpots,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'grdConfSpots
        '
        Me.grdConfSpots.AllowUserToAddRows = false
        Me.grdConfSpots.AllowUserToDeleteRows = false
        Me.grdConfSpots.AllowUserToResizeColumns = false
        Me.grdConfSpots.AllowUserToResizeRows = false
        Me.grdConfSpots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdConfSpots.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colBroadcastDate, Me.colWeek, Me.colBroadcastTime, Me.colProgramName, Me.colGrossPrice, Me.colNetPrice, Me.colFilmCode, Me.colFilmLength, Me.colEstimatedTRP, Me.colSurcharges})
        Me.grdConfSpots.GridColor = System.Drawing.SystemColors.ControlDarkDark
        Me.grdConfSpots.Location = New System.Drawing.Point(0, 26)
        Me.grdConfSpots.Name = "grdConfSpots"
        Me.grdConfSpots.RowHeadersVisible = false
        Me.grdConfSpots.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdConfSpots.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdConfSpots.Size = New System.Drawing.Size(1035, 426)
        Me.grdConfSpots.TabIndex = 0
        Me.grdConfSpots.VirtualMode = true
        '
        'colBroadcastDate
        '
        Me.colBroadcastDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colBroadcastDate.HeaderText = "Broadcast date"
        Me.colBroadcastDate.Name = "colBroadcastDate"
        Me.colBroadcastDate.ReadOnly = true
        '
        'colWeek
        '
        Me.colWeek.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colWeek.HeaderText = "Week"
        Me.colWeek.Name = "colWeek"
        Me.colWeek.ReadOnly = true
        '
        'colBroadcastTime
        '
        Me.colBroadcastTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colBroadcastTime.HeaderText = "Broadcast time"
        Me.colBroadcastTime.Name = "colBroadcastTime"
        Me.colBroadcastTime.ReadOnly = true
        '
        'colProgramName
        '
        Me.colProgramName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colProgramName.FillWeight = 150!
        Me.colProgramName.HeaderText = "Prog. Name"
        Me.colProgramName.Name = "colProgramName"
        Me.colProgramName.ReadOnly = true
        '
        'colGrossPrice
        '
        Me.colGrossPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle1.Format = "#,###"
        Me.colGrossPrice.DefaultCellStyle = DataGridViewCellStyle1
        Me.colGrossPrice.HeaderText = "Gross price"
        Me.colGrossPrice.Name = "colGrossPrice"
        Me.colGrossPrice.ReadOnly = true
        '
        'colNetPrice
        '
        Me.colNetPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle2.Format = "#,###"
        Me.colNetPrice.DefaultCellStyle = DataGridViewCellStyle2
        Me.colNetPrice.HeaderText = "Net price"
        Me.colNetPrice.Name = "colNetPrice"
        Me.colNetPrice.ReadOnly = true
        Me.colNetPrice.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'colFilmCode
        '
        Me.colFilmCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colFilmCode.HeaderText = "Film code"
        Me.colFilmCode.Name = "colFilmCode"
        Me.colFilmCode.ReadOnly = true
        '
        'colFilmLength
        '
        Me.colFilmLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colFilmLength.HeaderText = "Film length"
        Me.colFilmLength.Name = "colFilmLength"
        Me.colFilmLength.ReadOnly = true
        '
        'colEstimatedTRP
        '
        Me.colEstimatedTRP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colEstimatedTRP.HeaderText = "Estimated TRP"
        Me.colEstimatedTRP.Name = "colEstimatedTRP"
        Me.colEstimatedTRP.ReadOnly = true
        '
        'colSurcharges
        '
        Me.colSurcharges.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colSurcharges.HeaderText = "Surcharges"
        Me.colSurcharges.Name = "colSurcharges"
        Me.colSurcharges.ReadOnly = true
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1035, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = true
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.HeaderText = "End date"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = true
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.FillWeight = 150!
        Me.DataGridViewTextBoxColumn3.HeaderText = "Start date"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = true
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn4.HeaderText = "Net budget"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = true
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn5.HeaderText = "Latest changed date"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = true
        Me.DataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.HeaderText = "Film code"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = true
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn7.HeaderText = "Film length"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = true
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn8.HeaderText = "Estimated TRP"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = true
        '
        'lblAmountOfSpots
        '
        Me.lblAmountOfSpots.AutoSize = true
        Me.lblAmountOfSpots.BackColor = System.Drawing.SystemColors.ControlLight
        Me.lblAmountOfSpots.Location = New System.Drawing.Point(931, 7)
        Me.lblAmountOfSpots.Name = "lblAmountOfSpots"
        Me.lblAmountOfSpots.Size = New System.Drawing.Size(40, 13)
        Me.lblAmountOfSpots.TabIndex = 2
        Me.lblAmountOfSpots.Text = "Label1"
        '
        'lblSpottsText
        '
        Me.lblSpottsText.AutoSize = true
        Me.lblSpottsText.BackColor = System.Drawing.SystemColors.ControlLight
        Me.lblSpottsText.Location = New System.Drawing.Point(969, 7)
        Me.lblSpottsText.Name = "lblSpottsText"
        Me.lblSpottsText.Size = New System.Drawing.Size(39, 13)
        Me.lblSpottsText.TabIndex = 3
        Me.lblSpottsText.Text = "spotts"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label2.Location = New System.Drawing.Point(12, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Version:"
        '
        'lblVersionNm
        '
        Me.lblVersionNm.AutoSize = true
        Me.lblVersionNm.BackColor = System.Drawing.SystemColors.ControlLight
        Me.lblVersionNm.Location = New System.Drawing.Point(55, 7)
        Me.lblVersionNm.Name = "lblVersionNm"
        Me.lblVersionNm.Size = New System.Drawing.Size(82, 13)
        Me.lblVersionNm.TabIndex = 5
        Me.lblVersionNm.Text = "lblVersionNmb"
        '
        'lblLastEdited
        '
        Me.lblLastEdited.AutoSize = true
        Me.lblLastEdited.BackColor = System.Drawing.SystemColors.ControlLight
        Me.lblLastEdited.Location = New System.Drawing.Point(213, 7)
        Me.lblLastEdited.Name = "lblLastEdited"
        Me.lblLastEdited.Size = New System.Drawing.Size(53, 13)
        Me.lblLastEdited.TabIndex = 7
        Me.lblLastEdited.Text = "lblEdited"
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label4.Location = New System.Drawing.Point(155, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Last edited:"
        '
        'frmPreviewConfirmation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1035, 453)
        Me.Controls.Add(Me.lblLastEdited)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblVersionNm)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblSpottsText)
        Me.Controls.Add(Me.lblAmountOfSpots)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.grdConfSpots)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MaximumSize = New System.Drawing.Size(1051, 492)
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(1051, 492)
        Me.Name = "frmPreviewConfirmation"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Confirmations"
        CType(Me.grdConfSpots,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents grdConfSpots As Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents DataGridViewTextBoxColumn6 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblAmountOfSpots As Windows.Forms.Label
    Friend WithEvents lblSpottsText As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents lblVersionNm As Windows.Forms.Label
    Friend WithEvents lblLastEdited As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents colBroadcastDate As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colWeek As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBroadcastTime As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProgramName As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colGrossPrice As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNetPrice As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFilmCode As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFilmLength As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEstimatedTRP As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSurcharges As Windows.Forms.DataGridViewTextBoxColumn
End Class
