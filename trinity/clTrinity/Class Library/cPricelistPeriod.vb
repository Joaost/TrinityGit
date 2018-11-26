Namespace Trinity
    Public Class cPricelistPeriod
        Implements IDetectsProblems

        Private mvarName As String
        Private mvarTargetNat As Integer ' national universe
        Private mvarTargetUni As Integer ' Target universe
        Private mvarFromDate As Date
        Private mvarToDate As Date
        Private mvarID As String = ""
        Private mvarPrice(0 To cKampanj.MAX_DAYPARTS) As Single
        Private Main As Trinity.cKampanj
        Private mvarPricelistPeriods As cPricelistPeriods

        Private bolPriceOnCPP As Boolean = True

        Private Delegate Sub PriceListChangedHandler()
        Private Delegate Sub TargetChangedHandler()

        Private PriceListDelegate As PriceListChangedHandler
        Private TargetChangedDelegate As TargetChangedHandler

        Private BeforeCampaignStartDate As Boolean = False
        Private AfterCampaignStartDate As Boolean = False
        Private InCampaign As Boolean = False



        Public ReadOnly Property PricelistPeriods() As cPricelistPeriods
            Get
                Return mvarPricelistPeriods
            End Get
        End Property

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided
            'it will return True of succeded and false if failed

            On Error GoTo On_Error

            colXml.SetAttribute("ID", Me.ID)
            colXml.SetAttribute("Name", Me.Name)
            colXml.SetAttribute("isCPP", Me.PriceIsCPP)
            colXml.SetAttribute("Price", Me.Price(Me.PriceIsCPP))
            For i As Integer = 0 To Main.Dayparts.Count - 1
                colXml.SetAttribute("PriceDP" & i, Me.Price(Me.PriceIsCPP, i))
            Next
            colXml.SetAttribute("UniSize", Me.TargetUni)
            colXml.SetAttribute("UniSizeNat", Me.TargetNat)
            colXml.SetAttribute("FromDate", Me.FromDate)
            colXml.SetAttribute("ToDate", Me.ToDate)

            Exit Function

On_Error:
            colXml.AppendChild(xmlDoc.CreateComment("ERROR (" & Err.Number & "): " & Err.Description))
            errorMessege.Add("Error saving PriceListPeriod for ID (" & Me.ID & ")")
            Resume Next
        End Function

        Public Sub AddActualspotToWatch(ByVal spot As cActualSpot)

            Dim del As New PriceListChangedHandler(AddressOf spot.PriceListChanged)
            PriceListDelegate = MulticastDelegate.Combine(PriceListDelegate, del)

            Dim del1 As New TargetChangedHandler(AddressOf spot.TargetsChanged)
            TargetChangedDelegate = MulticastDelegate.Combine(TargetChangedDelegate, del1)
        End Sub

        Public Sub TargetWasChanged()
            If TargetChangedDelegate Is Nothing Then Exit Sub
            TargetChangedDelegate.DynamicInvoke()
        End Sub

        Public Sub PriceListWasChanged()
            If PriceListDelegate Is Nothing Then Exit Sub
            PriceListDelegate.DynamicInvoke()
        End Sub

        Public Property PriceIsCPP() As Boolean
            Get
                Return bolPriceOnCPP
            End Get
            Set(ByVal value As Boolean)
                If bolPriceOnCPP = value Then Exit Property
                If bolPriceOnCPP Then
                    For i As Integer = 0 To mvarPrice.Length - 1
                        mvarPrice(i) = mvarPrice(i) / mvarTargetUni * 100
                    Next
                    bolPriceOnCPP = value
                Else
                    For i As Integer = 0 To mvarPrice.Length - 1
                        mvarPrice(i) = mvarPrice(i) * mvarTargetUni / 100
                    Next
                    bolPriceOnCPP = value
                End If
            End Set
        End Property

        Public Property TargetNat() As Integer
            Get
                Return mvarTargetNat
            End Get
            Set(ByVal value As Integer)
                mvarTargetNat = value
                If PriceListDelegate IsNot Nothing Then
                    PriceListDelegate.DynamicInvoke()
                End If
            End Set
        End Property

        Public Property TargetUni() As Integer
            Get
                Return mvarTargetUni
            End Get
            Set(ByVal value As Integer)
                mvarTargetUni = value
                If PriceListDelegate IsNot Nothing Then
                    PriceListDelegate.DynamicInvoke()
                End If
            End Set
        End Property


        Public Property Price(ByVal CPP As Boolean, Optional ByVal Daypart As Integer = -1) As Single
            Get
                If CPP Then
                    If Daypart = -1 Then
                        If bolPriceOnCPP Then
                            'if we have CPP price entered
                            Return mvarPrice(0)
                        Else
                            'if we have CPT price entered
                            Return mvarPrice(0) * mvarTargetUni / 100
                        End If
                    Else
                        If bolPriceOnCPP Then
                            'if we have CPP price entered
                            Return mvarPrice(Daypart + 1)
                        Else
                            'if we have CPT price entered
                            Return mvarPrice(Daypart + 1) * mvarTargetUni / 100
                        End If
                    End If
                Else
                    If Daypart = -1 Then
                        If bolPriceOnCPP Then
                            'if we have CPP price entered
                            Return mvarPrice(0) / mvarTargetUni * 100
                        Else
                            'if we have CPT price entered
                            Return mvarPrice(0)
                        End If
                    Else
                        If bolPriceOnCPP Then
                            'if we have CPP price entered
                            Return mvarPrice(Daypart + 1) / mvarTargetUni * 100
                        Else
                            'if we have CPT price entered
                            Return mvarPrice(Daypart + 1)
                        End If
                    End If
                End If
            End Get
            Set(ByVal value As Single)
                If value < 0 Then
                    Exit Property
                End If

                If CPP Then
                    If bolPriceOnCPP Then
                        If Daypart = -1 Then
                            mvarPrice(0) = value
                            For i As Integer = 1 To cKampanj.MAX_DAYPARTS
                                mvarPrice(i) = value
                            Next
                        Else
                            mvarPrice(Daypart + 1) = value
                        End If
                    Else
                        If Daypart = -1 Then
                            mvarPrice(0) = value / mvarTargetUni * 100
                            For i As Integer = 1 To cKampanj.MAX_DAYPARTS
                                mvarPrice(0) = value / mvarTargetUni * 100
                            Next
                        Else
                            mvarPrice(Daypart + 1) = value / mvarTargetUni * 100
                        End If
                    End If
                Else
                    If bolPriceOnCPP Then
                        If Daypart = -1 Then
                            mvarPrice(0) = value * mvarTargetUni / 100
                            For i As Integer = 1 To cKampanj.MAX_DAYPARTS
                                mvarPrice(0) = value * mvarTargetUni / 100
                            Next
                        Else
                            mvarPrice(Daypart + 1) = value * mvarTargetUni / 100
                        End If
                    Else
                        If Daypart = -1 Then
                            mvarPrice(0) = value
                            For i As Integer = 1 To cKampanj.MAX_DAYPARTS
                                mvarPrice(i) = value
                            Next
                        Else
                            mvarPrice(Daypart + 1) = value
                        End If
                    End If
                End If
                If PriceListDelegate IsNot Nothing Then
                    PriceListDelegate.DynamicInvoke()
                End If
            End Set
        End Property

        Public Sub New(ByVal MainObject As Trinity.cKampanj, ByVal ParentObject As cPricelistPeriods)
            Main = MainObject
            mvarPricelistPeriods = ParentObject
            Main.RegisterProblemDetection(Me)
        End Sub

        Public Property Name() As String
            Get
                Name = mvarName
            End Get
            Set(ByVal value As String)
                mvarName = value
            End Set
        End Property

        Public Property FromDate() As Date
            Get
                FromDate = mvarFromDate
            End Get
            Set(ByVal value As Date)
                mvarFromDate = Date.FromOADate(Math.Floor(value.ToOADate))
                If PriceListDelegate IsNot Nothing Then
                    PriceListDelegate.DynamicInvoke()
                End If

            End Set
        End Property

        Public Property ToDate() As Date
            Get
                ToDate = mvarToDate
            End Get
            Set(ByVal value As Date)
                mvarToDate = Date.FromOADate(Math.Floor(value.ToOADate))
                If PriceListDelegate IsNot Nothing Then
                    PriceListDelegate.DynamicInvoke()
                End If

            End Set
        End Property

        Public Property ID() As String
            Get
                Return mvarID
            End Get
            Set(ByVal value As String)
                mvarID = value
            End Set
        End Property

        Enum ProblemsEnum
            UniverseOutOfDate = 1
            LowPrice = 2
            DuplicatePeriod = 3
            OverlappingPeriods = 4
        End Enum

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

            If Main.StartDate = 0 Or Main.EndDate = 0 Then Return New List(Of cProblem)

            Dim _problems As New List(Of cProblem)


            Try
                Dim PricelistTarget As cPricelistTarget = TryCast(mvarPricelistPeriods.ParentObject, cPricelistTarget)

                If PricelistTarget IsNot Nothing Then
                    If Me.ToDate > Date.FromOADate(Main.StartDate) And Me.FromDate < Date.FromOADate(Main.EndDate) Then
                        'Overlapping pricelist periods
                        Dim PLPeriods As New List(Of cPricelistPeriod)

                        For Each tmpPeriod As cPricelistPeriod In PricelistTarget.PricelistPeriods
                            If tmpPeriod IsNot Me Then
                                If tmpPeriod.ToDate > Date.FromOADate(Main.StartDate) And tmpPeriod.FromDate < Date.FromOADate(Main.EndDate) Then
                                    If Me.ToDate > tmpPeriod.FromDate And Me.FromDate < tmpPeriod.ToDate Then
                                        If DirectCast(tmpPeriod.PricelistPeriods.ParentObject, cPricelistTarget).Bookingtype.BuyingTarget Is PricelistTarget Then
                                            If DirectCast(tmpPeriod.PricelistPeriods.ParentObject, cPricelistTarget).Bookingtype.BookIt Then
                                                PLPeriods.Add(tmpPeriod)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Next

                        If PLPeriods.Count > 0 Then
                            Dim _helpText As New System.Text.StringBuilder

                            _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Overlapping pricelist period</p>")
                            _helpText.AppendLine("<p>The period <p>" & PricelistTarget.Bookingtype.ToString & ": " & Me.Name & " - " & Me.FromDate.ToShortDateString & _
                                                 " - " & Me.ToDate.ToShortDateString & "<p> overlaps / is overlapped by")
                            For Each PD As cPricelistPeriod In PLPeriods

                                _helpText.AppendLine("<br>" & PricelistTarget.Bookingtype.ToString & " : " & PD.Name & " - " & vbTab & PD.FromDate.ToShortDateString & vbTab & " - " & PD.ToDate.ToShortDateString)
                            Next

                            _helpText.AppendLine("</p>")
                            _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                            _helpText.AppendLine("<p>Find this period in the price list and remove the entries you don't want, or adjust the periods</p>")

                            Dim _problem As New cProblem(ProblemsEnum.OverlappingPeriods, cProblem.ProblemSeverityEnum.Warning, "Overlapping pricelist period", DirectCast(mvarPricelistPeriods.ParentObject, cPricelistTarget).Bookingtype.ToString & " - " & DirectCast(mvarPricelistPeriods.ParentObject, cPricelistTarget).TargetName, _helpText.ToString, Me)
                            _problems.Add(_problem)
                        End If
                    End If


                    'Universe size is wrong


                    If PricelistTarget.Bookingtype.BuyingTarget Is PricelistTarget AndAlso Me.FromDate <= Date.FromOADate(Me.Main.EndDate) AndAlso Me.ToDate >= Date.FromOADate(Me.Main.StartDate) AndAlso PricelistTarget.Bookingtype.BookIt Then
                        Dim TargetUniSize As Integer = PricelistTarget.Target.UniSize
                        If mvarTargetUni <> TargetUniSize Then
                            'Debug.Print("Has: " & mvarTargetUni & " - Should be: " & TargetUniSize)
                            Dim _helpText As New System.Text.StringBuilder

                            _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Universe out of date</p>")
                            _helpText.AppendLine("<p>The universe size for target " & PricelistTarget.TargetName & _
                                                 " in the booking type " & PricelistTarget.Bookingtype.ToString & _
                                                 " is out of date for period with name " & Me.Name & " stretching between " & Me.FromDate.ToShortDateString & " - " & Me.ToDate.ToShortDateString & _
                                                 ". It should be " & TargetUniSize & " but is currently " & mvarTargetUni & "</p>")
                            _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                            _helpText.AppendLine("<p>You can click the green icon on the right to automatically fix this.</p>")

                            Dim _problem As New UniverseOutOfDateProblem(ProblemsEnum.UniverseOutOfDate, cProblem.ProblemSeverityEnum.Error, "Universe out of date", DirectCast(mvarPricelistPeriods.ParentObject, cPricelistTarget).Bookingtype.ToString & " - " & DirectCast(mvarPricelistPeriods.ParentObject, cPricelistTarget).TargetName, _helpText.ToString, Me, , True)
                            _problems.Add(_problem)
                        End If

                    End If

                    If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
                    Return _problems
                End If
            Catch ex As Exception
                Return New List(Of cProblem)
            End Try

        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound
    End Class

    Public Class UniverseOutOfDateProblem
        Inherits cProblem

        Dim PricelistPeriod As cPricelistPeriod

        Public Overrides Function FixMe() As Boolean

            Try
                Dim PricelistTarget As cPricelistTarget = DirectCast(PricelistPeriod.PricelistPeriods.ParentObject, cPricelistTarget)

                PricelistPeriod.TargetNat = PricelistTarget.Target.UniSize
                PricelistPeriod.TargetUni = PricelistTarget.Target.UniSize '
                Return True
            Catch ex As Exception
                Return False
            End Try     

        End Function

        Public Sub New(ByVal ProblemID As Integer, ByVal Severity As ProblemSeverityEnum, ByVal Message As String, ByVal Source As String, ByVal HelpText As String, ByVal SourceObject As IDetectsProblems, Optional ByVal SourceString As String = "", Optional ByVal AutoFixable As Boolean = False)
            MyBase.New(ProblemID, Severity, Message, Source, HelpText, SourceObject, , AutoFixable)
            PricelistPeriod = DirectCast(SourceObject, cPricelistPeriod)
        End Sub

    End Class


End Namespace
