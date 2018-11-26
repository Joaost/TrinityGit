Partial Public Class Notes
    ''' <summary>
    ''' General class to wrap around the LotusNotes View representing the Inbox, subclassing Notes.View
    ''' </summary>
    Public Class Inbox
        Inherits Notes.View

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Inbox" /> class.
        ''' </summary>
        ''' <param name="Session">The Notes session.</param>
        ''' <param name="Database">The Notes database.</param>
        Friend Sub New(ByVal Session As Domino.NotesSession, ByVal Database As Domino.NotesDatabase)
            MyBase.New(Session, Database)
        End Sub

        ''' <summary>
        ''' Gets the view representing the Inbox.
        ''' </summary>
        ''' <returns></returns>
        Friend Overrides Function GetView() As Domino.NotesView
            Return _db.GetView("$inbox")
        End Function

    End Class

End Class
