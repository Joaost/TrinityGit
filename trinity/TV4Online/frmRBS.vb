Imports System.ServiceModel
Imports System.Drawing

Public Class frmRBS

    Private _entries As New List(Of Entry)
        Public _releases as Object
        Public _release as TV4Online.SpotlightApiV23.xsd.ReleasePeriod
        Public _rbsPeriods As IEnumerable(Of TV4Online.SpotlightApiV23.xsd.RbsPeriod)
    'Private _client As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV5Client(New WSHttpBinding(SecurityMode.Transport), New EndpointAddress(TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("TV4Spotlight_beta", "Endpoint")))
    Private _client As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV5Client(DirectCast(IIf(TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("TV4Spotlight_V5", "Endpoint").StartsWith("https"), New WSHttpBinding(SecurityMode.Transport), New BasicHttpBinding), System.ServiceModel.Channels.Binding), New EndpointAddress(TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("TV4Spotlight_V5", "Endpoint")))
    Private Class Entry
        Public StartDate As Date
        Public EndDate As Date
        Public Daypart As String
        Public Filmcode As String
        Public Length As Integer
        Public CPP30 As Single
        Public Discount As Single
        Public NetPrice As Single
        Public TRP As Single
        Public Indices As IEnumerable(Of String)
        Public RbsFilm As TV4Online.SpotlightApiV23.xsd.RbsFilm
        Public Selected As Boolean
        Public WeekName As String
    End Class

    Sub New(RbsPeriods As IEnumerable(Of TV4Online.SpotlightApiV23.xsd.RbsPeriod), Skip As List(Of TV4Online.SpotlightApiV23.xsd.RbsFilm), channel As String)

        ' This call is required by the designer.
        InitializeComponent()
        _rbsPeriods = RbsPeriods
        ' Add any initialization after the InitializeComponent() call.
        If RbsPeriods.Count > 0 Then
            For Each _period In RbsPeriods
                For Each _daypart In _period.RbsDayparts
                    For Each _film In _daypart.RbsFilms
                        Dim _entry As New Entry
                        _entry.StartDate = _period.StartDate
                        _entry.EndDate = _period.EndDate
                        _entry.Daypart = _daypart.Name
                        _entry.Filmcode = _film.FilmCode
                        _entry.Length = _film.FilmLength
                        _entry.CPP30 = _daypart.GrossCPP30 * (1 - _daypart.NegotiatedDiscount)
                        _entry.Discount = _daypart.NegotiatedDiscount
                        _entry.NetPrice = _film.NetBudget
                        _entry.TRP = _film.TRP
                        _entry.Indices = _film.Indices.Select(Function(i) i.Name)
                        _entry.RbsFilm = _film
                        _entry.Selected = Not Skip.Contains(_film)
                        _entry.WeekName = DatePart(DateInterval.WeekOfYear, _period.StartDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                        _entries.Add(_entry)
                    Next
                Next
            Next
            grdRBS.Rows.Add(_entries.Count)
            Dim summaryRow As Integer = grdRBS.Rows.Add
            grdRBS.Rows(summaryRow).Tag = summaryRow

            grdRBS.SelectColumn = colSelected
            
            _releases = _client.GetReleasePeriods(True).First()
            _release = _releases

            lblCurrentRelease.Text = _release.Name
        End If
    End Sub

    Private Sub grdRBS_CellMouseUp(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdRBS.CellMouseUp
        If e.ColumnIndex = colSelected.Index Then
            grdRBS.CommitEdit(Windows.Forms.DataGridViewDataErrorContexts.Commit)
            grdRBS.Invalidate()
        End If
    End Sub

    Private Sub grdRBS_CellValueNeeded(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdRBS.CellValueNeeded

        If e.RowIndex < _entries.Count
            Dim _entry As Entry = _entries(e.RowIndex)
            
            Select Case e.ColumnIndex
                Case colSelected.Index
                    e.Value = _entry.Selected
                    If colSelected.ReadOnly
                        colSelected.ReadOnly = False
                    End If
                Case colWeek.Index
                    e.Value = _entry.WeekName
                Case colStart.Index
                    e.Value = _entry.StartDate
                    if colStart.ReadOnly
                        colStart.ReadOnly = False
                    End If
                Case colEnd.Index
                    e.Value = _entry.EndDate
                    If colEnd.ReadOnly
                        colEnd.ReadOnly = False
                    End If
                Case colDaypart.Index
                    e.Value = _entry.Daypart
                Case colFilmcode.Index
                    e.Value = _entry.Filmcode
                Case colLength.Index
                    e.Value = _entry.Length
                Case colCPP.Index
                    e.Value = _entry.CPP30
                Case colDiscount.Index
                    e.Value = _entry.Discount
                Case colIndices.Index
                    e.Value = String.Join(",", _entry.Indices.ToArray)
                Case colTRP.Index
                    e.Value = _entry.TRP
                Case colNetPrice.Index
                    e.Value = _entry.NetPrice
            End Select
        Else
            Select Case e.ColumnIndex            
                Case colSelected.Index
                    e.Value = False
                Case colStart.Index
                    e.Value = "Total row:"
                Case colTRP.Index
                    e.Value = _entries.Sum(Function(_row) _row.TRP)
                Case colNetPrice.Index
                    e.Value = _entries.Sum(Function(_row) _row.NetPrice)
            End Select
        End If
    End Sub

    Private Sub grdRBS_CellValuePushed(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdRBS.CellValuePushed
        If e.RowIndex < _entries.Count
            Dim _entry As Entry = _entries(e.RowIndex)
            Select Case e.ColumnIndex
                Case colSelected.Index
                    _entry.Selected = e.Value            
                'Solution for Adam Larsson at Mindshare. He had to change the end date for some of the bookings. Trinity does not support periods with the same start.
                Case colStart.Index                
                    Dim d As New DateTime(e.Value)
                    _entry.StartDate = d                
                Case colEnd.Index
                    Dim d As Date = Convert.ToDateTime(e.Value)
                    _entry.EndDate = d
            End Select
        Else
            Select case e.ColumnIndex
                Case colSelected.index
                    e.Value = False
            end Select
        End If
    End Sub

    Public ReadOnly Property SkipList As List(Of TV4Online.SpotlightApiV23.xsd.RbsFilm)
        Get
            Return _entries.Where(Function(e) Not e.Selected).Select(Function(e) e.RbsFilm).ToList
        End Get
    End Property

    Private Sub grdRBS_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRBS.CellContentClick
        If e.RowIndex < _entries.Count
            Dim _entry As Entry = _entries(e.RowIndex)
            Dim es = _entries
        Else
            Select Case e.ColumnIndex
                Case colSelected.Index
                   colSelected.ReadOnly = True
                Case colStart.index
                    colStart.ReadOnly = True
            end Select

        End If
    End Sub

    Private Sub frmRBS_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        grdRBS.CommitEdit(Windows.Forms.DataGridViewDataErrorContexts.Commit)
        _client.Close()
    End Sub

    'Private Sub grdRBS_ColumnSumCalculate(sender As Object, ByRef e As SummaryDataGridView.SummarizeColumnEventArgs) Handles grdRBS.ColumnSumCalculate
    '    Select Case e.Column.Index
    '        Case colNetPrice.Index, colTRP.Index
    '            e.SumFunction = SummaryDataGridView.SummarizeColumnEventArgs.SumFunctionEnum.Sum
    '        Case Else
    '            e.DoNotSum = True
    '    End Select
    'End Sub

    Private Sub cmdAutoHide_Click(sender As System.Object, e As System.EventArgs) Handles cmdAutoHide.Click
        'Eliminate first the periods that are lower than the period
        Dim _tmpRelease = _client.GetReleasePeriods(True).Take(1)
        For Each _entry As Entry In _entries.Where(Function(_e) _e.Selected AndAlso _tmpRelease.Where(Function(r) _e.StartDate <= r.BroadcastEndDate AndAlso _e.EndDate <= r.BroadcastStartDate).Count > 0)
            _entry.Selected = False
        Next
        'Then eliminate the periods that are higher than the period
        For Each _entry As Entry In _entries.Where(Function(_e) _e.Selected AndAlso _tmpRelease.Where(Function(r) _e.StartDate >= r.BroadcastEndDate AndAlso _e.EndDate >= r.BroadcastStartDate).Count > 0)
            _entry.Selected = False
        Next
        grdRBS.Invalidate()
        grdRBS.CurrentCell = Nothing
    End Sub

    Private Sub grdRBS_CellFormatting(sender As Object, e As Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdRBS.CellFormatting        
        If e.RowIndex + 1 > _entries.Count
            e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Bold)
        End If
    End Sub
End Class