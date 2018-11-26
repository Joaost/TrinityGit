Public Partial Class findstore
    Inherits System.Web.UI.Page

    Dim startIndex As Integer
    Dim pageSize As Integer = 10

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            grdSearchResults.PageSize = pageSize
            Update()
        End If
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Update()
    End Sub

    Sub Update()
        Dim Stores As DataTable = DirectCast(Session("Database"), cDBReader).GetStores
        Dim FilteredStores = (From Store As DataRow In Stores Skip startIndex Take pageSize Select Store, Store!Name, Store!Address, Store!City, Store!PhoneNo Where SearchFilter(Store, txtSearch.Text))
        grdSearchResults.VirtualItemCount = (From Store As DataRow In Stores Select Store Where SearchFilter(Store, txtSearch.Text)).Count
        grdSearchResults.DataSource = FilteredStores
        grdSearchResults.DataBind()
    End Sub

    Private Function SearchFilter(ByVal Store As DataRow, ByVal SearchString As String) As Boolean
        If SearchString = "" Then Return True
        For Each TmpString As String In SearchString.ToUpper.Split(",")
            Dim WordExists As Boolean = False
            If Store!Address.ToUpper.Contains(TmpString.Trim) Then
                WordExists = True
            ElseIf Store!City.ToUpper.Contains(TmpString.Trim) Then
                WordExists = True
            ElseIf Store!Name.ToUpper.Contains(TmpString.Trim) Then
                WordExists = True
            End If
            If Not WordExists Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub grdSearchResults_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdSearchResults.PageIndexChanged
        grdSearchResults.CurrentPageIndex = e.NewPageIndex
        startIndex = grdSearchResults.CurrentPageIndex * grdSearchResults.PageSize
        Update()
    End Sub
End Class