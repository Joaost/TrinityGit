<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPerformance
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
        Me.fileSystemTestButton = New System.Windows.Forms.Button
        Me.networkTestButton = New System.Windows.Forms.Button
        Me.testAdEdgeButton = New System.Windows.Forms.Button
        Me.resultFileSystem = New System.Windows.Forms.TextBox
        Me.resultMemory = New System.Windows.Forms.TextBox
        Me.resultAdEdge = New System.Windows.Forms.TextBox
        Me.testAllButton = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'fileSystemTestButton
        '
        Me.fileSystemTestButton.Location = New System.Drawing.Point(12, 12)
        Me.fileSystemTestButton.Name = "fileSystemTestButton"
        Me.fileSystemTestButton.Size = New System.Drawing.Size(115, 51)
        Me.fileSystemTestButton.TabIndex = 0
        Me.fileSystemTestButton.Text = "Test File System"
        Me.fileSystemTestButton.UseVisualStyleBackColor = True
        '
        'networkTestButton
        '
        Me.networkTestButton.Enabled = False
        Me.networkTestButton.Location = New System.Drawing.Point(12, 69)
        Me.networkTestButton.Name = "networkTestButton"
        Me.networkTestButton.Size = New System.Drawing.Size(115, 51)
        Me.networkTestButton.TabIndex = 1
        Me.networkTestButton.Text = "Test Network"
        Me.networkTestButton.UseVisualStyleBackColor = True
        '
        'testAdEdgeButton
        '
        Me.testAdEdgeButton.Location = New System.Drawing.Point(10, 126)
        Me.testAdEdgeButton.Name = "testAdEdgeButton"
        Me.testAdEdgeButton.Size = New System.Drawing.Size(115, 51)
        Me.testAdEdgeButton.TabIndex = 2
        Me.testAdEdgeButton.Text = "Test AdvantEdge"
        Me.testAdEdgeButton.UseVisualStyleBackColor = True
        '
        'resultFileSystem
        '
        Me.resultFileSystem.Location = New System.Drawing.Point(133, 28)
        Me.resultFileSystem.Name = "resultFileSystem"
        Me.resultFileSystem.Size = New System.Drawing.Size(90, 20)
        Me.resultFileSystem.TabIndex = 3
        '
        'resultMemory
        '
        Me.resultMemory.Location = New System.Drawing.Point(133, 85)
        Me.resultMemory.Name = "resultMemory"
        Me.resultMemory.Size = New System.Drawing.Size(90, 20)
        Me.resultMemory.TabIndex = 4
        '
        'resultAdEdge
        '
        Me.resultAdEdge.Location = New System.Drawing.Point(133, 142)
        Me.resultAdEdge.Name = "resultAdEdge"
        Me.resultAdEdge.Size = New System.Drawing.Size(90, 20)
        Me.resultAdEdge.TabIndex = 5
        '
        'testAllButton
        '
        Me.testAllButton.Location = New System.Drawing.Point(12, 183)
        Me.testAllButton.Name = "testAllButton"
        Me.testAllButton.Size = New System.Drawing.Size(115, 51)
        Me.testAllButton.TabIndex = 7
        Me.testAllButton.Text = "Test All"
        Me.testAllButton.UseVisualStyleBackColor = True
        '
        'frmPerformance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(240, 251)
        Me.Controls.Add(Me.testAllButton)
        Me.Controls.Add(Me.resultAdEdge)
        Me.Controls.Add(Me.resultMemory)
        Me.Controls.Add(Me.resultFileSystem)
        Me.Controls.Add(Me.testAdEdgeButton)
        Me.Controls.Add(Me.networkTestButton)
        Me.Controls.Add(Me.fileSystemTestButton)
        Me.Name = "frmPerformance"
        Me.Text = "Performance Tests"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents fileSystemTestButton As System.Windows.Forms.Button
    Friend WithEvents networkTestButton As System.Windows.Forms.Button
    Friend WithEvents testAdEdgeButton As System.Windows.Forms.Button
    Friend WithEvents resultFileSystem As System.Windows.Forms.TextBox
    Friend WithEvents resultMemory As System.Windows.Forms.TextBox
    Friend WithEvents resultAdEdge As System.Windows.Forms.TextBox
    Friend WithEvents testAllButton As System.Windows.Forms.Button
End Class
