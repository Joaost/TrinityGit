Public Class cSearchValues

    Private _budgetTotalCTC As Integer
    Private _client As String
    Private _compensation As String

#Region "Props"

    Public Property TotalBudgetCTC() As Integer
        Get
            Return _budgetTotalCTC
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property


    Public Property Client() As String
        Get
            Return _client
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property Compensation() As String
        Get
            Return _client
        End Get
        Set(ByVal value As String)

        End Set
    End Property

#End Region


End Class
