<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNew
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
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("New event", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("From template", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Blank event", 0)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNew))
        Me.lvwEvents = New System.Windows.Forms.ListView
        Me.imlNew = New System.Windows.Forms.ImageList(Me.components)
        Me.cmdOk = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lvwEvents
        '
        Me.lvwEvents.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ListViewGroup1.Header = "New event"
        ListViewGroup1.Name = "grpNewEvent"
        ListViewGroup2.Header = "From template"
        ListViewGroup2.Name = "grpTemplates"
        Me.lvwEvents.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        ListViewItem1.Group = ListViewGroup1
        Me.lvwEvents.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.lvwEvents.LargeImageList = Me.imlNew
        Me.lvwEvents.Location = New System.Drawing.Point(12, 12)
        Me.lvwEvents.Name = "lvwEvents"
        Me.lvwEvents.Size = New System.Drawing.Size(437, 252)
        Me.lvwEvents.TabIndex = 0
        Me.lvwEvents.UseCompatibleStateImageBehavior = False
        '
        'imlNew
        '
        Me.imlNew.ImageStream = CType(resources.GetObject("imlNew.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlNew.TransparentColor = System.Drawing.Color.Transparent
        Me.imlNew.Images.SetKeyName(0, "new")
        Me.imlNew.Images.SetKeyName(1, "template")
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOk.Location = New System.Drawing.Point(364, 270)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(85, 32)
        Me.cmdOk.TabIndex = 1
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'frmNew
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(461, 314)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.lvwEvents)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNew"
        Me.Text = "Create new event"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lvwEvents As System.Windows.Forms.ListView
    Friend WithEvents imlNew As System.Windows.Forms.ImageList
    Friend WithEvents cmdOk As System.Windows.Forms.Button
End Class
