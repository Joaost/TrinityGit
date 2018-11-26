Namespace Trinity
    Public Class cDaypart

        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                If Parent IsNot Nothing AndAlso value <> _name Then
                    Parent.ReplaceKey(_name, value)
                End If
                _name = value
            End Set
        End Property

        Private _startMaM As Integer
        Public Property StartMaM() As Integer
            Get
                Return _startMaM
            End Get
            Set(ByVal value As Integer)
                _startMaM = value
                If _parent IsNot Nothing AndAlso _parent.Parent IsNot Nothing Then _parent.Parent.InvalidateMainDaypartSplit()
            End Set
        End Property

        Private _endMaM As Integer
        Public Property EndMaM() As Integer
            Get
                Return _endMaM
            End Get
            Set(ByVal value As Integer)
                _endMaM = value
                If _parent IsNot Nothing AndAlso _parent.Parent IsNot Nothing Then _parent.Parent.InvalidateMainDaypartSplit()
            End Set
        End Property

        Private _isPrime As Boolean
        Public Property IsPrime() As Boolean
            Get
                Return _isPrime
            End Get
            Set(ByVal value As Boolean)
                _isPrime = value
            End Set
        End Property

        Private _share As Single
        Public Property Share() As Single
            Get
                Return _share
            End Get
            Set(ByVal value As Single)
                _share = value
                If _parent IsNot Nothing AndAlso _parent.Parent IsNot Nothing Then _parent.Parent.InvalidateMainDaypartSplit()
            End Set
        End Property

        Private _parent As cDayparts
        Public Property Parent() As cDayparts
            Get
                Return _parent
            End Get
            Set(ByVal value As cDayparts)
                _parent = value
            End Set
        End Property

        Private _myindex As Integer
        Public Property MyIndex() As Integer
            Get
                Return _myindex
            End Get
            Set(ByVal value As Integer)
                _myindex = value
            End Set
        End Property

    End Class
End Namespace