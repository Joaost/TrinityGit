Imports System.Windows.Forms
Imports System.IO

Public Class frmPreferences
    'This class is a simple form window where you can set the external paths(for channel information ect),
    'name of the maker and the color scheme to use
    '

    Dim schemes() As Trinity.cColorScheme

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'sets all information from the text boxen into the settings *.INI file
        TrinitySettings.UserName = txtName.Text
        TrinitySettings.UserPhoneNr = txtPhoneNr.Text
        TrinitySettings.UserEmail = txtEmail.Text
        TrinitySettings.MarathonUser = txtMarathonUser.Text
        TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork) = txtDataPath.Text
        TrinitySettings.CampaignFiles = txtCampaignFiles.Text
        TrinitySettings.ChannelSchedules = txtChannelSchedules.Text
        TrinitySettings.SharedDataPath = txtSharedDataPath.Text
        TrinitySettings.Debug = chkDebug.Checked
        TrinitySettings.PPFirst = txtPIBFirst.Text
        TrinitySettings.PPLast = txtPIBLast.Text
        TrinitySettings.DefaultContractDatabaseID = txtContractPath.Tag
        TrinitySettings.DefaultContractPath = txtContractPath.Text
        TrinitySettings.DefaultContractLoadCosts = chkLoadCosts.Checked
        TrinitySettings.DefaultContractLoadTargets = chkLoadPrices.Checked
        TrinitySettings.DefaultContractLoadIndexes = chkLoadIndexes.Checked
        TrinitySettings.DefaultContractLoadAddedValues = chkLoadAV.Checked
        TrinitySettings.DefaultContractLoadCombinations = chkLoadCombinations.Checked
        TrinitySettings.BetaUser = chkBetaUser.Checked
        TrinitySettings.TrustedUser = chkTrustedUser.Checked

        TrinitySettings.Autosave = chkAutosave.Checked
        TrinitySettings.DefaultMonitorChart = cmbMonitor.SelectedIndex
        TrinitySettings.MatrixDatabaseServer = txtMatrixServer.Text
        TrinitySettings.MatrixDatabase = txtMatrixDB.Text
        If TrinitySettings.AdtooxUsername <> txtAdtooxUsername.Text OrElse TrinitySettings.AdtooxPassword <> txtAdtooxPassword.Text Then
            TrinitySettings.AdtooxUsername = txtAdtooxUsername.Text
            TrinitySettings.AdtooxPassword = txtAdtooxPassword.Text
            Campaign.AdToox = New Trinity.cAdtoox
        End If

        If Campaign.xmlColorSchemes.Count = 0 Then
            Dim i As Integer
            For i = 0 To schemes.Length - 1
                If i < TrinitySettings.ColorSchemeCount Then
                    TrinitySettings.Color(i + 1, "Headline") = Trinity.Helper.ConvertARGBToInt(schemes(i).HeadlineColor)
                    TrinitySettings.Color(i + 1, "Panel") = Trinity.Helper.ConvertARGBToInt(schemes(i).PanelBGColor)
                    TrinitySettings.Color(i + 1, "PanelText") = Trinity.Helper.ConvertARGBToInt(schemes(i).PanelFGColor)
                    TrinitySettings.Color(i + 1, "Text") = Trinity.Helper.ConvertARGBToInt(schemes(i).TextBGColor)
                    TrinitySettings.SchemeFont(i + 1) = schemes(i).textFont
                    TrinitySettings.ColorScheme(i + 1) = schemes(i).name
                    TrinitySettings.Color(i + 1, "Diagram1") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc1)
                    TrinitySettings.Color(i + 1, "Diagram2") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc2)
                    TrinitySettings.Color(i + 1, "Diagram3") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc3)
                    TrinitySettings.Color(i + 1, "Diagram4") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc4)
                    TrinitySettings.Color(i + 1, "Diagram5") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc5)
                    TrinitySettings.Color(i + 1, "Diagram6") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc6)
                    TrinitySettings.Color(i + 1, "Diagram7") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc7)
                    TrinitySettings.Color(i + 1, "Diagram8") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc8)
                    TrinitySettings.Color(i + 1, "Diagram9") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc9)
                    TrinitySettings.Color(i + 1, "Diagram10") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc10)
                Else
                    TrinitySettings.ColorSchemeCount = TrinitySettings.ColorSchemeCount + 1
                    TrinitySettings.ColorScheme(TrinitySettings.ColorSchemeCount) = schemes(i).name
                    TrinitySettings.Color(i + 1, "Headline") = Trinity.Helper.ConvertARGBToInt(schemes(i).HeadlineColor)
                    TrinitySettings.Color(i + 1, "Panel") = Trinity.Helper.ConvertARGBToInt(schemes(i).PanelBGColor)
                    TrinitySettings.Color(i + 1, "PanelText") = Trinity.Helper.ConvertARGBToInt(schemes(i).PanelFGColor)
                    TrinitySettings.Color(i + 1, "Text") = Trinity.Helper.ConvertARGBToInt(schemes(i).TextBGColor)
                    TrinitySettings.Color(i + 1, "Diagram1") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc1)
                    TrinitySettings.Color(i + 1, "Diagram2") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc2)
                    TrinitySettings.Color(i + 1, "Diagram3") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc3)
                    TrinitySettings.Color(i + 1, "Diagram4") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc4)
                    TrinitySettings.Color(i + 1, "Diagram5") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc5)
                    TrinitySettings.Color(i + 1, "Diagram6") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc6)
                    TrinitySettings.Color(i + 1, "Diagram7") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc7)
                    TrinitySettings.Color(i + 1, "Diagram8") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc8)
                    TrinitySettings.Color(i + 1, "Diagram9") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc9)
                    TrinitySettings.Color(i + 1, "Diagram10") = Trinity.Helper.ConvertARGBToInt(schemes(i).pbc10)
                End If
            Next
        Else
            Campaign.xmlColorSchemes.SaveColorSchemes()
        End If


        TrinitySettings.LocalDataBase = txtLocalDB.Text
        TrinitySettings.NetworkDataBase = txtNetworkDB.Text

        'save changes to your profile in the palnner/buyer list
        Dim xmldoc As New Xml.XmlDocument
        xmldoc.Load(Trinity.Helper.Pathify(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)) & "people.xml")

        Dim planners As Xml.XmlNode
        Dim buyers As Xml.XmlNode

        Dim Plist As New List(Of String)
        Dim Blist As New List(Of String)

        Dim bolFound As Boolean = False

        planners = xmldoc.GetElementsByTagName("planners").Item(0)
        buyers = xmldoc.GetElementsByTagName("buyers").Item(0)

        Dim xmlTmp As Xml.XmlElement
        For Each xmlTmp In planners.ChildNodes
            Plist.Add(xmlTmp.GetAttribute("name"))

            'if we found the person we update the information
            If xmlTmp.GetAttribute("name") = TrinitySettings.UserName Then
                xmlTmp.SetAttribute("phone", TrinitySettings.UserPhoneNr)
                xmlTmp.SetAttribute("email", TrinitySettings.UserEmail)
                bolFound = True
            End If
        Next

        For Each xmlTmp In buyers.ChildNodes
            Blist.Add(xmlTmp.GetAttribute("name"))

            'if we found the person we update the information
            If xmlTmp.GetAttribute("name") = TrinitySettings.UserName Then
                xmlTmp.SetAttribute("phone", TrinitySettings.UserPhoneNr)
                xmlTmp.SetAttribute("email", TrinitySettings.UserEmail)
                bolFound = True
            End If
        Next

        'if we have a new person we need to interact with the user
        If Not bolFound Then
            Dim frm As New Form
            frm.Width = 450
            frm.Height = 350

            Dim lbl As New Label
            lbl.Text = "If you have changed your name please mark the name(s) in the list below and press OK"
            Dim lbl2 As New Label
            lbl2.Text = "If you are a new user then check the box(es) for the group you want to belong to"

            frm.Controls.Add(lbl)
            frm.Controls.Add(lbl2)

            Dim lblP As New Label
            lblP.Text = "Planners:"

            Dim lblb As New Label
            lblb.Text = "Buyers:"

            frm.Controls.Add(lblb)
            frm.Controls.Add(lblP)

            lblb.Left = 200
            lblb.Top = 80

            lblP.Left = 10
            lblP.Top = 80

            lbl.Left = 5
            lbl.Top = 20
            lbl.Width = 450

            lbl2.Left = 5
            lbl2.Top = 40
            lbl2.Width = 450

            Dim lp As New ListBox
            lp.DataSource = Plist
            lp.ClearSelected()
            lp.Height = 130

            Dim lb As New ListBox
            lb.DataSource = Blist
            lb.ClearSelected()
            lb.Height = 130

            frm.Controls.Add(lp)
            frm.Controls.Add(lb)

            lp.Left = 10
            lp.Top = 105

            lb.Left = 200
            lb.Top = 105

            Dim b As New Button
            b.Text = "OK"

            frm.Controls.Add(b)

            b.DialogResult = Windows.Forms.DialogResult.OK
            b.Left = 300
            b.Top = 280

            Dim bc As New Button
            bc.Text = "Cancel"

            frm.Controls.Add(bc)

            bc.DialogResult = Windows.Forms.DialogResult.Cancel
            bc.Left = 200
            bc.Top = 280

            Dim chP As New CheckBox
            chP.Text = "Add me as new Planner"
            frm.Controls.Add(chP)
            chP.Width = 150
            chP.Top = 240
            chP.Left = 10

            Dim chb As New CheckBox
            chb.Text = "Add me as new Buyer"
            frm.Controls.Add(chb)
            chb.Width = 150
            chb.Top = 240
            chb.Left = 200


            If frm.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub


            If chP.Checked Then
                'we add the user as new planner
                xmlTmp = xmldoc.CreateElement("planner")
                xmlTmp.SetAttribute("name", TrinitySettings.UserName)
                xmlTmp.SetAttribute("phone", TrinitySettings.UserPhoneNr)
                xmlTmp.SetAttribute("email", TrinitySettings.UserEmail)
                planners.AppendChild(xmlTmp)
            Else
                'we update the information
                Dim name As String = lp.SelectedItem
                For Each xmlTmp In planners
                    'if we found the person we update the information
                    If xmlTmp.GetAttribute("name") = name Then
                        xmlTmp.SetAttribute("name", TrinitySettings.UserName)
                        xmlTmp.SetAttribute("phone", TrinitySettings.UserPhoneNr)
                        xmlTmp.SetAttribute("email", TrinitySettings.UserEmail)
                    End If
                Next
            End If

            If chb.Checked Then
                'we add the user as new buyer
                xmlTmp = xmldoc.CreateElement("buyer")
                xmlTmp.SetAttribute("name", TrinitySettings.UserName)
                xmlTmp.SetAttribute("phone", TrinitySettings.UserPhoneNr)
                xmlTmp.SetAttribute("email", TrinitySettings.UserEmail)
                buyers.AppendChild(xmlTmp)
            Else
                'we update the information
                Dim name As String = lb.SelectedItem
                For Each xmlTmp In buyers
                    'if we found the person we update the information
                    If xmlTmp.GetAttribute("name") = name Then
                        xmlTmp.SetAttribute("name", TrinitySettings.UserName)
                        xmlTmp.SetAttribute("phone", TrinitySettings.UserPhoneNr)
                        xmlTmp.SetAttribute("email", TrinitySettings.UserEmail)
                    End If
                Next
            End If

            'we save the XML file
            xmldoc.Save(Trinity.Helper.Pathify(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)) & "people.xml")
        End If



        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmPreferences_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Stop
    End Sub

    Private Sub frmPreferences_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'on load we gather all information from the settings *.INI file and fill up the text boxes with the information
        txtName.Text = TrinitySettings.UserName
        txtPhoneNr.Text = TrinitySettings.UserPhoneNr
        txtEmail.Text = TrinitySettings.UserEmail
        txtMarathonUser.Text = TrinitySettings.MarathonUser
        If TrinitySettings.AdtooxEnabled Then
            txtAdtooxUsername.Text = TrinitySettings.AdtooxUsername
            txtAdtooxPassword.Text = TrinitySettings.AdtooxPassword
            tpAdtoox.Visible = True
            If Not tabPref.TabPages.Contains(tpAdtoox) Then
                tabPref.TabPages.Add(tpAdtoox)
            End If
        Else
            tpAdtoox.Visible = False
            tabPref.TabPages.Remove(tpAdtoox)
        End If
        txtDataPath.Text = TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)
        txtCampaignFiles.Text = TrinitySettings.CampaignFiles
        txtChannelSchedules.Text = TrinitySettings.ChannelSchedules
        txtNetworkDB.Text = TrinitySettings.NetworkDataBase
        txtLocalDB.Text = TrinitySettings.LocalDataBase
        txtSharedDataPath.Text = TrinitySettings.SharedDataPath

        txtContractPath.Enabled = TrinitySettings.SaveCampaignsAsFiles

        txtContractPath.Tag = TrinitySettings.DefaultContractDatabaseID
        txtContractPath.Text = TrinitySettings.DefaultContractPath
        chkLoadCosts.Checked = TrinitySettings.DefaultContractLoadCosts
        chkLoadPrices.Checked = TrinitySettings.DefaultContractLoadTargets
        chkLoadIndexes.Checked = TrinitySettings.DefaultContractLoadIndexes
        chkLoadAV.Checked = TrinitySettings.DefaultContractLoadAddedValues
        chkLoadCombinations.Checked = TrinitySettings.DefaultContractLoadCombinations
        chkAutosave.Checked = TrinitySettings.Autosave
        chkBetaUser.Checked = TrinitySettings.BetaUser
        chkTrustedUser.Checked = TrinitySettings.TrustedUser

        chkErrorChecking.Checked = TrinitySettings.ErrorChecking


        txtMatrixDB.Text = TrinitySettings.MatrixDatabase
        txtMatrixServer.Text = TrinitySettings.MatrixDatabaseServer

        cmbMonitor.SelectedIndex = TrinitySettings.DefaultMonitorChart

        chkDebug.Checked = TrinitySettings.Debug
        'chkMarathon.Checked = TrinitySettings.MarathonEnabled
        txtPIBFirst.Text = TrinitySettings.PPFirst
        txtPIBLast.Text = TrinitySettings.PPLast

        If DBReader.getDBType = 1 Then 'new version of DB
            'cmdChangePwd.Visible = True
        Else
            cmdChangePwd.Visible = False
        End If


        readSchemes()
        If cmbColorScheme.Items.Count > 0 Then
            cmbColorScheme.SelectedIndex = 0
        End If


        'This code creates a file named people.xml to replace the old people.pst file.
        'This code was entered 2007-06-27
        If Not System.IO.File.Exists(Trinity.Helper.Pathify(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)) & "people.xml") Then
            Dim list As New List(Of String)


            'if the file dont exist we skip this part
            If System.IO.File.Exists(Trinity.Helper.Pathify(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)) & "people.lst") Then
                'declaring a FileStream to open the file named txt with access mode of reading
                Dim fsRead As New FileStream(Trinity.Helper.Pathify(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)) & "people.lst", FileMode.Open, FileAccess.Read)

                'creating a new StreamReader and passing the filestream object fsRead as argument
                Dim sr As New StreamReader(fsRead, System.Text.Encoding.UTF7)

                'set the pointer in the beginning of the file
                sr.BaseStream.Seek(0, SeekOrigin.Begin)

                Dim line As String
                Do
                    line = sr.ReadLine()
                    If Not line Is Nothing Then
                        list.Add(line)
                    End If
                Loop Until line Is Nothing
                sr.Close()
            End If


            Dim xmldoc As New Xml.XmlDocument
            Dim xmlData As XmlElement
            Dim xmlPlanners As XmlElement
            Dim xmlBuyers As XmlElement
            Dim xmle As XmlElement

            Dim Node As Object = xmldoc.CreateProcessingInstruction("xml", "version='1.0'")

            xmldoc.AppendChild(Node)

            xmlData = xmldoc.CreateElement("data")
            xmlPlanners = xmldoc.CreateElement("planners")
            xmlBuyers = xmldoc.CreateElement("buyers")

            For Each s As String In list
                'add as planner
                xmle = xmldoc.CreateElement("planner")
                xmle.SetAttribute("name", s)
                xmlPlanners.AppendChild(xmle)

                'add as planner
                xmle = xmldoc.CreateElement("buyer")
                xmle.SetAttribute("name", s)
                xmlBuyers.AppendChild(xmle)

            Next

            xmlData.AppendChild(xmlPlanners)
            xmlData.AppendChild(xmlBuyers)

            xmldoc.AppendChild(xmlData)

            xmldoc.Save(Trinity.Helper.Pathify(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)) & "people.xml")
        End If
    End Sub

    Private Sub readSchemes()

        If Campaign.xmlColorSchemes.ReadColorSchemes Then
            If Not cmbColorScheme.Items.Count = Campaign.xmlColorSchemes.Count Then
                For Each scheme As Trinity.cColorScheme In Campaign.xmlColorSchemes
                    cmbColorScheme.Items.Add(scheme.name)
                Next
            End If
            Exit Sub
        End If


        Dim i As Integer
        cmbColorScheme.Items.Clear()
        Dim schemes2() As Trinity.cColorScheme
        For i = 0 To TrinitySettings.ColorSchemeCount - 1
            If schemes2 IsNot Nothing Then
                ReDim Preserve schemes2(schemes2.Length)
            Else
                ReDim schemes2(0)
            End If
            schemes2(i) = New Trinity.cColorScheme
            schemes2(i).HeadlineColor = Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(i + 1, "Headline"))
            schemes2(i).PanelBGColor = Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(i + 1, "Panel"))
            schemes2(i).PanelFGColor = Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(i + 1, "PanelText"))
            schemes2(i).TextBGColor = Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(i + 1, "Text"))
            schemes2(i).textFont = TrinitySettings.SchemeFont(i + 1)
            schemes2(i).name = TrinitySettings.ColorScheme(i + 1)

            schemes2(i).pbc1 = Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(i + 1, "Diagram1"))
            schemes2(i).pbc2 = Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(i + 1, "Diagram2"))
            schemes2(i).pbc3 = Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(i + 1, "Diagram3"))
            schemes2(i).pbc4 = Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(i + 1, "Diagram4"))
            schemes2(i).pbc5 = Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(i + 1, "Diagram5"))
            schemes2(i).pbc6 = Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(i + 1, "Diagram6"))
            schemes2(i).pbc7 = Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(i + 1, "Diagram7"))
            schemes2(i).pbc8 = Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(i + 1, "Diagram8"))
            schemes2(i).pbc9 = Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(i + 1, "Diagram9"))
            schemes2(i).pbc10 = Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(i + 1, "Diagram10"))

            cmbColorScheme.Items.Add(schemes2(i).name)
            schemes = schemes2
        Next
    End Sub

    Private Sub picHeadline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picHeadline.Click
        dlgColor.Color = picHeadline.BackColor
        If dlgColor.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        picHeadline.BackColor = dlgColor.Color

        If Campaign.xmlColorSchemes.Count = 0 Then
            schemes(cmbColorScheme.Tag).HeadlineColor = dlgColor.Color
        Else
            Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).headlinecolor = dlgColor.Color
        End If


    End Sub

    Private Sub picPanelBG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picPanelBG.Click
        dlgColor.Color = picPanelBG.BackColor
        If dlgColor.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        picPanelBG.BackColor = dlgColor.Color

        If Campaign.xmlColorSchemes.Count = 0 Then
            schemes(cmbColorScheme.Tag).PanelBGColor = dlgColor.Color
        Else
            DirectCast(Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex), Trinity.cColorScheme).PanelBGColor = dlgColor.Color
        End If

    End Sub

    Private Sub picPanelFG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picPanelFG.Click
        dlgColor.Color = picPanelFG.BackColor
        If dlgColor.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        picPanelFG.BackColor = dlgColor.Color


        If Campaign.xmlColorSchemes.Count = 0 Then
            schemes(cmbColorScheme.Tag).PanelFGColor = dlgColor.Color
        Else
            DirectCast(Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex), Trinity.cColorScheme).PanelFGColor = dlgColor.Color
        End If

    End Sub

    Private Sub picText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picText.Click
        dlgColor.Color = picText.BackColor
        If dlgColor.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        picText.BackColor = dlgColor.Color


        If Campaign.xmlColorSchemes.Count = 0 Then
            schemes(cmbColorScheme.Tag).TextBGColor = dlgColor.Color
        Else
            DirectCast(Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex), Trinity.cColorScheme).TextBGColor = dlgColor.Color
        End If

    End Sub

    Private Sub cmbColorScheme_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbColorScheme.KeyUp

        If Campaign.xmlColorSchemes.Count = 0 Then
            cmbColorScheme.Items(cmbColorScheme.Tag) = cmbColorScheme.Text
            'TrinitySettings.ColorScheme(cmbColorScheme.Tag + 1) = cmbColorScheme.Text
            schemes(cmbColorScheme.Tag).name = cmbColorScheme.Text
        Else
            Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).name = cmbColorScheme.Text
        End If

    End Sub

    Private Sub cmbColorScheme_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbColorScheme.SelectedIndexChanged

        If Campaign.xmlColorSchemes.Count = 0 Then
            picHeadline.BackColor = schemes(cmbColorScheme.SelectedIndex).HeadlineColor
            picPanelBG.BackColor = schemes(cmbColorScheme.SelectedIndex).PanelBGColor
            picPanelFG.BackColor = schemes(cmbColorScheme.SelectedIndex).PanelFGColor
            picText.BackColor = schemes(cmbColorScheme.SelectedIndex).TextBGColor
            cmdFont.Text = schemes(cmbColorScheme.SelectedIndex).textFont
            cmbColorScheme.Tag = cmbColorScheme.SelectedIndex
            pbc1.BackColor = schemes(cmbColorScheme.SelectedIndex).pbc1
            pbc2.BackColor = schemes(cmbColorScheme.SelectedIndex).pbc2
            pbc3.BackColor = schemes(cmbColorScheme.SelectedIndex).pbc3
            pbc4.BackColor = schemes(cmbColorScheme.SelectedIndex).pbc4
            pbc5.BackColor = schemes(cmbColorScheme.SelectedIndex).pbc5
            pbc6.BackColor = schemes(cmbColorScheme.SelectedIndex).pbc6
            pbc7.BackColor = schemes(cmbColorScheme.SelectedIndex).pbc7
            pbc8.BackColor = schemes(cmbColorScheme.SelectedIndex).pbc8
            pbc9.BackColor = schemes(cmbColorScheme.SelectedIndex).pbc9
            pbc10.BackColor = schemes(cmbColorScheme.SelectedIndex).pbc10
        Else
            picHeadline.BackColor = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).HeadlineColor
            picPanelBG.BackColor = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).PanelBGColor
            picPanelFG.BackColor = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).PanelFGColor
            picText.BackColor = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).TextBGColor
            cmdFont.Text = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).textFont
            cmbColorScheme.Tag = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex)
            pbc1.BackColor = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).pbc1
            pbc2.BackColor = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).pbc2
            pbc3.BackColor = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).pbc3
            pbc4.BackColor = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).pbc4
            pbc5.BackColor = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).pbc5
            pbc6.BackColor = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).pbc6
            pbc7.BackColor = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).pbc7
            pbc8.BackColor = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).pbc8
            pbc9.BackColor = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).pbc9
            pbc10.BackColor = Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex).pbc10
        End If
    End Sub

    Private Sub cmdAddColorScheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddColorScheme.Click
        If Campaign.xmlColorSchemes.Count = 0 Then
            ReDim Preserve schemes(schemes.Length)
            schemes(schemes.Length - 1) = New Trinity.cColorScheme
            schemes(schemes.Length - 1).name = "New Scheme"
            cmbColorScheme.Items.Add(schemes(schemes.Length - 1).name)
            cmbColorScheme.SelectedIndex = cmbColorScheme.Items.Count - 1
        Else
            Dim newScheme As Trinity.cColorScheme = New Trinity.cColorScheme("New Scheme")
            cmbColorScheme.Items.Add(newScheme.name)
            Campaign.xmlColorSchemes.Add(newScheme)
            cmbColorScheme.SelectedIndex = Campaign.xmlColorSchemes.Count - 1
        End If
    End Sub

    Private Sub cmdBrowseNetworkPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseNetworkPath.Click
        dlgBrowse.SelectedPath = txtDataPath.Text
        If dlgBrowse.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtDataPath.Text = dlgBrowse.SelectedPath
        End If
    End Sub

    Private Sub cmdChannelSchedules_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannelSchedules.Click
        dlgBrowse.SelectedPath = txtChannelSchedules.Text
        If dlgBrowse.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtChannelSchedules.Text = dlgBrowse.SelectedPath
        End If
    End Sub

    Private Sub cmdCampaignFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCampaignFiles.Click
        dlgBrowse.SelectedPath = txtCampaignFiles.Text
        If dlgBrowse.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtCampaignFiles.Text = dlgBrowse.SelectedPath
        End If
    End Sub

    Private Sub cmdFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFont.Click
        Dim dlgFont As New System.Windows.Forms.FontDialog
        dlgFont.Font = New System.Drawing.Font(cmdFont.Text, 10)
        dlgFont.MaxSize = 10
        dlgFont.MinSize = 10
        dlgFont.ShowColor = False
        dlgFont.ShowApply = False
        dlgFont.ShowEffects = False
        If dlgFont.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        cmdFont.Text = dlgFont.Font.Name
        schemes(cmbColorScheme.Tag).textFont = dlgFont.Font.Name
    End Sub

    Private Sub cmdBrowseNetworkDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseNetworkDB.Click
        If txtNetworkDB.Text <> "SQL" Then
            Dim TmpDialog As New Windows.Forms.OpenFileDialog
            TmpDialog.Filter = "Access databases|*.mdb|Sql databases|*.*"
            If TmpDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                txtNetworkDB.Text = TmpDialog.FileName
                Windows.Forms.MessageBox.Show("This database will be used the next time you start Trinity.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            Dim ConnectionString As String = InputBox("Connection string:", "T R I N I T Y", TrinitySettings.ConnectionString(Trinity.cSettings.SettingsLocationEnum.locNetwork))
            If ConnectionString <> "" AndAlso ConnectionString <> TrinitySettings.ConnectionString(Trinity.cSettings.SettingsLocationEnum.locNetwork) Then
                Windows.Forms.MessageBox.Show("This database will be used the next time you start Trinity.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TrinitySettings.ConnectionString(Trinity.cSettings.SettingsLocationEnum.locNetwork) = ConnectionString
            End If
        End If
    End Sub

    Private Sub cmdBrowseLocalDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseLocalDB.Click
        Dim TmpDialog As New Windows.Forms.OpenFileDialog
        TmpDialog.Filter = "SqlCe databases|*.sdf"
        If TmpDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtLocalDB.Text = TmpDialog.FileName
            Windows.Forms.MessageBox.Show("This database will be used the next time you start Trinity.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub cmdSync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmSync
        frm.ShowDialog()
    End Sub


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub pbc1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbc1.Click
        dlgColor.Color = pbc1.BackColor
        If dlgColor.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        pbc1.BackColor = dlgColor.Color


        If Campaign.xmlColorSchemes.Count = 0 Then
            schemes(cmbColorScheme.Tag).pbc1 = dlgColor.Color
        Else
            DirectCast(Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex), Trinity.cColorScheme).pbc1 = dlgColor.Color
        End If

    End Sub

    Private Sub pbc2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbc2.Click
        dlgColor.Color = pbc2.BackColor
        If dlgColor.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        pbc2.BackColor = dlgColor.Color

        If Campaign.xmlColorSchemes.Count = 0 Then
            schemes(cmbColorScheme.Tag).pbc2 = dlgColor.Color
        Else
            DirectCast(Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex), Trinity.cColorScheme).pbc2 = dlgColor.Color
        End If

    End Sub

    Private Sub pbc3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbc3.Click
        dlgColor.Color = pbc3.BackColor
        If dlgColor.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        pbc3.BackColor = dlgColor.Color


        If Campaign.xmlColorSchemes.Count = 0 Then
            schemes(cmbColorScheme.Tag).pbc3 = dlgColor.Color
        Else
            DirectCast(Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex), Trinity.cColorScheme).pbc3 = dlgColor.Color
        End If

    End Sub

    Private Sub pbc4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbc4.Click
        dlgColor.Color = pbc4.BackColor
        If dlgColor.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        pbc4.BackColor = dlgColor.Color


        If Campaign.xmlColorSchemes.Count = 0 Then
            schemes(cmbColorScheme.Tag).pbc4 = dlgColor.Color
        Else
            DirectCast(Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex), Trinity.cColorScheme).pbc4 = dlgColor.Color
        End If

    End Sub

    Private Sub pbc5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbc5.Click
        dlgColor.Color = pbc5.BackColor
        If dlgColor.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        pbc5.BackColor = dlgColor.Color

        If Campaign.xmlColorSchemes.Count = 0 Then
            schemes(cmbColorScheme.Tag).pbc5 = dlgColor.Color
        Else
            DirectCast(Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex), Trinity.cColorScheme).pbc5 = dlgColor.Color
        End If

    End Sub

    Private Sub pbc6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbc6.Click
        dlgColor.Color = pbc6.BackColor
        If dlgColor.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        pbc6.BackColor = dlgColor.Color

        If Campaign.xmlColorSchemes.Count = 0 Then
            schemes(cmbColorScheme.Tag).pbc6 = dlgColor.Color
        Else
            DirectCast(Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex), Trinity.cColorScheme).pbc6 = dlgColor.Color
        End If

    End Sub

    Private Sub pbc7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbc7.Click
        dlgColor.Color = pbc7.BackColor
        If dlgColor.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        pbc7.BackColor = dlgColor.Color

        If Campaign.xmlColorSchemes.Count = 0 Then
            schemes(cmbColorScheme.Tag).pbc7 = dlgColor.Color
        Else
            DirectCast(Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex), Trinity.cColorScheme).pbc7 = dlgColor.Color
        End If

    End Sub

    Private Sub pbc8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbc8.Click
        dlgColor.Color = pbc8.BackColor
        If dlgColor.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        pbc8.BackColor = dlgColor.Color

        If Campaign.xmlColorSchemes.Count = 0 Then
            schemes(cmbColorScheme.Tag).pbc8 = dlgColor.Color
        Else
            DirectCast(Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex), Trinity.cColorScheme).pbc8 = dlgColor.Color
        End If
    End Sub

    Private Sub pbc9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbc9.Click
        dlgColor.Color = pbc9.BackColor
        If dlgColor.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        pbc9.BackColor = dlgColor.Color

        If Campaign.xmlColorSchemes.Count = 0 Then
            schemes(cmbColorScheme.Tag).pbc9 = dlgColor.Color
        Else
            DirectCast(Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex), Trinity.cColorScheme).pbc9 = dlgColor.Color
        End If
    End Sub

    Private Sub pbc10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbc10.Click
        dlgColor.Color = pbc10.BackColor
        If dlgColor.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        pbc10.BackColor = dlgColor.Color

        If Campaign.xmlColorSchemes.Count = 0 Then
            schemes(cmbColorScheme.Tag).pbc10 = dlgColor.Color
        Else
            DirectCast(Campaign.xmlColorSchemes(cmbColorScheme.SelectedIndex), Trinity.cColorScheme).pbc10 = dlgColor.Color
        End If
    End Sub

    Private Sub cmdPwd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChangePwd.Click
        lblPwd.Text = "Enter your OLD password and press 'Submit'"
        lblPwd.Visible = True
        txtPwd.Visible = True
        cmdChangePwd.Visible = True
        txtPwd.Tag = "OLD"
    End Sub

    Private Sub cmdSavePwd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSavePwd.Click
        If txtPwd.Tag = "OLD" Then
            If DBReader.checkPwd(TrinitySettings.UserID, txtPwd.Text) Then
                lblPwd.Text = "Enter your NEW password and press 'Submit'"
                txtPwd.Tag = "NEW"
            Else
                lblPwd.Text = "You entered the wrong password, please enter your OLD one"
            End If
        Else
            If DBReader.updatePwd(txtPwd.Text, TrinitySettings.UserID) Then
                lblPwd.Text = "Your password was updated"
                txtPwd.Visible = False
                cmdSavePwd.Visible = False
            Else
                lblPwd.Text = "Error updateing password"
            End If
        End If
    End Sub

    Private Sub cmdBrowseContract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseContract.Click
        If TrinitySettings.SaveCampaignsAsFiles Then
            Dim TmpDialog As New Windows.Forms.OpenFileDialog
            TmpDialog.Filter = "Trinity Contracts|*.tct"
            If TmpDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                txtContractPath.Text = TmpDialog.FileName
            End If
        Else
            Dim _dialog As New frmSelectContract
            If _dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                txtContractPath.Text = _dialog.grdContracts.SelectedRows(0).Tag!Name
                txtContractPath.Tag = _dialog.grdContracts.SelectedRows(0).Tag!Id
            End If
        End If
    End Sub

    Private Sub cmdSyncNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSyncNow.Click
        Trinity.Helper.SyncLocalData()
    End Sub

    Private Sub cmdBrowseSharedPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseSharedPath.Click
        dlgBrowse.SelectedPath = txtSharedDataPath.Text
        If dlgBrowse.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtSharedDataPath.Text = dlgBrowse.SelectedPath
        End If
    End Sub

    Private Sub chkErrorChecking_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkErrorChecking.CheckedChanged
        Campaign.ErrorCheckingEnabled = chkErrorChecking.Checked
        frmMain.icnProblems.Visible = chkErrorChecking.Checked
    End Sub

    Private Sub txtContractPath_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtContractPath.TextChanged

    End Sub

    Private Sub chkTrustedUser_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkTrustedUser.CheckedChanged
        If chkTrustedUser.Checked AndAlso Not TrinitySettings.TrustedUser Then
            Dim _pass As String = InputBox("A password is needed to enable the Trusted user setting:", "T R I N I T Y", "")
            If _pass <> "" Then
                If Not _pass = "arsenal" Then
                    Windows.Forms.MessageBox.Show("Invalid password. Please contact 'trinity@mecglobal.com' if you feel you should have this setting.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    chkTrustedUser.Checked = False
                End If
            End If
        End If
    End Sub
End Class


