Public Class frmPrint

   
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Dim Word As Object = CreateObject("Word.Application")
        Dim Doc As Object = Word.Documents.Add(Template:="""" & BalthazarSettings.DataFolder & "Docs\template.doc" & """", NewTemplate:=False, DocumentType:=0)
        Dim Lang As New cLanguage
        Dim Sel As Object = Word.Selection

        Sel.Font.Name = "Arial"
        Sel.Font.Size = 10

        Dim HeadlineCount As Integer = 0
        For Each TmpItem As String In lstChosen.Items
            Select Case TmpItem
                Case "Contacts"
                    Sel.ParagraphFormat.TabStops.Add(Position:=Word.CentimetersToPoints(5.4), Alignment:=0, Leader:=0)
                    Sel.ParagraphFormat.TabStops.Add(Position:=Word.CentimetersToPoints(11.75), Alignment:=0, Leader:=0)
                    Sel.Font.Bold = True
                    Sel.font.SmallCaps = True
                    Sel.TypeText("MEC Access")
                    Sel.TypeParagraph()
                    Sel.Font.Bold = False
                    Sel.font.SmallCaps = False
                    For Each TmpContact As cContact In MyEvent.InternalContacts
                        Sel.TypeText(Text:=TmpContact.Role & vbTab & TmpContact.Name & vbTab & TmpContact.PhoneNr)
                        Sel.TypeParagraph()
                    Next
                    Sel.TypeParagraph()
                    For Each TmpContact As cContact In MyEvent.ExternalContacts
                        Sel.Font.Bold = True
                        Sel.font.SmallCaps = True
                        If TmpContact.Role <> "" Then
                            Sel.TypeText(Text:=TmpContact.Role & ":")
                        End If
                        Sel.Font.Bold = False
                        Sel.font.SmallCaps = False
                        Sel.typetext(vbTab & TmpContact.Name & vbTab & TmpContact.PhoneNr)
                        Sel.TypeParagraph()
                    Next
                    Sel.TypeParagraph()
                Case "Background"
                    Sel.Font.Bold = True
                    Sel.font.SmallCaps = True
                    Sel.TypeText(Text:=Lang("Background"))
                    Sel.TypeParagraph()
                    Sel.Font.Bold = False
                    Sel.font.SmallCaps = False
                    Sel.TypeText(Text:=MyEvent.Campaign.Background)
                    Sel.TypeParagraph()
                    Sel.TypeParagraph()
                Case "What/Purpose/How/When"
                    Sel.ParagraphFormat.TabStops.Add(Position:=Word.CentimetersToPoints(1.59), Alignment:=0, Leader:=0)

                    Sel.font.underline = True
                    Sel.TypeText(Text:=Lang("What") & ":")
                    Sel.font.underline = False
                    Sel.typetext(vbTab & MyEvent.Campaign.What)
                    Sel.TypeParagraph()

                    Sel.font.underline = True
                    Sel.TypeText(Text:=Lang("Purpose") & ":")
                    Sel.font.underline = False
                    Sel.typetext(vbTab & MyEvent.Campaign.Purpose)
                    Sel.TypeParagraph()

                    Sel.font.underline = True
                    Sel.TypeText(Text:=Lang("How") & ":")
                    Sel.font.underline = False
                    Sel.typetext(vbTab & MyEvent.Campaign.How)
                    Sel.TypeParagraph()

                    Sel.font.underline = True
                    Sel.TypeText(Text:=Lang("When") & ":")
                    Sel.font.underline = False
                    Sel.typetext(vbTab & MyEvent.Campaign.WhenIsIt)
                    Sel.TypeParagraph()
                    Sel.TypeParagraph()
                Case "Our Mission"
                    Sel.Font.Bold = True
                    Sel.font.SmallCaps = True
                    Sel.TypeText(Text:=Lang("Our mission"))
                    Sel.TypeParagraph()
                    Sel.Font.Bold = False
                    Sel.font.SmallCaps = False
                    Sel.typetext(MyEvent.OurMission)
                    Sel.TypeParagraph()
                    Sel.TypeParagraph()
                Case "Purpose"
                    Sel.Font.Bold = True
                    Sel.font.SmallCaps = True
                    Sel.TypeText(Text:=Lang("Purpose"))
                    Sel.TypeParagraph()
                    Sel.Font.Bold = False
                    Sel.font.SmallCaps = False
                    With Word.ListGalleries(1).ListTemplates(1).ListLevels(1)
                        .NumberFormat = "r"
                        .TrailingCharacter = 0
                        .NumberStyle = 23 'wdListNumberStyleBullet
                        .NumberPosition = Word.CentimetersToPoints(0.63)
                        .Alignment = 0
                        .TextPosition = Word.CentimetersToPoints(1.27)
                        .TabPosition = Word.CentimetersToPoints(1.27)
                        .ResetOnHigher = 0
                        .StartAt = 1
                        With .Font
                            .Bold = 9999999
                            .Italic = 9999999
                            .StrikeThrough = 9999999
                            .Subscript = 9999999
                            .Superscript = 9999999
                            .Shadow = 9999999
                            .Outline = 9999999
                            .Emboss = 9999999
                            .Engrave = 9999999
                            .AllCaps = 9999999
                            .Hidden = 9999999
                            .Underline = 9999999
                            .Color = 9999999
                            .Size = 9
                            .Animation = 9999999
                            .DoubleStrikeThrough = 9999999
                            .Name = "Wingdings"
                        End With
                        .LinkedStyle = ""
                    End With
                    Sel.Range.ListFormat.ApplyListTemplate(ListTemplate:=Word.ListGalleries(1).ListTemplates(1), ContinuePreviousList:=False, ApplyTo:=0, DefaultListBehavior:=2)
                    For Each TmpPurpose As String In MyEvent.Purposes
                        Sel.TypeText(Text:=TmpPurpose)
                        Sel.TypeParagraph()
                    Next
                    Sel.Range.ListFormat.RemoveNumbers(NumberType:=1)
                    Sel.TypeParagraph()
                Case "Target"
                    Sel.Font.Bold = True
                    Sel.font.SmallCaps = True
                    Sel.TypeText(Text:=Lang("Target"))
                    Sel.TypeParagraph()
                    Sel.Font.Bold = False
                    Sel.font.SmallCaps = False
                    Sel.typetext(Text:=MyEvent.Target)
                    Sel.TypeParagraph()
                    Sel.TypeParagraph()
                Case "Message"
                    Sel.Font.Bold = True
                    Sel.font.SmallCaps = True
                    Sel.TypeText(Text:=Lang("Message"))
                    Sel.TypeParagraph()
                    Sel.Font.Bold = False
                    Sel.font.SmallCaps = False
                    Sel.typetext(Text:=MyEvent.Message)
                    Sel.TypeParagraph()
                    Sel.TypeParagraph()
                Case "Goals"
                    Sel.Font.Bold = True
                    Sel.font.SmallCaps = True
                    Sel.TypeText(Text:=Lang("Campaign goal"))
                    Sel.TypeParagraph()
                    Sel.Font.Bold = False
                    Sel.font.SmallCaps = False
                    With Word.ListGalleries(1).ListTemplates(1).ListLevels(1)
                        .NumberFormat = "r"
                        .TrailingCharacter = 0
                        .NumberStyle = 23 'wdListNumberStyleBullet
                        .NumberPosition = Word.CentimetersToPoints(0.63)
                        .Alignment = 0
                        .TextPosition = Word.CentimetersToPoints(1.27)
                        .TabPosition = Word.CentimetersToPoints(1.27)
                        .ResetOnHigher = 0
                        .StartAt = 1
                        With .Font
                            .Bold = 9999999
                            .Italic = 9999999
                            .StrikeThrough = 9999999
                            .Subscript = 9999999
                            .Superscript = 9999999
                            .Shadow = 9999999
                            .Outline = 9999999
                            .Emboss = 9999999
                            .Engrave = 9999999
                            .AllCaps = 9999999
                            .Hidden = 9999999
                            .Underline = 9999999
                            .Color = 9999999
                            .Size = 9
                            .Animation = 9999999
                            .DoubleStrikeThrough = 9999999
                            .Name = "Wingdings"
                        End With
                        .LinkedStyle = ""
                    End With
                    Sel.Range.ListFormat.ApplyListTemplate(ListTemplate:=Word.ListGalleries(1).ListTemplates(1), ContinuePreviousList:=False, ApplyTo:=0, DefaultListBehavior:=2)
                    For Each TmpGoal As String In MyEvent.Goals
                        Sel.TypeText(Text:=TmpGoal)
                        Sel.TypeParagraph()
                    Next
                    Sel.Range.ListFormat.RemoveNumbers(NumberType:=1)
                    Sel.TypeParagraph()
                Case "Core values"
                    Sel.Font.Bold = True
                    Sel.font.SmallCaps = True
                    Sel.TypeText(Text:=Lang("Core values"))
                    Sel.TypeParagraph()
                    Sel.Font.Bold = False
                    Sel.font.SmallCaps = False
                    Sel.typetext(Text:=MyEvent.CoreValues)
                    Sel.TypeParagraph()
                    Sel.TypeParagraph()
                Case "Q&A"
                    Sel.Font.Bold = True
                    Sel.font.SmallCaps = True
                    Sel.TypeText(Text:=Lang("Q&A") & " - " & MyEvent.Name)
                    Sel.TypeParagraph()
                    Sel.Font.Bold = False
                    Sel.font.SmallCaps = False
                    For Each TmpQA As cQuestionAndAnswer In MyEvent.QuestionAndAnswers
                        Sel.TypeParagraph()
                        Sel.Font.Bold = True
                        Sel.TypeText(Text:=TmpQA.Question)
                        Sel.Font.Bold = False
                        Sel.TypeParagraph()
                        Sel.TypeText(Text:=TmpQA.Answer)
                        Sel.TypeParagraph()
                    Next
                Case "Staff profiles"
                    Sel.Font.Bold = True
                    Sel.Font.SmallCaps = True
                    Sel.TypeText(Text:=Lang("Staff profiles"))
                    Sel.TypeParagraph()
                    Sel.TypeParagraph()
                    Sel.Font.Bold = False
                    Sel.Font.SmallCaps = False

                    Dim TmpStaffList As New Dictionary(Of Integer, cStaff)
                    For Each TmpLoc As cStaffScheduleLocation In MyEvent.Schedule.Locations
                        For Each TmpRole As cStaffScheduleRole In TmpLoc.Roles
                            For Each TmpDay As cStaffScheduleDay In TmpRole.Days
                                For Each TmpShift As cStaffScheduleShift In TmpDay.Shifts
                                    For Each TmpStaff As cStaff In TmpShift.AssignedStaff
                                        If Not TmpStaffList.ContainsKey(TmpStaff.DatabaseID) Then
                                            TmpStaffList.Add(TmpStaff.DatabaseID, Database.GetSingleStaff(TmpStaff.DatabaseID))
                                        End If
                                    Next
                                Next
                            Next
                        Next
                    Next
                    For Each TmpStaff As cStaff In TmpStaffList.Values
                        Sel.Font.Bold = True
                        Sel.TypeText(Text:=TmpStaff.LastName & ", " & TmpStaff.FirstName)
                        Sel.TypeParagraph()
                        Dim TmpPic As String = My.Computer.FileSystem.GetTempFileName
                        If Not TmpStaff.Picture Is Nothing Then
                            TmpStaff.Picture.Save(TmpPic)
                            Sel.InlineShapes.AddPicture(FileName:=TmpPic, LinkToFile:=False, SaveWithDocument:=True)
                            Kill(TmpPic)
                            Sel.TypeParagraph()
                        End If

                        Sel.TypeParagraph()
                    Next
                Case "<Page break>"
                    Sel.InsertBreak(Type:=7)
                Case "<Headline>"
                    HeadlineCount += 1
                    Sel.Font.Bold = True
                    Sel.font.SmallCaps = True
                    Sel.TypeText(HeadlineCount & ".")
                Case Else
                    If TmpItem.Substring(0, 12) = "Day Template" Then
                        Dim Template As String = TmpItem.Substring(14)
                        Dim TmpDT As cDayTemplate = Nothing
                        Dim FoundIt As Boolean = False
                        For Each TmpDT In MyEvent.DayTemplates
                            If TmpDT.Name = Template Then
                                FoundIt = True
                                Exit For
                            End If
                        Next
                        If FoundIt Then
                            Dim Roles As New Collection
                            For Each TmpShift As cShift In TmpDT.Shifts.Values
                                For Each TmpRole As cRole In TmpShift.Roles
                                    If Not Roles.Contains(TmpRole.ID) And TmpRole.Quantity > 0 Then
                                        Roles.Add(TmpRole, TmpRole.ID)
                                    End If
                                Next
                            Next
                            Sel.Font.Bold = True
                            Sel.font.SmallCaps = True
                            Sel.TypeText(Text:=Lang("Workday example") & " - " & TmpDT.Name)
                            Sel.TypeParagraph()
                            Sel.Font.Bold = False
                            Sel.font.SmallCaps = False
                            Sel.TypeParagraph()
                            With Doc.Tables.Add(Doc.range(Sel.start, Sel.end), TmpDT.Shifts.Count + 1, Roles.Count + 2, 1, 1)
                                .Rows(1).cells(1).Select()
                                If .Style IsNot "Tabellrutnät" Then
                                    .Style = "Tabellrutnät"
                                End If
                                .ApplyStyleHeadingRows = True
                                .ApplyStyleLastRow = True
                                .ApplyStyleFirstColumn = True
                                .ApplyStyleLastColumn = True
                                Sel.Font.Bold = True
                                Sel.TypeText(Text:=Lang("Time"))
                                Sel.MoveRight(Unit:=12)
                                Sel.Font.Bold = True
                                Sel.TypeText(Text:=Lang("Shift name"))
                                Sel.MoveRight(Unit:=12)
                                For Each TmpRole As cRole In Roles
                                    Sel.Font.Bold = True
                                    Sel.TypeText(Text:=TmpRole.Name)
                                    Sel.MoveRight(Unit:=12)
                                Next
                                For Each TmpShift As cShift In TmpDT.Shifts.Values
                                    Sel.Font.Bold = False
                                    Sel.TypeText(Text:=TmpShift.StartTime & "-" & TmpShift.EndTime)
                                    Sel.MoveRight(Unit:=12)
                                    Sel.Font.Bold = False
                                    Sel.TypeText(TmpShift.Name)
                                    Sel.MoveRight(Unit:=12)
                                    For Each TmpRole As cRole In Roles
                                        Sel.Font.Bold = False
                                        Sel.TypeText(Text:=CStr(TmpShift.Roles(TmpRole.ID).Quantity))
                                        Sel.MoveRight(Unit:=12)
                                    Next
                                Next
                                .rows(TmpDT.Shifts.Count + 2).delete()
                                Sel.TypeParagraph()
                            End With
                        End If
                    ElseIf TmpItem.Substring(0, 9) = "Template:" Then
                        Dim Template As String = TmpItem.Substring(10)
                        Dim TmpTemplate As cTemplate = Nothing
                        Dim FoundIt As Boolean = False
                        For Each TmpTemplate In MyEvent.Templates
                            If TmpTemplate.Name = Template Then
                                FoundIt = True
                                Exit For
                            End If
                        Next
                        If FoundIt Then
                            Dim SaveStart As Integer = Sel.Start
                            Sel.Font.Bold = True
                            Sel.font.SmallCaps = True
                            Sel.TypeText(TmpTemplate.Description)
                            Sel.TypeParagraph()
                            Sel.Font.Bold = False
                            Sel.font.SmallCaps = False
                            Sel.TypeParagraph()
                            Clipboard.Clear()
                            Clipboard.SetData(System.Windows.Forms.DataFormats.Rtf, TmpTemplate.Text)
                            Sel.PasteAndFormat(20)
                            Sel.HomeKey(Unit:=5)
                            Sel.EndKey(Unit:=6)
                            Dim SaveEnd As Integer = Sel.start
                            Sel.start = SaveStart
                            Sel.end = SaveStart
                            With Word.ListGalleries(1).ListTemplates(1).ListLevels(1)
                                .NumberFormat = "r"
                                .TrailingCharacter = 0
                                .NumberStyle = 23 'wdListNumberStyleBullet
                                .NumberPosition = Word.CentimetersToPoints(0.63)
                                .Alignment = 0
                                .TextPosition = Word.CentimetersToPoints(1.27)
                                .TabPosition = Word.CentimetersToPoints(1.27)
                                .ResetOnHigher = 0
                                .StartAt = 1
                                With .Font
                                    .Bold = 9999999
                                    .Italic = 9999999
                                    .StrikeThrough = 9999999
                                    .Subscript = 9999999
                                    .Superscript = 9999999
                                    .Shadow = 9999999
                                    .Outline = 9999999
                                    .Emboss = 9999999
                                    .Engrave = 9999999
                                    .AllCaps = 9999999
                                    .Hidden = 9999999
                                    .Underline = 9999999
                                    .Color = 9999999
                                    .Size = 9
                                    .Animation = 9999999
                                    .DoubleStrikeThrough = 9999999
                                    .Name = "Wingdings"
                                End With
                                .LinkedStyle = ""
                            End With
                            While Sel.start < SaveEnd
                                If Sel.Range.ListFormat.SingleList Then
                                    'Stop
                                    Sel.Range.ListFormat.ApplyListTemplate(ListTemplate:=Word.ListGalleries(1).ListTemplates(1), ContinuePreviousList:=False, ApplyTo:=0, DefaultListBehavior:=2)
                                End If
                                Sel.movedown(Unit:=5, Count:=1)
                                Sel.start = Sel.end
                            End While
                        End If
                    End If
            End Select
        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Word.visible = True
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        lstChosen.Items.Add(lstAvailable.SelectedItem)
        If Not lstAvailable.SelectedItem.ToString.Substring(0, 1) = "<" Then
            lstAvailable.Items.Remove(lstAvailable.SelectedItem)
        End If
    End Sub

    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        If Not lstChosen.SelectedItem.ToString.Substring(0, 1) = "<" Then
            lstAvailable.Items.Add(lstChosen.SelectedItem)
        End If
        lstChosen.Items.Remove(lstChosen.SelectedItem)
    End Sub

    Private Sub frmPrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lstAvailable.Items.Clear()
        lstAvailable.Items.AddRange(New Object() {"Contacts", "Background", "What/Purpose/How/When", "Our Mission", "Purpose", "Target", "Message", "Goals", "Core values", "Staff schedule", "Locations", "Q&A", "Staff profiles", "<Headline>", "<Page break>"})
        For Each TmpDT As cDayTemplate In MyEvent.DayTemplates
            lstAvailable.Items.Add("Day Template: " & TmpDT.Name)
        Next
        For Each TmpTemplate As cTemplate In MyEvent.Templates
            lstAvailable.Items.Add(TmpTemplate.ToString)
        Next

        lstChosen.Items.Clear()
    End Sub

    Private Sub cmdMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveUp.Click
        If lstChosen.SelectedIndex = 0 Then Exit Sub
        Dim TmpString As String = lstChosen.Items(lstChosen.SelectedIndex - 1)
        lstChosen.Items(lstChosen.SelectedIndex - 1) = lstChosen.Items(lstChosen.SelectedIndex)
        lstChosen.Items(lstChosen.SelectedIndex) = TmpString
        lstChosen.SelectedIndex -= 1
    End Sub

    Private Sub cmdMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveDown.Click
        If lstChosen.SelectedIndex = lstChosen.Items.Count - 1 Then Exit Sub
        Dim TmpString As String = lstChosen.Items(lstChosen.SelectedIndex + 1)
        lstChosen.Items(lstChosen.SelectedIndex + 1) = lstChosen.Items(lstChosen.SelectedIndex)
        lstChosen.Items(lstChosen.SelectedIndex) = TmpString
        lstChosen.SelectedIndex += 1
    End Sub

    Private Sub lstAvailable_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstAvailable.DoubleClick
        cmdAdd_Click(New Object, New EventArgs)
    End Sub

    Private Sub lstChosen_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstChosen.DoubleClick
        cmdRemove_Click(New Object, New EventArgs)
    End Sub

    Private Sub cmdSaveTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveTemplate.Click
        Dim TmpName As String = InputBox("Name of template", "BALTHAZAR", "New template")
        If TmpName = "" Then Exit Sub
        If Not My.Computer.FileSystem.FileExists(BalthazarSettings.DataFolder & "Templates\" & TmpName & ".xml") Then
            Dim XMLDoc As New Xml.XmlDocument
            Dim XMLTemplate As Xml.XmlElement = XMLDoc.CreateElement("Template")
            XMLTemplate.SetAttribute("Name", TmpName)
            For Each TmpString As String In lstChosen.Items
                Dim XMLChosen As Xml.XmlElement = XMLDoc.CreateElement("Item")
                XMLChosen.SetAttribute("Text", TmpString)
                XMLTemplate.AppendChild(XMLChosen)
            Next
            XMLDoc.AppendChild(XMLTemplate)
            XMLDoc.Save(BalthazarSettings.DataFolder & "Templates\" & TmpName & ".xml")
        Else
            If Windows.Forms.MessageBox.Show("The template already exists. Replace?", "T R I N I T Y", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
                Dim XMLDoc As New Xml.XmlDocument
                Dim XMLTemplate As Xml.XmlElement = XMLDoc.CreateElement("Template")
                XMLTemplate.SetAttribute("Name", TmpName)
                For Each TmpString As String In lstChosen.Items
                    Dim XMLChosen As Xml.XmlElement = XMLDoc.CreateElement("Item")
                    XMLChosen.SetAttribute("Text", TmpString)
                    XMLTemplate.AppendChild(XMLChosen)
                Next
                XMLDoc.AppendChild(XMLTemplate)
                XMLDoc.Save(BalthazarSettings.DataFolder & "Templates\" & TmpName & ".xml")
            End If
            Exit Sub
        End If
    End Sub

    Private Sub cmdOpenTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpenTemplate.Click

        frmTemplates.ShowDialog()
        If frmTemplates.lvwTemplates.SelectedItems.Count = 0 Then Exit Sub

        Dim XMLDoc As New Xml.XmlDocument

        XMLDoc.LoadXml(frmTemplates.lvwTemplates.SelectedItems(0).Tag)

        lstChosen.Items.Clear()
        lstAvailable.Items.Clear()
        lstAvailable.Items.AddRange(New Object() {"Contacts", "Background", "What/Purpose/How/When", "Our Mission", "Purpose", "Target", "Message", "Goals", "Core values", "Staff schedule", "Locations", "Q&A", "<Headline>", "<Page break>"})
        For Each TmpDT As cDayTemplate In MyEvent.DayTemplates
            lstAvailable.Items.Add("Day Template: " & TmpDT.Name)
        Next
        For Each TmpTemplate As cTemplate In MyEvent.Templates
            lstAvailable.Items.Add(TmpTemplate.ToString)
        Next

        Dim XMLChosen As Xml.XmlElement = XMLDoc.GetElementsByTagName("Template").Item(0).FirstChild

        While Not XMLChosen Is Nothing
            Try
                If Not XMLChosen.GetAttribute("Text").ToString.Substring(0, 1) = "<" Then
                    lstAvailable.Items.RemoveAt(lstAvailable.Items.IndexOf(XMLChosen.GetAttribute("Text")))
                End If
                lstChosen.Items.Add(XMLChosen.GetAttribute("Text"))
            Catch
                frmChooseOther.Status = XMLChosen.GetAttribute("Text") & " was not found."
                frmChooseOther.lstList.Items.Clear()
                If XMLChosen.GetAttribute("Text").ToString.Substring(0, 9) = "Template:" Then
                    For Each TmpTemplate As cTemplate In MyEvent.Templates
                        frmChooseOther.lstList.Items.Add(TmpTemplate)
                    Next
                ElseIf XMLChosen.GetAttribute("Text").ToString.Substring(0, 12) = "Day Template" Then
                    For Each TmpDT As cDayTemplate In MyEvent.DayTemplates
                        frmChooseOther.lstList.Items.Add(TmpDT)
                    Next
                End If
                Select Case frmChooseOther.ShowDialog
                    Case Windows.Forms.DialogResult.Retry
                        'Replace
                        lstAvailable.Items.RemoveAt(lstAvailable.Items.IndexOf(frmChooseOther.lstList.SelectedItem.ToString))
                        lstChosen.Items.Add(frmChooseOther.lstList.SelectedItem.ToString)
                    Case Windows.Forms.DialogResult.Ignore
                        'Rename
                        lstAvailable.Items.RemoveAt(lstAvailable.Items.IndexOf(frmChooseOther.lstList.SelectedItem.ToString))
                        frmChooseOther.lstList.SelectedItem.name = XMLChosen.GetAttribute("Text").Substring(XMLChosen.GetAttribute("Text").IndexOf(":") + 2)
                        lstChosen.Items.Add(XMLChosen.GetAttribute("Text"))
                End Select
            End Try
            XMLChosen = XMLChosen.NextSibling
        End While

    End Sub
End Class