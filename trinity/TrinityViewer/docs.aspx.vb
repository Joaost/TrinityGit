Public Partial Class docs
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim TmpRow As DataRow = DirectCast(Session("Database"), TrinityViewer.cDBReader).GetDocument(Request.QueryString("id"))
        'Response.ContentType = TmpRow!mimetype
        'Dim TmpByte() As Byte = TmpRow!data
        Context.Response.Clear()
        Context.Response.ClearContent()
        Context.Response.ClearHeaders()

        Dim file() As Byte = CType(TmpRow("Data"), Byte())
        Context.Response.ContentType = "application/octet-stream"
        Context.Response.AddHeader("content-disposition", "attachment;filename=" + TmpRow("Name"))
        Context.Response.BinaryWrite(file)
        'Response.BinaryWrite(TmpByte)
    End Sub

End Class