<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDelivery
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDelivery))
        Me.grdDelivery = New System.Windows.Forms.DataGridView()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.cmdExcel = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChannel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlannedTRP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCPP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCPP30 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colConfirmedTRP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colActualTRP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDiff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.grdDelivery,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'grdDelivery
        '
        Me.grdDelivery.AllowUserToAddRows = false
        Me.grdDelivery.AllowUserToDeleteRows = false
        Me.grdDelivery.AllowUserToResizeColumns = false
        Me.grdDelivery.AllowUserToResizeRows = false
        Me.grdDelivery.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdDelivery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDelivery.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colChannel, Me.colPlannedTRP, Me.colCPP, Me.colCPP30, Me.colConfirmedTRP, Me.colActualTRP, Me.colCost, Me.colValue, Me.colDiff})
        Me.grdDelivery.Location = New System.Drawing.Point(12, 50)
        Me.grdDelivery.Name = "grdDelivery"
        Me.grdDelivery.ReadOnly = true
        Me.grdDelivery.RowHeadersVisible = false
        Me.grdDelivery.Size = New System.Drawing.Size(613, 191)
        Me.grdDelivery.TabIndex = 1
        Me.grdDelivery.VirtualMode = true
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.delivery_2
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = false
        '
        'cmdExcel
        '
        Me.cmdExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdExcel.Image = Global.clTrinity.My.Resources.Resources.excel_2
        Me.cmdExcel.Location = New System.Drawing.Point(601, 22)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(24, 22)
        Me.cmdExcel.TabIndex = 3
        Me.cmdExcel.UseVisualStyleBackColor = true
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Channel"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = true
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Booked TRP 30"""
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = true
        Me.DataGridViewTextBoxColumn2.Width = 75
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "CPP"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = true
        Me.DataGridViewTextBoxColumn3.Width = 65
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "CPP 30"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = true
        Me.DataGridViewTextBoxColumn4.Width = 65
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Confirmed TRP 30"""
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = true
        Me.DataGridViewTextBoxColumn5.Visible = false
        Me.DataGridViewTextBoxColumn5.Width = 75
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Actual TRP 30"""
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = true
        Me.DataGridViewTextBoxColumn6.Width = 75
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "Cost"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = true
        Me.DataGridViewTextBoxColumn7.Width = 75
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "Value"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = true
        Me.DataGridViewTextBoxColumn8.Width = 75
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "Diff"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = true
        Me.DataGridViewTextBoxColumn9.Width = 75
        '
        'colChannel
        '
        Me.colChannel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colChannel.HeaderText = "Channel"
        Me.colChannel.Name = "colChannel"
        Me.colChannel.ReadOnly = true
        '
        'colPlannedTRP
        '
        Me.colPlannedTRP.HeaderText = "Booked TRP 30"""
        Me.colPlannedTRP.Name = "colPlannedTRP"
        Me.colPlannedTRP.ReadOnly = true
        Me.colPlannedTRP.Width = 75
        '
        'colCPP
        '
        Me.colCPP.HeaderText = "CPP"
        Me.colCPP.Name = "colCPP"
        Me.colCPP.ReadOnly = true
        Me.colCPP.Width = 65
        '
        'colCPP30
        '
        Me.colCPP30.HeaderText = "CPP 30"
        Me.colCPP30.Name = "colCPP30"
        Me.colCPP30.ReadOnly = true
        Me.colCPP30.Width = 65
        '
        'colConfirmedTRP
        '
        Me.colConfirmedTRP.HeaderText = "Confirmed TRP 30"""
        Me.colConfirmedTRP.Name = "colConfirmedTRP"
        Me.colConfirmedTRP.ReadOnly = true
        Me.colConfirmedTRP.Visible = false
        Me.colConfirmedTRP.Width = 75
        '
        'colActualTRP
        '
        Me.colActualTRP.HeaderText = "Actual TRP 30"""
        Me.colActualTRP.Name = "colActualTRP"
        Me.colActualTRP.ReadOnly = true
        Me.colActualTRP.Width = 75
        '
        'colCost
        '
        Me.colCost.HeaderText = "Cost"
        Me.colCost.Name = "colCost"
        Me.colCost.ReadOnly = true
        Me.colCost.Width = 75
        '
        'colValue
        '
        Me.colValue.HeaderText = "Value"
        Me.colValue.Name = "colValue"
        Me.colValue.ReadOnly = true
        Me.colValue.Width = 75
        '
        'colDiff
        '
        Me.colDiff.HeaderText = "Diff"
        Me.colDiff.Name = "colDiff"
        Me.colDiff.ReadOnly = true
        Me.colDiff.Width = 75
        '
        'frmDelivery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(637, 253)
        Me.Controls.Add(Me.cmdExcel)
        Me.Controls.Add(Me.grdDelivery)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmDelivery"
        Me.Text = "Delivery monitor"
        CType(Me.grdDelivery,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents grdDelivery As System.Windows.Forms.DataGridView
    Friend WithEvents colChannel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlannedTRP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCPP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCPP30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colConfirmedTRP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colActualTRP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDiff As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdExcel As System.Windows.Forms.Button
End Class
