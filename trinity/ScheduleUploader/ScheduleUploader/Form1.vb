Public Class frmImport
    
    Private Shared _internalApplication As Trinity.clsIni

        
    Private Sub cmdAddServer_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddServer.Click
        lstServers.Items.Add(txtServer.Text)
    End Sub

    Private Sub cmdRemoveServer_Click(sender As System.Object, e As System.EventArgs) Handles cmdRemoveServer.Click
        lstServers.Items.RemoveAt(lstServers.SelectedIndex)
    End Sub

    Private Sub cmdBrowse_Click(sender As System.Object, e As System.EventArgs) Handles cmdBrowse.Click
        Dim _dlg As New Windows.Forms.OpenFileDialog()
        _dlg.Title = "Open schedule file"
        _dlg.Filter = "All readable files|*.txt;*.xml|Text files|*.txt|XML-files|*.xml"

        If _dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtFile.Text = _dlg.FileName
        End If
    End Sub

    Private Sub cmdAddFile_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddFile.Click
        lstFiles.Items.Add(txtFile.Text)
    End Sub

    Private Sub cmdRemoveFile_Click(sender As System.Object, e As System.EventArgs) Handles cmdRemoveFile.Click
        Dim _removeList As New List(Of String)
        For Each _item In lstFiles.SelectedItems
            _removeList.Add(_item)
        Next
        For Each _item In _removeList
            lstFiles.Items.Remove(_item)
        Next
    End Sub

    Private Sub cmdImport_Click(sender As System.Object, e As System.EventArgs) Handles cmdImport.Click
        pbImport.Visible = True
        AddHandler XmlReader.Progress, AddressOf Progress
        AddHandler Writer.Progress, AddressOf Progress
        Dim _breaks As New List(Of Break)
        For Each _item As String In lstFiles.Items
            If _item.EndsWith(".xml") Or _item.StartsWith("http") Then
                _breaks.AddRange(XmlReader.Read(_item))
            Else
                _breaks.AddRange(TextReader.Read(_item))
            End If
        Next
        For Each _item As String In lstServers.Items
            Dim _data As String() = _item.Split("/")
            Try
                Writer.Write(_data(0), _data(1), _breaks)
            Catch ex As Exception
                Windows.Forms.MessageBox.Show("Error while writing:" & vbNewLine & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Next
        lblStatus.Text = ""
        pbImport.Visible = False
    End Sub

    Sub Progress(p As Integer, Message As String)
        pbImport.Value = p
        lblStatus.Text = Message
        Application.DoEvents()
    End Sub
    
    Private Sub frmImport_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Dim _xmlSettings = <settings>
        '                       <schedules path="F:\Software developer\Tablåer\Auto"></schedules>
        '                       <servers>
        '                           <server>sto-app60/trinity_mec</server>
        '                           <server>sto-app60/trinity_mc</server>
        '                           <server>sto-app60/trinity_maxus</server>
        '                           <server>sto-app60/trinity_ms</server>
        '                       </servers>
        '                   </settings>
        Dim _doc As XDocument
        Dim _xmlSettings As XElement        
        'If IO.File.Exists(IO.Path.Combine(My.Application.Info.DirectoryPath, "settings.xml")) Then
        lblPath.Text = My.Application.Info.DirectoryPath
        If IO.File.Exists(IO.Path.Combine(My.Application.Info.DirectoryPath, "settings.xml")) Then
            _doc = XDocument.Load(IO.Path.Combine(My.Application.Info.DirectoryPath, "settings.xml"))
            For Each _file In My.Computer.FileSystem.GetFiles(_doc.<settings>.<schedules>.@path, FileIO.SearchOption.SearchTopLevelOnly, "*.xml", "*.txt")
                lstFiles.Items.Add(_file)
            Next
            lstServers.Items.AddRange((From _server As XElement In _doc.<settings>.<servers>...<server> Select _server.Value).ToArray)
            '_xmlSettings = _doc.<settings>
        End If
    End Sub

    Private Sub cmdAddTV4_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddTV4.Click
        Dim _channels() As Integer = {1, 12, 25, 100, 101, 102, 103, 104}
        Dim _startDate As Date = Now.AddMonths(1)
        Dim _endDate As Date = Now.AddMonths(2)

        _startDate = _startDate.AddDays(-_startDate.Day + 1)
        _endDate = _endDate.AddDays(-_endDate.Day)

        If _startDate.DayOfWeek < DayOfWeek.Friday Then
            While _startDate.DayOfWeek <> DayOfWeek.Monday
                _startDate = _startDate.AddDays(-1)
            End While
        Else
            While _startDate.DayOfWeek <> DayOfWeek.Monday
                _startDate = _startDate.AddDays(1)
            End While
        End If
        If _endDate.DayOfWeek < DayOfWeek.Friday Then
            While _endDate.DayOfWeek <> DayOfWeek.Sunday
                _endDate = _endDate.AddDays(-1)
            End While
        Else
            While _endDate.DayOfWeek <> DayOfWeek.Sunday
                _endDate = _endDate.AddDays(1)
            End While
        End If

        For Each _channel In _channels
            Dim _url As String = String.Format("http://online.tv4.se/Release/XMLChart.aspx?channel={0}&startdate={1}&enddate={2}", _channel, Format(_startDate, "Short date"), Format(_endDate, "Short date"))
            lstFiles.Items.Add(_url)
        Next
    End Sub


End Class
