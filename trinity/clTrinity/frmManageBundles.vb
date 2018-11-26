Public Class frmManageBundles

    Dim BookingtypesBigList As List(Of Trinity.cBookingType)

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

        Dim BTList As New List(Of Trinity.cPricelistTarget)
        Dim Package As New KeyValuePair(Of String, List(Of Trinity.cPricelistTarget))(InputBox("Enter name for the new package", "Enter name"), BTList)
        If Package.Key <> "" Then
            Campaign.ChannelBundles.Add(Package.Key, Package.Value)
            cmbPackages.Items.Add(Package)
        End If
        cmbPackages.SelectedIndex = 0
       
    End Sub

    Private Sub frmManageBundles_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
       
    End Sub

    Private Sub cmbPackages_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPackages.SelectedIndexChanged

        If cmbPackages.SelectedItem Is Nothing Then Exit Sub

        lstContents.Items.Clear()
        lstAvailable.Items.Clear()

        For Each item As Object In cmbPackages.SelectedItem.value
            If item IsNot Nothing Then lstContents.Items.Add(item)
        Next

        lstAvailable.Items.AddRange(BookingtypesBigList.ToArray)

        For Each item As Object In lstContents.Items
            If lstAvailable.Items.Contains(item) Then
                lstAvailable.Items.Remove(item)
            End If

        Next

    End Sub

    Private Sub frmManageBundles_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lstContents.DisplayMember = "Longname"

        cmbPackages.DropDownStyle = Windows.Forms.ComboBoxStyle.DropDownList
        cmbPackages.DisplayMember = "Key"
        BookingtypesBigList = New List(Of Trinity.cBookingType)
        For Each tmpChan As Trinity.cChannel In Campaign.Channels
            For Each tmpBT As Trinity.cBookingType In tmpChan.BookingTypes
                BookingtypesBigList.Add(tmpBT)
            Next
        Next

        cmbPackages.Items.Clear()
        If Not Campaign.ChannelBundles Is Nothing Then
            For Each Bundle As Object In Campaign.ChannelBundles
                cmbPackages.Items.Add(Bundle)
            Next
        End If

        If cmbPackages.Items.Count > 0 Then cmbPackages.SelectedIndex = 0

    End Sub

    Public Sub PickedfromAvailable(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim target As Trinity.cPricelistTarget = sender.tag

        lstContents.Items.Add(target)
        DirectCast(Campaign.ChannelBundles(cmbPackages.SelectedIndex).value, List(Of Trinity.cPricelistTarget)).Add(target)
        lstAvailable.Items.Remove(target.Bookingtype)

    End Sub



    Private Sub lstAvailable_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstAvailable.MouseClick

        Dim targetMenu As New Windows.Forms.ContextMenuStrip

        Dim bookingType As Trinity.cBookingType = DirectCast(sender.selecteditem, Trinity.cBookingType)

        For Each target As Trinity.cPricelistTarget In bookingType.Pricelist.Targets
            Dim targetSubItem As New Windows.Forms.ToolStripMenuItem(target.TargetName)
            targetSubItem.Tag = target
            targetMenu.Items.Add(targetSubItem)
            AddHandler targetSubItem.Click, AddressOf PickedFromAvailable
        Next

        targetMenu.Show(MousePosition.X, MousePosition.Y)

    End Sub

    Private Sub lstContents_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstContents.MouseClick



    End Sub


    Private Sub lstAvailable_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstAvailable.MouseDoubleClick

        lstAvailable.Items.Add(sender.selecteditem)
        DirectCast(Campaign.ChannelBundles(cmbPackages.SelectedIndex).value, List(Of Trinity.cBookingType)).Remove(sender.selecteditem)
        lstContents.Items.Remove(sender.selecteditem)

        'lstContents.Items.Add(sender.selecteditem)
        'DirectCast(Campaign.ChannelBundles(cmbPackages.SelectedIndex).value, List(Of Trinity.cBookingType)).Add(sender.selecteditem)
        'lstAvailable.Items.Remove(sender.selecteditem)

    End Sub

    Private Sub lstContents_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstContents.MouseDoubleClick
        lstAvailable.Items.Add(sender.selecteditem)
        Campaign.ChannelBundles(cmbPackages.SelectedIndex).value.remove(sender.selecteditem)
        lstContents.Items.Remove(sender.selecteditem)

    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Campaign.ChannelBundles.Remove(cmbPackages.SelectedItem.key)
        cmbPackages.Items.RemoveAt(cmbPackages.SelectedIndex)
        cmbPackages.SelectedIndex = -1
    End Sub

    Public Sub SaveBundles(ByVal sender As Object, ByVal e As EventArgs)
        If sender.tag = "For me" Then
            Campaign.ChannelBundles.Save(False)
        Else
            Campaign.ChannelBundles.Save(True)
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Campaign.ChannelBundles.Save(False)

        'Dim ForMe As New System.Windows.Forms.ToolStripMenuItem("For me")
        'ForMe.Tag = "For me"
        'AddHandler ForMe.Click, AddressOf SaveBundles

        'Dim ForEveryOne As New System.Windows.Forms.ToolStripMenuItem("For everyone")
        'ForEveryOne.Tag = "For everyone"
        'AddHandler ForEveryOne.Click, AddressOf SaveBundles

        'Dim ForWho As New Windows.Forms.ContextMenuStrip
        'ForWho.Items.Add(ForMe)
        'ForWho.Items.Add(ForEveryOne)

        'ForWho.Show(MousePosition)

    End Sub


    Private Sub lstAvailable_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstAvailable.SelectedIndexChanged

    End Sub
End Class