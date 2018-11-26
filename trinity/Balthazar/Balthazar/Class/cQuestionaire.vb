Imports System
Imports System.Xml

Public Class cQuestionaire
    Public Name As String
    Public DatabaseID As Long = -1

    Enum ValidateEnum
        [True]
        [False]
    End Enum

    Private _entities As New List(Of cQuestionaireEntity)
    Public Property Entities() As List(Of cQuestionaireEntity)
        Get
            Return _entities
        End Get
        Set(ByVal value As List(Of cQuestionaireEntity))
            _entities = value
        End Set
    End Property


    Private _validate As ValidateEnum = ValidateEnum.True
    Public Property Validate() As ValidateEnum
        Get
            Return _validate
        End Get
        Set(ByVal value As ValidateEnum)
            _validate = value
        End Set
    End Property

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim TmpNode As Xml.XmlElement = XMLDoc.CreateElement("questionaire")
        TmpNode.SetAttribute("name", Name)
        TmpNode.SetAttribute("DatabaseID", DatabaseID)
        If _validate = ValidateEnum.True Then
            TmpNode.SetAttribute("validate", "true")
        Else
            TmpNode.SetAttribute("validate", "false")
        End If
        Dim _xml As String = ""
        For Each _entity As cQuestionaireEntity In _entities
            _xml &= _entity.GetTag
        Next
        TmpNode.InnerXml = _xml
        Return TmpNode
    End Function

    Public Function CreatePreviewHTML() As String
        Dim htmlTable As String = "<body style='font-family: Arial'><span style='font-size:16px;font-weight:bold;'>" & Name & "</span>" & vbCrLf & "<table style='font-family: Arial; font-size: 10pt;'>"
        Dim _lastEntity As cQuestionaireEntity = Nothing
        For Each _entity As cQuestionaireEntity In _entities
            Dim _html As String = _entity.GetPreviewHTML
            If _lastEntity IsNot Nothing AndAlso _entity.GetType Is GetType(cQuestionaireEntityGroup) AndAlso _lastEntity.GetType Is GetType(cQuestionaireEntityGroup) Then
                'remove </table> from end of previous html and get the _entity again but without the <table> tags
                htmlTable = htmlTable.Substring(0, htmlTable.Length - "</table></td></tr>".Length)
                _html = DirectCast(_entity, cQuestionaireEntityGroup).GetPreviewHTML(True)
            ElseIf _lastEntity IsNot Nothing AndAlso (_entity.GetType Is GetType(cQuestionaireEntityInput) AndAlso DirectCast(_entity, cQuestionaireEntityInput).Type = cQuestionaireEntityInput.InputTypeEnum.Rating) AndAlso (_lastEntity.GetType Is GetType(cQuestionaireEntityInput) AndAlso DirectCast(_lastEntity, cQuestionaireEntityInput).Type = cQuestionaireEntityInput.InputTypeEnum.Rating) Then
                'remove </table> from end of previous html and get the _entity again but without the <table> tags
                htmlTable = htmlTable.Trim()
                htmlTable = htmlTable.Substring(0, htmlTable.Length - "</table></td></tr>".Length)
                _html = DirectCast(_entity, cQuestionaireEntityInput).GetPreviewHTML(True)
            End If
            htmlTable &= _html
            _lastEntity = _entity
        Next
        htmlTable &= "</table></body>"
        Return htmlTable
    End Function

    'Public Function CreatePreviewHTML() As String
    '    Dim Lang As New cLanguage
    '    Dim HTML As String = "<html>"
    '    HTML &= "<img src='" & BalthazarSettings.DataFolder & "\logos\cia.bmp' border=0><br>"
    '    HTML &= "<table border=0>"
    '    HTML &= "<tr><td class='headline'>" & Lang("Follow-up") & ":</td><td class='headline' width=100%>" & MyEvent.Product.Name & "</td></tr>"
    '    HTML &= "<tr><td class='subheadline'>" & Lang("Campaign") & ":</td><td class='subheadline' width=100%>" & MyEvent.Questionaire.Name & "</td></tr>"
    '    HTML &= "<tr><td>&nbsp;</td></tr>"
    '    HTML &= "<tr><td colspan=2 class='info'>" & MyEvent.Questionaire.Instructions & "</td></tr>"
    '    HTML &= "<tr><td>&nbsp;</td></tr>"

    '    HTML &= "<tr><td colspan=2>"
    '    HTML &= "<table border=0 width=100%>"
    '    HTML &= "<tr><td>" & Lang("Your name") & "</td><td><input type'text' class='box'></td><td>" & Lang("Cell phone") & "</td><td><input type'text' class='box' style='width: 100%;'></td></tr>"
    '    HTML &= "<tr><td>" & Lang("Employer") & "</td><td><input type'text' class='box'></td><td>" & Lang("Event date") & "</td><td><input type'text' class='box' style='width: 100%;'></td></tr>"
    '    HTML &= "<tr><td>" & Lang("Locations") & "</td><td><input type'text' class='box'></td><td>" & Lang("Activity time") & "</td><td>" & Lang("Start") & "&nbsp;<input type'text' class='smallbox'>&nbsp;" & Lang("End") & "&nbsp;<input type'text' class='smallbox'></td></tr>"
    '    HTML &= "<tr><td>&nbsp;</td></tr>"
    '    HTML &= "</table></td></tr>"

    '    HTML &= "<tr><td colspan=2>"
    '    HTML &= "<table border=0 width=100%>"
    '    HTML &= "<tr><td>" & QuantityText & "</td><td><input type'text' class='smallbox'></td><td>" & InTargetText & "</td><td><input type'text' class='smallbox'></td></tr>"
    '    HTML &= "<tr><td>" & TargetText & "</td><td><input type'text' class='smallbox'></td><td>" & PositiveText & "</td><td><input type'text' class='smallbox'></td></tr>"
    '    HTML &= "<tr><td class='info'>&nbsp;</td></tr>"
    '    HTML &= "</table></td></tr>"

    '    HTML &= "<tr><td colspan=2>"
    '    HTML &= "<table border=0 width=100%>"
    '    HTML &= "<tr><td colspan=8 class='headline'>" & Lang("Rating") & "</td>"
    '    For Each TmpQuestion As cRatingQuestion In RatingQuestions
    '        HTML &= TmpQuestion.CreatePreviewHTML
    '    Next
    '    HTML &= "</table></td></tr>"

    '    HTML &= "<tr><td colspan=2>"
    '    HTML &= "<table border=0 width=100%>"
    '    HTML &= "<tr><td>"
    '    For Each TmpQuestion As cCommentQuestion In CommentQuestions
    '        HTML &= TmpQuestion.CreatePreviewHTML
    '    Next
    '    HTML &= "</td></tr></table></td></tr>"
    '    Return HTML
    'End Function

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        _entities = New List(Of cQuestionaireEntity)
        Name = Node.GetAttribute("name")
        DatabaseID = Node.GetAttribute("DatabaseID")
        If DatabaseID = 0 Then DatabaseID = -1 'To handle early bug
        If Node.GetAttribute("validate") = "true" Then
            _validate = ValidateEnum.True
        Else
            _validate = ValidateEnum.False
        End If
        For Each _node As XmlElement In Node.ChildNodes
            Dim _entity As cQuestionaireEntity = cQuestionaireEntity.GetEntityFromXML(_node, Me)
            _entities.Add(_entity)
        Next

    End Sub

    Public Overrides Function ToString() As String
        Return Name
    End Function


End Class
