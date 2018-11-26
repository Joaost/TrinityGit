Public Class Break
    Property Channel As String
    Property [Date] As Date

    Private _time As String
    Property Time As String
        Get
            Return _time
        End Get
        Set(value As String)
            _time = value
            If MaM() < 6 * 60 Then
                Dim _h As Integer 
                Dim _m As Integer
                If _time.Length <= 4 
                    _h = _time.Substring(0, 2)
                    _m = _time.Substring(2, 2)
                Else 
                    _h = _time.Substring(0, 2)
                    _m = _time.Substring(3, 2)
                End If
                
                _h += 24
                _time = IIf(_h < 10, "0" & _h, _h) & IIf(_m < 10, "0" & _m, _m)
            End If
        End Set
    End Property

    Property Programme As String = ""
    Property ChanEst As Single
    Property Price As Long
    Property IsLocal As Boolean = False
    Property IsRB As Boolean = False
    Property Comment As String = ""
    Property ID As String

    Property Addition As Integer = 0
    Property EstimationTarget As String
    Property UseCPP As Boolean = False
    Property Area As String = "SE"

    Function MaM() As Integer
        Dim hours As Integer = 0
        Dim minutes As Integer = 0

        hours = CType(Strings.Left(Time, 2), Integer)
        minutes = CType(Strings.Right(Time, 2), Integer)

        Return hours * 60 + minutes
    End Function

    Sub SetMaM(Mam As Integer)
        Dim _h As Integer = Mam \ 60
        Dim _m As Integer = Mam Mod 60
        _time = IIf(_h < 10, "0" & _h, _h) & IIf(_m < 10, "0" & _m, _m)
    End Sub

    Function Duration() As Integer
        If _nextBreak Is Nothing Then Return 0
        Return _nextBreak.MaM() - MaM()
    End Function

    Private _prevBreak As Break
    Public _nextBreak As Break
    Public Sub New(PreviousBreak As Break)
        _prevBreak = PreviousBreak
        ID = Guid.NewGuid.ToString
    End Sub

End Class
