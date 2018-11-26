Imports clTrinity.CultureSafeExcel

Namespace Trinity
    Public MustInherit Class cExcelAnalysis
        Inherits cAnalysis

        ''' <summary>
        ''' Instance of Excel where the active workbook should be written to
        ''' </summary>
        Friend _excel As Application

        ''' <summary>
        ''' Gets the Excel sheet where data should be written.
        ''' </summary>
        ''' <returns></returns>
        Friend MustOverride Function GetSheet() As String

        Friend Overrides Function CheckTemplate() As System.Collections.Generic.List(Of String)

            Dim _list As New List(Of String)

            If _campaign.Channels(1).BookingTypes(1).Weeks.Count > GetVariable("Weeks") Then
                ShowTemplateError("The template you are trying to use is adjusted for " & GetVariable("Weeks") & " weeks" & vbCrLf & "and this campaign has " & _campaign.Channels(1).BookingTypes(1).Weeks.Count & " weeks, wich may cause errors in the plan.", _list)
            End If
            If ChannelCount > GetVariable("Channels") Then
                ShowTemplateError("The template you are trying to use is adjusted for " & GetVariable("Channels") & " channels" & vbCrLf & "and this campaign has " & ChannelCount & " channels, wich may cause errors in the plan.", _list)
            End If
            If BookingTypeCount > GetVariable("Bookingtypes") Then
                ShowTemplateError("The template you are trying to use is adjusted for " & GetVariable("Bookingtypes") & " bookingtypes" & vbCrLf & "and this campaign has " & BookingTypeCount & " bookingtypes, wich may cause errors in the plan.", _list)
            End If
            If FilmCodeCount > GetVariable("Filmcodes") Then
                ShowTemplateError("The template you are trying to use is adjusted for " & GetVariable("Filmcodes") & " films" & vbCrLf & "and this campaign has " & FilmCodeCount & " films, wich may cause errors in the plan.", _list)
            End If
            Return _list

        End Function

        ''' <summary>
        ''' Prints the report header.
        ''' </summary>
        Friend Overrides Sub PrintHeader()
            With _excel.Sheets(GetSheet())
                .Range("B3").Value = _campaign.Name
                .Range("B4").value = _campaign.Client
                .Range("B5").value = _campaign.Product
                Dim StartWeek As Integer = DatePart(DateInterval.WeekOfYear, Date.FromOADate(_campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                Dim EndWeek As Integer = DatePart(DateInterval.WeekOfYear, Date.FromOADate(_campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                Dim PeriodStr As String = StartWeek
                If EndWeek > StartWeek Then PeriodStr = PeriodStr & " - " & EndWeek
                PeriodStr = PeriodStr & ", " & Format(Date.FromOADate(_campaign.StartDate), "yyyy")
                .Range("B6").value = PeriodStr
                .Range("B7").value = StartWeek
                .Range("B8").value = EndWeek
                .Range("B9").value = _campaign.StartDate
                .Range("B10").value = _campaign.EndDate
                .Range("E3").value = _campaign.Buyer
                .Range("G3").value = _campaign.BuyerEmail
                .Range("I3").value = _campaign.BuyerPhone
                .Range("E4").value = _campaign.Planner
                .Range("G4").value = _campaign.PlannerEmail
                .Range("I4").value = _campaign.PlannerPhone

                'Print target sizes
                .Range("D6").Numberformat = "@"
                .Range("D6").FORMULA = _campaign.MainTarget.TargetNameNice
                .Range("E6").value = _campaign.MainTarget.UniSize * 1000

                .Range("D7").NUMBERFORMAT = "@"
                .Range("D7").FORMULA = _campaign.SecondaryTarget.TargetNameNice
                .Range("E7").value = _campaign.SecondaryTarget.UniSize * 1000

                .Range("D8").NUMBERFORMAT = "@"
                .Range("D8").FORMULA = _campaign.ThirdTarget.TargetNameNice
                .Range("E8").value = _campaign.ThirdTarget.UniSize * 1000

                .Range("E9").value = _campaign.Adedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , , _campaign.TargColl(_campaign.AllAdults, _campaign.Adedge) - 1) * 1000

            End With
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="cExcelAnalysis" /> class.
        ''' </summary>
        ''' <param name="Campaign">The campaign to be analyzed.</param>
        ''' <param name="Excel">The excel workbook to print analysis to.</param>
        Sub New(ByVal Campaign As cKampanj, ByVal Excel As Application)
            MyBase.New(Campaign)
            _excel = Excel
            ParseVariables()
        End Sub

        ''' <summary>
        ''' Parses the analysis variables from "Setup" Excel sheet.
        ''' </summary>
        Friend Sub ParseVariables()
            With _excel.Sheets("Setup")
                Dim r As Integer = 3
                While Not .cells(r, 2).Value Is Nothing
                    AddVariable(.cells(r, 2).value, .cells(r, 3).value)
                    r += 1
                End While
            End With
        End Sub
    End Class
End Namespace
