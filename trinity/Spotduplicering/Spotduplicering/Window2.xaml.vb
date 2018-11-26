Imports System.Windows
Partial Public Class Window2
    Public result As String

    Private Sub frmPicktarget_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Initialized

        If tvwBrands.Items.Count > 0 Then Exit Sub
        tvwBrands.Items.Clear()

        Dim excludedlist As New List(Of String)

        Dim fs As New System.IO.FileStream("excluded.txt", IO.FileMode.OpenOrCreate)
        Dim sr As New System.IO.StreamReader(fs)
        While Not sr.EndOfStream
            excludedlist.Add(sr.ReadLine)
        End While
        sr.Close()
        fs.Close()

        Dim parentnode As TreeViewItem = New TreeViewItem
        parentnode.Header = "Definitions"
        tvwBrands.Items.Add(parentnode)

        Dim brands As Connect.Brands
        brands = CreateObject("Connect.brands")

        Dim users As Integer = brands.lookupNoUsers

        For i = 0 To users - 1

            Dim username As String = brands.lookupUsers(i)
            Dim usernode As New TreeViewItem
            usernode.Header = username

            If Not excludedlist.Contains(username) Then
                parentnode.Items.Add(usernode)
                Try
                    Dim nobrandfilters As Integer = brands.lookupNoBrandFiltersByUser(username)


                    For p = 0 To nobrandfilters - 1
                        Dim brandfilter As String = brands.lookupBrandFiltersByUser(username, p)
                        Dim brandnode As TreeViewItem = New TreeViewItem
                        brandnode.Header = brandfilter
                        brandnode.Tag = username & "," & brandfilter
                        usernode.Items.Add(brandnode)
                    Next
                Catch
                    fs = New System.IO.FileStream("excluded.txt", IO.FileMode.Append)
                    Dim sw As New System.IO.StreamWriter(fs)
                    sw.WriteLine(username)
                    sw.Flush()
                    sw.Close()
                End Try
            Else
                ' Stop
            End If

        Next

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
        result = tvwBrands.SelectedItem.tag
    End Sub
End Class
