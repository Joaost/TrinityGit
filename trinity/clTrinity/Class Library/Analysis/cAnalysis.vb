Namespace Trinity
    Public MustInherit Class cAnalysis

        Friend _progressWindow As frmProgress
        Private _vars As New Dictionary(Of String, Object)
        Friend _campaign As Trinity.cKampanj
        Friend _channelCount As Integer = 0
        Friend _btCount As Integer = 0
        Friend _filmCount As Integer = 0



        ' Flag if the printing should cease, usually set to true if errors makes the printing impossible
        Friend _flagCancelExport As Boolean = False
        Public Property FlagCancelExport As Boolean
            Get
                Return Me._flagCancelExport
            End Get
            Set(value As Boolean)
                Me._flagCancelExport = value
            End Set
        End Property

        Public Event DoBeforeRun()
        Public Event DoAfterRun()

        Public Event TemplateError(ByVal sender As Object, ByVal e As TemplateErrorArgs)


        Private _showCombinationsAsSingleRow As Boolean = True
        ''' <summary>
        ''' Gets or sets a value indicating whether the analysis should show combinations as a single row.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if combinations should be shown as a single row; otherwise, <c>false</c>.
        ''' </value>
        Public Property ShowCombinationsAsSingleRow() As Boolean
            Get
                Return _showCombinationsAsSingleRow
            End Get
            Set(ByVal value As Boolean)
                _showCombinationsAsSingleRow = value
                CalculateChannelCount()
            End Set
        End Property

        Private _doNotShowTemplateCheckAlerts As Boolean = False
        ''' <summary>
        ''' Gets or sets a value indicating whether alerts should be shown during CheckTemplate.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if alerts should be shown during CheckTemplate; otherwise, <c>false</c>.
        ''' </value>
        Public Property DoNotShowTemplateCheckAlerts() As Boolean
            Get
                Return _doNotShowTemplateCheckAlerts
            End Get
            Set(ByVal value As Boolean)
                _doNotShowTemplateCheckAlerts = value
            End Set
        End Property

        Private _includeCompensations As Boolean
        ''' <summary>
        ''' Gets or sets a value indicating whether to include compensations.
        ''' </summary>
        ''' <value><c>true</c> if compensations should be included; otherwise, <c>false</c>.</value>
        Public Property IncludeCompensations() As Boolean
            Get
                Return _includeCompensations
            End Get
            Set(ByVal value As Boolean)
                _includeCompensations = value
            End Set
        End Property

        Private _showProgressWindow As Boolean
        Public Property ShowProgressWindow() As String
            Get
                Return _showProgressWindow
            End Get
            Set(ByVal value As String)
                _showProgressWindow = value
            End Set
        End Property

        Private _autoHideProgressWindow As Boolean = True
        Public Property AutoHideProgressWindow() As Boolean
            Get
                Return _autoHideProgressWindow
            End Get
            Set(ByVal value As Boolean)
                _autoHideProgressWindow = value
            End Set
        End Property

        ''' <summary>
        ''' Sets the progress window.
        ''' </summary>
        ''' <param name="ProgressWindow">The progress window.</param>
        Public Sub SetProgressWindow(ByVal ProgressWindow As frmProgress)
            _progressWindow = ProgressWindow
        End Sub

        ''' <summary>
        ''' Calculates the channel count.
        ''' </summary>
        Private Sub CalculateChannelCount()
            _channelCount = 0
            _btCount = 0
            If ShowCombinationsAsSingleRow Then
                For Each TmpChan As Trinity.cChannel In Campaign.Channels
                    Dim BTCountThis As Integer = 0
                    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                        If TmpBT.BookIt AndAlso TmpBT.ShowMe Then
                            If BTCountThis = 0 Then
                                _channelCount += 1
                            End If
                            BTCountThis += 1
                        End If
                    Next
                    If BTCountThis > _btCount Then _btCount = BTCountThis
                Next
                For Each c As Trinity.cCombination In Campaign.Combinations
                    If c.ShowAsOne Then
                        _channelCount += 1
                        If c.Relations.count > _btCount Then _btCount = c.Relations.count
                    End If
                Next
            Else
                For Each TmpChan As Trinity.cChannel In Campaign.Channels
                    Dim BTCountThis As Integer = 0
                    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                        If TmpBT.BookIt Then
                            If BTCountThis = 0 Then
                                _channelCount += 1
                            End If
                            BTCountThis += 1
                        End If
                    Next
                    If BTCountThis > _btCount Then _btCount = BTCountThis
                Next
            End If
        End Sub

        ''' <summary>
        ''' Gets the channel count.
        ''' </summary>
        ''' <value>The channel count.</value>
        Friend ReadOnly Property ChannelCount() As Integer
            Get
                Return _channelCount
            End Get
        End Property

        ''' <summary>
        ''' Gets the booking type count.
        ''' </summary>
        ''' <value>The booking type count.</value>
        Friend ReadOnly Property BookingTypeCount() As Integer
            Get
                Return _btCount
            End Get
        End Property

        Friend ReadOnly Property FilmCodeCount() As Integer
            Get
                Return _campaign.Channels(1).BookingTypes(1).Weeks(1).Films.Count
            End Get
        End Property

        ''' <summary>
        ''' Sets the campaign to analyze.
        ''' </summary>
        ''' <param name="Campaign">The campaign.</param>
        Public Sub SetCampaign(ByVal Campaign As Trinity.cKampanj)
            _campaign = Campaign
            CalculateChannelCount()
        End Sub

        ''' <summary>
        ''' Sets the progress message.
        ''' </summary>
        ''' <param name="Message">The message.</param>
        Friend Sub SetProgress(Optional ByVal Message As String = "", Optional ByVal ProgressValue As Integer = 0)
            If _progressWindow IsNot Nothing Then
                If Message <> "" Then _progressWindow.Status = Message
                _progressWindow.Progress = ProgressValue
                _progressWindow.Show()
            End If
        End Sub

        ''' <summary>
        ''' Adds an analysis variable.
        ''' </summary>
        ''' <param name="Name">The variable name.</param>
        ''' <param name="Value">The variable value.</param>
        Public Sub AddVariable(ByVal Name As String, ByVal Value As Object)
            If Not _vars.ContainsKey(Name) Then
                _vars.Add(Name, Value)
            End If
            _vars(Name) = Value
        End Sub

        ''' <summary>
        ''' Gets an analysis variable.
        ''' </summary>
        ''' <param name="Name">The variable name.</param>
        ''' <param name="Default">The default value.</param>
        ''' <returns>Value of the variable</returns>
        Friend Function GetVariable(ByVal [Name] As String, Optional ByVal [Default] As Object = False) As Object
            If VariableExists(Name) Then
                Return _vars(Name)
            End If
            If Not [Default] Then
                Throw New KeyNotFoundException("The variable " & Name & " is needed, but has not been specified.")
            End If
            Return [Default]
        End Function

        ''' <summary>
        ''' Gets a value indicating whether a variable exists.
        ''' </summary>
        ''' <value><c>true</c> if variable exists; otherwise, <c>false</c>.</value>
        ReadOnly Property VariableExists(ByVal Name As String) As Boolean
            Get
                Return _vars.ContainsKey(Name)
            End Get
        End Property

        Friend Sub CreateProgressWindow()
            If ShowProgressWindow AndAlso _progressWindow Is Nothing Then
                _progressWindow = New frmProgress()
            End If

            If _progressWindow IsNot Nothing Then
                _progressWindow.Show()
            End If
        End Sub

        Friend Overridable Sub Run(ByVal NoBeforeEvent As Boolean, ByVal NoAfterEvent As Boolean)
            If Not NoBeforeEvent Then RaiseEvent DoBeforeRun()


            ' Runs a check if the chosen template is valid
            Dim TmpList As List(Of String) = Me.CheckTemplate()

            ' Check if any errors occured
            If TmpList.Count > 0 Then

                Dim tmpArgs As New TemplateErrorArgs(TmpList)
                RaiseEvent TemplateError(Me, tmpArgs)

                If Me.FlagCancelExport AndAlso DoNotShowTemplateCheckAlerts Then
                    Exit Sub
                End If
            End If

            CreateProgressWindow()

            InitializeAdvantedge()

            PrintHeader()
            PrintReach()
            PrintFilmList()

            PrintChannels()

            PrintCostDetails()

            If _autoHideProgressWindow AndAlso _progressWindow IsNot Nothing Then
                _progressWindow.Hide()
            End If

            If Not NoAfterEvent Then RaiseEvent DoAfterRun()
        End Sub


        Public Sub Run()
            Me.Run(False, False)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="cAnalysis" /> class.
        ''' </summary>
        ''' <param name="Campaign">The campaign to be analyzed.</param>
        Public Sub New(ByVal Campaign As Trinity.cKampanj)
            SetCampaign(Campaign)
        End Sub

        Friend Sub ShowTemplateError(ByVal Message As String, ByRef List As List(Of String))
            If Not DoNotShowTemplateCheckAlerts Then
                Windows.Forms.MessageBox.Show(Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            End If
            List.Add(Message)
        End Sub

        ''' <summary>
        ''' Initializes the advantedge object.
        ''' </summary>
        Overridable Sub InitializeAdvantedge()
            If _campaign.Adedge.validate > 0 Then
                _campaign.Adedge.setPeriod("-1d")
                _campaign.Adedge.setChannelsArea(_campaign.ChannelString, _campaign.Area)
                Trinity.Helper.AddTimeShift(_campaign.Adedge)
                _campaign.Adedge.setArea(_campaign.Area)                
            End If
            Trinity.Helper.AddTargetsToAdedge(_campaign.Adedge, True)
        End Sub

#Region "MustOverride Methods"

        ''' <summary>
        ''' Checks the template to see that it is correct.
        ''' </summary>
        Friend MustOverride Function CheckTemplate() As List(Of String)
        ''' <summary>
        ''' Prints the header.
        ''' </summary>
        Friend MustOverride Sub PrintHeader()
        ''' <summary>
        ''' Prints the reach.
        ''' </summary>
        Friend MustOverride Sub PrintReach()
        ''' <summary>
        ''' Prints the film list.
        ''' </summary>
        Friend MustOverride Sub PrintFilmList()
        ''' <summary>
        ''' Prints the channels.
        ''' </summary>
        Friend MustOverride Sub PrintChannels()
        ''' <summary>
        ''' Prints one channel.
        ''' </summary>
        ''' <param name="Channel">The channel.</param>
        Friend MustOverride Sub PrintChannel(ByVal Channel As Trinity.cChannel)
        ''' <summary>
        ''' Prints one booking type.
        ''' </summary>
        ''' <param name="BookingType">Type of the booking.</param>
        Friend MustOverride Sub PrintBookingType(ByVal BookingType As Trinity.cBookingType, Optional ByVal ChannelLabel As String = "", Optional ByVal BookingtypeLabel As String = "")
        ''' <summary>
        ''' Prints the cost per filmcode for a booking type.
        ''' </summary>
        ''' <param name="BookingType">Booking type.</param>
        Friend MustOverride Sub PrintCostPerFilmcode(ByVal BookingType As Trinity.cBookingType)
        ''' <summary>
        ''' Prints the budgets.
        ''' </summary>
        ''' <param name="BookingType">Booking type to print budgets for.</param>
        Friend MustOverride Sub PrintBudgets(ByVal BookingType As Trinity.cBookingType)
        ''' <summary>
        ''' Prints the week.
        ''' </summary>
        ''' <param name="Week">The week.</param>
        Friend MustOverride Sub PrintWeek(ByVal Week As Trinity.cWeek)
        ''' <summary>
        ''' Prints the compensations.
        ''' </summary>
        ''' <param name="Week">The week.</param>
        Friend MustOverride Sub PrintCompensations(ByVal Week As Trinity.cWeek)
        ''' <summary>
        ''' Prints a film.
        ''' </summary>
        ''' <param name="Film">The film.</param>
        Friend MustOverride Sub PrintFilm(ByVal Film As Trinity.cFilm, ByVal Week As Trinity.cWeek)
        ''' <summary>
        ''' Prints a daypart.
        ''' </summary>
        ''' <param name="Daypart">The daypart.</param>
        ''' <param name="Week">The week.</param>
        Friend MustOverride Sub PrintDaypart(ByVal Daypart As Integer, ByVal Week As cWeek)
        Friend MustOverride Sub PrintDaypart(ByVal Daypart As Integer, ByVal Channel As cChannel)

        ''' <summary>
        ''' Prints the combinations.
        ''' </summary>
        Friend MustOverride Sub PrintCombinations()
        ''' <summary>
        ''' Prints the cost details.
        ''' </summary>
        Friend MustOverride Sub PrintCostDetails()
        ''' <summary>
        ''' Prints the pos in break.
        ''' </summary>
        Friend MustOverride Sub PrintPosInBreak()

#End Region

    End Class
End Namespace