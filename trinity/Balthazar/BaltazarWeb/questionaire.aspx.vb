Public Partial Class questionaire
    Inherits System.Web.UI.Page

    'Public Sub ValidateDate(ByVal sender As Object, ByVal e As ServerValidateEventArgs) Handles valDate.ServerValidate
    '    Dim TmpDate As Date
    '    Try
    '        If e.Value = "" Then
    '            e.IsValid = False
    '            Exit Sub
    '        End If
    '        TmpDate = CDate(e.Value)
    '        e.IsValid = True
    '    Catch
    '        e.IsValid = False
    '    End Try
    'End Sub

    Private Sub Questionaire_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Master.Database.GetLoggedInUserInfo Is Nothing Then
            FormsAuthentication.RedirectToLoginPage()
            Exit Sub
        End If
        Dim TmpRow As DataRow = Master.Database.GetQuestionaire(CInt(Request.QueryString("id")))
        Dim TmpAnswer As DataRow = Nothing
        If Request.QueryString("answerid") IsNot Nothing Then
            TmpAnswer = Master.Database.GetQuestionaireAnswer(Request.QueryString("answerid"))
            If TmpAnswer!QuestionaireID <> Request.QueryString("id") Then
                mvwQuestionaire.SetActiveView(vwNoAccess)
                Exit Sub
            End If
            fldCity.Value = TmpAnswer!City
            fldLocation.Value = TmpAnswer!Location
            fldDate.Value = TmpAnswer!Date
            fldBookingID.Value = TmpAnswer!BookingID
        End If
        Dim TmpInfo As cUserInfo = Master.Database.GetLoggedInUserInfo
        If Not TmpRow Is Nothing Then
            Dim XML As New XmlDocument
            If Not IsDBNull(TmpAnswer!xml) AndAlso Not TmpAnswer!xml = "" Then
                XML.LoadXml(TmpAnswer!xml)
            Else
                XML.LoadXml(TmpRow!xml)
            End If
            Dim xmlQuestionaire As XmlElement
            xmlQuestionaire = XML.GetElementsByTagName("questionaire")(0)

            lblName.Text = xmlQuestionaire.GetAttribute("name")

            Dim tblQuestionaire As New Table
            tblQuestionaire.Font.Size = 10
            tblQuestionaire.Font.Name = "Arial"
            Dim _ratingtable As Table = Nothing
            Dim _grouptable As Table = Nothing
            For Each _element As XmlElement In xmlQuestionaire.ChildNodes
                Dim _row As New TableRow()
                Select Case _element.Name
                    Case "input"
                        Dim validator As BaseValidator = Nothing
                        If ((xmlQuestionaire.GetAttribute("validate") = "true" OrElse xmlQuestionaire.GetAttribute("validate") = "") AndAlso Not _element.GetAttribute("validate") = "false") OrElse _element.GetAttribute("validate") = "true" Then
                            validator = New RequiredFieldValidator
                            validator.ForeColor = Drawing.Color.Red
                            validator.Display = ValidatorDisplay.Dynamic
                            validator.Text = "*"
                        End If
                        Select Case _element.GetAttribute("type")
                            Case "text"
                                _ratingtable = Nothing
                                Dim _headercell As New TableCell
                                _headercell.Controls.Add(New Label With {.Text = _element.GetAttribute("text")})
                                _headercell.Font.Bold = True
                                Dim _valuecell As New TableCell
                                Dim _txt As New TextBox
                                If _element.GetAttribute("data") <> "" AndAlso TmpAnswer IsNot Nothing Then
                                    _txt.Text = TmpAnswer(_element.GetAttribute("data"))
                                End If
                                If _element.GetAttribute("answer") <> "" Then
                                    _txt.Text = _element.GetAttribute("answer")
                                End If
                                _valuecell.Controls.Add(_txt)
                                _txt.ID = _element.GetAttribute("name")
                                If validator IsNot Nothing Then
                                    validator.ControlToValidate = _txt.ID
                                    _headercell.Controls.Add(validator)
                                End If
                                _row.Cells.Add(_headercell)
                                _row.Cells.Add(_valuecell)
                            Case "number"
                                _ratingtable = Nothing
                                Dim _headercell As New TableCell
                                _headercell.Controls.Add(New Label With {.Text = _element.GetAttribute("text")})
                                _headercell.Font.Bold = True
                                Dim _valuecell As New TableCell
                                Dim _txt As New TextBox
                                If _element.GetAttribute("answer") <> "" Then
                                    _txt.Text = _element.GetAttribute("answer")
                                End If
                                _valuecell.Controls.Add(_txt)
                                _txt.ID = _element.GetAttribute("name")
                                If validator IsNot Nothing Then
                                    validator.ControlToValidate = _txt.ID
                                    _headercell.Controls.Add(validator)
                                End If
                                If _element.GetAttribute("unit") <> "" Then
                                    _valuecell.Controls.Add(New Label With {.Text = _element.GetAttribute("unit")})
                                End If
                                _row.Cells.Add(_headercell)
                                _row.Cells.Add(_valuecell)
                            Case "textarea"
                                _ratingtable = Nothing
                                Dim _cell As New TableCell
                                _cell.ColumnSpan = "2"
                                Dim _table As New Table
                                Dim _headlineRow As New TableRow
                                _table.Width = Unit.Parse("100%")
                                _headlineRow.BackColor = Drawing.Color.FromArgb(0, 153, 153)
                                _headlineRow.ForeColor = Drawing.Color.White
                                _headlineRow.Font.Size = FontUnit.Parse("10px")
                                _headlineRow.Font.Bold = True
                                _table.CellPadding = 0
                                _table.CellSpacing = 0
                                _table.BorderStyle = BorderStyle.Solid
                                _table.BorderColor = Drawing.Color.Black
                                _table.BorderWidth = Unit.Pixel(1)
                                _headlineRow.Cells.Add(New TableCell)
                                _headlineRow.Cells(0).Controls.Add(New Label With {.Text = _element.GetAttribute("text")})
                                _table.Rows.Add(_headlineRow)
                                Dim _contentRow As New TableRow
                                _contentRow.Cells.Add(New TableCell)
                                Dim _txt As New TextBox
                                _txt.TextMode = TextBoxMode.MultiLine
                                _txt.Width = WebControls.Unit.Percentage(100)
                                _txt.ID = _element.GetAttribute("name")
                                If validator IsNot Nothing Then
                                    validator.ControlToValidate = _txt.ID
                                    _headlineRow.Cells(0).Controls.Add(validator)
                                End If
                                _txt.BorderStyle = BorderStyle.None
                                If _element.GetAttribute("rows") <> "" Then
                                    _txt.Rows = _element.GetAttribute("rows")
                                Else
                                    _txt.Rows = 5
                                End If
                                If _element.GetAttribute("answer") <> "" Then
                                    _txt.Text = _element.GetAttribute("answer")
                                End If
                                _contentRow.Cells(0).Controls.Add(_txt)
                                _table.Rows.Add(_contentRow)
                                _cell.Controls.Add(_table)
                                _row.Cells.Add(_cell)
                            Case "singlechoice"
                                _ratingtable = Nothing
                                Dim _headercell As New TableCell
                                _headercell.Controls.Add(New Label With {.Text = _element.GetAttribute("text")})
                                _headercell.Font.Bold = True
                                Dim _valuecell As New TableCell
                                Dim _choices As New RadioButtonList
                                _choices.RepeatDirection = RepeatDirection.Horizontal
                                For Each _choice As XmlElement In _element.ChildNodes
                                    Dim _itm As New ListItem With {.Text = _choice.GetAttribute("text"), .Value = _choice.GetAttribute("value")}
                                    _choices.Items.Add(_itm)
                                Next
                                If _element.GetAttribute("answer") <> "" Then
                                    _choices.SelectedValue = _element.GetAttribute("answer")
                                End If
                                _choices.ID = _element.GetAttribute("name")
                                If validator IsNot Nothing Then
                                    validator.ControlToValidate = _choices.ID
                                    _headercell.Controls.Add(validator)
                                End If
                                _valuecell.Controls.Add(_choices)
                                _row.Cells.Add(_headercell)
                                _row.Cells.Add(_valuecell)
                            Case "rating"
                                Dim _cell As New TableCell
                                _cell.ColumnSpan = 2
                                If _ratingtable Is Nothing Then
                                    _ratingtable = New Table
                                    '_ratingtable.Width = Unit.Percentage(100)
                                    _ratingtable.CellSpacing = 0
                                End If
                                Dim _headlineRow As New TableRow
                                _headlineRow.Cells.Add(New TableCell With {.BackColor = Drawing.Color.FromArgb(0, 153, 153)})
                                _headlineRow.Cells.Add(New TableCell With {.Text = _element.GetAttribute("left_text"), .BackColor = Drawing.Color.FromArgb(0, 153, 153)})
                                _headlineRow.Cells.Add(New TableCell With {.Text = _element.GetAttribute("right_text"), .BackColor = Drawing.Color.FromArgb(0, 153, 153), .HorizontalAlign = HorizontalAlign.Right})
                                _ratingtable.Rows.Add(_headlineRow)

                                Dim _dataRow As New TableRow
                                _dataRow.Cells.Add(New TableCell)
                                _dataRow.Cells(0).Controls.Add(New Label With {.Text = _element.GetAttribute("text")})

                                Dim _valuecell As New TableCell
                                Dim _choices As New RadioButtonList
                                _choices.RepeatDirection = RepeatDirection.Horizontal
                                For i As Integer = 1 To _element.GetAttribute("values")
                                    Dim _itm As New ListItem With {.Text = i, .Value = i}
                                    _choices.Items.Add(_itm)
                                Next
                                If _element.GetAttribute("answer") <> "" Then
                                    _choices.SelectedValue = _element.GetAttribute("answer")
                                End If
                                _valuecell.Wrap = False
                                _choices.ID = _element.GetAttribute("name")
                                _valuecell.ColumnSpan = 2
                                _valuecell.Controls.Add(_choices)
                                If validator IsNot Nothing Then
                                    validator.ControlToValidate = _choices.ID
                                    _dataRow.Cells(0).Controls.Add(validator)
                                End If

                                _dataRow.Cells.Add(_valuecell)
                                _ratingtable.Rows.Add(_dataRow)

                                'Dim _tbl As New Table
                                'Dim _r As New TableRow
                                'Dim _c As New TableCell
                                '_tbl.Font.Size = FontUnit.Parse("8px")
                                '_r.Cells.Add(New TableCell With {.Text = _element.GetAttribute("left_text")})
                                '_c.Controls.Add(_choices)
                                '_r.Cells.Add(_c)
                                '_r.Cells.Add(New TableCell With {.Text = _element.GetAttribute("right_text")})
                                '_tbl.Rows.Add(_r)
                                _cell.Controls.Add(_ratingtable)
                                _row.Cells.Add(_cell)
                        End Select
                    Case "output"
                        _ratingtable = Nothing
                        Select Case _element.GetAttribute("type")
                            Case "text"
                                Dim _cell As New TableCell
                                _cell.ColumnSpan = 2
                                If _element.GetAttribute("value") <> "" Then
                                    _cell.Text = _element.GetAttribute("value")
                                Else
                                    _cell.Text = "&nbsp;"
                                End If
                                _cell.ID = _element.GetAttribute("name")
                                _row.Cells.Add(_cell)
                            Case "headline"
                                Dim _cell As New TableCell
                                _cell.ColumnSpan = 2
                                If _element.GetAttribute("value") <> "" Then
                                    _cell.Text = _element.GetAttribute("value")
                                Else
                                    _cell.Text = "&nbsp;"
                                End If
                                _cell.Font.Bold = True
                                _cell.Font.Size = FontUnit.Parse("16px")
                                _cell.ID = _element.GetAttribute("name")
                                _row.Cells.Add(_cell)
                            Case "data"
                                Dim _headercell As New TableCell
                                _headercell.Text = _element.GetAttribute("text")
                                _headercell.Font.Bold = True
                                Dim _valuecell As New TableCell
                                Select Case _element.GetAttribute("value")
                                    Case "date"
                                        If TmpAnswer IsNot Nothing Then
                                            _valuecell.Text = Format(TmpAnswer!Date, "Short date")
                                        Else
                                            _valuecell.Text = ""
                                        End If
                                    Case "week"
                                        If TmpAnswer IsNot Nothing Then
                                            _valuecell.Text = DatePart(DateInterval.WeekOfYear, TmpAnswer!Date, Microsoft.VisualBasic.FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                                        Else
                                            _valuecell.Text = ""
                                        End If
                                    Case "weekday"
                                        If TmpAnswer IsNot Nothing Then
                                            _valuecell.Text = WeekdayName(Weekday(TmpAnswer!Date, Microsoft.VisualBasic.FirstDayOfWeek.Monday), False, Microsoft.VisualBasic.FirstDayOfWeek.Monday)
                                        Else
                                            _valuecell.Text = ""
                                        End If
                                    Case "location"
                                        If TmpAnswer IsNot Nothing Then
                                            _valuecell.Text = TmpAnswer!Location
                                        Else
                                            _valuecell.Text = ""
                                        End If
                                    Case "city"
                                        If TmpAnswer IsNot Nothing Then
                                            _valuecell.Text = TmpAnswer!City
                                        Else
                                            _valuecell.Text = ""
                                        End If
                                End Select
                                If _element.GetAttribute("name") <> "" Then
                                    _valuecell.ID = _element.GetAttribute("name")
                                End If
                                _row.Cells.Add(_headercell)
                                _row.Cells.Add(_valuecell)
                            Case "line"
                                Dim _cell As New TableCell
                                _cell.ColumnSpan = 2
                                _cell.Height = Unit.Pixel(1)
                                _cell.Style.Add("border-bottom", "1px solid #009999")
                                _row.Cells.Add(_cell)
                        End Select
                    Case "group"
                        Dim validator As BaseValidator = Nothing
                        If xmlQuestionaire.GetAttribute("validate") = "true" OrElse xmlQuestionaire.GetAttribute("validate") = "" AndAlso Not _element.GetAttribute("validate") = "false" OrElse _element.GetAttribute("validate") = "true" Then
                            validator = New RequiredFieldValidator
                            validator.ForeColor = Drawing.Color.Red
                            validator.Display = ValidatorDisplay.Dynamic
                            validator.Text = "*"
                        End If
                        If _grouptable Is Nothing Then
                            Dim _cell As New TableCell
                            _cell.ColumnSpan = 2
                            _grouptable = New Table
                            Dim _headlineRow As New TableRow
                            _headlineRow.Font.Bold = True
                            _headlineRow.Font.Size = FontUnit.Parse("10px")
                            For Each _groupElement As XmlElement In _element.ChildNodes
                                _headlineRow.Cells.Add(New TableCell With {.Text = _groupElement.GetAttribute("text"), .Wrap = True, .Width = Unit.Pixel(1)})
                            Next
                            _grouptable.Rows.Add(_headlineRow)
                            _cell.Controls.Add(_grouptable)
                            _row.Cells.Add(_cell)
                        End If
                        Dim _valueRow As New TableRow
                        For Each _groupElement As XmlElement In _element.ChildNodes
                            Select Case _groupElement.Name
                                Case "input"
                                    Dim _vcell As New TableCell
                                    Dim _txt As New TextBox
                                    _txt.ID = _groupElement.GetAttribute("name")
                                    If _groupElement.GetAttribute("answer") <> "" Then
                                        _txt.Text = _groupElement.GetAttribute("answer")
                                    End If
                                    _vcell.Controls.Add(_txt)
                                    'If validator IsNot Nothing Then
                                    '    validator.ControlToValidate = _txt.ID
                                    '    _vcell.Controls.Add(validator)
                                    'End If
                                    _valueRow.Cells.Add(_vcell)
                                    _grouptable.Rows.Add(_valueRow)
                                Case "output"
                                    _valueRow.Cells.Add(New TableCell With {.Text = _groupElement.GetAttribute("value"), .Wrap = False})
                            End Select
                        Next
                        _grouptable.Rows.Add(_valueRow)
                End Select
                tblQuestionaire.Rows.Add(_row)
            Next
            vwQuestionaire.Controls.Add(tblQuestionaire)
            If TmpAnswer IsNot Nothing AndAlso TmpAnswer!Answered Then
                tblQuestionaire.Enabled = False
                Dim lblBack As New LinkButton
                lblBack.Text = "<-- Tillbaka"
                lblBack.ForeColor = Drawing.Color.FromArgb(0, 153, 153)
                lblBack.PostBackUrl = "~/Default.aspx"
                vwQuestionaire.Controls.Add(New LiteralControl("<br />"))
                vwQuestionaire.Controls.Add(lblBack)
            Else
                Dim cmdSubmitQuestionaire As New Button
                With cmdSubmitQuestionaire
                    .Text = "Skicka"
                    .BackColor = Drawing.Color.FromName("White")
                    .BorderColor = Drawing.Color.FromArgb(197, 187, 175) '("#C5BBAF")
                    .BorderStyle = BorderStyle.Solid
                    .BorderWidth = Unit.Pixel(1)
                    .ForeColor = Drawing.Color.FromArgb(28, 94, 85) '"#1C5E55"
                End With
                AddHandler cmdSubmitQuestionaire.Click, AddressOf cmdSubmit_Click
                vwQuestionaire.Controls.Add(New LiteralControl("<br />"))
                vwQuestionaire.Controls.Add(cmdSubmitQuestionaire)
            End If
            mvwQuestionaire.SetActiveView(vwQuestionaire)
        Else
            mvwQuestionaire.SetActiveView(vwNoAccess)
        End If
    End Sub

    'Private Sub Questionaire_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    If Not IsPostBack Then
    '        Dim TmpRow As DataRow = Master.Database.GetQuestionaire(CInt(Request.QueryString("id")))
    '        Dim TmpInfo As cUserInfo = Master.Database.GetLoggedInUserInfo
    '        If TmpInfo.Type = cUserInfo.UserTypeEnum.Staff Then
    '            lblMyName.Text = TmpInfo.FirstName & " " & TmpInfo.LastName
    '            lblCellPhone.Text = TmpInfo.MobilePhone
    '        Else
    '            cellEmployer.Text = "Demobolag"
    '            cellCity.Text = "Butik & ort"
    '            lblMyName.Visible = False
    '            lblCellPhone.Visible = False
    '            txtMyName.Visible = True
    '            valMyName.Enabled = True
    '            txtCellPhone.Visible = True
    '            valCellPhone.Enabled = True
    '        End If
    '        If Not TmpRow Is Nothing Then
    '            lblName.Text = TmpRow!name
    '            lblInfo.Text = TmpRow!Instruction
    '            lblClient.Text = TmpRow!Client
    '            lblQuantity.Text = TmpRow!QuantityText
    '            lblInTarget.Text = TmpRow!InTargetText
    '            lblTarget.Text = TmpRow!TargetText
    '            lblPositive.Text = TmpRow!PositiveInTargetText

    '            If Request.QueryString("answerid") IsNot Nothing Then
    '                Dim TmpAnswer As DataRow = Master.Database.GetQuestionaireAnswer(Request.QueryString("answerid"))
    '                If TmpAnswer!QuestionaireID <> Request.QueryString("id") Then
    '                    mvwQuestionaire.SetActiveView(vwNoAccess)
    '                    Exit Sub
    '                End If
    '                txtDate.Text = Format(TmpAnswer!Date, "Short date")
    '                lblDate.Text = Format(TmpAnswer!Date, "Short date")
    '                txtStart.Text = Helper.Mam2Time(TmpAnswer!StartTime)
    '                txtEnd.Text = Helper.Mam2Time(TmpAnswer!EndTime)
    '                lblStart.Text = Helper.Mam2Time(TmpAnswer!StartTime)
    '                lblEnd.Text = Helper.Mam2Time(TmpAnswer!EndTime)
    '                txtLocation.Text = TmpAnswer!Locations
    '                txtQuantity.Text = TmpAnswer!Quantity
    '                lblQuantityValue.Text = TmpAnswer!Quantity
    '                txtInTarget.Text = TmpAnswer!InTarget
    '                lblInTargetValue.Text = TmpAnswer!InTarget
    '                txtTarget.Text = TmpAnswer!Target
    '                lblTargetValue.Text = TmpAnswer!Target
    '                txtPositive.Text = TmpAnswer!Positive
    '                lblPositiveValue.Text = TmpAnswer!InTarget
    '                txtComments.Text = TmpAnswer!Comments
    '                lblComments.Text = TmpAnswer!Comments
    '                lblAnswered.Value = TmpAnswer!Answered
    '                If TmpInfo.Type = cUserInfo.UserTypeEnum.Provider OrElse TmpInfo.Type = cUserInfo.UserTypeEnum.Salesman OrElse TmpInfo.Type = cUserInfo.UserTypeEnum.HeadOfSales Then
    '                    txtEmployer.Visible = False
    '                    txtLocation.Visible = False
    '                    lblProvider.Visible = True
    '                    lblStore.Visible = True
    '                    lblCity.Visible = True
    '                    lblProvider.Text = Master.Database.GetLoggedInUserInfo.FirstName
    '                    lblStore.Text = TmpAnswer!Locations & ", "
    '                    lblCity.Text = TmpAnswer!City
    '                    lblInstruction.Visible = False

    '                    If TmpInfo.Type = cUserInfo.UserTypeEnum.Salesman OrElse TmpInfo.Type = cUserInfo.UserTypeEnum.HeadOfSales OrElse (TmpInfo.Type = cUserInfo.UserTypeEnum.Provider AndAlso TmpAnswer!Answered) Then
    '                        lblMyName.Visible = True
    '                        lblCellPhone.Visible = True
    '                        txtMyName.Visible = False
    '                        txtCellPhone.Visible = False
    '                        valMyName.Enabled = False
    '                        valCellPhone.Enabled = False

    '                        txtDate.Visible = False
    '                        pnlTimeTextBoxes.Visible = False
    '                        txtQuantity.Visible = False
    '                        txtInTarget.Visible = False
    '                        txtTarget.Visible = False
    '                        txtPositive.Visible = False
    '                        txtComments.Visible = False
    '                        txtMyName.Visible = False
    '                        txtCellPhone.Visible = False
    '                        cmdSubmit.Visible = False

    '                        lblProvider.Text = TmpRow!chosenprovidername
    '                        lblMyName.Text = TmpAnswer!NameOfPerson
    '                        lblCellPhone.Text = TmpAnswer!CellPhoneOfPerson

    '                        lblDate.Visible = True
    '                        lblStart.Visible = True
    '                        lblEnd.Visible = True
    '                        pnlTimeLabels.Visible = True
    '                        lblQuantityValue.Visible = True
    '                        lblInTargetValue.Visible = True
    '                        lblTargetValue.Visible = True
    '                        lblPositiveValue.Visible = True
    '                        lblComments.Visible = True
    '                        lblBack.Visible = True
    '                    End If
    '                End If

    '                grdRatingQuestions.DataSource = Master.Database.GetRatingQuestions(CInt(Request.QueryString("id")))
    '                grdRatingQuestions.DataBind()

    '                lstAnsweredQuestionaires.DataSource = Master.Database.GetCommentQuestions(CInt(Request.QueryString("id")))
    '                lstAnsweredQuestionaires.DataBind()

    '                Dim i As Integer = 0
    '                For Each TmpRating As DataRow In Master.Database.GetRatingQuestionAnswers(Request.QueryString("answerid")).Rows
    '                    DirectCast(grdRatingQuestions.Rows(i).Cells(3).FindControl("rblRating"), RadioButtonList).SelectedValue = TmpRating!Answer
    '                    DirectCast(grdRatingQuestions.Rows(i).Cells(6).FindControl("lblAnswerID"), Label).Text = TmpRating!ID
    '                    i += 1
    '                Next
    '                i = 0
    '                For Each TmpComment As DataRow In Master.Database.GetCommentQuestionAnswers(Request.QueryString("answerid")).Rows
    '                    DirectCast(lstAnsweredQuestionaires.Items(i).FindControl("lblAnswerID"), Label).Text = TmpComment!ID
    '                    For j As Integer = 1 To 10
    '                        DirectCast(lstAnsweredQuestionaires.Items(i).FindControl("TextBox" & j), TextBox).Text = TmpComment.Item("Text" & j)
    '                        DirectCast(lstAnsweredQuestionaires.Items(i).FindControl("Label" & j), Label).Text = TmpComment.Item("Text" & j)
    '                    Next
    '                    If TmpInfo.Type = cUserInfo.UserTypeEnum.Salesman OrElse TmpInfo.Type = cUserInfo.UserTypeEnum.HeadOfSales OrElse (TmpInfo.Type = cUserInfo.UserTypeEnum.Provider AndAlso TmpAnswer!Answered) Then
    '                        lstAnsweredQuestionaires.Items(i).FindControl("pnlTextboxes").Visible = False
    '                        lstAnsweredQuestionaires.Items(i).FindControl("pnlLabels").Visible = True
    '                    End If
    '                    i += 1
    '                Next
    '            Else
    '                If Master.Database.GetLoggedInUserInfo.Type <> cUserInfo.UserTypeEnum.Provider Then
    '                    mvwQuestionaire.SetActiveView(vwNoAccess)
    '                    Exit Sub
    '                End If
    '            End If
    '            mvwQuestionaire.SetActiveView(vwQuestionaire)
    '        Else
    '            mvwQuestionaire.SetActiveView(vwNoAccess)
    '        End If
    '    End If
    'End Sub
    Protected Sub cmdSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Page.IsValid Then
            Dim TmpRow As DataRow = Master.Database.GetQuestionaire(CInt(Request.QueryString("id")))
            Dim TmpAnswer As DataRow = Nothing
            If Request.QueryString("answerid") IsNot Nothing Then
                TmpAnswer = Master.Database.GetQuestionaireAnswer(Request.QueryString("answerid"))
                If TmpAnswer!QuestionaireID <> Request.QueryString("id") Then
                    mvwQuestionaire.SetActiveView(vwNoAccess)
                    Exit Sub
                End If
            End If
            If Not TmpRow Is Nothing Then
                Dim XML As New XmlDocument
                If Not IsDBNull(TmpAnswer!xml) AndAlso Not TmpAnswer!xml = "" Then
                    XML.LoadXml(TmpAnswer!xml)
                Else
                    XML.LoadXml(TmpRow!xml)
                End If
                Dim xmlQuestionaire As XmlElement
                xmlQuestionaire = XML.GetElementsByTagName("questionaire")(0)
                For Each _element As XmlElement In xmlQuestionaire.ChildNodes
                    If _element.Name = "input" Then
                        SetAnswer(_element)
                    ElseIf _element.Name = "group" Then
                        For Each _subElement As XmlElement In _element.ChildNodes
                            SetAnswer(_subElement)
                        Next
                    End If
                Next
                Master.Database.SaveQuestionAnswer(QID:=Request.QueryString("id"), Answered:=True, AnswerXML:=xmlQuestionaire.OuterXml, AnswerID:=Request.QueryString("answerid"), AtDate:=fldDate.Value, City:=fldCity.Value, BookingID:=fldBookingID.Value, Locations:=fldLocation.Value)
                Response.Redirect("~/Default.aspx")
            End If
        End If
    End Sub

    Sub SetAnswer(ByVal node As XmlElement)
        If node.Name = "input" Then
            Dim Ctrl As Object
            Ctrl = RecursiveFindControl(Page, node.GetAttribute("name"))
            Dim Val As String = ""
            Select Case node.GetAttribute("type")
                Case "singlechoice", "rating"
                    Val = DirectCast(Ctrl, RadioButtonList).SelectedValue
                Case Else
                    Val = DirectCast(Ctrl, TextBox).Text
            End Select
            node.SetAttribute("answer", Val)
        End If
    End Sub

    Function RecursiveFindControl(ByVal TopControl As Control, ByVal ID As String) As Control
        For Each Ctrl As Control In TopControl.Controls
            If FindControl(ID) IsNot Nothing Then
                Return FindControl(ID)
                Exit For
            End If
            If Ctrl.ID = ID Then
                Return Ctrl
                Exit For
            End If
            Dim _ctrl As Control = RecursiveFindControl(Ctrl, ID)
            If _ctrl IsNot Nothing Then
                Return _ctrl
                Exit For
            End If
        Next
        Return Nothing
    End Function

    'Protected Sub cmdSubmit_Click_old(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSubmit.Click
    '    If Not Page.IsValid Then
    '        Exit Sub
    '    End If

    '    If Request.QueryString("answerid") Is Nothing Then
    '        Dim AnswerID As Integer
    '        AnswerID = Master.Database.SaveQuestionAnswer(CInt(Request.QueryString("id")), True, txtMyName.Text, txtCellPhone.Text, CDate(txtDate.Text), Helper.Time2Mam(txtStart.Text), Helper.Time2Mam(txtEnd.Text), txtLocation.Text, "", txtQuantity.Text, txtInTarget.Text, txtTarget.Text, txtPositive.Text, txtComments.Text)
    '        For Each TmpRow As GridViewRow In grdRatingQuestions.Rows
    '            Master.Database.SaveRatingQuestionAnswer(AnswerID, TmpRow.Cells(0).Text, DirectCast(TmpRow.Cells(3).FindControl("rblRating"), RadioButtonList).SelectedValue)
    '        Next
    '        For Each TmpItem As DataListItem In lstAnsweredQuestionaires.Items
    '            Dim TmpAnswer(0 To 9) As String
    '            For i As Integer = 0 To 9
    '                TmpAnswer(i) = DirectCast(TmpItem.FindControl("TextBox" & i + 1), TextBox).Text
    '            Next
    '            Master.Database.SaveCommentQuestionAnswer(AnswerID, CInt(DirectCast(TmpItem.FindControl("lblID"), Label).Text), TmpAnswer)
    '        Next
    '        Response.Redirect("~/Default.aspx")
    '    Else
    '        Dim TmpCity As String = ""
    '        If Master.Database.GetLoggedInUserInfo.Type = cUserInfo.UserTypeEnum.Provider Then
    '            TmpCity = lblCity.Text
    '        End If
    '        Master.Database.SaveQuestionAnswer(CInt(Request.QueryString("id")), True, txtMyName.Text, txtCellPhone.Text, CDate(txtDate.Text), Helper.Time2Mam(txtStart.Text), Helper.Time2Mam(txtEnd.Text), txtLocation.Text, TmpCity, txtQuantity.Text, txtInTarget.Text, txtTarget.Text, txtPositive.Text, txtComments.Text, Request.QueryString("answerid"))

    '        For Each TmpRow As GridViewRow In grdRatingQuestions.Rows
    '            Master.Database.SaveRatingQuestionAnswer(Request.QueryString("answerid"), TmpRow.Cells(0).Text, DirectCast(TmpRow.Cells(3).FindControl("rblRating"), RadioButtonList).SelectedValue, DirectCast(TmpRow.Cells(6).FindControl("lblAnswerID"), Label).Text)
    '        Next
    '        For Each TmpItem As DataListItem In lstAnsweredQuestionaires.Items
    '            Dim TmpAnswer(0 To 9) As String
    '            For i As Integer = 0 To 9
    '                TmpAnswer(i) = DirectCast(TmpItem.FindControl("TextBox" & i + 1), TextBox).Text
    '            Next
    '            Master.Database.SaveCommentQuestionAnswer(Request.QueryString("answerid"), CInt(DirectCast(TmpItem.FindControl("lblID"), Label).Text), TmpAnswer, DirectCast(TmpItem.FindControl("lblAnswerID"), Label).Text)
    '        Next
    '        Response.Redirect("~/Default.aspx")
    '    End If
    'End Sub

    'Private Sub grdRatingQuestions_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdRatingQuestions.RowDataBound
    '    If Master.Database.GetLoggedInUserInfo.Type = cUserInfo.UserTypeEnum.Salesman OrElse Master.Database.GetLoggedInUserInfo.Type = cUserInfo.UserTypeEnum.HeadOfSales OrElse (Master.Database.GetLoggedInUserInfo.Type = cUserInfo.UserTypeEnum.Provider AndAlso lblAnswered.Value) Then
    '        If Not e.Row.FindControl("rblRating") Is Nothing Then
    '            DirectCast(e.Row.FindControl("rblRating"), RadioButtonList).Enabled = False
    '        End If
    '    End If
    'End Sub

    'Private Sub valAll_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles valAll.ServerValidate
    '    If Not valDate.IsValid Then
    '        args.IsValid = True
    '    Else
    '        For Each TmpRow As GridViewRow In grdRatingQuestions.Rows
    '            If Not DirectCast(TmpRow.FindControl("valRating"), RequiredFieldValidator).IsValid Then
    '                args.IsValid = False
    '                Exit Sub
    '            End If
    '        Next
    '        If Not valCellPhone.IsValid OrElse Not valMyName.IsValid OrElse Not valStartTime.IsValid OrElse Not valEndTime.IsValid Then
    '            args.IsValid = False
    '        End If
    '    End If
    'End Sub

End Class