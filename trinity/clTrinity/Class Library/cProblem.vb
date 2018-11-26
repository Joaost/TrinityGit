Namespace Trinity
    Public Class cProblem
        Enum ProblemSeverityEnum
            Message
            Warning
            [Error]
        End Enum

        Friend _severity As ProblemSeverityEnum
        Public Property Severity() As ProblemSeverityEnum
            Get
                Return _severity
            End Get
            Friend Set(ByVal value As ProblemSeverityEnum)
                _severity = value
            End Set
        End Property

        Friend _autofix As Boolean
        Public Property AutoFix() As Boolean
            Get
                Return _autofix
            End Get
            Set(ByVal value As Boolean)
                _autofix = value
            End Set
        End Property

        Friend _autofixable As Boolean
        Public Property AutoFixable() As Boolean
            Get
                Return _autofixable
            End Get
            Set(ByVal value As Boolean)
                _autofixable = value
            End Set
        End Property

        Friend _source As String
        Public Property Source() As String
            Get
                Return _source
            End Get
            Set(ByVal value As String)
                _source = value
            End Set
        End Property

        Friend _message As String
        Public Property Message() As String
            Get
                Return _message
            End Get
            Friend Set(ByVal value As String)
                _message = value
            End Set
        End Property

        Friend _helpText As String
        Public Property HelpText() As String
            Get
                Return _helpText
            End Get
            Set(ByVal value As String)
                _helpText = value
            End Set
        End Property

        Friend _sourceObject As IDetectsProblems
        Public Property SourceObject() As IDetectsProblems
            Get
                Return _sourceObject
            End Get
            Set(ByVal value As IDetectsProblems)
                _sourceObject = value
            End Set
        End Property

        Friend _problemID As Integer
        Public Property ProblemID() As Integer
            Get
                Return _problemID
            End Get
            Set(ByVal value As Integer)
                _problemID = value
            End Set
        End Property

        Friend _sourceString As String
        Public Property SourceString() As String
            Get
                Return _sourceString
            End Get
            Set(ByVal value As String)
                _sourceString = value
            End Set
        End Property

        Public ReadOnly Property IsVisible() As Boolean
            Get
                If Severity = ProblemSeverityEnum.Error Then
                    'Errors can never be hidden
                    Return True
                End If
                Return TrinitySettings.DisplayProblem(Me.SourceObject.GetType.Name, ProblemID)
            End Get
        End Property

        Public Sub HideAllProblemsOfThisType()
            TrinitySettings.DisplayProblem(Me.SourceObject.GetType.Name, ProblemID) = False
        End Sub

        Public Sub ShowAllProblemsOfThisType()
            TrinitySettings.DisplayProblem(Me.SourceObject.GetType.Name, ProblemID) = True
        End Sub

        Public Sub FixAllProblemsOfThisType()
            TrinitySettings.FixProblem(Me.SourceObject.GetType.Name, ProblemID) = True
            TrinitySettings.DisplayProblem(Me.SourceObject.GetType.Name, ProblemID) = False
        End Sub

        Public Sub DontFixAllProblemsOfThisType()
            TrinitySettings.FixProblem(Me.SourceObject.GetType.Name, ProblemID) = False
            TrinitySettings.DisplayProblem(Me.SourceObject.GetType.Name, ProblemID) = True
        End Sub

        Public Sub New(ByVal ProblemID As Integer, ByVal Severity As ProblemSeverityEnum, ByVal Message As String, ByVal Source As String, ByVal HelpText As String, ByVal SourceObject As IDetectsProblems, Optional ByVal SourceString As String = "", Optional ByVal AutoFixable As Boolean = False, Optional ByVal AutoFix As Boolean = False)
            _message = Message
            _source = Source
            _sourceObject = SourceObject
            _severity = Severity
            _helpText = HelpText
            _sourceString = SourceString
            _problemID = ProblemID
            _autofixable = AutoFixable
            _autofix = AutoFix
        End Sub

        Public Overridable Function FixMe() As Boolean
            Return False
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj.GetType Is GetType(cProblem) OrElse obj.GetType.BaseType Is GetType(cProblem) Then
                If DirectCast(obj, cProblem).ProblemID = ProblemID AndAlso ((SourceString = "" AndAlso DirectCast(obj, cProblem).SourceObject Is SourceObject) OrElse (SourceString <> "" AndAlso SourceString = DirectCast(obj, cProblem).SourceString)) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return MyBase.Equals(obj)
            End If
        End Function

    End Class
End Namespace