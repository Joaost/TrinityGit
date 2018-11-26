<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSummary
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSummary))
        Me.QuestionaireAnswersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.BalthazarDataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.BalthazarDataSet = New Balthazar.balthazarDataSet
        Me.QuestionaireBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.QuestionaireTableAdapter = New Balthazar.balthazarDataSetTableAdapters.QuestionaireTableAdapter
        Me.QuestionaireAnswersTableAdapter = New Balthazar.balthazarDataSetTableAdapters.QuestionaireAnswersTableAdapter
        Me.grdSummary = New System.Windows.Forms.DataGridView
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.cmdExcel = New System.Windows.Forms.ToolStripButton
        CType(Me.QuestionaireAnswersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BalthazarDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BalthazarDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.QuestionaireBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'QuestionaireAnswersBindingSource
        '
        Me.QuestionaireAnswersBindingSource.DataMember = "QuestionaireAnswers"
        Me.QuestionaireAnswersBindingSource.DataSource = Me.BalthazarDataSetBindingSource
        '
        'BalthazarDataSetBindingSource
        '
        Me.BalthazarDataSetBindingSource.DataSource = Me.BalthazarDataSet
        Me.BalthazarDataSetBindingSource.Position = 0
        '
        'BalthazarDataSet
        '
        Me.BalthazarDataSet.DataSetName = "balthazarDataSet"
        Me.BalthazarDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'QuestionaireBindingSource
        '
        Me.QuestionaireBindingSource.DataMember = "Questionaire"
        Me.QuestionaireBindingSource.DataSource = Me.BalthazarDataSetBindingSource
        '
        'QuestionaireTableAdapter
        '
        Me.QuestionaireTableAdapter.ClearBeforeFill = True
        '
        'QuestionaireAnswersTableAdapter
        '
        Me.QuestionaireAnswersTableAdapter.ClearBeforeFill = True
        '
        'grdSummary
        '
        Me.grdSummary.AllowUserToAddRows = False
        Me.grdSummary.AllowUserToDeleteRows = False
        Me.grdSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSummary.Location = New System.Drawing.Point(0, 0)
        Me.grdSummary.Name = "grdSummary"
        Me.grdSummary.ReadOnly = True
        Me.grdSummary.RowHeadersVisible = False
        Me.grdSummary.Size = New System.Drawing.Size(1215, 689)
        Me.grdSummary.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdExcel})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1215, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmdExcel
        '
        Me.cmdExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdExcel.Image = Global.Balthazar.My.Resources.Resources.excel
        Me.cmdExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(23, 22)
        Me.cmdExcel.Text = "ToolStripButton1"
        '
        'frmSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1215, 689)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.grdSummary)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSummary"
        Me.Text = "Summary"
        CType(Me.QuestionaireAnswersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BalthazarDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BalthazarDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.QuestionaireBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BalthazarDataSetBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents BalthazarDataSet As Balthazar.balthazarDataSet
    Friend WithEvents QuestionaireBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents QuestionaireTableAdapter As Balthazar.balthazarDataSetTableAdapters.QuestionaireTableAdapter
    Friend WithEvents QuestionaireAnswersBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents QuestionaireAnswersTableAdapter As Balthazar.balthazarDataSetTableAdapters.QuestionaireAnswersTableAdapter
    Friend WithEvents grdSummary As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdExcel As System.Windows.Forms.ToolStripButton
End Class
