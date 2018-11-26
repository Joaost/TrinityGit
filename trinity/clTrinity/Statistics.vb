Namespace statistic

    Public Class Statistics

        Private list As Double()

        Public Sub New(ByVal ParamArray list As Double())
            Me.list = list
        End Sub

        Public Sub update(ByVal ParamArray list As Double())
            Me.list = list
        End Sub

        Public Function mode() As Double
            Try
                Dim i As Double() = New Double(list.Length - 1) {}
                list.CopyTo(i, 0)
                sort(i)
                Dim val_mode As Double = i(0), help_val_mode As Double = i(0)
                Dim old_counter As Integer = 0, new_counter As Integer = 0
                Dim j As Integer = 0
                While j <= i.Length - 1
                    If i(j) = help_val_mode Then
                        new_counter += 1
                    ElseIf new_counter > old_counter Then
                        old_counter = new_counter
                        new_counter = 1
                        help_val_mode = i(j)
                        val_mode = i(j - 1)
                    ElseIf new_counter = old_counter Then
                        val_mode = Double.NaN
                        help_val_mode = i(j)
                        new_counter = 1
                    Else
                        help_val_mode = i(j)
                        new_counter = 1
                    End If
                    j += 1
                End While
                If new_counter > old_counter Then
                    val_mode = i(j - 1)
                ElseIf new_counter = old_counter Then
                    val_mode = Double.NaN
                End If
                Return val_mode
            Catch generatedExceptionName As Exception
                Return Double.NaN
            End Try
        End Function

        Public Function length() As Integer
            Return list.Length
        End Function

        Public Function min() As Double
            Dim minimum As Double = Double.PositiveInfinity
            For i As Integer = 0 To list.Length - 1
                If list(i) < minimum Then
                    minimum = list(i)
                End If
            Next
            Return minimum
        End Function

        Public Function max() As Double
            Dim maximum As Double = Double.NegativeInfinity
            For i As Integer = 0 To list.Length - 1
                If list(i) > maximum Then
                    maximum = list(i)
                End If
            Next
            Return maximum
        End Function

        Public Function Q1() As Double
            Return Qi(0.25)
        End Function

        Public Function Q2() As Double
            Return Qi(0.5)
        End Function

        Public Function Q3() As Double
            Return Qi(0.75)
        End Function

        Public Function mean() As Double
            Try
                Dim sum As Double = 0
                For i As Integer = 0 To list.Length - 1
                    sum += list(i)
                Next
                Return sum / list.Length
            Catch generatedExceptionName As Exception
                Return Double.NaN
            End Try
        End Function

        Public Function range() As Double
            Dim minimum As Double = min()
            Dim maximum As Double = max()
            Return (maximum - minimum)
        End Function

        Public Function IQ() As Double
            Return Q3() - Q1()
        End Function

        Public Function middle_of_range() As Double
            Dim minimum As Double = min()
            Dim maximum As Double = max()
            Return (minimum + maximum) / 2
        End Function

        Public Function var() As Double
            Try
                Dim s As Double = 0
                For i As Integer = 0 To list.Length - 1
                    s += Math.Pow(list(i), 2)
                Next
                Return (s - list.Length * Math.Pow(mean(), 2)) / (list.Length - 1)
            Catch generatedExceptionName As Exception
                Return Double.NaN
            End Try
        End Function

        Public Function s() As Double
            Return Math.Sqrt(var())
        End Function

        Public Function YULE() As Double
            Try
                Return ((Q3() - Q2()) - (Q2() - Q1())) / (Q3() - Q1())
            Catch generatedExceptionName As Exception
                Return Double.NaN
            End Try
        End Function

        Public Function Z(ByVal member As Double) As Double
            Try
                If exist(member) Then
                    Return (member - mean()) / s()
                Else
                    Return Double.NaN
                End If
            Catch generatedExceptionName As Exception
                Return Double.NaN
            End Try
        End Function

        Public Function cov(ByVal s As statistics) As Double
            Try
                If Me.length() <> s.length() Then
                    Return Double.NaN
                End If
                Dim len As Integer = Me.length()
                Dim sum_mul As Double = 0
                For i As Integer = 0 To len - 1
                    sum_mul += (Me.list(i) * s.list(i))
                Next
                Return (sum_mul - len * Me.mean() * s.mean()) / (len - 1)
            Catch generatedExceptionName As Exception
                Return Double.NaN
            End Try
        End Function

        Public Shared Function cov(ByVal s1 As statistics, ByVal s2 As statistics) As Double
            Try
                If s1.length() <> s2.length() Then
                    Return Double.NaN
                End If
                Dim len As Integer = s1.length()
                Dim sum_mul As Double = 0
                For i As Integer = 0 To len - 1
                    sum_mul += (s1.list(i) * s2.list(i))
                Next
                Return (sum_mul - len * s1.mean() * s2.mean()) / (len - 1)
            Catch generatedExceptionName As Exception
                Return Double.NaN
            End Try
        End Function

        Public Function r(ByVal design As statistics) As Double
            Try
                Return Me.cov(design) / (Me.s() * design.s())
            Catch generatedExceptionName As Exception
                Return Double.NaN
            End Try
        End Function

        Public Shared Function r(ByVal design1 As statistics, ByVal design2 As statistics) As Double
            Try
                Return cov(design1, design2) / (design1.s() * design2.s())
            Catch generatedExceptionName As Exception
                Return Double.NaN
            End Try
        End Function

        Public Function a(ByVal design As statistics) As Double
            Try
                Return Me.cov(design) / (Math.Pow(design.s(), 2))
            Catch generatedExceptionName As Exception
                Return Double.NaN
            End Try
        End Function

        Public Shared Function a(ByVal design1 As statistics, ByVal design2 As statistics) As Double
            Try
                Return cov(design1, design2) / (Math.Pow(design2.s(), 2))
            Catch generatedExceptionName As Exception
                Return Double.NaN
            End Try
        End Function

        Public Function b(ByVal design As statistics) As Double
            Return Me.mean() - Me.a(design) * design.mean()
        End Function

        Public Shared Function b(ByVal design1 As statistics, ByVal design2 As statistics) As Double
            Return design1.mean() - a(design1, design2) * design2.mean()
        End Function

        Private Function Qi(ByVal i As Double) As Double
            Try
                Dim j As Double() = New Double(list.Length - 1) {}
                list.CopyTo(j, 0)
                sort(j)
                If Math.Ceiling(list.Length * i) = list.Length * i Then
                    Return (j(CInt(Math.Truncate(list.Length * i - 1))) + j(CInt(Math.Truncate(list.Length * i)))) / 2
                Else
                    Return j(CInt(Math.Truncate(Math.Ceiling(list.Length * i))) - 1)
                End If
            Catch generatedExceptionName As Exception
                Return Double.NaN
            End Try
        End Function

        Private Sub sort(ByVal i As Double())
            Dim temp As Double() = New Double(i.Length - 1) {}
            merge_sort(i, temp, 0, i.Length - 1)
        End Sub

        Private Sub merge_sort(ByVal source As Double(), ByVal temp As Double(), ByVal left As Integer, ByVal right As Integer)
            Dim mid As Integer
            If left < right Then
                mid = (left + right) \ 2
                merge_sort(source, temp, left, mid)
                merge_sort(source, temp, mid + 1, right)
                merge(source, temp, left, mid + 1, right)
            End If
        End Sub

        Private Sub merge(ByVal source As Double(), ByVal temp As Double(), ByVal left As Integer, ByVal mid As Integer, ByVal right As Integer)
            Dim i As Integer, left_end As Integer, num_elements As Integer, tmp_pos As Integer
            left_end = mid - 1
            tmp_pos = left
            num_elements = right - left + 1
            While (left <= left_end) AndAlso (mid <= right)
                If source(left) <= source(mid) Then
                    temp(tmp_pos) = source(left)
                    tmp_pos += 1
                    left += 1
                Else
                    temp(tmp_pos) = source(mid)
                    tmp_pos += 1
                    mid += 1
                End If
            End While
            While left <= left_end
                temp(tmp_pos) = source(left)
                left += 1
                tmp_pos += 1
            End While
            While mid <= right
                temp(tmp_pos) = source(mid)
                mid += 1
                tmp_pos += 1
            End While
            For i = 1 To num_elements
                source(right) = temp(right)
                right -= 1
            Next
        End Sub

        Private Function exist(ByVal member As Double) As Boolean
            Dim is_exist As Boolean = False
            Dim i As Integer = 0
            While i <= list.Length - 1 AndAlso Not is_exist
                is_exist = (list(i) = member)
                i += 1
            End While
            Return is_exist
        End Function

    End Class

End Namespace
