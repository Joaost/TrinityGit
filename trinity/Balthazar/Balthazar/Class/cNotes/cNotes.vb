Imports Balthazar.Extensions
Imports Domino

Public Class cNotes

    Private _mailServer As String
    Private _mailFile As String
    Private _session As NotesSession
    Private _notesDB As NotesDatabase

    Public ReadOnly Property DefaultNotesServer() As String
        Get
            Return GetValueFromINI("MailServer")
        End Get
    End Property

    Public ReadOnly Property DefaultNotesMailfile() As String
        Get
            Return GetValueFromINI("MailFile")
        End Get
    End Property

    Public Function GetValueFromINI(ByVal Key As String) As String
        Dim INI As New cINI
        INI.Create(NotesDirectory.EndWith("\") & "notes.ini")
        Return INI.ReadString("Notes", Key, "")
    End Function

    Private _recipients As New List(Of cNotesRecipient)
    Public ReadOnly Property Recipients() As List(Of cNotesRecipient)
        Get
            Return _recipients
        End Get
    End Property

    Private _attachments As New List(Of cNotesAttachment)
    Public ReadOnly Property Attachments() As List(Of cNotesAttachment)
        Get
            Return _attachments
        End Get
    End Property

    Public ReadOnly Property NotesDirectory() As String
        Get
            Dim RegKey As Microsoft.Win32.RegistryKey
            RegKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\\Lotus\\Notes\\Installer")
            If RegKey Is Nothing Then
                Throw New IO.FileNotFoundException("Lotus notes is not installed.")
            ElseIf RegKey.GetValue("NOTESPATH", "") <> "" Then
                Return RegKey.GetValue("NOTESPATH")
            Else
                Throw New IO.FileNotFoundException("Lotus notes is not installed.")
            End If
        End Get
    End Property

    Private _notesPassword As String
    Public Property NotesPassword() As String
        Get
            Return _notesPassword
        End Get
        Set(ByVal value As String)
            _notesPassword = value
            InitSession()
        End Set
    End Property

    Private _subject As String
    Public Property Subject() As String
        Get
            Return _subject
        End Get
        Set(ByVal value As String)
            _subject = value
        End Set
    End Property

    Private _bodyHTML As String
    Public Property BodyHTML() As String
        Get
            Return _bodyHTML
        End Get
        Set(ByVal value As String)
            _bodyHTML = value
        End Set
    End Property

    Private _body As String
    Public Property Body() As String
        Get
            Return _body
        End Get
        Set(ByVal value As String)
            _body = value
            If _bodyHTML = "" Then
                _bodyHTML = value
            End If
        End Set
    End Property

    Private _htmlMail As Boolean = False
    Public Property HTMLMail() As Boolean
        Get
            Return _htmlMail
        End Get
        Set(ByVal value As Boolean)
            _htmlMail = value
        End Set
    End Property

    Sub CreateCalendarReminder(ByVal DateTime As Date, ByVal PopUpStr As String, ByVal SubjectString As String, Optional ByVal MinutesBefore As Integer = 0)

        If Not (_session Is Nothing) Then
            If _notesDB Is Nothing Then
                InitSession()
            End If
            If Not (_notesDB Is Nothing) Then

                Dim reminderDoc As NotesDocument = _notesDB.CreateDocument

                With reminderDoc

                    .ReplaceItemValue("Form", "Appointment")
                    .ReplaceItemValue("Notes", "")
                    .ReplaceItemValue("$Alarm", 1)
                    .ReplaceItemValue("$AlarmDescription", PopUpStr)
                    .ReplaceItemValue("$AlarmMemoOptions", "")
                    .ReplaceItemValue("$AlarmOffset", -MinutesBefore)
                    .ReplaceItemValue("$AlarmUnit", "M")

                    .ReplaceItemValue("Subject", SubjectString)
                    .ReplaceItemValue("Alarms", "1")

                    .ReplaceItemValue("CalendarDateTime", DateTime)
                    .ReplaceItemValue("StartDate", DateTime)
                    .ReplaceItemValue("StartTime", DateTime)
                    .ReplaceItemValue("StartDateTime", DateTime)

                    .ReplaceItemValue("EndDate", DateTime)
                    .ReplaceItemValue("EndTime", DateTime)
                    .ReplaceItemValue("EndDateTime", DateTime)

                    .ReplaceItemValue("AppointmentType", "4")

                    .ComputeWithForm(False, False)
                    .Save(True, False)

                    .PutInFolder("$Alarms")

                End With
            End If
        End If
    End Sub

    Sub InitSession()
        _session.Initialize(NotesPassword)
        _notesDB = _session.GetDatabase(_mailServer, _mailFile, False)
    End Sub

    Public Sub SendMail(ByVal ToName As String, ByVal ToEmail As String, ByVal Subject As String, ByVal Body As String)
        _recipients.Clear()
        _recipients.Add(New cNotesRecipient(ToEmail, ToName))
        _subject = Subject
        _body = Body
        _htmlMail = False
        SendMail()
    End Sub

    Public Sub SendMail()
        If _notesDB Is Nothing Then
            InitSession()
        End If
        Dim TmpSendToList(_recipients.Count - 1) As String
        Dim TmpPrincipalList(_recipients.Count - 1) As String
        Dim TmpINetMailList(_recipients.Count - 1) As String

        Dim Rec As Integer = 0
        For Each TmpRec As cNotesRecipient In _recipients
            TmpSendToList(Rec) = TmpRec.Email
            TmpPrincipalList(Rec) = TmpRec.Name & " <" & TmpRec.Email & "@NotesDomain>"
            TmpINetMailList(Rec) = TmpRec.Name & " <" & TmpRec.Email & ">"
            Rec += 1
        Next
        Dim MailDoc As Domino.NotesDocument = _notesDB.CreateDocument
        If _htmlMail Then
            _session.ConvertMime = False ' Do not convert to rich text 
            Dim TmpBody As Domino.NotesMIMEEntity
            Dim mh As Domino.NotesMIMEHeader
            Dim mc As Domino.NotesMIMEEntity
            Dim stream As Domino.NotesStream

            MailDoc.AppendItemValue("Form", "Memo")
            MailDoc.AppendItemValue("SendTo", TmpSendToList)
            MailDoc.AppendItemValue("Subject", _subject)

            TmpBody = MailDoc.CreateMIMEEntity
            mh = TmpBody.CreateHeader("MIME-Version")
            Call mh.SetHeaderVal("1.0")
            mh = TmpBody.CreateHeader("Content-Type")
            Call mh.SetHeaderValAndParams("multipart/alternative;boundary=""=NextPart_=""")

            'Send the plain text part first
            mc = TmpBody.CreateChildEntity()
            stream = _session.CreateStream()
            Call stream.WriteText(_body)
            Call mc.SetContentFromText(stream, "text/plain", Domino.MIME_ENCODING.ENC_NONE)

            'Now send the HTML part. Order is important!
            mc = TmpBody.CreateChildEntity()
            stream = _session.CreateStream()
            Call stream.WriteText(BodyHTML)
            Call mc.SetContentFromText(stream, "text/html;charset=""utf-8""", Domino.MIME_ENCODING.ENC_NONE)

            If _attachments.Count > 0 Then
                'Now the attachments
                For Each TmpAttachment As cNotesAttachment In _attachments
                    Dim file() As Byte = My.Computer.FileSystem.ReadAllBytes(TmpAttachment.FilePath)
                    mc = TmpBody.CreateChildEntity()
                    mh = mc.CreateHeader("Content-Disposition")
                    mh.SetHeaderValAndParams("attachment; filename=""" & TmpAttachment.FilePath.Substring(TmpAttachment.FilePath.LastIndexOf("\") + 1) & """;")
                    stream = _session.CreateStream()
                    Call stream.WriteText(System.Convert.ToBase64String(file))
                    Call mc.SetContentFromText(stream, TmpAttachment.MIMEType, Domino.MIME_ENCODING.ENC_BASE64)
                Next
            End If
            Call stream.Close()
            _session.ConvertMime = True
        Else
            _session.ConvertMime = True
            MailDoc.ReplaceItemValue("Form", "Memo")
            MailDoc.ReplaceItemValue("SendTo", TmpSendToList)
            MailDoc.ReplaceItemValue("Subject", Subject)
            Dim rt As Domino.NotesRichTextItem
            rt = MailDoc.CreateRichTextItem("Body")
            rt.AppendText(Body)
            For Each TmpAttachment As cNotesAttachment In _attachments
                Dim AttachME As Domino.NotesRichTextItem
                Dim EmbedObj As Domino.NotesEmbeddedObject
                AttachME = MailDoc.CreateRichTextItem("Attachment")
                EmbedObj = AttachME.EmbedObject(Domino.EMBED_TYPE.EMBED_ATTACHMENT, "", TmpAttachment.FilePath, "Attachment")
            Next
            rt = Nothing
        End If
        MailDoc.ReplaceItemValue("Principal", TmpPrincipalList)
        MailDoc.ReplaceItemValue("InetFrom", TmpINetMailList)
        MailDoc.ReplaceItemValue("ReplyTo", TmpINetMailList)
        MailDoc.SaveMessageOnSend = False
        MailDoc.Send(False)

        MailDoc = Nothing
    End Sub

    Public Sub New()
        SetupNotes(DefaultNotesMailfile, DefaultNotesServer)
    End Sub

    Public Sub New(ByVal NotesServer As String)
        SetupNotes(DefaultNotesMailfile, _mailServer)
    End Sub

    Public Sub New(ByVal NotesServer As String, ByVal MailFile As String)
        SetupNotes(NotesServer, MailFile)
    End Sub

    Private Sub SetupNotes(ByVal MailFile As String, ByVal NotesServer As String)
        _mailFile = MailFile
        _mailServer = NotesServer
        _session = New NotesSession
    End Sub

    Protected Overrides Sub Finalize()
        _notesDB = Nothing
        _session = Nothing
        MyBase.Finalize()
    End Sub
End Class

Public Class cNotesRecipient

    Private _email As String
    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property

    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal Email As String, ByVal Name As String)
        _email = Email
        _name = Name
    End Sub

End Class

Public Class cNotesAttachment

    Private _filePath As String
    Public Property FilePath() As String
        Get
            Return _filePath
        End Get
        Set(ByVal value As String)
            _filePath = value
        End Set
    End Property

    Private _mimeType As String
    Public Property MIMEType() As String
        Get
            Return _mimeType
        End Get
        Set(ByVal value As String)
            _mimeType = value
        End Set
    End Property

    Public Function GuessMIMEType() As Boolean
        Dim Ext As String = System.IO.Path.GetExtension(_filePath).ToLower()
        Dim RegKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(Ext)
        If RegKey IsNot Nothing AndAlso RegKey.GetValue("Content Type") IsNot Nothing Then
            MIMEType = RegKey.GetValue("Content Type").ToString()
            Return True
        Else
            MIMEType = "application/unknown"
            Return False
        End If
    End Function

End Class
