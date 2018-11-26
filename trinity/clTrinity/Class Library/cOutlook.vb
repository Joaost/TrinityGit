Imports Microsoft.Office.Interop

Namespace Trinity
    Public Class cOutlook



        Public Shared Sub send(ByVal _toMail As String, ByVal _subject As String, ByVal _body As String, ByVal _files As List(Of String))

            'Start outlook
            Dim objOutlook As Outlook.Application
            objOutlook = CreateObject("Outlook.Application")

            'Logon 
            Dim olNs As Outlook.NameSpace
            olNs = objOutlook.GetNamespace("MAPI")
            'olNs.Logon()

            ' Create an instance of the Inbox folder. 
            ' If Outlook is not already running, this has the side
            ' effect of initializing MAPI.
            Dim mailFolder As Outlook.Folder
            mailFolder = olNs.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderOutbox)


            ' Create a new MailItem.
            Dim oMsg As Outlook._MailItem
            oMsg = objOutlook.CreateItem(Outlook.OlItemType.olMailItem)
            oMsg.Subject = _subject
            oMsg.Body = _body
            oMsg.To = _toMail


            Dim sBodyLen As String = oMsg.Body.Length
            Dim oAttachs As Outlook.Attachments = oMsg.Attachments
            Dim oAttach As Outlook.Attachment
            For Each _file As String In _files
                oAttachs.Add(_file)
            Next
            'oAttach = oAttachs(_files)

            ' Send
            oMsg.Send()

            ' Clean up
            oMsg = Nothing
            oAttach = Nothing
            oAttachs = Nothing

        End Sub
    End Class
End Namespace

