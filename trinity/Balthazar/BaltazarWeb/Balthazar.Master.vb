Partial Public Class BalthazarMaster
    Inherits System.Web.UI.MasterPage

    Public ReadOnly Property Database() As cDBReader
        Get
            If Session("Database") Is Nothing Then
                Session("Database") = New cDBReaderSQL
            End If
            Return DirectCast(Session("Database"), cDBReader)
        End Get
    End Property

    Public ReadOnly Property Mam2Time(ByVal MaM As Integer) As String
        Get
            Return Helper.Mam2Time(MaM)
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class