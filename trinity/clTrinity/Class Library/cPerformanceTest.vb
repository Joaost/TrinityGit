Imports System
Imports System.IO
Imports System.Text
Imports System.Diagnostics.Stopwatch
Imports System.String
Imports Connect
Imports System.Windows.Forms

    Public Class cPerformanceTest

    Shared testFilePath As String = "c:\testfile.txt"
    Shared testFile As Stream
    Shared fs As FileStream

    Shared fileReadWriteStopWatch As New Stopwatch
    Shared adEdgeStopWatch As New Stopwatch
    Shared networkReadStopWatch As New Stopwatch

    Dim tmpFrm = New frmPerformance

    Public Shared Event Progress(ByVal percentage As Integer)


        Enum AdEdgeScores

        Poor = 30
        Adequate = 25
        Good = 20
        Excellent = 15

        End Enum

        Enum FileScores

        Poor = 25
        Adequate = 20
        Good = 15
        Excellent = 10

        End Enum

        Enum AllTestScores

            Poor = AdEdgeScores.Poor + FileScores.Poor
            Adequate = AdEdgeScores.Adequate + FileScores.Adequate
            Good = AdEdgeScores.Good + FileScores.Good
            Excellent = AdEdgeScores.Excellent + FileScores.Excellent

        End Enum


        Public Shared Sub WriteFile(ByVal fileSize As Long)

        Dim textChunks(100) As String
        Dim i As Integer

            For i = 1 To 100
                textChunks(i) = StrDup(CType(fileSize / 100, Integer), "x")
            Next

            Using outFile As StreamWriter = New StreamWriter(testFilePath)
                For i = 1 To 100
                    outFile.Write(textChunks(i))
                    RaiseEvent Progress(i / 2)
                Next
                outFile.Close()
            End Using


            textChunks = Nothing

        End Sub

        Public Shared Sub ReadFile()

            Dim testData As String = Nothing

            Dim i As Integer = 100

            fs = File.OpenRead(testFilePath)

            Dim b(fs.Length / 100) As Byte

            Dim temp As UTF8Encoding = New UTF8Encoding(True)

            Do While fs.Read(b, 0, b.Length) > 0
            testData = temp.GetString(b)
                RaiseEvent Progress(i / 2)
                i += 1
            Loop

            fs.Close()
            testData = Nothing

        End Sub

    Public Shared Function ReadWriteTest() As FileScores

        frmProgress.Show()

        fileReadWriteStopWatch.Start()
        WriteFile(100000000)
        ReadFile()
        fileReadWriteStopWatch.Stop()

        'MessageBox.Show("File read & write took " & fileReadWriteStopWatch.Elapsed.Seconds.ToString)

        frmProgress.Close()

        Select Case fileReadWriteStopWatch.Elapsed.Seconds
            Case Is <= FileScores.Excellent
                Return FileScores.Excellent
            Case Is <= FileScores.Good
                Return FileScores.Good
            Case Is <= FileScores.Adequate
                Return FileScores.Adequate
            Case Is <= FileScores.Poor
                Return FileScores.Poor
            Case Else
                Return FileScores.Poor
        End Select

    End Function

    Public Shared Function AdvantEdgeTest() As AdEdgeScores

        Dim x As New Connect.Brands
        Dim adEdgeStopWatch As New Stopwatch

        Dim z As Integer
        'Dim I As Integer
        'Dim s As String

        Dim benchMarkCountry As String = "SE" 'Sätt den här till kanske värdet på mvarArea
        Dim benchMarkChannel As String = Nothing 'Sätter den här sedan beroende på värdet på benchMarkCountry

        frmProgress.Show()

        z = x.setArea(TrinitySettings.DefaultArea)

        Select Case TrinitySettings.DefaultArea
            Case "SE"
                benchMarkChannel = "TV4"
            Case "DK"
                benchMarkChannel = "TV2 dk"
            Case "NO"
                benchMarkChannel = "TV2 no"
            Case Else
                benchMarkChannel = "TV4"
        End Select

        z = x.setChannels(benchMarkChannel)

        x.setPeriod("07")
        z = x.setTargetMnemonic("3+", False)
        x.registerCallback(New CallBack)
        adEdgeStopWatch.Start()

        z = x.Run

        adEdgeStopWatch.Stop()

        x.unregisterCallback()

        x = Nothing

        'MessageBox.Show(adEdgeStopWatch.Elapsed.Seconds.ToString)

        frmProgress.Close()

        Select Case adEdgeStopWatch.Elapsed.Seconds
            Case Is <= AdEdgeScores.Excellent
                Return AdEdgeScores.Excellent
            Case Is <= AdEdgeScores.Good
                Return AdEdgeScores.Good
            Case Is <= AdEdgeScores.Adequate
                Return AdEdgeScores.Adequate
            Case Is <= AdEdgeScores.Poor
                Return AdEdgeScores.Poor
            Case Else
                Return AdEdgeScores.Poor
        End Select

    End Function

    Public Shared Sub AllTest()

        ReadWriteTest()
        'NetworkTest()
        AdvantEdgeTest()

    End Sub

    Public Sub New()
        tmpFrm.ShowDialog()
    End Sub

End Class


Class CallBack

    Implements Connect.ICallBack

    Public Shared Event Progress(ByVal percentage As Integer)

    Public Sub callback(ByVal p As Integer) Implements Connect.ICallBack.callback
        RaiseEvent Progress(p)
    End Sub
End Class