Partial Public Class Marathon
    Public Class Order
        Private _planNumber As String
        Public Property PlanNumber() As String
            Get
                Return _planNumber
            End Get
            Set(ByVal value As String)
                _planNumber = value
            End Set
        End Property

        Private _companyID As String
        Public Property CompanyID() As String
            Get
                Return _companyID
            End Get
            Set(ByVal value As String)
                _companyID = value
            End Set
        End Property

        Private _mediaID As String
        Public Property MediaID() As String
            Get
                Return _mediaID
            End Get
            Set(ByVal value As String)
                _mediaID = value
            End Set
        End Property

    End Class
End Class
