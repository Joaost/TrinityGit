Namespace Trinity
    Public Class cDayparts
        Implements IEnumerable

        Private mCol As New Collection

        Public Sub Add(ByVal Daypart As cDaypart)
            If Not mCol.Contains(Daypart.Name) Then
                Daypart.Parent = Me
                mCol.Add(Daypart, Daypart.Name)
                Daypart.MyIndex = mCol.Count
            End If
        End Sub

        Public Function Contains(ByVal Daypart As cDaypart) As Boolean
            Return mCol.Contains(Daypart.Name)
        End Function

        Public Function Contains(ByVal Daypart As String) As Boolean
            Return mCol.Contains(Daypart)
        End Function

        Friend Sub Replace(ByVal OldDaypart As cDaypart, ByVal NewDaypart As cDaypart)
            'Create place holders
            Dim _tempName As String = CreateGUID()
            Dim _tempObject As New Object

            mCol.Add(_tempObject, _tempName, After:=OldDaypart.Name)
            mCol.Remove(OldDaypart.Name)
            mCol.Add(NewDaypart, NewDaypart.Name, After:=_tempName)
            mCol.Remove(_tempName)
        End Sub

        Friend Sub ReplaceKey(ByVal OldKey As String, ByVal NewKey As String)
            If OldKey Is Nothing Then OldKey = ""
            If Not mCol.Contains(OldKey) Then Exit Sub
            Dim _tempName As String = CreateGUID()
            Dim _tempObject As New Object
            Dim _myObject As cDaypart = mCol(OldKey)
            mCol.Add(_tempObject, _tempName, After:=OldKey)
            mCol.Remove(OldKey)
            mCol.Add(_myObject, NewKey, Before:=_tempName)
            mCol.Remove(_tempName)
        End Sub

        Public Sub SwitchPosition(ByVal FirstDaypart As cDaypart, ByVal SecondDaypart As cDaypart)
            Dim _tempFirst As String = CreateGUID()
            Dim _tempSecond As String = CreateGUID()
            Dim _tempObject As New Object
            Dim _myObject As cDaypart = mCol(SecondDaypart.Name)
            mCol.Add(_tempObject, _tempFirst, Before:=FirstDaypart.Name)
            mCol.Add(_tempObject, _tempSecond, Before:=SecondDaypart.Name)
            mCol.Remove(SecondDaypart.Name)
            mCol.Remove(FirstDaypart.Name)
            mCol.Add(SecondDaypart, SecondDaypart.Name, Before:=_tempFirst)
            mCol.Add(FirstDaypart, FirstDaypart.Name, Before:=_tempSecond)
            mCol.Remove(_tempFirst)
            mCol.Remove(_tempSecond)
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function

        Default ReadOnly Property Item(ByVal index As Integer) As cDaypart
            Get
                'The old definition was zero-based, so we add 1 for backwards compatibility
                If index < mCol.Count Then
                    Return mCol(index + 1)
                Else
                    Dim myParent As cBookingType = DirectCast(_parent, cBookingType)
                    Throw New IndexOutOfRangeException("Trinity tried to access a daypart that doesn't exist. The daypart number was " & index + 1 & " in " & _
                                                       myParent.ParentChannel.ChannelName & " - " & myParent.Name)
                End If

            End Get
        End Property

        Default ReadOnly Property Item(ByVal Name As String) As cDaypart
            Get
                Return mCol(Name)
            End Get
        End Property

        Public Function GetDaypartForMam(ByVal Mam As Integer) As cDaypart
            For Each _dp As cDaypart In mCol
                If (_dp.StartMaM <= Mam AndAlso _dp.EndMaM >= Mam) OrElse (_dp.StartMaM <= Mam + 24 * 60 AndAlso _dp.EndMaM >= Mam + 24 * 60) OrElse (_dp.StartMaM <= Mam - 24 * 60 AndAlso _dp.EndMaM >= Mam - 24 * 60) Then
                    Return _dp
                End If
            Next
            Return Nothing
        End Function

        Public Function GetDaypartIndexForMam(ByVal Mam As Integer) As Integer
            Dim _idx As Integer = 0
            For Each _dp As cDaypart In mCol
                If (_dp.StartMaM <= Mam AndAlso _dp.EndMaM >= Mam) OrElse (_dp.StartMaM <= Mam + 24 * 60 AndAlso _dp.EndMaM >= Mam + 24 * 60) OrElse (_dp.StartMaM <= Mam - 24 * 60 AndAlso _dp.EndMaM >= Mam - 24 * 60) Then
                    Return _idx
                End If
                _idx += 1
            Next
            Return -1
        End Function

        Public Function Count() As Integer
            Return mCol.Count
        End Function

        Public Sub Clear()
            mCol.Clear()
        End Sub

        Public Sub Remove(ByVal Daypart As cDaypart)
            mCol.Remove(Daypart.Name)
        End Sub

        Public Sub RemoveAt(ByVal Index As Integer)
            mCol.Remove(Index + 1)
        End Sub

        'Private _parent As cBookingType
        Private _parent As Object
        Public Sub New(ByVal Parent As cKampanj)
            _parent = Parent

        End Sub

        Public Sub New(ByVal Parent As cBookingType)
            _parent = Parent
            

        End Sub

        Public Sub New(ByVal Parent As Object)
            _parent = Parent


        End Sub

        Friend ReadOnly Property Parent() As Object
            Get
                Return _parent
            End Get
        End Property

        Friend ReadOnly Property FirstPrimeIndex As Integer
            Get
                For Each _dp As cDaypart In mCol
                    If _dp.IsPrime Then Return _dp.MyIndex
                Next
                Return -1
            End Get
        End Property

        Public Enum ProblemsEnum
            DoesNotCoverFullDay = 1
        End Enum

    End Class
End Namespace