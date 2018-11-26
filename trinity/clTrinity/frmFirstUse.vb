Public Class frmFirstUse

    Private CountryCompanyList As New Dictionary(Of String, List(Of String))

    Private Sub SetupCountries()

        CountryCompanyList.Add("Sweden", New List(Of String)({"MEC", "Mindshare", "Mediacompany", "Mediacom", "Maxus"}))
        CountryCompanyList.Add("Norway", New List(Of String)({"MEC", "Mindshare", "Mediacom", "Maxus"}))
        CountryCompanyList.Add("Denmark", New List(Of String)({"MEC", "Mindshare", "Mediacompany", "Mediacom", "Maxus"}))
        CountryCompanyList.Add("Estonia", New List(Of String)({"Mediacom"}))
    End Sub

    Private Sub CompanyChosen(ByVal sender As Object, ByVal e As System.EventArgs)
        grpAdEdge.Visible = True
        arrow2.Visible = True
    End Sub
    Private Sub CountryChosen(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim y As Integer = 20

        grpCompany.Visible = True
        arrow1.Visible = True


        grpCompany.Controls.Clear()

        Dim CompanyList As List(Of String) = DirectCast(sender.tag, List(Of String))

        For Each tmpCompany As String In CompanyList
            Dim rdoButton As New Windows.Forms.RadioButton
            rdoButton.Name = "btn" & tmpCompany
            rdoButton.Checked = False
            rdoButton.Location = New Point(10, y)
            rdoButton.Text = tmpCompany

            AddHandler rdoButton.CheckedChanged, AddressOf CompanyChosen
            grpCompany.Controls.Add(rdoButton)
            y += 30
        Next

    End Sub
    Private Sub frmFirstUse_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        Dim y As Integer = 20
        grpCompany.Visible = False
        grpAdEdge.Visible = False
        arrow1.Visible = False
        arrow2.Visible = False

        For Each tmpCountry As String In CountryCompanyList.Keys
            Dim rdoButton As New Windows.Forms.RadioButton
            rdoButton.Name = "btn" & tmpCountry
            rdoButton.Checked = False
            rdoButton.Location = New Point(10, y)
            rdoButton.Text = tmpCountry
            rdoButton.Tag = CountryCompanyList(tmpCountry)
            AddHandler rdoButton.CheckedChanged, AddressOf CountryChosen
            grpCountry.Controls.Add(rdoButton)
            y += 30
        Next
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        SetupCountries()

    End Sub


    Private Sub btnSelectAdedge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SelectAdEdge.Click
        Dim AdEdgeFinder As New Windows.Forms.FolderBrowserDialog
        AdEdgeFinder.ShowDialog()
        Dim fileList As List(Of String) = IO.Directory.GetFiles(AdEdgeFinder.SelectedPath).ToList

        Dim AnalysisFound As Boolean = False

        For Each tmpFile As String In fileList
            If tmpFile.Contains("ANALYSIS.EXE") Then
                AnalysisFound = True
            End If
        Next


        If Not AnalysisFound Then
            Windows.Forms.MessageBox.Show("AdvantEdge not found in this folder. Please try again.", "Not found")
        Else
            PictureBox1.Image = My.Resources.check
            lblAdEdgeFound.Visible = True
            btnFinish.Enabled = True
            btnFinish.Visible = True

            Dim Reg As New Process
            Reg.StartInfo.FileName = "regsvr32"
            Reg.StartInfo.Arguments = "/s """ & AdEdgeFinder.SelectedPath & "Connect.dll" & """"
            Reg.Start()
            Reg.WaitForExit()
        End If

    End Sub

    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click

        Dim iniFile As String

        Dim country As Windows.Forms.RadioButton = (From tmpBtn As Windows.Forms.RadioButton In grpCountry.Controls.OfType(Of Windows.Forms.RadioButton)() Select tmpBtn Where tmpBtn.Checked = True).First
        Dim company As Windows.Forms.RadioButton = (From tmpBtn As Windows.Forms.RadioButton In grpCompany.Controls.OfType(Of Windows.Forms.RadioButton)() Select tmpBtn Where tmpBtn.Checked = True).First

        Try

            country = (From tmpBtn As Windows.Forms.RadioButton In grpCountry.Controls.OfType(Of Windows.Forms.RadioButton)() Select tmpBtn Where tmpBtn.Checked = True).First
            company = (From tmpBtn As Windows.Forms.RadioButton In grpCompany.Controls.OfType(Of Windows.Forms.RadioButton)() Select tmpBtn Where tmpBtn.Checked = True).First

        Catch ex As Exception
            Exit Sub
        End Try

        Try
            iniFile = country.Text & "_" & company.Text & ".ini"
            IO.File.Copy(iniFile, Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.UserProfile) & "Trinity 4.0\trinity.ini", True)
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message)
            Exit Sub
        End Try
       



    End Sub
End Class