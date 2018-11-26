<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTranslateFromSpotlight
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.grdTranslate = New System.Windows.Forms.DataGridView()
        Me.colSpotlight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTrinity = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.TableLayoutPanel1.SuspendLayout
        CType(Me.grdTranslate,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40!))
        Me.TableLayoutPanel1.Controls.Add(Me.cmdOk, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.grdTranslate, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Font = New System.Drawing.Font("Segoe UI",  8.25!)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.87786!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.12214!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(284, 262)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.cmdOk, 2)
        Me.cmdOk.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmdOk.Location = New System.Drawing.Point(197, 227)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(84, 31)
        Me.cmdOk.TabIndex = 0
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = true
        '
        'grdTranslate
        '
        Me.grdTranslate.AllowUserToAddRows = false
        Me.grdTranslate.AllowUserToDeleteRows = false
        Me.grdTranslate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTranslate.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSpotlight, Me.colTrinity})
        Me.TableLayoutPanel1.SetColumnSpan(Me.grdTranslate, 3)
        Me.grdTranslate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdTranslate.Location = New System.Drawing.Point(3, 3)
        Me.grdTranslate.Name = "grdTranslate"
        Me.grdTranslate.RowHeadersVisible = false
        Me.grdTranslate.Size = New System.Drawing.Size(278, 218)
        Me.grdTranslate.TabIndex = 2
        Me.grdTranslate.VirtualMode = true
        '
        'colSpotlight
        '
        Me.colSpotlight.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.colSpotlight.DefaultCellStyle = DataGridViewCellStyle1
        Me.colSpotlight.HeaderText = "Spotlight"
        Me.colSpotlight.Name = "colSpotlight"
        Me.colSpotlight.ReadOnly = true
        Me.colSpotlight.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colSpotlight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colTrinity
        '
        Me.colTrinity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.colTrinity.DefaultCellStyle = DataGridViewCellStyle2
        Me.colTrinity.HeaderText = "Trinity"
        Me.colTrinity.Name = "colTrinity"
        Me.colTrinity.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'frmTranslateFromSpotlight
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmTranslateFromSpotlight"
        Me.Text = "Translate Spotlight -> Trinity"
        Me.TableLayoutPanel1.ResumeLayout(false)
        CType(Me.grdTranslate,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents grdTranslate As System.Windows.Forms.DataGridView
    Friend WithEvents colSpotlight As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTrinity As Windows.Forms.DataGridViewComboBoxColumn
End Class
