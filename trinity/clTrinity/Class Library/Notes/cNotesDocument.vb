Partial Public Class Notes
    ''' <summary>
    ''' General class that wraps a Domino.NotesDocument for easier access
    ''' </summary>
    Public Class Document

        Friend _doc As Domino.NotesDocument

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Document" /> class.
        ''' </summary>
        ''' <param name="Document">An instance of Domino.NotesDocument to wrap.</param>
        Friend Sub New(ByVal Document As Domino.NotesDocument)
            _doc = Document
        End Sub

        ''' <summary>
        ''' Gets the document subject.
        ''' </summary>
        ''' <value>The subject.</value>
        Public ReadOnly Property Subject() As String
            Get
                If _doc.HasItem("Subject") Then
                    Dim _sb As New System.Text.StringBuilder()
                    For Each _string As String In _doc.GetItemValue("Subject")
                        _sb.Append(_string)
                    Next
                    Return _sb.ToString
                End If
                Return ""
            End Get
        End Property

        ''' <summary>
        ''' Gets the document body.
        ''' </summary>
        ''' <value>The body.</value>
        Public ReadOnly Property Body() As String
            Get
                If _doc.HasItem("Body") Then
                    Dim _sb As New System.Text.StringBuilder()
                    For Each _string As String In _doc.GetItemValue("Body")
                        _sb.Append(_string)
                    Next
                    Return _sb.ToString
                End If
                Return ""
            End Get
        End Property

        ''' <summary>
        ''' Gets who the document is from.
        ''' </summary>
        ''' <value>From.</value>
        Public ReadOnly Property [From]() As String
            Get
                If _doc.HasItem("From") Then
                    Dim _sb As New System.Text.StringBuilder()
                    For Each _string As String In _doc.GetItemValue("From")
                        _sb.Append(_string).Append(",")
                    Next
                    Return _sb.ToString.TrimEnd(",")
                End If
                Return ""
            End Get
        End Property

        ''' <summary>
        ''' Gets the Domino.NotesDocument.
        ''' </summary>
        ''' <returns></returns>
        Friend Function GetNotesDocument()
            Return _doc
        End Function

        ''' <summary>
        ''' Sets a Lotus Notes flag.
        ''' </summary> 
        ''' <param name="Flag">The flag.</param>
        ''' <param name="Value">The value.</param>
        Sub SetFlag(ByVal Flag As String, ByVal Value As Object)
            If _doc.HasItem(Flag) Then
                _doc.ReplaceItemValue(Flag, Value)
            Else
                _doc.AppendItemValue(Flag, Value)
            End If
        End Sub

        ''' <summary>
        ''' Gets a Lotus Notes flag.
        ''' </summary>
        ''' <param name="Flag">The flag.</param>
        ''' <returns>Value of the flag</returns>
        Function GetFlag(ByVal Flag As String) As Object
            If _doc.HasItem(Flag) Then
                Return _doc.GetItemValue(Flag)(0)
            Else
                Return False
            End If
        End Function

    End Class
End Class
