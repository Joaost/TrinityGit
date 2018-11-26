Partial Public Class Notes
    ''' <summary>
    ''' Abstract class for retrieving a Lotus Notes View
    ''' </summary>
    Public MustInherit Class View

        ''' <summary>
        ''' Notes session
        ''' </summary>
        Friend _ns As Domino.NotesSession
        ''' <summary>
        ''' Notes database
        ''' </summary>
        Friend _db As Domino.NotesDatabase
        ''' <summary>
        ''' Domino.NotesView to wrap around
        ''' </summary>
        Friend _view As Domino.NotesView
        ''' <summary>
        ''' Current active NotesDocument in this View
        ''' </summary>
        Friend _doc As Domino.NotesDocument
        ''' <summary>
        ''' Next NotesDocument in line
        ''' </summary>
        Friend _nextDoc As Domino.NotesDocument

        ''' <summary>
        ''' Initializes a new instance of the <see cref="View" /> class.
        ''' </summary>
        ''' <param name="Session">The Notes session.</param>
        ''' <param name="Database">The Notes database.</param>
        Friend Sub New(ByVal Session As Domino.NotesSession, ByVal Database As Domino.NotesDatabase)
            _ns = Session
            _db = Database
            _view = GetView()
            MoveFirst()
        End Sub

        Friend MustOverride Function GetView() As Domino.NotesView

        ''' <summary>
        ''' Moves to the first Notes Document of this View.
        ''' </summary>
        ''' <returns></returns>
        Function MoveFirst() As Document
            _doc = _view.GetFirstDocument()
            If _doc IsNot Nothing Then
                _nextDoc = _view.GetNextDocument(_doc)
                Return GetCurrentDocument()
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Moves to the next Focument in this View.
        ''' </summary>
        ''' <returns></returns>
        Function MoveNext() As Document
            'The reason for saving nextDoc is for DeleteDocument to work properly. See http://www.breakingpar.com/bkp/home.nsf/0/87256B280015193F87256BCA0066CA46
            _doc = _nextDoc
            If _doc IsNot Nothing Then
                _nextDoc = _view.GetNextDocument(_doc)
                Return GetCurrentDocument()
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Gets the current active document.
        ''' </summary>
        ''' <returns>The current Document</returns>
        Function GetCurrentDocument() As Notes.Document
            If _doc IsNot Nothing Then
                Return New Document(_doc)
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Deletes the current document.
        ''' </summary>
        Sub DeleteCurrentDocument()
            _doc.Remove(True)
            MoveNext()
        End Sub
    End Class
End Class
