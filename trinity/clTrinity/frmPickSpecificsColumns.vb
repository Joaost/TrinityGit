Imports System.Windows.Forms

Public Class frmPickSpecificsColumns

    Dim _specialColumns As System.Windows.Forms.ListBox

    Public Property specificsColumns() As System.Windows.Forms.ListBox
        Get
            Return ListBox2
        End Get
        Set(ByVal Value As System.Windows.Forms.ListBox)
            _specialColumns = Value
        End Set
    End Property

    'We need to populate the right side, Listbox2, with the columns that are currently chosen in the bookings window, or 
    'would have been chosen (in case the user has not opened a channel for booking at the moment
    Private Sub frmPickSpecificsColumns_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ListBox1.Items.Clear()
        ListBox2.Items.Clear()

        'Populate available columns

        Dim Columns() As String = {"Date", "Time", "Channel", "Program", "Week", "Weekday", "Gross Price", _
                                   "Net Price", "Filmcode", "Film", "Film dscr", "Position", "Est", "Est (Buying)", _
                                   "Chan Est", "Gross CPP", "CPP (Main)", "CPP 30"" (Main)", "CPP (Chn Est)", _
                                   "Quality", "Remarks", "Notes", "Added value", "Bid"}

        For Each Column As String In Columns
            ListBox1.Items.Add(Column)
        Next

        'Populate picked columns
        For i As Integer = 2 To frmBooking.grdSpotlist.Columns.Count - 1
            ListBox2.Items.Add(frmBooking.grdSpotlist.Columns(i).HeaderCell.Value)
            ListBox1.Items.Remove(frmBooking.grdSpotlist.Columns(i).HeaderCell.Value)
        Next



    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim Column As Object = ListBox2.SelectedItem
        If Not Column Is Nothing Then
            Dim Index As Integer = ListBox2.Items.IndexOf(Column)
            If Not Index = 0 Then
                ListBox2.Items.RemoveAt(Index)
                Index -= 1
                ListBox2.Items.Insert(Index, Column)
                ListBox2.SelectedIndex = Index
            End If
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim Column As Object = ListBox2.SelectedItem
        If Not Column Is Nothing Then
            Dim Index As Integer = ListBox2.Items.IndexOf(Column)
            If Index < ListBox2.Items.Count - 1 Then
                ListBox2.Items.RemoveAt(Index)
                Index += 1
                ListBox2.Items.Insert(Index, Column)
                ListBox2.SelectedIndex = Index
            End If
        End If
    End Sub

    'Moves a column from available to picked, increasing the column count and adding the column name to trinity.ini
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        'Add to picked then remove from unpicked
        ListBox2.Items.Add(ListBox1.SelectedItem)
        ListBox1.Items.Remove(ListBox1.SelectedItem)

        TrinitySettings.PrintBookingColumnCount += 1

        'Add this column to trinity.ini
        TrinitySettings.PrintBookingColumn(ListBox2.Items.Count) = ListBox2.Items(ListBox2.Items.Count - 1)

    End Sub

    'Moves a column from picked to available, decreasing the column count and removing the column name from trinity.ini
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        'Add to unpicked then remove from picked
        ListBox1.Items.Add(ListBox2.SelectedItem)

        TrinitySettings.PrintBookingColumn(ListBox2.SelectedIndex) = 0

        ListBox2.Items.Remove(ListBox2.SelectedItem)

        TrinitySettings.PrintBookingColumnCount -= 1

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        ' Dim specialFormatBooking As New frmPrintBooking(True, False, ListBox2)
        ' specialFormatBooking.ShowDialog()
        Me.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub
End Class