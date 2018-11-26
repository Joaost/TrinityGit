Partial Public Class Trinity
    Inherits System.Web.UI.MasterPage

    Public Function Database() As TrinityViewer.cDBReader
        If Session("Database") Is Nothing Then
            Session("Database") = New TrinityViewer.cDBReader
        End If
        Return Session("Database")
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class
