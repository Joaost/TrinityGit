<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRFEstimationBreaks
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRFEstimationBreaks))
        Me.grdBreaks = New System.Windows.Forms.DataGridView()
        Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colProgramme = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTRP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.grdBreaks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdBreaks
        '
        Me.grdBreaks.AllowUserToAddRows = False
        Me.grdBreaks.AllowUserToDeleteRows = False
        Me.grdBreaks.AllowUserToResizeColumns = False
        Me.grdBreaks.AllowUserToResizeRows = False
        Me.grdBreaks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdBreaks.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDate, Me.colTime, Me.colProgramme, Me.colTRP})
        Me.grdBreaks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdBreaks.Location = New System.Drawing.Point(0, 0)
        Me.grdBreaks.Name = "grdBreaks"
        Me.grdBreaks.ReadOnly = True
        Me.grdBreaks.RowHeadersVisible = False
        Me.grdBreaks.Size = New System.Drawing.Size(660, 196)
        Me.grdBreaks.TabIndex = 0
        Me.grdBreaks.VirtualMode = True
        '
        'colDate
        '
        Me.colDate.HeaderText = "Date"
        Me.colDate.Name = "colDate"
        Me.colDate.ReadOnly = True
        '
        'colTime
        '
        Me.colTime.HeaderText = "Time"
        Me.colTime.Name = "colTime"
        Me.colTime.ReadOnly = True
        '
        'colProgramme
        '
        Me.colProgramme.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colProgramme.HeaderText = "Programme"
        Me.colProgramme.Name = "colProgramme"
        Me.colProgramme.ReadOnly = True
        '
        'colTRP
        '
        Me.colTRP.HeaderText = "TRP"
        Me.colTRP.Name = "colTRP"
        Me.colTRP.ReadOnly = True
        '
        'frmRFEstimationBreaks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(660, 196)
        Me.Controls.Add(Me.grdBreaks)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmRFEstimationBreaks"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Reach estimation breaks"
        CType(Me.grdBreaks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdBreaks As System.Windows.Forms.DataGridView
    Friend WithEvents colDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProgramme As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTRP As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
