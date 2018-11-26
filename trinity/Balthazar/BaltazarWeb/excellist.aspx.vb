Public Partial Class excellist
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Clear()
        Response.Charset = ""
        'set the response mime type for excel
        Response.ContentType = "application/vnd.ms-excel"
        'create a string writer
        Dim stringWrite As New System.IO.StringWriter
        'create an htmltextwriter which uses the stringwriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
        'instantiate a datagrid
        Dim dg As New DataGrid
        dg.Columns.Add(New WebControls.BoundColumn With {.HeaderText = "Kampanj", .DataField = "Name"})
        dg.Columns.Add(New WebControls.BoundColumn With {.HeaderText = "Datum", .DataField = "Dates"})
        dg.Columns.Add(New WebControls.BoundColumn With {.HeaderText = "Butik", .DataField = "Store"})
        dg.Columns.Add(New WebControls.BoundColumn With {.HeaderText = "Ort", .DataField = "city"})
        dg.Columns.Add(New WebControls.BoundColumn With {.HeaderText = "Namn personal", .DataField = "chosenproviderstaffname"})
        dg.AutoGenerateColumns = False

        'set the datagrid datasource to the dataset passed in
        dg.DataSource = DirectCast(Session("Database"), cDBReader).GetBookings(cDBReader.BookingStatusEnum.bsConfirmed)
        'bind the datagrid
        dg.DataBind()
        'tell the datagrid to render itself to our htmltextwriter
        dg.RenderControl(htmlWrite)
        'all that's left is to output the html
        Response.Write("<b>Bekr&auml;ftade demonstrationer</b><br /><br />")
        Dim TmpString As String = stringWrite.ToString
        TmpString = TmpString.Replace("å", "&aring;")
        TmpString = TmpString.Replace("ä", "&auml;")
        TmpString = TmpString.Replace("ö", "&ouml;")
        TmpString = TmpString.Replace("Å", "&Aring;")
        TmpString = TmpString.Replace("Ä", "&Auml;")
        TmpString = TmpString.Replace("Ö", "&Ouml;")
        Response.Write(TmpString)
        Response.End()
    End Sub

End Class