Imports System.Windows.Forms

Public Class frmAddProduct
    Dim rd As Odbc.OdbcDataReader

    Dim AdtooxBrandID As Long = -1
    Dim AdtooxAdvertiserID As Long = -1
    Dim AdtooxDivisionID As Long = -1

    Private Sub frmAddProduct_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'if the "EDIT" tag exist we load all information into the fields
        AdtooxBrandID = -1
        AdtooxAdvertiserID = -1
        AdtooxDivisionID = -1
        cmbAdtooxCustomers.Items.Clear()
        cmbAdtooxDivisions.Items.Clear()
        cmbAdtooxBrands.Items.Clear()

        If Me.Tag = "EDIT" Then
            Dim product As DataTable = DBReader.getAllFromProducts(Campaign.ProductID)

            'Dim com As New Odbc.OdbcCommand("SELECT * FROM Products WHERE id=" & Campaign.ProductID, DBConn)
            'Dim rd As Odbc.OdbcDataReader = com.ExecuteReader
            '            rd.Read()

            If product.Rows.Count = 1 Then
                For Each rd As DataRow In product.Rows
                    txtName.Text = rd("Name")
                    txtClientID.Text = rd("MarathonClient")
                    txtDepartment.Text = rd("MarathonProduct")
                    txtCompany.Text = rd("MarathonCompany")
                    txtContract.Text = rd("MarathonContract")
                    If Not IsDBNull(rd("AdTooxBrandId")) Then
                        AdtooxBrandID = rd("AdTooxBrandId")
                    End If
                    If Not IsDBNull(rd("AdTooxAdvertiserID")) Then
                        AdtooxAdvertiserID = rd("AdtooxAdvertiserID")
                    End If
                    If Not IsDBNull(rd("AdTooxDivisionID")) Then
                        AdtooxDivisionID = rd("AdTooxDivisionID")
                    End If
                    If Not IsDBNull(rd("AdTooxProductType")) Then
                        txtAdtooxProductType.Text = rd("AdTooxProductType")
                    End If
                Next
            End If
            'If rd.HasRows Then
            '    txtName.Text = rd!Name
            '    txtClientID.Text = rd!MarathonClient
            '    txtDepartment.Text = rd!MarathonProduct
            '    txtCompany.Text = rd!MarathonCompany
            '    txtContract.Text = rd!MarathonContract
            'End If
        End If
        If Not TrinitySettings.MarathonEnabled Then
            grpMarathon.Visible = False
            Me.Height = 155
            grpMarathon.Height = 0
        Else
            grpMarathon.Visible = True
            Me.Height = 265
        End If

        'If TrinitySettings.AdtooxEnabled Then
        '    grpAdToox.Visible = True
        grpAdToox.Top = grpMarathon.Bottom + 10
        Me.Height = grpAdToox.Bottom + cmdOk.Height + 40

        '    For Each Customer As Trinity.cAdtooxCustomer In Campaign.AdToox.GetCustomersForUser
        '        cmbAdtooxCustomers.Items.Add(Customer)
        '        If AdtooxAdvertiserID = Customer.ID Then
        '            cmbAdtooxCustomers.SelectedItem = Customer
        '        End If
        '    Next
        'Else
        '    grpAdToox.Visible = False
        'End If



        If Campaign.AdEdgeProducts.Count > 0 Then
            txtProducts.ForeColor = Color.Black
            txtProducts.Text = Campaign.AdEdgeProducts.Count & " brands chosen"
        Else
            txtProducts.ForeColor = Color.Red
            txtProducts.Text = "No brands chosen"
        End If

        cmbAdtooxCustomers.DisplayMember = "Name"
        cmbAdtooxDivisions.DisplayMember = "Name"
        cmbAdtooxBrands.DisplayMember = "Name"

        If DBReader.isLocal Then
            MsgBox("You are using a Local database and all changes you make will be lost when you connect to network.", MsgBoxStyle.Information, "FYI")
        End If
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        'Dim com As New Odbc.OdbcCommand("SELECT * FROM Products", DBConn)
        If txtName.Text <> "" Then ' the name cannot be empty
            'if we are to create a product (no tag)
            Dim AdTooxCustomerID As Integer = -1
            Dim AdTooxDivisionID As Integer = -1
            Dim AdTooxBrandID As Integer = -1
            If cmbAdtooxCustomers.SelectedItem IsNot Nothing Then AdTooxCustomerID = cmbAdtooxCustomers.SelectedItem.ID
            If cmbAdtooxDivisions.SelectedItem IsNot Nothing Then AdTooxDivisionID = cmbAdtooxDivisions.SelectedItem.ID
            If cmbAdtooxBrands.SelectedItem IsNot Nothing Then AdTooxBrandID = cmbAdtooxBrands.SelectedItem.ID
            If Me.Tag = "" Then ' tag cannot be empty
                'rd = com.ExecuteReader ' read all from products
                'While rd.Read
                '    If UCase(rd!Name) = UCase(txtName.Text) Then ' check if the name is unique
                '        If MessageBox.Show("There is already a product called '" & txtName.Text & "' in the database." & vbCrLf & vbCrLf & "Are you sure you want to add another one?", "T R I N I T Y", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then Exit While
                '        Exit Sub
                '    End If
                'End While
                'rd.Close()
                'com.CommandText = "INSERT INTO Products (Name,ClientID,MarathonClient,MarathonProduct,MarathonCompany,MarathonContract) VALUES ('" & txtName.Text & "'," & Campaign.ClientID & ",'" & txtClientID.Text & "','" & txtDepartment.Text & "','" & txtCompany.Text & "','" & txtContract.Text & "')"
                'com.ExecuteScalar() 'insert the new product

                If DBReader.productExist(txtName.Text.Trim) Then
                    If MessageBox.Show("There is already a product called '" & txtName.Text & "' in the database." & vbCrLf & vbCrLf & "Are you sure you want to add another one?", "T R I N I T Y", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then Exit Sub
                End If

                DBReader.addProduct(txtName.Text, Campaign.ClientID, txtClientID.Text, txtDepartment.Text, txtCompany.Text, txtContract.Text, Campaign.AdEdgeProducts, AdTooxCustomerID, AdTooxDivisionID, AdTooxBrandID, txtAdtooxProductType.Text)
                'if we are to edit a product (edit tag)
            ElseIf Me.Tag = "EDIT" Then
                DBReader.updateProduct(Campaign.ProductID, txtName.Text, Campaign.ClientID, txtClientID.Text, txtDepartment.Text, txtCompany.Text, txtContract.Text, Campaign.AdEdgeProducts, AdTooxCustomerID, AdTooxDivisionID, AdTooxBrandID, txtAdtooxProductType.Text)
            End If
            '????????????????????????????????????????????????? lägga allt i en SQL sats?
            'com.CommandText = "UPDATE Products SET Name='" & txtName.Text & "' WHERE id=" & Campaign.ProductID
            'com.ExecuteScalar()
            'com.CommandText = "UPDATE Products SET MarathonClient='" & txtClientID.Text & "' WHERE id=" & Campaign.ProductID
            'com.ExecuteScalar()
            'com.CommandText = "UPDATE Products SET MarathonProduct='" & txtDepartment.Text & "' WHERE id=" & Campaign.ProductID
            'com.ExecuteScalar()
            'com.CommandText = "UPDATE Products SET MarathonCompany='" & txtCompany.Text & "' WHERE id=" & Campaign.ProductID
            'com.ExecuteScalar()
            'com.CommandText = "UPDATE Products SET MarathonContract='" & txtContract.Text & "' WHERE id=" & Campaign.ProductID
            'com.ExecuteScalar()
        End If
        Me.Hide()
    End Sub

    Private Sub cmdPick_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPick.Click
        frmPickBrands.ShowDialog()
        If Campaign.AdEdgeProducts.Count > 0 Then
            txtProducts.ForeColor = Color.Black
            txtProducts.Text = Campaign.AdEdgeProducts.Count & " brands chosen"
        Else
            txtProducts.ForeColor = Color.Red
            txtProducts.Text = "No brands chosen"
        End If
    End Sub

    Private Sub cmbAdtooxCustomers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAdtooxCustomers.SelectedIndexChanged
        cmbAdtooxDivisions.Items.Clear()
        cmbAdtooxBrands.Items.Clear()
        For Each Division As Trinity.cAdtooxDivision In Campaign.AdToox.GetDivisionsForCustomer(CType(cmbAdtooxCustomers.Items(cmbAdtooxCustomers.SelectedIndex), Trinity.cAdtooxCustomer).ID)
            cmbAdtooxDivisions.Items.Add(Division)
            If AdtooxDivisionID = Division.ID Then
                cmbAdtooxDivisions.SelectedItem = Division
            End If
        Next
        'If cmbAdtooxDivisions.Items.Count > 0 Then cmbAdtooxDivisions.SelectedItem = cmbAdtooxDivisions.Items(1)
    End Sub

    Private Sub cmbAdtooxDivisions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAdtooxDivisions.SelectedIndexChanged

        If cmbAdtooxDivisions.SelectedIndex < 0 Then Exit Sub

        cmbAdtooxBrands.Items.Clear()

        For Each Brand As Trinity.cAdtooxBrand In Campaign.AdToox.GetBrandsForDivision(CType(cmbAdtooxDivisions.Items(cmbAdtooxDivisions.SelectedIndex), Trinity.cAdtooxDivision).ID)
            cmbAdtooxBrands.Items.Add(Brand)
            If AdtooxBrandID = Brand.ID Then
                cmbAdtooxBrands.SelectedItem = Brand
            End If
        Next
        'If cmbAdtooxBrands.Items.Count > 0 Then cmbAdtooxBrands.SelectedItem = cmbAdtooxBrands.Items(1)
    End Sub

    Private Sub cmbAdtooxBrands_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAdtooxBrands.SelectedIndexChanged
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class