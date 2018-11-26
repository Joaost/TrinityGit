<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSpotControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSpotControl))
        Me.cmdBrowse = New System.Windows.Forms.Button
        Me.txtFile = New System.Windows.Forms.TextBox
        Me.txtFromDate = New System.Windows.Forms.TextBox
        Me.txtToDate = New System.Windows.Forms.TextBox
        Me.grdAdvantEdge = New System.Windows.Forms.DataGridView
        Me.col_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Time = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Channel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Prolgram = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Spot = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Duration = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_FilmCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grdSpotControl = New System.Windows.Forms.DataGridView
        Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colChannel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colProgram = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colReason = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colSpot = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDuration = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmdMatch = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        CType(Me.grdAdvantEdge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSpotControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Location = New System.Drawing.Point(314, 32)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(75, 23)
        Me.cmdBrowse.TabIndex = 0
        Me.cmdBrowse.Text = "Browse"
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'txtFile
        '
        Me.txtFile.Location = New System.Drawing.Point(12, 34)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.Size = New System.Drawing.Size(296, 20)
        Me.txtFile.TabIndex = 1
        '
        'txtFromDate
        '
        Me.txtFromDate.Location = New System.Drawing.Point(406, 34)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.Size = New System.Drawing.Size(99, 20)
        Me.txtFromDate.TabIndex = 2
        '
        'txtToDate
        '
        Me.txtToDate.Location = New System.Drawing.Point(521, 34)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.Size = New System.Drawing.Size(93, 20)
        Me.txtToDate.TabIndex = 3
        '
        'grdAdvantEdge
        '
        Me.grdAdvantEdge.AllowUserToAddRows = False
        Me.grdAdvantEdge.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAdvantEdge.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAdvantEdge.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Date, Me.col_Time, Me.col_Channel, Me.col_Prolgram, Me.col_Spot, Me.col_Duration, Me.col_FilmCode})
        Me.grdAdvantEdge.Location = New System.Drawing.Point(12, 298)
        Me.grdAdvantEdge.Name = "grdAdvantEdge"
        Me.grdAdvantEdge.RowHeadersVisible = False
        Me.grdAdvantEdge.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdAdvantEdge.Size = New System.Drawing.Size(763, 215)
        Me.grdAdvantEdge.TabIndex = 4
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
        'col_FilmCode
        '
        Me.col_FilmCode.HeaderText = "FilmCode"
        Me.col_FilmCode.Name = "col_FilmCode"
        '
        'grdSpotControl
        '
        Me.grdSpotControl.AllowUserToAddRows = False
        Me.grdSpotControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSpotControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSpotControl.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDate, Me.colTime, Me.colChannel, Me.colProgram, Me.colReason, Me.colSpot, Me.colDuration})
        Me.grdSpotControl.Location = New System.Drawing.Point(12, 70)
        Me.grdSpotControl.Name = "grdSpotControl"
        Me.grdSpotControl.RowHeadersVisible = False
        Me.grdSpotControl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSpotControl.Size = New System.Drawing.Size(763, 222)
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
        Me.cmdMatch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdMatch.Image = CType(resources.GetObject("cmdMatch.Image"), System.Drawing.Image)
        Me.cmdMatch.Location = New System.Drawing.Point(781, 70)
        Me.cmdMatch.Name = "cmdMatch"
        Me.cmdMatch.Size = New System.Drawing.Size(26, 23)
        Me.cmdMatch.TabIndex = 6
        Me.cmdMatch.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "File:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(412, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Dates"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(505, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "-"
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.Location = New System.Drawing.Point(700, 519)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 10
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.Location = New System.Drawing.Point(619, 519)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 11
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'frmSpotControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(819, 549)
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
        Me.Name = "frmSpotControl"
        Me.Text = "frmSpotControl"
        CType(Me.grdAdvantEdge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSpotControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents colDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChannel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProgram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colReason As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSpot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDuration As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Channel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Prolgram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Spot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Duration As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_FilmCode As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
