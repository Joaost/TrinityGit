Public Partial Class info
    Inherits System.Web.UI.Page

    Public ReadOnly Property Database() As cDBReader
        Get
            If Session("Database") Is Nothing Then
                Session("Database") = New cDBReaderSQL
            End If
            Return DirectCast(Session("Database"), cDBReader)
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            grdCategories.DataSource = Database.GetCategories
            grdCategories.DataBind()
        End If
    End Sub

End Class