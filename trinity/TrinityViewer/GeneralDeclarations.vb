Module GeneralDeclarations
    Public LoggingIsOn As Boolean
    Public DBReader As New TrinityViewer.cDBReader

    Public Function CreateGUID() As String
        Return Guid.NewGuid.ToString
    End Function
End Module
