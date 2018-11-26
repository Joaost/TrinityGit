Public Class DebugStack
    Private _queue As New Collections.Generic.Queue(Of String)

    Private Shared _instance As DebugStack

    Protected Sub New()

    End Sub

    Private Shared Function Instance() As DebugStack
        If _instance Is Nothing Then
            _instance = New DebugStack
        End If
        Return _instance
    End Function

    Protected Sub _log(LogIt As String)
        If System.Threading.Thread.CurrentThread.IsBackground Then Exit Sub
        _queue.Enqueue(LogIt)
        If _queue.Count > 100 Then
            _queue.Dequeue()
        End If
    End Sub

    Protected Function _getLog() As List(Of String)
        Return _queue.ToList
    End Function

    Public Shared Sub Log(LogIt As String)
        Instance._log(LogIt)
    End Sub

    Public Shared Function GetLog() As List(Of String)
        Return Instance._getLog()
    End Function


End Class
