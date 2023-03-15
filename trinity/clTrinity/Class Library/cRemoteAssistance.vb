Imports System.Xml
Imports System.Runtime.InteropServices
Imports HelpServiceTypeLib
Imports System.Text.RegularExpressions

Public Class cRemoteAssistance
    Sub CheckAndSetRegistry()
        '//This code block checks for and enables Unsolicited Remote Assistance ticktet generation
        '//See: http://support.microsoft.com/kb/301527
        '//and: http://msdn2.microsoft.com/en-us/library/ms811079.aspx

        Dim PermKeyname As String = "Software\policies\Microsoft\Windows NT\Terminal Services"
        Dim DACLkeyname As String = "Software\policies\Microsoft\Windows NT\Terminal Services\RAUnsolicit"
        Dim PermValueName As String = "fAllowUnsolicited"
        Dim PermValueName2 As String = "fAllowUnsolicitedFullControl"

        Dim PermValue As String = String.Empty
        Dim permValue2 As String = String.Empty
        Dim getValueFailed As Boolean = False

        '//Enable unsolicited remote assistance requests
        Dim PermRegKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.LocalMachine.OpenSubKey(PermKeyname, False)
        Try
            PermValue = PermRegKey.GetValue(PermValueName).ToString
            permValue2 = PermRegKey.GetValue(PermValueName2).ToString
            If Not PermValue = "1" Then
                getValueFailed = True
            End If
            If Not permValue2 = "1" Then
                getValueFailed = True
            End If
        Catch ex As Exception
            getValueFailed = True
        End Try

        If getValueFailed Then
            PermRegKey = My.Computer.Registry.LocalMachine.OpenSubKey(PermKeyname, True)
            Try
                PermRegKey.SetValue(PermValueName, 1)
                PermRegKey.SetValue(PermValueName2, 1)
            Catch ex As Exception
                Throw ex
            End Try
        End If

        '//Set permissions for members of the administrators group
        Dim DACLregKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.LocalMachine.OpenSubKey(DACLkeyname, False)
        Try
            PermRegKey.GetValue("Administrators")
        Catch ex As Exception
            Try
                DACLregKey = My.Computer.Registry.LocalMachine.OpenSubKey(DACLkeyname, True)
                PermRegKey.SetValue("Administrators", "Administrators")
            Catch ex2 As Exception
                Throw ex2
            End Try
        End Try
    End Sub

    Function RequestAssistance() As Boolean
        Dim strComputerName As String = Net.Dns.GetHostName()
        Dim strUserName As String = My.User.Name
        Dim intTest As Boolean = False
        Dim strTicketLocation As String = String.Empty
        Dim f As New frmRAProgress
        Dim t As New Threading.Thread(New Threading.ThreadStart(AddressOf f.ShowProgress))
        Dim Password As String

        If frmNotesPassword.ShowDialog <> Windows.Forms.DialogResult.OK Then
            Return False
        End If
        Password = frmNotesPassword.TextBox1.Text

        t.Start()

        '//Function makeRaTicket([in] User_Name, [in] Computer_Name, [out] Ticket_Location) as Boolean
        intTest = makeRaTicket(strUserName, strComputerName, strTicketLocation)

        If Not intTest Then
            Return False
        Else
            Try
#If NOTES Then
                SendMailNotes(Password, TrinitySettings.NotesMailServer, TrinitySettings.NotesMailFile, TrinitySettings.UserName, TrinitySettings.UserEmail, "Remote Assistance Request", "", "johan.hogfeldt@mecglobal.com,hannes.falth@mecglobal.com", strTicketLocation)
#End If
            Catch ex As COMException
                Threading.Thread.Sleep(5000)
                '//End the running thread for progress bar
                t.Abort()
                Throw ex
            Catch
                Dim exception As New Exception("Could not send mail." & vbCrLf & vbCrLf & "You can send the request ticket manually from:" & vbCrLf & strTicketLocation)
                Threading.Thread.Sleep(5000)
                '//End the running thread for progress bar
                t.Abort()
                Throw exception
            End Try
        End If
        'If intTest Then
        '    If Not TryCifsCopy(strTicketLocation, S_salemTicketDropPath) Then
        '        If Not TryAltCopy(strTicketLocation, S_salemTicketAltPath) Then
        '            t.Abort()
        '            MessageBox.Show("Unable to upload Remote Assistance request ticket.", "Remote Assistance Helper :: Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Exit Function
        '        End If
        '    End If
        'End If
        Threading.Thread.Sleep(5000)
        '//End the running thread for progress bar
        t.Abort()

        '//Cleanup thread components
        t = Nothing
        Return True
    End Function

    Private Function makeRaTicket(ByVal strUser As String, ByVal strComputer As String, ByRef strSaveLocation As String) As Boolean
        Try
            Dim hs As New HelpServicesClasses
            'Dim sessions As Session() = hs.GetSessions(strComputer)
            Dim strUserName As String
            Dim strUserDomain As String
            Dim strTicket As String = String.Empty
            Dim strSessionHelpBlob As String = String.Empty
            Dim xml As System.Xml.XmlDocument

            'replace with value from XML config file
            strSaveLocation = My.Computer.FileSystem.SpecialDirectories.Temp & "\" & _
                              strComputer & "_" & getFormattedFileDateTime() & "_" & _
                              "RAInvite" & ".msrcincident"

            strUserDomain = strUser.Remove(strUser.IndexOf("\"), strUser.Length - strUser.IndexOf("\"))
            strUserName = strUser.Remove(0, strUser.IndexOf("\") + 1)
            strTicket = hs.GenerateTicket(strComputer, _
                            strUserName, _
                            strUserDomain, _
                            0, _
                            strSessionHelpBlob)
            xml = hs.FormatIncident(strTicket, 60)
            xml.Save(strSaveLocation)
        Catch notAuth As UnauthorizedAccessException
            Dim ex As New Exception("An error occured while Remote Assistance was connecting to the computer " & strComputer & "." & vbCrLf & "Access Denied.")
            Throw ex
        Catch comErr As System.Runtime.InteropServices.COMException
            Throw comErr
        End Try
        Return True
    End Function

    Private Function getFormattedFileDateTime() As String
        Dim timeNow As DateTime = DateTime.Now
        Dim year As Integer = timeNow.Year
        Dim sYear As String = String.Empty
        Dim month As Integer = timeNow.Month
        Dim sMonth As String = String.Empty
        Dim day As Integer = timeNow.Day
        Dim sDay As String = String.Empty
        Dim hour As Integer = timeNow.Hour
        Dim sHour As String = String.Empty
        Dim minute As Integer = timeNow.Minute
        Dim sMinute As String = String.Empty

        If year < 10 Then
            sYear = 0 & year.ToString
        Else
            sYear = year.ToString
        End If
        If month < 10 Then
            sMonth = 0 & month.ToString
        Else
            sMonth = month.ToString
        End If
        If day < 10 Then
            sDay = 0 & day.ToString
        Else
            sDay = day.ToString
        End If
        If hour < 10 Then
            sHour = 0 & hour.ToString
        Else
            sHour = hour.ToString
        End If
        If minute < 10 Then
            sMinute = 0 & minute.ToString
        Else
            sMinute = minute.ToString
        End If

        Dim formattedDate As String = sYear & sMonth & sDay & "_" & sHour & sMinute
        Return formattedDate
    End Function

#If NOTES Then
    Private Sub SendMailNotes(ByVal Password As String, ByVal Server As String, ByVal Mailfile As String, ByVal FromName As String, ByVal FromEmail As String, ByVal Subject As String, ByVal Body As String, ByVal ToEmail As String, Optional ByVal AttachFile As String = "", Optional ByVal Filename As String = "", Optional ByVal Debug As Boolean = False)
        If Not (My.Application.CommandLineArgs.Contains("nomail") AndAlso Not My.Application.CommandLineArgs.Contains(ToEmail)) Then
            Dim ns As New Domino.NotesSession
            Dim db As Domino.NotesDatabase
            Dim doc As Domino.NotesDocument
            Trinity.Helper.WriteToLogFile("Started")
            If Not (ns Is Nothing) Then
                Trinity.Helper.WriteToLogFile("Created Session. Login with " & Password)
                ns.Initialize(Password)
                Trinity.Helper.WriteToLogFile("Init OK. Get database on " & Server & " for " & Mailfile)
                db = ns.GetDatabase(Server, Mailfile, False)
                If Not (db Is Nothing) Then
                    Trinity.Helper.WriteToLogFile("DB OK")
                    doc = db.CreateDocument()
                    Trinity.Helper.WriteToLogFile("Doc created")
                    doc.ReplaceItemValue("Form", "Memo")
                    doc.ReplaceItemValue("SendTo", ToEmail.Split(","))
                    doc.ReplaceItemValue("Subject", Subject)
                    doc.SaveMessageOnSend = False
                    Dim rt As Domino.NotesRichTextItem
                    rt = doc.CreateRichTextItem("Body")
                    rt.AppendText(Body)
                    Trinity.Helper.WriteToLogFile("Body OK")
                    If AttachFile <> "" Then
                        Trinity.Helper.WriteToLogFile("Attaching File")
                        Dim AttachME As Domino.NotesRichTextItem
                        Dim EmbedObj As Domino.NotesEmbeddedObject
                        AttachME = doc.CreateRichTextItem("Attachment")
                        EmbedObj = AttachME.EmbedObject(Domino.EMBED_TYPE.EMBED_ATTACHMENT, "", AttachFile, "Attachment")
                        Trinity.Helper.WriteToLogFile("File attached")
                    End If
                    doc.Send(False)
                    Trinity.Helper.WriteToLogFile("Mail sent")
                    rt = Nothing
                    doc = Nothing
                End If
                db = Nothing
                ns = Nothing
            Else
                Trinity.Helper.WriteToLogFile("NS is nothing")
            End If
        End If
    End Sub
#End If
End Class

'/* VB.net Port of DotNetHelpServices
' * =============
' * 
' * DotNetHelpServices (http://jonathanmalek.com/)
' * Copyright © 2004 Jonathan Malek. All Rights Reserved.
' * 
' * Permission is hereby granted, free of charge, to any person obtaining 
' * a copy of this software and associated documentation files (the "Software"), 
' * to deal in the Software without restriction, including without limitation 
' * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
' * and/or sell copies of the Software, and to permit persons to whom the 
' * Software is furnished to do so, subject to the following conditions:
' * 
' * The above copyright notice and this permission notice shall be included in all
' * copies or substantial portions of the Software.
' * 
' * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
' * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
' * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
' * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
' * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
' * THE SOFTWARE.
'*/

Public Class HelpServicesClasses
    ' Methods
    Public Sub New()

    End Sub
    Public Function FormatIncident(ByVal ticket As String, ByVal length As Integer) As XmlDocument
        Dim incident1 As New Incident
        incident1.Ticket = ticket
        incident1.Length = length
        incident1.Unsolicited = True
        Dim document1 As New System.Xml.XmlDocument
        document1.LoadXml(incident1.ToString)
        Return document1
    End Function

    Public Function GenerateTicket(ByVal MachineName As String, ByVal UserName As String, ByVal DomainName As String, ByVal SessionID As Integer, ByVal UserHelpBlob As String) As String
#If NOTES Then
        Dim type1 As Type = Type.GetTypeFromProgID("PCH.HelpService", MachineName, True)
        Dim obj1 As Object = Activator.CreateInstance(type1)
        Dim service1 As IPCHService = CType(obj1, IPCHService)
        Dim text1 As String = service1.RemoteConnectionParms(UserName, DomainName, SessionID, UserHelpBlob)
        Marshal.ReleaseComObject(obj1)
        Return text1
#End If
    End Function

    Public Function GetSessions(ByVal MachineName As String) As Session()
#If NOTES Then
        Dim type1 As Type = Type.GetTypeFromProgID("PCH.HelpService", MachineName, True)
        Dim obj1 As Object = Activator.CreateInstance(type1)
        Dim service1 As IPCHService = CType(obj1, IPCHService)
        Dim collection1 As IPCHCollection = Nothing
        Try
            collection1 = service1.RemoteUserSessionInfo
        Catch exception1 As COMException
            Throw New ApplicationException("Could not execute call on remote system.  Most likely the user of the remote system has 'Unsolicited' turned off.", exception1)
        End Try
        If (collection1 Is Nothing) Then
            Throw New ApplicationException("The user you are trying to help has 'Unsolicited' turned off.")
        End If
        If (collection1.Count = 0) Then
            Throw New ApplicationException("There are no users logged in to the machine.")
        End If
        Dim enumerator1 As IEnumerator = collection1.GetEnumerator
        Dim list1 As New ArrayList
        Dim num1 As Integer = 0
        Do While (num1 < collection1.Count)
            enumerator1.MoveNext()
            Dim session1 As ISAFSession = TryCast(enumerator1.Current, ISAFSession)
            Dim session2 As New Session
            session2.UserName = session1.UserName
            session2.DomainName = session1.DomainName
            session2.SessionID = CType(session1.SessionID, Integer)
            session2.SessionState = session1.SessionState.ToString
            list1.Add(session2)
            num1 += 1
        Loop
        Marshal.ReleaseComObject(obj1)
        Return CType(list1.ToArray(GetType(Session)), Session())
#End If
    End Function

End Class

Public Class Incident
    ' Methods
    Public Sub New()
        Me.Unsolicited = True
        Me.Length = 60
        Me.StartTime = DateTime.Now
        Me.LocalNetwork = False
    End Sub

    Public Function GetUnixTime(ByVal dateTime As DateTime) As Integer
        Dim time2 As New DateTime(1970, 1, 1, 0, 0, 0, 0)
        Dim time1 As DateTime = time2.ToLocalTime
        Dim span1 As TimeSpan = dateTime.Subtract(time1)
        Return CType(span1.TotalSeconds, Integer)
    End Function

    Public Overrides Function ToString() As String
        Dim writer1 As New IO.StringWriter
        Dim writer2 As New XmlTextWriter(writer1)
        writer2.Formatting = Formatting.Indented
        writer2.Indentation = 1
        writer2.IndentChar = Char.Parse(ChrW(9))
        writer2.WriteStartDocument()
        writer2.WriteStartElement("UPLOADINFO")
        writer2.WriteAttributeString("", "TYPE", "", "Escalated")
        writer2.WriteStartElement("UPLOADDATA")
        writer2.WriteAttributeString("", "RCTICKET", "", Me.Ticket)
        writer2.WriteAttributeString("", "RCTICKETENCRYPTED", "", "0")
        Dim num1 As Integer = Me.GetUnixTime(Me.StartTime)
        writer2.WriteAttributeString("", "DtStart", "", num1.ToString)
        writer2.WriteAttributeString("", "DtLength", "", Me.Length.ToString)
        If Me.Unsolicited Then
            writer2.WriteAttributeString("", "URA", "", "1")
        End If
        writer2.WriteEndElement()
        writer2.WriteEndElement()
        writer2.WriteEndDocument()
        Return writer1.GetStringBuilder.ToString
    End Function

    ' Properties
    Public Property Length() As Integer
        Get
            Return Me._length
        End Get
        Set(ByVal value As Integer)
            Me._length = value
        End Set
    End Property

    Public Property LocalNetwork() As Boolean
        Get
            Return Me._localNetwork
        End Get
        Set(ByVal value As Boolean)
            Me._localNetwork = False
        End Set
    End Property

    Public Property StartTime() As DateTime
        Get
            Return Me._startTime
        End Get
        Set(ByVal value As DateTime)
            Me._startTime = value
        End Set
    End Property

    Public Property Ticket() As String
        Get
            Dim services1 As New IpServices
            If (services1.BehindNat And Not Me.LocalNetwork) Then
                Dim textArray1 As String() = Me._ticket.Split(New Char() {","c})
                Dim text2 As String = textArray1(2)
                Dim text3 As String = text2.Split(New Char() {";"c})(0).Split(New Char() {":"c})(1)
                textArray1(2) = (services1.IPAddress.ToString & ":" & text3)
                Return String.Join(",", textArray1)
            End If
            Return Me._ticket
        End Get
        Set(ByVal value As String)
            Me._ticket = value
        End Set
    End Property

    Public Property Unsolicited() As Boolean
        Get
            Return Me._unsolicited
        End Get
        Set(ByVal value As Boolean)
            Me._unsolicited = value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return Me._userName
        End Get
        Set(ByVal value As String)
            Me._userName = value
        End Set
    End Property

    ' Fields
    Private _length As Integer
    Private _localNetwork As Boolean
    Private _startTime As DateTime
    Private _ticket As String
    Private _unsolicited As Boolean
    Private _userName As String
    Private Const RCTICKET_ADDRESSES As Integer = 2
End Class

Public Class IpServices
    ' Methods
    Public Sub New()
        Dim text1 As String = Net.Dns.GetHostName
        Me._hostEntry = Net.Dns.GetHostEntry(text1)
        Me.ConnectToGetIPAddress()
        Me._behindNat = True
        Dim addressArray1 As Net.IPAddress() = Me._hostEntry.AddressList
        Dim num1 As Integer = 0
        Do While (num1 < addressArray1.Length)
            Dim address1 As Net.IPAddress = addressArray1(num1)
            If address1.Equals(Me._usedIPAddress) Then
                Me._behindNat = False
                Return
            End If
            num1 += 1
        Loop
    End Sub

    Private Sub ConnectToGetIPAddress()
        Dim ipHostEntry As Net.IPHostEntry = Net.Dns.GetHostEntry(Net.Dns.GetHostName())
        Dim ipAddr As Net.IPAddress() = ipHostEntry.AddressList
        Debug.Print(ipAddr(0).ToString)
        Me._usedIPAddress = ipAddr(0)
    End Sub

    ' Properties
    Public ReadOnly Property BehindNat() As Boolean
        Get
            Return Me._behindNat
        End Get
    End Property

    Public ReadOnly Property HostName() As String
        Get
            Return Me._hostEntry.HostName
        End Get
    End Property

    Public ReadOnly Property IPAddress() As Net.IPAddress
        Get
            Return Me._usedIPAddress
        End Get
    End Property

    ' Fields
    Private _behindNat As Boolean
    Private _hostEntry As Net.IPHostEntry
    Private _usedIPAddress As Net.IPAddress
End Class

Public Class Session
    ' Methods
    Public Sub New()

    End Sub

    ' Fields
    Public DomainName As String
    Public SessionID As Integer
    Public SessionState As String
    Public UserName As String
End Class



