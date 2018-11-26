Namespace TrinityViewer
    Public Class cFilmInfo

        Private _name As String
        Private _filmcode As String
        Public Share As Single
        Public Index As Decimal
        Private ParentColl As Collection
        Private _week As cWeekInfo

        Public Function BudgetShare() As Single
            Dim TotIdx As Decimal = Share * (Index / 100)
            For Each TmpFilm As cFilmInfo In _week.Films
                If Not TmpFilm Is Me Then
                    TotIdx += TmpFilm.Share * (TmpFilm.Index / 100)
                End If
            Next
            If TotIdx > 0 Then
                Return ((Share * (Index / 100)) / TotIdx) * 100
            Else
                Return 0
            End If
        End Function '

        Public Property Name() As String
            Get
                Name = _name
            End Get
            Set(ByVal value As String)
                Dim TmpFilm As cFilmInfo

                If value <> _name Then

                    If (Not ParentColl Is Nothing) AndAlso _name IsNot Nothing AndAlso ParentColl.Contains(_name) Then
                        TmpFilm = ParentColl(_name)
                        If ParentColl.Contains(value) Then
                            Err.Raise(vbObjectError + 600, "cFilm.Name", "Two films can not share name.")
                        End If
                        ParentColl.Remove(_name)
                        ParentColl.Add(TmpFilm, value)
                    End If
                End If
                _name = value
            End Set
        End Property

        Public Sub New(ByVal Parent As Collection, ByVal week As cWeekInfo)
            ParentColl = Parent
            _week = week
        End Sub

        Public Property Filmcode() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : Filmcode
            ' DateTime  : 2003-07-13 17:27
            ' Author    : joho
            ' Purpose   : Returns/sets the filmcode for this film
            '---------------------------------------------------------------------------------------
            Get
                On Error GoTo Filmcode_Error

                Filmcode = _filmcode

                On Error GoTo 0
                Exit Property

Filmcode_Error:

                Err.Raise(Err.Number, "cFilm: Filmcode", Err.Description)
            End Get
            Set(ByVal value As String)

                On Error GoTo Filmcode_Error

                _filmcode = value

                On Error GoTo 0
                Exit Property

Filmcode_Error:

                Err.Raise(Err.Number, "cFilm: Filmcode", Err.Description)

            End Set
        End Property

    End Class
End Namespace