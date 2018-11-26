Module BalthazarModule
    Public WithEvents MyEvent As cEvent
    Public LoggingIsOn As Boolean = False
    Public Database As cDBReader
    Public LotusNotes As cNotes

    Private Sub MyEvent_SaveProgres(ByVal p As Integer) Handles MyEvent.SaveProgres
        frmProgress.Status = "Saving..."
        frmProgress.Progress = p
    End Sub

End Module
