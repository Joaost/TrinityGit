Public Class cQuestionaireEntityGroup
    Inherits cQuestionaireEntity

    Private _members As New List(Of cQuestionaireEntity)
    Public Property Members() As List(Of cQuestionaireEntity)
        Get
            Return _members
        End Get
        Set(ByVal value As List(Of cQuestionaireEntity))
            _members = value
        End Set
    End Property

    Public Overrides Function GetTag() As String
        Dim _tag = "<group name=""" & Me.Name & """>"
        For Each _member As cQuestionaireEntity In _members
            _tag &= _member.GetTag
        Next
        _tag &= "</group>"
        Return _tag
    End Function

    Public Overrides Function GetPreviewHTML() As String
        Dim _tag = vbTab & "<tr><td colspan='2'><table><tr style='font-weight: bold; font-size: 10px;'>"
        For Each _member As cQuestionaireEntity In _members
            _tag &= "<td style='width: 1px;'>" & _member.Text & "</td>"
        Next
        _tag &= "</tr><tr>"
        For Each _member As cQuestionaireEntity In _members
            If _member.GetType Is GetType(cQuestionaireEntityInput) Then
                _tag &= "<td><input type='text' /></td>"
            ElseIf _member.GetType Is GetType(cQuestionaireEntityOutput) Then
                _tag &= "<td>" & _member.Value & "</td>"
            Else
                Stop
            End If
        Next
        _tag &= "</tr></table></td></tr>"
        Return _tag
    End Function

    Public Overloads Function GetPreviewHTML(ByVal ContinuePreviousTable As Boolean) As String
        Dim _tag As String
        If Not ContinuePreviousTable Then
            _tag = vbTab & "<tr><td><table><tr>"
            For Each _member As cQuestionaireEntity In _members
                _tag &= "<td style='font-weight: bold;'>" & _member.Text & "</td>"
            Next
            _tag &= "</tr><tr>"
        Else
            _tag = "<tr>"
        End If
        For Each _member As cQuestionaireEntity In _members
            If _member.GetType Is GetType(cQuestionaireEntityInput) Then
                _tag &= "<td><input type='text' /></td>"
            ElseIf _member.GetType Is GetType(cQuestionaireEntityOutput) Then
                _tag &= "<td>" & _member.Value & "</td>"
            Else
                Stop
            End If
        Next
        _tag &= "</tr></table></td></tr>"
        Return _tag
    End Function

    Public Sub New(ByVal Questionaire As cQuestionaire)
        Me.Questionaire = Questionaire
    End Sub
End Class
