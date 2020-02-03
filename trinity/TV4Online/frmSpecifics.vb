Imports System.ServiceModel
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing

Public Class frmSpecifics

    Private _specifics As IEnumerable(Of TV4Online.SpotlightApiV23.xsd.SpecificSpot)
    Private _client As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV5Client(New WSHttpBinding(SecurityMode.Transport), New EndpointAddress(TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("TV4Spotlight_V5", "Endpoint")))
    Private _surcharges As New List(Of String)
    Private _tv4Indices As New List(Of String)
    Dim _releases As TV4Online.SpotlightApiV23.xsd.ReleasePeriod
    Dim _release as TV4Online.SpotlightApiV23.xsd.ReleasePeriod
    Private _skip As List(Of TV4Online.SpotlightApiV23.xsd.SpecificSpot)

    Sub New(ByRef Specifics As IEnumerable(Of TV4Online.SpotlightApiV23.xsd.SpecificSpot), Skip As List(Of TV4Online.SpotlightApiV23.xsd.SpecificSpot))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _specifics = Specifics
        _surcharges = _client.GetSurchargeNames.ToList
        _tv4Indices = _client.GetIndexNames.ToList
        _skip = Skip

        If Specifics IsNot Nothing And Specifics.Count > 0 Then
            grdSpecifics.Rows.Add(Specifics.Count)
        End If

        Dim summaryRow As Integer = grdSpecifics.Rows.Add
        grdSpecifics.Rows(summaryRow).Tag = summaryRow

        grdSpecifics.SelectColumn = colSelected
        
        _releases = _client.GetReleasePeriods(True).First
        _release = _releases
        lblCurrentRelease.Text = _release.Name

    End Sub

    Private Sub grdSpecifics_CellContentDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSpecifics.CellContentDoubleClick
        If e.ColumnIndex = colSurcharges.Index AndAlso grdSpecifics.Rows(e.RowIndex).Cells(e.ColumnIndex).ErrorText <> "" Then
            Dim _values As New List(Of String)
            For Each _spec In _specifics
                For Each _sc In _spec.Surcharges
                    If Not _surcharges.Select(Function(_s) _s.ToLower).Contains(_sc.Name.Replace(" ", "").ToLower) AndAlso Not _values.Contains(_sc.Name) Then
                        _values.Add(_sc.Name)
                    End If
                Next
            Next
            Dim _frm = New frmTranslate(_values, _surcharges)
            _frm.ShowDialog()
            For Each _spec In _specifics
                For Each _sc In _spec.Surcharges
                    If Not _surcharges.Select(Function(_s) _s.ToLower).Contains(_sc.Name.Replace(" ", "").ToLower) Then
                        If TV4OnlinePlugin.InternalApplication.GetUserPreference("TV4Spotlight", _sc.Name) <> "" Then
                            _sc.Name = TV4OnlinePlugin.InternalApplication.GetUserPreference("TV4Spotlight", _sc.Name)
                        End If
                    End If
                Next
            Next
            grdSpecifics.Invalidate()
        End If

    End Sub

    Private Sub grdSpecifics_CellErrorTextNeeded(sender As Object, e As System.Windows.Forms.DataGridViewCellErrorTextNeededEventArgs) Handles grdSpecifics.CellErrorTextNeeded
        Dim _spec As TV4Online.SpotlightApiV23.xsd.SpecificSpot = _specifics(e.RowIndex)
        If e.ColumnIndex = colSurcharges.Index And _spec IsNot Nothing Then
            Dim _sb As New System.Text.StringBuilder
            For Each _sc In _spec.Surcharges
                If Not _surcharges.Select(Function(_s) _s.ToLower).Contains(_sc.Name.Replace(" ", "").ToLower) Then
                    _sb.AppendLine(_sc.Name & " is not a valid Spotlight surcharge")
                End If
            Next
            e.ErrorText = _sb.ToString
        End If
    End Sub

    Private Sub grdSpecifics_CellValueNeeded(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSpecifics.CellValueNeeded
        Dim _spec As TV4Online.SpotlightApiV23.xsd.SpecificSpot = _specifics(e.RowIndex)
        If e.RowIndex < _specifics.Count
            Select Case e.ColumnIndex
                Case colSelected.Index
                    e.Value = Not _skip.Contains(_spec)
                Case colDate.Index
                    e.Value = _spec.BroadcastDate
                Case colTime.Index
                    Dim tmpBTime = clTrinity.Trinity.Helper.Mam2Tid(_spec.BroadcastTime)
                    e.Value = tmpBTime
                Case colProg.Index
                    e.Value = _spec.ProgramName
                Case colRating.Index
                    e.Value = _spec.EstimatedTRP
                Case colDiscount.Index
                    e.Value = _spec.NegotiatedDiscount
                Case colNetPrice.Index
                    e.Value = _spec.NetPrice
                Case colFilmcode.Index
                    e.Value = _spec.FilmCode
                Case colFilmLength.Index
                    e.Value = _spec.FilmLength
                Case colSurcharges.Index
                    e.Value = String.Join(",", _spec.Surcharges.Select(Function(s) s.Name).ToArray)
            End Select
        Else
            Select Case e.ColumnIndex            
                Case colSelected.Index
                    colSelected.ReadOnly = True
                    e.Value = False        
                Case colDate.Index
                    e.Value = "Total row:"
                Case colRating.Index
                    e.Value = _specifics.Sum(Function(TRP) TRP.EstimatedTRP)
                Case colNetPrice.Index
                    e.Value = _specifics.Sum(Function(NetPrice) NetPrice.NetPrice)
            End Select
        End If

    End Sub

    Private Sub grdSpecifics_CellValuePushed(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSpecifics.CellValuePushed
       
    End Sub

    Public ReadOnly Property SkipList As List(Of TV4Online.SpotlightApiV23.xsd.SpecificSpot)
        Get
            Return _skip
        End Get
    End Property

    Private Sub frmSpecifics_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        grdSpecifics.CommitEdit(Windows.Forms.DataGridViewDataErrorContexts.Commit)
        _client.Close()
    End Sub

    Private Sub grdSpecifics_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSpecifics.CellContentClick
        Dim _spec As TV4Online.SpotlightApiV23.xsd.SpecificSpot = _specifics(e.RowIndex)
        If e.RowIndex < _specifics.Count
            Select Case e.ColumnIndex
                Case colSelected.Index
                    If _skip.Contains(_spec) Then
                        _skip.Remove(_spec)
                    Else
                        _skip.Add(_spec)
                    End If
                    grdSpecifics.Invalidate()
            End Select
        Else
            Select e.ColumnIndex
            End Select
        End if
    End Sub

    'Private Sub grdSpecifics_ColumnSumCalculate(sender As Object, ByRef e As SummaryDataGridView.SummarizeColumnEventArgs) Handles grdSpecifics.ColumnSumCalculate
    '    Select Case e.Column.Index
    '        Case colNetPrice.Index, colRating.Index
    '            e.SumFunction = SummaryDataGridView.SummarizeColumnEventArgs.SumFunctionEnum.Sum
    '        Case Else
    '            e.DoNotSum = True
    '    End Select
    'End Sub

    Private Sub grdSpecifics_CellMouseUp(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdSpecifics.CellMouseUp
        
    End Sub

    Private Sub grdSpecifics_CellErrorTextChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSpecifics.CellErrorTextChanged

    End Sub

    Private Sub cmdAutoHide_Click(sender As System.Object, e As System.EventArgs) Handles cmdAutoHide.Click

        If Not _specifics Is Nothing Then
            '_skip.AddRange(_specifics.Where(Function(_s) Not _skip.Contains(_s) AndAlso _releases.Where(Function(r) r.BroadcastStartDate <= _s.BroadcastDate AndAlso r.BroadcastEndDate >= _s.BroadcastDate).Count = 0))
            '_skip.AddRange(_specifics.Where(Function(_s) Not _skip.Contains(_s) AndAlso _releases.Where(Function(r) r.BroadcastStartDate <= _s.BroadcastDate AndAlso r.BroadcastEndDate >= _s.BroadcastDate).Count = 0))
            'Adds spots to _skip-list if the broadcast date is higher than the periods enddate. 
            _skip.AddRange(_specifics.Where(Function(_s) Not _skip.Contains(_s) AndAlso _release.BroadcastStartDate >= _s.BroadcastDate AndAlso _s.BroadcastDate <= _release.BroadcastEndDate))
        End If
        grdSpecifics.Invalidate()
        grdSpecifics.CurrentCell = Nothing
    End Sub

    Private Sub grdSpecifics_CellFormatting(sender As Object, e As Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdSpecifics.CellFormatting
        If e.RowIndex = _specifics.Count
            e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Bold)
        End If
    End Sub
   
End Class