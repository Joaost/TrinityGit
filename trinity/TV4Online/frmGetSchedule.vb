Imports System.ServiceModel
Imports System.Windows.Forms

Public Class frmGetSchedule

    Dim listOfScheduleBreaks As new List(Of TV4Online.SpotlightApiV23.xsd.SpotPrice)
    Dim importList As New List(Of Break)
    'Private _client As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV3Client(DirectCast(IIf(TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("TV4Spotlight", "Endpoint").StartsWith("https"), New WSHttpBinding(SecurityMode.Transport), New BasicHttpBinding), System.ServiceModel.Channels.Binding), New EndpointAddress(TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("TV4Spotlight", "Endpoint")))
    Dim res As New TV4OnlinePlugin
    Dim _user As String = ""
    Dim _DB As String = ""
    Dim _connection As String = ""

    Dim fromDate As Date
    Dim toDate As Date
    Dim channel As String = ""
    Dim amountOfbreaksImported As Integer = 0
    
    Private Shared TrinityIni As New TV4Online.Trinity.clsIni
    Public sub New

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        fillGrid()
        fillCmbChannelPick()
        
        Dim tmpUser = TV4OnlinePlugin.InternalApplication.GetUserPreference("ID", "UserEmail")
        Dim tmpDB = TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("Shared", "Name")
        Dim tmpConnection = TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("Messages", "ConnectionString")
        
        If tmpUser <> ""
            _user = tmpUser
        End If
        If tmpDB <> ""
            _DB = tmpDB
        End If
        If tmpConnection <> ""
            _connection = tmpConnection
        End If
    End Sub
    Sub fillGrid()
        If listOfScheduleBreaks.Count > 1
            grdScheduleList.Rows.Clear()
            For each tmpSpot In listOfScheduleBreaks
                Dim newRow As Integer = grdScheduleList.Rows.Add
                grdScheduleList.Rows(newRow).Tag = tmpSpot
            Next
        End If
        lblAmountOfScheduleSpots.Text = listOfScheduleBreaks.Count & " breaks."
    End Sub
    Sub fillCmbChannelPick
        
        Dim channels As String() = res.getChannels()

        For each item In channels
            cmbChannelPick.Items.Add(item.ToString())
        Next
        cmbChannelPick.SelectedIndex = 0
    End Sub

    Private Sub frmGetSchedule_Load(sender As Object, e As EventArgs)
        
    End Sub
    Private Sub dtPickerTo_ValueChanged(sender As Object, e As EventArgs) Handles dtPickerTo.ValueChanged
        toDate = dtPickerTo.Value
    End Sub

    Private Sub dtPickerFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtPickerFrom.ValueChanged
        fromDate = dtPickerFrom.Value
    End Sub

    Private Sub cmdGetSchedule_Click_1(sender As Object, e As EventArgs) Handles cmdGetSchedule.Click      
        Dim _b As Integer = 0
        fromDate = dtPickerFrom.Value
        toDate = dtPickerTo.Value
        Dim retVal As TV4Online.SpotlightApiV23.xsd.PricesAndScheduleResponse = res.checkAvailableSchedule(fromDate, toDate, cmbChannelPick.Text)

        Try
            If retVal.SpotPrices.Count > 1
                listOfScheduleBreaks.Clear()
                For each tmpSpotPrice In retVal.SpotPrices
                    listOfScheduleBreaks.Add(tmpSpotPrice)
                    _b += 1
                    Progress((_b / retVal.SpotPrices.Count) * 100, "Reading spots from Spotlight")
                Next
                channel = cmbChannelPick.SelectedItem
                fillGrid()
                btnImportSchedule.Enabled = True
            else
                btnImportSchedule.Enabled = False
                listOfScheduleBreaks.Clear()
                grdScheduleList.Rows.Clear()
                lblAmountOfScheduleSpots.Text = "0 breaks"
            End If
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try        
        cleanUpProgressBar()
    End Sub

    Private Sub grdScheduleList_CellValueNeeded(sender As Object, e As DataGridViewCellValueEventArgs) Handles grdScheduleList.CellValueNeeded
        If listOfScheduleBreaks.Count < 1 Then Exit Sub Else
        Dim x As TV4Online.SpotlightApiV23.xsd.SpotPrice = listOfScheduleBreaks(e.RowIndex)
        If x Is Nothing Then Exit Sub

        Select Case e.ColumnIndex
            Case colBDate.Index
                e.Value = Format(x.BroadcastDate, "yyyy-MM-dd")
            Case colChannel.Index
                e.Value = channel
            Case colTime.Index
                Dim _time As String
                Dim _h As Integer = x.BroadcastTime \ 60
                Dim _m As Integer = x.BroadcastTime Mod 60
                _time = Format(_h, "00") & Format(_m, ":00")
                e.Value = _time
            Case coLProgram.Index
                e.Value = x.ProgramName
            Case colGrossPrice.Index
                e.Value = x.GrossCPP
            Case colEstTrp.Index
                e.Value = x.EstimatedTRP
        End Select
    End Sub
    Function convertSpots()        
        Try
            If listOfScheduleBreaks.Count > 1
        
                Dim tmpStep As Decimal = listOfScheduleBreaks.Count / 100       
                prgBar1.Step = tmpStep
            
                Dim _prevBreak As Break = Nothing
                Dim spotCount As long = 0

                importList.Clear()
                
                For each tmpScheduleSpot As TV4Online.SpotlightApiV23.xsd.SpotPrice in listOfScheduleBreaks
                    Dim newBreak As New Break(_prevBreak)
                    Dim test = tmpScheduleSpot.BroadcastDate.Date.ToOADate
                    newBreak.Channel = channel
                    newBreak.Date = tmpScheduleSpot.BroadcastDate
                    newBreak.SetMaM(tmpScheduleSpot.BroadcastTime)
                    newBreak.Programme = tmpScheduleSpot.ProgramName
                    newBreak.Price = tmpScheduleSpot.GrossPrice30
                    'newBreak.UseCPP = True
                    newBreak.ChanEst = tmpScheduleSpot.EstimatedTRP
                    newBreak.IsLocal = tmpScheduleSpot.LocalTvAvailable

                    _prevBreak = newBreak
                    importList.Add(newBreak)
                    spotCount += 1
                
                    Progress((spotCount / listOfScheduleBreaks.Count) * 100, "Converting spots from Spotlight to Trinity")
                Next
            End If
            cleanUpProgressBar()        
            Return true
        Catch ex As Exception            
            Windows.Forms.MessageBox.Show(ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try
    End Function
    Function writeBreaksToDB(Breaks As List(Of Break))
        Using _conn As New SqlClient.SqlConnection(_connection & "Integrated Security=True;"&";")
            _conn.Open()

            Using _tran As SqlClient.SqlTransaction = _conn.BeginTransaction()
                Try
                    For Each _channel As String In (From _break As Break In Breaks Select _break.Channel).Distinct
                        Dim _tmpChannel = _channel
                        Dim _dates = From _break As Break In Breaks Where _break.Channel = _tmpChannel Select _break.Date
                        Dim _from As Long = _dates.Min.ToOADate
                        Dim _to As Long = _dates.Max.ToOADate
                        Using _com As New SqlClient.SqlCommand("DELETE FROM events WHERE channel=@channel AND date>=@from AND date<=@to", _conn)
                            _com.Transaction = _tran
                            _com.Parameters.AddWithValue("channel", _channel)
                            _com.Parameters.AddWithValue("to", _to)
                            _com.Parameters.AddWithValue("from", _from)
                            _com.ExecuteNonQuery()
                        End Using
                    Next
                    Dim _b As Long = 0
                    For Each _break In Breaks
                        Using _com As New SqlClient.SqlCommand("INSERT INTO events ([ID],[Channel],[Date],[Time],[StartMam],[Duration],[Name],[ChanEst],[Price],[IsLocal],[EstimationPeriod],[Area],[Comment],[Type],[UseCPP],[EstimationTarget],[Addition]) VALUES (@ID,@Channel,@Date,@Time,@StartMam,@Duration,@Name," & _break.ChanEst.ToString.Replace(",", ".") & ",@Price,@IsLocal,@EstimationPeriod,@Area,@Comment,0,@UseCPP,@EstTarget,@Addition)", _conn)
                            _com.Transaction = _tran
                            _com.Parameters.AddWithValue("ID", _break.ID)
                            _com.Parameters.AddWithValue("Channel", _break.Channel)
                            _com.Parameters.AddWithValue("Date", _break.Date.ToOADate)
                            _com.Parameters.AddWithValue("Time", _break.Time)
                            _com.Parameters.AddWithValue("StartMam", _break.MaM)
                            _com.Parameters.AddWithValue("Duration", _break.Duration)
                            _com.Parameters.AddWithValue("Name", _break.Programme.Substring(0, IIf(_break.Programme.Length > 60, 60, _break.Programme.Length)))
                            _com.Parameters.AddWithValue("Price", _break.Price)
                            _com.Parameters.AddWithValue("IsLocal", _break.IsLocal)
                            _com.Parameters.AddWithValue("EstimationPeriod", "-4fw")
                            _com.Parameters.AddWithValue("Area", IIf(String.IsNullOrEmpty(_break.Area), "SE", _break.Area))
                            _com.Parameters.AddWithValue("UseCPP", _break.UseCPP)
                            _com.Parameters.AddWithValue("EstTarget", IIf(String.IsNullOrEmpty(_break.EstimationTarget), "", _break.EstimationTarget))
                            _com.Parameters.AddWithValue("Addition", _break.Addition)
                            _com.Parameters.AddWithValue("Comment", "")
                            _com.ExecuteNonQuery()
                            _b += 1
                            amountOfbreaksImported = _b
                            Progress((_b / listOfScheduleBreaks.Count) * 100, "Writing spots to " & _db)
                        End Using
                    Next
                    _tran.Commit()
                Catch ex As Exception
                    _tran.Rollback()
                    _conn.Close()
                    Windows.Forms.MessageBox.Show(ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                End Try
            End Using
            cleanUpProgressBar()
            _conn.Close()
            Return  True
        End Using
    End Function
    Sub cleanUpProgressBar()
        lblProgressText.Text = ""
        lblProgressText.Visible = False
        prgBar1.Value = 0
        prgBar1.Visible = False
    End Sub
    Private Sub dtPickerTo_Enter(sender As Object, e As EventArgs) Handles dtPickerTo.Enter
        dtPickerTo.MinDate = dtPickerFrom.Value
    End Sub
    Sub Progress(p As Decimal, Message As String)
        prgBar1.Visible = True
        lblProgressText.Visible = true
        prgBar1.Value = p
        lblProgressText.Text = Message
        Application.DoEvents()
    End Sub
    Private Sub btnImportSchedule_Click(sender As Object, e As EventArgs) Handles btnImportSchedule.Click
        
        If Windows.Forms.MessageBox.Show("Are you sure you will import " & channel & " schedules between " & format(dtPickerFrom.Value, "yyyy-MM-dd") & " to " & Format(dtPickerTo.Value, "yyyy-MM-dd") & "?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Information) = DialogResult.Yes            
            If convertSpots 
                If writeBreaksToDB(importList)                
                    Windows.Forms.MessageBox.Show("Successfully imported " & amountOfbreaksImported & ".", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                End If
            End If
        End If

    End Sub
End Class