<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XtraForm1
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmbCountry = New DevExpress.XtraEditors.ComboBoxEdit
        Me.cmbPeriod = New DevExpress.XtraEditors.ComboBoxEdit
        Me.cmbTarget = New DevExpress.XtraEditors.ComboBoxEdit
        Me.cmbBookingType = New DevExpress.XtraEditors.ComboBoxEdit
        Me.cmbChannel = New DevExpress.XtraEditors.ComboBoxEdit
        Me.cmbAgency = New DevExpress.XtraEditors.ComboBoxEdit
        CType(Me.cmbCountry.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPeriod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTarget.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBookingType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbChannel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbAgency.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbCountry
        '
        Me.cmbCountry.Location = New System.Drawing.Point(12, 12)
        Me.cmbCountry.Name = "cmbCountry"
        Me.cmbCountry.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCountry.Size = New System.Drawing.Size(100, 20)
        Me.cmbCountry.TabIndex = 0
        '
        'cmbPeriod
        '
        Me.cmbPeriod.Location = New System.Drawing.Point(12, 142)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPeriod.Size = New System.Drawing.Size(100, 20)
        Me.cmbPeriod.TabIndex = 2
        '
        'cmbTarget
        '
        Me.cmbTarget.Location = New System.Drawing.Point(12, 116)
        Me.cmbTarget.Name = "cmbTarget"
        Me.cmbTarget.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTarget.Size = New System.Drawing.Size(100, 20)
        Me.cmbTarget.TabIndex = 3
        '
        'cmbBookingType
        '
        Me.cmbBookingType.Location = New System.Drawing.Point(12, 90)
        Me.cmbBookingType.Name = "cmbBookingType"
        Me.cmbBookingType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBookingType.Size = New System.Drawing.Size(100, 20)
        Me.cmbBookingType.TabIndex = 4
        '
        'cmbChannel
        '
        Me.cmbChannel.Location = New System.Drawing.Point(12, 64)
        Me.cmbChannel.Name = "cmbChannel"
        Me.cmbChannel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbChannel.Size = New System.Drawing.Size(100, 20)
        Me.cmbChannel.TabIndex = 5
        '
        'cmbAgency
        '
        Me.cmbAgency.Location = New System.Drawing.Point(12, 38)
        Me.cmbAgency.Name = "cmbAgency"
        Me.cmbAgency.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbAgency.Size = New System.Drawing.Size(100, 20)
        Me.cmbAgency.TabIndex = 6
        '
        'XtraForm1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(899, 515)
        Me.Controls.Add(Me.cmbAgency)
        Me.Controls.Add(Me.cmbChannel)
        Me.Controls.Add(Me.cmbBookingType)
        Me.Controls.Add(Me.cmbTarget)
        Me.Controls.Add(Me.cmbPeriod)
        Me.Controls.Add(Me.cmbCountry)
        Me.Name = "XtraForm1"
        Me.Text = "XtraForm1"
        CType(Me.cmbCountry.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPeriod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTarget.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBookingType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbChannel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbAgency.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbCountry As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbPeriod As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbTarget As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbBookingType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbChannel As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbAgency As DevExpress.XtraEditors.ComboBoxEdit
End Class
