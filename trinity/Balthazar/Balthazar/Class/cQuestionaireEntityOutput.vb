Public Class cQuestionaireEntityOutput
    Inherits cQuestionaireEntity

    Enum OutputTypeEnum
        Text
        Line
        Headline
        Data
    End Enum

    Private _type As OutputTypeEnum
    Public Property Type() As OutputTypeEnum
        Get
            Return _type
        End Get
        Set(ByVal value As OutputTypeEnum)
            _type = value
        End Set
    End Property

    Public Overrides Function GetTag() As String
        Dim _tag As String = "<output"

        Select Case _type
            Case OutputTypeEnum.Data
                _tag &= " type=""data"" name=""" & Me.Name & """ value=""" & Me.Value & """ text=""" & Me.Text & """"
            Case OutputTypeEnum.Headline
                _tag &= " type=""headline"" name=""" & Me.Name & """ value=""" & Me.Value & """"
            Case OutputTypeEnum.Line
                _tag &= " type=""line"""
            Case OutputTypeEnum.Text
                _tag &= " type=""text"" name=""" & Me.Name & """ value=""" & Me.Value & """ text=""" & Me.Text & """"
        End Select
        If Me.Validate = ValidateEnum.True Then
            _tag &= " validate=""true"""
        ElseIf Me.Validate = ValidateEnum.False Then
            _tag &= " validate=""false"""
        End If
        _tag &= " />"

        Return _tag
    End Function

    Public Overrides Function GetPreviewHTML() As String
        Dim _tag As String = vbTab & "<tr><td"

        Select Case _type
            Case OutputTypeEnum.Data
                _tag &= " style='font-weight:bold;'>" & Me.Text & "</td><td style='font-decoration: italic;'>" & Me.Value
            Case OutputTypeEnum.Headline
                _tag &= " colspan='2' style='font-size: 16px; font-weight:bold;'>" & Me.Value
            Case OutputTypeEnum.Line
                _tag &= " colspan='2' style='border-bottom: 1px solid #009999; height: 1px;'>&nbsp;"
            Case OutputTypeEnum.Text
                _tag &= " colspan='2'>" & Me.Value
                If Me.Value = "" Then
                    _tag &= "&nbsp;"
                End If
        End Select
        _tag &= "</td></tr>" & vbCrLf
        Return _tag
    End Function

    Public Sub New(ByVal Questionaire As cQuestionaire)
        Me.Questionaire = Questionaire
    End Sub
End Class
