Imports System.Xml
Imports System.Text
Imports System.Data

Public Class cDBReaderSQL

    Dim planner As Xml.XmlNode
    Dim buyers As Xml.XmlNode


    Private connectionString As String = "Server=STO-APP60;Database=trinity_mec;Uid=johanh;Pwd=turbo"
    Public Property _connectionString As String
        Get
            Return connectionString
        End Get
        Set(ByVal value As String)
            connectionString = value
        End Set
    End Property

    Public Function GetCampaign()

        Using _conn As New SqlClient.SqlConnection(_connectionString)
            _conn.Open()
            Using Command As New SqlClient.SqlCommand
                Command.Connection = _conn
                Command.CommandText = "SELECT xml from campaigns where id = '338'"
                Using ds As New DataTable
                    Try
                        Using rd As SqlClient.SqlDataReader = Command.ExecuteReader
                            ds.Load(rd)
                            If ds.Rows.Count = 0 Then
                                _conn.Close()
                                Return ""
                            End If

                            Dim tmpXML As New XmlDocument
                            tmpXML.LoadXml(ds.Rows(0).Item(0))
                            If tmpXML.SelectSingleNode("Campaign").Attributes("DatabaseID") Is Nothing Then
                                DirectCast(tmpXML.SelectSingleNode("Campaign"), XmlElement).SetAttribute("DatabaseID", "338")
                            End If
                            _conn.Close()
                            Return tmpXML.OuterXml.ToString()
                        End Using
                    Catch ex As Exception
                        _conn.Close()
                        Return ""
                    End Try
                    _conn.Close()
                End Using
            End Using
        End Using
    End Function

    Public Function GetBuyer(ByVal buyerText)

        Dim xmlE As Xml.XmlElement

        If Not buyers Is Nothing Then
            xmlE = buyers.SelectNodes("//buyer[@name='" & buyerText & "']").Item(0)
        End If

        Return xmlE.Value

    End Function

    Public Function GetPlanner(ByVal plannerText)


        Dim xmlE As Xml.XmlElement

        If Not buyers Is Nothing Then
            xmlE = buyers.SelectNodes("//planner[@name='" & plannerText & "']").Item(0)
        End If

        Return xmlE.Value

    End Function

    Public Function getAllClients() As DataTable
        Using _clients As DataTable = New DataTable
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT * FROM Clients ORDER By Name", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        _clients.Load(rd)
                        rd.Close()
                        _conn.Close()
                        Return _clients
                    End Using
                End Using
            End Using
        End Using
    End Function

End Class
