<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.cmdImport = New System.Windows.Forms.Button
        Me.cmdExport = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdINI = New System.Windows.Forms.Button
        Me.cmdUpload = New System.Windows.Forms.Button
        Me.cmdSchedule = New System.Windows.Forms.Button
        Me.cmdFile = New System.Windows.Forms.Button
        Me.cmdXML = New System.Windows.Forms.Button
        Me.grpCommon = New System.Windows.Forms.GroupBox
        Me.cmdPremiums = New System.Windows.Forms.Button
        Me.cmdSIFO = New System.Windows.Forms.Button
        Me.cmdEditPrices = New System.Windows.Forms.Button
        Me.cmdEditChannel = New System.Windows.Forms.Button
        Me.grpAdvanced = New System.Windows.Forms.GroupBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.cmdEditPricelists = New System.Windows.Forms.Button
        Me.grpCommon.SuspendLayout()
        Me.grpAdvanced.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdImport
        '
        Me.cmdImport.Location = New System.Drawing.Point(6, 76)
        Me.cmdImport.Name = "cmdImport"
        Me.cmdImport.Size = New System.Drawing.Size(115, 50)
        Me.cmdImport.TabIndex = 1
        Me.cmdImport.Text = "Import"
        Me.cmdImport.UseVisualStyleBackColor = True
        '
        'cmdExport
        '
        Me.cmdExport.Location = New System.Drawing.Point(6, 19)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.Size = New System.Drawing.Size(115, 50)
        Me.cmdExport.TabIndex = 2
        Me.cmdExport.Text = "Export"
        Me.cmdExport.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(37, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(195, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Welcome to Trinity Administrative Tools!"
        '
        'cmdINI
        '
        Me.cmdINI.Location = New System.Drawing.Point(128, 19)
        Me.cmdINI.Name = "cmdINI"
        Me.cmdINI.Size = New System.Drawing.Size(115, 50)
        Me.cmdINI.TabIndex = 5
        Me.cmdINI.Text = "INI-Files"
        Me.cmdINI.UseVisualStyleBackColor = True
        '
        'cmdUpload
        '
        Me.cmdUpload.Enabled = False
        Me.cmdUpload.Location = New System.Drawing.Point(20, 388)
        Me.cmdUpload.Name = "cmdUpload"
        Me.cmdUpload.Size = New System.Drawing.Size(235, 33)
        Me.cmdUpload.TabIndex = 6
        Me.cmdUpload.Text = "Upload Changes"
        Me.cmdUpload.UseVisualStyleBackColor = True
        Me.cmdUpload.Visible = False
        '
        'cmdSchedule
        '
        Me.cmdSchedule.Location = New System.Drawing.Point(8, 19)
        Me.cmdSchedule.Name = "cmdSchedule"
        Me.cmdSchedule.Size = New System.Drawing.Size(113, 50)
        Me.cmdSchedule.TabIndex = 7
        Me.cmdSchedule.Text = "Load Schedules"
        Me.cmdSchedule.UseVisualStyleBackColor = True
        '
        'cmdFile
        '
        Me.cmdFile.Location = New System.Drawing.Point(8, 76)
        Me.cmdFile.Name = "cmdFile"
        Me.cmdFile.Size = New System.Drawing.Size(113, 50)
        Me.cmdFile.TabIndex = 8
        Me.cmdFile.Text = "Upload Files"
        Me.cmdFile.UseVisualStyleBackColor = True
        '
        'cmdXML
        '
        Me.cmdXML.Location = New System.Drawing.Point(8, 19)
        Me.cmdXML.Name = "cmdXML"
        Me.cmdXML.Size = New System.Drawing.Size(113, 50)
        Me.cmdXML.TabIndex = 9
        Me.cmdXML.Text = "XML-Files"
        Me.cmdXML.UseVisualStyleBackColor = True
        '
        'grpCommon
        '
        Me.grpCommon.Controls.Add(Me.cmdPremiums)
        Me.grpCommon.Controls.Add(Me.cmdSIFO)
        Me.grpCommon.Controls.Add(Me.cmdEditPrices)
        Me.grpCommon.Controls.Add(Me.cmdEditChannel)
        Me.grpCommon.Controls.Add(Me.cmdFile)
        Me.grpCommon.Controls.Add(Me.cmdSchedule)
        Me.grpCommon.Location = New System.Drawing.Point(12, 86)
        Me.grpCommon.Name = "grpCommon"
        Me.grpCommon.Size = New System.Drawing.Size(249, 188)
        Me.grpCommon.TabIndex = 10
        Me.grpCommon.TabStop = False
        Me.grpCommon.Text = "Normal Users"
        '
        'cmdPremiums
        '
        Me.cmdPremiums.Location = New System.Drawing.Point(130, 131)
        Me.cmdPremiums.Name = "cmdPremiums"
        Me.cmdPremiums.Size = New System.Drawing.Size(113, 50)
        Me.cmdPremiums.TabIndex = 13
        Me.cmdPremiums.Text = "Load Premiums"
        Me.cmdPremiums.UseVisualStyleBackColor = True
        '
        'cmdSIFO
        '
        Me.cmdSIFO.Location = New System.Drawing.Point(8, 132)
        Me.cmdSIFO.Name = "cmdSIFO"
        Me.cmdSIFO.Size = New System.Drawing.Size(113, 50)
        Me.cmdSIFO.TabIndex = 12
        Me.cmdSIFO.Text = "Load Spot Ctrl"
        Me.cmdSIFO.UseVisualStyleBackColor = True
        '
        'cmdEditPrices
        '
        Me.cmdEditPrices.Location = New System.Drawing.Point(128, 75)
        Me.cmdEditPrices.Name = "cmdEditPrices"
        Me.cmdEditPrices.Size = New System.Drawing.Size(115, 50)
        Me.cmdEditPrices.TabIndex = 9
        Me.cmdEditPrices.Text = "Edit Prices"
        Me.cmdEditPrices.UseVisualStyleBackColor = True
        '
        'cmdEditChannel
        '
        Me.cmdEditChannel.Location = New System.Drawing.Point(128, 19)
        Me.cmdEditChannel.Name = "cmdEditChannel"
        Me.cmdEditChannel.Size = New System.Drawing.Size(115, 50)
        Me.cmdEditChannel.TabIndex = 8
        Me.cmdEditChannel.Text = "Edit Channel"
        Me.cmdEditChannel.UseVisualStyleBackColor = True
        '
        'grpAdvanced
        '
        Me.grpAdvanced.Controls.Add(Me.cmdINI)
        Me.grpAdvanced.Controls.Add(Me.cmdXML)
        Me.grpAdvanced.Enabled = False
        Me.grpAdvanced.Location = New System.Drawing.Point(12, 280)
        Me.grpAdvanced.Name = "grpAdvanced"
        Me.grpAdvanced.Size = New System.Drawing.Size(249, 96)
        Me.grpAdvanced.TabIndex = 11
        Me.grpAdvanced.TabStop = False
        Me.grpAdvanced.Text = "Advanced Users (Dont mess anything up!!)"
        Me.grpAdvanced.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdExport)
        Me.GroupBox1.Controls.Add(Me.cmdImport)
        Me.GroupBox1.Location = New System.Drawing.Point(267, 290)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(130, 229)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Maries current version"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(20, 55)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(233, 25)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "Set penetration level"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'cmdEditPricelists
        '
        Me.cmdEditPricelists.Location = New System.Drawing.Point(20, 442)
        Me.cmdEditPricelists.Name = "cmdEditPricelists"
        Me.cmdEditPricelists.Size = New System.Drawing.Size(235, 50)
        Me.cmdEditPricelists.TabIndex = 14
        Me.cmdEditPricelists.Text = "Edit Pricelists"
        Me.cmdEditPricelists.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 531)
        Me.Controls.Add(Me.cmdEditPricelists)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpAdvanced)
        Me.Controls.Add(Me.grpCommon)
        Me.Controls.Add(Me.cmdUpload)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Trinity Admin Program"
        Me.grpCommon.ResumeLayout(False)
        Me.grpAdvanced.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdImport As System.Windows.Forms.Button
    Friend WithEvents cmdExport As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdINI As System.Windows.Forms.Button
    Friend WithEvents cmdUpload As System.Windows.Forms.Button
    Friend WithEvents cmdSchedule As System.Windows.Forms.Button
    Friend WithEvents cmdFile As System.Windows.Forms.Button
    Friend WithEvents cmdXML As System.Windows.Forms.Button
    Friend WithEvents grpCommon As System.Windows.Forms.GroupBox
    Friend WithEvents cmdEditPrices As System.Windows.Forms.Button
    Friend WithEvents cmdEditChannel As System.Windows.Forms.Button
    Friend WithEvents grpAdvanced As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSIFO As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cmdPremiums As System.Windows.Forms.Button
    Friend WithEvents cmdEditPricelists As System.Windows.Forms.Button

End Class
