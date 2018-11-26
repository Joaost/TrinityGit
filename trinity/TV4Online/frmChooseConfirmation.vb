Imports System.ComponentModel.Composition
Imports System.ComponentModel.Composition.Hosting
Imports System.Reflection
Imports TrinityPlugin
Imports System.ServiceModel
Imports System.Xml.Serialization
Imports System.Drawing
Imports System.Collections
Imports System.Diagnostics
Imports System.Windows.Forms

Public Class frmChooseConfirmation

    Dim _list As List(Of TV4Online.SpotlightApiV23.xsd.Confirmation)

    Public sub New(listOfConfirmations As List(Of TV4Online.SpotlightApiV23.xsd.Confirmation))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim sortedList = From s In listOfConfirmations
                         Order by s.ChangedDateTime Descending
        'For each c In sortedList.ToList
        '    _list.Add(c)
        'Next


        _list = sortedList.ToList()

        '_list = listOfConfirmations
        fillGrid()
    End Sub

    Private Sub grdConfirmations_CellValueNeeded(sender As Object, e As DataGridViewCellValueEventArgs) Handles grdConfirmations.CellValueNeeded
        Dim x As TV4Online.SpotlightApiV23.xsd.Confirmation = _list(e.RowIndex)
        Select Case e.ColumnIndex
            Case colName.Index
                e.Value = x.Channel & " " & x.Type & " " & x.Versions().Max().Name
            Case colStartDate.Index
                e.Value = Format(x.CampaignStartDate, "yyyy-MM-dd")
            Case colEndDate.Index
                e.Value = Format(x.CampaignEndDate, "yyyy-MM-dd")
            Case colChangedDate.Index
                e.Value = x.ChangedDateTime
            Case colBookedBudget.Index
                e.Value = x.BookedBudget
            Case colBookingType.Index
                e.Value = x.Type
            Case colVersionNr.Index
                e.Value = x.Versions.Count().ToString()
        End Select
    End Sub
    Sub fillGrid()
        Dim dates  = {Date.Now, Date.Now.AddDays(1), Date.Now.AddDays(2)}

        If grdConfirmations.RowCount < 1
            grdConfirmations.Rows.Clear()
            For each tmpConf As TV4Online.SpotlightApiV23.xsd.Confirmation in _list
                Dim newRow As Integer = grdConfirmations.Rows.Add
                grdConfirmations.Rows(newRow).Tag = tmpConf
            Next
        End If
        Dim sortColumn  As DataGridViewColumn = colChangedDate



        'grdConfirmations.VirtualMode = False
        'grdConfirmations.Sort(sortColumn, SortOrder.Ascending)
    End Sub

    Private Sub grdConfirmations_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdConfirmations.CellDoubleClick
    End Sub

    Private Sub grdConfirmations_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdConfirmations.CellContentClick
        
        
        Dim x As TV4Online.SpotlightApiV23.xsd.Confirmation = _list(e.RowIndex)
        Select Case e.ColumnIndex
            Case colName.Index                
            Dim frmConfPreview As new frmPreviewConfirmation(x)
            frmConfPreview.ShowDialog()
        End Select

    End Sub
End Class