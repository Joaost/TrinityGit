Imports System.Xml
Imports System.Linq
Imports System.IO

Public Class Form2

    Dim peopleList As New List(Of cPerson)
    Dim DatabaseIDList As New Dictionary(Of String, Long)
    Dim DBConn As New System.Data.SqlClient.SqlConnection("Server=STO-APP60;Database=Trinity_mec;Uid=johanh;Pwd=turbo;")

    Public fileList As New List(Of String)

    Private Function FromTV4Date(ByVal DateString As String) As Date
        Dim returnDate As Date = DateSerial(DateString.Substring(0, 4), DateString.Substring(4, 2), DateString.Substring(6, 2)).AddHours(DateString.Substring(8, 2)).AddMinutes(DateString.Substring(10, 2))
        If returnDate.Hour < 6 Then
            Return returnDate.AddDays(-1)
        Else
            Return returnDate
        End If
    End Function

    Public Function getAllPeople() As System.Collections.Generic.List(Of cPerson)

        Dim Command As New SqlClient.SqlCommand
       
        Command.Connection = DBConn

        Dim PeopleList As New List(Of cPerson)
        Command.Connection = DBConn
        Command.CommandText = "SELECT * from people"

        Dim ds As New DataTable
        Dim rd As SqlClient.SqlDataReader
        Try
            rd = Command.ExecuteReader
        Catch ex As SqlClient.SqlException
            Debug.Print(ex.Message)
        End Try

        ds.Load(rd)

        For Each row As Object In ds.Rows
            DatabaseIDList.Add(row!name, row!id)
        Next
        Return PeopleList

    End Function

    Public Sub WriteToDB(ByVal Breaks As Breaks)

        Dim Databases As New List(Of String)
        'Databases.Add("Server=SE-SQL;Database=trinity_maxus;Uid=johanh;Pwd=turbo;")
        Databases.Add("Server=STO-APP60;Database=Trinity_mec;Uid=johanh;Pwd=turbo;")
        'Databases.Add("Server=STO-APP60;Database=Trinity_ms;Uid=johanh;Pwd=turbo;")

        For Each Database As String In Databases

            Dim DBConn As New System.Data.SqlClient.SqlConnection
            DBConn.ConnectionString = Database
            DBConn.Open()

            Dim Affected As Integer

            Dim SQLCmd As New SqlClient.SqlCommand
            SQLCmd.Connection = DBConn

            Dim SQL As String = "DELETE from events WHERE Channel='" & Breaks(1).Channel & "' and Date >= '" & Math.Floor(Breaks.StartDate.ToOADate) & "' and Date <= '" & Math.Floor(Breaks.EndDate.ToOADate) & "'"

            SQLCmd.CommandText = SQL

            Try
                Affected = SQLCmd.ExecuteNonQuery()
            Catch ex As SqlClient.SqlException
                System.Windows.Forms.MessageBox.Show(ex.Message)
            End Try

            Dim Item As Integer = 0

            For Each Break As Break In Breaks
                Dim SB As New System.Text.StringBuilder
                SB.Append("INSERT into events (ID,Channel,Date,Time, " & _
                          "StartMam, Duration, Name, ChanEst, Price, " & _
                          "Type,SubType,IsLocal,EstimationPeriod,DateString, " & _
                          "UseCPP,Addition,EstimationTarget,Area,Comment) VALUES (")
                SB.Append("'" & Break.ID & "',")
                SB.Append("'" & Break.Channel & "',")
                SB.Append("'" & Math.Floor(Break.AirDate.ToOADate).ToString & "',")
                SB.Append("'" & Break.TimeString & "',")

                SB.Append(Break.MaM & ",")
                SB.Append(Break.Duration & ",")
                SB.Append("'" & Strings.Left(Break.Title.Replace("'", ""), 25) & "',")
                SB.Append("'" & Break.Rating & "',")
                SB.Append("'" & Break.Price & "',")

                SB.Append("'" & 0 & "',")
                SB.Append("'" & 0 & "',")
                SB.Append("'" & Convert.ToByte(Break.IsRegional) & "',")
                SB.Append("'" & "-4w" & "',")
                SB.Append("'" & "0" & "',")

                SB.Append("'" & Convert.ToByte(True) & "',")
                SB.Append("'" & "0" & "',")
                SB.Append("'" & "12-59" & "',")
                SB.Append("'" & "SE" & "',")
                SB.Append("'" & "None" & "')")

                Item += 1

                SQLCmd = New SqlClient.SqlCommand
                SQLCmd.Connection = DBConn
                SQLCmd.CommandText = SB.ToString
                Try
                    Affected = SQLCmd.ExecuteNonQuery()
                Catch ex As SqlClient.SqlException
                    System.Windows.Forms.MessageBox.Show(ex.Message & vbNewLine & _
                                                        Break.AirDate & " " & Break.Title)

                End Try

            Next

            DBConn.Close()
        Next

    End Sub

    Private Sub ReadMTGText(ByVal filename As String, ByVal Channel As String)

        Dim fileReader As IO.FileStream
        Dim BreakString() As String
        Dim thesebreaks As New Breaks
        Dim lastBreak As Break = Nothing
        Dim item As Integer = 0

        Try
            fileReader = New IO.FileStream(filename, IO.FileMode.Open)
        Catch ex As IO.IOException
            MessageBox.Show(ex.Message)
            fileReader.Close()
            fileReader = Nothing
            Exit Sub
        End Try

        Dim fileStream As New IO.StreamReader(fileReader)

        Dim BreakList As New List(Of String())

        While (Not fileStream.EndOfStream)
            Dim tmpBreakString() As String = Strings.Split(fileStream.ReadLine, vbTab)
            BreakList.Add(tmpBreakString)
        End While

        BreakList.Reverse()

        For Each BreakString In BreakList
            If BreakString(0) <> "" AndAlso BreakString(1) <> "" Then
                Try
                    Dim thisBreak As New Break
                    With thisBreak
                        .Channel = Channel
                        .AirDate = DateSerial(BreakString(0).Substring(0, 4), BreakString(0).Substring(5, 2), BreakString(0).Substring(8, 2)).AddHours(BreakString(1).Substring(0, 2)).AddMinutes(BreakString(1).Substring(3, 2))
                        .Title = BreakString(2)
                        .Rating = 0 'Convert.ToDecimal(BreakString(4))
                        .IsRegional = False
                        If BreakString.Length > 5 AndAlso BreakString(5) <> "" Then
                            .Price = Convert.ToDouble(BreakString(5))
                        Else
                            .Price = 0
                        End If
                        If lastBreak Is Nothing Then
                            .Duration = 0
                        Else
                            .Duration = DateDiff(DateInterval.Minute, thisBreak.AirDate, lastBreak.AirDate)
                        End If
                    End With
                    thesebreaks.Add(thisBreak)
                    lastBreak = thisBreak
                    item += 1
                    Debug.Print(item)
                Catch ex As Exception
                    MessageBox.Show(ex.Message & ", " & item)
                    Exit Sub

                End Try
            End If
        Next

        fileStream.Close()
        fileReader.Close()
        fileStream = Nothing
        fileReader = Nothing

        WriteToDB(thesebreaks)

    End Sub

    Private Sub ReadP7S1Text(ByVal filename As String, ByVal Channel As String)

        Dim fileReader As IO.FileStream
        Dim BreakString() As String
        Dim tmpBreakString() As String
        Dim theseBreaks As New Breaks
        Dim lastBreak As Break = Nothing

        Try
            fileReader = New IO.FileStream(filename, IO.FileMode.Open)
        Catch ex As IO.IOException
            MessageBox.Show(ex.Message)
            fileReader.Close()
            fileReader = Nothing
            Exit Sub
        End Try

        Dim fileStream As New IO.StreamReader(fileReader)

        Dim BreakList As New List(Of String())

        While (Not fileStream.EndOfStream)
            tmpBreakString = Strings.Split(fileStream.ReadLine, vbTab)
            BreakList.Add(tmpBreakString)
        End While

        BreakList.Reverse()

        For Each BreakString In BreakList

            Dim thisBreak As New Break
            With thisBreak
                .Channel = Channel
                .AirDate = DateSerial(BreakString(0).Substring(0, 4), BreakString(0).Substring(5, 2), BreakString(0).Substring(8, 2)).AddHours(BreakString(1).Substring(0, 2)).AddMinutes(BreakString(1).Substring(3, 2))
                .Title = BreakString(2)
                .Rating = Convert.ToDecimal(BreakString(4))
                .IsRegional = False
                .Price = Convert.ToDouble(BreakString(6))
                If lastBreak Is Nothing Then
                    .Duration = 0
                Else
                    .Duration = DateDiff(DateInterval.Minute, thisBreak.AirDate, lastBreak.AirDate)
                End If
            End With
            theseBreaks.Add(thisBreak)
            lastBreak = thisBreak
        Next

        fileStream.Close()
        fileReader.Close()
        fileStream = Nothing
        fileReader = Nothing

        WriteToDB(theseBreaks)

    End Sub

    Private Sub ReadXML(ByVal fileName As String)

        Dim theseBreaks As New Breaks
        Dim lastBreak As Break = Nothing

        Dim XMLSchedule As New XmlDocument
        XMLSchedule.Load(fileName)

        Dim line As Integer = 1


        For i As Integer = XMLSchedule.SelectNodes("tv/programme").Count - 1 To 0 Step -1
            Dim Break As XmlNode = XMLSchedule.SelectNodes("tv/programme").Item(i)
            'Next
            'For Each Break As XmlNode In XMLSchedule.SelectNodes("tv/programme")
            Dim tmpBreak = New Break
            With tmpBreak
                Try
                    .Channel = Break.Attributes("channel").Value
                    .AirDate = FromTV4Date(Break.Attributes("start").Value)
                    .IsRegional = Break.Attributes("is_regional").Value = "1"
                    .Title = Break.SelectSingleNode("title").InnerText
                    If lastBreak Is Nothing Then
                        .Duration = 0
                    Else
                        .Duration = DateDiff(DateInterval.Minute, .AirDate, lastBreak.AirDate)
                    End If

                    If Break.SelectSingleNode("estimate/rating").InnerText <> "" Then .Rating = Convert.ToDecimal(Break.SelectSingleNode("estimate/rating").InnerText.Replace(".", ","))
                    If Break.SelectSingleNode("estimate/price").InnerText <> "" Then .Price = Convert.ToDouble(Break.SelectSingleNode("estimate/price").InnerText)
                    If Break.SelectSingleNode("estimate/cpp").InnerText <> "" Then .CPP = Convert.ToDouble(Break.SelectSingleNode("estimate/cpp").InnerText)
                    theseBreaks.Add(tmpBreak)
                    lastBreak = tmpBreak
                Catch
                    System.Windows.Forms.MessageBox.Show("Error on item " & line & " in " & fileName)
                    Exit Sub
                End Try
            End With
            line += 1
        Next

        WriteToDB(theseBreaks)


    End Sub
    Private Sub PictureBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TV4Box.DragDrop

        fileList.AddRange(e.Data.GetData(DataFormats.FileDrop))

        For Each file As String In fileList
            ReadXML(file)
        Next

    End Sub

    Private Sub PictureBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TV4Box.DragEnter

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    'TV3
    Private Sub PictureBox2_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TV3Box.DragDrop

        fileList.AddRange(e.Data.GetData(DataFormats.FileDrop))

        For Each file As String In fileList
            ReadMTGText(file, "TV3")
        Next

    End Sub

    Private Sub PictureBox2_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TV3Box.DragEnter

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    'TV6
    Private Sub TV6Box_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TV6Box.DragDrop

        fileList.AddRange(e.Data.GetData(DataFormats.FileDrop))

        For Each file As String In fileList
            ReadMTGText(file, "TV6")
        Next

    End Sub

    Private Sub TV6Box_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TV6Box.DragEnter

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    'TV8
    Private Sub TV8Box_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TV8Box.DragDrop

        fileList.AddRange(e.Data.GetData(DataFormats.FileDrop))

        For Each file As String In fileList
            ReadMTGText(file, "TV8")
        Next

    End Sub

    Private Sub TV8_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TV8Box.DragEnter

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    'TV10
    Private Sub TV10_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TV10Box.DragDrop

        fileList.AddRange(e.Data.GetData(DataFormats.FileDrop))

        For Each file As String In fileList
            ReadMTGText(file, "TV10")
        Next

    End Sub

    Private Sub TV10_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TV10Box.DragEnter

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    'Kanal 5
    Private Sub PictureBox3_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles K5Box.DragDrop

        fileList.AddRange(e.Data.GetData(DataFormats.FileDrop))

        For Each file As String In fileList
            ReadP7S1Text(file, "Kanal 5")
        Next

    End Sub

    Private Sub PictureBox3_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles K5Box.DragEnter

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub K9Box_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles K9Box.DragDrop

        fileList.AddRange(e.Data.GetData(DataFormats.FileDrop))

        For Each file As String In fileList
            ReadP7S1Text(file, "Kanal 9")
        Next

    End Sub

    Private Sub K9Box_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles K9Box.DragEnter

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub


    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        For Each Control As Windows.Forms.Control In Me.Controls
            Control.AllowDrop = True
        Next
    End Sub

    Private Sub AdEdgeBox_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles AdEdgeBox.DragDrop

        fileList.AddRange(e.Data.GetData(DataFormats.FileDrop))

        For Each file As String In fileList
            ConnectShovel(file)
        Next

    End Sub

    Private Sub AdEdgeBox_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles AdEdgeBox.DragEnter

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub ConnectShovel(ByVal filename As String)
        Dim fs As New IO.FileStream("directories.txt", IO.FileMode.Open)
        Dim fr As New IO.StreamReader(fs)
        Dim dict As New Dictionary(Of String, String)
        Dim file As String = Strings.Right(filename, filename.Length - 1 - filename.LastIndexOf("\"))
        Dim failLog As New System.Text.StringBuilder
        Dim successLog As New System.Text.StringBuilder

        While Not fr.EndOfStream
            Dim line() As String = Strings.Split(fr.ReadLine, vbTab)
            dict.Add(line(0), line(1))
        End While
        For Each item As KeyValuePair(Of String, String) In dict
            Try
                System.IO.File.Copy(filename, item.Value & file, True)
                successLog.Append("Copy to " & item.Key & " was successful!" & vbNewLine)
            Catch ex As IO.IOException
                failLog.Append("Failed to copy to " & item.Key & " - " & ex.Message & vbNewLine)
            End Try
        Next
        MessageBox.Show(successLog.ToString & failLog.ToString, "Results")
    End Sub


    Private Sub CampaignBox_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles CampaignBox.DragDrop

        fileList.AddRange(e.Data.GetData(DataFormats.FileDrop))
        Dim list As List(Of String) = FileHelper.GetFilesRecursive(fileList.Item(0))

        Dim Progress As Decimal = 100 / list.Count
        Dim Value As Decimal = 0
        Dim XMLDoc As Xml.XmlDocument
        Dim Counter = 0

        Dim stopWatch As New Stopwatch
        Dim averageTime As Double

        'peopleList = getAllPeople()

        prgProgress.Value = 0

        stopWatch.Start()
        For Each Campaign As String In list
            Dim tmpCmp As New CampaignEssentials
            Counter += 1
            XMLDoc = New Xml.XmlDocument
            Try
                XMLDoc.Load(Campaign)
                tmpCmp.originalLocation = Campaign.Replace("""", "").Replace("'", "")
                tmpCmp.CampaignID = XMLDoc.SelectSingleNode("Campaign").Attributes("ID").Value
                tmpCmp.name = XMLDoc.SelectSingleNode("Campaign").Attributes("Name").Value
                tmpCmp.startdate = Date.FromOADate(XMLDoc.SelectSingleNode("Campaign/Channels/Channel/BookingTypes/BookingType/Weeks/Week").Attributes("StartDate").Value)
                tmpCmp.enddate = Date.FromOADate(XMLDoc.SelectSingleNode("Campaign/Channels/Channel/BookingTypes/BookingType/Weeks").LastChild.Attributes("EndDate").Value)
                tmpCmp.client = XMLDoc.SelectSingleNode("Campaign").Attributes("ClientID").Value
                tmpCmp.product = XMLDoc.SelectSingleNode("Campaign").Attributes("ProductID").Value
                tmpCmp.status = XMLDoc.SelectSingleNode("Campaign").Attributes("Status").Value
                tmpCmp.planner = XMLDoc.SelectSingleNode("Campaign").Attributes("Planner").Value
                tmpCmp.buyer = XMLDoc.SelectSingleNode("Campaign").Attributes("Buyer").Value
                tmpCmp.maintarget = XMLDoc.SelectSingleNode("Campaign/MainTarget").Attributes("Name").Value
                tmpCmp.secondtarget = XMLDoc.SelectSingleNode("Campaign/SecondaryTarget").Attributes("Name").Value
                tmpCmp.thirdtarget = XMLDoc.SelectSingleNode("Campaign/ThirdTarget").Attributes("Name").Value
                tmpCmp.originalfilechangeddate = File.GetLastWriteTime(Campaign)
                If Not SaveCampaign(tmpCmp, XMLDoc) Then
                    'Stop
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
            End Try

            averageTime = stopWatch.ElapsedMilliseconds / 1000 / Counter

            Value += Progress
            lblCampaignsleft.Text = list.Count - Counter
            lblTimeleft.Text = (averageTime * (list.Count - Counter) \ 60) & "m " & (averageTime * (list.Count - Counter) Mod 60) & "s"
            prgProgress.Value = Value
            Application.DoEvents()

        Next
        stopWatch.Stop()
        MessageBox.Show("Finished!")
    End Sub


    Function SaveCampaign(ByVal Camp As CampaignEssentials, ByVal XML As System.Xml.XmlDocument) As Boolean
        Dim Command As New SqlClient.SqlCommand
        Dim DBConn As New System.Data.SqlClient.SqlConnection
        DBConn.ConnectionString = "Server=STO-APP60;Database=Trinity_mec;Uid=johanh;Pwd=turbo;"
        DBConn.Open()
        Command.Connection = DBConn

        'Command.CommandText = "SELECT id from campaigns WHERE id = '" & Camp.id & "'"
        'Dim dt As New DataTable
        'Dim rd As SqlClient.SqlDataReader
        'rd = Command.ExecuteReader
        'dt.Load(rd)
        'If dt.Rows.Count = 0 Then

        Try
            Command.CommandText = "INSERT INTO campaigns VALUES('" & Camp.CampaignID & "','" & Camp.name & "','" & _
                                    Camp.startdate & "','" & Camp.enddate & "'," & _
                                    Camp.client & "," & Camp.product & ",'" & Camp.maintarget & "','" & _
                                    Camp.secondtarget & "','" & Camp.thirdtarget & "','" & _
                                    GetDatabaseIDForName(Camp.planner) & "','" & GetDatabaseIDForName(Camp.buyer) & "',@xml, GETDATE(),GETDATE(),'" & Camp.originalLocation & "','" & _
                                    Camp.originalfilechangeddate & "',0,'" & Camp.status & "','','')"

            Command.Parameters.Add("@xml", SqlDbType.Xml)
            Command.Parameters("@xml").Value = XML.OuterXml.ToString
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try

        Try
            Command.ExecuteNonQuery()
            Return True
        Catch ex As SqlClient.SqlException
            Debug.Print(ex.Message)
            Return False
        End Try
        'Else
        'Command.CommandText = "UPDATE campaigns SET name = '" & Camp.name & "', startdate='" & Camp.startdate & "',enddate='" & Camp.enddate & _
        '"',client=" & Camp.client & ",product=" & Camp.product & ",maintarget='" & Camp.maintarget & "',secondtarget='" & Camp.secondtarget & "'," & _
        '"thirdtarget='" & Camp.thirdtarget & "',planner=" & Camp.planner & ",buyer=" & Camp.buyer & ",xml=@xml,lastsaved=GETDATE(),originallocation='" & _
        'Camp.originalLocation & "',originalfilechangeddate='" & Camp.originalfilechangeddate & "' WHERE id='" & Camp.id & "'"
        'Command.Parameters.Add("@xml", SqlDbType.Xml)
        'Command.Parameters("@xml").Value = XML.OuterXml.ToString
        'Try
        '    Command.ExecuteNonQuery()
        '    Return True
        'Catch ex As SqlClient.SqlException
        '    Windows.Forms.MessageBox.Show("Could not save campaign " & Camp.name & ". Error message: " & vbNewLine & ex.Message)
        '    Return False
        'End Try
        'End If

    End Function

    Public Function GetDatabaseIDForName(ByVal name As String) As Long

        If DatabaseIDList.ContainsKey(name) Then
            Return DatabaseIDList(name)
        Else
            DBConn.Close()
            DBConn.Open()
            Dim Command As SqlClient.SqlCommand
            Command = New SqlClient.SqlCommand
            Command.Connection = DBConn

            Command.CommandText = "INSERT into people values('" & name & "','','')"

            Try
                Command.ExecuteNonQuery()
                Command.CommandText = "SELECT @@IDENTITY FROM people"
                Dim DBID As Long = Command.ExecuteScalar()
                DatabaseIDList.Add(name, DBID)
                Return DBID
            Catch ex As SqlClient.SqlException
                Debug.Print(ex.Message)
                DBConn.Close()
            End Try
        End If

    End Function


    Private Sub CampaignBox_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles CampaignBox.DragEnter

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub CampaignBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CampaignBox.Click

    End Sub
End Class

Public Class Break

    Public ID As Long
    Public CampaignID As String
    Public Channel As String
    Public AirDate As Date
    Public IsRegional As Boolean
    Public Title As String
    Public Rating As Decimal
    Public Price As Double
    Public CPP As Double
    Public Duration As Integer

    Public ReadOnly Property MaM() As Integer
        Get
            Return AirDate.Hour * 60 + AirDate.Minute
        End Get
    End Property

    Public ReadOnly Property TimeString() As String
        Get
            Dim Hours As Integer = Me.AirDate.Hour
            Dim Minutes As Integer = Me.AirDate.Minute
            If Hours < 6 Then
                Hours += 24
            End If

            Return Hours.ToString.PadLeft(2, "0") & Minutes.ToString.PadLeft(2, "0")
        End Get
    End Property
    Public Sub New()
        ID = Guid.NewGuid.ToString
    End Sub

End Class

Public Class Breaks
    Implements IEnumerable

    Private mCol As Collection

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return mCol.GetEnumerator
    End Function

    Public Sub Add(ByVal Break As Break)
        mCol.Add(Break, Break.ID)
    End Sub

    Public Sub New()
        mCol = New Collection
    End Sub

    Public Function StartDate() As Date
        Dim Smallest As Date = DateSerial(3000, 12, 31)
        For Each item As Break In mCol
            If item.AirDate < Smallest Then Smallest = item.AirDate
        Next
        Return Smallest
    End Function

    Public Function EndDate() As Date
        Dim Largest As Date = DateSerial(2000, 12, 31)
        For Each item As Break In mCol
            If item.AirDate > Largest Then Largest = item.AirDate
        Next
        Return Largest
    End Function

End Class

Public Class FileHelper

    ''' <summary>
    ''' This method starts at the specified directory, and traverses all subdirectories.
    ''' It returns a List of those directories.
    ''' </summary>
    Public Shared Function GetFilesRecursive(ByVal initial As String) As List(Of String)
        ' This list stores the results.
        Dim result As New List(Of String)

        ' This stack stores the directories to process.
        Dim stack As New Stack(Of String)

        ' Add the initial directory
        stack.Push(initial)

        ' Continue processing for each stacked directory
        Do While (stack.Count > 0)
            ' Get top directory string
            Dim dir As String = stack.Pop
            Try
                ' Add all immediate file paths
                result.AddRange(Directory.GetFiles(dir, "*.cmp"))

                ' Loop through all subdirectories and add them to the stack.
                Dim directoryName As String
                For Each directoryName In Directory.GetDirectories(dir)
                    stack.Push(directoryName)
                Next

            Catch ex As Exception
            End Try
        Loop

        ' Return the list
        Return result
    End Function

    

End Class

Public Class CampaignEssentials
    Public _ID As Integer
    Public _name As String
    Public CampaignID As String
    Public Year As Integer
    Public Month As Integer
    Public client As Integer
    Public product As Integer
    Public planner As String
    Public buyer As String
    Public lastopened As Date
    Public lastsaved As Date
    Public startdate As Date
    Public enddate As Date
    Public maintarget As String
    Public secondtarget As String
    Public thirdtarget As String
    Public originalLocation As String
    Public originalfilechangeddate As Date
    Public status As String


    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Property id() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property


End Class

Public Class cPerson
    Private _id As Integer
    Private _name As String
    Public phone As String
    Public email As String

    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
End Class
