<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSpotControlMatch
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
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.grdSpotControl = New System.Windows.Forms.DataGridView
        Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colChannel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colProgram = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colReason = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colSpot = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDuration = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grdAdvantEdge = New System.Windows.Forms.DataGridView
        Me.col_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Time = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Channel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Prolgram = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Spot = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Duration = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colAdvertiser = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_FilmCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdSpotControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAdvantEdge, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(664, 394)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 15
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(745, 394)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 14
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'grdSpotControl
        '
        Me.grdSpotControl.AllowUserToAddRows = False
        Me.grdSpotControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSpotControl.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDate, Me.colTime, Me.colChannel, Me.colProgram, Me.colReason, Me.colSpot, Me.colDuration})
        Me.grdSpotControl.Location = New System.Drawing.Point(12, 12)
        Me.grdSpotControl.Name = "grdSpotControl"
        Me.grdSpotControl.RowHeadersVisible = False
        Me.grdSpotControl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSpotControl.Size = New System.Drawing.Size(808, 54)
        Me.grdSpotControl.TabIndex = 13
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
        'grdAdvantEdge
        '
        Me.grdAdvantEdge.AllowUserToAddRows = False
        Me.grdAdvantEdge.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAdvantEdge.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Date, Me.col_Time, Me.col_Channel, Me.col_Prolgram, Me.col_Spot, Me.col_Duration, Me.colAdvertiser, Me.col_FilmCode})
        Me.grdAdvantEdge.Location = New System.Drawing.Point(12, 72)
        Me.grdAdvantEdge.Name = "grdAdvantEdge"
        Me.grdAdvantEdge.RowHeadersVisible = False
        Me.grdAdvantEdge.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdAdvantEdge.Size = New System.Drawing.Size(808, 311)
        Me.grdAdvantEdge.TabIndex = 12
        '
        'col_Date
        '
        Me.col_Date.HeaderText = "Date"
        Me.col_Date.Name = "col_Date"
        '
        'col_Time
        '
        Me.col_Time.HeaderText = "Time"
        Me.col_Time.Name = "col_Time"
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
        'col_Duration
        '
        Me.col_Duration.HeaderText = "Duration"
        Me.col_Duration.Name = "col_Duration"
        '
        'colAdvertiser
        '
        Me.colAdvertiser.HeaderText = "Advertiser"
        Me.colAdvertiser.Name = "colAdvertiser"
        '
        'col_FilmCode
        '
        Me.col_FilmCode.HeaderText = "FilmCode"
        Me.col_FilmCode.Name = "col_FilmCode"
        '
        'frmSpotControlMatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(832, 429)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.grdSpotControl)
        Me.Controls.Add(Me.grdAdvantEdge)
        Me.Name = "frmSpotControlMatch"
        Me.Text = "frmSpotControlMatch"
        CType(Me.grdSpotControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAdvantEdge, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents grdSpotControl As System.Windows.Forms.DataGridView
    Friend WithEvents colDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChannel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProgram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colReason As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSpot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDuration As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdAdvantEdge As System.Windows.Forms.DataGridView
    Friend WithEvents col_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Channel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Prolgram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Spot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Duration As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAdvertiser As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_FilmCode As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
