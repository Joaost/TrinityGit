Namespace TrinityViewer
    Public Class cDBReader

        Private _loggedInUserID As Integer
        Public Function Connect() As Object
            Dim DBConn As New SqlClient.SqlConnection("Server=STO-APP60;Database=extranet;Uid=johanh;Pwd=turbo;")
            DBConn.Open()
            Return DBConn
        End Function

        Public Function Login(ByVal Username As String, ByVal Password As String) As Integer
            Using Conn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("SELECT id FROM users WHERE username=@username and Password=@Password", Conn)
                    com.Parameters.AddWithValue("@username", Username)
                    com.Parameters.AddWithValue("@password", Password)
                    Dim id As String = com.ExecuteScalar
                    If IsDBNull(id) Then
                        _loggedInUserID = -1
                        Return -1
                    Else
                        _loggedInUserID = id
                        Return id
                    End If
                End Using
            End Using
        End Function

        Public Sub RecoverPassword(ByVal username As String)

        End Sub

        Public Function Campaigns(ByVal Authorized As Boolean) As DataTable
            Using Conn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("SELECT *,convert(varchar(MAX),startdate,112)+'-'+convert(varchar(MAX),enddate,112) as dates FROM Campaign,usercampaigns WHERE userid=@uid and campaignid=campaign.id and authorized=@aut", Conn)
                    Try
                        com.Parameters.AddWithValue("@uid", _loggedInUserID)
                        com.Parameters.AddWithValue("@aut", Authorized)
                        Dim dt As New DataTable
                        dt.Load(com.ExecuteReader)
                        Return dt
                    Catch
                        Return Nothing
                    End Try
                End Using
            End Using
        End Function

        Public Function GetDocument(ByVal DocumentID As Integer) As DataRow
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    com.CommandText = "SELECT * FROM Docs WHERE id=" & DocumentID
                    Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
                    Dim dt As New DataTable
                    dt.Load(rd)
                    DBConn.Close()
                    Return dt.Rows(0)
                End Using
            End Using
        End Function

        Public Sub New()

        End Sub
    End Class
End Namespace
