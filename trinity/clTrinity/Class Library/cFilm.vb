Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cFilm
        Implements IDetectsProblems

        Private mvarFilmcode As String = ""
        Private mvarName As String = ""
        Private mvarFilmLength As Byte
        Private mvarIndex As Single
        Private mvarGrossIndex As Single
        Private mvarShareTRP As Decimal
        Private mvarShareBudget As Decimal
        Private mvarDescription As String = ""
        Private mvarIsVisible As Boolean

        Private Main As cKampanj
        Private mvarBookingtype As cBookingType
        Private ParentColl As Collection
        Private mvarWeek As Trinity.cWeek

        Public Enum FilmShareEnum
            fseTRP = 0
            fseBudget = 1
        End Enum

        Public Event FilmChanged(film As cFilm)
        Public Event Film()

        Friend WriteOnly Property ParentCollection() As Collection
            '---------------------------------------------------------------------------------------
            ' Procedure : ParentCollection
            ' DateTime  : 2003-07-07 13:18
            ' Author    : joho
            ' Purpose   : Sets the Collection of wich this film is a member. This is used
            '             when a new Filmcode is set. See that property for further explanation
            '---------------------------------------------------------------------------------------
            '
            Set(ByVal value As Collection)
                ParentColl = value
            End Set

        End Property

        Friend WriteOnly Property Bookingtype() As cBookingType
            Set(ByVal value As cBookingType)
                mvarBookingtype = value
            End Set
        End Property

        Public Overrides Function tostring() As String
            Return Me.Name
        End Function
        Friend WriteOnly Property MainObject() As cKampanj
            Set(ByVal value As cKampanj)
                Main = value
            End Set
        End Property

        Public Property Filmcode() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : Filmcode
            ' DateTime  : 2003-07-13 17:27
            ' Author    : joho
            ' Purpose   : Returns/sets the filmcode for this film
            '---------------------------------------------------------------------------------------
            Get
                On Error GoTo Filmcode_Error

                Filmcode = mvarFilmcode

                On Error GoTo 0
                Exit Property

Filmcode_Error:

                Err.Raise(Err.Number, "cFilm: Filmcode", Err.Description)
            End Get
            Set(ByVal value As String)

                On Error GoTo Filmcode_Error

                mvarFilmcode = value
                RaiseEvent FilmChanged(Me)

                On Error GoTo 0
                Exit Property

Filmcode_Error:

                Err.Raise(Err.Number, "cFilm: Filmcode", Err.Description)

            End Set
        End Property

        Public Property FilmLength() As Byte
            '---------------------------------------------------------------------------------------
            ' Procedure : FilmLength
            ' DateTime  : 2003-07-13 17:27
            ' Author    : joho
            ' Purpose   : Returns/sets the length in seconds of this film
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo FilmLength_Error

                FilmLength = mvarFilmLength

                On Error GoTo 0
                Exit Property

FilmLength_Error:

                Err.Raise(Err.Number, "cFilm: FilmLength", Err.Description)
            End Get
            Set(ByVal value As Byte)
                On Error GoTo FilmLength_Error

                mvarFilmLength = value
                RaiseEvent FilmChanged(Me)

                On Error GoTo 0
                Exit Property

FilmLength_Error:

                Err.Raise(Err.Number, "cFilm: FilmLength", Err.Description)

            End Set
        End Property

        Public Property GrossIndex() As Single
            Get
                If mvarWeek Is mvarWeek.Bookingtype.Weeks(1) Then
                    Return mvarGrossIndex
                Else
                    Return mvarWeek.Bookingtype.Weeks(1).Films(Name).GrossIndex
                End If
            End Get
            Set(ByVal value As Single)
                If mvarWeek Is mvarWeek.Bookingtype.Weeks(1) Then
                    mvarGrossIndex = value
                    RaiseEvent FilmChanged(Me)
                ElseIf mvarWeek.Bookingtype.Weeks(1).Films(Name).GrossIndex <> value Then
                    mvarWeek.Bookingtype.Weeks(1).Films(Name).GrossIndex = value
                End If
            End Set
        End Property

        Public Property Index() As Single
            '---------------------------------------------------------------------------------------
            ' Procedure : Index
            ' DateTime  : 2003-07-13 17:27
            ' Author    : joho
            ' Purpose   : Returns/sets the filmlength index for this film
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Index_Error

                If mvarWeek Is mvarWeek.Bookingtype.Weeks(1) Then
                    Return mvarIndex
                Else
                    Return mvarWeek.Bookingtype.Weeks(1).Films(Name).Index
                End If
                On Error GoTo 0
                Exit Property

Index_Error:

                Err.Raise(Err.Number, "cFilm: Index", Err.Description)
            End Get
            Set(ByVal value As Single)
                On Error GoTo Index_Error

                If mvarWeek Is mvarWeek.Bookingtype.Weeks(1) Then
                    mvarIndex = value
                    If mvarGrossIndex = 0 Then
                        mvarGrossIndex = value
                    End If
                    mvarBookingtype.InvalidateTotalTRP()
                    RaiseEvent FilmChanged(Me)
                Else
                    If mvarWeek.Bookingtype.Weeks(1).Films(Name).Index <> value Then mvarWeek.Bookingtype.Weeks(1).Films(Name).Index = value
                End If


                On Error GoTo 0
                Exit Property

Index_Error:

                Err.Raise(Err.Number, "cFilm: Index", Err.Description)

            End Set
        End Property

        Public Property Share(Optional ByVal Type As FilmShareEnum = FilmShareEnum.fseTRP) As Decimal
            '---------------------------------------------------------------------------------------
            ' Procedure : Share
            ' DateTime  : 2003-07-13 17:27
            ' Author    : joho
            ' Purpose   : Returns/sets the planned share of contacts for this film in percent
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Share_Error

                If Type = FilmShareEnum.fseTRP Then
                    Return mvarShareTRP
                Else
                    Return mvarShareBudget
                End If

                On Error GoTo 0
                Exit Property

Share_Error:

                Err.Raise(Err.Number, "cFilm: Share", Err.Description)
            End Get
            Set(ByVal value As Decimal)
                On Error GoTo Share_Error

                If Type = FilmShareEnum.fseTRP Then
                    mvarShareTRP = value
                    Dim TotIdx As Decimal = mvarShareTRP * (Index / 100)
                    For Each TmpFilm As Trinity.cFilm In mvarWeek.Films
                        If Not TmpFilm Is Me Then
                            TotIdx += TmpFilm.Share * (TmpFilm.Index / 100)
                        End If
                    Next
                    For Each TmpFilm As Trinity.cFilm In mvarWeek.Films
                        If TotIdx > 0 Then
                            'mvarShareBudget = ((mvarShareTRP * (Index / 100)) / TotIdx) * 100
                            TmpFilm.SetShare(((TmpFilm.Share * (TmpFilm.Index / 100)) / TotIdx) * 100, FilmShareEnum.fseBudget)
                        Else
                            TmpFilm.SetShare(0, FilmShareEnum.fseBudget)
                        End If
                    Next
                Else
                    mvarShareBudget = value
                    Dim TotIdx As Decimal = value / Index
                    For Each TmpFilm As Trinity.cFilm In mvarWeek.Films
                        If Not TmpFilm Is Me Then
                            TotIdx += TmpFilm.Share(FilmShareEnum.fseBudget) / TmpFilm.Index
                        End If
                    Next
                    'mvarShareTRP = (1 - (Index / value) / TotIdx) * 100
                    For Each TmpFilm As Trinity.cFilm In mvarWeek.Films
                        If TotIdx > 0 Then
                            TmpFilm.SetShare(((TmpFilm.Share(FilmShareEnum.fseBudget) / TmpFilm.Index) / TotIdx) * 100, FilmShareEnum.fseTRP)
                        Else
                            TmpFilm.SetShare(0, FilmShareEnum.fseTRP)
                        End If
                    Next
                End If
                mvarBookingtype.InvalidateTotalTRP()
                RaiseEvent FilmChanged(Me)

                On Error GoTo 0
                Exit Property

Share_Error:

                Err.Raise(Err.Number, "cFilm: Share", Err.Description)

            End Set
        End Property

        Friend Sub SetShare(ByVal Share As Decimal, ByVal Type As FilmShareEnum)
            If Type = FilmShareEnum.fseBudget Then
                mvarShareBudget = Share
            Else
                mvarShareTRP = Share
            End If
            RaiseEvent FilmChanged(Me)
        End Sub

        Public Property Description() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : Description
            ' DateTime  : 2003-07-13 17:27
            ' Author    : joho
            ' Purpose   : Returns/sets a description for this film
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Description_Error

                Description = mvarDescription

                On Error GoTo 0
                Exit Property

Description_Error:

                Err.Raise(Err.Number, "cFilm: Description", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo Description_Error

                mvarDescription = value
                RaiseEvent FilmChanged(Me)

                On Error GoTo 0
                Exit Property

Description_Error:

                Err.Raise(Err.Number, "cFilm: Description", Err.Description)

            End Set
        End Property

        Public Property IsVisible() As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : IsVisible
            ' DateTime  : 2003-07-13 17:28
            ' Author    : joho
            ' Purpose   : Returns/sets wether this filmcode should be visible in the charts
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IsVisible_Error

                IsVisible = mvarIsVisible

                On Error GoTo 0
                Exit Property

IsVisible_Error:

                Err.Raise(Err.Number, "cFilm: IsVisible", Err.Description)
            End Get
            Set(ByVal value As Boolean)
                On Error GoTo IsVisible_Error

                mvarIsVisible = value
                RaiseEvent FilmChanged(Me)

                On Error GoTo 0
                Exit Property

IsVisible_Error:

                Err.Raise(Err.Number, "cFilm: IsVisible", Err.Description)


            End Set
        End Property

        Public Property Name() As String
            Get
                Name = mvarName
            End Get
            Set(ByVal value As String)
                Dim TmpFilm As cFilm

                If value <> mvarName Then

                    If (Not ParentColl Is Nothing) AndAlso ParentColl.Contains(mvarName) Then
                        TmpFilm = ParentColl(mvarName)
                        If ParentColl.Contains(value) Then
                            Err.Raise(vbObjectError + 600, "cFilm.Name", "Two films can not share name.")
                        End If
                        ParentColl.Remove(mvarName)
                        ParentColl.Add(TmpFilm, value)
                    End If
                End If
                mvarName = value
                RaiseEvent FilmChanged(Me)
            End Set
        End Property

        Public Function FilmString() As String
            Dim Tmpstr As String
            Dim TmpChan As cChannel
            Dim TmpBT As cBookingType
            Dim TmpWeek As cWeek

            Tmpstr = ""
            For Each TmpChan In Main.Channels
                For Each TmpBT In TmpChan.BookingTypes
                    For Each TmpWeek In TmpBT.Weeks
                        If TmpBT.BookIt Then
                            If Not TmpWeek.Films(mvarName) Is Nothing Then
                                If InStr(Tmpstr, TmpWeek.Films(mvarName).Filmcode & ",") = 0 Then
                                    If TmpWeek.Films(mvarName).Filmcode <> "" Then
                                        Tmpstr = Tmpstr + TmpWeek.Films(mvarName).Filmcode & ","
                                    End If
                                End If
                            End If
                        End If
                    Next
                Next
            Next



            If Len(Tmpstr) = 0 Then
                FilmString = ""
            Else
                FilmString = Left(Tmpstr, Len(Tmpstr) - 1)
            End If
        End Function

        Public Sub New(ByVal MainObject As cKampanj, ByVal Week As Trinity.cWeek)
            Main = MainObject
            mvarWeek = Week
            If mvarWeek Is Nothing Then
                Throw New Exception("This film does not belong to a week. Tell the developers about this. " & Me.ToString)
            End If
            mvarIsVisible = True
            Main.RegisterProblemDetection(Me)
        End Sub

        Private _adTooxStatus As Trinity.cAdTooxStatus
        Private _adTooxStatusRead As Boolean = False
        Public ReadOnly Property AdTooxStatus() As Trinity.cAdTooxStatus
            Get
                If Not _adTooxStatusRead Then
                    Try
                        _adTooxStatus = Main.GetAdTooxStatusForChannel(mvarBookingtype.ParentChannel, mvarFilmcode)
                    Catch
                        _adTooxStatus = Nothing
                    End Try
                    _adTooxStatusRead = True
                End If
                Return _adTooxStatus
            End Get
        End Property

        Friend Sub InvalidateAdtooxStatus()
            _adTooxStatusRead = False
        End Sub

        Public Enum ProblemsEnum
            NoIndex = 1
            NoGrossIndex = 2
            NoFilmcode = 3
            AdtooxError = 4
        End Enum

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems
            Dim _problems As New List(Of cProblem)
            If Not mvarBookingtype.BookIt Then Return _problems

            If Index = 0 Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Film without index</p>")
                _helpText.AppendLine("<p>The film '" & Name & "' does not have an index in '" & mvarBookingtype.ToString & "'</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Setup'-window, choose the 'Films'-tab and select the film '" & Name & "'. In the lower right pane, check the film index.</p>")
                Dim _problem As New cProblem(ProblemsEnum.NoIndex, cProblem.ProblemSeverityEnum.Warning, "Film without index", Name, _helpText.ToString, Me)

                _problems.Add(_problem)
            End If
            If GrossIndex = 0 Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Film without gross index</p>")
                _helpText.AppendLine("<p>The film '" & Name & "' does not have a gross index in '" & mvarBookingtype.ToString & "'</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Setup'-window, choose the 'Films'-tab and select the film '" & Name & "'. In the lower right pane, check the gross index.</p>")
                Dim _problem As New cProblem(ProblemsEnum.NoGrossIndex, cProblem.ProblemSeverityEnum.Warning, "Film without gross index", Name, _helpText.ToString, Me)

                _problems.Add(_problem)
            End If
            If Filmcode = "" Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Film without filmcode</p>")
                _helpText.AppendLine("<p>The film '" & Name & "' does not have a filmcode in '" & mvarBookingtype.ToString & "'</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Setup'-window, choose the 'Films'-tab and select the film '" & Name & "'. In the lower right pane, enter a filmcode on '" & mvarBookingtype.ToString & "'.</p>")
                Dim _problem As New cProblem(ProblemsEnum.NoFilmcode, cProblem.ProblemSeverityEnum.Warning, "Film without filmcode", Name, _helpText.ToString, Me)

                _problems.Add(_problem)
            End If

            Return _problems
        End Function

        Public Sub Clone(Film As cFilm)
            mvarDescription = Film.Description
            mvarFilmLength = Film.FilmLength
            mvarIndex = Film.Index
            mvarGrossIndex = Film.Index
            mvarFilmcode = Film.Filmcode
            mvarIsVisible = True
            SetShare(0, FilmShareEnum.fseTRP)
        End Sub

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound

        Public Sub DetectAdTooxProblems()
            Dim _problems As New List(Of cProblem)
            If TrinitySettings.AdtooxEnabled AndAlso mvarBookingtype.BookIt Then
                Try
                    If mvarFilmcode <> "" Then
                        Main.GetAdTooxStatusForChannel(mvarBookingtype.ParentChannel, mvarFilmcode)
                    End If
                Catch ex As Exception
                    Dim _helpText As New Text.StringBuilder

                    _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>A film caused an error in AdToox</p>")
                    _helpText.AppendLine("<p>The film '" & Name & "' caused the following error in AdToox:</p>")
                    _helpText.AppendLine("<p>" & ex.Message & "</p>")
                    _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                    _helpText.AppendLine("<p>This film either does not exist in the Adtoox database, or you do not have permission to see its status." & _
                                         "You have entered " & TrinitySettings.AdtooxUsername & " as your Adtoox username.</p>")
                    Dim _problem As New cProblem(ProblemsEnum.AdtooxError, cProblem.ProblemSeverityEnum.Message, "A film caused an error in AdToox", Name, _helpText.ToString, Me, mvarFilmcode)
                    If Not _problems.Contains(_problem) Then _problems.Add(_problem)
                End Try
            End If

            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
        End Sub

    End Class

End Namespace