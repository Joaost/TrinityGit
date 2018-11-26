Public Partial Class fetchdoc
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim TmpRow As DataRow = DirectCast(Session("Database"), cDBReader).GetDocument(Request.QueryString("id"))
        If UCase(TmpRow!doctype) = ".PDF" Then
            Response.ContentType = "application/pdf"
        ElseIf UCase(TmpRow!doctype) = ".DOC" Then
            Response.ContentType = "application/msword"
        End If
        Dim TmpByte() As Byte = TmpRow!document
        Response.BinaryWrite(TmpByte)
        'Response.End()
    End Sub

End Class