

'The purpuse of this class is to check for newer versions while Trinity is running
'The Run sub should be run in a seperete thread.

Namespace Trinity
    Public Class cVersionCheck
        Dim form As Object

        Dim WithEvents client As New System.Net.WebClient

        Delegate Sub EndThread(ByVal IsNewVersion As Boolean)

        Public Sub New(ByVal f As Windows.Forms.Form)
            form = f
        End Sub

        Public Sub Run()
            'check for a newer version
            'Dim LatestVersion As String
            'Dim MyVersion As String
            Dim intMyVersion As Integer
            Dim intLatestVersion As Integer

            Try
                Dim Ini As New clsIni
                Ini.Create(My.Application.Info.DirectoryPath & "\launch.ini")
                If Ini.Text("Server", "Address") = "" Then Exit Sub

                If Ini.Text("Server", "Address").Substring(0, 3).ToUpper = "WWW" Then
                    Dim data As IO.Stream = client.OpenRead("http://" & Ini.Text("Server", "Address") & "/trinity/versions.xml")

                    'get the server version (it was downloaded by the launcher
                    Dim xmlDocServer As New Xml.XmlDocument
                    xmlDocServer.Load(My.Application.Info.DirectoryPath & "\versions.xml")

                    intLatestVersion = DirectCast(xmlDocServer.SelectSingleNode("//File[@Name='Trinity.exe']"), XmlElement).GetAttribute("Version")

                    xmlDocServer.Load(My.Application.Info.DirectoryPath & "\version.xml")

                    intMyVersion = DirectCast(xmlDocServer.SelectSingleNode("//File[@Name='Trinity.exe']"), XmlElement).GetAttribute("Version")

                Else 'end of web and start of network procedure

                    'get the server version (it was downloaded by the launcher
                    Dim xmlDocServer As New Xml.XmlDocument
                    xmlDocServer.Load(Ini.Text("Server", "Address") & "versions.xml")

                    intLatestVersion = DirectCast(xmlDocServer.SelectSingleNode("//File[@Name='Trinity.exe']"), XmlElement).GetAttribute("Version")

                    xmlDocServer.Load(My.Application.Info.DirectoryPath & "\version.xml")

                    intMyVersion = DirectCast(xmlDocServer.SelectSingleNode("//File[@Name='Trinity.exe']"), XmlElement).GetAttribute("Version")

                End If

                If intMyVersion < intLatestVersion Then
                    form.Invoke(New EndThread(AddressOf form.EndThread), True)
                Else
                    form.Invoke(New EndThread(AddressOf form.EndThread), False)
                End If

            Catch
                form.Invoke(New EndThread(AddressOf form.EndThread), False)
            End Try
        End Sub
    End Class '
End Namespace
