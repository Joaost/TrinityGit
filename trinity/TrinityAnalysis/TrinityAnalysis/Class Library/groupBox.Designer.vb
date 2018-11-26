<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class groupBox
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(groupBox))
        Me.cmdDeleteValue = New System.Windows.Forms.Button()
        Me.cmdAddValue = New System.Windows.Forms.Button()
        Me.cmbValueList = New System.Windows.Forms.ComboBox()
        Me.lblValueNr = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.lblToValue = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdDeleteValue
        '
        Me.cmdDeleteValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDeleteValue.Image = CType(resources.GetObject("cmdDeleteValue.Image"), System.Drawing.Image)
        Me.cmdDeleteValue.Location = New System.Drawing.Point(554, 38)
        Me.cmdDeleteValue.Name = "cmdDeleteValue"
        Me.cmdDeleteValue.Size = New System.Drawing.Size(22, 22)
        Me.cmdDeleteValue.TabIndex = 14
        Me.cmdDeleteValue.UseVisualStyleBackColor = True
        '
        'cmdAddValue
        '
        Me.cmdAddValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddValue.Image = CType(resources.GetObject("cmdAddValue.Image"), System.Drawing.Image)
        Me.cmdAddValue.Location = New System.Drawing.Point(554, 14)
        Me.cmdAddValue.Name = "cmdAddValue"
        Me.cmdAddValue.Size = New System.Drawing.Size(22, 22)
        Me.cmdAddValue.TabIndex = 13
        Me.cmdAddValue.UseVisualStyleBackColor = True
        '
        'cmbValueList
        '
        Me.cmbValueList.FormattingEnabled = True
        Me.cmbValueList.Location = New System.Drawing.Point(6, 35)
        Me.cmbValueList.Name = "cmbValueList"
        Me.cmbValueList.Size = New System.Drawing.Size(148, 21)
        Me.cmbValueList.TabIndex = 15
        '
        'lblValueNr
        '
        Me.lblValueNr.AutoSize = True
        Me.lblValueNr.Location = New System.Drawing.Point(6, 16)
        Me.lblValueNr.Name = "lblValueNr"
        Me.lblValueNr.Size = New System.Drawing.Size(34, 13)
        Me.lblValueNr.TabIndex = 16
        Me.lblValueNr.Text = "Value"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.lblTo)
        Me.GroupBox1.Controls.Add(Me.lblFrom)
        Me.GroupBox1.Controls.Add(Me.lblToValue)
        Me.GroupBox1.Controls.Add(Me.lblValueNr)
        Me.GroupBox1.Controls.Add(Me.cmbValueList)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(548, 65)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(366, 16)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(34, 13)
        Me.lblTo.TabIndex = 20
        Me.lblTo.Text = "Value"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(166, 16)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(34, 13)
        Me.lblFrom.TabIndex = 19
        Me.lblFrom.Text = "Value"
        '
        'lblToValue
        '
        Me.lblToValue.AutoSize = True
        Me.lblToValue.Location = New System.Drawing.Point(346, 38)
        Me.lblToValue.Name = "lblToValue"
        Me.lblToValue.Size = New System.Drawing.Size(20, 13)
        Me.lblToValue.TabIndex = 18
        Me.lblToValue.Text = "To"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(169, 35)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(171, 20)
        Me.TextBox1.TabIndex = 21
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(369, 35)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(171, 20)
        Me.TextBox2.TabIndex = 22
        '
        'groupBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdDeleteValue)
        Me.Controls.Add(Me.cmdAddValue)
        Me.Name = "groupBox"
        Me.Size = New System.Drawing.Size(579, 76)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdDeleteValue As System.Windows.Forms.Button
    Friend WithEvents cmdAddValue As System.Windows.Forms.Button
    Friend WithEvents cmbValueList As System.Windows.Forms.ComboBox
    Friend WithEvents lblValueNr As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblToValue As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox

End Class
