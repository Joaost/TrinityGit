<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAllBookings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            Database.UnRegisterBookingEventConsumer(Me)
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
        Me.components = New System.ComponentModel.Container
        Me.grdBookings = New System.Windows.Forms.DataGridView
        Me.colDates = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDays = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colClient = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colEvent = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colSalesman = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colStore = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colCity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colRequestedProvider = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colProvider = New Balthazar.ExtendedComboboxColumn
        Me.colConfirmed = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.colQDays = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colInvoiced = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.lblDates = New System.Windows.Forms.Label
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ExtendedComboboxColumn1 = New Balthazar.ExtendedComboboxColumn
        Me.cmbResponsible = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkNotConfirmed = New System.Windows.Forms.CheckBox
        Me.chkNotInvoiced = New System.Windows.Forms.CheckBox
        Me.cmdExcel = New System.Windows.Forms.Button
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.tmrCheckForChanges = New System.Windows.Forms.Timer(Me.components)
        CType(Me.grdBookings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdBookings
        '
        Me.grdBookings.AllowUserToAddRows = False
        Me.grdBookings.AllowUserToDeleteRows = False
        Me.grdBookings.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdBookings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDates, Me.colDays, Me.colClient, Me.colEvent, Me.colSalesman, Me.colStore, Me.colCity, Me.colRequestedProvider, Me.colProvider, Me.colConfirmed, Me.colQDays, Me.colInvoiced})
        Me.grdBookings.Location = New System.Drawing.Point(12, 34)
        Me.grdBookings.Name = "grdBookings"
        Me.grdBookings.RowHeadersVisible = False
        Me.grdBookings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdBookings.Size = New System.Drawing.Size(974, 578)
        Me.grdBookings.TabIndex = 1
        Me.grdBookings.VirtualMode = True
        '
        'colDates
        '
        Me.colDates.HeaderText = "Dates"
        Me.colDates.Name = "colDates"
        Me.colDates.ReadOnly = True
        Me.colDates.Width = 120
        '
        'colDays
        '
        Me.colDays.HeaderText = "Days"
        Me.colDays.Name = "colDays"
        Me.colDays.ReadOnly = True
        Me.colDays.Width = 40
        '
        'colClient
        '
        Me.colClient.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colClient.FillWeight = 30.0!
        Me.colClient.HeaderText = "Client"
        Me.colClient.Name = "colClient"
        Me.colClient.ReadOnly = True
        '
        'colEvent
        '
        Me.colEvent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colEvent.FillWeight = 30.0!
        Me.colEvent.HeaderText = "Event"
        Me.colEvent.Name = "colEvent"
        Me.colEvent.ReadOnly = True
        '
        'colSalesman
        '
        Me.colSalesman.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colSalesman.FillWeight = 40.0!
        Me.colSalesman.HeaderText = "Sales person"
        Me.colSalesman.MinimumWidth = 50
        Me.colSalesman.Name = "colSalesman"
        Me.colSalesman.ReadOnly = True
        '
        'colStore
        '
        Me.colStore.HeaderText = "Store"
        Me.colStore.Name = "colStore"
        Me.colStore.ReadOnly = True
        '
        'colCity
        '
        Me.colCity.HeaderText = "City"
        Me.colCity.Name = "colCity"
        Me.colCity.ReadOnly = True
        '
        'colRequestedProvider
        '
        Me.colRequestedProvider.HeaderText = "Req.Provider"
        Me.colRequestedProvider.Name = "colRequestedProvider"
        Me.colRequestedProvider.ReadOnly = True
        '
        'colProvider
        '
        Me.colProvider.HeaderText = "Provider"
        Me.colProvider.Name = "colProvider"
        Me.colProvider.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'colConfirmed
        '
        Me.colConfirmed.HeaderText = "Confirmed"
        Me.colConfirmed.Items.AddRange(New Object() {"", "Confirmed", "Rejected"})
        Me.colConfirmed.Name = "colConfirmed"
        Me.colConfirmed.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colConfirmed.Width = 60
        '
        'colQDays
        '
        Me.colQDays.HeaderText = "Q-days"
        Me.colQDays.Name = "colQDays"
        Me.colQDays.ReadOnly = True
        Me.colQDays.Width = 45
        '
        'colInvoiced
        '
        Me.colInvoiced.HeaderText = "Invoiced"
        Me.colInvoiced.Name = "colInvoiced"
        Me.colInvoiced.Width = 55
        '
        'lblDates
        '
        Me.lblDates.AutoSize = True
        Me.lblDates.BackColor = System.Drawing.Color.White
        Me.lblDates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDates.Location = New System.Drawing.Point(24, 89)
        Me.lblDates.Name = "lblDates"
        Me.lblDates.Size = New System.Drawing.Size(41, 16)
        Me.lblDates.TabIndex = 2
        Me.lblDates.Text = "Label4"
        Me.lblDates.Visible = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Dates"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 120
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Days"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 40
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.FillWeight = 30.0!
        Me.DataGridViewTextBoxColumn3.HeaderText = "Client"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn4.FillWeight = 30.0!
        Me.DataGridViewTextBoxColumn4.HeaderText = "Event"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn5.FillWeight = 40.0!
        Me.DataGridViewTextBoxColumn5.HeaderText = "Sales person"
        Me.DataGridViewTextBoxColumn5.MinimumWidth = 50
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Store"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "City"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "Req.Provider"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'ExtendedComboboxColumn1
        '
        Me.ExtendedComboboxColumn1.HeaderText = "Provider"
        Me.ExtendedComboboxColumn1.Name = "ExtendedComboboxColumn1"
        Me.ExtendedComboboxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'cmbResponsible
        '
        Me.cmbResponsible.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbResponsible.FormattingEnabled = True
        Me.cmbResponsible.Location = New System.Drawing.Point(83, 6)
        Me.cmbResponsible.Name = "cmbResponsible"
        Me.cmbResponsible.Size = New System.Drawing.Size(121, 22)
        Me.cmbResponsible.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 14)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Responsible"
        '
        'chkNotConfirmed
        '
        Me.chkNotConfirmed.AutoSize = True
        Me.chkNotConfirmed.Location = New System.Drawing.Point(210, 9)
        Me.chkNotConfirmed.Name = "chkNotConfirmed"
        Me.chkNotConfirmed.Size = New System.Drawing.Size(228, 18)
        Me.chkNotConfirmed.TabIndex = 5
        Me.chkNotConfirmed.Text = "Only show bookings pending confirmation"
        Me.chkNotConfirmed.UseVisualStyleBackColor = True
        '
        'chkNotInvoiced
        '
        Me.chkNotInvoiced.AutoSize = True
        Me.chkNotInvoiced.Location = New System.Drawing.Point(444, 8)
        Me.chkNotInvoiced.Name = "chkNotInvoiced"
        Me.chkNotInvoiced.Size = New System.Drawing.Size(140, 18)
        Me.chkNotInvoiced.TabIndex = 6
        Me.chkNotInvoiced.Text = "Only show not invoiced"
        Me.chkNotInvoiced.UseVisualStyleBackColor = True
        '
        'cmdExcel
        '
        Me.cmdExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExcel.Image = Global.Balthazar.My.Resources.Resources.excel
        Me.cmdExcel.Location = New System.Drawing.Point(992, 34)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(24, 24)
        Me.cmdExcel.TabIndex = 14
        Me.cmdExcel.UseVisualStyleBackColor = True
        '
        'cmdDelete
        '
        Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDelete.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdDelete.Location = New System.Drawing.Point(992, 64)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(24, 24)
        Me.cmdDelete.TabIndex = 15
        Me.cmdDelete.UseVisualStyleBackColor = True
        '
        'tmrCheckForChanges
        '
        Me.tmrCheckForChanges.Enabled = True
        Me.tmrCheckForChanges.Interval = 2000
        '
        'frmAllBookings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 626)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdExcel)
        Me.Controls.Add(Me.chkNotInvoiced)
        Me.Controls.Add(Me.chkNotConfirmed)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbResponsible)
        Me.Controls.Add(Me.lblDates)
        Me.Controls.Add(Me.grdBookings)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmAllBookings"
        Me.Text = "All Bookings"
        CType(Me.grdBookings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdBookings As System.Windows.Forms.DataGridView
    Friend WithEvents lblDates As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExtendedComboboxColumn1 As Balthazar.ExtendedComboboxColumn
    Friend WithEvents cmbResponsible As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkNotConfirmed As System.Windows.Forms.CheckBox
    Friend WithEvents chkNotInvoiced As System.Windows.Forms.CheckBox
    Friend WithEvents cmdExcel As System.Windows.Forms.Button
    Friend WithEvents colDates As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDays As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colClient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEvent As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSalesman As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStore As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRequestedProvider As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProvider As Balthazar.ExtendedComboboxColumn
    Friend WithEvents colConfirmed As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents colQDays As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colInvoiced As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents tmrCheckForChanges As System.Windows.Forms.Timer
End Class
