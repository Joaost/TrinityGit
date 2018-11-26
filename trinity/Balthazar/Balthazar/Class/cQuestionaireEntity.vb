Public MustInherit Class cQuestionaireEntity

    Enum ValidateEnum
        [True]
        [False]
        Inherit
    End Enum

    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _validate As ValidateEnum = ValidateEnum.Inherit
    Public Property Validate() As ValidateEnum
        Get
            Return _validate
        End Get
        Set(ByVal value As ValidateEnum)
            _validate = value
        End Set
    End Property

    Private _value As String
    Public Property Value() As String
        Get
            Return _value
        End Get
        Set(ByVal value As String)
            If value = "<none>" Then
                _value = ""
            Else
                _value = value
            End If
        End Set
    End Property

    Private _text As String
    Public Property Text() As String
        Get
            Return _text
        End Get
        Set(ByVal value As String)
            _text = value
        End Set
    End Property

    Public MustOverride Function GetTag() As String

    Public MustOverride Function GetPreviewHTML() As String

    Public Shared Function GetEntityFromXML(ByVal node As Xml.XmlElement, ByVal Questionaire As cQuestionaire) As cQuestionaireEntity
        Dim _entity As cQuestionaireEntity = Nothing
        Select Case node.Name
            Case "input"
                _entity = New cQuestionaireEntityInput(Questionaire)
                Select Case node.GetAttribute("type")
                    Case "text"
                        With DirectCast(_entity, cQuestionaireEntityInput)
                            .Type = cQuestionaireEntityInput.InputTypeEnum.Text
                        End With
                    Case "number"
                        With DirectCast(_entity, cQuestionaireEntityInput)
                            .Type = cQuestionaireEntityInput.InputTypeEnum.Number
                        End With
                    Case "singlechoice"
                        With DirectCast(_entity, cQuestionaireEntityInput)
                            .Type = cQuestionaireEntityInput.InputTypeEnum.SingleChoice
                            For Each _choice As Xml.XmlElement In node.ChildNodes
                                .Choices.Add(_choice.GetAttribute("value"), _choice.GetAttribute("text"))
                            Next
                        End With
                    Case "textarea"
                        With DirectCast(_entity, cQuestionaireEntityInput)
                            .Type = cQuestionaireEntityInput.InputTypeEnum.TextArea
                        End With
                    Case "rating"
                        With DirectCast(_entity, cQuestionaireEntityInput)
                            .Type = cQuestionaireEntityInput.InputTypeEnum.Rating
                            .Values = node.GetAttribute("values")
                            .LeftText = node.GetAttribute("left_text")
                            .RightText = node.GetAttribute("right_text")
                        End With
                End Select
            Case "output"
                _entity = New cQuestionaireEntityOutput(Questionaire)
                Select Case node.GetAttribute("type")
                    Case "text"
                        With DirectCast(_entity, cQuestionaireEntityOutput)
                            .Type = cQuestionaireEntityOutput.OutputTypeEnum.Text
                        End With
                    Case "data"
                        With DirectCast(_entity, cQuestionaireEntityOutput)
                            .Type = cQuestionaireEntityOutput.OutputTypeEnum.Data
                        End With
                    Case "line"
                        With DirectCast(_entity, cQuestionaireEntityOutput)
                            .Type = cQuestionaireEntityOutput.OutputTypeEnum.Line
                        End With
                    Case "headline"
                        With DirectCast(_entity, cQuestionaireEntityOutput)
                            .Type = cQuestionaireEntityOutput.OutputTypeEnum.Headline
                        End With
                    Case Else
                        Stop
                End Select
            Case "group"
                _entity = New cQuestionaireEntityGroup(Questionaire)
                For Each _node As Xml.XmlElement In node.ChildNodes
                    Dim _child As cQuestionaireEntity
                    _child = cQuestionaireEntity.GetEntityFromXML(_node, Questionaire)
                    DirectCast(_entity, cQuestionaireEntityGroup).Members.Add(_child)
                Next
        End Select
        Select Case node.GetAttribute("validate")
            Case "true"
                _entity.Validate = ValidateEnum.True
            Case "false"
                _entity.Validate = ValidateEnum.False
            Case Else
                _entity.Validate = ValidateEnum.Inherit
        End Select
        _entity.Value = node.GetAttribute("value")
        _entity.Name = node.GetAttribute("name")
        _entity.Text = node.GetAttribute("text")
        Return _entity
    End Function

    Private _questionaire As cQuestionaire
    Friend Property Questionaire() As cQuestionaire    
        Get
            Return _questionaire
        End Get
        Set(ByVal value As cQuestionaire)
            _questionaire = value
        End Set
    End Property

End Class
