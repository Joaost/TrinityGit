Public Class cProduct
    Public ID As Integer
    Public Name As String
    Public Client As cClient
    Public InternalContacts As cContacts
    Public ExternalContacts As cContacts

    Public Overrides Function ToString() As String
        Return Name
    End Function

    Public Sub New(ByVal main As cEvent)
        InternalContacts = New cContacts(main)
        ExternalContacts = New cContacts(main)
    End Sub
End Class
