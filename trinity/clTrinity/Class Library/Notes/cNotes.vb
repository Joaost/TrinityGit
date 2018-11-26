Imports Domino

''' <summary>
''' General class wrapping the Domino COM-object with more easy-to-use classes
''' </summary>
Public Class Notes

    ''' <summary>
    ''' Notes session
    ''' </summary>
    Private _ns As Domino.NotesSession
    ''' <summary>
    ''' Notes database
    ''' </summary>
    Private _db As Domino.NotesDatabase
    ''' <summary>
    ''' Inbox object
    ''' </summary>
    Private _inbox As Notes.Inbox
    ''' <summary>
    ''' Contains last error message returned by Lotus Notes
    ''' </summary>
    Private _lastErrorMessage As String

    Private _password As String
    ''' <summary>
    ''' Gets or sets the Lotus Notes password.
    ''' </summary>
    ''' <value>The password.</value>
    Public Property Password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property

    Private _server As String
    ''' <summary>
    ''' Gets or sets the Domino server.
    ''' </summary>
    ''' <value>The server.</value>
    Public Property Server() As String
        Get
            Return _server
        End Get
        Set(ByVal value As String)
            _server = value
        End Set
    End Property

    Private _mailFile As String
    ''' <summary>
    ''' Gets or sets the Domino mail file.
    ''' </summary>
    ''' <value>The mail file.</value>
    Public Property MailFile() As String
        Get
            Return _mailFile
        End Get
        Set(ByVal value As String)
            _mailFile = value
        End Set
    End Property

    ''' <summary>
    ''' Connects to Lotus Notes using the specified password.
    ''' </summary>
    ''' <param name="Password">The password.</param>
    ''' <returns></returns>
    Public Function Connect(Optional ByVal Password As String = "") As Boolean
        If Password = "" Then Password = _password
        _ns = New NotesSession
        If Not _ns Is Nothing Then
            Try
                _ns.Initialize(Password)
                _db = _ns.GetDatabase(Server, MailFile, False)
            Catch ex As Exception
                _lastErrorMessage = ex.Message
                Return False
            End Try
        Else
            _lastErrorMessage = "Could not initialize session."
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Gets the Lotus Notes inbox.
    ''' </summary>
    ''' <returns></returns>
    Function GetInbox() As Notes.Inbox
        If _inbox Is Nothing Then
            If _ns Is Nothing Then
                If Not Connect() Then
                    Throw New Exception("Could not connect to Notes:" & vbCrLf & vbCrLf & _lastErrorMessage)
                End If
            End If
            _inbox = New Notes.Inbox(_ns, _db)
        End If
        Return _inbox
    End Function

    ''' <summary>
    ''' Sends an email.
    ''' </summary>
    ''' <param name="FromName">From name.</param>
    ''' <param name="FromEmail">From email.</param>
    ''' <param name="Subject">The subject.</param>
    ''' <param name="Body">The body.</param>
    ''' <param name="ToEmail">To email.</param>
    ''' <param name="Attachments">List of attachment paths.</param>
    Public Sub SendMail(ByVal FromName As String, ByVal FromEmail As String, ByVal Subject As String, ByVal Body As String, ByVal ToEmail As String, Optional ByVal Attachments As List(Of String) = Nothing)
        If _ns Is Nothing Then
            If Not Connect() Then
                Throw New Exception("Could not connect to Notes:" & vbCrLf & vbCrLf & _lastErrorMessage)
            End If
        End If
        Dim doc As Domino.NotesDocument
        If Not (_db Is Nothing) Then
            doc = _db.CreateDocument()
            doc.ReplaceItemValue("Form", "Memo")
            doc.ReplaceItemValue("SendTo", ToEmail.Split(","))
            doc.ReplaceItemValue("Subject", Subject)
            doc.SaveMessageOnSend = False
            Dim rt As Domino.NotesRichTextItem
            rt = doc.CreateRichTextItem("Body")
            rt.AppendText(Body)
            If Attachments IsNot Nothing Then
                Dim _i As Integer = 1
                For Each _file As String In Attachments
                    Dim AttachME As Domino.NotesRichTextItem
                    Dim EmbedObj As Domino.NotesEmbeddedObject
                    AttachME = doc.CreateRichTextItem("Attachment" + _i.ToString)
                    EmbedObj = AttachME.EmbedObject(Domino.EMBED_TYPE.EMBED_ATTACHMENT, "", _file, "Attachment" + _i.ToString)
                    _i += 1
                Next
            End If
            doc.Send(False)
            rt = Nothing
            doc = Nothing
        Else
            Throw New Exception("No database connection")
        End If
    End Sub

    Public Function FindNotesPath() As String

        Dim Path As String = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lotus\\Notes").GetValue("Path")

        If Path Is Nothing Then Path = My.Computer.Registry.CurrentUser.OpenSubKey("\\Software\Lotus\Notes").GetValue("NotesIniPath")

        Return Path


    End Function

    'End Function

    ''' <summary>
    ''' Initializes a new instance of the <see cref="Notes" /> class.
    ''' </summary>
    Public Sub New()
        Dim _notesDirectory As String = FindNotesPath()
       
        Dim TmpIni As New IniFile
        TmpIni.Create(My.Computer.FileSystem.CombinePath(_notesDirectory, "notes.ini"))
        _mailFile = TmpIni.Text("Notes", "MailFile")
        _server = TmpIni.Text("Notes", "MailServer")
        _password = ""
    End Sub

    ''' <summary>
    ''' Disconnects this instance.
    ''' </summary>
    Sub Disconnect()
        _ns = Nothing
        _db = Nothing
    End Sub

    ''' <summary>
    ''' Allows an <see cref="T:System.Object" /> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Object" /> is reclaimed by garbage collection.
    ''' </summary>
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        Disconnect()
    End Sub
End Class
