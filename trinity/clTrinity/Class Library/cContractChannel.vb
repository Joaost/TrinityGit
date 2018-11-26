
Namespace Trinity
    Public Class cContractChannel
        Implements IDetectsProblems

        Private mvarChannelName As String
        Private mvarShortname As String
        Private ParentColl As Collection
        Private collContractLevel As New Collection
        Private mvarBuyingUniverse As String
        Private intActiveLevel As Integer = 1
        Private mvarListNumber As Integer
        Private mvarCommission As Single
        Private mvarStandardFilmIndex(500) As Single
        Friend MainObject As cKampanj

        Public Property Agencycommission() As Single
            Get
                Return mvarCommission
            End Get
            Set(ByVal value As Single)
                mvarCommission = value
            End Set
        End Property

        Public Property StandardFilmIndex(ByVal Length As Integer) As Single
            Get
                Try
                    StandardFilmIndex = mvarStandardFilmIndex(Length)
                Catch

                End Try
            End Get
            Set(ByVal value As Single)
                mvarStandardFilmIndex(Length) = value
            End Set
        End Property

        Public Property ListNumber() As Integer
            Get
                On Error GoTo ListNumber_Error

                ListNumber = mvarListNumber

                On Error GoTo 0
                Exit Property

ListNumber_Error:

                Err.Raise(Err.Number, "cChannel: ListNumber", Err.Description)

            End Get
            Set(ByVal value As Integer)
                On Error GoTo ListNumber_Error

                mvarListNumber = value

                On Error GoTo 0
                Exit Property

ListNumber_Error:

                Err.Raise(Err.Number, "cChannel: ListNumber", Err.Description)

            End Set
        End Property

        'Public ReadOnly Property BookingTypes(ByVal key As String) As cContractBookingtypes
        '    Get
        '        BookingTypes = collContractLevel(intActiveLevel)(key)
        '    End Get
        'End Property

        Public ReadOnly Property BookingTypesCount(ByVal Level As Integer) As Integer
            Get
                Return collContractLevel(Level).count
            End Get
        End Property

        'Hannes changed this from collContractLevel.Remove(index + 1)
        Public Sub RemoveLevel(ByVal index As Integer)
            collContractLevel.Remove(index + 1)
        End Sub

        Public Sub AddEmptyLevel()
            Dim BTs As New Trinity.cContractBookingtypes(MainObject, Me)
            BTs.ParentChannel = Me
            collContractLevel.Add(BTs, collContractLevel.Count + 1)
        End Sub

        Public Sub AddLevel()
            Dim BTs As New Trinity.cContractBookingtypes(MainObject, Me)
            Dim cbt As Trinity.cContractBookingtype

            If collContractLevel.Count = 0 Then
                'the first level is active by default
                For Each TmpBT As Trinity.cBookingType In MainObject.Channels(Me.ChannelName).BookingTypes
                    cbt = BTs.Add(TmpBT.Name)
                    cbt.ShortName = TmpBT.Shortname
                    cbt.IsRBS = TmpBT.IsRBS
                    cbt.IsSpecific = TmpBT.IsSpecific
                    cbt.PrintDayparts = TmpBT.PrintDayparts
                    cbt.PrintBookingCode = TmpBT.PrintBookingCode
                    cbt.Active = True
                    cbt.ParentChannel = Me
                    cbt.AddedValues = New Trinity.cAddedValues(MainObject)
                    cbt.Indexes = New Trinity.cIndexes(Campaign, cbt)
                    For i As Integer = 0 To 500
                        If TmpBT.FilmIndex(i) > 0 Then
                            cbt.FilmIndex(i) = TmpBT.FilmIndex(i)
                        End If
                    Next
                Next
            Else
                For Each TmpBT As Trinity.cContractBookingtype In BookingTypes(LevelCount)
                    cbt = BTs.Add(TmpBT.Name)
                    cbt.Active = False
                    cbt.ParentChannel = Me
                    cbt.ShortName = TmpBT.ShortName
                    cbt.IsRBS = TmpBT.IsRBS
                    cbt.IsSpecific = TmpBT.IsSpecific
                    cbt.PrintDayparts = TmpBT.PrintDayparts
                    cbt.PrintBookingCode = TmpBT.PrintBookingCode

                    For Each TmpTarget As Trinity.cContractTarget In TmpBT.ContractTargets
                        Dim Target As Trinity.cContractTarget = cbt.ContractTargets.Add(TmpTarget.TargetName)
                        Target.IsEntered = TmpTarget.IsEntered
                        Target.EnteredValue = TmpTarget.EnteredValue
                        Target.AdEdgeTargetName = TmpTarget.AdEdgeTargetName
                        Target.CalcCPP = TmpTarget.CalcCPP

                        For i As Integer = 0 To MainObject.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Dayparts.Count
                            Target.DefaultDaypart(i) = TmpTarget.DefaultDaypart(i)
                        Next
                    Next

                    'copy AV from the level before
                    For Each TmpAv As Trinity.cAddedValue In TmpBT.AddedValues
                        With cbt
                            With .AddedValues.Add(TmpAv.Name)
                                .IndexGross = TmpAv.IndexGross
                                .IndexNet = TmpAv.IndexNet
                                .ShowIn = TmpAv.ShowIn
                            End With
                        End With
                    Next

                    'copy indexes from the level before
                    If cbt.Indexes Is Nothing Then
                        cbt.Indexes = New Trinity.cIndexes(Campaign, cbt)
                    End If
                    For Each TmpIndex As Trinity.cIndex In TmpBT.Indexes
                        With cbt
                            With .Indexes.Add(TmpIndex.Name)
                                .FromDate = TmpIndex.FromDate
                                .ToDate = TmpIndex.ToDate
                                .IndexOn = TmpIndex.IndexOn
                                If TmpIndex.Enhancements.Count > 0 Then
                                    For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                                        With .Enhancements.Add()
                                            .Name = TmpEnh.Name
                                            .Amount = TmpEnh.Amount
                                        End With
                                    Next
                                    '.Enhancements.SpecificFactor = TmpIndex.Enhancements.SpecificFactor
                                Else
                                    .Index = TmpIndex.Index
                                End If
                            End With
                        End With
                    Next

                    'cbt.AddedValues = New Trinity.cAddedValues
                    'cbt.Indexes = New Trinity.cIndexes(Campaign, cbt)
                    For i As Integer = 0 To 500
                        If TmpBT.FilmIndex(i) > 0 Then
                            cbt.FilmIndex(i) = TmpBT.FilmIndex(i)
                        End If
                    Next
                Next
            End If

            collContractLevel.Add(BTs, collContractLevel.Count + 1)

        End Sub

        Public Function AddBookingType(ByVal Level As Integer, ByVal Name As String) As Trinity.cContractBookingtype
            Return DirectCast(collContractLevel(Level), Trinity.cContractBookingtypes).Add(Name)
        End Function


        Public ReadOnly Property LevelCount() As Integer
            Get
                Return collContractLevel.Count
            End Get
        End Property

        Public Property BookingTypes(ByVal Level As Integer) As cContractBookingtypes
            Get
                Try
                    BookingTypes = collContractLevel(Level)
                Catch ex As Exception
                    Windows.Forms.MessageBox.Show("Tried to remove a level that didn't exist")
                End Try
            End Get
            Set(ByVal value As cContractBookingtypes)
                Dim Newcoll As New Collection
                For i As Integer = 1 To collContractLevel.Count
                    If i = Level Then
                        Newcoll.Add(value)
                    Else
                        Newcoll.Add(collContractLevel(i))
                    End If
                Next

                collContractLevel = Newcoll
            End Set
        End Property

        'Public ReadOnly Property BookingTypes(ByVal key As String, ByVal Level As Integer) As cContractBookingtype
        '    Get
        '        BookingTypes = collContractLevel(Level)(key)
        '    End Get
        'End Property

        Public Property ActiveContractLevel() As Integer
            'Contains the level on the contracts step
            'is 0 by default
            Get
                ActiveContractLevel = intActiveLevel
            End Get
            Set(ByVal value As Integer)
                If collContractLevel.Count < value Then
                    MsgBox("The contract level does not exist in the loaded contract", MsgBoxStyle.Critical, "Error in contract")
                    Exit Property
                End If
                intActiveLevel = value
            End Set
        End Property

        Public Property ChannelName() As String
            Get
                ChannelName = mvarChannelName
            End Get
            Set(ByVal value As String)
                Dim TmpChannel As cContractChannel

                If value <> mvarChannelName And mvarChannelName <> "" Then

                    TmpChannel = ParentColl(mvarChannelName)
                    ParentColl.Add(TmpChannel, value)
                    ParentColl.Remove(mvarChannelName)

                End If
                mvarChannelName = value

            End Set
        End Property

        Public Property Shortname() As String

            Get
                Shortname = mvarShortname

            End Get
            Set(ByVal value As String)
                mvarShortname = value

            End Set
        End Property

        Friend WriteOnly Property ParentCollection() As Collection
            ' Purpose   : Sets the Collection of wich this channel is a member. This is used
            '             when a new ChannelName is set. See that property for further explanation
            Set(ByVal value As Collection)
                ParentColl = value
            End Set

        End Property

        Public Overrides Function ToString() As String
            Return mvarChannelName
        End Function

        Public Sub New(ByVal Main As cKampanj)
            MainObject = Main
            MainObject.RegisterProblemDetection(Me)
        End Sub



        Public Enum ProblemsEnum
            OrphanLevel = 1

        End Enum

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

            Dim _problems As New List(Of cProblem)

            'If Me.LevelCount <> Me.collContractLevel.Count Then
            '    Dim _helpText As New System.Text.StringBuilder

            '    _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Orphan contract level</p>")
            '    _helpText.AppendLine("<p>The problem is caused by...</p>")
            '    _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
            '    _helpText.AppendLine("<p>Solve the problem by...</p>")
            '    Dim _problem As New cProblem(ProblemsEnum.OrphanLevel, cProblem.ProblemSeverityEnum.Warning, "Orphan contract level", Me.ToString, _helpText.ToString, Me)

            '    _problems.Add(_problem)
            'End If

            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems
        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound




    End Class

End Namespace
