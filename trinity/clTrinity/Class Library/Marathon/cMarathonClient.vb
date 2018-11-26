Partial Public Class Marathon
    Public Class Client

        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private _id As String
        Public Property ID() As String
            Get
                Return _id
            End Get
            Set(ByVal value As String)
                _id = value
            End Set
        End Property

        Private _projects As New List(Of Project)
        Public Property Projects() As List(Of Project)
            Get
                Return _projects
            End Get
            Set(ByVal value As List(Of Project))
                _projects = value
            End Set
        End Property

    End Class
End Class
