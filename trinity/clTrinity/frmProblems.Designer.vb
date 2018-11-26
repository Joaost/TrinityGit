<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProblems
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProblems))
        Me.grdProblems = New System.Windows.Forms.DataGridView()
        Me.colSeverity = New System.Windows.Forms.DataGridViewImageColumn()
        Me.colProblem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSource = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colHelp = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.colFixit = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.lstImages = New System.Windows.Forms.ImageList(Me.components)
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.chkShowAll = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblHidden = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdSolveSelected = New System.Windows.Forms.Button()
        CType(Me.grdProblems,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'grdProblems
        '
        Me.grdProblems.AllowUserToAddRows = false
        Me.grdProblems.AllowUserToDeleteRows = false
        Me.grdProblems.AllowUserToResizeColumns = false
        Me.grdProblems.AllowUserToResizeRows = false
        Me.grdProblems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grdProblems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdProblems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSeverity, Me.colProblem, Me.colSource, Me.colHelp, Me.colFixit})
        Me.grdProblems.Location = New System.Drawing.Point(12, 50)
        Me.grdProblems.Name = "grdProblems"
        Me.grdProblems.ReadOnly = true
        Me.grdProblems.RowHeadersVisible = false
        Me.grdProblems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdProblems.Size = New System.Drawing.Size(684, 295)
        Me.grdProblems.TabIndex = 1
        Me.grdProblems.VirtualMode = true
        '
        'colSeverity
        '
        Me.colSeverity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colSeverity.HeaderText = ""
        Me.colSeverity.Name = "colSeverity"
        Me.colSeverity.ReadOnly = true
        Me.colSeverity.Width = 5
        '
        'colProblem
        '
        Me.colProblem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colProblem.HeaderText = "Issue"
        Me.colProblem.Name = "colProblem"
        Me.colProblem.ReadOnly = true
        '
        'colSource
        '
        Me.colSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colSource.HeaderText = "Where"
        Me.colSource.Name = "colSource"
        Me.colSource.ReadOnly = true
        Me.colSource.Width = 66
        '
        'colHelp
        '
        Me.colHelp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colHelp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.colHelp.HeaderText = ""
        Me.colHelp.Name = "colHelp"
        Me.colHelp.ReadOnly = true
        Me.colHelp.Width = 20
        '
        'colFixit
        '
        Me.colFixit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colFixit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.colFixit.HeaderText = "Fix it"
        Me.colFixit.Name = "colFixit"
        Me.colFixit.ReadOnly = true
        Me.colFixit.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colFixit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colFixit.Text = ""
        Me.colFixit.Width = 40
        '
        'lstImages
        '
        Me.lstImages.ImageStream = CType(resources.GetObject("lstImages.ImageStream"),System.Windows.Forms.ImageListStreamer)
        Me.lstImages.TransparentColor = System.Drawing.Color.Transparent
        Me.lstImages.Images.SetKeyName(0, "Warning")
        Me.lstImages.Images.SetKeyName(1, "Error")
        Me.lstImages.Images.SetKeyName(2, "Message")
        Me.lstImages.Images.SetKeyName(3, "Help")
        Me.lstImages.Images.SetKeyName(4, "Redpin")
        Me.lstImages.Images.SetKeyName(5, "Greenpin")
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewImageColumn1.HeaderText = ""
        Me.DataGridViewImageColumn1.Image = Global.clTrinity.My.Resources.Resources.find
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.info_2
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = false
        '
        'chkShowAll
        '
        Me.chkShowAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chkShowAll.AutoSize = true
        Me.chkShowAll.Location = New System.Drawing.Point(592, 26)
        Me.chkShowAll.Name = "chkShowAll"
        Me.chkShowAll.Size = New System.Drawing.Size(104, 17)
        Me.chkShowAll.TabIndex = 2
        Me.chkShowAll.Text = "Show all issues"
        Me.chkShowAll.UseVisualStyleBackColor = true
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(51, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(399, 31)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Note: The below issues may or may not require your attention. Check the source of"& _ 
    " the problem and decide if action needs to be taken."
        '
        'lblHidden
        '
        Me.lblHidden.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblHidden.AutoSize = true
        Me.lblHidden.ForeColor = System.Drawing.Color.Red
        Me.lblHidden.Location = New System.Drawing.Point(12, 355)
        Me.lblHidden.Name = "lblHidden"
        Me.lblHidden.Size = New System.Drawing.Size(137, 13)
        Me.lblHidden.TabIndex = 4
        Me.lblHidden.Text = "There are 0 hidden issues"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Problem"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = true
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn2.HeaderText = "Where"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = true
        '
        'cmdSolveSelected
        '
        Me.cmdSolveSelected.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cmdSolveSelected.FlatAppearance.BorderSize = 0
        Me.cmdSolveSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSolveSelected.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSolveSelected.ImageKey = "Greenpin"
        Me.cmdSolveSelected.ImageList = Me.lstImages
        Me.cmdSolveSelected.Location = New System.Drawing.Point(594, 351)
        Me.cmdSolveSelected.Name = "cmdSolveSelected"
        Me.cmdSolveSelected.Size = New System.Drawing.Size(102, 23)
        Me.cmdSolveSelected.TabIndex = 5
        Me.cmdSolveSelected.Text = "Solve selected"
        Me.cmdSolveSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSolveSelected.UseVisualStyleBackColor = true
        '
        'frmProblems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(708, 386)
        Me.Controls.Add(Me.cmdSolveSelected)
        Me.Controls.Add(Me.lblHidden)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkShowAll)
        Me.Controls.Add(Me.grdProblems)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmProblems"
        Me.Text = "Issues"
        CType(Me.grdProblems,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents grdProblems As System.Windows.Forms.DataGridView
    Friend WithEvents lstImages As System.Windows.Forms.ImageList
    Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkShowAll As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents colSeverity As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents colProblem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSource As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colHelp As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents colFixit As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents lblHidden As System.Windows.Forms.Label
    Friend WithEvents cmdSolveSelected As System.Windows.Forms.Button
End Class
