Partial Class Marathon
    Public Class Insertion
        Private _companyID As String
        Public Property CompanyID() As String
            Get
                Return _companyID
            End Get
            Set(ByVal value As String)
                _companyID = value
            End Set
        End Property

        Private _orderNumber As String
        Public Property OrderNumber() As String
            Get
                Return _orderNumber
            End Get
            Set(ByVal value As String)
                _orderNumber = value
            End Set
        End Property

        Private _insertionDate As Date
        Public Property InsertionDate() As Date
            Get
                Return _insertionDate
            End Get
            Set(ByVal value As Date)
                _insertionDate = value
            End Set
        End Property

        Private _endDate As Date
        Public Property EndDate() As Date
            Get
                Return _endDate
            End Get
            Set(ByVal value As Date)
                _endDate = value
            End Set
        End Property

        Private _priceRows As New List(Of Pricerow)
        Public Property PriceRows() As List(Of Pricerow)
            Get
                Return _priceRows
            End Get
            Set(ByVal value As List(Of Pricerow))
                _priceRows = value
            End Set
        End Property


        Friend Function GetFormattedInsertionDate() As String
            Return Format(_insertionDate, "yyyy-MM-dd")
        End Function

        Friend Function GetFormattedEndDate() As String
            Return Format(_endDate, "yyyy-MM-dd")
        End Function

    End Class
End Class