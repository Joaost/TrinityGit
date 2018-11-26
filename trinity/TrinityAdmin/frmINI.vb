Imports System.IO

Public Class frmINI
    Dim ini As Trinity.clsIni
    Dim newKeys() As String
  
    Dim fsWrite As FileStream
    Dim sw As StreamWriter

    Dim Filename As String
    Dim _xml As Xml.XmlElement
    Dim _xmlDoc As Xml.XmlDocument


    Public Property XmlDoc()
        Get
            Return _xmlDoc
        End Get
        Set(ByVal value)
            _xmlDoc = value
            _xml = _xmlDoc.CreateElement("TEMP")
        End Set
    End Property

    Public ReadOnly Property Xml()
        Get
            Return _xml
        End Get
    End Property

    Private Sub frmINI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'goes through the folder structure and saves all .ini files
        Dim s As New trinityAdmin.FileSearch("G:\TV\Trinity Data 4.0\", "*.ini")

        s.Search()

        Dim fi As FileInfo
        Dim strTmp As String

        For i As Integer = 0 To s.Files.Count - 1
            fi = s.Files.Item(i)
            strTmp = fi.FullName
            cmbFile.Items.Add(strTmp)
        Next
        
    End Sub

    Private Sub cmbFile_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFile.SelectedIndexChanged
        ini = New Trinity.clsIni
        cmbSection.Items.Clear()
        ini.Create(cmbFile.Items.Item(cmbFile.SelectedIndex))

        Dim al As New ArrayList()
        al = ini.GetSectionNames()

        Dim s As String
        For i As Integer = 0 To al.Count - 1
            s = al(i).ToString.Trim
            s = s.Substring(0, s.Length - 1)
            cmbSection.Items.Add(s)
        Next

        Filename = ini.Filename.Substring(ini.Filename.IndexOf("Trinity Data 4.0\") + 17, ini.Filename.Length - (ini.Filename.IndexOf("Trinity Data 4.0\") + 17))

    End Sub

    Private Sub cmbSection_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSection.SelectedIndexChanged

        'declaring a FileStream to open the file with access mode of reading
        Dim fsRead As New FileStream(cmbFile.Items.Item(cmbFile.SelectedIndex), FileMode.Open, FileAccess.Read)
        'creating a new StreamReader and passing the filestream object fsRead as argument
        Dim sr As New StreamReader(fsRead)
        cmbKey.Items.Clear()
        Dim strLine As String
        Dim searchLine As String = cmbSection.Items.Item(cmbSection.SelectedIndex).ToString
        Dim found As Boolean = False
        Dim strTmp As String

        'peek method of StreamReader object tells how much more data is left in the file
        While sr.Peek() > -1
            'read a new line
            strLine = sr.ReadLine

            If found = True Then
                'read the line and extract the Key, if we are at a new section or row is empty we are done
                If Not strLine Is Nothing AndAlso Not strLine = "" AndAlso strLine.IndexOf("]") = -1 Then
                    strTmp = strLine.Substring(0, strLine.IndexOf("="))
                    cmbKey.Items.Add(strTmp)
                Else
                    'since we have all we needed we can exit the sub
                    sr.Close()
                    sr.Dispose()
                    fsRead.Close()
                    fsRead.Dispose()
                    Exit Sub
                End If
            End If
            If Not strLine = "" AndAlso strLine.Substring(1, strLine.Length - 2) = searchLine Then
                'we are at the right palce in the ini file
                found = True
            End If
        End While
    End Sub

    Private Sub cmbKey_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbKey.SelectedIndexChanged
        Dim selection As String = cmbSection.Items.Item(cmbSection.SelectedIndex).ToString
        Dim Key As String = cmbKey.Items.Item(cmbKey.SelectedIndex).ToString
        txtValue.Text = ini.Text(selection, Key)

        If txtValue.Text = "" Then
            chkFrom.Enabled = False
        Else
            chkFrom.Enabled = True
        End If
    End Sub

    Private Sub cmdAddSection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddSection.Click
        Dim frm As New frmInput
        frm.Text = "Add Section"
        If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            cmbSection.Items.Add(frm.TextBox1.Text)
        End If
    End Sub

    Private Sub cmdAddKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddKey.Click
        'if the selection exist in the collection we dont need to add it
        Dim frm As New frmInput
        frm.Text = "Add Key"
        If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            cmbKey.Items.Add(frm.TextBox1.Text)
        End If
    End Sub

    Private Sub cmdSetValue_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetValue.Click
        Dim selection As String = cmbSection.Items.Item(cmbSection.SelectedIndex).ToString
        Dim Key As String = cmbKey.Items.Item(cmbKey.SelectedIndex).ToString

        Dim xe As Xml.XmlElement = _xmlDoc.CreateElement("INI")
        Dim xe1 As Xml.XmlElement = _xmlDoc.CreateElement("Change")

        xe.SetAttribute("FileName", Filename)

        Dim strSec As String = cmbSection.Items.Item(cmbSection.SelectedIndex).ToString.Trim
        Dim strKey As String = cmbKey.Items.Item(cmbKey.SelectedIndex).ToString.Trim
        xe1.SetAttribute("Name", strSec)
        xe1.SetAttribute("Value", strKey)

        If ini.Text(selection, Key) = "" Then
            xe1.SetAttribute("Type", "Add")
            xe1.SetAttribute("Tag", txtValue.Text)
        Else
            xe1.SetAttribute("Type", "Edit")
            If chkFrom.Checked Then
                xe1.SetAttribute("From", ini.Text(selection, Key))
            Else
                xe1.SetAttribute("From", "*")
            End If
            xe1.SetAttribute("To", txtValue.Text)
        End If

        xe.AppendChild(xe1)
        _xml.AppendChild(xe)
    End Sub

    Private Sub cmdDeleteKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteKey.Click
        If MsgBox("This will delete the marked key. Do you wish to proceed?", MsgBoxStyle.YesNo, "DELETE") = MsgBoxResult.Yes Then
            Dim xe As Xml.XmlElement = _xmlDoc.CreateElement("INI")
            Dim xe1 As Xml.XmlElement = _xmlDoc.CreateElement("Change")

            xe.SetAttribute("FileName", Filename)
            xe1.SetAttribute("Type", "Delete")
            xe1.SetAttribute("Name", cmbSection.Items.Item(cmbSection.SelectedIndex).ToString.Trim)
            xe1.SetAttribute("Value", cmbKey.Items.Item(cmbKey.SelectedIndex).ToString.Trim)
            xe.AppendChild(xe1)
            _xml.AppendChild(xe)
          
        End If
    End Sub

    Private Sub cmdDeleteSection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteSection.Click
        If MsgBox("This will delete the section and all its sub keys. Do you wish to proceed?", MsgBoxStyle.YesNo, "DELETE") = MsgBoxResult.Yes Then
            Dim xe As Xml.XmlElement = _xmlDoc.CreateElement("INI")
            Dim xe1 As Xml.XmlElement = _xmlDoc.CreateElement("Change")
            xe.SetAttribute("FileName", Filename)
            xe1.SetAttribute("Type", "DeleteSection")
            xe1.SetAttribute("Name", cmbSection.Items.Item(cmbSection.SelectedIndex).ToString.Trim)
            xe.AppendChild(xe1)
            _xml.AppendChild(xe)
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class
