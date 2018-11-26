<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShowMarathonInsertions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmShowMarathonInsertions))
        Me.grdInsertions = New System.Windows.Forms.DataGridView()
        Me.colOrderNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMedia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBookingType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.grdInsertions,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'grdInsertions
        '
        Me.grdInsertions.AllowUserToAddRows = false
        Me.grdInsertions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdInsertions.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colOrderNumber, Me.colMedia, Me.colBookingType, Me.colNet})
        Me.grdInsertions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdInsertions.Location = New System.Drawing.Point(0, 0)
        Me.grdInsertions.Name = "grdInsertions"
        Me.grdInsertions.RowHeadersVisible = false
        Me.grdInsertions.Size = New System.Drawing.Size(407, 131)
        Me.grdInsertions.TabIndex = 0
        '
        'colOrderNumber
        '
        Me.colOrderNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.colOrderNumber.HeaderText = "Order Number"
        Me.colOrderNumber.Name = "colOrderNumber"
        Me.colOrderNumber.Width = 97
        '
        'colMedia
        '
        Me.colMedia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colMedia.HeaderText = "Media"
        Me.colMedia.Name = "colMedia"
        '
        'colBookingType
        '
        Me.colBookingType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.colBookingType.HeaderText = "Booking Type"
        Me.colBookingType.Name = "colBookingType"
        Me.colBookingType.Width = 93
        '
        'colNet
        '
        Me.colNet.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.colNet.HeaderText = "Net"
        Me.colNet.Name = "colNet"
        Me.colNet.Width = 50
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn1.HeaderText = "Order Number"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.HeaderText = "Media"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn3.HeaderText = "Booking Type"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn4.HeaderText = "Net"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'frmShowMarathonInsertions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = true
        Me.ClientSize = New System.Drawing.Size(407, 131)
        Me.Controls.Add(Me.grdInsertions)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmShowMarathonInsertions"
        Me.Text = "Marathon insertions"
        CType(Me.grdInsertions,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents grdInsertions As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colOrderNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMedia As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBookingType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNet As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
