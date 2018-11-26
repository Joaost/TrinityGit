Public Class Helper
    Shared Function Time2Mam(ByVal Time As String) As Integer
        Dim h As Integer
        Dim m As Integer

        If Time.Length = 5 Then
            h = Left(Time, 2)
            m = Right(Time, 2)
        ElseIf Time.Length < 3 Then
            h = Time
            m = 0
        ElseIf Time.IndexOfAny(".:") > 0 Then
            h = Left(Time, Time.IndexOfAny(".:"))
            m = Mid(Time, Time.IndexOfAny(".:") + 2)
        Else
            Try
                h = Left(Time, Time.Length - 2)
                m = Right(Time, 2)
            Catch
                Return 0
            End Try
        End If
        Return h * 60 + m
    End Function

    Shared Function Mam2Time(ByVal MaM As Integer) As String
        Dim h As Integer
        Dim m As Integer
        h = MaM \ 60
        m = MaM Mod 60
        Return Format(h, "00") & ":" & Format(m, "00")
    End Function

    Shared Function CreateVerificationCode() As String
        Randomize()
        Dim TmpCode As String = ""
        For i As Integer = 1 To 7
            Dim TmpChar As String = ChrW(92)
            While AscW(TmpChar) > 90 AndAlso AscW(TmpChar) < 97
                TmpChar = ChrW(Int(Rnd() * 57) + 65)
            End While
            TmpCode &= TmpChar
        Next
        Return TmpCode
    End Function

End Class
