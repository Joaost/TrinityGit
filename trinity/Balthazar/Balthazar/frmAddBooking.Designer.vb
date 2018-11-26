<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddBooking
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbSalesman = New System.Windows.Forms.ComboBox
        Me.grpBooking = New System.Windows.Forms.GroupBox
        Me.pnlCalendar = New System.Windows.Forms.Panel
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.optTime1 = New System.Windows.Forms.RadioButton
        Me.optTime2 = New System.Windows.Forms.RadioButton
        Me.cmdAddIt = New System.Windows.Forms.Button
        Me.calDates = New System.Windows.Forms.MonthCalendar
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmdOpen = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.chkUseStoreKitchen = New System.Windows.Forms.CheckBox
        Me.cmbRequestedProvider = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtOtherRequests = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtStorePlacement = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtStoreContact = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtStorePhone = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtCity = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtStore = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmdRemoveCollaborator = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmdAddCollaborator = New System.Windows.Forms.Button
        Me.lstCollaborators = New System.Windows.Forms.ListBox
        Me.cmdRemoveProduct = New System.Windows.Forms.Button
        Me.cmdRemoveDate = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmdAddProduct = New System.Windows.Forms.Button
        Me.lstProducts = New System.Windows.Forms.ListBox
        Me.cmdAddDate = New System.Windows.Forms.Button
        Me.lstDates = New System.Windows.Forms.ListBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        Me.grpBooking.SuspendLayout()
        Me.pnlCalendar.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(442, 369)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 31)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 25)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 25)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Sales person"
        '
        'cmbSalesman
        '
        Me.cmbSalesman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSalesman.FormattingEnabled = True
        Me.cmbSalesman.Location = New System.Drawing.Point(12, 26)
        Me.cmbSalesman.Name = "cmbSalesman"
        Me.cmbSalesman.Size = New System.Drawing.Size(150, 22)
        Me.cmbSalesman.Sorted = True
        Me.cmbSalesman.TabIndex = 1
        '
        'grpBooking
        '
        Me.grpBooking.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpBooking.Controls.Add(Me.pnlCalendar)
        Me.grpBooking.Controls.Add(Me.GroupBox1)
        Me.grpBooking.Controls.Add(Me.cmdRemoveCollaborator)
        Me.grpBooking.Controls.Add(Me.Label4)
        Me.grpBooking.Controls.Add(Me.cmdAddCollaborator)
        Me.grpBooking.Controls.Add(Me.lstCollaborators)
        Me.grpBooking.Controls.Add(Me.cmdRemoveProduct)
        Me.grpBooking.Controls.Add(Me.cmdRemoveDate)
        Me.grpBooking.Controls.Add(Me.Label3)
        Me.grpBooking.Controls.Add(Me.cmdAddProduct)
        Me.grpBooking.Controls.Add(Me.lstProducts)
        Me.grpBooking.Controls.Add(Me.cmdAddDate)
        Me.grpBooking.Controls.Add(Me.lstDates)
        Me.grpBooking.Controls.Add(Me.Label2)
        Me.grpBooking.Location = New System.Drawing.Point(12, 54)
        Me.grpBooking.Name = "grpBooking"
        Me.grpBooking.Size = New System.Drawing.Size(576, 309)
        Me.grpBooking.TabIndex = 2
        Me.grpBooking.TabStop = False
        Me.grpBooking.Text = "Booking"
        '
        'pnlCalendar
        '
        Me.pnlCalendar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCalendar.Controls.Add(Me.GroupBox2)
        Me.pnlCalendar.Controls.Add(Me.cmdAddIt)
        Me.pnlCalendar.Controls.Add(Me.calDates)
        Me.pnlCalendar.Location = New System.Drawing.Point(156, 55)
        Me.pnlCalendar.Name = "pnlCalendar"
        Me.pnlCalendar.Size = New System.Drawing.Size(171, 236)
        Me.pnlCalendar.TabIndex = 3
        Me.pnlCalendar.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optTime1)
        Me.GroupBox2.Controls.Add(Me.optTime2)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 167)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(104, 59)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Time"
        '
        'optTime1
        '
        Me.optTime1.AutoSize = True
        Me.optTime1.Checked = True
        Me.optTime1.Location = New System.Drawing.Point(6, 17)
        Me.optTime1.Name = "optTime1"
        Me.optTime1.Size = New System.Drawing.Size(53, 18)
        Me.optTime1.TabIndex = 2
        Me.optTime1.TabStop = True
        Me.optTime1.Text = "10-15"
        Me.optTime1.UseVisualStyleBackColor = True
        '
        'optTime2
        '
        Me.optTime2.AutoSize = True
        Me.optTime2.Location = New System.Drawing.Point(6, 35)
        Me.optTime2.Name = "optTime2"
        Me.optTime2.Size = New System.Drawing.Size(53, 18)
        Me.optTime2.TabIndex = 3
        Me.optTime2.Text = "11-16"
        Me.optTime2.UseVisualStyleBackColor = True
        '
        'cmdAddIt
        '
        Me.cmdAddIt.Location = New System.Drawing.Point(113, 203)
        Me.cmdAddIt.Name = "cmdAddIt"
        Me.cmdAddIt.Size = New System.Drawing.Size(53, 23)
        Me.cmdAddIt.TabIndex = 1
        Me.cmdAddIt.Text = "Add"
        Me.cmdAddIt.UseVisualStyleBackColor = True
        '
        'calDates
        '
        Me.calDates.Location = New System.Drawing.Point(-1, -1)
        Me.calDates.Name = "calDates"
        Me.calDates.ShowWeekNumbers = True
        Me.calDates.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdOpen)
        Me.GroupBox1.Controls.Add(Me.cmdSave)
        Me.GroupBox1.Controls.Add(Me.chkUseStoreKitchen)
        Me.GroupBox1.Controls.Add(Me.cmbRequestedProvider)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtOtherRequests)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtStorePlacement)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtStoreContact)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtStorePhone)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtCity)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtAddress)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtStore)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(186, 19)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(384, 281)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "General info"
        '
        'cmdOpen
        '
        Me.cmdOpen.Image = Global.Balthazar.My.Resources.Resources.folder
        Me.cmdOpen.Location = New System.Drawing.Point(274, 209)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(24, 24)
        Me.cmdOpen.TabIndex = 18
        Me.cmdOpen.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Image = Global.Balthazar.My.Resources.Resources.disk_blue
        Me.cmdSave.Location = New System.Drawing.Point(304, 209)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(24, 24)
        Me.cmdSave.TabIndex = 17
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'chkUseStoreKitchen
        '
        Me.chkUseStoreKitchen.AutoSize = True
        Me.chkUseStoreKitchen.Location = New System.Drawing.Point(9, 254)
        Me.chkUseStoreKitchen.Name = "chkUseStoreKitchen"
        Me.chkUseStoreKitchen.Size = New System.Drawing.Size(110, 18)
        Me.chkUseStoreKitchen.TabIndex = 16
        Me.chkUseStoreKitchen.Text = "Use store kitchen"
        Me.chkUseStoreKitchen.UseVisualStyleBackColor = True
        '
        'cmbRequestedProvider
        '
        Me.cmbRequestedProvider.FormattingEnabled = True
        Me.cmbRequestedProvider.Location = New System.Drawing.Point(6, 226)
        Me.cmbRequestedProvider.Name = "cmbRequestedProvider"
        Me.cmbRequestedProvider.Size = New System.Drawing.Size(158, 22)
        Me.cmbRequestedProvider.Sorted = True
        Me.cmbRequestedProvider.TabIndex = 15
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 209)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(102, 14)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "Requested provider"
        '
        'txtOtherRequests
        '
        Me.txtOtherRequests.Location = New System.Drawing.Point(6, 153)
        Me.txtOtherRequests.Multiline = True
        Me.txtOtherRequests.Name = "txtOtherRequests"
        Me.txtOtherRequests.Size = New System.Drawing.Size(322, 53)
        Me.txtOtherRequests.TabIndex = 13
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 136)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 14)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "Other requests"
        '
        'txtStorePlacement
        '
        Me.txtStorePlacement.Location = New System.Drawing.Point(170, 113)
        Me.txtStorePlacement.Name = "txtStorePlacement"
        Me.txtStorePlacement.Size = New System.Drawing.Size(158, 20)
        Me.txtStorePlacement.TabIndex = 11
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(170, 96)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(85, 14)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Store placement"
        '
        'txtStoreContact
        '
        Me.txtStoreContact.Location = New System.Drawing.Point(170, 73)
        Me.txtStoreContact.Name = "txtStoreContact"
        Me.txtStoreContact.Size = New System.Drawing.Size(158, 20)
        Me.txtStoreContact.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(170, 56)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(109, 14)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Store contact person"
        '
        'txtStorePhone
        '
        Me.txtStorePhone.Location = New System.Drawing.Point(170, 33)
        Me.txtStorePhone.Name = "txtStorePhone"
        Me.txtStorePhone.Size = New System.Drawing.Size(158, 20)
        Me.txtStorePhone.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(170, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 14)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Store phone no"
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(6, 113)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(158, 20)
        Me.txtCity.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 14)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "City"
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(6, 73)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(158, 20)
        Me.txtAddress.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 56)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 14)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Address"
        '
        'txtStore
        '
        Me.txtStore.Location = New System.Drawing.Point(6, 33)
        Me.txtStore.Name = "txtStore"
        Me.txtStore.Size = New System.Drawing.Size(158, 20)
        Me.txtStore.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 14)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Store"
        '
        'cmdRemoveCollaborator
        '
        Me.cmdRemoveCollaborator.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveCollaborator.Location = New System.Drawing.Point(156, 270)
        Me.cmdRemoveCollaborator.Name = "cmdRemoveCollaborator"
        Me.cmdRemoveCollaborator.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveCollaborator.TabIndex = 12
        Me.cmdRemoveCollaborator.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 223)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 14)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Collaborators"
        '
        'cmdAddCollaborator
        '
        Me.cmdAddCollaborator.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddCollaborator.Location = New System.Drawing.Point(156, 240)
        Me.cmdAddCollaborator.Name = "cmdAddCollaborator"
        Me.cmdAddCollaborator.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddCollaborator.TabIndex = 11
        Me.cmdAddCollaborator.UseVisualStyleBackColor = True
        '
        'lstCollaborators
        '
        Me.lstCollaborators.FormattingEnabled = True
        Me.lstCollaborators.ItemHeight = 14
        Me.lstCollaborators.Location = New System.Drawing.Point(6, 240)
        Me.lstCollaborators.Name = "lstCollaborators"
        Me.lstCollaborators.Size = New System.Drawing.Size(144, 60)
        Me.lstCollaborators.TabIndex = 10
        '
        'cmdRemoveProduct
        '
        Me.cmdRemoveProduct.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveProduct.Location = New System.Drawing.Point(156, 186)
        Me.cmdRemoveProduct.Name = "cmdRemoveProduct"
        Me.cmdRemoveProduct.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveProduct.TabIndex = 8
        Me.cmdRemoveProduct.UseVisualStyleBackColor = True
        '
        'cmdRemoveDate
        '
        Me.cmdRemoveDate.Image = Global.Balthazar.My.Resources.Resources.delete2
        Me.cmdRemoveDate.Location = New System.Drawing.Point(156, 63)
        Me.cmdRemoveDate.Name = "cmdRemoveDate"
        Me.cmdRemoveDate.Size = New System.Drawing.Size(24, 24)
        Me.cmdRemoveDate.TabIndex = 4
        Me.cmdRemoveDate.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 139)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(125, 14)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Products to demonstrate"
        '
        'cmdAddProduct
        '
        Me.cmdAddProduct.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddProduct.Location = New System.Drawing.Point(156, 156)
        Me.cmdAddProduct.Name = "cmdAddProduct"
        Me.cmdAddProduct.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddProduct.TabIndex = 7
        Me.cmdAddProduct.UseVisualStyleBackColor = True
        '
        'lstProducts
        '
        Me.lstProducts.FormattingEnabled = True
        Me.lstProducts.ItemHeight = 14
        Me.lstProducts.Location = New System.Drawing.Point(6, 156)
        Me.lstProducts.Name = "lstProducts"
        Me.lstProducts.Size = New System.Drawing.Size(144, 60)
        Me.lstProducts.TabIndex = 6
        '
        'cmdAddDate
        '
        Me.cmdAddDate.Image = Global.Balthazar.My.Resources.Resources.add2
        Me.cmdAddDate.Location = New System.Drawing.Point(156, 33)
        Me.cmdAddDate.Name = "cmdAddDate"
        Me.cmdAddDate.Size = New System.Drawing.Size(24, 24)
        Me.cmdAddDate.TabIndex = 2
        Me.cmdAddDate.UseVisualStyleBackColor = True
        '
        'lstDates
        '
        Me.lstDates.FormattingEnabled = True
        Me.lstDates.ItemHeight = 14
        Me.lstDates.Location = New System.Drawing.Point(6, 33)
        Me.lstDates.Name = "lstDates"
        Me.lstDates.Size = New System.Drawing.Size(144, 102)
        Me.lstDates.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Dates"
        '
        'frmAddBooking
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(600, 413)
        Me.Controls.Add(Me.grpBooking)
        Me.Controls.Add(Me.cmbSalesman)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddBooking"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add booking"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.grpBooking.ResumeLayout(False)
        Me.grpBooking.PerformLayout()
        Me.pnlCalendar.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbSalesman As System.Windows.Forms.ComboBox
    Friend WithEvents grpBooking As System.Windows.Forms.GroupBox
    Friend WithEvents lstDates As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlCalendar As System.Windows.Forms.Panel
    Friend WithEvents cmdAddIt As System.Windows.Forms.Button
    Friend WithEvents calDates As System.Windows.Forms.MonthCalendar
    Friend WithEvents cmdAddDate As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdAddProduct As System.Windows.Forms.Button
    Friend WithEvents lstProducts As System.Windows.Forms.ListBox
    Friend WithEvents cmdRemoveProduct As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveDate As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveCollaborator As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdAddCollaborator As System.Windows.Forms.Button
    Friend WithEvents lstCollaborators As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtStore As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtStoreContact As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtStorePhone As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtStorePlacement As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtOtherRequests As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkUseStoreKitchen As System.Windows.Forms.CheckBox
    Friend WithEvents cmbRequestedProvider As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents optTime1 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optTime2 As System.Windows.Forms.RadioButton
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button

End Class
