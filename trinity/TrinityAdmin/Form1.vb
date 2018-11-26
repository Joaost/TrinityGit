Imports System.Globalization.CultureInfo
Imports System.IO

Public Class Form1
    Dim dDate As New Date

    Dim XMLDoc As Xml.XmlDocument = New Xml.XmlDocument
    Dim XMLHead As Xml.XmlElement
    Dim XMLSub As Xml.XmlElement
    Dim XmlUpdate As Xml.XmlElement
    Dim strFiles As New List(Of String)
    Dim strMeta As New List(Of String)
    Dim strVersion As New List(Of String)
    Dim strSIFO As New List(Of String)
    Dim strProg As New List(Of String)

    Private XmlLog As Xml.XmlDocument

    Dim penLvl As List(Of String)

    Private Sub cmdImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdImport.Click
        frmImport.Show()
    End Sub

    Private Sub cmdExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExport.Click
        frmExport.Show()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'check what DB is supposed to be used
        TrinitySettings = New Trinity.cSettings(Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.UserProfile) & "Trinity 4.0")

        campaign = New Trinity.cKampanj
        TrinitySettings.MainObject = campaign
        campaign.CreateChannels()

        Dim s As String
        s = TrinitySettings.DataBase(Trinity.cSettings.SettingsLocationEnum.locNetwork)
        If s.Substring(s.Length - 3, 3).ToUpper = "MDB" Then
            DBReader = New Trinity.cDBReaderAccess
        ElseIf TrinitySettings.DataBase(Trinity.cSettings.SettingsLocationEnum.locNetwork).ToUpper = "SQL" Then
            DBReader = New Trinity.cDBReaderSQL
        Else
            Stop 'no other types available
        End If

        'connect the handler to the database
        DBReader.Connect(Trinity.cDBReader.ConnectionPlace.Network)

        If Not DBReader.alive Then
            MsgBox("Could not connect to database", MsgBoxStyle.Critical, "Error connecting")
            Exit Sub
        End If

        dDate = Date.Now
        XmlUpdate = XMLDoc.CreateElement("Update")
        XmlUpdate.SetAttribute("Date", CStr(dDate.Date).Replace("-", ""))
    End Sub

    Private Sub closeing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        DBReader.closeConnection()
        DBReader = Nothing
    End Sub

    Private Sub cmdINI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdINI.Click
        Dim frm As New frmINI
        frm.XmlDoc = XMLDoc
        If frm.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        'if ok the add the nodes
        Dim xe As Xml.XmlElement
        Dim l As Xml.XmlElement = frm.Xml
        xe = l.FirstChild
        While Not xe Is Nothing
            XmlUpdate.AppendChild(xe)
            xe = l.ChildNodes.Item(0)
        End While

    End Sub

    Private Sub cmdSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSchedule.Click
        Dim frm As New frmImport
        frm.XmlDoc = XMLDoc
        frm.uploadList = strFiles

        If frm.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        'if ok the add the nodes
        Dim xe As Xml.XmlElement
        Dim l As Xml.XmlElement = frm.Xml
        xe = l.FirstChild
        While Not xe Is Nothing
            XmlUpdate.AppendChild(xe)
            xe = l.ChildNodes.Item(0)
        End While

        'retrive the added files aswell
        For Each s As String In frm.uploadList
            strMeta.Add(s)
        Next

    End Sub

    Private Sub cmdUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpload.Click

        dDate = Date.Now
        Dim h As String = dDate.Hour
        If h.Length = 1 Then h = "0" & h
        Dim m As String = dDate.Minute
        If m.Length = 1 Then m = "0" & m
        Dim tim As String = h & m
        If tim.Length < 4 Then
            MsgBox("Fel tid")
        End If
        XmlUpdate.SetAttribute("Time", tim)

        Dim ini As New Trinity.clsIni
        ini.Create(My.Application.Info.DirectoryPath & "\admin.ini")

        'set up the FTP object
        Dim ftp As New cFTP
        'note that i include the subpath in the servername!!!
        ftp.ftpAdress = ini.ReadString("FTP", "Adress")
        ftp.userName = Trinity.Helper.Decode(ini.ReadString("FTP", "Username"))
        ftp.password = Trinity.Helper.Decode(ini.ReadString("FTP", "Password"))

        Dim iFailed As Integer = 0
        Dim iSuccess As Integer = 0
        Dim j As Integer
        Dim s As String

        For j = 0 To penLvl.Count - 1
            'get the TrinityVersion connected to this company/country
            If ftp.downloadFile(penLvl(j) & "/TrinityVersion.xml", My.Application.Info.DirectoryPath & "\") Then
                Dim xmlD As New Xml.XmlDocument
                xmlD.Load(My.Application.Info.DirectoryPath & "\TrinityVersion.xml")
                XMLHead = xmlD.GetElementsByTagName("Updates").Item(0)
                XMLSub = XMLHead.GetElementsByTagName("New").Item(0)

                'a conversion because the xmlupdate is from another context
                Dim xe As Xml.XmlElement = xmlD.CreateElement("tmp")
                xe = xmlD.ImportNode(XmlUpdate, True)
                XMLSub.AppendChild(xe)
                xmlD.Save(My.Application.Info.DirectoryPath & "\TrinityVersion.xml")

            Else
                MsgBox("Error reading TrinityVersion file", MsgBoxStyle.Critical, "ERROR")
                Exit Sub
            End If

            For Each str As String In strFiles
                'upload file 
                s = penLvl(j)
                If Not str.IndexOf("Trinity Data 4.0") = -1 Then
                    s = s & str.Substring(str.IndexOf("Trinity Data 4.0") + 16).Replace("\", "/")
                    s = s.Substring(0, s.LastIndexOf("/")) 'deletes the filename
                End If
                If ftp.uploadFile(str, s) Then
                    iSuccess += 1
                Else
                    iFailed += 1
                End If
            Next
            For Each str As String In strProg
                'upload file 
                s = penLvl(j) & "/files/"
                If ftp.uploadFile(str, s) Then
                    iSuccess += 1
                Else
                    iFailed += 1
                End If
            Next
            For Each str As String In strVersion
                'upload file 
                s = penLvl(j) & "/latestVersion/"
                If ftp.uploadFile(str, s) Then
                    iSuccess += 1
                Else
                    iFailed += 1
                End If
            Next
            For Each str As String In strMeta
                'upload file 
                s = penLvl(j) & "/Meta/"
                If ftp.uploadFile(str, s) Then
                    iSuccess += 1
                Else
                    iFailed += 1
                End If
            Next
            For Each str As String In strSIFO
                'upload file 
                s = penLvl(j) & "/SIFO/"
                If ftp.uploadFile(str, s) Then
                    iSuccess += 1
                Else
                    iFailed += 1
                End If
            Next

            'upload the file (we do this last because no changes can be made until all files have been uploaded
            ftp.uploadFile(My.Application.Info.DirectoryPath & "\TrinityVersion.xml", penLvl(j))
        Next

        MsgBox(iSuccess & " files where successfully uploaded, " & iFailed & " files failed", MsgBoxStyle.Information, "Info")
        strFiles = New List(Of String)
        strMeta = New List(Of String)
        strVersion = New List(Of String)
        strSIFO = New List(Of String)
        strProg = New List(Of String)
        XmlUpdate = XMLDoc.CreateElement("Update")
        XmlUpdate.SetAttribute("Date", CStr(dDate.Date).Replace("-", ""))
        XmlUpdate.SetAttribute("Time", CStr(dDate.Hour & dDate.Minute))
    End Sub

    Private Sub cmdFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFile.Click
        On Error GoTo error_handler
        Dim frm As New frmFile
        If frm.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        'write the lines into the xml file for upload
        Dim xe As Xml.XmlElement = XMLDoc.CreateElement("CopyFile")

        If frm.rdbProg.Checked Then 'a program file (ie a file to be copied to the program folder)
            xe.SetAttribute("Tag", "PROG")
            xe.SetAttribute("FileName", "files/" & frm.lblFile.Text.Substring(frm.lblFile.Text.LastIndexOf("\") + 1))
            strProg.Add(frm.lblFile.Text)
        ElseIf frm.rdbVersion.Checked Then 'a new version file
            xe.SetAttribute("Tag", "VERSION")
            xe.SetAttribute("FileName", "latestVersion/" & frm.lblFile.Text.Substring(frm.lblFile.Text.LastIndexOf("\") + 1))
            strVersion.Add(frm.lblFile.Text)
        Else
            If frm.lblFile.Text.IndexOf("Trinity Data 4.0") = -1 Then
                xe.SetAttribute("FileName", frm.lblFile.Text.Substring(frm.lblFile.Text.LastIndexOf("\") + 1))
            Else
                xe.SetAttribute("FileName", frm.lblFile.Text.Substring(frm.lblFile.Text.IndexOf("Trinity Data 4.0") + 17))
            End If
            strFiles.Add(frm.lblFile.Text)
        End If


        If frm.chkOverwrite.Checked Then
            xe.SetAttribute("OverWrite", "True")
        ElseIf frm.chkNew.Checked Then
            xe.SetAttribute("OverWrite", "NEW")
        Else
            xe.SetAttribute("OverWrite", "False")
        End If


        XmlUpdate.AppendChild(xe)

        MsgBox("The file is set for upload!")

        Exit Sub

error_handler:

        MsgBox("Failed with actions.", MsgBoxStyle.Critical, "Error")

    End Sub

    Private Sub cmdXML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdXML.Click
        Dim frm As New frmXML
        frm.XmlDoc = XMLDoc
        If frm.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        'if ok the add the nodes
        Dim l As Xml.XmlElement = frm.Xml
        Dim xe As Xml.XmlElement
        xe = l.FirstChild
        While Not xe Is Nothing
            XmlUpdate.AppendChild(xe)
            xe = l.ChildNodes.Item(0)
        End While
    End Sub

    Private Sub cmdEditChannel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditChannel.Click
        Dim frm As New frmXMLChannel
        frm.XmlDoc = XMLDoc
        If frm.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        'if ok the add the nodes
        Dim l As Xml.XmlElement = frm.Xml
        Dim xe As Xml.XmlElement
        xe = l.FirstChild
        While Not xe Is Nothing
            XmlUpdate.AppendChild(xe)
            xe = l.ChildNodes.Item(0)
        End While

    End Sub

    Private Sub cmdEditPrices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditPrices.Click
        Dim frm As New frmXMLPricelist
        frm.XmlDoc = XMLDoc
        If frm.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        'if ok the add the nodes
        Dim l As Xml.XmlElement = frm.Xml
        Dim xe As Xml.XmlElement
        xe = l.FirstChild
        While Not xe Is Nothing
            XmlUpdate.AppendChild(xe)
            xe = l.ChildNodes.Item(0)
        End While
    End Sub

    Private Sub cmdSIFO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSIFO.Click
        Dim frm As New frmSpotControl
        If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim xe As Xml.XmlElement = XMLDoc.CreateElement("CopyFile")
            xe.SetAttribute("FileName", "Sifo/" & frm.returnName.Substring(frm.returnName.Trim.LastIndexOf("\") + 1))
            xe.SetAttribute("Tag", "SIFO")
            XmlUpdate.AppendChild(xe)
            strSIFO.Add(frm.returnName)
            MsgBox("The file is set for upload!")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim frm As New frmCompany

        frm.penetration = penLvl
        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            'get the comapy information
            grpCommon.Enabled = True
            grpAdvanced.Enabled = True
            cmdUpload.Enabled = True

            penLvl = frm.penetration
        End If
    End Sub

    Private Sub cmdPremiums_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPremiums.Click
        Dim frm As New frmPremiums
        frm.ShowDialog()

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
