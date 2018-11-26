Partial Class Marathon
    Public Class Plan
        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private _companyId As String
        Public Property CompanyID() As String
            Get
                Return _companyId
            End Get
            Set(ByVal value As String)
                _companyId = value
            End Set
        End Property

        Private _clientID As String
        Public Property ClientID() As String
            Get
                Return _clientID
            End Get
            Set(ByVal value As String)
                _clientID = value
            End Set
        End Property

        Private _productID As String
        Public Property ProductID() As String
            Get
                Return _productID
            End Get
            Set(ByVal value As String)
                _productID = value
            End Set
        End Property

        Private _agreementID As String
        Public Property AgreementID() As String
            Get
                Return _agreementID
            End Get
            Set(ByVal value As String)
                _agreementID = value
            End Set
        End Property

        Private _userID As String
        Public Property UserID() As String
            Get
                Return _userID
            End Get
            Set(ByVal value As String)
                _userID = value
            End Set
        End Property

        Private _orders As New List(Of Order)
        Public ReadOnly Property Orders() As List(Of Order)
            Get
                Return _orders
            End Get
        End Property

    End Class
End Class
