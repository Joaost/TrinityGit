<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDocuments
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
        Me.grdDocuments = New System.Windows.Forms.DataGridView
        Me.cmdView = New System.Windows.Forms.Button
        Me.cmdRemove = New System.Windows.Forms.Button
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.cmdReplace = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDateDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colFilename = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdDocuments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdDocuments
        '
        Me.grdDocuments.AllowUserToAddRows = False
        Me.grdDocuments.AllowUserToDeleteRows = False
        Me.grdDocuments.AllowUserToResizeColumns = False
        Me.grdDocuments.AllowUserToResizeRows = False
        Me.grdDocuments.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDocuments.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colDateDescription, Me.colFilename})
        Me.grdDocuments.Location = New System.Drawing.Point(12, 12)
        Me.grdDocuments.Name = "grdDocuments"
        Me.grdDocuments.RowHeadersVisible = False
        Me.grdDocuments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdDocuments.Size = New System.Drawing.Size(810, 517)
        Me.grdDocuments.TabIndex = 1
        Me.grdDocuments.VirtualMode = True
        '
        'cmdView
        '
        Me.cmdView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdView.Image = Global.Balthazar.My.Resources.Resources.document_view
        Me.cmdView.Location = New System.Drawing.Point(828, 72)
        Me.cmdView.Name = "cmdView"
        Me.cmdView.Size = New System.Drawing.Size(24, 24)
        Me.cmdView.TabIndex = 14
        Me.cmdView.UseVisualStyleBackColor = True
        '
        'cmdRemove
        '
        Me.cmdRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemove.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemove.Location = New System.Drawing.Point(828, 42)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemove.TabIndex = 13
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAdd.Location = New System.Drawing.Point(828, 12)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(24, 24)
        Me.cmdAdd.TabIndex = 12
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'cmdReplace
        '
        Me.cmdReplace.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdReplace.Image = Global.Balthazar.My.Resources.Resources.document_exchange
        Me.cmdReplace.Location = New System.Drawing.Point(828, 102)
        Me.cmdReplace.Name = "cmdReplace"
        Me.cmdReplace.Size = New System.Drawing.Size(24, 24)
        Me.cmdReplace.TabIndex = 14
        Me.cmdReplace.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.FillWeight = 30.0!
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.FillWeight = 70.0!
        Me.DataGridViewTextBoxColumn2.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colName.FillWeight = 30.0!
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        '
        'colDateDescription
        '
        Me.colDateDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colDateDescription.FillWeight = 70.0!
        Me.colDateDescription.HeaderText = "Description"
        Me.colDateDescription.Name = "colDateDescription"
        '
        'colFilename
        '
        Me.colFilename.HeaderText = "Filename"
        Me.colFilename.Name = "colFilename"
        Me.colFilename.ReadOnly = True
        Me.colFilename.Width = 125
        '
        'frmDocuments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(864, 541)
        Me.Controls.Add(Me.cmdReplace)
        Me.Controls.Add(Me.cmdView)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.grdDocuments)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmDocuments"
        Me.Text = "Documents"
        CType(Me.grdDocuments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdDocuments As System.Windows.Forms.DataGridView
    Friend WithEvents cmdView As System.Windows.Forms.Button
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdReplace As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDateDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFilename As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
