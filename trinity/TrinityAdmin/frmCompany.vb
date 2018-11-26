Public Class frmCompany
    Dim countries() As String = {"SE", "NO", "DK"}
    Dim SE() As String = {"MEC", "Mediacom", "Maxus", "Mindshare"}
    Dim NO() As String = {"Gut", "Lusekofta"}
    Dim DK() As String = {"Spise", "Laudrup"}

    Dim penList As List(Of String)

    Public Property penetration() As List(Of String)
        Get
            Return penList
        End Get
        Set(ByVal value As List(Of String))

            'set the size of the windows to minimal
            Me.Height = 130
            Me.Width = 185

            If value Is Nothing Then Exit Property

            penList = value

            'read the list and setup the window
            If penList(0) = "All/" Then
                'this is the default setting so no change is needed
            Else
                'not all countries

                'set the radio button and show the grid
                chkPickCountry.Checked = True
                Me.Width = 332
                grpCompanies.Visible = True

                If penList(0).Contains("All") Then
                    'all companies on the selected countries
                    'we only need to get countries


                Else
                    'certain companies on the selected countries
                    'we need to get both countries and company

                    Dim coun As String
                    Dim comp As String
                    Dim j As Integer

                    For i As Integer = 0 To penList.Count - 1
                        coun = penList(i).Substring(0, penList(i).IndexOf("/"))

                        For j = 0 To grdCountries.Rows.Count - 1
                            If grdCountries.Rows(j).Cells(0).Value = coun Then
                                grdCountries.Rows(j).Cells(0).Style.BackColor = Color.Red
                            End If
                        Next
                    Next

                    'need the countries before we change nad populare the company list
                    chkPickCompany.Checked = True

                    For i As Integer = 0 To penList.Count - 1
                        comp = penList(i)

                        For j = 0 To grdCompanies.Rows.Count - 1
                            If grdCompanies.Rows(j).Cells(0).Value = comp Then
                                grdCompanies.Rows(j).Cells(0).Style.BackColor = Color.Red
                            End If
                        Next
                    Next

                End If

            End If

            grdCountries.ClearSelection()
            grdCompanies.ClearSelection()
        End Set
    End Property

    Private Sub popCounties()
        Dim j As Integer
        grdCountries.Rows.Clear()
        For i As Integer = 0 To countries.Length - 1
            j = grdCountries.Rows.Add()
            grdCountries.Rows(j).Cells(0).Value = countries(i)
            grdCountries.Rows(j).Cells(0).Tag = countries(i)
        Next
        grdCountries.ClearSelection()
    End Sub

    Private Sub popCompanies()
        Dim i As Integer
        Dim j As Integer
        grdCompanies.Rows.Clear()

        For Each row As DataGridViewRow In grdCountries.Rows
            If row.Cells(0).Style.BackColor = Color.Red Then
                Select Case row.Cells(0).Value
                    Case Is = "SE"
                        For i = 0 To SE.Length - 1
                            j = grdCompanies.Rows.Add()
                            grdCompanies.Rows(j).Cells(0).Value = "SE/" & SE(i)
                            grdCompanies.Rows(j).Cells(0).Tag = "SE/" & SE(i)
                            grdCompanies.Rows(j).Cells(0).Style.BackColor = Color.LightYellow
                        Next

                    Case Is = "NO"
                        For i = 0 To NO.Length - 1
                            j = grdCompanies.Rows.Add()
                            grdCompanies.Rows(j).Cells(0).Value = "NO/" & NO(i)
                            grdCompanies.Rows(j).Cells(0).Tag = "NO/" & NO(i)
                            grdCompanies.Rows(j).Cells(0).Style.BackColor = Color.LightSkyBlue
                        Next

                    Case Is = "DK"
                        For i = 0 To DK.Length - 1
                            j = grdCompanies.Rows.Add()
                            grdCompanies.Rows(j).Cells(0).Value = "DK/" & DK(i)
                            grdCompanies.Rows(j).Cells(0).Tag = "DK/" & DK(i)
                            grdCompanies.Rows(j).Cells(0).Style.BackColor = Color.LightSalmon
                        Next

                End Select
            End If
        Next
    End Sub

    Private Sub frmCompany_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        grdCountries.ClearSelection()
        grdCompanies.ClearSelection()
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        penList = New List(Of String)
        If chkAll.Checked Then
            'if all we just add a simple list value
            penList.Add("All/")
        Else
            'now we need to check the companies aswell
            If chkAllCompanies.Checked Then
                For Each row As DataGridViewRow In grdCountries.Rows
                    If row.Cells(0).Style.BackColor = Color.Red Then
                        penList.Add(row.Cells(0).Value & "/All/")
                    End If
                Next
            Else
                For Each row As DataGridViewRow In grdCompanies.Rows
                    If row.Cells(0).Style.BackColor = Color.Red Then
                        penList.Add(row.Cells(0).Value)
                    End If
                Next
            End If
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub chkPickCountry_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPickCountry.CheckedChanged
        If chkPickCountry.Checked Then
            grdCountries.Visible = True
            popCounties()
            Me.Height = 301
        Else
            grdCountries.Visible = False
            grpCompanies.Visible = False
            Me.Height = 130
            Me.Width = 185
        End If
    End Sub

    Private Sub grdCountries_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCountries.CellClick
        Me.Width = 332
        grpCompanies.Visible = True

        If grdCountries.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.Red Then
            grdCountries.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.White
        Else
            grdCountries.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.Red
        End If
        grdCountries.ClearSelection()
        popCompanies()
    End Sub

    Private Sub grdCompanies_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCompanies.CellClick
        If grdCompanies.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.Red Then
            grdCompanies.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.White
        Else
            grdCompanies.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.Red
        End If
        grdCompanies.ClearSelection()
    End Sub

    Private Sub chkPickCompany_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPickCompany.CheckedChanged
        If chkPickCompany.Checked Then
            grdCompanies.Visible = True
            popCompanies()
        Else
            grdCompanies.Visible = False
        End If
    End Sub
End Class