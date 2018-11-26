Public Class cQuestionaireEntityInput
    Inherits cQuestionaireEntity

    Enum InputTypeEnum
        Text
        Number
        Rating
        SingleChoice
        TextArea
    End Enum

    Private _type As InputTypeEnum
    Public Property Type() As InputTypeEnum
        Get
            Return _type
        End Get
        Set(ByVal value As InputTypeEnum)
            _type = value
        End Set
    End Property

    Private _unit As String
    Public Property Unit() As String
        Get
            Return _unit
        End Get
        Set(ByVal value As String)
            _unit = value
        End Set
    End Property

    Private _data As String
    Public Property Data() As String
        Get
            Return _data
        End Get
        Set(ByVal value As String)
            If value = "<none>" Then
                _data = ""
            Else
                _data = value
            End If
        End Set
    End Property

    Private _leftText As String
    Public Property LeftText() As String
        Get
            Return _leftText
        End Get
        Set(ByVal value As String)
            _leftText = value
        End Set
    End Property

    Private _rightText As String
    Public Property RightText() As String
        Get
            Return _rightText
        End Get
        Set(ByVal value As String)
            _rightText = value
        End Set
    End Property

    Private _values As Byte = 5
    Public Property Values() As Byte
        Get
            Return _values
        End Get
        Set(ByVal value As Byte)
            _values = value
        End Set
    End Property

    Private _choices As New Dictionary(Of Byte, String)
    Public Property Choices() As Dictionary(Of Byte, String)
        Get
            Return _choices
        End Get
        Set(ByVal value As Dictionary(Of Byte, String))
            _choices = value
        End Set
    End Property

    Public Overrides Function GetTag() As String
        Dim _tag As String = "<input"

        If Me.Validate = ValidateEnum.True Then
            _tag &= " validate=""true"""
        ElseIf Me.Validate = ValidateEnum.False Then
            _tag &= " validate=""false"""
        End If
        If (Me.Validate = ValidateEnum.True OrElse (Me.Questionaire.Validate = cQuestionaire.ValidateEnum.True AndAlso Me.Validate = ValidateEnum.Inherit)) AndAlso Me.Name = "" Then
            Throw New Exception("All required parameters must have a name.")
        End If
        Select Case _type
            Case InputTypeEnum.Text
                _tag &= " type=""text"" name=""" & Me.Name & """ value=""" & Me.Value & """ text=""" & Me.Text & """ data=""" & _data & """"
            Case InputTypeEnum.Number
                _tag &= " type=""number"" name=""" & Me.Name & """ value=""" & Me.Value & """ text=""" & Me.Text & """ data=""" & _data & """ unit=""" & _unit & """"
            Case InputTypeEnum.TextArea
                _tag &= " type=""textarea"" name=""" & Me.Name & """ value=""" & Me.Value & """ text=""" & Me.Text & """ data=""" & _data & """"
            Case InputTypeEnum.Rating
                _tag &= " type=""rating"" name=""" & Me.Name & """ values=""" & _values & """ text=""" & Me.Text & """ left_text=""" & _leftText & """ right_text=""" & _rightText & """"
            Case InputTypeEnum.SingleChoice
                _tag &= " type=""singlechoice"" name=""" & Me.Name & """ text=""" & Me.Text & """>"
                For Each _choice As KeyValuePair(Of Byte, String) In _choices
                    _tag &= "<choice value=""" & _choice.Key & """ text=""" & _choice.Value & """ />"
                Next
                _tag = _tag.TrimEnd(">")
        End Select
        _tag &= "></input>"

        Return _tag
    End Function

    Public Overrides Function GetPreviewHTML() As String
        Dim _tag As String = vbTab & "<tr><td"

        Select Case _type
            Case InputTypeEnum.Text, InputTypeEnum.Number
                _tag &= " style='font-weight:bold;'>" & Me.Text & "</td><td><input type='text' />" & _unit
            Case InputTypeEnum.TextArea
                _tag &= " colspan='2'><table width='100%' cellspacing='0' cellpadding='0' style='border: 1px solid black;'><tr><td style='background: #009999; color: white; font-size: 10px; font-weight: bold;'>" & Me.Text & "</td></tr>"
                _tag &= "<tr><td><textarea style='border: none; width: 100%' rows='5'></textarea></td></tr></table>"
            Case InputTypeEnum.SingleChoice
                _tag &= " style='font-weight: bold;'>" & Me.Text & "</td><td>"
                Dim i As Integer = 0
                For Each _choice As KeyValuePair(Of Byte, String) In _choices
                    Dim _name As String = Me.Name
                    If _name = "" Then _name = Guid.NewGuid.ToString
                    _name &= i
                    _tag &= "<input type='radio' id='" & _name & "' /><label for='" & _name & "'>" & _choice.Value & "</label>"
                    i += 1
                Next
            Case InputTypeEnum.Rating
                _tag &= " colspan='2'><table cellspacing='0'><tr style='background: #009999'><td>&nbsp;</td><td>" & _leftText & "</td><td align='right'>" & _rightText & "</td></tr>"
                _tag &= "<tr><td>" & Me.Text & "</td><td colspan='2'>"
                _tag &= "<table><tr>"
                For i As Integer = 1 To _values
                    Dim _name As String = Me.Name
                    If _name = "" Then _name = Guid.NewGuid.ToString
                    _name &= i
                    _tag &= "<td><input type='radio' id='" & _name & "' /><label for='" & _name & "'>" & i & "</label></td>"
                Next
                _tag &= "</tr></table></td></tr></table>"
        End Select
        _tag &= "</td></tr>" & vbCrLf
        Return _tag
    End Function

    Public Overloads Function GetPreviewHTML(ByVal ContinuePreviousTable As Boolean) As String
        Dim _tag As String = vbTab & "<tr><td"

        Select Case _type
            Case InputTypeEnum.Text, InputTypeEnum.Number
                _tag &= " style='font-weight:bold;'>" & Me.Text & "</td><td><input type='text' />" & _unit
            Case InputTypeEnum.TextArea
                _tag &= " colspan='2'><table width='100%' cellspacing='0' cellpadding='0' style='border: 1px solid black;'><tr><td style='background: #009999; color: white; font-size: 10px; font-weight: bold;'>" & Me.Text & "</td></tr>"
                _tag &= "<tr><td><textarea style='border: none; width: 100%' rows='5'></textarea></td></tr></table>"
            Case InputTypeEnum.SingleChoice
                _tag &= " style='font-weight: bold;'>" & Me.Text & "</td><td>"
                Dim i As Integer = 0
                For Each _choice As KeyValuePair(Of Byte, String) In _choices
                    Dim _name As String = Me.Name
                    If _name = "" Then _name = Guid.NewGuid.ToString
                    _name &= i
                    _tag &= "<input type='radio' id='" & _name & "' /><label for='" & _name & "'>" & _choice.Value & "</label>"
                    i += 1
                Next
            Case InputTypeEnum.Rating
                If ContinuePreviousTable Then
                    _tag = _tag.Substring(0, _tag.Length - "<tr><td".Length)
                    _tag &= "<tr style='background: #009999'><td>&nbsp;</td><td>" & _leftText & "</td><td align='right'>" & _rightText & "</td></tr>"
                    _tag &= "<tr><td>" & Me.Text & "</td><td colspan='2'>"
                    _tag &= "<table><tr>"
                    For i As Integer = 1 To _values
                        Dim _name As String = Me.Name
                        If _name = "" Then _name = Guid.NewGuid.ToString
                        _name &= i
                        _tag &= "<td><input type='radio' id='" & _name & "' /><label for='" & _name & "'>" & i & "</label></td>"
                    Next
                    _tag &= "</tr></table></td></tr></table>"
                Else
                    _tag &= " colspan='2'><table cellspacing='0'><tr style='background: #009999'><td>&nbsp;</td><td>" & _leftText & "</td><td align='right'>" & _rightText & "</td></tr>"
                    _tag &= "<tr><td>" & Me.Text & "</td><td colspan='2'>"
                    _tag &= "<table><tr>"
                    For i As Integer = 1 To _values
                        Dim _name As String = Me.Name
                        If _name = "" Then _name = Guid.NewGuid.ToString
                        _name &= i
                        _tag &= "<td><input type='radio' id='" & _name & "' /><label for='" & _name & "'>" & i & "</label></td>"
                    Next
                    _tag &= "</tr></table></td></tr></table>"
                End If
        End Select
        _tag &= "</td></tr>" & vbCrLf
        Return _tag
    End Function

    Public Sub New(ByVal Questionaire As cQuestionaire)
        Me.Questionaire = Questionaire
    End Sub
End Class
