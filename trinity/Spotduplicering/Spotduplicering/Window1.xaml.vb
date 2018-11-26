
Imports System.Runtime.InteropServices
Imports Microsoft.Windows.Controls
Imports System.Data

Class Window1

    Public Delegate Sub UpdateProgressBarDelegate(ByVal dp As System.Windows.DependencyProperty, ByVal value As Object)

    Dim brands As Connect.Brands
    Dim other As Connect.ComModule


    'Dim brands As ConnectWrapper.Brands

    Public Shared tmpSpots As Spots
    Public Shared mydatatable As New DataTable

    Dim targetgroup As String = "Tobias Nurmi"
    Dim brandfilter As String = "Analys"

    Sub CreateColumns()

        If mydatatable.Columns.Count = 0 Then

            mydatatable.Columns.Add("Advertiser", Type.GetType("System.String"))
            mydatatable.Columns.Add("Product", Type.GetType("System.String"))
            mydatatable.Columns.Add("Datum", Type.GetType("System.String"))
            mydatatable.Columns.Add("Tid", Type.GetType("System.String"))
            mydatatable.Columns.Add("Kanal", Type.GetType("System.String"))
            mydatatable.Columns.Add("BreakID", Type.GetType("System.String"))
            mydatatable.Columns.Add("Filmkod", Type.GetType("System.String"))
            mydatatable.Columns.Add("ProgramAfter", Type.GetType("System.String"))
            mydatatable.Columns.Add("TRP", Type.GetType("System.Double"))
            mydatatable.Columns.Add("Remarks", Type.GetType("System.String"))
        End If
    End Sub

    Sub GetTobiasSpots()

        Dim brands As Connect.Brands
        'Dim other As Connect.ComModule


        'Dim brands As ConnectWrapper.Brands


        mydatatable = New DataTable


        Dim z As Integer
        Dim I As Integer
        Dim dateString As String = Format(fromDate.SelectedDate, "ddMMyy") & "-" & Format(toDate.SelectedDate, "ddMMyy")

        Dim updateSpotsDelegate As New UpdateProgressBarDelegate(AddressOf prgSpots.SetValue)
        Dim updateExcelDelegate As New UpdateProgressBarDelegate(AddressOf prgExcel.SetValue)

        tmpSpots = New Spots

        lblWhatsHappening.Content = "Trying to create an AdvantEdge connection..."

        brands = CreateObject("Connect.brands")
        z = brands.setArea("SE")
        z = brands.setChannels("TV4, Kanal 5, TV3 se, TV6 se, TV4+, Kanal 9, Discovery se, Eurosport se, MTV se, TV8, TV4 Sport, TV4 Fakta")
        z = brands.setBrandFilterUserDefined(targetgroup, brandfilter)
        brands.setPeriod(dateString)
        z = brands.setTargetMnemonic("12-59", False)

        Dim errorCode As Long = brands.validate
        If errorCode <> 0 Then
            MessageBox.Show("Advantedge har troligen inte data för denna period ännu. Försök med ett annat datum.")
            Exit Sub
        End If

        lblWhatsHappening.Content = "Trying to fetch all the spots from AdvantEdge..."
        z = brands.Run

        prgSpots.Maximum = z
        prgSpots.Minimum = 0

        Dim someTime As Date

        Dim BreakList As New List(Of String)
        Dim DoubleBreaks As New List(Of String)

        For I = 0 To z - 1
            lblWhatsHappening.Content = "Collecting spot number " & I + 1
            someTime = New Date()
            Dim tmpSpot As New Spot

            tmpSpot.Advertiser = brands.getAttrib(Connect.eAttribs.aBrandAdvertiser, I)
            tmpSpot.Product = brands.getAttrib(Connect.eAttribs.aBrandProduct, I)
            tmpSpot.Datum = Format(Date.FromOADate(brands.getAttrib(Connect.eAttribs.aDate, I)), "dd-MM-yyyy")
            tmpSpot.Tid = Format(someTime.AddSeconds(brands.getAttrib(Connect.eAttribs.aFromTime, I)), "HH:mm")
            tmpSpot.Kanal = brands.getAttrib(Connect.eAttribs.aChannel, I)
            tmpSpot.BreakID = brands.getAttrib(Connect.eAttribs.aBrandBreakSeqID, I)
            tmpSpot.Filmkod = brands.getAttrib(Connect.eAttribs.aBrandFilmCode, I)
            tmpSpot.ProgramAfter = brands.getAttrib(Connect.eAttribs.aBrandProgAfter, I)
            tmpSpot.TRP = brands.getUnit(Connect.eUnits.uTRP, I)


            Dim value1 As String = brands.getAttrib(Connect.eAttribs.aBrandFilmCode, I)

            Dim keypart1 As String = tmpSpot.Datum
            Dim keypart2 As String = tmpSpot.BreakID
            Dim key As String = keypart1 & keypart2 & tmpSpot.Advertiser
            If Not BreakList.Contains(key) Then
                BreakList.Add(key)
            Else
                If Not DoubleBreaks.Contains(key) Then
                    DoubleBreaks.Add(key)
                End If
            End If
            tmpSpots.Add(tmpSpot)

            prgSpots.Value += 1
            If prgSpots.Value Mod 100 = 0 Then
                ' Dispatcher.Invoke(updateSpotsDelegate, System.Windows.Threading.DispatcherPriority.Background, New Object() {ProgressBar.ValueProperty, prgSpots.Value})
            End If
        Next

        prgExcel.Minimum = 0
        prgExcel.Maximum = z

        lblWhatsHappening.Content = "Creating the columns in the report..."
        CreateColumns()

        Dim p As Integer = 0

        Dim sortedSpots As Object
        sortedSpots = (From x As Spot In tmpSpots Order By x.Kanal, x.Advertiser, x.Datum, x.Tid Select x)

        For Each tmpSpot As Spot In sortedSpots
            lblWhatsHappening.Content = "Adding spot number " & p + 1 & " to the report..."
            Dim keypart1 As String = tmpSpot.Datum
            Dim keypart2 As String = tmpSpot.BreakID
            Dim key As String = keypart1 & keypart2 & tmpSpot.Advertiser
            If DoubleBreaks.Contains(key) Then
                tmpSpot.Remarks = "DOUBLE"
            Else
                tmpSpot.Remarks = "INGEN ANM"
            End If

            Dim myrow As DataRow
            myrow = mydatatable.NewRow
            myrow("Advertiser") = tmpSpot.Advertiser
            myrow("Product") = tmpSpot.Product
            myrow("Datum") = tmpSpot.Datum
            myrow("Tid") = tmpSpot.Tid
            myrow("Kanal") = tmpSpot.Kanal
            myrow("BreakID") = tmpSpot.BreakID
            myrow("Filmkod") = tmpSpot.Filmkod
            myrow("ProgramAfter") = tmpSpot.ProgramAfter
            myrow("TRP") = CDbl(tmpSpot.TRP)
            myrow("Remarks") = tmpSpot.Remarks
            mydatatable.Rows.Add(myrow)

            prgExcel.Value += 1
            If prgExcel.Value Mod 100 = 0 Then
                'Dispatcher.Invoke(updateExcelDelegate, System.Windows.Threading.DispatcherPriority.Background, New Object() {ProgressBar.ValueProperty, prgExcel.Value})
            End If
            p += 1
        Next

        lblWhatsHappening.Content = "Opening the report window..."
        Dim spotswindow As New Window3

        lblWhatsHappening.Content = "spotswindow.showdialog()"
        spotswindow.ShowDialog()

        lblWhatsHappening.Content = "Ready for new report"

        prgExcel.Value = 0
        prgSpots.Value = 0
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        GetTobiasSpots()
        '  GetTargets()
    End Sub

    Private Sub cmdChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChange.Click

        Dim newWindow As New Window2
        newWindow.ShowDialog()
        Try
            targetgroup = Strings.Split(newWindow.result, ",")(0)
            brandfilter = Strings.Split(newWindow.result, ",")(1)
        Catch
        End Try
    End Sub


    Private Sub toDate_SelectedDateChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles toDate.SelectedDateChanged

    End Sub

    Private Sub toDate_TargetUpdated(ByVal sender As Object, ByVal e As System.Windows.Data.DataTransferEventArgs) Handles toDate.TargetUpdated

    End Sub

    Private Sub fromDate_SelectedDateChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles fromDate.SelectedDateChanged
        toDate.SelectedDate = fromDate.SelectedDate.Value.AddMonths(1).AddDays(-1)
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click

    End Sub
End Class
