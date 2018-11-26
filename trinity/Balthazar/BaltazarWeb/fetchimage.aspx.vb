Partial Public Class fetchimage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.ContentType = "image/jpeg"
        Dim TmpByte() As Byte = DirectCast(Session("Database"), cDBReader).GetImage
        Response.OutputStream.Write(TmpByte, 0, TmpByte.Length)
        Response.End()
    End Sub

End Class