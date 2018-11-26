Namespace Trinity
    Public Class CampaignEssentials
        Private _ID As Integer
        Public campaignid As String
        Public _name As String
        Public Year As Integer
        Public Month As Integer
        Public client As Integer
        Public product As Integer
        Public planner As String
        Public buyer As String
        Public lastopened As Date
        Public lastsaved As Date
        Public originalLocation As String
        Public originalfilechangeddate As String
        Public status As String
        Public startdate As Date
        Public enddate As Date
        Public contractid As Long = 0

        Public Property name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Public Property id() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
            End Set
        End Property
    End Class
End Namespace
