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




Public Class frmPreviewConfirmation
    
    Dim _listOfRBSSpots As New List(Of TV4Online.SpotlightApiV23.xsd.ConfirmationRbsSpot)
    Dim _listOfSpecificSpots As New List(Of TV4Online.SpotlightApiV23.xsd.ConfirmationSpecificSpot)
    Dim _confirmation As TV4Online.SpotlightApiV23.xsd.Confirmation 
    Public Sub New(Confirmation As TV4Online.SpotlightApiV23.xsd.Confirmation)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _confirmation = Confirmation
        fillList()
        fillGrid()
        lblLastEdited.Text = Format(Confirmation.ChangedDateTime, "yyyy-MM-dd")
        lblVersionNm.Text = Confirmation.Versions.Count().ToString()
        lblSpottsText.Left = lblAmountOfSpots.Right
    End Sub
    
    
    Sub fillList()
        if _confirmation.Type = "RBS"            
            For each tmpSpot As TV4Online.SpotlightApiV23.xsd.ConfirmationRbsSpot In _confirmation.Versions.Max().RbsSpots
                _listOfRBSSpots.Add(tmpSpot)
            Next
        Else
        For each tmpSpot As TV4Online.SpotlightApiV23.xsd.ConfirmationSpecificSpot In _confirmation.Versions.Max().SpecificSpots
            _listOfSpecificSpots.Add(tmpSpot)
        Next
        End If
    End Sub
    Sub fillGrid()        
        If grdConfSpots.RowCount < 1
            grdConfSpots.Rows.Clear()
            If _confirmation.Type = "RBS"
                
                For each tmpSpot As TV4Online.SpotlightApiV23.xsd.ConfirmationRbsSpot In _listOfRBSSpots
                    Dim newRow As Integer = grdConfSpots.Rows.Add
                    grdConfSpots.Rows(newRow).Tag = tmpSpot
                Next
                Dim lastRow As Integer = grdConfSpots.Rows.Add
                grdConfSpots.Rows(lastRow).Tag = lastRow
                lblAmountOfSpots.Text  = _listOfRBSSpots.Count
            Else
                For each tmpSpot As TV4Online.SpotlightApiV23.xsd.ConfirmationSpecificSpot In _listOfSpecificSpots
                    Dim newRow As Integer = grdConfSpots.Rows.Add
                    grdConfSpots.Rows(newRow).Tag = tmpSpot
                Next
                Dim lastRow As Integer = grdConfSpots.Rows.Add
                grdConfSpots.Rows(lastRow).Tag = lastRow
                lblAmountOfSpots.Text  = _listOfSpecificSpots.Count

            End If
        End If
    End Sub

    Private Sub grdConfSpots_CellValueNeeded_1(sender As Object, e As DataGridViewCellValueEventArgs) Handles grdConfSpots.CellValueNeeded
        If _confirmation.Type = "Specific"
            If e.RowIndex < _listOfSpecificSpots.Count
            
                Dim x As TV4Online.SpotlightApiV23.xsd.ConfirmationSpecificSpot = _listOfSpecificSpots(e.RowIndex)
        
                Select Case e.ColumnIndex
                    Case colBroadcastDate.Index
                        e.Value = Format(x.BroadcastDate, "yyyy-MM-dd")
                    Case colProgramName.Index
                        e.Value = x.ProgramName
                    Case colBroadCastTime.Index
                        Dim _time As String
                        Dim _h As Integer = x.BroadcastTime \ 60
                        Dim _m As Integer = x.BroadcastTime Mod 60
                        _time = Format(_h, "00") & Format(_m, ":00")
                        '_time = IIf(_h < 10, "0" & _h, "0" & _h) & IIf(_m < 10, "0" & _m, _m)
                        e.Value = _time
                    Case colGrossPrice.Index
                        e.Value = x.GrossPrice
                    Case colNetPrice.Index
                        e.Value = x.NetPrice
                    Case colFilmCode.Index
                        e.Value = x.FilmCode
                    Case colFilmLength.Index
                        e.Value = x.FilmLength
                    Case colEstimatedTrp.Index
                        e.Value = x.EstimatedTRP
                    Case colWeek.Index
                        e.Value =  DatePart(DateInterval.WeekOfYear, x.BroadcastDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                    Case colSurcharges.Index
                        e.Value = x.Placement
                End Select
            Else            
                Select Case e.ColumnIndex                
                    Case colBroadcastDate.Index
                        e.Value = "Total row:"
                    Case colGrossPrice.Index
                        e.Value = _listOfSpecificSpots.Sum(Function(GPrice) GPrice.GrossPrice)
                    Case colNetPrice.Index
                        e.Value = _listOfSpecificSpots.Sum(Function(Nprice) Nprice.NetPrice)
                    Case colEstimatedTRP.Index
                        e.Value = _listOfSpecificSpots.Sum(Function(eTRP) eTRP.EstimatedTRP)
                End Select
            End If
        Else            
            Dim tmpList As new List(Of TV4Online.SpotlightApiV23.xsd.ConfirmationRbsSpot)            
            If e.RowIndex < _listOfRBSSpots.Count
            
                Dim x As TV4Online.SpotlightApiV23.xsd.ConfirmationRbsSpot = _listOfRBSSpots(e.RowIndex)
        
                Select Case e.ColumnIndex
                    Case colBroadcastDate.Index
                        e.Value = Format(x.BroadcastDate, "yyyy-MM-dd")
                    Case colProgramName.Index
                        e.Value = x.ProgramName
                    Case colBroadCastTime.Index
                        Dim _time As String
                        Dim _h As Integer = x.BroadcastTime \ 60
                        Dim _m As Integer = x.BroadcastTime Mod 60
                        _time = Format(_h, "00") & Format(_m, ":00")
                        '_time = IIf(_h < 10, "0" & _h, "0" & _h) & IIf(_m < 10, "0" & _m, _m)
                        e.Value = _time
                    Case colGrossPrice.Index

                    Case colNetPrice.Index

                    Case colFilmCode.Index
                        e.Value = x.FilmCode
                    Case colFilmLength.Index
                        e.Value = x.FilmLength
                    Case colEstimatedTrp.Index
                        e.Value = x.ActualTRP
                    Case colWeek.Index
                        e.Value =  DatePart(DateInterval.WeekOfYear, x.BroadcastDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)                        
                End Select
            Else            
                Select Case e.ColumnIndex                
                    Case colBroadcastDate.Index
                        e.Value = "Total row:"
                    Case colWeek.Index
                    Case colGrossPrice.Index
                        'e.Value = _listOfRBSSpots.Sum(Function(GPrice) GPrice.GrossPrice)
                    Case colNetPrice.Index
                        'e.Value = _listOfRBSSpots.Sum(Function(Nprice) Nprice.NetPrice)
                    Case colEstimatedTRP.Index
                        e.Value = _listOfRBSSpots.Sum(Function(eTRP) eTRP.ActualTRP)
                End Select
            End If
        End If
        
    End Sub

    Private Sub grdConfSpots_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles grdConfSpots.CellFormatting
        If _confirmation.Type = "RBS"
            If e.RowIndex = _listOfRBSSpots.Count 
                e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Bold)
            End If
        Else
            If e.RowIndex = _listOfSpecificSpots.Count 
                e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Bold)
            End If
        End If
    End Sub

    Private Sub lblVersionNm_Click(sender As Object, e As EventArgs) Handles lblVersionNm.Click

    End Sub
End Class