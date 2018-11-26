<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThreeChoices
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmThreeChoices))
        Me.btnReplace = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnSkip = New System.Windows.Forms.Button()
        Me.chkDoForAll = New System.Windows.Forms.CheckBox()
        Me.lblExplanation = New System.Windows.Forms.Label()
        Me.lblItem = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnReplace
        '
        Me.btnReplace.FlatAppearance.BorderSize = 0
        Me.btnReplace.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReplace.Location = New System.Drawing.Point(11, 57)
        Me.btnReplace.Name = "btnReplace"
        Me.btnReplace.Size = New System.Drawing.Size(75, 23)
        Me.btnReplace.TabIndex = 0
        Me.btnReplace.Text = "Replace"
        Me.btnReplace.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Location = New System.Drawing.Point(92, 57)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 1
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnSkip
        '
        Me.btnSkip.FlatAppearance.BorderSize = 0
        Me.btnSkip.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSkip.Location = New System.Drawing.Point(173, 57)
        Me.btnSkip.Name = "btnSkip"
        Me.btnSkip.Size = New System.Drawing.Size(75, 23)
        Me.btnSkip.TabIndex = 2
        Me.btnSkip.Text = "Skip"
        Me.btnSkip.UseVisualStyleBackColor = True
        '
        'chkDoForAll
        '
        Me.chkDoForAll.AutoSize = True
        Me.chkDoForAll.Location = New System.Drawing.Point(254, 61)
        Me.chkDoForAll.Name = "chkDoForAll"
        Me.chkDoForAll.Size = New System.Drawing.Size(129, 17)
        Me.chkDoForAll.TabIndex = 3
        Me.chkDoForAll.Text = "Do this for all conflicts"
        Me.chkDoForAll.UseVisualStyleBackColor = True
        '
        'lblExplanation
        '
        Me.lblExplanation.Location = New System.Drawing.Point(8, 20)
        Me.lblExplanation.Name = "lblExplanation"
        Me.lblExplanation.Size = New System.Drawing.Size(372, 25)
        Me.lblExplanation.TabIndex = 4
        Me.lblExplanation.Text = "lblExplanation"
        '
        'lblItem
        '
        Me.lblItem.Location = New System.Drawing.Point(8, 35)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(268, 23)
        Me.lblItem.TabIndex = 5
        '
        'frmThreeChoices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(391, 92)
        Me.Controls.Add(Me.lblItem)
        Me.Controls.Add(Me.lblExplanation)
        Me.Controls.Add(Me.chkDoForAll)
        Me.Controls.Add(Me.btnSkip)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnReplace)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmThreeChoices"
        Me.Text = "Choose"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnReplace As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnSkip As System.Windows.Forms.Button
    Friend WithEvents chkDoForAll As System.Windows.Forms.CheckBox
    Friend WithEvents lblExplanation As System.Windows.Forms.Label
    Friend WithEvents lblItem As System.Windows.Forms.Label
End Class
