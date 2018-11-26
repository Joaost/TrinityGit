Imports System.Windows.Forms
Imports System.IO

Public Class frmNewUser

    Private Sub frmNewUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'copy the UserSettings.ini from the programfolder to my local folder
        Dim strUserPath As String = Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.UserProfile)

        Dim bol As Boolean = CreateFolder(strUserPath & "Trinity 4.0\")
        File.Copy(My.Application.Info.DirectoryPath & "\UserSettings.ini", strUserPath & "Trinity 4.0\Trinity.ini")


        'create a settings object
        TrinitySettings = New Trinity.cSettings(Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.UserProfile) & "Trinity 4.0")

        'only show marathon name if marathon is enabled
        If TrinitySettings.MarathonEnabled Then
            txtMarathonUser.Visible = True
            Label15.Visible = True
        Else
            txtMarathonUser.Visible = False
            Label15.Visible = False
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

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        'check all the text boxes

        If txtName.Text = "" Then
            MsgBox("You need to fill in your name")
            Exit Sub
        End If
        If txtPhoneNr.Text = "" Then
            MsgBox("You need to fill in your phone number")
            Exit Sub
        End If
        If txtEmail.Text = "" Then
            MsgBox("You need to fill in your email")
            Exit Sub
        End If
        If TrinitySettings.MarathonEnabled Then
            If txtMarathonUser.Text = "" Then
                MsgBox("You need to fill in your Marathon name")
                Exit Sub
            End If
        End If

        'save the user input
        TrinitySettings.UserName = txtName.Text
        TrinitySettings.UserPhoneNr = txtPhoneNr.Text
        TrinitySettings.UserEmail = txtEmail.Text
        If TrinitySettings.MarathonEnabled Then
            TrinitySettings.MarathonUser = txtMarathonUser.Text
        End If


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

            lbl.Left = 5
            lbl.Top = 20
            lbl.Width = 450

            lbl2.Left = 5
            lbl2.Top = 40
            lbl2.Width = 450

            Dim lp As New ListBox
            lp.DataSource = Plist
            lp.ClearSelected()

            Dim lb As New ListBox
            lb.DataSource = Blist
            lb.ClearSelected()

            frm.Controls.Add(lp)
            frm.Controls.Add(lb)

            lp.Left = 10
            lp.Top = 80

            lb.Left = 200
            lb.Top = 80

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
            chP.Width = 100
            chP.Top = 240
            chP.Left = 10

            Dim chb As New CheckBox
            chb.Text = "Add me as new Buyer"
            frm.Controls.Add(chb)
            chb.Width = 100
            chb.Top = 260
            chb.Left = 10


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

    Public Function CreateFolder(ByVal destDir As String) As Boolean

        Dim i As Long
        Dim prevDir As String = ""

        On Error Resume Next

        For i = Len(destDir) To 1 Step -1
            If Mid(destDir, i, 1) = "\" Then
                prevDir = Microsoft.VisualBasic.Left(destDir, i - 1)
                Exit For
            End If
        Next i

        If prevDir = "" Then CreateFolder = False : Exit Function
        If Not Len(Dir(prevDir & "\", vbDirectory)) > 0 Then
            If Not CreateFolder(prevDir) Then CreateFolder = False : Exit Function
        Else
            CreateFolder = True
            Exit Function
        End If

        On Error GoTo errDirMake
        MkDir(destDir)
        CreateFolder = True
        Exit Function

errDirMake:
        CreateFolder = False

    End Function
End Class