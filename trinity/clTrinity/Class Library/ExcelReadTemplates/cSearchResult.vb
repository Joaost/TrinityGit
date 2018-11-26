
Namespace ExcelReadTemplates
    Public Class cSearchResult

        Private _succeeded As Boolean = False
        Public Property Succeeded() As Boolean
            Get
                Return _succeeded
            End Get
            Friend Set(ByVal value As Boolean)
                _succeeded = value
            End Set
        End Property

        Private _result As String
        Public Property Result() As String
            Get
                Return _result
            End Get
            Friend Set(ByVal value As String)
                _result = value
            End Set
        End Property

        Private _row As Integer
        Public Property Row() As Integer
            Get
                Return _row
            End Get
            Friend Set(ByVal value As Integer)
                _row = value
            End Set
        End Property

        Private _column As String
        Public Property Column() As String
            Get
                Return _column
            End Get
            Friend Set(ByVal value As String)
                _column = value
            End Set
        End Property

    End Class
End Namespace